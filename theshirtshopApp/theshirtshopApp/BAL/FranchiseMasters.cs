//using Android.Service.Autofill;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace theshirtshopApp.BAL
{
    public class FranchiseMasters
    {
        public int Franchise_Id { get; set; }
        public string Franchise_Name { get; set; }
        public string Franchise_GSTNo { get; set; }

        public string Franchise_OwnerName { get; set; }

        public string Franchise_Address { get; set; }
        public string Franchise_Pincode { get; set; }
        public string Franchise_PermanentAddress { get; set; }

        public string Franchise_AddressProofPhoto { get; set; }

        public string Franchise_PanNo { get; set; }

        public string Franchise_PanPhoto { get; set; }

        public string FranchiseAadharNo { get; set; }

        public string FranchiseAadharPhoto { get; set; }


        public string Franchise_EmailId { get; set; }
        public string Franchise_PrimaryMobileNo { get; set; }
        public string Franchise_AlternateMobileNo { get; set; }


        public int Franchise_StateId { get; set; }
        public int Franchise_CityId { get; set; }

        public string Franchise_Photo { get; set; }
        public string Franchise_ExtraPhoto { get; set; }

        public string Franchise_BankName { get; set; }

        public string Franchise_BankAccountNo { get; set; }
        public string Franchise_Code { get; set; }


        public string Franchise_BankIFSCCode { get; set; }

        public int Created_By { get; set; }

        public DateTime Created_Date { get; set; }
        public int Modified_By { get; set; }

        public DateTime Modified_Date { get; set; }

        public bool Status { get; set; }
        public string Action { get; set; }


        public decimal Franchise_Latitude { get; set; }

        public decimal Franchise_Longitude { get; set; }


        public Byte[] Franchise_PhotoByte { get; set; }
        public Byte[] Aadhar_PhotoByte { get; set; }
        public Byte[] Pan_PhotoByte { get; set; }
        public Byte[] AddressProof_PhotoByte { get; set; }

        public IList<MultipleImage_Class> _MultipleImage_Data { get; set; }


        public string City_Name { get; set; }
        public string State_Name { get; set; } 
    }


}
