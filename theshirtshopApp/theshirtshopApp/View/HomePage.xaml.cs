using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using theshirtshopApp.Interface;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace theshirtshopApp.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            if (Application.Current.Properties.ContainsKey("Key"))
            {

                InitializeComponent();
                BindingContext = new HomePageViewModel(Navigation);


            }
            else
            {
                Navigation.PushModalAsync(new MainPage());
            }
           
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new SellPage());
        }

        private void Button_Clicked_1(object sender, EventArgs e)
        {
            Device.OpenUri(new Uri("http://www.theshirtshop.in"));
        }
    }
}