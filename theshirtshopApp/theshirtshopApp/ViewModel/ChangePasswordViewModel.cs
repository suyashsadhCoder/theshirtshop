using Acr.UserDialogs;
using BalLayer_TheShirtShop;
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
   public class ChangePasswordViewModel :ModelBase
    {
        private AppLoginClass AppLoginClass_Data;
        private IAllDataServices IAllDataServices_data;
        private INavigation _INavigation { get; set; }
        public ChangePasswordViewModel(INavigation  navigation)
        {
            _INavigation = navigation;
            AppLoginClass_Data = new AppLoginClass();
            IAllDataServices_data = new AllDataServices();
            SaveCommand = new Command(async () => await Save());
        }
        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }
        private Action Cancel()
        {
            var cancelSrc = new CancellationTokenSource();
            return cancelSrc.Cancel;
        }
        private async Task Save()
        {
            var Wait = UserDialogs.Instance.Loading("Wait...", Cancel(), "Cancel", true, MaskType.Black);
            Wait.Show();
            if (!string.IsNullOrEmpty(_OldPassword) && !string.IsNullOrEmpty(_NewPassword) && !string.IsNullOrEmpty(_ConformPassword))
            {
                if (_NewPassword == _ConformPassword)
                {
                  string pass=  Base64Decode(Application.Current.Properties["Password"].ToString());
                    if (pass == _OldPassword)
                    {
                        AppLoginClass_Data.AppUser_Password = _NewPassword;

                        
                        JObject result = await IAllDataServices_data.ChangePassword(AppLoginClass_Data);
                        if (result != null)
                        {
                            string type = result["Type"].ToString();

                            if (type == "1")
                            {
                                Application.Current.Properties["Password"] = Base64Encode(_NewPassword);

                                await App.Current.MainPage.DisplayAlert("Success!", (string)result["ResponseMessage"], "Ok");
                                var ChackPriousPage = _INavigation.NavigationStack.Where(x => x.Title == "Change Password").FirstOrDefault();

                                if (ChackPriousPage != null)
                                {

                                    _INavigation.RemovePage(ChackPriousPage);

                               }
                               await _INavigation.PushAsync(new ChangePasswordPage());
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
                        await App.Current.MainPage.DisplayAlert("Oops!", "Old Password not mached", "Ok");
                    }
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("Oops!","New Password and Confirm Password not mached","Ok");
                }
               
            }
            else
            {
                if (string.IsNullOrEmpty(_OldPassword))
                {
                    _OldPassword = "";
                }
                else if (string.IsNullOrEmpty(_NewPassword))
                {
                    _NewPassword = "";
                }
                else if (string.IsNullOrEmpty(_ConformPassword))
                {
                    _ConformPassword = "";
                }
            }
            Wait.Dispose();
        }

        [Display(Name ="Old Password")]
        [Required]
        [StringLength(maximumLength:50,ErrorMessage ="Out of lenth")]
        public string _OldPassword
        {
            get { return OldPassword; }
            set
            {
                OldPassword = value;
                NotifyPropertyChanged();
                ValidateProperty(value);
            }
        }
        [Display(Name = "New Password")]
        [Required]
        [StringLength(maximumLength: 50,MinimumLength =5,ErrorMessage ="please fill minimum 5 char and maximum 50 char")]
        public string _NewPassword
        {
            get { return NewPassword; }
            set
            {
                NewPassword = value;
                NotifyPropertyChanged();
                ValidateProperty(value);
            }
        }
        [Display(Name = "Confirm Password")]
        [Required]
        [StringLength(maximumLength: 50, MinimumLength = 5, ErrorMessage = "please fill minimum 5 char and maximum 50 char")]
        public string _ConformPassword
        {
            get { return ConformPassword; }
            set
            {
                ConformPassword = value;
                NotifyPropertyChanged();
                ValidateProperty(value);

            }
        }
        public ICommand SaveCommand { get; }
        public string OldPassword { get; private set; }
        public string NewPassword { get; private set; }
        public string ConformPassword { get; private set; }

        private static string Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes, 0, base64EncodedBytes.Length);
        }


      



    }
}
