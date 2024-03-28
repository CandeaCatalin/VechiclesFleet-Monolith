namespace VehiclesFleet.DataAccess.Entities;

public class Vehicle
{
    public Guid Id { get; set; }
    public Guid? UserId { get; set; }
    public User? User { get; set; }
    public string Type { get; set; }
    public string Model { get; set; }
    public int TotalKilometers { get; set; }
    public string Brand { get; set; }
    public string Color { get; set; }
    public string Status { get; set; }
    public DateTime PRTExpirationDate { get; set; }
    public string Year {get;set;}
    public IEnumerable<VehicleError> Errors { get; set; }
}