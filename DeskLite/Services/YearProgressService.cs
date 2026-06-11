namespace DeskLite.Services;

public readonly record struct YearProgressInfo(int Year, double Percent, int DaysElapsed, int DaysInYear);

public static class YearProgressService
{
    public static YearProgressInfo GetInfo(DateTime date)
    {
        var start = new DateTime(date.Year, 1, 1);
        var end = start.AddYears(1);
        var daysInYear = (end - start).Days;
        var daysElapsed = (int)(date - start).TotalDays + 1;
        var percent = (date - start).TotalDays / daysInYear * 100;
        return new YearProgressInfo(date.Year, percent, daysElapsed, daysInYear);
    }

    public static string GetLine(DateTime date)
    {
        var info = GetInfo(date);
        return $"{info.Year} 已过 {info.Percent:F1}%";
    }
}
