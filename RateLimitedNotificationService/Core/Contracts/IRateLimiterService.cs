using RateLimitedNotificationService.Core.Entities;

namespace RateLimitedNotificationService.Core.Contracts;

public interface IRateLimiterService
{
    bool IsRequestAllowed(NotificationRequest request);
}