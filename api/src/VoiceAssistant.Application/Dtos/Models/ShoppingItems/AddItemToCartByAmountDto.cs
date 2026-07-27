namespace VoiceAssistant.Application.Dtos.ShoppingItems;

public record AddItemToCartByAmountDto(
    string ItemName,
    int? AmountInHundreds,
    int? Amount,
    string Unit
);