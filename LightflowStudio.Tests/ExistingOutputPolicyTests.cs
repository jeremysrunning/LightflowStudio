using LightflowStudio;
using Xunit;

namespace LightflowStudio.Tests;

public sealed class ExistingOutputPolicyTests
{
    [Theory]
    [InlineData(false, true, 100, true)]
    [InlineData(true, true, 100, false)]
    [InlineData(false, false, 0, false)]
    [InlineData(false, true, 0, false)]
    public void ShouldPreserve_OnlyKeepsNonEmptyExistingFilesWhenOverwriteIsOff(
        bool overwrite, bool exists, long length, bool expected)
    {
        Assert.Equal(expected, ExistingOutputPolicy.ShouldPreserve(overwrite, exists, length));
    }
}
