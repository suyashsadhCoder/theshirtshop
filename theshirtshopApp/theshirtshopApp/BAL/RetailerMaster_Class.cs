using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using theshirtshopApp.Validation;

namespace theshirtshopApp.BAL
{
    public class RetailerMaster_Class :ModelBase
    {

      


        public int RetailerMaster_Id { get; set; }
        public int Employee_Id { get; set; }
        public DateTime Retailer_RegistrationDate { get; set; }
        public string Retailer_Name { get; set; }
        public string Retailer_Image { get; set; }
        public string Retailer_Secondary_Image { get; set; }
        public string Secondary_Mobile_No { get; set; }

        public string Email_Id { get; set; }
        public string Mobile_No { get; set; }
        public string GSTN_Adhar_No { get; set; }
        public int State_Id { get; set; }
        public int City_Id { get; set; }
        public string _Address { get; set; }
        public string _Action { get; set; }
        public DateTime Created_Date { get; set; }
        public int Created_By { get; set; }
        public DateTime Modified_Date { get; set; }
        public int Modified_By { get; set; }
        public Boolean _Status { get; set; }
      //  public string Primary_Mobile_No { get; set; }
        public string Date { get; set; }
        public string Form_Image { get; set; }
        public ObservableCollection<MultipleImage_Class> Form_ImageByte { get; set; }
        public ObservableCollection<MultipleImage_Class> _MultipleImage_ByteData { get; set; }
        public byte[] Retailer_Imagebyte { get; set; }
        public byte[] Adhar_ByteImage { get; set; }
        public string Adhar_Image { get; set; }
        //public ObservableCollection<MultipleContect_Class> Primary_Mobile_No { get; set; }
    }
}
