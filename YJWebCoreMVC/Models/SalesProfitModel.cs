using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace YJWebCoreMVC.Models
{
    public class SalesProfitModel
    {
        #region Detailed Sales Profit Parameters and list

        public string subcategory { get; set; }
        public IEnumerable<SelectListItem> SubCategoryTypes { get; set; }
        public string brandName { get; set; }
        public IEnumerable<SelectListItem> BrandTypes { get; set; }
        public string metalName { get; set; }
        public IEnumerable<SelectListItem> MetalTypes { get; set; }
        public string vendorName { get; set; }
        public IEnumerable<SelectListItem> VenderTypes { get; set; }
        public IEnumerable<SelectListItem> Groups { get; set; }
        public string CustomerCode { get; set; }
        public int CustomerCodeId { get; set; }
        public IEnumerable<SelectListItem> CustomerCodes { get; set; }
        public string categoryName { get; set; }
        public IEnumerable<SelectListItem> CategoryTypes { get; set; }
        public IEnumerable<SelectListItem> Salesmans { get; set; }
        public IEnumerable<SelectListItem> GLClasses { get; set; }
        public IEnumerable<SelectListItem> Sources { get; set; }
        public IEnumerable<SelectListItem> Stores { get; set; }



        public string ACC { get; set; }
        public string InvoiceNo { get; set; }
        [DataType(DataType.Date)]
        public DateTime? SaleDate { get; set; }

        public string SaleDate1 { get; set; }

        public decimal NetProfit { get; set; }

        public int Quantity { get; set; }
        public string Style { get; set; }
        public decimal Amount { get; set; }
        public decimal CostQty { get; set; }
        public decimal Cost { get; set; }
        public decimal Profit { get; set; }
        public string Description { get; set; }

        public decimal BankFee { get; set; }

        public string VndStyle { get; set; }
        public decimal RetailAmount { get; set; }
        public string StoreNo { get; set; }
        public string Source { get; set; }

        public string Salesman { get; set; }
        public string StoreName { get; set; }
        public string CastCode { get; set; }
        public string ClassGl { get; set; }
        public string GroupName { get; set; }
        public bool rbDateSelection { get; set; }
        public decimal InStock { get; set; }
        public IEnumerable<SalesProfitModel> getAllSalesProfitList { get; set; }
        public decimal Sales { get; set; }
        public decimal Margin { get; set; }
        public bool usePurchasePrice { get; set; }
        public decimal Qty_Received { get; set; }
        public decimal Total_Cost { get; set; }
        public decimal Qty_Sold { get; set; }
        public decimal Inv_Amount { get; set; }
        public decimal Inv_Profit { get; set; }
        // public decimal InStock { get; set; }
        public decimal InStockValue { get; set; }
        public string Vendor { get; set; }
        public string LastpayDate1 { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public decimal Total { get; set; }
        public decimal GrandTotal { get; set; }
        public decimal SREPAMTSHARE { get; set; }

        public decimal SREP { get; set; }

        public decimal SREPCOSTSHARE { get; set; }
        public decimal comish1 { get; set; }
        public decimal comish2 { get; set; }
        public decimal comish3 { get; set; }
        public decimal comish4 { get; set; }
        public string SREPNew { get; set; }

        public string layaway { get; set; }
        public string Special { get; set; }
        public decimal SubTotal { get; set; }
        public decimal COGs { get; set; }
        public decimal GrossProfit { get; set; }
        public string salesman3 { get; set; }
        public string salesman4 { get; set; }
        public decimal salesmanRate3 { get; set; }
        public decimal salesmanRate4 { get; set; }
        public string PriceRange { get; set; }
        public decimal OtherCharges { get; set; }
        public bool rbDateSelection1 { get; set; }
        public decimal QuantityNew { get; set; }

        #endregion



    }
}
