using System.IO;

namespace LightflowStudio;

internal sealed class BatchFileSelectionMemory
{
    private readonly HashSet<string> _deselectedFiles = new(StringComparer.OrdinalIgnoreCase);
    private string _folderKey = "";

    public void Remember(string folder, IEnumerable<BatchFileOption> files)
    {
        var folderKey = Normalize(folder);
        if (!string.Equals(folderKey, _folderKey, StringComparison.OrdinalIgnoreCase)) return;

        _deselectedFiles.Clear();
        foreach (var file in files.Where(file => !file.IsSelected))
            _deselectedFiles.Add(Normalize(file.FilePath));
    }

    public void Apply(string folder, IEnumerable<BatchFileOption> files)
    {
        var folderKey = Normalize(folder);
        if (!string.Equals(folderKey, _folderKey, StringComparison.OrdinalIgnoreCase))
        {
            _folderKey = folderKey;
            _deselectedFiles.Clear();
        }

        foreach (var file in files)
            file.IsSelected = !_deselectedFiles.Contains(Normalize(file.FilePath));
    }

    private static string Normalize(string path)
    {
        if (string.IsNullOrWhiteSpace(path)) return "";
        try { return Path.GetFullPath(path).TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar); }
        catch (Exception ex) when (ex is ArgumentException or NotSupportedException or PathTooLongException)
        {
            return path.Trim();
        }
    }
}
