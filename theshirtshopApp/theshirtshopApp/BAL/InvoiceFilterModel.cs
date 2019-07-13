using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace theshirtshopApp.BAL
{
   

    public class InvoiceFilterModel: INotifyPropertyChanged
    {

        
        string _fromDate;
        string _toDate;



        string _searchBy;
        public string SearchBy
        {
            get
            {
                return _searchBy;
            }
            set
            {
                _searchBy = value;
                OnPropertyChanged(); ;
            }
        }
        public string FromDate { get { return _fromDate; } set { _fromDate = value; OnPropertyChanged(); } }
        public string ToDate { get { return _toDate; } set { _toDate = value; OnPropertyChanged(); } }
        public string ArticleNo { get; set; }
        public string OrderId { get; set; }
        public string CurrentPage { get; set; }
        public string PageSize { get; set; }
        public string SortDirection { get; set; }
        public string SortExpression { get; set; }
        public string Status { get; set; }
        public string FranchiseId { get; set; }
        public string Amount { get; set; }

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
