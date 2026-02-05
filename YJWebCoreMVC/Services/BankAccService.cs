using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using System.Data;
using YJWebCoreMVC.Models;

namespace YJWebCoreMVC.Services
{
    public class BankAccService
    {
        private readonly ConnectionProvider _connectionProvider;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly HelperCommonService _helperCommonService;

        public BankAccService(ConnectionProvider connectionProvider, IHttpContextAccessor httpContextAccessor, HelperCommonService helperCommonService, IWebHostEnvironment env)
        {
            _connectionProvider = connectionProvider;
            _httpContextAccessor = httpContextAccessor;
            _helperCommonService = helperCommonService;
        }

        public DataRow GetBankBycode(string bcode)
        {
            return _helperCommonService.GetSqlRow("SELECT * FROM BANK_ACC WHERE CODE = @CODE", "@CODE", bcode);
        }

        public DataTable ShowPaymentData(string bcode, DateTime fdate, DateTime tdate, string store = "")
        {
            string s1 = fdate.ToString("MM-dd-yyyy");
            string s2 = tdate.ToString("MM-dd-yyyy");
            string qry = "SELECT P.INV_NO AS [Receipt No.], P.CHK_DATE AS [Date], P.ACC AS [Customer Code]," +
                "P.Store_no StoreNo,P.PaymentType, C.[NAME] AS Description, P.PAID-P.DISCOUNT AS Amount, " +
                "CAST(0 AS BIT) Deposited, P.RTV_PAY AS RTV_Pay " +
                "FROM PAYMENTS P LEFT JOIN CUSTOMER C ON P.ACC = C.ACC " +
                "WHERE cast(P.CHK_DATE as date)>= '" + s1 + "' AND cast(P.CHK_DATE as date)< '" + s2 +
                "' AND ISNULL(DEPNO, 0) = 0 AND " +
                "(ISNULL(BANK,'') = '" + bcode + "' OR ISNULL(BANK,'')='') AND P.PAID<>0 AND P.DEP_DT IS NULL " +
                "AND UPPER(P.PAYMENTTYPE)='CHECK' AND UPPER(P.RTV_PAY)='P' AND " +
                "P.STORE_NO= IIF('" + store + "'='',p.store_no,'" + store + "') ORDER BY [Date] Desc, [Receipt No.]  Desc ";
            return _helperCommonService.GetSqlData(qry);
        }

        public DataTable AddDeposits(string bcode, DateTime fdate, DateTime tdate, DateTime depositdate, string XML, bool IsCreditCard, string StoreCode, string loggeduser)
        {
            return _helperCommonService.GetStoreProc("ADDBANKDEPOSIT", "@BCODE", bcode, "@FDATE", Convert.ToDateTime(fdate).ToString("MM/dd/yyyy"), "@TDATE", Convert.ToDateTime(tdate).ToString("MM/dd/yyyy"), "@DEPOSITDATE", Convert.ToDateTime(depositdate).ToString("MM/dd/yyyy"), "@BANKDEPOSITS", XML, "@CreditCard", IsCreditCard.ToString(), "@StoreCode", StoreCode, "@loggeduser", loggeduser);
        }

        public List<SelectListItem> GetGLCodesList()
        {
            List<SelectListItem> glCodeList = new List<SelectListItem>
            {
                new SelectListItem { Text = "", Value = "" }
            };

            try
            {
                using (SqlConnection conn = _connectionProvider.GetConnection())
                using (SqlCommand cmd = new SqlCommand(@"SELECT LTRIM(RTRIM(ACC)) AS GLCODE, LTRIM(RTRIM(Name)) AS [ACCOUNT NAME] FROM gl_accs ORDER BY GLCODE", conn))
                using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    foreach (DataRow row in dt.Rows)
                    {
                        string code = row["GLCODE"].ToString().Trim();
                        string accountName = row["ACCOUNT NAME"].ToString().Trim();
                        glCodeList.Add(new SelectListItem
                        {
                            Text = $"{code} - {accountName}",
                            Value = code
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return glCodeList;
        }

        public List<SelectListItem> GetGLDeptsList()
        {
            List<SelectListItem> glDeptList = new List<SelectListItem>
            {
                new SelectListItem { Text = "", Value = "" }
            };

            try
            {
                using (SqlConnection conn = _connectionProvider.GetConnection())
                using (SqlCommand cmd = new SqlCommand(@"
                                                        SELECT LTRIM(RTRIM(DEPT)) AS GLDEPT, LTRIM(RTRIM(Name)) AS [ACCOUNT NAME]
                                                        FROM gl_dept
                                                        ORDER BY GLDEPT
                                                    ", conn))
                using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    foreach (DataRow row in dt.Rows)
                    {
                        string dept = row["GLDEPT"].ToString().Trim();
                        string accountName = row["ACCOUNT NAME"].ToString().Trim();
                        glDeptList.Add(new SelectListItem
                        {
                            Text = $"{dept} - {accountName}",
                            Value = dept
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return glDeptList;
        }

        public bool AddBankacc(BankACCModel bank)
        {
            using (SqlCommand dbCommand = new SqlCommand())
            {
                dbCommand.Connection = _connectionProvider.GetConnection();
                dbCommand.CommandType = CommandType.Text;
                dbCommand.CommandText = @"INSERT INTO bank_acc (CODE,[DESC],LAST_INV,IS_QB,QB_NAME,BANK_ACC_NUM,BANK_ROUT_NUM,ACCT_OPEN_DATE,BANK_NAME,BANK_ADDR1,BANK_ADDR2,BANK_ADDR3,BANK_ADDR4,USER_NAME,USER_ADDR1,USER_ADDR2,USER_ADDR3,USER_ADDR4,GL_CODE,GL_DEPT,store_no)
                                                          VALUES(@CODE,@DESC,@LAST_INV,@IS_QB,@QB_NAME,@BANK_ACC_NUM,@BANK_ROUT_NUM,@ACCT_OPEN_DATE,@BANK_NAME,@BANK_ADDR1,@BANK_ADDR2,@BANK_ADDR3,@BANK_ADDR4,@USER_NAME,@USER_ADDR1,@USER_ADDR2,@USER_ADDR3,@USER_ADDR4,@GL_CODE,@GL_DEPT,@storeName)";

                dbCommand.Parameters.AddWithValue("@CODE", string.IsNullOrEmpty(bank.CODE) ? "" : bank.CODE);
                dbCommand.Parameters.AddWithValue("@DESC", string.IsNullOrEmpty(bank.DESC) ? "" : bank.DESC);
                dbCommand.Parameters.AddWithValue("@LAST_INV", string.IsNullOrEmpty(bank.LAST_INV) ? "" : bank.LAST_INV);

                dbCommand.Parameters.AddWithValue("@IS_QB", bank.IS_QB);
                dbCommand.Parameters.AddWithValue("@QB_NAME", string.IsNullOrEmpty(bank.QB_NAME) ? "" : bank.QB_NAME);
                dbCommand.Parameters.AddWithValue("@BANK_ACC_NUM", string.IsNullOrEmpty(bank.BANK_ACC_NUM) ? "" : bank.BANK_ACC_NUM);

                dbCommand.Parameters.AddWithValue("@BANK_ROUT_NUM", string.IsNullOrEmpty(bank.BANK_ROUT_NUM) ? "" : bank.BANK_ROUT_NUM);
                dbCommand.Parameters.AddWithValue("@ACCT_OPEN_DATE", bank.ACCT_OPEN_DATE);
                dbCommand.Parameters.AddWithValue("@BANK_NAME", string.IsNullOrEmpty(bank.BANK_NAME) ? "" : bank.BANK_NAME);

                dbCommand.Parameters.AddWithValue("@BANK_ADDR1", string.IsNullOrEmpty(bank.BANK_ADDR1) ? "" : bank.BANK_ADDR1);
                dbCommand.Parameters.AddWithValue("@BANK_ADDR2", string.IsNullOrEmpty(bank.BANK_ADDR2) ? "" : bank.BANK_ADDR2);
                dbCommand.Parameters.AddWithValue("@BANK_ADDR3", string.IsNullOrEmpty(bank.BANK_ADDR3) ? "" : bank.BANK_ADDR3);

                dbCommand.Parameters.AddWithValue("@BANK_ADDR4", string.IsNullOrEmpty(bank.BANK_ADDR4) ? "" : bank.BANK_ADDR4);
                dbCommand.Parameters.AddWithValue("@USER_NAME", string.IsNullOrEmpty(bank.USER_NAME) ? "" : bank.USER_NAME);
                dbCommand.Parameters.AddWithValue("@USER_ADDR1", string.IsNullOrEmpty(bank.USER_ADDR1) ? "" : bank.USER_ADDR1);

                dbCommand.Parameters.AddWithValue("@USER_ADDR2", string.IsNullOrEmpty(bank.USER_ADDR2) ? "" : bank.USER_ADDR2);
                dbCommand.Parameters.AddWithValue("@USER_ADDR3", string.IsNullOrEmpty(bank.USER_ADDR3) ? "" : bank.USER_ADDR3);
                dbCommand.Parameters.AddWithValue("@USER_ADDR4", string.IsNullOrEmpty(bank.USER_ADDR4) ? "" : bank.USER_ADDR4);

                dbCommand.Parameters.AddWithValue("@GL_CODE", string.IsNullOrEmpty(bank.GL_CODE) ? "" : bank.GL_CODE);
                dbCommand.Parameters.AddWithValue("@GL_DEPT", string.IsNullOrEmpty(bank.GL_DEPT) ? "" : bank.GL_DEPT);
                dbCommand.Parameters.AddWithValue("@storeName", string.IsNullOrEmpty(bank.Storename) ? "" : bank.Storename);
                dbCommand.Connection.Open();
                var rowsAffected = dbCommand.ExecuteNonQuery();
                dbCommand.Connection.Close();

                return rowsAffected > 0;
            }
        }

        public DataTable GetBankAccounts(string MyStore)
        {
            return _helperCommonService.GetSqlData("SELECT * FROM BANK_ACC where @MyStore='' or @MyStore=store_no or store_no='' order by code ", "@MyStore", MyStore);
        }

        public bool EditBankacc(BankACCModel bank)
        {
            using (SqlCommand dbCommand = new SqlCommand())
            {
                dbCommand.Connection = _connectionProvider.GetConnection();
                dbCommand.CommandType = CommandType.Text;
                dbCommand.CommandText = @"Update bank_acc set [DESC]=@DESC,LAST_INV=@LAST_INV,IS_QB=@IS_QB,QB_NAME=@QB_NAME,BANK_ACC_NUM=@BANK_ACC_NUM,BANK_ROUT_NUM=@BANK_ROUT_NUM,ACCT_OPEN_DATE= @ACCT_OPEN_DATE,
                                        BANK_NAME=@BANK_NAME,BANK_ADDR1=@BANK_ADDR1,BANK_ADDR2=@BANK_ADDR2,BANK_ADDR3=@BANK_ADDR3,BANK_ADDR4=@BANK_ADDR4,USER_NAME=@USER_NAME,USER_ADDR1=@USER_ADDR1,USER_ADDR2=@USER_ADDR2,
                                        USER_ADDR3= @USER_ADDR3,USER_ADDR4=@USER_ADDR4,GL_CODE=@GL_CODE,GL_DEPT=@GL_DEPT,store_no =@storeName
                                        Where CODE = @CODE";

                dbCommand.Parameters.AddWithValue("@CODE", string.IsNullOrEmpty(bank.CODE) ? "" : bank.CODE);
                dbCommand.Parameters.AddWithValue("@DESC", string.IsNullOrEmpty(bank.DESC) ? "" : bank.DESC);
                dbCommand.Parameters.AddWithValue("@LAST_INV", string.IsNullOrEmpty(bank.LAST_INV) ? "" : bank.LAST_INV);

                dbCommand.Parameters.AddWithValue("@IS_QB", bank.IS_QB);
                dbCommand.Parameters.AddWithValue("@QB_NAME", string.IsNullOrEmpty(bank.QB_NAME) ? "" : bank.QB_NAME);
                dbCommand.Parameters.AddWithValue("@BANK_ACC_NUM", string.IsNullOrEmpty(bank.BANK_ACC_NUM) ? "" : bank.BANK_ACC_NUM);

                dbCommand.Parameters.AddWithValue("@BANK_ROUT_NUM", string.IsNullOrEmpty(bank.BANK_ROUT_NUM) ? "" : bank.BANK_ROUT_NUM);
                dbCommand.Parameters.AddWithValue("@ACCT_OPEN_DATE", bank.ACCT_OPEN_DATE);
                dbCommand.Parameters.AddWithValue("@BANK_NAME", string.IsNullOrEmpty(bank.BANK_NAME) ? "" : bank.BANK_NAME);

                dbCommand.Parameters.AddWithValue("@BANK_ADDR1", string.IsNullOrEmpty(bank.BANK_ADDR1) ? "" : bank.BANK_ADDR1);
                dbCommand.Parameters.AddWithValue("@BANK_ADDR2", string.IsNullOrEmpty(bank.BANK_ADDR2) ? "" : bank.BANK_ADDR2);
                dbCommand.Parameters.AddWithValue("@BANK_ADDR3", string.IsNullOrEmpty(bank.BANK_ADDR3) ? "" : bank.BANK_ADDR3);

                dbCommand.Parameters.AddWithValue("@BANK_ADDR4", string.IsNullOrEmpty(bank.BANK_ADDR4) ? "" : bank.BANK_ADDR4);
                dbCommand.Parameters.AddWithValue("@USER_NAME", string.IsNullOrEmpty(bank.USER_NAME) ? "" : bank.USER_NAME);
                dbCommand.Parameters.AddWithValue("@USER_ADDR1", string.IsNullOrEmpty(bank.USER_ADDR1) ? "" : bank.USER_ADDR1);

                dbCommand.Parameters.AddWithValue("@USER_ADDR2", string.IsNullOrEmpty(bank.USER_ADDR2) ? "" : bank.USER_ADDR2);
                dbCommand.Parameters.AddWithValue("@USER_ADDR3", string.IsNullOrEmpty(bank.USER_ADDR3) ? "" : bank.USER_ADDR3);
                dbCommand.Parameters.AddWithValue("@USER_ADDR4", string.IsNullOrEmpty(bank.USER_ADDR4) ? "" : bank.USER_ADDR4);

                dbCommand.Parameters.AddWithValue("@GL_CODE", string.IsNullOrEmpty(bank.GL_CODE) ? "" : bank.GL_CODE);
                dbCommand.Parameters.AddWithValue("@GL_DEPT", string.IsNullOrEmpty(bank.GL_DEPT) ? "" : bank.GL_DEPT);
                dbCommand.Parameters.AddWithValue("@storeName", string.IsNullOrEmpty(bank.Storename) ? "" : bank.Storename);

                dbCommand.Connection.Open();
                var rowsAffected = dbCommand.ExecuteNonQuery();
                dbCommand.Connection.Close();

                return rowsAffected > 0;
            }
        }

        public DataTable GetBankAccountsList(string MyStore, string sortBy)
        {
            string orderBy = sortBy == "bankName" ? "BANK_NAME" : "CODE";
            return _helperCommonService.GetSqlData($@"SELECT CODE, [DESC], BANK_NAME, BANK_ACC_NUM FROM BANK_ACC WHERE @MyStore='' OR @MyStore=store_no OR store_no='' ORDER BY {orderBy}", "@MyStore", MyStore);
        }

        public bool AddDefaultBank(string setDefaultBank, out string error, bool isRefund = false)
        {
            error = string.Empty;
            using (var connection = _connectionProvider.GetConnection())
            using (var command = new SqlCommand("DefaultBank", connection))
            {
                // Configure the command
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@CODE", SqlDbType.VarChar).Value = setDefaultBank ?? string.Empty;
                command.Parameters.Add("@ISREFUND", SqlDbType.Bit).Value = isRefund;

                // Open connection and execute
                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();

                return rowsAffected > 0;
            }
        }
        public DataTable GetBankcode(string MyStore)
        {
            return _helperCommonService.GetSqlData(@"select '' as CODE From bank_acc union select code from bank_acc 
                where @MyStore='' or @MyStore=store_no or store_no='' order by code", "@MyStore", MyStore);
        }
        public DataTable EditDeposits(string depno, string bcode, DateTime depositdate, string XML, string StoreCode, string loggeduser)
        {
            return _helperCommonService.GetStoreProc("EDITBANKDEPOSIT", "@DEPNO", depno, "@BCODE", bcode, "@DEPOSITDATE", depositdate.ToString("yyyy-MM-dd HH:mm:ss"), "BANKDEPOSITS", XML, "@StoreCode", StoreCode, "@loggeduser", loggeduser);

        }
        public bool DeleteDeposit(int id)
        {
            using (SqlCommand dbCommand = new SqlCommand())
            {

                dbCommand.Connection = _connectionProvider.GetConnection();
                dbCommand.CommandType = CommandType.Text;
                dbCommand.CommandText = "UPDATE payments SET depno = 0, dep_dt = NULL WHERE depno = @INV_NO; DELETE FROM deposits WHERE depno = @INV_NO; DELETE FROM bank WHERE depno = @INV_NO; DELETE FROM GLPOST WHERE INV_NO = @INV_NO AND TYPE='D';";

                dbCommand.Parameters.AddWithValue("@INV_NO", id);

                dbCommand.Connection.Open();
                var rowsAffected = dbCommand.ExecuteNonQuery();
                dbCommand.Connection.Close();

                return rowsAffected > 0;
            }
        }
        public DataTable PrintDepositByCode(string dCode, string bCode, string dMode)
        {
            return _helperCommonService.GetStoreProc("PRINTDEPOSITBYCODE", "@depCode", dCode, "@bankCode", bCode, "@depMode", dMode);
        }
        public DataTable ShowCreditCardPaymentData(string bcode, DateTime fdate, DateTime tdate, string store = "", bool isincldchecks = false)
        {
            DataTable dataTable = new DataTable();
            // Create the command and set its properties
            using (SqlDataAdapter SqlDataAdapter = new SqlDataAdapter())
            {
                // Create the command and set its properties
                SqlDataAdapter.SelectCommand = new SqlCommand();
                SqlDataAdapter.SelectCommand.Connection = _connectionProvider.GetConnection();
                SqlDataAdapter.SelectCommand.CommandType = CommandType.Text;

                SqlDataAdapter.SelectCommand.CommandText = "SELECT P.INV_NO AS [Receipt No.], P.CHK_DATE AS [Date], P.ACC AS [Customer Code],P.Store_no StoreNo, P.Paymenttype, " +
                    "C.[NAME] AS Description,P.PAID-P.DISCOUNT AS Amount, cast((P.PAID-P.DISCOUNT)*ISNULL(PT.BANK_FEE,0)/100 as decimal(13,2)) [Bank Fee], CAST(ISNULL(PT.TRANS_FEE,0) as decimal(13,2)) [Trans. Fee] " +
                    ",CAST((((P.PAID-P.DISCOUNT)*(100-ISNULL(PT.BANK_FEE,0))/100)) - ISNULL(PT.TRANS_FEE,0) as decimal(13,2)) [NetAmount], CAST(0 AS BIT) Deposited, " +
                    "P.RTV_PAY AS RTV_Pay FROM PAYMENTS P LEFT JOIN CUSTOMER C ON P.ACC = C.ACC " +
                    "INNER JOIN PAYMENTTYPES PT ON PT.PAYMENTTYPE = P.PAYMENTTYPE WHERE cast(chk_date as date) between cast(@date1 as date) and cast(@date2 as date) " +
                    " AND ISNULL(DEPNO, 0) = 0 and (isnull(PT.REQUIRES_DEPOSIT,0)=1 and " +
                    " (isnull(P.PAYMENTTYPE,'')<>'CHECK' or @isincldchecks=1 )) and chk_amt<>0 and " +
                    " P.STORE_NO= IIF(@store='',p.store_no,@store) AND P.PAID<>0   and (isnull(bank,'')=isnull(@bcode,'') or isnull(bank,'')='') ORDER BY [Date] Desc";
                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@date1", fdate);
                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@date2", tdate);
                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@store", store);
                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@bcode", bcode);
                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@isincldchecks", isincldchecks);
                SqlDataAdapter.Fill(dataTable);
            }
            return dataTable;
        }
        public DataTable ShowDeposits(string bcode, DateTime fdate, DateTime tdate)
        {
            return _helperCommonService.GetStoreProc("DEPOSITSLIST", "@BANKCODE", bcode, "@DATE1", fdate.ToString(), "@DATE2", tdate.ToString());
        }

        public string GetNextTransNo()
        {
            DataRow dataRow = _helperCommonService.GetSqlRow("SELECT ISNULL(MAX(CAST(INV_NO AS INT)),0)+1 INV_NO FROM BANK");
            return dataRow["INV_NO"].ToString();
        }
        public DataRow GetTransactionBycode(string tcode)
        {
            return _helperCommonService.GetSqlRow("SELECT * FROM BANK Where Trimmed_inv_no = trim(@INV_NO)", "@INV_NO", tcode);
        }
        public bool AddBankTransaction(bankTransactionModel bank)
        {
            using (SqlCommand dbCommand = new SqlCommand())
            {
                // Set the command object properties
                dbCommand.Connection = _connectionProvider.GetConnection();
                dbCommand.CommandType = CommandType.StoredProcedure;
                dbCommand.CommandText = @"Post_GL_BANKTRANS";

                dbCommand.Parameters.AddWithValue("@INV_NO", string.IsNullOrEmpty(bank.INV_NO) ? "" : bank.INV_NO);
                dbCommand.Parameters.AddWithValue("@DATE", bank.DATE != DateTime.MinValue ? bank.DATE : (object)DBNull.Value);
                dbCommand.Parameters.AddWithValue("@DESC", string.IsNullOrEmpty(bank.DESC) ? "" : bank.DESC);

                dbCommand.Parameters.AddWithValue("@DEB_CRD", string.IsNullOrEmpty(bank.DEB_CRD) ? "" : bank.DEB_CRD);
                dbCommand.Parameters.AddWithValue("@AMOUNT", bank.AMOUNT != 0 ? bank.AMOUNT : (object)0);

                dbCommand.Parameters.AddWithValue("@BANK", string.IsNullOrEmpty(bank.BANK) ? "" : bank.BANK);

                dbCommand.Parameters.AddWithValue("@ENTER_DATE", bank.ENTER_DATE != DateTime.MinValue ? bank.ENTER_DATE : (object)DBNull.Value);
                dbCommand.Parameters.AddWithValue("@CHK_NO", string.IsNullOrEmpty(bank.CHK_NO) ? "" : bank.CHK_NO);
                dbCommand.Parameters.AddWithValue("@GL_CODE", string.IsNullOrEmpty(bank.GL_CODE) ? "" : bank.GL_CODE);

                dbCommand.Parameters.AddWithValue("@CLRD", string.IsNullOrEmpty(bank.CLRD) ? "" : bank.CLRD);
                dbCommand.Parameters.AddWithValue("@DEPNO", bank.DEPNO != 0 ? bank.DEPNO : (object)0);
                dbCommand.Parameters.AddWithValue("@ON_QB", bank.ON_QB.HasValue ? (object)bank.ON_QB.Value : (object)DBNull.Value);

                dbCommand.Parameters.AddWithValue("@LOGNO", string.IsNullOrEmpty(bank.GL_LOG) ? "" : bank.GL_LOG);
                dbCommand.Parameters.AddWithValue("@LOGGEDUSER", string.IsNullOrEmpty(bank.LOGGEDUSER) ? "" : bank.LOGGEDUSER);

                dbCommand.Parameters.AddWithValue("@ISEDIT", bank.ISEDIT.HasValue ? (object)bank.ISEDIT.Value : (object)DBNull.Value);
                dbCommand.Parameters.AddWithValue("@MODE", string.IsNullOrEmpty(bank.MODE) ? "" : bank.MODE);
                dbCommand.Parameters.AddWithValue("@Isreconflag", bank.Isreconflag);

                // Open the connection, execute the query and close the connection
                dbCommand.Connection.Open();
                var rowsAffected = dbCommand.ExecuteNonQuery();
                dbCommand.Connection.Close();

                return rowsAffected > 0;
            }
        }
        public DataTable ShowTransactions(string bcode, string fdate, string tdate, string tmode = "", string reconLog = "", bool CheckUncleared = false)
        {
            return _helperCommonService.GetStoreProc("ShowTransactions", "@bcode", bcode, "@date1", fdate, "@date2", tdate, "@MODE", tmode, "@LogNo", reconLog, "@CheckUnCleared", CheckUncleared ? "1" : "0");
        }
        public string ShowBal(string bcode, bool checkUnCleared = false)
        {
            using (var connection = _connectionProvider.GetConnection())
            using (var command = new SqlCommand("ShowBal", connection))
            {
                // Configure the command
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@bank", SqlDbType.VarChar).Value = bcode;
                command.Parameters.Add("@CheckUnCleared", SqlDbType.Bit).Value = checkUnCleared;

                // Open connection and execute command
                connection.Open();
                object result = command.ExecuteScalar();

                // Return the result or "0" if null
                return result != null ? result.ToString() : "0";
            }
        }
        public bool AddNewBankTransfer(string bcodeFrom, string bcodeTo, DateTime transferDate, decimal amount, string notes, string loggedUser = "")
        {
            using (var connection = _connectionProvider.GetConnection())
            using (var command = new SqlCommand("BankTransfers", connection))
            {

                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@BANKFRM", SqlDbType.VarChar).Value = bcodeFrom;
                command.Parameters.Add("@BANKTO", SqlDbType.VarChar).Value = bcodeTo;
                command.Parameters.Add("@TDATE", SqlDbType.DateTime).Value = transferDate;
                command.Parameters.Add("@AMOUNT", SqlDbType.Decimal).Value = amount;
                command.Parameters.Add("@NOTES", SqlDbType.VarChar).Value = notes ?? string.Empty;
                command.Parameters.Add("@LOGGEDUSER", SqlDbType.VarChar).Value = loggedUser ?? string.Empty;

                command.CommandTimeout = 5000;

                // Open connection and execute the command
                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();

                return rowsAffected > 0;
            }
        }
        public bool isValidReconLog(string ReconLog)
        {
            return _helperCommonService.DataTableOK(_helperCommonService.GetSqlData("SELECT * FROM BANK WHERE LOG_RECON=" + ReconLog + ""));
        }
        public string GetBankofReconciledLog(string ReconLog)
        {
            DataTable dataTable = _helperCommonService.GetSqlData(@"SELECT ISNULL(BANK,'') FROM BANK WHERE TRIM(log_recon)=@ReconLog",
                "@ReconLog", ReconLog.Trim());
            return _helperCommonService.DataTableOK(dataTable) ? Convert.ToString(dataTable.Rows[0][0]) : "";
        }
        public DataTable GetReconciledTransactions(string ReconLog)
        {
            return _helperCommonService.GetStoreProc("GetReconciledTransactions", "@ReconLog", ReconLog);
        }
        public string ShowClearBal(string bcode)
        {
            using (SqlCommand dbCommand = new SqlCommand())
            {

                dbCommand.Connection = _connectionProvider.GetConnection();
                dbCommand.CommandType = CommandType.StoredProcedure;
                dbCommand.CommandText = "ShowClearBal";
                dbCommand.Parameters.AddWithValue("@bank", bcode);

                SqlParameter returnParameter = dbCommand.Parameters.Add("@output", SqlDbType.Decimal);
                returnParameter.Direction = ParameterDirection.Output;
                dbCommand.CommandTimeout = 5000;
                dbCommand.Connection.Open();
                dbCommand.ExecuteNonQuery();

                decimal? id = !string.IsNullOrWhiteSpace(returnParameter.Value.ToString()) ?
                                          decimal.Parse(returnParameter.Value.ToString().Replace(",", "")) :
                                          0;
                return id.ToString();
            }
        }
        public void DeleteReconciledLog(string ReconLog)
        {
            _helperCommonService.GetSqlData("UPDATE BANK SET CLRD = 0, LOG_RECON = '', CLRD_DATE = '' WHERE trim(LOG_RECON) = @ReconLog;",
                "@ReconLog", ReconLog.Trim());
        }
        public DataTable UpdateClearPay(string invno, string currLogNo = "")
        {
            return _helperCommonService.GetStoreProc("UpdateBankTransactions", "@invno", invno, "@currLogNo", currLogNo);
        }
        public DataTable GetBankAcc(string loggedstoreno = "")
        {
            return _helperCommonService.GetStoreProc("getdefaultbank", "@loggedstoreno", loggedstoreno);
        }

    }
}
