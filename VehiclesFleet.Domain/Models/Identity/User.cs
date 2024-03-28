namespace VehiclesFleet.Domain.Models.Identity;

public class User
{
    public String Email { get; set; }
    public DateTime CreatedAtTimeUtc { get; set; }
    public string? Name { get; set; }
}