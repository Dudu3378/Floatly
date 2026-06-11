using System.Drawing;
using System.IO;

namespace DeskLite.Services;

public static class AppIconService
{
    private static readonly string IconPath = Path.Combine(AppContext.BaseDirectory, "app.ico");

    public static Icon LoadTrayIcon() =>
        File.Exists(IconPath) ? new Icon(IconPath) : SystemIcons.Application;

    public static Uri? GetIconUri() =>
        File.Exists(IconPath) ? new Uri(IconPath, UriKind.Absolute) : null;
}
