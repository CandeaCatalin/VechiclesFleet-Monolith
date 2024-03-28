using Microsoft.Extensions.DependencyInjection;
using VehiclesFleet.DataAccess;
using VehiclesFleet.Domain.Models.Logger;
using VehiclesFleet.Services.Contracts;
using VehiclesFleet.Services.Contracts.Logger;

namespace VehiclesFleet.Services.Logger;

public class LoggerService: ILoggerService
{
    private IServiceProvider serviceProvider;
    private ILoggerMapper loggerMapper;
    private IJwtService jwtService;
    
    public LoggerService(IServiceProvider serviceProvider,ILoggerMapper loggerMapper,IJwtService jwtService)
    {
        this.serviceProvider = serviceProvider;
        this.loggerMapper = loggerMapper;
        this.jwtService = jwtService;
        
    }

    public async Task LogInfo(LoggerMessage message, string? token)
    {
        if (message is null)
        {
            throw new Exception($"{message} is null!");
        }
     
        if (String.IsNullOrEmpty(message.Message))
        {
            throw new Exception("Cannot log an empty message!");
        }

        if (token is not null)
        {
            message.UserEmail = jwtService.GetUserEmailFromToken(token);
        }
        
        var log = loggerMapper.LogDataAccessFromDomain(message, LogStatus.Info);

        using (var scope = serviceProvider.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<DataContext>();
            await dbContext.Logs.AddAsync(log);
            await dbContext.SaveChangesAsync();
        }
    }

    public async Task LogError(LoggerMessage message)
    {
        if (message is null)
        {
            throw new Exception($"{message} is null!");
        }
     
        if (String.IsNullOrEmpty(message.Message))
        {
            throw new Exception("Cannot log an empty message!");
        }
        
        var log = loggerMapper.LogDataAccessFromDomain(message, LogStatus.Error);

        using (var scope = serviceProvider.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<DataContext>();
            await dbContext.Logs.AddAsync(log);
            await dbContext.SaveChangesAsync();
        }
    }
}