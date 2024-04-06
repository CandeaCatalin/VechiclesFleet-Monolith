using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VehiclesFleet.BusinessLogic.Contracts;
using VehiclesFleet.Domain.Models.Analysis;

namespace VehiclesFleet.Controllers;

[ApiController]
[Route("analythics")]
[Authorize]
public class AnalythicsController
{
    private readonly IAnalyticsBusinessLogic analyticsBusinessLogic;
    public AnalythicsController(IAnalyticsBusinessLogic analyticsBusinessLogic)
    {
        this.analyticsBusinessLogic = analyticsBusinessLogic;
    }
    
    
    [HttpPost("getForVehicle")]
    public async Task<IList<VehicleAnalysis>> GetAnalyticsForVehicle(Guid vehicleId)
    {
        return await analyticsBusinessLogic.GetAnalyticsForVehicle(vehicleId);
    }
}