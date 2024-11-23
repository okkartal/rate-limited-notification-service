namespace RateLimitedNotificationService.Core.Entities;

public sealed class RateLimitSettings
{
    public int Limit { get; set; }
    public TimeSpan Period { get; set; }
}