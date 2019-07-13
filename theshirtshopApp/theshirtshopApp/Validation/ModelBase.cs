﻿using Acr.UserDialogs;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace theshirtshopApp.Validation
{
    public class ModelBase : ValidationBase, INotifyPropertyChanged
    {
       
        public event PropertyChangedEventHandler PropertyChanged;
        protected void NotifyPropertyChanged([CallerMemberName]string propertyName = "")
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
