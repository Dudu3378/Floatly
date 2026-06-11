namespace DeskLite.Models;

public sealed class TodoItem
{
    public string Id { get; set; } = Guid.NewGuid().ToString("N");
    public string Title { get; set; } = string.Empty;
    public string? Time { get; set; }
    public string Date { get; set; } = DateTime.Today.ToString("yyyy-MM-dd");
    public bool Done { get; set; }
    public bool Pinned { get; set; }
}
