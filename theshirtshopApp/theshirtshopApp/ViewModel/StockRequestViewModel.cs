
using Acr.UserDialogs;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using theshirtshopApp.BAL;
using theshirtshopApp.DAL;
using theshirtshopApp.Interface;
using theshirtshopApp.Validation;
using theshirtshopApp.View;
using Xamarin.Forms;

namespace theshirtshopApp.ViewModel
{
   public class StockRequestViewModel : ModelBase
    {

        private FranchiseRequest_Class _FranchiseRequest_Class;
        private IAllDataServices _IAllDataServices;
        private INavigation _INavigation { get; set; }
        public StockRequestViewModel( INavigation navigation)
        {
            _INavigation = navigation;
            SendCommand = new Command(async () => await Send());
            this._FranchiseRequest_Class = new FranchiseRequest_Class();
            this._IAllDataServices = new AllDataServices();
        }
        [Display(Name = "Request Title")]
        [Required]
        [StringLength(maximumLength:30,ErrorMessage ="Title Maximum 30 Char")]
        public string Title
        {
            get { return _FranchiseRequest_Class.Title; }
            set
            {
                _FranchiseRequest_Class.Title = value;
                NotifyPropertyChanged();
                ValidateProperty(_FranchiseRequest_Class.Title);
                //  OnPropertChange(nameof(Required_Message));
            }
        }
        [Display(Name = "Request Description")]
        [Required]
        [StringLength(maximumLength: 150, ErrorMessage = "Description Maximum 150 Char")]
        public string Description
        {
            get { return _FranchiseRequest_Class.Description; }
            set
            {
                _FranchiseRequest_Class.Description = value;
                NotifyPropertyChanged();
                ValidateProperty(_FranchiseRequest_Class.Description);
                //   OnPropertChange(nameof(Required_Message));
            }
        }
        public Command SendCommand { get; }
        async Task Send()
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

                    if (!string.IsNullOrEmpty(_FranchiseRequest_Class.Title) && !string.IsNullOrEmpty(_FranchiseRequest_Class.Description))
                    {
                        var Wait = UserDialogs.Instance.Loading("Wait..", Cancel(), "Cancel", true, MaskType.Black);
                        Wait.Show();
                         JObject result = await _IAllDataServices.SendOrderRequest(_FranchiseRequest_Class);

                        if (result != null)
                        {
                            string type = result["Type"].ToString();
                            if (type == "1")
                            {
                                await App.Current.MainPage.DisplayAlert("Success!", (string)result["ResponseMessage"], "Ok");
                               
                                var ChackPriousPage = _INavigation.NavigationStack.Where(x => x.Title == "Stock Request").FirstOrDefault();

                                if (ChackPriousPage != null)
                                {

                                    _INavigation.RemovePage(ChackPriousPage);

                                }
                                await _INavigation.PushAsync(new StockRequestPage());

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
                       
                        Wait.Dispose();
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(_FranchiseRequest_Class.Title))
                        {
                            Title = "";
                        }
                        else if (string.IsNullOrEmpty(_FranchiseRequest_Class.Description))
                        {
                            Description = "";
                        }
                    }
                }
            }
            catch (Exception ee)
            {
                await App.Current.MainPage.DisplayAlert("Error", ee.Message, "Ok");
            }
        }

        private Action Cancel()
        {
            var cancelSrc = new CancellationTokenSource();
            return cancelSrc.Cancel;
        }
    }
}
