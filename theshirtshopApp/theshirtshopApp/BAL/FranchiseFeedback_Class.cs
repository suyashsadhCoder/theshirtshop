using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace theshirtshopApp.BAL
{
   public class FranchiseFeedback_Class
    {
        public int Testimonial_Id { get; set; }
        public int Franchise_Id { get; set; }
        public string Franchise_Name { get; set; }
        public string _Title { get; set; }
        public string Franchise_Photo { get; set; }
        public string _Description { get; set; }
        public string _Action { get; set; }
        public Boolean _Status { get; set; }
        public DateTime Created_Date { get; set; }
        public int Created_By { get; set; }
        public DateTime Modified_Date { get; set; }
        public int Modified_By { get; set; }


        public string Date { get; set; }

    }
}
