using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using theshirtshopApp.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace theshirtshopApp.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ReturnRefundDetailView : ContentPage
    {
        ReturnRefundDetailViewModel viewModel;

        public ReturnRefundDetailView( string orderid)
        {
            InitializeComponent();
            this.BindingContext = viewModel = new ReturnRefundDetailViewModel(orderid);
        }
        public ReturnRefundDetailView() {
            InitializeComponent();

        }
    }
}