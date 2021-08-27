namespace SaintSender.Core.Interfaces
{
    public interface IAccountService
    {
        Models.StatusCodes Authenticate(string email, string password);

        string HashPassword(string password);

        bool VerifyPassword(string password);
    }
}