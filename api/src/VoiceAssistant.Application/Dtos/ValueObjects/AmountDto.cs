namespace VoiceAssistant.Application.Dtos.ValueObjects;

/// <inheritdoc cref="VoiceAssistant.Domain.ValueObjects.Amount"/>
public record AmountDto(
    string Unit,
    decimal Value
);