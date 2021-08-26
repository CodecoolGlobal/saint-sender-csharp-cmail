using SaintSender.Core.Models;
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
    /// Interaction logic for Details.xaml
    /// </summary>
    public partial class Details : Window
    {
        private DetailsViewModel _vm;
        private InboxViewModel _inboxViewModel;

        public Details(Email email)
        {
            // set DataContext to the ViewModel object
            _vm = new DetailsViewModel(email);
            DataContext = _vm;
            InitializeComponent();
        }

        private void ReplyButton_Click(object sender, RoutedEventArgs e)
        {
            _vm.ReplyMail();
        }
    }
}
