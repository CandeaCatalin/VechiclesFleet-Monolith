using VehiclesFleet.DataAccess.Entities;

namespace VehiclesFleet.Repository.Contracts.Mappers;

public interface ITelemetryMapper
{
    VehicleTelemetry DomainToDataAccess(Domain.Models.Vehicle.VehicleTelemetry telemetry);
}