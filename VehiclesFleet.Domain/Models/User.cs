namespace VehiclesFleet.Domain.Models;

public class User
{
    public String Role { get; set; }
    public String Email { get; set; }
    public DateTime CreatedAtTimeUtc { get; set; }
    public string? Name { get; set; }
}