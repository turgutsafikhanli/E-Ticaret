namespace E_Ticaret.Persistence.Configurations;
using E_Ticaret.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.Property(c => c.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.HasMany(c => c.Products)
            .WithOne(p => p.Category)
            .HasForeignKey(p => p.CategoryId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(c => c.MainCategory)
               .WithMany(c => c.SubCategories)
               .HasForeignKey(c => c.MainCategoryId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}

