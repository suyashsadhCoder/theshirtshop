using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace theshirtshopApp.BAL
{
    public class CategoryMaster_Class
    {
        

        public int Category_Id { get; set; }

        public string Category_Name { get; set; }

        public int Created_By { get; set; }

        public DateTime Created_Date { get; set; }
        public int Modified_By { get; set; }

        public DateTime Modified_Date { get; set; }

        public bool Status { get; set; }

        public ObservableCollection<SubCategoryMaster_Class> SubCategoryMaster_List { get; set; }
        public ObservableCollection<SubCategoryMaster_Class> SubCategoryMasterList { get; set; }
        public SubCategoryMaster_Class SubCategoryMaster_Class_Data { get; set; }
        public SubCategoryMaster_Class SubCategoryMaster_Data { get; set; }
    }


}
