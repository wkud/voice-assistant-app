using VoiceAssistant.Application.Dtos.ValueObjects;

namespace VoiceAssistant.Application.Dtos.ShopProduct;

public record ShopProductDto(
    Guid Id,
    string FullProductName,
    string Url,
    string? Description,
    string? ImageUrl,
    AmountDto? AmountPerItem
);