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
	public partial class AllGoodsReturnDetailPage : ContentPage
	{
        public FranchiseGoodReturn_Class ocd;

        public AllGoodsReturnDetailPage (FranchiseGoodReturn_Class fc)
		{
			
            if (Application.Current.Properties.ContainsKey("Key"))
            {

                InitializeComponent();
                ocd = new FranchiseGoodReturn_Class();
                ocd = fc;
                if (fc.Action == "Accepted")
                {
                    ViewPdf.IsVisible = true;
                }
                BindingContext = new AllGoodsReturnDetailPageViewModel(fc);
            }
            else
            {
                Navigation.PushModalAsync(new MainPage());
            }
        }
        private void ViewPdf_Clicked(object sender, EventArgs e)
        {
            DependencyService.Get<Interface.IDownloadManager>().Main("http://www.theshirtshop.in/TemplateView/order-return-invoice.html?Id=" + ocd.Franchise_OrderReturnId);
        }
    }
}