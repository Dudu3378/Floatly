using System.Windows;
using System.Windows.Controls;
using WpfButton = System.Windows.Controls.Button;
using WpfTextBox = System.Windows.Controls.TextBox;

namespace DeskLite.Services;

public static class FontScaleHelper
{
    public static double ClampScale(double scale) => Math.Clamp(scale, 0.85, 1.35);

    public static void Apply(MainWindow window, double scale)
    {
        scale = ClampScale(scale);

        Set(window.ClockText, 26, scale);
        Set(window.DateText, 13, scale);
        Set(window.LunarText, 12, scale);
        Set(window.LunarSubText, 11, scale);
        Set(window.YearProgressLabel, 11, scale);
        Set(window.YearProgressPercent, 13, scale);
        Set(window.YearProgressDetail, 10, scale);
        window.YearProgressTrack.Height = 7 * scale;
        Set(window.CityText, 11, scale);
        Set(window.WeatherText, 12, scale);
        Set(window.WeatherExtraText, 11, scale);
        Set(window.CountdownLabel, 11, scale);
        Set(window.CountdownDays, 13, scale);
        Set(window.CountdownHint, 10, scale);
        window.CountdownTrack.Height = 7 * scale;
        Set(window.DailyQuoteText, 11, scale);
        Set(window.ScratchBox, 11, scale);
        Set(window.TodoTitleText, 12, scale);
        Set(window.EmptyTodoText, 12, scale);
        Set(window.CalendarTitleText, 11, scale);
        Set(window.NewTodoBox, 12, scale);
        Set(window.AddButton, 12, scale);

        window.CalPrevBtn.FontSize = 14 * scale;
        window.CalNextBtn.FontSize = 14 * scale;
        window.CalWeekModeBtn.FontSize = 10 * scale;
        window.CalMonthModeBtn.FontSize = 10 * scale;
    }

    private static void Set(DependencyObject element, double baseSize, double scale)
    {
        switch (element)
        {
            case TextBlock tb:
                tb.FontSize = baseSize * scale;
                break;
            case WpfTextBox box:
                box.FontSize = baseSize * scale;
                break;
            case WpfButton btn:
                btn.FontSize = baseSize * scale;
                break;
        }
    }
}
