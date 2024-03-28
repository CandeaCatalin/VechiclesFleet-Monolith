using VehiclesFleet.DataAccess.Entities;

namespace VehiclesFleet.Repository.Contracts.Mappers;

public interface IUserMapper
{
    public Domain.Models.Identity.User DataAccessToDomain(User user);
}