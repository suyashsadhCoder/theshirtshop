
using Acr.UserDialogs;
using BalLayer_TheShirtShop;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Plugin.Connectivity;
using Plugin.Connectivity.Abstractions;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using theshirtshopApp.BAL;
using theshirtshopApp.Interface;
using Xamarin.Forms;

namespace theshirtshopApp.DAL
{
    public class AllDataServices : IAllDataServices
    {

        //string Baseurl = "http://msit.satisfactionwebsolution.in/";
        string Baseurl = "http://dev.setquestionpaper.com/";

        //All Post Method
        public async Task<JObject> CheckLoginAsync(AppLoginClass _AppLoginClass)
        {
            if (CheckConnection())
            {
                using (var client = new HttpClient())
                {
                    string url = Baseurl + "/API/Api/LoginApi/CheckLogin";
                    client.BaseAddress = new Uri(Baseurl);

                    client.DefaultRequestHeaders.Clear();

                    client.DefaultRequestHeaders.ExpectContinue = false;
                    //Define request data format  
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var jsonRequest = JsonConvert.SerializeObject(_AppLoginClass);

                    var content = new StringContent(jsonRequest, null, "application/json");
                    HttpResponseMessage Res = null;
                    Res = await client.PostAsync(url, content);
                    JObject results = new JObject();
                    if (Res.IsSuccessStatusCode)
                    {
                        var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                        results = JObject.Parse(EmpResponse);
                    }
                    return results;
                }
            }
            else
            {
                JObject jb = new JObject();
                jb.Add("Type", "-1");
                jb.Add("ResponseMessage", " Network not available. Please check your  internet connection and try again..... ");
                return jb;
            }
        }
        public async Task<JObject> SendOrderRequest(FranchiseRequest_Class _FranchiseRequest_Class)
        {
            if (CheckConnection())
            {
                using (var client = new HttpClient())
                {
                    string url = Baseurl + "/API/api/FranchiseStockRequestApi/SaveFranchiseStockRequest";
                    client.BaseAddress = new Uri(Baseurl);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Add("Key", Application.Current.Properties["Key"].ToString());
                    client.DefaultRequestHeaders.Add("UserName", Application.Current.Properties["UserName"].ToString());
                    client.DefaultRequestHeaders.Add("Password", Application.Current.Properties["Password"].ToString());
                    var jsonRequest = JsonConvert.SerializeObject(_FranchiseRequest_Class);
                    var content = new StringContent(jsonRequest, null, "application/json");
                    HttpResponseMessage Res = await client.PostAsync(url, content);
                    JObject results = new JObject();
                    if (Res.IsSuccessStatusCode)
                    {
                        var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                        results = JObject.Parse(EmpResponse);
                    }
                    return results;
                }
            }
            else
            {
                JObject jb = new JObject();

                jb.Add("Type", "0");
                jb.Add("ResponseMessage", " Network not available. Please check your  internet connection and try again..... ");

                return jb;
            }
        }
        public async Task<JObject> ChangePassword(AppLoginClass appLoginClass)
        {
            if (CheckConnection())
            {
                using (var client = new HttpClient())
                {
                    string url = Baseurl + "/API/Api/ChangePasswordApi/ChangePassword";
                    client.BaseAddress = new Uri(Baseurl);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.ExpectContinue = false;
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Add("Key", Application.Current.Properties["Key"].ToString());
                    client.DefaultRequestHeaders.Add("UserName", Application.Current.Properties["UserName"].ToString());
                    client.DefaultRequestHeaders.Add("Password", Application.Current.Properties["Password"].ToString());
                    var jsonRequest = JsonConvert.SerializeObject(appLoginClass);
                    var content = new StringContent(jsonRequest, null, "application/json");
                    HttpResponseMessage Res = null;
                    Res = await client.PostAsync(url, content);
                    JObject results = new JObject();
                    if (Res.IsSuccessStatusCode)
                    {
                        var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                        results = JObject.Parse(EmpResponse);
                    }
                    return results;
                }
            }
            else
            {
                JObject jb = new JObject();

                jb.Add("Type", "0");
                jb.Add("ResponseMessage", " Network not available. Please check your  internet connection and try again..... ");

                return jb;
            }
        }
        public async Task<JObject> RetailerRegistration(RetailerMaster_Class retailerMaster_Class)
        {
            if (CheckConnection())
            {
                using (var client = new HttpClient())
                {
                    string url = Baseurl + "/API/Api/RetailerRegistrationApi/SaveReatailerInfo";
                    client.BaseAddress = new Uri(Baseurl);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.ExpectContinue = false;
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Add("Key", Application.Current.Properties["Key"].ToString());
                    client.DefaultRequestHeaders.Add("UserName", Application.Current.Properties["UserName"].ToString());
                    client.DefaultRequestHeaders.Add("Password", Application.Current.Properties["Password"].ToString());
                    var jsonRequest = JsonConvert.SerializeObject(retailerMaster_Class);
                    var content = new StringContent(jsonRequest, null, "application/json");
                    HttpResponseMessage Res = null;
                    Res = await client.PostAsync(url, content);
                    JObject results = new JObject();
                    if (Res.IsSuccessStatusCode)
                    {
                        var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                        results = JObject.Parse(EmpResponse);
                    }
                    return results;
                }
            }
            else
            {
                JObject jb = new JObject();

                jb.Add("Type", "0");
                jb.Add("ResponseMessage", " Network not available. Please check your  internet connection and try again..... ");

                return jb;
            }
        }
        public async Task<JObject> GetRetailerByMobileNo(RetailerMaster_Class retailerMaster_Class)
        {
            if (CheckConnection())
            {
                using (var client = new HttpClient())
                {
                    string url = Baseurl + "/API/Api/CashCollectionApi/GetRetailer_ByMobileNo";
                    client.BaseAddress = new Uri(Baseurl);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.ExpectContinue = false;
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Add("Key", Application.Current.Properties["Key"].ToString());
                    client.DefaultRequestHeaders.Add("UserName", Application.Current.Properties["UserName"].ToString());
                    client.DefaultRequestHeaders.Add("Password", Application.Current.Properties["Password"].ToString());
                    var jsonRequest = JsonConvert.SerializeObject(retailerMaster_Class);
                    var content = new StringContent(jsonRequest, null, "application/json");
                    HttpResponseMessage Res = null;
                    Res = await client.PostAsync(url, content);
                    JObject results = new JObject();
                    if (Res.IsSuccessStatusCode)
                    {
                        var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                        results = JObject.Parse(EmpResponse);
                    }
                    return results;
                }
            }
            else
            {
                JObject jb = new JObject();

                jb.Add("Type", "0");
                jb.Add("ResponseMessage", " Network not available. Please check your  internet connection and try again..... ");

                return jb;
            }
        }
        public async Task<JObject> GetOTP(RetailerMaster_Class retailerMaster_Class)
        {
            if (CheckConnection())
            {
                using (var client = new HttpClient())
                {
                    string url = Baseurl + "/API/Api/CashCollectionApi/SendOTP";
                    client.BaseAddress = new Uri(Baseurl);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.ExpectContinue = false;
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Add("Key", Application.Current.Properties["Key"].ToString());
                    client.DefaultRequestHeaders.Add("UserName", Application.Current.Properties["UserName"].ToString());
                    client.DefaultRequestHeaders.Add("Password", Application.Current.Properties["Password"].ToString());
                    var jsonRequest = JsonConvert.SerializeObject(retailerMaster_Class);
                    var content = new StringContent(jsonRequest, null, "application/json");
                    HttpResponseMessage Res = null;
                    Res = await client.PostAsync(url, content);
                    JObject results = new JObject();
                    if (Res.IsSuccessStatusCode)
                    {
                        var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                        results = JObject.Parse(EmpResponse);
                    }
                    return results;
                }
            }
            else
            {
                JObject jb = new JObject();

                jb.Add("Type", "0");
                jb.Add("ResponseMessage", " Network not available. Please check your  internet connection and try again..... ");

                return jb;
            }

        }
        public async Task<JObject> SaveCashCollection(CashCollection_Class cashCollection_Class)
        {
            if (CheckConnection())
            {
                using (var client = new HttpClient())
                {
                    string url = Baseurl + "/API/Api/CashCollectionApi/SaveCashCollection";
                    client.BaseAddress = new Uri(Baseurl);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.ExpectContinue = false;
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Add("Key", Application.Current.Properties["Key"].ToString());
                    client.DefaultRequestHeaders.Add("UserName", Application.Current.Properties["UserName"].ToString());
                    client.DefaultRequestHeaders.Add("Password", Application.Current.Properties["Password"].ToString());
                    var jsonRequest = JsonConvert.SerializeObject(cashCollection_Class);
                    var content = new StringContent(jsonRequest, null, "application/json");
                    HttpResponseMessage Res = null;
                    Res = await client.PostAsync(url, content);
                    JObject results = new JObject();
                    if (Res.IsSuccessStatusCode)
                    {
                        var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                        results = JObject.Parse(EmpResponse);
                    }
                    return results;
                }

            }
            else
            {
                JObject jb = new JObject();

                jb.Add("Type", "0");
                jb.Add("ResponseMessage", " Network not available. Please check your  internet connection and try again..... ");

                return jb;
            }
        }
        public async Task<JObject> FranchiseRegistration(FranchiseMasters FranchiseMasters_Class)
        {
            if (CheckConnection())
            {
                using (var client = new HttpClient())
                {
                    string url = Baseurl + "/API/Api/FranchiseRegistrationApi/SaveFranchise";
                    client.BaseAddress = new Uri(Baseurl);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.ExpectContinue = false;
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Add("Key", Application.Current.Properties["Key"].ToString());
                    client.DefaultRequestHeaders.Add("UserName", Application.Current.Properties["UserName"].ToString());
                    client.DefaultRequestHeaders.Add("Password", Application.Current.Properties["Password"].ToString());
                    var jsonRequest = JsonConvert.SerializeObject(FranchiseMasters_Class);
                    var content = new StringContent(jsonRequest, null, "application/json");
                    HttpResponseMessage Res = null;
                    Res = await client.PostAsync(url, content);
                    JObject results = new JObject();
                    if (Res.IsSuccessStatusCode)
                    { 
                        var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                        results = JObject.Parse(EmpResponse);
                    }
                    return results;
                }

            }
            else
            {
                JObject jb = new JObject();

                jb.Add("Type", "0");
                jb.Add("ResponseMessage", " Network not available. Please check your  internet connection and try again..... ");

                return jb;
            }
        }
        public async Task<JObject> CustomerRegistration(Customer_Class Customer_Class_Data)
        {
            if (CheckConnection())
            {
                using (var client = new HttpClient())
                {
                    string url = Baseurl + "/API/Api/CustomerRegistrationApi/SaveCustomer";
                    client.BaseAddress = new Uri(Baseurl);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.ExpectContinue = false;
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Add("Key", Application.Current.Properties["Key"].ToString());
                    client.DefaultRequestHeaders.Add("UserName", Application.Current.Properties["UserName"].ToString());
                    client.DefaultRequestHeaders.Add("Password", Application.Current.Properties["Password"].ToString());
                    var jsonRequest = JsonConvert.SerializeObject(Customer_Class_Data);
                    var content = new StringContent(jsonRequest, null, "application/json");
                    HttpResponseMessage Res = null;
                    Res = await client.PostAsync(url, content);
                    JObject results = new JObject();
                    if (Res.IsSuccessStatusCode)
                    {
                        var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                        results = JObject.Parse(EmpResponse);
                    }
                    return results;
                }
        }
            else
            {
                JObject jb = new JObject();

        jb.Add("Type", "0");
                jb.Add("ResponseMessage", " Network not available. Please check your  internet connection and try again..... ");
              
                return jb;
            }
}
        public async Task<JObject> ReferralRegistration(FranchiseReferral_Class FranchiseReferral_Class_Data)
        {
            if (CheckConnection())
            {
                using (var client = new HttpClient())
                {
                    string url = Baseurl + "/API/Api/FranchiseReferralApi/SaveReferral";
                    client.BaseAddress = new Uri(Baseurl);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.ExpectContinue = false;
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Add("Key", Application.Current.Properties["Key"].ToString());
                    client.DefaultRequestHeaders.Add("UserName", Application.Current.Properties["UserName"].ToString());
                    client.DefaultRequestHeaders.Add("Password", Application.Current.Properties["Password"].ToString());
                    var jsonRequest = JsonConvert.SerializeObject(FranchiseReferral_Class_Data);
                    var content = new StringContent(jsonRequest, null, "application/json");
                    HttpResponseMessage Res = null;
                    Res = await client.PostAsync(url, content);
                    JObject results = new JObject();
                    if (Res.IsSuccessStatusCode)
                    {
                        var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                        results = JObject.Parse(EmpResponse);
                    }
                    return results;
                }
            }
            else
            {
                JObject jb = new JObject();

                jb.Add("Type", "0");
                jb.Add("ResponseMessage", " Network not available. Please check your  internet connection and try again..... ");

                return jb;
            }
        }
        public async Task<JObject> SaveFeedBack(FranchiseFeedback_Class FranchiseFeedback_Class_Data)
        {
            if (CheckConnection())
            {
                using (var client = new HttpClient())
                {
                    string url = Baseurl + "/API/Api/FranchiseFeedbackApi/SaveFeeback";
                    client.BaseAddress = new Uri(Baseurl);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.ExpectContinue = false;
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Add("Key", Application.Current.Properties["Key"].ToString());
                    client.DefaultRequestHeaders.Add("UserName", Application.Current.Properties["UserName"].ToString());
                    client.DefaultRequestHeaders.Add("Password", Application.Current.Properties["Password"].ToString());
                    var jsonRequest = JsonConvert.SerializeObject(FranchiseFeedback_Class_Data);
                    var content = new StringContent(jsonRequest, null, "application/json");
                    HttpResponseMessage Res = null;
                    Res = await client.PostAsync(url, content);
                    JObject results = new JObject();
                    if (Res.IsSuccessStatusCode)
                    {
                        var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                        results = JObject.Parse(EmpResponse);
                    }
                    return results;
                }
            }
            else
            {
                JObject jb = new JObject();

                jb.Add("Type", "0");
                jb.Add("ResponseMessage", " Network not available. Please check your  internet connection and try again..... ");

                return jb;
            }
        }
        public async Task<JObject> GetCustomerByMobileNo(Customer_Class Customer_Class_Data)
        {
            if (CheckConnection())
            {
                using (var client = new HttpClient())
                {
                    string url = Baseurl + "/API/Api/CustomerRegistrationApi/CheckMobleNoForCustomer";
                    client.BaseAddress = new Uri(Baseurl);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.ExpectContinue = false;
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Add("Key", Application.Current.Properties["Key"].ToString());
                    client.DefaultRequestHeaders.Add("UserName", Application.Current.Properties["UserName"].ToString());
                    client.DefaultRequestHeaders.Add("Password", Application.Current.Properties["Password"].ToString());
                    var jsonRequest = JsonConvert.SerializeObject(Customer_Class_Data);
                    var content = new StringContent(jsonRequest, null, "application/json");
                    HttpResponseMessage Res = null;
                    Res = await client.PostAsync(url, content);
                    JObject results = new JObject();
                    if (Res.IsSuccessStatusCode)
                    {
                        var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                        results = JObject.Parse(EmpResponse);
                    }
                    return results;
                }
            }
            else
            {
                JObject jb = new JObject();

                jb.Add("Type", "0");
                jb.Add("ResponseMessage", " Network not available. Please check your  internet connection and try again..... ");

                return jb;
            }
        }
        public async Task<JObject> SaveFeedBack(OrderMaster_Class OrderMaster_Class_Data)
        {
            if (CheckConnection())
            {
               
                using (var client = new HttpClient())
                {
                    string url = Baseurl + "/API/Api/FranchiseOrderApi/SaveOrderMaster_ByFranchise";
                    client.BaseAddress = new Uri(Baseurl);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.ExpectContinue = false;
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Add("Key", Application.Current.Properties["Key"].ToString());
                    client.DefaultRequestHeaders.Add("UserName", Application.Current.Properties["UserName"].ToString());
                    client.DefaultRequestHeaders.Add("Password", Application.Current.Properties["Password"].ToString());
                    var jsonRequest = JsonConvert.SerializeObject(OrderMaster_Class_Data);
                    var content = new StringContent(jsonRequest, null, "application/json");
                    HttpResponseMessage Res = null;
                    Res = await client.PostAsync(url, content);
                    JObject results = new JObject();
                    if (Res.IsSuccessStatusCode)
                    {
                        var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                        results = JObject.Parse(EmpResponse);
                    }
                    return results;
                }
            }
            else
            {
                JObject jb = new JObject();

                jb.Add("Type", "0");
                jb.Add("ResponseMessage", " Network not available. Please check your  internet connection and try again..... ");

                return jb;
            }
        }
        public async Task<JObject> SaveSellCustomer(FranchiseSell_Class FranchiseSell_Class_Data)
        {
            if (CheckConnection())
            {
                using (var client = new HttpClient())
                {
                    string url = Baseurl + "/API/Api/FranchiseSellApi/SaveFranchiseStockRequest";
                    client.BaseAddress = new Uri(Baseurl);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.ExpectContinue = false;
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Add("Key", Application.Current.Properties["Key"].ToString());
                    client.DefaultRequestHeaders.Add("UserName", Application.Current.Properties["UserName"].ToString());
                    client.DefaultRequestHeaders.Add("Password", Application.Current.Properties["Password"].ToString());
                    var jsonRequest = JsonConvert.SerializeObject(FranchiseSell_Class_Data);
                    var content = new StringContent(jsonRequest, null, "application/json");
                    HttpResponseMessage Res = null;
                    Res = await client.PostAsync(url, content);
                    JObject results = new JObject();
                    if (Res.IsSuccessStatusCode)
                    {
                        var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                        results = JObject.Parse(EmpResponse);
                    }
                    return results;
                }
            }
            else
            {
                JObject jb = new JObject();

                jb.Add("Type", "0");
                jb.Add("ResponseMessage", " Network not available. Please check your  internet connection and try again..... ");

                return jb;
            }
        }
        public async Task<JObject> SendOTPCustomer(Customer_Class Customer_Class_Data)
        {
            if (CheckConnection())
            {
                using (var client = new HttpClient())
                {
                    string url = Baseurl + "/API/Api/CustomerRegistrationApi/SendOTP";
                    client.BaseAddress = new Uri(Baseurl);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.ExpectContinue = false;
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Add("Key", Application.Current.Properties["Key"].ToString());
                    client.DefaultRequestHeaders.Add("UserName", Application.Current.Properties["UserName"].ToString());
                    client.DefaultRequestHeaders.Add("Password", Application.Current.Properties["Password"].ToString());
                    var jsonRequest = JsonConvert.SerializeObject(Customer_Class_Data);
                    var content = new StringContent(jsonRequest, null, "application/json");
                    HttpResponseMessage Res = null;
                    Res = await client.PostAsync(url, content);
                    JObject results = new JObject();
                    if (Res.IsSuccessStatusCode)
                    {
                        var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                        results = JObject.Parse(EmpResponse);
                    }
                    return results;
                }
            }
            else
            {
                JObject jb = new JObject();

                jb.Add("Type", "0");
                jb.Add("ResponseMessage", " Network not available. Please check your  internet connection and try again..... ");

                return jb;
            }
        }
        public async Task<JObject> SaveRetailerOrder(Employee_OrderGenerate_Class Employee_OrderGenerate_Class_Data)
        {
            if (CheckConnection())
            {
                using (var client = new HttpClient())
                {
                    string url = Baseurl + "/API/Api/RetailerOrderApi/SaveRetailerOrder";
                    client.BaseAddress = new Uri(Baseurl);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.ExpectContinue = false;
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Add("Key", Application.Current.Properties["Key"].ToString());
                    client.DefaultRequestHeaders.Add("UserName", Application.Current.Properties["UserName"].ToString());
                    client.DefaultRequestHeaders.Add("Password", Application.Current.Properties["Password"].ToString());
                    var jsonRequest = JsonConvert.SerializeObject(Employee_OrderGenerate_Class_Data);
                    var content = new StringContent(jsonRequest, null, "application/json");
                    HttpResponseMessage Res = null;
                    Res = await client.PostAsync(url, content);
                    JObject results = new JObject();
                    if (Res.IsSuccessStatusCode)
                    {
                        var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                        results = JObject.Parse(EmpResponse);
                    }
                    return results;
                }
            }
            else
            {
                JObject jb = new JObject();

                jb.Add("Type", "0");
                jb.Add("ResponseMessage", " Network not available. Please check your  internet connection and try again..... ");

                return jb;
            }
        }
        public async Task<JObject> SaveGoodReturn(FranchiseGoodReturn_Class FranchiseGoodReturn_Class_Date)
        {
            if (CheckConnection())
            {
                using (var client = new HttpClient())
                {
                    string url = Baseurl + "/API/Api/FranchiseGoodReturnApi/SaveFranchiseGoodReturn";
                    client.BaseAddress = new Uri(Baseurl);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.ExpectContinue = false;
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Add("Key", Application.Current.Properties["Key"].ToString());
                    client.DefaultRequestHeaders.Add("UserName", Application.Current.Properties["UserName"].ToString());
                    client.DefaultRequestHeaders.Add("Password", Application.Current.Properties["Password"].ToString());
                    var jsonRequest = JsonConvert.SerializeObject(FranchiseGoodReturn_Class_Date);
                    var content = new StringContent(jsonRequest, null, "application/json");
                    HttpResponseMessage Res = null;
                    Res = await client.PostAsync(url, content);
                    JObject results = new JObject();
                    if (Res.IsSuccessStatusCode)
                    {
                        var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                        results = JObject.Parse(EmpResponse);
                    }
                    return results;
                }
            }
            else
            {
                JObject jb = new JObject();

                jb.Add("Type", "0");
                jb.Add("ResponseMessage", " Network not available. Please check your  internet connection and try again..... ");

                return jb;
            }
        }
        public async Task<JObject> FistAdminPayAmount(int Id)
        {
            if (CheckConnection())
            {
                using (var client = new HttpClient())
                {
                    string url = Baseurl + "/API/api/FranchiseCustomerSellApi/FranchiseCustomerSellAndPayDetails_Get?Id=" + Id;
                    client.BaseAddress = new Uri(Baseurl);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.ExpectContinue = false;
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    //client.DefaultRequestHeaders.Add("Key", Application.Current.Properties["Key"].ToString());
                    //client.DefaultRequestHeaders.Add("UserName", Application.Current.Properties["UserName"].ToString());
                    //client.DefaultRequestHeaders.Add("Password", Application.Current.Properties["Password"].ToString());
                   // var jsonRequest = JsonConvert.SerializeObject(FranchiseGoodReturn_Class_Date);
                  //  var content = new StringContent(jsonRequest, null, "application/json");
                    HttpResponseMessage Res = null;
                    Res = await client.PostAsync(url, null);
                    JObject results = new JObject();
                    if (Res.IsSuccessStatusCode)
                    {
                        var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                        results = JObject.Parse(EmpResponse);
                    }
                    return results;
                }
            }
            else
            {
                JObject jb = new JObject();

                jb.Add("Type", "0");
                jb.Add("ResponseMessage", " Network not available. Please check your  internet connection and try again..... ");

                return jb;
            }
        }

        public async Task<JObject> SecondAdminPayAmount(int Month, int Year, string TermType, int Id)
        {
            if (CheckConnection())
            {
                using (var client = new HttpClient())
                {
                    string url = Baseurl + "/API/api/FranchiseCustomerSellApi/FranchiseCustomerSellAndPayDetails_Get?Month=" + Month + "&Year=" + Year + "&TermType=" + TermType + "&Id=" + Id;
                    client.BaseAddress = new Uri(Baseurl);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.ExpectContinue = false;
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage Res = null;
                    Res = await client.PostAsync(url, null);
                    JObject results = new JObject();
                    if (Res.IsSuccessStatusCode)
                    {
                        var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                        results = JObject.Parse(EmpResponse);
                    }
                    return results;

                 
                }
            }
            else
            {
                JObject jb = new JObject();

                jb.Add("Type", "0");
                jb.Add("ResponseMessage", " Network not available. Please check your  internet connection and try again..... ");

                return jb;
            }
        }
        public async Task<JObject> NotificationUpdate(string Types)
        {
            if (CheckConnection())
            {
                using (var client = new HttpClient())
                {
                    string url = Baseurl + "/API/api/FranchiseNotificationApi/Update_NotifactionStatus?_Type=" + Types;
                    client.BaseAddress = new Uri(Baseurl);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.ExpectContinue = false;
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Add("Key", Application.Current.Properties["Key"].ToString());
                    client.DefaultRequestHeaders.Add("UserName", Application.Current.Properties["UserName"].ToString());
                    client.DefaultRequestHeaders.Add("Password", Application.Current.Properties["Password"].ToString());
                   // var jsonRequest = JsonConvert.SerializeObject(FranchiseGoodReturn_Class_Date);
                  //  var content = new StringContent(jsonRequest, null, "application/json");
                    HttpResponseMessage Res = null;
                    Res = await client.PostAsync(url, null);
                    JObject results = new JObject();
                    if (Res.IsSuccessStatusCode)
                    {
                        var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                        results = JObject.Parse(EmpResponse);
                    }
                    return results;
                }
            }
            else
            {
                JObject jb = new JObject();

                jb.Add("Type", "0");
                jb.Add("ResponseMessage", " Network not available. Please check your  internet connection and try again..... ");

                return jb;
            }
        }
        /// <summary>
        /// All Get Method
        /// </summary>
        /// <returns></returns>
        public async Task<JObject> GetStateAndCity()
        {
            if (CheckConnection())
            {
                var Wait = UserDialogs.Instance.Loading("Loading", null, null, true, MaskType.Black);
                Wait.Show();
                using (var client = new HttpClient())
                {
                    string url = Baseurl + "/API/api/CityApi/GetCity";
                    client.BaseAddress = new Uri(Baseurl);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Add("Key", Application.Current.Properties["Key"].ToString());
                    client.DefaultRequestHeaders.Add("UserName", Application.Current.Properties["UserName"].ToString());
                    client.DefaultRequestHeaders.Add("Password", Application.Current.Properties["Password"].ToString());
                    HttpResponseMessage Res = await client.GetAsync(url);
                    JObject results = new JObject();
                    if (Res.IsSuccessStatusCode)
                    {
                        var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                        results = JObject.Parse(EmpResponse);
                    }
                    Wait.Dispose();
                    return results;
                }
            }
            else
            {
                JObject jb = new JObject();

                jb.Add("Type", "0");
                jb.Add("ResponseMessage", " Network not available. Please check your  internet connection and try again..... ");

                return jb;
            }
        }
        public async Task<JObject> GetProfile(string v)
        {
            if (CheckConnection())
            {
                using (var client = new HttpClient())
                {
                    string url = Baseurl + "/API/Api/ProfileApi/GetOtherProfile?v="+v;
                     client.BaseAddress = new Uri(Baseurl);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Add("Key", Application.Current.Properties["Key"].ToString());
                    client.DefaultRequestHeaders.Add("UserName", Application.Current.Properties["UserName"].ToString());
                    client.DefaultRequestHeaders.Add("Password", Application.Current.Properties["Password"].ToString());
                 
                    HttpResponseMessage Res = null;
                    Res =  await client.GetAsync(url);
                    JObject results = new JObject();
                    if (Res.IsSuccessStatusCode)
                    {
                        var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                        results = JObject.Parse(EmpResponse);
                    }
                 
                    return results;
                }
            }
            else
            {
                JObject jb = new JObject();

                jb.Add("Type", "0");
                jb.Add("ResponseMessage", " Network not available. Please check your  internet connection and try again..... ");

                return jb;
            }
        }
        public async Task<JObject> GetProfileForDriver(string v)
        {
            if (CheckConnection())
            {
                using (var client = new HttpClient())
                {
                    string url = Baseurl + "/API/Api/ProfileApi/GetOtherProfile?v=" + v;
                    client.BaseAddress = new Uri(Baseurl);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Add("Key", Application.Current.Properties["Key"].ToString());
                    client.DefaultRequestHeaders.Add("UserName", Application.Current.Properties["UserName"].ToString());
                    client.DefaultRequestHeaders.Add("Password", Application.Current.Properties["Password"].ToString());

                    HttpResponseMessage Res = null;
                    Res = await client.GetAsync(url);
                    JObject results = new JObject();
                    if (Res.IsSuccessStatusCode)
                    {
                        var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                        results = JObject.Parse(EmpResponse);
                    }

                    return results;
                }
            }
            else
            {
                JObject jb = new JObject();

                jb.Add("Type", "0");
                jb.Add("ResponseMessage", " Network not available. Please check your  internet connection and try again..... ");

                return jb;
            }
        }
        public async Task<JObject> GetAdminInfo()
        {
            if (CheckConnection())
            {
                using (var client = new HttpClient())
                {
                    string url = Baseurl + "/API/Api/AdminInfoApi/GetAdminInfo";
                    client.BaseAddress = new Uri(Baseurl);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Add("Key", Application.Current.Properties["Key"].ToString());
                    client.DefaultRequestHeaders.Add("UserName", Application.Current.Properties["UserName"].ToString());
                    client.DefaultRequestHeaders.Add("Password", Application.Current.Properties["Password"].ToString());
                    HttpResponseMessage Res = await client.GetAsync(url);
                    JObject results = new JObject();
                    if (Res.IsSuccessStatusCode)
                    {
                        var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                        results = JObject.Parse(EmpResponse);
                    }
                    return results;
                }
            }

            else
            {
                JObject jb = new JObject();

                jb.Add("Type", "0");
                jb.Add("ResponseMessage", " Network not available. Please check your  internet connection and try again..... ");

                return jb;
            }
        }
        public async Task<JObject> GetEmployeeStock()
        {
            if (CheckConnection())
            {
                using (var client = new HttpClient())
                {
                    string url = Baseurl + "/API/Api/EmployeeStockApi/GetEmpStock";
                   client.BaseAddress = new Uri(Baseurl);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Add("Key", Application.Current.Properties["Key"].ToString());
                    client.DefaultRequestHeaders.Add("UserName", Application.Current.Properties["UserName"].ToString());
                    client.DefaultRequestHeaders.Add("Password", Application.Current.Properties["Password"].ToString());
                    HttpResponseMessage Res = await client.GetAsync(url);
                    JObject results = new JObject();
                    if (Res.IsSuccessStatusCode)
                    {
                        var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                        results = JObject.Parse(EmpResponse);
                    }
                    return results;
                }
            }
            else
            {
                JObject jb = new JObject();

                jb.Add("Type", "0");
                jb.Add("ResponseMessage", " Network not available. Please check your  internet connection and try again..... ");

                return jb;
            }
        }
        public async Task<JObject> GetAllSendStockRequest()
        {
            if (CheckConnection())
            {
                using (var client = new HttpClient())
                {
                    string url = Baseurl + "/API/Api/FranchiseStockRequestApi/GetFranchiseStockRequest";
                    client.BaseAddress = new Uri(Baseurl);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Add("Key", Application.Current.Properties["Key"].ToString());
                    client.DefaultRequestHeaders.Add("UserName", Application.Current.Properties["UserName"].ToString());
                    client.DefaultRequestHeaders.Add("Password", Application.Current.Properties["Password"].ToString());
                    HttpResponseMessage Res = await client.GetAsync(url);
                    JObject results = new JObject();
                    if (Res.IsSuccessStatusCode)
                    {
                        var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                        results = JObject.Parse(EmpResponse);
                    }
                    return results;
                }
            }
            else
            {
                JObject jb = new JObject();

                jb.Add("Type", "0");
                jb.Add("ResponseMessage", " Network not available. Please check your  internet connection and try again..... ");

                return jb;
            }
        }
        public async Task<JObject> GetFranchiseStock()
        {
            if (CheckConnection())
            {
                using (var client = new HttpClient())
                {
                 
                    string url = Baseurl + "/API/Api/FranchiseStockApi/GetFranchiseStock";
                    client.BaseAddress = new Uri(Baseurl);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Add("Key", Application.Current.Properties["Key"].ToString());
                    client.DefaultRequestHeaders.Add("UserName", Application.Current.Properties["UserName"].ToString());
                    client.DefaultRequestHeaders.Add("Password", Application.Current.Properties["Password"].ToString());
                    HttpResponseMessage Res = await client.GetAsync(url);
                
                    JObject results = new JObject();
                    if (Res.IsSuccessStatusCode)
                    {
                        var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                        results = JObject.Parse(EmpResponse);
                    }
                    return results;
                }
            }
            else
            {
                JObject jb = new JObject();

                jb.Add("Type", "0");
                jb.Add("ResponseMessage", " Network not available. Please check your  internet connection and try again..... ");

                return jb;
            }
        }
        public async Task<JObject> GetAllUpCommingOrder()
        {
            if (CheckConnection())
            {
                using (var client = new HttpClient())
                {
                    string url = Baseurl + "/API/Api/FranchiseOrderApi/GetOrderByFranchise";
                    client.BaseAddress = new Uri(Baseurl);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Add("Key", Application.Current.Properties["Key"].ToString());
                    client.DefaultRequestHeaders.Add("UserName", Application.Current.Properties["UserName"].ToString());
                    client.DefaultRequestHeaders.Add("Password", Application.Current.Properties["Password"].ToString());
                    HttpResponseMessage Res =await  client.GetAsync(url);
                    JObject results = new JObject();
                    if (Res.IsSuccessStatusCode)
                    {
                        var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                        results = JObject.Parse(EmpResponse);
                    }
                    return results;
                }
            }
            else
            {
                JObject jb = new JObject();

                jb.Add("Type", "0");
                jb.Add("ResponseMessage", " Network not available. Please check your  internet connection and try again..... ");

                return jb;
            }
        }
        public async Task<JObject> GetAllInvoiceOrder()
        {
            if (CheckConnection())
            {
                using (var client = new HttpClient())
                {
                    string url = Baseurl + "/API/Api/FranchiseOrderApi/GetOrder_ByFranchise_Recieved";
                    client.BaseAddress = new Uri(Baseurl);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Add("Key", Application.Current.Properties["Key"].ToString());
                    client.DefaultRequestHeaders.Add("UserName", Application.Current.Properties["UserName"].ToString());
                    client.DefaultRequestHeaders.Add("Password", Application.Current.Properties["Password"].ToString());
                   
                    HttpResponseMessage Res = await client.GetAsync(url);
                      
                    JObject results = new JObject();
                    if (Res.IsSuccessStatusCode)
                    {
                        var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                        results = JObject.Parse(EmpResponse);
                    }
                    return results;
                }
            }
            else
            {
                JObject jb = new JObject();

                jb.Add("Type", "0");
                jb.Add("ResponseMessage", " Network not available. Please check your  internet connection and try again..... ");

                return jb;
            }
        }
        public async Task<JObject> GetAllInvoiceCustomer()
        {
            if (CheckConnection())
            {
                using (var client = new HttpClient())
                {
                    string url = Baseurl + "/API/Api/FranchiseSellApi/GetCustomerIvoice";
                     client.BaseAddress = new Uri(Baseurl);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Add("Key", Application.Current.Properties["Key"].ToString());
                    client.DefaultRequestHeaders.Add("UserName", Application.Current.Properties["UserName"].ToString());
                    client.DefaultRequestHeaders.Add("Password", Application.Current.Properties["Password"].ToString());
                    HttpResponseMessage Res = await client.GetAsync(url);
                    JObject results = new JObject();
                    if (Res.IsSuccessStatusCode)
                    {
                        var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                        results = JObject.Parse(EmpResponse);
                    }
                    return results;
                }
            }
            else
            {
                JObject jb = new JObject();

                jb.Add("Type", "0");
                jb.Add("ResponseMessage", " Network not available. Please check your  internet connection and try again..... ");

                return jb;
            }
        }
               
        public async Task<JObject> GetHomeRecord(string v)
        {
            if (CheckConnection())
            {
                using (var client = new HttpClient())
                {
                    string url = Baseurl + "/API/Api/FranchiseRewardPointApi/GetFranchiseRewardPoint_ByFranchise?v=" + v;
                     client.BaseAddress = new Uri(Baseurl);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Add("Key", Application.Current.Properties["Key"].ToString());
                    client.DefaultRequestHeaders.Add("UserName", Application.Current.Properties["UserName"].ToString());
                    client.DefaultRequestHeaders.Add("Password", Application.Current.Properties["Password"].ToString());
                    HttpResponseMessage Res = null;
                     Res = await client.GetAsync(url);
                    JObject results = new JObject();
                    if (Res.IsSuccessStatusCode)
                    {
                        var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                        
                        results = JObject.Parse(EmpResponse);
                   
                    }
                    return results;
                }
            }
            else
            {
                JObject jb = new JObject();

                jb.Add("Type", "NotFound");
                jb.Add("ResponseMessage", " Network not available. Please check your  internet connection and try again..... ");

                return jb;
            }
        }
        public async Task<JObject> GetAllGoodReturn()
        {
            if (CheckConnection())
            {
                using (var client = new HttpClient())
                {
                    string url = Baseurl + "/API/Api/FranchiseGoodReturnApi/GetFranchiseGoodReturn_ByFranchise";
                    client.BaseAddress = new Uri(Baseurl);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Add("Key", Application.Current.Properties["Key"].ToString());
                    client.DefaultRequestHeaders.Add("UserName", Application.Current.Properties["UserName"].ToString());
                    client.DefaultRequestHeaders.Add("Password", Application.Current.Properties["Password"].ToString());
                    HttpResponseMessage Res = await client.GetAsync(url);
                    JObject results = new JObject();
                    if (Res.IsSuccessStatusCode)
                    {
                        var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                        results = JObject.Parse(EmpResponse);
                    }
                    return results;
                }
            }
            else
            {
                JObject jb = new JObject();

                jb.Add("Type", "0");
                jb.Add("ResponseMessage", " Network not available. Please check your  internet connection and try again..... ");

                return jb;
            }
        }
        public async Task<JObject> GetAllLadger()
        {
            if (CheckConnection())
            {
                using (var client = new HttpClient())
                {
                    string url = Baseurl + "/API/api/FranchiseLaserApi/SaveFranchiseStockRequest";
                    client.BaseAddress = new Uri(Baseurl);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Add("Key", Application.Current.Properties["Key"].ToString());
                    client.DefaultRequestHeaders.Add("UserName", Application.Current.Properties["UserName"].ToString());
                    client.DefaultRequestHeaders.Add("Password", Application.Current.Properties["Password"].ToString());
                    HttpResponseMessage Res =  client.GetAsync(url).Result;
                    JObject results = new JObject();
                    if (Res.IsSuccessStatusCode)
                    {
                        var EmpResponse = await Res.Content.ReadAsStringAsync();
                        results = JObject.Parse(EmpResponse);
                    }
                    return results;
                }
            }
            else
            {
                JObject jb = new JObject();

                jb.Add("Type", "0");
                jb.Add("ResponseMessage", " Network not available. Please check your  internet connection and try again..... ");

                return jb;
            }
        }
        public async Task<JObject> GetAllNotiication()
        {
            if (CheckConnection())
            {
                using (var client = new HttpClient())
                {
                    string url = Baseurl + "/API/api/FranchiseNotificationApi/AllNotification_Get";
                    client.BaseAddress = new Uri(Baseurl);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Add("Key", Application.Current.Properties["Key"].ToString());
                    client.DefaultRequestHeaders.Add("UserName", Application.Current.Properties["UserName"].ToString());
                    client.DefaultRequestHeaders.Add("Password", Application.Current.Properties["Password"].ToString());
                    HttpResponseMessage Res = await client.GetAsync(url);
                    JObject results = new JObject();
                    if (Res.IsSuccessStatusCode)
                    {
                        var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                        results = JObject.Parse(EmpResponse);
                    }
                    return results;
                }
            }
            else
            {
                JObject jb = new JObject();

                jb.Add("Type", "0");
                jb.Add("ResponseMessage", " Network not available. Please check your  internet connection and try again..... ");

                return jb;
            }
        }
   


        //Check network Connection
        private bool CheckConnection()
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                return true;
            }
            else
            {
                return false;



            }
        }

        public async Task<JObject> CheckMobileNo(string mobileNo)
        {
            if (CheckConnection())
            {
                using (var client = new HttpClient())
                {
                    string url = Baseurl + "API/api/ForgetPasswordApi/CheckMobileNo?MobileNo="+mobileNo;
                    client.BaseAddress = new Uri(Baseurl);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                
                    
                    HttpResponseMessage Res = await client.PostAsync(url,null);
                    JObject results = new JObject();
                    if (Res.IsSuccessStatusCode)
                    {
                        var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                        results = JObject.Parse(EmpResponse);
                    }
                    return results;
                }
            }
            else
            {
                JObject jb = new JObject();

                jb.Add("Type", "0");
                jb.Add("ResponseMessage", " Network not available. Please check your  internet connection and try again..... ");

                return jb;
            }

        }
        public async Task<JObject> getAllArdersByFranchisee(InvoiceFilterModel filterModel)
        {
            if (CheckConnection())
            {
                using (var client = new HttpClient())
                {
                    string url = Baseurl + "API/api/CustomerReturnApi/GetCustomerReturnByFranchiseId?SearchBy="
                        +filterModel.SearchBy+"&FromDate="+filterModel.FromDate+
                        "&ToDate="+filterModel.ToDate+"&ArticleNo="+filterModel.ArticleNo+"&CurrentPage="+filterModel.CurrentPage+
                        "&PageSize=10&SortDirection=DESC&SortExpression="+filterModel.SortExpression+
                        "&Status=&FranchiseId="+filterModel.FranchiseId+"&Amount="+filterModel.Amount+ "&Status="+filterModel.Status;
                    client.BaseAddress = new Uri(Baseurl);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));


                    HttpResponseMessage Res = await client.GetAsync(url);
                    JObject results = new JObject();
                    if (Res.IsSuccessStatusCode)
                    {
                        var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                        results = JObject.Parse(EmpResponse);
                    }
                    return results;
                }
            }
            else
            {
                JObject jb = new JObject();

                jb.Add("Type", "0");
                jb.Add("ResponseMessage", " Network not available. Please check your  internet connection and try again..... ");

                return jb;
            }

        }

        public async Task<JObject> getInvoiceDetailsByID(string invoiceID)
        {
            if (CheckConnection())
            {
                using (var client = new HttpClient())
                {
                    ///CustomerReturnApi/GetCustomerReturnByFranchiseId?SearchBy=&FromDate=&ToDate=&ArticleNo=&CurrentPage=1&PageSize=10&SortDirection=DESC&SortExpression=FranchiseSellId&Status=&FranchiseId=46&Amount=
                    string url = Baseurl + "API/api/CustomerReturnApi/GetCustomerReturnDetailByFranchiseSellId?FranchiseSellId="+invoiceID;
                    client.BaseAddress = new Uri(Baseurl);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));


                    HttpResponseMessage Res = await client.GetAsync(url);
                    JObject results = new JObject();
                    if (Res.IsSuccessStatusCode)
                    {
                        var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                        results = JObject.Parse(EmpResponse);
                    }
                    return results;
                }
            }
            else
            {
                JObject jb = new JObject();

                jb.Add("Type", "0");
                jb.Add("ResponseMessage", " Network not available. Please check your  internet connection and try again..... ");

                return jb;
            }

        }

        public async Task<JObject> saveReturnedOrderDetails(SubmitReturnModel model)
        {
            if (CheckConnection())
            {
                using (var client = new HttpClient())
                {
                    string url = Baseurl + "API/Api/CustomerReturnApi/CustomerOrderReturn_SaveReturn";
                    
                    client.BaseAddress = new Uri(Baseurl);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.ExpectContinue = false;
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Add("Key", Application.Current.Properties["Key"].ToString());
                    client.DefaultRequestHeaders.Add("UserName", Application.Current.Properties["UserName"].ToString());
                    client.DefaultRequestHeaders.Add("Password", Application.Current.Properties["Password"].ToString());
                    var jsonRequest = JsonConvert.SerializeObject(model);
                    var content = new StringContent(jsonRequest, null, "application/json");
                    HttpResponseMessage Res = null;
                    Res = await client.PostAsync(url, content);
                    JObject results = new JObject();
                    if (Res.IsSuccessStatusCode)
                    {
                        var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                        results = JObject.Parse(EmpResponse);
                    }
                    return results;
                }
            }
            else
            {
                JObject jb = new JObject();

                jb.Add("Type", "0");
                jb.Add("ResponseMessage", " Network not available. Please check your  internet connection and try again..... ");

                return jb;
            }
        }

        public async Task<JObject> CheckCouponCode(CouponModel model)
        {
            if (CheckConnection())
            {
                using (var client = new HttpClient())
                {
                    string url = Baseurl + "API/api/CustomerDiscountApi/CheckCouponCode?CustomerId="+model.CustomerId+
                        "&CurrentFranchiseId="+model.CurrentFranchiseId+"&CurrentOrderAmount="+model.CurrentOrderAmount+
                        "&CouponCode="+model.CouponCode+"&CreatedDate="+model.CreatedDate;
                    client.BaseAddress = new Uri(Baseurl);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));


                    HttpResponseMessage Res = await client.GetAsync(url);
                    JObject results = new JObject();
                    if (Res.IsSuccessStatusCode)
                    {
                        var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                        results = JObject.Parse(EmpResponse);
                    }
                    return results;
                }
            }
            else
            {
                JObject jb = new JObject();

                jb.Add("Type", "0");
                jb.Add("ResponseMessage", " Network not available. Please check your  internet connection and try again..... ");

                return jb;
            }

        }

        public async Task<JObject> GetReturnListByFranchiseId(string frachiseeID)
        {
            if (CheckConnection())
            {
                using (var client = new HttpClient())
                {
                    string url = Baseurl + "/API/api//CustomerReturnApi/GetReturnListByFranchiseId?FranchiseId=" + frachiseeID;
                    client.BaseAddress = new Uri(Baseurl);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));


                    HttpResponseMessage Res = await client.GetAsync(url);
                    JObject results = new JObject();
                    if (Res.IsSuccessStatusCode)
                    {
                        var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                        results = JObject.Parse(EmpResponse);
                    }
                    return results;
                }
            }
            else
            {
                JObject jb = new JObject();

                jb.Add("Type", "0");
                jb.Add("ResponseMessage", " Network not available. Please check your  internet connection and try again..... ");

                return jb;
            }
        }

        public async Task<JObject> GetReturnDetailReturnId(string returnID)
        {
            if (CheckConnection())
            {
                using (var client = new HttpClient())
                {
                    string url = Baseurl + "API/api/CustomerReturnApi/GetReturnDetailByReturnId?CustomerOrderReturnId=" + returnID;
                    client.BaseAddress = new Uri(Baseurl);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));


                    HttpResponseMessage Res = await client.GetAsync(url);
                    JObject results = new JObject();
                    if (Res.IsSuccessStatusCode)
                    {
                        var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                        results = JObject.Parse(EmpResponse);
                    }
                    return results;
                }
            }
            else
            {
                JObject jb = new JObject();

                jb.Add("Type", "0");
                jb.Add("ResponseMessage", " Network not available. Please check your  internet connection and try again..... ");

                return jb;
            }
        }
    }
}
