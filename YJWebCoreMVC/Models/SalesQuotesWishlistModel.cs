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
}
