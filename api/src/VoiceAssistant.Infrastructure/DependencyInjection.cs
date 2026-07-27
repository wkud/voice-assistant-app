using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using VoiceAssistant.Application.Abstractions;
using VoiceAssistant.Application.Abstractions.ShoppingItems;
using VoiceAssistant.Application.Abstractions.Users;
using VoiceAssistant.Infrastructure.Options;
using VoiceAssistant.Infrastructure.Repositories;

namespace VoiceAssistant.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IShoppingItemRepository, ShoppingItemRepository>();
        
        return services;
    }
    
    public static IServiceCollection AddDatabaseInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection")
                               ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

        services
            .AddOptions<DatabaseOptions>()
            .Bind(configuration.GetSection(DatabaseOptions.SectionName))
            .ValidateDataAnnotations()
            .ValidateOnStart();
        
        services.AddDbContext<ApplicationDbContext>((sp, options) =>
        {
            var databaseOptions = sp.GetRequiredService<IOptions<DatabaseOptions>>().Value;

            options.UseNpgsql(connectionString, npgsqlOptions =>
                {
                    npgsqlOptions.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName);
                    npgsqlOptions.EnableRetryOnFailure(
                        maxRetryCount: databaseOptions.MaxRetryCount,
                        maxRetryDelay: databaseOptions.MaxRetryDelay,
                        errorCodesToAdd: null);
                })
                .UseSnakeCaseNamingConvention();
        });

        return services;
    }
}