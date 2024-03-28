using VehiclesFleet.Domain.Dtos.IdentityControllerDtos;
using VehiclesFleet.Domain.Models.Identity;

namespace VehiclesFleet.BusinessLogic.Contracts;

public interface IUserBusinessLogic
{
    public Task<string> Login(LoginDto loginDto);
    public Task Register(RegisterDto registerDto);
    public Task<List<User>> GetAllUsers();
}