using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace YJWebCoreMVC.Models
{
    public class PosDetailsModel
    {

        public string INV_NO { get; set; }
        //public DateTime {get;set;}
        [DataType(DataType.Date)]
        public DateTime? OrderDate { get; set; }
        public IEnumerable<SelectListItem> CustomerCodes { get; set; }
        public string STYLE { get; set; }
        public decimal QTY { get; set; }
        public string ACC { get; set; }
        public string PON { get; set; }
        public decimal RCVD { get; set; }
        public string NOTE { get; set; }
        public decimal EXTRA { get; set; }
        public string SIZE { get; set; }
        [DataType(DataType.Date)]
        public DateTime? DUE_DATE { get; set; }
        public string NOTE1 { get; set; }
        public string NOTE2 { get; set; }
        public string NOTE3 { get; set; }
        public decimal GOLD_PRICE { get; set; }
        public string vnd_style { get; set; }
        public string GOLD_COLR { get; set; }
        public string METAL { get; set; }
        public string STYLE_NO { get; set; }
        public decimal SELL_PRICE { get; set; }
        public decimal STONE_COST { get; set; }
        public decimal CHAIN_PRC { get; set; }
        public decimal PRICE { get; set; }
        public string PNOTE { get; set; }
        public decimal COST { get; set; }
        public string ORG_STYLE { get; set; }
        public decimal CLOSED { get; set; }
        public string HEADSHAPE { get; set; }
        public string HEADSIZE { get; set; }
        public string HEADTYPE { get; set; }
        public string QUALITY { get; set; }
        public DateTime SHIP_DATE { get; set; }
        public string METAL_STAMP { get; set; }
        public string TRADEMARK { get; set; }
        public int item_no { get; set; }
        public string INVOICE_NO { get; set; }
        public string ProcessedBy { get; set; }
        public decimal WEIGHT { get; set; }
        public decimal SHIPED { get; set; }
        public decimal Qty_Open { get; set; }
        public string ship_status { get; set; }
        public bool confirmed { get; set; }
        public string Approved { get; set; }
        public DateTime Approve_Date { get; set; }
        public string Approve_Note { get; set; }

        public string Stores { get; set; }

        public string Warranty { get; set; }
        public decimal WarrantyCost { get; set; }
        public DateTime Date { get; set; }
        //public List<WarrantyItemVM> WarrantyItems { get; set; }
        public bool IsSelected { get; set; }
        public string StoreName { get; set; }
        public IEnumerable<SelectListItem> StoresList { get; set; }
        public bool rbDateSelection { get; set; }
        public string InvoiceNo { get; set; }
        [DataType(DataType.Date)]
        public DateTime? SaleDate { get; set; }
        public string SaleDate1 { get; set; }
        public string Salesman { get; set; }
        public string Register { get; set; }
        public string Style { get; set; }
        public decimal Price { get; set; }
        public decimal Amount { get; set; }
        public decimal TagAmount { get; set; }
        public decimal CostQty { get; set; }
        public string Description { get; set; }
        public string BillingAddr_Name { get; set; }
        public string BillingAddr_Addr1 { get; set; }
        public string BillingAddr_Addr2 { get; set; }
        public string BillingAddr_City { get; set; }
        public string BillingAddr_State { get; set; }
        public string BillingAddr_Country { get; set; }
        public string BillingAddr_ZipCode { get; set; }

        public string NoteMessage { get; set; }
        public string salesTaxAmount { get; set; }
        public string GrandTotal { get; set; }
        public string SubTotal { get; set; }
        public string TradeInAmount { get; set; }
        public string ShippingAmount { get; set; }
        public string PaidAmount { get; set; }
        public string BalanceDue { get; set; }


        public string PaymentDate { get; set; }

        public string PaymentType { get; set; }

        public string PaymentMethod { get; set; }
        public decimal Payment { get; set; }
        public string PaymentNote { get; set; }
        public string PaymentCurrType { get; set; }
        public string PaymentCurrRate { get; set; }
        public decimal PaymentCurrAmount { get; set; }
        public string customercode { get; set; }
        public string ordernumber { get; set; }
        public string styledesc { get; set; }
        public decimal styprice { get; set; }
        public DateTime orderentrydate { get; set; }
        public DateTime ordercanceldate { get; set; }

        public IEnumerable<PosDetailsModel> getSalesTaxList { get; set; }

        public bool rbListofDiscountsSelection { get; set; }
        public string fromDate { get; set; }
        public string toDate { get; set; }
        public string salesTaxRate { get; set; }
        public string StateName { get; set; }
        public string NetAmount { get; set; }
        public string NoTaxReason { get; set; }
        public string NoTaxReasonGrandTotal { get; set; }
        public decimal NoTaxReasonGrandTotal1 { get; set; }
        public IEnumerable<PosDetailsModel> NoSalesTaxReasonList { get; set; }
        public string CityName { get; set; }
        public string cPPToken = string.Empty;

    }
}