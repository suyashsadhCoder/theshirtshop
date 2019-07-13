using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace theshirtshopApp.BAL
{
 public   class RetailerOrder_Class
    {
        public int Order_Id { get; set; }
        public int Emp_Id { get; set; }
        public string Emp_Name { get; set; }
        public string Emp_Code { get; set; }
        public string Retailer_Name { get; set; }

        public int Retailer_Id { get; set; }
        public int Info_ID { get; set; }
        public string Invoice_No { get; set; }
        public double Total_Amount { get; set; }
        public double OutStanding_Amount { get; set; }
        public int Total_Item { get; set; }
        public string Action { get; set; }
        public string InvoiceStatus { get; set; }
        public string _Feedback { get; set; }
        public string _Remark { get; set; }
        public string Preferred_Transport { get; set; }
        public DateTime Date_Of_Dispatch { get; set; }

        public Boolean _Status { get; set; }
        public Boolean _CreatedBy { get; set; }
        public DateTime _CreatedDate { get; set; }
        public int _ModifiedBy { get; set; }
        public DateTime _ModifiedDate { get; set; }

        public List<OrderDetailsMaster_Class> odm { get; set; }

       
    }
}
