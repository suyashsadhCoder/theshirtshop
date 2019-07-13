using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using theshirtshopApp.Validation;

namespace theshirtshopApp.BAL
{
    public class Customer_Class : ModelBase
    {
        public string ContactNo=string.Empty;
        public int CustomerId;
        public string  CustomerName;

        public int Customer_Id { get; set; }
        public string Customer_Name { get; set; }

        public  string  Contact_No
        {
            get;set;

        }

        public string Email_Id { get; set; }
      
        public string Address { get; set; }

        

    }
}
