namespace VehiclesFleet.Services.Contracts;

public interface ILoggerService
{
    Task LogInfo(string message);
    Task LogError(string message);
}