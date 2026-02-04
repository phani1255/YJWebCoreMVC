using Microsoft.AspNetCore.Mvc.Rendering;

namespace YJWebCoreMVC.Models
{
    public class StylesModel
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
        public bool radSummaryByVendorStyle { get; set; } = false;
        public string CentSubtype { get; set; }
        public bool IncludeDiscontinued { get; set; } = false;
        public bool Replace_cost { get; set; } = false;
        public string TRAY_NO { get; set; } = "";
        public string Parts { get; set; } = "";
        public string SubParts { get; set; } = "";

        public string CustomerCode { get; set; }
        public int CustomerCodeId { get; set; }
        public IEnumerable<SelectListItem> CustomerCodes { get; set; }
        public IEnumerable<SelectListItem> CategoryTypes { get; set; }
        public string subcat { get; set; }
        public IEnumerable<SelectListItem> SubCategoryTypes { get; set; }
        public IEnumerable<SelectListItem> BrandTypes { get; set; }
        public IEnumerable<SelectListItem> MetalTypes { get; set; }
        public string vendor { get; set; }
        public IEnumerable<SelectListItem> VendorTypes { get; set; }
        public IEnumerable<SelectListItem> AllGroups { get; set; }
        public string storecode { get; set; }
        public IEnumerable<SelectListItem> AllStores { get; set; }
        public string item_type { get; set; }
        public IEnumerable<SelectListItem> AllItemTypes { get; set; }
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
        public string center_stype { get; set; }
        public IEnumerable<SelectListItem> AllCenterStypes { get; set; }
        public string cert_type { get; set; }
        public IEnumerable<SelectListItem> AllCerttypes { get; set; }
        public string StyleQulaity { get; set; }
        public IEnumerable<SelectListItem> AllStyleQulaities { get; set; }
        public string DiamondShape { get; set; }
        public IEnumerable<SelectListItem> AllDiamondShapes { get; set; }
        public string DiamondColor { get; set; }
        public IEnumerable<SelectListItem> AllDiamondColors { get; set; }
        public IEnumerable<SelectListItem> AllStoresData { get; set; }
        public IEnumerable<SelectListItem> AllMetalColors { get; set; }
        public string FingerSize { get; set; }
        public IEnumerable<SelectListItem> AllFingerSizes { get; set; }
        public string Template { get; set; }
        public IEnumerable<SelectListItem> AllTemplates { get; set; }
        public IEnumerable<SelectListItem> AllAutoDescTemplates { get; set; }
        public IEnumerable<SelectListItem> AllPrinters { get; set; }
        public string Culet { get; set; }
        public IEnumerable<SelectListItem> AllCulets { get; set; }
        public string Girdle { get; set; }
        public IEnumerable<SelectListItem> AllGirdles { get; set; }
        public string Polish { get; set; }
        public IEnumerable<SelectListItem> AllPolish { get; set; }
        public string Symmetry { get; set; }
        public IEnumerable<SelectListItem> AllSymmetry { get; set; }
        public string CutGrade { get; set; }
        public IEnumerable<SelectListItem> AllCutGrades { get; set; }
        public IEnumerable<SelectListItem> AllClarities { get; set; }
        public IEnumerable<SelectListItem> AllColors { get; set; }
        public string FlourIntensity { get; set; }
        public IEnumerable<SelectListItem> AllFlourIntensities { get; set; }
        public string GemexFire { get; set; }
        public IEnumerable<SelectListItem> AllGemexFires { get; set; }
        public string GemexSparkle { get; set; }
        public IEnumerable<SelectListItem> AllGemexSparkles { get; set; }
        public string GemexBrilliance { get; set; }
        public IEnumerable<SelectListItem> AllGemexBrilliances { get; set; }
        public string TemplateDiamond { get; set; }
        public IEnumerable<SelectListItem> AllTemplateDiamonds { get; set; }
        public string lefkcmb1 { get; set; }
        public IEnumerable<SelectListItem> AllDiamondreaders { get; set; }
        public string lefkcmb2 { get; set; }
        public IEnumerable<SelectListItem> AllColorgemreaders { get; set; }
        public string lefkcmb3 { get; set; }
        public IEnumerable<SelectListItem> AllCastingreaders { get; set; }
        public string lefkcmb4 { get; set; }
        public IEnumerable<SelectListItem> AllJewelerreaders { get; set; }
        public string lefkcmb5 { get; set; }
        public IEnumerable<SelectListItem> AllSettingreaders { get; set; }
        public string lefkcmb6 { get; set; }
        public IEnumerable<SelectListItem> AllModelreaders { get; set; }
        public string Attr1_sel { get; set; }
        public IEnumerable<SelectListItem> AllAttrib1Attributes { get; set; }
        public string Attr2_sel { get; set; }
        public IEnumerable<SelectListItem> AllAttrib2Attributes { get; set; }
        public string Attr3_sel { get; set; }
        public IEnumerable<SelectListItem> AllAttrib3Attributes { get; set; }
        public string Attr4_sel { get; set; }
        public IEnumerable<SelectListItem> AllAttrib4Attributes { get; set; }
        public string Attr5_sel { get; set; }
        public IEnumerable<SelectListItem> AllAttrib5Attributes { get; set; }
        public string Attr6_sel { get; set; }
        public IEnumerable<SelectListItem> AllAttrib6Attributes { get; set; }
        public string Attr7_sel { get; set; }
        public IEnumerable<SelectListItem> AllAttrib7Attributes { get; set; }
        public string Attr8_sel { get; set; }
        public IEnumerable<SelectListItem> AllAttrib8Attributes { get; set; }
        public string Attr9_sel { get; set; }
        public IEnumerable<SelectListItem> AllAttrib9Attributes { get; set; }
        public string Attr10_sel { get; set; }
        public IEnumerable<SelectListItem> AllAttrib10Attributes { get; set; }
        public string Attr11_sel { get; set; }
        public IEnumerable<SelectListItem> AllAttrib11Attributes { get; set; }
        public string Attr12_sel { get; set; }
        public IEnumerable<SelectListItem> AllAttrib12Attributes { get; set; }
        public string Attr19_sel { get; set; }
        public IEnumerable<SelectListItem> AllAttrib19Attributes { get; set; }
        public string Attr20_sel { get; set; }
        public IEnumerable<SelectListItem> AllAttrib20Attributes { get; set; }
        public string Attr21_sel { get; set; }
        public IEnumerable<SelectListItem> AllAttrib21Attributes { get; set; }
        public string Partner { get; set; }
        public IEnumerable<SelectListItem> AllPartners { get; set; }
        public string Finding { get; set; }
        public IEnumerable<SelectListItem> AllFindings { get; set; }
        public string SetChrgType { get; set; }
        public IEnumerable<SelectListItem> AllSetChrgTypes { get; set; }
        public string LaborChrgType { get; set; }
        public IEnumerable<SelectListItem> AllLaborChrgTypes { get; set; }
        public string Setter { get; set; }
        public IEnumerable<SelectListItem> AllSetters { get; set; }
        public IEnumerable<SalesQuotesWishlistModel> AllStyles { get; set; }
        public string Disclaimer { get; set; }
        public IEnumerable<SelectListItem> AllDisclaimers { get; set; }
        public string salesman { get; set; }
        public IEnumerable<SelectListItem> AllSalesman { get; set; }


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

        public string intchkattr1 { get; set; }
        public string intchkattr2 { get; set; }
        public string intchkattr3 { get; set; }
        public string intchkattr4 { get; set; }
        public string intchkattr5 { get; set; }
        public string intchkattr6 { get; set; }

    }



}
