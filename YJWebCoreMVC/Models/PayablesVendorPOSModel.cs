namespace YJWebCoreMVC.Models
{
    public class PayablesVendorPOSModel
    {
        public string STYLE { get; set; }
        public decimal QTY { get; set; }
        public string VENDOR { get; set; }
        public string VendorPO { get; set; }
        public string PON { get; set; }
        public bool RECEIVED { get; set; }

        public static string FixedStoreCode { get; set; }
        public static string StoreCodeInUse { get; set; }
    }
}
