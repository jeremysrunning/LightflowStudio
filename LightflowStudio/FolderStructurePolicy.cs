namespace LightflowStudio;

internal static class FolderStructurePolicy
{
    public static bool IsAvailable(bool includeSubfolders, OutputDestinationMode outputMode) =>
        includeSubfolders && outputMode is OutputDestinationMode.SameFolder or OutputDestinationMode.SpecificFolder;

    public static bool ShouldPreserve(bool includeSubfolders, OutputDestinationMode outputMode, bool selected) =>
        IsAvailable(includeSubfolders, outputMode) && selected;
}
