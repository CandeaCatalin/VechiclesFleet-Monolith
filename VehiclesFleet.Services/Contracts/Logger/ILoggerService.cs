using VehiclesFleet.Domain.Models;

namespace VehiclesFleet.Services.Contracts.Logger;

public interface ILoggerService
{
    Task LogInfo(LoggerMessage message, string? token);
    Task LogError(LoggerMessage message);
}