using Microsoft.AspNetCore.Mvc.Rendering;

namespace YJWebCoreMVC.Models
{
    public class GLAcctModel
    {
        public IEnumerable<SelectListItem> DeptDetails { get; set; }
        public IEnumerable<SelectListItem> GlCodes { get; set; }
        public IEnumerable<SelectListItem> GlAccs { get; set; }
        public IEnumerable<SelectListItem> GlDefaultAccs { get; set; }
        public string DEPT { get; set; }
        public string NAME { get; set; }

    }
}
