using Acr.UserDialogs;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using theshirtshopApp.BAL;
using theshirtshopApp.Interface;
using theshirtshopApp.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace theshirtshopApp.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OrderInvoiceDetailPage : ContentPage
    {
        public ObservableCollection<string> Items { get; set; }
        private OrderMaster_Class ocd;
        public OrderInvoiceDetailPage(OrderMaster_Class OrderMaster_Class_Data)
        {
            if (Application.Current.Properties.ContainsKey("Key"))
            {
                InitializeComponent();
                BindingContext = new OrderInvoiceDetailPageViewModel(OrderMaster_Class_Data);
                if (!string.IsNullOrEmpty(OrderMaster_Class_Data.Invoice_No))
                {
                    ocd = OrderMaster_Class_Data;
                }
            }
            else
            {
                Navigation.PushModalAsync(new MainPage());
            }
        }

        private  void ViewPdf_Clicked(object sender, EventArgs e)
        {
            DependencyService.Get<Interface.IDownloadManager>().Main("http://www.theshirtshop.in/TemplateView/invoice.html?Id=" + ocd.Order_Id);
        }
    }
}
