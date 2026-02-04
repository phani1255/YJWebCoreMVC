using Microsoft.AspNetCore.Mvc.Rendering;

namespace YJWebCoreMVC.Models
{
    public class CustomerModel
    {
        // ===== Identity =====
        public int ID { get; set; }
        public string ACC { get; set; } = string.Empty;
        public string BILL_ACC { get; set; } = string.Empty;
        public string old_customer { get; set; } = string.Empty;
        public string oldCustomerCode { get; set; } = string.Empty;
        public string RefBy_acc { get; set; } = string.Empty;

        // ===== Names =====
        public string NAME { get; set; } = string.Empty;
        public string NAME2 { get; set; } = string.Empty;
        public string BUYER { get; set; } = string.Empty;

        // ===== Addresses (Primary) =====
        public string ADDR1 { get; set; } = string.Empty;
        public string ADDR12 { get; set; } = string.Empty;
        public string ADDR13 { get; set; } = string.Empty;
        public string CITY1 { get; set; } = string.Empty;
        public string STATE1 { get; set; } = string.Empty;
        public string ZIP1 { get; set; } = string.Empty;
        public string COUNTRY { get; set; } = string.Empty;

        // ===== Addresses (Secondary / Shipping) =====
        public string ADDR2 { get; set; } = string.Empty;
        public string ADDR22 { get; set; } = string.Empty;
        public string ADDR23 { get; set; } = string.Empty;
        public string CITY2 { get; set; } = string.Empty;
        public string STATE2 { get; set; } = string.Empty;
        public string ZIP2 { get; set; } = string.Empty;
        public string COUNTRY2 { get; set; } = string.Empty;
        public string ON_MAIL { get; set; } = string.Empty;
        public string REASON { get; set; } = String.Empty;
        public bool? COLCSENT { get; set; } = false;
        public string COLCRSN { get; set; } = String.Empty;

        public bool different_ship { get; set; }
        public string SHIP_VIA { get; set; } = string.Empty;
        public string SHIP_TYPE { get; set; } = string.Empty;

        // ===== Contact =====
        public decimal? TEL { get; set; }
        public decimal? TEL2 { get; set; }
        public decimal? FAX { get; set; }
        public string CELL { get; set; } = string.Empty;
        public string EMAIL { get; set; } = string.Empty;
        public string WWW { get; set; } = string.Empty;
        public string OSTEL { get; set; } = string.Empty;

        // ===== Preferences =====
        public bool ok_toemail { get; set; }
        public bool ok_totext { get; set; }
        public bool ok_tocall { get; set; }
        public bool ok_tomail { get; set; }
        public bool No_Mass_SMS { get; set; }

        // ===== Dates =====
        public DateTime? DOB { get; set; }
        public DateTime? EST_DATE { get; set; }
        public DateTime? LAST_INT { get; set; }
        public DateTime? COLCDATE { get; set; }

        // ===== Financial =====
        public decimal? CREDIT { get; set; }
        public decimal? INTEREST { get; set; }
        public decimal? GRACE { get; set; }
        public bool ON_ACCOUNT { get; set; }
        public bool IS_COD { get; set; }
        public string COD_TYPE { get; set; } = string.Empty;
        public bool? ON_HOLD { get; set; }
        public bool? INACTIVE { get; set; }
        public bool declined { get; set; }

        // ===== Terms =====
        public decimal? TERM1 { get; set; }
        public decimal? TERM2 { get; set; }
        public decimal? TERM3 { get; set; }
        public decimal? TERM4 { get; set; }
        public decimal? TERM5 { get; set; }
        public decimal? TERM6 { get; set; }
        public decimal? TERM7 { get; set; }
        public decimal? TERM8 { get; set; }

        public decimal? TERM_PCT1 { get; set; }
        public decimal? TERM_PCT2 { get; set; }
        public decimal? TERM_PCT3 { get; set; }
        public decimal? TERM_PCT4 { get; set; }
        public decimal? TERM_PCT5 { get; set; }
        public decimal? TERM_PCT6 { get; set; }
        public decimal? TERM_PCT7 { get; set; }
        public decimal? TERM_PCT8 { get; set; }

        public string TERM_TEXT { get; set; } = string.Empty;

        // ===== Sales =====
        public string SALESMAN1 { get; set; } = string.Empty;
        public string SALESMAN2 { get; set; } = string.Empty;
        public string SALESMAN3 { get; set; } = string.Empty;
        public string SALESMAN4 { get; set; } = string.Empty;

        public decimal? SHARE1 { get; set; }
        public decimal? PERCENT { get; set; }
        public decimal? PERCENT1 { get; set; }
        public decimal? PERCENT2 { get; set; }
        public decimal? PERCENT3 { get; set; }
        public decimal? PERCENT4 { get; set; }

        // ===== Tax / Compliance =====
        public string TAX_ID { get; set; } = string.Empty;
        public bool Non_Taxable { get; set; }
        public string Non_Taxable_Reason { get; set; } = string.Empty;
        public string RESIDENT { get; set; } = string.Empty;
        public string PRICE_FILE { get; set; } = string.Empty;
        public string JBT { get; set; } = string.Empty;

        // ===== IDs / KYC =====
        public string driverlicense_state { get; set; } = string.Empty;
        public string driverlicense_number { get; set; } = string.Empty;
        public string CONTACTMODE { get; set; } = String.Empty;
        public string SSN { get; set; } = String.Empty;
        public string Ring_Size_1 { get; set; } = string.Empty;
        public string Ring_Size_2 { get; set; } = string.Empty;
        public string Id_Type { get; set; } = string.Empty;
        public string iDType { get; set; } = string.Empty;
        public string iDNumber { get; set; } = string.Empty;
        public string Sex { get; set; } = string.Empty;

        // ===== Physical / KYC Extra =====
        public string nation { get; set; } = string.Empty;
        public string height { get; set; } = string.Empty;
        public string weight { get; set; } = string.Empty;
        public string hairColor { get; set; } = string.Empty;
        public string eyeColor { get; set; } = string.Empty;
        public string Eye { get; set; } = string.Empty;
        public string Height_WM { get; set; } = string.Empty;
        public string Race_WM { get; set; } = string.Empty;

        // ===== Attributes =====
        public string ATTR1VAL { get; set; } = string.Empty;
        public string ATTR2VAL { get; set; } = string.Empty;
        public string ATTR3VAL { get; set; } = string.Empty;
        public string ATTR4VAL { get; set; } = string.Empty;
        public string ATTR5VAL { get; set; } = string.Empty;
        public string ATTR6VAL { get; set; } = string.Empty;
        public string ATTR7VAL { get; set; } = string.Empty;
        public string ATTR8VAL { get; set; } = string.Empty;

        public string MULTIATTR1VAL { get; set; } = string.Empty;
        public string MULTIATTR2VAL { get; set; } = string.Empty;
        public string MULTIATTR3VAL { get; set; } = string.Empty;

        public bool CUSTCHECKVAL1 { get; set; }
        public bool CUSTCHECKVAL2 { get; set; }
        public bool CUSTCHECKVAL3 { get; set; }
        public bool CUSTCHECKVAL4 { get; set; }
        public bool CUSTCHECKVAL5 { get; set; }
        public bool CUSTCHECKVAL6 { get; set; }
        public bool CUSTCHECKVAL7 { get; set; }
        public bool CUSTCHECKVAL8 { get; set; }

        public string CUSTATTRLABELS { get; set; } = string.Empty;

        // ===== Misc =====
        public string Store_no { get; set; } = string.Empty;
        public string DIVISION { get; set; } = string.Empty;
        public string NOTE { get; set; } = string.Empty;
        public string POPUPNOTE { get; set; } = string.Empty;
        public string Source { get; set; } = string.Empty;
        public string FDX_NO { get; set; } = string.Empty;
        public string WEB_USER { get; set; } = string.Empty;
        public string WEB_PASSWORD { get; set; } = string.Empty;
        public bool ISPRIVATECUST { get; set; }
        public bool install { get; set; }

        // ===== Flags =====
        public bool _Dealer { get; set; }
        public bool _Donotemailst { get; set; }
        public bool _Noreffal { get; set; }
        public bool _Action { get; set; }
        public bool _LoyaltyProgram { get; set; }

        // ===== Relations =====
        public string SpouseAcc { get; set; } = string.Empty;

        // ===== UI-only =====
        public IEnumerable<SelectListItem>? AllStatesList { get; set; }
        public IEnumerable<SelectListItem>? AllCountriesList { get; set; }
        public IEnumerable<SelectListItem>? AllSalesManList { get; set; }
        public IEnumerable<SelectListItem>? MainEventList { get; set; }
        public IEnumerable<SelectListItem>? SubEventList { get; set; }
        public IEnumerable<SelectListItem>? AllStores { get; set; }
    }

}
