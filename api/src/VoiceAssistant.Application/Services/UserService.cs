using Microsoft.Extensions.Logging;
using VoiceAssistant.Application.Abstractions;
using VoiceAssistant.Application.Abstractions.Users;
using VoiceAssistant.Application.Dtos.Users;
using VoiceAssistant.Domain.Enums;
using VoiceAssistant.Domain.Models;

namespace VoiceAssistant.Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _repository;
    private readonly ILogger<UserService> _logger;

    public UserService(IUserRepository repository, ILogger<UserService> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<UserDto> CreateAsync(CreateUserDto dto, CancellationToken ct = default)
    {
        _logger.LogInformation("Creating new user");

        var user = new User
        {
            Id = Guid.NewGuid(),
            FirstName =  dto.FirstName,
            LastName =  dto.LastName,
            EmailAddress =  dto.EmailAddress,
            Status = UserAccountStatus.Created,
            CreatedAt = DateTime.UtcNow
        };

        await _repository.AddAsync(user, ct);

        _logger.LogDebug("Successfully created new user with ID: {UserId}", user.Id);

        return MapToDto(user);
    }

    public async Task<UserDto?> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        _logger.LogDebug("Fetching user with ID: {UserId}", id);

        var user = await _repository.GetByIdAsync(id, ct);

        if (user is null)
        {
            _logger.LogWarning("User with ID: {UserId} not found", id);
            return null;
        }

        _logger.LogDebug("Successfully retrieved user with ID: {UserId}", id);

        return MapToDto(user);
    }

    public async Task<IEnumerable<UserDto>> GetManyAsync(CancellationToken ct = default)
    {
        _logger.LogDebug("Fetching all users");

        var users = await _repository.ListAllAsync(ct);

        _logger.LogInformation("Retrieved {Count} users", users.Count());

        return users.Select(MapToDto);
    }

    public async Task<UserDto?> UpdateAsync(Guid id, UpdateUserDto dto, CancellationToken ct = default)
    {
        _logger.LogInformation("Updating user with ID: {UserId}", id);

        var user = await _repository.GetByIdAsync(id, ct);
        if (user is null)
        {
            _logger.LogWarning("User with ID: {UserId} not found for update", id);
            return null;
        }

        user.FirstName =  dto.FirstName;
        user.LastName =  dto.LastName;
        user.EmailAddress =  dto.EmailAddress;
        user.Status = dto.Status;
        user.UpdatedAt = DateTime.UtcNow;

        await _repository.UpdateAsync(user, ct);

        _logger.LogInformation("Successfully updated user with ID: {UserId}", id);

        return MapToDto(user);
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken ct = default)
    {
        _logger.LogInformation("Deleting user with ID: {UserId}", id);

        var user = await _repository.GetByIdAsync(id, ct);
        if (user is null)
        {
            _logger.LogWarning("User with ID: {UserId} not found for deletion", id);
            return false;
        }

        await _repository.DeleteAsync(user, ct);

        _logger.LogInformation("Successfully deleted user with ID: {UserId}", id);

        return true;
    }

    private static UserDto MapToDto(User user)
        => new UserDto
        {
            Id = user.Id,
            FirstName =  user.FirstName,
            LastName =  user.LastName,
            EmailAddress =  user.EmailAddress,
            Status = user.Status,
            CreatedAt = user.CreatedAt,
            UpdatedAt = user.UpdatedAt
        };
}