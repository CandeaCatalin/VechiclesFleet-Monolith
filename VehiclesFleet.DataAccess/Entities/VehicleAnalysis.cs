namespace VehiclesFleet.DataAccess.Entities;

public class VehicleAnalysis
{
    public Guid Id { get; set; }
    public Guid VehicleId { get; set; }
    public string UserId { get; set; }
    public User? User { get; set; }
    public Vehicle? Vehicle { get; set; }
    public int MinimumSpeed { get; set; }
    public int MaximumSpeed { get; set; }
    public double AverageSpeed { get; set; }
    public double FuelConsumption { get; set; }
    public bool HasTierPressureAnomaly { get; set; }
    public int TotalKilometersPassed { get; set; }
}