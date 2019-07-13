using Acr.UserDialogs;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
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
    public partial class FranchiseAdminPaymentDeatailAfterLoginPage : ContentPage
    {
        private IAllDataServices _IAllDataServices;
        private FranchiseAdminPaymentDetail_Main_Class _list;
        public int id { get; set; }
        public FranchiseAdminPaymentDeatailAfterLoginPage(int Id)
        {
            InitializeComponent();
            id = Id;
            _IAllDataServices = new AllDataServices();
            _list = new FranchiseAdminPaymentDetail_Main_Class();
            GatData(Id);
        }

        private async Task GatData(int Id)
        {
            CheckOut.IsVisible = false;
            PaymentMsg.IsVisible = false;
            var wait = UserDialogs.Instance.Loading(null, null, null, true, MaskType.Black);
            wait.Show();

            JObject result = await _IAllDataServices.FistAdminPayAmount(Id);
            if (result != null)
            {
                string type = result["Type"].ToString();
                if (type == "1")
                {
                    _list = JsonConvert.DeserializeObject<FranchiseAdminPaymentDetail_Main_Class>(result["Result"].ToString());
                    if (_list.PaidStatus == true)
                    {
                       
                        PaymentMsg.IsVisible = true;
                    }
                    else
                    {
                        CheckOut.IsVisible = true;
                    }
                    if (_list._FranchiseSellDetailsModel != null)
                    {
                        if (_list._FranchiseSellDetailsModel.Count > 0)
                        {
                            totalAmount.Text = (_list._FranchiseSellDetailsModel.Select(x => x.Amount).Sum()).ToString();
                            CustomerList.ItemsSource = _list._FranchiseSellDetailsModel;
                        }
                        else
                        {
                            CheckOut.IsVisible = false;
                            PaymentMsg.IsVisible = false;
                            UserDialogs.Instance.Alert("Record Not Found", "Opps!", "Ok");
                        }
                    }
                    else
                    {
                        CheckOut.IsVisible = false;
                        PaymentMsg.IsVisible = false;

                        UserDialogs.Instance.Alert("Record Not Found", "Opps!", "Ok");
                    }

                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("Error!", (string)result["Result"]["ResponseMessage"], "Ok");
                }
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Opps!", "Record not found", "Ok");
            }
            wait.Hide();
        }

        private void CheckOut_Clicked(object sender, EventArgs e)
        {

           
            Navigation.PushModalAsync(new PaymentGetWayPage(id));

        }
      
        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            this.OnBackButtonPressed();
        }
    }
}