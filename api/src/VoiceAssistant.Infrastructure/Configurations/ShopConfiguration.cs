using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VoiceAssistant.Domain.Models;

namespace VoiceAssistant.Infrastructure.Configurations;

public class ShopConfiguration : IEntityTypeConfiguration<Shop>
{
    public void Configure(EntityTypeBuilder<Shop> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id);
        
        builder.Property(x => x.Name)
            .HasMaxLength(50)
            .IsRequired();
        
        builder.Property(x => x.WebsiteUrl)
            .HasMaxLength(100);

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.Property(x => x.UpdatedAt);
        
        builder.HasIndex(x => x.CreatedAt);
    }
}