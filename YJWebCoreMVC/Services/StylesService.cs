using Microsoft.Data.SqlClient;
using System.Data;
using YJWebCoreMVC.Models;

namespace YJWebCoreMVC.Services
{
    public class StylesService
    {
        private readonly ConnectionProvider _connectionProvider;

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

    }
}
