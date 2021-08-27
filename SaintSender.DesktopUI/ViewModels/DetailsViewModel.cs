using SaintSender.Core.Models;
using SaintSender.DesktopUI.Views;
using System;
using System.ComponentModel;

namespace SaintSender.DesktopUI.ViewModels
{
    class DetailsViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public string Message { get; set; }
        public string Sender { get; set; }
        public DateTime Date { get; set; }
        public string Subject { get; set; }
        public bool Read { get; set; }
        public Email Email { get; set; }

        public DetailsViewModel(Email email)
        {
            Email = email;

            Message = email.Message;
            Sender = email.Sender;
            Subject = email.Subject;
            Date = email.Date;
        }

        internal void ReplyMail()
        {
            SendEmailWindow sendEmailWindow = new SendEmailWindow();
            sendEmailWindow.Show();
        }
    }
}
