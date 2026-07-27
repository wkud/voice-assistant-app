namespace VoiceAssistant.Domain.Abstractions;

public interface IAuditable
{
    DateTime CreatedAt { get; init; }
    DateTime? UpdatedAt { get; set; }
}