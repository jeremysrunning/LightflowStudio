using LightflowStudio;
using Xunit;

namespace LightflowStudio.Tests;

public sealed class FolderStructurePolicyTests
{
    [Theory]
    [InlineData(false, OutputDestinationMode.SameFolder, false)]
    [InlineData(false, OutputDestinationMode.SpecificFolder, false)]
    [InlineData(true, OutputDestinationMode.Subfolder, false)]
    [InlineData(true, OutputDestinationMode.SameFolder, true)]
    [InlineData(true, OutputDestinationMode.SpecificFolder, true)]
    internal void IsAvailable_OnlyForRecursiveNonSubfolderOutputs(
        bool recursive, OutputDestinationMode mode, bool expected)
    {
        Assert.Equal(expected, FolderStructurePolicy.IsAvailable(recursive, mode));
    }

    [Fact]
    public void ShouldPreserve_RequiresAvailabilityAndSelection()
    {
        Assert.True(FolderStructurePolicy.ShouldPreserve(true, OutputDestinationMode.SpecificFolder, true));
        Assert.False(FolderStructurePolicy.ShouldPreserve(true, OutputDestinationMode.SpecificFolder, false));
        Assert.False(FolderStructurePolicy.ShouldPreserve(true, OutputDestinationMode.Subfolder, true));
        Assert.False(FolderStructurePolicy.ShouldPreserve(false, OutputDestinationMode.SameFolder, true));
    }
}
