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
using Xamarin.Forms;

namespace theshirtshopApp.ViewModel
{
  public  class AllGoodsReturnPageViewModel : ModelBase
    {
        private INavigation _navigation { get; set; }
        private ObservableCollection<FranchiseGoodReturn_Class> GoodsReturnClassList { get; set; }

        private IAllDataServices _IAllDataServices;
        private bool RequestList = false;
        private bool _ShowHideMsg = false;
        public AllGoodsReturnPageViewModel(INavigation navigation)
        {
            _navigation = navigation;
            _IAllDataServices = new AllDataServices();
            GetData();

        }
        public bool _Show_Hide_Msg
        {
            get { return _ShowHideMsg; }
            set { _ShowHideMsg = value;
                NotifyPropertyChanged();
            }
        }
        public async Task GetData()
        {
            try
            {
                var Wait = UserDialogs.Instance.Loading("Wait...", Cancel(), "Cancel", true, MaskType.Black);
                Wait.Show();
                JObject result = await _IAllDataServices.GetAllGoodReturn() as JObject;
                if (result != null)
                {
                    var type = (int)result["Type"];
                    if (type == 1)
                    {
                        _GoodsReturn_Class_List = JsonConvert.DeserializeObject<ObservableCollection<FranchiseGoodReturn_Class>>(result["Result"].ToString());
                        if (_GoodsReturn_Class_List.Count == 0)
                        {
                             await App.Current.MainPage.DisplayAlert("Oops!", "Goods Return Not Found", "Ok");
                          //  _Show_Hide_Msg = true;
                              _Request_List = false;
                        }
                        else
                        {
                            _Request_List = true;
                         //   _Show_Hide_Msg = false;
                        }

                    }
                    else
                    {
                        await App.Current.MainPage.DisplayAlert("Error!", (string)result["ResponseMessage"], "Ok");
                    }
                    Wait.Hide();
                }
              

            }
            catch (Exception ee)
            {
                await App.Current.MainPage.DisplayAlert("Error!", ee.Message, "Ok");
            }
        }

        private Action Cancel()
        {
            var ca = new CancellationTokenSource();
            return ca.Cancel;
        }

        public ObservableCollection<FranchiseGoodReturn_Class> _GoodsReturn_Class_List
        {
            get { return GoodsReturnClassList; }
            set
            {
                GoodsReturnClassList = value;
                NotifyPropertyChanged();
            }
        }
        public bool _Request_List
        {
            get { return RequestList; }
            set
            {
                RequestList = value;
                NotifyPropertyChanged();
            }
        }
     
    }
}
