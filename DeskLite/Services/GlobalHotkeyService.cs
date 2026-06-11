using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;

namespace DeskLite.Services;

public sealed class GlobalHotkeyService : IDisposable
{
    private const int WmHotkey = 0x0312;
    private const uint ModControl = 0x0002;
    private const uint ModShift = 0x0004;
    private const int IdToggle = 1;
    private const int IdQuickTodo = 2;

    private readonly Window _window;
    private readonly Action _onToggleWindow;
    private readonly Action _onQuickTodo;
    private HwndSource? _source;
    private bool _registered;

    public GlobalHotkeyService(Window window, Action onToggleWindow, Action onQuickTodo)
    {
        _window = window;
        _onToggleWindow = onToggleWindow;
        _onQuickTodo = onQuickTodo;
    }

    public void Register()
    {
        if (_registered)
        {
            return;
        }

        var helper = new WindowInteropHelper(_window);
        if (helper.Handle == IntPtr.Zero)
        {
            return;
        }

        _source = HwndSource.FromHwnd(helper.Handle);
        _source?.AddHook(WndProc);

        RegisterHotKey(helper.Handle, IdToggle, ModControl | ModShift, 0x44); // Ctrl+Shift+D
        RegisterHotKey(helper.Handle, IdQuickTodo, ModControl | ModShift, 0x4E); // Ctrl+Shift+N
        _registered = true;
    }

    public void Unregister()
    {
        if (!_registered)
        {
            return;
        }

        var helper = new WindowInteropHelper(_window);
        if (helper.Handle != IntPtr.Zero)
        {
            UnregisterHotKey(helper.Handle, IdToggle);
            UnregisterHotKey(helper.Handle, IdQuickTodo);
        }

        _source?.RemoveHook(WndProc);
        _registered = false;
    }

    private IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
    {
        if (msg != WmHotkey)
        {
            return IntPtr.Zero;
        }

        switch (wParam.ToInt32())
        {
            case IdToggle:
                _onToggleWindow();
                handled = true;
                break;
            case IdQuickTodo:
                _onQuickTodo();
                handled = true;
                break;
        }

        return IntPtr.Zero;
    }

    public void Dispose() => Unregister();

    [DllImport("user32.dll")]
    private static extern bool RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, uint vk);

    [DllImport("user32.dll")]
    private static extern bool UnregisterHotKey(IntPtr hWnd, int id);
}
