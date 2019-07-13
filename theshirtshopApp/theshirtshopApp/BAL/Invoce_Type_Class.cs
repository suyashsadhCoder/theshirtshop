using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace theshirtshopApp.BAL
{
   public class Invoce_Type_Class
    {
        public int Invoce_Type_Id { get; set; }
        public String Invoce_Type_Name { get; set; }

        public ObservableCollection<Invoce_Type_Class> _Get_Data()
        {
            ObservableCollection<Invoce_Type_Class> data = new ObservableCollection<Invoce_Type_Class> {
                new Invoce_Type_Class{
                    Invoce_Type_Id=1,
                    Invoce_Type_Name="Customer Invoice"

                },
                  new Invoce_Type_Class{
                    Invoce_Type_Id=2,
                    Invoce_Type_Name="Order Invoice"

                }
            };
            return data;
        }
    }
}
