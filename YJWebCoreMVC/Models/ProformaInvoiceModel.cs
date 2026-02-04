namespace YJWebCoreMVC.Models
{
    public class ProformaInvoiceModel
    {

        public string INV_NO { get; set; }

        public string BACC { get; set; }

        public string ACC { get; set; }

        public decimal? ADD_COST { get; set; }

        public decimal? SNH { get; set; }

        public DateTime? DATE { get; set; }

        public string PON { get; set; }

        public string RSFM { get; set; }

        public decimal? RSFM_AMT { get; set; }

        public string MESSAGE { get; set; }

        public string MESSAGE1 { get; set; }

        public string MESSAGE2 { get; set; }

        public decimal? GR_TOTAL { get; set; }

        public string ADDR1 { get; set; }

        public string ADDR2 { get; set; }

        public string ADDR3 { get; set; }

        public string CITY { get; set; }

        public string STATE { get; set; }

        public string ZIP { get; set; }

        public string COUNTRY { get; set; }

        public decimal? CREDITS { get; set; }

        public string BH_VEND_NO { get; set; }

        public decimal? DEDUCTION { get; set; } = 0;

        public string VIA_UPS { get; set; }

        public string IS_COD { get; set; }

        public decimal? WEIGHT { get; set; }

        public string COD_TYPE { get; set; }

        public string CUST_PON { get; set; }

        public string OPERATOR { get; set; }

        public string SHIP_BY { get; set; }

        public string FIN_REF { get; set; }

        public string SAMPLE { get; set; }

        public string TERM { get; set; }

        public string NAME { get; set; }

        public string PICKUPNO { get; set; }

        public string UPSTRAK { get; set; }

        public string ATTN { get; set; }

        public string COMBINE { get; set; }

        public decimal? INSURED { get; set; }

        public string EARLY { get; set; }

        public decimal? PERCENT { get; set; }

        public bool? MAN_SHIP { get; set; }

        public string DEPT { get; set; }

        public string ASN { get; set; }

        public string RESIDENT { get; set; }

        public bool? IS_EDI { get; set; }

        public bool? BILLED { get; set; }

        public string DP { get; set; }

        public bool? IS_FDX { get; set; }

        public bool? IS_DEB { get; set; }

        public decimal? DED_PER { get; set; }

        public bool? INV_PAID { get; set; }

        public string METHOD { get; set; }

        public string CHECK_NO { get; set; }

        public string PAYINVNO { get; set; }

        public bool? ON_QB { get; set; }

        public string QB_REF { get; set; }

        public int SHIPTYPE { get; set; }

        public Decimal NO_BOX { get; set; }

        public bool? CCRD { get; set; }

        public string TYPE { get; set; }

        public bool? BY_WT { get; set; }

        public bool LAPTOP { get; set; } = false;

        public string STORE_NO { get; set; } = "";
    }
}
