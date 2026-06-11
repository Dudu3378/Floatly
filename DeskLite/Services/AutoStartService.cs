using Microsoft.Win32;

namespace DeskLite.Services;

public static class AutoStartService
{
    private const string RunKeyPath = @"Software\Microsoft\Windows\CurrentVersion\Run";

    public static void MigrateLegacyAutoStartIfNeeded()
    {
        using var key = Registry.CurrentUser.OpenSubKey(RunKeyPath, true);
        if (key is null)
        {
            return;
        }

        if (key.GetValue(AppConstants.AutoStartRegistryValueName) is string)
        {
            return;
        }

        if (key.GetValue(AppConstants.LegacyAutoStartRegistryValueName) is string legacyValue)
        {
            key.SetValue(AppConstants.AutoStartRegistryValueName, legacyValue);
            key.DeleteValue(AppConstants.LegacyAutoStartRegistryValueName, false);
        }
    }

    public static bool IsEnabled()
    {
        MigrateLegacyAutoStartIfNeeded();
        using var key = Registry.CurrentUser.OpenSubKey(RunKeyPath, false);
        return key?.GetValue(AppConstants.AutoStartRegistryValueName) is string;
    }

    public static void SetEnabled(bool enabled)
    {
        MigrateLegacyAutoStartIfNeeded();
        using var key = Registry.CurrentUser.OpenSubKey(RunKeyPath, true);
        if (key is null)
        {
            return;
        }

        if (enabled)
        {
            var exe = Environment.ProcessPath;
            if (!string.IsNullOrWhiteSpace(exe))
            {
                key.SetValue(AppConstants.AutoStartRegistryValueName, $"\"{exe}\"");
            }

            key.DeleteValue(AppConstants.LegacyAutoStartRegistryValueName, false);
        }
        else
        {
            key.DeleteValue(AppConstants.AutoStartRegistryValueName, false);
            key.DeleteValue(AppConstants.LegacyAutoStartRegistryValueName, false);
        }
    }
}
