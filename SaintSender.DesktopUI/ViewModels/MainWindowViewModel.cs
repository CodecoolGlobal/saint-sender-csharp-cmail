using SaintSender.Core.Interfaces;
using SaintSender.Core.Models;
using SaintSender.Core.Services;
using SaintSender.DesktopUI.Views;
using System.ComponentModel;

namespace SaintSender.DesktopUI.ViewModels
{
    /// <summary>
    /// ViewModel for Main window. Contains all shown information
    /// and necessary service classes to make view functional.
    /// </summary>
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private string _name;
        private string _greeting;
        private string _message;
        private readonly IAccountService _accountService;

        /// <summary>
        /// Whenever a property value changed the subscribed event handler is called.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Gets or sets value of Greeting.
        /// </summary>
        public string Greeting
        {
            get => _greeting;
            set
            {
                _greeting = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Greeting)));
            }
        }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Name)));
            }
        }

        public string Message
        {
            get => _message;
            set
            {
                _message = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Message)));
            }
        }

        public MainWindowViewModel()
        {
            Name = string.Empty;
            _accountService = new AccountService();
        }

        public StatusCodes Login(string name, string password)
        {
            StatusCodes status = _accountService.Authenticate(name, password);
            Message = StatusCodeParser.GetStatusMessage(status);

            if (status == StatusCodes.auth_success)
            {
                Inbox inbox = new Inbox();
                inbox.Show();
            }
            return status;
        }

        public void StoreAccount(Account account)
        {
            Isolate.SaveIntoIsolatedStorage(account);
        }

        public bool LoginOffline()
        {
            string[] saved = Isolate.GetOfflineAccounts();
            if (saved.Length == 0)
            {
                Message = StatusCodeParser.GetStatusMessage(StatusCodes.offline_nocache);
                return false;
            }
            if (!Isolate.isoStore.FileExists(Isolate._accountFilePath))
            {
                Message = StatusCodeParser.GetStatusMessage(StatusCodes.offline_nologin);
                return false;
            }
            Account account = Isolate.ReadFromIsolatedStorage();
            if (!Isolate.isoStore.DirectoryExists(account.Email))
            {
                Message = StatusCodeParser.GetStatusMessage(StatusCodes.offline_nocacheforlogin);
                return false;
            }
            Authentication.OpenOffline(account.Email);
            Inbox inbox = new Inbox();
            inbox.Show();
            Message = StatusCodeParser.GetStatusMessage(StatusCodes.auth_success);
            return true;
        }
    }
}
