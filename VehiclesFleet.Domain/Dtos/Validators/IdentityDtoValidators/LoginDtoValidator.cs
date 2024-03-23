using FluentValidation;
using VehiclesFleet.Domain.Dtos.IdentityControllerDtos;

namespace VehiclesFleet.Domain.Dtos.Validators.IdentityDtoValidators;

public class LoginDtoValidator : AbstractValidator<LoginDto>
{
    public LoginDtoValidator()
    {
        RuleFor(x => x.Email).NotEmpty().WithMessage("Please provide an email!");
        RuleFor(x => x.Password).NotEmpty().WithMessage("Please provide a password!");
    }
}