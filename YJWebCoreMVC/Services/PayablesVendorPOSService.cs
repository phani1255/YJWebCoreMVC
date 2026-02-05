/*chakri 05/26/2025 created new Model.
 *chakri 05/26/2025 Added GetvendorPoinfobasedoninv and DeleteRtv methods.
 *Cahkri 01/30/2026 Added getallVendorPos and updateAllVendorPos methods and added properties.
 * 
 */
using Microsoft.Data.SqlClient;
using System.Data;

namespace YJWebCoreMVC.Services
{
    public class PayablesVendorPOSService
    {
        private readonly ConnectionProvider _connectionProvider;
        private readonly HelperCommonService _helperCommonService;

        public PayablesVendorPOSService(ConnectionProvider connectionProvider, HelperCommonService helperCommonService)
        {
            _connectionProvider = connectionProvider;
            _helperCommonService = helperCommonService;
        }

        public DataTable GetvendorPoinfobasedoninv(string inv)
        {
            return _helperCommonService.GetSqlData("select co.inv_no,co.date,co.style,co.size,co.qty,co.rcvd,co.acc,co.due_date,co.gold_price,s.price,co.Approved,co.Approve_Date,co.Approve_Note from CAST_ORD co left join styles s on s.style = co.style where trim(co.inv_no) in(@inv) ", "@inv", inv.Trim());
        }
        public void DeleteRtv(string vendorponumber, string LoggedUser)
        {
            _helperCommonService.GetSqlData("delete from CAST_ORD where Trimmed_inv_no='" + vendorponumber.Trim() + "';INSERT INTO KEEP_REC ([DATE], [TIME], WHO, WHAT)  VALUES (GETDATE(), CONVERT(time, GETDATE()), '" + LoggedUser + "', 'Vendor PO" + vendorponumber + " was deleted')");

        }


        public DataTable getallVendorPos(string acc, string fdate, string tdate, bool optPoOpen)
        {
            var dataTable = new DataTable();
            string query = @"
        SELECT STYLE, SIZE, (ISNULL(QTY,0) - ISNULL(RCVD,0)) AS QTY, 
               ACC AS VENDOR, INV_NO, PON, RECEIVED  
        FROM CAST_ORD 
        WHERE (ACC = @acc OR @acc = '')  
          AND CAST(DATE AS DATE) BETWEEN @fdate AND @tdate 
          AND (ISNULL(QTY,0) - ISNULL(RCVD,0)) >= 0";

            if (optPoOpen)
            {
                query = @"SELECT STYLE, SIZE, (ISNULL(QTY,0) - ISNULL(RCVD,0)) AS QTY, 
                    ACC AS VENDOR, INV_NO, PON, RECEIVED  
                    FROM CAST_ORD 
                    WHERE (ACC = @acc OR @acc = '')  
                    AND CAST(DATE AS DATE) BETWEEN @fdate AND @tdate 
                    AND RECEIVED = @rcvd 
                    AND (ISNULL(QTY,0) - ISNULL(RCVD,0)) >= 0";
            }

            using (var connection = _connectionProvider.GetConnection())
            using (var command = new SqlCommand(query, connection))
            {
                command.CommandType = CommandType.Text;
                command.Parameters.AddWithValue("@acc", acc);
                command.Parameters.AddWithValue("@fdate", DateTime.Parse(fdate));
                command.Parameters.AddWithValue("@tdate", DateTime.Parse(tdate));

                if (optPoOpen)
                    command.Parameters.AddWithValue("@rcvd", '0');

                using (var adapter = new SqlDataAdapter(command))
                {
                    adapter.Fill(dataTable);
                }
            }
            return dataTable;
        }

        public void updateAllVendorPos(string vendorPos, string loggedUser, string store, bool addToInvtry = false)
        {
            using (var connection = _connectionProvider.GetConnection())
            using (var command = new SqlCommand("UPDATEALLVENDORPOS", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@user", loggedUser);
                command.Parameters.AddWithValue("@store", store);
                command.Parameters.AddWithValue("@AddtoInvtry", addToInvtry);
                command.Parameters.Add(new SqlParameter("@VENDORPOS", SqlDbType.Xml) { Value = vendorPos });

                var dataTable = new DataTable();
                using (var adapter = new SqlDataAdapter(command))
                {
                    adapter.Fill(dataTable);
                }
            }
        }


    }
}
