using Acr.UserDialogs;
using Android.Widget;
using Plugin.Connectivity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using theshirtshopApp.BAL;
using theshirtshopApp.View;
using theshirtshopApp.ViewModel;
using Xamarin.Forms;

namespace theshirtshopApp
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            try
            {
                App.exitcount = 0;
                for (int i = 0; i < Navigation.ModalStack.Count; i++)
                {
                     Navigation.PopModalAsync();
                }
                InitializeComponent();
              
                BindingContext = new LoginViewModel(Navigation);


            }
            catch (Exception ee)
            {
                DisplayAlert("Error", ee.Message, "ok");
            }
        }
        private void userid_Completed(object sender, EventArgs e)
        {
            password.Focus();
        }
        private void password_Completed(object sender, EventArgs e)
        {
            Login.Focus();
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            Device.OpenUri(new Uri("http://www.theshirtshop.in/terms-of-use"));
        }

        private void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
        {
            Device.OpenUri(new Uri("http://www.theshirtshop.in/"));
        }
        private void TapGestureRecognizer_forgetpassword(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new ForgetPassword());
        }


    }
}
