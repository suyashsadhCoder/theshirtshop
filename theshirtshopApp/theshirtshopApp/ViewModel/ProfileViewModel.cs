using Acr.UserDialogs;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using theshirtshopApp.BAL;
using theshirtshopApp.DAL;
using theshirtshopApp.Interface;
using theshirtshopApp.Validation;
using Xamarin.Forms;

namespace theshirtshopApp.ViewModel
{
  public class ProfileViewModel : ModelBase
    {
      
        private Profile_Class profile_Class_Data;
        private IAllDataServices IAllDataServices_data;
        private INavigation _navigation { get; set; }
        public ProfileViewModel(INavigation nav)
        {
            _navigation = nav;
            profile_Class_Data = new Profile_Class();
            IAllDataServices_data = new AllDataServices();
          getData();
          
        }
        public string _Photo
        {
            get { return profile_Class_Data.Photo_Path; }
            set { profile_Class_Data.Photo_Path = value;
                NotifyPropertyChanged();
            }
        }
        public string _ContectNo
        {
            get { return profile_Class_Data.Contect_No; }
            set
            {
                profile_Class_Data.Contect_No = value;
                NotifyPropertyChanged();
            }
        }
        public string _MailId
        {
            get { return profile_Class_Data.Mail_Id; }
            set
            {
                profile_Class_Data.Mail_Id = value;
                NotifyPropertyChanged();
            }
        }
        public string _Address
        {
            get { return profile_Class_Data.Address; }
            set
            {
                profile_Class_Data.Address = value;
                NotifyPropertyChanged();
            }
        }
        public string _Name
        {
            get { return profile_Class_Data.Name; }
            set
            {
                profile_Class_Data.Name = value;
                NotifyPropertyChanged();
            }
        }
        public string _Code
        {
            get { return profile_Class_Data.Code; }
            set
            {
                profile_Class_Data.Code = value;
                NotifyPropertyChanged();
            }
        }
        private static string Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes,0, base64EncodedBytes.Length);
        }
        private Action Cancel()
        {
            var cancelSrc = new CancellationTokenSource();
            return cancelSrc.Cancel;
        }
        private async void getData()
        {
            var Wait = UserDialogs.Instance.Loading("Wait...", Cancel(), "Cancel", true, MaskType.Black);
            Wait.Show();
            try
            {
                _Code = App.Code;

                _ContectNo = App.ContectNo;
                _Address = App.Address;
                _MailId = App.MailId;
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
                //            _Code = (string)result["Result"]["Franchise_Code"];

                //            _ContectNo = (string)result["Result"]["Franchise_PrimaryMobileNo"];
                //            _Address = (string)result["Result"]["Franchise_PermanentAddress"] + ',' + (string)result["Result"]["City_Name"] + ',' + (string)result["Result"]["State_Name"];
                //            _MailId = (string)result["Result"]["Franchise_EmailId"];
                //            _Name = (string)result["Result"]["Franchise_OwnerName"];
                //            _Photo = "http://www.theshirtshop.in/TemplateContent/Admin/images/FranchiseImages/FranchisePhoto/" + (string)result["Result"]["Franchise_Photo"];

                //            App.Code = (string)result["Result"]["Franchise_Code"];
                //            App.ContectNo = (string)result["Result"]["Franchise_PrimaryMobileNo"];
                //            App.Address = (string)result["Result"]["Franchise_PermanentAddress"] + ',' + (string)result["Result"]["City_Name"] + ',' + (string)result["Result"]["State_Name"];
                //            App.MailId = (string)result["Result"]["Franchise_EmailId"];
                //            App.Name = (string)result["Result"]["Franchise_OwnerName"];
                //            App.Photo = "http://www.theshirtshop.in/TemplateContent/Admin/images/FranchiseImages/FranchisePhoto/" + (string)result["Result"]["Franchise_Photo"];
                //        }
                //        else
                //        {
                //            _Code = (string)result["Result"]["Emp_Code"];
                //            _ContectNo = (string)result["Result"]["Emp_PrimaryMobileNo"];
                //            _Address = (string)result["Result"]["Emp_PermanetAddress"] + ',' + (string)result["Result"]["City_Name"] + ',' + (string)result["Result"]["State_Name"];
                //            _MailId = (string)result["Result"]["Emp_EmailId"];
                //            _Name = (string)result["Result"]["Emp_FirstName"] + " " + (string)result["Result"]["Emp_LastName"];
                //            _Photo = "http://www.theshirtshop.in/TemplateContent/Admin/images/EmployeeImages/EmployeePhoto/" + (string)result["Result"]["Emp_Photo"];

                //            App.Code = (string)result["Result"]["Emp_Code"];
                //            App.ContectNo = (string)result["Result"]["Emp_PrimaryMobileNo"];
                //            App.Address = (string)result["Result"]["Emp_PermanetAddress"] + ',' + (string)result["Result"]["City_Name"] + ',' + (string)result["Result"]["State_Name"];
                //            App.MailId = (string)result["Result"]["Emp_EmailId"];
                //            App.Name = (string)result["Result"]["Emp_FirstName"] + " " + (string)result["Result"]["Emp_LastName"];
                //            App.Photo = "http://www.theshirtshop.in/TemplateContent/Admin/images/EmployeeImages/EmployeePhoto/" + (string)result["Result"]["Emp_Photo"];
                //        }

                //    }
                //    else if (type == "101")
                //    {

                //        var output = await UserDialogs.Instance.ConfirmAsync(new ConfirmConfig
                //        {
                //            Message = "Application new version available at play store you need to update new version of app.",
                //            OkText = "Yes",
                //            CancelText = "No",
                //            Title = "New Version"

                //        });

                //        if (output)
                //        {
                //            Device.OpenUri(new Uri("https://play.google.com/store/apps/details?id=com.companyname.theshirtshopApp"));
                //            if (Application.Current.Properties.ContainsKey("Key"))
                //            {
                //                Application.Current.Properties["Key"] = null;
                //                Application.Current.Properties["UserName"] = null;
                //                Application.Current.Properties["Password"] = null;
                //                Application.Current.Properties["OtherId"] = null;
                //                Application.Current.Properties.Remove("Key");
                //                Application.Current.Properties.Remove("UserName");
                //                Application.Current.Properties.Remove("Password");
                //                Application.Current.Properties.Remove("OtherId");
                //            }

                //            App.Current.MainPage = new MainPage();
                //        }
                //        else
                //        {

                //            if (Application.Current.Properties.ContainsKey("Key"))
                //            {
                //                Application.Current.Properties["Key"] = null;
                //                Application.Current.Properties["UserName"] = null;
                //                Application.Current.Properties["Password"] = null;
                //                Application.Current.Properties["OtherId"] = null;
                //                Application.Current.Properties.Remove("Key");
                //                Application.Current.Properties.Remove("UserName");
                //                Application.Current.Properties.Remove("Password");
                //                Application.Current.Properties.Remove("OtherId");
                //            }

                //            App.Current.MainPage = new MainPage();
                //        }

                //    }
                //    else if(!string.IsNullOrEmpty(type))
                //    {

                //        if (Application.Current.Properties.ContainsKey("Key"))
                //        {
                //            Application.Current.Properties["Key"] = null;
                //            Application.Current.Properties["UserName"] = null;
                //            Application.Current.Properties["Password"] = null;
                //            Application.Current.Properties["OtherId"] = null;
                //            Application.Current.Properties.Remove("Key");
                //            Application.Current.Properties.Remove("UserName");
                //            Application.Current.Properties.Remove("Password");
                //            Application.Current.Properties.Remove("OtherId");
                //        }
                //        App.Current.MainPage = new MainPage();
                //    }
                //}
                //else
                //{

                //    await App.Current.MainPage.DisplayAlert("Oops!", "Please Refresh Page And try Again....", "Ok");
                //}
                Wait.Hide();
            }
            catch (Exception ex)
            {
             
            }
           
        }
    }
}
