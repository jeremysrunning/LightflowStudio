namespace JeremyMediaToolkit;

internal static class ActivityLog
{
    public static string Prepend(string existing, string entry)
    {
        var normalized = entry.TrimEnd();
        if (string.IsNullOrEmpty(normalized)) return existing;
        return string.IsNullOrEmpty(existing)
            ? normalized
            : normalized + Environment.NewLine + existing;
    }
}
