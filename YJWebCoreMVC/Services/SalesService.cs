/*
 *  Created By Dharani on 19-Jan-2026
 *  
 */
using Microsoft.Data.SqlClient;
using System.Data;

namespace YJWebCoreMVC.Services
{
    public class SalesService
    {
        private readonly ConnectionProvider _connectionProvider;
        private readonly HelperCommonService _helperCommonService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SalesService(ConnectionProvider connectionProvider, IHttpContextAccessor httpContextAccessor, HelperCommonService helperCommonService)
        {
            _connectionProvider = connectionProvider;
            _helperCommonService = helperCommonService;
            _httpContextAccessor = httpContextAccessor;
        }

        public bool AddorEditSalesman(string scode, string uname, string addr1, string addr2, string addr3, string ph1, string ph2, string ph3, string notes1, string notes2, string notes3, string email, string commssion, bool incativeornot = false, string sfContact = "")
        {
            // Use 'using' blocks for proper disposal of connection and command
            using (SqlConnection connection = _connectionProvider.GetConnection())
            using (SqlCommand dbCommand = new SqlCommand("AddOrEditSalesMan", connection))
            {
                // Set the command type to stored procedure
                dbCommand.CommandType = CommandType.StoredProcedure;

                // Add parameters with explicit types
                dbCommand.Parameters.AddWithValue("@SCODE", scode);
                dbCommand.Parameters.AddWithValue("@UNAME", uname);
                dbCommand.Parameters.AddWithValue("@ADDR1", addr1);
                dbCommand.Parameters.AddWithValue("@ADDR2", addr2);
                dbCommand.Parameters.AddWithValue("@ADDR3", addr3);
                dbCommand.Parameters.AddWithValue("@PHONE1", ph1);
                dbCommand.Parameters.AddWithValue("@PHONE2", ph2);
                dbCommand.Parameters.AddWithValue("@PHONE3", ph3);
                dbCommand.Parameters.AddWithValue("@NOTES1", notes1);
                dbCommand.Parameters.AddWithValue("@NOTES2", notes2);
                dbCommand.Parameters.AddWithValue("@NOTES3", notes3);
                dbCommand.Parameters.AddWithValue("@EMAIL", email);
                dbCommand.Parameters.AddWithValue("@COMMISSION", commssion);
                dbCommand.Parameters.AddWithValue("@incativeornot", incativeornot);
                dbCommand.Parameters.AddWithValue("@sfContact", sfContact);

                // Open the connection and execute the command
                connection.Open();
                int rowsAffected = dbCommand.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }

        public DataTable Getsalesmancodes()
        {
            return _helperCommonService.GetSqlData("select '' as CODE FROM [dbo].[SALESMEN]  union select CODE FROM [dbo].[SALESMEN] order by code");
        }

        public void DeleteSalesmanacc(string bcode, string LoggedUser)
        {
            _helperCommonService.GetSqlData("delete FROM [dbo].[SALESMEN] where code  = @CODE;INSERT INTO KEEP_REC ([DATE], [TIME], WHO, WHAT)  VALUES (GETDATE(), CONVERT(time, GETDATE()), '" + LoggedUser + "', 'SalesMan " + bcode + " was deleted')",
                "@CODE", bcode);
        }

        public DataTable GetAllSalesman()
        {
            return _helperCommonService.GetSqlData("select NAME , CODE ,TEL, ADDR1, ADDR2, ADDR3, NOTE1 FROM [dbo].[SALESMEN] order by code");
        }

        public DataRow GetSalesmancode(string lcode)
        {
            return _helperCommonService.GetSqlRow(@"SELECT * FROM SALESMEN with(nolock) WHERE CODE = @CODE", "@CODE", lcode);
        }

        public DataTable ListofDiscounts(DateTime? date1, DateTime? date2, string store, string strSearchOption, bool isLocgroupby, bool isDiscodegroupby)
        {
            return _helperCommonService.GetStoreProc("ListofDiscounts", "@date1", date1.ToString(), "date2", date2.ToString(), "@SelectedStore", store, "SearchOption", strSearchOption, "@isLocgroupby", isLocgroupby.ToString(), "@isDiscodegroupby", isDiscodegroupby.ToString());
        }

        public DataTable Invoice_Discounts(string strDiscount, DateTime? date1, DateTime? date2, string store)
        {
            return _helperCommonService.GetStoreProc("PROC_Invoice_Discounts", "@DISCOUNT", strDiscount, "@date1", date1.ToString(), "date2", date2.ToString(), "@SelectedStore", store);

        }
    }
}
