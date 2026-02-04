// Chakri  12/31/2025  Created New Model.
// Chakri  12/31/2025  Added UpdateEmailSettings method.
// Chakri  01/01/2026  Added SetEmailSetupPerUser method and related properties.
// Chakri  01/02/2026  Changes in SetEmailSetupPerUser  method, and added related properties.

using Microsoft.Data.SqlClient;
using System.Data;

namespace YJWebCoreMVC.Services
{
    public class EmailSettingsService
    {
        private readonly ConnectionProvider _connectionProvider;
        private readonly HelperCommonService _helperCommonService;

        public EmailSettingsService(ConnectionProvider connectionProvider, HelperCommonService helperCommonService, IWebHostEnvironment env)
        {
            _connectionProvider = connectionProvider;
            _helperCommonService = helperCommonService;
        }


        public bool UpdateEmailSettings(string email, string pass, int port, string smtpserver, bool usessl, string displayname, bool useoauth2, string email_Signature)
        {
            // Use 'using' to ensure proper disposal of resources
            using (SqlConnection connection = _connectionProvider.GetConnection())
            using (SqlCommand dbCommand = new SqlCommand())
            {
                // Set the command object properties
                dbCommand.Connection = connection;
                dbCommand.CommandType = CommandType.StoredProcedure;
                dbCommand.CommandText = "EmailSetting";

                // Add parameters with explicit SQL types for better type safety
                dbCommand.Parameters.Add(new SqlParameter("@Email", SqlDbType.NVarChar) { Value = email });
                dbCommand.Parameters.Add(new SqlParameter("@Password", SqlDbType.NVarChar) { Value = pass });
                dbCommand.Parameters.Add(new SqlParameter("@Port", SqlDbType.Int) { Value = port });
                dbCommand.Parameters.Add(new SqlParameter("@smtpserver", SqlDbType.NVarChar) { Value = smtpserver });
                dbCommand.Parameters.Add(new SqlParameter("@usessl", SqlDbType.Bit) { Value = usessl });
                dbCommand.Parameters.Add(new SqlParameter("@displayname", SqlDbType.NVarChar) { Value = displayname });
                dbCommand.Parameters.Add(new SqlParameter("@useoauth2", SqlDbType.Bit) { Value = useoauth2 });
                dbCommand.Parameters.Add(new SqlParameter("@email_signature", SqlDbType.NVarChar) { Value = email_Signature });

                // Open the connection, execute the query, and close the connection
                connection.Open();
                var rowsAffected = dbCommand.ExecuteNonQuery();
                connection.Close();

                // Return true if rows were affected, otherwise false
                return rowsAffected > 0;
            }
        }
        public bool SetEmailSetupPerUser(string user, string email, string pass, int port, string smtpserver,
            bool usessl, string displayname, bool useoauth2, string email_Signature, bool SignatureIsJpg, byte[] SignatureJpg)
        {
            // Use 'using' to ensure resources are properly disposed
            using (SqlConnection connection = _connectionProvider.GetConnection())
            using (SqlCommand dbCommand = new SqlCommand())
            {
                // Set up the command and properties
                dbCommand.Connection = connection;
                dbCommand.CommandType = CommandType.StoredProcedure;
                dbCommand.CommandText = "EmailSetupPerUser";

                // Add parameters explicitly with their data types
                dbCommand.Parameters.Add(new SqlParameter("@user", SqlDbType.NVarChar) { Value = user });
                dbCommand.Parameters.Add(new SqlParameter("@Email", SqlDbType.NVarChar) { Value = email });
                dbCommand.Parameters.Add(new SqlParameter("@Password", SqlDbType.NVarChar) { Value = pass });
                dbCommand.Parameters.Add(new SqlParameter("@Port", SqlDbType.Int) { Value = port });
                dbCommand.Parameters.Add(new SqlParameter("@smtpserver", SqlDbType.NVarChar) { Value = smtpserver });
                dbCommand.Parameters.Add(new SqlParameter("@usessl", SqlDbType.Bit) { Value = usessl });
                dbCommand.Parameters.Add(new SqlParameter("@displayname", SqlDbType.NVarChar) { Value = displayname });
                dbCommand.Parameters.Add(new SqlParameter("@useoauth2", SqlDbType.Bit) { Value = useoauth2 });
                dbCommand.Parameters.Add(new SqlParameter("@email_Signature", SqlDbType.NVarChar) { Value = email_Signature });
                dbCommand.Parameters.Add(new SqlParameter("@SignatureIsJpg", SqlDbType.Bit) { Value = SignatureIsJpg });
                dbCommand.Parameters.Add(new SqlParameter("@SignatureJpg", SqlDbType.VarBinary, -1) { Value = (object)SignatureJpg ?? DBNull.Value });


                // Open the connection, execute the command, and close the connection
                connection.Open();
                var rowsAffected = dbCommand.ExecuteNonQuery();
                connection.Close();

                // Return true if rows were affected, false otherwise
                return rowsAffected > 0;
            }
        }

    }
}
