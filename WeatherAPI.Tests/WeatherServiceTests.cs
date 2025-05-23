using Xunit;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Microsoft.AspNetCore.Mvc.Testing;

// Add the correct namespace if needed, e.g.:
// using WeatherAPI;

public class WeatherServiceTests
{
    [Fact]
    public void FormatForecast_ReturnsNoDataMessage_WhenListIsNull()
    {
        var service = new WeatherService(null, new DummyConfig());
        var result = service.FormatForecast(null);
        Assert.Equal("No forecast data available.", result);
    }

    [Fact]
    public void FormatForecast_ReturnsFormattedForecast_ForValidData()
    {
        var service = new WeatherService(null, new DummyConfig());
        var forecast = new OpenWeatherResponse
        {
            list = new List<ForecastItem>
            {
                new ForecastItem
                {
                    main = new MainInfo { temp_min = 10, temp_max = 20 },
                    dt = new DateTime(2024, 5, 22).Subtract(DateTime.UnixEpoch).TotalSecondsAsLong()
                },
                new ForecastItem
                {
                    main = new MainInfo { temp_min = 12, temp_max = 22 },
                    dt = new DateTime(2024, 5, 22).Subtract(DateTime.UnixEpoch).TotalSecondsAsLong()
                }
            }
        };

        var result = service.FormatForecast(forecast);
        Assert.Contains("2024-05-22: Min 10°C, Max 22°C", result);
    }
}

// Helper config for testing
class DummyConfig : IConfiguration
{
    public string? this[string key] { get => "Cordoba"; set { } }
    public IEnumerable<IConfigurationSection> GetChildren() => System.Linq.Enumerable.Empty<IConfigurationSection>();
    public IConfigurationSection GetSection(string key) => new DummyConfigurationSection();
    public IChangeToken GetReloadToken() => new DummyChangeToken();
}

class DummyConfigurationSection : IConfigurationSection
{
    public string? this[string key] { get => ""; set { } }
    public string Key => "";
    public string Path => "";
    public string? Value { get => ""; set { } }
    public IEnumerable<IConfigurationSection> GetChildren() => System.Linq.Enumerable.Empty<IConfigurationSection>();
    public IChangeToken GetReloadToken() => new DummyChangeToken();
    public IConfigurationSection GetSection(string key) => this;
}

class DummyChangeToken : IChangeToken
{
    public bool HasChanged => false;
    public bool ActiveChangeCallbacks => false;
    public IDisposable RegisterChangeCallback(Action<object?> callback, object? state) => new DummyDisposable();
    class DummyDisposable : IDisposable { public void Dispose() { } }
}

// Extension method for Unix time conversion
public static class DateTimeExtensions
{
    public static long TotalSecondsAsLong(this TimeSpan ts) => (long)ts.TotalSeconds;
}

// DEJO COMENTADO EL TEST END-TO-END PORQUE FALLA AL CREAR EL ARCHIVO TESTHOST.DEPS.JSON

// public class EndToEndTests : IClassFixture<WebApplicationFactory<Program>>
// {
//     private readonly HttpClient _client;

//     public EndToEndTests(WebApplicationFactory<Program> factory)
//     {
//         _client = factory.CreateClient();
//     }

//     [Fact]
//     public async Task GetCurrent_ReturnsSuccess()
//     {
//         var response = await _client.GetAsync("/current");
//         response.EnsureSuccessStatusCode();
//     }
// }