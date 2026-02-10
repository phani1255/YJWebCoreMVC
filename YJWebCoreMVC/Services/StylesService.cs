// venkat 02/06/2026 added new method AddAStyle()

using Microsoft.Data.SqlClient;
using System.Data;
using YJWebCoreMVC.Models;

namespace YJWebCoreMVC.Services
{
    public class StylesService
    {
        private readonly ConnectionProvider _connectionProvider;
        private readonly HelperService _helperservice;
        public StylesService(ConnectionProvider connectionProvider)
        {
            _connectionProvider = connectionProvider;
        }

        public DataSet GetStoreInvData(StylesModel inventorymodel, StylesAttribute modStyleAtr, string cstore, bool ignorein_Shop = false, bool separateSNFromModel = false, bool IsprintLongdesc = false)
        {
            DataSet dataSet = new DataSet();

            using (SqlDataAdapter SqlDataAdapter = new SqlDataAdapter())
            {
                // Create the command and set its properties
                SqlDataAdapter.SelectCommand = new SqlCommand();
                SqlDataAdapter.SelectCommand.Connection = _connectionProvider.GetConnection();
                SqlDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;

                // Assign the SQL to the command object
                SqlDataAdapter.SelectCommand.CommandText = "PRINTSTOREINVENTORYDATA";
                SqlDataAdapter.SelectCommand.CommandTimeout = 6000;

                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@CATEGORY", inventorymodel.category);
                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@SUBCATEGORY", inventorymodel.subcategories);
                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@METAL", inventorymodel.metal);
                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@VENDORS", inventorymodel.vendors);
                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@BRAND", inventorymodel.brand);
                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@SUBBRAND", inventorymodel.subbrand);
                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@SHAPE", inventorymodel.shape);
                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@CTYPE", inventorymodel.ctype);
                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@STYLEFROM", inventorymodel.fromstyle);
                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@STYLETO", inventorymodel.tostyle);
                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@BELOWMINSTOCK", inventorymodel.selectedminstock);
                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@PRINTBYSIZE", inventorymodel.printbysize);
                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@PRINTBYSUMMERY", inventorymodel.printbysummery);
                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@PRINTBYSUMMERYMODEL", inventorymodel.printbysummerymodel);
                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@STOCKTYPE", inventorymodel.displaystocktype);
                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@ALLSTOCK", inventorymodel.displayallstock);
                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@INCLUDEINSTOCK", inventorymodel.displayincludeinstock);
                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@INCLUDESALESMANINVENTORY", inventorymodel.displayincludesalesmaninventory);
                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@ITEMSONMEMO", inventorymodel.displayitemsonmemo);
                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@DESC_INCLUDE", inventorymodel.descriptionInclude);
                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@COLOR", inventorymodel.COLOR);
                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@CLARITY", inventorymodel.CLARITY);
                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@MOUNTED", inventorymodel.MOUNTED);
                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@NONMOUNTED", inventorymodel.NONMOUNTED);
                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@WtFrom", inventorymodel.WtFrom);
                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@WtThru", inventorymodel.WtThru);
                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@GLCLASS", inventorymodel.GLCLASS);
                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@SelectedStore", cstore);
                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@Group", inventorymodel.group ?? "");
                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@ignorein_Shop", ignorein_Shop);
                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@CntStnSizeFrom", inventorymodel.cntStnSizeFrm);
                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@CntStnSizeTo", inventorymodel.cntStnSizeTo);
                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@VndStyle", inventorymodel.vnd_style);
                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@strItemType", inventorymodel.strItemType ?? "");
                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@IsNegative", inventorymodel.IsNegative);
                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@IsReplacementCost", inventorymodel.IsReplacementCost);
                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@fdate", inventorymodel.fdate);
                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@tdate", inventorymodel.tdate);
                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@DWtFrom", inventorymodel.DWtFrom);
                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@DWtThru", inventorymodel.DWtThru);
                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@CASE_NO", inventorymodel.CASE_NO);
                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@CentSubType", inventorymodel.CentSubtype);
                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@IncludeDiscontinued", inventorymodel.IncludeDiscontinued);
                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@TRAY_NO", inventorymodel.TRAY_NO);
                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@PartS", inventorymodel.Parts);
                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@SubPart", inventorymodel.SubParts);

                if (modStyleAtr != null)
                {
                    SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@Attrib1", modStyleAtr.Attrib1);
                    SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@Attrib2", modStyleAtr.Attrib2);
                    SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@Attrib3", modStyleAtr.Attrib3);
                    SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@Attrib4", modStyleAtr.Attrib4);
                    SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@Attrib5", modStyleAtr.Attrib5);
                    SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@Attrib6", modStyleAtr.Attrib6);
                    SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@Attrib7", modStyleAtr.Attrib7);
                    SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@Attrib8", modStyleAtr.Attrib8);
                    SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@Attrib9", modStyleAtr.Attrib9);
                    SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@Attrib10", modStyleAtr.Attrib10);
                    SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@Attrib11", modStyleAtr.Attrib11);
                    SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@Attrib12", modStyleAtr.Attrib12);

                    SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@Attrib_13", modStyleAtr.Attrib_13);
                    SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@Match_13", modStyleAtr.Match13);

                    SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@Attrib_14", modStyleAtr.Attrib_14);
                    SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@Match_14", modStyleAtr.Match14);

                    SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@Attrib_15", modStyleAtr.Attrib_15);
                    SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@Match_15", modStyleAtr.Match15);

                    SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@Attrib_16", modStyleAtr.Attrib_16);
                    SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@Match_16", modStyleAtr.Match16);

                    SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@Attrib_17", modStyleAtr.Attrib_17);
                    SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@Match_17", modStyleAtr.Match17);

                    SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@Attrib_18", modStyleAtr.Attrib_18);
                    SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@Match_18", modStyleAtr.Match18);

                    SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@Attrib_19", modStyleAtr.Attrib_19);
                    SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@Attrib_20", modStyleAtr.Attrib_20);
                    SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@Attrib_21", modStyleAtr.Attrib_21);

                    SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@fieldvalue4", modStyleAtr.fieldvalue4);
                    SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@fieldvalue5", modStyleAtr.fieldvalue5);

                    SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@fieldvalue6", modStyleAtr.fieldvalue6);
                    SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@fieldvalue7", modStyleAtr.fieldvalue7);
                    SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@fieldvalue8", modStyleAtr.fieldvalue8);

                    SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@intchkattr1", modStyleAtr.intchkattr1);
                    SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@intchkattr2", modStyleAtr.intchkattr2);
                    SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@intchkattr3", modStyleAtr.intchkattr3);
                    SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@intchkattr4", modStyleAtr.intchkattr4);
                    SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@intchkattr5", modStyleAtr.intchkattr5);
                    SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@intchkattr6", modStyleAtr.intchkattr6);
                }
                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@SeparateSNFromModel", separateSNFromModel);
                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@longdesc", IsprintLongdesc);
                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@QuickSearch", inventorymodel.QuickSearch);
                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@Storecodeforinstock", inventorymodel.storeno);
                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@SummryVndrstyle", inventorymodel.radSummaryByVendorStyle);

                SqlDataAdapter.Fill(dataSet);
            }
            return dataSet;
        }

        private object CheckForNull(object value)
        {
            return value ?? DBNull.Value;
        }

        public bool AddAStyle(DiamondInventoryModel diamondinvModel, string accode, string LOGGEDUSER, string StoreCodeInUse, string stonedata, DataTable Moldhistory, out string error, string stoneMaterial = "", string stylesCase = "", string stonedatabom = "")
        {
            error = string.Empty;
            using (SqlConnection dbConnection = new SqlConnection(_connectionProvider.GetConnectionString()))
            using (SqlCommand dbCommand = new SqlCommand("AddOrEditStyle", dbConnection))
            {

                dbCommand.CommandType = CommandType.StoredProcedure;
                dbCommand.CommandText = "AddOrEditStyle";
                dbCommand.Parameters.AddWithValue("@STYLE", CheckForNull((object)diamondinvModel.STYLE));
                dbCommand.Parameters.AddWithValue("@DATE", CheckForNull((object)diamondinvModel.DATE));
                dbCommand.Parameters.AddWithValue("@DESC", CheckForNull((object)diamondinvModel.DESC));
                dbCommand.Parameters.AddWithValue("@PIECE", CheckForNull((object)diamondinvModel.PIECE));
                dbCommand.Parameters.AddWithValue("@LONGDESC", CheckForNull((object)diamondinvModel.LONGDESC));
                dbCommand.Parameters.AddWithValue("@PRICE", CheckForNull((object)diamondinvModel.PRICE));
                dbCommand.Parameters.AddWithValue("@UPC", CheckForNull((object)diamondinvModel.UPC));
                dbCommand.Parameters.AddWithValue("@brand", CheckForNull((object)diamondinvModel.brand));
                dbCommand.Parameters.AddWithValue("@CATEGORY", CheckForNull((object)diamondinvModel.CATEGORY));
                dbCommand.Parameters.AddWithValue("@SUBCAT", CheckForNull((object)diamondinvModel.SUBCAT));
                dbCommand.Parameters.AddWithValue("@METAL", CheckForNull((object)diamondinvModel.METAL));
                dbCommand.Parameters.AddWithValue("@METALCOLOR", CheckForNull((object)diamondinvModel.METALCOLOR));
                dbCommand.Parameters.AddWithValue("@VND_STYLE", CheckForNull((object)diamondinvModel.VND_STYLE));
                dbCommand.Parameters.AddWithValue("@CAST_NAME", CheckForNull((object)diamondinvModel.CAST_NAME));
                dbCommand.Parameters.AddWithValue("@CAST_CODE", CheckForNull((object)diamondinvModel.CAST_CODE));
                dbCommand.Parameters.AddWithValue("@ROYAL_PERSON", CheckForNull((object)diamondinvModel.ROYAL_PERSON));
                dbCommand.Parameters.AddWithValue("@ROYALTY", CheckForNull((object)diamondinvModel.ROYALTY));
                dbCommand.Parameters.AddWithValue("@center_type", CheckForNull((object)diamondinvModel.center_type));
                dbCommand.Parameters.AddWithValue("@SHAPE", CheckForNull((object)diamondinvModel.SHAPE));
                dbCommand.Parameters.AddWithValue("@center_size", CheckForNull((object)diamondinvModel.center_size));
                dbCommand.Parameters.AddWithValue("@GOLD_WT", CheckForNull((object)diamondinvModel.GOLD_WT));
                dbCommand.Parameters.AddWithValue("@Cost_Gram", CheckForNull((object)diamondinvModel.COST_GRAM));
                dbCommand.Parameters.AddWithValue("@Cost_Gram1", CheckForNull((object)diamondinvModel.COST_GRAM1));
                dbCommand.Parameters.AddWithValue("@IS_DWT", diamondinvModel.IS_DWT == true ? 1 : 0);
                dbCommand.Parameters.AddWithValue("@LABOR_GR", CheckForNull((object)diamondinvModel.LABOR_GR));
                dbCommand.Parameters.AddWithValue("@GOLDBASE", CheckForNull((object)diamondinvModel.GOLDBASE));
                dbCommand.Parameters.AddWithValue("@COSTPER", CheckForNull((object)diamondinvModel.COSTPER));
                dbCommand.Parameters.AddWithValue("@SLVR_WT", CheckForNull((object)diamondinvModel.SLVR_WT));
                dbCommand.Parameters.AddWithValue("@SLVR_PRICE", CheckForNull((object)diamondinvModel.SLVR_PRICE));
                dbCommand.Parameters.AddWithValue("@Silvper", CheckForNull((object)diamondinvModel.Silvper));
                dbCommand.Parameters.AddWithValue("@Plat_wt", CheckForNull((object)diamondinvModel.Plat_wt));
                dbCommand.Parameters.AddWithValue("@Plat_price", CheckForNull((object)diamondinvModel.Plat_price));
                dbCommand.Parameters.AddWithValue("@Platper", CheckForNull((object)diamondinvModel.Platper));
                dbCommand.Parameters.AddWithValue("@MIN_STOK", CheckForNull((object)diamondinvModel.MIN_STOK));
                dbCommand.Parameters.AddWithValue("@IN_STOCK", CheckForNull((object)diamondinvModel.IN_STOCK));
                dbCommand.Parameters.AddWithValue("@WT_STOCK", CheckForNull((object)diamondinvModel.WT_STOCK));
                dbCommand.Parameters.AddWithValue("@NOTE", CheckForNull((object)diamondinvModel.NOTE));
                dbCommand.Parameters.AddWithValue("@NOTE1", CheckForNull((object)diamondinvModel.NOTE1));
                dbCommand.Parameters.AddWithValue("@OVER_WT", diamondinvModel.OVER_WT == true ? 1 : 0);
                dbCommand.Parameters.AddWithValue("@STONE_WT", CheckForNull((object)diamondinvModel.STONE_WT));
                dbCommand.Parameters.AddWithValue("@DQLTY", CheckForNull((object)diamondinvModel.DQLTY));
                dbCommand.Parameters.AddWithValue("@COLOR_WT", CheckForNull((object)diamondinvModel.COLOR_WT));
                dbCommand.Parameters.AddWithValue("@CQLTY", CheckForNull((object)diamondinvModel.CQLTY));
                dbCommand.Parameters.AddWithValue("@tag_info1", CheckForNull((object)diamondinvModel.tag_info1));
                dbCommand.Parameters.AddWithValue("@tag_info2", CheckForNull((object)diamondinvModel.tag_info2));
                dbCommand.Parameters.AddWithValue("@tag_info3", CheckForNull((object)diamondinvModel.tag_info3));
                dbCommand.Parameters.AddWithValue("@CASTING", CheckForNull((object)diamondinvModel.CASTING));
                dbCommand.Parameters.AddWithValue("@SETTING", CheckForNull((object)diamondinvModel.SETTING));
                dbCommand.Parameters.AddWithValue("@ROD_CHRG", CheckForNull((object)diamondinvModel.ROD_CHRG));
                dbCommand.Parameters.AddWithValue("@MISC", CheckForNull((object)diamondinvModel.MISC));
                dbCommand.Parameters.AddWithValue("@POLISH", CheckForNull((object)diamondinvModel.POLISH));
                dbCommand.Parameters.AddWithValue("@MISC1", CheckForNull((object)diamondinvModel.MISC1));
                dbCommand.Parameters.AddWithValue("@LASER", CheckForNull((object)diamondinvModel.LASER));
                dbCommand.Parameters.AddWithValue("@PCOMPLETE", diamondinvModel.PCOMPLETE == true ? 1 : 0);
                dbCommand.Parameters.AddWithValue("@IsPriceTaken", diamondinvModel.IsPriceTaken == true ? 1 : 0);
                dbCommand.Parameters.AddWithValue("@COST", CheckForNull((object)diamondinvModel.COST));
                dbCommand.Parameters.AddWithValue("@CopyFromStyle", CheckForNull((object)diamondinvModel.CopyFromStyle));
                dbCommand.Parameters.AddWithValue("@DUTIES", CheckForNull((object)diamondinvModel.DUTIES));

                dbCommand.Parameters.AddWithValue("@Supplier_Cost", diamondinvModel.Supplier_Cost);
                dbCommand.Parameters.AddWithValue("@VAT_Amount", diamondinvModel.VAT_Amount);
                dbCommand.Parameters.AddWithValue("@Margin", diamondinvModel.Margin);
                dbCommand.Parameters.AddWithValue("@Conversion_Rate", diamondinvModel.Conversion_Rate);
                dbCommand.Parameters.AddWithValue("@SalesTax_Rate", diamondinvModel.SalesTax_Rate);

                dbCommand.Parameters.AddWithValue("@T_COST", CheckForNull((object)diamondinvModel.T_COST));
                dbCommand.Parameters.AddWithValue("@CC_FEE", CheckForNull((object)diamondinvModel.CC_FEE));
                dbCommand.Parameters.AddWithValue("@Silver_Fee", CheckForNull((object)diamondinvModel.Silver_Fee));
                dbCommand.Parameters.AddWithValue("@MULTI", CheckForNull((object)diamondinvModel.MULTI));
                dbCommand.Parameters.AddWithValue("@FIND1", CheckForNull((object)diamondinvModel.FIND1));
                dbCommand.Parameters.AddWithValue("@FIND2", CheckForNull((object)diamondinvModel.FIND2));
                dbCommand.Parameters.AddWithValue("@FIND3", CheckForNull((object)diamondinvModel.FIND3));
                dbCommand.Parameters.AddWithValue("@FIND_QTY1", CheckForNull((object)diamondinvModel.FIND_QTY1));
                dbCommand.Parameters.AddWithValue("@FIND_QTY2", CheckForNull((object)diamondinvModel.FIND_QTY2));
                dbCommand.Parameters.AddWithValue("@FIND_QTY3", CheckForNull((object)diamondinvModel.FIND_QTY3));
                dbCommand.Parameters.AddWithValue("@FIND_COST", CheckForNull((object)diamondinvModel.FIND_COST));
                dbCommand.Parameters.AddWithValue("@SET_TYPE1", CheckForNull((object)diamondinvModel.SET_TYPE1));
                dbCommand.Parameters.AddWithValue("@SET1", CheckForNull((object)diamondinvModel.SET1));
                dbCommand.Parameters.AddWithValue("@SET_COST1", CheckForNull((object)diamondinvModel.SET_COST1));
                dbCommand.Parameters.AddWithValue("@SET_TYPE2", CheckForNull((object)diamondinvModel.SET_TYPE2));
                dbCommand.Parameters.AddWithValue("@SET2", CheckForNull((object)diamondinvModel.SET2));
                dbCommand.Parameters.AddWithValue("@SET_COST2", CheckForNull((object)diamondinvModel.SET_COST2));
                dbCommand.Parameters.AddWithValue("@SET_TYPE3", CheckForNull((object)diamondinvModel.SET_TYPE3));
                dbCommand.Parameters.AddWithValue("@SET3", CheckForNull((object)diamondinvModel.SET3));
                dbCommand.Parameters.AddWithValue("@SET_COST3", CheckForNull((object)diamondinvModel.SET_COST3));
                dbCommand.Parameters.AddWithValue("@SET_TYPE4", CheckForNull((object)diamondinvModel.SET_TYPE4));
                dbCommand.Parameters.AddWithValue("@SET4", CheckForNull((object)diamondinvModel.SET4));
                dbCommand.Parameters.AddWithValue("@SET_COST4", CheckForNull((object)diamondinvModel.SET_COST4));
                dbCommand.Parameters.AddWithValue("@SET_TYPE5", CheckForNull((object)diamondinvModel.SET_TYPE5));
                dbCommand.Parameters.AddWithValue("@SET5", CheckForNull((object)diamondinvModel.SET5));
                dbCommand.Parameters.AddWithValue("@SET_COST5", CheckForNull((object)diamondinvModel.SET_COST5));
                dbCommand.Parameters.AddWithValue("@SET_TYPE6", CheckForNull((object)diamondinvModel.SET_TYPE6));
                dbCommand.Parameters.AddWithValue("@SET6", CheckForNull((object)diamondinvModel.SET6));
                dbCommand.Parameters.AddWithValue("@SET_COST6", CheckForNull((object)diamondinvModel.SET_COST6));
                dbCommand.Parameters.AddWithValue("@SET_TYPE7", CheckForNull((object)diamondinvModel.SET_TYPE7));
                dbCommand.Parameters.AddWithValue("@SET7", CheckForNull((object)diamondinvModel.SET7));
                dbCommand.Parameters.AddWithValue("@SET_COST7", CheckForNull((object)diamondinvModel.SET_COST7));
                dbCommand.Parameters.AddWithValue("@SET_TYPE8", CheckForNull((object)diamondinvModel.SET_TYPE8));
                dbCommand.Parameters.AddWithValue("@SET8", CheckForNull((object)diamondinvModel.SET8));
                dbCommand.Parameters.AddWithValue("@SET_COST8", CheckForNull((object)diamondinvModel.SET_COST8));
                dbCommand.Parameters.AddWithValue("@Attrib1", CheckForNull((object)diamondinvModel.Attrib1));
                dbCommand.Parameters.AddWithValue("@Attrib2", CheckForNull((object)diamondinvModel.Attrib2));
                dbCommand.Parameters.AddWithValue("@Attrib3", CheckForNull((object)diamondinvModel.Attrib3));
                dbCommand.Parameters.AddWithValue("@Attrib4", CheckForNull((object)diamondinvModel.Attrib4));
                dbCommand.Parameters.AddWithValue("@Attrib5", CheckForNull((object)diamondinvModel.Attrib5));
                dbCommand.Parameters.AddWithValue("@Attrib6", CheckForNull((object)diamondinvModel.Attrib6));
                dbCommand.Parameters.AddWithValue("@Attrib7", CheckForNull((object)diamondinvModel.Attrib7));
                dbCommand.Parameters.AddWithValue("@Attrib8", CheckForNull((object)diamondinvModel.Attrib8));
                dbCommand.Parameters.AddWithValue("@Attrib9", CheckForNull((object)diamondinvModel.Attrib9));
                dbCommand.Parameters.AddWithValue("@Attrib10", CheckForNull((object)diamondinvModel.Attrib10));
                dbCommand.Parameters.AddWithValue("@Attrib11", CheckForNull((object)diamondinvModel.Attrib11));
                dbCommand.Parameters.AddWithValue("@Attrib12", CheckForNull((object)diamondinvModel.Attrib12));
                dbCommand.Parameters.AddWithValue("@Attrib13", CheckForNull((object)diamondinvModel.Attrib13));
                dbCommand.Parameters.AddWithValue("@Attrib14", CheckForNull((object)diamondinvModel.Attrib14));
                dbCommand.Parameters.AddWithValue("@Attrib15", CheckForNull((object)diamondinvModel.Attrib15));
                dbCommand.Parameters.AddWithValue("@Attrib16", CheckForNull((object)diamondinvModel.Attrib16));
                dbCommand.Parameters.AddWithValue("@Attrib17", CheckForNull((object)diamondinvModel.Attrib17));
                dbCommand.Parameters.AddWithValue("@Attrib18", CheckForNull((object)diamondinvModel.Attrib18));
                dbCommand.Parameters.AddWithValue("@Attrib19", CheckForNull((object)diamondinvModel.Attrib19));
                dbCommand.Parameters.AddWithValue("@Attrib20", CheckForNull((object)diamondinvModel.Attrib20));
                dbCommand.Parameters.AddWithValue("@Attrib21", CheckForNull((object)diamondinvModel.Attrib21));
                dbCommand.Parameters.AddWithValue("@COLOR_COST", CheckForNull((object)diamondinvModel.COLOR_COST));
                dbCommand.Parameters.AddWithValue("@DIA_COST", CheckForNull((object)diamondinvModel.DIA_COST));
                dbCommand.Parameters.AddWithValue("@GOLD_COST", CheckForNull((object)diamondinvModel.GOLD_COST));
                dbCommand.Parameters.AddWithValue("@GEM_COST", CheckForNull((object)diamondinvModel.GEM_COST));
                dbCommand.Parameters.AddWithValue("@RHODIUM", CheckForNull((object)diamondinvModel.RHODIUM));
                dbCommand.Parameters.AddWithValue("@STYLESIZE", CheckForNull((object)diamondinvModel.SIZE));
                dbCommand.Parameters.AddWithValue("@IS_ACTIVE", CheckForNull((object)diamondinvModel.IS_ACTIVE));
                dbCommand.Parameters.AddWithValue("@FIELDTEXT1", CheckForNull((object)diamondinvModel.FIELDTEXT1));
                dbCommand.Parameters.AddWithValue("@FIELDTEXT2", CheckForNull((object)diamondinvModel.FIELDTEXT2));
                dbCommand.Parameters.AddWithValue("@FIELDTEXT3", CheckForNull((object)diamondinvModel.FIELDTEXT3));
                dbCommand.Parameters.AddWithValue("@FIELDVALUE1", CheckForNull((object)diamondinvModel.FIELDVALUE1));
                dbCommand.Parameters.AddWithValue("@FIELDVALUE2", CheckForNull((object)diamondinvModel.FIELDVALUE2));
                dbCommand.Parameters.AddWithValue("@FIELDVALUE3", CheckForNull((object)diamondinvModel.FIELDVALUE3));
                dbCommand.Parameters.AddWithValue("@FIELDVALUE4", CheckForNull((object)diamondinvModel.FIELDVALUE4));
                dbCommand.Parameters.AddWithValue("@FIELDVALUE5", CheckForNull((object)diamondinvModel.FIELDVALUE5));
                dbCommand.Parameters.AddWithValue("@FIELDVALUE20", CheckForNull((object)diamondinvModel.FIELDVALUE20));
                dbCommand.Parameters.AddWithValue("@FIELDVALUE21", CheckForNull((object)diamondinvModel.FIELDVALUE21));
                dbCommand.Parameters.AddWithValue("@FIELDVALUE22", CheckForNull((object)diamondinvModel.FIELDVALUE22));
                dbCommand.Parameters.AddWithValue("@FIELDVALUE23", CheckForNull((object)diamondinvModel.FIELDVALUE23));
                dbCommand.Parameters.AddWithValue("@FIELDVALUE24", CheckForNull((object)diamondinvModel.FIELDVALUE24));
                dbCommand.Parameters.AddWithValue("@FIELDVALUE25", CheckForNull((object)diamondinvModel.FIELDVALUE25));
                dbCommand.Parameters.AddWithValue("@FIELDVALUE26", CheckForNull((object)diamondinvModel.FIELDVALUE26));
                dbCommand.Parameters.AddWithValue("@FIELDVALUE27", CheckForNull((object)diamondinvModel.FIELDVALUE27));
                dbCommand.Parameters.AddWithValue("@FIELDVALUE28", CheckForNull((object)diamondinvModel.FIELDVALUE28));
                dbCommand.Parameters.AddWithValue("@FIELDVALUE29", CheckForNull((object)diamondinvModel.FIELDVALUE29));
                dbCommand.Parameters.AddWithValue("@IMG_STYLE", CheckForNull((object)diamondinvModel.IMG_STYLE));
                dbCommand.Parameters.AddWithValue("@WEBPRICE", CheckForNull((object)diamondinvModel.WEB_PRICE));

                dbCommand.Parameters.AddWithValue("@ACTION", CheckForNull((object)accode));
                dbCommand.Parameters.AddWithValue("@LOGGEDUSER", CheckForNull((object)LOGGEDUSER));
                dbCommand.Parameters.AddWithValue("@StoreCodeInUse", CheckForNull((object)StoreCodeInUse));
                dbCommand.Parameters.AddWithValue("@TBLSTONESMATERIAL", CheckForNull((object)stoneMaterial));
                dbCommand.Parameters.AddWithValue("@POPUPNOTE", CheckForNull((object)diamondinvModel.POPUPNOTE));

                dbCommand.Parameters.AddWithValue("@tag_info4", CheckForNull((object)diamondinvModel.tag_info4));
                dbCommand.Parameters.AddWithValue("@IS_DWT_POT", diamondinvModel.IS_DWT_POT == true ? 1 : 0);
                dbCommand.Parameters.AddWithValue("@IS_CWT_POT", diamondinvModel.IS_CWT_POT == true ? 1 : 0);
                dbCommand.Parameters.AddWithValue("@LNM", 0);
                dbCommand.Parameters.AddWithValue("@LABOR1", CheckForNull((object)diamondinvModel.LABOR_TYPE1));
                dbCommand.Parameters.AddWithValue("@LABOR_QTY1", CheckForNull((object)diamondinvModel.LABOR_QTY1));
                dbCommand.Parameters.AddWithValue("@LABOR_COST1", CheckForNull((object)diamondinvModel.LABOR_COST1));
                dbCommand.Parameters.AddWithValue("@LABOR2", CheckForNull((object)diamondinvModel.LABOR_TYPE2));
                dbCommand.Parameters.AddWithValue("@LABOR_QTY2", CheckForNull((object)diamondinvModel.LABOR_QTY2));
                dbCommand.Parameters.AddWithValue("@LABOR_COST2", CheckForNull((object)diamondinvModel.LABOR_COST2));
                dbCommand.Parameters.AddWithValue("@LABOR3", CheckForNull((object)diamondinvModel.LABOR_TYPE3));
                dbCommand.Parameters.AddWithValue("@LABOR_QTY3", CheckForNull((object)diamondinvModel.LABOR_QTY3));
                dbCommand.Parameters.AddWithValue("@LABOR_COST3", CheckForNull((object)diamondinvModel.LABOR_COST3));
                dbCommand.Parameters.AddWithValue("@LABOR4", CheckForNull((object)diamondinvModel.LABOR_TYPE4));
                dbCommand.Parameters.AddWithValue("@LABOR_QTY4", CheckForNull((object)diamondinvModel.LABOR_QTY4));
                dbCommand.Parameters.AddWithValue("@LABOR_COST4", CheckForNull((object)diamondinvModel.LABOR_COST4));

                dbCommand.Parameters.AddWithValue("@COLOR", CheckForNull((object)diamondinvModel.COLOR));
                dbCommand.Parameters.AddWithValue("@QUALITY", CheckForNull((object)diamondinvModel.QUALITY));
                dbCommand.Parameters.AddWithValue("@COLOR_TO", CheckForNull((object)diamondinvModel.COLOR_TO));
                dbCommand.Parameters.AddWithValue("@QUALITY_TO", CheckForNull((object)diamondinvModel.QUALITY_TO));
                dbCommand.Parameters.AddWithValue("@CT_WEIGHT", CheckForNull((object)diamondinvModel.CT_WEIGHT));
                dbCommand.Parameters.AddWithValue("@MOUNTED", diamondinvModel.MOUNTED == true ? 1 : 0);
                dbCommand.Parameters.AddWithValue("@ACCENT", 0);
                dbCommand.Parameters.AddWithValue("@FIELDVALUE6", CheckForNull((object)diamondinvModel.FIELDVALUE6));
                dbCommand.Parameters.AddWithValue("@FIELDVALUE7", CheckForNull((object)diamondinvModel.FIELDVALUE7));
                dbCommand.Parameters.AddWithValue("@FIELDVALUE8", CheckForNull((object)diamondinvModel.FIELDVALUE8));
                dbCommand.Parameters.AddWithValue("@SUBBRAND", CheckForNull((object)diamondinvModel.SUBBRAND));
                dbCommand.Parameters.AddWithValue("@ITEM_TYPE", CheckForNull((object)diamondinvModel.ITEM_TYPE));
                dbCommand.Parameters.AddWithValue("@CASE_NO", CheckForNull((object)diamondinvModel.CASE_NO));
                dbCommand.Parameters.AddWithValue("@GROUPS", CheckForNull((object)diamondinvModel.GROUPS));
                dbCommand.Parameters.AddWithValue("@CLASS_GL", CheckForNull((object)diamondinvModel.CLASS_GL));
                dbCommand.Parameters.AddWithValue("@AUTO_DESC", diamondinvModel.AUTO_DESC == true ? 1 : 0);
                dbCommand.Parameters.AddWithValue("@AUTO_DESCTEMPLATE", diamondinvModel.AUTO_DESCTEMPLATE);
                dbCommand.Parameters.AddWithValue("@Discountinued", diamondinvModel.Discountinued == true ? 1 : 0);

                dbCommand.Parameters.AddWithValue("@CERT_TYPE", CheckForNull((object)diamondinvModel.CERT_TYPE));
                dbCommand.Parameters.AddWithValue("@STN_TYPE", CheckForNull((object)diamondinvModel.STN_TYPE));
                dbCommand.Parameters.AddWithValue("@CERT_NO", CheckForNull((object)diamondinvModel.CERT_NO));
                dbCommand.Parameters.AddWithValue("@CT_CUT", CheckForNull((object)diamondinvModel.CT_CUT));
                dbCommand.Parameters.AddWithValue("@CT_DEPTH", CheckForNull((object)diamondinvModel.CT_DEPTH));
                dbCommand.Parameters.AddWithValue("@CT_TABLE", CheckForNull((object)diamondinvModel.CT_TABLE));
                dbCommand.Parameters.AddWithValue("@CT_GIRDLE", CheckForNull((object)diamondinvModel.CT_GIRDLE));
                dbCommand.Parameters.AddWithValue("@CT_POLISH", CheckForNull((object)diamondinvModel.CT_POLISH));
                dbCommand.Parameters.AddWithValue("@SYMMETRY", CheckForNull((object)diamondinvModel.SYMMETRY));
                dbCommand.Parameters.AddWithValue("@CT_GRADE", CheckForNull((object)diamondinvModel.GRADE));
                dbCommand.Parameters.AddWithValue("@FLOUR", CheckForNull((object)diamondinvModel.FLOUR));
                dbCommand.Parameters.AddWithValue("@CT_NOTE", CheckForNull((object)diamondinvModel.CT_NOTE));
                dbCommand.Parameters.AddWithValue("@RAPPRICE", CheckForNull((object)diamondinvModel.RAPPRICE));
                dbCommand.Parameters.AddWithValue("@RAP_DISC", CheckForNull((object)diamondinvModel.RAP_DISC));
                dbCommand.Parameters.AddWithValue("@FAN_CLR", CheckForNull((object)diamondinvModel.FAN_CLR));
                dbCommand.Parameters.AddWithValue("@FAN_INT", CheckForNull((object)diamondinvModel.FAN_INT));
                dbCommand.Parameters.AddWithValue("@FAN_OVER", CheckForNull((object)diamondinvModel.FAN_OVER));
                dbCommand.Parameters.AddWithValue("@GEMEX_CERT", CheckForNull((object)diamondinvModel.GEMEX_CERT));
                dbCommand.Parameters.AddWithValue("@GEMEX_FIRE", CheckForNull((object)diamondinvModel.GEMEX_FIRE));
                dbCommand.Parameters.AddWithValue("@GEMEX_SPARKLE", CheckForNull((object)diamondinvModel.GEMEX_SPARKLE));
                dbCommand.Parameters.AddWithValue("@GEMEX_BRILLIANCE", CheckForNull((object)diamondinvModel.GEMEX_BRILLIANCE));
                dbCommand.Parameters.AddWithValue("@ON_RAP", diamondinvModel.ON_RAP == true ? 1 : 0);
                dbCommand.Parameters.AddWithValue("@FINGERSIZE", CheckForNull((object)diamondinvModel.FINGERSIZE));
                dbCommand.Parameters.AddWithValue("@TOBEDELETE", diamondinvModel.TOBEDELETE == true ? 1 : 0);
                dbCommand.Parameters.AddWithValue("@ATTR_CHECK1", diamondinvModel.ATTR_CHECK1 == true ? 1 : 0);
                dbCommand.Parameters.AddWithValue("@ATTR_CHECK2", diamondinvModel.ATTR_CHECK2 == true ? 1 : 0);
                dbCommand.Parameters.AddWithValue("@ATTR_CHECK3", diamondinvModel.ATTR_CHECK3 == true ? 1 : 0);
                dbCommand.Parameters.AddWithValue("@ATTR_CHECK4", diamondinvModel.ATTR_CHECK4 == true ? 1 : 0);
                dbCommand.Parameters.AddWithValue("@ATTR_CHECK5", diamondinvModel.ATTR_CHECK5 == true ? 1 : 0);
                dbCommand.Parameters.AddWithValue("@ATTR_CHECK6", diamondinvModel.ATTR_CHECK6 == true ? 1 : 0);

                dbCommand.Parameters.AddWithValue("@NOT_STOCK", diamondinvModel.NOT_STOCK == true ? 1 : 0);
                dbCommand.Parameters.AddWithValue("@NO_TAX", diamondinvModel.no_tax == true ? 1 : 0);
                dbCommand.Parameters.AddWithValue("@BULLION", diamondinvModel.bullion == true ? 1 : 0);
                dbCommand.Parameters.AddWithValue("@YEAR", CheckForNull((object)diamondinvModel.year));
                dbCommand.Parameters.AddWithValue("@BULLIONSIZE", CheckForNull((object)diamondinvModel.bullionsize));
                dbCommand.Parameters.AddWithValue("@MINPRICE", CheckForNull((object)diamondinvModel.MIN_PRICE));
                dbCommand.Parameters.AddWithValue("@crwn_angl", CheckForNull((object)diamondinvModel.crwn_angl));
                dbCommand.Parameters.AddWithValue("@crwn_hght", CheckForNull((object)diamondinvModel.crwn_hght));
                dbCommand.Parameters.AddWithValue("@pvln_angl", CheckForNull((object)diamondinvModel.pvln_angl));
                dbCommand.Parameters.AddWithValue("@pvln_dpth", CheckForNull((object)diamondinvModel.pvln_dpth));
                dbCommand.Parameters.AddWithValue("@star_len", CheckForNull((object)diamondinvModel.star_len));
                dbCommand.Parameters.AddWithValue("@lowr_hlf", CheckForNull((object)diamondinvModel.lowr_hlf));
                dbCommand.Parameters.AddWithValue("@flour_c", CheckForNull((object)diamondinvModel.flour_c));
                dbCommand.Parameters.AddWithValue("@partner1", CheckForNull((object)diamondinvModel.partner1));
                dbCommand.Parameters.AddWithValue("@partner2", CheckForNull((object)diamondinvModel.partner2));
                dbCommand.Parameters.AddWithValue("@partner3", CheckForNull((object)diamondinvModel.partner3));
                dbCommand.Parameters.AddWithValue("@partner4", CheckForNull((object)diamondinvModel.partner4));

                dbCommand.Parameters.AddWithValue("@part_share1", CheckForNull((object)diamondinvModel.part_share1));
                dbCommand.Parameters.AddWithValue("@part_share2", CheckForNull((object)diamondinvModel.part_share2));
                dbCommand.Parameters.AddWithValue("@part_share3", CheckForNull((object)diamondinvModel.part_share3));
                dbCommand.Parameters.AddWithValue("@part_share4", CheckForNull((object)diamondinvModel.part_share4));

                //chandana code              
                dbCommand.Parameters.AddWithValue("@sku", CheckForNull((object)diamondinvModel.sku));
                dbCommand.Parameters.AddWithValue("@joma_sku", CheckForNull((object)diamondinvModel.joma_sku));
                dbCommand.Parameters.AddWithValue("@model", CheckForNull((object)diamondinvModel.model));
                dbCommand.Parameters.AddWithValue("@reference_number", CheckForNull((object)diamondinvModel.reference_number));
                dbCommand.Parameters.AddWithValue("@serial_number", CheckForNull((object)diamondinvModel.serial_number));

                dbCommand.Parameters.AddWithValue("@country", CheckForNull((object)diamondinvModel.country));
                dbCommand.Parameters.AddWithValue("@dispatchtimemax", CheckForNull((object)diamondinvModel.dispatchtimemax));
                dbCommand.Parameters.AddWithValue("@style_type", CheckForNull((object)diamondinvModel.style_type));
                dbCommand.Parameters.AddWithValue("@gender", CheckForNull((object)diamondinvModel.gender));
                dbCommand.Parameters.AddWithValue("@Condition", CheckForNull((object)diamondinvModel.Condition));
                dbCommand.Parameters.AddWithValue("@html_desc", CheckForNull((object)diamondinvModel.html_desc));
                dbCommand.Parameters.AddWithValue("@Calendar", CheckForNull((object)diamondinvModel.Calendar));
                dbCommand.Parameters.AddWithValue("@Movement", CheckForNull((object)diamondinvModel.Movement));
                dbCommand.Parameters.AddWithValue("@Power_Reserve", CheckForNull((object)diamondinvModel.Power_Reserve));
                dbCommand.Parameters.AddWithValue("@Luminance", CheckForNull((object)diamondinvModel.Luminance));
                dbCommand.Parameters.AddWithValue("@Metal_Stamp", CheckForNull((object)diamondinvModel.Metal_Stamp));
                dbCommand.Parameters.AddWithValue("@W_R_Depth", CheckForNull((object)diamondinvModel.W_R_Depth));
                dbCommand.Parameters.AddWithValue("@W_R_Unit", CheckForNull((object)diamondinvModel.W_R_Unit));
                dbCommand.Parameters.AddWithValue("@B_Type", CheckForNull((object)diamondinvModel.B_Type));
                dbCommand.Parameters.AddWithValue("@B_Material", CheckForNull((object)diamondinvModel.B_Material));
                dbCommand.Parameters.AddWithValue("@B_Color", CheckForNull((object)diamondinvModel.B_Color));
                dbCommand.Parameters.AddWithValue("@B_Length", CheckForNull((object)diamondinvModel.B_Length));
                dbCommand.Parameters.AddWithValue("@B_Length_Unit", CheckForNull((object)diamondinvModel.B_Length_Unit));
                dbCommand.Parameters.AddWithValue("@B_Width", CheckForNull((object)diamondinvModel.B_Width));
                dbCommand.Parameters.AddWithValue("@B_Width_Unit", CheckForNull((object)diamondinvModel.B_Width_Unit));
                dbCommand.Parameters.AddWithValue("@B_Number_of_links", CheckForNull((object)diamondinvModel.B_Number_of_links));
                dbCommand.Parameters.AddWithValue("@C_Material", CheckForNull((object)diamondinvModel.C_Material));
                dbCommand.Parameters.AddWithValue("@C_Color", CheckForNull((object)diamondinvModel.C_Color));
                dbCommand.Parameters.AddWithValue("@C_Shape", CheckForNull((object)diamondinvModel.C_Shape));
                dbCommand.Parameters.AddWithValue("@C_Finish", CheckForNull((object)diamondinvModel.C_Finish));
                dbCommand.Parameters.AddWithValue("@C_Back", CheckForNull((object)diamondinvModel.C_Back));
                dbCommand.Parameters.AddWithValue("@C_Crown", CheckForNull((object)diamondinvModel.C_Crown));
                dbCommand.Parameters.AddWithValue("@C_Diameter", CheckForNull((object)diamondinvModel.C_Diameter));
                dbCommand.Parameters.AddWithValue("@C_Thickness", CheckForNull((object)diamondinvModel.C_Thickness));
                dbCommand.Parameters.AddWithValue("@C_Thickness_Unit", CheckForNull((object)diamondinvModel.C_Thickness_Unit));
                dbCommand.Parameters.AddWithValue("@I_Type", CheckForNull((object)diamondinvModel.I_Type));
                dbCommand.Parameters.AddWithValue("@I_Material", CheckForNull((object)diamondinvModel.I_Material));
                dbCommand.Parameters.AddWithValue("@I_Function", CheckForNull((object)diamondinvModel.I_Function));
                dbCommand.Parameters.AddWithValue("@T_Type", CheckForNull((object)diamondinvModel.T_Type));
                dbCommand.Parameters.AddWithValue("@C_Diameter_Unit", CheckForNull((object)diamondinvModel.C_Diameter_Unit));
                dbCommand.Parameters.AddWithValue("@T_Material", CheckForNull((object)diamondinvModel.T_Material));
                dbCommand.Parameters.AddWithValue("@T_Code", CheckForNull((object)diamondinvModel.T_Code));
                dbCommand.Parameters.AddWithValue("@D_Type", CheckForNull((object)diamondinvModel.D_Type));
                dbCommand.Parameters.AddWithValue("@D_Material", CheckForNull((object)diamondinvModel.D_Material));
                dbCommand.Parameters.AddWithValue("@D_Color", CheckForNull((object)diamondinvModel.D_Color));
                dbCommand.Parameters.AddWithValue("@D_Crystal", CheckForNull((object)diamondinvModel.D_Crystal));
                dbCommand.Parameters.AddWithValue("@D_Features", CheckForNull((object)diamondinvModel.D_Features));
                dbCommand.Parameters.AddWithValue("@D_Functions", CheckForNull((object)diamondinvModel.D_Functions));
                dbCommand.Parameters.AddWithValue("@Offline_price", CheckForNull((object)diamondinvModel.Offline_price));
                dbCommand.Parameters.AddWithValue("@TheCollective", CheckForNull((object)diamondinvModel.TheCollective));
                dbCommand.Parameters.AddWithValue("@Duration", CheckForNull((object)diamondinvModel.Duration));
                dbCommand.Parameters.AddWithValue("@BestOfferEnabled", CheckForNull((object)diamondinvModel.BestOfferEnabled));

                dbCommand.Parameters.AddWithValue("@Amazon_Price", CheckForNull((object)diamondinvModel.Amazon_Price));
                dbCommand.Parameters.AddWithValue("@Amazon_CA_Price", CheckForNull((object)diamondinvModel.Amazon_CA_Price));
                dbCommand.Parameters.AddWithValue("@Ebay_price", CheckForNull((object)diamondinvModel.Ebay_price));
                dbCommand.Parameters.AddWithValue("@Main_Image_URL", CheckForNull((object)diamondinvModel.Main_Image_URL));
                dbCommand.Parameters.AddWithValue("@Back_Image_URL", CheckForNull((object)diamondinvModel.Back_Image_URL));
                dbCommand.Parameters.AddWithValue("@Side_Image_URL", CheckForNull((object)diamondinvModel.Side_Image_URL));
                dbCommand.Parameters.AddWithValue("@Serial_Image_URL", CheckForNull((object)diamondinvModel.Serial_Image_URL));
                dbCommand.Parameters.AddWithValue("@Other_Image_URL_1", CheckForNull((object)diamondinvModel.Other_Image_URL_1));
                dbCommand.Parameters.AddWithValue("@Other_Image_URL_2", CheckForNull((object)diamondinvModel.Other_Image_URL_2));
                dbCommand.Parameters.AddWithValue("@Other_Image_URL_3", CheckForNull((object)diamondinvModel.Other_Image_URL_3));
                dbCommand.Parameters.AddWithValue("@Other_Image_URL_4", CheckForNull((object)diamondinvModel.Other_Image_URL_4));
                dbCommand.Parameters.AddWithValue("@Authenticity", CheckForNull((object)diamondinvModel.Authenticity));
                dbCommand.Parameters.AddWithValue("@Box_Papers", CheckForNull((object)diamondinvModel.Box_Papers));
                dbCommand.Parameters.AddWithValue("@External_Wear", CheckForNull((object)diamondinvModel.External_Wear));
                dbCommand.Parameters.AddWithValue("@Functionality", CheckForNull((object)diamondinvModel.Functionality));
                dbCommand.Parameters.AddWithValue("@Water_Testing", CheckForNull((object)diamondinvModel.Water_Testing));
                dbCommand.Parameters.AddWithValue("@Inspection_comment", CheckForNull((object)diamondinvModel.Inspection_comment));
                dbCommand.Parameters.AddWithValue("@Format", CheckForNull((object)diamondinvModel.Format));
                dbCommand.Parameters.AddWithValue("@Ebay_Minimum_Price", CheckForNull((object)diamondinvModel.Ebay_Minimum_Price));
                dbCommand.Parameters.AddWithValue("@Shipping", CheckForNull((object)diamondinvModel.Shipping));
                dbCommand.Parameters.AddWithValue("@ShippingService_1_Option", CheckForNull((object)diamondinvModel.ShippingService_1_Option));
                dbCommand.Parameters.AddWithValue("@ShippingService_1_Cost", CheckForNull((object)diamondinvModel.ShippingService_1_Cost));
                dbCommand.Parameters.AddWithValue("@Returns", CheckForNull((object)diamondinvModel.Returns));
                dbCommand.Parameters.AddWithValue("@ReturnsWithinOption", CheckForNull((object)diamondinvModel.ReturnsWithinOption));
                dbCommand.Parameters.AddWithValue("@RefundOption", CheckForNull((object)diamondinvModel.RefundOption));
                dbCommand.Parameters.AddWithValue("@ShippingCostPaidByOption", CheckForNull((object)diamondinvModel.ShippingCostPaidByOption));
                dbCommand.Parameters.AddWithValue("@RestockingFeeValueOption", CheckForNull((object)diamondinvModel.RestockingFeeValueOption));
                dbCommand.Parameters.AddWithValue("@IntlShippingService_1_Option", CheckForNull((object)diamondinvModel.IntlShippingService_1_Option));
                dbCommand.Parameters.AddWithValue("@IntlShippingService_1_Cost", CheckForNull((object)diamondinvModel.IntlShippingService_1_Cost));
                dbCommand.Parameters.AddWithValue("@IntlShippingService_1_Locations", CheckForNull((object)diamondinvModel.IntlShippingService_1_Locations));
                dbCommand.Parameters.AddWithValue("@MPN", CheckForNull((object)diamondinvModel.MPN));
                dbCommand.Parameters.AddWithValue("@StoreCategory", CheckForNull((object)diamondinvModel.StoreCategory));
                dbCommand.Parameters.AddWithValue("@StoreCategory2", CheckForNull((object)diamondinvModel.StoreCategory2));
                dbCommand.Parameters.AddWithValue("@UseTaxTable", CheckForNull((object)diamondinvModel.UseTaxTable));
                dbCommand.Parameters.AddWithValue("@Shipping_Template", CheckForNull((object)diamondinvModel.Shipping_Template));
                dbCommand.Parameters.AddWithValue("@IS_Jomashop", (bool)diamondinvModel.IS_Jomashop ? 1 : 0);
                dbCommand.Parameters.AddWithValue("@IS_Watchfact", (bool)diamondinvModel.IS_Watchfact ? 1 : 0);
                dbCommand.Parameters.AddWithValue("@OnWatchfact", (bool)diamondinvModel.OnWatchfact ? 1 : 0);
                dbCommand.Parameters.AddWithValue("@OnJomaShop", (bool)diamondinvModel.OnJomaShop ? 1 : 0);

                // Added by Sachin For Joma - Sachin
                dbCommand.Parameters.AddWithValue("@JOMA_Brand", CheckForNull((object)diamondinvModel.JOMA_Brand));
                dbCommand.Parameters.AddWithValue("@JOMA_ProdIDType", CheckForNull((object)diamondinvModel.JOMA_ProdIDType));
                dbCommand.Parameters.AddWithValue("@JOMA_Gender", CheckForNull((object)diamondinvModel.JOMA_Gender));
                dbCommand.Parameters.AddWithValue("@JOMA_BoxPaper", CheckForNull((object)diamondinvModel.JOMA_BoxPaper));
                dbCommand.Parameters.AddWithValue("@JOMA_PreOwned", CheckForNull((object)diamondinvModel.JOMA_PreOwned));
                dbCommand.Parameters.AddWithValue("@JOMA_Style", CheckForNull((object)diamondinvModel.JOMA_Style));
                dbCommand.Parameters.AddWithValue("@JOMA_Movement", CheckForNull((object)diamondinvModel.JOMA_Movement));
                dbCommand.Parameters.AddWithValue("@JOMA_D_Type", CheckForNull((object)diamondinvModel.JOMA_D_Type));
                dbCommand.Parameters.AddWithValue("@JOMA_D_Material", CheckForNull((object)diamondinvModel.JOMA_D_Material));
                dbCommand.Parameters.AddWithValue("@JOMA_D_Color", CheckForNull((object)diamondinvModel.JOMA_D_Color));
                dbCommand.Parameters.AddWithValue("@JOMA_D_Crystal", CheckForNull((object)diamondinvModel.JOMA_D_Crystal));
                dbCommand.Parameters.AddWithValue("@JOMA_D_Feature", CheckForNull((object)diamondinvModel.JOMA_D_Feature));
                dbCommand.Parameters.AddWithValue("@JOMA_D_Function", CheckForNull((object)diamondinvModel.JOMA_D_Function));
                dbCommand.Parameters.AddWithValue("@JOMA_D_Maker", CheckForNull((object)diamondinvModel.JOMA_D_Maker));
                dbCommand.Parameters.AddWithValue("@JOMA_C_Material", CheckForNull((object)diamondinvModel.JOMA_C_Material));
                dbCommand.Parameters.AddWithValue("@JOMA_C_Color", CheckForNull((object)diamondinvModel.JOMA_C_Color));
                dbCommand.Parameters.AddWithValue("@JOMA_C_Shape", CheckForNull((object)diamondinvModel.JOMA_C_Shape));
                dbCommand.Parameters.AddWithValue("@JOMA_C_Back", CheckForNull((object)diamondinvModel.JOMA_C_Back));
                dbCommand.Parameters.AddWithValue("@JOMA_C_Crown", CheckForNull((object)diamondinvModel.JOMA_C_Crown));
                dbCommand.Parameters.AddWithValue("@JOMA_B_Type", CheckForNull((object)diamondinvModel.JOMA_B_Type));
                dbCommand.Parameters.AddWithValue("@JOMA_B_Material", CheckForNull((object)diamondinvModel.JOMA_B_Material));
                dbCommand.Parameters.AddWithValue("@JOMA_B_Color", CheckForNull((object)diamondinvModel.JOMA_B_Color));
                dbCommand.Parameters.AddWithValue("@JOMA_BZ_Type", CheckForNull((object)diamondinvModel.JOMA_BZ_Type));
                dbCommand.Parameters.AddWithValue("@JOMA_BZ_Material", CheckForNull((object)diamondinvModel.JOMA_BZ_Material));
                dbCommand.Parameters.AddWithValue("@JOMA_BZ_Color", CheckForNull((object)diamondinvModel.JOMA_BZ_Color));
                dbCommand.Parameters.AddWithValue("@JOMA_CL_Type", CheckForNull((object)diamondinvModel.JOMA_CL_Type));
                dbCommand.Parameters.AddWithValue("@JOMA_Warranty", CheckForNull((object)diamondinvModel.JOMA_Warranty));
                dbCommand.Parameters.AddWithValue("@JOMA_Country", CheckForNull((object)diamondinvModel.JOMA_Country));
                dbCommand.Parameters.AddWithValue("@ASSEMBLY_COST", CheckForNull((object)diamondinvModel.ASSEMBLY_COST));

                dbCommand.Parameters.AddWithValue("@STONE_COST", CheckForNull((object)diamondinvModel.STONE_COST));
                dbCommand.Parameters.AddWithValue("@LABOR_COST", CheckForNull((object)diamondinvModel.LABOR_COST));
                dbCommand.Parameters.AddWithValue("@FIELDVAL1_COST", CheckForNull((object)diamondinvModel.FILEDVAL1_COST));
                dbCommand.Parameters.AddWithValue("@FIELDVAL2_COST", CheckForNull((object)diamondinvModel.FILEDVAL2_COST));
                dbCommand.Parameters.AddWithValue("@FIELDVAL3_COST", CheckForNull((object)diamondinvModel.FILEDVAL3_COST));

                dbCommand.Parameters.AddWithValue("@FIELD1TXT_COST", CheckForNull((object)diamondinvModel.field1txt_cost));
                dbCommand.Parameters.AddWithValue("@FIELD2TXT_COST", CheckForNull((object)diamondinvModel.field2txt_cost));
                dbCommand.Parameters.AddWithValue("@FIELD3TXT_COST", CheckForNull((object)diamondinvModel.field3txt_cost));

                // Added by Sachin For Joma -End
                dbCommand.Parameters.AddWithValue("@D_MARKER", CheckForNull((object)diamondinvModel.D_MARKER));

                dbCommand.Parameters.AddWithValue("@ESP", CheckForNull((object)diamondinvModel.ESP));
                dbCommand.Parameters.AddWithValue("@stoneCostMulti", CheckForNull((object)diamondinvModel.stoneCostMulti));
                dbCommand.Parameters.AddWithValue("@stnLaborCost", CheckForNull((object)diamondinvModel.StnLaborCost));
                dbCommand.Parameters.AddWithValue("@MinimumPrice", CheckForNull((object)diamondinvModel.MinimumPrice));
                dbCommand.Parameters.AddWithValue("@NextRoundOff", CheckForNull((object)diamondinvModel.NextRoundOff));

                SqlParameter parameter = new SqlParameter();
                parameter = new SqlParameter()
                {
                    ParameterName = "@STYLEATTRTEXT",
                    SqlDbType = System.Data.SqlDbType.Xml,
                    Value = diamondinvModel.STYLEATTRTEXT
                };
                dbCommand.Parameters.Add(parameter);

                parameter = new SqlParameter()
                {
                    ParameterName = "@TBLSTONES",
                    SqlDbType = System.Data.SqlDbType.Xml,
                    Value = stonedata
                };
                dbCommand.Parameters.Add(parameter);

                dbCommand.Parameters.AddWithValue("@NoCommission", CheckForNull((object)diamondinvModel.No_Commission));
                dbCommand.Parameters.AddWithValue("@NoWarranty", CheckForNull((object)diamondinvModel.No_Warranty));
                dbCommand.Parameters.AddWithValue("@ReplacementCost", CheckForNull((object)diamondinvModel.ReplacementCost));
                dbCommand.Parameters.AddWithValue("@Diff_ReplacementCost", CheckForNull((object)diamondinvModel.diff_replacementCost));
                dbCommand.Parameters.AddWithValue("@StylesCase", stylesCase);
                dbCommand.Parameters.AddWithValue("@Disclaimers", diamondinvModel.disclaimer);

                dbCommand.Parameters.AddWithValue("@STYLES_COST1", CheckForNull((object)diamondinvModel.Style_Cost1));
                dbCommand.Parameters.AddWithValue("@STYLES_COST2", CheckForNull((object)diamondinvModel.Style_Cost2));
                dbCommand.Parameters.AddWithValue("@STYLES_COST3", CheckForNull((object)diamondinvModel.Style_Cost3));
                dbCommand.Parameters.AddWithValue("@STYLES_COST4", CheckForNull((object)diamondinvModel.Style_Cost4));
                dbCommand.Parameters.AddWithValue("@STYLES_COST5", CheckForNull((object)diamondinvModel.Style_Cost5));
                dbCommand.Parameters.AddWithValue("@STYLES_COST6", CheckForNull((object)diamondinvModel.Style_Cost6));
                dbCommand.Parameters.AddWithValue("@STYLES_COST7", CheckForNull((object)diamondinvModel.Style_Cost7));
                dbCommand.Parameters.AddWithValue("@STYLES_COST8", CheckForNull((object)diamondinvModel.Style_Cost8));
                dbCommand.Parameters.AddWithValue("@STYLES_COST9", CheckForNull((object)diamondinvModel.Style_Cost9));
                dbCommand.Parameters.AddWithValue("@STYLES_COST10", CheckForNull((object)diamondinvModel.Style_Cost10));

                dbCommand.Parameters.AddWithValue("@STYLES_COST11", CheckForNull((object)diamondinvModel.Style_Cost11));
                dbCommand.Parameters.AddWithValue("@STYLES_COST12", CheckForNull((object)diamondinvModel.Style_Cost12));

                dbCommand.Parameters.AddWithValue("@LefkDiamond", CheckForNull((object)diamondinvModel.LefkDiamond));
                dbCommand.Parameters.AddWithValue("@LefkColorgem", CheckForNull((object)diamondinvModel.LefkColorgem));
                dbCommand.Parameters.AddWithValue("@LefkCasting", CheckForNull((object)diamondinvModel.LefkCasting));
                dbCommand.Parameters.AddWithValue("@LefkJeweler", CheckForNull((object)diamondinvModel.LefkJeweler));
                dbCommand.Parameters.AddWithValue("@LefkSetting", CheckForNull((object)diamondinvModel.LefkSetting));
                dbCommand.Parameters.AddWithValue("@LefkModel", CheckForNull((object)diamondinvModel.LefkModel));
                dbCommand.Parameters.AddWithValue("@JobBagNo", CheckForNull((object)diamondinvModel.JobBagNo));

                //2nd Cert Related params
                dbCommand.Parameters.AddWithValue("@CERT_TYPE2", CheckForNull((object)diamondinvModel.CERT_TYPE2));
                dbCommand.Parameters.AddWithValue("@CERT_NO2", CheckForNull((object)diamondinvModel.CERT_NO2));
                dbCommand.Parameters.AddWithValue("@CT_CUT2", CheckForNull((object)diamondinvModel.CT_CUT2));
                dbCommand.Parameters.AddWithValue("@SIZE2", CheckForNull((object)diamondinvModel.SIZE2));
                dbCommand.Parameters.AddWithValue("@CT_WEIGHT2", CheckForNull((object)diamondinvModel.CT_WEIGHT2));
                dbCommand.Parameters.AddWithValue("@CT_DEPTH2", CheckForNull((object)diamondinvModel.CT_DEPTH2));
                dbCommand.Parameters.AddWithValue("@CT_TABLE2", CheckForNull((object)diamondinvModel.CT_TABLE2));
                dbCommand.Parameters.AddWithValue("@CT_GIRDLE2", CheckForNull((object)diamondinvModel.CT_GIRDLE2));
                dbCommand.Parameters.AddWithValue("@CT_POLISH2", CheckForNull((object)diamondinvModel.CT_POLISH2));
                dbCommand.Parameters.AddWithValue("@SYMMETRY2", CheckForNull((object)diamondinvModel.SYMMETRY2));
                dbCommand.Parameters.AddWithValue("@CT_GRADE2", CheckForNull((object)diamondinvModel.GRADE2));
                dbCommand.Parameters.AddWithValue("@QUALITY2", CheckForNull((object)diamondinvModel.QUALITY2));
                dbCommand.Parameters.AddWithValue("@COLOR2", CheckForNull((object)diamondinvModel.COLOR2));
                dbCommand.Parameters.AddWithValue("@crwn_angl2", CheckForNull((object)diamondinvModel.crwn_angl2));
                dbCommand.Parameters.AddWithValue("@crwn_hght2", CheckForNull((object)diamondinvModel.crwn_hght2));
                dbCommand.Parameters.AddWithValue("@pvln_angl2", CheckForNull((object)diamondinvModel.pvln_angl2));
                dbCommand.Parameters.AddWithValue("@pvln_dpth2", CheckForNull((object)diamondinvModel.pvln_dpth2));
                dbCommand.Parameters.AddWithValue("@star_len2", CheckForNull((object)diamondinvModel.star_len2));
                dbCommand.Parameters.AddWithValue("@lowr_hlf2", CheckForNull((object)diamondinvModel.lowr_hlf2));
                dbCommand.Parameters.AddWithValue("@FLOUR2", CheckForNull((object)diamondinvModel.FLOUR2));
                dbCommand.Parameters.AddWithValue("@flour_c2", CheckForNull((object)diamondinvModel.flour_c2));
                dbCommand.Parameters.AddWithValue("@CT_NOTE2", CheckForNull((object)diamondinvModel.CT_NOTE2));
                dbCommand.Parameters.AddWithValue("@DEPTH", CheckForNull((object)diamondinvModel.DEPTH));
                dbCommand.Parameters.AddWithValue("@DEPTH2", CheckForNull((object)diamondinvModel.DEPTH2));
                dbCommand.Parameters.AddWithValue("@FANCY_CLR2", CheckForNull((object)diamondinvModel.FANCY_CLR2));
                dbCommand.Parameters.AddWithValue("@FAN_INT2", CheckForNull((object)diamondinvModel.FAN_INT2));
                dbCommand.Parameters.AddWithValue("@USE_CERT2", CheckForNull((object)diamondinvModel.USE_CERT2));
                dbCommand.Parameters.AddWithValue("@SLDPRTNR", CheckForNull((object)diamondinvModel.SLDPRTNR));
                dbCommand.Parameters.AddWithValue("@PRTNR1PRFT", CheckForNull((object)diamondinvModel.PRTNR1PRFT));
                dbCommand.Parameters.AddWithValue("@PRTNR2PRFT", CheckForNull((object)diamondinvModel.PRTNR2PRFT));
                dbCommand.Parameters.AddWithValue("@PRTNR3PRFT", CheckForNull((object)diamondinvModel.PRTNR3PRFT));
                dbCommand.Parameters.AddWithValue("@PRTNR4PRFT", CheckForNull((object)diamondinvModel.PRTNR4PRFT));
                dbCommand.Parameters.AddWithValue("@DATE_SOLD", CheckForNull((object)diamondinvModel.DATE_SOLD));
                dbCommand.Parameters.AddWithValue("@PRC_SOLD", CheckForNull((object)diamondinvModel.PRC_SOLD));
                dbCommand.Parameters.AddWithValue("@VIDEO_LINK", CheckForNull((object)diamondinvModel.VIDEO_LINK));
                dbCommand.Parameters.AddWithValue("@PRICEBYWT", diamondinvModel.PRICEBYWT == true ? 1 : 0);
                dbCommand.Parameters.AddWithValue("@Is_AddStock", diamondinvModel.IsAddStock);
                dbCommand.Parameters.AddWithValue("@Reason", diamondinvModel.Reason);
                dbCommand.Parameters.AddWithValue("@FIND4", CheckForNull((object)diamondinvModel.FIND4));
                dbCommand.Parameters.AddWithValue("@FIND_QTY4", CheckForNull((object)diamondinvModel.FIND_QTY4));

                dbCommand.Parameters.AddWithValue("@clasp_wt", CheckForNull((object)diamondinvModel.clasp_wt));
                dbCommand.Parameters.AddWithValue("@LaborPc", CheckForNull((object)diamondinvModel.LaborPc));

                dbCommand.Parameters.AddWithValue("@CLASP_OZ", CheckForNull((object)diamondinvModel.CLASP_OZ));
                dbCommand.Parameters.AddWithValue("@CHAIN_OZ", CheckForNull((object)diamondinvModel.CHAIN_OZ));
                dbCommand.Parameters.AddWithValue("@GoldPart4_OZ", CheckForNull((object)diamondinvModel.GoldPart4_OZ));
                dbCommand.Parameters.AddWithValue("@CLASP_SRCHRG", CheckForNull((object)diamondinvModel.CLASP_SRCHRG));
                dbCommand.Parameters.AddWithValue("@CHAIN_SRCHRG", CheckForNull((object)diamondinvModel.CHAIN_SRCHRG));
                dbCommand.Parameters.AddWithValue("@GoldPart4_Srchrg", CheckForNull((object)diamondinvModel.GoldPart4_Srchrg));
                dbCommand.Parameters.AddWithValue("@GoldPart2_GR", CheckForNull((object)diamondinvModel.GoldPart2_GR));
                dbCommand.Parameters.AddWithValue("@GoldPart3_GR", CheckForNull((object)diamondinvModel.GoldPart3_GR));
                dbCommand.Parameters.AddWithValue("@GoldPart4_Gr", CheckForNull((object)diamondinvModel.GoldPart4_Gr));
                dbCommand.Parameters.AddWithValue("@Silver_GR", CheckForNull((object)diamondinvModel.Silver_GR));
                dbCommand.Parameters.AddWithValue("@Platinum_GR", CheckForNull((object)diamondinvModel.Platinum_GR));
                dbCommand.Parameters.AddWithValue("@GoldPart2_Karat", CheckForNull((object)diamondinvModel.GoldPart2_Karat));
                dbCommand.Parameters.AddWithValue("@GoldPart3_Karat", CheckForNull((object)diamondinvModel.GoldPart3_Karat));
                dbCommand.Parameters.AddWithValue("@GoldPart4_Karat", CheckForNull((object)diamondinvModel.GoldPart4_Karat));

                dbCommand.Parameters.AddWithValue("@MultiMarkup", (bool)diamondinvModel.MultiMarkup ? 1 : 0);
                dbCommand.Parameters.AddWithValue("@CastingMulti", CheckForNull((object)diamondinvModel.CastingCostMulti));
                dbCommand.Parameters.AddWithValue("@SettingMulti", CheckForNull((object)diamondinvModel.SettingCostMulti));
                dbCommand.Parameters.AddWithValue("@FindingMulti", CheckForNull((object)diamondinvModel.FindingCostMulti));
                dbCommand.Parameters.AddWithValue("@StoneMulti", CheckForNull((object)diamondinvModel.stoneCostMulti));
                dbCommand.Parameters.AddWithValue("@LaborMulti", CheckForNull((object)diamondinvModel.LaborCostMulti));
                dbCommand.Parameters.AddWithValue("@IsBom", (bool)diamondinvModel.Isbom ? 1 : 0);
                dbCommand.Parameters.AddWithValue("@center_Subtype", CheckForNull((object)diamondinvModel.center_subtype));
                dbCommand.Parameters.AddWithValue("@tag_template", CheckForNull((object)diamondinvModel.Tag_Template));
                dbCommand.Parameters.AddWithValue("@IsFromBill", (bool)diamondinvModel.IsFromBill ? 1 : 0);
                dbCommand.Parameters.AddWithValue("@IsNoStockNo", (bool)diamondinvModel.no_stkno ? 1 : 0);
                dbCommand.Parameters.AddWithValue("@PartNo", diamondinvModel.PART_NO);
                dbCommand.Parameters.AddWithValue("@SubPart", diamondinvModel.SUBPART);
                dbCommand.Parameters.AddWithValue("@SoldByPiece", diamondinvModel.by_pc);
                dbCommand.Parameters.AddWithValue("@Metal_Gross", diamondinvModel.Metal_gross);
                dbCommand.Parameters.AddWithValue("@Labor_Gross", diamondinvModel.Labor_gross);
                dbCommand.Parameters.AddWithValue("@isInStock", diamondinvModel.isInStock);
                dbCommand.Parameters.AddWithValue("@SHARED_INVENTORY", diamondinvModel.SHARED_INVENTORY);
                dbCommand.Parameters.AddWithValue("@PcsInSet", diamondinvModel.PcsInSet);
                dbCommand.Parameters.AddWithValue("@DiamondWt", diamondinvModel.DiamondWt);
                dbCommand.Parameters.AddWithValue("@UncutDiamondWt", diamondinvModel.UncutDiamondWt);
                dbCommand.Parameters.AddWithValue("@LabGrown", diamondinvModel.LabGrown);
                dbCommand.Parameters.AddWithValue("@NoDiscount", (bool)diamondinvModel.NoDiscount ? 1 : 0);
                dbCommand.Parameters.AddWithValue("@CostAdjustment", (bool)diamondinvModel.CostAdjustment ? 1 : 0);
                dbCommand.Parameters.AddWithValue("@Adjustment", CheckForNull((object)diamondinvModel.Adjustment));
                dbCommand.Parameters.AddWithValue("@PriceAdjustment", CheckForNull((object)diamondinvModel.PriceAdjustment));
                dbCommand.Parameters.AddWithValue("@WebPercentage", CheckForNull((object)diamondinvModel.WebPercentage));
                dbCommand.Parameters.AddWithValue("@MfgDetails", CheckForNull((object)diamondinvModel.MfgDetails));
                parameter = new SqlParameter()
                {
                    ParameterName = "@TBLSTONESBOM",
                    SqlDbType = System.Data.SqlDbType.Xml,
                    Value = stonedatabom
                };
                dbCommand.Parameters.Add(parameter);

                dbCommand.CommandTimeout = 5000;
                dbCommand.Connection.Open();
                var rowsAffected = dbCommand.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }

    }
}
