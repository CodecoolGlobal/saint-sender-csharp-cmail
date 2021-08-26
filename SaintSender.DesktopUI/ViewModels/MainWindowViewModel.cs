using SaintSender.Core.Interfaces;
using SaintSender.Core.Models;
using SaintSender.Core.Services;
using SaintSender.DesktopUI.Views;
using System;
using System.ComponentModel;
using System.Threading;

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
        private readonly IGreetService _greetService;

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
            _greetService = new GreetService();
            _accountService = new AccountService();
        }

        /// <summary>
        /// Call a vendor service and apply its value into <see cref="Message"/> property.
        /// </summary>
        public void Greet()
        {
            Greeting = _greetService.Greet(Name);
        }

        public void Login(string name, string password)
        {
            Thread thread = Thread.CurrentThread;
            thread.Name = "Main";
            StatusCodes status = _accountService.Authenticate(name, password);
            Message = StatusCodeParser.GetStatusMessage(status);
            
            if (status == StatusCodes.auth_success)
            {
                Inbox inbox = new Inbox();
                inbox.Show();
            }
        }

        public void StoreAccount(Account account)
        {
            Isolate.SaveIntoIsolatedStorage(account);
        }

        public void ForgetAccount()
        {
            Isolate.DeleteFromIsolatedStorage();
        }
    }
}
