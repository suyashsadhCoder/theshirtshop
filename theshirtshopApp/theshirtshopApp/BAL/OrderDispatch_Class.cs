using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace theshirtshopApp.BAL
{
    public class OrderDispatch_Class
    {
        public int Dispatch_Id { get; set; }
        public int Order_Id { get; set; }
        public string LRNumber { get; set; }
        public string Trasporter_Name { get; set; }
        public string Transporter_Contact { get; set; }
        public DateTime Dispatch_Date { get; set; }
        public DateTime Created_Date { get; set; }
        public int Created_By { get; set; }
        public Boolean _Status { get; set; }

    }
}
