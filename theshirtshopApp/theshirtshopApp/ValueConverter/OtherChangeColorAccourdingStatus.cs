using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace theshirtshopApp.ValueConverter
{
    public class OtherChangeColorAccourdingStatus : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {


            switch (value.ToString())
            {
                case "Generated":
                    return Color.FromHex("#88a793");
                case "Dispatched":
                    return Color.Green;
                case "Packing Slip":
                    return Color.LightBlue;
                case "Invoice Generated":
                    return Color.Blue;
                default:
                    return Color.Red;
            }





        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
