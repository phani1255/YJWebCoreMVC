/*
 * Phanindra 02/09/2026 Added all neededparameters.
 */
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace YJWebCoreMVC.Models
{
    public class CastOrdModel
    {
        // ===== Order / Invoice =====
        public string InvoiceNo { get; set; }
        public string INV_NO { get; set; }

        [DataType(DataType.Date)]
        public DateTime? OrderDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime? SaleDate { get; set; }

        public string SaleDate1 { get; set; }

        // ===== Item Details =====
        public string STYLE { get; set; }
        public string STYLE_NO { get; set; }
        public string ORG_STYLE { get; set; }
        public string vnd_style { get; set; }

        public decimal QTY { get; set; }
        public decimal Qty_Open { get; set; }
        public decimal RCVD { get; set; }
        public decimal SHIPED { get; set; }
        public decimal CLOSED { get; set; }

        public string SIZE { get; set; }
        public decimal WEIGHT { get; set; }

        // ===== Pricing =====
        public decimal Price { get; set; }
        public decimal SELL_PRICE { get; set; }
        public decimal COST { get; set; }
        public decimal GOLD_PRICE { get; set; }
        public decimal STONE_COST { get; set; }
        public decimal CHAIN_PRC { get; set; }
        public decimal EXTRA { get; set; }
        public decimal Amount { get; set; }
        public decimal TagAmount { get; set; }
        public decimal CostQty { get; set; }

        // ===== Metal / Quality =====
        public string METAL { get; set; }
        public string GOLD_COLR { get; set; }
        public string METAL_STAMP { get; set; }
        public string QUALITY { get; set; }
        public string TRADEMARK { get; set; }

        // ===== Head Details =====
        public string HEADSHAPE { get; set; }
        public string HEADSIZE { get; set; }
        public string HEADTYPE { get; set; }

        // ===== Vendor / Customer =====
        public string ACC { get; set; }
        public string PON { get; set; }
        public string Stores { get; set; }
        public string Salesman { get; set; }
        public string Register { get; set; }
        public string Style { get; set; }

        // ===== Notes =====
        public string NOTE { get; set; }
        public string NOTE1 { get; set; }
        public string NOTE2 { get; set; }
        public string NOTE3 { get; set; }
        public string PNOTE { get; set; }
        public string NoteMessage { get; set; }

        // ===== Shipping =====
        [DataType(DataType.Date)]
        public DateTime? DUE_DATE { get; set; }

        public DateTime SHIP_DATE { get; set; }
        public string ship_status { get; set; }

        // ===== Approval =====
        public bool confirmed { get; set; }
        public string Approved { get; set; }
        public DateTime Approve_Date { get; set; }
        public string Approve_Note { get; set; }
        public string ProcessedBy { get; set; }

        // ===== Billing Address =====
        public string BillingAddr_Name { get; set; }
        public string BillingAddr_Addr1 { get; set; }
        public string BillingAddr_Addr2 { get; set; }
        public string BillingAddr_City { get; set; }
        public string BillingAddr_State { get; set; }
        public string BillingAddr_Country { get; set; }
        public string BillingAddr_ZipCode { get; set; }

        // ===== Totals =====
        public string SubTotal { get; set; }
        public string salesTaxAmount { get; set; }
        public string GrandTotal { get; set; }
        public string TradeInAmount { get; set; }
        public string ShippingAmount { get; set; }
        public string PaidAmount { get; set; }
        public string BalanceDue { get; set; }

        // ===== Payment =====
        public string PaymentDate { get; set; }
        public string PaymentType { get; set; }
        public string PaymentMethod { get; set; }
        public decimal Payment { get; set; }
        public string PaymentNote { get; set; }
        public string PaymentCurrType { get; set; }
        public string PaymentCurrRate { get; set; }
        public decimal PaymentCurrAmount { get; set; }

        // ===== Misc =====
        public string Description { get; set; }
        public int item_no { get; set; }

        // ===== Collections =====
        public IEnumerable<CastOrdModel> getSalesTaxList { get; set; }
    }
}
