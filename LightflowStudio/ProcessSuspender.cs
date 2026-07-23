using System.Diagnostics;
using System.Runtime.InteropServices;

namespace LightflowStudio;

internal static class ProcessSuspender
{
    [DllImport("ntdll.dll")]
    private static extern int NtSuspendProcess(IntPtr processHandle);

    [DllImport("ntdll.dll")]
    private static extern int NtResumeProcess(IntPtr processHandle);

    public static bool TrySuspend(Process? process)
    {
        try
        {
            return process is { HasExited: false } && NtSuspendProcess(process.Handle) >= 0;
        }
        catch (Exception exception) when (exception is InvalidOperationException or System.ComponentModel.Win32Exception or PlatformNotSupportedException)
        {
            return false;
        }
    }

    public static void TryResume(Process? process)
    {
        try
        {
            if (process is { HasExited: false }) NtResumeProcess(process.Handle);
        }
        catch (Exception exception) when (exception is InvalidOperationException or System.ComponentModel.Win32Exception or PlatformNotSupportedException)
        {
            // The process may have exited while the close dialog was open.
        }
    }
}