using VehiclesFleet.DataAccess.Entities;

namespace VehiclesFleet.Services.Contracts;

public interface IJwtService
{
     string GenerateToken(User existingUser);
     string GetUserEmailFromToken(string token);
}