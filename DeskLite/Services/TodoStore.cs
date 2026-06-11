using DeskLite.Models;

namespace DeskLite.Services;

public sealed class TodoStore
{
    private AppDataFile _data;

    public TodoStore()
    {
        _data = JsonStore.LoadData();
    }

    public AppDataFile Data => _data;

    public IReadOnlyList<TodoItem> GetTodayTodos()
    {
        var today = DateTime.Today.ToString("yyyy-MM-dd");
        return _data.Todos
            .Where(t => !t.Done && (string.IsNullOrWhiteSpace(t.Date) || t.Date == today))
            .OrderByDescending(t => t.Pinned)
            .ThenBy(t => string.IsNullOrWhiteSpace(t.Time) ? "99:99" : t.Time)
            .ThenBy(t => t.Title)
            .Take(5)
            .ToList();
    }

    public IReadOnlyList<TodoItem> GetTodayTimedTodos()
    {
        var today = DateTime.Today.ToString("yyyy-MM-dd");
        return _data.Todos
            .Where(t => !t.Done && t.Date == today && !string.IsNullOrWhiteSpace(t.Time))
            .ToList();
    }

    public void Add(string title, string? time = null)
    {
        title = title.Trim();
        if (string.IsNullOrWhiteSpace(title))
        {
            return;
        }

        if (!string.IsNullOrWhiteSpace(time) && !TimeSpan.TryParse(time, out _))
        {
            time = null;
        }

        _data.Todos.Add(new TodoItem
        {
            Title = title,
            Time = string.IsNullOrWhiteSpace(time) ? null : time,
            Date = DateTime.Today.ToString("yyyy-MM-dd")
        });
        Save();
    }

    public void ToggleDone(string id)
    {
        var item = _data.Todos.FirstOrDefault(t => t.Id == id);
        if (item is null)
        {
            return;
        }

        item.Done = !item.Done;
        Save();
    }

    public void Remove(string id)
    {
        _data.Todos.RemoveAll(t => t.Id == id);
        Save();
    }

    public void SaveScratch(string scratch)
    {
        _data.Scratch = scratch;
        Save();
    }

    public void AddCountdown(string title, DateTime date, bool repeatYearly)
    {
        title = title.Trim();
        if (string.IsNullOrWhiteSpace(title))
        {
            return;
        }

        _data.Countdowns.Add(new CountdownItem
        {
            Title = title,
            Date = date.ToString("yyyy-MM-dd"),
            RepeatYearly = repeatYearly
        });
        Save();
    }

    private void Save() => JsonStore.SaveData(_data);
}
