namespace VoiceAssistant.Application.Dtos.ShoppingItems;

public record AddItemToCartByCountDto(
    string ItemName,
    int Count
);