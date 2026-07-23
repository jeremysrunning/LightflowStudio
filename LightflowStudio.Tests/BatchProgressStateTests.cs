using Xunit;
using LightflowStudio;

namespace LightflowStudio.Tests;

public sealed class BatchProgressStateTests
{
    [Fact]
    public void StartBatch_ResetsProgressFromPreviousBatch()
    {
        var progress = new BatchProgressState();
        progress.StartBatch(2);
        progress.ReportBatchProgress(2, 2);
        progress.ReportFileProgress(100);

        progress.StartBatch(3);

        Assert.Equal(0, progress.BatchPercent);
        Assert.Equal(0, progress.FilePercent);
        Assert.Equal("Completed 0 of 3 — estimated remaining: calculating…", progress.StatusText);
    }

    [Fact]
    public void StartFile_ResetsOnlyFileProgress()
    {
        var progress = new BatchProgressState();
        progress.StartBatch(2);
        progress.ReportBatchProgress(1, 2);
        progress.ReportFileProgress(100);

        progress.StartFile();

        Assert.Equal(50, progress.BatchPercent);
        Assert.Equal(0, progress.FilePercent);
    }
}
