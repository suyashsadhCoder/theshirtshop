using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using theshirtshopApp.BAL;
using theshirtshopApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace theshirtshopApp.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class InvoiceDetailsSubmit : ContentPage
    {
        public InvoiceDetailsSubmitViewModel viewModel;
        public InvoiceDetailsSubmit()
        {
            InitializeComponent();

        }
        public InvoiceDetailsSubmit(InvoiceDetailModel model) {
            InitializeComponent();
            this.BindingContext = viewModel = new InvoiceDetailsSubmitViewModel(model, this.Navigation);
        }

       

        private void cancel_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }
    }
}