using System.Windows;
using System.Windows.Media;
using WpfButton = System.Windows.Controls.Button;
using WpfTextBox = System.Windows.Controls.TextBox;
using WpfColor = System.Windows.Media.Color;

namespace DeskLite.Services;

public static class TodoThemeHelper
{
    public static void ApplyResources(ResourceDictionary resources, AppThemePalette palette)
    {
        resources["TodoTextBrush"] = Brush(palette.TodoText);
        resources["TodoDeleteBrush"] = Brush(palette.DeleteButton);
        resources["TodoCardBgBrush"] = Brush(palette.TodoCardBackground);
        resources["TodoCardBorderBrush"] = Brush(palette.TodoCardBorder);
        resources["TodoCardHoverBrush"] = Brush(palette.TodoCardHover);
        resources["TodoTimeBadgeBrush"] = Brush(palette.TodoTimeBadge);
        resources["TodoPinActiveBrush"] = Brush(palette.TodoPinActive);
        resources["TodoPinInactiveBrush"] = Brush(palette.TodoPinInactive);
        resources["TodoActionHoverBrush"] = Brush(palette.TodoActionHover);
        resources["TodoCountBadgeBrush"] = Brush(palette.TodoCountBadge);
        resources["TodoLinkBrush"] = Brush(palette.TodoLink);
        resources["TodoAccentBrush"] = Brush(palette.Accent);
        resources["TodoAccentButtonBrush"] = Brush(palette.TodoAccentButton);
        resources["TodoInputBgBrush"] = Brush(palette.InputBackground);
        resources["TodoInputBorderBrush"] = Brush(palette.InputBorder);
        resources["TodoInputTextBrush"] = Brush(palette.InputText);
        resources["TodoMutedBrush"] = Brush(palette.TextMuted);
        resources["TodoEmptyBrush"] = Brush(palette.TextEmpty);
        resources["TodoSecondaryBrush"] = Brush(palette.TextSecondary);
        resources["TodoPrimaryBrush"] = Brush(palette.TextPrimary);
        resources["TodoPanelBgBrush"] = Brush(palette.PanelBackground);
        resources["TodoPanelBorderBrush"] = Brush(palette.PanelBorder);
        resources["TodoDividerBrush"] = Brush(palette.Divider);
    }

    public static void StyleActionButton(WpfButton button, double fontSize)
    {
        button.Background = System.Windows.Media.Brushes.Transparent;
        button.BorderThickness = new Thickness(0);
        button.Cursor = System.Windows.Input.Cursors.Hand;
        button.FontSize = fontSize;
        button.Padding = new Thickness(0);
    }

    public static void StyleAccentButton(WpfButton button, AppThemePalette palette, double fontSize)
    {
        button.Background = Brush(palette.TodoAccentButton);
        button.Foreground = System.Windows.Media.Brushes.White;
        button.BorderThickness = new Thickness(0);
        button.Cursor = System.Windows.Input.Cursors.Hand;
        button.FontSize = fontSize;
    }

    public static void StyleInput(WpfTextBox box, AppThemePalette palette, double fontSize)
    {
        box.Background = Brush(palette.InputBackground);
        box.BorderBrush = Brush(palette.InputBorder);
        box.Foreground = Brush(palette.InputText);
        box.FontSize = fontSize;
    }

    private static SolidColorBrush Brush(WpfColor color) => new(color);
}
