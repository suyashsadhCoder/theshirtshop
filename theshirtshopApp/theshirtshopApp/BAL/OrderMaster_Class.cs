using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace theshirtshopApp.BAL
{
    public class OrderMaster_Class
    {
      
        public int Order_Id { get; set; }
        public int Franchise_Id { get; set; }

        public string Invoice_No { get; set; }

        public decimal Total_Amount { get; set; }
        public int Total_Item { get; set; }
        public string Action { get; set; }
        public string Feedback { get; set; }



        public int Created_By { get; set; }

        public DateTime Created_Date { get; set; }
        public int Modified_By { get; set; }

        public DateTime Modified_Date { get; set; }

        public bool Status { get; set; }

        public ObservableCollection<OrderDetailsMaster_Class> OrderDetailsMaster_Class_List { get; set; }

        public FranchiseOrderDispatchClass FranchiseOrderDispatchClass_Data { get; set; }

        public ObservableCollection<OrderDetailsMaster_Class> odm { get; set; }
        public OrderDispatch_Class OrderDispatch_Class_Data_ { get; set; }



    }


}
