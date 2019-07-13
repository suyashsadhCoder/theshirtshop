using Acr.UserDialogs;
using Newtonsoft.Json.Linq;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using theshirtshopApp.BAL;
using theshirtshopApp.DAL;
using theshirtshopApp.Interface;
using theshirtshopApp.Validation;
using theshirtshopApp.View;
using Xamarin.Forms;

namespace theshirtshopApp.ViewModels
{
    public class InvoiceDetailsSubmitViewModel : ModelBase
    {

        private InvoiceDetailModel _invoiceDetail;

        public ICommand submitCommand { get; }
        public ICommand isReturnCommand { get; }
        public ICommand isRefundCommand { get; }
        public IAllDataServices _IAllDataServices;

        public InvoiceDetailModel invoiceDetail
        {

            get { return _invoiceDetail; }
            set
            {
                _invoiceDetail = value;
                NotifyPropertyChanged();
            }

        }

        private bool _isReturn;
        private bool _isRefund;

        public bool IsReturn{
            get { return _isReturn; }
            set { _isReturn = value; NotifyPropertyChanged();
            }
        }

        private INavigation _navigation;
        public bool IsRefund
        {
            get { return _isRefund; }
            set { _isRefund = value; NotifyPropertyChanged(); }
        }
        public InvoiceDetailsSubmitViewModel(InvoiceDetailModel invoice, INavigation navigation)
        {
            IsReturn = true;
            _navigation = navigation;
            submitCommand = new Command(async () => await submitInvoiceCommand());
            isRefundCommand = new Command<bool>(setIsRefund);
            isReturnCommand = new Command<bool>(setIsReturn);
            invoiceDetail = invoice;
            _IAllDataServices = new AllDataServices();
        }

        private void setIsReturn(bool obj)
        {
            IsReturn = obj;
            IsRefund = !obj;
        }

        private void setIsRefund(bool obj)
        {
            IsRefund = obj;
            IsReturn = !obj;
        }

        private async Task submitInvoiceCommand()
        {
            if (!App.isBusy)
            {
                // ObservableCollection<FranchiseSellDetails_Class> fscd = new ObservableCollection<FranchiseSellDetails_Class>();
                App.isBusy = true;


                var Wait = UserDialogs.Instance.Loading("Wait..", null, null, true, MaskType.Black);
                Wait.Show();

                SubmitReturnModel returnModel = invoiceDetail.ConvertToSubmitReturnModel(invoiceDetail, IsRefund);
                JObject result = await _IAllDataServices.saveReturnedOrderDetails(returnModel);
                
                if (result!=null && result["Type"].ToString() == "1")
                {
                    await App.Current.MainPage.DisplayAlert((string)result["ResponseMessage"], (string)result["Result"], "Ok");
                    App.isBusy = false;
                    PopUntilDestination(typeof(RefundReturnPage));


                }
                else {
                    App.isBusy = false;
                    await App.Current.MainPage.DisplayAlert("Oops!", (string)result["ResponseMessage"], "Ok");
                }

                Wait.Hide();
            }

        }
        void PopUntilDestination(Type DestinationPage)
        {
            int LeastFoundIndex = 0;
            int PagesToRemove = 0;

            for (int index = _navigation.NavigationStack.Count - 2; index > 0; index--)
            {
                if (_navigation.NavigationStack[index].GetType().Equals(DestinationPage))
                {
                    break;
                }
                else
                {
                    LeastFoundIndex = index;
                    PagesToRemove++;
                }
            }

            for (int index = 0; index < PagesToRemove; index++)
            {
                _navigation.RemovePage(_navigation.NavigationStack[LeastFoundIndex]);
            }

            _navigation.PopAsync();
        }
    }
}

