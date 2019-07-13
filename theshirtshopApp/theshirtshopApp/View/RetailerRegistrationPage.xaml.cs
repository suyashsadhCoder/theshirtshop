using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using theshirtshopApp.BAL;
using theshirtshopApp.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace theshirtshopApp.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RetailerRegistrationPage : ContentPage
    {
        
        public RetailerRegistrationPage()
        {
            if (Application.Current.Properties.ContainsKey("Key"))
            {
                InitializeComponent();
                BindingContext = new RetailerRegistrationViewModel(Navigation);
            }
            else
            {
                Navigation.PushModalAsync(new MainPage());
            }
           
        
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            var Secondary_Mobile_No = button?.BindingContext as MultipleContect_Class;
            var vm = BindingContext as RetailerRegistrationViewModel;
            vm?.DeleteRowCommand.Execute(Secondary_Mobile_No);
        }

        private void RemoveSecondaryImage(object sender, EventArgs e)
        {

            //  var button = sender as Button;
            //   var MultipleImage_Class = button?.BindingContext as MultipleContect_Class;
              var vm = BindingContext as RetailerRegistrationViewModel;
          //  int asd = RetailerSecondarImage.Position;
             vm?.DeleteRetailerSeconaryImageCommand.Execute(RetailerSecondarImage.Position);
        }
        private void RemoveFormImage(object sender, EventArgs e)
        {

            //  var button = sender as Button;
            //   var MultipleImage_Class = button?.BindingContext as MultipleContect_Class;
            var vm = BindingContext as RetailerRegistrationViewModel;
            //  int asd = RetailerSecondarImage.Position;
            vm?.DeleteRetailerFormImageCommand.Execute(FormImage.Position);
        }

        private void RetailerName_Completed(object sender, EventArgs e)
        {
            EmailId.Focus();
        }

        private void EmailId_Completed(object sender, EventArgs e)
        {
            PrimaryMobileNo.Focus();
        }

        private void PrimaryMobileNo_Completed(object sender, EventArgs e)
        {
            GSTNAdharNo.Focus();
        }

        private void GSTNAdharNo_Completed(object sender, EventArgs e)
        {
            StateMasterList.Focus();
        }

       

        private void CityMasterList_SelectedIndexChanged(object sender, EventArgs e)
        {
            Address.Focus();
        }

      
    }
}