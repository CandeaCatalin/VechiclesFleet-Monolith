using FluentValidation;
using VehiclesFleet.Domain.Dtos.VehicleControllerDtos;

namespace VehiclesFleet.Domain.Dtos.Validators.VehicleDtoValidators;

public class RemoveVehicleDtoValidator : AbstractValidator<RemoveVehicleDto>
{
    public RemoveVehicleDtoValidator()
    {
        RuleFor(x => x.VehicleId).NotEmpty().WithMessage("Provide a correct id format!");
    }
}