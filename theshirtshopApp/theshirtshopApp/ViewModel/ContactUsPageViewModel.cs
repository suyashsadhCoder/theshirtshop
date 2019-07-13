using Acr.UserDialogs;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using theshirtshopApp.DAL;
using theshirtshopApp.Interface;
using theshirtshopApp.Validation;

namespace theshirtshopApp.ViewModel
{
   public class ContactUsPageViewModel : ModelBase
    {
        private IAllDataServices IAllDataServices_data;
      
        public ContactUsPageViewModel()
        {
            
            IAllDataServices_data = new AllDataServices();
          getData();
            
        }
        public string _CompanyName
        {
            get { return CompanyName; }
            set
            {
                CompanyName = value;
                NotifyPropertyChanged();
            }
        }
        public string _OwnerName
        {
            get { return OwnerName; }
            set
            {
                OwnerName = value;
                NotifyPropertyChanged();
            }
        }
        public string _ContectNo
        {
            get { return ContectNo; }
            set
            {
                ContectNo = value;
                NotifyPropertyChanged();
            }
        }
        public string _MailId
        {
            get { return MailId; }
            set
            {
                MailId = value;
                NotifyPropertyChanged();
            }
        }
        public string _Address
        {
            get { return Address; }
            set
            {
                Address = value;
                NotifyPropertyChanged();
            }
        }
        public string CompanyName { get;  set; }
        public string OwnerName { get;  set; }
        public string ContectNo { get;  set; }
        public string MailId { get;  set; }
        public string Address { get;  set; }
        private Action Cancel()
        {
            var cancelSrc = new CancellationTokenSource();
            return cancelSrc.Cancel;
        }
        private async Task getData()
        {
            var Wait = UserDialogs.Instance.Loading("Wait...", Cancel(), "Cancel", true, MaskType.Black);
            Wait.Show();
            JObject result = await IAllDataServices_data.GetAdminInfo();
            if (result != null)
            {
                string type = result["Type"].ToString();

                if (type == "1")
                {
                        _CompanyName = (string)result["Result"]["Company_Name1"];
                        _ContectNo = (string)result["Result"]["Mobile_No"]+','+ (string)result["Result"]["Alternate_MobileNo"];
                        _Address = (string)result["Result"]["Address"];
                        _MailId = (string)result["Result"]["Email_ID"];
                        _OwnerName = (string)result["Result"]["Owner_Name1"];
                        
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("Error!", (string)result["ResponseMessage"], "Ok");
                }


            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Oops!", "Please Refresh Page And try Again....", "Ok");
            }
            Wait.Dispose();
        }



    }
}
