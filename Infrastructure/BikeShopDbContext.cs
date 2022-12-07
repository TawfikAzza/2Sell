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
        modelBuilder.Entity<Post>()
            .Property(p => p.Id)
            .ValueGeneratedOnAdd();
        modelBuilder.Entity<Post>()
            .HasOne(p => p.User)
            .WithMany(u => u.Posts)
            .HasForeignKey(p => p.UserId)
            .OnDelete(DeleteBehavior.Cascade);
       /* modelBuilder.Entity<User>()
            .Ignore(u => u.Posts);
        modelBuilder.Entity<Post>()
            .Ignore(p => p.User);*/
    }

    public DbSet<User> UsersTable { get; set; }
    public DbSet<Post> PostTable { get; set; }
}