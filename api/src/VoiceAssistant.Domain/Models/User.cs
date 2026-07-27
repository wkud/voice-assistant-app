using VoiceAssistant.Domain.Abstractions;
using VoiceAssistant.Domain.Enums;

namespace VoiceAssistant.Domain.Models;

public record User : IEntity, IAuditable
{
    public required Guid Id { get; init; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string EmailAddress { get; set; }
    public required UserAccountStatus Status { get; set; }
    
    public required DateTime CreatedAt { get; init; }
    public DateTime? UpdatedAt { get; set; }
}