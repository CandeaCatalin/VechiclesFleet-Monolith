using System.Text.RegularExpressions;
using FluentValidation;
using VehiclesFleet.Domain.Dtos.IdentityControllerDtos;

namespace VehiclesFleet.Domain.Dtos.Validators.IdentityDtoValidators;

public class RegisterDtoValidator : AbstractValidator<RegisterDto>
{
    public RegisterDtoValidator()
    {
        RuleFor(x => x.Email).EmailAddress().WithMessage("Please provide a valid email!");
        RuleFor(x => x.Password).Must(BeAValidPassword).WithMessage("Please provide a valid password!");
        RuleFor(x => x.ConfirmPassword).Must((model, confirmPassword) => confirmPassword == model.Password).WithMessage("Password must match!");
        RuleFor(x => x.Name).NotEmpty().WithMessage("Name is empty!");
    }
    private bool BeAValidPassword(string? password)
    {
        const string pattern = "^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$";
        Regex regex = new Regex(pattern);
        return password != null && regex.IsMatch(password);
    }
}