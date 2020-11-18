using Events.Notification.Services.Infra.DB.Config;
using Events.Notification.Services.Infra.DB.Service;
using Events.Notification.Services.Infra.Messaging.Config;
using Events.Notification.Services.Infra.Messaging.Service;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace Events.Notification
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
            services.AddControllers();
            // Pass configuration from appsettings to Mongo DB service Configuration
            services.Configure<DatabaseConfig>(Configuration.GetSection("MongoDB"));
            services.AddSingleton<IDatabaseConfig>(sp => sp.GetRequiredService<IOptions<DatabaseConfig>>().Value);
            
            // Pass configuration from appsettings to RabbitMQ service Configuration
            services.Configure<EventMessagingConfig>(Configuration.GetSection("RabbitMq"));
            services.AddSingleton<IEventMessagingConfig>(sp => sp.GetRequiredService<IOptions<EventMessagingConfig>>().Value);
            
            // Inject Notification DB Service
            services.AddTransient<IEmailNotificationsDbService, EmailNotificationsDbService>();

            services.AddHostedService<EventConsumerService>();  

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

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
