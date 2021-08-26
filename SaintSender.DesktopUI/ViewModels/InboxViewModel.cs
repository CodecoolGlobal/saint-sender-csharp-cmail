using SaintSender.Core.Models;
using SaintSender.DesktopUI.Views;
using System.Collections.ObjectModel;
using System.ComponentModel;
using SaintSender.Core.Models.SaintSender.Core.Models;

namespace SaintSender.DesktopUI.ViewModels
{
    class InboxViewModel : INotifyPropertyChanged

    {
        public event PropertyChangedEventHandler PropertyChanged;

        private ObservableCollection<Email> _emails;

        public ObservableCollection<Email> Emails
        {
            get => _emails;
            set
            {
                _emails = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Emails)));
            }
        }

        public InboxViewModel()
        {
            Emails = Authentication.GetInbox();
        }

        internal void OpenSendEmailWindow()
        {
            SendEmailWindow sendEmailWindow = new SendEmailWindow();
            sendEmailWindow.Show();
        }

        public void ForgetAccount()
        {
            Isolate.DeleteFromIsolatedStorage();
        }
    }
}
