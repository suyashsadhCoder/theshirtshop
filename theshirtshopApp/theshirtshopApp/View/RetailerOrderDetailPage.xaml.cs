using System;
using System.Collections.Generic;
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
    public partial class RetailerOrderDetailPage : ContentPage
    {
        public RetailerOrderDetailPage(Employee_OrderGenerate_Class eoc)
        {
            
            if (Application.Current.Properties.ContainsKey("Key"))
            {
                InitializeComponent();
                BindingContext = new RetailerOrderDetailPageViewModel(eoc, Navigation);
            }
            else
            {
                Navigation.PushModalAsync(new MainPage());
            }
        }
    }
}