// venkat 12/26/2025 added for handling subreports

namespace YJWebCoreMVC.ReportEngine
{
    public class SubReportRequest
    {
        // Subreport RDLC file name (example: rptPrintRtvItem.rdlc)
        public string ReportName { get; set; }

        // Dataset name inside the subreport RDLC
        public string DataSetName { get; set; }

        // Actual data (DataTable / IEnumerable)
        public object SubData { get; set; }

    }
}
