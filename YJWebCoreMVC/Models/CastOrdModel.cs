using System.ComponentModel.DataAnnotations;

namespace YJWebCoreMVC.Models
{
    public class CastOrdModel
    {

        public string INV_NO { get; set; }
        //public DateTime {get;set;}
        [DataType(DataType.Date)]
        public DateTime? OrderDate { get; set; }
        public string STYLE { get; set; }
        public decimal QTY { get; set; }
        public string ACC { get; set; }
        public string PON { get; set; }
        public decimal RCVD { get; set; }
        public string NOTE { get; set; }
        public decimal EXTRA { get; set; }
        public string SIZE { get; set; }
        [DataType(DataType.Date)]
        public DateTime? DUE_DATE { get; set; }
        public string NOTE1 { get; set; }
        public string NOTE2 { get; set; }
        public string NOTE3 { get; set; }
        public decimal GOLD_PRICE { get; set; }
        public string vnd_style { get; set; }
        public string GOLD_COLR { get; set; }
        public string METAL { get; set; }
        public string STYLE_NO { get; set; }
        public decimal SELL_PRICE { get; set; }
        public decimal STONE_COST { get; set; }
        public decimal CHAIN_PRC { get; set; }
        public decimal PRICE { get; set; }
        public string PNOTE { get; set; }
        public decimal COST { get; set; }
        public string ORG_STYLE { get; set; }
        public decimal CLOSED { get; set; }
        public string HEADSHAPE { get; set; }
        public string HEADSIZE { get; set; }
        public string HEADTYPE { get; set; }
        public string QUALITY { get; set; }
        public DateTime SHIP_DATE { get; set; }
        public string METAL_STAMP { get; set; }
        public string TRADEMARK { get; set; }
        public int item_no { get; set; }
        public string INVOICE_NO { get; set; }
        public string ProcessedBy { get; set; }
        public decimal WEIGHT { get; set; }
        public decimal SHIPED { get; set; }
        public decimal Qty_Open { get; set; }
        public string ship_status { get; set; }
        public bool confirmed { get; set; }
        public string Approved { get; set; }
        public DateTime Approve_Date { get; set; }
        public string Approve_Note { get; set; }
        public string Stores { get; set; }
    }
}
