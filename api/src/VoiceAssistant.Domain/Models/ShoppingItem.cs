using VoiceAssistant.Domain.Abstractions;

namespace VoiceAssistant.Domain.Models;

/// <summary>
/// Represents an abstract item, which user is familiar with.
/// Single instance is a representation of single (or multiple synonymous) phrase describing a shopping item.
/// Examples: "Bread", "Cheese", "Ham", "Milk", "Oat flakes"
/// Counter examples (not represented by this class): "Family bread 1kg", "Slavic bread 500g", "Mlekovita Milk - 1 liter", etc.
/// </summary>
public record ShoppingItem : IEntity, IAuditable
{
    public required Guid Id { get; init; }

    public required string Name { get; set; }

    public required DateTime CreatedAt { get; init; }
    public DateTime? UpdatedAt { get; set; }
    
    public ShopProduct? ShopProduct { get; set; }
}