using Acr.UserDialogs;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using theshirtshopApp.DAL;
using theshirtshopApp.Interface;
using theshirtshopApp.Validation;
using Xamarin.Forms;

namespace theshirtshopApp.ViewModel
{
   public class ForgetPasswordViewModel: ModelBase
    {
        private string _primeryNumber;
        [Display(Name = "Mobile Number")]
        [Required]
        [StringLength(10, ErrorMessage = "Invalid Mobile Number.")]
      
        private IAllDataServices _IAllDataServices;
        public INavigation Navigation { get; set; }
        public string strPrimeryNumber {
            get {
                return _primeryNumber;
            }
            set {
                _primeryNumber = value;
                NotifyPropertyChanged();
            }

        }
        public ICommand forgetCommand { get; set; }
        public ForgetPasswordViewModel(INavigation navigation) {
            this.forgetCommand = new Command(async () => await forgetPasswordCommand());
            Navigation = navigation;
        }

        private async Task  forgetPasswordCommand()
        {
            if (!string.IsNullOrEmpty(strPrimeryNumber)&& strPrimeryNumber.Length==10 && !strPrimeryNumber.Contains(" "))
            {
                var Wait = UserDialogs.Instance.Loading("Wait...", null, null, true, MaskType.Black);
                Wait.Show();
                _IAllDataServices = new AllDataServices();
                JObject result = await _IAllDataServices.CheckMobileNo(strPrimeryNumber);
                Wait.Hide();
                if (result["StatusCode"].ToString() == "406")
                {
                    await App.Current.MainPage.DisplayAlert("Error", result["ResponseMessage"].ToString(), "Ok");
                }
                else {

                    await App.Current.MainPage.DisplayAlert("Error", result["ResponseMessage"].ToString(), "Ok");
                    await Navigation.PopModalAsync();
                }
                
            }
            else
            {
               
                await App.Current.MainPage.DisplayAlert("Error", "Invalid Mobile Number!!", "Ok");

            }
         }
    }
}
