namespace SaintSender.Core.Models
{
    public enum StatusCodes
    {
        // authentication
        auth_invalidcred, auth_missingcred, auth_success, auth_nonet
    }

    public static class StatusCodeParser
    {
        public static string GetStatusMessage(StatusCodes status)
        {
            switch (status)
            {
                case StatusCodes.auth_invalidcred:
                    return "Invalid address or password. Check that you typed everything correctly.";
                case StatusCodes.auth_missingcred:
                    return "Please enter your login credentials.";
                case StatusCodes.auth_success:
                    return "Login successful!";
                case StatusCodes.auth_nonet:
                    return "Your device cannot access the Internet. Check that your device is connected to the Internet and try again.";
                default:
                    return "";
            }
        }
    }
}
