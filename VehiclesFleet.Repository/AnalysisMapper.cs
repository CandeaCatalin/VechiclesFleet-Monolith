using VehiclesFleet.Domain.Models.Analysis;
using VehiclesFleet.Repository.Contracts.Mappers;

namespace VehiclesFleet.Repository;

public class AnalysisMapper:IAnalysisMapper
{
    public VehicleAnalysis DataAccessToDomain(DataAccess.Entities.VehicleAnalysis dataAccessVehicleAnalytics)
    {
        return new VehicleAnalysis
        {
            Id = dataAccessVehicleAnalytics.Id,
            VehicleId = dataAccessVehicleAnalytics.VehicleId,
            UserId = dataAccessVehicleAnalytics.UserId,
            MinimumSpeed = dataAccessVehicleAnalytics.MinimumSpeed,
            MaximumSpeed = dataAccessVehicleAnalytics.MaximumSpeed,
            AverageSpeed = dataAccessVehicleAnalytics.AverageSpeed,
            FuelConsumption = dataAccessVehicleAnalytics.FuelConsumption,
            HasTierPressureAnomaly = dataAccessVehicleAnalytics.HasTierPressureAnomaly,
            TotalKilometersPassed = dataAccessVehicleAnalytics.TotalKilometersPassed
        };
    }
}