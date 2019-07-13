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
using theshirtshopApp.Validation;

namespace theshirtshopApp.ViewModel
{
    public class ReturnRefundDetailViewModel : ModelBase
    {

        ReturnRefundModel _returnRefund;
        public ReturnRefundModel ReturnDetailsList { get { return _returnRefund; }set { _returnRefund = value; NotifyPropertyChanged(); } }

        private IAllDataServices IAllDataServices_data;

        public ReturnRefundDetailViewModel(string returnID) {

            ReturnDetailsList = new ReturnRefundModel();
            IAllDataServices_data = new AllDataServices();
            initiallist(returnID);

        }
        private async void initiallist(string frid)
        {
            var Wait = UserDialogs.Instance.Loading("Wait...", Cancel(), "Cancel", true, MaskType.Black);
            Wait.Show();
            try
            {

                JObject result = await IAllDataServices_data.GetReturnDetailReturnId(frid) as JObject;
                if (result != null)
                {
                    var type = (int)result["Type"];
                    if (type == 1)
                    {
                        ObservableCollection<ReturnRefundServerModel> tempLIst = JsonConvert.DeserializeObject<ObservableCollection<ReturnRefundServerModel>>(result["Result"].ToString());
                        if (tempLIst.Count == 0)
                        {
                            await App.Current.MainPage.DisplayAlert("Oops!", "Goods Return Not Found", "Ok");


                        }
                        else
                        {
                            ReturnDetailsList = new ReturnRefundModel().getReturnRefundModel(tempLIst);
                           
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
                Wait.Hide();
                await App.Current.MainPage.DisplayAlert("Error!", ee.Message, "Ok");
            }
        }



        private Action Cancel()
        {
            //var cancelSrc = new CancellationTokenSource();
            //return cancelSrc.Cancel;
            return null;
        }
    }
}
