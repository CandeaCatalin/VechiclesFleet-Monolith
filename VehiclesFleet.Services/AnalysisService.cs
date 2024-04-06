using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using VehiclesFleet.DataAccess;
using VehiclesFleet.DataAccess.Entities;
using VehiclesFleet.Services.Contracts;

namespace VehiclesFleet.Services;

public class AnalysisService : IAnalysisService
{
    public IServiceProvider serviceProvider;

    public AnalysisService(IServiceProvider serviceProvider)
    {
        this.serviceProvider = serviceProvider;
    }

    public async Task GenerateAnalysisForVehicle(Guid vehicleId)
    {
        using (var scope = serviceProvider.CreateScope())
        {
            var dataContext = scope.ServiceProvider.GetRequiredService<DataContext>();

            var vehicle = await dataContext.Vehicles.Include(v => v.User).FirstOrDefaultAsync(v => v.Id == vehicleId);

            if (vehicle is null)
            {
                throw new Exception("Vehicle not found!");
            }

            var telemetriesForVehicle = await dataContext.VehicleTelemetries.Where(vt => vt.VehicleId == vehicleId)
                .OrderBy(vt => vt.CreateAt).ToListAsync();

            var eligibleTelemetries = GetEligibleTelemetries(telemetriesForVehicle);

            if (eligibleTelemetries.Count() != 0)
            {
                var vehicleAnalysis = new VehicleAnalysis
                {
                    Id = Guid.NewGuid(),
                    VehicleId = vehicleId,
                    UserId = vehicle.UserId.ToString(),
                    Vehicle = vehicle,
                    MinimumSpeed = GetMinimumSpeed(eligibleTelemetries),
                    MaximumSpeed = GetMaximumSpeed(eligibleTelemetries),
                    AverageSpeed = GetAverageSpeed(eligibleTelemetries),
                    FuelConsumption = GetFuelConsumption(eligibleTelemetries),
                    HasTierPressureAnomaly = ArePressureAnomalies(eligibleTelemetries),
                    TotalKilometersPassed = telemetriesForVehicle.LastOrDefault()!.KilometersSinceStart
                };
                await dataContext.VehiclesAnalysis.AddAsync(vehicleAnalysis);
                await dataContext.SaveChangesAsync();
            }
        }
    }

    private IList<VehicleTelemetry> GetEligibleTelemetries(IList<VehicleTelemetry> telemetries)
    {
        var filteredList = telemetries.Where(v => v.ActualSpeed == 0).ToList();
        if (filteredList.Count >= 2)
        {
            var last = filteredList.LastOrDefault();
            var first = filteredList.ElementAt(filteredList.Count - 2);
         
            int lastIndex =
                telemetries.IndexOf(last); 
            int secondLastIndex = telemetries.IndexOf(first);

            if (lastIndex != -1 && secondLastIndex != -1)
            {
            
                int startIndex = Math.Min(lastIndex, secondLastIndex);
                int endIndex = Math.Max(lastIndex, secondLastIndex);

                return telemetries.Skip(startIndex).Take(endIndex - startIndex + 1)
                    .ToList();
            }
        }

        return new List<VehicleTelemetry>();
    }
    
    private bool ArePressureAnomalies(IList<VehicleTelemetry> telemetries)
    {
        var tirePressure = telemetries.Select(x => x.TirePressure).ToList();
        return tirePressure.Max() - tirePressure.Min() >= 10;
    }

    private int GetMinimumSpeed(IList<VehicleTelemetry> telemetries)
    {
        return telemetries.Where(e => e.ActualSpeed != 0).Min(e => e.ActualSpeed);
    }
    
    private int GetMaximumSpeed(IList<VehicleTelemetry> telemetries)
    {
        return telemetries.Where(e => e.ActualSpeed != 0).Max(e => e.ActualSpeed);
    }

    private double GetAverageSpeed(IList<VehicleTelemetry> telemetries)
    {
        return telemetries.Where(t=>t.ActualSpeed != 0)
            .Average(e => e.ActualSpeed);
    }
    
    private double GetFuelConsumption(IList<VehicleTelemetry> telemetries)
    {
        var fuels = telemetries.Select(x => x.Fuel).ToList();
                    
        var maxFuel = fuels.ElementAt(0);
        var minFuel = maxFuel;
        var fuelConsumption = 0.0;
        foreach (var f in fuels)
        {
            if (f > minFuel)
            {
                fuelConsumption = (double)(maxFuel - minFuel);
                maxFuel = f;
                minFuel = f;
            }
            else
            {
                minFuel = f;
            }
        }

        if (fuelConsumption == 0)
        {
            fuelConsumption = (double)(maxFuel - minFuel);
        }

        return fuelConsumption;
    }
}