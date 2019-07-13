using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace theshirtshopApp.BAL
{
    public class MultipleContect_Class
    {
        //[Display(Name = "Franchise Alternate Contact No")]
        //[StringLength(maximumLength: 12, MinimumLength = 10,ErrorMessage ="minimumLength 10 and maximumLength 12 required")]
        //[RegularExpression("[0-9]{10-12}", ErrorMessage = "Incorrect Contact No.")]
        string ContactNo = string.Empty;
        public string Contect_No { get {return ContactNo; }
            set {ContactNo=value;
                if (12 >= value.Length && value.Length >= 10)
                {
                    ErrorMessageStatus = false;
                }
                else if (value.Length == 0)
                {
                    ErrorMessageStatus = false;
                }
                else
                {
                    ErrorMessageStatus = true;
                }

            } }
        //public string ErrorMessage { get { return "minimumLength 10 and maximumLength 12 required"; } }
        public bool ErrorMessageStatus { get;
            set; }
       
    }
}
