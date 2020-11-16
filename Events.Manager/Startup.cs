using Events.Manager.Services.Core;
using Events.Manager.Services.Infra.DB.Config;
using Events.Manager.Services.Infra.Encryption.Config;
using Events.Manager.Services.Infra.Encryption.Service;
using Events.Manager.Services.Infra.Messaging.Config;
using Events.Tenancy.Services.Infra.DB.Service;
using Events.Tenancy.Services.Infra.Messaging.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;

namespace Events.Manager
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public readonly IWebHostEnvironment _HostEnvironment;
        public Startup(IConfiguration configuration, IWebHostEnvironment hostEnvironment)
        {
            Configuration = configuration;
            _HostEnvironment = hostEnvironment;

            var builder = new ConfigurationBuilder()
                            .SetBasePath(hostEnvironment.ContentRootPath)
                            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)                            
                            .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication(AuthScheme =>
            {
                AuthScheme.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                AuthScheme.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = Configuration["JWT:Issuer"],
                    ValidAudience = Configuration["JWT:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:Secret"])),
                    ClockSkew = TimeSpan.Zero
                };
            });
            services.AddControllers();

            // Pass configuration from appsettings to Mongo DB service Configuration
            services.Configure<DatabaseConfig>(Configuration.GetSection("MongoDB"));
            services.AddSingleton<IDatabaseConfig>(sp => sp.GetRequiredService<IOptions<DatabaseConfig>>().Value);

            // Pass configuration from appsettings to Encryption service Configuration
            services.Configure<EncryptionConfig>(Configuration.GetSection("Encryption"));
            services.AddSingleton<IEncryptionConfig>(sp => sp.GetRequiredService<IOptions<EncryptionConfig>>().Value);

            // Pass configuration from appsettings to RabbitMQ service Configuration
            services.Configure<EventMessagingConfig>(Configuration.GetSection("RabbitMq"));
            services.AddSingleton<IEventMessagingConfig>(sp => sp.GetRequiredService<IOptions<EventMessagingConfig>>().Value);


            // Inject Events & Participation Service Dependencies
            services.AddSingleton<IEventsService, EventsService>();
            services.AddSingleton<IParticipantsService, ParticipantsService>();
            
            // Inject Events & Participation DB Service Dependencies
            services.AddSingleton<IEventsDbService, EventsDbService>();
            services.AddSingleton<IParticipantsDbService, ParticipantsDbService>();
            
            // Inject Encryption Service
            services.AddSingleton<IEncryptionService, EncryptionService>();

            // Inject Messaging Service
            services.AddSingleton<IEventMessagingService, EventMessagingService>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseExceptionHandler(
                options =>
                {
                    options.Run(
                    async context =>
                    {
                        var ex = context.Features.Get<IExceptionHandlerFeature>();
                        if (ex != null)
                        {
                            await context.Response.WriteAsync(ex.Error.Message.ToString());
                        }
                    });
                }
            );

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
