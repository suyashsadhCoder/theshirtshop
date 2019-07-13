using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using theshirtshopApp.BAL;
using theshirtshopApp.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace theshirtshopApp.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SellDetailPage : ContentPage
    {
        public SellDetailPage(FranchiseSell_Class fs)
        {
           
            if (Application.Current.Properties.ContainsKey("Key"))
            {
                InitializeComponent();
                BindingContext = new SellDetailPageViewModel(fs, Navigation);
            }
            else
            {
                Navigation.PushModalAsync(new MainPage());
            }
        }
    }
}