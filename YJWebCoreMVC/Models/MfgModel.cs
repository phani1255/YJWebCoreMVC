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
}
