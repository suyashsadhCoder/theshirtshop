using Acr.UserDialogs;
using Java.Util;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
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
    public class SellViewModel : ModelBase
    {
        string type1;
        private string GetBtnText = "Get";
        private bool TextEnable = true;
        private string ArticleNo = string.Empty;
        private bool ShowAndHideStack = false;
        private Customer_Class Customer_Class;

        private bool _Registration_layout = false;



        public Customer_Class CustomerClassToBind {

            get {
                return Customer_Class;

            }
            set {
                Customer_Class = value;
                NotifyPropertyChanged();
            }

        }
        
        private ObservableCollection<ArticleMaster_Class> ArticleMaster_Class_Data_List;

        public ICommand GetContectNoCommand { get; }
        public ICommand GetArticleNoCommand { get; }
        public Command GetScanCommand { get; }
        public ICommand SaveCommand { get; }
        public ICommand AddCommand { get; }
        public ICommand CancelCommand { get { return new Command(() => CancelTask()); } }


        public bool HideShowSaveButton = false;

        private FranchiseSell_Class Franchise_Sell_Class_Data { get; set; }
        private ObservableCollection<ArticleMaster_Class> ArticleMasterClassList;
        public IAllDataServices _IAllDataServices;
        private ArticleMaster_Class _SelectArticleMasterClassList;
        //private ArticleMaster_Class Old_Article_Class_Data;
        public INavigation navigation { get; set; }
        public bool MainStackShowHide = true;
        public bool SubStackShowHide = false;
        


        public SellViewModel(INavigation _navigation)
        {
            navigation = _navigation;
            App.isBusy = false;
            ArticleMaster_Class_Data_List = new ObservableCollection<ArticleMaster_Class>();
            this.Customer_Class = new Customer_Class();
            GetContectNoCommand = new Command(async () => await CheckContectNo());
            GetArticleNoCommand = new Command(async () => await CheckArticleNo());
            GetScanCommand = new Command(async () => await ScanArticleNo());
            AddCommand = new Command(async () => await Add());
          //  CancelCommand = new Command(  CancelTask());
            SaveCommand = new Command(async () => await Save());
            CustomerClassToBind = new Customer_Class();
            _IAllDataServices = new AllDataServices();
            Franchise_Sell_Class_Data = new FranchiseSell_Class();
            ArticleMasterClassList = new ObservableCollection<ArticleMaster_Class>();
             GetListAsync();
            
        }

        private async Task Save()
        {
            if (!App.isBusy)
            {
                // ObservableCollection<FranchiseSellDetails_Class> fscd = new ObservableCollection<FranchiseSellDetails_Class>();
                App.isBusy = true;
                FranchiseSell_Class Franchise_Sell_Class = new FranchiseSell_Class();
                Franchise_Sell_Class.Customer_Id = Franchise_Sell_Class_Data.Customer_Id;

                var Wait = UserDialogs.Instance.Loading("Wait..", null, null, true, MaskType.Black);
                Wait.Show();

                if (Franchise_Sell_Class.Customer_Id > 0)
                {

                    Franchise_Sell_Class.Customer_Class_Data = Franchise_Sell_Class_Data.Customer_Class_Data;
                    foreach (var item in ArticleMasterClassList)
                    {
                        foreach (var Subitem in item.FranchiseStokeMaster_Class_List)
                        {
                            if (Subitem.Insert_New_Qty > 0)
                            {
                                FranchiseSellDetails_Class fsl = new FranchiseSellDetails_Class();
                                Franchise_Sell_Class.Total_Amount = Franchise_Sell_Class.Total_Amount + (Subitem.Insert_New_Qty * item.MRP);
                                fsl.SubCategory_Id = Subitem.SubCategoryMaster_Class_Data.SubCategory_Id;
                                fsl.Article_Id = item.Article_Id;
                                fsl.MRP = item.MRP;
                                fsl.Quantity = Subitem.Insert_New_Qty;
                                StockMaster_Class smc = new StockMaster_Class();
                                smc.Stock_Id = Subitem.Stock_Id;
                                fsl.FranchiseStokeMaster_Class_Data = smc;
                                ArticleMaster_Class amc = new ArticleMaster_Class();
                                amc.Article_No = item.Article_No;
                                amc.Article_PrimaryImage = item.Article_PrimaryImage;
                                amc.Article_SecondaryImage = item.Article_SecondaryImage;
                                fsl.ArticleMaster_Class_Data = amc;
                                CategoryMaster_Class cmc = new CategoryMaster_Class();
                                cmc.Category_Name = item.CategoryMaster_Class_Data.Category_Name + " - " + Subitem.SubCategoryMaster_Class_Data.SubCategory_Name;
                                fsl._CategoryMaster_Class_Data = cmc;
                                //fsl._SubCategoryMaster_Class_Data.SubCategory_Name = Subitem.SubCategory_Name;
                                fsl.Total_Amount = (fsl.MRP * fsl.Quantity).ToString();
                                // fscd.Add(fsl);
                                Franchise_Sell_Class.FranchiseSellDetails_Class_List.Add(fsl);
                            }
                        }
                    }
                    //   Franchise_Sell_Class_Data.FranchiseSellDetails_Class_List.Add(fscd);
                    Wait.Hide();
                    App.isBusy = false;
                    if (Franchise_Sell_Class.Total_Amount > 0)
                    {
                       

                        await navigation.PushAsync(new SellDetailPage(Franchise_Sell_Class), true);
                    }
                    else
                    {
                        
                        await App.Current.MainPage.DisplayAlert("Oops!", "Please Fill Qty Then Submit..", "Ok");
                    }



                }
                else
                {
                    if (Customer_Class.Customer_Name != null && Customer_Class.Customer_Name != "")
                    {

                        JObject result = await _IAllDataServices.CustomerRegistration(Customer_Class);

                        if (result != null && result["Type"].ToString() == "1" || result["Type"].ToString() == "0")
                        {

                            JObject result2 = await _IAllDataServices.GetCustomerByMobileNo(Customer_Class);
                            type1 = result2["Type"].ToString();
                            if (type1 == "1")
                            {


                                Franchise_Sell_Class.Customer_Id = (int)result2["Result"]["Customer_Id"];

                                foreach (var item in ArticleMasterClassList)
                                {
                                    foreach (var Subitem in item.FranchiseStokeMaster_Class_List)
                                    {
                                        if (Subitem.Insert_New_Qty > 0)
                                        {
                                            FranchiseSellDetails_Class fsl = new FranchiseSellDetails_Class();
                                            Franchise_Sell_Class.Total_Amount = Franchise_Sell_Class.Total_Amount + (Subitem.Insert_New_Qty * item.MRP);
                                            fsl.SubCategory_Id = Subitem.SubCategoryMaster_Class_Data.SubCategory_Id;
                                            fsl.Article_Id = item.Article_Id;
                                            fsl.MRP = item.MRP;
                                            fsl.Quantity = Subitem.Insert_New_Qty;
                                            StockMaster_Class smc = new StockMaster_Class();
                                            smc.Stock_Id = Subitem.Stock_Id;
                                            fsl.FranchiseStokeMaster_Class_Data = smc;
                                            ArticleMaster_Class amc = new ArticleMaster_Class();
                                            amc.Article_No = item.Article_No;
                                            amc.Article_PrimaryImage = item.Article_PrimaryImage;
                                            amc.Article_SecondaryImage = item.Article_SecondaryImage;
                                            fsl.ArticleMaster_Class_Data = amc;
                                            CategoryMaster_Class cmc = new CategoryMaster_Class();
                                            cmc.Category_Name = item.CategoryMaster_Class_Data.Category_Name + " - " + Subitem.SubCategoryMaster_Class_Data.SubCategory_Name;
                                            fsl._CategoryMaster_Class_Data = cmc;
                                            //fsl._SubCategoryMaster_Class_Data.SubCategory_Name = Subitem.SubCategory_Name;
                                            fsl.Total_Amount = (fsl.MRP * fsl.Quantity).ToString();
                                            // fscd.Add(fsl);
                                            Franchise_Sell_Class.FranchiseSellDetails_Class_List.Add(fsl);
                                        }
                                    }
                                }



                                if (Franchise_Sell_Class.Total_Amount > 0)
                                {
                                    App.isBusy = false;
                                    await navigation.PushAsync(new SellDetailPage(Franchise_Sell_Class), true);
                                }
                                else
                                {
                                    App.isBusy = false;
                                    await App.Current.MainPage.DisplayAlert("Oops!", "Please Fill Qty Then Submit..", "Ok");
                                }


                            }
                            else
                            {
                                App.isBusy = false;
                                await App.Current.MainPage.DisplayAlert("Oops!", "Something is worng please try Again....", "Ok");
                            }

                        }
                        else
                        {

                            App.isBusy = false;
                            await App.Current.MainPage.DisplayAlert("Oops!", (string)result["ResponseMessage"], "Ok");


                        }



                    }
                    else
                    {
                        App.isBusy = false;
                        await App.Current.MainPage.DisplayAlert("Oops!", "Please fill customer name ", "Ok");

                    }
                    Wait.Hide();
                }

                Wait.Hide();

            }

        }

        private  void CancelTask()
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
            bool Process=false;
            string Item_Name = string.Empty,Qty=string.Empty;
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
                await App.Current.MainPage.DisplayAlert("Oops!", "Enter quantity is more than available quantity. ( Item ="+Item_Name+" Qty="+Qty+" not available ) ", "Ok");
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

        private async Task CheckContectNo()
        {
            if (!App.isBusy)
            {
                try
                {
                    if (HasErrors)
                    {
                        App.isBusy = false;
                        ScrollToControlProperty(GetFirstInvalidPropertyName);
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(Customer_Class.Contact_No))
                        {
                            if (_Get_Btn_Text == "Get")
                            {
                                var Wait = UserDialogs.Instance.Loading("Wait..", null, null, true, MaskType.Black);
                                Wait.Show();
                                JObject result = await _IAllDataServices.GetCustomerByMobileNo(Customer_Class);
                                if (result != null)
                                {
                                    App.isBusy = false;
                                    string type = result["Type"].ToString();
                                    if (type == "1")
                                    {
                                        _Show_And_Hide_Stack = true;
                                        _Get_Btn_Text = "Cancel";
                                        _Text_Enable = false;

                                        //await App.Current.MainPage.DisplayAlert("Success!", "Customer : " + (string)result["Result"]["Customer_Name"] + " Is Registered", "Ok");
                                        Franchise_Sell_Class_Data.Customer_Id = (int)result["Result"]["Customer_Id"];
                                        Customer_Class customer_ = new Customer_Class();
                                        customer_.Contact_No = result["Result"]["Contact_No"].ToString();
                                        customer_.Email_Id = result["Result"]["Email_Id"].ToString();
                                        customer_.Customer_Name = result["Result"]["Customer_Name"].ToString();
                                        CustomerClassToBind = customer_;

                                        Franchise_Sell_Class_Data.Customer_Class_Data = Customer_Class;
                                    }
                                    else
                                    {
                                        App.isBusy = false;
                                        Registration_layout = true;
                                        _Show_And_Hide_Stack = true;
                                        _Get_Btn_Text = "Cancel";
                                        //await App.Current.MainPage.DisplayAlert("Error!", (string)result["ResponseMessage"], "Ok");
                                        _Text_Enable = false;
                                        Franchise_Sell_Class_Data.Customer_Id = 0;
                                    }
                                }
                                else
                                {
                                    App.isBusy = false;
                                    _Show_And_Hide_Stack = false;
                                    //_Get_Btn_Text = "Get";
                                    _Get_Btn_Text = "Cancel";
                                    _Text_Enable = true;
                                    await App.Current.MainPage.DisplayAlert("Oops!", "Please Refresh Page And try Again....", "Ok");
                                }
                                Wait.Dispose();
                            }
                            else
                            {
                                //var fm = new FranchiseMaster();
                                //var page = (Page)Activator.CreateInstance(typeof(SellPage));
                                //page.Title = "Sell";
                                //fm.Detail = new NavigationPage(page)
                                //{
                                //    BarBackgroundColor = Color.Black,
                                //    BarTextColor = Color.White,
                                //};
                                //fm.IsPresented = false;
                                //await navigation.PushModalAsync(fm);
                                App.isBusy = false;
                                SellPage cp = new SellPage();
                                var ChackPriousPage = navigation.NavigationStack.Where(x => x.Title == cp.Title).FirstOrDefault();

                                if (ChackPriousPage != null)
                                {

                                    await navigation.PopAsync();

                                }
                                await navigation.PushAsync(new SellPage());
                                // null List
                            }
                        }
                        else
                        {

                            _Contect_No = string.Empty;
                            _Show_And_Hide_Stack = false;
                            _Get_Btn_Text = "Get";
                            _Text_Enable = true;
                        }
                    }
                }
                catch (Exception ee)
                {
                    App.isBusy = false;
                    await App.Current.MainPage.DisplayAlert("Error", ee.Message, "Ok");
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
            set { ArticleMasterClassList = value;
                NotifyPropertyChanged();
                if (value.Count > 0)
                {
                    _HideShowSaveButton = true;
                    
                }
            }
        }

        [Display(Name = "Customer Contact No.")]
        [Required]
        [StringLength(10, ErrorMessage = "Only 10 Number Insert")]
        [RegularExpression("[0-9]{10}", ErrorMessage = "Incorrect Contact No.")]
        public string _Contect_No
        {
            get { return Customer_Class.Contact_No; }
            set
            {
                Customer_Class.Contact_No = value;

                NotifyPropertyChanged();
                ValidateProperty(value);

            }
        }
        /// <summary>
         [Display(Name = "Customer Name")]
        [Required]
        [StringLength(maximumLength: 30, ErrorMessage = "Maximum 30 Char Required")]
        public string _Customer_Name
        {
            get { return Customer_Class.Customer_Name; }
            set
            {
                Customer_Class.Customer_Name = value;
                NotifyPropertyChanged();
                ValidateProperty(value);

            }
        }
        [Display(Name = "Customer Email Id ")]
        [RegularExpression(@"^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$", ErrorMessage = "Incorrect Email Id.")]
        public string _Customer_Email_Id
        {
            get { return Customer_Class.Email_Id; }
            set
            {
                Customer_Class.Email_Id = value;
                NotifyPropertyChanged();
                ValidateProperty(value);

            }
        }

        /// 
        /// 
        /// 
        /// 
        /// 
        /// </summary>




        [Display(Name = "Product Article No.")]
        [Required]

        public string _Article_No
        {
            get { return ArticleNo; }
            set
            {
                ArticleNo = value;
                if (!string.IsNullOrEmpty(_Contect_No))
                {
                    NotifyPropertyChanged();
                    ValidateProperty(value);
                }
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
        public bool Registration_layout
        {
            get { return _Registration_layout; }
            set
            {
                _Registration_layout = value;
                NotifyPropertyChanged();
            }
        }
        public string _Get_Btn_Text
        {
            get { return GetBtnText; }
            set
            {
                GetBtnText = value;
                NotifyPropertyChanged();
            }
        }
        public bool _Text_Enable
        {
            get { return TextEnable; }
            set
            {
                TextEnable = value;
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
