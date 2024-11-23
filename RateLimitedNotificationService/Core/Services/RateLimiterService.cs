using Microsoft.Extensions.Caching.Memory;
using RateLimitedNotificationService.Core.Contracts;
using RateLimitedNotificationService.Core.Entities;

namespace RateLimitedNotificationService.Core.Services;

public class RateLimiterService(IMemoryCache memoryCache, RateLimitConfig configuration) : IRateLimiterService
{
    private readonly Dictionary<string, RateLimitSettings> _rateLimits = configuration.Limits;

    public bool IsRequestAllowed(NotificationRequest request)
    {
        if (!_rateLimits.TryGetValue(request.Type, out var rateLimitSettings))
            return true;

        var cacheKey = $"{request.Type}:{request.Recipient}";

        var requestCount = memoryCache.GetOrCreate(cacheKey, entry =>
        {
            entry.AbsoluteExpirationRelativeToNow = rateLimitSettings.Period;
            return 0;
        });
        
        if(requestCount >= rateLimitSettings.Limit)
            return false;

        memoryCache.Set(cacheKey, requestCount + 1);
        return true;
    }
}