using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using theshirtshopApp.BAL;
using theshirtshopApp.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace theshirtshopApp.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EmployeeRetailerMaster : MasterDetailPage
    {
        private NavigationPage _innerNavPage;
        private NavigationPage _outerNavPage;
        private bool status { get; set; }
        public EmployeeRetailerMaster()
        {
            if (Application.Current.Properties.ContainsKey("Key"))
            {
                InitializeComponent();
                BindingContext = new EmployeeRetailerMasterMasterViewModel(Navigation);

                ProfilePage ProfilePage = new ProfilePage();
                NavigationPage.SetHasNavigationBar(ProfilePage, false);
                status = false;
                IsPresentedChanged += EmployeeRetailerMaster_IsPresentedChanged; 

                photo.BorderColor = Color.FromHex("#feac41");
                _innerNavPage = new NavigationPage(ProfilePage)
                {
                    Title = ProfilePage.Title
                };
                _innerNavPage.PropertyChanged += (sender, args) =>
                {
                    if (args.PropertyName == NavigationPage.CurrentPageProperty.PropertyName)
                        _innerNavPage.Title = _innerNavPage.CurrentPage.Title;
                };
                _outerNavPage = new NavigationPage(_innerNavPage);
                Detail = _outerNavPage;
            }
            else
            {
                Navigation.PushModalAsync(new MainPage());
            }
        }

        private void EmployeeRetailerMaster_IsPresentedChanged(object sender, EventArgs e)
        {
            if (status == false)
            {
                IsPresented = true;
            }
        }

        private void MenuItemsListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            try
            {
                if (Application.Current.Properties.ContainsKey("Key"))
                {
                    if (!string.IsNullOrEmpty(Application.Current.Properties["Key"].ToString()))
                    {
                        var item = (EmployeeRetailerMasterMenuItem)e.Item;
                        if (item.TargetType != null)
                        {

                            Page page = (Page)Activator.CreateInstance(item.TargetType);
                            if (item.Title == "Profile")
                            {
                                if (!(_innerNavPage.CurrentPage is ProfilePage))
                                    _innerNavPage.PopToRootAsync();
                            }
                            else
                            {
                                var ChackPriousPage = _innerNavPage.Navigation.NavigationStack.Where(x => x.Title == page.Title).FirstOrDefault();
                                if (ChackPriousPage != null)
                                {
                                    _innerNavPage.Navigation.RemovePage(ChackPriousPage);
                                }
                                _innerNavPage.PushAsync(page);
                            }
                        }
                        else
                        {
                            Device.OpenUri(new Uri(item.Url));
                        }
                        status = true;
                        IsPresented = false;
                        status = false;
                    }
                    else
                    {
                        Navigation.PushModalAsync(new MainPage());
                    }
                }
                else
                {
                    Navigation.PushModalAsync(new MainPage());
                }
            }
            catch (Exception ee)
            {
                App.Current.MainPage.DisplayAlert("Error!", ee.Message, "Ok");
            }
        }
        private void Button_Clicked(object sender, EventArgs e)
        {
            status = true;
            IsPresented = false;
            status = false;
        }
    }
}