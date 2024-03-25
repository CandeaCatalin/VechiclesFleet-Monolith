using System.ComponentModel.DataAnnotations;
using VehiclesFleet.Domain.Models;

namespace VehiclesFleet.DataAccess.Entities;

public class Log
{
    [Key] public Guid Id { get; set; }
    public string Message { get; set; }
    public string UserEmail { get; set; }
    public string Status { get; set; }
    public DateTime CreateTime { get; set; }
}