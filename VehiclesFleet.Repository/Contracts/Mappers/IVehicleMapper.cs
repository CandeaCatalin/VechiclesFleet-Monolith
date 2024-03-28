using VehiclesFleet.DataAccess.Entities;

namespace VehiclesFleet.Repository.Contracts.Mappers;

public interface IVehicleMapper
{
    Vehicle DomainToDataAccess(Domain.Models.Vehicle.Vehicle domainVehicle);
    Domain.Models.Vehicle.Vehicle DataAccessToDomain(Vehicle dataAccessVehicle);
    Domain.Models.Vehicle.VehicleError ErrorDataAccessToDomain(VehicleError dataAccessError);
}