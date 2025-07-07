using E_Ticaret.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using static System.Net.Mime.MediaTypeNames;
using static E_Ticaret.Application.Shared.Permissions;

namespace E_Ticaret.Persistence.Contexts;

public class E_TicaretDbContext : IdentityDbContext<AppUser>
{
    public E_TicaretDbContext(DbContextOptions<E_TicaretDbContext> options) : base(options)
    {
    }
    public DbSet<Domain.Entities.Product> Products { get; set; }
    public DbSet<Domain.Entities.Category> Categories { get; set; }
    public DbSet<Domain.Entities.Image> Images { get; set; }
    public DbSet<Domain.Entities.Favourite> Favourites { get; set; }
    public DbSet<Domain.Entities.Order> Orders { get; set; }
    public DbSet<Domain.Entities.OrderProduct> OrderProducts { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(E_TicaretDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}
