using Microsoft.Extensions.Hosting;

public class WeatherScheduler : BackgroundService
{
    private readonly IServiceProvider _services;

    public WeatherScheduler(IServiceProvider services)
    {
        _services = services;
    }

protected override async Task ExecuteAsync(CancellationToken stoppingToken)
{
    while (!stoppingToken.IsCancellationRequested)
    {
        try
        {
            using (var scope = _services.CreateScope())
            {
                var weatherService = scope.ServiceProvider.GetRequiredService<WeatherService>();
                var subscriberService = scope.ServiceProvider.GetRequiredService<SubscriberService>();
                var emailService = scope.ServiceProvider.GetRequiredService<EmailService>();

                var subscribers = subscriberService.GetSubscribers();

                foreach (var sub in subscribers)
                {
                    try
                    {
                        var forecast = await weatherService.Get5DayForecastAsync(sub.City);
                        string body = weatherService.FormatForecast(forecast, sub.City);
                        emailService.Send(sub.Email, "Pronóstico de clima", body);
                    }
                    catch (Exception ex)
                    {
                        // Log or handle email sending error
                        Console.WriteLine($"Failed to send email to {sub.Email}: {ex.Message}");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Log or handle general scheduler error
            Console.WriteLine($"Scheduler error: {ex.Message}");
        }
        await Task.Delay(TimeSpan.FromHours(12), stoppingToken);
    }
}
}