using Microsoft.AspNetCore.Mvc.Rendering;

namespace YJWebCoreMVC.Models
{
    public class ListOfItemsSoldModel
    {
        
        public IEnumerable<SelectListItem> CustomerCodes { get; set; }
        public IEnumerable<SelectListItem> VenderTypes { get; set; }
        public string FROMDATE { get; set; }
        public string DateTo { get; set; }
        public string Store { get; set; }
        public string Sales { get; set; }
        public bool IsLaywayUnpaid { get; set; }
        public bool saletax { get; set; }
        public bool IsInvoicedReport { get; set; }
        public bool DateValueCheck { get; set; }
        public bool repair { get; set; }
        public bool specialorder { get; set; }
        public bool layaway { get; set; }
        public string prom_Fromdate { get; set; }
        public string prom_Todate { get; set; }
        public bool IsOpenQuotes { get; set; }
        public bool register { get; set; }
        public bool ByPickupDate { get; set; }
        public string ByWhichDate { get; set; }
        public string Brand { get; set; }
        public string Category { get; set; }
        public string Selbrand { get; set; }
        public string IsComision { get; set; }
        public string IsCost { get; set; }
        public string CommByDiscount { get; set; }
        public string IsCommbyProfit { get; set; }
        public string Is_DiamondDealer { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string AmountFrom { get; set; }
        public string AmountTo { get; set; }
        public bool ChkSpecifyAmount { get; set; }
        public bool ChkExcludeRepairInvoiceofZeroDollor { get; set; }
        public bool ChkshowshopifyOnly { get; set; }
        public bool ChkOpenInvoicesOnly { get; set; }
        public string Vender { get; set; }
        public string Ccode { get; set; }
        public string SubCategory { get; set; }
        public string Metal { get; set; }
        public string FromStyle { get; set; }
        public string ToStyle { get; set; }
        public string Vendor { get; set; }
        public int NoOfDays { get; set; }
        public bool SummByModel { get; set; }
        public bool Group { get; set; }
        public bool Purcfdate { get; set; }
        public bool Purctdate { get; set; }
        public bool IncludeInactive { get; set; }
        public bool NoSalesTax { get; set; }
        public bool IncludeNotaxInvoices { get; set; }
        public bool TaxState { get; set; }
        public bool IncParialPay { get; set; }
        public bool Day { get; set; }
        public bool WithGP { get; set; }
        public bool SeparateSM { get; set; }
        public bool IsSalesCOG { get; set; }
        public bool Monthproft { get; set; }
        public bool ISLaySpe { get; set; }
        public bool Isinclbankfee { get; set; }
        public bool Details { get; set; }
        public bool Sources { get; set; }
        public bool VendorStyle { get; set; }
        public string Ccode2 { get; set; }
        public string Trantype { get; set; }
        public int IsRepairOnly { get; set; }
        public string CurrencyType { get; set; }
        public int IsShowCurrency { get; set; }
        public string Opt { get; set; }
        public string Vendorcode1 { get; set; }
        public string Vendorcode2 { get; set; }
        public string Check1 { get; set; }
        public string Check2 { get; set; }
        public int Dateval { get; set; }
        public DateTime Date1 { get; set; }
        public DateTime Date2 { get; set; }
        public decimal Amt1 { get; set; }
        public decimal Amt2 { get; set; }
        public string Bank { get; set; }
        public string Glcode { get; set; }
        public bool IsAllGL { get; set; }
        public string StrSearchOption { get; set; }
        public bool Unapplied { get; set; }
        public string _Acc { get; set; }
        public string _Name { get; set; }
        public string _Reason { get; set; }
        public IEnumerable<SelectListItem> AllStores { get; set; }
        public List<SelectListItem> GlCodes { get; set; }

        public List<ListOfItemsSoldModel> ExistingReasons { get; set; }

    }
}
