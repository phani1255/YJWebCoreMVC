using Microsoft.AspNetCore.Mvc.Rendering;

namespace YJWebCoreMVC.Models
{
    public class InvoiceModel
    {

        public string INV_NO { get; set; } = string.Empty;

        public string BACC { get; set; } = string.Empty;

        public string ACC { get; set; } = string.Empty;

        public decimal? ADD_COST { get; set; } = 0;

        public decimal? SNH { get; set; } = 0;

        public DateTime? DATE { get; set; } = DateTime.Now;

        public string PON { get; set; } = string.Empty;

        public string MESSAGE { get; set; } = string.Empty;

        public decimal? GR_TOTAL { get; set; } = 0;

        public string ADDR1 { get; set; } = string.Empty;

        public string ADDR2 { get; set; } = string.Empty;

        public string ADDR3 { get; set; } = string.Empty;

        public string CITY { get; set; } = string.Empty;

        public string STATE { get; set; } = string.Empty;

        public string ZIP { get; set; } = string.Empty;

        public string COUNTRY { get; set; } = string.Empty;

        public decimal? DEDUCTION { get; set; } = 0;

        public string VIA_UPS { get; set; } = string.Empty;

        public string IS_COD { get; set; } = string.Empty;

        public decimal? WEIGHT { get; set; } = 0;

        public string COD_TYPE { get; set; } = string.Empty;

        public string CUST_PON { get; set; } = string.Empty;

        public string OPERATOR { get; set; } = string.Empty;

        public string SHIP_BY { get; set; } = string.Empty;

        public string FIN_REF { get; set; } = string.Empty;

        public string SAMPLE { get; set; } = string.Empty;

        public decimal? TERM1 { get; set; } = 0;

        public decimal? TERM2 { get; set; } = 0;
        public string BUYER { get; set; } = "";
        public decimal? TEL { get; set; } = 0;
        public string Email { get; set; } = "";
        public string PRICE_FILE { get; set; } = "";
        public decimal? TERM3 { get; set; } = 0;
        public bool? no_stat { get; set; }
        public bool iSByPicInv { get; set; } = false;

        public decimal? TERM4 { get; set; } = 0;

        public decimal? TERM_PCT1 { get; set; } = 0;

        public decimal? TERM_PCT2 { get; set; } = 0;

        public decimal? TERM_PCT3 { get; set; } = 0;

        public decimal? TERM_PCT4 { get; set; } = 0;

        public string NAME { get; set; } = string.Empty;

        public string PICKUPNO { get; set; } = string.Empty;

        public string UPSTRAK { get; set; } = string.Empty;

        public string ATTN { get; set; } = string.Empty;

        public string COMBINE { get; set; } = string.Empty;

        public decimal? INSURED { get; set; } = 0;

        public string EARLY { get; set; } = string.Empty;
        public string DP { get; set; }

        public decimal? PERCENT { get; set; } = 0;

        public bool? MAN_SHIP { get; set; } = false;

        public string DEPT { get; set; } = string.Empty;

        public string RESIDENT { get; set; } = string.Empty;

        public decimal? TERM5 { get; set; } = 0;

        public decimal? TERM_PCT5 { get; set; } = 0;

        public decimal? TERM6 { get; set; } = 0;

        public decimal? TERM_PCT6 { get; set; } = 0;

        public decimal? TERM7 { get; set; } = 0;

        public decimal? TERM_PCT7 { get; set; } = 0;

        public decimal? TERM8 { get; set; } = 0;

        public decimal? TERM_PCT8 { get; set; } = 0;

        public bool? IS_FDX { get; set; } = false;

        public bool? IS_DEB { get; set; } = false;

        public string CHECK_NO { get; set; } = string.Empty;

        public int SHIPTYPE { get; set; } = 0;

        public int AMOUNT_TYPE { get; set; } = 0;

        public string SALESMAN1 { get; set; } = string.Empty;
        public string SALESMAN2 { get; set; } = string.Empty;
        public decimal? DED_PER { get; set; }
        public string STORE_NO { get; set; } = string.Empty;
        public bool? TAXABLE { get; set; } = false;
        public decimal? SALES_TAX { get; set; } = 0;
        public bool? TRADEIN { get; set; } = false;
        public string TRADEINDESC { get; set; } = string.Empty;
        public decimal? TRADEINAMT { get; set; } = 0;
        public bool? SPECIAL { get; set; } = false;
        public bool? PICKED { get; set; } = false;
        public Decimal SALES_TAX_PER { get; set; }
        public bool? TAXINCLUDED { get; set; } = false;
        public string SYSTEMNAME { get; set; } = string.Empty;
        public int ShipType { get; set; }
        public Decimal NO_BOX { get; set; }
        public string USERGCNO { get; set; } = string.Empty;
        public bool? CCRD { get; set; }
        public bool? is_Watches { get; set; }
        public string type { get; set; }
        public bool? BY_WT { get; set; }
        public bool Laptop { get; set; } = false;
        public decimal? SILVER_PRICE { get; set; }
        public bool IS_GLENN { get; set; } = false;
        public string CASH_REG_CODE { get; set; } = string.Empty;
        public string CASH_REG_STORE { get; set; } = string.Empty;
        public string division { get; set; }
        public string RET_INV_NO { get; set; } = string.Empty;
        public string charity { get; set; } = string.Empty;
        public decimal? charity_amount { get; set; } = 0;
        public decimal? Sales_Tax_Rate { get; set; } = 0;
        public string SALESMAN3 { get; set; } = string.Empty;
        public string SALESMAN4 { get; set; } = string.Empty;
        public string BANK { get; set; } = string.Empty;
        public bool print_Check { get; set; } = false;
        public string Registry { get; set; } = string.Empty;
        public decimal? comish1 { get; set; } = 0;
        public decimal? comish2 { get; set; } = 0;
        public decimal? comish3 { get; set; } = 0;
        public decimal? comish4 { get; set; } = 0;
        public string PROGRAM { get; set; } = "";
        public string Currency { get; set; } = "";
        public decimal? COMISHAMOUNT1 { get; set; } = 0;
        public bool iSAsPerContract { get; set; } = false;
        public decimal? COMISHAMOUNT2 { get; set; } = 0;
        public decimal? COMISHAMOUNT3 { get; set; } = 0;
        public decimal? COMISHAMOUNT4 { get; set; } = 0;
        public bool ISByAmount { get; set; } = false;
        public decimal? DISC_PERCENT { get; set; } = 0;
        public decimal? Sales_fee_Rate { get; set; } = 0;
        public decimal? Sales_fee_Amount { get; set; } = 0;
        public string NOTE { get; set; } = string.Empty;
        public decimal? SalesTax1 { get; set; } = 0;
        public decimal? SalesTax2 { get; set; } = 0;
        public decimal? SalesTax3 { get; set; } = 0;
        public string ScrapLogno { get; set; } = string.Empty;
        public bool? AllowTradeIn { get; set; } = false;
        public string noTax_reason { get; set; } = string.Empty;
        public decimal? EMI_INTERESTAMOUNT { get; set; } = 0;
        public decimal? EMI_MONTHLYPAYMENT { get; set; } = 0;
        public int EMI_NOOFINSTALLMENTS { get; set; } = 0;
        public decimal? EMI_RATEOFINTEREST { get; set; } = 0;
        public bool IS_INSTALLMENT { get; set; } = false;
        public DateTime Final_Payment_Due_Date { get; set; } = DateTime.Now;
        public int Payment_Frequency { get; set; } = 0;
        public string TaxState { get; set; } = string.Empty;
        public bool iSNotPickedUp { get; set; } = false;
        public decimal Gold_Price { get; set; } = 0;
        public decimal Silver_Price { get; set; } = 0;
        public decimal Plat_Price { get; set; } = 0;
        public bool DoNotChangePaymentStore { get; set; } = false;
        public bool iSVatInclude { get; set; } = false;
        public String GivenChange { get; set; } = String.Empty;
        public bool iSCompanyName2 { get; set; } = false;
        public bool? is_sample { get; set; }
        public bool? is_private { get; set; }
        public decimal? multcost { get; set; }
        public bool ISMULTI { get; set; } = false;
        public bool? Is_SwissVat { get; set; } = false;
        public string SHIPFROM { get; set; } = "";
        public decimal AddPer { get; set; } = 0;
        public decimal GoldSur { get; set; } = 0;
        public decimal? logo_chrg { get; set; } = 0;
        public string Ref { get; set; } = "";
        public decimal Rate { get; set; } = 0;
        public string ShopifyOrderNumber { get; set; } = "";
        public string CompanyName { get; set; } = "";
        public string AvataxTranCode { get; set; } = "";
        public bool Surprise { get; set; } = false;
        public DateTime ExpetToShipDate { get; set; } = DateTime.Now;
        public string Source { get; set; }
        public string Source2 { get; set; }
        public string ShopifyStoreNo { get; set; } = "";
        public bool CustAddByShop { get; set; } = false;
        public bool iSDateSameAsPickup { get; set; } = false;
        public DateTime? INV_DATE { get; set; }
        public string CUSTOMER_CODE { get; set; } = "";
        public string CUSTOMER_NAME { get; set; } = "";
        public string STORE { get; set; } = "";
        public decimal? GRAND_TOTAL { get; set; }
        public string ShopifyOrderID { get; set; } = "";
        public DateTime? DRAFT_DATE { get; set; }
        public decimal GrandTotal { get; set; }
        public string AccountCode { get; set; }
        public IEnumerable<SelectListItem> CustomerCodes { get; set; }
        public List<string> Reasons { get; set; } = new List<string>();
        public string SelectedReason { get; set; }
        public string FPON { get; set; } = "";
        public string CustomerName { get; set; }
        public string store { get; set; } = "";
        public string email { get; set; } = "";
        public string Customer { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string Country { get; set; }
        public string Tel { get; set; }
        public string SalesRep1 { get; set; } = string.Empty;
        public string SalesRep2 { get; set; } = string.Empty;
        public string SalesRep3 { get; set; } = string.Empty;
        public string SalesRep4 { get; set; } = string.Empty;
        public decimal SubTotal { get; set; }
        public List<string> PaymentTypes { get; set; } = new List<string>();
        public List<string> DiscountTypes { get; set; } = new List<string>();
        public string DiscountType { get; set; }
        public decimal TotalDiscount { get; set; }
        public decimal TradeIn { get; set; }
        public decimal Shipping { get; set; }
        public decimal SalesTaxPercent { get; set; } = 10;
        public decimal SalesTaxAmount { get; set; }
        public decimal SalesFeePercent { get; set; } = 0;
        public decimal SalesFee { get; set; }
        public bool Isinvoice { get; set; } = true;// defualt now true untile memo we will implimemnt
        public bool iSMarkInvoicePickedUp { get; set; } = false;
        public List<InvoiceModel> Discounts { get; set; } = new List<InvoiceModel>();
        public string ReceiptNumber { get; set; } = string.Empty;
        public List<InvoiceModel> InvoiceItems { get; set; } = new List<InvoiceModel>();
        public List<InvoiceModel> Payments { get; set; } = new List<InvoiceModel>();
        public InvoiceModel InvoiceSummary { get; set; }
        public InvoiceModel TransactionDetails { get; set; }
        public decimal Discount { get; set; }
        public string Style { get; set; }
        public string Description { get; set; }
        public decimal Quantity { get; set; }
        public decimal TagPrice { get; set; }
        public decimal Price { get; set; }
        public bool SPCL { get; set; }
        public bool Shop { get; set; }
        public decimal TotalPrice { get; set; }
        public string PaymentType { get; set; }
        public decimal Amount { get; set; }
        public decimal Tax { get; set; }
        public decimal Total { get; set; }
        public string TransactionId { get; set; }
        public DateTime TransactionDate { get; set; }
        public bool Is_Update { get; set; }
        public string ScrapLogNo { get; set; }
        public string TradeInDescription { get; set; }
        public decimal TradeInAmount { get; set; }
        public string METHOD { get; set; }
        public decimal CURR_RATE { get; set; }
        public decimal CURR_AMOUNT { get; set; }
        public decimal AMOUNT { get; set; }
        public string SALESMAN { get; set; }
        public int askedSigature { get; set; }
        public bool isold { get; set; }
        public bool isoldNon_Layaway { get; set; }
        public bool AUTHRIZED { get; set; }
        public string CURR_TYPE { get; set; }
        public string PAY_NO { get; set; }
        public string CreditNo { get; set; }
        public decimal CreditAmt { get; set; }
        public decimal EnteredAmt { get; set; }
        public string NEW_CreditNo { get; set; }
        public decimal NEW_CreditAmt { get; set; }
        public decimal NEW_EnteredAmt { get; set; }
        public bool? No_TAX { get; set; }

    }
}
