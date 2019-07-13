using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace theshirtshopApp.ValueConverter
{
  public  class AppendString : IValueConverter
    {

        #region IValueConverter implementation

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            //if (value is MyModel && value != null)
            //{

            //    MyModel tmp = (MyModel)value;
            //    return string.Format("{0} - {1} / {2}", tmp.Address, tmp.City, tmp.State);
            //}
            //return "**-**/**";
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
