using Microsoft.Data.SqlClient;
using System.Data;
using YJWebCoreMVC.Models;

namespace YJWebCoreMVC.Services
{
    public class CommonService
    {
        private readonly ConnectionProvider _connectionProvider;
        private readonly HelperCommonService _helperCommonService;

        public CommonService(ConnectionProvider connectionProvider, HelperCommonService helperCommonService, IWebHostEnvironment env)
        {
            _connectionProvider = connectionProvider;
            _helperCommonService = helperCommonService;
        }


        public DataTable GetStylesSearchData(InventoryModel crmodel, StylesAttribute modStyleAtr, string cstore, bool ignorein_Shop = false, bool separateSNFromModel = false, bool IsprintLongdesc = false)
        {
            DataTable dataTable = new DataTable();

            using (SqlDataAdapter SqlDataAdapter = new SqlDataAdapter())
            {
                SqlDataAdapter.SelectCommand = new SqlCommand();
                SqlDataAdapter.SelectCommand.Connection = _connectionProvider.GetConnection();
                SqlDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter.SelectCommand.CommandText = "GetStylesSearchData";
                SqlDataAdapter.SelectCommand.CommandTimeout = 6000;

                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@CATEGORY", crmodel.category);
                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@SUBCATEGORY", crmodel.subcategories);
                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@METAL", crmodel.metal);
                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@VENDORS", crmodel.vendors);
                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@BRAND", crmodel.brand);
                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@SUBBRAND", crmodel.subbrand);
                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@SHAPE", crmodel.shape);
                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@CTYPE", crmodel.ctype);
                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@STYLEFROM", crmodel.fromstyle);
                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@STYLETO", crmodel.tostyle);
                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@BELOWMINSTOCK", crmodel.selectedminstock);
                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@PRINTBYSIZE", crmodel.printbysize);
                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@PRINTBYSUMMERY", crmodel.printbysummery);
                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@PRINTBYSUMMERYMODEL", crmodel.printbysummerymodel);
                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@STOCKTYPE", crmodel.displaystocktype);
                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@ALLSTOCK", crmodel.displayallstock);
                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@INCLUDEINSTOCK", crmodel.displayincludeinstock);
                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@INCLUDESALESMANINVENTORY", crmodel.displayincludesalesmaninventory);
                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@ITEMSONMEMO", crmodel.displayitemsonmemo);
                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@DESC_INCLUDE", crmodel.descriptionInclude);
                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@COLOR", crmodel.COLOR);
                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@CLARITY", crmodel.CLARITY);
                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@MOUNTED", crmodel.MOUNTED);
                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@NONMOUNTED", crmodel.NONMOUNTED);
                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@WtFrom", crmodel.WtFrom);
                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@WtThru", crmodel.WtThru);
                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@GLCLASS", crmodel.GLCLASS);
                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@SelectedStore", cstore);
                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@Group", crmodel.group != null ? crmodel.group : "");
                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@ignorein_Shop", ignorein_Shop);
                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@CntStnSizeFrom", crmodel.cntStnSizeFrm);
                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@CntStnSizeTo", crmodel.cntStnSizeTo);
                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@VndStyle", crmodel.vnd_style);
                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@strItemType", crmodel.strItemType == null ? "" : crmodel.strItemType);
                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@IsNegative", crmodel.IsNegative);
                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@IsReplacementCost", crmodel.IsReplacementCost);
                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@fdate", crmodel.fdate);
                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@tdate", crmodel.tdate);

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

                SqlDataAdapter.Fill(dataTable);
            }

            return dataTable;
        }

        public DataTable GetStyleHistoryDetails(string style)
        {
            DataTable dataTable = new DataTable();

            using (SqlDataAdapter SqlDataAdapter = new SqlDataAdapter())
            {
                SqlDataAdapter.SelectCommand = new SqlCommand();
                SqlDataAdapter.SelectCommand.Connection = _connectionProvider.GetConnection();
                SqlDataAdapter.SelectCommand.CommandType = CommandType.Text;

                SqlDataAdapter.SelectCommand.CommandText = "select * From STK_HIST Where STYLE= @STYLE  order by date";

                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@STYLE", style);

                SqlDataAdapter.Fill(dataTable);
            }

            return dataTable;
        }

        public string GetCustomerNameByCode(string acc)
        {
            DataTable dataTable = _helperCommonService.GetSqlData("select acc,NAME from customer where acc='" + acc + "'");
            string name = "";
            if (_helperCommonService.DataTableOK(dataTable))
            {
                DataRow row = dataTable.Rows[0];
                name = row["NAME"].ToString();
            }

            return name;
        }

        public string GetPotCustomerNameByCode(string acc)
        {
            DataTable dataTable = _helperCommonService.GetSqlData("select acc,NAME from MAILING where acc='" + acc + "'");
            string name = "";
            if (_helperCommonService.DataTableOK(dataTable))
            {
                DataRow row = dataTable.Rows[0];
                name = row["NAME"].ToString();
            }

            return name;
        }

    }
}
