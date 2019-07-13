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
    public partial class ForgetPassword : ContentPage
    {
        public ForgetPasswordViewModel viewModel;
        

        public ForgetPassword()
        {
            InitializeComponent();
            this.BindingContext = viewModel = new ForgetPasswordViewModel(Navigation);
            crossButton.Clicked += CrossButton_Clicked;
        }
        

        private void CrossButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }
    }
}