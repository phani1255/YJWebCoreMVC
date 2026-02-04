using Microsoft.Data.SqlClient;
using System.Data;

namespace YJWebCoreMVC.Services
{
    public class HelperSravanService
    {
        private readonly ConnectionProvider _connectionProvider;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly HelperCommonService _helperCommonService;
        private readonly IWebHostEnvironment _env;
        private readonly string _companyName;
        private readonly string _storeCodeInUse;
        private readonly string _loggedUser;

        public HelperSravanService(ConnectionProvider connectionProvider, IHttpContextAccessor httpContextAccessor, HelperCommonService helperCommonService, IWebHostEnvironment env)
        {
            _connectionProvider = connectionProvider;
            _httpContextAccessor = httpContextAccessor;
            _helperCommonService = helperCommonService;
            _companyName = _httpContextAccessor.HttpContext?.Session.GetString("COMPANYNAME");
            _storeCodeInUse = _httpContextAccessor.HttpContext?.Session.GetString("STORE_CODE");
            _loggedUser = _httpContextAccessor.HttpContext?.Session.GetString("UserId");
            _env = env;
        }

        public DataTable dtAddCheckR { get; set; }
        public DataTable GetCSVData(bool isConsignment = false, string BillConsignNo = "", string SheetName = "")
        {
            if (!isConsignment)
            {
                if (SheetName == "CSV2")
                    return _helperCommonService.GetSqlData("SELECT '' COL1, ISNULL(b.VND_NO,'')VENDOR, bi.STYLE, ISNULL(bi.PCS, 0) QUANTITY, ISNULL(bi.COST, 0) UNIT_COST, ISNULL(bi.STORE_NO,'') LOCATION FROM BILLS b with(nolock) LEFT JOIN BIL_ITEM bi with (nolock) ON b.INV_NO = bi.INV_NO WHERE LTRIM(RTRIM(b.INV_NO))= @BillConsignNo", "@BillConsignNo", BillConsignNo);
                else
                    return _helperCommonService.GetSqlData("SELECT '' COL1, bi.STYLE STYLE1, bi.STYLE STYL21, ISNULL(s.BRAND,'') BRAND, ISNULL(bi.VND_STYLE,'')VND_STYLE, ISNULL(b.VND_NO,'') VENDOR, ISNULL(s.[DESC],'')[DESC], 'FALSE' [TRUEFALSE], ISNULL(s.METAL, '') METAL, ISNULL(s.CATEGORY, '') CATEGORY, ISNULL(s.CENTER_TYPE, '') STONE_TYPE, '' BLANK1, ISNULL(s.[GROUP], '') [GROUP], '' BLANK2, '' GL_CLASS, '' BLANK3, '' BLANK4, ISNULL(bi.PRICE, 0) RETAIL_PRICE, ISNULL(bi.PRICE, 0) RETAIL_PRICE1, ISNULL(bi.COST, 0) COST, '' BLANK5, '' EMP, '' MEMO, '' BLANK6, '' BLANK7, '' BLANK8, '1305000' [1305000] ,'' BLANK9, '' GL_ACCOUNT FROM BILLS b LEFT JOIN BIL_ITEM bi with(nolock) ON b.INV_NO = bi.INV_NO LEFT JOIN STYLES s with (nolock) on s.STYLE = dbo.InvStyle(bi.STYLE) WHERE TRIM(b.INV_NO)= @BillConsignNo", "@BillConsignNo", BillConsignNo);
            }
            else if (SheetName == "CSV2")
                return _helperCommonService.GetSqlData("SELECT '' COL1, ISNULL(b.VND_NO,'')VENDOR, bi.STYLE, ISNULL(bi.PCS, 0) QUANTITY, ISNULL(bi.COST, 0) UNIT_COST, ISNULL(bi.STORE_NO,'') LOCATION FROM APM b LEFT JOIN APM_ITEM bi with (nolock) ON b.INV_NO = bi.INV_NO WHERE LTRIM(RTRIM(b.INV_NO))= @BillConsignNo", "@BillConsignNo", BillConsignNo);
            else
                return _helperCommonService.GetSqlData("SELECT '' COL1, bi.STYLE STYLE1, bi.STYLE STYLE2, ISNULL(s.BRAND,'') BRAND, ISNULL(bi.VND_STYLE,'')VND_STYLE, ISNULL(b.VND_NO,'') VENDOR,  ISNULL(s.[DESC],'')[DESC], 'TRUE' [TRUEFALSE], ISNULL(s.METAL, '') METAL, ISNULL(s.CATEGORY, '') CATEGORY, ISNULL(s.CENTER_TYPE, '') STONE_TYPE, '' BLANK1, ISNULL(s.[GROUP], '') [GROUP], '' BLANK2, '' GL_CLASS, '' BLANK3, '' BLANK4, ISNULL(bi.PRICE, 0) RETAIL_PRICE, ISNULL(bi.PRICE, 0) RETAIL_PRICE1, ISNULL(bi.COST, 0) COST, '' BLANK5, '' EMP,'' MEMO, '' BLANK6, '' BLANK7, '' BLANK8, '1306000' [1306000], '' BLANK9, '' GL_ACCOUNT FROM APM b with(nolock) LEFT JOIN APM_ITEM bi ON b.INV_NO = bi.INV_NO LEFT JOIN STYLES s with (nolock) on s.STYLE = dbo.InvStyle(bi.STYLE) WHERE TRIM(b.INV_NO)= @BillConsignNo", "@BillConsignNo", BillConsignNo);
        }
        public decimal SafeDecimal(DataRow row, string column)
        {
            if (row == null) return 0m;
            if (row.Table == null) return 0m;
            if (!row.Table.Columns.Contains(column)) return 0m;

            var value = row[column];
            if (value == null || value == DBNull.Value) return 0m;

            decimal result;
            if (decimal.TryParse(value.ToString(), out result))
                return result;

            return 0m;
        }

        public string SafeString(DataRow row, string column)
        {
            if (row == null) return "";
            if (row.Table == null) return "";
            if (!row.Table.Columns.Contains(column)) return "";

            var value = row[column];
            if (value == null || value == DBNull.Value) return "";

            return value.ToString();
        }
        public DataRow GetVendorMax(int minval, int maxval)
        {
            return _helperCommonService.GetSqlRow(@"SELECT Isnull(NULLIF(( CASE WHEN Max(dbo.Getfirstnumeric(acc)) < @min  THEN @min ELSE Max(dbo.Getfirstnumeric(acc)) END), @min), @min) AS acc FROM VENDORS with (nolock) WHERE Isnull(dbo.Getnonnumeric(acc), 0) <> 0 AND try_convert(int, acc) IS NOT NULL AND Isnull(dbo.Getnonnumeric(acc), 0) < @Max", "@min", minval.ToString(), "@Max", maxval.ToString());
        }
        public DataTable GetBillnChecks(string vcode)
        {
            return _helperCommonService.GetSqlData("SELECT * FROM checks with (nolock) WHERE PACK = -1 AND ISNULL(bank,'')<>'' AND acc = @acc ORDER BY Date", "@acc", vcode.Trim());
        }
        public DataTable CheckForExistingCheck_Banks(string vcode, string exclueCheck = "")
        {
            if (exclueCheck.Trim().Length == 0)
                return _helperCommonService.GetSqlData("SELECT TOP (1) Check_no, Bank FROM Checks with (nolock) WHERE acc=@acc and pack<0  AND isnull(bank,'')<>'' ORDER BY date", "@acc", vcode.Trim());
            string strExclude = _helperCommonService.Left(exclueCheck, exclueCheck.Length - 1);
            return _helperCommonService.GetSqlData(string.Format("SELECT TOP (1) Check_no, Bank FROM Checks with (nolock) WHERE acc='{0}' AND pack<0  AND ISNULL(bank,'')<>'' AND TRIM(Check_no) NOT IN ({1}) ORDER BY date", vcode.Trim(), strExclude));
        }
        public DataTable CheckForExistingBankCheck(string Check, string Bank)
        {
            return _helperCommonService.GetSqlData("SELECT * FROM CHECKS  WHERE PACK=-1 AND TRIM(CHECK_NO)=@Check AND TRIM(BANK)=@Bank",
                    "@Check", Check.Trim(), "@Bank", Bank.Trim());
        }
        public DataTable GetVendorCheckIssueData(string vendorcode, string Isissuecheck = "", string storeno = "", bool paybygold = false)
        {
            return _helperCommonService.GetStoreProc("GetVendorCheckIssueData", "@vendorcode", vendorcode, "@Isissuecheck", Isissuecheck.ToString(), "@storeno", storeno, "@paybygold", paybygold ? "1" : "0");
        }

        public DataTable GetSalesmancodeonly()
        {
            return _helperCommonService.GetSqlData(@"SELECT DISTINCT CODE FROM SALESMEN Order By CODE");
        }
        public DataTable CheckOpenBillsForIssuechecks(string tblAPCredit)
        {
            using (var sqlDataAdapter = new SqlDataAdapter())
            {
                var command = new SqlCommand("CheckOpenBills", _connectionProvider.GetConnection())
                {
                    CommandType = CommandType.StoredProcedure,
                    CommandTimeout = 6000
                };

                command.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@tblAPCredit",
                    SqlDbType = SqlDbType.Xml,
                    Value = tblAPCredit
                });

                var dataTable = new DataTable();
                sqlDataAdapter.SelectCommand = command;
                sqlDataAdapter.Fill(dataTable);

                return dataTable;
            }
        }
        public DataTable GetDetByCCLog(string logno, bool isMulticcrd = false)
        {
            if (isMulticcrd)
                return _helperCommonService.GetSqlData("select b.Acc,a.* from bil_ccrd a with (nolock) , bills b with (nolock) where trim(a.BILL_NO)=trim(b.INV_NO) and log_no in (" + logno + ")", "@log", logno);
            return _helperCommonService.GetSqlData("select * from bil_ccrd where log_no=@log", "@log", logno);
        }
        public DataTable IssueCheckForAllVendor(DateTime? FromDt, DateTime? ToDt, bool IsBillDt, string storeno = "")
        {
            return _helperCommonService.GetStoreProc("Get_IssueCheckForAllVendor", "@FromDt", FromDt.ToString(), "@ToDt", ToDt.ToString(), "@IsBillDt", IsBillDt.ToString(), "@storeno", storeno);
        }

    }
}
