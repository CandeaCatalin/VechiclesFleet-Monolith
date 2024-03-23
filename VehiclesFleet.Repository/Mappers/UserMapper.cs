                          
using VehiclesFleet.Domain.Models;
using VehiclesFleet.Repository.Contracts.Mappers;

namespace VehiclesFleet.Repository.Mappers;

public class UserMapper : IUserMapper
{
    public User DataAccessToDomain(DataAccess.Entities.User user)
    {
        return new User
        {
            Email = user.Email,
            Name = user.Name,
        };
    }
}