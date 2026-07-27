using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VoiceAssistant.Domain.Models;
using VoiceAssistant.Domain.ValueObjects;

namespace VoiceAssistant.Infrastructure.Configurations;

public class ShopProductConfiguration : IEntityTypeConfiguration<ShopProduct>
{
    public void Configure(EntityTypeBuilder<ShopProduct> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id);
        
        builder.Property(x => x.Url)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.Description)
            .HasMaxLength(300);
        
        builder.Property(x => x.ImageUrl)
            .HasMaxLength(100);

        builder.OwnsOne(typeof(Amount), nameof(ShopProduct.AmountPerItem));
        
        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.Property(x => x.UpdatedAt);
        
        builder.HasOne(x => x.Shop)
            .WithMany(x => x.ShopProducts)
            .HasForeignKey(x => x.ShopId)
            .IsRequired()
            .OnDelete(DeleteBehavior.NoAction);
        
        builder.HasOne(x => x.ShoppingItem)
            .WithOne(x => x.ShopProduct)
            .HasForeignKey<ShopProduct>(x => x.ShoppingItemId)
            .IsRequired()
            .OnDelete(DeleteBehavior.NoAction);
        
        builder.HasIndex(x => x.ShopId);
        builder.HasIndex(x => x.ShoppingItemId);
        builder.HasIndex(x => x.CreatedAt);
    }
}