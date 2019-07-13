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
    public class FeedbackPageViewModel : ModelBase
    {
        private FranchiseFeedback_Class FranchiseFeedback_Class_Data = new FranchiseFeedback_Class();
        private IAllDataServices _IAllDataServices;
        public Command SendCommand { get; }
        private INavigation _INavigation { get; set; }
        public FeedbackPageViewModel(INavigation navigation)
        {
            _INavigation = navigation;
            SendCommand = new Command(async () => await Send());
            this._IAllDataServices = new AllDataServices();
        }
        [Display(Name = "Title")]
        [Required]
        [StringLength(maximumLength:20,ErrorMessage ="Maximum 20 char required.")]
        public string Title
        {
            get { return FranchiseFeedback_Class_Data._Title; }
            set
            {
                FranchiseFeedback_Class_Data._Title = value;
                NotifyPropertyChanged();
                ValidateProperty(value);
            }
        }
        [Display(Name = "Message")]
        [Required]
        [StringLength(maximumLength:150,MinimumLength =30,ErrorMessage ="Maximum 150 char and Minimum 30 char Required.")]
        public string Description
        {
            get { return FranchiseFeedback_Class_Data._Description; }
            set
            {
                FranchiseFeedback_Class_Data._Description = value;
                NotifyPropertyChanged();
                ValidateProperty(value);
            }
        }
        async Task Send()
        {
            try
            {
                if (HasErrors)
                {
                    ScrollToControlProperty(GetFirstInvalidPropertyName);
                }
                else
                {
                    if (!string.IsNullOrEmpty(FranchiseFeedback_Class_Data._Title) && !string.IsNullOrEmpty(FranchiseFeedback_Class_Data._Description))
                    {
                        var Wait = UserDialogs.Instance.Loading("Wait...", null, null, true, MaskType.Black);
                        Wait.Show();
                        JObject result = await _IAllDataServices.SaveFeedBack(FranchiseFeedback_Class_Data);
                       


                        if (result != null)
                        {
                            string type = result["Type"].ToString();
                            if (type == "1")
                            {
                                await App.Current.MainPage.DisplayAlert("Success!", (string)result["ResponseMessage"], "Ok");
                                
                               
                                var ChackPriousPage = _INavigation.NavigationStack.Where(x => x.Title == "Feedback").FirstOrDefault();

                                if (ChackPriousPage != null)
                                {

                                    _INavigation.RemovePage(ChackPriousPage);

                                }
                                await _INavigation.PushAsync(new FeedbackPage());

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
                        if (string.IsNullOrEmpty(FranchiseFeedback_Class_Data._Title))
                        {
                            Title = "";
                        }
                        else if (string.IsNullOrEmpty(FranchiseFeedback_Class_Data._Description))
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
    }
}
