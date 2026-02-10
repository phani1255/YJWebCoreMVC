/*
 *  Created By Phanindra on 26-Mar-2025
 *  Chakri 06/05/2025 Added DeleteEmployee method.
 *  chakri 06/06/2025 Added VacationRequest, GetEmployeeCode, GetEmployeeVacationDays and AddEmpVacation methods.
 *  Chakri 02/05/2026 Created EmploueeService.
 */

using Microsoft.Data.SqlClient;
using System.Data;

namespace YJWebCoreMVC.Services
{
    public class EmployeeService
    {
        private readonly ConnectionProvider _connectionProvider;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly HelperCommonService _helperService;
        private readonly IWebHostEnvironment _env;

        private readonly string _companyName;
        private readonly string _storeCodeInUse;
        private readonly string _loggedUser;

        public EmployeeService(ConnectionProvider connectionProvider, IHttpContextAccessor httpContextAccessor, HelperCommonService helperService, IWebHostEnvironment env)
        {
            _connectionProvider = connectionProvider;
            _httpContextAccessor = httpContextAccessor;
            _helperService = helperService;
            _env = env;

            _companyName = _httpContextAccessor.HttpContext?.Session.GetString("COMPANYNAME");
            _storeCodeInUse = _httpContextAccessor.HttpContext?.Session.GetString("STORE_CODE");
            _loggedUser = _httpContextAccessor.HttpContext?.Session.GetString("UserId");
        }

        public DataRow CheckValidEmployeeCode(string acc)
        {
            return _helperService.GetSqlRow(
                "SELECT * FROM EMPLOYEE WHERE LTRIM(RTRIM(code)) = LTRIM(RTRIM(@acc))",
                "@acc",
                acc
            );
        }

        public bool DeleteEmployee(string acc, out string error)
        {
            error = string.Empty;

            const string query = @"
                DELETE FROM EMPLOYEE WHERE TRIM(code) = TRIM(@acc);
                DELETE FROM EMP_DEP WHERE TRIM(code) = TRIM(@acc);
                DELETE FROM SALARY WHERE code = @acc;";

            try
            {
                using SqlCommand command = new SqlCommand(
                    query,
                    _connectionProvider.GetConnection()
                );

                command.CommandType = CommandType.Text;
                command.Parameters.AddWithValue("@acc", acc ?? (object)DBNull.Value);

                command.Connection.Open();
                command.ExecuteNonQuery();

                return true;
            }
            catch (SqlException ex)
            {
                error = ex.Message.Split('\n')[0];
                return false;
            }
        }

        public DataTable GetEmployeeCode()
        {
            return _helperService.GetSqlData(
                "SELECT '' AS code UNION SELECT code FROM EMPLOYEE"
            );
        }

        public DataTable GetEmployeeVacationDays(string empcode)
        {
            return _helperService.GetSqlData(
                "SELECT acc AS Employee_Code, CAST(date AS date) AS date, " +
                "TRIM(type) AS Vacation_Type, Note FROM vacation WHERE acc = @acc",
                "@acc",
                empcode
            );
        }

        public DataTable AddEmpVacation(string empdata, string empcode)
        {
            using SqlCommand command = new SqlCommand(
                "EMPVACATION",
                _connectionProvider.GetConnection()
            );

            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add("@TBLEMPVACATION", SqlDbType.Xml)
                   .Value = empdata ?? (object)DBNull.Value;

            command.Parameters.AddWithValue("@empcode", empcode ?? (object)DBNull.Value);

            using SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable dt = new DataTable();
            adapter.Fill(dt);

            return dt;
        }

        public DataTable GetVacationDaysCount(string empcode, DateTime fromdate, DateTime Todate, bool allcheckemp)
        {
            DataTable dataTable = new DataTable();
            DateTime tdate = Todate.AddDays(1).AddSeconds(-1);
            using (SqlDataAdapter SqlDataAdapter = new SqlDataAdapter())
            {

                SqlDataAdapter.SelectCommand = new SqlCommand();
                SqlDataAdapter.SelectCommand.Connection = _connectionProvider.GetConnection();
                SqlDataAdapter.SelectCommand.CommandType = CommandType.Text;
                if (allcheckemp)
                    SqlDataAdapter.SelectCommand.CommandText = "select type,count(*) as days from vacation where date between @fromdate and @todate group by type  order by type";
                else
                    SqlDataAdapter.SelectCommand.CommandText = "select acc,type,count(*) as days from vacation where acc = @acc and  date between @fromdate and @todate group by type ,acc order by type";
                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@acc", empcode);
                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@fromdate", fromdate);
                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@todate", tdate);
                SqlDataAdapter.Fill(dataTable);
            }
            return dataTable;
        }
        public DataTable getEmployeeAttendence(string acc, DateTime fromDate, DateTime toDate, string store_no = "")
        {
            var dataTable = new DataTable();
            using (var connection = _connectionProvider.GetConnection())
            using (var command = new SqlCommand("EmployeeAttendance", connection))
            using (var dataAdapter = new SqlDataAdapter(command))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@acc", acc ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@fdate", fromDate);
                command.Parameters.AddWithValue("@tdate", toDate);
                command.Parameters.AddWithValue("@store", string.IsNullOrWhiteSpace(store_no) ? (object)DBNull.Value : store_no);
                dataAdapter.Fill(dataTable);
            }
            return dataTable;
        }
        public DataTable GetVacationDays(string empcode, DateTime fromdate, DateTime Todate, bool allcheckemp)
        {
            DataTable dataTable = new DataTable();
            DateTime tdate = Todate.AddDays(1).AddSeconds(-1);
            using (SqlDataAdapter SqlDataAdapter = new SqlDataAdapter())
            {

                SqlDataAdapter.SelectCommand = new SqlCommand();
                SqlDataAdapter.SelectCommand.Connection = _connectionProvider.GetConnection();
                SqlDataAdapter.SelectCommand.CommandType = CommandType.Text;

                if (allcheckemp)
                    SqlDataAdapter.SelectCommand.CommandText = "select acc,date,type,note from vacation where  date between @fromdate and @todate order by date desc";
                else
                    SqlDataAdapter.SelectCommand.CommandText = "select acc,date,type,note from vacation where  acc = @acc and date between @fromdate and @todate order by date desc";

                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@acc", empcode);
                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@fromdate", fromdate);
                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@todate", tdate);
                SqlDataAdapter.Fill(dataTable);
            }

            return dataTable;
        }

        public DataTable GetEmployeeDetails(string employeeacc)
        {
            return _helperService.GetSqlData("SELECT * FROM EMPLOYEE (nolock) WHERE CODE = @employeeacc", "@employeeacc", employeeacc);
        }

        public DataTable GetPunchRecords(string employeeacc, DateTime selecteddate, string store = "")
        {
            string dt = selecteddate.ToString("yyyy-MM-dd");
            return _helperService.GetSqlData("SELECT ACC,convert(date,CHECK_IN) as [DATE],ISNULL(CONVERT(VARCHAR(5),CHECK_IN,108),'00:00') AS IN_TIME,ISNULL(CONVERT(VARCHAR(5),CHECK_OUT,108),'00:00') AS OUT_TIME,NOTE,Store_no FROM WORK WHERE ACC= @employeeacc AND (CONVERT(date,CHECK_IN) = Convert(date,@selecteddate) or CONVERT(date,CHECK_OUT) = Convert(date,@selecteddate)) and Store_no = Iif('" + store + "' <> '', '" + store + "',store_no)",
                "@employeeacc", employeeacc, "@selecteddate", dt);
        }
        public DataTable GetEmpCodes()
        {
            return _helperService.GetSqlData("SELECT code FROM employee with (nolock) ORDER BY code");
        }
    }
}
