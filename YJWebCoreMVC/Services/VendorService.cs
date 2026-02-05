using Microsoft.Data.SqlClient;
using System.Data;

namespace YJWebCoreMVC.Services
{
    public class VendorService
    {

        private readonly ConnectionProvider _connectionProvider;
        private readonly HelperCommonService _helperCommonService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public VendorService(ConnectionProvider connectionProvider, HelperCommonService helperCommonService, IHttpContextAccessor httpContextAccessor, IWebHostEnvironment env)
        {
            _connectionProvider = connectionProvider;
            _helperCommonService = helperCommonService;
            _httpContextAccessor = httpContextAccessor;
        }

        public DataRow CheckValidVendorCode(string acc)
        {
            return _helperCommonService.GetSqlRow("select *  From vendors Where acc=@acc or oldvendorcode=@acc order by acc", "@acc", acc.Trim());
        }

        public DataTable SearchVendors(bool isCreditCard = false, string attribFilter = "1=1", bool chkCCrd = false)
        {
            string qry;

            if (chkCCrd)
            {
                if (isCreditCard)
                    qry = @"SELECT ACC,NAME,try_cast(TEL as Nvarchar(30)) as TEL,EMAIL,ADDR11,STATE1,ZIP1,CITY1,TERM,GL_CODE FROM VENDORS with (nolock) WHERE IS_CRD = 1 AND " + attribFilter + " order by acc";
                else
                    qry = @"SELECT ACC,NAME,try_cast(TEL as Nvarchar(30)) as TEL,EMAIL,ADDR11,STATE1,ZIP1,CITY1,TERM,GL_CODE FROM VENDORS with (nolock) WHERE IS_CRD = 0  AND " + attribFilter + " order by acc";
            }
            else if (isCreditCard)
                qry = @"SELECT ACC,NAME,try_cast(TEL as Nvarchar(30)) as TEL,EMAIL,ADDR11,STATE1,ZIP1,CITY1,TERM,GL_CODE FROM VENDORS with (nolock) WHERE IS_CRD = 1 order by acc";
            else
                qry = @"SELECT ACC,NAME,try_cast(TEL as Nvarchar(30)) as TEL,EMAIL,ADDR11,STATE1,ZIP1,CITY1,TERM,GL_CODE FROM VENDORS with (nolock) WHERE  " + attribFilter + " order by acc";
            return _helperCommonService.GetSqlData(qry);
        }

        public bool CancelCreditCheck(string acc, string invNo, string bank, string checkNo, string loggedUser, out string error)
        {
            error = string.Empty;

            try
            {
                using (SqlConnection connection = _connectionProvider.GetConnection())
                using (SqlCommand command = new SqlCommand("CancelCreditCheck", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandTimeout = 5000;

                    // Add parameters with explicit types
                    command.Parameters.Add(new SqlParameter("@acc", SqlDbType.NVarChar) { Value = acc });
                    command.Parameters.Add(new SqlParameter("@inv_no", SqlDbType.NVarChar) { Value = invNo });
                    command.Parameters.Add(new SqlParameter("@bank", SqlDbType.NVarChar) { Value = bank });
                    command.Parameters.Add(new SqlParameter("@check_no", SqlDbType.NVarChar) { Value = checkNo });
                    command.Parameters.Add(new SqlParameter("@LoggedUser", SqlDbType.NVarChar) { Value = loggedUser });

                    // Open connection and execute command
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();

                    return rowsAffected > 0;
                }
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return false;
            }
        }
        public DataTable CheckValidAPCredit(string inv_no, string Acc, bool IsDelete)
        {
            return _helperCommonService.GetStoreProc("CheckValidAPCredit", "@inv_no", inv_no, "@Acc", Acc.Trim(), "@IsDelete", IsDelete.ToString());
        }

        public DataTable GeAllVendorAcc()
        {
            return _helperCommonService.GetSqlData("select distinct acc from vendors with (nolock) order by acc");
        }
    }
}
