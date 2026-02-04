using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data;

namespace YJWebCoreMVC.Models
{
    public class DiamondInvModel
    {
        public string vendors { get; set; }

        public string shape { get; set; }

        public string fromstyle { get; set; }

        public string tostyle { get; set; }

        public bool? printbysize { get; set; }

        public bool? printbysummery { get; set; }

        public bool? printrapprice { get; set; }

        public string descriptionInclude { get; set; }

        public string COLOR { get; set; }

        public string CLARITY { get; set; }

        public decimal WtFrom { get; set; }

        public decimal WtThru { get; set; }

        public decimal PriceFrom { get; set; }

        public decimal PriceThru { get; set; }

        public string GLCLASS { get; set; }

        public string CERT_TYPE { get; set; }

        public string CERT_NO { get; set; }

        public string StoreNo { get; set; }

        public bool? displayitemsonmemo { get; set; }

        public bool? displayallstock { get; set; }

        public bool? displayincludeinstock { get; set; }

        public bool? IsMountedDiamond { get; set; }

        public bool? IncludeDiscontinued { get; set; }

        public List<SelectListItem> TemplateList { get; set; }
        public List<SelectListItem> PrinterList { get; set; }

        public List<SelectListItem> StoreList { get; set; }
        public string SelectedStore { get; set; }

        public string SelectedTemplate { get; set; }
        public string SelectedPrinter { get; set; }
        public DataTable StyleQtyTable { get; set; }
        public IEnumerable<SelectListItem> StyleItems { get; set; }


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
        public string StyleField1 { get; set; }
        public string StyleField2 { get; set; }
        public string StyleField3 { get; set; }
        public string StyleField4 { get; set; }
        public string StyleField5 { get; set; }
        public string StyleField20 { get; set; }
        public string StyleField21 { get; set; }
        public string StyleField22 { get; set; }
        public string StyleField23 { get; set; }
        public string StyleField24 { get; set; }
        public string StyleField25 { get; set; }
        public string StyleField26 { get; set; }
        public string StyleField27 { get; set; }
        public string StyleField28 { get; set; }
        public string StyleField29 { get; set; }
        public string StyleCheck1 { get; set; }
        public string StyleCheck2 { get; set; }
        public string StyleCheck3 { get; set; }
        public string StyleCheck4 { get; set; }
        public string StyleCheck5 { get; set; }
        public string StyleCheck6 { get; set; }
        public decimal? Style_Cost1 { get; set; } = 0;
        public decimal? Style_Cost2 { get; set; } = 0;
        public decimal? Style_Cost3 { get; set; } = 0;
        public decimal? Style_Cost4 { get; set; } = 0;
        public decimal? Style_Cost5 { get; set; } = 0;
        public decimal? Style_Cost6 { get; set; } = 0;
        public decimal? Style_Cost7 { get; set; } = 0;
        public decimal? Style_Cost8 { get; set; } = 0;
        public decimal? Style_Cost9 { get; set; } = 0;
        public decimal? Style_Cost10 { get; set; } = 0;

        public string STYLEATTRTEXT { get; set; }

        public string STYLE { get; set; }


    }
}
