using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using theshirtshopApp.BAL;
using theshirtshopApp.Validation;

namespace theshirtshopApp.ViewModel
{
   public class AllGoodsReturnDetailPageViewModel :ModelBase
    {
        public AllGoodsReturnDetailPageViewModel(FranchiseGoodReturn_Class _OrderMaster_Class)
        {

            _OCD = _OrderMaster_Class;
        }
        public FranchiseGoodReturn_Class _OCD
        {
            get { return OCD; }
            set
            {
                OCD = value;
                NotifyPropertyChanged();
            }
        }

        public FranchiseGoodReturn_Class OCD { get; set; }
    }
}
