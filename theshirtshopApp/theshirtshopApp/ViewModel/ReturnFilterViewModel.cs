using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Reflection;
using System.Threading.Tasks;
using theshirtshopApp.BAL;
using theshirtshopApp.Validation;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace theshirtshopApp.ViewModel
{
    
    
    
    public class ReturnFilterViewModel: INotifyPropertyChanged
    {



        public ReturnFilterViewModel(InvoiceFilterModel filter) {

            FromDate = ToDate =System.DateTime.Now;
            MinFromDate = MinToDate = System.DateTime.Now.AddYears(-5);
            

            if (filter == null)
            {
                filterModel = new InvoiceFilterModel()
                {
                    Amount = "",
                    ArticleNo = "",
                    CurrentPage = "1",
                    FranchiseId = Application.Current.Properties["OtherId"].ToString(),
                    FromDate = "",
                    SearchBy = "",
                    SortDirection = "",
                    PageSize = "",
                    SortExpression = "FranchiseSellId",
                    Status = "",
                    ToDate = ""
                };
            }
            else {
                filterModel = filter;
            }
            OrderTypes = new ObservableCollection<string>()
            { "RETURNED" ,"REFUNDED", "PARTIAL REFUNDED","PARTIAL RETURNED", "SOLD"};
            TimeOptions = new ObservableCollection<string>()
            { "Last 10 Days", "Last 30 Days", "Last 6 Months", System.DateTime.Today.Year.ToString(), (System.DateTime.Today.Year - 1).ToString(), "Custom" };
            FilterOptions = new ObservableCollection<string>
            { "Article No.", "Amount","Time", "Status" };
            isCustomDate = false;
        }


        private DateTime _minToDate;
        public DateTime MinToDate { get { return _minToDate; } set { _minToDate = value; OnPropertyChanged(); } }

        private DateTime _minFromDate;
        public DateTime MinFromDate { get { return _minFromDate; } set { _minFromDate = value; OnPropertyChanged(); } }

        private DateTime _ToDate;
        public DateTime ToDate { get { return _ToDate; } set { _ToDate = value; OnPropertyChanged(); } }

        private DateTime _FromDate;
        public DateTime FromDate { get { return _FromDate; } set { _FromDate = value; OnPropertyChanged(); } }


        public Command LoadItemsCommand { get; set; }
        ObservableCollection<string> _orderTypes;
        public ObservableCollection<string> OrderTypes
        {
            get
            {
                return _orderTypes;
            }
            set
            {
                _orderTypes = value;
                OnPropertyChanged();
            }
        }
       

        ObservableCollection<string> _timeOptions;
        public ObservableCollection<string> TimeOptions
        {
            get
            {
                return _timeOptions;
            }
            set
            {
                _timeOptions = value;
                OnPropertyChanged();
            }
        }

        ObservableCollection<string> _filterOption;
        public ObservableCollection<string> FilterOptions
        {
            get
            {
                return _filterOption;
            }
            set
            {
                _filterOption = value;
                OnPropertyChanged();
            }
        }

        bool _isFilterOneSelected;
        public bool isFilterOneSelected
        {

            get { return _isFilterOneSelected; }
            set
            {
                _isFilterOneSelected = value;
                OnPropertyChanged();
            }


        }

        bool _isFiltertwoSelected;
        public bool isFiltertwoSelected
        {


            get { return _isFiltertwoSelected; }
            set
            {
                _isFiltertwoSelected = value;
                OnPropertyChanged();
            }


        }
        bool _isFilterThreeSelected;
        public bool isFilterThreeSelected
        {


            get { return _isFilterThreeSelected; }
            set
            {
                _isFilterThreeSelected = value;
                OnPropertyChanged();
            }


        }
        bool _isFilterFourSelected;
        public bool isFilterFourSelected
        {


            get { return _isFilterFourSelected; }
            set
            {
                _isFilterFourSelected = value;
                OnPropertyChanged();
            }


        }
        
        InvoiceFilterModel _filterModel;
        public InvoiceFilterModel filterModel
        {

            get { return _filterModel; }
            set
            {

                _filterModel = value;
                OnPropertyChanged();
            }
        }
        bool _isCustomDate;
        public bool isCustomDate
        {

            get { return _isCustomDate; }
            set
            {

                _isCustomDate = value;
                OnPropertyChanged();
            }


        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
