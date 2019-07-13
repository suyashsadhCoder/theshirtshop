using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace theshirtshopApp.BAL
{

   
     public class InvoiceDataModel
    {
       public string Sno { get; set; }
        public string FranchiseSellId { get; set;}
        public string CustomerName { get; set; }
        public string ContactNo { get; set;}
        public string InvoiceNo { get; set; }
        public string TotalAmount { get; set;}
        public DateTime CreatedDate { get; set; }
        
        public string SellStatus { get; set; }
        public string customerAndMobileNumber { get { return ContactNo + " (" + CustomerName+")"; } }
        public string InvoiceDate { get { return CreatedDate.ToString("dd/MM/yyyy"); } }
        public string totalAmoutInRupees
        {
            get {
                decimal parsed = decimal.Parse(TotalAmount, CultureInfo.InvariantCulture);
                CultureInfo hindi = new CultureInfo("hi-IN");
                return string.Format(hindi, "{0:c}", parsed);
            }
        }
                  


    }
}
