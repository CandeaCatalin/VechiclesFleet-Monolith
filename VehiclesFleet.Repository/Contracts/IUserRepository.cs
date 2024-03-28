using VehiclesFleet.Domain.Dtos.IdentityControllerDtos;
using VehiclesFleet.Domain.Models.Identity;

namespace VehiclesFleet.Repository.Contracts;

public interface IUserRepository
{
    public Task<string> Login(LoginDto loginDto);
    public Task Register(RegisterDto registerDto);

    public Task<List<User>> GetAllUsers();
}