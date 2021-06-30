using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Net.Http.Headers;
using Microsoft.OpenApi.Models;
using SiteAvailabilityMonitoring.Api.BackgroundServices;
using SiteAvailabilityMonitoring.Api.Options;
using SiteAvailabilityMonitoring.DataAccess;
using SiteAvailabilityMonitoring.DataAccess.Implementations;
using SiteAvailabilityMonitoring.Domain;
using SiteAvailabilityMonitoring.Domain.DataAccessPoint;

namespace SiteAvailabilityMonitoring.Api
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services
                .ConfigureDatabase(_configuration)
                .ConfigureMediatr();

            services
                .AddSingleton<IWebsiteRepository, WebsiteRepository>()
                .AddHttpClient<WebsiteCheckerClient>();

            services.Configure<CheckerOptions>(_configuration.GetSection("CheckerOptions"));

            services.AddHostedService<CheckerBackgroundService>();

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo {Title = "Site Availability Monitoring", Version = "v1"});
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment()) app.UseDeveloperExceptionPage();

            ConfigureSwagger(app);
            
            app.UseRouting();

            app.UseCors(policy => policy.WithOrigins("https://localhost:5001","http://localhost:5000")
                .AllowAnyMethod()
                .WithHeaders(HeaderNames.ContentType));
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private static void ConfigureSwagger(IApplicationBuilder app)
        {
            const string swaggerBasePath = "api";

            app.UseSwagger(c => { c.RouteTemplate = $"{swaggerBasePath}" + "/swagger/{documentName}/swagger.json"; });
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint($"/{swaggerBasePath}/swagger/v1/swagger.json", $"APP API");
                c.RoutePrefix = $"{swaggerBasePath}/swagger";
            });
        }
    }
}