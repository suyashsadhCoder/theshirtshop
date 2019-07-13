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
    public partial class CustomerRegistrationPage : ContentPage
    {
        public CustomerRegistrationPage()
        {
            if (Application.Current.Properties.ContainsKey("Key"))
            {
                InitializeComponent();
                BindingContext = new CustomerRegistrationPageViewModel(Navigation);
            }
            else
            {
                Navigation.PushModalAsync(new MainPage());
            }
         
        }

        private void ContectNo_Completed(object sender, EventArgs e)
        {
            Name.Focus();
        }

        private void Name_Completed(object sender, EventArgs e)
        {
            EmailId.Focus();
        }

        private void EmailId_Completed(object sender, EventArgs e)
        {
            Submit.Focus();
        }

      
    }
}