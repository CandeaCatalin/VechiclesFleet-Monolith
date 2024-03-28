using Microsoft.EntityFrameworkCore;
using VehiclesFleet.DataAccess;
using VehiclesFleet.Domain.Models.Vehicle;
using VehiclesFleet.Repository.Contracts;
using VehiclesFleet.Repository.Contracts.Mappers;
using Vehicle = VehiclesFleet.Domain.Models.Vehicle.Vehicle;
using VehicleError = VehiclesFleet.Domain.Models.Vehicle.VehicleError;

namespace VehiclesFleet.Repository;

public class VehicleRepository: IVehicleRepository
{
    private readonly DataContext dataContext;
    private readonly IVehicleMapper vehicleMapper;

    public VehicleRepository(DataContext dataContext,IVehicleMapper vehicleMapper)
    {
        this.dataContext = dataContext;
        this.vehicleMapper = vehicleMapper;
    }

    public async Task<DataAccess.Entities.Vehicle> AddVehicle(Vehicle vehicle)
    {
        var vehicleToAdd = vehicleMapper.DomainToDataAccess(vehicle);
        
        if (vehicle.UserId is null)
        {
            vehicleToAdd.UserId = new Guid();
        }
        else
        {
            vehicleToAdd.UserId =new Guid(vehicle.UserId);

        }

        await dataContext.Vehicles.AddAsync(vehicleToAdd);
        await dataContext.SaveChangesAsync();

        return vehicleToAdd;
    }

    public async Task<DataAccess.Entities.Vehicle> EditVehicle(Vehicle vehicle)
    {
        var vehicleToUpdate =await dataContext.Vehicles.FindAsync(vehicle.Id);
        if (vehicleToUpdate is null)
        {
            throw new NullReferenceException($"The vehicle with Id:{vehicle.Id} does not exist");
        }

        vehicleToUpdate.Brand = vehicle.Brand;
        vehicleToUpdate.TotalKilometers = vehicle.TotalKilometers;
        vehicleToUpdate.Color = vehicle.Color;
        vehicleToUpdate.Status = vehicle.Status;
        vehicleToUpdate.Model = vehicle.Model;
        vehicleToUpdate.PRTExpirationDate = vehicle.PRTExpirationDate;
        vehicleToUpdate.Year = vehicle.Year;
        vehicleToUpdate.Type = vehicle.Type;

        if (vehicle.UserId is null)
        {
            vehicleToUpdate.UserId = new Guid();
        }
        else
        {
            vehicleToUpdate.UserId =new Guid(vehicle.UserId);

        }
        
        dataContext.Update(vehicleToUpdate);
        
        await dataContext.SaveChangesAsync();

        return vehicleToUpdate;
    }

    public async Task<Guid> RemoveVehicle(Guid vehicleId)
    {
        var vehicleInDb =await dataContext.Vehicles.FindAsync(vehicleId);
        if (vehicleInDb is null)
        {
            throw new NullReferenceException($"The vehicle with Id:{vehicleId} does not exist");
        }

        dataContext.Vehicles.Remove(vehicleInDb);
       
        await dataContext.SaveChangesAsync();

        return vehicleId;
    }

    public async Task<IList<Vehicle>> GetVehicles(bool allVehicles)
    {
        var vehicles =await dataContext.Vehicles.Include(v=>v.User).Where(v => allVehicles == true || v.UserId != null).ToListAsync();

        var domainVehicles = new List<Vehicle>();

        foreach (var v in vehicles)
        {
            domainVehicles.Add(vehicleMapper.DataAccessToDomain(v));
        }

        return domainVehicles;
    }

    public async Task ChangeUserToVehicle(Guid userId, Guid vehicleId)
    {
        var vehicle =await dataContext.Vehicles.FirstOrDefaultAsync(v => v.Id == vehicleId);
        if (vehicle is null)
        {
            throw new Exception($"Vehicle with id:{vehicleId} not found!");
        }
        vehicle.UserId = userId;
        dataContext.Vehicles.Update(vehicle);
        await dataContext.SaveChangesAsync();
    }

    public async Task AddErrorsToVehicle(IList<Error> errorsList, Guid vehicleId)
    {
        var vehicle =await dataContext.Vehicles.Include(v=>v.Errors).FirstOrDefaultAsync(v => v.Id == vehicleId);
        if (vehicle is null)
        {
            throw new Exception($"Vehicle with id:{vehicleId} not found!");
        }
        
        IList<DataAccess.Entities.VehicleError> errorsToAdd = new List<DataAccess.Entities.VehicleError>();
       
        
        foreach (var error in errorsList)
        {
            var errorToAdd = new DataAccess.Entities.VehicleError
            {
                Id = Guid.NewGuid(),
                ErrorName = error.ToString(),
                VehicleId = vehicleId,
                CreationDate = DateTime.Now,
                FixDate = DateTime.MinValue,
                IsFixed = false
            };
            errorsToAdd.Add(errorToAdd);
        }
        if (errorsToAdd.Count()+ vehicle.Errors.Count(e => e.IsFixed != true) > 3)
        {
            vehicle.Status = VehicleStatus.NotFunctional.ToString();
        }else if(errorsToAdd.Count()+ vehicle.Errors.Count(e => e.IsFixed != true) >0)
        {
            vehicle.Status = VehicleStatus.Damaged.ToString();
        }

        var errorsOfVehicle = await dataContext.VehiclesErrors.Where(v => v.VehicleId == vehicleId).ToListAsync();
        errorsOfVehicle.AddRange(errorsToAdd);
        vehicle.Errors = errorsOfVehicle;
        await dataContext.VehiclesErrors.AddRangeAsync(errorsToAdd);
        dataContext.Vehicles.Update(vehicle);
        await dataContext.SaveChangesAsync();
    }

    public async Task<IList<VehicleError>> GetVehicleErrors(Guid vehicleId)
    {
       var dataAccessErrors = await dataContext.VehiclesErrors.Where(v => v.VehicleId == vehicleId).ToListAsync();
       var domainErrors = new List<VehicleError>();

       foreach (var e in dataAccessErrors)
       {
           domainErrors.Add(vehicleMapper.ErrorDataAccessToDomain(e));
       }

       return domainErrors;
    }

    public async Task FixErrorForVehicle(Guid errorId, Guid vehicleId)
    {
        var vehicle =await dataContext.Vehicles.Include(v=>v.Errors).FirstOrDefaultAsync(v => v.Id == vehicleId);
        if (vehicle is null)
        {
            throw new Exception($"Vehicle with id:{vehicleId} not found!");
        }

        var error = await dataContext.VehiclesErrors.FirstOrDefaultAsync(v =>
            v.VehicleId == vehicleId && v.Id == errorId);
        
        if (error is null)
        {
            throw new Exception($"Error with id:{errorId} not found!");
        }
        
        error.FixDate = DateTime.Now;
        error.IsFixed = true;
       
        var totalErrorsUnFixed = 0;
        foreach (var err in vehicle.Errors)
        {
            if (err.IsFixed == false)
            {
                totalErrorsUnFixed++;
            }
        }

        if (totalErrorsUnFixed > 3)
        {
            vehicle.Status = VehicleStatus.NotFunctional.ToString();
        }else if (totalErrorsUnFixed > 0)
        {
            vehicle.Status = VehicleStatus.Damaged.ToString();
        }
        else if(totalErrorsUnFixed == 0)
        {
            vehicle.Status = VehicleStatus.Functional.ToString();
        }

        dataContext.Update(error);
        dataContext.Update(vehicle);
        await dataContext.SaveChangesAsync();
    }
}