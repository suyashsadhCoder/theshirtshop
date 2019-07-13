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
    public partial class ReferralPage : ContentPage
    {
        public ReferralPage()
        {
            if (Application.Current.Properties.ContainsKey("Key"))
            {
                InitializeComponent();
                BindingContext = new ReferralPageViewModel(Navigation);
            }
            else
            {
                Navigation.PushModalAsync(new MainPage());
            }
          
        }

        private void Name_Completed(object sender, EventArgs e)
        {
            ContectNo.Focus();
        }
        private void ContectNo_Completed(object sender, EventArgs e)
        {
            EmailId.Focus();
        }
        private void EmailId_Completed(object sender, EventArgs e)
        {
            Address.Focus();
        }
        private void Address_Completed(object sender, EventArgs e)
        {
            Save.Focus();
        }
    }
}