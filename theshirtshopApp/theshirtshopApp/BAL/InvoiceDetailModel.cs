using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;

using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

using Xamarin.Forms;

namespace theshirtshopApp.BAL
{

    public class SubmitReturnModel
    {
        public string FranchiseId { get; set; }
        public string OrderId { get; set; }
        public string CustomerId { get; set; }
        public int IsRefund { get; set; }
        public string CreatedBy { get; set; }
        public List<SubmitArticleModel> lstProducts { get; set; }



    }

    public class SubmitArticleModel
    {
        public string ArticleId { get; set; }
        public string SubCategoryId { get; set; }
        public string DamagedQty { get; set; }
        public string MarketableQty { get; set; }

    }

    public class InvoiceDetailModel 
    {
        public string InvoiceNo { get; set; }
        public string FranchiseSellId { get; set; }
        public string CreatedDate { get; set; }
        public string TotalAmount { get; set; }
        public string TotalQty { get; set; }
        public string CustomerName { get; set; }
        public string CustomerId { get; set; }
        public string TotalAmountInRupees { get; set; }
        public string FrachiseID { get { return Application.Current.Properties["OtherId"].ToString(); } }
        public string SellStatus { get; set; }
        public ObservableCollection<ArticleDetails> articleList { get; set; }
        public InvoiceDetailModel sortOrderDetails(ObservableCollection<InvoiceDetailsServerModel> tempLIst)
        {
            InvoiceDetailModel model = new InvoiceDetailModel()
            {
                InvoiceNo = tempLIst[0].InvoiceNo,
                FranchiseSellId = tempLIst[0].FranchiseSellId,
                CreatedDate = tempLIst[0].CreatedDate,
                TotalAmount = tempLIst[0].TotalAmount,
                TotalAmountInRupees = ConvertInRupees(tempLIst[0].TotalAmount),
                TotalQty = tempLIst[0].TotalQty,
                CustomerName = tempLIst[0].CustomerName + " (" + tempLIst[0].ContactNo + ")",
                SellStatus = tempLIst[0].SellStatus,
                CustomerId = tempLIst[0].CustomerId,
                
                articleList = new ObservableCollection<ArticleDetails>()
            };
            int index = 0;
            foreach (InvoiceDetailsServerModel item in tempLIst)
            {
                model.articleList.Add(new ArticleDetails()
                {
                    ArticleId = item.ArticleId,
                    CategoryName = item.CategoryName,
                    damagedQty = 0,
                    isSelected = false,
                    MRP = ConvertInRupees(item.MRP),
                    AdjustedQty = item.AdjustedQty != null ? Convert.ToInt32(item.AdjustedQty) : 0,
                    AvailableQty = Convert.ToInt32(item.TotalQty) - (item.AdjustedQty != null ? Convert.ToInt32(item.AdjustedQty) : 0),
                    returnedQty = 0,
                    SubCategoryId = item.SubCategoryId,
                    SubCategoryName = item.SubCategoryName,
                    TotalQty = Convert.ToInt32(model.TotalQty), artId = index

                });
                index++;
                
            }
            
         return model;
        }
        public SubmitReturnModel ConvertToSubmitReturnModel(InvoiceDetailModel model, bool isRefund) {

            SubmitReturnModel item = new SubmitReturnModel() { CustomerId=model.CustomerId, FranchiseId = model.FrachiseID, OrderId =model.FranchiseSellId, IsRefund= isRefund? 1:0, lstProducts = new List<SubmitArticleModel>(),CreatedBy = model.FrachiseID};

            foreach (ArticleDetails article in model.articleList) {
                item.lstProducts.Add(new SubmitArticleModel() {
                    ArticleId =article.ArticleId,
                    DamagedQty = article.damagedQty.ToString(),
                    SubCategoryId = article.SubCategoryId,
                    MarketableQty =article.damagedQty>0?(article.TotalQty-article.damagedQty).ToString():article.returnedQty.ToString()
                });

            }

            return item;
        }
        public string ConvertInRupees(string str)
        {
           
                decimal parsed = decimal.Parse(str, CultureInfo.InvariantCulture);
                CultureInfo hindi = new CultureInfo("hi-IN");
                return string.Format(hindi, "{0:c}", parsed);
           
        }
        public bool isQuantityUpdated(InvoiceDetailModel model) {

            bool isUpdated =false;
            for (int i = 0; i < this.articleList.Count; i++)
            {
                if (this.articleList[i].returnedQty != model.articleList[i].returnedQty) { return true; }
                if (this.articleList[i].damagedQty != model.articleList[i].damagedQty) { return true; }

            }

            return isUpdated;
        }
        

    }

    public class InvoiceDetailsServerModel
    {
      public string FranchiseSellId { get; set; }
       public string CustomerName { get; set; }
        public string CustomerId { get; set; }
        public string ContactNo { get; set; }
        public string InvoiceNo { get; set; }
        public string TotalAmount { get; set; }
        public string CreatedDate { get; set; }
        public string SellStatus { get; set; }
        public string IsEditable { get; set; }
        public string ArticleId { get; set; }
        public string ArticleNo { get; set; }
        public string Color { get; set; }
        public string TotalQty { get; set; }
        public string AdjustedQty { get; set; }
        public string MRP { get; set; }
        public string CategoryName { get; set; }
        public string SubCategoryId { get; set; }
        public string SubCategoryName { get; set; }


    }

    public class ArticleDetails: INotifyPropertyChanged
    {
        public int artId { get; set; }
        public string ArticleId { get; set; }
        public string CategoryName { get; set; }
        public string SubCategoryName { get; set; }
        public string SubCategoryId { get; set; }
        public int TotalQty { get; set; }
        public int AdjustedQty { get; set; }
        public int AvailableQty { get; set; }
        public string MRP { get; set; }
        public string backgroundColor { get { return isSelected ? "Orange" : "White"; } }
        public int returnedQty { get; set; }
        public int damagedQty { get; set; }
        public bool isSelected {
            get {
                return _isSelected;
            }
            set {
                _isSelected = value;
                if (!_isSelected) {
                    damagedQty = 0;
                    OnPropertyChanged("damagedQty");
                    returnedQty = 0;
                    OnPropertyChanged("returnedQty");
                }
                OnPropertyChanged();
            }
        }

        private bool _isSelected;

        public void increaseDamage()
        {

            damagedQty = damagedQty < AvailableQty-returnedQty ? damagedQty + 1 : damagedQty;
            OnPropertyChanged("damagedQty");
        }
        public void decreaseDamage()
        {
            damagedQty = damagedQty > 0 ? damagedQty - 1 : 0;
            OnPropertyChanged("damagedQty");
        }
        public void increaseReturn()
        {
            returnedQty = returnedQty < AvailableQty-damagedQty ? returnedQty + 1 : returnedQty;
            OnPropertyChanged("returnedQty");
        }
        public void decreaseReturn()
        {
            returnedQty = returnedQty > 0 ? returnedQty - 1 : 0;
            OnPropertyChanged("returnedQty");
        }

       


        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
