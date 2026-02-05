// Dharani 07/30/2025 Added StyleItems property, GetStoreInvetoryHist method
// Manoj   10/30/2025 Added CancelStoreInvnt method
// Lokesh  11/12/2025 Added GetStoreInvetoryReport() and Send2MultiStores();
// Hemanth 11/12/2025 Added editinventory
// Lokesh  11/28/2025 Added EditTransferLog();
// Hemanth   12/31/2025 Added getStoreInvData

using Microsoft.Data.SqlClient;
using System.Data;

namespace YJWebCoreMVC.Services
{
    public class StoreInvtService
    {
        private readonly ConnectionProvider _connectionProvider;
        private readonly HelperCommonService _helperCommonService;

        public StoreInvtService(ConnectionProvider connectionProvider, HelperCommonService helperCommonService)
        {
            _connectionProvider = connectionProvider;
            _helperCommonService = helperCommonService;
        }

        public DataTable GetStoreInvetoryHist(string storeNo, string styleNo, bool allStores, string fromDate, string toDate,
            bool allStyles, bool startWithStyle, bool inventoryAdjOnly, string username, bool isAdjByPhysicalInv)
        {
            DataTable dataTable = new DataTable();

            using (var connection = _connectionProvider.GetConnection())
            using (var command = new SqlCommand("GETSTOREHIST", connection))
            using (var adapter = new SqlDataAdapter(command))
            {
                command.CommandType = CommandType.StoredProcedure;

                // Add parameters explicitly
                command.Parameters.Add(new SqlParameter("@store_no", SqlDbType.VarChar) { Value = storeNo ?? (object)DBNull.Value });
                command.Parameters.Add(new SqlParameter("@style", SqlDbType.VarChar) { Value = styleNo ?? (object)DBNull.Value });
                command.Parameters.Add(new SqlParameter("@lallstores", SqlDbType.Bit) { Value = allStores });
                command.Parameters.Add(new SqlParameter("@fromdate", SqlDbType.VarChar) { Value = string.IsNullOrEmpty(fromDate) ? (object)DBNull.Value : fromDate });
                command.Parameters.Add(new SqlParameter("@todate", SqlDbType.VarChar) { Value = string.IsNullOrEmpty(toDate) ? (object)DBNull.Value : toDate });
                command.Parameters.Add(new SqlParameter("@allstyles", SqlDbType.Bit) { Value = allStyles });
                command.Parameters.Add(new SqlParameter("@StartWithstyle", SqlDbType.Bit) { Value = startWithStyle });
                command.Parameters.Add(new SqlParameter("@Inventoryadjony", SqlDbType.Bit) { Value = inventoryAdjOnly });
                command.Parameters.Add(new SqlParameter("@Username", SqlDbType.VarChar) { Value = username ?? (object)DBNull.Value });
                command.Parameters.Add(new SqlParameter("@IsAdj_CauseByPhysicalInv", SqlDbType.Bit) { Value = isAdjByPhysicalInv });

                // Open connection and fill the DataTable
                connection.Open();
                adapter.Fill(dataTable);
            }

            return dataTable;
        }

        public bool CancelStoreInvnt(string logno, string loggeduser)
        {
            using (var connection = _connectionProvider.GetConnection())
            using (var dbCommand = new SqlCommand("CANCELSTORINVT", connection))
            {
                dbCommand.CommandType = CommandType.StoredProcedure;
                dbCommand.Parameters.Add(new SqlParameter("@CLOG", SqlDbType.VarChar) { Value = logno });
                dbCommand.Parameters.Add(new SqlParameter("@LOGGEDUSER", SqlDbType.VarChar) { Value = loggeduser });

                connection.Open();
                int rowsAffected = dbCommand.ExecuteNonQuery();
                return rowsAffected > 0; // Return true if rows are affected.
            }
        }

        public DataTable Send2MultiStores(string data1, bool isAck, string frmStore, string loggedUser, string dateOfSend, bool isStyleItem)
        {
            DataTable dataTable = new DataTable();

            using (var connection = _connectionProvider.GetConnection())
            using (var command = new SqlCommand("INVNT2MULTISTORE", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandTimeout = 6000;

                command.Parameters.AddWithValue("@str1XmlData", data1 ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@ISACK", isAck);
                command.Parameters.AddWithValue("@cFrmStore", frmStore ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@loggeduser", loggedUser ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@DateofSend", string.IsNullOrEmpty(dateOfSend) ? (object)DBNull.Value : dateOfSend);
                command.Parameters.AddWithValue("@IsStyleItem", isStyleItem);

                using (var adapter = new SqlDataAdapter(command))
                {
                    connection.Open();
                    adapter.Fill(dataTable);
                }
            }
            return dataTable;
        }
        public DataTable GetStoreInvetoryReport(string inv_no)
        {
            return _helperCommonService.GetStoreProc("GetStoreInvntRep", "@inv_no", inv_no);
        }

        public DataTable editinventory(DataTable inventoryData, string loggedUser, string storeNo, bool withoutGL = false, bool isStyleItem = false)
        {

            using (var connection = _connectionProvider.GetConnection())
            using (var command = new SqlCommand("EditStockInventoryData", connection))
            using (var adapter = new SqlDataAdapter(command))
            {
                command.CommandType = CommandType.StoredProcedure;

                // Add parameters with null handling
                command.Parameters.AddWithValue("@TBLEDITINVENTORYITEMS", inventoryData);
                command.Parameters.AddWithValue("@LOGGEDUSER", loggedUser);
                command.Parameters.AddWithValue("@CSTORE", storeNo);

                //if (withoutGL)
                command.Parameters.AddWithValue("@WITHOUT_GL", withoutGL);

                command.Parameters.AddWithValue("@isStyleItem", isStyleItem);

                // Fill and return DataTable
                var dataTable = new DataTable();
                adapter.Fill(dataTable);
                return dataTable;
            }
        }

        public bool EditTransferLog(string tableXml, string cLog, string loggedUser, bool lDoGlTran, string dateOfSend,
           bool isStyleItem)
        {
            using (var connection = _connectionProvider.GetConnection())
            using (var command = new SqlCommand("EDITSTORETRANSFER", connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add(new SqlParameter("@str1XmlData", SqlDbType.Xml) { Value = tableXml });
                command.Parameters.Add(new SqlParameter("@cLog", SqlDbType.VarChar) { Value = cLog });
                command.Parameters.Add(new SqlParameter("@loggeduser", SqlDbType.VarChar) { Value = loggedUser });
                command.Parameters.Add(new SqlParameter("@lDoGlTran", SqlDbType.Bit) { Value = lDoGlTran });
                command.Parameters.Add(new SqlParameter("@DateofSend", SqlDbType.VarChar)
                {
                    Value = string.IsNullOrEmpty(dateOfSend) ? (object)DBNull.Value : dateOfSend
                });
                command.Parameters.Add(new SqlParameter("@IsStyleItem", SqlDbType.Bit) { Value = isStyleItem });

                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }

        //public DataSet getStoreInvData(InventoryForMassedit inventorymodel, StylesAttributeForMassedit modStyleAtr, string cstore, bool ignorein_Shop = false, bool separateSNFromModel = false, bool IsprintLongdesc = false)
        //{
        //    DataSet dataSet = new DataSet();

        //    using (SqlDataAdapter SqlDataAdapter = new SqlDataAdapter())
        //    {
        //        // Create the command and set its properties
        //        SqlDataAdapter.SelectCommand = new SqlCommand();
        //        SqlDataAdapter.SelectCommand.Connection = _connectionProvider.GetConnection();
        //        SqlDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;

        //        // Assign the SQL to the command object
        //        SqlDataAdapter.SelectCommand.CommandText = "PRINTSTOREINVENTORYDATA";
        //        SqlDataAdapter.SelectCommand.CommandTimeout = 6000;

        //        SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@CATEGORY", inventorymodel.category);
        //        SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@SUBCATEGORY", inventorymodel.subcategories);
        //        SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@METAL", inventorymodel.metal);
        //        SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@VENDORS", inventorymodel.vendors);
        //        SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@BRAND", inventorymodel.brand);
        //        SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@SUBBRAND", inventorymodel.subbrand);
        //        SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@SHAPE", inventorymodel.shape);
        //        SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@CTYPE", inventorymodel.ctype);
        //        SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@STYLEFROM", inventorymodel.fromstyle);
        //        SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@STYLETO", inventorymodel.tostyle);
        //        SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@BELOWMINSTOCK", inventorymodel.selectedminstock);
        //        SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@PRINTBYSIZE", inventorymodel.printbysize);
        //        SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@PRINTBYSUMMERY", inventorymodel.printbysummery);
        //        SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@PRINTBYSUMMERYMODEL", inventorymodel.printbysummerymodel);
        //        SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@STOCKTYPE", inventorymodel.displaystocktype);
        //        SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@ALLSTOCK", inventorymodel.displayallstock);
        //        SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@INCLUDEINSTOCK", inventorymodel.displayincludeinstock);
        //        SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@INCLUDESALESMANINVENTORY", inventorymodel.displayincludesalesmaninventory);
        //        SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@ITEMSONMEMO", inventorymodel.displayitemsonmemo);
        //        SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@DESC_INCLUDE", inventorymodel.descriptionInclude);
        //        SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@COLOR", inventorymodel.COLOR);
        //        SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@CLARITY", inventorymodel.CLARITY);
        //        SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@MOUNTED", inventorymodel.MOUNTED);
        //        SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@NONMOUNTED", inventorymodel.NONMOUNTED);
        //        SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@WtFrom", inventorymodel.WtFrom);
        //        SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@WtThru", inventorymodel.WtThru);
        //        SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@GLCLASS", inventorymodel.GLCLASS);
        //        SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@SelectedStore", cstore);
        //        SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@Group", inventorymodel.group ?? "");
        //        SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@ignorein_Shop", ignorein_Shop);
        //        SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@CntStnSizeFrom", inventorymodel.cntStnSizeFrm);
        //        SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@CntStnSizeTo", inventorymodel.cntStnSizeTo);
        //        SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@VndStyle", inventorymodel.vnd_style);
        //        SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@strItemType", inventorymodel.strItemType ?? "");
        //        SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@IsNegative", inventorymodel.IsNegative);
        //        SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@IsReplacementCost", inventorymodel.IsReplacementCost);
        //        SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@fdate", inventorymodel.fdate);
        //        SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@tdate", inventorymodel.tdate);
        //        SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@DWtFrom", inventorymodel.DWtFrom);
        //        SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@DWtThru", inventorymodel.DWtThru);
        //        SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@CASE_NO", inventorymodel.CASE_NO);
        //        SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@CentSubType", inventorymodel.CentSubtype);
        //        SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@IncludeDiscontinued", inventorymodel.IncludeDiscontinued);
        //        SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@TRAY_NO", inventorymodel.TRAY_NO);
        //        SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@PartS", inventorymodel.Parts);
        //        SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@SubPart", inventorymodel.SubParts);

        //        if (modStyleAtr != null)
        //        {
        //            SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@Attrib1", modStyleAtr.Attrib1);
        //            SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@Attrib2", modStyleAtr.Attrib2);
        //            SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@Attrib3", modStyleAtr.Attrib3);
        //            SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@Attrib4", modStyleAtr.Attrib4);
        //            SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@Attrib5", modStyleAtr.Attrib5);
        //            SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@Attrib6", modStyleAtr.Attrib6);
        //            SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@Attrib7", modStyleAtr.Attrib7);
        //            SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@Attrib8", modStyleAtr.Attrib8);
        //            SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@Attrib9", modStyleAtr.Attrib9);
        //            SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@Attrib10", modStyleAtr.Attrib10);
        //            SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@Attrib11", modStyleAtr.Attrib11);
        //            SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@Attrib12", modStyleAtr.Attrib12);

        //            SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@Attrib_13", modStyleAtr.Attrib_13);
        //            SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@Match_13", modStyleAtr.Match13);

        //            SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@Attrib_14", modStyleAtr.Attrib_14);
        //            SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@Match_14", modStyleAtr.Match14);

        //            SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@Attrib_15", modStyleAtr.Attrib_15);
        //            SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@Match_15", modStyleAtr.Match15);

        //            SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@Attrib_16", modStyleAtr.Attrib_16);
        //            SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@Match_16", modStyleAtr.Match16);

        //            SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@Attrib_17", modStyleAtr.Attrib_17);
        //            SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@Match_17", modStyleAtr.Match17);

        //            SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@Attrib_18", modStyleAtr.Attrib_18);
        //            SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@Match_18", modStyleAtr.Match18);

        //            SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@Attrib_19", modStyleAtr.Attrib_19);
        //            SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@Attrib_20", modStyleAtr.Attrib_20);
        //            SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@Attrib_21", modStyleAtr.Attrib_21);

        //            SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@fieldvalue4", modStyleAtr.fieldvalue4);
        //            SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@fieldvalue5", modStyleAtr.fieldvalue5);

        //            SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@fieldvalue6", modStyleAtr.fieldvalue6);
        //            SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@fieldvalue7", modStyleAtr.fieldvalue7);
        //            SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@fieldvalue8", modStyleAtr.fieldvalue8);

        //            SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@intchkattr1", modStyleAtr.intchkattr1);
        //            SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@intchkattr2", modStyleAtr.intchkattr2);
        //            SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@intchkattr3", modStyleAtr.intchkattr3);
        //            SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@intchkattr4", modStyleAtr.intchkattr4);
        //            SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@intchkattr5", modStyleAtr.intchkattr5);
        //            SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@intchkattr6", modStyleAtr.intchkattr6);
        //        }
        //        SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@SeparateSNFromModel", separateSNFromModel);
        //        SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@longdesc", IsprintLongdesc);
        //        SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@QuickSearch", inventorymodel.QuickSearch);
        //        SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@Storecodeforinstock", inventorymodel.storeno);
        //        SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@SummryVndrstyle", inventorymodel.SummryVndrstyle);

        //        SqlDataAdapter.Fill(dataSet);
        //    }
        //    return dataSet;
        //}
    }
}
