using VehiclesFleet.DataAccess.Entities;
using VehiclesFleet.Domain.Models;

namespace VehiclesFleet.Services.Contracts.Logger;

public interface ILoggerMapper
{
    public Log LogDataAccessFromDomain(LoggerMessage loggerMessage,LogStatus logStatus);
}