using LightflowStudio;
using Xunit;

namespace LightflowStudio.Tests;

public sealed class BatchFileSelectionMemoryTests
{
    [Fact]
    public void Apply_PreservesDeselectionsWhenTheSourceFolderHasNotChanged()
    {
        var memory = new BatchFileSelectionMemory();
        var original = Options(@"C:\Videos");
        memory.Apply(@"C:\Videos", original);
        original[1].IsSelected = false;
        memory.Remember(@"C:\Videos", original);

        var refreshed = Options(@"C:\Videos");
        memory.Apply(@"C:\Videos", refreshed);

        Assert.True(refreshed[0].IsSelected);
        Assert.False(refreshed[1].IsSelected);
    }

    [Fact]
    public void Apply_SelectsEverythingWhenTheSourceFolderChanges()
    {
        var memory = new BatchFileSelectionMemory();
        var original = Options(@"C:\Videos");
        memory.Apply(@"C:\Videos", original);
        original[1].IsSelected = false;
        memory.Remember(@"C:\Videos", original);

        var replacement = Options(@"D:\Different");
        memory.Apply(@"D:\Different", replacement);

        Assert.All(replacement, file => Assert.True(file.IsSelected));
    }

    [Fact]
    public void Apply_SelectsNewFilesAddedToTheSameFolder()
    {
        var memory = new BatchFileSelectionMemory();
        var original = Options(@"C:\Videos");
        memory.Apply(@"C:\Videos", original);
        original[0].IsSelected = false;
        memory.Remember(@"C:\Videos", original);
        var added = new BatchFileOption(@"C:\Videos\third.mp4", "third.mp4");
        var refreshed = Options(@"C:\Videos").Append(added).ToList();

        memory.Apply(@"C:\Videos", refreshed);

        Assert.False(refreshed[0].IsSelected);
        Assert.True(added.IsSelected);
    }

    private static List<BatchFileOption> Options(string folder) =>
    [
        new(Path.Combine(folder, "first.mp4"), "first.mp4"),
        new(Path.Combine(folder, "second.mp4"), "second.mp4")
    ];
}
