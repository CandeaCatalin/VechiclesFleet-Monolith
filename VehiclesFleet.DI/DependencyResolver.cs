using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Net.Http.Headers;
using Microsoft.OpenApi.Models;
using VehiclesFleet.BusinessLogic;
using VehiclesFleet.BusinessLogic.Contracts;
using VehiclesFleet.Constants;
using VehiclesFleet.DataAccess;
using VehiclesFleet.Repository;
using VehiclesFleet.Repository.Contracts;
using VehiclesFleet.Repository.Contracts.Mappers;
using VehiclesFleet.Repository.Mappers;
using VehiclesFleet.Services;
using VehiclesFleet.Services.Contracts;

namespace VehiclesFleet.DI;

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
        services.AddSingleton<IJwtService, JwtService>();
        services.AddSingleton<ILoggerService, LoggerService>();
        services.AddSingleton<IUserMapper, UserMapper>();
        // services.AddSingleton<IAuthorizationService, AuthorizationService>();

        services.AddScoped<IUserBusinessLogic, UserBusinessLogic>();
        services.AddScoped<IUserRepository, UserRepository>();

        services.AddDbContext<DataContext>(options =>
            options.UseMySql(GetConnectionString(services),ServerVersion.AutoDetect(GetConnectionString(services))));
        
        services.AddControllers();
        
        DoMigrations(services);
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

    private static void DoMigrations(IServiceCollection services)
    {
        var serviceProvider = services.BuildServiceProvider();
        using (var scope = serviceProvider.CreateScope())
        {
            var servicesproviders = scope.ServiceProvider;

            var context = servicesproviders.GetRequiredService<DataContext>();
            context.Database.Migrate();
        }
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