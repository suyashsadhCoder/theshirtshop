using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace theshirtshopApp.BAL
{
    public class Ladger_Class
    {
        public string franchiseName { get; set; }
        public int? franchiseId { get; set; }
        public int transactionId { get; set; }
        public int orderId { get; set; }
        public string invoiceNo { get; set; }
        public string Type { get; set; }
        public decimal Credit { get; set; }
        public decimal Debit { get; set; }
        public decimal OutstandingAmount { get; set; }
        public DateTime createdDate { get; set; }
        public string transactionNo { get; set; }
        public string transactionDate { get; set; }
        public string transactionType { get; set; }
    }
    public class Use_Ladger_Class
    {
        public decimal Credit { get; set; }
        public decimal Debit { get; set; }
        public decimal OutstandingAmount { get; set; }
        public DateTime Date { get; set; }
        public string Type { get; set; }
    }
   
}
