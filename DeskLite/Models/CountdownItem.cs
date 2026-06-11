namespace DeskLite.Models;

public sealed class CountdownItem
{
    public string Id { get; set; } = Guid.NewGuid().ToString("N");
    public string Title { get; set; } = string.Empty;
    public string Date { get; set; } = string.Empty;
    public bool RepeatYearly { get; set; }
}
