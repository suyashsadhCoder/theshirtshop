
using Acr.UserDialogs;
using Plugin.DownloadManager;
using Plugin.DownloadManager.Abstractions;
using System;
using System.Collections.ObjectModel;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using theshirtshopApp.BAL;
using theshirtshopApp.Interface;
using theshirtshopApp.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace theshirtshopApp.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CustomerInvoiceDetailPage : ContentPage
    {
        public ObservableCollection<string> Items { get; set; }
        private FranchiseSell_Class ps;
        public CustomerInvoiceDetailPage(FranchiseSell_Class Franchise_Sell_Class_Data)
        {
            if (Application.Current.Properties.ContainsKey("Key"))
            {

                InitializeComponent();
                
                BindingContext = new CustomerInvoiceDetailPageViewModel(Franchise_Sell_Class_Data);
                if (!string.IsNullOrEmpty(Franchise_Sell_Class_Data.Invoice_No))
                {
                    ps = Franchise_Sell_Class_Data;
                }
            }
            else
            {
                Navigation.PushModalAsync(new MainPage());
            }

        }
        private  void ViewPdf_Clicked(object sender, EventArgs e)
        {
            DependencyService.Get<Interface.IDownloadManager>().Main("http://www.setquestionpaper.com/TemplateView/invoice_customer.html?sellId=" + ps.FranchiseSell_Id);
            // DependencyService.Get<Interface.IDownloadManager>().Main("http://www.theshirtshop.in/TemplateView/invoice_customer.html?sellId=" + ps.FranchiseSell_Id);
        }

      
    }
}
