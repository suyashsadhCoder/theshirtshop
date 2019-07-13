using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using theshirtshopApp.BAL;
using theshirtshopApp.Controls;
using theshirtshopApp.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace theshirtshopApp.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ReturnRefundFilterView : ContentPage
    {

        public ReturnFilterViewModel viewModel;

        public ReturnRefundFilterView(InvoiceFilterModel filter)
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            
            this.BindingContext = viewModel = new ReturnFilterViewModel(filter);
            viewModel.isFilterOneSelected = true;
            lstView.ItemTapped += LstView_ItemTapped;
            timeTypePicker.SelectedItem = Application.Current.Properties["TimePicker"];
            orderTypePicker.SelectedItem = Application.Current.Properties["ordertypepicker"];

            timeTypePicker.SelectedIndexChanged += TimeTypePicker_SelectedIndexChanged;
            orderTypePicker.SelectedIndexChanged += OrderTypePicker_SelectedIndexChanged;
        }

        private void OrderTypePicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            var orderselected = sender as Picker;
            viewModel.filterModel.Status = orderselected.SelectedItem.ToString();
        }

        private void TimeTypePicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            var picker = sender as Picker;
           updateDateValue(picker.SelectedIndex);
        }

     
        private void LstView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            updateFilterUI(e.Item as string);
        }

        void Handle_ToDateSelected(object sender, Xamarin.Forms.DateChangedEventArgs e)
        {
            if(e.NewDate > System.DateTime.Now.AddYears(-5))
            viewModel.ToDate = e.NewDate;
        }
        void Handle_FromDateSelected(object sender, Xamarin.Forms.DateChangedEventArgs e)
        {
            if (e.NewDate > System.DateTime.Now.AddYears(-5))
                viewModel.FromDate = e.NewDate;
        }
        private void updateFilterUI(string str)
        {
            var index = (lstView.ItemsSource as ObservableCollection<string>).IndexOf(str);
           // viewModel.filterModel.SearchBy = str;
            switch (index)
            {
                case 0:
                   
                    viewModel.isFilterOneSelected = true;
                    viewModel.isFiltertwoSelected = false;
                    viewModel.isFilterThreeSelected = false;
                    viewModel.isFilterFourSelected = false;
                    break;
                case 1:
                    viewModel.isFilterOneSelected = false;
                    viewModel.isFiltertwoSelected = true;
                    viewModel.isFilterThreeSelected = false;
                    viewModel.isFilterFourSelected = false;
                    break;
                case 2:
                    viewModel.isFilterOneSelected = false;
                    viewModel.isFiltertwoSelected = false;
                    viewModel.isFilterThreeSelected = true;
                    viewModel.isFilterFourSelected = false;
                    break;
                case 3:
                    viewModel.isFilterOneSelected = false;
                    viewModel.isFiltertwoSelected = false;
                    viewModel.isFilterThreeSelected = false;
                    viewModel.isFilterFourSelected = true;
                    break;
                default:
                    
                    break;
            }


        }
        private void updateDateValue(int str)
        {
            viewModel.isCustomDate = false;
            switch (str)
            {
                case 0:
                    {
                        viewModel.filterModel.FromDate = System.DateTime.Now.AddDays(-10).ToString("yyyy-MM-dd");
                        viewModel.filterModel.ToDate = System.DateTime.Now.ToString("yyyy-MM-dd");

                    }
                    break;
                case 1:
                    {
                        viewModel.filterModel.FromDate = System.DateTime.Now.AddDays(-30).ToString("yyyy-MM-dd");
                        viewModel.filterModel.ToDate = System.DateTime.Now.ToString("yyyy-MM-dd");

                    }
                    break;
                case 2:
                    {
                        viewModel.filterModel.FromDate = System.DateTime.Now.AddMonths(-6).ToString("yyyy-MM-dd");
                        viewModel.filterModel.ToDate = System.DateTime.Now.ToString("yyyy-MM-dd");

                    }
                    break;
                case 3:
                    {
                        viewModel.filterModel.FromDate = (System.DateTime.Now.Year - 1).ToString() + "-01-01";
                        viewModel.filterModel.ToDate = System.DateTime.Now.ToString("yyyy-MM-dd");

                    }
                    break;
                case 4:
                    {
                        viewModel.filterModel.FromDate =  (System.DateTime.Now.Year-1).ToString()+ "-01-01";
                        viewModel.filterModel.ToDate =  (System.DateTime.Now.Year- 1).ToString()+ "-12-31";

                    }
                    break;
                case 5:
                    {
                        viewModel.isCustomDate = true;
                        viewModel.filterModel.FromDate = viewModel.FromDate.ToString("yyyy-MM-dd");
                        viewModel.filterModel.ToDate = viewModel.ToDate.ToString("yyyy-MM-dd");
                    }
                    break;
                default:

                    break;
            }
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            Application.Current.Properties["isChanged"] = "Y";
            Application.Current.Properties["Amount"] = viewModel.filterModel.Amount;
            Application.Current.Properties["ArticleNo"] = viewModel.filterModel.ArticleNo;
            Application.Current.Properties["FromDate"] = viewModel.filterModel.FromDate;
            Application.Current.Properties["SearchBy"] = viewModel.filterModel.SearchBy;
            Application.Current.Properties["SortDirection"] = viewModel.filterModel.SortDirection;
            Application.Current.Properties["PageSize"] = viewModel.filterModel.PageSize;
            Application.Current.Properties["SortExpression"] = viewModel.filterModel.SortExpression;
            Application.Current.Properties["Status"] = viewModel.filterModel.Status;
            Application.Current.Properties["ToDate"] = viewModel.filterModel.ToDate;
            Application.Current.Properties["TimePicker"] = timeTypePicker.SelectedItem.ToString();
            Application.Current.Properties["ordertypepicker"] = orderTypePicker.SelectedItem.ToString();
            Navigation.PopAsync();
        }
        private void Button_resetClicked(object sender, EventArgs e) {
            Application.Current.Properties["isChanged"] = "Y";
            Application.Current.Properties["Amount"] = "";
            Application.Current.Properties["ArticleNo"] = "";
            Application.Current.Properties["FromDate"] = "";
            Application.Current.Properties["SearchBy"] = "";
            Application.Current.Properties["SortDirection"] = "DESC";
            Application.Current.Properties["PageSize"] = "50";
            Application.Current.Properties["SortExpression"] = "FranchiseSellId";
            Application.Current.Properties["Status"] = "";
            Application.Current.Properties["ToDate"] = "";
            Application.Current.Properties["TimePicker"] = "";
            Application.Current.Properties["ordertypepicker"] = "";
            Navigation.PopAsync();

        }
    }
}