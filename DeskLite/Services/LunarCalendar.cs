namespace DeskLite.Services;

/// <summary>
/// 轻量万年历：农历、节气、节日、干支、生肖（1900-2100，本地计算）。
/// </summary>
public static class LunarCalendar
{
    private static readonly int[] LunarInfo =
    [
        0x04bd8, 0x04ae0, 0x0a570, 0x054d5, 0x0d260, 0x0d950, 0x16554, 0x056a0, 0x09ad0, 0x055d2,
        0x04ae0, 0x0a5b6, 0x0a4d0, 0x0d250, 0x1d255, 0x0b540, 0x0d6a0, 0x0ada2, 0x095b0, 0x14977,
        0x04970, 0x0a4b0, 0x0b4b5, 0x06a50, 0x06d40, 0x1ab54, 0x02b60, 0x09570, 0x052f2, 0x04970,
        0x06566, 0x0d4a0, 0x0ea50, 0x06e95, 0x05ad0, 0x02b60, 0x186e3, 0x092e0, 0x1c8d7, 0x0c950,
        0x0d4a0, 0x1d8a6, 0x0b550, 0x056a0, 0x1a5b4, 0x025d0, 0x092d0, 0x0d2b2, 0x0a950, 0x0b557,
        0x06ca0, 0x0b550, 0x15355, 0x04da0, 0x0a5b0, 0x14573, 0x052b0, 0x0a6a8, 0x0e4d0, 0x06e55,
        0x06aa0, 0x0ab50, 0x053b4, 0x04b60, 0x0aae4, 0x0a570, 0x05260, 0x0f263, 0x0d950, 0x05b57,
        0x056a0, 0x096d0, 0x04dd5, 0x04ad0, 0x0a4d0, 0x0d4d4, 0x0d250, 0x0d558, 0x0b540, 0x0b6a0,
        0x195a6, 0x095b0, 0x049b0, 0x0a974, 0x0a4b0, 0x0b27a, 0x06a50, 0x06d40, 0x0af46, 0x0ab60,
        0x09570, 0x04af5, 0x04970, 0x064b0, 0x074a3, 0x0ea50, 0x06b58, 0x05ac0, 0x0ab60, 0x096d5,
        0x092e0, 0x0c960, 0x0d954, 0x0d4a0, 0x0da50, 0x07552, 0x056a0, 0x0abb7, 0x025d0, 0x092d0,
        0x0cab5, 0x0a950, 0x0b4a0, 0x0baa4, 0x0ad50, 0x055d9, 0x04ba0, 0x0a5b0, 0x15176, 0x052b0,
        0x0a930, 0x07954, 0x06aa0, 0x0ad50, 0x05b52, 0x04b60, 0x0a6e6, 0x0a4e0, 0x0d260, 0x0ea65,
        0x0d530, 0x05aa0, 0x076a3, 0x096d0, 0x04afb, 0x04ad0, 0x0a4d0, 0x1d0b6, 0x0d250, 0x0d520,
        0x0dd45, 0x0b5a0, 0x056d0, 0x055b2, 0x049b0, 0x0a577, 0x0a4b0, 0x0aa50, 0x1b255, 0x06d20,
        0x0ada0, 0x14b63
    ];

    private static readonly string[] Gan = ["甲", "乙", "丙", "丁", "戊", "己", "庚", "辛", "壬", "癸"];
    private static readonly string[] Zhi = ["子", "丑", "寅", "卯", "辰", "巳", "午", "未", "申", "酉", "戌", "亥"];
    private static readonly string[] Animals = ["鼠", "牛", "虎", "兔", "龙", "蛇", "马", "羊", "猴", "鸡", "狗", "猪"];

    private static readonly string[] SolarTerms =
    [
        "小寒", "大寒", "立春", "雨水", "惊蛰", "春分", "清明", "谷雨",
        "立夏", "小满", "芒种", "夏至", "小暑", "大暑", "立秋", "处暑",
        "白露", "秋分", "寒露", "霜降", "立冬", "小雪", "大雪", "冬至"
    ];

    private static readonly string[] WeekNames = ["日", "一", "二", "三", "四", "五", "六"];

    private static readonly double[] TermMinutes =
    [
        0, 21208, 42467, 63836, 85337, 107014, 128867, 150921,
        173149, 195551, 218072, 240693, 263343, 285989, 308563, 331033,
        353350, 375494, 397447, 419210, 440795, 462224, 483532, 504758
    ];

    private static readonly (int Month, int Day, string Name)[] SolarFestivals =
    [
        (1, 1, "元旦"), (2, 14, "情人节"), (3, 8, "妇女节"), (5, 1, "劳动节"),
        (5, 4, "青年节"), (6, 1, "儿童节"), (10, 1, "国庆节"), (12, 25, "圣诞节")
    ];

    private static readonly (int Month, int Day, string Name)[] LunarFestivals =
    [
        (1, 1, "春节"), (1, 15, "元宵"), (2, 2, "龙抬头"), (5, 5, "端午"),
        (7, 7, "七夕"), (7, 15, "中元"), (8, 15, "中秋"), (9, 9, "重阳"), (12, 8, "腊八")
    ];

    public static LunarDayInfo Get(DateTime date) => BuildInfo(date.Date);

    public static LunarDayInfo GetToday() => BuildInfo(DateTime.Today);

    public static DateTime? GetSpringFestivalDate(int year)
    {
        for (var date = new DateTime(year, 1, 1); date.Year == year && date.Month <= 3; date = date.AddDays(1))
        {
            var lunar = SolarToLunar(date);
            if (lunar.Month == 1 && lunar.Day == 1 && !lunar.IsLeap)
            {
                return date;
            }
        }

        return null;
    }

    private static LunarDayInfo BuildInfo(DateTime date)
    {
        var lunar = SolarToLunar(date);
        var term = GetSolarTerm(date);
        var festival = GetFestival(date, lunar.Month, lunar.Day, lunar.IsLeap);

        var headline = festival ?? term;
        var line = headline is null
            ? $"农历{lunar.MonthDayText}"
            : $"农历{lunar.MonthDayText} · {headline}";

        var ganZhiYear = ToGanZhiYear(lunar.Year);
        var animal = Animals[(lunar.Year - 4) % 12];
        var ganZhiDay = ToGanZhiDay(date);
        var subLine = $"{ganZhiYear}年 · 属{animal} · {ganZhiDay}日";

        var mark = festival ?? term;
        return new LunarDayInfo(
            line,
            subLine,
            WeekNames[(int)date.DayOfWeek],
            GetShortLunarLabel(lunar),
            mark);
    }

    private static string GetShortLunarLabel(LunarDate lunar) =>
        lunar.Day == 1 ? lunar.MonthDayText : ToChinaDay(lunar.Day);

    private static string ToGanZhiYear(int lunarYear) =>
        Gan[(lunarYear - 4) % 10] + Zhi[(lunarYear - 4) % 12];

    private static string ToGanZhiDay(DateTime date)
    {
        var baseDate = new DateTime(1900, 1, 1);
        var t = (int)(date.Date - baseDate).TotalDays;
        var index = (6 * ((t + 9) % 10) - 5 * ((t + 3) % 12) + 60) % 60;
        return Gan[index % 10] + Zhi[index % 12];
    }

    private static LunarDate SolarToLunar(DateTime date)
    {
        var year = date.Year;
        var month = date.Month;
        var day = date.Day;

        if (year is < 1900 or > 2100)
        {
            return new LunarDate(year, 1, 1, false, "正月初一");
        }

        var offset = (int)((new DateTime(year, month, day) - new DateTime(1900, 1, 31)).TotalDays);
        var lunarYear = 1900;
        var temp = 0;

        for (lunarYear = 1900; lunarYear < 2101 && offset > 0; lunarYear++)
        {
            temp = YearDays(lunarYear);
            offset -= temp;
        }

        if (offset < 0)
        {
            offset += temp;
            lunarYear--;
        }

        var leap = LeapMonth(lunarYear);
        var isLeap = false;
        var lunarMonth = 1;

        for (lunarMonth = 1; lunarMonth < 13 && offset > 0; lunarMonth++)
        {
            if (leap > 0 && lunarMonth == leap + 1 && !isLeap)
            {
                lunarMonth--;
                isLeap = true;
                temp = LeapDays(lunarYear);
            }
            else
            {
                temp = MonthDays(lunarYear, lunarMonth);
            }

            if (isLeap && lunarMonth == leap + 1)
            {
                isLeap = false;
            }

            offset -= temp;
        }

        if (offset == 0 && leap > 0 && lunarMonth == leap + 1)
        {
            if (isLeap)
            {
                isLeap = false;
            }
            else
            {
                isLeap = true;
                lunarMonth--;
            }
        }

        if (offset < 0)
        {
            offset += temp;
            lunarMonth--;
        }

        var lunarDay = offset + 1;
        return new LunarDate(
            lunarYear,
            lunarMonth,
            lunarDay,
            isLeap,
            $"{(isLeap ? "闰" : "")}{ToChinaMonth(lunarMonth)}{ToChinaDay(lunarDay)}");
    }

    private static string? GetSolarTerm(DateTime date)
    {
        for (var i = 0; i < 24; i++)
        {
            if (GetTermDate(date.Year, i) == date.Date)
            {
                return SolarTerms[i];
            }
        }

        return null;
    }

    private static DateTime GetTermDate(int year, int index)
    {
        var baseDate = new DateTime(1900, 1, 6, 2, 5, 0);
        var minutes = TermMinutes[index] + 525948.76 * (year - 1900);
        return baseDate.AddMinutes(minutes).Date;
    }

    private static string? GetFestival(DateTime date, int lunarMonth, int lunarDay, bool isLeap)
    {
        foreach (var (m, d, name) in SolarFestivals)
        {
            if (date.Month == m && date.Day == d)
            {
                return name;
            }
        }

        if (!isLeap)
        {
            foreach (var (m, d, name) in LunarFestivals)
            {
                if (lunarMonth == m && lunarDay == d)
                {
                    return name;
                }
            }
        }

        return null;
    }

    private static int YearDays(int year)
    {
        var sum = 348;
        var info = LunarInfo[year - 1900];
        for (var i = 0x8000; i > 0x8; i >>= 1)
        {
            if ((info & i) != 0)
            {
                sum++;
            }
        }

        return sum + LeapDays(year);
    }

    private static int LeapMonth(int year) => LunarInfo[year - 1900] & 0xf;

    private static int LeapDays(int year) =>
        LeapMonth(year) != 0
            ? (LunarInfo[year - 1900] & 0x10000) != 0 ? 30 : 29
            : 0;

    private static int MonthDays(int year, int month) =>
        (LunarInfo[year - 1900] & (0x10000 >> month)) != 0 ? 30 : 29;

    private static string ToChinaMonth(int month) =>
        month switch
        {
            1 => "正月", 2 => "二月", 3 => "三月", 4 => "四月", 5 => "五月", 6 => "六月",
            7 => "七月", 8 => "八月", 9 => "九月", 10 => "十月", 11 => "冬月", 12 => "腊月",
            _ => $"{month}月"
        };

    private static string ToChinaDay(int day)
    {
        if (day == 10) return "初十";
        if (day == 20) return "二十";
        if (day == 30) return "三十";

        var prefix = day switch
        {
            < 10 => "初",
            < 20 => "十",
            < 30 => "廿",
            _ => ""
        };

        var digit = new[] { "一", "二", "三", "四", "五", "六", "七", "八", "九" };
        return prefix + digit[day % 10 - 1];
    }

    private sealed record LunarDate(int Year, int Month, int Day, bool IsLeap, string MonthDayText);

    public sealed record LunarDayInfo(
        string Line,
        string SubLine,
        string WeekName,
        string ShortLunar,
        string? Mark);
}
