using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using theshirtshopApp.BAL;

namespace theshirtshopApp.ViewModel
{

  public  class CustomerInvoiceDetailPageViewModel
    {
       
        public CustomerInvoiceDetailPageViewModel(FranchiseSell_Class FranchiseSell_Class)
        {
            Franchise_Sell_Class_Data = FranchiseSell_Class;
        }
        public FranchiseSell_Class Franchise_Sell_Class_Data { get; set; }
    }
}

