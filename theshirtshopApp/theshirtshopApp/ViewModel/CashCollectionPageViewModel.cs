using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using System.Windows.Input;
using theshirtshopApp.BAL;
using theshirtshopApp.DAL;
using theshirtshopApp.Interface;
using theshirtshopApp.Validation;
using Xamarin.Forms;
using Newtonsoft.Json.Linq;
using Acr.UserDialogs;
using System.Threading;
using theshirtshopApp.View;

namespace theshirtshopApp.ViewModel
{
    public class CashCollectionPageViewModel : ModelBase
    {
        private CashCollection_Class CashCollection_Class_Data;
        private RetailerMaster_Class RetailerMaster_Class_Data;
        private IAllDataServices _IAllDataServices;
        public bool RetailerContectNoEnabled = true;
        public bool AfterCheckContect = false;
        public string ButtonText = "Get";
        private string ResivedOTP;
        public bool ButtonSendOTPVisible = true;
        public bool AfterSendOTPCommand = false;
        public string OTP;
        private INavigation _INavigation { get; set; }
        public CashCollectionPageViewModel(INavigation navigation)
        {
            _INavigation = navigation;
            RetailerMaster_Class_Data = new RetailerMaster_Class();
            _IAllDataServices = new AllDataServices();
            GetContectCommand = new Command(async () => await GetContectAsync());
            CashCollection_Class_Data = new CashCollection_Class();
            SendOTPCommand = new Command(async () => await SendOTP());
            SaveCommand = new Command(async () => await Save());
        }
        private Action Cancel()
        {
            var cancelSrc = new CancellationTokenSource();
            return cancelSrc.Cancel;
        }
        private static string Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes, 0, base64EncodedBytes.Length);
        }
        private async Task Save()
        {
            var Wait = UserDialogs.Instance.Loading("Wait...", Cancel(), "Cancel", true, MaskType.Black);
            Wait.Show();
            if (HasErrors)
            {
                // Error message
                ScrollToControlProperty(GetFirstInvalidPropertyName);
            }
            else
            {
                if (_Retailer_Collected_Cash_Amount > 0)
                {
                    if (_Resived_OTP == _OTP)
                    {
                        CashCollection_Class_Data.Collected_CashAmount = _Retailer_Collected_Cash_Amount;
                        JObject result = await _IAllDataServices.SaveCashCollection(CashCollection_Class_Data);

                        if (result != null)
                        {
                            string type = result["Type"].ToString();
                            if (type == "1")
                            {
                                await App.Current.MainPage.DisplayAlert("Success!", (string)result["ResponseMessage"], "Ok");
                                var ChackPriousPage = _INavigation.NavigationStack.Where(x => x.Title == "Cash Collection").FirstOrDefault();

                                if (ChackPriousPage != null)
                                {

                                    _INavigation.RemovePage(ChackPriousPage);

                                }
                                await _INavigation.PushAsync(new CashCollectionPage());
                            }
                            else
                            {
                                await App.Current.MainPage.DisplayAlert("Error!", (string)result["ResponseMessage"], "Ok");

                            }
                        }
                        else
                        {
                            await App.Current.MainPage.DisplayAlert("Oops!", "Please Refresh Page And try Again....", "Ok");
                        }
                    }
                    else
                    {
                        await App.Current.MainPage.DisplayAlert("Oops!", "OTP is Not Mached...", "Ok");
                    }
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("Oops!", "Please fill Amount...", "Ok");
                }
            }
            Wait.Dispose();
        }

        private async Task SendOTP()
        {
            var Wait = UserDialogs.Instance.Loading("Wait...", Cancel(), "Cancel", true, MaskType.Black);
            Wait.Show();
            if (HasErrors)
            {
                // Error message
                ScrollToControlProperty(GetFirstInvalidPropertyName);
            }
            else
            {
                if (_Retailer_Collected_Cash_Amount > 0)
                {

                    JObject result = await _IAllDataServices.GetOTP(RetailerMaster_Class_Data);



                    if (result != null)
                    {
                        string type = result["Type"].ToString();



                        if (type == "1")
                        {
                            await App.Current.MainPage.DisplayAlert("Success!", (string)result["ResponseMessage"], "Ok");
                            _Resived_OTP = (string)result["Result"];
                            _Retailer_Contect_No_Enabled = false;
                            _Button_Send_OTP_Visible = false;
                            _After_Send_OTP_Command = true;

                        }
                        else
                        {
                            await App.Current.MainPage.DisplayAlert("Error!", (string)result["ResponseMessage"], "Ok");

                        }
                    }
                    else
                    {
                        await App.Current.MainPage.DisplayAlert("Oops!", "Please try Again....", "Ok");
                    }
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("Oops!", "Please fill Amount...", "Ok");
                }
            }

            Wait.Dispose();
        }

        private async Task GetContectAsync()
        {
            var Wait = UserDialogs.Instance.Loading("Wait...", Cancel(), "Cancel", true, MaskType.Black);
            Wait.Show();
            if (_Button_Text == "Get")
            {


                if (HasErrors)
                {
                    // Error message
                    ScrollToControlProperty(GetFirstInvalidPropertyName);
                }
                else
                {
                    if (!string.IsNullOrEmpty(RetailerMaster_Class_Data.Mobile_No))
                    {

                        JObject result = await _IAllDataServices.GetRetailerByMobileNo(RetailerMaster_Class_Data);
                        if (result != null)
                        {
                            string type = result["Type"].ToString();
                            if (type == "1")
                            {
                                await App.Current.MainPage.DisplayAlert("Success!", "Retailer : " + (string)result["Result"]["Retailer_Name"] + " Is Registered", "Ok");
                                CashCollection_Class_Data.Retailer_Id = (int)result["Result"]["RetailerMaster_Id"];
                                _Retailer_Contect_No_Enabled = false;
                                _Button_Text = "Cancel";
                                _After_Check_Contect = true;
                            }
                            else
                            {
                                await App.Current.MainPage.DisplayAlert("Error!", (string)result["ResponseMessage"], "Ok");

                            }
                        }
                        else
                        {
                            await App.Current.MainPage.DisplayAlert("Oops!", "Please try Again....", "Ok");
                        }

                    }
                    else
                    {
                        _Retailer_Contect_No = "";
                    }
                }
            }
            else
            {
                _Retailer_Contect_No = "";
                _Retailer_Contect_No_Enabled = true;
                _Button_Text = "Get";
                _Button_Send_OTP_Visible = true;
                _After_Check_Contect = false;
                _Retailer_Collected_Cash_Amount = 0;
                _After_Send_OTP_Command = false;
                ///////////////////////////////////////////////   _OTP = "";
                _Resived_OTP = "";
            }

            Wait.Dispose();
        }

        [Display(Name = "Retailer Mobile No.")]
        [Required]
        [StringLength(10, ErrorMessage = "Only 10 Number Insert")]
        [RegularExpression("[0-9]{10}", ErrorMessage = "Incorrect Mobile No.")]
        public string _Retailer_Contect_No
        {
            get { return RetailerMaster_Class_Data.Mobile_No; }
            set
            {
                RetailerMaster_Class_Data.Mobile_No = value;
                NotifyPropertyChanged();
                ValidateProperty(value);

            }
        }
        public string _Button_Text
        {
            get { return ButtonText; }
            set
            {
                ButtonText = value;
                NotifyPropertyChanged();
            }
        }
        public bool _After_Check_Contect
        {
            get { return AfterCheckContect; }
            set
            {
                AfterCheckContect = value;
                NotifyPropertyChanged();
            }
        }
        public bool _Retailer_Contect_No_Enabled
        {
            get { return RetailerContectNoEnabled; }
            set
            {
                RetailerContectNoEnabled = value;
                NotifyPropertyChanged();
            }
        }
        public bool _Button_Send_OTP_Visible
        {
            get { return ButtonSendOTPVisible; }
            set
            {
                ButtonSendOTPVisible = value;
                NotifyPropertyChanged();
            }
        }
        public bool _After_Send_OTP_Command
        {
            get { return AfterSendOTPCommand; }
            set
            {
                AfterSendOTPCommand = value;
                NotifyPropertyChanged();
            }
        }

        public string _Resived_OTP
        {
            get { return ResivedOTP; }
            set
            {
                ResivedOTP = value;
                NotifyPropertyChanged();
            }
        }



        [Display(Name = "Retailer Collected Cash")]
        [Required]
        public int _Retailer_Collected_Cash_Amount
        {
            get { return CashCollection_Class_Data.Collected_CashAmount; }
            set
            {
                CashCollection_Class_Data.Collected_CashAmount = value;
                NotifyPropertyChanged();
                ValidateProperty(value);

            }
        }
        [Display(Name = "OTP")]
        [Required]
        public string _OTP
        {
            get { return OTP; }
            set
            {
                OTP = value;
                NotifyPropertyChanged();
                ValidateProperty(value);

            }
        }


        public ICommand GetContectCommand { get; }
        public ICommand SendOTPCommand { get; }
        public ICommand SaveCommand { get; }

    }
}
