 using ClinicAppointment_System.Models.Entittes;
 using Microsoft.EntityFrameworkCore;
namespace ClinicAppointment_System.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    //this for any table ya as7aby 
    // public DbSet<Product> Products { get; set;
    public DbSet<User> Users { get; set; }
}