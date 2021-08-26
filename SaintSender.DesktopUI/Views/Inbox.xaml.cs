using SaintSender.DesktopUI.ViewModels;
using System;
using SaintSender.Core.Models;
using System.Windows;

namespace SaintSender.DesktopUI.Views
{
    /// <summary>
    /// Interaction logic for Inbox.xaml
    /// </summary>
    public partial class Inbox : Window
    {
        private InboxViewModel _vm;

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
                Isolate.isoStore.DeleteFile(Isolate._accountFilePath);
            MainWindow login = new MainWindow();
            login.Show();
            Close();
        }
        
        private void ForgetMeButton_Click(object sender, RoutedEventArgs e)
        {
            _vm.ForgetAccount();
        }

        private void SendEmailButton_Click(object sender, RoutedEventArgs e)
        {
            _vm.SendEmail();
        }
    }
}