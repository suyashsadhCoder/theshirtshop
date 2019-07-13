using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace theshirtshopApp.BAL
{
   public class StateMaster
    {
        //public StateMaster()
        //{
        //    this.CityClassList = new List<CityMaster>();
        //}
        public int State_ID { get; set; }

        public string State_Name { get; set; }

        public List<CityMaster> CityClassList { get; set; }
    }

  
}
