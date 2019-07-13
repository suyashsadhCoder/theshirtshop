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
using theshirtshopApp.Controls;
using theshirtshopApp.Droid.Renderer;
using Xamarin.Forms;


[assembly: Dependency(typeof(EntryPopupLoader))]
namespace theshirtshopApp.Droid.Renderer
{
    public class EntryPopupLoader : IEntryPopupLoader
    {
        public void ShowPopup(EntryPopUp reference)
        {
            var alert = new AlertDialog.Builder(Forms.Context);

            var edit = new EditText(Forms.Context) { Text = reference.Text };
            alert.SetView(edit);

            alert.SetTitle(reference.Title);

            alert.SetPositiveButton("OK", (senderAlert, args) =>
            {
                reference.OnPopupClosed(new EntryPopupClosedArgs
                {
                    Button = "OK",
                    Text = edit.Text
                });
            });

            alert.SetNegativeButton("Cancel", (senderAlert, args) =>
            {
                reference.OnPopupClosed(new EntryPopupClosedArgs
                {
                    Button = "Cancel",
                    Text = edit.Text
                });
            });
            alert.Show();


        }
    }
}