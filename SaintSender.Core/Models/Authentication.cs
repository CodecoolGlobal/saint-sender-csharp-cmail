namespace SaintSender.Core.Models
{
    using MailKit;
    using MailKit.Net.Imap;
    using MailKit.Net.Smtp;
    using MimeKit;
    using System;
    using System.Collections.ObjectModel;
    using System.Linq;

    namespace SaintSender.Core.Models
    {
        static public class Authentication
        {
            private static Account _account;

            public static Account Account { get => _account; }

            public static StatusCodes AuthenticateAccount(string email, string password)
            {
                if (email == "" || password == "")
                {
                    return StatusCodes.auth_missingcred;
                }
                using (var client = new ImapClient())
                {
                    client.Connect("imap.gmail.com", 993, true);

                    try
                    {
                        client.Authenticate(email, password);
                        _account = new Account(email, password);
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

            public static ObservableCollection<Email> GetInbox()
            {
                ObservableCollection<Email> emails = new ObservableCollection<Email>();
                using (var client = new ImapClient())
                {
                    client.Connect("imap.gmail.com", 993, true);

                    client.Authenticate(_account.Email, _account.Password);
                    IMailFolder inbox = client.Inbox;
                    inbox.Open(FolderAccess.ReadOnly);

                    foreach (var email in inbox)
                    {
                        string message = email.TextBody;
                        string sender = email.From.ToString();
                        DateTime date = email.Date.Date;
                        string subject = email.Subject;
                        bool read = false;

                    emails.Add(new Email(message, sender, date, subject, read));
                }

                    client.Disconnect(true);
                    return emails;
                }

            }
            public static void WriteEmail(Email email)
            {

                var message = new MimeMessage();
                message.From.Add(new MailboxAddress(_account.Email, _account.Email));
                message.To.Add(new MailboxAddress(email.To, email.To));
                message.Subject = email.Subject;

                message.Body = new TextPart("plain")
                {
                    Text = email.Message
                };

                using (var client = new SmtpClient())
                {
                    client.Connect("smtp.gmail.com", 587, false);

                    // Note: only needed if the SMTP server requires authentication
                    client.Authenticate(_account.Email, _account.Password);

                    client.Send(message);
                    client.Disconnect(true);
                }
            }
        }
    }
}
