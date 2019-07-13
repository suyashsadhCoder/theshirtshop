using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace theshirtshopApp.BAL
{
  public  class ProductMaster
    {
        public ProductMaster()
        {
            this.SubCategoryMasterList = new ObservableCollection<SubCategoryMaster_Class>();
            // this.GetProduct = new ObservableCollection<ProductMaster>();
        }
        public string Img1 { get; set; }
        public string Img2 { get; set; }
        public string ArticleNo { get; set; }
        public string Category { get; set; }
        public string Color { get; set; }
        public string Discription { get; set; }
        public string MRP { get; set; }
       
        public ObservableCollection<SubCategoryMaster_Class> SubCategoryMasterList { get; set; }

      
      
    }
}
