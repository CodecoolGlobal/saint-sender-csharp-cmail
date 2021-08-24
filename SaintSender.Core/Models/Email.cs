namespace SaintSender.Core.Models
{
    public class Email
    {
        public string Message { get; set; }

        public Email(string message)
        {
            Message = message;
        }
    }
}