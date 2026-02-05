namespace YJWebCoreMVC.Models
{
    public class OrderModel
    {

    }

    public class ListOfPO
    {
        public bool AllCustomers { get; set; } = true;
        public string CustomerCode { get; set; }

        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public bool AllDates { get; set; } = true;

        public bool SearchByOrderDate { get; set; } = true;
        public string Category { get; set; } = "All";
        public string MemoOption { get; set; } = "All";

        public bool OpenPOOnly { get; set; }
        public bool ShowDetails { get; set; }
        public bool ExcludeReserved { get; set; }
        public bool DateOfEssence { get; set; }

        public bool AllOrderTypes { get; set; } = true;
        public string OrderType { get; set; }

        public bool AllDivisions { get; set; } = true;
        public string Division { get; set; }

        public string RefNo { get; set; }
        public string StyleStartsWith { get; set; }
        public bool ShowInvoiceDates { get; set; }
        public bool ShowVPO { get; set; }
        public bool ShowPayTerm { get; set; }
        public bool TotPerStyle { get; set; }
        public bool ShowSku { get; set; }

    }
}
