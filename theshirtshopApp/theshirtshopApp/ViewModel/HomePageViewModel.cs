using Acr.UserDialogs;
using Bal_TheShirtShop;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using theshirtshopApp.DAL;
using theshirtshopApp.Interface;
using theshirtshopApp.Validation;
using Xamarin.Forms;

namespace theshirtshopApp.View
{
    public class HomePageViewModel :ModelBase
    {
        private IAllDataServices _IAllDataServices;
        private List<FranchiseAchieveRewardDetail_Class> FranchiseAchieveRewardDetail_Class_Date { get; set; }
        private decimal total = 0;
        private INavigation _navigation { get; set; }
        public HomePageViewModel(INavigation nv)
        {
            _navigation = nv;
            _IAllDataServices = new AllDataServices();
            PlayWayCommand = new Command(() =>  Way());
            PlayMp3Command = new Command(() =>  Mp());

            FranchiseAchieveRewardDetail_Class_Date = new List<FranchiseAchieveRewardDetail_Class>() {

               new  FranchiseAchieveRewardDetail_Class {
                   Purchase_Point=0,
                   Totla_Amount=0
               },
               new  FranchiseAchieveRewardDetail_Class {
                   Purchase_Point=0,
                   Totla_Amount=0
               },
               new  FranchiseAchieveRewardDetail_Class {
                   Purchase_Point=0,
                   Totla_Amount=0
               }
            };
           // Sound();
             GetRcord();

           
           // GetRcord();
        }

        private async void GetRcord()
        {
            var Wait = UserDialogs.Instance.Loading("Wait..", Cancel(), "Cancel", true, MaskType.Black);
            Wait.Show();
            try
            {
             
                var version = DependencyService.Get<IAppVersionProvider>();
                var versionString = version.AppVersion;
                JObject result = await _IAllDataServices.GetHomeRecord(versionString);


                if (result != null)
                {
                   
                    string type = result["Type"].ToString();
                    if (type == "Success")
                    {

                        if (!string.IsNullOrEmpty((string)result["ResponseMessage"]))
                        {
                            await App.Current.MainPage.DisplayAlert("Opps!", (string)result["ResponseMessage"], "Ok");

                        }

                        var data = JsonConvert.DeserializeObject<List<FranchiseAchieveRewardDetail_Class>>(result["Result"].ToString());

                        if (data != null && data.Count != 0)
                        {
                            for (int i = 0; i < data.Count; i++)
                            {
                                FranchiseAchieveRewardDetail_Class_Date[i].Totla_Amount = data[i].Totla_Amount;
                                FranchiseAchieveRewardDetail_Class_Date[i].Purchase_Point = data[i].Purchase_Point;
                                total = total + data[i].Totla_Amount;
                            }
                            _FranchiseAchieveRewardDetail_Class_Date = FranchiseAchieveRewardDetail_Class_Date;
                            Total = total;
                        }
                        else
                        {
                            _FranchiseAchieveRewardDetail_Class_Date = FranchiseAchieveRewardDetail_Class_Date;
                            Total = total;
                        }
                      
                    }
                    else if (type == "NotFound")
                    {
                        if (!string.IsNullOrEmpty((string)result["ResponseMessage"]))
                        {
                            await App.Current.MainPage.DisplayAlert("Opps!", (string)result["ResponseMessage"], "Ok");

                        }
                        _FranchiseAchieveRewardDetail_Class_Date = FranchiseAchieveRewardDetail_Class_Date;
                        Total = total;
                      
                    }
                    else if (type == "NewVersion")
                    {
                        Wait.Hide();
                        var output = await UserDialogs.Instance.ConfirmAsync(new ConfirmConfig
                        {
                            Message = "Application new version available at play store you need to update new version of app.",
                            OkText = "Yes",
                            CancelText = "No",
                            Title = "New Version"

                        });

                        if (output)
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
                            _FranchiseAchieveRewardDetail_Class_Date = FranchiseAchieveRewardDetail_Class_Date;
                            Total = total;
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
                            //if (_navigation.ModalStack.Count > 0)
                            //{
                            //    await _navigation.PopModalAsync();
                            //}

                            //await _navigation.PushModalAsync(new MainPage());
                            App.Current.MainPage = new MainPage();
                        }
                    }
                    else if (!string.IsNullOrEmpty(type))
                    {
                     
                        _FranchiseAchieveRewardDetail_Class_Date = FranchiseAchieveRewardDetail_Class_Date;
                        Total = total;
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

                Wait.Hide();

            }
            catch (Exception ex)
            {
              

            }
          
        }

        private Action Cancel()
        {
            var cancelSrc = new CancellationTokenSource();
            return cancelSrc.Cancel;
        }

        private void Sound()
        {
            DependencyService.Get<IAudio>().PlayMp3File("coin.mp3");
    
        }

        private void Mp()
        {
            DependencyService.Get<IAudio>().PlayWavFile("ding_persevy.wav");
        }

        private void Way()
        {
            DependencyService.Get<IAudio>().PlayMp3File("test.mp3");
        }

        public Command PlayWayCommand { get; }
        public Command PlayMp3Command { get; }

        public List<FranchiseAchieveRewardDetail_Class> _FranchiseAchieveRewardDetail_Class_Date
        {
            get{ return FranchiseAchieveRewardDetail_Class_Date; }
            set { FranchiseAchieveRewardDetail_Class_Date = value;
                NotifyPropertyChanged();
            }
        }
        public decimal Total
        {
            get { return total; }
            set
            {
                total = value;
                NotifyPropertyChanged();
            }
        }

    }
}