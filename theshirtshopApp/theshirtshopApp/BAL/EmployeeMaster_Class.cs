using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace theshirtshopApp.BAL
{
   public class EmployeeMaster_Class
    {
        public int Emp_Id { get; set; }
        public string Emp_Code { get; set; }
        public string Emp_FirstName { get; set; }

        public string Emp_LastName { get; set; }

        public string Emp_Qualification { get; set; }
        public string Emp_Photo { get; set; }

        public string Emp_PanPhoto { get; set; }

        public string Emp_PanNo { get; set; }

        public string Emp_AdharPhoto { get; set; }
        public string Emp_AdharNo { get; set; }
        public string Emp_EmailId { get; set; }

        public string Emp_PrimaryMobileNo { get; set; }
        public string Emp_AlternateMobileNo { get; set; }


        public int EmpStateId { get; set; }
        public int EmpCityId { get; set; }

        public string Emp_LocalAddress { get; set; }

        public string Emp_PermanetAddress { get; set; }

        public string Emp_AddressProofPhoto { get; set; }


        public int Created_By { get; set; }

        public DateTime Created_Date { get; set; }
        public int Modified_By { get; set; }

        public DateTime Modified_Date { get; set; }

        public bool Status { get; set; }
        public string Action { get; set; }


        public string RollName { get; set; }

      

        public string FullName { get; set; }

        public string City_Name { get; set; }

        public string State_Name { get; set; }
    }
}
