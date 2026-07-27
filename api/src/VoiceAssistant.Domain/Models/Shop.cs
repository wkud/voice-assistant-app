using VoiceAssistant.Domain.Abstractions;

namespace VoiceAssistant.Domain.Models;

/// <summary>
/// Represents an online shop. Correlated with single platform or a website.
/// </summary>
public record Shop : IEntity, IAuditable
{
    public required Guid Id { get; init; }
    
    public required string Name { get; set; }
    public string? WebsiteUrl { get; set; }
    
    public required DateTime CreatedAt { get; init; }
    public DateTime? UpdatedAt { get; set; }
    
    public ICollection<ShopProduct>? ShopProducts { get; set; }
}