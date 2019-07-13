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
using Xamarin.Forms;

namespace theshirtshopApp.ViewModel
{
    public class ViewOnlyReturnRefundViewModel: ModelBase
    {
        private IAllDataServices IAllDataServices_data;

        ObservableCollection<CustomerReturnViewItem> _invoicedataList;
        public ObservableCollection<CustomerReturnViewItem> allReturnDataList
        {
            get
            {
                return _invoicedataList;
            }
            set
            {
                _invoicedataList = value;
                NotifyPropertyChanged();
            }
        }
        public ViewOnlyReturnRefundViewModel() {
            IAllDataServices_data = new AllDataServices();
            allReturnDataList = new ObservableCollection<CustomerReturnViewItem>();
            initiallist();
        }
        public async Task initiallist()
        {
            try
            {
                var Wait = UserDialogs.Instance.Loading("Wait...", Cancel(), "Cancel", true, MaskType.Black);

                Wait.Show();


                string frID = Application.Current.Properties["OtherId"].ToString();
                JObject result = await IAllDataServices_data.GetReturnListByFranchiseId(frID) as JObject;
                if (result != null)
                {
                    var type = (int)result["Type"];
                    if (type == 1)
                    {
                        allReturnDataList = JsonConvert.DeserializeObject<ObservableCollection<CustomerReturnViewItem>>(result["Result"].ToString());
                        if (allReturnDataList.Count == 0)
                        {
                            await App.Current.MainPage.DisplayAlert("Oops!", "Goods Return Not Found", "Ok");


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

            return null;
        }
    }
}
