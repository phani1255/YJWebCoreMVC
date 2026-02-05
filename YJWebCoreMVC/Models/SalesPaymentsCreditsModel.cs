using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data;

namespace YJWebCoreMVC.Models
{
    public class SalesPaymentsCreditsModel
    {
        public string CustomerCode { get; set; }
        public int CustomerCodeId { get; set; }
        public IEnumerable<SelectListItem> CustomerCodes { get; set; }
        public string PaymentType { get; set; }
        public IEnumerable<SelectListItem> PaymentTypes { get; set; }
        public string bankcode { get; set; }
        public IEnumerable<SelectListItem> AllBankCodes { get; set; }
        public string reason { get; set; }
        public IEnumerable<SelectListItem> AllReasons { get; set; }
        public DataRow ReceiptInfo { get; set; }
        public string Note { get; set; }
        public DataView DvPayItems { get; set; }
        public DataTable DtCreditDetails { get; set; }
        public string Rcots { get; set; }
        public string Credit_No { get; set; }
        public string DefaultBank { get; set; }
        public IEnumerable<SelectListItem> DefaultBanks { get; set; }
        public string RTV_PAY { get; set; }
        public string CODE { get; set; }
    }
}
