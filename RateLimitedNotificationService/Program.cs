using RateLimitedNotificationService.Core.Contracts;
using RateLimitedNotificationService.Core.Entities;
using RateLimitedNotificationService.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var rateLimitingConfig = new RateLimitConfig();
builder.Configuration.GetSection("RateLimiting").Bind(rateLimitingConfig);
builder.Services.AddSingleton(rateLimitingConfig);

builder.Services.AddInfrastructure();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapPost("/send-notification", (INotificationService notificationService, NotificationRequest notificationRequest) =>
    {
       var response = notificationService.Send(notificationRequest);
       
       return response.Success ? Results.Ok(response) : Results.BadRequest(response);
    })
    .WithName("SendNotification")
    .WithOpenApi(operation =>
    {
        operation.Summary = "Sends a notification";
        operation.Description = "Handles notification requests and applies rate limiting";
        return operation;
    });

app.Run();