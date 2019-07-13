using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace theshirtshopApp.BAL
{
  public  class StockMaster_Class
    {
        public StockMaster_Class()
        {
            _CategoryMaster_Class = new List<CategoryMaster_Class>();
            SubCategoryMaster_Class_Data = new SubCategoryMaster_Class();
        }
        public int Stock_Id { get; set; }

        public int Article_Id { get; set; }
        public int SubCategory_Id { get; set; }
        public string Article_PrimaryImage { get; set; }
        public string Article_SecondaryImage { get; set; }
        public string Article_No { get; set; }
        public string Color_Name { get; set; }
        public decimal MRP { get; set; }
        public decimal Selling_Price { get; set; }
        public string Description { get; set; }
        public List<CategoryMaster_Class> _CategoryMaster_Class { get; set; }
        public int Order_Id { get; set; }
        public string SubCategory_Name { get; set; }
        public string Category_Name { get; set; }
        public int? Enter_Quantity { get; set; }
        public int Insert_New_Qty { get { return InsertNewQty; } set { InsertNewQty = value; } }

        public int Remove_Quantity { get; set; }
        public int Total_Quantity { get; set; }
        public int? Quantity { get; set; }
        public Boolean status { get; set; }
        public DateTime Created_Date { get; set; }
        public int Created_By { get; set; }
        //pankaj code
        public SubCategoryMaster_Class SubCategoryMaster_Class_Data { get; set; }
        public int InsertNewQty = 0;
    }
}
