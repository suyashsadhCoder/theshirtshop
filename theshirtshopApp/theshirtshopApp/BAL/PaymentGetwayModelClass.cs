using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace theshirtshopApp.BAL
{
  public  class PaymentGetwayModelClass
    {
        public int amount { get; set; }
        public string buyerEmail { get; set; }
        public string currency { get; set; }
        public string merchantIdentifier { get; set; }
        public string orderId { get; set; }
        public string checksum { get; set; }
    }
}
