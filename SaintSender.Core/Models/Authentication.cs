namespace SaintSender.Core.Models
{
    using MailKit;
    using MailKit.Net.Imap;
    using System;
    using System.Net;

    /// <summary>
    /// Defines the <see cref="Authentication" />.
    /// </summary>
    static public class Authentication
    {
        private static Account account;

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
            } else if (CheckForInternetConnection())
            {
                return "No internet connection";
            }
            using (var client = new ImapClient())
            {
                client.Connect("imap.gmail.com", 993, true);

                try
                {
                    client.Authenticate(email, password);
                    account = new Account(email, password);
                }
                catch (Exception e)
                {
                    if (e is MailKit.Security.AuthenticationException)
                    {
                        return "Invalid username/password";
                    } else if (e is System.Net.Sockets.SocketException)
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
        public static IMailFolder GetInbox()
        {
            using (var client = new ImapClient())
            {
                client.Connect("imap.gmail.com", 993, true);

                client.Authenticate(account.Email, account.Password);
                IMailFolder inbox = client.Inbox;
                inbox.Open(FolderAccess.ReadOnly);

                client.Disconnect(true);
                return inbox;
            }
        }

        public static bool CheckForInternetConnection()
        {
            try
            {
                using (var client = new WebClient())
                using (client.OpenRead("http://google.com/generate_204"))
                    return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
