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
    public class RetailerOrderPageViewModel : ModelBase
    {

        private RetailerMaster_Class RetailerMaster_Class_Data;
        private IAllDataServices IAllDataServices_Data;
        //  private RetailerOrder_Class RetailerOrder_Class_Data;
        //    private Employee_OrderGenerate_Class Employee_OrderGenerate_Class_Data;
        public bool IsBussy = false;
        public bool RetailerContectNoEnabled = true;
        public bool SubStackShowHide = false;
        public string ButtonText = "Get";
        public bool AfterCheckContect = false;
        public ObservableCollection<ArticleMaster_Class> ArticleList;
        public ICommand GetContectCommand { get; }
        public bool HideShowSaveButton = false;
        private ArticleMaster_Class _SelectArticleMasterClassList;
        private bool MainStackShowHide = true;
        private INavigation _navigation { get; set; }
        public ICommand AddCommand { get; }
        public ICommand CancelCommand { get { return new Command(() => CancelTask()); } }
        public ICommand SaveCommand { get; }
       

        public RetailerOrderPageViewModel(INavigation navigation)
        {
            _navigation = navigation;
            RetailerMaster_Class_Data = new RetailerMaster_Class();
            IAllDataServices_Data = new AllDataServices();
            // RetailerOrder_Class_Data = new RetailerOrder_Class();
            //  Employee_OrderGenerate_Class_Data = new Employee_OrderGenerate_Class();
            ArticleList = new ObservableCollection<ArticleMaster_Class>();
            GetContectCommand = new Command(async () => await GetContectAsync());
            AddCommand = new Command(async () => await Add());
          //  CancelCommand = new Command(async () => await CancelTask());
            SaveCommand = new Command(async () => await Save());

              getlistAsync();
           
        }
        private async Task Save()
        {
            Employee_OrderGenerate_Class eoc = new Employee_OrderGenerate_Class();
            eoc.Retailer_Id = RetailerMaster_Class_Data.RetailerMaster_Id;
            eoc.RetailerMaster_Class_Data.Mobile_No = RetailerMaster_Class_Data.Mobile_No;
            foreach (var data in _Article_List)
            {

                foreach (var subdata in data.FranchiseStokeMaster_Class_List)
                {
                    if (subdata.Insert_New_Qty > 0)
                    {
                        eoc.Total_Amount = eoc.Total_Amount + (subdata.Insert_New_Qty * Convert.ToDouble(data.MRP));
                        eoc.Total_Item = eoc.Total_Item + subdata.Insert_New_Qty;
                        Employee_OrderDetailsMaster_Class eodc = new Employee_OrderDetailsMaster_Class();
                        eodc.Article_Id = data.Article_Id;
                        eodc.MRP = Convert.ToDouble(data.MRP);
                        eodc.Quantity = subdata.Insert_New_Qty;
                        eodc.SellPrice = data.Selling_Price;
                        eodc.SubCategory_Id = subdata.SubCategory_Id;
                        eodc.ArticleMaster_Class_Data.Article_No = data.Article_No;
                        
                        eodc.ArticleMaster_Class_Data.Article_PrimaryImage = data.Article_PrimaryImage;
                        eodc.ArticleMaster_Class_Data.Article_SecondaryImage = data.Article_SecondaryImage;
                        eodc.CategoryMaster_Class_Date.Category_Name = data.CategoryMaster_Class_Data.Category_Name + '-' + subdata.SubCategoryMaster_Class_Data.SubCategory_Name;
                        eoc.odm.Add(eodc);
                    }
                }
            }

          
            if (eoc.Total_Amount > 0)
            {
                await _navigation.PushAsync(new RetailerOrderDetailPage(eoc), true);
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Oops!", "please fill qty then submit..", "ok");
            }
            //FranchiseSell_Class Franchise_Sell_Class = new FranchiseSell_Class();
            //Franchise_Sell_Class.Customer_Id = Franchise_Sell_Class_Data.Customer_Id;
            //Franchise_Sell_Class.Customer_Class_Data = Franchise_Sell_Class_Data.Customer_Class_Data;
            //foreach (var item in ArticleMasterClassList)
            //{
            //    foreach (var Subitem in item.FranchiseStokeMaster_Class_List)
            //    {
            //        if (Subitem.Insert_New_Qty > 0 && Subitem.Insert_New_Qty != null)
            //        {
            //            FranchiseSellDetails_Class fsl = new FranchiseSellDetails_Class();
            //            Franchise_Sell_Class.Total_Amount = Franchise_Sell_Class.Total_Amount + (Subitem.Insert_New_Qty * item.MRP);
            //            fsl.SubCategory_Id = Subitem.SubCategoryMaster_Class_Data.SubCategory_Id;
            //            fsl.Article_Id = item.Article_Id;
            //            fsl.MRP = item.MRP;
            //            fsl.Quantity = Subitem.Insert_New_Qty;
            //            StockMaster_Class smc = new StockMaster_Class();
            //            smc.Stock_Id = Subitem.Stock_Id;
            //            fsl.FranchiseStokeMaster_Class_Data = smc;
            //            ArticleMaster_Class amc = new ArticleMaster_Class();
            //            amc.Article_No = item.Article_No;
            //            amc.Article_PrimaryImage = item.Article_PrimaryImage;
            //            amc.Article_SecondaryImage = item.Article_SecondaryImage;
            //            fsl.ArticleMaster_Class_Data = amc;
            //            CategoryMaster_Class cmc = new CategoryMaster_Class();
            //            cmc.Category_Name = item.CategoryMaster_Class_Data.Category_Name + " - " + Subitem.SubCategoryMaster_Class_Data.SubCategory_Name;
            //            fsl._CategoryMaster_Class_Data = cmc;
            //            //fsl._SubCategoryMaster_Class_Data.SubCategory_Name = Subitem.SubCategory_Name;
            //            fsl.Total_Amount = (fsl.MRP * fsl.Quantity).ToString();
            //            // fscd.Add(fsl);
            //            Franchise_Sell_Class.FranchiseSellDetails_Class_List.Add(fsl);
            //        }
            //    }
            //}
            ////   Franchise_Sell_Class_Data.FranchiseSellDetails_Class_List.Add(fscd);

            //if (Franchise_Sell_Class.Total_Amount > 0)
            //{
            //    await navigation.PushAsync(new SellDetailPage(Franchise_Sell_Class), true);
            //}
            //else
            //{
            //    await App.Current.MainPage.DisplayAlert("Oops!", "Please Fill Qty Then Submit..", "Ok");
            //}

        }

        private async Task getlistAsync()
        {
            var Wait = UserDialogs.Instance.Loading("Wait..", Cancel(), "Cancel", true, MaskType.Black);
            Wait.Show();
            JObject result_List = await IAllDataServices_Data.GetEmployeeStock();
            //_Article_List = JsonConvert.DeserializeObject<ObservableCollection<ArticleMaster_Class>>((string)result_List["Result"]);
            //string sdads = "";
            //int a = result_List["Result"].Count();

            if (result_List != null)
            {
                ObservableCollection<ArticleMaster_Class> newror = new ObservableCollection<ArticleMaster_Class>();
                foreach (var od in result_List["Result"])
                {
                    ArticleMaster_Class ror = new ArticleMaster_Class();
                    ror.Article_Id = (int)od["Article_Id"];
                    ror.Article_No = (string)od["Article_No"];
                    ror.Article_PrimaryImage = (string)od["Article_PrimaryImage"];
                    ror.Article_SecondaryImage = (string)od["Article_SecondaryImage"];
                    ror.CategoryMaster_Class_Data.Category_Name = (string)od["CategoryMaster_Class_Data"]["Category_Name"];
                    ror.Color = (string)od["Color"];
                    ror.Description = (string)od["Description"];
                    ror.MRP = (decimal)od["MRP"];
                    ror.Selling_Price = (double)od["Selling_Price"];

                    //ror.FranchiseStokeMaster_Class_List=
                    ObservableCollection<StockMaster_Class> rbcl = new ObservableCollection<StockMaster_Class>();
                    foreach (var bd in od["FranchiseStokeMaster_Class_List"])
                    {
                        StockMaster_Class rbc = new StockMaster_Class();
                        rbc.Quantity = (int)bd["Quantity"];
                        rbc.Stock_Id = (int)bd["Stock_Id"];
                        rbc.SubCategory_Id = (int)bd["SubCategoryMaster_Class_Data"]["SubCategory_Id"];
                        rbc.SubCategoryMaster_Class_Data.SubCategory_Name = (string)bd["SubCategoryMaster_Class_Data"]["SubCategory_Name"];

                        rbcl.Add(rbc);

                    }
                    ror.FranchiseStokeMaster_Class_List = rbcl;
                    newror.Add(ror);

                }
                _Article_List = newror;
            }
           
           
            Wait.Dispose();
        }

        private async Task GetContectAsync()
        {

            if (_Button_Text == "Get")
            {

                if (HasErrors)
                {
                    // Error message
                    ScrollToControlProperty(GetFirstInvalidPropertyName);
                }
                else
                {
                    if (!string.IsNullOrEmpty(RetailerMaster_Class_Data.Mobile_No))
                    {
                        var Wait = UserDialogs.Instance.Loading("Wait..", Cancel(), "Cancel", true, MaskType.Black);
                        Wait.Show();
                        JObject result = await IAllDataServices_Data.GetRetailerByMobileNo(RetailerMaster_Class_Data);

                        if (result != null)
                        {
                            string type = result["Type"].ToString();



                            if (type == "1")
                            {
                                await App.Current.MainPage.DisplayAlert("Success!", "Retailer : " + (string)result["Result"]["Retailer_Name"] + " Is Registered", "Ok");
                                RetailerMaster_Class_Data.RetailerMaster_Id = (int)result["Result"]["RetailerMaster_Id"];
                                _Retailer_Contect_No_Enabled = false;
                                _Button_Text = "Cancel";
                                _After_Check_Contect = true;

                                if (_Article_List.Count == 0)
                                {
                                    await App.Current.MainPage.DisplayAlert("Oops!","Product Is Not Available Please Contact Administrator...","Ok");
                                }

                                //JObject result_List = await IAllDataServices_Data.GetEmployeeStock();
                                //if (type == "1")
                                //{

                                //    //var res = result_List["Result"];
                                //  _Selected_Product_Master_List = JsonConvert.DeserializeObject<ObservableCollection<RetailerOrderResponse_Class>>(result_List["Result"].ToString());
                                //}
                                //else
                                //{
                                //    await App.Current.MainPage.DisplayAlert("Error!", (string)result["ResponseMessage"], "Ok");

                                //}
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
                        _Retailer_Contect_No = "";
                    }
                }
            }
            else
            {
                _Retailer_Contect_No = "";
                _Retailer_Contect_No_Enabled = true;
                _Button_Text = "Get";
                _After_Check_Contect = false;
               getlistAsync();
               

            }

        }

        private Action Cancel()
        {
            var cancelSrc = new CancellationTokenSource();
            return cancelSrc.Cancel;
        }

        [Display(Name = "Retailer Mobile No.")]
        [Required]
        [StringLength(10, ErrorMessage = "Only 10 Number Insert")]
        [RegularExpression("[0-9]{10}", ErrorMessage = "Incorrect Mobile No.")]
        public string _Retailer_Contect_No
        {
            get { return RetailerMaster_Class_Data.Mobile_No; }
            set
            {
                RetailerMaster_Class_Data.Mobile_No = value;
                NotifyPropertyChanged();
                ValidateProperty(value);

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
        public bool _Retailer_Contect_No_Enabled
        {
            get { return RetailerContectNoEnabled; }
            set
            {
                RetailerContectNoEnabled = value;
                NotifyPropertyChanged();
            }
        }
        public string _Button_Text
        {
            get { return ButtonText; }
            set
            {
                ButtonText = value;
                NotifyPropertyChanged();
            }
        }

        public bool _After_Check_Contect
        {
            get { return AfterCheckContect; }
            set
            {
                AfterCheckContect = value;
                NotifyPropertyChanged();
            }
        }

        public ObservableCollection<ArticleMaster_Class> _Article_List
        {
            get { return ArticleList; }
            set
            {
                ArticleList = value;
                NotifyPropertyChanged();
                if (value.Count > 0)
                {
                    _HideShowSaveButton = true;

                }
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
        public bool _MainStackShowHide
        {
            get { return MainStackShowHide; }
            set
            {
                MainStackShowHide = value;
                NotifyPropertyChanged();

            }
        }

        private void CancelTask()
        {
            _MainStackShowHide = true;
            _SubStackShowHide = false;
           // bool ProcessStatus = false;
            foreach (var st in SelectArticleMasterClassList.FranchiseStokeMaster_Class_List)
            {
                if (st.Quantity >= st.Insert_New_Qty)
                {
                  //  ProcessStatus = true;
                }
                else
                {
                    st.Insert_New_Qty = 0;
                }
            }
            //if (ProcessStatus == false)
            //{
            //    SelectArticleMasterClassList.FixRate = 0;
            //}
        }

        private async Task Add()
        {
            bool Process = false;
            string Item_Name = string.Empty, Qty = string.Empty;
            int check = 0;
            foreach (var st in SelectArticleMasterClassList.FranchiseStokeMaster_Class_List)
            {
                if (st.Quantity >= st.Insert_New_Qty)
                {
                    Process = true;
                     if (st.Insert_New_Qty == 0)
                    {
                        check = check + 1;
                    }
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
                //if (SelectArticleMasterClassList.FixRate > 0)
                //{
                    if (check != SelectArticleMasterClassList.FranchiseStokeMaster_Class_List.Count)
                    {
                        _MainStackShowHide = true;
                        _SubStackShowHide = false;

                        var indexof = _Article_List.IndexOf(SelectArticleMasterClassList);
                        _Article_List.Remove(SelectArticleMasterClassList);
                        _Article_List.Insert(indexof, SelectArticleMasterClassList);
                    }
                    else
                    {
                        await App.Current.MainPage.DisplayAlert("Oops!", "Enter Subcategory Qty then Add...", "Ok");
                    }
                //}
                //else
                //{
                //    await App.Current.MainPage.DisplayAlert("Oops!", "Enter Fix Rate", "Ok");
                //}

            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Oops!", "Enter quantity is more than available quantity. ( Item =" + Item_Name + " Qty=" + Qty + " not available ) ", "Ok");
            }
        }
    }
}
