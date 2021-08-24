using MailKit;

namespace SaintSender.Core.Interfaces
{
    public interface IAccountService
    {
        string Authenticate(string email, string password);

        string HashPassword(string password);

        bool VerifyPassword(string password);
    }
}