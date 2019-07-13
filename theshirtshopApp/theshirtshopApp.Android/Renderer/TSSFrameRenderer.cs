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
using theshirtshopApp.Droid.Renderer;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;



[assembly: ExportRenderer(typeof(Frame), typeof(TSSFrameRenderer))]
namespace theshirtshopApp.Droid.Renderer
{
   public  class TSSFrameRenderer: FrameRenderer
    {

        public TSSFrameRenderer(Context context) {

        }
        protected override void OnElementChanged(ElementChangedEventArgs<Frame> e)
        {
            base.OnElementChanged(e);
            if (e.NewElement != null)
            {
                ViewGroup.SetBackgroundResource(Resource.Drawable.shadow);
            }
        }
    }
}