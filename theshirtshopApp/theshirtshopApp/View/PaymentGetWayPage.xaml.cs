using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using theshirtshopApp.BAL;
using theshirtshopApp.Interface;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace theshirtshopApp.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PaymentGetWayPage : ContentPage
    {
        public PaymentGetWayPage(int id)
        {
            if (id > 0)
            {
                InitializeComponent();
                progress.IsVisible = true;
               string convertedId= Base64Encode("Id=" + id);
                view.Source = "http://www.theshirtshop.in/paymentGetway/Payment.html?" + convertedId;
               
            }
        }

        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }

        private void view_Navigated(object sender, WebNavigatedEventArgs e)
        {
            progress.IsVisible = false;
        }

        private void view_Navigating(object sender, WebNavigatingEventArgs e)
        {

            progress.IsVisible = true;
            
           
        }

    }
}