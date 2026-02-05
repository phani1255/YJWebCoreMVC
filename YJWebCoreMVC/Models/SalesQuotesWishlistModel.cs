using Microsoft.AspNetCore.Mvc.Rendering;

namespace YJWebCoreMVC.Models
{
    public class SalesQuotesWishlistModel
    {

        public string STYLE { get; set; }
        public string DESCRIPTION { get; set; }
        public decimal PRICE { get; set; }
        public string CustomerCode { get; set; }
        public int CustomerCodeId { get; set; }
        public IEnumerable<SelectListItem> CustomerCodes { get; set; }
        public string PotentialCustomerCode { get; set; }
        public IEnumerable<SelectListItem> PotentialCustomerCodes { get; set; }
        public string storecode { get; set; }
        public IEnumerable<SelectListItem> AllStores { get; set; }
        public IEnumerable<SalesQuotesWishlistModel> AllStyles { get; set; }
        public IEnumerable<AnalyticsModel> getQuotationDetailsList { get; set; }
        public int source { get; set; }
        public IEnumerable<SelectListItem> AllSources { get; set; }
        public string salesman { get; set; }
        public string salesman2 { get; set; }
        public IEnumerable<SelectListItem> AllSalesmanList { get; set; }
        public string bankcode { get; set; }
        public IEnumerable<SelectListItem> AllBankCodes { get; set; }
        public IEnumerable<SelectListItem> AllStoreCodes { get; set; }

    }

    public class QuoteModel
    {
        public string QN { get; set; }
        public string ACC { get; set; }
        public string STYLE { get; set; }
        public string ENTER_DATE { get; set; }
        public string DESC { get; set; }
        public string OPERATOR { get; set; }
        public decimal PRICE { get; set; }
    }
    public class PotentialCustomerModel
    {
        public string ACC { get; set; }
        public string NAME { get; set; }
        public string ADDR1 { get; set; }
        public string ADDR12 { get; set; }
        public string CITY1 { get; set; }
        public string STATE1 { get; set; }
        public string ZIP1 { get; set; }
        public string COUNTRY { get; set; }
        public string EMAIL { get; set; }
        public string NOTE1 { get; set; }
        public string TEL { get; set; }
        public string FAX { get; set; }
        public DateTime? EST_DATE { get; set; }
        public string SALESMAN { get; set; }
        public string DNB { get; set; }
    }
}
