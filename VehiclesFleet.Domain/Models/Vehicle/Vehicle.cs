namespace VehiclesFleet.Domain.Models.Vehicle;

public class Vehicle
{
    public Guid Id { get; set; }
    public string? UserId { get; set; }
    public string Type { get; set; }
    public string Model { get; set; }
    public int TotalKilometers { get; set; } = 0;
    public string Brand { get; set; }
    public string Color { get; set; }
    public string Status { get; set; }
    public DateTime PRTExpirationDate { get; set; }
    public string Year {get;set;}
}