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
	public partial class CashCollectionPage : ContentPage
	{
		public CashCollectionPage ()
		{
            if (Application.Current.Properties.ContainsKey("Key"))
            {
                InitializeComponent();
                BindingContext = new CashCollectionPageViewModel(Navigation);
            }
            else
            {
                Navigation.PushModalAsync(new MainPage());
            }
        }
	}
}