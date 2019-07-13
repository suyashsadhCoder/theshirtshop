using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using theshirtshopApp.BAL;
using theshirtshopApp.DAL;
using theshirtshopApp.Interface;
using theshirtshopApp.Validation;
using theshirtshopApp.View;
using Xamarin.Forms;

namespace theshirtshopApp.ViewModel
{
    public class UpComingStockRequestDetailPageViewModel : ModelBase
    {
        public OrderMaster_Class orderMaster_Class_Data { get; set; }
        //public OrderMaster_Class ArticleMaster_Class_List { get; set; }
        private IAllDataServices _IAllDataServices;
        private INavigation _INavigation { get; set; }
        public UpComingStockRequestDetailPageViewModel(OrderMaster_Class OrderMaster_Class_Data,INavigation  navigation)
        {
            _INavigation = navigation;
            _IAllDataServices = new AllDataServices();
            orderMaster_Class_Data = OrderMaster_Class_Data;

           // OrderMaster_Class_Data = orderMaster_Class;

            if (OrderMaster_Class_Data.Action == "Dispatched")
            {
                _Dispatch_Visible = true;
            }
            SendFeedbackCommand = new Command(async () => await SendFeedback());
        }

        private async Task SendFeedback()
        {

            //if (!string.IsNullOrEmpty(_Feedback))
           // {
                OrderMaster_Class oc = new OrderMaster_Class();
                oc.Order_Id = orderMaster_Class_Data.Order_Id;
                oc.Feedback = _Feedback;

                JObject result = await _IAllDataServices.SaveFeedBack(oc);
                if (result != null)
                {
                    string type = result["Type"].ToString();

                    if (type == "1")
                    {
                        await App.Current.MainPage.DisplayAlert("Success!", (string)result["ResponseMessage"], "Ok");
                        var ChackPriousPage = _INavigation.NavigationStack.Where(x => x.Title == "UpComing Request Detail").FirstOrDefault();
                        if (ChackPriousPage != null)
                        {
                            _INavigation.RemovePage(ChackPriousPage);
                        }
                    var secondChackPriousPage = _INavigation.NavigationStack.Where(x => x.Title == "UpComing Request").FirstOrDefault();
                    if (secondChackPriousPage != null)
                    {
                        _INavigation.RemovePage(secondChackPriousPage);
                    }

                        await _INavigation.PushAsync(new UpComingStockRequestPage());

                    }
                    else
                    {
                        await App.Current.MainPage.DisplayAlert("Oops!", (string)result["ResponseMessage"], "Ok");
                    }
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("Oops!", "Please Refresh Page And try Again....", "Ok");
                }
           // }
            //else
            //{
            //    _Feedback = "";
            //    await App.Current.MainPage.DisplayAlert("Oops!", "Please fill Feedback......", "Ok");
            //}
        }

        bool DispatchVisible = false;
        string Feedback = string.Empty;
        public bool _Dispatch_Visible {
            get { return DispatchVisible; }
            set { DispatchVisible = value;
                NotifyPropertyChanged();
            }

        }
        [Display(Name ="Feedback")]
        [StringLength(maximumLength:150,MinimumLength =20,ErrorMessage ="Minimum 20 Char And Maximum 150 Char Required.")]
        public string _Feedback
        {
            get { return Feedback; }
            set { Feedback = value;
                NotifyPropertyChanged();
                ValidateProperty(value);
            }
        }

       public Command SendFeedbackCommand { get; }
    }
}
