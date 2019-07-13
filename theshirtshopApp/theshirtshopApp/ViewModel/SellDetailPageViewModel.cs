using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using theshirtshopApp.Validation;
using theshirtshopApp.BAL;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;
using Acr.UserDialogs;
using Newtonsoft.Json.Linq;
using theshirtshopApp.Interface;
using theshirtshopApp.DAL;
using System.Threading;
using System.ComponentModel.DataAnnotations;
using theshirtshopApp.View;
using Newtonsoft.Json;
using theshirtshopApp.Controls;

namespace theshirtshopApp.ViewModel
{
    public class SellDetailPageViewModel : ModelBase
    {
        
        public FranchiseSell_Class FranchiseSell_Class_Data_List { get; set; }
        public ICommand SendOTPCommand { get; }
        public ICommand SaveCommand { get; }


        bool _isRemoveCoupon;
        public bool IsRemoveCoupon
        {
            get
            {
                return _isRemoveCoupon;
            }
            set
            {
                _isRemoveCoupon = value;
                ButtonTitle = _isRemoveCoupon ? "Remove Coupon" : "Apply Coupon";
                NotifyPropertyChanged();
            }
        }
        string _buttonTitel;
        public string ButtonTitle { get { return _buttonTitel; } set { _buttonTitel = value;NotifyPropertyChanged(); } }


        public IAllDataServices _IAllDataServices;
        public string Resived_OTP { get; set; }
        public bool Button_Send_OTP_Visible = true;
        public bool AfterSendOTPCommand = false;
        public string OTP;
        public INavigation _navigation { get;set;}
        public bool enableSave { get; set; }
        decimal _netPayble;
        public decimal NetPayble
        {
            get
            {
                return FranchiseSell_Class_Data_List.Total_Amount - FranchiseSell_Class_Data_List.CouponCodeAmount;
            }
            set {
                _netPayble = FranchiseSell_Class_Data_List.Total_Amount - FranchiseSell_Class_Data_List.CouponCodeAmount;
                NotifyPropertyChanged();
            }
        }

        public SellDetailPageViewModel(FranchiseSell_Class fs,INavigation navigation)
        {
            _navigation = navigation;
            enableSave = true;
            IsRemoveCoupon = false;
            FranchiseSell_Class_Data_List = fs;
            SendOTPCommand = new Command(async () => await SendOTP());
            SaveCommand = new Command(async () => await SaveAsync());
            _IAllDataServices = new AllDataServices();
        }

        private async Task SaveAsync()
        {
            enableSave = false;
            var Wait = UserDialogs.Instance.Loading("Wait...", Cancel(), "Cancel", true, MaskType.Black);
            Wait.Show();
            if (HasErrors)
            {
                enableSave = true;
                // Error message
                ScrollToControlProperty(GetFirstInvalidPropertyName);
            }
            else
            {
                if (FranchiseSell_Class_Data_List.FranchiseSellDetails_Class_List.Count > 0)
                {
                    FranchiseSell_Class_Data_List.Customer_Class_Data = null;
                    FranchiseSell_Class_Data_List.FranchiseSellDetails_Class_Data = null;
                    FranchiseSell_Class_Data_List.Total_Amount = NetPayble;
                    foreach (var item in FranchiseSell_Class_Data_List.FranchiseSellDetails_Class_List)
                    {
                        item.ArticleMaster_Class_Data = null;
                        item._CategoryMaster_Class_Data = null;
                        item._CategoryMaster_Class_List = null;
                        item._FranchiseSell_Class_List = null;
                        item._SubCategoryMaster_Class_Data = null;
                    }
                    JObject result = await _IAllDataServices.SaveSellCustomer(FranchiseSell_Class_Data_List);

                        if (result != null)
                        {
                            string type = result["Type"].ToString();



                            if (type == "1")
                            {
                                await App.Current.MainPage.DisplayAlert("Success!", (string)result["ResponseMessage"], "Ok");
                                _Resived_OTP = (string)result["Result"];
                              
                               
                                var ChackPriousPage = _navigation.NavigationStack.Where(x => x.Title == "Sell Detail").FirstOrDefault();

                                if (ChackPriousPage != null)
                                {

                                    _navigation.RemovePage(ChackPriousPage);

                                }
                                var ChackSecondPriousPage = _navigation.NavigationStack.Where(x => x.Title == "Sell").FirstOrDefault();

                                if (ChackSecondPriousPage != null)
                                {

                                    _navigation.RemovePage(ChackSecondPriousPage);

                                }
                               await _navigation.PushAsync(new SellPage());
                            }
                            else
                            {
                            enableSave = true;
                            await App.Current.MainPage.DisplayAlert("Error!", (string)result["ResponseMessage"], "Ok");

                            }
                        }
                        else
                        {
                        enableSave = true;
                        await App.Current.MainPage.DisplayAlert("Oops!", "Please Refresh Page And try Again....", "Ok");
                        }
                }
                else
                {
                    enableSave = true;
                    await App.Current.MainPage.DisplayAlert("Oops!", "Please Fill Qty Then Send OTP", "Ok");
                }
            }
            Wait.Dispose();
        }

        private async Task SendOTP()
        {
            if (!IsRemoveCoupon)
            {
                var popup = new EntryPopUp("Coupon Code", string.Empty, "OK", "Cancel");
                popup.PopupClosed += async (o, closedArgs) =>
                {

                    if (closedArgs.Button == "OK" && closedArgs.Text.Length > 0)
                    {
                        var Wait = UserDialogs.Instance.Loading("Wait...", Cancel(), "Cancel", true, MaskType.Black);
                        Wait.Show();
                        var coupon = new CouponModel()
                        {
                            CouponCode = closedArgs.Text,
                            CreatedDate = System.DateTime.Now.ToString("yyyy/MM/dd"),
                            CurrentFranchiseId = Application.Current.Properties["OtherId"].ToString(),
                            CurrentOrderAmount = FranchiseSell_Class_Data_List.Total_Amount.ToString(),
                            CustomerId = FranchiseSell_Class_Data_List.Customer_Id.ToString()
                        };
                        JObject result = await _IAllDataServices.CheckCouponCode(coupon);
                        if (result != null)
                        {
                            string type = result["Type"].ToString();
                            if (type == "1")
                            {
                                await App.Current.MainPage.DisplayAlert((string)result["ResponseMessage"], (string)result["Result"]["returnMessage"], "Ok");
                                IsRemoveCoupon = true;
                                FranchiseSell_Class_Data_List.CouponCode = coupon.CouponCode;
                                FranchiseSell_Class_Data_List.CouponCodeAmount = Convert.ToDecimal((string)result["Result"]["DiscountAmount"]);
                                NetPayble = FranchiseSell_Class_Data_List.Total_Amount - FranchiseSell_Class_Data_List.CouponCodeAmount;
                            }
                            else
                            {
                                await App.Current.MainPage.DisplayAlert((string)result["ResponseMessage"], (string)result["Result"]["returnMessage"], "Ok");

                            }
                        }
                        Wait.Hide();
                    }
                };

                popup.Show();
            }
            else {
                NetPayble = FranchiseSell_Class_Data_List.Total_Amount + FranchiseSell_Class_Data_List.CouponCodeAmount;
                FranchiseSell_Class_Data_List.CouponCodeAmount = 0;
                IsRemoveCoupon = false;
            }

        // var dialog = UserDialogs.Instance.Login(new LoginConfig() {a })
        //    var Wait = UserDialogs.Instance.Loading("Wait...", Cancel(), "Cancel", true, MaskType.Black);
        //    Wait.Show();
        //    if (HasErrors)
        //    { 
            //        ScrollToControlProperty(GetFirstInvalidPropertyName);
            //    }
            //    else
            //    {
            //        if (FranchiseSell_Class_Data_List.FranchiseSellDetails_Class_List.Count > 0)
            //        {

            //            JObject result = await _IAllDataServices.SendOTPCustomer(FranchiseSell_Class_Data_List.Customer_Class_Data);

            //            if (result != null)
            //            {
            //                string type = result["Type"].ToString();
            //                if (type == "1")
            //                {
            //                    await App.Current.MainPage.DisplayAlert("Success!", (string)result["ResponseMessage"], "Ok");
            //                    _Resived_OTP = (string)result["Result"];

            //                    _Button_Send_OTP_Visible = false;
            //                    After_Send_OTP_Command = true;

            //                }
            //                else
            //                {
            //                    await App.Current.MainPage.DisplayAlert("Error!", (string)result["ResponseMessage"], "Ok");

            //                }
            //            }
            //            else
            //            {
            //                await App.Current.MainPage.DisplayAlert("Oops!", "Please  try Again....", "Ok");
            //            }


            //        }
            //        else
            //        {
            //            await App.Current.MainPage.DisplayAlert("Oops!", "Please Fill Qty Then Send OTP", "Ok");
            //        }
            //    }

            //    Wait.Dispose();
        }

        private Action Cancel()
        {
            var cancelSrc = new CancellationTokenSource();
            return cancelSrc.Cancel;
        }
        public bool _Button_Send_OTP_Visible
        {
            get { return Button_Send_OTP_Visible; }
            set {
                Button_Send_OTP_Visible= value ;
                NotifyPropertyChanged();
                    }
        }
        public string _Resived_OTP
        {
            get { return Resived_OTP; }
            set { Resived_OTP = value;
                NotifyPropertyChanged();
            }
        }

        public bool After_Send_OTP_Command
        {
            get { return AfterSendOTPCommand; }
            set
            {
                AfterSendOTPCommand= value ;
                NotifyPropertyChanged();
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

        
    }
}
