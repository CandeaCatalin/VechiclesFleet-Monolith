using VehiclesFleet.Domain.Dtos.IdentityControllerDtos;

namespace VehiclesFleet.Repository.Contracts;

public interface IUserRepository
{
    public Task<string> Login(LoginDto loginDto);
    public Task Register(RegisterDto registerDto);

    public Task<List<Domain.Models.User>> GetAllUsers();
}