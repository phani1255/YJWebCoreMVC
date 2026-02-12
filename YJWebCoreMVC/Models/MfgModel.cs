using Microsoft.AspNetCore.Mvc.Rendering;

namespace YJWebCoreMVC.Models
{
    public class MfgModel
    {
        public string setter { get; set; }
        public IEnumerable<SelectListItem> setters { get; set; }
    }

    public class PersonModel
    {
        public string NAME { get; set; }
        public bool freq_used { get; set; }
        public string DEPT { get; set; }
        public string gl_code { get; set; }
        public string accname { get; set; }
        public string LoginCode { get; set; }
        public bool InActive { get; set; }
    }
    public class DepartmentViewModel
    {
        public string dept { get; set; }
        public string hours { get; set; }
        public string STATUS { get; set; }
        public string Prev { get; set; }
    }

    public class SaveJobBagRequest
    {
        public string JobBagNo { get; set; }
        public string Person { get; set; }
        public bool IsReceivedBack { get; set; }
        public DateTime? DueDate { get; set; }
        public List<JobBagItem> JobBagItems { get; set; }
        public List<string> DeletedJobBags { get; set; }
    }

    public class JobBagItem
    {
        public string JobNo { get; set; }
        public decimal Qty { get; set; }
        public string Style { get; set; }
        public string Pon { get; set; }
        public decimal OpenQty { get; set; }
        public string Notes { get; set; }
    }

    public class JobBagHistory
    {
        public string GiveToPerson { get; set; }
        public string TakeBackFrom { get; set; }
        public string Transact { get; set; }
        public string DateGiven { get; set; }
        public string TimeGiven { get; set; }
        public string DateRcvd { get; set; }
        public string TimeRcvd { get; set; }
        public decimal QtyGiven { get; set; }
        public decimal QtyRcvd { get; set; }
        public decimal WtGiven { get; set; }
        public decimal WtRcvd { get; set; }
        public string DueDate { get; set; }
        public string Note1 { get; set; }
        public string Note2 { get; set; }
    }
    public class ReturnJobRequest
    {
        public string LogNo { get; set; }
        public string Setter { get; set; }                // person
        public DateTime? DueDate { get; set; }            // optional, if you need it
        public List<ReturnJobItem> JobItems { get; set; }
    }

    public class ReturnJobItem
    {
        public string JobNo { get; set; }
        public decimal Qty { get; set; }
        public bool AddStock { get; set; }
        public bool RsvForInvoice { get; set; }
        public bool Closed { get; set; }                  // client may ignore; server recomputes anyway
        public bool Email { get; set; }                   // optional flags coming from UI
        public bool Text { get; set; }                    // optional flags coming from UI
    }
    public class PickupResult
    {
        public int EmailSent { get; set; }
        public int TextSent { get; set; }
        public int TextFailed { get; set; }
    }
}
