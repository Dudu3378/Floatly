namespace DeskLite.Models;

public sealed class WeatherCache
{
    public string City { get; set; } = string.Empty;
    public int Temperature { get; set; }
    public int TempMin { get; set; }
    public int TempMax { get; set; }
    public string Description { get; set; } = string.Empty;
    public string Icon { get; set; } = "☀";
    public string? Sunrise { get; set; }
    public string? Sunset { get; set; }
    public int? TomorrowMin { get; set; }
    public int? TomorrowMax { get; set; }
    public string? TomorrowDescription { get; set; }
    public string? TomorrowIcon { get; set; }
    public DateTime UpdatedAt { get; set; }
}
