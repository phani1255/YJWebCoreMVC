using Microsoft.AspNetCore.Mvc.Rendering;

namespace YJWebCoreMVC.Models
{
    public class SalesPOSReportsModel
    {
        public string charity { get; set; }
        public IEnumerable<SelectListItem> charities { get; set; }
    }
}
