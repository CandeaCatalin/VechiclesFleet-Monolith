using FluentValidation;
using VehiclesFleet.Domain.Dtos.Validators.VehicleDtoValidators;

namespace VehiclesFleet.Domain.Dtos.VehicleControllerDtos;

public class RemoveVehicleDto
{
    public static RemoveVehicleDtoValidator Validator => new();
    public string VehicleId { get; set; }
    
    public async Task ValidateAndThrow()
    {
        await Validator.ValidateAndThrowAsync(this);
    }
}