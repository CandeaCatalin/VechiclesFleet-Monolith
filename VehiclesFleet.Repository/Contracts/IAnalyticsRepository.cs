using VehiclesFleet.Domain.Models.Analysis;

namespace VehiclesFleet.Repository.Contracts;

public interface IAnalyticsRepository
{
    Task<IList<VehicleAnalysis>> GetAnalyticsForVehicle(Guid vehicleId);
}