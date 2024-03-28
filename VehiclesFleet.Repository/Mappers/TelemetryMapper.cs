using VehiclesFleet.DataAccess.Entities;
using VehiclesFleet.Repository.Contracts.Mappers;

namespace VehiclesFleet.Repository.Mappers;

public class TelemetryMapper:ITelemetryMapper
{
    public VehicleTelemetry DomainToDataAccess(Domain.Models.Vehicle.VehicleTelemetry telemetry)
    {
        return new VehicleTelemetry
        {
            Id = Guid.NewGuid(),
            VehicleId = telemetry.VehicleId,
            ActualSpeed = telemetry.ActualSpeed,
            KilometersSinceStart = telemetry.KilometersSinceStart,
            Latitude = telemetry.Latitude,
            Longitude = telemetry.Longitude,
            Fuel = telemetry.Fuel,
            TirePressure = telemetry.TirePressure
        };
    }
}