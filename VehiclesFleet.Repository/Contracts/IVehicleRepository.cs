using VehiclesFleet.Domain.Models.Vehicle;

namespace VehiclesFleet.Repository.Contracts;

public interface IVehicleRepository
{
    Task<DataAccess.Entities.Vehicle> AddVehicle(Vehicle vehicle);
    Task<DataAccess.Entities.Vehicle> EditVehicle(Vehicle vehicle);
    Task<Guid> RemoveVehicle(Guid vehicleId);
    Task<IList<Vehicle>> GetVehicles(bool allVehicles);
    Task ChangeUserToVehicle(Guid UserId, Guid VehicleId);
    Task AddErrorsToVehicle(IList<Error> errorsList,Guid VehicleId);
    Task<IList<VehicleError>> GetVehicleErrors(Guid vehicleId);
    Task FixErrorForVehicle(Guid errorId, Guid vehicleId);

}