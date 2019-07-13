using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace theshirtshopApp.BAL
{
    public class FranchiseGoodReturn_Class
    {
        public int Franchise_OrderReturnId { get; set; }
        public int FranchiseOrderReturnDetail_Id { get; set; }
        public int Franchise_Id { get; set; }
        public int SubCategory_Id { get; set; }
        public int FranchiseStock_Id { get; set; }
        public string Franchise_Code { get; set; }
        public string Franchise_Name { get; set; }
        public string Invoice_No { get; set; }
        public int Total_Item { get; set; }

        public decimal Total_Amount { get; set; }
        public string Mod_Of_Return { get; set; }
        public string Remark { get; set; }
        public string Action { get; set; }
        public string Article_No { get; set; }
        public string Category { get; set; }

        public int Created_By { get; set; }

        public DateTime Created_Date { get; set; }
        public int Modified_By { get; set; }

        public DateTime Modified_Date { get; set; }

        public bool Status { get; set; }
        public int Actual_Qty { get; set; }
        public int Stock_Qty { get; set; }
        public int Reject_Qty { get; set; }
        public int Miss_Qty { get; set; }
      public ObservableCollection<FranchiseOrderReturnDetails_Class> goodreturnDetail { get; set; }
      public List<StockMaster_Class> _FranchiseStokeMaster_Class_List { get; set; }
      public int Article_Id { get; set; }
    }

    public class FranchiseOrderReturnDetails_Class
    {
        public int FranchiseOrderReturnDetail_Id { get; set; }
        public int FranchiseOrderReturn_Id { get; set; }
        public int FranchiseStock_Id { get; set; }
        public int Quantity { get; set; }
        public string Subcategory_Name { get; set; }
        public int Rejected_Quantity { get; set; }
        public int Marketable_Quantity { get; set; }

        public int Subcategory_Id { get; set; }
        public int? Article_Id { get; set; }
        public int? Stock_Qty { get; set; }
        public int? Reject_Qty { get; set; }
        public int? Miss_Qty { get; set; }

        public ArticleMaster_Class ArticleMaster_Class_Data { get; set; }

        public CategoryMaster_Class CategoryMaster_Class_Data { get; set; }

        public List<StockMaster_Class> _FranchiseStokeMaster_Class_List { get; set; }
    }
}
