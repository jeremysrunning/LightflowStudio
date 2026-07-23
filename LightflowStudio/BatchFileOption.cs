using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace LightflowStudio;

internal sealed class BatchFileOption : INotifyPropertyChanged
{
    private bool _isSelected = true;

    public BatchFileOption(string filePath, string displayName)
    {
        FilePath = filePath;
        DisplayName = displayName;
    }

    public string FilePath { get; }
    public string DisplayName { get; }

    public bool IsSelected
    {
        get => _isSelected;
        set
        {
            if (_isSelected == value) return;
            _isSelected = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsSelected)));
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;
}
