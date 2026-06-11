namespace DeskLite.Models;

public sealed class AppDataFile
{
    public List<TodoItem> Todos { get; set; } = [];
    public List<CountdownItem> Countdowns { get; set; } = [];
    public string Scratch { get; set; } = string.Empty;
}
