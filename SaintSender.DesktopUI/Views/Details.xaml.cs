using SaintSender.Core.Models;
using SaintSender.DesktopUI.ViewModels;
using System.Windows;

namespace SaintSender.DesktopUI.Views
{
    /// <summary>
    /// Interaction logic for Details.xaml
    /// </summary>
    public partial class Details : Window
    {
        private readonly DetailsViewModel _vm;

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
