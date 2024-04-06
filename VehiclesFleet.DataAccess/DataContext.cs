using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using VehiclesFleet.DataAccess.Entities;

namespace VehiclesFleet.DataAccess;

public class DataContext : IdentityDbContext<User>
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.Entity<VehicleTelemetry>().Property(p => p.Latitude).HasColumnType("decimal(10,7)");
        builder.Entity<VehicleTelemetry>().Property(p => p.Longitude).HasColumnType("decimal(10,7)");
        builder.Entity<VehicleTelemetry>().Property(p => p.Fuel).HasColumnType("decimal(5,2)");
    }

    public DbSet<Log> Logs { get; set; }
    public DbSet<Vehicle> Vehicles { get; set; }
    public DbSet<VehicleAnalysis> VehiclesAnalysis { get; set; }
    public DbSet<VehicleError> VehiclesErrors { get; set; }
    public DbSet<VehicleTelemetry> VehicleTelemetries { get; set; }
}