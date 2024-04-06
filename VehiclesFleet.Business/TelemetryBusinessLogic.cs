using VehiclesFleet.BusinessLogic.Contracts;
using VehiclesFleet.Domain.Dtos.TelemetryControllerDtos;
using VehiclesFleet.Domain.Models.Logger;
using VehiclesFleet.Domain.Models.Vehicle;
using VehiclesFleet.Repository.Contracts;
using VehiclesFleet.Services.Contracts;
using VehiclesFleet.Services.Contracts.Logger;

namespace VehiclesFleet.BusinessLogic;

public class TelemetryBusinessLogic: ITelemetryBusinessLogic
{
    private readonly ITelemetryRepository telemetryRepository;
    private readonly IVehicleRepository vehicleRepository;
    private readonly IAnalysisService analysisService;
    private readonly ILoggerService loggerService;
    
    public TelemetryBusinessLogic(ITelemetryRepository telemetryRepository,IVehicleRepository vehicleRepository, ILoggerService loggerService,IAnalysisService analysisService)
    {
        this.telemetryRepository = telemetryRepository;
        this.vehicleRepository = vehicleRepository;
        this.loggerService = loggerService;
        this.analysisService = analysisService;
    }
    
    public async Task AddTelemetry(AddTelemetryDto dto, string? token)
    {
        await dto.ValidateAndThrow();

        var telemetry = new VehicleTelemetry
        {
            VehicleId = dto.VehicleId,
            ActualSpeed = dto.ActualSpeed,
            KilometersSinceStart = dto.KilometersSinceStart,
            Latitude = dto.Latitude,
            Longitude = dto.Longitude,
            Fuel = dto.Fuel,
            TirePressure = dto.TirePressure
        };

        var telemetryId =  await telemetryRepository.AddTelemetry(telemetry);
        await vehicleRepository.AddErrorsToVehicle(dto.Errors,dto.VehicleId);
        if (dto.ActualSpeed == 0 && dto.KilometersSinceStart != 0)
        {
            await analysisService.GenerateAnalysisForVehicle(dto.VehicleId);
        }
        await loggerService.LogInfo(new LoggerMessage
        {
            Message = $"Telemetry with Id: {telemetryId} was added for vehicle {dto.VehicleId}"
        }, token);
        
    }
    
}