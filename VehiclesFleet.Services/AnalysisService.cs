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
            var filteredList = telemetriesForVehicle.Where(v => v.ActualSpeed == 0).ToList();

            if (filteredList.Count >= 2)
            {
                // Get the last two entities that meet the condition

                var last = filteredList.LastOrDefault();
                var first = filteredList.ElementAt(filteredList.Count -2);
                // Find the indices of the last two entities in the original list
                int lastIndex =
                    telemetriesForVehicle.IndexOf(last); // Index of the second-to-last entity
                int secondLastIndex = telemetriesForVehicle.IndexOf(first); // Index of the last entity

                if (lastIndex != -1 && secondLastIndex != -1)
                {
                    // Determine the range of indices for entities between the last two entities
                    int startIndex = Math.Min(lastIndex, secondLastIndex);
                    int endIndex = Math.Max(lastIndex, secondLastIndex);

                    // Extract all entities between the last two occurrences
                    var totalEntities = telemetriesForVehicle.Skip(startIndex).Take(endIndex - startIndex +1)
                        .ToList();
                    var entitiesBetween = telemetriesForVehicle.Skip(startIndex + 1).Take(endIndex - startIndex - 1)
                        .ToList();
                    var minimumSpeed = entitiesBetween.Where(e => e.ActualSpeed != 0).Min(e => e.ActualSpeed);
                    var maxSpeed = entitiesBetween.Where(e => e.ActualSpeed != 0).Max(e => e.ActualSpeed);
                    var averageSpeed = entitiesBetween
                        .Average(e => e.ActualSpeed);
                    var fuels = totalEntities.Select(x => x.Fuel);
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
                    
                    var vehicleAnalysis = new VehicleAnalysis
                    {
                        Id = Guid.NewGuid(),
                        VehicleId = vehicleId,
                        UserId = vehicle.UserId.ToString(),
                        Vehicle = vehicle,
                        MinimumSpeed = minimumSpeed,
                        MaximumSpeed = maxSpeed,
                        AverageSpeed = averageSpeed,
                        FuelConsumption = fuelConsumption,
                        HasTierPressureAnomaly = false,
                        TotalKilometersPassed = telemetriesForVehicle.LastOrDefault()!.KilometersSinceStart
                    };
                    await dataContext.VehiclesAnalysis.AddAsync(vehicleAnalysis);
                    await dataContext.SaveChangesAsync();

                }
            }
        }
    }
}