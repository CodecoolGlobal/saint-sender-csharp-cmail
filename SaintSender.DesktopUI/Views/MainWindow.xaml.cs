using SaintSender.Core.Models;
using SaintSender.Core.Models.SaintSender.Core.Models;
using SaintSender.DesktopUI.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace SaintSender.DesktopUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly MainWindowViewModel _vm;

        public MainWindow()
        {
            // set DataContext to the ViewModel object
            _vm = new MainWindowViewModel();
            DataContext = _vm;
            InitializeComponent();
        }

        private void LoginBtn_Click(object sender, RoutedEventArgs e)
        {            
            StatusCodes status = _vm.Login(Email.Text, passwordBox.Password);
            if (status == StatusCodes.auth_success)
                Close();
            else if (status == StatusCodes.auth_nonet)
            {
                Button offline = (Button)FindName("btn_offline");
                offline.IsEnabled = true;
                offline.Opacity = 1.0d;
            }

            if ((bool)Checkbox.IsChecked)
            {
                _vm.StoreAccount(Authentication.Account);
            }
        }

        private void Window_ContentRendered(object sender, System.EventArgs e)
        {

            if (Isolate.isoStore.FileExists(Isolate._accountFilePath))
            {
                Account account = Isolate.ReadFromIsolatedStorage();
                if (!(account is null))
                {
                    StatusCodes status = _vm.Login(account.Email, account.Password);
                    if (status == StatusCodes.auth_success)
                        Close();
                    else if (status == StatusCodes.auth_nonet)
                    {
                        Button offline = (Button)FindName("btn_offline");
                        offline.IsEnabled = true;
                        offline.Opacity = 1.0d;
                    }
                }
            }
        }

        private void StartOffline(object sender, RoutedEventArgs e)
        {
            Button offline = (Button)sender;
            if (_vm.LoginOffline()) Close();
            else offline.IsEnabled = false;
        }
    }
}
