using VehiclesFleet.DataAccess.Entities;

namespace VehiclesFleet.Services.Contracts;

public interface IJwtService
{
     string GenerateToken(User existingUser, IList<string> roles);
}