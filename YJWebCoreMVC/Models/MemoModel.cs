using Microsoft.AspNetCore.Mvc.Rendering;

namespace YJWebCoreMVC.Models
{
    public class MemoModel
    {
        public IEnumerable<SelectListItem> CustomerCodes { get; set; }
        public IEnumerable<SelectListItem> StyleItems { get; set; }
    }
}
