using FluentValidation;
using VehiclesFleet.Domain.Dtos.Validators.VehicleDtoValidators;
using VehiclesFleet.Domain.Models.Vehicle;

namespace VehiclesFleet.Domain.Dtos.VehicleControllerDtos;

public class AddErrorsToVehicleDto
{
    public static AddErrorsToVehicleDtoValidator Validator => new();

    public IList<Error> ErrorsList { get; set; }
    public string VehicleId { get; set; }

    public async Task ValidateAndThrow()
    {
        await Validator.ValidateAndThrowAsync(this);
    }
}