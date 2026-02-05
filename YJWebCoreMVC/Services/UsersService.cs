/*
 * Phanindra 09/13/2024 Added GetUpsIns1 function to get WebImagesUrl to show style images
 * Phanindra 10/02/2024 Added Companyname to the session to show or hide menu items in getupsins function
 * Phanindra 11/06/2024 Added GetUserStoreDetails for login page
 * Phanindra 01/10/2025 Added MICR column for report related condition
 * Phanindra 09/01/2025 Added getPassfile method.
 * Phanindra 09/17/2025 replaced WebImagesUrl with Web_Images_Path in getUpsIns1 method
 */
using Microsoft.Data.SqlClient;
using System.Data;
using System.Text;

namespace YJWebCoreMVC.Services
{
    public class UsersService
    {
        private readonly ConnectionProvider _connectionProvider;
        private readonly HelperService _helperServices;

        public UsersService(ConnectionProvider connectionProvider, HelperService helperServices)
        {
            _connectionProvider = connectionProvider;
            _helperServices = helperServices;
        }

        public DataTable CheckUserCredential(string username, string passwd)
        {
            DataTable dataTable = new DataTable();

            passwd = passwd.ToUpper();
            using (SqlDataAdapter dataAdapter = new SqlDataAdapter())
            {
                try
                {
                    // Create the command and set its properties
                    dataAdapter.SelectCommand = new SqlCommand();
                    dataAdapter.SelectCommand.Connection = _connectionProvider.GetConnection();

                    dataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                    dataAdapter.SelectCommand.CommandText = "CheckUserCredential";
                    byte[] password = Encoding.UTF8.GetBytes(passwd);

                    // Add the parameter to the parameter collection
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@NAME", username);
                    dataAdapter.SelectCommand.Parameters.Add("@PASSWD", SqlDbType.Binary, password.Length).Value = password;

                    // Fill the datatable From adapter
                    dataAdapter.Fill(dataTable);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                // Get the datarow from the table
                return dataTable.Rows.Count > 0 ? dataTable : null;
            }
        }

        public DataTable GetCustomers(string fname, string lname, string state, string city, string zip, string phone, string email, string optionvalue = "")
        {
            DataTable dataTable = new DataTable();
            string Query1 = "";
            using (SqlDataAdapter dataAdapter = new SqlDataAdapter())
            {
                try
                {
                    // Create the command and set its properties
                    dataAdapter.SelectCommand = new SqlCommand();
                    dataAdapter.SelectCommand.Connection = _connectionProvider.GetConnection();

                    dataAdapter.SelectCommand.CommandType = CommandType.Text;
                    if (optionvalue == "Invoice")
                        Query1 = "SELECT ACC,NAME,CITY1,STATE1,ZIP1,TEL,EMAIL,Addr1,Addr12,Addr13,Country FROM CUSTOMER WHERE NAME LIKE @fname AND NAME LIKE @lname AND STATE1 LIKE @state AND CITY1 LIKE @city AND ZIP1 LIKE @zip AND tel LIKE @phone AND EMAIL LIKE @email ORDER BY ACC ";
                    else
                        Query1 = "SELECT ACC,NAME,CITY1,STATE1,ZIP1,TEL,EMAIL FROM CUSTOMER WHERE NAME LIKE @fname AND NAME LIKE @lname AND STATE1 LIKE @state AND CITY1 LIKE @city AND ZIP1 LIKE @zip AND tel LIKE @phone AND EMAIL LIKE @email ORDER BY ACC ";
                    dataAdapter.SelectCommand.CommandText = Query1;

                    // Add the parameter to the parameter collection
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@fname", fname + "%");
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@lname", "%" + lname + "%");
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@state", "%" + state + "%");
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@city", "%" + city + "%");
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@zip", "%" + zip + "%");
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@phone", "%" + phone + "%");
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@email", "%" + email + "%");

                    // Fill the datatable From adapter
                    dataAdapter.Fill(dataTable);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                // Get the datarow from the table
                return dataTable.Rows.Count > 0 ? dataTable : null;
            }
        }

        public DataTable GetUpsIns()
        {
            DataTable dataTable = new DataTable();

            using (SqlDataAdapter dataAdapter = new SqlDataAdapter())
            {
                try
                {
                    // Create the command and set its properties
                    dataAdapter.SelectCommand = new SqlCommand();
                    dataAdapter.SelectCommand.Connection = _connectionProvider.GetConnection();

                    dataAdapter.SelectCommand.CommandType = CommandType.Text;
                    dataAdapter.SelectCommand.CommandText = "SELECT PCS_DECIMAL,COMPANYNAME,MICR FROM UPS_INS";

                    // Fill the datatable From adapter
                    dataAdapter.Fill(dataTable);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                // Get the datarow from the table
                return dataTable.Rows.Count > 0 ? dataTable : null;
            }
        }

        public DataTable GetUpsIns1()
        {
            DataTable dataTable = new DataTable();

            using (SqlDataAdapter dataAdapter = new SqlDataAdapter())
            {
                try
                {
                    // Create the command and set its properties
                    dataAdapter.SelectCommand = new SqlCommand();
                    dataAdapter.SelectCommand.Connection = _connectionProvider.GetConnection();

                    dataAdapter.SelectCommand.CommandType = CommandType.Text;
                    dataAdapter.SelectCommand.CommandText = "SELECT Web_Images_Path FROM UPS_INS1";

                    // Fill the datatable From adapter
                    dataAdapter.Fill(dataTable);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                // Get the datarow from the table
                return dataTable.Rows.Count > 0 ? dataTable : null;
            }
        }

        public DataTable GetUserStoreDetails(string username)
        {
            DataTable dataTable = new DataTable();

            using (SqlDataAdapter dataAdapter = new SqlDataAdapter())
            {
                try
                {
                    // Create the command and set its properties
                    dataAdapter.SelectCommand = new SqlCommand();
                    dataAdapter.SelectCommand.Connection = _connectionProvider.GetConnection();
                    dataAdapter.SelectCommand.CommandType = CommandType.Text;
                    dataAdapter.SelectCommand.CommandText = "select store_code,Fixed_store,UserLang from passfile where name = @name";
                    // Add the parameter to the parameter collection
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@name", username);

                    // Fill the datatable From adapter
                    dataAdapter.Fill(dataTable);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                // Get the datarow from the table
                return dataTable.Rows.Count > 0 ? dataTable : null;

            }
        }

        public DataTable GetPassfile(string CODE)
        {
            return _helperServices.HelperCommon.GetSqlData("SELECT * FROM PASSFILE WHERE NAME = @NAME", "@NAME", CODE);
        }


    }
}
