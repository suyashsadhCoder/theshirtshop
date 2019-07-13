using Acr.UserDialogs;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using theshirtshopApp.BAL;
using theshirtshopApp.DAL;
using theshirtshopApp.Interface;
using theshirtshopApp.Validation;

namespace theshirtshopApp.ViewModel
{
    public class StockDetailViewModel : ModelBase
    {
        // StockDetailDataServices _StockDetailDataServices;
        //public IList<StockDetailMaster> _StockDetailMasterList;
        //public IList<StockDetailMaster> StockDetailMasterList 
        //{
        //    get { return _StockDetailMasterList; }
        //    set { _StockDetailMasterList = value; }
        //}
        private IAllDataServices _IAllDataServices;
        public ObservableCollection<ArticleMaster_Class> AC;
        public ArticleMaster_Class OldarticleMaster_Class { get; set; }
        private ArticleMaster_Class st;
        private bool MainListVisible = false;
        private bool _ShowHideMsg = false;

        public StockDetailViewModel()
        {
            // _StockDetailDataServices = new StockDetailDataServices();
            // this._StockDetailMasterList = _StockDetailDataServices.GetStockDetail().ToList();
            _IAllDataServices = new AllDataServices();
            AC = new ObservableCollection<ArticleMaster_Class>();
            GetListAsync();
           
        }

        public async Task GetListAsync()
        {
            try
            {
                var Wait = UserDialogs.Instance.Loading("Wait..", Cancel(), "Cancel", true, MaskType.Black);
                Wait.Show();
                JObject result = await _IAllDataServices.GetFranchiseStock();
                if (result != null)
                {
                    string type = result["Type"].ToString();

                    if (type == "1")
                    {
                     Main_List = JsonConvert.DeserializeObject<ObservableCollection<ArticleMaster_Class>>(result["Result"].ToString());
                        if (Main_List.Count == 0)
                        {
                            await App.Current.MainPage.DisplayAlert("Oops!", "Stock Not Available", "Ok");
                            Main_List_Visible = false;
                           // _Show_Hide_Msg = true;
                        }
                        else
                        {
                            Main_List_Visible = true;
                           // _Show_Hide_Msg = false;
                        }
                    }
                    else
                    {
                        await App.Current.MainPage.DisplayAlert("Error!", (string)result["ResponseMessage"], "Ok");
                    }
                }
               
                Wait.Dispose();
            }
            catch (Exception ee)
            {
                await App.Current.MainPage.DisplayAlert("Error!", ee.Message, "Ok");
            }

        }

        private Action Cancel()
        {
            var ds =new CancellationTokenSource();
            return ds.Cancel;
        }

        public ObservableCollection<ArticleMaster_Class> Main_List
        {
            get { return AC; }
            set {
                AC = value;
                NotifyPropertyChanged();
            }
        }
        public ArticleMaster_Class SelectTab
        {
            get { return st; }
            set
            {
                st = value;
                NotifyPropertyChanged();
                if (value != null)
                {
                    HidenShow(st);
                }
            }
        }
        public bool _Show_Hide_Msg
        {
            get { return _ShowHideMsg; }
            set { _ShowHideMsg = value;
                NotifyPropertyChanged();
            }
        }

        private void HidenShow(ArticleMaster_Class st)
        {
            var cfg = new ActionSheetConfig()
                    .SetTitle("Sub Category")
                    .SetMessage(null)
                    .SetUseBottomSheet(false);

            foreach (var item in st.FranchiseStokeMaster_Class_List)
            {
                
                cfg.Add(
                    "Item : " + item.SubCategoryMaster_Class_Data.SubCategory_Name + "     Qty : "+item.Quantity
                  
                );
            }
            //cfg.SetDestructive();
            //if (true)
                cfg.SetCancel();

            var disp = UserDialogs.Instance.ActionSheet(cfg);
            if (this.AutoCancel)
            {
                Task.Delay(TimeSpan.FromSeconds(3))
                    .ContinueWith(x => disp.Dispose());
            }


        }
        bool autoCancel;
        CancellationTokenSource cancelSrc;
        public bool AutoCancel
        {
            get { return this.autoCancel; }
            set
            {
                if (this.autoCancel == value)
                    return;

                this.autoCancel = value;
                NotifyPropertyChanged();
                if (value)
                    this.cancelSrc = new CancellationTokenSource();
                else
                    this.cancelSrc = null;
            }
        }

        public bool Main_List_Visible {
            get { return MainListVisible; }


            set { MainListVisible = value;
                NotifyPropertyChanged();
            } }

    }
}
