using VoiceAssistant.Application.Abstractions.ShoppingItems;
using VoiceAssistant.Application.Dtos.ShoppingItems;
using VoiceAssistant.Application.Dtos.ShopProduct;
using VoiceAssistant.Application.Exceptions;
using VoiceAssistant.Domain.Models;

namespace VoiceAssistant.Application.Services;

public class ShoppingService : IShoppingService
{
    private readonly IShoppingItemRepository _shoppingItemRepository;

    public ShoppingService(IShoppingItemRepository shoppingItemRepository)
    {
        _shoppingItemRepository = shoppingItemRepository;
    }

    public async Task<ShopProductDto> AddItemToCartSingleAsync(AddItemToCartSingleDto dto, CancellationToken ct = default)
    {
        var byCountDto = new AddItemToCartByCountDto(dto.ItemName, 1);
        return await AddItemToCartByCountAsync(byCountDto, ct);
    }

    public async Task<ShopProductDto> AddItemToCartByCountAsync(AddItemToCartByCountDto dto, CancellationToken ct = default)
    {
        // Assume:
        // - the only shop is Frisco for now (Auchan, BiedronkaOnGlovo can be added later on)  

        // 1. Validate if dto.ShoppingItemName exists in Database (ShoppingItem.Name)
        var shoppingItem = await _shoppingItemRepository.GetByNameIncludeShopProductAsync(dto.ItemName, ct);
        if (shoppingItem is null)
        {
            // TODO write middleware to handle EntityNotFoundException exception
            throw new EntityNotFoundException(nameof(ShoppingItem), nameof(dto.ItemName), dto.ItemName); 
        }
        
        // 2. Find ShopProduct with Name matching to dto.ItemName
        //     - Assume there is only one Shop entity (for now)
        var shopProduct = shoppingItem.ShopProduct;
        if (shopProduct is null)
        {
            throw new EntityNotFoundException(nameof(ShopProduct), nameof(ShopProduct.ShoppingItemId), shoppingItem.Id.ToString());
        }
        
        // 3. Get ShopProduct.Url
        var url = shopProduct.Url;
        var count = dto.Count;
        
        // 4. Delegate a job to run specific action in a browser (adds to cart) given the shopProductUrl and count
        //     - if dto.Count == null (useCase == Single), then assume count = 1
        
        return await Task.FromException<ShopProductDto>(new NotImplementedException()); // TODO
    }

    public async Task<ShopProductDto> AddItemToCartByAmountAsync(AddItemToCartByAmountDto dto, CancellationToken ct = default)
    {
        // 1. Find `ShopProduct` with Name matching to `dto.ItemName`
        // 1. Fetch matching `ShopProduct`'s properties:
        //     - `Description`
        //     - `AmountPerPiece` - amount of product per piece (e.g. 500g, 1l, 2kg etc.)
        //     - `UnitOfMeasurement` (e.g. `"g"`, `"l"`, `"kg"`)
        //     - Assume there is only one `Shop` entity
        // 2. Get `ShopProduct.Url`
        // 3. Calculate `piecesNeeded` to add to the cart.
        //     - `piecesNeeded = dto.Amount / shopProduct.AmountPerPiece` 
        // 4. Delegate a job to run specific action in a browser (adds to cart) given the `shopProductUrl`
        
        return await Task.FromException<ShopProductDto>(new NotImplementedException());  // TODO
    }
}