using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace theshirtshopApp.BAL
{
  public  class FranchiseReferral_Class
    {
        public int Referral_Id { get; set; }
        public int Franchise_Id { get; set; }
        public string Franchise_Name { get; set; }
        public string Referral_Name { get; set; }

        public string Referral_EmailId { get; set; }

        public string Referral_MobileNo { get; set; }
        public string Referral_Address { get; set; }

        public DateTime Referral_Date { get; set; }
    }
}
