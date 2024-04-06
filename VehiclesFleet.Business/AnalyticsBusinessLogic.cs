using VehiclesFleet.BusinessLogic.Contracts;
using VehiclesFleet.Domain.Models.Analysis;
using VehiclesFleet.Repository.Contracts;

namespace VehiclesFleet.BusinessLogic;

public class AnalyticsBusinessLogic:IAnalyticsBusinessLogic
{
    private readonly IAnalyticsRepository analyticsRepository;

    public AnalyticsBusinessLogic(IAnalyticsRepository analyticsRepository)
    {
        this.analyticsRepository = analyticsRepository;
    }
    public async Task<IList<VehicleAnalysis>> GetAnalyticsForVehicle(Guid vehicleId)
    {
        return await analyticsRepository.GetAnalyticsForVehicle(vehicleId);
    }
}