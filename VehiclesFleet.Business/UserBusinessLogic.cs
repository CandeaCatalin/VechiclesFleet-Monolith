using VehiclesFleet.BusinessLogic.Contracts;
using VehiclesFleet.Domain.Dtos.IdentityControllerDtos;
using VehiclesFleet.Domain.Models.Identity;
using VehiclesFleet.Domain.Models.Logger;
using VehiclesFleet.Repository.Contracts;
using VehiclesFleet.Services.Contracts.Logger;

namespace VehiclesFleet.BusinessLogic;

public class UserBusinessLogic : IUserBusinessLogic
{
    private readonly IUserRepository userRepository;
    private readonly ILoggerService loggerService;

    public UserBusinessLogic(IUserRepository userRepository,ILoggerService loggerService)
    {
        this.userRepository = userRepository;
        this.loggerService = loggerService;
    }

    public async Task<List<User>> GetAllUsers()
    {
        return await userRepository.GetAllUsers();
    }

    public async Task<string> Login(LoginDto loginDto)
    {
        await loginDto.ValidateAndThrow();
        var token = await userRepository.Login(loginDto);
        await loggerService.LogInfo(new LoggerMessage
        {
            Message = "User logged in!"
        }, token);
        return token;
    }

    public async Task Register(RegisterDto registerDto)
    {
        await registerDto.ValidateAndThrow();
        await userRepository.Register(registerDto);
        await loggerService.LogInfo(new LoggerMessage
        {
            Message = "New user was registered successfully!"
        }, null);
    }
}