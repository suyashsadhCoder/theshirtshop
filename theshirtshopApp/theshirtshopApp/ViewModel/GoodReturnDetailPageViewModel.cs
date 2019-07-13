using Acr.UserDialogs;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using theshirtshopApp.BAL;
using theshirtshopApp.DAL;
using theshirtshopApp.Interface;
using theshirtshopApp.Validation;
using theshirtshopApp.View;
using Xamarin.Forms;

namespace theshirtshopApp.ViewModel
{
   public class GoodReturnDetailPageViewModel :ModelBase
    {


        public FranchiseGoodReturn_Class FranchiseGoodReturn_Class_Data { get; set; }
        public ObservableCollection<ArticleMaster_Class> ArticleMaster_Class_List { get; set; }
        public ICommand SaveCommand { get; }

        public IAllDataServices _IAllDataServices;
        public INavigation _navigation { get; set; }
        public GoodReturnDetailPageViewModel(FranchiseGoodReturn_Class fs, ObservableCollection<ArticleMaster_Class> ac , INavigation navigation)
        {
            _navigation = navigation;
            FranchiseGoodReturn_Class_Data = fs;
            ArticleMaster_Class_List = ac;
            SaveCommand = new Command(async () => await SaveAsync());
            _IAllDataServices = new AllDataServices();
        }

        private async Task SaveAsync()
        {
            var Wait = UserDialogs.Instance.Loading("Wait...", Cancel(), "Cancel", true, MaskType.Black);
            Wait.Show();
            if (HasErrors)
            {
                // Error message
                ScrollToControlProperty(GetFirstInvalidPropertyName);
            }
            else
            {
                if (FranchiseGoodReturn_Class_Data.goodreturnDetail.Count > 0)
                {
                    if (!string.IsNullOrEmpty(_Mod_Of_Return) && !string.IsNullOrEmpty(_Remark))
                    {
                  

                        FranchiseGoodReturn_Class_Data.Mod_Of_Return = _Mod_Of_Return;
                        FranchiseGoodReturn_Class_Data.Remark = _Remark;
                        FranchiseGoodReturn_Class_Data._FranchiseStokeMaster_Class_List = null;
                 
                        foreach (var item in FranchiseGoodReturn_Class_Data.goodreturnDetail)
                        {
                            item.ArticleMaster_Class_Data = null;
                            item._FranchiseStokeMaster_Class_List = null;
                            item.CategoryMaster_Class_Data = null;
                        }

                        JObject result = await _IAllDataServices.SaveGoodReturn(FranchiseGoodReturn_Class_Data);

                        if (result != null)
                        {
                            string type = result["Type"].ToString();
                            if (type == "1")
                            {
                                await App.Current.MainPage.DisplayAlert("Success!", (string)result["ResponseMessage"], "Ok");


                                var ChackPriousPage = _navigation.NavigationStack.Where(x => x.Title == "Detail").FirstOrDefault();

                                if (ChackPriousPage != null)
                                {

                                    _navigation.RemovePage(ChackPriousPage);

                                }
                                //await _navigation.PopAsync();
                                var ChackSecondPriousPage = _navigation.NavigationStack.Where(x => x.Title == "Goods Return").FirstOrDefault();

                                if (ChackSecondPriousPage != null)
                                {

                                    _navigation.RemovePage(ChackSecondPriousPage);

                                }
                                await _navigation.PushAsync(new GoodReturnPage());
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
                     
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(_Mod_Of_Return))
                        {
                            _Mod_Of_Return = "";
                        }
                        else if (string.IsNullOrEmpty(_Remark))
                        {
                            _Remark = "";
                        }
                    }
                    
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("Oops!", "Please Fill Qty Then Send OTP", "Ok");
                }
            }
            Wait.Dispose();
        }

       

        private Action Cancel()
        {
            var cancelSrc = new CancellationTokenSource();
            return cancelSrc.Cancel;
        }
       [Required]
       [StringLength(maximumLength:30,ErrorMessage ="Fill Only Maximum 30 Char")]
        public string _Mod_Of_Return
        {
            get { return ModOfReturn; }
            set { ModOfReturn = value;
                NotifyPropertyChanged();
                ValidateProperty(value);
            }

        }

        [Required]
        [StringLength(maximumLength: 150, ErrorMessage = "Fill Only Maximum 150 Char")]
        public string _Remark
        {
            get { return Remark; }
            set
            {
                Remark = value;
                NotifyPropertyChanged();
                ValidateProperty(value);
            }

        }
        private string ModOfReturn;
        private string Remark;
    }
}
