namespace VehiclesFleet.Services.Contracts;

public interface IAnalysisService
{
    Task GenerateAnalysisForVehicle(Guid vehicleId);
}