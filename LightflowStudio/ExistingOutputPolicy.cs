namespace LightflowStudio;

internal static class ExistingOutputPolicy
{
    public static bool ShouldPreserve(bool overwriteExisting, bool outputExists, long outputLength) =>
        !overwriteExisting && outputExists && outputLength > 0;
}
