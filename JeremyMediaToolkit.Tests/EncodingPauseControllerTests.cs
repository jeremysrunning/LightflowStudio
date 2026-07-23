using JeremyMediaToolkit;
using Xunit;

namespace JeremyMediaToolkit.Tests;

public sealed class EncodingPauseControllerTests
{
    [Fact]
    public void PauseAndResume_InvokeLifecycleOnce()
    {
        var suspendCalls = 0;
        var resumeCalls = 0;
        var controller = new EncodingPauseController(_ => { suspendCalls++; return true; }, _ => resumeCalls++);

        Assert.True(controller.Pause(null));
        Assert.True(controller.Pause(null));
        controller.Resume(null);
        controller.Resume(null);

        Assert.Equal(1, suspendCalls);
        Assert.Equal(1, resumeCalls);
        Assert.False(controller.IsPaused);
    }

    [Fact]
    public void FailedPause_DoesNotAttemptResume()
    {
        var resumeCalls = 0;
        var controller = new EncodingPauseController(_ => false, _ => resumeCalls++);

        Assert.False(controller.Pause(null));
        controller.Resume(null);

        Assert.Equal(0, resumeCalls);
        Assert.False(controller.IsPaused);
    }

    [Fact]
    public void Clear_ForgetsSuspendedProcessWithoutResumingIt()
    {
        var resumeCalls = 0;
        var controller = new EncodingPauseController(_ => true, _ => resumeCalls++);

        controller.Pause(null);
        controller.Clear();
        controller.Resume(null);

        Assert.Equal(0, resumeCalls);
        Assert.False(controller.IsPaused);
    }
}