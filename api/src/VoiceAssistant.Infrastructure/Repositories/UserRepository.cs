using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using VoiceAssistant.Application.Abstractions.Users;
using VoiceAssistant.Domain.Models;

namespace VoiceAssistant.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _db;
    private readonly ILogger<UserRepository> _logger;

    public UserRepository(ApplicationDbContext db, ILogger<UserRepository> logger)
    {
        _db = db;
        _logger = logger;
    }

    public async Task<User?> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        _logger.LogDebug("Querying database for user with ID: {UserId}", id);

        var user = await _db.Users.FindAsync([id], ct);

        _logger.LogDebug("Database query completed for user ID: {UserId}, Found: {Found}", id, user is not null);
        return user;
    }

    public async Task<List<User>> ListAllAsync(CancellationToken ct = default)
    {
        _logger.LogDebug("Querying database for all users");

        var users = await _db.Users
            .AsNoTracking()
            .OrderByDescending(p => p.CreatedAt)
            .ToListAsync(ct);

        _logger.LogDebug("Database query completed, retrieved {Count} users", users.Count);
        return users;
    }

    public async Task AddAsync(User user, CancellationToken ct = default)
    {
        _logger.LogInformation("Adding user to database with ID: {UserId}", user.Id);

        _db.Users.Add(user);
        await _db.SaveChangesAsync(ct);

        _logger.LogInformation("Successfully persisted user with ID: {UserId}", user.Id);
    }

    public async Task UpdateAsync(User user, CancellationToken ct = default)
    {
        _logger.LogInformation("Updating user in database with ID: {UserId}", user.Id);

        _db.Users.Update(user);
        await _db.SaveChangesAsync(ct);

        _logger.LogInformation("Successfully persisted update for user with ID: {UserId}", user.Id);
    }

    public async Task DeleteAsync(User user, CancellationToken ct = default)
    {
        _logger.LogInformation("Deleting user from database with ID: {UserId}", user.Id);

        _db.Users.Remove(user);
        await _db.SaveChangesAsync(ct);

        _logger.LogInformation("Successfully deleted user with ID: {UserId}", user.Id);
    }
}