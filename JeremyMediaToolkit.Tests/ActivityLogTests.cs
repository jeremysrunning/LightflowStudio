using JeremyMediaToolkit;
using Xunit;

namespace JeremyMediaToolkit.Tests;

public sealed class ActivityLogTests
{
    [Fact]
    public void Prepend_PutsNewestEntryFirst()
    {
        var text = ActivityLog.Prepend("Older entry", "Newest entry");

        Assert.Equal($"Newest entry{Environment.NewLine}Older entry", text);
    }

    [Fact]
    public void Prepend_TrimsTrailingLineBreaks()
    {
        Assert.Equal("Entry", ActivityLog.Prepend("", "Entry\r\n"));
    }

    [Fact]
    public void Prepend_IgnoresBlankEntries()
    {
        Assert.Equal("Existing", ActivityLog.Prepend("Existing", "\r\n"));
    }
}
