using Core;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public class BikeShopDbContext : DbContext
{
    public BikeShopDbContext(DbContextOptions<BikeShopDbContext> options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .Property(u => u.Id)
            .ValueGeneratedOnAdd();
        modelBuilder.Entity<User>()
            .HasIndex(u => u.Email)
            .IsUnique();
    }

    public DbSet<User> UsersTable { get; set; }
}