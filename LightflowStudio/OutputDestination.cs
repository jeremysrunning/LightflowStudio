using System.IO;

namespace LightflowStudio;

internal enum OutputDestinationMode
{
    SameFolder,
    Subfolder,
    SpecificFolder
}

internal sealed record OutputDestinationOptions(
    OutputDestinationMode Mode,
    string SubfolderName,
    string SpecificFolder);

internal static class OutputDestinationPlanner
{
    public static string ResolveRoot(string inputFolder, OutputResolution resolution, OutputDestinationOptions options)
    {
        if (!Directory.Exists(inputFolder)) throw new ArgumentException("Select a valid video folder.", nameof(inputFolder));
        if (!Enum.IsDefined(options.Mode)) throw new ArgumentOutOfRangeException(nameof(options));

        return options.Mode switch
        {
            OutputDestinationMode.SameFolder => inputFolder,
            OutputDestinationMode.Subfolder => Path.Combine(inputFolder, ResolveSubfolderName(resolution, options.SubfolderName)),
            OutputDestinationMode.SpecificFolder when !string.IsNullOrWhiteSpace(options.SpecificFolder) =>
                Path.GetFullPath(options.SpecificFolder.Trim()),
            OutputDestinationMode.SpecificFolder =>
                throw new ArgumentException("Choose a specific output folder.", nameof(options)),
            _ => throw new ArgumentOutOfRangeException(nameof(options))
        };
    }

    public static string ResolveSubfolderName(OutputResolution resolution, string name)
    {
        var value = string.IsNullOrWhiteSpace(name) ? EncodingPathPlanner.ResolutionName(resolution) : name.Trim();
        if (Path.IsPathRooted(value)
            || value.IndexOfAny(Path.GetInvalidFileNameChars()) >= 0
            || value.Contains(Path.DirectorySeparatorChar)
            || value.Contains(Path.AltDirectorySeparatorChar)
            || value is "." or "..")
            throw new ArgumentException("The output subfolder must be a single valid folder name.", nameof(name));
        return value;
    }
}
