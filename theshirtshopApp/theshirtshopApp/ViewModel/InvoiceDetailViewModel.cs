using Acr.UserDialogs;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using theshirtshopApp.BAL;
using theshirtshopApp.DAL;
using theshirtshopApp.Interface;
using theshirtshopApp.Validation;
using theshirtshopApp.View;
using Xamarin.Forms;

namespace theshirtshopApp.ViewModel
{
    public enum DisabledStatus {
        RETURNED,
        REFUNDED
    }
    public class InvoiceDetailViewModel :ModelBase
    {
        private IAllDataServices IAllDataServices_data;
        public Command returnDecrese { get { return new Command<int>(async param => await returnDecreseBtn(param)); } }
        public Command damageIncrease { get { return new Command<int>(async param => await returnDecreseBtn(param)); } }
        public Command damageDecrease { get { return new Command<int>(async param => await returnDecreseBtn(param)); } }
        public Command returnIncrease { get { return new Command<int>(async param => await returnDecreseBtn(param)); } }
        public Command redirectToPreview => new Command(async () => await redirectPreviewBtnAsync());

        private async Task redirectPreviewBtnAsync()
        {
            if (invoiceDetail.isQuantityUpdated(InvoiceDetailTEMP))
            {

                await navigation.PushAsync(new InvoiceDetailsSubmit(invoiceDetail));

            }
            else
            {

                await App.Current.MainPage.DisplayAlert("Error!", "Nothing has been changed!!", "Ok");

            }

        }

        private Task damageDecreaseBtn(int param)
        {
            invoiceDetail.articleList[param].decreaseDamage();
            return null;
        }
        private Task damageIncreaseBtn(int param)
        {
            invoiceDetail.articleList[param].increaseDamage();
            return null;
        }


        private Task returnDecreseBtn(int param)
        {
            invoiceDetail.articleList[param].decreaseReturn();
            return null;
        }

        private Task returnIncreaseBtn(int param)
        {
            invoiceDetail.articleList[param].increaseReturn();
            return null;
        }

        

        bool _isEnabledToReturend;
        public bool IsEnabledToReturnd {
            get {
                return _isEnabledToReturend;
            }
            set {
                _isEnabledToReturend = value;
               
                NotifyPropertyChanged();
            }
        }

        private string _layoutColor;

        public string LayoutColor { get { return _layoutColor; } set { _layoutColor = value; NotifyPropertyChanged(); } }

        private InvoiceDetailModel _invoiceDetail;
        public InvoiceDetailModel invoiceDetail
        {

            get { return _invoiceDetail; }
            set
            {
                _invoiceDetail = value;
                NotifyPropertyChanged();
            }

        }


        private InvoiceDetailModel _invoiceDetailTEMP;
        public InvoiceDetailModel InvoiceDetailTEMP
        {

            get { return _invoiceDetailTEMP; }
            set
            {
                _invoiceDetailTEMP = value;
                NotifyPropertyChanged();
            }

        }

        public INavigation navigation { get; set; }

        public InvoiceDetailViewModel(string frechiseSellID, INavigation navi) {
            navigation = navi;
            LayoutColor = "Silver";
            IAllDataServices_data = new AllDataServices();
            initiallist(frechiseSellID);
            
           

        }
        private async void initiallist(string frid)
        {
            var Wait = UserDialogs.Instance.Loading("Wait...", Cancel(), "Cancel", true, MaskType.Black);
            Wait.Show();
            try
            {
               
                JObject result = await IAllDataServices_data.getInvoiceDetailsByID(frid) as JObject;
                if (result != null)
                {
                    var type = (int)result["Type"];
                    if (type == 1)
                    {
                        ObservableCollection<InvoiceDetailsServerModel> tempLIst = JsonConvert.DeserializeObject<ObservableCollection<InvoiceDetailsServerModel>>(result["Result"].ToString());
                        if (tempLIst.Count == 0)
                        {
                            await App.Current.MainPage.DisplayAlert("Oops!", "Goods Return Not Found", "Ok");


                        }
                        else
                        {
                           invoiceDetail = new InvoiceDetailModel().sortOrderDetails(tempLIst);
                            InvoiceDetailTEMP = JsonConvert.DeserializeObject<InvoiceDetailModel>(JsonConvert.SerializeObject(invoiceDetail)); ;
                            IsEnabledToReturnd = !Enum.GetNames(typeof(DisabledStatus)).ToList<string>().Contains(invoiceDetail.SellStatus);
                            LayoutColor =IsEnabledToReturnd? "Orange": "Silver";
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
