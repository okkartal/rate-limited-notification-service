namespace RateLimitedNotificationService.Core.Entities;

public sealed class NotificationRequest
{
    public string Type { get; set; }
    public string Recipient { get; set; }
    public string Message { get; set; }
}