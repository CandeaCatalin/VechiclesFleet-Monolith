using VehiclesFleet.Domain.Models.Vehicle;

namespace VehiclesFleet.Repository.Contracts;

public interface ITelemetryRepository
{
    public Task<Guid> AddTelemetry(VehicleTelemetry telemetry);
}