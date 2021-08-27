using SaintSender.Core.Models;
using SaintSender.DesktopUI.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public Email email { get; set; }

        public DetailsViewModel(Email email)
        {
            this.email = email;

            this.Message = email.Message;
            this.Sender = email.Sender;
            this.Subject = email.Subject;
            this.Date = email.Date;
        }

        internal void ReplyMail()
        {
            SendEmailWindow sendEmailWindow = new SendEmailWindow();
            sendEmailWindow.Show();
        }
    }
}
