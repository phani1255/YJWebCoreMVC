namespace YJWebCoreMVC.Models
{
    public class BillModel
    {

        public string Style { get; set; }
        public string VndStyle { get; set; }
        public decimal Cost { get; set; }
        public decimal Price { get; set; }
        public string Group { get; set; }
        public string Category { get; set; }
        public string Metal { get; set; }
        public string ItemType { get; set; }
        public string PartNo { get; set; }
        public string SubPart { get; set; }
        public string FingerSize { get; set; }
        public string Description { get; set; }

        public string Brand { get; set; }
        public string SubBrand { get; set; }
        public string SubCategory { get; set; }
        public string CenterSize { get; set; }
        public string CenterType { get; set; }
        public string StoneShape { get; set; }
        public decimal StoneWeight { get; set; }
        public decimal ColorWeight { get; set; }


        // Bill add new style  end

        // Bill model
        public string INV_NO { get; set; } = string.Empty;

        public string ACC { get; set; } = string.Empty;

        public decimal? AMOUNT { get; set; } = 0;

        public DateTime? DATE { get; set; } = null;

        public string VND_NO { get; set; } = string.Empty;

        public decimal? TERM { get; set; } = 0;

        public DateTime? DUE_DATE { get; set; } = null;

        public decimal? BALANCE { get; set; } = 0;

        public DateTime? ENTER_DATE { get; set; } = null;

        public bool? SFM { get; set; } = false;

        public bool? ON_QB { get; set; } = false;
        public string Store_No { get; set; } = string.Empty;
        public string PaymentTerms { get; set; } = string.Empty;
        public decimal SHIPPING_CHARGE { get; set; } = 0;
        public decimal OTHER_CHARGE { get; set; } = 0;
        public decimal Discount { get; set; } = 0;
        public string TermNote { get; set; } = string.Empty;

        public int NoOfDay1 { get; set; } = 0;
        public int NoOfDay2 { get; set; } = 0;
        public int NoOfDay3 { get; set; } = 0;

        public decimal DiscPercent1 { get; set; } = 0;
        public decimal DiscPercent2 { get; set; } = 0;
        public decimal DiscPercent3 { get; set; } = 0;
        public bool IsAutoDESC { get; set; } = false;
        public string VNDPOINVNO { get; set; } = string.Empty;
        public decimal? NOOFTERMS { get; set; } = 0;
        public decimal? TERMINTERVAL { get; set; } = 0;
        public bool IsAddChargBill { get; set; } = false;
        public string ExisAddChrgBill { get; set; } = string.Empty;
        public bool IsChkNotAddToStock { get; set; } = false;
        public bool IsGuaranteedbuy { get; set; } = false;
        public bool Isupdatecostprice { get; set; } = false;
        public bool IsAutoFillTags { get; set; } = false;
        public string Note { get; set; } = string.Empty;
        public bool IsImportAutoDesc { get; set; } = false;
        public bool IsImport { get; set; } = false;
        public bool IsUpdRepClaimPrice { get; set; } = false;
        public bool isstylenobycat { get; set; } = false;
        public bool IsJobbagBillReturn { get; set; } = false;
        public decimal Insurance { get; set; } = 0;
        public string CurrencyType { get; set; } = string.Empty;
        public decimal CurrencyRate { get; set; } = 0;
        public bool IsMultiCurr { get; set; } = false;
        public bool IsVpoBill { get; set; } = false;
        public bool IsStyleItem { get; set; } = false;
        public bool IsVpoWoStylesBill { get; set; } = false;
        public string Castordno { get; set; } = string.Empty;
        public bool IsDraft { get; set; } = false;
        public bool IsbillWoStyles { get; set; } = false;



        public class BillItemModel
        {
            public string Group { get; set; }
            public string Style { get; set; }
            public string VendorStyle { get; set; }
            public string ItemType { get; set; }
            public bool IsNewStyle { get; set; }

            public string GLClass { get; set; }
            public decimal Total { get; set; }



        }
        public class BillItems
        {


            public string GLClass { get; set; }


            public string STYLE { get; set; }
            public string GROUP { get; set; }

            public string VND_STYLE { get; set; }
            public decimal PCS { get; set; }
            public decimal COST { get; set; }
            public decimal PRICE { get; set; }

            public string Brand { get; set; }
            public string SubBrand { get; set; }


            public string Category { get; set; }
            public string SubCategory { get; set; }
            public string Metal { get; set; }

            public decimal STONE_WT { get; set; }
            public decimal Colorwt { get; set; }

            public string Center_Size { get; set; }
            public string Center_Type { get; set; }
            public string CTR_SHAPE { get; set; }

            public decimal WEIGHT { get; set; }
            public bool BY_WT { get; set; }
            public bool Image { get; set; }

            public decimal TOTAL { get; set; }

            public string Disclaimer { get; set; }
            public string ORDERNO { get; set; }
            public string VpoNo { get; set; }
            public string Desc { get; set; }

            public string INV_NO { get; set; }
            public int Line_no { get; set; }
            public string Org_Style { get; set; }
            public string Ord_Style { get; set; }
        }
        public class GLRowModel
        {
            public string BillNo { get; set; }
            public string GLCode { get; set; }
            public string StoreCode { get; set; }
            public string Description { get; set; }
            public decimal Amount { get; set; }
            public bool IsEditable { get; set; }
            public string DEPT { get; set; }
            public string TYPE { get; set; }
        }
        public class BillGL
        {
            public string INV_NO { get; set; }
            public string GL_CODE { get; set; }

            public string DESC { get; set; }
            public decimal AMOUNT { get; set; }
            public bool IsEditable { get; set; }
            public string DEPT { get; set; }

        }
        public class SpecialOrderItem
        {
            public string InvoiceNo { get; set; }
            public string CustomerName { get; set; }
            public string Vendor { get; set; }
            public string Style { get; set; }
            public string Store { get; set; }
            public string ManufacturerNo { get; set; }
            public string MetalType { get; set; }
            public string FingerSize { get; set; }
            public string Note { get; set; }
            public DateTime? DueDate { get; set; }
            public string CenterStoneDimension { get; set; }
            public decimal Price { get; set; }
            public string SalesRep { get; set; }
            public DateTime? VendorOrderDate { get; set; }
            public DateTime? ExpectedDeliveryDate { get; set; }
            public string VendorConfirmationNo { get; set; }
        }

        public class BillTemplateModel
        {
            public string Issavenewtemplate { get; set; }
            public string SaveImpTemplateName { get; set; }
            public string ddlImpTemplate { get; set; }

            public string VendorStyleNo { get; set; }
            public string OurStyleNo { get; set; }
            public string Group { get; set; }
            public string Quantity { get; set; }
            public string UnitCost { get; set; }
            public string Price { get; set; }

            public string Brand { get; set; }
            public string SubBrand { get; set; }
            public string GLClass { get; set; }
            public string Category { get; set; }
            public string SubCat { get; set; }
            public string Metal { get; set; }

            public string GoldWt { get; set; }
            public string DiaWt { get; set; }
            public string CntrSize { get; set; }
            public string CntrType { get; set; }
            public string CentShape { get; set; }
            public string Weight { get; set; }
            public string ByWt { get; set; }

            public string Orderno { get; set; }
            public string Description { get; set; }

            public string DiamQuality { get; set; }
            public string ColorWt { get; set; }
            public string ColorQuality { get; set; }

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
            public string Attrib19 { get; set; }
            public string Attrib20 { get; set; }
            public string Attrib21 { get; set; }

            public string StyleCheck1 { get; set; }
            public string StyleCheck2 { get; set; }
            public string StyleCheck3 { get; set; }
            public string StyleCheck4 { get; set; }
            public string StyleCheck5 { get; set; }
            public string StyleCheck6 { get; set; }

            public string Disclaimer { get; set; }
            public string StyleField1 { get; set; }
            public string StyleField2 { get; set; }
            public string StyleField3 { get; set; }
            public string StyleField4 { get; set; }
            public string StyleField5 { get; set; }

            public string ItemType { get; set; }
            public string CertType { get; set; }
            public string CertNo { get; set; }
            public string Shape { get; set; }
            public string Culet { get; set; }
            public string Measurements { get; set; }
            public string CertWeight { get; set; }
            public string Depth { get; set; }
            public string Table { get; set; }
            public string Girdle { get; set; }
            public string Polish { get; set; }
            public string Symmetry { get; set; }
            public string CutGrade { get; set; }
            public string Clarity { get; set; }
            public string Color { get; set; }

            public string FingerSize { get; set; }
            public string SilverWt { get; set; }
            public string PlatinumWt { get; set; }
            public string MinPrice { get; set; }

            public string TagInfo1 { get; set; }
            public string TagInfo2 { get; set; }
            public string TagInfo3 { get; set; }
            public string TagInfo4 { get; set; }

            public string PriceByWt { get; set; }
            public string Labor { get; set; }
            public string LongDesc { get; set; }

            public string FancyColor { get; set; }
            public string FancyIntensity { get; set; }
            public string FancyOvertone { get; set; }
            public string IMAGE { get; set; } = string.Empty;
            public string TOTALCOST { get; set; } = string.Empty;
            public bool IsOverWrite { get; set; }
            public int txtrowsskip { get; set; }
        }
    }
}
