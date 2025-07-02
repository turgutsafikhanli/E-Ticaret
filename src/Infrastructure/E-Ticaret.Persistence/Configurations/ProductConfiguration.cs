namespace E_Ticaret.Persistence.Configurations;
using E_Ticaret.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(150);

        builder.HasOne(p => p.Category)
            .WithMany(c => c.Products)
            .HasForeignKey(p => p.CategoryId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(p => p.User)
            .WithMany(u => u.Products)
            .HasForeignKey(p => p.UserId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(p => p.Images)
            .WithOne(i => i.Product)
            .HasForeignKey(i => i.ProductId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(p => p.Favourites)
            .WithOne(f => f.Product)
            .HasForeignKey(f => f.ProductId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(p => p.OrderProducts)
            .WithOne(op => op.Product)
            .HasForeignKey(op => op.ProductId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);
    }
}

