// Neetha    04/22/2025   Created New Model//

using Microsoft.Data.SqlClient;
using System.Data;

namespace YJWebCoreMVC.Services
{
    public class SalesSpecialOrdersService
    {
        private readonly ConnectionProvider _connectionProvider;
        private readonly HelperCommonService _helperCommonService;

        public SalesSpecialOrdersService(ConnectionProvider connectionProvider, HelperCommonService helperCommonService)
        {
            _connectionProvider = connectionProvider;
            _helperCommonService = helperCommonService;
        }

        public DataTable GetInvoicePayments(string inv_no, bool showlayaway = true, bool is_return = false, bool iSFromReturn = false)
        {
            DataTable dataTable = new DataTable();
            using (SqlDataAdapter dataAdapter = new SqlDataAdapter())
            {
                // Create the command and set its properties
                dataAdapter.SelectCommand = new SqlCommand();
                dataAdapter.SelectCommand.Connection = _connectionProvider.GetConnection();

                dataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                dataAdapter.SelectCommand.CommandText = "GetInvoicePayments";

                dataAdapter.SelectCommand.Parameters.AddWithValue("@inv_no", inv_no);
                dataAdapter.SelectCommand.Parameters.AddWithValue("@showlayaway", showlayaway);
                dataAdapter.SelectCommand.Parameters.AddWithValue("@is_return", is_return);
                dataAdapter.SelectCommand.Parameters.AddWithValue("@iSFromReturn", iSFromReturn);
                dataAdapter.Fill(dataTable);
                return dataTable;
            }
        }
        public string GetAccfromInvoice(string inv_no)
        {
            DataTable dtable = _helperCommonService.GetSqlData($"SELECT top 1 ACC FROM INVOICE WHERE inv_no='{inv_no}'");
            if (_helperCommonService.DataTableOK(dtable))
                return Convert.ToString(dtable.Rows[0]["ACC"]);
            return string.Empty;
        }
        public bool CancelLayaway(string invno, decimal restockfee, string payments, string loggeduser, string register, string store_no, bool _IsIssueStore = false, string bank = "", string checkNo = "", bool iSSpecial = false)
        {
            using (SqlCommand dbCommand = new SqlCommand())
            {
                // Set the command object properties
                dbCommand.Connection = _connectionProvider.GetConnection();
                dbCommand.CommandType = CommandType.StoredProcedure;
                dbCommand.CommandText = "CancelLayaway";

                SqlParameter parameter = new SqlParameter();
                parameter.ParameterName = "@invno";
                parameter.SqlDbType = System.Data.SqlDbType.NVarChar;
                parameter.Value = invno;
                dbCommand.Parameters.Add(parameter);

                SqlParameter parameter1 = new SqlParameter();
                parameter1.ParameterName = "@restockfee";
                parameter1.SqlDbType = System.Data.SqlDbType.Decimal;
                parameter1.Value = restockfee;
                dbCommand.Parameters.Add(parameter1);

                SqlParameter parameter2 = new SqlParameter();
                parameter2.ParameterName = "@payments";
                parameter2.SqlDbType = System.Data.SqlDbType.Xml;
                parameter2.Value = payments;
                dbCommand.Parameters.Add(parameter2);

                SqlParameter parameter3 = new SqlParameter();
                parameter3.ParameterName = "@loggeduser";
                parameter3.SqlDbType = System.Data.SqlDbType.NVarChar;
                parameter3.Value = loggeduser;
                dbCommand.Parameters.Add(parameter3);

                SqlParameter parameter4 = new SqlParameter();
                parameter4.ParameterName = "@register";
                parameter4.SqlDbType = System.Data.SqlDbType.NVarChar;
                parameter4.Value = register;
                dbCommand.Parameters.Add(parameter4);

                SqlParameter parameter5 = new SqlParameter();
                parameter5.ParameterName = "@store_code";
                parameter5.SqlDbType = System.Data.SqlDbType.NVarChar;
                parameter5.Value = store_no;
                dbCommand.Parameters.Add(parameter5);

                SqlParameter parameter6 = new SqlParameter();
                parameter6.ParameterName = "@IsIssueStorePayments";
                parameter6.SqlDbType = System.Data.SqlDbType.Bit;
                parameter6.Value = _IsIssueStore;
                dbCommand.Parameters.Add(parameter6);

                SqlParameter parameter7 = new SqlParameter();
                parameter7.ParameterName = "@BANK";
                parameter7.SqlDbType = System.Data.SqlDbType.NVarChar;
                parameter7.Value = bank;
                dbCommand.Parameters.Add(parameter7);

                SqlParameter parameter8 = new SqlParameter();
                parameter8.ParameterName = "@CHECK_NO";
                parameter8.SqlDbType = System.Data.SqlDbType.NVarChar;
                parameter8.Value = checkNo;
                dbCommand.Parameters.Add(parameter8);

                SqlParameter iSSpecialOtder = new SqlParameter();
                iSSpecialOtder.ParameterName = "@iSFromSpecial";
                iSSpecialOtder.SqlDbType = System.Data.SqlDbType.Bit;
                iSSpecialOtder.Value = iSSpecial;
                dbCommand.Parameters.Add(iSSpecialOtder);

                dbCommand.Connection.Open();
                var rowsAffected = dbCommand.ExecuteNonQuery();
                dbCommand.Connection.Close();
                //return outrestinvno.Value != System.DBNull.Value ? outrestinvno.Value.ToString() : "";
                return rowsAffected > 0;
            }
        }
        public DataTable CheckValidCheckNo(string check_no, string bank)
        {
            string CommandText = "select CHECKS.*, CAST(BANK.CLRD AS BIT) as CLRD1 from checks LEFT JOIN Bank ON TRIM(Bank.inv_no)=TRIM(CHECKS.TRANSACT) WHERE TRIM(checks.bank)= @bank AND TRIM(CHECKS.check_no) = @chk_no";
            DataTable datatable = _helperCommonService.GetSqlData(CommandText, "@chk_no", check_no.Trim(), "@bank", bank.Trim());
            return datatable.Rows.Count > 0 ? datatable : null;
        }
    }
}
