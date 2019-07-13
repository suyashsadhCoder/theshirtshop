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
    public partial class FranchiseAdminPaymentDetail : ContentPage
    {
        private IAllDataServices _IAllDataServices;
        private FranchiseAdminPaymentDetail_Main_Class _list;
        private  int Id { get; set; }
        public FranchiseAdminPaymentDetail()
        {
            InitializeComponent();
            _IAllDataServices = new AllDataServices();
            _list = new FranchiseAdminPaymentDetail_Main_Class();
         Id=Convert.ToInt32( Application.Current.Properties["OtherId"].ToString());
            string [] mon = new  string[5];
            int years = DateTime.Now.Year;
            mon[0] = years.ToString();
            for (int i=1;i<5;i++)
            {
               /// year = year - i;
                mon[i] =(years-i).ToString();
            }

            year.ItemsSource = mon;
            GetData();
        }

        private async Task GetData()
        {
           
            CheckOut.IsVisible = false;
            PaymentMsg1.IsVisible = false;
            var wait = UserDialogs.Instance.Loading(null, null, null, true, MaskType.Black);
            wait.Show();
            JObject result = await _IAllDataServices.FistAdminPayAmount(Convert.ToInt32(Id));
            if (result != null)
            {
                string type = result["Type"].ToString();
                if (type == "1")
                {

                    _list = JsonConvert.DeserializeObject<FranchiseAdminPaymentDetail_Main_Class>(result["Result"].ToString());
                    if (_list.PaidStatus == true)
                    {
                        PaymentMsg1.IsVisible = true;

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
                            CustomerList.IsVisible = true;

                        }
                        else
                        {
                            CheckOut.IsVisible = false;
                            CustomerList.IsVisible = false;
                            UserDialogs.Instance.Alert("Current record Not Found", "Opps!", "Ok");
                            //     await App.Current.MainPage.DisplayAlert("Error!", "Current record Not Found", "Ok");
                        }
                    }
                    else
                    {
                       CheckOut.IsVisible = false;
                        CustomerList.IsVisible = false;
                        UserDialogs.Instance.Alert("Current record Not Found", "Opps!", "Ok");
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

        private async void Button_Clicked(object sender, EventArgs e)
        {
            CheckOut.IsVisible = false;
            PaymentMsg1.IsVisible = false;
            DateTime  current_date = DateTime.Now;
            int current_month = current_date.Month;
            int current_day = current_date.Day;
            var months = month.SelectedIndex;
            var years = year.SelectedItem;
            var terms = term.SelectedItem;
            if(months >=0 && years != null && terms != null)
            {
                var wait = UserDialogs.Instance.Loading(null, null, null, true, MaskType.Black);
                wait.Show();
                int m = Convert.ToInt32(months)+1;
                int y = Convert.ToInt32(years);
               string t = terms.ToString();
                int i = Convert.ToInt32(Id);
                JObject result = await _IAllDataServices.SecondAdminPayAmount(m,y,t,i);
                if (result != null)
                {
                    string type = result["Type"].ToString();
                    if (type == "1")
                    {
                        _list = JsonConvert.DeserializeObject<FranchiseAdminPaymentDetail_Main_Class>(result["Result"].ToString());
                        if (_list.PaidStatus == true)
                        {
                            if (m == current_month && current_day <= 15 && terms == "First-Term")
                            {

                                CheckOut.IsVisible = false;
                                PaymentMsg1.IsVisible = false;

                            }
                            else if (m == current_month && current_day > 15 && terms == "Second-Term")
                            {

                                CheckOut.IsVisible = false;
                                PaymentMsg1.IsVisible = false;

                            }
                            else
                            {
                                CheckOut.IsVisible = false;
                                PaymentMsg1.IsVisible = true;
                            }
                        }
                        else
                        {
                            CheckOut.IsVisible = true;
                            PaymentMsg1.IsVisible = false;
                        }
                        if (_list._FranchiseSellDetailsModel != null)
                        {
                            if (_list._FranchiseSellDetailsModel.Count > 0)
                            {
                                totalAmount.Text = (_list._FranchiseSellDetailsModel.Select(x => x.Amount).Sum()).ToString();
                                CustomerList.ItemsSource = _list._FranchiseSellDetailsModel;
                                CustomerList.IsVisible = true;

                            }
                            else
                            {
                                CheckOut.IsVisible = false;
                                CustomerList.IsVisible = false;
                                UserDialogs.Instance.Alert("Record Not Found", "Opps!", "Ok");
                            }
                        }
                        else
                        {
                            CheckOut.IsVisible = false;
                            CustomerList.IsVisible = false;
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
            else
            {
                if(months < 0)
                {
                    UserDialogs.Instance.Alert("Please select month...", "Opps!", "Ok");
                }
                else if(years==null)
                {
                    UserDialogs.Instance.Alert("Please select year...", "Opps!", "Ok");
                }
                else
                {
                    UserDialogs.Instance.Alert("Please select term...", "Opps!", "Ok");
                }
            }
        }
        private void CheckOut_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
             Navigation.PushAsync(new PaymentGetWayPage(Id));
        }
    }
}