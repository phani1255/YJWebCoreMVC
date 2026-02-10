using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace YJWebCoreMVC.Models
{
    public class CommonModel
    {
        public string CustomerCode { get; set; }
        public int CustomerCodeId { get; set; }
        public IEnumerable<SelectListItem> CustomerCodes { get; set; }
        public string category { get; set; }
        public IEnumerable<SelectListItem> CategoryTypes { get; set; }
        //public DateTime {get;set;}
        [DataType(DataType.Date)]
        public DateTime? FromDate { get; set; }
        [DataType(DataType.Date)]
        public DateTime? ToDate { get; set; }
        public string subcat { get; set; }
        public IEnumerable<SelectListItem> SubCategoryTypes { get; set; }
        public string brand { get; set; }
        public IEnumerable<SelectListItem> BrandTypes { get; set; }
        public string metal { get; set; }
        public IEnumerable<SelectListItem> MetalTypes { get; set; }

        public string vender { get; set; }
        public IEnumerable<SelectListItem> VenderTypes { get; set; }
        public string group { get; set; }
        public IEnumerable<SelectListItem> AllGroups { get; set; }
        public string storecode { get; set; }
        public IEnumerable<SelectListItem> AllStores { get; set; }
        public string item_type { get; set; }
        public IEnumerable<SelectListItem> AllItemTypes { get; set; }
        public string subbrand { get; set; }
        public IEnumerable<SelectListItem> AllSubBrands { get; set; }
        public string centerstone_color { get; set; }
        public IEnumerable<SelectListItem> AllCenterStoneColor { get; set; }
        public string centerstone_clarity { get; set; }
        public IEnumerable<SelectListItem> AllCenterStoneClarity { get; set; }
        public string centerstone_shape { get; set; }
        public IEnumerable<SelectListItem> AllCenterStoneShapes { get; set; }
        public string center_type { get; set; }
        public IEnumerable<SelectListItem> AllCenterTypes { get; set; }
        public string gl_class { get; set; }
        public IEnumerable<SelectListItem> AllGLClasses { get; set; }


    }


    public class InventoryModel
    {
        public string category { get; set; }
        public string subcategories { get; set; }
        public string metal { get; set; }
        public string vendors { get; set; }
        public string brand { get; set; }
        public string subbrand { get; set; }
        public string shape { get; set; }
        public string ctype { get; set; }
        public string fromstyle { get; set; }
        public string tostyle { get; set; }
        public string selectedminstock { get; set; }
        public string printbysize { get; set; }
        public string printbysummery { get; set; }
        public string printbysummerymodel { get; set; }
        public string displaystocktype { get; set; }
        public string displayallstock { get; set; }
        public string displayincludeinstock { get; set; }
        public string displayincludesalesmaninventory { get; set; }
        public string displayitemsonmemo { get; set; }
        public string descriptionInclude { get; set; }
        public string COLOR { get; set; }
        public string CLARITY { get; set; }
        public bool MOUNTED { get; set; }
        public bool NONMOUNTED { get; set; }
        public string WtFrom { get; set; }
        public string WtThru { get; set; }
        public string GLCLASS { get; set; }
        public string group { get; set; }
        public string cntStnSizeFrm { get; set; }
        public string cntStnSizeTo { get; set; }
        public string vnd_style { get; set; }
        public string strItemType { get; set; }
        public bool IsNegative { get; set; }
        public bool IsReplacementCost { get; set; }
        public DateTime? fdate { get; set; }
        public DateTime? tdate { get; set; }
    }

    
}
