using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace theshirtshopApp.Validation
{
 public   class MPHybridWebView : WebView
    {
       

        public static readonly BindableProperty UriProperty = BindableProperty.Create(
            propertyName: "Uri",
            returnType: typeof(string),
            declaringType: typeof(MPHybridWebView),
            defaultValue: default(string));

        public string Uri
        {
            get { return (string)GetValue(UriProperty); }
            set { SetValue(UriProperty, value); }
        }

        public static readonly BindableProperty DictionaryProperty = BindableProperty.Create(
           propertyName: "Dictionary",
           returnType: typeof(Dictionary<string,string>),
           declaringType: typeof(MPHybridWebView),
           defaultValue: default(Dictionary<string, string>));

        public Dictionary<string, string> Dictionary
        {
            get { return (Dictionary<string, string>)GetValue(DictionaryProperty); }
            set { SetValue(DictionaryProperty, value); }
        }


    }
}
