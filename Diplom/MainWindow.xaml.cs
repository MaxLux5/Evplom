using System.Windows;
using System.Windows.Controls;

namespace Diplom;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private ViewModel ViewModel => (ViewModel)DataContext;
    public MainWindow()
    {
        InitializeComponent();
        DataContext = new ViewModel(TextPresenter);
    }

    private void TextBox_SelectionChanged(object sender, RoutedEventArgs e)
    {
        if (ViewModel.Selection is null)
            return;
        var tb = (TextBox)sender;
        ViewModel.Selection.Line = tb.GetLineIndexFromCharacterIndex(tb.SelectionStart);
        ViewModel.Selection.Text = tb.SelectedText;
    }

    private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
    {
        ViewModel.SaveFile();
    }
}