using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Diplom.View;
using Diplom.View.Screens;
using Diplom.ViewModel.Screens;
using System.Windows.Controls;

namespace Diplom.ViewModel
{
    public partial class MainWindowViewModel : ObservableObject
    {
        [ObservableProperty]
        private UserControl _currentScreen;


        public MainWindowViewModel()
        {
            CurrentScreen = new AuthorizationScreen();
            var authViewModel = new AuthorizationScreenViewModel();
            CurrentScreen.DataContext = authViewModel;
            authViewModel.LoginCompleted += Login;
        }


        /// <summary>
        /// Смена на страницу с меню.
        /// </summary>
        private void Login(object? sender, EventArgs e)
        {
            var mainMenu = new MainMenu();
            ClosingEvent += (obj, e) => { mainMenu.Closing(); };
            CurrentScreen = mainMenu;
        }

        public event EventHandler ClosingEvent;
        [RelayCommand]
        private void Closing()
        {
            ClosingEvent?.Invoke(this, EventArgs.Empty);
        }
    }
}
