using NUnit.Framework;
using SaintSender.Core.Models;
using SaintSender.Core.Services;

namespace SaintSender.Core.Tests
{
    [TestFixture]
    internal class AccountServiceTests
    {

        [Test]
        public void AccountService_ReturnSuccessStatus()
        {
            AccountService accountService = new AccountService();
            StatusCodes statusCode = accountService.Authenticate("cmail.test11@gmail.com", "Cmailtest11-");
            Assert.AreEqual(StatusCodes.auth_success, statusCode);
        }

        [Test]
        public void AccountService_ReturnMissingCredentialsStatus()
        {
            AccountService accountService = new AccountService();
            StatusCodes statusCode = accountService.Authenticate("", "Cmailtest11-");
            Assert.AreEqual(StatusCodes.auth_missingcred, statusCode);
        }

        [Test]
        public void AccountService_ReturnCorrectStatus()
        {
            AccountService accountService = new AccountService();
            StatusCodes statusCode = accountService.Authenticate("invalid.invalid@gmail.com", "Invalid");
            Assert.AreEqual(StatusCodes.auth_invalidcred, statusCode);
        }
    }
}
