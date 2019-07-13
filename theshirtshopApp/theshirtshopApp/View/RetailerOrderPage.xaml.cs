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
    public partial class RetailerOrderPage : ContentPage
    {
        public RetailerOrderPage()
        {
            if (Application.Current.Properties.ContainsKey("Key"))
            {
                InitializeComponent();
                BindingContext = new RetailerOrderPageViewModel(Navigation); ;
              
            }
            else
            {
                Navigation.PushModalAsync(new MainPage());
            }
        }

        private void SelectedProductMasterList_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var vm = BindingContext as RetailerOrderPageViewModel;
            var article = e.Item as ArticleMaster_Class;
            vm.SelectArticleMasterClassList = article;
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            var img = sender as Image;
           //Uri uri = new Uri(img.Source.ToString());
            Navigation.PushAsync(new ZoomPage(img.Source));
        }

        private void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
        {
            var img = (Image)sender;
            //Uri uri = new Uri(img.Source.ToString());
            Navigation.PushAsync(new ZoomPage(img.Source));

        }
    }
}