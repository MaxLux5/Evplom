using Diplom.ViewModel;
using System.Windows;
using System.Windows.Controls;

namespace Diplom.View
{
    /// <summary>
    /// Логика взаимодействия для MainMenu.xaml
    /// </summary>
    public partial class MainMenu : UserControl
    {
        private MainViewModel ViewModel => (MainViewModel)DataContext;
        public MainMenu()
        {
            InitializeComponent();
            DataContext = new MainViewModel(TextPresenter);
        }

        private void TextBox_SelectionChanged(object sender, RoutedEventArgs e)
        {
            if (ViewModel.Selection is null)
                return;
            var tb = (TextBox)sender;
            ViewModel.Selection.Line = tb.GetLineIndexFromCharacterIndex(tb.SelectionStart);
            ViewModel.Selection.Text = tb.SelectedText;
        }

        public void Closing()
        {
            ViewModel.SaveFile();
        }
    }
}
