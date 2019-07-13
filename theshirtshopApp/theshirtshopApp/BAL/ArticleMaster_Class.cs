using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace theshirtshopApp.BAL
{
    public class ArticleMaster_Class
    {
        public ArticleMaster_Class()
        {
            CategoryMaster_Class_Data = new CategoryMaster_Class();
            StockMaster_Class_Lsit = new List<StockMaster_Class>();
            OrderDetailsMaster_Class_Data = new OrderDetailsMaster_Class();
            FranchiseStokeMaster_Class_List = new ObservableCollection<StockMaster_Class>();
            
        }
        public double fixrate = 0; 
        public int Article_Id { get; set; }
        public int Category_Id { get; set; }
        public string Article_No { get; set; }
        public string Color { get; set; }
        public decimal MRP { get; set; }
        public double Selling_Price { get; set; }
        public string Article_PrimaryImage { get; set; }
        public string Article_SecondaryImage { get; set; }
        public string Description { get; set; }

        public int Created_By { get; set; }

        public DateTime Created_Date { get; set; }
        public int Modified_By { get; set; }

        public DateTime Modified_Date { get; set; }

        public bool Status { get; set; }
        public double FixRate { get { return fixrate; }
            set { fixrate = value; }
        }
        public CategoryMaster_Class CategoryMaster_Class_Data { get; set; }
        public List<StockMaster_Class> StockMaster_Class_Lsit { get; set; }
        public OrderDetailsMaster_Class OrderDetailsMaster_Class_Data { get; set; }
        public ObservableCollection<StockMaster_Class> FranchiseStokeMaster_Class_List { get; set; }

    }
        


    }

