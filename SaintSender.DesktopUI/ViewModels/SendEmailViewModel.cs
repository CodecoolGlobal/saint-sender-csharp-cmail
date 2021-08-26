using SaintSender.Core.Models;
using SaintSender.Core.Models.SaintSender.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaintSender.DesktopUI.ViewModels
{
    class SendEmailViewModel
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
