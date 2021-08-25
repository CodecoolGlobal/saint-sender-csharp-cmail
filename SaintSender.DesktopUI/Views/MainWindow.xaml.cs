using SaintSender.Core.Models;
using SaintSender.DesktopUI.ViewModels;
using System.Windows;

namespace SaintSender.DesktopUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainWindowViewModel _vm;

        public MainWindow()
        {
            // set DataContext to the ViewModel object
            _vm = new MainWindowViewModel();
            DataContext = _vm;
            InitializeComponent();
        }

        private void LoginBtn_Click(object sender, RoutedEventArgs e)
        {            
            _vm.Login(Email.Text, passwordBox.Password);
            Close();

            if ((bool)Checkbox.IsChecked)
            {
                _vm.StoreAccount(Authentication.Account);
            }
        }

        private void Window_ContentRendered(object sender, System.EventArgs e)
        {

            if (Isolate.isoStore.FileExists("AccountStore.txt") == true)
            {
                Account account = Isolate.ReadFromIsolatedStorage();
                _vm.Login(account.Email, account.Password);
                Close();
            }
        }
    }
}
