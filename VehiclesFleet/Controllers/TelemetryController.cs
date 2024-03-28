using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using VehiclesFleet.BusinessLogic.Contracts;
using VehiclesFleet.Domain.Dtos.TelemetryControllerDtos;

namespace VehiclesFleet.Controllers;

[ApiController]
[Route("telemetry")]
public class TelemetryController: ControllerBase
{
    private readonly ITelemetryBusinessLogic telemetryBusinessLogic;

    public TelemetryController(ITelemetryBusinessLogic telemetryBusinessLogic)
    {
        this.telemetryBusinessLogic = telemetryBusinessLogic;
    }
    
    [HttpPost("add")]
    public async Task<IActionResult> AddTelemetry(AddTelemetryDto dto)
    {
        var token = GetToken();
        await telemetryBusinessLogic.AddTelemetry(dto,token);
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