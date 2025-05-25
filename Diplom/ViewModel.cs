using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Win32;
using System.ComponentModel;
using System.Windows.Controls;

namespace Diplom;

partial class ViewModel : ObservableObject
{
    [ObservableProperty, NotifyCanExecuteChangedFor(nameof(CreateHeaderCommand))] private TextFile? _textFile;
    [ObservableProperty, NotifyCanExecuteChangedFor(nameof(DeleteHeaderCommand), nameof(JumpCommand))] private Header? _selectedHeader;
    [ObservableProperty, NotifyCanExecuteChangedFor(nameof(CreateHeaderCommand))] private Selection _selection;
    [ObservableProperty] private bool _isNotReady = true;
    private readonly TextBox _presenter;

    public ViewModel(TextBox presenter)
    {
        _selection = new();
        _selection.PropertyChanged += NotifySelectionChanged;
        _presenter = presenter;
    }

    [RelayCommand(CanExecute = nameof(CreateHeaderCanExecute))]
    public void CreateHeader()
    {
        TextFile?.CreateHeader(Selection.Line, Selection.Text);
    }

    [RelayCommand(CanExecute = nameof(IsHeaderSelected))]
    public void DeleteHeader()
    {
        TextFile?.DeleteHeader(SelectedHeader!);
    }

    [RelayCommand(CanExecute = nameof(IsHeaderSelected))]
    public void Jump()
    {
        _presenter.ScrollToLine(SelectedHeader!.Line);
    }

    [RelayCommand]
    public void OpenFile()
    {
        Selection.Reset();
        var dialog = new OpenFileDialog();
        dialog.Filter = "Text files|*.mtxt";
        var result = dialog.ShowDialog();
        if (result != true)
            return;
        TextFile = TextFile.Open(dialog.FileName);
        IsNotReady = false;
    }

    [RelayCommand]
    public void CreateFile()
    {
        Selection.Reset();
        var dialog = new SaveFileDialog();
        dialog.Filter = "Text files|*.mtxt";
        dialog.AddExtension = true;
        var result = dialog.ShowDialog();
        if (result != true)
            return;
        TextFile = TextFile.Create(dialog.FileName);
        IsNotReady = false;
    }

    [RelayCommand]
    public void SaveFile()
    {
        TextFile?.Save();
    }

    private void NotifySelectionChanged(object? sender, PropertyChangedEventArgs args)
    {
        OnPropertyChanged(new PropertyChangedEventArgs(nameof(Selection)));
        CreateHeaderCommand.NotifyCanExecuteChanged();
    }
    private bool CreateHeaderCanExecute() =>
        TextFile is not null && (!Selection?.IsEmpty() ?? false);
    private bool IsHeaderSelected() =>
        SelectedHeader is not null;
}
