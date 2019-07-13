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
    public partial class InvoiceDetailPage : ContentPage
    {

        public InvoiceDetailViewModel viewModel;
        public InvoiceDetailPage(string invoiceID)
        {
            InitializeComponent();
            BindingContext = this.viewModel = new InvoiceDetailViewModel(invoiceID, this.Navigation);
        }
        void Handle_increaseReturn(object sender, System.EventArgs e)
        {
            viewModel.invoiceDetail.articleList[Convert.ToInt32((sender as Button).CommandParameter)].increaseReturn();

        }
        void Handle_decreaseReturn(object sender, System.EventArgs e)
        {
            viewModel.invoiceDetail.articleList[Convert.ToInt32((sender as Button).CommandParameter)].decreaseReturn();
        }
        void Handle_increaseDamage(object sender, System.EventArgs e)
        {
            viewModel.invoiceDetail.articleList[Convert.ToInt32((sender as Button).CommandParameter)].increaseDamage();
        }

        void Handle_decreaseDamage(object sender, System.EventArgs e)
        {
            viewModel.invoiceDetail.articleList[Convert.ToInt32((sender as Button).CommandParameter)].decreaseDamage();
        }


        void Handle_CancelClicked(object sender, System.EventArgs e)
        {
            Navigation.PopAsync();
        }

       
    }
}