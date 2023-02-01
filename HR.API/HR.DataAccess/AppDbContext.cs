using Microsoft.EntityFrameworkCore;

namespace HR.DataAccess.Entities;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options): base(options)
    {

    }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Address> Addresses { get; set; }
}