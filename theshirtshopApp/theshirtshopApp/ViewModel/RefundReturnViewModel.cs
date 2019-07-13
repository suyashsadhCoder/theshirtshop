using Acr.UserDialogs;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using theshirtshopApp.BAL;
using theshirtshopApp.DAL;
using theshirtshopApp.Interface;
using theshirtshopApp.Validation;
using Xamarin.Forms;

namespace theshirtshopApp.ViewModel
{
  
    public class RefundReturnViewModel : ModelBase
    {
        private IAllDataServices IAllDataServices_data;
        InvoiceFilterModel _filterModel;

        
        string _mobileInvoiceNumber;
        public string mobileOrInvoiceNumber { get { return _mobileInvoiceNumber; } set { _mobileInvoiceNumber = value; NotifyPropertyChanged(); } }
        public  InvoiceFilterModel InvoiceFilter
        {
            get
            {
                return _filterModel;
            }
            set
            {
                _filterModel = value;
                NotifyPropertyChanged();
            }
        }
        ObservableCollection<InvoiceDataModel> _invoicedataList;
        public ObservableCollection<InvoiceDataModel> InvoiceDataList {
            get {
                return _invoicedataList;
            }
            set {
                _invoicedataList = value;
                NotifyPropertyChanged();
            }
        }
        public RefundReturnViewModel()
        {
           
            InvoiceFilter = new InvoiceFilterModel()
            {
                Amount = "",
                ArticleNo = "",
                CurrentPage = "1",
                FranchiseId = Application.Current.Properties["OtherId"].ToString(),
                FromDate = "",
                SearchBy = "",
                SortDirection = "",
                PageSize = "50",
                SortExpression = "FranchiseSellId",
                Status = "",
                ToDate = ""
            };
            IAllDataServices_data = new AllDataServices();
            InvoiceDataList = new ObservableCollection<InvoiceDataModel>();
            
        }
        public async  Task initiallist() {
            try
            {
                var Wait = UserDialogs.Instance.Loading("Wait...", Cancel(), "Cancel", true, MaskType.Black);
               
                    Wait.Show();

                

                JObject result = await IAllDataServices_data.getAllArdersByFranchisee(InvoiceFilter) as JObject;
                if (result != null)
                {
                    var type = (int)result["Type"];
                    if (type == 1)
                    {
                        InvoiceDataList = JsonConvert.DeserializeObject<ObservableCollection<InvoiceDataModel>>(result["Result"].ToString());
                        if (InvoiceDataList.Count == 0)
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
        public async Task loadlistByFilterAsync() {
            try
            {
               


                JObject result = await IAllDataServices_data.getAllArdersByFranchisee(InvoiceFilter) as JObject;
                if (result != null)
                {
                    var type = (int)result["Type"];
                    if (type == 1)
                    {
                        InvoiceDataList = JsonConvert.DeserializeObject<ObservableCollection<InvoiceDataModel>>(result["Result"].ToString());
                        if (InvoiceDataList.Count == 0)
                        {
                            await App.Current.MainPage.DisplayAlert("Oops!", "Goods Return Not Found", "Ok");


                        }

                    }
                    else
                    {
                        await App.Current.MainPage.DisplayAlert("Error!", (string)result["ResponseMessage"], "Ok");
                    }
                  
                }


            }
            catch (Exception ee)
            {
                await App.Current.MainPage.DisplayAlert("Error!", ee.Message, "Ok");
            }
        }

    }
}
