using Microsoft.AspNetCore.Identity;

namespace VehiclesFleet.DataAccess.Entities;

public class User : IdentityUser
{
    public DateTime CreatedAtTimeUtc { get; set; }
    public string? Name { get; set; }
}