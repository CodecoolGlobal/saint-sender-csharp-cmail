using MailKit;
using MailKit.Net.Imap;
using MailKit.Net.Smtp;
using MimeKit;
using System;
using System.Collections.ObjectModel;

namespace SaintSender.Core.Models
{
    public static class Authentication
    {
        private static bool isOffline = false;

        public static Account Account { get; private set; }

        public static StatusCodes AuthenticateAccount(string email, string password)
        {
            if (email == "" || password == "")
            {
                return StatusCodes.auth_missingcred;
            }
            using (ImapClient client = new ImapClient())
            {

                try
                {
                    client.Connect("imap.gmail.com", 993, true);
                    client.Authenticate(email, password);
                    Account = new Account(email, password);
                }
                catch (Exception e)
                {
                    if (e is MailKit.Security.AuthenticationException)
                    {
                        return StatusCodes.auth_invalidcred;
                    }
                    else if (e is System.Net.Sockets.SocketException)
                    {
                        return StatusCodes.auth_nonet;
                    }
                }
                return StatusCodes.auth_success;
            }
        }
        public static void OpenOffline(string email)
        {
            Account = new Account(email, "");
            isOffline = true;
        }

        public static string GetAddress()
        {
            return Account.Email;
        }

        public static ObservableCollection<Email> GetInbox()
        {
            if (isOffline)
            {
                return Isolate.ReadCache(Account.Email);
            }
            ObservableCollection<Email> emails = new ObservableCollection<Email>();
            using (ImapClient client = new ImapClient())
            {
                client.Connect("imap.gmail.com", 993, true);

                client.Authenticate(Account.Email, Account.Password);
                IMailFolder inbox = client.Inbox;
                _ = inbox.Open(FolderAccess.ReadOnly);

                for (int i = 0; i < inbox.Count; i++)
                {
                    MimeMessage message = inbox.GetMessage(i);
                    Console.WriteLine("Subject: {0}", message.Subject);
                    Console.WriteLine(message.TextBody);
                }

                foreach (MimeMessage email in inbox)
                {
                    string message = email.TextBody;
                    string sender = email.From.ToString();
                    DateTime date = email.Date.Date;
                    string subject = email.Subject;
                    bool read = false;
                    string id = email.MessageId;

                    emails.Add(new Email(message, sender, date, subject, read, id));
                }

                client.Disconnect(true);
                return emails;
            }

        }
        public static void WriteEmail(Email email)
        {

            MimeMessage message = new MimeMessage();
            message.From.Add(new MailboxAddress(Account.Email, Account.Email));
            message.To.Add(new MailboxAddress(email.To, email.To));
            message.Subject = email.Subject;

            message.Body = new TextPart("plain")
            {
                Text = email.Message
            };

            using (SmtpClient client = new SmtpClient())
            {
                client.Connect("smtp.gmail.com", 587, false);

                // Note: only needed if the SMTP server requires authentication
                client.Authenticate(Account.Email, Account.Password);

                client.Send(message);
                client.Disconnect(true);
            }
        }
    }
}
