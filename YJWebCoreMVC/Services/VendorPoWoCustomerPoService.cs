/* Created By Dharani on 02/04/2026
 * 
 */

using Microsoft.Data.SqlClient;
using System.Data;

namespace YJWebCoreMVC.Services
{
    public class VendorPoWoCustomerPoService
    {
        private readonly ConnectionProvider _connectionProvider;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly HelperCommonService _helperCommonService;
        private readonly IWebHostEnvironment _env;
        private readonly string _companyName;
        private readonly string _storeCodeInUse;
        private readonly string _loggedUser;

        public VendorPoWoCustomerPoService(ConnectionProvider connectionProvider, IHttpContextAccessor httpContextAccessor, HelperCommonService helperCommonService, IWebHostEnvironment env)
        {
            _connectionProvider = connectionProvider;
            _httpContextAccessor = httpContextAccessor;
            _helperCommonService = helperCommonService;
            _companyName = _httpContextAccessor.HttpContext?.Session.GetString("COMPANYNAME");
            _storeCodeInUse = _httpContextAccessor.HttpContext?.Session.GetString("STORE_CODE");
            _loggedUser = _httpContextAccessor.HttpContext?.Session.GetString("UserId");
            _env = env;
        }

        public DataTable addVendorPo_AP(string xml, string fdate = "", string tdate = "", bool isAllDates = false, bool includePrevious = false, bool printList = false)
        {
            var dataTable = new DataTable();

            using (var connection = _connectionProvider.GetConnection())
            using (var command = new SqlCommand("addVendorPo_AP", connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add(new SqlParameter("@XMLDATA", SqlDbType.Xml) { Value = xml });
                command.Parameters.Add(new SqlParameter("@fdate", SqlDbType.Date) { Value = fdate });
                command.Parameters.Add(new SqlParameter("@tdate", SqlDbType.Date) { Value = tdate });
                command.Parameters.AddWithValue("@isAlldates", isAllDates);
                command.Parameters.AddWithValue("@isshowprevious", includePrevious);
                command.Parameters.AddWithValue("@PrintList", printList);

                using (var adapter = new SqlDataAdapter(command))
                {
                    adapter.Fill(dataTable);
                }
            }
            return dataTable;
        }
    }
}
