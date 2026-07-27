using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VoiceAssistant.Domain.Models;

namespace VoiceAssistant.Infrastructure.Configurations;

public class ShoppingItemConfiguration : IEntityTypeConfiguration<ShoppingItem>
{
    public void Configure(EntityTypeBuilder<ShoppingItem> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id);
        
        builder.Property(x => x.Name)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.Property(x => x.UpdatedAt);
        
        builder.HasIndex(x => x.Name);
        builder.HasIndex(x => x.CreatedAt);
    }
}