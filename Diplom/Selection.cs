using CommunityToolkit.Mvvm.ComponentModel;

namespace Diplom;

partial class Selection : ObservableObject
{
    [ObservableProperty] private int _line;
    [ObservableProperty] private string _text = string.Empty;
    
    public void Reset()
    {
        Line = 0;
        Text = string.Empty;
    }

    public bool IsEmpty() =>
        string.IsNullOrEmpty(Text);
}
