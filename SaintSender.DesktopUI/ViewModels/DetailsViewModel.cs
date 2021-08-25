using SaintSender.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaintSender.DesktopUI.ViewModels
{
    class DetailsViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public string test { get; set; }
        public Email email { get; set; }

        public DetailsViewModel(Email email)
        {
            this.email = email;
            this.test = email.Message;
        }
    }
}
