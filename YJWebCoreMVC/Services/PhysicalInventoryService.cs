/*
 *  Created By Manoj 11/07/2025
 *  11/07/2025 Manoj Added AllStores,VenderTypes property and GetPhysicalTrays method
 *  11/11/2025 Manoj 11/11 Categories,Brands propertys Deletephysicaltray,Makematch methods
 *  11/19/2025 Manoj ShowPhyInvtbycode,GetStockByStyle Methods
 *  11/20/2025 Manoj Added DelPhyinvRec,AddNewPhysicalInv Methods
 *  12/22/2025 Manoj modified ShowPhyInvtbycode for data rendering issue
 */
using Microsoft.Data.SqlClient;
using System.Data;

namespace YJWebCoreMVC.Services
{
    public class PhysicalInventoryService
    {
        private readonly ConnectionProvider _connectionProvider;
        private readonly HelperCommonService _helperCommonService;

        public PhysicalInventoryService(ConnectionProvider connectionProvider, HelperCommonService helperCommonService)
        {
            _connectionProvider = connectionProvider;
            _helperCommonService = helperCommonService;
        }

        public DataTable GetPhysicalTrays(string Tray = "", string StoreCode = "")
        {
            if (Tray != "" && StoreCode != "")
                return _helperCommonService.GetSqlData(@"select distinct(tray) as Tray from STYLCOMP where tray=@tray and store=@storeno", "@storeno", StoreCode, "@tray", Tray);
            if (StoreCode != "")
                return _helperCommonService.GetSqlData(@"select distinct(tray) as Tray from STYLCOMP where store=@storeno", "@storeno", StoreCode);
            return _helperCommonService.GetSqlData(@"select distinct(tray) as Tray from STYLCOMP");
        }


        public DataTable Makematch(string txtStyleStartWith, string user, string store, bool includeLaywayShop = false, bool IslessthanPhysical = false, bool IsmorethanPhysical = false, bool IsnotinSystem = false, string category = "", string vendor = "", string brand = "", bool isStyleItem = false)
        {
            DataTable dataTable = new DataTable();
            using (SqlConnection connection = _connectionProvider.GetConnection())
            using (SqlCommand sqlCommand = new SqlCommand("MAKEPHYSICALINVENTORY", connection))
            {
                // Set command properties
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.CommandTimeout = 12000;

                // Add parameters to the command
                sqlCommand.Parameters.AddWithValue("@txtStyleStartWith", txtStyleStartWith);
                sqlCommand.Parameters.AddWithValue("@LOGGEDUSER", user);
                sqlCommand.Parameters.AddWithValue("@STORE", store);
                sqlCommand.Parameters.AddWithValue("@includeLaywayShop", includeLaywayShop);
                sqlCommand.Parameters.AddWithValue("@lessthanphysical", IslessthanPhysical ? 1 : 0);
                sqlCommand.Parameters.AddWithValue("@morethanphysical", IsmorethanPhysical ? 1 : 0);
                sqlCommand.Parameters.AddWithValue("@notinsystem", IsnotinSystem ? 1 : 0);
                sqlCommand.Parameters.AddWithValue("@category", category);
                sqlCommand.Parameters.AddWithValue("@vendor", vendor);
                sqlCommand.Parameters.AddWithValue("@brand", brand);
                sqlCommand.Parameters.AddWithValue("@IsStyleItem", isStyleItem);

                // Open connection and execute the command
                connection.Open();
                using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand))
                {
                    sqlDataAdapter.Fill(dataTable);
                }
            }
            return dataTable;
        }

        public void Deletephysicaltray(string bcode, string store)
        {
            if (bcode != "All")
                _helperCommonService.GetSqlData("delete FROM stylcomp where tray  = @CODE and store = @STORE", "@Store", store, "@code", bcode);
            else
                _helperCommonService.GetSqlData("delete FROM stylcomp where  store = @STORE", "@Store", store);
        }

        public DataTable ShowPhyInvtbycode(string levelcode, string store, bool isGlenn)
        {
            if (isGlenn)
                return _helperCommonService.GetSqlData("select distinct p.style as Style,st.fieldvalue5 as OLD_SKU, p.size as Size , ISNULL(cast(s.in_stock as decimal(8,2)),0) as Qty_In_Stock, cast(p.physical as decimal(8,2)) as Physical_Inventory,ISNULL(ST.[DESC],'') AS [DESC]  from [dbo].[STYLCOMP] p left join [dbo].[STOCK] S ON  S.STYLE = P.STYLE AND S.STORE_NO = P.STORE and  P.STYLE IS NOT NULL join styles st on st.style = p.style where p.TRAY= @levelcode and P.store = @store and cast(p.physical as decimal(12,2)) <> 0  group by p.style,p.size,s.in_stock,p.physical,st.fieldvalue5,ST.[DESC]", "@levelcode", levelcode, "@store", store);
            return _helperCommonService.GetSqlData("select distinct p.style as Style,p.size as Size , ISNULL(cast(s.in_stock as decimal(8,2)),0) as Qty_In_Stock, cast(p.physical as decimal(8,2)) as Physical_Inventory,ISNULL(ST.[DESC],'') AS [DESC]  from [dbo].[STYLCOMP] p left join [dbo].[STOCK] S ON  S.STYLE = P.STYLE AND S.STORE_NO = P.STORE and  P.STYLE IS NOT NULL left join styles st on st.style = p.style  where p.TRAY= @levelcode and P.store = @store and cast(p.physical as decimal(12,2)) <> 0  group by p.style,p.size,s.in_stock,p.physical,st.[desc]", "@levelcode", levelcode, "@store", store);
        }

        public DataRow GetStockByStyle(string style, string store)
        {
            return _helperCommonService.GetSqlRow("select in_stock from stock where style = @style and store_no = @store ",
                "@style", style, "@store", store);
        }

        public DataTable DelPhyinvRec(string tray, string style, string size, decimal qtyinstock, decimal physical, string store)
        {
            DataTable dataTable = new DataTable();
            using (SqlConnection connection = _connectionProvider.GetConnection())
            using (SqlCommand sqlCommand = new SqlCommand("DELETE FROM STYLCOMP WHERE TRAY = @tray AND STYLE = @style AND store = @store", connection))
            {
                // Set the command type to text (SQL query)
                sqlCommand.CommandType = CommandType.Text;

                // Add parameters to the command
                sqlCommand.Parameters.AddWithValue("@tray", tray);
                sqlCommand.Parameters.AddWithValue("@style", style);
                sqlCommand.Parameters.AddWithValue("@store", store);

                // Open the connection and execute the delete command
                connection.Open();
                sqlCommand.ExecuteNonQuery();
                return dataTable;
            }
        }
        public DataTable AddNewPhysicalInv(string plevel, string user, DataTable leveldet, string store)
        {
            DataTable dataTable = new DataTable();

            using (var connection = _connectionProvider.GetConnection())
            using (var sqlCommand = new SqlCommand("PHYSICALINVENT", connection))
            using (var sqlDataAdapter = new SqlDataAdapter(sqlCommand))
            {
                // Set up the command properties
                sqlCommand.CommandType = CommandType.StoredProcedure;

                // Add parameters to the command
                sqlCommand.Parameters.AddWithValue("@TRAY", plevel);
                sqlCommand.Parameters.AddWithValue("@LOGGEDUSER", user);
                sqlCommand.Parameters.AddWithValue("@STORE", store);
                sqlCommand.Parameters.AddWithValue("@TBLPHYSICALINVENTORY", leveldet);

                // Open the connection and fill the DataTable
                connection.Open();
                sqlDataAdapter.Fill(dataTable);
            }
            return dataTable;
        }
    }
}
