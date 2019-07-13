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
    public partial class AllStockRequestPage : ContentPage
    {
        public AllStockRequestPage()
        {
            if (Application.Current.Properties.ContainsKey("Key"))
            {
                InitializeComponent();
                BindingContext = new AllStockRequestPageViewModel(Navigation);
                MsgResult.IsVisible = false;
            }
            else
            {
                Navigation.PushModalAsync(new MainPage());
            }


        }

        private void FranchiseRequestClassList_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var item = e.Item as FranchiseRequest_Class;
            Navigation.PushAsync(new StockRequestDetailPage(item), true);
        }

        private void FranchiseRequestClassList_Refreshing(object sender, EventArgs e)
        {
            var vm = BindingContext as AllStockRequestPageViewModel;
            vm.GetData();

            FranchiseRequestClassList.IsRefreshing = false;
        }

        private void RequestSearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            RequestListFilter();
        }

        private void RequestSearchBar_SearchButtonPressed(object sender, EventArgs e)
        {
            RequestListFilter();

        }

        private void RequestListFilter()
        {
            string filter = RequestSearchBar.Text;
            var vm = BindingContext as AllStockRequestPageViewModel;
            //FranchiseSellClassList.BeginRefresh();
            if (string.IsNullOrWhiteSpace(filter))
            {
                FranchiseRequestClassList.ItemsSource = vm._FranchiseRequest_Class_List;
                MsgResult.IsVisible = false;
            }
            else
            {
                var sm = vm._FranchiseRequest_Class_List.Where(x => x.Requested_Date.ToString("dd-MM-yyyy").Contains(filter.ToLower())
                   || x.Title.ToLower().Contains(filter.ToLower()) || x.Description.ToLower().Contains(filter.ToLower()));
                FranchiseRequestClassList.ItemsSource = sm;
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
    }
}