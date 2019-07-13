using Acr.UserDialogs;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using theshirtshopApp.BAL;
using theshirtshopApp.DAL;
using theshirtshopApp.Interface;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace theshirtshopApp.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class NotificationPage : ContentPage
	{
        private ObservableCollection<Notification_Class> _listData;
        private IAllDataServices IAllDataServices_data;
        public NotificationPage()
		{
            
			InitializeComponent ();
            IAllDataServices_data = new AllDataServices();
            _listData = new ObservableCollection<Notification_Class>();
            GetData();
        }

        private async Task GetData()
        {


            NotificationMsg.IsVisible = false;
            var wait = UserDialogs.Instance.Loading(null, null, null, true, MaskType.Black);
            wait.Show();
            JObject result = await IAllDataServices_data.GetAllNotiication();
            if (result != null)
            {
                string type = result["Type"].ToString();

                if (type == "1")
                {

                    _listData = JsonConvert.DeserializeObject<ObservableCollection<Notification_Class>>(result["Result"].ToString());
                    if (_listData.Count > 0)
                    {
                        NotificationList.ItemsSource = _listData;
                    }
                    else {
                        NotificationMsg.IsVisible = true;

                    }
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("Error!", (string)result["ResponseMessage"], "Ok");
                }
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Oops!", "Please Refresh Page And try Again....", "Ok");
            }
            wait.Hide();
        }

        private async void NotificationList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as Notification_Class;
            if(item != null)
            {
                if (item._Type == "Generated" || item._Type == "Packing Slip" || item._Type == "Invoice Generated" || item._Type == "Dispatched")
                {
                    await IAllDataServices_data.NotificationUpdate(item._Type);
                    await Navigation.PushAsync(new UpComingStockRequestPage());
                }
                else if (item._Type == "Accepted")
                {
                    await IAllDataServices_data.NotificationUpdate(item._Type);
                    await Navigation.PushAsync(new AllGoodsReturnPage());

                }
                else if (item._Type == "Subscription")
                {
                    await IAllDataServices_data.NotificationUpdate(item._Type);
                    await Navigation.PushAsync(new FranchiseAdminPaymentDetail());
                }
                else if (item._Type == "Mismatch")
                {
                    await IAllDataServices_data.NotificationUpdate(item._Type);
                    await Navigation.PushAsync(new LedgerPage());
                }
                else if (item._Type == "Transaction")
                {
                    await IAllDataServices_data.NotificationUpdate(item._Type);
                }

            }
        }
    }
}