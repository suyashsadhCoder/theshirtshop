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
    public partial class ViewOnlyReturnRefund : ContentPage
    {
        public ViewOnlyReturnRefundViewModel viewmodel;
        public ViewOnlyReturnRefund()
        {
            InitializeComponent();
            this.BindingContext = viewmodel = new ViewOnlyReturnRefundViewModel();
            returnRefundList.ItemTapped += ReturnRefundList_ItemTapped;
            returnRefundList.RefreshCommand = new Command(async () => {
                //Do your stuff.
                returnRefundList.IsRefreshing = true;
                await viewmodel.initiallist();
                returnRefundList.IsRefreshing = false;
            });
        }

        private void ReturnRefundList_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var orderId = e.Item as CustomerReturnViewItem;
            Navigation.PushAsync(new ReturnRefundDetailView(orderId.OrderReturnId));


        }
    }
}