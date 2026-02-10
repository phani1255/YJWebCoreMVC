/*
 * Manoj 02/04/2026 created file
 * Manoj 02/04/2026 Added charity,charities Properties
 */

using Microsoft.AspNetCore.Mvc.Rendering;


namespace YJWebCoreMVC.Models
{
    public class SalesPOSReportsModel
    {
        public string charity { get; set; }
        public IEnumerable<SelectListItem> charities { get; set; }
    }
}
