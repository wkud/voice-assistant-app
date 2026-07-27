using Microsoft.AspNetCore.Mvc;
using VoiceAssistant.Application.Abstractions.ShoppingItems;
using VoiceAssistant.Application.Dtos.ShoppingItems;
using VoiceAssistant.Application.Dtos.ShopProduct;

namespace VoiceAssistant.Api.Controllers;

[ApiController]
[Route("api/[controller]s")]
public class ShoppingController : ControllerBase
{
    private readonly IShoppingService _service;

    public ShoppingController(IShoppingService service)
    {
        _service = service;
    }
    
    [HttpPost("addItemToCart/single")]
    public async Task<ActionResult<ShopProductDto>> AddToCartSingle([FromBody] AddItemToCartSingleDto dto, CancellationToken ct = default)
    {
        var shoppingItemDto = await _service.AddItemToCartSingleAsync(dto, ct);
        return CreatedAtAction(nameof(AddToCartSingle), new { id = shoppingItemDto.Id }, shoppingItemDto);
    }
    
    [HttpPost("addItemToCart/byCount")]
    public async Task<ActionResult<ShopProductDto>> AddToCartByCount([FromBody] AddItemToCartByCountDto dto, CancellationToken ct = default)
    {
        var shoppingItemDto = await _service.AddItemToCartByCountAsync(dto, ct);
        return CreatedAtAction(nameof(AddToCartByCount), new { id = shoppingItemDto.Id }, shoppingItemDto);
    }
    
    [HttpPost("addItemToCart/byAmount")]
    public async Task<ActionResult<ShopProductDto>> AddToCartByAmount([FromBody] AddItemToCartByAmountDto dto, CancellationToken ct = default)
    {
        var shoppingItemDto = await _service.AddItemToCartByAmountAsync(dto, ct);
        return CreatedAtAction(nameof(AddToCartByAmount), new { id = shoppingItemDto.Id }, shoppingItemDto);
    }
}