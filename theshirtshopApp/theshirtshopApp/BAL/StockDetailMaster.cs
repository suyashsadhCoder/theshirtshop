using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using theshirtshopApp.DAL;

namespace theshirtshopApp.BAL
{
    public class StockDetailMaster
    {
       
        public string ProductName  = NullConstants.NullString;
        public string ArticleNo = NullConstants.NullString;
        public string ProductColor = NullConstants.NullString;
        public string CtegoryName = NullConstants.NullString;
        public string ProductPrice = NullConstants.NullString;
        public int SubCategoryId = NullConstants.NullInt;
        public string ProductPhoto = NullConstants.NullString;
       

        //  public List<SubCategoryMaster> SubCategoryMasterList = null;

        public string Product_Name { get { return ProductName; }set { ProductName = value; } }
        public string Article_No { get { return ArticleNo; } set { ArticleNo = value; } }
        public string Product_Color { get { return ProductColor; } set { ProductColor = value; } }
        public string Ctegory_Name { get { return CtegoryName; } set { CtegoryName = value; } }
        public string Product_Price { get { return ProductPrice; } set { ProductPrice = value; } }
        public int Sub_Category_Id { get { return SubCategoryId; } set { SubCategoryId = value; } }
        public string Product_Photo { get { return ProductPhoto; } set { ProductPhoto = value; } }
        public IList<SubCategoryMaster_Class> Sub_Category_Master_List { get; set; }

      

    }
}
