using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data;

namespace YJWebCoreMVC.Models
{
    public class SalesmenModel
    {

        public string salesmen { get; set; }
        public IEnumerable<SelectListItem> salesmenlist { get; set; }
        public string store { get; set; }
        public IEnumerable<SelectListItem> storeList { get; set; }
        public IEnumerable<SalesmenModel> salesmanListsalesmanActiveList { get; set; }
        public string InvoiceNo { get; set; }
        public string ACC { get; set; }
        public string SaleDate1 { get; set; }
        public string Salesman { get; set; }
        public string Register { get; set; }
        public string BillingAddr_Name { get; set; }
        public string BillingAddr_Addr1 { get; set; }
        public string BillingAddr_Addr2 { get; set; }
        public string BillingAddr_City { get; set; }
        public string BillingAddr_ZipCode { get; set; }
        public string BillingAddr_State { get; set; }
        public string BillingAddr_Country { get; set; }
        public string ShippingAddr_Name { get; set; }
        public string ShippingAddr_Addr1 { get; set; }
        public string ShippingAddr_Addr2 { get; set; }
        public string ShippingAddr_City { get; set; }
        public string ShippingAddr_State { get; set; }
        public string ShippingAddr_Country { get; set; }
        public string ShippingAddr_ZipCode { get; set; }
        public string SubTotal { get; set; }
        public string TradeInAmount { get; set; }
        public string ShippingAmount { get; set; }
        public string GrandTotal { get; set; }
        public string PaidAmount { get; set; }
        public string salesTaxAmount { get; set; }
        public string BalanceDueb { get; set; }
        public string BalanceDue { get; set; }
        public string Style { get; set; }
        public string Terms { get; set; }
        public string Description { get; set; }

        public string CostQty { get; set; }
        public string TagAmount { get; set; }
        public string Price { get; set; }
        public string Amount { get; set; }
        public string NoteMessage { get; set; }
        public string PaymentDate { get; set; }
        public string PaymentType { get; set; }
        public string Payment { get; set; }
        public string PaymentNote { get; set; }
        public string PaymentCurrType { get; set; }
        public string PaymentCurrRate { get; set; }
        public string PaymentCurrAmount { get; set; }
        public string store_no { get; set; }
        public string refNo { get; set; }
        public string DATE { get; set; }
        public string cusName { get; set; }
        public string sales { get; set; }
        public string salesmanShare { get; set; }
        public string salesman { get; set; }
        public string SalesmanName { get; set; }
        public string commission { get; set; }
        public string paid { get; set; }
        public string ship_via { set; get; }
        public string PON { set; get; }
        public string telephone_no { get; set; }
        public string storeInfo { get; set; }
        public string StoreName { get; set; }
        public byte[] StoreLogoImage { get; set; }

        public string OtherCharges { get; set; }
        public string Deduction { get; set; }
        public DataTable StyleTable { get; set; }
    }
}
