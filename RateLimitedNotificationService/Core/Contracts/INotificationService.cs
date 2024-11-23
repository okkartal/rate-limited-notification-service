using RateLimitedNotificationService.Core.Entities;

namespace RateLimitedNotificationService.Core.Contracts;

public interface INotificationService
{
    NotificationResponse Send(NotificationRequest request);
}