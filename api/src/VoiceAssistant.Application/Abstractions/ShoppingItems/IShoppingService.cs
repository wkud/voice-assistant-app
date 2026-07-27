using VoiceAssistant.Application.Dtos.ShoppingItems;
using VoiceAssistant.Application.Dtos.ShopProduct;

namespace VoiceAssistant.Application.Abstractions.ShoppingItems;

public interface IShoppingService
{
    Task<ShopProductDto> AddItemToCartSingleAsync(AddItemToCartSingleDto dto, CancellationToken ct = default);
    Task<ShopProductDto> AddItemToCartByCountAsync(AddItemToCartByCountDto dto, CancellationToken ct = default);
    Task<ShopProductDto> AddItemToCartByAmountAsync(AddItemToCartByAmountDto dto, CancellationToken ct = default);
}