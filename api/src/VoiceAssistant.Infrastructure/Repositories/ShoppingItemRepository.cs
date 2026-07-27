using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using VoiceAssistant.Application.Abstractions.ShoppingItems;
using VoiceAssistant.Domain.Models;

namespace VoiceAssistant.Infrastructure.Repositories;

public class ShoppingItemRepository : IShoppingItemRepository
{
    private readonly ApplicationDbContext _db;
    private readonly ILogger<ShoppingItemRepository> _logger;

    public ShoppingItemRepository(ApplicationDbContext db, ILogger<ShoppingItemRepository> logger)
    {
        _db = db;
        _logger = logger;
    }

    public async Task<ShoppingItem?> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        _logger.LogDebug("Querying database for shoppingItem with ID: {ShoppingItemId}", id);

        var shoppingItem = await _db.ShoppingItems.FindAsync([id], ct);

        _logger.LogDebug("Database query completed for shoppingItem ID: {ShoppingItemId}, Found: {Found}", id, shoppingItem is not null);
        return shoppingItem;
    }

    public async Task<ShoppingItem?> GetByNameIncludeShopProductAsync(string name, CancellationToken ct = default)
    {
        _logger.LogDebug("Querying database for shoppingItem with name: {ShoppingItemName}", name);

        var shoppingItem = await _db.ShoppingItems
            .AsNoTracking()
            .Include(x => x.ShopProduct)
            .SingleOrDefaultAsync(x => x.Name == name, ct);

        _logger.LogDebug("Database query completed for shoppingItem name: {ShoppingItemName}, Found: {Found}", name, shoppingItem is not null);
        return shoppingItem;
    }

    public async Task<List<ShoppingItem>> ListAllAsync(CancellationToken ct = default)
    {
        _logger.LogDebug("Querying database for all shoppingItems");

        var shoppingItems = await _db.ShoppingItems
            .AsNoTracking()
            .OrderByDescending(p => p.CreatedAt)
            .ToListAsync(ct);

        _logger.LogDebug("Database query completed, retrieved {Count} shoppingItems", shoppingItems.Count);
        return shoppingItems;
    }

    public async Task AddAsync(ShoppingItem shoppingItem, CancellationToken ct = default)
    {
        _logger.LogInformation("Adding shoppingItem to database with ID: {ShoppingItemId}", shoppingItem.Id);

        _db.ShoppingItems.Add(shoppingItem);
        await _db.SaveChangesAsync(ct);

        _logger.LogInformation("Successfully persisted shoppingItem with ID: {ShoppingItemId}", shoppingItem.Id);
    }

    public async Task UpdateAsync(ShoppingItem shoppingItem, CancellationToken ct = default)
    {
        _logger.LogInformation("Updating shoppingItem in database with ID: {ShoppingItemId}", shoppingItem.Id);

        _db.ShoppingItems.Update(shoppingItem);
        await _db.SaveChangesAsync(ct);

        _logger.LogInformation("Successfully persisted update for shoppingItem with ID: {ShoppingItemId}", shoppingItem.Id);
    }

    public async Task DeleteAsync(ShoppingItem shoppingItem, CancellationToken ct = default)
    {
        _logger.LogInformation("Deleting shoppingItem from database with ID: {ShoppingItemId}", shoppingItem.Id);

        _db.ShoppingItems.Remove(shoppingItem);
        await _db.SaveChangesAsync(ct);

        _logger.LogInformation("Successfully deleted shoppingItem with ID: {ShoppingItemId}", shoppingItem.Id);
    }
}