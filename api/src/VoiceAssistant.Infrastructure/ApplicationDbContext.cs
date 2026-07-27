using Microsoft.EntityFrameworkCore;
using VoiceAssistant.Domain.Models;

namespace VoiceAssistant.Infrastructure;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; } = null!;
    public DbSet<ShoppingItem> ShoppingItems { get; set; } = null!;
    public DbSet<Shop> Shops { get; set; } = null!;
    public DbSet<ShopProduct> ShopProducts { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }
}