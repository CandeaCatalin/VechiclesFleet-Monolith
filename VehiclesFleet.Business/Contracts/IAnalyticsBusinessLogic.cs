using VehiclesFleet.Domain.Models.Analysis;

namespace VehiclesFleet.BusinessLogic.Contracts;

public interface IAnalyticsBusinessLogic
{
    public Task<IList<VehicleAnalysis>> GetAnalyticsForVehicle(Guid vehicleId);
    
}