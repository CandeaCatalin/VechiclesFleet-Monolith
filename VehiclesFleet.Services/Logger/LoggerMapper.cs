using VehiclesFleet.DataAccess.Entities;
using VehiclesFleet.Domain.Models;
using VehiclesFleet.Services.Contracts.Logger;

namespace VehiclesFleet.Services.Logger;

public class LoggerMapper:ILoggerMapper
{
    public Log LogDataAccessFromDomain(LoggerMessage loggerMessage,LogStatus logStatus)
    {
        return new Log
        {
            Id = Guid.NewGuid(),
            Message = loggerMessage.Message,
            UserEmail = loggerMessage.UserEmail,
            Status = logStatus.ToString(),
            CreateTime = DateTime.Now
        };
    }
}