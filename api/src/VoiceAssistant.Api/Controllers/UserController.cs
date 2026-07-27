using Microsoft.AspNetCore.Mvc;
using VoiceAssistant.Application.Abstractions;
using VoiceAssistant.Application.Abstractions.Users;
using VoiceAssistant.Application.Dtos.Users;

namespace VoiceAssistant.Api.Controllers;

[ApiController]
[Route("api/[controller]s")]
public class UserController : ControllerBase
{
    private readonly IUserService _service;

    public UserController(IUserService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<ActionResult<UserDto>> Create([FromBody] CreateUserDto dto)
    {
        var userDto = await _service.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = userDto.Id }, userDto);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<UserDto>> GetById(Guid id)
    {
        var userDto = await _service.GetByIdAsync(id);
        return userDto is null ? NotFound() : Ok(userDto);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserDto>>> GetMany()
        => Ok(await _service.GetManyAsync());

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<UserDto>> Update(Guid id, [FromBody] UpdateUserDto dto)
    {
        var userDto = await _service.UpdateAsync(id, dto);
        return userDto is null ? NotFound() : Ok(userDto);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var success = await _service.DeleteAsync(id);
        return success ? NoContent() : NotFound();
    }
}