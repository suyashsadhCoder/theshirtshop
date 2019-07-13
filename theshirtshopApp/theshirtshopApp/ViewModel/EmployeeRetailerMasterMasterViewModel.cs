using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using theshirtshopApp.DAL;
using theshirtshopApp.Interface;
using theshirtshopApp.Validation;
using theshirtshopApp.View;
using Xamarin.Forms;

namespace theshirtshopApp.ViewModel
{
   public class EmployeeRetailerMasterMasterViewModel : ModelBase
    {
        public ObservableCollection<EmployeeRetailerMasterMenuItem> MenuItems { get; set; }
        private IAllDataServices IAllDataServices_data;
        public Command LogoutComand { get; set; }
        private INavigation navigation { get; set; }
       
        public EmployeeRetailerMasterMasterViewModel(INavigation nov)
        {
            navigation = nov;
            LogoutComand = new Command(async () => await Logout());
            MenuItems = new ObservableCollection<EmployeeRetailerMasterMenuItem>(new[]
            {
                    new EmployeeRetailerMasterMenuItem {  Title = "Change Password", Icon = "changepass.png", TargetType = typeof(ChangePasswordPage)},
                    new EmployeeRetailerMasterMenuItem {  Title = "Contact Us", Icon = "contactus.png", TargetType = typeof(ContactUsPage)},
                   
                    new EmployeeRetailerMasterMenuItem {  Title = "Profile", Icon = "editprofile.png", TargetType =typeof(ProfilePage)},
                     new EmployeeRetailerMasterMenuItem {  Title = "Retailer Registration", Icon = "retailerregistration.png", TargetType =typeof(RetailerRegistrationPage)},
                        new EmployeeRetailerMasterMenuItem {  Title = "Retailer Order", Icon = "retailerorder.png", TargetType = typeof(RetailerOrderPage)},
                           new EmployeeRetailerMasterMenuItem {  Title = "Cash Collection", Icon = "cashcollection.png", TargetType = typeof(CashCollectionPage)},
                             new EmployeeRetailerMasterMenuItem {  Title = "About Us", Icon = "aboutus.png", TargetType = null,Url="http://www.theshirtshop.in/about"}
                });
            IAllDataServices_data = new AllDataServices();
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

        public string Photo_Path { get;  set; }
        public string Name { get;  set; }

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
                //            _Photo = "http://www.theshirtshop.in/TemplateContent/images/FranchiseImages/FranchisePhoto/" + (string)result["Result"]["Franchise_Photo"];


                //        }
                //        else 
                //        {

                //            _Name = (string)result["Result"]["Emp_FirstName"] + " " + (string)result["Result"]["Emp_LastName"];
                //            _Photo = "http://www.theshirtshop.in/TemplateContent/Admin/images/EmployeeImages/EmployeePhoto/" + (string)result["Result"]["Emp_Photo"];
                //        }

                //    }
                //    else
                //    {
                //        //  await App.Current.MainPage.DisplayAlert("Error!", (string)result["ResponseMessage"], "Ok");
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
