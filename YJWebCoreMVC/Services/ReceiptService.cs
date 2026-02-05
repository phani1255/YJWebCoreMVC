/*
 * Created by Dharani 14-May-2025
 * Dharani  10/07/2025 Added GetPaymentSummarytByTimeFrame method
 * Dharani  10/20/2025 Added GetPayment, GetPaymentByCheckNo methods.
 */

using Microsoft.Data.SqlClient;
using System.Data;

namespace YJWebCoreMVC.Services
{
    //ReceiptServiceModel refernce to ReceiptService
    public class ReceiptService
    {
        private readonly ConnectionProvider _connectionProvider;
        private readonly HelperCommonService _helperCommonService;

        public ReceiptService(ConnectionProvider connectionProvider, HelperCommonService helperCommonService)
        {
            _connectionProvider = connectionProvider;
            _helperCommonService = helperCommonService;
        }

        public DataTable SummaryByRegister(string datetype, string date1, string date2, string trantype, string registers)
        {
            DataTable dataTable = new DataTable();

            using (SqlDataAdapter dataAdapter = new SqlDataAdapter())
            {
                try
                {
                    // Create the command and set its properties
                    dataAdapter.SelectCommand = new SqlCommand();
                    dataAdapter.SelectCommand.Connection = _connectionProvider.GetConnection();

                    dataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                    dataAdapter.SelectCommand.CommandText = "GetRcvableCreditByRegister";
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@datetype", datetype);
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@date1", date1);
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@date2", date2);
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@trantype", trantype);
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@registers", registers);

                    // Fill the datatable From adapter
                    dataAdapter.Fill(dataTable);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }


                return dataTable;
            }
        }
        public DataTable GetPaymentSummarytByTimeFrame(string date1, string date2, string registers, string allstore, string paymenttype)
        {
            using (var connection = _connectionProvider.GetConnection())
            using (var command = new SqlCommand("GetPaymentSummary", connection))
            using (var adapter = new SqlDataAdapter(command))
            {
                // Set command properties
                command.CommandType = CommandType.StoredProcedure;

                // Add parameters with explicit types
                command.Parameters.Add(new SqlParameter("@date1", SqlDbType.NVarChar) { Value = date1 });
                command.Parameters.Add(new SqlParameter("@date2", SqlDbType.NVarChar) { Value = date2 });
                command.Parameters.Add(new SqlParameter("@registers", SqlDbType.NVarChar) { Value = registers });
                command.Parameters.Add(new SqlParameter("@AllStore", SqlDbType.NVarChar) { Value = allstore });
                command.Parameters.Add(new SqlParameter("@PaymentType", SqlDbType.NVarChar) { Value = paymenttype });

                // Fill and return the DataTable
                var dataTable = new DataTable();
                adapter.Fill(dataTable);

                return dataTable.Rows.Count > 0 ? dataTable : null;
            }
        }
        public DataRow GetPayment(string inv_no)
        {
            return _helperCommonService.GetSqlRow("select * from payments where rtv_pay = 'P' and Trimmed_inv_no = trim(@inv_no)", "@inv_no", inv_no);
        }
        public DataRow GetPaymentByCheckNo(string acc, string check_no)
        {
            return _helperCommonService.GetSqlRow("select * from payments where trim(acc) =  trim(@acc) and trim(check_no) = trim(@check_no)", "@acc", acc, "@check_no", check_no);
        }
    }
}
