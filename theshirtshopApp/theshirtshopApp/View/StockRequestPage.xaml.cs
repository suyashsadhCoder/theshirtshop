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
    public partial class StockRequestPage : ContentPage
    {
        public StockRequestPage()
        {
            if (Application.Current.Properties.ContainsKey("Key"))
            {
                InitializeComponent();
                BindingContext = new StockRequestViewModel(Navigation);
            }
            else
            {
                Navigation.PushModalAsync(new MainPage());
            }
           
        }

        private void Title_Completed(object sender, EventArgs e)
        {
            Description.Focus();
        }
    }
}