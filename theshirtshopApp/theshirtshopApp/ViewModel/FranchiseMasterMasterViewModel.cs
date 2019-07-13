using Acr.UserDialogs;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    class FranchiseMasterMasterViewModel : ModelBase
    {
        public ObservableCollection<FranchiseMasterMenuItem> MenuItems { get; set; }
        private IAllDataServices IAllDataServices_data;
        public Command LogoutComand { get; set; }
        private INavigation navigation { get; set; }
        private string Count = "0";
        private ObservableCollection<Notification_Class> _listData;

        public FranchiseMasterMasterViewModel(INavigation nov)
        {
            navigation = nov;

            LogoutComand = new Command(async () => await Logout());
            IAllDataServices_data = new AllDataServices();
            _listData = new ObservableCollection<Notification_Class>();
            Task task = Task.Run(async () =>
            {
                JObject result = await IAllDataServices_data.GetAllNotiication();
                if (result != null)
                {
                    string type = result["Type"].ToString();

                    if (type == "1")
                    {
                        _listData = JsonConvert.DeserializeObject<ObservableCollection<Notification_Class>>(result["Result"].ToString());
                        Count = _listData.Count().ToString();
                        MenuItems = new ObservableCollection<FranchiseMasterMenuItem>(new[]
         {
                 new FranchiseMasterMenuItem {  Title = "Notification", Icon = "menunoti", TargetType = typeof(NotificationPage),Count=Count},
                    new FranchiseMasterMenuItem {  Title = "Home", Icon = "menuhome", TargetType = typeof(HomePage)},
                     new FranchiseMasterMenuItem {  Title = "Amount Detail/Payment gateway", Icon = "menuatm", TargetType = typeof(FranchiseAdminPaymentDetail)},
                     //new FranchiseMasterMenuItem {  Title = "Customer Registration", Icon = "customereegistration.png", TargetType = typeof(CustomerRegistrationPage)},
                      new FranchiseMasterMenuItem {  Title = "Sell", Icon = "menusell", TargetType = typeof(SellPage)},
                      new FranchiseMasterMenuItem {  Title = "Stock", Icon = "menustock", TargetType = typeof(StockDetailPage)},
                       new FranchiseMasterMenuItem {  Title = "New Generated Order", Icon = "menuneworder", TargetType = typeof(UpComingStockRequestPage)},
                      new FranchiseMasterMenuItem {  Title = "Send Request", Icon = "menusendreq", TargetType = typeof(StockRequestPage)}
                      ,
                      new FranchiseMasterMenuItem {  Title = "All Send Request", Icon = "menusendreq", TargetType = typeof(AllStockRequestPage)},

                    new FranchiseMasterMenuItem {  Title = "Change Password", Icon = "menucngpwd", TargetType = typeof(ChangePasswordPage)},

                    new FranchiseMasterMenuItem {  Title = "Contact Us", Icon = "menucontact", TargetType = typeof(ContactUsPage)},
                    new FranchiseMasterMenuItem {  Title = "Create Refund/Return", Icon = "menureturnref", TargetType = typeof(RefundReturnPage)},
                    new FranchiseMasterMenuItem {  Title = "View Refund/Return", Icon = "menuview", TargetType = typeof(ViewOnlyReturnRefund)},

                    new FranchiseMasterMenuItem {  Title = "Goods Return", Icon = "menugoods", TargetType = typeof(GoodReturnPage)},
                     new FranchiseMasterMenuItem {  Title = "All Goods Return", Icon = "menugoods", TargetType = typeof(AllGoodsReturnPage)},
                    new FranchiseMasterMenuItem {  Title = "Invoice", Icon = "menuinvoice", TargetType = typeof(InvoicePage)},
                    new FranchiseMasterMenuItem {  Title = "Ledger", Icon = "menuledger", TargetType = typeof(LedgerPage)},
                    new FranchiseMasterMenuItem {  Title = "Profile", Icon = "menuprofile", TargetType = typeof(ProfilePage)},
                    new FranchiseMasterMenuItem {  Title = "Referral", Icon = "menureferral", TargetType = typeof(ReferralPage)},
                     new FranchiseMasterMenuItem {  Title = "FeedBack", Icon = "menufeedback", TargetType = typeof(FeedbackPage)},

                       new FranchiseMasterMenuItem {  Title = "FAQs", Icon = "menufaq", TargetType =null,Url="http://www.theshirtshop.in/FAQ"},
                         new FranchiseMasterMenuItem {  Title = "About Us", Icon = "menuabout_us", TargetType = null,Url="http://www.theshirtshop.in/about"},
                           new FranchiseMasterMenuItem {  Title = "Terms of Use", Icon = "menuterms_of_use", TargetType = null,Url="http://www.theshirtshop.in/terms-of-use"}


                });
                    }
                    else
                    {

                        // UserDialogs.Instance.Alert( (string)result["ResponseMessage"], "Error!", "Ok");
                        // await App.Current.MainPage.DisplayAlert("Error!", (string)result["ResponseMessage"], "Ok");
                    }
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("Oops!", "Please Refresh Page And try Again....", "Ok");
                }


            });
            task.Wait();


            getData();

        }

        private async Task Logout()
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
            for (int i = 0; i < navigation.ModalStack.Count; i++)
            {
                await navigation.PopModalAsync();
            }
            await navigation.PushModalAsync(new MainPage());
        }


        public string _Photo
        {
            get { return Photo_Path; }
            set
            {
                Photo_Path = value;
                NotifyPropertyChanged();
            }
        }
        public string _Name
        {
            get { return Name; }
            set
            {
                Name = value;
                NotifyPropertyChanged();
            }
        }

        public string Photo_Path { get; set; }
        public string Name { get; set; }

        private static string Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes, 0, base64EncodedBytes.Length);
        }
        private async Task getData()
        {
            try
            {
                _Name = App.Name;
                _Photo = App.Photo;
                //var version = DependencyService.Get<IAppVersionProvider>();
                //var versionString = version.AppVersion;
                //JObject result = await IAllDataServices_data.GetProfile(versionString);

                //if (result != null)
                //{

                //    string type = result["Type"].ToString();
                //    if (type == "1")
                //    {
                //        string[] key = Base64Decode(Application.Current.Properties["Key"].ToString()).Split('-');
                //        if (key[1].ToString() == "1")
                //        {

                //            _Name = (string)result["Result"]["Franchise_OwnerName"];
                //            _Photo = "http://www.theshirtshop.in/TemplateContent/Admin/images/FranchiseImages/FranchisePhoto/" + (string)result["Result"]["Franchise_Photo"];


                //        }
                //        else 
                //        {

                //            _Name = (string)result["Result"]["Emp_FirstName"] + " " + (string)result["Result"]["Emp_LastName"];
                //            _Photo = "http://www.theshirtshop.in/TemplateContent/Admin/images/EmployeeImages/EmployeePhoto/" + (string)result["Result"]["Emp_Photo"];
                //        }

                //    }
                //    else
                //    {
                //        // UserDialogs.Instance.Alert((string)result["ResponseMessage"], "Error!", "Ok");
                //    }
                //}
                //else
                //{
                //    await App.Current.MainPage.DisplayAlert("Oops!", "Please Refresh Page And try Again....", "Ok");
                //}
            }
            catch (Exception ex)
            {

               
            }
           
        }
    }
}
