using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace theshirtshopApp.BAL
{
    public class Employee_OrderGenerate_Class
    {
        public Employee_OrderGenerate_Class()
        {
            odm = new ObservableCollection<Employee_OrderDetailsMaster_Class>();
            RetailerMaster_Class_Data = new RetailerMaster_Class();
            EmployeeMaster_Class_Data = new EmployeeMaster_Class();
            _CategoryMaster_Class = new ObservableCollection<CategoryMaster_Class>();
        }
        public int Order_Id { get; set; }
        public int Emp_Id { get; set; }
        //public string Emp_Name { get; set; }
        //public string Emp_Code { get; set; }
        //public string Retailer_Name { get; set; }
        //Article
        public int Article_Id { get; set; }
        //public int SubCategory_Id { get; set; }
        //public string Article_PrimaryImage { get; set; }
        //public string Article_SecondaryImage { get; set; }
        public string Article_No { get; set; }
        //public string Color_Name { get; set; }
        //public decimal MRP { get; set; }
        //public decimal Selling_Price { get; set; }
        //public string Description { get; set; }
        //Article End
        public string Remark { get; set; }
        public string PreferredTransport { get; set; }
        public DateTime DateOfDispatch { get; set; }
        public int Retailer_Id { get; set; }
        public int Info_ID { get; set; }
        public string Invoice_No { get; set; }
        public double Total_Amount { get; set; }
        public double OutStanding_Amount { get; set; }
        public int Total_Item { get; set; }
        public string Action { get; set; }
        public string InvoiceStatus { get; set; }
        public string _Feedback { get; set; }
        public Boolean _Status { get; set; }
        public int _CreatedBy { get; set; }
        public DateTime _CreatedDate { get; set; }
        public int _ModifiedBy { get; set; }
        public DateTime _ModifiedDate { get; set; }
        public ObservableCollection<Employee_OrderDetailsMaster_Class> odm { get; set; }
       // public AdminInfo_Class AdminInfo_Class_Data { get; set; }
        public RetailerMaster_Class RetailerMaster_Class_Data { get; set; }
        public EmployeeMaster_Class EmployeeMaster_Class_Data { get; set; }

        public ObservableCollection<CategoryMaster_Class> _CategoryMaster_Class { get; set; }
        //public string Category_Name { get; set; }
        //public int OrderDetail_Id { get; set; }


    }
    public class Employee_OrderDetailsMaster_Class
    {
        public Employee_OrderDetailsMaster_Class()
        {
            ArticleMaster_Class_Data = new ArticleMaster_Class();
            SubCategoryMaster_Class_List = new ObservableCollection<SubCategoryMaster_Class>();
            CategoryMaster_Class_Date = new CategoryMaster_Class();
            SubCategoryMaster_Class_Data = new SubCategoryMaster_Class();
        }
        public Boolean Status { get; set; }
        public int OrderDetail_Id { get; set; }
        public int Order_Id { get; set; }
        public int Article_Id { get; set; }
        public int SubCategory_Id { get; set; }
        public int Quantity { get; set; }
        public double MRP { get; set; }
        public double SellPrice { get; set; }
        public ArticleMaster_Class ArticleMaster_Class_Data { get; set; }
        public CategoryMaster_Class CategoryMaster_Class_Date { get; set; }
        public SubCategoryMaster_Class SubCategoryMaster_Class_Data { get; set; }
        public ObservableCollection<SubCategoryMaster_Class> SubCategoryMaster_Class_List { get; set; }



    }
}
