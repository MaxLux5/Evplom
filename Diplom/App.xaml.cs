using Diplom.View;
using Diplom.ViewModel;
using System.Windows;

namespace Diplom;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        MainWindow mainWindow = new MainWindow();
        mainWindow.DataContext = new MainWindowViewModel();
        mainWindow.Show();
    }
}

