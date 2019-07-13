using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using theshirtshopApp.Droid;
using theshirtshopApp.Validation;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using static Java.Util.ResourceBundle;

[assembly: Dependency(typeof(WebView))]
namespace theshirtshopApp.Droid
{
    //public class HybridWebViewRenderer : WebViewRenderer
    //{
    //    protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.WebView> e)
    //    {
    //        base.OnElementChanged(e);

    //        var formsWebView = e.NewElement as MPHybridWebView;

    //        if (formsWebView != null)
    //        {
    //            var nativeWebView = Control as Android.Webkit.WebView;
    //            nativeWebView.Settings.JavaScriptEnabled = true;
    //            nativeWebView.Settings.DomStorageEnabled = true;
    //            nativeWebView.Settings.DisplayZoomControls = true;
    //            nativeWebView.LoadUrl(formsWebView.Uri, formsWebView.Dictionary);
    //        }
    //    }

    //}
    public class WebViewRenderer : Xamarin.Forms.Platform.Android.WebViewRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<WebView> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                Control.Settings.BuiltInZoomControls = true;
                Control.Settings.DisplayZoomControls = true;
            }
        }
    }
}   