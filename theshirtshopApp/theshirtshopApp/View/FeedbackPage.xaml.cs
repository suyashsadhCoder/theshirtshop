using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using theshirtshopApp.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace theshirtshopApp.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FeedbackPage : ContentPage
    {
        public FeedbackPage()
        {
            if (Application.Current.Properties.ContainsKey("Key"))
            {
                InitializeComponent();
                BindingContext = new FeedbackPageViewModel(Navigation);
            }
            else
            {
                Navigation.PushModalAsync(new MainPage());
            }
        
        }

        private void Title_Completed(object sender, EventArgs e)
        {
            Description.Focus();
        }

        private void Description_Completed(object sender, EventArgs e)
        {
            Send.Focus();
        }
    }
}