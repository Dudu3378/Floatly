namespace DeskLite.Models;

public sealed class TodoDisplayItem
{
    public required string Id { get; init; }
    public required string Title { get; init; }
    public string? Time { get; init; }
    public bool Pinned { get; init; }
    public bool Done { get; init; }
    public string Date { get; init; } = string.Empty;
    public bool HasTime => !string.IsNullOrWhiteSpace(Time);
    public string PinIcon => Pinned ? "★" : "☆";
    public string Display { get; init; } = string.Empty;

    public static TodoDisplayItem From(TodoItem item) => new()
    {
        Id = item.Id,
        Title = item.Title,
        Time = item.Time,
        Pinned = item.Pinned,
        Done = item.Done,
        Date = item.Date,
        Display = FormatDisplay(item)
    };

    public static string FormatDisplay(TodoItem item)
    {
        var body = string.IsNullOrWhiteSpace(item.Time) ? item.Title : $"{item.Time} {item.Title}";
        return body;
    }
}
