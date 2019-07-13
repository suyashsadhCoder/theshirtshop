using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace theshirtshopApp.BAL
{
   public class AuthClass
    {

        public AuthClass()
        {
            this.SetKey();
            this.SetOtherId();
            this.SetPassword();
            this.SetUserName();

        }
        public string Key = string.Empty;
        public string UserName = string.Empty;
        public string Password = string.Empty;
        public int OtherId = 0;

      
        public string _Key
        {
            get { return Key; }
            set { Key = value;

                SetKey();
            }

        }
        public string _UserName
        {
            get { return UserName; }
            set
            {
                UserName = value;

                SetUserName();
            }

        }
        public string _Password
        {
            get { return Password; }
            set
            {
                Password = value;

                SetPassword();
            }

        }
        public int _OtherId
        {
            get { return OtherId; }
            set
            {
                OtherId = value;

                SetOtherId();
            }

        }

        public void SetKey()
        {
            Key =(string) Application.Current.Properties["Key"];
        }
        public void SetUserName()
        {
            UserName = (string)Application.Current.Properties["UserName"];
        }
        public void SetPassword()
        {
            Password = (string)Application.Current.Properties["Password"];
        }
        public void SetOtherId()
        {
            OtherId = (int)Application.Current.Properties["OtherId"];
        }
    }
}
