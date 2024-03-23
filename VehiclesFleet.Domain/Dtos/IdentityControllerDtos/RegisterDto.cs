using FluentValidation;
using VehiclesFleet.Domain.Dtos.Validators.IdentityDtoValidators;

namespace VehiclesFleet.Domain.Dtos.IdentityControllerDtos;

public class RegisterDto
{
    public static RegisterDtoValidator Validator => new();
    public string? Name { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }
    public string? ConfirmPassword { get; set; }
    public async Task ValidateAndThrow()
    {
        await Validator.ValidateAndThrowAsync(this);
    }
}