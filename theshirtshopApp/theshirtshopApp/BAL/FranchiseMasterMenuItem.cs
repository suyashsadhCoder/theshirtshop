using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace theshirtshopApp.View
{

    public class FranchiseMasterMenuItem
    {
        //public FranchiseMasterMenuItem()
        //{
        //    TargetType = typeof(HomePage);
        //}
     
        public string Title { get; set; }
        public string Icon { get; set; }
        public string Count { get; set; }
        public string Url { get; set; }
        public Type TargetType { get; set; }


       // public string Title { get; set; }
       
       // public Type TargetType { get; set; }
    }
}