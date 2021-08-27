using SaintSender.DesktopUI.ViewModels;
using System.Windows;

namespace SaintSender.DesktopUI.Views
{
    /// <summary>
    /// Interaction logic for SendEmailWindow.xaml
    /// </summary>
    public partial class SendEmailWindow : Window
    {

        private readonly SendEmailViewModel _vm;
        public SendEmailWindow()
        {
            _vm = new SendEmailViewModel();
            DataContext = _vm;
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _vm.SendEmail();
            Close();
        }
    }
}
