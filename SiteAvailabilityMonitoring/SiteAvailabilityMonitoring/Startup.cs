using FluentMigrator.Runner;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using SiteAvailabilityMonitoring.DataAccess.Base;
using SiteAvailabilityMonitoring.DataAccess.Implementations;
using SiteAvailabilityMonitoring.DataAccess.Migrations;
using SiteAvailabilityMonitoring.Domain;
using SiteAvailabilityMonitoring.Domain.Contracts;
using SiteAvailabilityMonitoring.Domain.DataAccessPoint;

namespace SiteAvailabilityMonitoring
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
            ConfigureDatabase(services);
            
            services.AddSingleton<IWebsiteRepository, WebsiteRepository>();
            services.AddSingleton<IWebsiteManager, WebsiteManager>();
            services.AddHttpClient<WebsiteCheckerClient>();

            services.AddControllers();
            
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                c.RoutePrefix = string.Empty;
            });

            app.UseRouting();
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }

        private void ConfigureDatabase(IServiceCollection services)
        {
            services.AddSingleton<IConnectionFactory>(
                new ConnectionFactory(_configuration.GetConnectionString("NpgsqlDatabase")));
            
            services.AddFluentMigratorCore()
                .ConfigureRunner(builder =>
                {
                    builder.AddPostgres();
                    builder.WithGlobalConnectionString(_configuration.GetConnectionString("NpgsqlDatabase"));
                    builder.ScanIn(typeof(InitMigration).Assembly).For.Migrations();
                })
                .AddLogging(builder => builder.AddFluentMigratorConsole());
        }
    }
}