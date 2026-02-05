namespace YJWebCoreMVC.Models
{
    public class SalesChartRequest
    {
        public string Salesman1 { get; set; }
        public string Salesman2 { get; set; }
        public string Salesman3 { get; set; }
        public string Salesman4 { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
    }
}
