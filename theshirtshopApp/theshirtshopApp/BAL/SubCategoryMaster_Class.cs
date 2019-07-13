using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace theshirtshopApp.BAL
{
  public  class SubCategoryMaster_Class
    {

        public int SubCategory_Id { get; set; }

        public int Category_Id { get; set; }

        public string SubCategory_Name { get; set; }

        public int Created_By { get; set; }

        public DateTime Created_Date { get; set; }
        public int Modified_By { get; set; }

        public DateTime Modified_Date { get; set; }

        public bool Status { get; set; }

        public StockMaster_Class StockMaster_Class_Data { get; set; }
        public OrderDetailsMaster_Class OrderDetailsMaster_Class_Data { get; set; }
        //public FranchiseSellDetails_Class FranchiseSellDetails_Class_Data { get; set; }
        //public FranchiseOrderReturnDetails_Class _FranchiseOrderReturnDetails_Data { get; set; }
        //public FranchiseStokeMaster_Class FranchiseStokeMaster_Class_Data { get; set; }
    }
}
