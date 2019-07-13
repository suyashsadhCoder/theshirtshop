using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace theshirtshopApp.BAL
{
   public class FranchiseAdminPaymentDetail_Class
    {
        public int? Franchise_Id { get; set; }
        public int? Customer_Id { get; set; }
        public string Customer_Name { get; set; }
        public DateTime? Created_Date { get; set; }
        public Decimal? Amount { get; set; }
    }
    public class FranchiseAdminPaymentDetail_Main_Class
    {
        public FranchiseAdminPaymentDetail_Main_Class()
        {
            _FranchiseSellDetailsModel = new List<FranchiseAdminPaymentDetail_Class>();
        }
        public Boolean PaidStatus { get; set; }
        public List<FranchiseAdminPaymentDetail_Class> _FranchiseSellDetailsModel { get; set; }
    }

    //    First Method
    //FranchiseCustomerSellAndPayDetails_Get(int Id)


    //Second Method
    //FranchiseCustomerSellAndPayDetails_Get(int Month, int Year, string TermType, int Id)

    //FranchiseCustomerSellApiController
}
