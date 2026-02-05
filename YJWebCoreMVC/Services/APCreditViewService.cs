using Microsoft.Data.SqlClient;
using System.Data;
using YJWebCoreMVC.Models;

namespace YJWebCoreMVC.Services
{
    public class APCreditViewService
    {

        private readonly ConnectionProvider _connectionProvider;
        private readonly HelperCommonService _helperCommonService;

        public APCreditViewService(ConnectionProvider connectionProvider, HelperCommonService helperCommonService, IWebHostEnvironment env)
        {
            _connectionProvider = connectionProvider;
            _helperCommonService = helperCommonService;
        }


        public bool AddAPCredit(APCreditViewModel apcredit, string ReturnStatus, out string error)
        {
            error = string.Empty;

            try
            {
                using (var connection = _connectionProvider.GetConnection())
                using (var dbCommand = new SqlCommand("AddAPCreditWOBill", connection))
                {
                    // Set command properties
                    dbCommand.CommandType = CommandType.StoredProcedure;

                    // Add parameters
                    dbCommand.Parameters.AddWithValue("@INV_NO", apcredit.CreditNo);
                    dbCommand.Parameters.AddWithValue("@ACC", apcredit.VendorCode);
                    dbCommand.Parameters.AddWithValue("@AMOUNT", apcredit.TotalAmount);
                    dbCommand.Parameters.AddWithValue("@DATE", apcredit.CreditDate);
                    dbCommand.Parameters.AddWithValue("@VND_NO", apcredit.VendorRefNo);
                    dbCommand.Parameters.AddWithValue("@BALANCE", apcredit.Balance);
                    dbCommand.Parameters.AddWithValue("@PACK", "-1");
                    dbCommand.Parameters.AddWithValue("@MESSAGE", apcredit.Notes);
                    dbCommand.Parameters.AddWithValue("@NOTE1", string.IsNullOrEmpty(apcredit.Notes1) ? "" : apcredit.Notes1);
                    dbCommand.Parameters.AddWithValue("@NOTE2", string.IsNullOrEmpty(apcredit.Notes2) ? "" : apcredit.Notes2);
                    dbCommand.Parameters.AddWithValue("@REPL_IT", ReturnStatus == "DoNotReplace" ? "0" : ReturnStatus == "Replace" ? "1" : "2");
                    dbCommand.Parameters.AddWithValue("@ON_QB", apcredit.ON_QB);
                    dbCommand.Parameters.AddWithValue("@Store_no", string.IsNullOrEmpty(apcredit.Store) ? "" : apcredit.Store);
                    dbCommand.Parameters.AddWithValue("@loggeduser", _helperCommonService.LoggedUser);

                    // Open connection, execute command, and close connection
                    connection.Open();
                    return dbCommand.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return false;
            }
        }
        public bool UpdateAPCredit(APCreditViewModel apcredit, out string error)
        {
            error = string.Empty;

            try
            {
                using (SqlConnection connection = _connectionProvider.GetConnection())
                using (SqlCommand command = new SqlCommand("UpdateAPCreditWOBill", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Add parameters with explicit types
                    command.Parameters.Add(new SqlParameter("@INV_NO", SqlDbType.NVarChar) { Value = apcredit.CreditNo });
                    command.Parameters.Add(new SqlParameter("@ACC", string.IsNullOrEmpty(apcredit.VendorCode) ? "" : apcredit.VendorCode));
                    command.Parameters.Add(new SqlParameter("@AMOUNT", SqlDbType.Decimal) { Value = apcredit.TotalAmount });
                    command.Parameters.Add(new SqlParameter("@DATE", SqlDbType.DateTime) { Value = apcredit.CreditDate });
                    command.Parameters.Add(new SqlParameter("@VND_NO", SqlDbType.NVarChar) { Value = apcredit.VendorRefNo });
                    command.Parameters.Add(new SqlParameter("@BALANCE", SqlDbType.Decimal) { Value = apcredit.Balance });
                    command.Parameters.Add(new SqlParameter("@PACK", SqlDbType.Decimal) { Value = apcredit.PACK });
                    command.Parameters.Add(new SqlParameter("@MESSAGE", string.IsNullOrEmpty(apcredit.Notes) ? "" : apcredit.Notes));
                    command.Parameters.Add(new SqlParameter("@NOTE1", string.IsNullOrEmpty(apcredit.Notes1) ? "" : apcredit.Notes1));
                    command.Parameters.Add(new SqlParameter("@NOTE2", string.IsNullOrEmpty(apcredit.Notes2) ? "" : apcredit.Notes2));
                    command.Parameters.Add(new SqlParameter("@REPL_IT", apcredit.ReturnStatus == "Replace" ? "1" : apcredit.ReturnStatus == "DoNotReplace" ? "0" : "2"));
                    command.Parameters.Add(new SqlParameter("@ON_QB", SqlDbType.Bit) { Value = apcredit.ON_QB });
                    command.Parameters.Add(new SqlParameter("@Store_no", string.IsNullOrEmpty(apcredit.Store) ? "" : apcredit.Store));
                    command.Parameters.Add(new SqlParameter("@loggeduser", string.IsNullOrEmpty(apcredit.loggeduser) ? "" : apcredit.loggeduser));

                    // Open connection and execute the command
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();

                    return rowsAffected > 0;
                }
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return false;
            }
        }
    }
}
