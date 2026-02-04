namespace YJWebCoreMVC.Models
{
    public class ScrapGoldModel
    {
        public string INV_NO { get; set; } = string.Empty;

        public string BACC { get; set; } = string.Empty;

        public string ACC { get; set; } = string.Empty;

        public decimal? ADD_COST { get; set; } = 0;

        public DateTime? DATE { get; set; } = DateTime.Now;

        public string MESSAGE { get; set; } = string.Empty;

        public string MESSAGE1 { get; set; } = string.Empty;

        public string MESSAGE2 { get; set; } = string.Empty;

        public decimal? GR_TOTAL { get; set; } = 0;

        public string ADDR1 { get; set; } = string.Empty;

        public string ADDR2 { get; set; } = string.Empty;

        public string ADDR3 { get; set; } = string.Empty;

        public string CITY { get; set; } = string.Empty;

        public string STATE { get; set; } = string.Empty;

        public string ZIP { get; set; } = string.Empty;

        public string COUNTRY { get; set; } = string.Empty;

        public string OPERATOR { get; set; } = string.Empty;

        public string NAME { get; set; } = string.Empty;

        public decimal? CASH { get; set; } = 0;
        public decimal? STORECREDIT { get; set; } = 0;
        public decimal? CHECK { get; set; } = 0;
        public string CHECK_NO { get; set; } = string.Empty;

        public decimal? GOLDPRICE { get; set; } = 0;
        public decimal? SILVERPRICE { get; set; } = 0;
        public decimal? PLATPRICE { get; set; } = 0;
        public decimal? PALLADPRICE { get; set; } = 0;
        public string SALESMAN1 { get; set; } = string.Empty;

        public string STORE_NO { get; set; } = string.Empty;

        public int? DWT_GR { get; set; } = 0;
        public string BANK { get; set; } = string.Empty;
        public string CASH_REGISTER { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;
        public int IsUsetradein { get; set; } = 0;
        public decimal? Agreeduponprice { get; set; } = 0;
        public string invoiceno { get; set; }
        public bool iSAddedFromInvoice { get; set; }
    }
}
