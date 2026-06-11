using DeskLite.Models;

namespace DeskLite.Services;

public readonly record struct CountdownInfo(string Title, int Days, double ProgressPercent);

public static class CountdownService
{
    private const int ProgressHorizonDays = 365;

    private static readonly (string Title, int Month, int Day)[] BuiltInYearly =
    [
        ("元旦", 1, 1),
        ("劳动节", 5, 1),
        ("国庆节", 10, 1)
    ];

    public static CountdownInfo? GetInfo(IReadOnlyList<CountdownItem> items, DateTime today)
    {
        var nearest = FindNearest(items, today);
        if (nearest is null)
        {
            return null;
        }

        var progress = Math.Clamp((1.0 - nearest.Value.Days / (double)ProgressHorizonDays) * 100, 0, 100);
        return new CountdownInfo(nearest.Value.Title, nearest.Value.Days, progress);
    }

    public static string? GetLine(IReadOnlyList<CountdownItem> items, DateTime today)
    {
        var info = GetInfo(items, today);
        return info is null ? null : $"⏳ 距离{info.Value.Title}还有 {info.Value.Days} 天";
    }

    public static (string Title, int Days)? FindNearest(IReadOnlyList<CountdownItem> items, DateTime today)
    {
        (string Title, int Days)? best = null;

        foreach (var item in items)
        {
            if (!DateTime.TryParse(item.Date, out var target))
            {
                continue;
            }

            var days = DaysUntil(today, target.Date, item.RepeatYearly);
            if (days < 0)
            {
                continue;
            }

            if (best is null || days < best.Value.Days)
            {
                best = (item.Title, days);
            }
        }

        foreach (var (title, month, day) in BuiltInYearly)
        {
            var target = new DateTime(today.Year, month, day);
            var days = DaysUntil(today, target, true);
            if (days < 0)
            {
                continue;
            }

            if (best is null || days < best.Value.Days)
            {
                best = (title, days);
            }
        }

        // 春节按农历正月初一
        var spring = LunarCalendar.GetSpringFestivalDate(today.Year);
        if (spring is not null)
        {
            var days = DaysUntil(today, spring.Value, false);
            if (days >= 0 && (best is null || days < best.Value.Days))
            {
                best = ("春节", days);
            }
        }

        return best;
    }

    private static int DaysUntil(DateTime today, DateTime target, bool repeatYearly)
    {
        if (repeatYearly)
        {
            target = new DateTime(today.Year, target.Month, target.Day);
            if (target < today)
            {
                target = target.AddYears(1);
            }
        }

        return (target - today).Days;
    }
}
