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
    public partial class AllGoodsReturnPage : ContentPage
    {
        public AllGoodsReturnPage()
        {

            if (Application.Current.Properties.ContainsKey("Key"))
            {
                InitializeComponent();
                BindingContext = new AllGoodsReturnPageViewModel(Navigation);
                MsgResult.IsVisible = false;
            }
            else
            {
                Navigation.PushModalAsync(new MainPage());
            }

        }

        private void GoodsReturnSearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            GoodsReturnListFilter();
        }

        private void GoodsReturnSearchBar_SearchButtonPressed(object sender, EventArgs e)
        {
            GoodsReturnListFilter();

        }
        private void GoodsReturnListFilter()
        {
            string filter = GoodsReturnSearchBar.Text;
            var vm = BindingContext as AllGoodsReturnPageViewModel;
            //FranchiseSellClassList.BeginRefresh();
            if (string.IsNullOrWhiteSpace(filter))
            {
                GoodsReturnList.ItemsSource = vm._GoodsReturn_Class_List;
                MsgResult.IsVisible = false;
            }
            else
            {
                var sm = vm._GoodsReturn_Class_List.Where(x => x.Created_Date.ToString("dd-MM-yyyy").Contains(filter.ToLower())
                   || x.Invoice_No.ToLower().Contains(filter.ToLower()) || x.Total_Item.ToString().Contains(filter.ToLower()));
                GoodsReturnList.ItemsSource = sm;
                if (sm.ToList().Count == 0)
                {
                    MsgResult.IsVisible = true;
                }
                else
                {
                    MsgResult.IsVisible = false;
                }
            }
        }
        private void GoodsReturnList_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var item = e.Item as FranchiseGoodReturn_Class;
            Navigation.PushAsync(new AllGoodsReturnDetailPage(item), true);
        }

        private void GoodsReturnList_Refreshing(object sender, EventArgs e)
        {
            var vm = BindingContext as AllGoodsReturnPageViewModel;
            vm.GetData();

            GoodsReturnList.IsRefreshing = false;
        }
    }
}