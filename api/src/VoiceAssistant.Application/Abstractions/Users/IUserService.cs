using VoiceAssistant.Application.Dtos.Users;

namespace VoiceAssistant.Application.Abstractions.Users;

public interface IUserService
{
    Task<UserDto> CreateAsync(CreateUserDto dto, CancellationToken ct = default);
    Task<UserDto?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<IEnumerable<UserDto>> GetManyAsync(CancellationToken ct = default);
    Task<UserDto?> UpdateAsync(Guid id, UpdateUserDto dto, CancellationToken ct = default);
    Task<bool> DeleteAsync(Guid id, CancellationToken ct = default);
}