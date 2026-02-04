namespace YJWebCoreMVC.Models
{
    public class VendorModel
    {

        public string ACC { get; set; } = string.Empty;

        public string NAME { get; set; } = string.Empty;

        public decimal? TEL { get; set; } = 0;

        public decimal? TEL2 { get; set; } = 0;

        public decimal? FAX { get; set; } = 0;

        public string ADDR11 { get; set; } = string.Empty;

        public string ADDR12 { get; set; } = string.Empty;

        public string CITY1 { get; set; } = string.Empty;

        public string STATE1 { get; set; } = string.Empty;

        public string ZIP1 { get; set; } = string.Empty;

        public string COUNTRY { get; set; } = string.Empty;

        public string ADDR21 { get; set; } = string.Empty;

        public string ADDR22 { get; set; } = string.Empty;

        public string CITY2 { get; set; } = string.Empty;

        public string STATE2 { get; set; } = string.Empty;

        public string ZIP2 { get; set; } = string.Empty;

        public string CONTACT { get; set; } = string.Empty;

        public string DNB { get; set; } = string.Empty;

        public string SOUNDX { get; set; } = string.Empty;

        public string GL_CODE { get; set; } = string.Empty;

        public string NOTE { get; set; } = string.Empty;

        public decimal? TERM { get; set; } = 0;

        public string NOTE1 { get; set; } = string.Empty;

        public string EMAIL { get; set; } = string.Empty;

        public string WEBSITE { get; set; } = string.Empty;

        public string OS_TEL2 { get; set; } = string.Empty;
        public string OS_TEL1 { get; set; } = string.Empty;

        public bool? CASTER { get; set; } = false;

        public decimal? FAX_EMAIL { get; set; } = 0;

        public decimal? CAST_COPY { get; set; } = 0;

        public string CAST_EMAIL { get; set; } = string.Empty;

        public string CAST_FAX { get; set; } = string.Empty;

        public bool? FIN_VND { get; set; } = false;

        public string OS_FAX { get; set; } = string.Empty;

        public string OUR_ACCT { get; set; } = string.Empty;

        public bool? ON_QB { get; set; } = false;

        public bool? IS_CRD { get; set; } = false;

        public bool? HAS_RECURRING { get; set; } = false;

        public int recurring_day { get; set; } = 0;

        //public string recur_last_bill { get; set; } = string.Empty;

        //public string recur_final_bill{ get; set; } = string.Empty;
        public DateTime? recur_last_bill { get; set; } = null;
        public DateTime? recur_final_bill { get; set; } = null;

        public string recur_gl_code1 { get; set; } = string.Empty;

        public string recur_gl_code2 { get; set; } = string.Empty;

        public string recur_gl_code3 { get; set; } = string.Empty;

        public string recur_gl_code4 { get; set; } = string.Empty;
        public string gl_ap { get; set; } = string.Empty;
        public decimal? recur_amount1 { get; set; } = 0;
        public decimal? recur_amount2 { get; set; } = 0;
        public decimal? recur_amount3 { get; set; } = 0;
        public decimal? recur_amount4 { get; set; } = 0;
        public int Is1099 { get; set; } = 0;
        public string TAX_ID { get; set; } = string.Empty;
        public string MultiAttr1Val { get; set; } = string.Empty;
        public string Cellno { get; set; } = string.Empty;
        public string Whatsappno { get; set; } = string.Empty;
        public string GroupAccno { get; set; } = string.Empty;
        public string Groupname { get; set; } = string.Empty;
        public string GroupAccno1 { get; set; } = string.Empty;
        public string Groupname1 { get; set; } = string.Empty;
        public string Userid { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string OldVendorcode { get; set; } = string.Empty;
        public string NAME3 { get; set; } = string.Empty;
        public string ADDR31 { get; set; } = string.Empty;
        public string ADDR32 { get; set; } = string.Empty;
        public string ADDR33 { get; set; } = string.Empty;
        public bool? Depositonly { get; set; } = false;
        public string SalesrepTel { get; set; } = string.Empty;

        public bool? PAY_BY_GOLD { get; set; } = false;
        public bool? PRIVATE_SLLER { get; set; } = false;
        public string Vndno { get; set; } = string.Empty;
        public decimal? VndMarkup { get; set; } = 0;
        public string Setter { get; set; } = string.Empty;
        public int NoOfDay1 { get; set; } = 0;
        public decimal DiscPercent1 { get; set; } = 0;
        public string Extension { get; set; } = string.Empty;
    }
}
