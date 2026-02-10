/*
 *  Created By Phanindra on 26-Mar-2025
 *  Chakri 06/05/2025 Added DeleteEmployee method.
 *  chakri 06/06/2025 Added VacationRequest, GetEmployeeCode, GetEmployeeVacationDays and AddEmpVacation methods.
 *  Chakri 02/05/2026 Created EmploueeService.
 */

using Microsoft.Data.SqlClient;
using System.Data;
using YJWebCoreMVC.Models;

namespace YJWebCoreMVC.Services
{
    public class APCreditViewService
    {
        private readonly ConnectionProvider _connectionProvider;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly HelperCommonService _helperService;
        private readonly IWebHostEnvironment _env;

        private readonly string _companyName;
        private readonly string _storeCodeInUse;
        private readonly string _loggedUser;

        public APCreditViewService(ConnectionProvider connectionProvider, IHttpContextAccessor httpContextAccessor, HelperCommonService helperService, IWebHostEnvironment env)
        {
            _connectionProvider = connectionProvider;
            _httpContextAccessor = httpContextAccessor;
            _helperService = helperService;
            _env = env;

            _companyName = _httpContextAccessor.HttpContext?.Session.GetString("COMPANYNAME");
            _storeCodeInUse = _httpContextAccessor.HttpContext?.Session.GetString("STORE_CODE");
            _loggedUser = _httpContextAccessor.HttpContext?.Session.GetString("UserId");
        }

        public bool AddAPCredit(APCreditViewModel apcredit, out string error)
        {
            error = string.Empty;

            try
            {
                using (var connection = new SqlConnection(_connectionProvider.GetConnectionString()))
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
                    dbCommand.Parameters.AddWithValue("@REPL_IT", apcredit.ReturnStatus == "DoNotReplace" ? "0" : apcredit.ReturnStatus == "Replace" ? "1" : "2");
                    dbCommand.Parameters.AddWithValue("@ON_QB", apcredit.ON_QB);
                    dbCommand.Parameters.AddWithValue("@Store_no", string.IsNullOrEmpty(apcredit.Store) ? "" : apcredit.Store);
                    dbCommand.Parameters.AddWithValue("@loggeduser", _helperService.LoggedUser);

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
                using (SqlConnection connection = new SqlConnection(_connectionProvider.GetConnectionString()))
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
