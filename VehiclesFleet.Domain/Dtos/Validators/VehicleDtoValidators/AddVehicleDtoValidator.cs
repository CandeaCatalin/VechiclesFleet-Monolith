using FluentValidation;
using VehiclesFleet.Domain.Dtos.VehicleControllerDtos;

namespace VehiclesFleet.Domain.Dtos.Validators.VehicleDtoValidators;

public class AddVehicleDtoValidator:AbstractValidator<AddVehicleDto>
{
    public AddVehicleDtoValidator()
    {
        RuleFor(x => x.Type).IsInEnum().WithMessage("Please provide a valid type from the list: Car, Truck,Van!");
        RuleFor(x => x.Model).NotEmpty().WithMessage("Please provide a valid vehicle model!");
        RuleFor(x => x.Brand).NotEmpty().WithMessage("Please provide a valid vehicle brand!");
        RuleFor(x => x.Color).NotEmpty().WithMessage("Please provide a color!");
        RuleFor(x => x.Status).IsInEnum().WithMessage("Please provide a valid status!");
        RuleFor(x => x.PRTExpirationDate).NotEmpty().WithMessage("Please provide a PRTE expiration date!");
        RuleFor(x => x.Year).NotEmpty().WithMessage("Please provide a valid year!");
    }
}
