using Acr.UserDialogs;
using Newtonsoft.Json;
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
    public class GoodReturnViewModel : ModelBase
    {
        private string ArticleNo = string.Empty;
        private bool ShowAndHideStack = false;
        private ObservableCollection<ArticleMaster_Class> ArticleMaster_Class_Data_List;

        public ICommand GetArticleNoCommand { get; }
        public Command GetScanCommand { get; }
        public ICommand SaveCommand { get; }
        public ICommand AddCommand { get; }
        public ICommand CancelCommand { get { return new Command(() => CancelTask()); } }


        public bool HideShowSaveButton = false;

        private ObservableCollection<ArticleMaster_Class> ArticleMasterClassList;
        public IAllDataServices _IAllDataServices;
        private ArticleMaster_Class _SelectArticleMasterClassList;
        //private ArticleMaster_Class Old_Article_Class_Data;
        public INavigation navigation { get; set; }
        public bool MainStackShowHide = true;
        public bool SubStackShowHide = false;
       
        public GoodReturnViewModel(INavigation _navigation)
        {
            navigation = _navigation;
            _IAllDataServices = new AllDataServices();

            ArticleMaster_Class_Data_List = new ObservableCollection<ArticleMaster_Class>();
            ArticleMasterClassList = new ObservableCollection<ArticleMaster_Class>();
            GetArticleNoCommand = new Command(async () => await CheckArticleNo());
            GetScanCommand = new Command(async () => await ScanArticleNo());
            AddCommand = new Command(async () => await Add());
            SaveCommand = new Command(async () => await Save());
             GetListAsync();
          
        }

        private async Task Save()
        {

            FranchiseGoodReturn_Class FranchiseGoodReturn_Class_Data = new FranchiseGoodReturn_Class();
            FranchiseGoodReturn_Class_Data.Actual_Qty = 0;

            ObservableCollection<FranchiseOrderReturnDetails_Class> od = new ObservableCollection<FranchiseOrderReturnDetails_Class>();

            ObservableCollection<ArticleMaster_Class> amcl = new ObservableCollection<ArticleMaster_Class>();
            foreach (var item in ArticleMasterClassList)
            {
                foreach (var Subitem in item.FranchiseStokeMaster_Class_List)
                {
                    if (Subitem.Insert_New_Qty > 0)
                    {
                        FranchiseOrderReturnDetails_Class fsl = new FranchiseOrderReturnDetails_Class();
                        FranchiseGoodReturn_Class_Data.Total_Amount = FranchiseGoodReturn_Class_Data.Total_Amount + (Subitem.Insert_New_Qty * Convert.ToDecimal(item.Selling_Price));
                        FranchiseGoodReturn_Class_Data.Total_Item = FranchiseGoodReturn_Class_Data.Total_Item + Subitem.Insert_New_Qty;
                        fsl.FranchiseStock_Id = Subitem.Stock_Id;
                        fsl.Quantity = Subitem.Insert_New_Qty;
                        od.Add(fsl);

                        ArticleMaster_Class amc = new ArticleMaster_Class();
                        amc.Article_No = item.Article_No;
                        amc.Article_PrimaryImage = item.Article_PrimaryImage;
                        amc.Article_SecondaryImage = item.Article_SecondaryImage;
                        CategoryMaster_Class cmc = new CategoryMaster_Class();
                        cmc.Category_Name = item.CategoryMaster_Class_Data.Category_Name + " - " + Subitem.SubCategoryMaster_Class_Data.SubCategory_Name;
                        amc.Selling_Price =Convert.ToDouble(item.Selling_Price);
                        amc.MRP = Subitem.Insert_New_Qty * Convert.ToDecimal(item.Selling_Price);
                        amc.CategoryMaster_Class_Data = cmc;
                        amc.Article_Id = Subitem.Insert_New_Qty;
                        amcl.Add(amc);
                    }
                }
            }
            FranchiseGoodReturn_Class_Data.goodreturnDetail = od;
            //   Franchise_Sell_Class_Data.FranchiseSellDetails_Class_List.Add(fscd);

            if (FranchiseGoodReturn_Class_Data.Total_Amount > 0)
            {
                await navigation.PushAsync(new GoodReturnDetailPage(FranchiseGoodReturn_Class_Data, amcl), true);
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Oops!", "Please Fill Qty Then Submit..", "Ok");
            }

        }

        private void CancelTask()
        {
            _MainStackShowHide = true;
            _SubStackShowHide = false;

            foreach (var st in SelectArticleMasterClassList.FranchiseStokeMaster_Class_List)
            {
                if (st.Quantity >= st.Insert_New_Qty)
                {

                }
                else
                {
                    st.Insert_New_Qty = 0;
                }
            }
        }

        private async Task Add()
        {
            bool Process = false;
            string Item_Name = string.Empty, Qty = string.Empty;
            foreach (var st in SelectArticleMasterClassList.FranchiseStokeMaster_Class_List)
            {
                if (st.Quantity >= st.Insert_New_Qty)
                {
                    Process = true;
                }
                else
                {
                    Process = false;
                    Item_Name = st.SubCategoryMaster_Class_Data.SubCategory_Name;
                    Qty = st.Insert_New_Qty.ToString();
                    break;
                }
            }

            if (Process == true)
            {
                _MainStackShowHide = true;
                _SubStackShowHide = false;

                //var indexof = _ArticleMaster_Class_List.IndexOf(Old_Article_Class_Data);
                //_ArticleMaster_Class_List.Remove(Old_Article_Class_Data);
                //_ArticleMaster_Class_List.Insert(indexof, SelectArticleMasterClassList);
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Oops!", "Enter quantity is more than available quantity. ( Item =" + Item_Name + " Qty=" + Qty + " not available ) ", "Ok");
            }
        }

        private async Task GetListAsync()
        {
            JObject result = await _IAllDataServices.GetFranchiseStock();

            if (result != null)
            {
                string type = result["Type"].ToString();
                if (type == "1")
                {
                    ArticleMaster_Class_Data_List = JsonConvert.DeserializeObject<ObservableCollection<ArticleMaster_Class>>(result["Result"].ToString());
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("Error!", (string)result["Result"]["ResponseMessage"], "Ok");
                }
            }
          
           
        }

        private async Task CheckArticleNo()
        {
            //  Is_Bussy_To_Get_Article_No = true;
            try
            {
                var Wait = UserDialogs.Instance.Loading("Wait...", Cancel(), "Cancel", true, MaskType.Black);
                Wait.Show();
                if (HasErrors)
                {

                    ScrollToControlProperty(GetFirstInvalidPropertyName);
                }
                else
                {
                    //Count Prasent list Article
                    //Method

                    var Art = ArticleMasterClassList.Where(x => x.Article_No == _Article_No).FirstOrDefault();
                    if (Art == null)
                    {
                        //Insert Artilcle Data in Prasent list and chek it
                        var check = ArticleMaster_Class_Data_List.Where(x => x.Article_No == _Article_No).FirstOrDefault();

                        if (check != null)
                        {
                            ArticleMasterClassList.Add(check);
                            _ArticleMaster_Class_List = ArticleMasterClassList;
                        }
                        else
                        {
                            await App.Current.MainPage.DisplayAlert("Oops!", "Aritical Not Found", "Ok");
                        }
                    }
                    else
                    {
                        await App.Current.MainPage.DisplayAlert("Oops!", "This Article No. Already Exists in List", "Ok");
                    }
                }
                Wait.Dispose();
            }
            catch (Exception ee)
            {
                await App.Current.MainPage.DisplayAlert("Error", ee.Message, "Ok");
            }

        }
        private async Task ScanArticleNo()
        {
            try
            {
                var Wait = UserDialogs.Instance.Loading("Wait...", Cancel(), "Cancel", true, MaskType.Black);
                Wait.Show();
                var scanResult = await Acr.BarCodes.BarCodes.Instance.Read();
                if (!scanResult.Success)
                {
                    await App.Current.MainPage.DisplayAlert("Alert ! ", "Sorry ! \n Failed to read the Barcode !", "Ok");
                }
                else
                {
                    _Article_No = scanResult.Code;
                    //await App.Current.MainPage.DisplayAlert("Scan Successful  ! ", String.Format("Barcode Format : {0} \n Barcode Value : {1}", scanResult.Format, scanResult.Code), "","Ok");

                }
                Wait.Dispose();
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
        public ObservableCollection<ArticleMaster_Class> _ArticleMaster_Class_List
        {
            get { return ArticleMasterClassList; }
            set
            {
                ArticleMasterClassList = value;
                NotifyPropertyChanged();
                if (value.Count > 0)
                {
                    _HideShowSaveButton = true;
                    _Show_And_Hide_Stack = true;

                }
            }
        }


        [Display(Name = "Product Article No.")]
        [Required]
        public string _Article_No
        {
            get { return ArticleNo; }
            set
            {
                ArticleNo = value;
                    NotifyPropertyChanged();
                    ValidateProperty(value);
                
            }
        }
        public bool _Show_And_Hide_Stack
        {
            get { return ShowAndHideStack; }
            set
            {
                ShowAndHideStack = value;
                NotifyPropertyChanged();
            }
        }
       


        public bool _HideShowSaveButton
        {
            get { return HideShowSaveButton; }
            set
            {
                HideShowSaveButton = value;
                NotifyPropertyChanged();

            }
        }
        public bool _MainStackShowHide
        {
            get { return MainStackShowHide; }
            set
            {
                MainStackShowHide = value;
                NotifyPropertyChanged();

            }
        }
        public bool _SubStackShowHide
        {
            get { return SubStackShowHide; }
            set
            {
                SubStackShowHide = value;
                NotifyPropertyChanged();

            }
        }
        public ArticleMaster_Class SelectArticleMasterClassList
        {
            get { return _SelectArticleMasterClassList; }
            set
            {
                _SelectArticleMasterClassList = value;
                NotifyPropertyChanged();
                if (value != null)
                {
                    _MainStackShowHide = false;
                    _SubStackShowHide = true;
                    // Old_Article_Class_Data = value;
                }
            }
        }

        public Command<ArticleMaster_Class> DeleteItemInListCommand
        {
            get
            {
                return new Command<ArticleMaster_Class>((ArtNo) =>
                {
                    var index = _ArticleMaster_Class_List.IndexOf(ArtNo);
                    _ArticleMaster_Class_List.RemoveAt(index);
                    if (_ArticleMaster_Class_List.Count > 0)
                    {
                        _HideShowSaveButton = true;
                    }
                    else
                    {
                        _HideShowSaveButton = false;
                    }
                });
            }

        }
















      






    }
}
