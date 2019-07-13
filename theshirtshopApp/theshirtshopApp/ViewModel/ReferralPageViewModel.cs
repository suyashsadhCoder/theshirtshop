using Acr.UserDialogs;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using theshirtshopApp.BAL;
using theshirtshopApp.DAL;
using theshirtshopApp.Interface;
using theshirtshopApp.Validation;
using theshirtshopApp.View;
using Xamarin.Forms;

namespace theshirtshopApp.ViewModel
{
   public class ReferralPageViewModel :ModelBase
    {
        private FranchiseReferral_Class FranchiseReferral_Class_Data;
        private IAllDataServices _IAllDataServices;
        public Command SubmitCommand { get; }
        private INavigation _INavigation { get; set; }
        public ReferralPageViewModel(INavigation navigation)
        {
            _INavigation = navigation;
            FranchiseReferral_Class_Data = new FranchiseReferral_Class();
            this.SubmitCommand = new Command(async () => await Submit());
           
            this._IAllDataServices = new AllDataServices();
        }
        private async Task Submit()
        {
            try
            {
                if (HasErrors)
                {
                    ScrollToControlProperty(GetFirstInvalidPropertyName);
                }
                else
                {
                    if (!string.IsNullOrEmpty(FranchiseReferral_Class_Data.Referral_Address) && !string.IsNullOrEmpty(FranchiseReferral_Class_Data.Referral_EmailId) && !string.IsNullOrEmpty(FranchiseReferral_Class_Data.Referral_MobileNo) && !string.IsNullOrEmpty(FranchiseReferral_Class_Data.Referral_Name))
                    {
                        var Wait = UserDialogs.Instance.Loading("Wait...", null, null, true, MaskType.Black);
                        Wait.Show();
                        JObject result = await _IAllDataServices.ReferralRegistration(FranchiseReferral_Class_Data);
                       


                        if (result != null)
                        {
                            string type = result["Type"].ToString();
                            if (type == "1")
                            {
                                await App.Current.MainPage.DisplayAlert("Success!", (string)result["ResponseMessage"], "Ok");
                              
                                
                                var ChackPriousPage = _INavigation.NavigationStack.Where(x => x.Title == "Referral").FirstOrDefault();

                                if (ChackPriousPage != null)
                                {

                                    _INavigation.RemovePage(ChackPriousPage);

                                }
                                await _INavigation.PushAsync(new ReferralPage());
                            }
                            else
                            {
                                await App.Current.MainPage.DisplayAlert("Error", (string)result["ResponseMessage"], "Ok");
                            }
                        }
                        else
                        {
                            await App.Current.MainPage.DisplayAlert("Oops!", "Please Refresh Page And try Again....", "Ok");
                        }
                        Wait.Dispose();
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(FranchiseReferral_Class_Data.Referral_Name))
                        {
                            _Referral_Name = "";
                        }
                        else if (string.IsNullOrEmpty(FranchiseReferral_Class_Data.Referral_MobileNo))
                        {
                            _Referral_Contect_No = "";
                        }
                        else if (string.IsNullOrEmpty(FranchiseReferral_Class_Data.Referral_EmailId))
                        {
                            _Referral_Email_Id = "";
                        }
                        else if (string.IsNullOrEmpty(FranchiseReferral_Class_Data.Referral_Address))
                        {
                            _Address = "";
                        }
                    }
                }
            }
            catch (Exception ee)
            {
                await App.Current.MainPage.DisplayAlert("Error", ee.Message, "Ok");
            }
        }
        [Display(Name = "Referral Name")]
        [Required]
        [StringLength(maximumLength:30,ErrorMessage ="Maximum 30 char required")]
        public string _Referral_Name
        {
            get { return FranchiseReferral_Class_Data.Referral_Name; }
            set
            {
                FranchiseReferral_Class_Data.Referral_Name = value;
                NotifyPropertyChanged();
                ValidateProperty(value);

            }
        }
        [Display(Name = "Referral Contact No.")]
        [Required]
        [StringLength(10, ErrorMessage = "Only 10 Number Insert")]
        [RegularExpression("[0-9]{10}", ErrorMessage = "Incorrect Contact No.")]
        public string _Referral_Contect_No
        {
            get { return FranchiseReferral_Class_Data.Referral_MobileNo; }
            set
            {
                FranchiseReferral_Class_Data.Referral_MobileNo = value;
                NotifyPropertyChanged();
                ValidateProperty(value);
                //  OnPropertChange(nameof(Required_Message));
            }
        }
        
        [Display(Name = "Referral Email Id ")]
        [Required]
        [RegularExpression(@"^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$", ErrorMessage = "Incorrect Email Id.")]
        public string _Referral_Email_Id
        {
            get { return FranchiseReferral_Class_Data.Referral_EmailId; }
            set
            {
                FranchiseReferral_Class_Data.Referral_EmailId = value;
                NotifyPropertyChanged();
                ValidateProperty(value);
            }
        }
        [Display(Name = "Referral Address")]
        [Required]
        [StringLength(maximumLength:150,ErrorMessage ="Maximum 150 Char Required")]
        public string _Address
        {
            get { return FranchiseReferral_Class_Data.Referral_Address; }
            set
            {
                FranchiseReferral_Class_Data.Referral_Address = value;
                NotifyPropertyChanged();
                ValidateProperty(value);
            }
        }
    }
}
