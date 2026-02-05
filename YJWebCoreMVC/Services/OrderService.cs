/*
 *  Created By Dharani on 12-Jan-2026
 *  Dharani 01/21/2026 Added ListofCustPO method and ListOfPO class.
 *  Dharani 01/22/2026 Added GetOrderType
 *  Dharani 01/26/2026 Added GetOrderbyCustPO, GetUpsTrakByPON, GetCastOrderedByPON and GetRejectedByPON methods.
 *  Manoj   01/26/2026 Added AddOrderType,UpdateOrderType
 *  Manoj   01/30/2026 Added OrderStyleTracking method
 */

using Microsoft.Data.SqlClient;
using System.Data;

namespace YJWebCoreMVC.Services
{
    public class OrderService
    {

        private readonly ConnectionProvider _connectionProvider;
        private readonly HelperCommonService _helperCommonService;

        public OrderService(ConnectionProvider connectionProvider, HelperCommonService helperCommonService)
        {
            _connectionProvider = connectionProvider;
            _helperCommonService = helperCommonService;
        }

        public DataTable CheckValidOrder(string pon)
        {
            return _helperCommonService.GetSqlData("select top 1 * from orders where pon like '%" + pon + "'");
        }

        public DataTable GetPOReportOrderItemsByPON(string pon, string orderBy = "", int sortBy = 0, bool isTorkiaPODraft = false, bool isPrintMsrp = false, bool isVibhor = false)
        {
            DataTable dataTable = new DataTable();

            // Using `using` to ensure proper disposal of resources for connection and command
            using (var connection = _connectionProvider.GetConnection())
            using (var command = new SqlCommand("GetPOReportOrderItemsByPON", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandTimeout = 3000; // Adjust the timeout as needed

                // Add parameters to the command
                command.Parameters.AddWithValue("@PON", pon);
                command.Parameters.AddWithValue("@OrderBy", orderBy);
                command.Parameters.AddWithValue("@SortBy", sortBy);
                command.Parameters.AddWithValue("@PrintMsrp", isPrintMsrp);

                // Open the connection and execute the command
                connection.Open();

                // Use ExecuteReader and load data directly into DataTable
                using (var reader = command.ExecuteReader())
                    dataTable.Load(reader);  // Efficiently load data into DataTable
            }
            return dataTable;
        }
        public DataTable ListofCustPO(string acc1, string acc2, int dateval, DateTime? date1, DateTime? date2, bool openonly, bool showdetail, bool excludereserved, string fromstyle = "")
        {
            using (var connection = _connectionProvider.GetConnection())
            using (var command = new SqlCommand("ListofCustPO", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandTimeout = 6000;

                // Add parameters explicitly
                command.Parameters.Add(new SqlParameter("@acc1", SqlDbType.NVarChar) { Value = acc1 });
                command.Parameters.Add(new SqlParameter("@acc2", SqlDbType.NVarChar) { Value = acc2 });
                command.Parameters.Add(new SqlParameter("@dateval", SqlDbType.Int) { Value = dateval });
                command.Parameters.Add(new SqlParameter("@date1", SqlDbType.DateTime) { Value = (object)date1 ?? DBNull.Value });
                command.Parameters.Add(new SqlParameter("@date2", SqlDbType.DateTime) { Value = (object)date2 ?? DBNull.Value });
                command.Parameters.Add(new SqlParameter("@openonly", SqlDbType.Bit) { Value = openonly });
                command.Parameters.Add(new SqlParameter("@showdetail", SqlDbType.Bit) { Value = showdetail });
                command.Parameters.Add(new SqlParameter("@excludereserved", SqlDbType.Bit) { Value = excludereserved });
                command.Parameters.Add(new SqlParameter("@STYLEFROM", SqlDbType.NVarChar) { Value = fromstyle });

                // Create and fill the DataTable
                using (var dataTable = new DataTable())
                using (var dataAdapter = new SqlDataAdapter(command))
                {
                    dataAdapter.Fill(dataTable);
                    return dataTable;
                }
            }
        }
        public DataTable GetOrderType(string strOrderType = "")
        {
            if (strOrderType == string.Empty)
                return _helperCommonService.GetSqlData("Select Ordertype,Ordertype as Ordertype_Old from ordertype order by 1");
            return _helperCommonService.GetSqlData("Select ordertype from ordertype where trim(ordertype)=@OrdType order by 1", "@OrdType", strOrderType.Trim());
        }

        public DataTable GetOrderbyCustPO(string cust_pon, string acc)
        {
            return _helperCommonService.GetSqlData("select * from orders where cust_pon = @cust_pon and acc = @acc",
                "@cust_pon", cust_pon, "@acc", acc);
        }
        public DataTable GetUpsTrakByPON(string pon)
        {
            return _helperCommonService.GetStoreProc("GetUpsTrakByPON", "@PON", pon);
        }
        public DataTable GetCastOrderedByPON(string pon)
        {
            return _helperCommonService.GetStoreProc("GetCastOrderedByPON", "@PON", pon);
        }
        public DataTable GetRejectedByPON(string pon)
        {
            return _helperCommonService.GetStoreProc("GetRejectedByPON", "@PON", pon);
        }

        public bool AddOrderType(string strOrderType, string type)
        {
            // Use `using` to ensure proper disposal of resources
            using (var connection = _connectionProvider.GetConnection())
            using (var command = new SqlCommand())
            {
                command.Connection = connection;
                command.CommandType = CommandType.Text;

                command.CommandText = type == "DELETE"
                    ? "DELETE FROM OrderType WHERE TRIM(OrderType) = @OrderType"
                    : "INSERT INTO OrderType (OrderType) VALUES (@OrderType)";

                // Add the parameter with trimmed value
                command.Parameters.AddWithValue("@OrderType", strOrderType.Trim());

                // Open the connection, execute the query, and return the result
                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();

                return rowsAffected > 0;
            }
        }

        public bool UpdateOrderType(string strOrdertype_New, string strOrdertype_Old)
        {
            using (var connection = _connectionProvider.GetConnection())
            using (var dbCommand = new SqlCommand("UPDATE OrderType SET OrderType = @Ordertype_New WHERE TRIM(OrderType) = @Ordertype_Old", connection))
            {
                dbCommand.CommandType = CommandType.Text;
                dbCommand.Parameters.AddWithValue("@Ordertype_New", strOrdertype_New.Trim());
                dbCommand.Parameters.AddWithValue("@Ordertype_Old", strOrdertype_Old.Trim());
                connection.Open();
                return dbCommand.ExecuteNonQuery() > 0;
            }
        }


        public DataTable OrderStyleTracking(string styles, bool openonly, string acc1, string acc2, string date1,
            string date2, bool byparentstyle = false)
        {
            var dataTable = new DataTable();
            using (var connection = _connectionProvider.GetConnection())
            using (var command = new SqlCommand("OrderStyleTracking", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandTimeout = 60; // Set a reasonable timeout

                // Add parameters explicitly
                command.Parameters.Add(new SqlParameter("@styleno", SqlDbType.NVarChar) { Value = styles });
                command.Parameters.Add(new SqlParameter("@openonly", SqlDbType.Bit) { Value = openonly });
                command.Parameters.Add(new SqlParameter("@acc1", SqlDbType.NVarChar) { Value = acc1 ?? (object)DBNull.Value });
                command.Parameters.Add(new SqlParameter("@acc2", SqlDbType.NVarChar) { Value = acc2 ?? (object)DBNull.Value });
                command.Parameters.Add(new SqlParameter("@date1", SqlDbType.NVarChar) { Value = date1 ?? (object)DBNull.Value });
                command.Parameters.Add(new SqlParameter("@date2", SqlDbType.NVarChar) { Value = date2 ?? (object)DBNull.Value });
                command.Parameters.Add(new SqlParameter("@byparentstyle", SqlDbType.Bit) { Value = byparentstyle });

                using (var adapter = new SqlDataAdapter(command))
                {
                    adapter.Fill(dataTable);
                }
            }

            return dataTable;
        }
    }
}
