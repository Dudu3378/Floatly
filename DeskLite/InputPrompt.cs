using System.Windows;
using WpfTextBox = System.Windows.Controls.TextBox;
using WpfButton = System.Windows.Controls.Button;

namespace DeskLite;

public static class InputPrompt
{
    public static string? Show(string title, string message, string defaultValue = "")
    {
        var box = new WpfTextBox
        {
            Text = defaultValue,
            Margin = new Thickness(0, 8, 0, 0),
            Padding = new Thickness(6, 4, 6, 4),
            MinWidth = 220
        };

        var ok = false;
        string? result = null;
        Window? dialog = null;

        dialog = new Window
        {
            Title = title,
            Width = 280,
            Height = 150,
            WindowStartupLocation = WindowStartupLocation.CenterScreen,
            ResizeMode = ResizeMode.NoResize,
            WindowStyle = WindowStyle.ToolWindow
        };

        dialog.Content = BuildContent(message, box, () =>
        {
            ok = true;
            result = box.Text.Trim();
            dialog.Close();
        }, () => dialog.Close());

        dialog.ShowDialog();
        return ok ? result : null;
    }

    private static UIElement BuildContent(string message, WpfTextBox box, Action onOk, Action onCancel)
    {
        var panel = new System.Windows.Controls.StackPanel { Margin = new Thickness(16) };
        panel.Children.Add(new System.Windows.Controls.TextBlock { Text = message, TextWrapping = TextWrapping.Wrap });
        panel.Children.Add(box);

        var buttons = new System.Windows.Controls.StackPanel
        {
            Orientation = System.Windows.Controls.Orientation.Horizontal,
            HorizontalAlignment = System.Windows.HorizontalAlignment.Right,
            Margin = new Thickness(0, 12, 0, 0)
        };
        var okBtn = new WpfButton { Content = "确定", Width = 64, Margin = new Thickness(0, 0, 8, 0), IsDefault = true };
        okBtn.Click += (_, _) => onOk();
        var cancelBtn = new WpfButton { Content = "取消", Width = 64, IsCancel = true };
        cancelBtn.Click += (_, _) => onCancel();
        buttons.Children.Add(okBtn);
        buttons.Children.Add(cancelBtn);
        panel.Children.Add(buttons);
        return panel;
    }
}
