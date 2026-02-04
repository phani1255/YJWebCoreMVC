using Microsoft.Data.SqlClient;
using System.Data;

namespace YJWebCoreMVC.Services
{
    public class HelperVenkatService
    {
        private readonly ConnectionProvider _connectionProvider;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly HelperCommonService _helperCommonService;
        private readonly IWebHostEnvironment _env;
        private readonly string _companyName;
        private readonly string _storeCodeInUse;
        private readonly string _loggedUser;

        public HelperVenkatService(ConnectionProvider connectionProvider, IHttpContextAccessor httpContextAccessor, HelperCommonService helperCommonService, IWebHostEnvironment env)
        {
            _connectionProvider = connectionProvider;
            _httpContextAccessor = httpContextAccessor;
            _helperCommonService = helperCommonService;
            _companyName = _httpContextAccessor.HttpContext?.Session.GetString("COMPANYNAME");
            _storeCodeInUse = _httpContextAccessor.HttpContext?.Session.GetString("STORE_CODE");
            _loggedUser = _httpContextAccessor.HttpContext?.Session.GetString("UserId");
            _env = env;
        }
        public object MsgBox(string message, string title = "Info")
        {
            return new { showMsg = true, message, title };
        }
        public DataRow getrapnetuserdetails(bool iseditStyleRetStoneInfo = false, bool isIGI = false)
        {
            return _helperCommonService.GetSqlRow(@"SELECT rap_usr,rap_pw,cert_path,gia_key FROM ups_ins with (nolock)");
        }

        public void UpdateRapnetTable(DataTable rapnetdata)
        {
            if (!_helperCommonService.DataTableOK(rapnetdata))
                throw new ArgumentException("The provided DataTable is null or empty.", nameof(rapnetdata));

            using (var connection = _connectionProvider.GetConnection())
            using (var command = new SqlCommand("UpdateRapnetTable", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                var tableParameter = command.Parameters.AddWithValue("@TBLRAPITEMS", rapnetdata);
                tableParameter.SqlDbType = SqlDbType.Structured;
                connection.Open();
                command.ExecuteNonQuery();
            }
        }
        public DataRow getHighestCustAccValue(int minVal, int maxVal, bool isPotentialCust = false)
        {
            using (var connection = _connectionProvider.GetConnection())
            using (var command = new SqlCommand("GetNextCustomer", connection))
            using (var adapter = new SqlDataAdapter(command))
            {
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add("@minValue", SqlDbType.Int).Value = minVal;
                command.Parameters.Add("@maxValue", SqlDbType.Int).Value = maxVal;
                command.Parameters.Add("@IsPotentialCust", SqlDbType.Bit).Value = isPotentialCust;

                var dataTable = new DataTable();
                connection.Open();
                adapter.Fill(dataTable);

                return dataTable.Rows.Count > 0 ? dataTable.Rows[0] : null;
            }
        }

        public T CheckForNull<T>(object objval)
        {
            if (objval == null || objval == DBNull.Value)
                return default(T);

            return (T)objval;
        }

        public string GetAdminMenuPassword()
        {
            return _helperCommonService.GetSqlRow(@"SELECT pwd FROM ups_ins")[0].ToString();
        }

        public DataTable ListOfAllUsers()
        {
            return _helperCommonService.GetSqlData("SELECT name,level,code,username,role,iif(isnull(inactive,0)=0,'NO','YES') inactive FROM passfile where isnull(name,'')<>'' order by name");
        }
        public DataTable GetListOfUserRoles()
        {
            return _helperCommonService.GetSqlData("SELECT [NAME] Role, Previlage, Notes FROM UserRoles");
        }

    }
}
