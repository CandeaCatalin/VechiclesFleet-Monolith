using VehiclesFleet.DataAccess.Entities;

namespace VehiclesFleet.Repository.Contracts.Mappers;

public interface IUserMapper
{
    public Domain.Models.User DataAccessToDomain(User user);
}