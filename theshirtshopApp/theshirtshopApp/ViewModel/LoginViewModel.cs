using Acr.UserDialogs;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using BalLayer_TheShirtShop;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using theshirtshopApp.BAL;
using theshirtshopApp.DAL;
using theshirtshopApp.Interface;
using theshirtshopApp.Validation;
using theshirtshopApp.View;
using Xamarin.Forms;
using Application = Xamarin.Forms.Application;

namespace theshirtshopApp.ViewModel
{
    public class LoginViewModel : ModelBase
    {
        private INavigation _navigation { get; set; }
        private AppLoginClass _AppLoginClass;
        private IAllDataServices _IAllDataServices;
        private IAccountInformation _IAccountInformation;
        private bool TermConditionChecked = false;
        public LoginViewModel(INavigation navigation)
        {
            this._navigation = navigation;
            this.LoginCommand = new Command(async () => await Login());
            this._AppLoginClass = new AppLoginClass();
            this._IAllDataServices = new AllDataServices();
            this._IAccountInformation = new AccountInformation();
        }
        [Display(Name = "User Id")]
        [Required]
        [StringLength(maximumLength:30 ,ErrorMessage ="Maximum 30 Char Required.")]
        public string User_Id
        {
            get { return UserId; }
            set
            {
                UserId = value;
                NotifyPropertyChanged();
                ValidateProperty(value);
            }
        }
        [Display(Name = "Password")]
        [Required]
        [StringLength(maximumLength: 20, ErrorMessage = "Maximum 20 Char Required.")]
        public string User_Password
        {
            get { return UserPassword; }
            set
            {
                UserPassword = value;
                NotifyPropertyChanged();
                ValidateProperty(value);
            }
        }
        public bool Term_Condition_Checked
        {
            get { return TermConditionChecked; }
            set {
                TermConditionChecked = value;
                NotifyPropertyChanged();
            }
        }
        public ICommand LoginCommand { get; }
        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }
        public Command RegistrationCommand { get; }
        public string UserPassword { get;set; }
        public string UserId { get;set; }
        private static string Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes, 0, base64EncodedBytes.Length);
        }

        public async Task Login()
        {
            try
            {
                if (HasErrors)
                {
                    ScrollToControlProperty(GetFirstInvalidPropertyName);
                }
                else
                {
                    if (!string.IsNullOrEmpty(User_Id) && !string.IsNullOrEmpty(User_Password))
                    {
                        if (Term_Condition_Checked == true)
                        {
                            _AppLoginClass.AppUser_Name = Base64Encode(User_Id);
                            _AppLoginClass.AppUser_Password = Base64Encode(User_Password);
                            var Wait = UserDialogs.Instance.Loading("Wait...", null, null, true, MaskType.Black);
                            Wait.Show();
                            JObject result = await _IAllDataServices.CheckLoginAsync(_AppLoginClass);
                            if (result != null)
                            {
                                string type = result["Type"].ToString();
                                if (type == "0")
                                {
                                    AppLoginClass apc = new AppLoginClass();
                                    apc.AppLogin_Id = (int)result["Result"]["AppLogin_Id"];
                                    apc.AppUser_Name = (string)_AppLoginClass.AppUser_Name;
                                    apc.AppUser_Password = (string)_AppLoginClass.AppUser_Password;
                                    apc.AppUserType_Id = (int)result["Result"]["AppUserType_Id"];
                                    apc.Other_Id = (int)result["Result"]["Other_Id"];
                                    apc.User_Key = (string)result["Result"]["User_Key"];
                                    _IAccountInformation.SaveCredentials(apc);
                                    
                                  

                                  
                                        var version = DependencyService.Get<IAppVersionProvider>();
                                        var versionString = version.AppVersion;


                                        JObject output = await _IAllDataServices.GetProfile(versionString);

                                        if (output != null)
                                        {
                                            string Success = output["Type"].ToString();
                                            if (Success == "1")
                                            {
                                                if (apc.AppUserType_Id == 1)
                                                {
                                                    App.Code = (string)output["Result"]["Franchise_Code"];
                                               
                                                App.ContectNo = (string)output["Result"]["Franchise_PrimaryMobileNo"];
                                                    App.Address = (string)output["Result"]["Franchise_PermanentAddress"] + ',' + (string)output["Result"]["City_Name"] + ',' + (string)output["Result"]["State_Name"];
                                                    App.MailId = (string)output["Result"]["Franchise_EmailId"];
                                                    App.Name = (string)output["Result"]["Franchise_OwnerName"];
                                                    App.Photo = "http://www.theshirtshop.in/TemplateContent/Admin/images/FranchiseImages/FranchisePhoto/" + (string)output["Result"]["Franchise_Photo"];
                                                }
                                                else
                                                {
                                                    App.Code = (string)output["Result"]["Emp_Code"];
                                                    App.ContectNo = (string)output["Result"]["Emp_PrimaryMobileNo"];
                                                    App.Address = (string)output["Result"]["Emp_PermanetAddress"] + ',' + (string)output["Result"]["City_Name"] + ',' + (string)output["Result"]["State_Name"];
                                                    App.MailId = (string)output["Result"]["Emp_EmailId"];
                                                    App.Name = (string)output["Result"]["Emp_FirstName"] + " " + (string)output["Result"]["Emp_LastName"];
                                                    App.Photo = "http://www.theshirtshop.in/TemplateContent/Admin/images/EmployeeImages/EmployeePhoto/" + (string)output["Result"]["Emp_Photo"];
                                                }

                                                if (apc.AppUserType_Id == 1)
                                                {
                                                    User_Id = "";
                                                    User_Password = "";
                                                    App.Current.MainPage = new FranchiseMaster();
                                                }
                                                else if (apc.AppUserType_Id == 2)
                                                {

                                                    User_Id = "";
                                                    User_Password = "";

                                                    App.Current.MainPage = new EmployeeFranchiseMaster();
                                                }
                                                else if (apc.AppUserType_Id == 3)
                                                {

                                                    User_Id = "";
                                                    User_Password = "";

                                                    App.Current.MainPage = new EmployeeRetailerMaster();

                                                }
                                                else if (apc.AppUserType_Id == 4)
                                                {

                                                    User_Id = "";
                                                    User_Password = "";

                                                    App.Current.MainPage = new EmployeeFranchiseWithRetailerMaster();

                                                }
                                                else
                                                {
                                                    await App.Current.MainPage.DisplayAlert("Error", (string)result["ResponseMessage"], "Ok");
                                                    //   return Ok(new { StatusCode = HttpStatusCode.NotAcceptable, Type = "0", ResponseMessage = "You are not authorized person", Result = "" });
                                                }

                                            }
                                            else if (Success == "101")
                                            {

                                                var outputs = await UserDialogs.Instance.ConfirmAsync(new ConfirmConfig
                                                {
                                                    Message = "Application new version available at play store you need to update new version of app.",
                                                    OkText = "Yes",
                                                    CancelText = "No",
                                                    Title = "New Version"

                                                });

                                                if (outputs)
                                                {
                                                    Device.OpenUri(new Uri("https://play.google.com/store/apps/details?id=com.companyname.theshirtshopApp"));
                                                    if (Application.Current.Properties.ContainsKey("Key"))
                                                    {
                                                        Application.Current.Properties["Key"] = null;
                                                        Application.Current.Properties["UserName"] = null;
                                                        Application.Current.Properties["Password"] = null;
                                                        Application.Current.Properties["OtherId"] = null;
                                                        Application.Current.Properties.Remove("Key");
                                                        Application.Current.Properties.Remove("UserName");
                                                        Application.Current.Properties.Remove("Password");
                                                        Application.Current.Properties.Remove("OtherId");
                                                    }

                                                    App.Current.MainPage = new MainPage();
                                                }
                                                else
                                                {

                                                    if (Application.Current.Properties.ContainsKey("Key"))
                                                    {
                                                        Application.Current.Properties["Key"] = null;
                                                        Application.Current.Properties["UserName"] = null;
                                                        Application.Current.Properties["Password"] = null;
                                                        Application.Current.Properties["OtherId"] = null;
                                                        Application.Current.Properties.Remove("Key");
                                                        Application.Current.Properties.Remove("UserName");
                                                        Application.Current.Properties.Remove("Password");
                                                        Application.Current.Properties.Remove("OtherId");
                                                    }

                                                    App.Current.MainPage = new MainPage();
                                                }

                                            }
                                            else if (!string.IsNullOrEmpty(Success))
                                            {

                                                if (Application.Current.Properties.ContainsKey("Key"))
                                                {
                                                    Application.Current.Properties["Key"] = null;
                                                    Application.Current.Properties["UserName"] = null;
                                                    Application.Current.Properties["Password"] = null;
                                                    Application.Current.Properties["OtherId"] = null;
                                                    Application.Current.Properties.Remove("Key");
                                                    Application.Current.Properties.Remove("UserName");
                                                    Application.Current.Properties.Remove("Password");
                                                    Application.Current.Properties.Remove("OtherId");
                                                }
                                                App.Current.MainPage = new MainPage();
                                            }
                                        }

                                    
                                }
                                else if (Convert.ToInt32(type) > 0)
                                {
                                    var con = await UserDialogs.Instance.ConfirmAsync(new ConfirmConfig
                                    {
                                        Title = "Online Transaction",
                                        Message = (string)result["ResponseMessage"],
                                        OkText = "Yes",
                                        CancelText = "No",

                                    });
                                    if (con)
                                    {
                                        await _navigation.PushModalAsync(new FranchiseAdminPaymentDeatailAfterLoginPage(Convert.ToInt32(type)));
                                    }
                                }
                                else
                                {
                                    await App.Current.MainPage.DisplayAlert("Error", (string)result["ResponseMessage"], "Ok");
                                }
                            }
                            Wait.Dispose();
                        }
                        else
                        {
                            await App.Current.MainPage.DisplayAlert("Opps!", "Please Checked Terms and Conditions then after login", "Ok");
                        }
                    }
                    else
                    {
                        User_Id = "";
                        User_Password = "";
                    }
                }
            }
            catch (Exception ee)
            {
                await App.Current.MainPage.DisplayAlert("Error", ee.Message, "Ok");
            }
        }
    }
}
