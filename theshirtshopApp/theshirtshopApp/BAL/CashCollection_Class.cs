using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace theshirtshopApp.BAL
{
  public  class CashCollection_Class
    {
        public int EmpCashCollectionId { get; set; }

        public int EmployeeId { get; set; }

        public int Collected_CashAmount { get; set; }

        public int? ReceivedAmount { get; set; }

        public int Retailer_Id { get; set; }
        public bool Status { get; set; }

        public string Action { get; set; }



        public DateTime? Created_Date { get; set; }

        public DateTime? Received_Date { get; set; }

        public int? Received_By { get; set; }


        public string Emp_FirstName { get; set; }

        public string Retailer_Address { get; set; }
        public string Retailer_Name { get; set; }


        public string Retailer_MobileNo { get; set; }

        public string Cash_Date { get; set; }
    }
}
