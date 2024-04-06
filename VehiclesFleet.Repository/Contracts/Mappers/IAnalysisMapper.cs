using VehiclesFleet.DataAccess.Entities;

namespace VehiclesFleet.Repository.Contracts.Mappers;

public interface IAnalysisMapper
{
    
    Domain.Models.Analysis.VehicleAnalysis DataAccessToDomain(VehicleAnalysis dataAccessVehicleAnalytics);
}