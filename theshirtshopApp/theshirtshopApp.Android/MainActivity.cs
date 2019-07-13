using System;
using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Xamarin.Forms;
using Plugin.Media;
using Acr.UserDialogs;
using Android.Net;
using Plugin.Connectivity;
using Plugin.Permissions;
using Android.Support.V7.App;
using Xamarin.Forms.Platform.Android;
using System.Threading;
using System.Threading.Tasks;
using Plugin.DownloadManager;
using Plugin.DownloadManager.Abstractions;
using System.IO;
using System.Linq;
using Plugin.CurrentActivity;
using Android.Content.Res;

namespace theshirtshopApp.Droid
{
    [Activity(Label = "The Shirt Shop", Icon = "@drawable/logo", Theme = "@style/MainTheme", MainLauncher = false, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            FormsAppCompatActivity.ToolbarResource = Resource.Layout.Toolbar;
            FormsAppCompatActivity.TabLayoutResource = Resource.Layout.Tabbar;
            base.OnCreate(bundle);
            global::Xamarin.Forms.Forms.Init(this, bundle);
            global::Acr.BarCodes.BarCodes.Init(() => (Activity)Forms.Context);
            global::Plugin.Media.CrossMedia.Current.Initialize();
            UserDialogs.Init(this);
            CrossCurrentActivity.Current.Activity = this;
            LoadApplication(new App());
            
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Permission[] grantResults)
        {
            PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
        public override Resources Resources
        {
            get
            {
                Resources res = base.Resources;
                Configuration config = new Configuration();
                config.SetToDefaults();
                res.UpdateConfiguration(config, res.DisplayMetrics);
                return res;
            }

        }
        public override void OnBackPressed()
        {
            if (App.BackCommand())
            {
                base.OnBackPressed();
            }
            else
            {
                if (App.exitcount == 1)
                {
                    Toast.MakeText(this, "press once again to exit", ToastLength.Short).Show();
                }
                else
                {
                    base.Finish();
                }
            }
        }


    }
}
