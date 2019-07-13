using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace theshirtshopApp.BAL
{
  public  class OrderDetailsMaster_Class
    {
       
        public int OrderDetail_Id { get; set; }
        public int Order_Id { get; set; }
        public int Article_Id { get; set; }
        public int SubCategory_Id { get; set; }
        public int? Quantity { get; set; }
        public decimal MRP { get; set; }
        public decimal SellPrice { get; set; }

        public ArticleMaster_Class ArticleMaster_Class_Data { get; set; }
        public SubCategoryMaster_Class SubCategoryMaster_Class_Data { get; set; }
        public OrderMaster_Class OrderMaster_Class_Data { get; set; }
        public CategoryMaster_Class CategoryMaster_Class_Data { get; set; }
        public List<SubCategoryMaster_Class> SubCategoryMaster_Class_List { get; set; }
    }
}
