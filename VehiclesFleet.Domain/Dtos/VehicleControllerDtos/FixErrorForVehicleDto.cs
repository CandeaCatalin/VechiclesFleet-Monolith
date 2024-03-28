namespace VehiclesFleet.Domain.Dtos.VehicleControllerDtos;

public class FixErrorForVehicleDto
{
    public Guid VehicleId { get; set; }
    public Guid ErrorId { get; set; }
}