using Microsoft.Data.SqlClient;
using System.Data;

namespace YJWebCoreMVC.Services
{
    public class HelperManojService
    {

        private readonly ConnectionProvider _connectionProvider;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly HelperCommonService _helperCommonService;
        private readonly IWebHostEnvironment _env;
        private readonly string _companyName;
        private readonly string _storeCodeInUse;
        private readonly string _loggedUser;

        public HelperManojService(ConnectionProvider connectionProvider, IHttpContextAccessor httpContextAccessor, HelperCommonService helperCommonService, IWebHostEnvironment env)
        {
            _connectionProvider = connectionProvider;
            _httpContextAccessor = httpContextAccessor;
            _helperCommonService = helperCommonService;
            _companyName = _httpContextAccessor.HttpContext?.Session.GetString("COMPANYNAME");
            _storeCodeInUse = _httpContextAccessor.HttpContext?.Session.GetString("STORE_CODE");
            _loggedUser = _httpContextAccessor.HttpContext?.Session.GetString("UserId");
            _env = env;
        }
        public DataTable PrintDataTable { get; set; }
        public string GetLoginCodeBySetter(string cPerson)
        {
            return _helperCommonService.GetValue(_helperCommonService.GetSqlData($"SELECT LOGIN_CODE FROM SETTERS WHERE NAME='{cPerson}'"), "LOGIN_CODE");
        }

        public bool CheckJobOpen(string cPerson, string cJob)
        {
            return _helperCommonService.DataTableOK(_helperCommonService.GetSqlData(@"select * from ACTUALHOURS where ltrim(Trimmed_inv_no) = trim(@invno) and setter=@cPerson and closed_job=0", "@invno", cJob, "@cPerson", cPerson));
        }

        public bool CheckOpenJobByPerson(string cPerson, bool chkOtherPersonWorking = false)
        {
            return _helperCommonService.DataTableOK(_helperCommonService.GetSqlData(@"select * from ACTUALHOURS where  setter=@cPerson and closed_job=0", "@cPerson", cPerson));
        }


        public bool UpdateActualHours(string cPerson, string cJob, bool JobComplete, bool lCloseOpen, out string error)
        {
            error = string.Empty;
            try
            {
                using (var connection = _connectionProvider.GetConnection())
                using (var dbCommand = new SqlCommand("ACTUAL_HOURS", connection))
                {
                    dbCommand.CommandType = CommandType.StoredProcedure;
                    dbCommand.CommandTimeout = 3000;

                    // Add parameters
                    dbCommand.Parameters.AddWithValue("@cJobBag", cJob);
                    dbCommand.Parameters.AddWithValue("@cPerson", cPerson);
                    dbCommand.Parameters.AddWithValue("@lJobComplete", JobComplete ? 1 : 0);
                    dbCommand.Parameters.AddWithValue("@lCloseJobOpenNew", lCloseOpen ? 1 : 0);

                    connection.Open();
                    var rowsAffected = dbCommand.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
            catch (Exception ex)
            {
                error = ex.Message.Split(new[] { '\r', '\n' })[0];
                return false;
            }
        }

        public DataTable CalcDaysForQueuedJobs(decimal nHoursPerDay)
        {
            DataTable dataTable = new DataTable();
            using (var connection = _connectionProvider.GetConnection())
            using (var sqlDataAdapter = new SqlDataAdapter())
            {
                sqlDataAdapter.SelectCommand = new SqlCommand("CalcDaysForQueuedJobs", connection)
                {
                    CommandType = CommandType.StoredProcedure,
                    CommandTimeout = 3000
                };

                sqlDataAdapter.SelectCommand.Parameters.AddWithValue("@nHoursPerDay", nHoursPerDay);

                sqlDataAdapter.Fill(dataTable);
            }
            PrintDataTable = dataTable;
            return dataTable;
        }

        public string GetEmail(string acc)
        {
            DataTable dataTable = _helperCommonService.GetSqlData(@"SELECT ISNULL(EMAIL,'') as EMAIL FROM CUSTOMER with (nolock) WHERE ACC=@ACC", "@ACC", acc);
            return _helperCommonService.DataTableOK(dataTable) ? dataTable.Rows[0]["email"].ToString() : string.Empty;
        }

        public string GetAddressLabelFrom_InvoiceTbl(string acc, string seperator, string inv_no = "", bool isLandscape = false, string type = "I", bool isMemo = false)
        {
            string qry;
            if (!isMemo)
                qry = "select *,isnull(c.tel,0) tel  From Invoice I with (nolock) left outer join customer C with (nolock) on(C.acc=I.acc) Where Trimmed_inv_no=@inv_no";
            else
                qry = "select *,isnull(c.tel,0) tel  From Memo I with (nolock) left outer join customer C with (nolock) on(C.acc=I.acc) Where trim(memo_no)=@inv_no";

            DataRow dataRow = _helperCommonService.GetRowOne(_helperCommonService.GetSqlData(qry, "@acc", acc.Trim(), "@inv_no", inv_no.Trim()));
            DataRow CustRow = _helperCommonService.GetSqlRow("select *  From Customer with (nolock) Where acc=@acc", "@acc", acc);
            return _helperCommonService.GetAddressLabelFrom_InvoiceTbl(dataRow, CustRow, seperator, isLandscape, type, isMemo);
        }

        public bool HasHyperlink(string reportPath)
        {
            string xml = System.IO.File.ReadAllText(reportPath);

            return xml.IndexOf("<Hyperlink>", StringComparison.OrdinalIgnoreCase) >= 0
                || xml.IndexOf("<Drillthrough>", StringComparison.OrdinalIgnoreCase) >= 0;
        }

        public DataTable GetActualDuration(string cJob)
        {
            return _helperCommonService.GetSqlData("SELECT SETTER, [DATE], [TIME], RCV_DATE, RCV_TIME, ACTUAL_DURATION, ACTUAL_DURATION AS OLD_DURATION FROM ACTUALHOURS WHERE Trimmed_inv_no=@cJob order by date", "@cJob", cJob.Trim());
        }

        public bool AdjustActualDuration(string cJobbag, string cTableString, out string error)
        {
            error = string.Empty;
            try
            {
                using (var connection = _connectionProvider.GetConnection())
                using (var dbCommand = new SqlCommand("AdjustActualDuration", connection))
                {
                    dbCommand.CommandType = CommandType.StoredProcedure;
                    dbCommand.CommandTimeout = 3000;

                    dbCommand.Parameters.AddWithValue("@cJobBag", cJobbag);
                    dbCommand.Parameters.AddWithValue("@TBLRTVITEMS", cTableString);

                    connection.Open();
                    var rowsAffected = dbCommand.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
            catch (Exception ex)
            {
                error = ex.Message.Split(new[] { '\r', '\n' })[0];
                return false;
            }
        }


        public DataTable GetEstimateTemplates(string cTemplate = "")
        {
            if (!string.IsNullOrWhiteSpace(cTemplate))
                return _helperCommonService.GetSqlData("Select *  from ESTIMATES_TEMPLATE where Template_Name = @cTemplate", "@cTemplate", cTemplate);
            return _helperCommonService.GetSqlData("Select DISTINCT Template_Name from ESTIMATES_TEMPLATE with(nolock) order by Template_Name");
        }

        public DataTable GetEstimatesByJob(string cJob = "")
        {
            if (string.IsNullOrWhiteSpace(cJob))
                return _helperCommonService.GetSqlData("SELECT SETTER, WORK_HOURS FROM ESTIMATES WHERE 1=0");
            return _helperCommonService.GetSqlData("SELECT SETTER, WORK_HOURS FROM ESTIMATES WHERE Trimmed_inv_no=@cJob", "@cJob", cJob.Trim());
        }

        public DataTable checkImageInStyimages(string jobbagno)
        {
            return _helperCommonService.GetSqlData(@"select * from styl_images with (nolock) where style = @jobbagno", "@jobbagno", jobbagno);
        }

        public DataTable GetRepairItemwithjobbag(string jobbbag)
        {
            return _helperCommonService.GetSqlData(@"SELECT * FROM REP_ITEM with (nolock) WHERE barcode = @jobbbag", "@jobbbag", jobbbag.Trim());
        }

        public bool UpdateEstimateTemplate(string cTemplate, string grid1XmlData, out string error)
        {
            error = string.Empty;
            try
            {
                using (var connection = _connectionProvider.GetConnection())
                using (var dbCommand = new SqlCommand("UPDESTTEMPLATE", connection))
                {
                    dbCommand.CommandType = CommandType.StoredProcedure;
                    dbCommand.Parameters.AddWithValue("@cTemplate", cTemplate);
                    dbCommand.Parameters.AddWithValue("@grid1XmlData", grid1XmlData);
                    dbCommand.CommandTimeout = 3000;

                    connection.Open();
                    var rowsAffected = dbCommand.ExecuteNonQuery();

                    return rowsAffected > 0;
                }
            }
            catch (Exception ex)
            {
                error = ex.Message.Split(new char[] { '\r', '\n' })[0];
                return false;
            }
        }

        public bool UpdateEstimates(string cJob, string grid1XmlData, out string error)
        {
            error = string.Empty;

            try
            {
                using (var connection = _connectionProvider.GetConnection())
                using (var dbCommand = new SqlCommand("UpdateEstimates", connection))
                {
                    dbCommand.CommandType = CommandType.StoredProcedure;
                    dbCommand.Parameters.AddWithValue("@cJobBag", cJob);
                    dbCommand.Parameters.AddWithValue("@grid1XmlData", grid1XmlData);
                    dbCommand.CommandTimeout = 3000;

                    connection.Open();
                    var rowsAffected = dbCommand.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
            catch (Exception ex)
            {
                error = ex.Message.Split(new[] { '\r', '\n' })[0];
                return false;
            }
        }

        public bool DeleteEstTemplate(string cTemplate)
        {
            try
            {
                using (var connection = _connectionProvider.GetConnection())
                using (var dbCommand = new SqlCommand("DELETE FROM ESTIMATES_TEMPLATE WHERE Template_Name=@cTemplate", connection))
                {
                    dbCommand.CommandType = CommandType.Text;
                    dbCommand.Parameters.AddWithValue("@cTemplate", cTemplate);

                    connection.Open();
                    var rowsAffected = dbCommand.ExecuteNonQuery();

                    return rowsAffected > 0;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while deleting the estimate template", ex);
            }
        }
    }
}
