using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using VehiclesFleet.DataAccess.Entities;

namespace VehiclesFleet.DataAccess;

public class DataContext : IdentityDbContext<User>
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // modelBuilder.Entity<InvoiceLine>().ToTable("InvoiceLine");
        // modelBuilder
        //     .Entity<Invoice>().ToTable("Invoice")
        //     .HasMany(x => x.InvoiceLines)
        //     .WithOne(x => x.Invoice)
        //     .HasForeignKey(x => x.InvoiceNumber)
        //     .HasPrincipalKey(x => x.InvoiceNumber)
        //     .IsRequired();
    }

    // public DbSet<Invoice> Invoices { get; set; }
    // public DbSet<InvoiceLine> InvoiceLines { get; set; }
}