using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VehiclesFleet.BusinessLogic.Contracts;
using VehiclesFleet.Domain.Dtos.IdentityControllerDtos;

namespace VehiclesFleet.Controllers;

[ApiController]
[Route("auth")]
public class IdentityController : ControllerBase
{
    private readonly IUserBusinessLogic userBusinessLogic;

    public IdentityController(IUserBusinessLogic userBusinessLogic)
    {
        this.userBusinessLogic = userBusinessLogic;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDto dto)
    {
        var token = await userBusinessLogic.Login(dto);
        return Ok(new { Token = token });
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterDto dto)
    {
        await userBusinessLogic.Register(dto);
        return Ok();
    }
    
    [HttpGet("checkSession")]
    [Authorize]
    public  IActionResult CheckSession()
    {
        return Ok();
    }

    [HttpGet("GetAllUsers")]
    
    [Authorize]
    public async Task<IActionResult> GetAllUsers()
    {
        var allUsersList = await userBusinessLogic.GetAllUsers();
        return Ok(allUsersList);
    }
}