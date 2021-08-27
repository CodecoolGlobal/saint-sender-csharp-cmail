using NUnit.Framework;
using SaintSender.Core.Models;
using SaintSender.Core.Services;

namespace SaintSender.Core.Tests
{
    [TestFixture]
    class AccountServiceTests
    {

        [Test]
        public void AccountService_ReturnSuccessStatus()
        {
            var accountService = new AccountService();
            var statusCode = accountService.Authenticate("cmail.test11@gmail.com", "Cmailtest11-");
            Assert.AreEqual(StatusCodes.auth_success, statusCode);
        }

        [Test]
        public void AccountService_ReturnMissingCredentialsStatus()
        {
            var accountService = new AccountService();
            var statusCode = accountService.Authenticate("", "Cmailtest11-");
            Assert.AreEqual(StatusCodes.auth_missingcred, statusCode);
        }

        [Test]
        public void AccountService_ReturnCorrectStatus()
        {
            var accountService = new AccountService();
            var statusCode = accountService.Authenticate("invalid.invalid@gmail.com", "Invalid");
            Assert.AreEqual(StatusCodes.auth_invalidcred, statusCode);
        }
    }
}
