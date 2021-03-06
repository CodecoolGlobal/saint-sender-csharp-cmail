using SaintSender.DesktopUI.ViewModels;
using SaintSender.Core.Models;
using System.Windows;
using System.Windows.Input;

namespace SaintSender.DesktopUI.Views
{
    /// <summary>
    /// Interaction logic for Inbox.xaml
    /// </summary>
    public partial class Inbox : Window
    {
        private readonly InboxViewModel _vm;
        public Inbox()
        {
            // set DataContext to the ViewModel object
            _vm = new InboxViewModel();
            DataContext = _vm;
            InitializeComponent();
        }

        private void Logout(object sender, RoutedEventArgs e)
        {
            if (Isolate.isoStore.FileExists(Isolate._accountFilePath))
            {
                Isolate.isoStore.DeleteFile(Isolate._accountFilePath);
            }

            MainWindow login = new MainWindow();
            login.Show();
            Close();
        }

        private void SyncOffline(object sender, RoutedEventArgs e)
        {
            _vm.SyncOffline();
        }

        private void ForgetMeButton_Click(object sender, RoutedEventArgs e)
        {
            _vm.ForgetAccount();
            Logout(null, null);
        }

        private void SendEmailButton_Click(object sender, RoutedEventArgs e)
        {
            _vm.OpenSendEmailWindow();
        }

        private void ListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            // how do i pass the email?
            // SelectedItem="{Binding SelectedEmail}" was the answer
            _vm.OpenDetails();
        }

        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            _vm.Emails = Authentication.GetInbox();
        }
    }
}