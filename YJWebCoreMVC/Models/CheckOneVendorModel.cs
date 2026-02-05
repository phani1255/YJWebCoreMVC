using System.Data;

namespace YJWebCoreMVC.Models
{
    public class CheckOneVendorModel
    {
        public string VendorCode { get; set; }

        public string Checkno { get; set; }

        public string banks { get; set; }

        public decimal EnteredCheckAmt { get; set; } = 0;
        public decimal txtBalChkAmt { get; set; } = 0;

        public DateTime? CheckDate { get; set; } = null;
        public DataTable dtAddCheck { get; set; }
        public DataTable dtAPCredit { get; set; }
        public string packno { get; set; }
        public string _Errors { get; set; }
        public int Counts { get; set; }

        public bool result { get; set; } = false;

        public List<InvoiceCheckOneVendorModel> InvoiceList { get; set; } = new List<InvoiceCheckOneVendorModel>();

    }

    public class InvoiceCheckOneVendorModel
    {
        public string BillNo { get; set; }
        public string RefNo { get; set; }
        public DateTime Date { get; set; }
        public DateTime DueDate { get; set; }
        public decimal Balance { get; set; }
        public decimal DiscountPercent { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal SecondDiscountPercent { get; set; }
        public decimal SecondDiscountAmount { get; set; }

        public string Typs { get; set; }
        // FIELDS YOU WANT TO PREFILL
        public decimal PayAmount { get; set; }
        public bool IsFullPayment { get; set; }
    }
}
