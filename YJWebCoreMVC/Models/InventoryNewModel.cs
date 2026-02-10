/* Created by Dharani 02/03/2026 
 
 */

using Microsoft.AspNetCore.Mvc.Rendering;


namespace YJWebCoreMVC.Models
{
    public class InventoryNewModel
    {
        public List<SelectListItem> VendorStyleItems { get; set; }
        public List<SelectListItem> Groups { get; set; }
        public List<SelectListItem> Categories { get; set; }
        public List<SelectListItem> SubCategories { get; set; }
        public List<SelectListItem> Metals { get; set; }
        public List<SelectListItem> CenterTypes { get; set; }
        public List<SelectListItem> CenterSizes { get; set; }
        public List<SelectListItem> Sizes { get; set; }
        public List<SelectListItem> Years { get; set; }
        public List<SelectListItem> Shapes { get; set; }
        public List<SelectListItem> Colors { get; set; }
        public List<SelectListItem> Brands { get; set; }
        public List<SelectListItem> Vendors { get; set; }
        public List<SelectListItem> SubBrands { get; set; }
        public List<SelectListItem> Clarities { get; set; }

        public class Inventory
        {
            public string category { get; set; }
            public string subcategories { get; set; }
            public string metal { get; set; }
            public string vendors { get; set; }
            public string brand { get; set; }
            public string shape { get; set; }
            public string ctype { get; set; }
            public string csize { get; set; }
            public string fromstyle { get; set; }
            public string tostyle { get; set; }
            public bool selectedminstock { get; set; }
            public string printbysize { get; set; }
            public string printbysummery { get; set; }
            public bool printbysummerymodel { get; set; }
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
            public string subbrand { get; set; }
            public DateTime asofdate { get; set; }
            public string Replace_cost { get; set; }
            public string group { get; set; }
            public string strItemType { get; set; }
        }

        public class StylesAttribute
        {
            public string Attrib1 { get; set; }
            public string Attrib2 { get; set; }
            public string Attrib3 { get; set; }
            public string Attrib4 { get; set; }
            public string Attrib5 { get; set; }
            public string Attrib6 { get; set; }
            public string Attrib7 { get; set; }
            public string Attrib8 { get; set; }
            public string Attrib9 { get; set; }
            public string Attrib10 { get; set; }
            public string Attrib11 { get; set; }
            public string Attrib12 { get; set; }
            public string Attrib13 { get; set; }
            public string Attrib14 { get; set; }
            public string Attrib15 { get; set; }
            public string Attrib16 { get; set; }
            public string Attrib17 { get; set; }
            public string Attrib18 { get; set; }
            public string Attrib19 { get; set; }
            public string Attrib20 { get; set; }
            public string Attrib21 { get; set; }
            public string Attrib_13 { get; set; }  // Multi-select for Attrib13
            public string Attrib_14 { get; set; }  // Multi-select for Attrib14
            public string Attrib_15 { get; set; }  // Multi-select for Attrib15
            public string Attrib_16 { get; set; }  // Multi-select for Attrib16
            public string Attrib_17 { get; set; }  // Multi-select for Attrib17
            public string Attrib_18 { get; set; }  // Multi-select for Attrib18
            public string fieldvalue4 { get; set; }
            public string fieldvalue5 { get; set; }
            public string fieldvalue6 { get; set; }
            public string fieldvalue7 { get; set; }
            public string fieldvalue8 { get; set; }
            public int Check1 { get; set; }        // -1 (Ignore), 1 (Checked), 0 (Unchecked)
            public int Check2 { get; set; }
            public int Check3 { get; set; }
            public int Check4 { get; set; }
            public int Check5 { get; set; }
            public int Check6 { get; set; }
            public int MatchAll13_14 { get; set; } // 1 (Match All), 0 (Match Any)
            public int MatchAll15_16 { get; set; }
            public int MatchAll17_18 { get; set; }
        }

        public class StylesAttributeForMassedit
        {
            public string Attrib1 { get; set; }
            public string Attrib2 { get; set; }
            public string Attrib3 { get; set; }
            public string Attrib4 { get; set; }
            public string Attrib5 { get; set; }
            public string Attrib6 { get; set; }
            public string Attrib7 { get; set; }
            public string Attrib8 { get; set; }
            public string Attrib9 { get; set; }
            public string Attrib10 { get; set; }
            public string Attrib11 { get; set; }
            public string Attrib12 { get; set; }

            public string Attrib_13 { get; set; }
            public string Match13 { get; set; }

            public string Attrib_14 { get; set; }
            public string Match14 { get; set; }

            public string Attrib_15 { get; set; }
            public string Match15 { get; set; }

            public string Attrib_16 { get; set; }
            public string Match16 { get; set; }

            public string Attrib_17 { get; set; }
            public string Match17 { get; set; }

            public string Attrib_18 { get; set; }
            public string Match18 { get; set; }

            public string Attrib_19 { get; set; }
            public string Attrib_20 { get; set; }
            public string Attrib_21 { get; set; }
            public string fieldvalue4 { get; set; }
            public string fieldvalue5 { get; set; }
            public string fieldvalue6 { get; set; }
            public string fieldvalue7 { get; set; }
            public string fieldvalue8 { get; set; }

            public int intchkattr1 { get; set; }
            public int intchkattr2 { get; set; }
            public int intchkattr3 { get; set; }
            public int intchkattr4 { get; set; }
            public int intchkattr5 { get; set; }
            public int intchkattr6 { get; set; }
        }

        public class InventoryForMassedit
        {
            public string style { get; set; }

            public string size { get; set; }
            public string qty { get; set; }
            public string instock { get; set; }
            public string notes { get; set; }

            public string category { get; set; }
            public string subcategories { get; set; }
            public string metal { get; set; }
            public string vendors { get; set; }
            public string brand { get; set; }
            public DateTime asofdate { get; set; }
            public string shape { get; set; }
            public string ctype { get; set; }
            public string csize { get; set; }
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
            public DateTime fromdate { get; set; }
            public DateTime todate { get; set; }
            public string descriptionInclude { get; set; }
            public string COLOR { get; set; }
            public string CLARITY { get; set; }
            public bool MOUNTED { get; set; }
            public bool NONMOUNTED { get; set; }

            public decimal WtFrom { get; set; }
            public decimal WtThru { get; set; }
            public decimal StoneWtFrom { get; set; }
            public decimal StoneWtThru { get; set; }
            public bool modelSummary { get; set; }
            public string GLCLASS { get; set; }
            public string subbrand { get; set; } = "";
            public string vnd_style { get; set; } = string.Empty;
            public string group { get; set; } = "";
            public string cntStnSizeFrm { get; set; }
            public string cntStnSizeTo { get; set; }
            public string strItemType { get; set; }
            public bool IsNegative { get; set; } = false;
            public bool IsReplacementCost { get; set; } = false;
            public string fdate { get; set; } = string.Empty;
            public string tdate { get; set; } = string.Empty;

            public decimal DWtFrom { get; set; } = 0;
            public decimal DWtThru { get; set; } = 0;
            public bool QuickSearch { get; set; } = false;
            public string CASE_NO { get; set; } = "";
            public string storeno { get; set; } = "";
            public bool SummryVndrstyle { get; set; } = false;
            public string CentSubtype { get; set; }
            public bool IncludeDiscontinued { get; set; } = false;
            public bool Replace_cost { get; set; } = false;
            public string TRAY_NO { get; set; } = "";
            public string Parts { get; set; } = "";
            public string SubParts { get; set; } = "";

        }

    }
}
