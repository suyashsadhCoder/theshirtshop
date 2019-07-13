using Acr.UserDialogs;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using theshirtshopApp.BAL;
using theshirtshopApp.DAL;
using theshirtshopApp.Interface;
using theshirtshopApp.Validation;
using theshirtshopApp.View;
using Xamarin.Forms;

namespace theshirtshopApp.ViewModel
{
    public class UpComingStockRequestViewModel : ModelBase
    {

        // private OrderMaster_Class orderMaster_Class_Data = new OrderMaster_Class();
        private ObservableCollection<OrderMaster_Class> OrderMaster_Class_List = new ObservableCollection<OrderMaster_Class>();
        // private OrderMaster_Class OrderMasterClassListSelectedItem { get; set; }
        private INavigation _navigation { get; set; }
        private IAllDataServices _IAllDataServices;
        private bool _ShowHideMsg = false;
        public UpComingStockRequestViewModel(INavigation navigation)
        {
            _navigation = navigation;
            _IAllDataServices = new AllDataServices();
             GetListAsync();
           
        }
        public bool _Show_Hide_Msg
        {
            get { return _ShowHideMsg; }
            set { _ShowHideMsg = value;
                NotifyPropertyChanged();
            }
        }
        public async Task GetListAsync()
        {
            try
            {
                var Wait = UserDialogs.Instance.Loading("Wait..", Cancel(), "Cancel", true, MaskType.Black);
                Wait.Show();
                JObject result = await _IAllDataServices.GetAllUpCommingOrder();
                if (result != null)
                {
                    string type = result["Type"].ToString();

                    if (type == "1")
                    {
                        if (result["Result"].ToList().Count > 0)
                        {
                            _OrderMaster_Class_List = JsonConvert.DeserializeObject<ObservableCollection<OrderMaster_Class>>(result["Result"].ToString());
                           // _Show_Hide_Msg = false;
                        }
                        else
                        {
                             await App.Current.MainPage.DisplayAlert("Oops!", "New Order Not Found...", "Ok");
                           // _Show_Hide_Msg = true;
                        }

                    }
                    else
                    {
                        await App.Current.MainPage.DisplayAlert("Error!", (string)result["ResponseMessage"], "Ok");
                    }
                }
               
                Wait.Dispose();
            }
            catch (Exception ee)
            {
                await App.Current.MainPage.DisplayAlert("Error!", ee.Message, "Ok");
            }


        }

        private Action Cancel()
        {
            var cancelSrc = new CancellationTokenSource();
            return cancelSrc.Cancel;
        }

        public ObservableCollection<OrderMaster_Class> _OrderMaster_Class_List
        {
            get { return OrderMaster_Class_List; }
            set
            {
                OrderMaster_Class_List = value;
                NotifyPropertyChanged();
            }
        }
    }
}
