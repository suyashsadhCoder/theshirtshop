using Acr.UserDialogs;
using ImageCircle.Forms.Plugin.Abstractions;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using theshirtshopApp.DAL;
using theshirtshopApp.Interface;
using theshirtshopApp.Validation;
using theshirtshopApp.View;
using theshirtshopApp.ViewModel;
using Xamarin.Forms;
using static Android.Media.Audiofx.BassBoost;
using static Java.Text.Normalizer;

namespace theshirtshopApp
{
    public partial class App : Application
    {
      
        public static double ScreenWidth;
        public static double ScreenHeight;
        public static int exitcount { get; set; }
        public static string Code;
        public static string ContectNo;
        public static string Address;
        public static string MailId;
        public static string Name;
        public static string Photo;
        public static bool isBusy;
        private IAllDataServices IAllDataServices_data;
        public App()
        {
            try
            {
            var wait=    UserDialogs.Instance.Loading(null, null, null, true, MaskType.Black);
                wait.Show();
                exitcount = 0;
                Permissions ps = new Permissions();
                ps.CheckGpsAsync();
                IAllDataServices_data = new AllDataServices();

                InitializeComponent();
                if (Application.Current.Properties.ContainsKey("Key"))
                {
                   Task ts = Task.Run(async () => {
                        var version = DependencyService.Get<IAppVersionProvider>();
                        var versionString = version.AppVersion;


                        JObject output =await  IAllDataServices_data.GetProfile(versionString);

                        if (output != null)
                        {
                            string Success = output["Type"].ToString();
                            if (Success == "1")
                            {

                                string[] key = Base64Decode(Application.Current.Properties["Key"].ToString()).Split('-');
                                if (key[1].ToString() == "1")
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

                    });
                    ts.Wait();
                    if (!string.IsNullOrEmpty(Application.Current.Properties["Key"].ToString()))
                    {

                        string[] TypeId = Base64Decode(Application.Current.Properties["Key"].ToString()).Split('-');

                        if (TypeId[1].ToString() == "1")
                        {

                            MainPage = new FranchiseMaster();
                        }
                        else if (TypeId[1].ToString() == "2")
                        {
                            MainPage = new EmployeeFranchiseMaster();
                        }
                        else if (TypeId[1].ToString() == "3")
                        {
                            MainPage = new EmployeeRetailerMaster();
                        }
                        else if (TypeId[1].ToString() == "4")
                        {
                            //Both
                            MainPage = new EmployeeFranchiseWithRetailerMaster();
                        }
                        else
                        {
                            MainPage = new MainPage();
                        }
                    }
                    else
                    {
                        
                        MainPage = new MainPage();
                    }
                }
                else
                {
                    MainPage = new MainPage();

                }
                wait.Hide();
            }
            catch (Exception ee)
            {
                Application.Current.Properties.Remove("Key");
                Application.Current.Properties.Remove("UserName");
                Application.Current.Properties.Remove("Password");
                Application.Current.Properties.Remove("OtherId");

              
                 MainPage = new MainPage();
            }
            //MainPage = new MainPage();
        }

        private static string Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes, 0, base64EncodedBytes.Length);
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
        public static bool BackCommand()
        {
            if (Application.Current.Properties.ContainsKey("Key"))
            {
                if (!string.IsNullOrEmpty(Application.Current.Properties["Key"].ToString()))
                {
                   // int title = App.Current.MainPage.Navigation.ModalStack.Count;
                   //if (title > 0)
                   // {
                        return true;
                   // }
                   // else
                   // {
                       // exitcount++;
                      //  return false;
                   // }
                   
                }
                else
                {
                    exitcount++;
                    return false;
                   
                }
            }
            else
            {
               
                exitcount++;
                return false;
               
            }
        }
      
    }
}
