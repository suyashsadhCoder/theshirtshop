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
using theshirtshopApp.Validation;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(MpLabel), typeof(UnderLineLabelRenderer))]
namespace theshirtshopApp.Droid.Renderer
{

        public class UnderLineLabelRenderer : LabelRenderer
        {
            protected override void OnElementChanged(ElementChangedEventArgs<Label> e)
            {
                base.OnElementChanged(e);

                if (Control != null && Element != null)
                {
                    if (((MpLabel)Element).IsUnderlined)
                    {
                        Control.PaintFlags = Android.Graphics.PaintFlags.UnderlineText;
                    }

                    if (((MpLabel)Element).IsStrikelined)
                    {
                        Control.PaintFlags = Android.Graphics.PaintFlags.StrikeThruText;
                    }
                }
            }
        }

}