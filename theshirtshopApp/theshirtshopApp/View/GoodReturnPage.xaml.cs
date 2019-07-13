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
    public partial class GoodReturnPage : ContentPage
    {
        public GoodReturnPage()
        {
            if (Application.Current.Properties.ContainsKey("Key"))
            {
                InitializeComponent();
                BindingContext = new GoodReturnViewModel(Navigation);
            }
            else
            {
                Navigation.PushModalAsync(new MainPage());
            }
        }
        private void SelectedProductMasterList_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var vm = BindingContext as GoodReturnViewModel;
            var article = e.Item as ArticleMaster_Class;
            vm.SelectArticleMasterClassList = article;
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            var item = (Xamarin.Forms.Button)sender;

            var vm = BindingContext as GoodReturnViewModel;
            vm?.DeleteItemInListCommand.Execute(item.CommandParameter);
        }
    }
}