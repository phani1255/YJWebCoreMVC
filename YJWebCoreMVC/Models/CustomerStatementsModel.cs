namespace YJWebCoreMVC.Models
{
    public class CustomerStatementsModel
    {
        // Properties for finance charges form
        public string InvoiceNo { get; set; }
        public string ACC { get; set; }
        public string SaleDate1 { get; set; }
        public string Terms { get; set; }
        public string Salesman { get; set; }
        public string BillingAddr_Name { get; set; }
        public string BillingAddr_Addr1 { get; set; }
        public string BillingAddr_Addr2 { get; set; }
        public string BillingAddr_City { get; set; }
        public string BillingAddr_State { get; set; }
        public string BillingAddr_Country { get; set; }
        public string BillingAddr_ZipCode { get; set; }
        public string ShippingAddr_Name { get; set; }
        public string ShippingAddr_Addr1 { get; set; }
        public string ShippingAddr_Addr2 { get; set; }
        public string ShippingAddr_City { get; set; }
        public string ShippingAddr_State { get; set; }
        public string ShippingAddr_Country { get; set; }
        public string ShippingAddr_ZipCode { get; set; }
        public string SubTotal { get; set; }
        public string GrandTotal { get; set; }
        public string NoteMessage { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public decimal OtherCharges { get; set; }
        public decimal ShipAndHandling { get; set; }

    }
}
