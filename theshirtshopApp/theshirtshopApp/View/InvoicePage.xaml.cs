using Acr.UserDialogs;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using theshirtshopApp.BAL;
using theshirtshopApp.DAL;
using theshirtshopApp.Interface;
using theshirtshopApp.Validation;
using theshirtshopApp.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace theshirtshopApp.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class InvoicePage : TabbedPage
    {
        private IAllDataServices _IAllDataServices;
        private ObservableCollection<OrderMaster_Class> _OrderMaster_Class_List;
        private ObservableCollection<FranchiseSell_Class> _Franchise_Sell_Class_List;
        private ObservableCollection<FranchiseGoodReturn_Class> _GoodsReturn_Class_List;
        public InvoicePage()
        {
            if (Application.Current.Properties.ContainsKey("Key"))
            {
                InitializeComponent();
                
                _IAllDataServices = new AllDataServices();
                _OrderMaster_Class_List = new ObservableCollection<OrderMaster_Class>();
                _Franchise_Sell_Class_List = new ObservableCollection<FranchiseSell_Class>();
                _GoodsReturn_Class_List = new ObservableCollection<FranchiseGoodReturn_Class>();
                popupLoginView.IsVisible = false;
                this.CurrentPageChanged += (object sender, EventArgs e) => {
                  
                    var index = this.Children.IndexOf(this.CurrentPage);
                    if (index == 0)
                    {
                        popupLoginView.IsVisible = false;
                        GetOrderInvoice();
                    }
                    else if (index == 1)
                    {
                        popupLoginView1.IsVisible = false;
                        GetCustomerInvoice();
                    }
                    else if (index == 2)
                    {
                        popupLoginView2.IsVisible = false;
                        GetReturnInvoice();
                    }
                   
                };
                ShowCustomerInvoice.IsVisible = false;
                ShowOrderInvoice.IsVisible = false;
                ShowReturnInvoice.IsVisible = false;
                MsgResult.IsVisible = false;
                MsgResul.IsVisible = false;
                MsgResu.IsVisible = false;

                GetOrderInvoice();

                //BindingContext = new InvoicePageViewModel(Navigation);
                //MsgResult.IsVisible = false;
                //MsgResul.IsVisible = false;
                //gridFilter.IsVisible = false;
            }
            else
            {
                Navigation.PushModalAsync(new MainPage());
            }

        }
        private Action Cancel()
        {
            var it = new CancellationTokenSource();
            return it.Cancel;
        }
        public async Task GetOrderInvoice()
        {
            var Wait = UserDialogs.Instance.Loading("Wait..", Cancel(), "Cancel", true, MaskType.Black);
            Wait.Show();
            try
            {
                JObject result = await _IAllDataServices.GetAllInvoiceOrder();
                if (result != null)
                {
                    string type = result["Type"].ToString();

                    if (type == "1")
                    {
                        _OrderMaster_Class_List = JsonConvert.DeserializeObject<ObservableCollection<OrderMaster_Class>>(result["Result"].ToString());
                        if (_OrderMaster_Class_List.Count == 0)
                        {
                            await App.Current.MainPage.DisplayAlert("Oops!", "Order Invoice Not Found", "Ok");
                            ShowOrderInvoice.IsVisible = false;
                            
                        }
                        else
                        {
                            ShowOrderInvoice.IsVisible = true;

                            OrderMasterClassList.ItemsSource = _OrderMaster_Class_List;
                            MsgResult.IsVisible = false;
                            MsgResul.IsVisible = false;
                        }

                    }
                    else
                    {
                        await App.Current.MainPage.DisplayAlert("Error!", (string)result["ResponseMessage"], "Ok");
                    }
                }

            }
            catch (Exception ee)
            {
                await App.Current.MainPage.DisplayAlert("Error!", ee.Message, "Ok");
            }
            Wait.Dispose();
        }

        public async Task GetCustomerInvoice()
        {
            var Wait = UserDialogs.Instance.Loading("Wait..", Cancel(), "Cancel", true, MaskType.Black);
            Wait.Show();
            try
            {

                JObject result = await _IAllDataServices.GetAllInvoiceCustomer();
                if (result != null)
                {
                    string type = result["Type"].ToString();

                    if (type == "1")
                    {
                        _Franchise_Sell_Class_List = JsonConvert.DeserializeObject<ObservableCollection<FranchiseSell_Class>>(result["Result"].ToString());
                        if (_Franchise_Sell_Class_List.Count == 0)
                        {
                            await App.Current.MainPage.DisplayAlert("Oops!", "Customer Invoice Not Found", "Ok");
                            ShowCustomerInvoice.IsVisible = false;
                           
                        }
                        else
                        {
                            ShowCustomerInvoice.IsVisible = true;
                            MsgResult.IsVisible = false;
                            MsgResul.IsVisible = false;
                            MsgResu.IsVisible = false;
                            FranchiseSellClassList.ItemsSource = _Franchise_Sell_Class_List;


                        }
                    }
                    else
                    {
                        await App.Current.MainPage.DisplayAlert("Error!", (string)result["ResponseMessage"], "Ok");
                    }
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("Oops!", "No Record Found....", "Ok");
                }
            }
            catch (Exception ee)
            {
                await App.Current.MainPage.DisplayAlert("Error!", ee.Message, "Ok");
            }
            Wait.Dispose();
        }

        public async Task GetReturnInvoice()
        {
            try
            {
                var Wait = UserDialogs.Instance.Loading("Wait...", Cancel(), "Cancel", true, MaskType.Black);
                Wait.Show();
                JObject result = await _IAllDataServices.GetAllGoodReturn() as JObject;
                if (result != null)
                {
                    var type = (int)result["Type"];
                    if (type == 1)
                    {
                        _GoodsReturn_Class_List = JsonConvert.DeserializeObject<ObservableCollection<FranchiseGoodReturn_Class>>(result["Result"].ToString());
                        if (_GoodsReturn_Class_List.Count == 0)
                        {
                            await App.Current.MainPage.DisplayAlert("Oops!", "Order Return Invoice Not Found", "Ok");
                            ShowReturnInvoice.IsVisible = false;

                        }
                        else
                        {
                            ShowReturnInvoice.IsVisible = true;
                            MsgResult.IsVisible = false;
                            MsgResul.IsVisible = false;
                            MsgResu.IsVisible = false;
                            ReturnOrderMasterClassList.ItemsSource = _GoodsReturn_Class_List.Where(x=>x.Action== "Accepted").ToList();


                        }

                    }
                    else
                    {
                        await App.Current.MainPage.DisplayAlert("Error!", (string)result["ResponseMessage"], "Ok");
                    }
                    Wait.Hide();
                }


            }
            catch (Exception ee)
            {
                await App.Current.MainPage.DisplayAlert("Error!", ee.Message, "Ok");
            }
        }
        private void OrderMasterClassList_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var article = e.Item as OrderMaster_Class;
            Navigation.PushAsync(new OrderInvoiceDetailPage(article), true);
        }

        private void FranchiseSellClassList_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var article = e.Item as FranchiseSell_Class;
            Navigation.PushAsync(new CustomerInvoiceDetailPage(article), true);
        }

        private void SellSearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            SellFilterNames();
        }

        private void SellSearchBar_SearchButtonPressed(object sender, EventArgs e)
        {
            SellFilterNames();
        }
     

        private void OrderSearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            OrderFilterNames();
        }

        private void OrderSearchBar_SearchButtonPressed(object sender, EventArgs e)
        {
            OrderFilterNames();
        }
        private void OrderFilterNames()
        {
            string filter = OrderSearchBar.Text;
          //var vm = BindingContext as InvoicePageViewModel;
            // OrderMasterClassList.BeginRefresh();
            if (string.IsNullOrWhiteSpace(filter))
            {
                OrderMasterClassList.ItemsSource = _OrderMaster_Class_List;
                MsgResul.IsVisible = false;
            }
            else
            {
                var sm = _OrderMaster_Class_List.Where(x => x.Created_Date.ToString("dd-MM-yyyy").Contains(filter.ToLower())
                   || x.Invoice_No.ToLower().Contains(filter.ToLower()));
                OrderMasterClassList.ItemsSource = sm;
                if (sm.ToList().Count == 0)
                {
                    MsgResul.IsVisible = true;
                }
                else
                {
                    MsgResul.IsVisible = false;
                }
            }
            //   OrderMasterClassList.EndRefresh();
        }
        private void SellFilterNames()
        {
            string filter = SellSearchBar.Text;
            //var vm = BindingContext as InvoicePageViewModel;
            //FranchiseSellClassList.BeginRefresh();
            if (string.IsNullOrWhiteSpace(filter))
            {
                FranchiseSellClassList.ItemsSource = _Franchise_Sell_Class_List;
                MsgResult.IsVisible = false;
            }
            else
            {
                var sm = _Franchise_Sell_Class_List.Where(x => x.Customer_Class_Data.Customer_Name.ToLower().Contains(filter.ToLower())
                  || x.Customer_Class_Data.Contact_No.ToLower().Contains(filter.ToLower())
                   || x.Invoice_No.ToLower().Contains(filter.ToLower()));
                FranchiseSellClassList.ItemsSource = sm;
                if (sm.ToList().Count == 0)
                {
                    MsgResult.IsVisible = true;
                }
                else
                {
                    MsgResult.IsVisible = false;
                }
            }
            //  FranchiseSellClassList.EndRefresh();
        }
        private void ReturnFilterNames()
        {
            string filter = ReturnSearchBar.Text;
            //var vm = BindingContext as InvoicePageViewModel;
            // OrderMasterClassList.BeginRefresh();
            if (string.IsNullOrWhiteSpace(filter))
            {
                ReturnOrderMasterClassList.ItemsSource = _GoodsReturn_Class_List;
                MsgResu.IsVisible = false;
            }
            else
            {
                var sm = _GoodsReturn_Class_List.Where(x => x.Created_Date.ToString("dd-MM-yyyy").Contains(filter.ToLower())
                   || x.Invoice_No.ToLower().Contains(filter.ToLower()));
                ReturnOrderMasterClassList.ItemsSource = sm;
                if (sm.ToList().Count == 0)
                {
                    MsgResu.IsVisible = true;
                }
                else
                {
                    MsgResu.IsVisible = false;
                }
            }
            //   OrderMasterClassList.EndRefresh();
        }

        private void FranchiseSellClassList_Refreshing(object sender, EventArgs e)
        {
            GetCustomerInvoice();
            FranchiseSellClassList.IsRefreshing = false;
        }

        private void OrderMasterClassList_Refreshing(object sender, EventArgs e)
        {
            GetOrderInvoice();
            OrderMasterClassList.IsRefreshing = false;
        }

        private void filter_Clicked(object sender, EventArgs e)
        {
            var index = this.Children.IndexOf(this.CurrentPage);
            if (index == 0)
            {
                popupLoginView.IsVisible = true;
            }
            else if (index == 1)
            {
                popupLoginView1.IsVisible = true;
            }
            else if (index == 2)
            {
                popupLoginView2.IsVisible = true;
            }
        }


        private void Download()
        {
            var index = this.Children.IndexOf(this.CurrentPage);
            if (index == 1)
            {
                if ((endDatePicker1.Date - startDatePicker1.Date).Days >= 0)
                {

                    var sm = _Franchise_Sell_Class_List.Where(x => x.Created_Date >= startDatePicker1.Date && x.Created_Date <= endDatePicker1.Date);
                    if (sm.ToList().Count > 0)
                    {
                        string id = Application.Current.Properties["OtherId"].ToString();
                        Device.OpenUri(new Uri("http://www.theshirtshop.in/TemplateView/bulk-customer-invoice.html?Id=" + id + "&From=" + startDatePicker1.Date.ToString("dd-MM-yyyy") + "&To=" + endDatePicker1.Date.ToString("dd-MM-yyyy")));
                    }
                    else
                    {
                        UserDialogs.Instance.Alert("Record not found", "Opps!", "Ok");
                    }

                }

            }
            else if (index == 0)
            {

                if ((endDatePicker.Date - startDatePicker.Date).Days >= 0)
                {

                    var sm = _OrderMaster_Class_List.Where(x => x.Created_Date >= startDatePicker.Date && x.Created_Date <= endDatePicker.Date);

                    if (sm.ToList().Count > 0)
                    {
                        string id = Application.Current.Properties["OtherId"].ToString();
                        Device.OpenUri(new Uri("http://www.theshirtshop.in/TemplateView/bulk-franchise-invoice.html?Id=" + id + "&From=" + startDatePicker.Date.ToString("dd-MM-yyyy") + "&To=" + endDatePicker.Date.ToString("dd-MM-yyyy")));
                    }
                    else
                    {
                        UserDialogs.Instance.Alert("Record not found", "Opps!", "Ok");
                    }

                }
            }
            else if (index == 2)
            {
                if ((endDatePicker2.Date - startDatePicker2.Date).Days >= 0)
                {

                    var sm = _GoodsReturn_Class_List.Where(x => x.Created_Date >= startDatePicker2.Date && x.Created_Date <= endDatePicker2.Date);

                    if (sm.ToList().Count > 0)
                    {
                        string id = Application.Current.Properties["OtherId"].ToString();
                        Device.OpenUri(new Uri("http://www.theshirtshop.in/TemplateView/bulk-franchise-order-return-invoice.html?Id=" + id + "&From=" + startDatePicker2.Date.ToString("dd-MM-yyyy") + "&To=" + endDatePicker2.Date.ToString("dd-MM-yyyy")));
                    }
                    else
                    {
                        UserDialogs.Instance.Alert("Record not found", "Opps!", "Ok");
                    }

                }
            }
            else
            {
                UserDialogs.Instance.Alert("Please click on customer invoice or order invoice", "Opps!", "Ok");
            }
        }
        private void Filter()
        {
            var index = this.Children.IndexOf(this.CurrentPage);
            if (index == 1)
            {
                if ((endDatePicker1.Date - startDatePicker1.Date).Days >= 0)
                {

                    var sm = _Franchise_Sell_Class_List.Where(x => x.Created_Date >= startDatePicker1.Date && x.Created_Date <= endDatePicker1.Date);
                    FranchiseSellClassList.ItemsSource = sm;
                    if (sm.ToList().Count == 0)
                    {
                        MsgResult.IsVisible = true;
                    }
                    else
                    {
                        MsgResult.IsVisible = false;
                    }

                }

            }
            else if (index == 0)
            {
                if ((endDatePicker.Date - startDatePicker.Date).Days >= 0)
                {

                    var sm = _OrderMaster_Class_List.Where(x => x.Created_Date >= startDatePicker.Date && x.Created_Date <= endDatePicker.Date);
                    OrderMasterClassList.ItemsSource = sm;
                    if (sm.ToList().Count == 0)
                    {
                        MsgResul.IsVisible = true;
                    }
                    else
                    {
                        MsgResul.IsVisible = false;
                    }

                }
            }
            else if (index == 2)
            {
                if ((endDatePicker2.Date - startDatePicker2.Date).Days >= 0)
                {

                    var sm = _GoodsReturn_Class_List.Where(x => x.Created_Date >= startDatePicker2.Date && x.Created_Date <= endDatePicker2.Date);
                    ReturnOrderMasterClassList.ItemsSource = sm;
                    if (sm.ToList().Count == 0)
                    {
                        MsgResu.IsVisible = true;
                    }
                    else
                    {
                        MsgResu.IsVisible = false;
                    }

                }
            }
            else
            {
                UserDialogs.Instance.Alert("Please click on customer invoice or order invoice", "Opps!", "Ok");
            }
        }
        private void PopupCancel()
        {
          
                popupLoginView.IsVisible = false;
                popupLoginView1.IsVisible = false;
            popupLoginView2.IsVisible = false;

        }
        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            PopupCancel();
        }

        private void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
        {
            Filter();
            PopupCancel();
        }

        private void TapGestureRecognizer_Tapped_2(object sender, EventArgs e)
        {
            Download();
            PopupCancel();
        }

        private void ReturnSearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            ReturnFilterNames();
        }

        private void ReturnSearchBar_SearchButtonPressed(object sender, EventArgs e)
        {
            ReturnFilterNames();
        }

        private void ReturnOrderMasterClassList_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var item = e.Item as FranchiseGoodReturn_Class;
            Navigation.PushAsync(new AllGoodsReturnDetailPage(item), true);
        }

        private void ReturnOrderMasterClassList_Refreshing(object sender, EventArgs e)
        {
            GetReturnInvoice();
            ReturnOrderMasterClassList.IsRefreshing = false;
        }
    }
}
