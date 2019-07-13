using Acr.UserDialogs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace theshirtshopApp.DAL
{
    public abstract class AbstractViewModel : INotifyPropertyChanged
    {
        protected AbstractViewModel(IUserDialogs dialogs)
        {
            this.Dialogs = dialogs;
        }


        protected IUserDialogs Dialogs { get; }


        protected virtual void Result(string msg)
        {
            this.Dialogs.Alert(msg);
        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
