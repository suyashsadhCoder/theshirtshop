using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using theshirtshopApp.BAL;
using theshirtshopApp.Validation;

namespace theshirtshopApp.ViewModel
{
    public class OrderInvoiceDetailPageViewModel : ModelBase
    {

        public OrderInvoiceDetailPageViewModel(OrderMaster_Class _OrderMaster_Class)
        {

            _OCD = _OrderMaster_Class;
        }
        public OrderMaster_Class _OCD
        {
            get { return OCD; } 
                set { OCD = value;
                NotifyPropertyChanged();
            }
        }

        public OrderMaster_Class OCD { get; set; }
    }
}
