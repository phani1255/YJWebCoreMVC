using System.Data;

namespace YJWebCoreMVC.Models
{
    public class ShopifyModel
    {
        public string _ShopifyCustId { get; set; }
        public int TotalOrdersProcessed { get; private set; }
        public static int _MaxLimit = 0, _MinLimit = 0;
        private string INVOICE_NO = string.Empty;
        Int64 value = 0, getmaxval = 0;
        private string _storeName = "";
        public bool unattended = false;

        DataTable dtDownloadedOrders;
    }
}
