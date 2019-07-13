using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace theshirtshopApp.BAL
{
    public class ReturnRefundModel
    {
        public string OrderReturnId { get; set; }
        public string customername { get; set; }
        public string TotalQty { get; set; }
        public string Amount { get; set; }
        public string ReturnStatus { get; set; }
        public string CreatedDate { get; set; }
        public string IsCreditNoteUsed { get; set; }
        public string invoiceno { get; set; }
        public string ContactNo { get; set; }
        public string CustomerNameContact { get { return customername + "(" + ContactNo + ")"; } }
        public string AmountInRupees {
            get {
                decimal parsed = decimal.Parse(Amount != null ? Amount : "0.00", CultureInfo.InvariantCulture);
                CultureInfo hindi = new CultureInfo("hi-IN");
                return string.Format(hindi, "{0:c}", parsed);
               
            }
        }




        public ObservableCollection<ReturnArticleDetails> AsticaleList { get; set; }

        public ReturnRefundModel getReturnRefundModel(ObservableCollection<ReturnRefundServerModel> model) {

            ReturnRefundModel refundModel = new ReturnRefundModel()
            {
                OrderReturnId =model[0].OrderReturnId, customername=model[0].customername,
                TotalQty = model[0].TotalQty, Amount=model[0].Amount, ReturnStatus = model[0].ReturnStatus,
                CreatedDate = model[0].CreatedDate, IsCreditNoteUsed = model[0].IsCreditNoteUsed,
                invoiceno = model[0].invoiceno, ContactNo = model[0].ContactNo
            };
            refundModel.AsticaleList = new ObservableCollection<ReturnArticleDetails>();
            foreach (ReturnRefundServerModel item in model) {
                refundModel.AsticaleList.Add(new ReturnArticleDetails()
                {
                    ArticleNo = item.ArticleNo, CategoryName = item.CategoryName,
                    Color = item.Color, DamagedQty = item.DamagedQty, MarketableQty = item.MarketableQty, QtyAmount = item.QtyAmount,
                    SubCategoryName = item.SubCategoryName
                });

            }


            return refundModel;
        }


    }

    public class ReturnRefundServerModel
    {
        public string OrderReturnId { get; set; }
        public string customername { get; set; }
        public string TotalQty { get; set; }
        public string Amount { get; set; }
        public string ReturnStatus { get; set; }
        public string CreatedDate { get; set; }
        public string IsCreditNoteUsed { get; set; }
        public string invoiceno { get; set; }
        public string DamagedQty { get; set; }
        public string QtyAmount { get; set; }
        public string MarketableQty { get; set; }
        public string ContactNo { get; set; }
        public string ArticleNo { get; set; }
        public string Color { get; set; }
        public string CategoryName { get; set; }
        public string SubCategoryName { get; set; }


    }

    public class ReturnArticleDetails {

        public string DamagedQty { get; set; }
        public string QtyAmount { get; set; }
        public string MarketableQty { get; set; }
       
        public string ArticleNo { get; set; }
        public string Color { get; set; }
        public string CategoryName { get; set; }
        public string SubCategoryName { get; set; }
        public string QtyAmountInRupees
        {
            get
            {
                decimal parsed = decimal.Parse(QtyAmount != null ? QtyAmount : "0.00", CultureInfo.InvariantCulture);
                CultureInfo hindi = new CultureInfo("hi-IN");
                return string.Format(hindi, "{0:c}", parsed);

            }
        }
    }
}
