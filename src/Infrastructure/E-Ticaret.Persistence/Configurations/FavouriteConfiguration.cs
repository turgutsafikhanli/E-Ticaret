namespace E_Ticaret.Persistence.Configurations;
using E_Ticaret.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class FavouriteConfiguration : IEntityTypeConfiguration<Favourite>
{
    public void Configure(EntityTypeBuilder<Favourite> builder)
    {
        builder.HasKey(f => f.Id);

        builder.HasOne(f => f.User)
            .WithMany(u => u.Favourites)
            .HasForeignKey(f => f.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(f => f.Product)
            .WithMany(p => p.Favourites)
            .HasForeignKey(f => f.ProductId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
