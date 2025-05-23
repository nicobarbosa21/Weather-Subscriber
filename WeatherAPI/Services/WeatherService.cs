public class WeatherService
{
    private readonly HttpClient _httpClient;
    private readonly string _apiKey;
    private readonly string _city;

    public WeatherService(HttpClient httpClient, IConfiguration config)
    {
        _httpClient = httpClient;
        _apiKey = config["Weather:ApiKey"] ?? "";
        _city = config["Weather:City"] ?? "";
    }

    public string City => _city;
    
public async Task<OpenWeatherResponse?> Get5DayForecastAsync(string? city = null)
    {
    var cityToUse = string.IsNullOrWhiteSpace(city) ? _city : city;
    var url = $"https://api.openweathermap.org/data/2.5/forecast?q={cityToUse}&appid={_apiKey}&units=metric";
    return await _httpClient.GetFromJsonAsync<OpenWeatherResponse>(url);
    }

    public string FormatForecast(OpenWeatherResponse? forecast, string city)
    {
        if (forecast?.list == null) return "No forecast data available.";
    
        var daily = forecast.list
            .GroupBy(f => f.DateTimeUtc.Date)
            .Select(g => new
            {
                Date = g.Key,
                Min = g.Min(x => x.main?.temp_min ?? 0),
                Max = g.Max(x => x.main?.temp_max ?? 0)
            });
    
        var lines = daily.Select(d =>
            $"{d.Date:yyyy-MM-dd}: Min {d.Min}°C, Max {d.Max}°C");
    
        return $"Aquí tiene el pronóstico para los próximos 6 días en {city}:\n" + string.Join("\n", lines);
    }
}
public class OpenWeatherResponse
{
    public List<ForecastItem>? list { get; set; }
}

public class ForecastItem
{
    public MainInfo? main { get; set; }
    public long dt { get; set; }
    public List<WeatherDescription>? weather { get; set; }

    public DateTime DateTimeUtc => DateTimeOffset.FromUnixTimeSeconds(dt).UtcDateTime;
}

public class MainInfo
{
    public float temp_min { get; set; }
    public float temp_max { get; set; }
}

public class WeatherDescription
{
    public string? description { get; set; }
}

