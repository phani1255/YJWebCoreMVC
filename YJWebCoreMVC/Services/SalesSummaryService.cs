using Microsoft.Data.SqlClient;
using System.Data;
using YJWebCoreMVC.Models;

namespace YJWebCoreMVC.Services
{
    public class SalesSummaryService
    {

        private readonly ConnectionProvider _connectionProvider;

        public SalesSummaryService(ConnectionProvider connectionProvider)
        {
            _connectionProvider = connectionProvider;
        }

        public DataTable GetSales(string DateFrom, string DateTo, bool iSPartial, bool bypickdate, int type)
        {
            DataTable dataTable = new DataTable();
            List<SalesSummaryModel> lstSummaryOrd = new List<SalesSummaryModel>();

            using (SqlDataAdapter dataAdapter = new SqlDataAdapter())
            {
                try
                {
                    // Create the command and set its properties
                    dataAdapter.SelectCommand = new SqlCommand();
                    dataAdapter.SelectCommand.Connection = _connectionProvider.GetConnection();

                    dataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                    dataAdapter.SelectCommand.CommandText = "SalesSummaryReport";
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@DateFrom", Convert.ToDateTime(DateFrom));
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@DateTo", Convert.ToDateTime(DateTo));
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@iSPartial", iSPartial);
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@bypickdate", bypickdate);
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@type", type);

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
    }
}
