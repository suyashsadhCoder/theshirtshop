using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using theshirtshopApp.BAL;
using theshirtshopApp.ViewModel;

namespace theshirtshopApp.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UpComingStockRequestDetailPage : ContentPage
    {
        public UpComingStockRequestDetailPage(OrderMaster_Class orderMaster_Class)
        {
            if (Application.Current.Properties.ContainsKey("Key"))
            {
                InitializeComponent();
                
                BindingContext = new UpComingStockRequestDetailPageViewModel(orderMaster_Class,Navigation);
            }
            else
            {
                Navigation.PushModalAsync(new MainPage());
            }
           
        }
    }
}