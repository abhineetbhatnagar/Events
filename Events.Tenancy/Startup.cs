using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Events.Tenancy.Services.Infra.DB.Config;
using Events.Tenancy.Services.Core.Tenant;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Events.Tenancy.Services.Infra.JWT.Config;
using Events.Tenancy.Services.Infra.Encryption.Config;
using Events.Tenancy.Services.Infra.JWT;
using Events.Tenancy.Services.Infra.DB.Service;
using Events.Tenancy.Services.Infra.Encryption.Service;

namespace Events.Tenancy
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
                            .AddJsonFile($"appsettings.{hostEnvironment.EnvironmentName}.json", optional: true)
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

            // Pass configuration from appsettings to JWT service Configuration
            services.Configure<JwtConfig>(Configuration.GetSection("JWT"));
            services.AddSingleton<IJwtConfig>(sp => sp.GetRequiredService<IOptions<JwtConfig>>().Value);

            // Pass configuration from appsettings to Encryption service Configuration
            services.Configure<EncryptionConfig>(Configuration.GetSection("Encryption"));
            services.AddSingleton<IEncryptionConfig>(sp => sp.GetRequiredService<IOptions<EncryptionConfig>>().Value);

            // Inject Tenant, DB, Jwt & Encryption Service Dependencies
            services.AddScoped<ITenantService, TenantService>();
            services.AddScoped<ITenantDbService, TenantDbService>();
            services.AddScoped<IJwtService, JwtService>();
            services.AddScoped<IEncryptionService, EncryptionService>();
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
