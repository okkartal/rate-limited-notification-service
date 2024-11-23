using RateLimitedNotificationService.Core.Contracts;
using RateLimitedNotificationService.Core.Entities;

namespace RateLimitedNotificationService.Core.Services;

public class NotificationService(IRateLimiterService rateLimiterService) : INotificationService
{
    public NotificationResponse Send(NotificationRequest request)
    {
        if (rateLimiterService.IsRequestAllowed(request))
        {
            Console.WriteLine($"Sending '{request.Type}' notification to {request.Recipient}: {request.Message}");
            return new NotificationResponse
            {
                Success = true,
                Message = $"Notification '{request.Type}' has been sent to {request.Recipient}'"
            };
        }
        Console.WriteLine($"Rate limit exceeded for '{request.Type}' notification to {request.Recipient} failed");
        return new NotificationResponse
        {
            Success = false,
            Message = $"Rate limit exceeded for '{request.Type}' notification to {request.Recipient}"
        };
    }
}