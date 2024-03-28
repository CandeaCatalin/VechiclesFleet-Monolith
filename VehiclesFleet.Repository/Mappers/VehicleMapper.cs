using VehiclesFleet.DataAccess.Entities;
using VehiclesFleet.Repository.Contracts.Mappers;
using Vehicle = VehiclesFleet.DataAccess.Entities.Vehicle;

namespace VehiclesFleet.Repository.Mappers;

public class VehicleMapper:IVehicleMapper
{
    public Vehicle DomainToDataAccess(Domain.Models.Vehicle.Vehicle domainVehicle)
    {
        return new Vehicle
        {
            Id = Guid.NewGuid(),
            Type = domainVehicle.Type,
            Model = domainVehicle.Model,
            TotalKilometers = domainVehicle.TotalKilometers,
            Brand = domainVehicle.Brand,
            Color = domainVehicle.Color,
            Status = domainVehicle.Status,
            PRTExpirationDate = domainVehicle.PRTExpirationDate,
            Year = domainVehicle.Year,
            Errors = new List<VehicleError>()
        };
    }

    public Domain.Models.Vehicle.Vehicle DataAccessToDomain(Vehicle dataAccessVehicle)
    {
        return new Domain.Models.Vehicle.Vehicle
        {
            Id = dataAccessVehicle.Id,
            UserId = dataAccessVehicle.UserId.ToString(),
            Type = dataAccessVehicle.Type,
            Model = dataAccessVehicle.Model,
            TotalKilometers = dataAccessVehicle.TotalKilometers,
            Brand = dataAccessVehicle.Brand,
            Color = dataAccessVehicle.Color,
            Status = dataAccessVehicle.Status,
            PRTExpirationDate = dataAccessVehicle.PRTExpirationDate,
            Year = dataAccessVehicle.Year,
        };
    }

    public Domain.Models.Vehicle.VehicleError ErrorDataAccessToDomain(VehicleError dataAccessError)
    {
        return new Domain.Models.Vehicle.VehicleError
        {
            Id = dataAccessError.Id,
            ErrorName = dataAccessError.ErrorName,
            VehicleId = dataAccessError.VehicleId,
            CreationDate = dataAccessError.CreationDate,
            FixDate = dataAccessError.FixDate,
            IsFixed = dataAccessError.IsFixed
        };
    }
}