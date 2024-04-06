namespace VehiclesFleet.Domain.Models.Analysis;

public class VehicleAnalysis
{
    public Guid Id { get; set; }
    public Guid VehicleId { get; set; }
    public string UserId { get; set; }
    public int MinimumSpeed { get; set; }
    public int MaximumSpeed { get; set; }
    public double AverageSpeed { get; set; }
    public double FuelConsumption { get; set; }
    public bool HasTierPressureAnomaly { get; set; }
    public int TotalKilometersPassed { get; set; }
}