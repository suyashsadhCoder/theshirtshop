using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BalLayer_TheShirtShop
{
   public class WebapiResponse_Class
    {
       public WebapiResponse_Class()
       {
           this.message = new List<object>();
       }

       public string type { get; set; }
       public IEnumerable<object> message { get; set; }
    }
}
