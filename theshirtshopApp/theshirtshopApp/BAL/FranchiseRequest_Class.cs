using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace theshirtshopApp.BAL
{
    public class FranchiseRequest_Class
    {
        public int FranchiseRequest_Id { get; set; }
        public int Franchise_Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Requested_Date { get; set; }
        public string Action { get; set; }


    }
}
