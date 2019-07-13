using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace theshirtshopApp.BAL
{
   public class CouponModel
    {
        public string CustomerId { get; set; }
        public string CurrentFranchiseId { get; set; }
        public string CurrentOrderAmount { get; set; }
        public string CouponCode { get; set; }
        public string CreatedDate { get; set; }

    }
}
