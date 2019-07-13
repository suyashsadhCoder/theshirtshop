using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using theshirtshopApp.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace theshirtshopApp.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChangePasswordPage : ContentPage
    {
        public ChangePasswordPage()
        {
            if (Application.Current.Properties.ContainsKey("Key"))
            {
                InitializeComponent();
                BindingContext = new ChangePasswordViewModel(Navigation);
            }
            else
            {
                Navigation.PushModalAsync(new MainPage());
            }
        }

        private void OldPassword_Completed(object sender, EventArgs e)
        {
            NewPassword.Focus();
        }

        private void NewPassword_Completed(object sender, EventArgs e)
        {
            ConformPassword.Focus();
        }

        private void ConformPassword_Completed(object sender, EventArgs e)
        {
            Save.Focus();
        }
    }
}