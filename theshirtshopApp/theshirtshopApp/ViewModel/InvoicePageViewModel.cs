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
using theshirtshopApp.BAL;
using theshirtshopApp.DAL;
using theshirtshopApp.Interface;
using theshirtshopApp.Validation;
using theshirtshopApp.View;
using Xamarin.Forms;
//using Xamarin.Forms.ListView;
namespace theshirtshopApp.ViewModel
{
    public class InvoicePageViewModel : ModelBase
    {
        private IAllDataServices _IAllDataServices;
        public InvoicePageViewModel(int index)
        {
            if (index == 0)
            {
                GetOrderInvoice();
            }
            else if (index == 1)
            {
                GetCustomerInvoice();
            }
            else if (index == 2)
            {

            }
            //CommandGetInvoiceCustomer = new Command(async () => await GetCustomerInvoice());
           // CommandGetInvoiceOrder = new Command(async () => await GetOrderInvoice());

            _IAllDataServices = new AllDataServices();
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
                                    Show_Order_Invoice = false;
                                    Show_Customer_Invoice = false;
                                }
                                else
                                {
                                    Show_Order_Invoice = true;
                                    Show_Customer_Invoice = false;
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
                                    Show_Order_Invoice = false;
                                    Show_Customer_Invoice = false;
                                }
                                else
                                {
                                    Show_Customer_Invoice = true;
                                    Show_Order_Invoice = false;
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

        private ObservableCollection<Invoce_Type_Class> Invoce_Type_List;
        private ObservableCollection<FranchiseSell_Class> Franchise_Sell_Class_List;
        private ObservableCollection<OrderMaster_Class> OrderMaster_Class_List;
        private Invoce_Type_Class Invoce_Type_Class_Data = new Invoce_Type_Class();
        private FranchiseSell_Class Franchise_Sell_Class_Data = new FranchiseSell_Class();
        private OrderMaster_Class OrderMaster_Class_Data = new OrderMaster_Class();

        public Command CommandGetInvoiceCustomer { get; }
        public Command CommandGetInvoiceOrder { get; }


        public bool ShowCustomerInvoice = false;
        public bool ShowOrderInvoice = false;
       // public FranchiseSell_Class FranchiseSellClassListSelectedItem { get; set; }
     //   public OrderMaster_Class OrderMasterClassListSelectedItem { get; set; }

        private INavigation _navigation { get; set; }




       
        private Action Cancel()
        {
            var it = new CancellationTokenSource();
            return it.Cancel;
        }

        //public ObservableCollection<Invoce_Type_Class> _Invoce_Type_List
        //{
        //    get { return Invoce_Type_List; }
        //    set
        //    {
        //        Invoce_Type_List = value;
        //        NotifyPropertyChanged();
        //        ValidateProperty(Invoce_Type_List);
        //    }
        //}

        public ObservableCollection<FranchiseSell_Class> _Franchise_Sell_Class_List
        {
            get { return Franchise_Sell_Class_List; }
            set
            {
                Franchise_Sell_Class_List = value;
                NotifyPropertyChanged();
                ValidateProperty(Franchise_Sell_Class_List);
            }
        }

        public ObservableCollection<OrderMaster_Class> _OrderMaster_Class_List
        {
            get { return OrderMaster_Class_List; }
            set
            {
                OrderMaster_Class_List = value;
                NotifyPropertyChanged();
                ValidateProperty(OrderMaster_Class_List);
            }
        }
       
        //public OrderMaster_Class OrderMasterClassList_SelectedItem
        //{
        //    get { return OrderMasterClassListSelectedItem; }
        //    set
        //    {
        //        OrderMasterClassListSelectedItem = value;
        //        if (OrderMasterClassListSelectedItem.Action == "Received" && !string.IsNullOrEmpty(OrderMasterClassListSelectedItem.Action))
        //        {
        //            NotifyPropertyChanged();
        //             GotoOrderMasterDetailPageAsync(OrderMasterClassListSelectedItem);
        //        }

        //    }

        //}

        //private async void GoToDetailPage(OrderMaster_Class orderMasterClassListSelectedItem)
        //{
        //    await _navigation.PushAsync(new UpComingStockRequestDetailPage(orderMasterClassListSelectedItem), true);

        //}
        //public int _Selected_Invice_Type_Id
        //{
        //    get; set;
        //}

        public bool Show_Customer_Invoice
        {
            get { return ShowCustomerInvoice; }
            set
            {
                ShowCustomerInvoice = value;
                NotifyPropertyChanged();


            }

        }
        public bool Show_Order_Invoice
        {
            get { return ShowOrderInvoice; }
            set
            {
                ShowOrderInvoice = value;
                NotifyPropertyChanged();


            }
        }
        public bool isSelected = false;
        public bool IsSelected
        {
            get { return isSelected; }
            set
            {
                isSelected = value;
                NotifyPropertyChanged();
            }
        }
       
        //public FranchiseSell_Class FranchiseSellClassList_SelectedItem
        //{
        //    get { return FranchiseSellClassListSelectedItem; }
        //    set
        //    {
        //        FranchiseSellClassListSelectedItem = value;
        //        if (FranchiseSellClassListSelectedItem.FranchiseSell_Id != 0)
        //        {
        //            NotifyPropertyChanged();
        //            GotoDetailPageAsync(FranchiseSellClassListSelectedItem);
        //        }



        //    }
        //}


        //private async Task GotoOrderMasterDetailPageAsync(OrderMaster_Class orderMasterClassListSelectedItem)
        //{
        //    await _navigation.PushAsync(new OrderInvoiceDetailPage(orderMasterClassListSelectedItem),true);
        //}

        //private async Task GotoDetailPageAsync(FranchiseSell_Class _Franchise_Sell_Class)
        //{
        //    await _navigation.PushModalAsync(new CustomerInvoiceDetailPage(_Franchise_Sell_Class));
        //}
    }
}
