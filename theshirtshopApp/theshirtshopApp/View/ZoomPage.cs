using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using theshirtshopApp.DAL;
using theshirtshopApp.Validation;
using Xamarin.Forms;
namespace theshirtshopApp.View
{
    public class ZoomPage : ContentPage
    {
        public ZoomPage(ImageSource img)
        {
            Title = "Zoom Image";
            NavigationPage.SetHasNavigationBar(this, false);
            Content = new StackLayout
            {
                Padding = new Thickness(20),
                Children = { new ZoomImage { Source = img } }
            };

        }
    }
}