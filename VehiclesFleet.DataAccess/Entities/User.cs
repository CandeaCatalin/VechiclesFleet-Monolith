using Microsoft.AspNetCore.Identity;

namespace VehiclesFleet.DataAccess.Entities;

public class User : IdentityUser
{
    public DateTime CreatedAtTimeUtc { get; set; }
    public string? Name { get; set; }
    public Guid? VehicleId { get; set; }
    public Vehicle? Vehicle { get; set; }
}