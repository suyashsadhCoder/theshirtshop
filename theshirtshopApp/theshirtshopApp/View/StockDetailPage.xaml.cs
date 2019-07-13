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
    public partial class StockDetailPage : ContentPage
    {
        public StockDetailPage()
        {
            if (Application.Current.Properties.ContainsKey("Key"))
            {
                InitializeComponent();
                 BindingContext = new StockDetailViewModel();
                MsgResul.IsVisible = false;
            }
            else
            {
                Navigation.PushModalAsync(new MainPage());
            }
         
        }

        private void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var vm = BindingContext as StockDetailViewModel;
                   var article = e.Item as ArticleMaster_Class;
                   vm.SelectTab=article;
        }

        private void ListView_Refreshing(object sender, EventArgs e)
        {
            var vm = BindingContext as StockDetailViewModel;
           // var article = e.Item as ArticleMaster_Class;
           vm.GetListAsync();
           Mainlist.IsRefreshing = false;
        }

        private void SearchBar_SearchButtonPressed(object sender, EventArgs e)
        {
            FilterNames();
        }

        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            FilterNames();
        }
        private void FilterNames()
        {
            string filter = StockSearchBar.Text;
            var vm = BindingContext as StockDetailViewModel;
            // OrderMasterClassList.BeginRefresh();
            if (string.IsNullOrWhiteSpace(filter))
            {
                Mainlist.ItemsSource = vm.Main_List;
                MsgResul.IsVisible = false;
            }
            else
            {
                var sm = vm.Main_List.Where(x => 
                    x.Article_No.ToLower().Contains(filter.ToLower())||
                    x.CategoryMaster_Class_Data.Category_Name.ToLower().Contains(filter.ToLower())||
                    x.Selling_Price.Equals(filter)||
                    x.MRP.Equals(filter));
                Mainlist.ItemsSource = sm;
                if (sm.ToList().Count == 0)
                {
                    MsgResul.IsVisible = true;
                }
                else
                {
                    MsgResul.IsVisible = false;
                }
            }
            //   OrderMasterClassList.EndRefresh();
        }
    }
}