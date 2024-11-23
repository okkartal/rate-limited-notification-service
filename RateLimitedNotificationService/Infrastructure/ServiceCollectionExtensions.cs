using RateLimitedNotificationService.Core.Contracts;
using RateLimitedNotificationService.Core.Services;

namespace RateLimitedNotificationService.Infrastructure;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddMemoryCache();
        services.AddScoped<IRateLimiterService, RateLimiterService>();
        services.AddScoped<INotificationService, NotificationService>();
        
        return services;
    }
}