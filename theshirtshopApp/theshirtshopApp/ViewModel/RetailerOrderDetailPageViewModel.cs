using Acr.UserDialogs;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using theshirtshopApp.BAL;
using theshirtshopApp.DAL;
using theshirtshopApp.Interface;
using theshirtshopApp.Validation;
using theshirtshopApp.View;
using Xamarin.Forms;

namespace theshirtshopApp.ViewModel
{
  public  class RetailerOrderDetailPageViewModel :ModelBase
    {
        public Employee_OrderGenerate_Class Employee_OrderGenerate_Class_data { get; set; }
        public ICommand SendOTPCommand { get; }
        public ICommand SaveCommand { get; }

        public IAllDataServices _IAllDataServices;
        public string Resived_OTP { get; set; }
        public bool Button_Send_OTP_Visible = true;
        public bool AfterSendOTPCommand = false;
        public string OTP;
        public INavigation _navigation { get; set; }
        private DateTime DispatchDate=DateTime.Now;
        public RetailerOrderDetailPageViewModel(Employee_OrderGenerate_Class fs, INavigation navigation)
        {
            _navigation = navigation;
            fs.DateOfDispatch = DateTime.Now;
            Employee_OrderGenerate_Class_data = fs;
            SendOTPCommand = new Command(async () => await SendOTP());
            SaveCommand = new Command(async () => await SaveAsync());
            _IAllDataServices = new AllDataServices();
        }
        private static string Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes, 0, base64EncodedBytes.Length);
        }
        private async Task SaveAsync()
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
                if (Employee_OrderGenerate_Class_data.odm.Count > 0)
                {
                    if (string.IsNullOrEmpty(Employee_OrderGenerate_Class_data.Remark))
                    {
                        await App.Current.MainPage.DisplayAlert("Oops!", "Please Fill Remark...", "Ok");
                    }
                    else if (Employee_OrderGenerate_Class_data.DateOfDispatch == null)
                    {
                        await App.Current.MainPage.DisplayAlert("Oops!", "Please select Dispatch Date...", "Ok");
                    }
                    else if (string.IsNullOrEmpty(Employee_OrderGenerate_Class_data.PreferredTransport))
                    {
                        await App.Current.MainPage.DisplayAlert("Oops!", "Please Fill Preferred Transport...", "Ok");
                    }
                    else if(Employee_OrderGenerate_Class_data.DateOfDispatch<=DateTime.Now)
                        {
                        await App.Current.MainPage.DisplayAlert("Oops!", "Selected dispatch date less then current date...", "Ok");
                    }
                    else if (_Resived_OTP == _OTP)
                    {
                        Employee_OrderGenerate_Class_data.EmployeeMaster_Class_Data = null;
                        Employee_OrderGenerate_Class_data.RetailerMaster_Class_Data = null;
                        Employee_OrderGenerate_Class_data._CategoryMaster_Class = null;
                        foreach (var item in Employee_OrderGenerate_Class_data.odm)
                        {
                            item.ArticleMaster_Class_Data = null;
                            item.CategoryMaster_Class_Date = null;
                            item.SubCategoryMaster_Class_Data = null;
                            item.SubCategoryMaster_Class_List = null;
                           
                        }

                        JObject result = await _IAllDataServices.SaveRetailerOrder(Employee_OrderGenerate_Class_data);

                        if (result != null)
                        {
                            string type = result["Type"].ToString();



                            if (type == "1")
                            {
                                await App.Current.MainPage.DisplayAlert("Success!", (string)result["ResponseMessage"], "Ok");
                               
                              
                                var ChackPriousPage = _navigation.NavigationStack.Where(x => x.Title == "Order Detail").FirstOrDefault();

                                if (ChackPriousPage != null)
                                {

                                    _navigation.RemovePage(ChackPriousPage);

                                }
                                var ChackSecondPriousPage = _navigation.NavigationStack.Where(x => x.Title == "Retailer Order").FirstOrDefault();

                                if (ChackSecondPriousPage != null)
                                {

                                    _navigation.RemovePage(ChackSecondPriousPage);

                                }
                                await _navigation.PushAsync(new RetailerOrderPage());
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
                    await App.Current.MainPage.DisplayAlert("Oops!", "Please Fill Qty Then Send OTP", "Ok");
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
                if (Employee_OrderGenerate_Class_data.odm.Count > 0)
                {
                    if (string.IsNullOrEmpty(Employee_OrderGenerate_Class_data.Remark))
                    {
                        await App.Current.MainPage.DisplayAlert("Oops!", "Please Fill Remark...", "Ok");
                    }
                    else if (Employee_OrderGenerate_Class_data.DateOfDispatch == null)
                    {
                        await App.Current.MainPage.DisplayAlert("Oops!", "Please select Dispatch Date...", "Ok");
                    }
                    else if (string.IsNullOrEmpty(Employee_OrderGenerate_Class_data.PreferredTransport))
                    {
                        await App.Current.MainPage.DisplayAlert("Oops!", "Please Fill Preferred Transport...", "Ok");
                    }
                    else if (Employee_OrderGenerate_Class_data.DateOfDispatch <= DateTime.Now)
                    {
                        await App.Current.MainPage.DisplayAlert("Oops!", "Selected dispatch date less then current date...", "Ok");
                    }
                    else if (_Resived_OTP == _OTP)
                    {


                        JObject result = await _IAllDataServices.GetOTP(Employee_OrderGenerate_Class_data.RetailerMaster_Class_Data);

                        if (result != null)
                        {
                            string type = result["Type"].ToString();



                            if (type == "1")
                            {
                                await App.Current.MainPage.DisplayAlert("Success!", (string)result["ResponseMessage"], "Ok");
                                _Resived_OTP = (string)result["Result"];
                                After_Send_OTP_Command = true;
                                _Button_Send_OTP_Visible = false;
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
                    await App.Current.MainPage.DisplayAlert("Oops!", "Please Fill Qty Then Send OTP", "Ok");
                }
            }

            Wait.Dispose();
        }

        private Action Cancel()
        {
            var cancelSrc = new CancellationTokenSource();
            return cancelSrc.Cancel;
        }
        public bool _Button_Send_OTP_Visible
        {
            get { return Button_Send_OTP_Visible; }
            set
            {
                Button_Send_OTP_Visible = value;
                NotifyPropertyChanged();
            }
        }
        //public DateTime Dispatch_Date
        //{
        //    get { return DispatchDate; }
        //    set { DispatchDate = value ;
        //        NotifyPropertyChanged();
        //    }
            
        //}
        //public string Remark
        //{
            
        //}
        public string _Resived_OTP
        {
            get { return Resived_OTP; }
            set
            {
                Resived_OTP = value;
                NotifyPropertyChanged();
            }
        }

        public bool After_Send_OTP_Command
        {
            get { return AfterSendOTPCommand; }
            set
            {
                AfterSendOTPCommand = value;
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
