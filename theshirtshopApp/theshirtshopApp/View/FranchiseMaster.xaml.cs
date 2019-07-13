using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using theshirtshopApp.Interface;
using theshirtshopApp.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace theshirtshopApp.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FranchiseMaster : MasterDetailPage
    {
        private NavigationPage _innerNavPage;
        private NavigationPage _outerNavPage;
        private bool status { get; set; }
        public FranchiseMaster()
        {
            if (Application.Current.Properties.ContainsKey("Key"))
            {

                InitializeComponent();
                status = false; 
                IsPresentedChanged += FranchiseMaster_IsPresentedChanged;
                BindingContext = new FranchiseMasterMasterViewModel(Navigation);
                photo.BorderColor = Color.Silver;
                HomePage HomePage = new HomePage();
                NavigationPage.SetHasNavigationBar(HomePage, false);
                _innerNavPage = new NavigationPage(HomePage)
                {
                    Title = HomePage.Title
                };
                _innerNavPage.PropertyChanged += (sender, args) =>
                {
                    if (args.PropertyName == NavigationPage.CurrentPageProperty.PropertyName)
                    {
                        _innerNavPage.Title = _innerNavPage.CurrentPage.Title;
                       
                    }
                };
               _outerNavPage = new NavigationPage(_innerNavPage);
                Detail = _outerNavPage;
            }
            else
            {
                Navigation.PushModalAsync(new MainPage());
            }
        }
        private void FranchiseMaster_IsPresentedChanged(object sender, EventArgs e)
        {
            if (status == false)
            {
                IsPresented = true;
            }
        }


        private void MenuItemsListView_Refreshing(object sender, EventArgs e)
        {
            BindingContext = new FranchiseMasterMasterViewModel(Navigation);
            MenuItemsListView.IsRefreshing = false;
        }

        private void MenuItemsListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            NavigateToPages(e.Item as FranchiseMasterMenuItem);
        }
        public void NavigateToPages(FranchiseMasterMenuItem item)
        {
            try
            {
                if (Application.Current.Properties.ContainsKey("Key"))
                {

                    if (!string.IsNullOrEmpty(Application.Current.Properties["Key"].ToString()))
                    {
                        status = true;
                        IsPresented = false;
                        status = false;
                        
                        if (item.TargetType != null)
                        {

                            Page page = (Page)Activator.CreateInstance(item.TargetType);
                            if (item.Title == "Home")
                            {
                                if (!(_innerNavPage.CurrentPage is HomePage))
                                {
                                    _innerNavPage.PopToRootAsync();

                                }
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
        public void Button_Clicked(object sender, EventArgs e)
        {
            status = true;
            IsPresented = false;
            status = false;
        }
    }
}