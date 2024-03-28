using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using VehiclesFleet.BusinessLogic.Contracts;
using VehiclesFleet.Domain.Dtos.VehicleControllerDtos;

namespace VehiclesFleet.Controllers;

public class VehicleController: ControllerBase
{
    private readonly IVehicleBusinessLogic vehicleBusinessLogic;
    
    public VehicleController(IVehicleBusinessLogic vehicleBusinessLogic)
    {
        this.vehicleBusinessLogic = vehicleBusinessLogic;
    }

    [HttpGet("getAllVehicles")]
    public async Task<IActionResult> GetAllVehicles()
    {
        
        var vehicles = await vehicleBusinessLogic.GetVehicles(true);
        return Ok(vehicles);
    }
    
    [HttpGet("getErrorsForVehicle")]
    public async Task<IActionResult> GetErrorsForVehicle(GetVehicleErrorsDto dto)
    {
        var errors = await vehicleBusinessLogic.GetVehicleErrors(dto);
        return Ok(errors);
    }
    
    [HttpGet("getVehiclesWithDriverAssigned")]
    public async Task<IActionResult> GetVehiclesWithDriver()
    {
        var vehicles = await vehicleBusinessLogic.GetVehicles(false);
        return Ok(vehicles);
    }
    
    [HttpPost("add")]
    public async Task<IActionResult> AddVehicle(AddVehicleDto dto)
    {
        var token = GetToken();
        await vehicleBusinessLogic.AddVehicle(dto,token);
        return Ok();
    }
    
     [HttpPost("fixErrorForVehicle")]
    public async Task<IActionResult> FixErrorForVehicle(FixErrorForVehicleDto dto)
    {
        var token = GetToken();
        await vehicleBusinessLogic.FixErrorForVehicle(dto,token);
        return Ok();
    }
    
    [HttpPut("edit")]
    public async Task<IActionResult> EditVehicle(EditVehicleDto dto)
    {
        var token = GetToken();
        await vehicleBusinessLogic.EditVehicle(dto,token);
        return Ok();
    }
    
    [HttpPut("changeUser")]
    public async Task<IActionResult> ChangeUserToVehicle(ChangeUserDto dto)
    {
        var token = GetToken();
        await vehicleBusinessLogic.ChangeUserToVehicle(dto,token);
        return Ok();
    }
    [HttpPut("addErrors")]
    public async Task<IActionResult> AddErrorsToVehicle(AddErrorsToVehicleDto dto)
    {
        var token = GetToken();
        await vehicleBusinessLogic.AddErrorsToVehicle(dto,token);
        return Ok();
    }
    
    [HttpDelete("remove")]
    public async Task<IActionResult> RemoveVehicle(RemoveVehicleDto dto)
    {
        var token = GetToken();
        await vehicleBusinessLogic.RemoveVehicle(dto,token);
        return Ok();
    }

    private string? GetToken()
    {
        if (Request.Headers.TryGetValue("Authorization", out StringValues authHeaderValue))
        {
              var token = authHeaderValue.ToString().Replace("Bearer ", "");
              
            return token;
        }

        return null;
    }
}