namespace RateLimitedNotificationService.Core.Entities;

public sealed class NotificationResponse
{
    public bool Success { get; set; }
    public string Message { get; set; }
}