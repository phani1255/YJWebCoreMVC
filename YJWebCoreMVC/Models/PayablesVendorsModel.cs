/*
 *  Dharani 02/04/2026 Updated to the core mvc.
 */


using Microsoft.AspNetCore.Mvc.Rendering;

namespace YJWebCoreMVC.Models
{
    public class PayablesVendorsModel
    {
        public List<SelectListItem> VendorStyleItems { get; set; }
        public class SoldQtyToVendorCriteria
        {
            public bool AllVendors { get; set; }
            public string VendorCode { get; set; }

            public DateTime? SoldFrom { get; set; }
            public DateTime? SoldThru { get; set; }
            public bool AllDates { get; set; }

            public bool CustomerReturnsDateRange { get; set; }
            public DateTime? RetFrom { get; set; }
            public DateTime? RetThru { get; set; }
            public bool RetAllDates { get; set; }

            public bool IncludePreviouslyReported { get; set; }
            public bool IncludeLayawaysUnpaid { get; set; }
            public bool SeparateByStore { get; set; }
            public bool PartialPaid { get; set; }
            public string Notes { get; set; }
        }
        public class VsfmRequest
        {
            public string XmlData { get; set; }
            public string FromDate { get; set; }
            public string ToDate { get; set; }
            public bool AllDates { get; set; }
            public bool IncludePrevious { get; set; }
            public bool SeparateByStore { get; set; }
        }
        public class ConsignmentSoldReportRequest
        {
            public string XmlData { get; set; }
            public string FromDate { get; set; }
            public string ToDate { get; set; }
            public bool AllDates { get; set; }
            public bool IncludePrevious { get; set; }
            public bool SeparateByStore { get; set; }
            public string Type { get; set; }
            public string Mode { get; set; }
        }
    }
}
