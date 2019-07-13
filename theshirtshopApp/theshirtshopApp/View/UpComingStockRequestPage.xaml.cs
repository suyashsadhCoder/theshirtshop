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
    public partial class UpComingStockRequestPage : ContentPage
    {
        public UpComingStockRequestPage()
        {
            if (Application.Current.Properties.ContainsKey("Key"))
            {
                InitializeComponent();
                BindingContext = new UpComingStockRequestViewModel(Navigation);
            }
            else
            {
                Navigation.PushModalAsync(new MainPage());
            }
           
        }

        private void OrderMasterClassList_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var item = e.Item as OrderMaster_Class;
            Navigation.PushAsync(new UpComingStockRequestDetailPage(item), true);
        }

        private void OrderMasterClassList_Refreshing(object sender, EventArgs e)
        {
            var vm = BindingContext as UpComingStockRequestViewModel;
             vm.GetListAsync();
           
            OrderMasterClassList.IsRefreshing = false;
        }
    }
}