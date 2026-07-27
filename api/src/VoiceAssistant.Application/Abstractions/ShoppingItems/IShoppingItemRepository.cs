using VoiceAssistant.Domain.Models;

namespace VoiceAssistant.Application.Abstractions.ShoppingItems;

public interface IShoppingItemRepository
{
    Task<ShoppingItem?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<ShoppingItem?> GetByNameIncludeShopProductAsync(string name, CancellationToken ct = default);
    Task<List<ShoppingItem>> ListAllAsync(CancellationToken ct = default);
    Task AddAsync(ShoppingItem shoppingItem, CancellationToken ct = default);
    Task UpdateAsync(ShoppingItem shoppingItem, CancellationToken ct = default);
    Task DeleteAsync(ShoppingItem shoppingItem, CancellationToken ct = default);
}