/*
 *  Venkat  12/11/2025 Added 
 *  Manoj   12/18/2025 Modified GenerateReport to include images in report
 *  Dharani 12/23/2025 Updated GenerateReport function
 *  Venkat  12/29/2025 Made changes for handling subreports
 *  Dharani 01/26/2026 Made changes for handling width of the columns in subreports
 *  Dharani 02/03/2026 Made changes to work with core mvc.
 */


using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Reporting.NETCore;
using YJWebCoreMVC.ReportEngine;

namespace YJWebCoreMVC.Controllers
{
    public abstract class ReportPrintingController : Controller
    {
        protected readonly ReportService _reportService;

        public ReportPrintingController(ReportService reportService)
        {
            _reportService = reportService;
        }

        protected IActionResult GenerateReport(
            string type,
            string reportPath,
            List<ReportDataSourceItem> dataSources,
            string fileName,
            bool IncludeImage = false,
            List<ReportParameter> parameters = null,
            List<SubReportRequest> subReports = null,
            string modifiedRdlcXml = null)
        {
            var req = new ReportRequest
            {
                ReportPath = reportPath,
                DataSources = dataSources,
                FileName = fileName,
                IncludeImage = IncludeImage,
                Parameters = parameters,
                SubReports = subReports,
                ModifiedRdlcXml = modifiedRdlcXml
            };

            //byte[] bytes = ReportUtility.Render(type, req);
            byte[] bytes = RenderReport(type, req);

            string contentType;
            string downloadFileName = null;

            switch (type?.ToLower())
            {
                case "pdf":
                    contentType = "application/pdf";
                    downloadFileName = fileName + ".pdf";
                    break;

                case "preview":
                    contentType = "application/pdf";
                    break;

                case "excel":
                    contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    downloadFileName = fileName + ".xlsx";
                    break;

                default:
                    contentType = "application/pdf";
                    break;
            }

            return File(bytes, contentType, downloadFileName);
        }

        private byte[] RenderReport(string type, ReportRequest request)
        {
            //var engine = new ReportService();

            switch (type?.ToLower())
            {
                case "pdf":
                case "preview":
                    return _reportService.RenderPdf(request);

                case "excel":
                    return _reportService.RenderExcel(request);

                case "word":
                    return _reportService.RenderWord(request);

                default:
                    return _reportService.RenderPdf(request);
            }
        }

        protected List<ReportParameter> BuildParams(Dictionary<string, string> dict)
        {
            var list = new List<ReportParameter>();

            if (dict != null)
            {
                foreach (var kv in dict)
                    list.Add(new ReportParameter(kv.Key, kv.Value));
            }

            return list;
        }
    }
}

