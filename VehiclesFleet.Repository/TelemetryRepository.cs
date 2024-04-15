using VehiclesFleet.DataAccess;
using VehiclesFleet.Domain.Models.Vehicle;
using VehiclesFleet.Repository.Contracts;
using VehiclesFleet.Repository.Contracts.Mappers;

namespace VehiclesFleet.Repository;

public class TelemetryRepository: ITelemetryRepository
{
    private readonly DataContext dataContext;
    private readonly ITelemetryMapper telemetryMapper;

    public TelemetryRepository(DataContext dataContext, ITelemetryMapper telemetryMapper)
    {
        this.dataContext = dataContext;
        this.telemetryMapper = telemetryMapper;
        
    }
    public async Task<Guid> AddTelemetry(VehicleTelemetry telemetry)
    {
        var telemetryToAdd = telemetryMapper.DomainToDataAccess(telemetry);
        await dataContext.VehicleTelemetries.AddAsync(telemetryToAdd);
        await dataContext.SaveChangesAsync();
        return telemetryToAdd.Id;
    }
}