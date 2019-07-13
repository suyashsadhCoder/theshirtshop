using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace theshirtshopApp.BAL
{
    public class CustomerReturnViewItem
    {
        public string OrderReturnId { get; set; }
        public string customername { get; set; }
        public string TotalQty { get; set; }
        public string Amount { get; set; }
        public string ReturnStatus { get; set; }
        public string CreatedDate { get; set; }
        public string IsCreditNoteUsed { get; set; }

        public string totalAmoutInRupees {

            get
            {
                decimal parsed = decimal.Parse(Amount!=null? Amount:"0.00", CultureInfo.InvariantCulture);
                CultureInfo hindi = new CultureInfo("hi-IN");
                return string.Format(hindi, "{0:c}", parsed);
            }
        }
    }
}
