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
using theshirtshopApp.Droid.Services;
using theshirtshopApp.Interface;
using Xamarin.Forms;

[assembly: Dependency(typeof(AppVersionProvider))]
namespace theshirtshopApp.Droid.Services
{
    public class AppVersionProvider : IAppVersionProvider
    {
        public string AppVersion
        {
            get
            {
                var context = Android.App.Application.Context;
                var info = context.PackageManager.GetPackageInfo(context.PackageName, 0);

                return $"{info.VersionName}.{info.VersionCode.ToString()}";
            }
        }
    }
}