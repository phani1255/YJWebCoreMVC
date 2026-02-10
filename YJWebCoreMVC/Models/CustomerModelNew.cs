using Microsoft.AspNetCore.Mvc.Rendering;

namespace YJWebCoreMVC.Models
{
    public class CustomerModelNew
    {
        public int ID { get; set; }
        public string ACC { get; set; } = String.Empty;
        public string NAME { get; set; } = String.Empty;
        public string ADDR1 { get; set; } = String.Empty;
        public string ADDR12 { get; set; } = String.Empty;
        public string ADDR13 { get; set; } = String.Empty;
        public string CITY1 { get; set; } = String.Empty;
        public string STATE1 { get; set; } = String.Empty;
        public string ZIP1 { get; set; } = String.Empty;
        public string NAME2 { get; set; } = String.Empty;
        public string ADDR2 { get; set; } = String.Empty;
        public string ADDR22 { get; set; } = String.Empty;
        public string ADDR23 { get; set; } = String.Empty;
        public string CITY2 { get; set; } = String.Empty;
        public string STATE2 { get; set; } = String.Empty;
        public string ZIP2 { get; set; } = String.Empty;
        public string SHIP_VIA { get; set; } = String.Empty;
        public string BUYER { get; set; } = String.Empty;
        public decimal? TEL { get; set; }
        public decimal? TEL2 { get; set; }
        public decimal? FAX { get; set; }
        public decimal? PERCENT { get; set; }
        public string BILL_ACC { get; set; }
        public decimal? TERM_PCT1 { get; set; }
        public decimal? TERM1 { get; set; }
        public decimal? TERM_PCT2 { get; set; }
        public decimal? TERM2 { get; set; }
        public decimal? TERM_PCT3 { get; set; }
        public decimal? TERM3 { get; set; }
        public decimal? TERM_PCT4 { get; set; }
        public decimal? TERM4 { get; set; }
        public string SALESMAN1 { get; set; } = String.Empty;
        public decimal? SHARE1 { get; set; }
        public decimal? PERCENT1 { get; set; }
        public string SALESMAN2 { get; set; } = String.Empty;
        public decimal? PERCENT2 { get; set; }
        public string SALESMAN3 { get; set; } = String.Empty;
        public decimal? PERCENT3 { get; set; }
        public string SALESMAN4 { get; set; } = String.Empty;
        public decimal? PERCENT4 { get; set; }
        public decimal? CREDIT { get; set; }
        public string PRICE_FILE { get; set; } = String.Empty;
        public string JBT { get; set; } = String.Empty;
        public DateTime? EST_DATE { get; set; }
        public string old_customer { get; set; } = String.Empty;
        public string IS_COD { get; set; } = String.Empty;
        public string COD_TYPE { get; set; } = String.Empty;
        public bool? ON_HOLD { get; set; }
        public bool? INACTIVE { get; set; }
        public string ON_MAIL { get; set; } = String.Empty;
        public decimal? INTEREST { get; set; }
        public string COUNTRY { get; set; } = String.Empty;
        public string WWW { get; set; } = String.Empty;
        public string EMAIL { get; set; } = String.Empty;
        public decimal? GRACE { get; set; }
        public DateTime? LAST_INT { get; set; }
        public string COUNTRY2 { get; set; } = String.Empty;
        public string RESIDENT { get; set; } = String.Empty;
        public decimal? TERM5 { get; set; }
        public decimal? TERM_PCT5 { get; set; }
        public decimal? TERM6 { get; set; }
        public decimal? TERM_PCT6 { get; set; }
        public decimal? TERM7 { get; set; }
        public decimal? TERM_PCT7 { get; set; }
        public decimal? TERM8 { get; set; }
        public decimal? TERM_PCT8 { get; set; }
        public string REASON { get; set; }
        public bool? COLCSENT { get; set; }
        public DateTime? COLCDATE { get; set; }
        public string COLCRSN { get; set; } = String.Empty;
        public string NOTE { get; set; } = String.Empty;
        public string TAX_ID { get; set; } = String.Empty;
        public string FDX_NO { get; set; } = String.Empty;
        public string ATTR1VAL { get; set; } = String.Empty;
        public string ATTR2VAL { get; set; } = String.Empty;
        public string ATTR3VAL { get; set; } = String.Empty;
        public string ATTR4VAL { get; set; } = String.Empty;
        public string ATTR5VAL { get; set; } = String.Empty;
        public string ATTR6VAL { get; set; } = String.Empty;
        public string ATTR7VAL { get; set; } = String.Empty;
        public string ATTR8VAL { get; set; } = String.Empty;
        public string MULTIATTR1VAL { get; set; } = String.Empty;
        public string MULTIATTR2VAL { get; set; } = String.Empty;
        public string MULTIATTR3VAL { get; set; } = String.Empty;
        public bool CUSTCHECKVAL1 { get; set; }
        public bool CUSTCHECKVAL2 { get; set; }
        public bool CUSTCHECKVAL3 { get; set; }
        public bool CUSTCHECKVAL4 { get; set; }
        public bool CUSTCHECKVAL5 { get; set; }
        public bool CUSTCHECKVAL6 { get; set; }
        public bool CUSTCHECKVAL7 { get; set; }
        public bool CUSTCHECKVAL8 { get; set; }
        public string CUSTATTRLABELS { get; set; } = String.Empty;
        public string DIVISION { get; set; } = String.Empty;
        public string OSTEL { get; set; } = String.Empty;
        public string POPUPNOTE { get; set; } = String.Empty;
        public string WEB_USER { get; set; } = String.Empty;
        public string WEB_PASSWORD { get; set; } = String.Empty;
        public string SHIP_TYPE { get; set; } = String.Empty;
        public bool ON_ACCOUNT { get; set; }
        public bool ISPRIVATECUST { get; set; }
        public string CONTACTMODE { get; set; } = String.Empty;
        public string SSN { get; set; } = String.Empty;
        public string Ring_Size_1 { get; set; } = string.Empty;
        public string Ring_Size_2 { get; set; } = string.Empty;
        public bool different_ship { get; set; }
        public string RefBy_acc { get; set; } = string.Empty;
        public string oldCustomerCode { get; set; } = string.Empty;
        public string driverlicense_state { get; set; } = string.Empty;
        public string driverlicense_number { get; set; } = string.Empty;
        public string CELL { get; set; } = string.Empty;
        public string Store_no { get; set; } = string.Empty;
        public bool ok_toemail { get; set; }
        public bool ok_totext { get; set; }
        public bool ok_tocall { get; set; }
        public bool ok_tomail { get; set; }
        public string iDType { get; set; } = string.Empty;
        public string iDNumber { get; set; } = string.Empty;
        public string nation { get; set; } = string.Empty;
        public string height { get; set; } = string.Empty;
        public string weight { get; set; } = string.Empty;
        public string hairColor { get; set; } = string.Empty;
        public string eyeColor { get; set; } = string.Empty;
        public DateTime? DOB { get; set; } = null;
        public bool Non_Taxable { get; set; } = false;
        public string Non_Taxable_Reason { get; set; } = string.Empty;
        public bool No_Mass_SMS { get; set; } = false;
        public string Source { get; set; } = string.Empty;
        public string TERM_TEXT { get; set; } = string.Empty;
        public bool install { get; set; } = false;
        public bool declined { get; set; } = false;

        public string SpouseAcc { get; set; } = string.Empty;
        public bool _Dealer { get; set; } = false;
        public bool _Donotemailst { get; set; } = false;


        public string Id_Type { get; set; } = string.Empty;
        public string Sex { get; set; } = string.Empty;
        public string Race_WM { get; set; } = string.Empty;
        public string Eye { get; set; } = string.Empty;
        public string Height_WM { get; set; } = string.Empty;

        public bool _Noreffal { get; set; } = false;
        public bool _Action { get; set; } = false;
        public bool _LoyaltyProgram { get; set; } = false;

    }
    
    public class CustomerTemplateViewModel
    {
        public string TemplateName { get; set; }
    }
}
