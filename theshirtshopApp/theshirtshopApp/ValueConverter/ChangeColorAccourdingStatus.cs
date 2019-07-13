using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace theshirtshopApp.ValueConverter
{
    class ChangeColorAccourdingStatus :IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
           
           
                switch (value.ToString())
                {
                    case "Pending":
                        return Color.FromHex("#88a793");
                    case "Done":
                        return Color.Green;
                    case "Decline":
                        return Color.Red;
                default:
                    return Color.Red;
                }
              

           
           

        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
            //return retSource;
        }
    }
}
