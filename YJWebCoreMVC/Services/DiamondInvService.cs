/*
 *  Created By Phanindra
 *chakri 06/17/2026 created new Model.
 *chakri  06/17/2026 Added DelALotNO method.
 *chakri  06/25/2025 Added GetStyles and RenameALot methods.
 *chakri  07/04/2025 Added some properties for list.
 *chakri  07/08/2025 Added PrintTagsAddStock method.
 *Manoj   07/29/2025 Added StyleItems property.
 *Dharani 08/05/2025 Added GetStyleHistory method.
 *Dharani 11/17/2025 Added STYLEATTRTEXT property.
 *Dharani 11/20/2025 Added Edit Style Attributes related properties.
 *Chakri  12/17/2025 Added QuickAddMultipleStyles,GetDropdownForCategory, GetDropdownForMetal, CheckValidVendorCode, and GetStyleByCode methods.
 *Phanindra 12/26/2025 Added UpdateRapPrice method.
 */
using Microsoft.Data.SqlClient;
using System.Data;
using YJWebCoreMVC.Models;

namespace YJWebCoreMVC.Services
{
    public class DiamondInvService
    {
        private readonly ConnectionProvider _connectionProvider;
        private readonly HelperCommonService _helperCommonService;

        public DiamondInvService(ConnectionProvider connectionProvider, HelperCommonService helperCommonService, IWebHostEnvironment env)
        {
            _connectionProvider = connectionProvider;
            _helperCommonService = helperCommonService;
        }


        public string DelALotNO(string lotCode)
        {
            using (var connection = _connectionProvider.GetConnection())
            using (var command = new SqlCommand("DelALotNO", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@LOTNO", SqlDbType.NVarChar, 100) { Value = lotCode });
                var returnParameter = new SqlParameter("@RETVAL", SqlDbType.NVarChar, 100)
                {
                    Direction = ParameterDirection.Output
                };
                command.Parameters.Add(returnParameter);
                command.CommandTimeout = 6000;
                connection.Open();
                command.ExecuteNonQuery();
                return returnParameter.Value.ToString();
            }
        }

        public DataTable GetDiamondInventoryData(DiamondInvModel DiamondInventoryModelData)
        {
            var dataTable = new DataTable();

            using (var connection = _connectionProvider.GetConnection())
            using (var command = new SqlCommand("PrintDiamondInventoryData", connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                // Add parameters with explicit type and size
                command.Parameters.Add(new SqlParameter("@vendors", SqlDbType.NVarChar, 100) { Value = DiamondInventoryModelData.vendors ?? string.Empty });
                command.Parameters.Add(new SqlParameter("@shape", SqlDbType.NVarChar, 50) { Value = DiamondInventoryModelData.shape ?? string.Empty });
                command.Parameters.Add(new SqlParameter("@fromstyle", SqlDbType.NVarChar, 50) { Value = DiamondInventoryModelData.fromstyle ?? string.Empty });
                command.Parameters.Add(new SqlParameter("@tostyle", SqlDbType.NVarChar, 50) { Value = DiamondInventoryModelData.tostyle ?? string.Empty });
                command.Parameters.Add(new SqlParameter("@COLOR", SqlDbType.NVarChar, 50) { Value = DiamondInventoryModelData.COLOR ?? string.Empty });
                command.Parameters.Add(new SqlParameter("@printbysize", SqlDbType.Bit) { Value = DiamondInventoryModelData.printbysize });
                command.Parameters.Add(new SqlParameter("@printbysummery", SqlDbType.Bit) { Value = DiamondInventoryModelData.printbysummery });
                command.Parameters.Add(new SqlParameter("@CLARITY", SqlDbType.NVarChar, 50) { Value = DiamondInventoryModelData.CLARITY ?? string.Empty });
                command.Parameters.Add(new SqlParameter("@displayallstock", SqlDbType.Bit) { Value = DiamondInventoryModelData.displayallstock });
                command.Parameters.Add(new SqlParameter("@displayincludeinstock", SqlDbType.Bit) { Value = DiamondInventoryModelData.displayincludeinstock });
                command.Parameters.Add(new SqlParameter("@displayitemsonmemo", SqlDbType.Bit) { Value = DiamondInventoryModelData.displayitemsonmemo });
                command.Parameters.Add(new SqlParameter("@descriptionInclude", SqlDbType.NVarChar, 100) { Value = DiamondInventoryModelData.descriptionInclude ?? string.Empty });
                command.Parameters.Add(new SqlParameter("@WtFrom", SqlDbType.Decimal) { Value = DiamondInventoryModelData.WtFrom });
                command.Parameters.Add(new SqlParameter("@WtThru", SqlDbType.Decimal) { Value = DiamondInventoryModelData.WtThru });
                command.Parameters.Add(new SqlParameter("@PriceFrom", SqlDbType.Decimal) { Value = DiamondInventoryModelData.PriceFrom });
                command.Parameters.Add(new SqlParameter("@PriceThru", SqlDbType.Decimal) { Value = DiamondInventoryModelData.PriceThru });
                command.Parameters.Add(new SqlParameter("@GLCLASS", SqlDbType.NVarChar, 50) { Value = DiamondInventoryModelData.GLCLASS ?? string.Empty });
                command.Parameters.Add(new SqlParameter("@CERT_TYPE", SqlDbType.NVarChar, 50) { Value = DiamondInventoryModelData.CERT_TYPE ?? string.Empty });
                command.Parameters.Add(new SqlParameter("@CERT_NO", SqlDbType.NVarChar, 50) { Value = DiamondInventoryModelData.CERT_NO ?? string.Empty });
                command.Parameters.Add(new SqlParameter("@StoreNoFilter", SqlDbType.NVarChar, 50) { Value = DiamondInventoryModelData.StoreNo ?? string.Empty });
                command.Parameters.Add(new SqlParameter("@IsMountedDiamond", SqlDbType.Bit) { Value = DiamondInventoryModelData.IsMountedDiamond });
                command.Parameters.Add(new SqlParameter("@IsPrintRapPrice", SqlDbType.Bit) { Value = DiamondInventoryModelData.printrapprice });
                command.Parameters.Add(new SqlParameter("@IncludeDiscontinued", SqlDbType.Bit) { Value = DiamondInventoryModelData.IncludeDiscontinued });

                using (var adapter = new SqlDataAdapter(command))
                {
                    // Fill the DataTable
                    adapter.Fill(dataTable);
                }

            }
            return dataTable;
        }
        public DataTable GetStyles()
        {
            return _helperCommonService.GetSqlData("select style from styles with (nolock) order by style");
        }

        public string RenameALot(string oldLotCode, string newLotCode, string loggedUser = "", bool isFromCombine2Style = false, string storeNo = "")
        {
            try
            {
                using (var connection = _connectionProvider.GetConnection())
                using (var command = new SqlCommand("RenameLot", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@OLDACC", SqlDbType.NVarChar, 100) { Value = oldLotCode });
                    command.Parameters.Add(new SqlParameter("@NEWACC", SqlDbType.NVarChar, 100) { Value = newLotCode });
                    command.Parameters.Add(new SqlParameter("@LoggedUser", SqlDbType.NVarChar, 100) { Value = loggedUser });
                    command.Parameters.Add(new SqlParameter("@iSFromCombine2Style", SqlDbType.Bit) { Value = isFromCombine2Style });
                    command.Parameters.Add(new SqlParameter("@StoreNo", SqlDbType.NVarChar, 100) { Value = storeNo });
                    var returnParameter = new SqlParameter("@RETVAL", SqlDbType.NVarChar, 100)
                    {
                        Direction = ParameterDirection.Output
                    };
                    command.Parameters.Add(returnParameter);
                    command.CommandTimeout = 3000;
                    connection.Open();
                    command.ExecuteNonQuery();
                    return returnParameter.Value.ToString();
                }

            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }
        public DataTable PrintTagsAddStock(string user, string levelDet, string storeName, bool isStyleItem = false)
        {
            var dataTable = new DataTable();

            using (var connection = _connectionProvider.GetConnection())
            using (var command = new SqlCommand("PRINTTAGSADDSTOCK", connection))
            using (var adapter = new SqlDataAdapter(command))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@LOGGEDUSER", SqlDbType.NVarChar, 50) { Value = user ?? string.Empty });
                command.Parameters.Add(new SqlParameter("@STORENAME", SqlDbType.NVarChar, 100) { Value = storeName ?? string.Empty });
                command.Parameters.Add(new SqlParameter("@isStyleItem", SqlDbType.Bit) { Value = isStyleItem });
                var levelDetParam = new SqlParameter("@TBLPRINTTAGADDSTOCK", SqlDbType.Xml)
                {
                    Value = levelDet
                };
                command.Parameters.Add(levelDetParam);
                adapter.Fill(dataTable);
            }
            return dataTable;
        }
        public DataTable GetStyleHistory(string lcode)
        {
            return _helperCommonService.GetSqlData(@"select * From STK_HIST with (nolock) Where STYLE= @STYLE  order by date", "@STYLE", lcode);
        }
        public bool QuickAddMultipleStyles(string STYLES, string STORE, string LOGGEDUSER = "", int NEXTROUNDOFF = 0)
        {
            using (SqlConnection connection = _connectionProvider.GetConnection())
            using (SqlCommand dbCommand = new SqlCommand("QuickAddMultipleStyles", connection))
            {
                dbCommand.CommandType = CommandType.StoredProcedure;
                dbCommand.Parameters.AddWithValue("@STYLES", STYLES);
                dbCommand.Parameters.AddWithValue("@STORE", STORE);
                dbCommand.Parameters.AddWithValue("@LOGGEDUSER", LOGGEDUSER);
                dbCommand.Parameters.AddWithValue("@NEXTROUNDOFF", NEXTROUNDOFF);
                connection.Open();
                return dbCommand.ExecuteNonQuery() > 0;
            }
        }
        public DataTable GetDropdownForCategory(string tblName, string fldName)
        {
            return _helperCommonService.GetSqlData("SELECT '' AS [" + fldName + "] UNION SELECT DISTINCT [" + fldName + "] FROM [" + tblName + "] WHERE ISNULL([" + fldName + "],'') <> '' ORDER BY [" + fldName + "]");
        }

        public DataTable GetDropdownForMetal(string tblName, string fldName)
        {
            return _helperCommonService.GetSqlData("SELECT '' AS [" + fldName + "] UNION SELECT DISTINCT [" + fldName + "] FROM [" + tblName + "] WHERE ISNULL([" + fldName + "],'') <> '' ORDER BY [" + fldName + "]");
        }
        public DataRow CheckValidVendorCode(string acc)
        {
            return _helperCommonService.GetSqlRow("select * From vendors with (nolock) Where acc=@acc or oldvendorcode=@acc order by acc", "@acc", acc.Trim());
        }

        public DataRow GetStyleByCode(string fcode, bool vstyle = false)
        {
            if (fcode.IndexOf('%') > 0)
                return _helperCommonService.GetSqlRow(string.Format("select top 1 * from styles with (nolock) where style like '{0}' order by style", fcode.Replace("'", "''")));
            if (vstyle)
                return _helperCommonService.GetSqlRow(string.Format("SELECT top 1 * FROM STYLES S with (nolock) LEFT JOIN STYLES2 S2 with (nolock) ON S.STYLE = S2.STYLE WHERE S.style = '{0}' or vnd_style='{0}' order by date desc", fcode.Replace("'", "''")));
            return _helperCommonService.GetSqlRow(string.Format("SELECT top 1 * FROM STYLES S with (nolock) LEFT JOIN STYLES2 S2 with (nolock) ON S.STYLE = S2.STYLE WHERE S.style = '{0}' order by date desc", fcode.Replace("'", "''")));
        }

        public DataRow UpdateRapPrice(string shape, string color, string clarity, decimal weightStock)
        {
            using (var connection = _connectionProvider.GetConnection())
            using (var command = new SqlCommand("UpdateRapPrice", connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                // Explicitly define parameters
                command.Parameters.Add(new SqlParameter("@shape", SqlDbType.VarChar, 50) { Value = shape });
                command.Parameters.Add(new SqlParameter("@color", SqlDbType.VarChar, 50) { Value = color });
                command.Parameters.Add(new SqlParameter("@clarity", SqlDbType.VarChar, 50) { Value = clarity });
                command.Parameters.Add(new SqlParameter("@wt_stock", SqlDbType.Decimal) { Value = weightStock });

                using (var adapter = new SqlDataAdapter(command))
                {
                    var dataTable = new DataTable();

                    // Fill the table from the adapter
                    adapter.Fill(dataTable);

                    // Get the DataRow from the table
                    return _helperCommonService.GetRowOne(dataTable);
                }
            }
        }



    }
}
