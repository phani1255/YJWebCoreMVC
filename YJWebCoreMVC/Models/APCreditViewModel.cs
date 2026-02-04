namespace YJWebCoreMVC.Models
{
    public class APCreditViewModel
    {

        public string CreditNo { get; set; }
        public string VendorCode { get; set; }
        public string VendorName { get; set; }
        public string VendorRefNo { get; set; }

        public decimal TotalAmount { get; set; }
        public decimal Balance { get; set; }
        public string CreditDate { get; set; }
        public string Notes { get; set; }
        public string Notes1 { get; set; }
        public string Notes2 { get; set; }
        public string ReturnStatus { get; set; }
        public string Store { get; set; }
        public decimal? PACK { get; set; } = 0;

        public decimal? REPL_IT { get; set; } = 0;

        public bool? ON_QB { get; set; } = false;

        public string loggeduser { get; set; } = string.Empty;


    }
}
