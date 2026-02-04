// Dharani   06/04/2025  Model Created. 
// Dharani   06/04/2025  Added AddOrEditSalesman, GetSalesmancode methods.

using Microsoft.Data.SqlClient;
using System.Data;

namespace YJWebCoreMVC.Services
{
    public class CustomersSalesmanService
    {
        private readonly ConnectionProvider _connectionProvider;
        private readonly HelperCommonService _helperCommonService;

        public CustomersSalesmanService(ConnectionProvider connectionProvider, HelperCommonService helperCommonService, IWebHostEnvironment env)
        {
            _connectionProvider = connectionProvider;
            _helperCommonService = helperCommonService;
        }


        public bool AddOrEditSalesman(string scode, string uname, string addr1, string addr2, string addr3, string ph1, string ph2, string ph3, string notes1, string notes2, string notes3, string email, string commission, bool incativeornot = false, string sfContact = "")
        {
            using (SqlConnection connection = _connectionProvider.GetConnection())
            using (SqlCommand dbCommand = new SqlCommand("AddOrEditSalesMan", connection))
            {
                dbCommand.CommandType = CommandType.StoredProcedure;

                // Add parameters with explicit types
                dbCommand.Parameters.AddWithValue("@SCODE", scode ?? (object)DBNull.Value);
                dbCommand.Parameters.AddWithValue("@UNAME", uname ?? (object)DBNull.Value);
                dbCommand.Parameters.AddWithValue("@ADDR1", addr1 ?? (object)DBNull.Value);
                dbCommand.Parameters.AddWithValue("@ADDR2", addr2 ?? (object)DBNull.Value);
                dbCommand.Parameters.AddWithValue("@ADDR3", addr3 ?? (object)DBNull.Value);
                dbCommand.Parameters.AddWithValue("@PHONE1", ph1 ?? (object)DBNull.Value);
                dbCommand.Parameters.AddWithValue("@PHONE2", ph2 ?? (object)DBNull.Value);
                dbCommand.Parameters.AddWithValue("@PHONE3", ph3 ?? (object)DBNull.Value);
                dbCommand.Parameters.AddWithValue("@NOTES1", notes1 ?? (object)DBNull.Value);
                dbCommand.Parameters.AddWithValue("@NOTES2", notes2 ?? (object)DBNull.Value);
                dbCommand.Parameters.AddWithValue("@NOTES3", notes3 ?? (object)DBNull.Value);
                dbCommand.Parameters.AddWithValue("@EMAIL", email ?? (object)DBNull.Value);
                dbCommand.Parameters.AddWithValue("@sfContact", sfContact ?? (object)DBNull.Value);
                dbCommand.Parameters.AddWithValue("@incativeornot", incativeornot);


                decimal commissionValue;
                if (string.IsNullOrWhiteSpace(commission) || !decimal.TryParse(commission, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out commissionValue))
                {
                    commissionValue = 0;
                }


                if (commissionValue < -99999999.99m || commissionValue > 99999999.99m)
                {
                    throw new ArgumentException("Commission value exceeds the allowed range for DECIMAL(10,2).");
                }

                SqlParameter commissionParam = new SqlParameter("@COMMISSION", SqlDbType.Decimal)
                {
                    Value = commissionValue,
                    Precision = 10,
                    Scale = 2
                };
                dbCommand.Parameters.Add(commissionParam);


                connection.Open();
                int rowsAffected = dbCommand.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }


        public DataRow GetSalesmancode(string lcode)
        {
            return _helperCommonService.GetSqlRow(@"SELECT * FROM SALESMEN WHERE CODE = @CODE", "@CODE", lcode);
        }
    }
}
