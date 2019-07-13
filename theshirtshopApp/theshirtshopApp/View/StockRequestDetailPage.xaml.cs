using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using theshirtshopApp.BAL;
using theshirtshopApp.ViewModel;

namespace theshirtshopApp.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class StockRequestDetailPage : ContentPage
	{
        public StockRequestDetailPage(FranchiseRequest_Class franchiseRequest_Class)
		{
            if (Application.Current.Properties.ContainsKey("Key"))
            {
                InitializeComponent();
                BindingContext = new StockRequestDetailPageViewModel(franchiseRequest_Class);
            }
            else
            {
                Navigation.PushModalAsync(new MainPage());
            }
          

        }
	}
}