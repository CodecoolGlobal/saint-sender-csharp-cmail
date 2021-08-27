using SaintSender.Core.Models;
using SaintSender.DesktopUI.Views;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace SaintSender.DesktopUI.ViewModels
{
    public class InboxViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private ObservableCollection<Email> _emails;

        public Email SelectedEmail { get; set; }

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

        internal void OpenDetails()
        {
            // hardcoded email
            //SelectedEmail = Emails[0];

            Details details = new Details(SelectedEmail);
            details.Show();
        }

        internal void SyncOffline()
        {
            Isolate.SaveMail(_emails, Authentication.GetAddress());
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
