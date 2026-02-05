using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data;

namespace YJWebCoreMVC.Models
{
    public class TaggingModel
    {
        public TagPrinterSetupModel TagPrinterSetup { get; set; }
    }

    public class TagPrinterSetupModel
    {
        public string PortTemplate1 { get; set; }
        public string SelectedPrinter { get; set; }
        public string PrintPort { get; set; }
        public string TemplateName { get; set; }
        public bool IsTagTemplate { get; set; } = true;
        public DataTable TemplateData { get; set; }
        public string ActionType { get; set; }
    }
    public class TagPrinterViewModel
    {
        public string PrinterName { get; set; }
        public string OldPrinterName { get; set; }
        public string PrinterPort { get; set; }
        public int? LeftMargin { get; set; }
        public int? RightMargin { get; set; }
        public int? TopMargin { get; set; }
        public int? DistanceLines { get; set; }
        public int? TopLeft { get; set; }
        public int? Font { get; set; }
        public string DefaultTemplate { get; set; }
        public bool NotSideBySide { get; set; }
        public bool Tsc { get; set; }
        public bool Citoh { get; set; }
        public bool Zebra { get; set; }
        public bool Godex { get; set; }
        public int? SecondTopOffset { get; set; }
        public int? SecondLeftOffset { get; set; }
        public int? TagLengthGodex { get; set; }
    }

    public class TagModel
    {
        public string tag_left1 { get; set; }
        public string tag_left2 { get; set; }
        public string tag_left3 { get; set; }
        public string tag_left4 { get; set; }
        public string tag_left5 { get; set; }
        public string tag_left6 { get; set; }
        public string tag_left7 { get; set; }

        public string tag_right4 { get; set; }
        public string tag_right5 { get; set; }
        public string tag_right6 { get; set; }
        public string tag_right7 { get; set; }

        public string tag_left1A { get; set; }
        public string tag_left2A { get; set; }
        public string tag_left3A { get; set; }
        public string tag_left4A { get; set; }
        public string tag_left5A { get; set; }
        public string tag_left6A { get; set; }
        public string tag_left7A { get; set; }

        public string tag_left1B { get; set; }
        public string tag_left2B { get; set; }
        public string tag_left3B { get; set; }
        public string tag_left4B { get; set; }
        public string tag_left5B { get; set; }
        public string tag_left6B { get; set; }
        public string tag_left7B { get; set; }

        public string tag_left1C { get; set; }
        public string tag_left2C { get; set; }
        public string tag_left3C { get; set; }
        public string tag_left4C { get; set; }
        public string tag_left5C { get; set; }
        public string tag_left6C { get; set; }
        public string tag_left7C { get; set; }

        public string tag_left1D { get; set; }
        public string tag_left2D { get; set; }
        public string tag_left3D { get; set; }
        public string tag_left4D { get; set; }
        public string tag_left5D { get; set; }
        public string tag_left6D { get; set; }
        public string tag_left7D { get; set; }

        public string tag_left1E { get; set; }
        public string tag_left2E { get; set; }
        public string tag_left3E { get; set; }
        public string tag_left4E { get; set; }
        public string tag_left5E { get; set; }
        public string tag_left6E { get; set; }
        public string tag_left7E { get; set; }

        public string tag_right1 { get; set; }
        public string tag_right2 { get; set; }
        public string tag_right3 { get; set; }

        public string tag_right1A { get; set; }
        public string tag_right2A { get; set; }
        public string tag_right3A { get; set; }
        public string tag_right4A { get; set; }
        public string tag_right5A { get; set; }
        public string tag_right6A { get; set; }
        public string tag_right7A { get; set; }

        public string tag_right1B { get; set; }
        public string tag_right2B { get; set; }
        public string tag_right3B { get; set; }
        public string tag_right4B { get; set; }
        public string tag_right5B { get; set; }
        public string tag_right6B { get; set; }
        public string tag_right7B { get; set; }

        public string tag_right1C { get; set; }
        public string tag_right2C { get; set; }
        public string tag_right3C { get; set; }
        public string tag_right4C { get; set; }
        public string tag_right5C { get; set; }
        public string tag_right6C { get; set; }
        public string tag_right7C { get; set; }

        public string tag_right1D { get; set; }
        public string tag_right2D { get; set; }
        public string tag_right3D { get; set; }
        public string tag_right4D { get; set; }
        public string tag_right5D { get; set; }
        public string tag_right6D { get; set; }
        public string tag_right7D { get; set; }

        public string tag_right1E { get; set; }
        public string tag_right2E { get; set; }
        public string tag_right3E { get; set; }
        public string tag_right4E { get; set; }
        public string tag_right5E { get; set; }
        public string tag_right6E { get; set; }
        public string tag_right7E { get; set; }

        public bool NoTagPrice { get; set; }
        public bool IgnoreDollerforprice { get; set; }
        public bool ignoredecimals { get; set; }

        public string Move_barcode { get; set; }

        public string CTag_Text { get; set; }
        public string CTag_Place { get; set; }
        public string CTag_Text2 { get; set; }
        public string CTag_Place2 { get; set; }
        public string CTag_Text3 { get; set; }
        public string CTag_Place3 { get; set; }
        public string CTag_Text4 { get; set; }
        public string CTag_Place4 { get; set; }

    }

    public class DiamondModel
    {
        public string tag_line1 { get; set; }
        public string tag_line2 { get; set; }
        public string tag_line3 { get; set; }
        public string tag_line4 { get; set; }
        public string tag_line5 { get; set; }
        public string tag_line1A { get; set; }
        public string tag_line2A { get; set; }
        public string tag_line3A { get; set; }
        public string tag_line4A { get; set; }
        public string tag_line5A { get; set; }
        public string tag_line1B { get; set; }
        public string tag_line2B { get; set; }
        public string tag_line3B { get; set; }
        public string tag_line4B { get; set; }
        public string tag_line5B { get; set; }
        public string tag_line1C { get; set; }
        public string tag_line2C { get; set; }
        public string tag_line3C { get; set; }
        public string tag_line4C { get; set; }
        public string tag_line5C { get; set; }
        public string tag_line1D { get; set; }
        public string tag_line2D { get; set; }
        public string tag_line3D { get; set; }
        public string tag_line4D { get; set; }
        public string tag_line5D { get; set; }
        public string tag_line1E { get; set; }
        public string tag_line2E { get; set; }
        public string tag_line3E { get; set; }
        public string tag_line4E { get; set; }
        public string tag_line5E { get; set; }
        public string fieldVal1 { get; set; }
        public string fieldVal2 { get; set; }
        public string fieldVal3 { get; set; }
        public string fieldVal4 { get; set; }
        public string fieldVal5 { get; set; }
    }

    public class AutoDescriptionTemplateModel
    {

        public string TemplateName { get; set; }

    }

    public class AutoDescModel
    {
        public string AUTO_DESC1 { get; set; }
        public string AUTO_DESC2 { get; set; }
        public string AUTO_DESC3 { get; set; }
        public string AUTO_DESC4 { get; set; }
        public string AUTO_DESC5 { get; set; }
        public string AUTO_DESC6 { get; set; }
        public string AUTO_DESC7 { get; set; }
        public string AUTO_DESC8 { get; set; }
        public string AUTO_DESC9 { get; set; }
        public string AUTO_DESC10 { get; set; }
        public string AUTO_DESC11 { get; set; }
        public string AUTO_DESC12 { get; set; }
        public string AUTO_DESC13 { get; set; }
        public string AUTO_DESC14 { get; set; }
        public string AUTO_DESC15 { get; set; }
        public string AUTO_DESC16 { get; set; }
        public string AUTO_DESC17 { get; set; }
        public string AUTO_DESC18 { get; set; }
        public string AUTO_DESC19 { get; set; }
        public string AUTO_DESC20 { get; set; }
        public string AUTO_DESC21 { get; set; }
        public string AUTO_DESC22 { get; set; }
        public string AUTO_DESC23 { get; set; }
        public string AUTO_DESC24 { get; set; }
        public string AUTO_DESC25 { get; set; }
        public string AUTO_DESC26 { get; set; }
        public string AUTO_DESC27 { get; set; }
        public string AUTO_DESC28 { get; set; }
        public string AUTO_DESC29 { get; set; }
        public string AUTO_DESC30 { get; set; }
        public string AUTO_DESC31 { get; set; }
        public string AUTO_DESC32 { get; set; }
        public string AUTO_DESC33 { get; set; }
        public string AUTO_DESC34 { get; set; }
        public string AUTO_DESC35 { get; set; }
        public string AUTO_DESC36 { get; set; }
        public string AUTO_DESC37 { get; set; }
        public string AUTO_DESC38 { get; set; }
        public string AUTO_DESC39 { get; set; }
        public string AUTO_DESC40 { get; set; }

        public string Fixeddesc1 { get; set; }
        public string Fixeddesc2 { get; set; }
        public string Fixeddesc3 { get; set; }
        public string Fixeddesc4 { get; set; }
        public string Fixeddesc5 { get; set; }
        public string Fixeddesc6 { get; set; }
        public string Fixeddesc7 { get; set; }
        public string Fixeddesc8 { get; set; }
        public string Fixeddesc9 { get; set; }
        public string Fixeddesc10 { get; set; }
        public string Fixeddesc11 { get; set; }
        public string Fixeddesc12 { get; set; }
        public string Fixeddesc13 { get; set; }
        public string Fixeddesc14 { get; set; }
        public string Fixeddesc15 { get; set; }
        public string Fixeddesc16 { get; set; }
        public string TemplateName { get; set; }
        public string desc_lineBreak { get; set; }
    }

    public class BreakPieceModel
    {
        public List<SelectListItem> StoreList { get; set; }
        public string SelectedStore { get; set; }
        public decimal CostEach { get; set; }
        public List<BreakItem> Items { get; set; } = new List<BreakItem>();

        public decimal TotalBrokenCost => Items.Sum(i => i.Qty * i.Cost);
        public decimal Balance => CostEach - TotalBrokenCost;
    }

    public class BreakItem
    {
        public string Style { get; set; }
        public decimal Qty { get; set; }
        public decimal Cost { get; set; }
    }
}
