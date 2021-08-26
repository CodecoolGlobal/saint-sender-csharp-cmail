namespace SaintSender.Core.Models
{
    using MailKit;
    using MailKit.Net.Imap;
    using MailKit.Net.Smtp;
    using MimeKit;
    using System;
    using System.Collections.ObjectModel;
    using System.Linq;

    /// <summary>
    /// Defines the <see cref="Authentication" />.
    /// </summary>
    static public class Authentication
    {
        private static Account _account;

        public static Account Account { get => _account; }

        /// <summary>
        /// The AuthenticateAccount.
        /// </summary>
        /// <param name="email">The email<see cref="string"/>.</param>
        /// <param name="password">The password<see cref="string"/>.</param>
        /// <returns>The <see cref="string"/>.</returns>
        public static string AuthenticateAccount(string email, string password)
        {
            if (email == "" && password == "")
            {
                return "Password and email requiered";
            }
            else if (email == "")
            {
                return "Email requiered";
            }
            else if (password == "")
            {
                return "Password requiered";
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
                        return "Invalid username/password";
                    }
                    else if (e is System.Net.Sockets.SocketException)
                    {
                        return "No internet connection";
                    }
                }
                return "Succesful login";
            }
        }

        /// <summary>
        /// The GetInbox.
        /// </summary>
        /// <param name="client">The client<see cref="ImapClient"/>.</param>
        /// <returns>The <see cref="IMailFolder"/>.</returns>
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
                emails.Reverse();

                client.Disconnect(true);
                return emails;
            }

        }
        public static void WriteEmail(Email email)
        {

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Joey Tribbiani", _account.Email));
            message.To.Add(new MailboxAddress("Mrs. Chanandler Bong", email.To));
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
