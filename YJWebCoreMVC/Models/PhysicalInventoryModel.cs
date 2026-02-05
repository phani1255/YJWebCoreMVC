using Microsoft.AspNetCore.Mvc.Rendering;

namespace YJWebCoreMVC.Models
{
    public class PhysicalInventoryModel
    {
        public IEnumerable<SelectListItem> AllStores { get; set; }
        public IEnumerable<SelectListItem> VenderTypes { get; set; }

        public List<SelectListItem> Categories { get; set; } = null;
        public List<SelectListItem> Brands { get; set; } = null;
        public List<SelectListItem> trayList { get; set; }
    }
}
