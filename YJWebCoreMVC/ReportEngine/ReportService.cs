
/*
 *  venkat 12/11/2025 added
 *  12/18/2025 Manoj Modified PrepareReport to include images in report
 *  venkat  12/26/2025 Made changes for handling subreports
 *  01/02/2026 Manoj   Added EnableHyperlinks for form Reports to handle report hyperlinks
 *  Dharani 01/26/2026 made changes in PrepareReport for handling width of the columns in reports.
 *  Dharani 02/03/2026 Made changes to work with core mvc.
 */

using Microsoft.Reporting.NETCore;
using YJWebCoreMVC.Services;


namespace YJWebCoreMVC.ReportEngine
{
    public class ReportService
    {
        private readonly HelperService _helperService;
        public ReportService(HelperService helperService)
        {
            _helperService = helperService;
        }
        public byte[] RenderPdf(ReportRequest request)
        {
            LocalReport report = PrepareReport(request);
            return Render(report, "PDF");
        }

        public byte[] RenderExcel(ReportRequest request)
        {
            LocalReport report = PrepareReport(request);
            return Render(report, "EXCELOPENXML");
        }

        public byte[] RenderWord(ReportRequest request)
        {
            LocalReport report = PrepareReport(request);
            return Render(report, "WORDOPENXML");
        }

        // =====================================================
        // PRIVATE: Prepare report instance
        // =====================================================
        /*private LocalReport PrepareReport(ReportRequest request)
        {
            LocalReport report = new LocalReport
            {
                ReportPath = request.ReportPath
            };

            report.DataSources.Clear();
            report.DataSources.Add(new ReportDataSource(
                request.DataSetName,
                request.Data
            ));

            if (request.Parameters != null)
                report.SetParameters(request.Parameters);

            return report;
        }*/

        private LocalReport PrepareReport(ReportRequest request)
        {
            LocalReport report = new LocalReport();

            if (!string.IsNullOrEmpty(request.ModifiedRdlcXml))
            {
                report.DataSources.Clear();
                foreach (var ds in request.DataSources)
                {
                    report.DataSources.Add(new ReportDataSource(ds.DataSetName, ds.Data));
                }
                using (StringReader sr = new StringReader(request.ModifiedRdlcXml))
                {
                    report.LoadReportDefinition(sr);
                }

                report.EnableExternalImages = request.IncludeImage;
            }
            else if (!string.IsNullOrEmpty(request.ReportPath))
            {
                report.ReportPath = request.ReportPath;
                report.EnableExternalImages = request.IncludeImage;
                report.EnableHyperlinks = _helperService.HelperManoj.HasHyperlink(request.ReportPath);
                report.DataSources.Clear();
                foreach (var ds in request.DataSources)
                {
                    report.DataSources.Add(new ReportDataSource(ds.DataSetName, ds.Data));
                }
            }
            else
            {
                throw new ArgumentException("Either ReportPath or ModifiedRdlcXml must be provided.");
            }

            if (request.Parameters != null && request.Parameters.Count > 0)
            {
                report.SetParameters(request.Parameters);
            }
            if (request.SubReports != null && request.SubReports.Any())
            {
                report.SubreportProcessing += (sender, e) =>
                {
                    var sub = request.SubReports.FirstOrDefault(x =>
                        string.Equals(Path.GetFileNameWithoutExtension(x.ReportName),
                                      Path.GetFileNameWithoutExtension(e.ReportPath),
                                      StringComparison.OrdinalIgnoreCase));

                    if (sub != null)
                    {
                        e.DataSources.Clear();
                        e.DataSources.Add(new ReportDataSource(sub.DataSetName, sub.SubData));
                    }
                };
            }

            return report;
        }

        // =====================================================
        // PRIVATE: Common Render Method
        // =====================================================
        private byte[] Render(LocalReport report, string format)
        {
            string mimeType, encoding, extension;
            Warning[] warnings;
            string[] streamIds;

            return report.Render(
                format, null,
                out mimeType,
                out encoding,
                out extension,
                out streamIds,
                out warnings
            );
        }
    }
}