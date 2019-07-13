using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public partial class GoodReturnDetailPage : ContentPage
    {
        public GoodReturnDetailPage(FranchiseGoodReturn_Class fc, ObservableCollection<ArticleMaster_Class> ac)
        {
            
            if (Application.Current.Properties.ContainsKey("Key"))
            {
                InitializeComponent();
                BindingContext = new GoodReturnDetailPageViewModel(fc, ac, Navigation);

            }
            else
            {
                Navigation.PushModalAsync(new MainPage());
            }
        }

        private void mdr_Completed(object sender, EventArgs e)
        {
            remark.Focus();
        }
    }
}