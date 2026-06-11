using System.Net.Http;
using System.Text.Json;

namespace DeskLite.Services;

public sealed class LocationService
{
    private static readonly HttpClient Http = new() { Timeout = TimeSpan.FromSeconds(8) };

    public async Task<DetectedLocation?> DetectByIpAsync(CancellationToken ct = default)
    {
        try
        {
            var json = await Http.GetStringAsync("https://ipwho.is/?lang=zh", ct);
            using var doc = JsonDocument.Parse(json);
            var root = doc.RootElement;
            if (!root.TryGetProperty("success", out var ok) || !ok.GetBoolean())
            {
                return null;
            }

            var city = root.TryGetProperty("city", out var cityEl) ? cityEl.GetString() : null;
            if (string.IsNullOrWhiteSpace(city))
            {
                return null;
            }

            var region = root.TryGetProperty("region", out var regionEl) ? regionEl.GetString() : null;
            var lat = root.GetProperty("latitude").GetDouble();
            var lon = root.GetProperty("longitude").GetDouble();
            return new DetectedLocation(city.Trim(), region?.Trim(), lat, lon);
        }
        catch
        {
            return null;
        }
    }

    public sealed record DetectedLocation(string City, string? Region, double Latitude, double Longitude);
}
