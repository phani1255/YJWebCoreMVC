using Microsoft.Data.SqlClient;
using System.Data;

namespace YJWebCoreMVC.Services
{
    public class BankingTransactionsService
    {
        private readonly ConnectionProvider _connectionProvider;
        private readonly HelperCommonService _helperCommonService;

        public BankingTransactionsService(ConnectionProvider connectionProvider, HelperCommonService helperCommonService, IWebHostEnvironment env)
        {
            _connectionProvider = connectionProvider;
            _helperCommonService = helperCommonService;
        }


        public DataRow GetTransactionBycode(string tcode)
        {
            return _helperCommonService.GetSqlRow("SELECT * FROM BANK Where Trimmed_inv_no = trim(@INV_NO)", "@INV_NO", tcode);
        }
        public void DeleteTransaction(int id, string logNo = "")
        {
            using (var connection = _connectionProvider.GetConnection())
            using (var command = new SqlCommand())
            {
                // Configure the command
                command.Connection = connection;
                command.CommandType = CommandType.Text;
                command.CommandText = @"
                    DELETE FROM bank WHERE INV_NO = @INV_NO;
                    DELETE FROM glpost WHERE LOG_NO = @logNo AND INV_NO = @INV_NO";

                // Add parameters with explicit types
                command.Parameters.Add("@INV_NO", SqlDbType.Int).Value = id;
                command.Parameters.Add("@logNo", SqlDbType.VarChar).Value = logNo ?? string.Empty;

                // Open connection and execute
                connection.Open();
                command.ExecuteNonQuery();
            }
        }
        public bool isValidReconLog(string ReconLog)
        {
            return _helperCommonService.DataTableOK(_helperCommonService.GetSqlData("SELECT * FROM BANK WHERE LOG_RECON=" + ReconLog + ""));
        }

        public void DeleteReconciledLog(string ReconLog)
        {
            _helperCommonService.GetSqlData("UPDATE BANK SET CLRD = 0, LOG_RECON = '', CLRD_DATE = '' WHERE trim(LOG_RECON) = @ReconLog;",
                "@ReconLog", ReconLog.Trim());
        }
    }
}
