using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace theshirtshopApp.BAL
{
  public  class FranchiseOrderDispatchClass
    {
        public int Dispatch_Id { get; set; }
        public int Order_Id { get; set; }
        public string LR_Number { get; set; }
        public string Transporter_Name { get; set; }
        public string Transporter_Contect_No { get; set; }
        public DateTime Dispatch_Date { get; set; }
        public DateTime Created_Date { get; set; }
        public int Created_By { get; set; }
        public bool Status { get; set; }

    }
}
