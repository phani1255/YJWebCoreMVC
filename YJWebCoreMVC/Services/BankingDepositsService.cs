using Microsoft.Data.SqlClient;
using System.Data;

namespace YJWebCoreMVC.Services
{
    public class BankingDepositsService
    {
        private readonly ConnectionProvider _connectionProvider;
        private readonly HelperCommonService _helperCommonService;

        public BankingDepositsService(ConnectionProvider connectionProvider, HelperCommonService helperCommonService, IWebHostEnvironment env)
        {
            _connectionProvider = connectionProvider;
            _helperCommonService = helperCommonService;
        }

        public DataRow CheckDeposit(string depno)
        {
            using (var connection = _connectionProvider.GetConnection())
            using (var command = new SqlCommand("checkdeposit", connection)
            {
                CommandType = CommandType.StoredProcedure
            })
            {
                command.Parameters.AddWithValue("@depno", depno);

                var dataTable = new DataTable();
                connection.Open();
                using (var dataAdapter = new SqlDataAdapter(command))
                    dataAdapter.Fill(dataTable);
                // Return the first DataRow, if any
                return dataTable.Rows.Count > 0 ? dataTable.Rows[0] : null;
            }
        }

        public DataRow GetDepositBycode(string tcode)
        {
            return _helperCommonService.GetSqlRow("select *  From DEPOSITS Where DEPNO = @DEPNO", "@DEPNO", tcode);
        }

        public bool DeleteDeposit(int id)
        {
            using (SqlCommand dbCommand = new SqlCommand())
            {
                // Set the command object properties
                dbCommand.Connection = _connectionProvider.GetConnection();
                dbCommand.CommandType = CommandType.Text;
                dbCommand.CommandText = "UPDATE payments SET depno = 0, dep_dt = NULL WHERE depno = @INV_NO; DELETE FROM deposits WHERE depno = @INV_NO; DELETE FROM bank WHERE depno = @INV_NO; DELETE FROM GLPOST WHERE INV_NO = @INV_NO AND TYPE='D';";

                dbCommand.Parameters.AddWithValue("@INV_NO", id);
                dbCommand.Connection.Open();
                var rowsAffected = dbCommand.ExecuteNonQuery();
                dbCommand.Connection.Close();

                return rowsAffected > 0;
            }
        }

    }
}
