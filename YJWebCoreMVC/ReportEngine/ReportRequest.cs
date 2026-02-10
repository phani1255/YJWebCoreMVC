/*
 * venkat 12/11/2025 added
 * 12/18/2025 Manoj Added IncludeImage parameter to include images in report
 *  venkat  12/26/2025 Made changes for handling subreports
 *  Dharani 01/26/2026 Made changes for handling width of the subreports.
 *  Dharani 02/03/2026 Made changes to work with core mvc.
 */

using Microsoft.Reporting.NETCore;


namespace YJWebCoreMVC.ReportEngine
{
    public class ReportRequest
    {
        public string ReportPath { get; set; }
        public string ModifiedRdlcXml { get; set; }
        public List<ReportDataSourceItem> DataSources { get; set; } = new List<ReportDataSourceItem>();

        public List<ReportParameter> Parameters { get; set; } = new List<ReportParameter>();

        public string FileName { get; set; } = "Report";

        public bool IncludeImage { get; set; }

        public List<SubReportRequest> SubReports { get; set; }

    }

    public class ReportDataSourceItem
    {
        public string DataSetName { get; set; }
        public object Data { get; set; }
    }

}