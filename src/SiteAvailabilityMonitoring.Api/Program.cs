using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Scalar.AspNetCore;
using SiteAvailabilityMonitoring.Api.BackgroundServices;
using SiteAvailabilityMonitoring.Api.Options;
using SiteAvailabilityMonitoring.DataAccess;
using SiteAvailabilityMonitoring.DataAccess.Implementations;
using SiteAvailabilityMonitoring.Domain;
using SiteAvailabilityMonitoring.Domain.DataAccessPoint;

var builder = WebApplication.CreateBuilder();

builder.Services.AddOpenApi();

builder.Services
    .ConfigureDatabase(builder.Configuration)
    .ConfigureMediatr();

builder.Services
    .AddSingleton<IWebsiteRepository, WebsiteRepository>()
    .AddHttpClient<CheckWebsiteAvailabilityClient>();

builder.Services.Configure<CheckerOptions>(builder.Configuration.GetSection("CheckerOptions"));

builder.Services.AddHostedService<CheckerBackgroundService>();

builder.Services.AddControllers();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseRouting();
app.MapControllers();

app.Run();