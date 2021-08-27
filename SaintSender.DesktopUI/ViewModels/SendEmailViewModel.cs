using SaintSender.Core.Models;

namespace SaintSender.DesktopUI.ViewModels
{
    public class SendEmailViewModel
    {
        public string To { get; set; }

        public string CC { get; set; }

        public string Subject { get; set; }

        public string Message { get; set; }

        public void SendEmail()
        {
            Authentication.WriteEmail(new Email(Message, Subject, To, CC));
        }
    }
}
