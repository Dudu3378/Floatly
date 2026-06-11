namespace DeskLite.Services;

public static class YearProgressService
{
    public static string GetLine(DateTime date)
    {
        var start = new DateTime(date.Year, 1, 1);
        var end = start.AddYears(1);
        var percent = (date - start).TotalDays / (end - start).TotalDays * 100;
        var filled = (int)Math.Round(percent / 10);
        var bar = new string('▓', filled) + new string('░', 10 - filled);
        return $"{date.Year} 已过 {percent:F1}%  {bar}";
    }
}
