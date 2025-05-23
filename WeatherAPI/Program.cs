var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddHttpClient();
builder.Services.AddHttpClient<WeatherService>();
builder.Services.AddHostedService<WeatherScheduler>();
builder.Services.AddSingleton<SubscriberService>();
builder.Services.AddSingleton<EmailService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.MapGet("/current", async (WeatherService weatherService) =>
{
    var forecast = await weatherService.Get5DayForecastAsync();
    return forecast;
});

app.MapGet("/forecast", async (WeatherService weatherService, string? city) =>
{
    var forecast = await weatherService.Get5DayForecastAsync(city);
    if (forecast?.list == null)
        return Results.Ok(new { City = city ?? weatherService.City, Forecasts = new List<object>() });

    var daily = forecast.list
        .GroupBy(f => f.DateTimeUtc.Date)
        .Select(g => new
        {
            Date = g.Key,
            Min = g.Min(x => x.main?.temp_min ?? 0),
            Max = g.Max(x => x.main?.temp_max ?? 0)
        }).ToList();

    return Results.Ok(new { City = city ?? weatherService.City, Forecasts = daily });
});

app.MapPost("/send-weather", async (WeatherService weatherService, SubscriberService subscriberService, EmailService emailService) =>
{
    var subscribers = subscriberService.GetSubscribers();

    foreach (var sub in subscribers)
    {
        var forecast = await weatherService.Get5DayForecastAsync(sub.City);
        string body = weatherService.FormatForecast(forecast, sub.City);
        emailService.Send(sub.Email, "Pronóstico de clima", body);
    }
    return Results.Ok("Emails sent!");
});

app.MapGet("/subscribers", (SubscriberService subscriberService) =>
{
    return Results.Ok(subscriberService.GetSubscribers());
});

app.MapPost("/subscribers", (SubscriberService subscriberService, Subscriber subscriber) =>
{
    if (string.IsNullOrWhiteSpace(subscriber.Name) ||
        string.IsNullOrWhiteSpace(subscriber.Email) ||
        string.IsNullOrWhiteSpace(subscriber.City))
    {
        return Results.BadRequest("Name, Email, and City are required.");
    }

    if (subscriberService.GetSubscribers().Any(s => s.Email == subscriber.Email))
    {
        return Results.BadRequest("Email already exists.");
    }
    
    subscriberService.AddSubscriber(subscriber);
    return Results.Ok("Subscriber added!");
});

app.MapDelete("/subscribers/{email}", (SubscriberService subscriberService, string email) =>
{
    subscriberService.RemoveSubscriber(email);
    return Results.Ok("Subscriber removed!");
});

app.Run();
record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
