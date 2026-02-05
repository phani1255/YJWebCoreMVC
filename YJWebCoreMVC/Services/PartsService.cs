/*
 *  Created By Phanindra on 25-Jul-2025
 *  
 */
using Microsoft.Data.SqlClient;
using System.Data;

namespace YJWebCoreMVC.Services
{
    public class PartsService
    {

        private readonly ConnectionProvider _connectionProvider;
        private readonly HelperCommonService _helperCommonService;

        public PartsService(ConnectionProvider connectionProvider, HelperCommonService helperCommonService)
        {
            _connectionProvider = connectionProvider;
            _helperCommonService = helperCommonService;
        }

        public bool UpdateReservedParts(string jobBagNo, string loggedUser, string reservedParts, string storeNo, bool isUpdatePrice = false)
        {
            const string storedProcedureName = "UpdateReservedPartsForJobBag";

            using (var connection = _connectionProvider.GetConnection())
            using (var command = new SqlCommand(storedProcedureName, connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandTimeout = 5000;

                // Add parameters
                command.Parameters.Add(new SqlParameter("@tblResrvParts", SqlDbType.Xml) { Value = reservedParts });
                command.Parameters.Add(new SqlParameter("@JobBagNo", SqlDbType.NVarChar) { Value = jobBagNo });
                command.Parameters.Add(new SqlParameter("@LoggedUser", SqlDbType.NVarChar) { Value = loggedUser });
                command.Parameters.Add(new SqlParameter("@Store_No", SqlDbType.NVarChar) { Value = storeNo });
                command.Parameters.Add(new SqlParameter("@iSUpdatePrice", SqlDbType.Bit) { Value = isUpdatePrice ? 1 : 0 });

                // Execute the command
                connection.Open();
                var rowsAffected = command.ExecuteNonQuery();

                return rowsAffected > 0;
            }
        }
    }
}
