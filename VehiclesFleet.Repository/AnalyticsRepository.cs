using Microsoft.EntityFrameworkCore;
using VehiclesFleet.DataAccess;
using VehiclesFleet.Domain.Models.Analysis;
using VehiclesFleet.Repository.Contracts;
using VehiclesFleet.Repository.Contracts.Mappers;

namespace VehiclesFleet.Repository;

public class AnalyticsRepository:IAnalyticsRepository
{
    private readonly DataContext dataContext;
    private readonly IAnalysisMapper analysisMapper;
    public AnalyticsRepository(DataContext dataContext,IAnalysisMapper analysisMapper)
    {
        this.dataContext = dataContext;
        this.analysisMapper = analysisMapper;
    }

    public async Task<IList<VehicleAnalysis>> GetAnalyticsForVehicle(Guid vehicleId)
    {
        var dataAccessVehicles =  await dataContext.VehiclesAnalysis.Where(vh => vh.VehicleId == vehicleId).ToListAsync();
        var domainVehicles = new List<VehicleAnalysis>();

        foreach (var v in dataAccessVehicles)
        {
            domainVehicles.Add(analysisMapper.DataAccessToDomain(v));
        }

        return domainVehicles;
    }
}