namespace SaintSender.Core.Models
{
    using MailKit;
    using MailKit.Net.Imap;
    using System;

    /// <summary>
    /// Defines the <see cref="Authentication" />.
    /// </summary>
    internal class Authentication
    {
        /// <summary>
        /// Defines the account.
        /// </summary>
        private Account account;

        /// <summary>
        /// The AuthenticateAccount.
        /// </summary>
        /// <param name="email">The email<see cref="string"/>.</param>
        /// <param name="password">The password<see cref="string"/>.</param>
        /// <returns>The <see cref="string"/>.</returns>
        public string AuthenticateAccount(string email, string password)
        {
            if (email == "" && password == "")
            {
                return "There is no email and password!";
            }
            else if (email == "")
            {
                return "There is no email!";
            }
            else if (password == "")
            {
                return "There is no password!";
            }
            using (var client = new ImapClient())
            {
                client.Connect("imap.gmail.com", 993, true);

                try
                {
                    client.Authenticate(email, password);
                    account = new Account(email, password);
                }
                catch (MailKit.Security.AuthenticationException e)
                {
                    return "Invalid username/password!";
                }

                IMailFolder inbox = GetInbox(client);
                return "Succesful login";
            }
        }

        /// <summary>
        /// The GetInbox.
        /// </summary>
        /// <param name="client">The client<see cref="ImapClient"/>.</param>
        /// <returns>The <see cref="IMailFolder"/>.</returns>
        private static IMailFolder GetInbox(ImapClient client)
        {
            IMailFolder inbox = client.Inbox;
            inbox.Open(FolderAccess.ReadOnly);

            Console.WriteLine("Total messages: {0}", inbox.Count);
            Console.WriteLine("Recent messages: {0}", inbox.Recent);

            for (int i = 0; i < inbox.Count; i++)
            {
                var message = inbox.GetMessage(i);
                Console.WriteLine("Subject: {0}", message.Subject);
            }

            client.Disconnect(true);
            return inbox;
        }
    }
}
