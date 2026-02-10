using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace YJWebCoreMVC.Models
{
    public class AnalasysServiceModel
    {

        public string CustomerCode { get; set; }
        public int CustomerCodeId { get; set; }
        public IEnumerable<SelectListItem> CustomerCodes { get; set; }
        public string category { get; set; }
        public IEnumerable<SelectListItem> CategoryTypes { get; set; }
        //public DateTime {get;set;}
        [DataType(DataType.Date)]
        public DateTime? FromDate { get; set; }
        [DataType(DataType.Date)]
        public DateTime? ToDate { get; set; }
        public string subcat { get; set; }
        public IEnumerable<SelectListItem> SubCategoryTypes { get; set; }
        public string brand { get; set; }
        public IEnumerable<SelectListItem> BrandTypes { get; set; }
        public string metal { get; set; }
        public IEnumerable<SelectListItem> MetalTypes { get; set; }

        public string vender { get; set; }
        public IEnumerable<SelectListItem> VenderTypes { get; set; }
        public string group { get; set; }
        public IEnumerable<SelectListItem> AllGroups { get; set; }
        public string storecode { get; set; }
        public IEnumerable<SelectListItem> AllStores { get; set; }
        public string QUALITY { get; set; }
        public string STYLE_NO { get; set; }
        public string ORG_STYLE { get; set; }
        public decimal PRICE { get; set; }
        public decimal On_memo { get; set; }
        public decimal In_STOCK { get; set; }
        public string StoreNo { get; set; }
        public decimal SELL_PRICE { get; set; }
        public int Qty_Open { get; set; }
        public string ACC { get; set; }
        public decimal QTY { get; set; }
        public string name { get; set; }
        public string paid1 { get; set; }
        public string increase1 { get; set; }
        public string paid2 { get; set; }
        public string increase2 { get; set; }
        public string paid3 { get; set; }
        public string email { get; set; }
        public DateTime date { get; set; }
        public decimal sales { get; set; }
        public decimal rtv { get; set; }
        public decimal credits { get; set; }
        public decimal net { get; set; }
        public decimal payments { get; set; }
        public string strFormattedDate { get; set; }
        public string Tel { get; set; }
        public string Address1 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string Salesman { get; set; }
        public IEnumerable<AnalasysServiceModel> getAllSoldorInStockList { get; set; }
        public IEnumerable<AnalasysServiceModel> getBestSellerCategoryReport { get; set; }
        public IEnumerable<AnalasysServiceModel> getBestSellerReport { get; set; }
        public IEnumerable<AnalasysServiceModel> getWorstSellerReport { get; set; }
        public IEnumerable<AnalasysServiceModel> getAnnualPaymentsReceived { get; set; }
        public IEnumerable<AnalasysServiceModel> getAnnualSalesComparison { get; set; }
        public IEnumerable<AnalasysServiceModel> getEndofMonth { get; set; }
        public IEnumerable<AnalasysServiceModel> getTotalMonthlySales { get; set; }
        public IEnumerable<AnalasysServiceModel> getTotalMonthlySalesForACustomer { get; set; }
        public List<CastOrdModel> getALLPOs { get; set; }
        public IEnumerable<SelectListItem> BrandsFromStyle { get; set; }
    }
}
