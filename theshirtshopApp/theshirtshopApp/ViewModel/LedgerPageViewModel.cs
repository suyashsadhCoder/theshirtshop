using Acr.UserDialogs;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using theshirtshopApp.BAL;
using theshirtshopApp.DAL;
using theshirtshopApp.Interface;
using theshirtshopApp.Validation;
using Xamarin.Forms;

namespace theshirtshopApp.ViewModel
{
   public class LedgerPageViewModel : ModelBase
    {
        private List<Ladger_Class> Store_Ladger_ClassList;
        private ObservableCollection<Ladger_Class> Old_Ladger_ClassList;

        private List<Use_Ladger_Class> Ladger_ClassList { get; set; }
      //  var date = new DateTime();
        private DateTime startDate = new DateTime(System.DateTime.Now.Year, 1, 1);

        private DateTime endDate = new DateTime(System.DateTime.Now.Year, 12,31);
        public ICommand GetCommand { get; }
        private IAllDataServices _IAllDataServices;
        private bool RequestList = false;
        public LedgerPageViewModel()
        {
            Store_Ladger_ClassList = new List<Ladger_Class>();
            Old_Ladger_ClassList = new ObservableCollection<Ladger_Class>();
               _IAllDataServices = new AllDataServices();
            GetCommand = new Command(async () => await GetFilterDataAsync());
            GetData();
        }

        private async Task GetFilterDataAsync()
        {
            var Wait = UserDialogs.Instance.Loading("Wait...", Cancel(), "Cancel", true, MaskType.Black);
            Wait.Show();
            if(_StartDate != null && _EndDate != null)
            {
                if(startDate<endDate)
                {
                    _List = Old_Ladger_ClassList.Where(x => x.createdDate.Date >= _StartDate.Date && x.createdDate.Date <= _EndDate.Date).ToList();

                    if(_List.Count ==0)
                    {
                        await App.Current.MainPage.DisplayAlert("Oops!", "Record Not Found", "Ok");
                    }
                   
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("Oops!", "Start date greater then end date", "Ok");

                }
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Oops!", "Please select start date and end date", "Ok");
            }
            Wait.Dispose();
        }

        public async Task GetData()
        {
            try
            {
                var Wait = UserDialogs.Instance.Loading("Wait...", Cancel(), "Cancel", true, MaskType.Black);
                Wait.Show();
                JObject result = await _IAllDataServices.GetAllLadger();
                if (result != null)
                {
                    var type = (int)result["Type"];
                    if (type == 1)
                    {
                        _List = JsonConvert.DeserializeObject<ObservableCollection<Ladger_Class>>(result["Result"].ToString()).ToList();
                        Old_Ladger_ClassList = JsonConvert.DeserializeObject<ObservableCollection<Ladger_Class>>(result["Result"].ToString());
                        if (_List.Count == 0)
                        {
                            await App.Current.MainPage.DisplayAlert("Oops!", "Record Not Found", "Ok");
                            _Request_List = false;
                        }
                        else
                        {
                            _Request_List = true;
                            //Old_Ladger_ClassList = new ObservableCollection<Use_Ladger_Class>();
                            //foreach (var ad in Store_Ladger_ClassList)
                            //{
                            //    Use_Ladger_Class ap = new Use_Ladger_Class();
                            //    if (!string.IsNullOrEmpty(ad.transactionDate))
                            //    {
                            //        ap.Credit = 0;
                            //        ap.Date = Convert.ToDateTime(ad.transactionDate);
                            //        ap.Debit = ad.Debit;
                            //        ap.OutstandingAmount = ad.OutstandingAmount;
                            //        ap.Type = "Transaction";
                            //    }
                            //    else
                            //    {
                            //        ap.Credit = ad.Credit;
                            //        ap.Date = Convert.ToDateTime(ad.createdDate);
                            //        ap.Debit = 0;
                            //        ap.OutstandingAmount = ad.OutstandingAmount;
                            //        ap.Type = "Order";
                            //    }
                            //    Old_Ladger_ClassList.Add(ap);
                            //}
                        }

                    }
                    else
                    {
                        await App.Current.MainPage.DisplayAlert("Error!", (string)result["ResponseMessage"], "Ok");
                    }
                    Wait.Hide();
                }


            }
            catch (Exception ee)
            {
                await App.Current.MainPage.DisplayAlert("Error!", ee.Message, "Ok");
            }
        }

        private Action Cancel()
        {
            var ca = new CancellationTokenSource();
            return ca.Cancel;
        }
        public List<Ladger_Class> _List
        {
            get { return Store_Ladger_ClassList; }
            set
            {
                Store_Ladger_ClassList = value;
                NotifyPropertyChanged();
            }
        }
        //public List<Use_Ladger_Class> _List
        //{
        //    get { return Ladger_ClassList; }
        //    set
        //    {
        //        Ladger_ClassList = value;
        //        NotifyPropertyChanged();
        //    }
        //}
        public bool _Request_List
        {
            get { return RequestList; }
            set
            {
                RequestList = value;
                NotifyPropertyChanged();
            }
        }
        
        public DateTime _StartDate
        {
            get { return startDate; }
            set
            {
                startDate = value;
                NotifyPropertyChanged();
            }
        }
       
        public DateTime _EndDate
        {
            get { return endDate; }
            set
            {
                endDate = value;
                NotifyPropertyChanged();
            }
        }
    }
}
