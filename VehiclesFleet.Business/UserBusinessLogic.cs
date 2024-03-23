using VehiclesFleet.BusinessLogic.Contracts;
using VehiclesFleet.Domain.Dtos.IdentityControllerDtos;
using VehiclesFleet.Domain.Models;
using VehiclesFleet.Repository.Contracts;

namespace VehiclesFleet.BusinessLogic;

public class UserBusinessLogic : IUserBusinessLogic
{
    private readonly IUserRepository userRepository;

    public UserBusinessLogic(IUserRepository userRepository)
    {
        this.userRepository = userRepository;
    }

    public async Task<List<User>> GetAllUsers()
    {
        return await userRepository.GetAllUsers();
    }

    public async Task<string> Login(LoginDto loginDto)
    {
        await loginDto.ValidateAndThrow();
        return await userRepository.Login(loginDto);
    }

    public async Task Register(RegisterDto registerDto)
    {
        await registerDto.ValidateAndThrow();
        await userRepository.Register(registerDto);
    }
}