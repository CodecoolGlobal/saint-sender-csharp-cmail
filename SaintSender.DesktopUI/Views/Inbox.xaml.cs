using SaintSender.DesktopUI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

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

        private void ForgetMeButton_Click(object sender, RoutedEventArgs e)
        {
            _vm.ForgetAccount();
        }

        private void SendEmailButton_Click(object sender, RoutedEventArgs e)
        {
            _vm.SendEmail();
        }

        private void ListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            // how do i pass the email?
            // SelectedItem="{Binding SelectedEmail}" was the answer
            _vm.OpenDetails();
        }
    }
}