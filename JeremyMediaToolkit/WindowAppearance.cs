using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;

namespace JeremyMediaToolkit;

internal static class WindowAppearance
{
    private const int DwmUseImmersiveDarkModeBefore20H1 = 19;
    private const int DwmUseImmersiveDarkMode = 20;

    public static void EnableDarkTitleBar(Window window)
    {
        if (!OperatingSystem.IsWindows()) return;
        var handle = new WindowInteropHelper(window).EnsureHandle();
        var enabled = 1;
        if (DwmSetWindowAttribute(handle, DwmUseImmersiveDarkMode, ref enabled, sizeof(int)) != 0)
            DwmSetWindowAttribute(handle, DwmUseImmersiveDarkModeBefore20H1, ref enabled, sizeof(int));
    }

    [DllImport("dwmapi.dll")]
    private static extern int DwmSetWindowAttribute(IntPtr window, int attribute, ref int value, int valueSize);
}
