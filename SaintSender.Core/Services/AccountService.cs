using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SaintSender.Core.Interfaces;
using MailKit.Net.Imap;
using MailKit.Search;
using MailKit;
using MimeKit;
using Org.BouncyCastle.Crypto.Generators;
using SaintSender.Core.Models;

namespace SaintSender.Core.Services
{
    public class AccountService : IAccountService
    {
        public string Authenticate(string email, string password)
        {
            throw new NotImplementedException();
        }

        public string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
		}

		public bool VerifyPassword(string password)
		{
			return BCrypt.Net.BCrypt.Verify(password, HashPassword(password));
		}
    }
}
