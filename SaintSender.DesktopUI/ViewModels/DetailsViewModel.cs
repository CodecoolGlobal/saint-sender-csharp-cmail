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

        public DetailsViewModel()
        {
            this.test = "közepes kutya";
        }
    }
}
