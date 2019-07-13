using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace theshirtshopApp.BAL
{
    public class FranchiseSell_Class:INotifyPropertyChanged
    {
        public FranchiseSell_Class()
        {
            FranchiseSellDetails_Class_Data = new FranchiseSellDetails_Class();
            fsd = new List<FranchiseSellDetails_Class>();
            FranchiseSellDetails_Class_List = new List<FranchiseSellDetails_Class>();
            Customer_Class_Data = new Customer_Class();
            
            
        }
        public int FranchiseSell_Id { get; set; }
        public int? Franchise_Id { get; set; }
        public string Franchise_Name { get; set; }

        public string Customer_Name { get; set; }
        public string Customer_MobileNo { get; set; }
        public string Customer_EmailId { get; set; }

        public string Customer_Address { get; set; }

        public int? Customer_Id { get; set; }
        public string Invoice_No { get; set; }

        public decimal Total_Amount { get; set; }
        decimal _couponCodeAmt;
        public decimal CouponCodeAmount { get { return _couponCodeAmt; } set { _couponCodeAmt = value; NotifyPropertyChanged(); } }
        string _couponCode;
        public string CouponCode { get { return _couponCode; } set { _couponCode = value; NotifyPropertyChanged(); } }
        public int Created_By { get; set; }

        public DateTime Created_Date { get; set; }
        public string New_Date { get; set; }
        public string Article_No { get; set; }
        public string SubCategory_Name { get; set; }
        public int? Quantity { get; set; }
        public decimal MRP { get; set; }
        public string Category_Name { get; set; }



        public FranchiseSellDetails_Class FranchiseSellDetails_Class_Data { get; set; }
        public List<FranchiseSellDetails_Class> fsd { get; set; }


        //Pankaj Code
        public int Modified_By { get; set; }
        public DateTime Modified_Date { get; set; }
        public bool Status { get; set; }
        public List<FranchiseSellDetails_Class> FranchiseSellDetails_Class_List { get; set; }
        public Customer_Class Customer_Class_Data { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;
        protected void NotifyPropertyChanged([CallerMemberName]string propertyName = "")
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

    }
    public class FranchiseSellDetails_Class
    {
        public FranchiseSellDetails_Class()
            {
            ArticleMaster_Class_Data = new ArticleMaster_Class();
            _CategoryMaster_Class_List = new List<CategoryMaster_Class>();
            _CategoryMaster_Class_Data = new CategoryMaster_Class();
            _SubCategoryMaster_Class_Data = new SubCategoryMaster_Class();
            _FranchiseSell_Class_List = new List<FranchiseSell_Class>();
            FranchiseStokeMaster_Class_Data = new StockMaster_Class();
           }
        public int FranchiseSellDetail_Id { get; set; }
        public int FranchiseSell_Id { get; set; }
        public int Article_Id { get; set; }
        public int SubCategory_Id { get; set; }
        public int? Quantity { get; set; }
        public decimal MRP { get; set; }
        public string Article_No { get; set; }
        public string Total_Amount { get; set; }

        //Pankaj Code
        public ArticleMaster_Class ArticleMaster_Class_Data { get; set; }
        public List<CategoryMaster_Class> _CategoryMaster_Class_List { get; set; }
        public CategoryMaster_Class _CategoryMaster_Class_Data { get; set; }
        public SubCategoryMaster_Class _SubCategoryMaster_Class_Data { get; set; }

        public List<FranchiseSell_Class> _FranchiseSell_Class_List { get; set; }
        public StockMaster_Class FranchiseStokeMaster_Class_Data { get; set; }
    }
}
