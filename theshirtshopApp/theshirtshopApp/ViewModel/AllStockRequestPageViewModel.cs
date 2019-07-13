
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
    public class AllStockRequestPageViewModel : ModelBase
    {
     //   private FranchiseRequest_Class _FranchiseRequestClassList_SelectedItem { get; set; }
        private INavigation _navigation { get; set; }
        private ObservableCollection<FranchiseRequest_Class> FranchiseRequestClassList { get; set; }
        
        private IAllDataServices _IAllDataServices;
        private bool RequestList = false;
        private bool _ShowHideMsg = false;
        public AllStockRequestPageViewModel(INavigation navigation)
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
                JObject result = await _IAllDataServices.GetAllSendStockRequest();
                if (result != null)
                {
                    var type = (int)result["Type"];
                    if (type == 1)
                    {
                        _FranchiseRequest_Class_List = JsonConvert.DeserializeObject<ObservableCollection<FranchiseRequest_Class>>(result["Result"].ToString());
                        if (_FranchiseRequest_Class_List.Count == 0)
                        {
                             await App.Current.MainPage.DisplayAlert("Oops!", "Order Request Not Found", "Ok");
                           // _Show_Hide_Msg = true;
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

        public ObservableCollection<FranchiseRequest_Class> _FranchiseRequest_Class_List
        {
            get { return FranchiseRequestClassList; }
            set
            {
                FranchiseRequestClassList = value;
                NotifyPropertyChanged();
            }
        }
        public bool _Request_List
        {
            get { return RequestList; }
            set { RequestList = value;
                NotifyPropertyChanged();
            }
        }
        //public FranchiseRequest_Class FranchiseRequestClassList_SelectedItem
        //{
        //    get { return _FranchiseRequestClassList_SelectedItem; }
        //    set
        //    {
        //        _FranchiseRequestClassList_SelectedItem = value;
        //        if (!string.IsNullOrEmpty(_FranchiseRequestClassList_SelectedItem.Action))
        //        {
        //            NotifyPropertyChanged();
        //            GoToDetailPage(_FranchiseRequestClassList_SelectedItem);
        //        }
        //    }
        //}
        //private async void GoToDetailPage(FranchiseRequest_Class franchiseRequest_Class)
        //{
        //    await _navigation.PushAsync(new StockRequestDetailPage(franchiseRequest_Class),true);
        //}
    }
}

