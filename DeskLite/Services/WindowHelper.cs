using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;

namespace DeskLite.Services;

public static class WindowHelper
{
    private const int GwlExstyle = -20;
    private const int WsExTransparent = 0x00000020;
    private const int WmNchitTest = 0x0084;
    private const int HtClient = 1;
    private const int HtLeft = 10;
    private const int HtRight = 11;
    private const int HtTop = 12;
    private const int HtTopLeft = 13;
    private const int HtTopRight = 14;
    private const int HtBottom = 15;
    private const int HtBottomLeft = 16;
    private const int HtBottomRight = 17;

    [DllImport("user32.dll")]
    private static extern int GetWindowLong(IntPtr hwnd, int index);

    [DllImport("user32.dll")]
    private static extern int SetWindowLong(IntPtr hwnd, int index, int newStyle);

    public static void SetClickThrough(Window window, bool enabled)
    {
        var hwnd = new WindowInteropHelper(window).Handle;
        if (hwnd == IntPtr.Zero)
        {
            return;
        }

        var style = GetWindowLong(hwnd, GwlExstyle);
        if (enabled)
        {
            SetWindowLong(hwnd, GwlExstyle, style | WsExTransparent);
        }
        else
        {
            SetWindowLong(hwnd, GwlExstyle, style & ~WsExTransparent);
        }
    }

    public static void EnableBorderlessResize(Window window, int grip = 8)
    {
        window.SourceInitialized += (_, _) =>
        {
            var source = HwndSource.FromHwnd(new WindowInteropHelper(window).Handle);
            source?.AddHook((IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled) =>
            {
                if (msg != WmNchitTest)
                {
                    return IntPtr.Zero;
                }

                var x = (short)(lParam.ToInt64() & 0xFFFF);
                var y = (short)((lParam.ToInt64() >> 16) & 0xFFFF);
                var point = window.PointFromScreen(new System.Windows.Point(x, y));
                var w = window.ActualWidth;
                var h = window.ActualHeight;

                if (point.X < 0 || point.Y < 0 || point.X > w || point.Y > h)
                {
                    return IntPtr.Zero;
                }

                var onLeft = point.X <= grip;
                var onRight = point.X >= w - grip;
                var onTop = point.Y <= grip;
                var onBottom = point.Y >= h - grip;

                handled = true;
                if (onTop && onLeft)
                {
                    return (IntPtr)HtTopLeft;
                }

                if (onTop && onRight)
                {
                    return (IntPtr)HtTopRight;
                }

                if (onBottom && onLeft)
                {
                    return (IntPtr)HtBottomLeft;
                }

                if (onBottom && onRight)
                {
                    return (IntPtr)HtBottomRight;
                }

                if (onLeft)
                {
                    return (IntPtr)HtLeft;
                }

                if (onRight)
                {
                    return (IntPtr)HtRight;
                }

                if (onTop)
                {
                    return (IntPtr)HtTop;
                }

                if (onBottom)
                {
                    return (IntPtr)HtBottom;
                }

                handled = false;
                return (IntPtr)HtClient;
            });
        };
    }
}
