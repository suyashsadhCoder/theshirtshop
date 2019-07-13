using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using BalLayer_TheShirtShop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using theshirtshopApp.Interface;
using Xamarin.Forms;
using Application = Xamarin.Forms.Application;

namespace theshirtshopApp.BAL
{
    public class AccountInformation : IAccountInformation
    {
        public void SaveCredentials(AppLoginClass alc)
        {
            Application.Current.Properties["Key"] = alc.User_Key;
            Application.Current.Properties["UserName"] = alc.AppUser_Name;
            Application.Current.Properties["Password"] = alc.AppUser_Password;
            Application.Current.Properties["OtherId"] = alc.Other_Id;
        }
    }
}
