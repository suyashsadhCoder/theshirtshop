using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using theshirtshopApp.BAL;
using theshirtshopApp.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace theshirtshopApp.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RefundReturnPage : ContentPage
    {
       
        public RefundReturnViewModel refundReturnViewModel;
        public RefundReturnPage()
        {
            InitializeComponent();
            Application.Current.Properties["isChanged"] = "N";
            this.BindingContext = refundReturnViewModel = new RefundReturnViewModel();
            btnFilter.Clicked += BtnFilter_Clicked;
            invoicelistview.ItemSelected += Invoicelistview_ItemSelected;
            invoicelistview.RefreshCommand = new Command(async () => {
                //Do your stuff.
                invoicelistview.IsRefreshing = true;
                await refundReturnViewModel.initiallist();
                invoicelistview.IsRefreshing = false;
            });

        }
        
        private void Invoicelistview_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as InvoiceDataModel;
            Navigation.PushAsync(new InvoiceDetailPage(item.FranchiseSellId));
        }

        private void BtnFilter_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ReturnRefundFilterView(refundReturnViewModel.InvoiceFilter));
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (Application.Current.Properties["isChanged"].ToString() == "Y") {
                Application.Current.Properties["isChanged"] = "N";
                refundReturnViewModel.InvoiceFilter =
                    new InvoiceFilterModel()
                    {
                        Amount = Application.Current.Properties["Amount"].ToString(),
                        ArticleNo = Application.Current.Properties["ArticleNo"].ToString(),
                        FranchiseId = Application.Current.Properties["OtherId"].ToString(),
                        FromDate = Application.Current.Properties["FromDate"].ToString(),
                        SearchBy = Application.Current.Properties["SearchBy"].ToString(),
                        SortDirection = "DESC",//Application.Current.Properties["SortDirection"].ToString(),
                        PageSize = "50",//Application.Current.Properties["PageSize"].ToString(),
                        SortExpression = Application.Current.Properties["SortExpression"].ToString(),
                        Status = Application.Current.Properties["Status"].ToString(),
                        ToDate = Application.Current.Properties["ToDate"].ToString(),
                        CurrentPage = "1" 
                    };
                
            }
            refundReturnViewModel.initiallist();
        }

        private void ImageEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            var str = e.NewTextValue;
            if (str.Length > 2)
            {
                refundReturnViewModel.InvoiceFilter = new InvoiceFilterModel()
                {
                    Amount = "",
                    ArticleNo = "",
                    CurrentPage = "1",
                    FranchiseId = Application.Current.Properties["OtherId"].ToString(),
                    FromDate = "",
                    SearchBy = str,
                    SortDirection = "DESC",
                    PageSize = "50",
                    SortExpression = "FranchiseSellId",
                    Status = "",
                    ToDate = ""
                };
                refundReturnViewModel.loadlistByFilterAsync();
            }
            else {
                invoicelistview.ItemsSource = refundReturnViewModel.InvoiceDataList;
            }
        }
    }
    }
