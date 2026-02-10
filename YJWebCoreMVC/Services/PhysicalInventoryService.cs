/*
 *  Created By Manoj 11/07/2025
 *  Manoj 11/07/2025 Added AllStores,VenderTypes property and GetPhysicalTrays method
 *  Manoj 11/11/2025 11/11 Categories,Brands propertys Deletephysicaltray,Makematch methods
 *  Manoj 11/19/2025 ShowPhyInvtbycode,GetStockByStyle Methods
 *  Manoj 11/20/2025 Added DelPhyinvRec,AddNewPhysicalInv Methods
 *  Manoj 12/22/2025 modified ShowPhyInvtbycode for data rendering issue 
 *  Manoj 02/05/2026 Added isValidStyle,PrintPhysicalInventory,IsValidTray,gettraydetailsfromstores,ComparePhysicalAsOfDate,ValidatePhysicalInventoryImport,MakematchPhysicalAsofdate,ValidatePhysicalInventoryImport Methods

 */

using Microsoft.Data.SqlClient;
using System.Data;

namespace YJWebCoreMVC.Services
{
    public class PhysicalInventoryService
    {
        private readonly ConnectionProvider _connectionProvider;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly HelperCommonService _helperCommonService;
        private readonly IWebHostEnvironment _env;
        private readonly string _companyName;
        private readonly string _storeCodeInUse;
        private readonly string _loggedUser;

        public PhysicalInventoryService(ConnectionProvider connectionProvider, IHttpContextAccessor httpContextAccessor, HelperCommonService helperCommonService, IWebHostEnvironment env)
        {
            _connectionProvider = connectionProvider;
            _httpContextAccessor = httpContextAccessor;
            _helperCommonService = helperCommonService;
            _companyName = _httpContextAccessor.HttpContext?.Session.GetString("COMPANYNAME");
            _storeCodeInUse = _httpContextAccessor.HttpContext?.Session.GetString("STORE_CODE");
            _loggedUser = _httpContextAccessor.HttpContext?.Session.GetString("UserId");
            _env = env;
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

        public bool isValidStyle(string Style)
        {
            return _helperCommonService.DataTableOK(_helperCommonService.GetSqlData("SELECT style FROM STYLES with (nolock) WHERE STYLE = @Style", "@Style", Style.Trim()));
        }

        public DataTable Getphysicalinventorymatched(string style)
        {
            return _helperCommonService.GetSqlData("select Date,style,qty,store from [matched] where (style=@style or @style='')", "@style", style);
        }

        public DataTable PrintPhysicalInventory(
            string tray, string txtStyleStartWith, bool summary, bool discrepancy, string store,
            bool includeLaywayShop = false, bool IslessthanPhysical = false, bool IsmorethanPhysical = false,
            bool IsnotinSystem = false, bool isMatchingPhy = false, string category = "", string brand = "", string vendor = "")
        {
            DataTable dataTable = new DataTable();
            string _filter = " WHERE 1=1 ";

            // Use a list to collect filter conditions to avoid redundant "OR" and "AND" handling
            List<string> filterConditions = new List<string>();

            if (IslessthanPhysical) filterConditions.Add("Type=1");
            if (IsmorethanPhysical) filterConditions.Add("Type=2");
            if (IsnotinSystem) filterConditions.Add("Type=3");
            if (isMatchingPhy) filterConditions.Add("Type=4");

            if (filterConditions.Count > 0)
                _filter += "AND (" + string.Join(" OR ", filterConditions) + ")";

            using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter())
            using (SqlCommand command = new SqlCommand("CompareAndPrint_Physical", new SqlConnection(_connectionProvider.GetConnectionString())))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandTimeout = 3000;

                command.Parameters.AddWithValue("@TRAY", tray);
                command.Parameters.AddWithValue("@StyleStartsWith", txtStyleStartWith);
                command.Parameters.AddWithValue("@store", store);
                command.Parameters.AddWithValue("@Summary", summary);
                command.Parameters.AddWithValue("@discrepancy", discrepancy);
                command.Parameters.AddWithValue("@includeLaywayShop", includeLaywayShop);
                command.Parameters.AddWithValue("@IslessthanPhysical", IslessthanPhysical);
                command.Parameters.AddWithValue("@IsmorethanPhysical", IsmorethanPhysical);
                command.Parameters.AddWithValue("@IsnotinSystem", IsnotinSystem);
                command.Parameters.AddWithValue("@isMatchingPhy", isMatchingPhy);
                command.Parameters.AddWithValue("@Category", category);
                command.Parameters.AddWithValue("@brand", brand);
                command.Parameters.AddWithValue("@vendor", vendor);
                command.Parameters.AddWithValue("@Filter", _filter);
                sqlDataAdapter.SelectCommand = command;
                sqlDataAdapter.Fill(dataTable);
            }
            return dataTable;
        }
        public bool IsValidTray(string store, string tray)
        {
            DataTable dataTable = _helperCommonService.GetSqlData("select tray from stylcomp where store ='" + store + "' and tray='" + tray.Trim() + "'");
            return _helperCommonService.DataTableOK(dataTable);
        }

        public DataTable gettraydetailsfromstores(string storename, bool IsloadValid = false)
        {
            if (!IsloadValid)
                return _helperCommonService.GetSqlData("select distinct tray from stylcomp where store='" + storename + "'  order by tray asc");

            return _helperCommonService.GetSqlData("select distinct tray from stylcomp where cast(physical as decimal(12,2)) <> 0 and store='" + storename + "'  order by tray asc");
        }


        public DataTable ComparePhysicalAsOfDate(string tray, string txtStyleStartWith, bool summary, bool discrepancy,
            string store, bool includeLaywayShop, bool IslessthanPhysical, bool IsmorethanPhysical, bool IsnotinSystem,
            bool isMatchingPhy, string category, string brand, string vendor, string asofdate, decimal price1, decimal price2)
        {
            DataTable dataTable = new DataTable();

            // Using a single 'using' block for connection and command
            using (var connection = new SqlConnection(_connectionProvider.GetConnectionString()))
            using (var command = new SqlCommand("ComparePhysicalAsOfDate", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandTimeout = 3000;

                // Adding parameters
                command.Parameters.AddWithValue("@TRAY", tray);
                command.Parameters.AddWithValue("@StyleStartsWith", txtStyleStartWith);
                command.Parameters.AddWithValue("@store", store);
                command.Parameters.AddWithValue("@Summary", summary);
                command.Parameters.AddWithValue("@discrepancy", discrepancy);
                command.Parameters.AddWithValue("@includeLaywayShop", includeLaywayShop);
                command.Parameters.AddWithValue("@IslessthanPhysical", IslessthanPhysical);
                command.Parameters.AddWithValue("@IsmorethanPhysical", IsmorethanPhysical);
                command.Parameters.AddWithValue("@IsnotinSystem", IsnotinSystem);
                command.Parameters.AddWithValue("@isMatchingPhy", isMatchingPhy);
                command.Parameters.AddWithValue("@Category", category);
                command.Parameters.AddWithValue("@brand", brand);
                command.Parameters.AddWithValue("@vendor", vendor);
                command.Parameters.AddWithValue("@asofdate", asofdate);
                command.Parameters.AddWithValue("@price1", price1);
                command.Parameters.AddWithValue("@price2", price2);

                // Open connection and execute the query
                connection.Open();
                using (var sqlDataAdapter = new SqlDataAdapter(command))
                {
                    sqlDataAdapter.Fill(dataTable);
                }
            }
            return dataTable;
        }


        public DataTable ValidatePhysicalInventoryImport(string data, bool IsDupilcateCheck = false, string store = "", bool ImportBars = false)
        {
            return _helperCommonService.GetStoreProc("GetPhysicalInvntImpdata", "@dtData", data, "@IsDupCheck", IsDupilcateCheck.ToString(), "@Store", store, "@ImportBarcodes", ImportBars ? "1" : "0");
        }


        public DataTable MakematchPhysicalAsofdate(string txtStyleStartWith, string user, string store,
          bool includeLaywayShop, bool IslessthanPhysical, bool IsmorethanPhysical, bool IsnotinSystem,
          string categories, bool lAllCats, string vendor, string brand, decimal price1, decimal price2, string asofdate)
        {
            DataTable dataTable = new DataTable();

            using (SqlConnection conn = new SqlConnection(_connectionProvider.GetConnectionString()))
            using (SqlCommand cmd = new SqlCommand("MatchPhysicalInventoryAsOfDate", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 12000; // Reduce timeout unless required

                // Add parameters with explicit types
                cmd.Parameters.Add(new SqlParameter("@txtStyleStartWith", SqlDbType.Xml)).Value = txtStyleStartWith;
                cmd.Parameters.Add(new SqlParameter("@LOGGEDUSER", SqlDbType.VarChar, 50)).Value = user;
                cmd.Parameters.Add(new SqlParameter("@STORE", SqlDbType.VarChar, 50)).Value = store;
                cmd.Parameters.Add(new SqlParameter("@includeLaywayShop", SqlDbType.Bit)).Value = includeLaywayShop;
                cmd.Parameters.Add(new SqlParameter("@lessthanphysical", SqlDbType.Bit)).Value = IslessthanPhysical;
                cmd.Parameters.Add(new SqlParameter("@morethanphysical", SqlDbType.Bit)).Value = IsmorethanPhysical;
                cmd.Parameters.Add(new SqlParameter("@notinsystem", SqlDbType.Bit)).Value = IsnotinSystem;
                cmd.Parameters.Add(new SqlParameter("@categories", SqlDbType.Xml)).Value = categories;
                cmd.Parameters.Add(new SqlParameter("@AllCats", SqlDbType.Bit)).Value = lAllCats;
                cmd.Parameters.Add(new SqlParameter("@vendor", SqlDbType.VarChar, 100)).Value = vendor;
                cmd.Parameters.Add(new SqlParameter("@brand", SqlDbType.VarChar, 100)).Value = brand;
                cmd.Parameters.Add(new SqlParameter("@price1", SqlDbType.Decimal)).Value = price1;
                cmd.Parameters.Add(new SqlParameter("@price2", SqlDbType.Decimal)).Value = price2;
                cmd.Parameters.Add(new SqlParameter("@asofdate", SqlDbType.VarChar, 30)).Value = asofdate;
                cmd.Parameters.Add(new SqlParameter("@IsStyleItem", SqlDbType.Bit)).Value = _helperCommonService.is_StyleItem;

                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                    dataTable.Load(reader);
            }
            return dataTable;
        }
    }
}
