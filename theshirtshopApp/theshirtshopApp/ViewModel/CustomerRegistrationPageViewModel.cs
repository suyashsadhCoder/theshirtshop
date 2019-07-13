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
    public class CustomerRegistrationPageViewModel : ModelBase
    {

        private IAllDataServices _IAllDataServices;
        private SellPage _sellPage;
        private Customer_Class Customer_Class_GeSet;
        private INavigation _INavigation { get; set; }
        public CustomerRegistrationPageViewModel(INavigation navigation)
        {
            _INavigation = navigation;
            this.SubmitCommand = new Command(async () => await Submit());
            this.Customer_Class_GeSet = new Customer_Class();
            this._IAllDataServices = new AllDataServices();
        }
        [Display(Name = "Customer Contact No.")]
        [Required]
        [StringLength(10, ErrorMessage = "Only 10 Number Insert")]
        [RegularExpression("[0-9]{10}", ErrorMessage = "Incorrect Contact No.")]
        public string _Customer_Contect_No
        {
            get { return Customer_Class_GeSet.Contact_No; }
            set
            {
                Customer_Class_GeSet.Contact_No = value;
                NotifyPropertyChanged();
                ValidateProperty(value);
                //  OnPropertChange(nameof(Required_Message));
            }
        }
        [Display(Name = "Customer Name")]
        [Required]
        [StringLength(maximumLength:30,ErrorMessage ="Maximum 30 Char Required")]
        public string _Customer_Name
        {
            get { return Customer_Class_GeSet.Customer_Name; }
            set
            {
                Customer_Class_GeSet.Customer_Name = value;
                NotifyPropertyChanged();
                ValidateProperty(value);

            }
        }
        [Display(Name = "Customer Email Id ")]
        [RegularExpression(@"^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$", ErrorMessage = "Incorrect Email Id.")]
        public string _Customer_Email_Id
        {
            get { return Customer_Class_GeSet.Email_Id; }
            set
            {
                Customer_Class_GeSet.Email_Id = value;
                NotifyPropertyChanged();
                ValidateProperty(value);

            }
        }


        ////public bool Registration_layout = true;
        //[Display(Name = "Customer Address")]
        //[Required]
        //[StringLength(maximumLength: 150, ErrorMessage = "Maximum 150 Char Required")]
        //public string _Address
        //{
        //    get { return Customer_Class_GeSet.Address; }
        //    set
        //    {
        //        Customer_Class_GeSet.Address = value;
        //        NotifyPropertyChanged();
        //        ValidateProperty(value);

        //    }
        //}
        public Command SubmitCommand { get; }
        private async Task Submit()
        {
            try
            {
                if (HasErrors)
                {
                    // Error message
                    ScrollToControlProperty(GetFirstInvalidPropertyName);
                }
                else
                {
                
                    if ( !string.IsNullOrEmpty(Customer_Class_GeSet.Contact_No) && !string.IsNullOrEmpty(Customer_Class_GeSet.Customer_Name) )
                    {
                        var Wait = UserDialogs.Instance.Loading("Wait...", null, null, true, MaskType.Black);
                        Wait.Show();
                        JObject result = await _IAllDataServices.CustomerRegistration(Customer_Class_GeSet);
                      
                       

                        if (result != null)
                        {
                            string type = result["Type"].ToString();
                            if (type == "1")
                            {
                                await App.Current.MainPage.DisplayAlert("Success!", (string)result["ResponseMessage"], "Ok");
                               
                                var ChackPriousPage = _INavigation.NavigationStack.Where(x => x.Title == "Customer Registration").FirstOrDefault();
                                if (ChackPriousPage != null)
                                {
                                   
                                    _INavigation.RemovePage(ChackPriousPage);

                                }
                                await _INavigation.PushAsync(new CustomerRegistrationPage());

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
                        //if (string.IsNullOrEmpty(Customer_Class_GeSet.Address))
                        //{
                        //    _Address = "";
                        //}
                        //else 
                        if (string.IsNullOrEmpty(Customer_Class_GeSet.Contact_No))
                        {
                            _Customer_Contect_No = "";
                        }
                        else if (string.IsNullOrEmpty(Customer_Class_GeSet.Customer_Name))
                        {
                            _Customer_Name = "";
                        }
                      
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
