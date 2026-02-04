namespace YJWebCoreMVC.Models
{
    public class RepairorderModel
    {

        public string REPAIR_NO { get; set; }
        public string ACC { get; set; }
        public string CUS_REP_NO { get; set; }
        public string CUS_DEB_NO { get; set; }
        public DateTime? DATE { get; set; }
        public DateTime? RCV_DATE { get; set; }
        public string MESSAGE { get; set; }
        public string MESSAGE1 { get; set; }
        public string Jeweler_Note { get; set; }
        public bool? OPEN { get; set; }
        public string OPERATOR { get; set; }
        public string NAME { get; set; }
        public string STORE { get; set; } = string.Empty;
        public string ADDR1 { get; set; }
        public string ADDR2 { get; set; }
        public string CITY { get; set; }
        public string STATE { get; set; }
        public string ZIP { get; set; }
        public DateTime? CAN_DATE { get; set; }
        public string COUNTRY { get; set; }
        public decimal? SNH { get; set; }
        public string VIA_UPS { get; set; }
        public string IS_COD { get; set; }
        public string COD_TYPE { get; set; }
        public string EARLY { get; set; }
        public string ISSUE_CRDT { get; set; }
        public string LINE { get; set; }
        public string ITEM { get; set; }
        public string QTY { get; set; }
        public string SHIPED { get; set; }
        public string STAT { get; set; }
        public string VENDOR { get; set; }
        public string BARCODE { get; set; }
        public string SIZE { get; set; }

        public string CITY1 { get; set; }
        public string STATE1 { get; set; }
        public string ZIP1 { get; set; }
        public string ADDR12 { get; set; }
        public string NAME2 { get; set; }
        public string ADDR22 { get; set; }
        public string CITY2 { get; set; }
        public string STATE2 { get; set; }
        public string ZIP2 { get; set; }
        public string SHIP_VIA { get; set; }
        public decimal PERCENT { get; set; }
        public string BILL_ACC { get; set; }
        public string TERM_PCT1 { get; set; }
        public string TERM1 { get; set; }
        public string TERM_PCT2 { get; set; }
        public string TERM2 { get; set; }
        public string TERM_PCT3 { get; set; }
        public string TERM3 { get; set; }
        public string TERM_PCT4 { get; set; }
        public string TERM4 { get; set; }
        public string NOTE { get; set; }
        public string BACC { get; set; }

        public string ADD_COST { get; set; }
        public string PON { get; set; }

        public string V_CTL_NO { get; set; }
        public string GR_TOTAL { get; set; }
        public decimal WEIGHT { get; set; }
        public string INSURED { get; set; }
        public decimal INSUREDDecimal { get; set; }
        public string RESIDENT { get; set; }

        public string DESC { get; set; }
        public string PRICE { get; set; }
        public string INV_NO { get; set; }
        public string DUE_DATE { get; set; }
        public string STYLE { get; set; }
        public string SHIPPED { get; set; }
        public string OLD_QTY { get; set; }
        public string SHIP_TYPE { get; set; }
        public decimal? PRICE1 { get; set; }
        public string USERGCNO { get; set; } = string.Empty;
        public decimal? DEPOSITBAL { get; set; }

        public decimal? ESTIMATE { get; set; }
        public decimal? SALES_TAX { get; set; }

        public bool? TAXABLE { get; set; }

        public string STRDATAXML { get; set; }
        public string SALESMAN1 { get; set; } = string.Empty;
        public string SALESMAN2 { get; set; } = string.Empty;
        public decimal? COMISH1 { get; set; } = 0;
        public decimal? COMISH2 { get; set; } = 0;
        public decimal? COMISHAMOUNT1 { get; set; } = 0;
        public decimal? COMISHAMOUNT2 { get; set; } = 0;
        public string CASH_REGISTER { get; set; } = string.Empty;
        public string Rtn_INV_NO { get; set; }
        public decimal? Deduction { get; set; } = 0;
        public string persons { get; set; } = "";
        public bool? IS_TAX { get; set; } = false;
        public string TaxReason { get; set; } = string.Empty;
        public bool iSFromWarranty { get; set; } = false;
        public decimal SalesFeeAmount { get; set; } = 0;
        public decimal SalesFeeRate { get; set; } = 0;
        public String repStatus { get; set; } = "A";
        public String Who { get; set; }
        public String RepSize { get; set; }
        public String RepMetal { get; set; }
        public bool paymelater { get; set; }
        public String StockStyle { get; set; } = string.Empty;
        public bool iSRepairStock { get; set; } = false;
        public decimal SalesTaxRate { get; set; } = 0;
        public string email { get; set; } = string.Empty;
        public string warranty_inv_no { get; set; } = string.Empty;
        public string warranty_style { get; set; }
        public int SHIPTYPE { get; set; }
        public string SHIP_BY { get; set; }
        public bool Surprise { get; set; }
        public string UPSTRAK { get; set; }
        public bool EstimateReady { get; set; }
        public string _Warrnty_desc { get; set; }
        public bool iSWarrantyRepair { get; set; }
    }
}
