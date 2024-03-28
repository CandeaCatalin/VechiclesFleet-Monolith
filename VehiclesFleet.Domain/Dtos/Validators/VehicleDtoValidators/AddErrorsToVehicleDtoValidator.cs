using FluentValidation;
using VehiclesFleet.Domain.Dtos.VehicleControllerDtos;

namespace VehiclesFleet.Domain.Dtos.Validators.VehicleDtoValidators;

public class AddErrorsToVehicleDtoValidator:AbstractValidator<AddErrorsToVehicleDto>
{
    public AddErrorsToVehicleDtoValidator()
    {
        RuleFor(v => v.VehicleId).NotEmpty().WithMessage("Error list must contain valid errors");
    }
}