using Microsoft.EntityFrameworkCore;

namespace HR.DataAccess.Entities;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options): base(options)
    {

    }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Address> Addresses { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Employee>()
            .Property(e => e.Email)
            .HasDefaultValue("sardor");

        modelBuilder.Entity<Address>()
            .HasData(new Address
            {
                Id = 1,
                AddressLine1 = "Bodomzor",
                AddressLine2 = "Bodomzor Masjid",
                PostalCode = "1022",
                City = "Tashkent",
                Country = "O'zbekistan"
            });
    }
}