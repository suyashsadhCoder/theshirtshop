using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using theshirtshopApp.Droid.Services;
using theshirtshopApp.Interface;
using Xamarin.Forms;

[assembly: Dependency(typeof(IpAddressServices))]
namespace theshirtshopApp.Droid.Services
{
  public  class IpAddressServices : IIpAddressServices
    {
        public string GetIPAddress()
        {
            IPAddress[] adresses = Dns.GetHostAddresses(Dns.GetHostName());

            if (adresses != null && adresses[0] != null)
            {
                return adresses[0].ToString();
            }
            else
            {
                return null;
            }
        }
    }
}