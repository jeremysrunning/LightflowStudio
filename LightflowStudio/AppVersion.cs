namespace LightflowStudio;

public static class AppVersion
{
    public static string Display => Format(typeof(AppVersion).Assembly.GetName().Version);

    public static string Format(Version? version) =>
        version is null ? "Unknown" : $"{version.Major}.{version.Minor}.{version.Build}";
}