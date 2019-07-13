using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace theshirtshopApp.Validation
{
  public  class MpLabel : Label
    {
        public static readonly BindableProperty IsUnderlinedProperty = BindableProperty.Create("IsUnderlined", typeof(bool), typeof(MpLabel), false);

        public bool IsUnderlined
        {
            get { return (bool)GetValue(IsUnderlinedProperty); }
            set { SetValue(IsUnderlinedProperty, value); }
        }
        public static readonly BindableProperty IsStrikelinedProperty = BindableProperty.Create("IsStrikelined", typeof(bool), typeof(MpLabel), false);

        public bool IsStrikelined
        {
            get { return (bool)GetValue(IsStrikelinedProperty); }
            set { SetValue(IsStrikelinedProperty, value); }
        }
    }
}
