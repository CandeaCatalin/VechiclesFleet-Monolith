using VehiclesFleet.Domain.Dtos.TelemetryControllerDtos;

namespace VehiclesFleet.BusinessLogic.Contracts;

public interface ITelemetryBusinessLogic
{
    Task AddTelemetry(AddTelemetryDto dto,string? token);
}