using System.Diagnostics;

namespace JeremyMediaToolkit;

internal sealed class EncodingPauseController
{
    private readonly Func<Process?, bool> _suspend;
    private readonly Action<Process?> _resume;

    public EncodingPauseController(Func<Process?, bool>? suspend = null, Action<Process?>? resume = null)
    {
        _suspend = suspend ?? ProcessSuspender.TrySuspend;
        _resume = resume ?? ProcessSuspender.TryResume;
    }

    public bool IsPaused { get; private set; }

    public bool Pause(Process? process)
    {
        if (IsPaused) return true;
        IsPaused = _suspend(process);
        return IsPaused;
    }

    public void Resume(Process? process)
    {
        if (!IsPaused) return;
        _resume(process);
        IsPaused = false;
    }

    public void Clear() => IsPaused = false;
}