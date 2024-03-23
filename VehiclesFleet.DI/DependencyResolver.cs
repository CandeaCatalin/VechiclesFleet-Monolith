using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Net.Http.Headers;
using Microsoft.OpenApi.Models;
using VechiclesFleet.Constants;
using VechiclesFleet.Services;
using VechiclesFleet.Services.Contracts;
using VehiclesFleet.DataAccess;

namespace VechiclesFleet.DI;

public static class DependencyResolver
{
    public static IServiceCollection AddDependencies(this IServiceCollection services)
    {
        var configurationRoot = LoadConfiguration();
        services.AddSingleton(configurationRoot);
        services
            .AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
                options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                options.JsonSerializerOptions.WriteIndented = true;
            });
        RegisterSwaggerWithAuthorization(services);
        services.AddSingleton<IAppSettingsReader, AppSettingsReader>();
        services.AddSingleton<ILoggerService, LoggerService>();
        // services.AddSingleton<IAuthorizationService, AuthorizationService>();

        // services.AddScoped<ICustomMonitorBusinessLogic, CustomMonitorBusinessLogic>();

        services.AddDbContext<DataContext>(options =>
            options.UseMySql(GetConnectionString(services),ServerVersion.AutoDetect(GetConnectionString(services))));

        services.AddControllers();

        return services;
    }

    private static string GetConnectionString(IServiceCollection services)
    {
        var serviceProvider = services.BuildServiceProvider();
        var appReader = serviceProvider.GetService<IAppSettingsReader>();
        return appReader.GetValue(AppSettingsConstants.Section.Database, AppSettingsConstants.Keys.ConnectionString);
    }
    private static void RegisterSwaggerWithAuthorization(IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo {Title = "Vehicles Fleet API", Version = "v1"});
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Please enter the word Bearer following a space and token",
                Name = HeaderNames.Authorization,
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer"
            });
            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        },
                        Scheme = "Bearer",
                        Name = "Bearer",
                        In = ParameterLocation.Header,
                    },
                    new List<string>()
                }
            });
        });
    }
    
    private static IConfigurationRoot LoadConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", true, true);

        var configuration = builder.Build();
        return configuration;
    }
}