
using BalLayer_TheShirtShop;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using theshirtshopApp.BAL;

namespace theshirtshopApp.Interface
{
  public  interface IAllDataServices 
    {
        // Post Method
          Task<JObject> CheckLoginAsync(AppLoginClass _AppLoginClass);
        Task<JObject> SendOrderRequest(FranchiseRequest_Class _FranchiseRequest_Class);
        Task<JObject> ChangePassword(AppLoginClass appLoginClass);
        Task<JObject> RetailerRegistration(RetailerMaster_Class retailerMaster_Class);
        Task<JObject> GetRetailerByMobileNo(RetailerMaster_Class retailerMaster_Class);
        Task<JObject> GetOTP(RetailerMaster_Class retailerMaster_Class);
        Task<JObject> SaveCashCollection(CashCollection_Class cashCollection_Class);
        Task<JObject> FranchiseRegistration(FranchiseMasters FranchiseMasters_Class);
        Task<JObject> CustomerRegistration(Customer_Class Customer_Class_Data);
        Task<JObject> ReferralRegistration(FranchiseReferral_Class FranchiseReferral_Class_Data);
        Task<JObject> SaveFeedBack(FranchiseFeedback_Class FranchiseFeedback_Class_Data);
        Task<JObject> GetCustomerByMobileNo(Customer_Class Customer_Class_Data);
        Task<JObject> SaveFeedBack(OrderMaster_Class OrderMaster_Class_Data);
        Task<JObject> SaveSellCustomer(FranchiseSell_Class FranchiseSell_Class_Data);
        Task<JObject> SendOTPCustomer(Customer_Class Customer_Class_Data);
        Task<JObject> SaveRetailerOrder(Employee_OrderGenerate_Class Employee_OrderGenerate_Class_Data);
        Task<JObject> SaveGoodReturn(FranchiseGoodReturn_Class FranchiseGoodReturn_Class_Date);
        Task<JObject> FistAdminPayAmount(int Id);
        Task<JObject> SecondAdminPayAmount(int Month, int Year, string TermType, int Id);
        Task<JObject> NotificationUpdate(string Type);
        //Get Method
        Task<JObject> GetAllSendStockRequest();
        Task<JObject> GetEmployeeStock();
        Task<JObject> GetAdminInfo();
        Task<JObject> GetStateAndCity();
        Task<JObject> GetProfile(string v);
        Task<JObject> GetProfileForDriver(string v);
        Task<JObject> GetFranchiseStock();
        Task<JObject> GetAllUpCommingOrder();
        Task<JObject> GetAllInvoiceOrder();
        Task<JObject> GetAllInvoiceCustomer();
        Task<JObject> GetHomeRecord(string v);
        Task<JObject> GetAllGoodReturn();
        Task<JObject> GetAllLadger();
        Task<JObject> GetAllNotiication();
        Task<JObject> CheckMobileNo(string mobileNo);
        Task<JObject> getAllArdersByFranchisee(InvoiceFilterModel filterModel);
        Task<JObject> getInvoiceDetailsByID(string invoiceID);
        Task<JObject> saveReturnedOrderDetails(SubmitReturnModel model);
        Task<JObject> CheckCouponCode(CouponModel model);
        Task<JObject> GetReturnListByFranchiseId(string frachiseeID);
        Task<JObject> GetReturnDetailReturnId(string returnID);
    }
}
