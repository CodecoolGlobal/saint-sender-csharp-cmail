namespace SaintSender.Core.Models
{
    public enum StatusCodes
    {
        // authentication
        auth_invalidcred, auth_missingcred, auth_success, auth_nonet,

        // offline functionality
        offline_nologin, offline_nocache, offline_nocacheforlogin,
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
                case StatusCodes.offline_nologin:
                    return "No login is saved. Offline inbox access requires a saved login.";
                case StatusCodes.offline_nocache:
                    return "No offline inboxes available.";
                case StatusCodes.offline_nocacheforlogin:
                    return "The stored login has no associated offline inbox cache.";
                default:
                    return "";
            }
        }
    }
}
