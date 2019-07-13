
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using theshirtshopApp.BAL;

namespace theshirtshopApp.ViewModel
{
   public class StockRequestDetailPageViewModel
    {
        public FranchiseRequest_Class FranchiseRequest_Class_Data { get; set; }
        public StockRequestDetailPageViewModel(FranchiseRequest_Class franchiseRequest_Class)
        {
            FranchiseRequest_Class_Data = franchiseRequest_Class;
        }
    }
}
