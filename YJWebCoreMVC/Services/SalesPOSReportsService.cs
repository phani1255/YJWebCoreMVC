/*
 * Manoj 02/04/2026 Created File
 * Manoj 05/20/2025 Added CharityDonations and getListofCharityDonations methods
 * Manoj 05/23/2025 Fixed Date and DDL Filter issue while fetch data 
 */

using System.Data;


namespace YJWebCoreMVC.Services
{
    public class SalesPOSReportsService
    {

        private readonly ConnectionProvider _connectionProvider;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly HelperCommonService _helperCommonService;
        private readonly IWebHostEnvironment _env;
        private readonly string _companyName;
        private readonly string _storeCodeInUse;
        private readonly string _loggedUser;

        public SalesPOSReportsService(ConnectionProvider connectionProvider, IHttpContextAccessor httpContextAccessor,
            HelperCommonService helperCommonService, IWebHostEnvironment env)
        {
            _connectionProvider = connectionProvider;
            _httpContextAccessor = httpContextAccessor;
            _helperCommonService = helperCommonService;
            _companyName = _httpContextAccessor.HttpContext?.Session.GetString("COMPANYNAME");
            _storeCodeInUse = _httpContextAccessor.HttpContext?.Session.GetString("STORE_CODE");
            _loggedUser = _httpContextAccessor.HttpContext?.Session.GetString("UserId");
            _env = env;
        }


        public DataTable getCharitiesList()
        {
            return _helperCommonService.GetSqlData("select * from charities with (nolock) order by charity");
        }

        public DataTable getCharityDonations(string sFilter = "")
        {
            string query = "select inv_no,CAST(date AS date) as date,name,charity,charity_amount from invoice with (nolock) where charity_amount > 0";


            if (!string.IsNullOrEmpty(sFilter))
            {
                query = "select inv_no,CAST(date AS date) as date,name,charity,charity_amount from invoice with (nolock) where charity_amount > 0" + sFilter;
            }


            return _helperCommonService.GetSqlData(query);
        }


    }
}
