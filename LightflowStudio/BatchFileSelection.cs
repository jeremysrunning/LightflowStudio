using System.IO;

namespace LightflowStudio;

internal static class BatchFileSelection
{
    public static IReadOnlyList<BatchFileOption> Discover(string folder, bool recursive) =>
        MediaFileCatalog.Discover(folder, recursive)
            .Select(path => new BatchFileOption(path, Path.GetRelativePath(folder, path)))
            .ToList();

    public static IReadOnlyList<string> SelectedFiles(IEnumerable<BatchFileOption> options) =>
        options.Where(option => option.IsSelected).Select(option => option.FilePath).ToList();

    public static string Summary(IEnumerable<BatchFileOption> options)
    {
        var items = options.ToList();
        var selected = items.Count(item => item.IsSelected);
        return items.Count == 0
            ? "No supported video files found"
            : $"{selected} of {items.Count} selected";
    }
}
