/*
 *  Created By Phanindra on 17-Apr-2025
 *  Manoj 08/13/2024 Added DeptDetails Property, GetAllGLAccts Method
 *  Manoj 08/14/2025 Added GetTrailBalnceLogs Method
 *  Manoj 08/18/2025 Added DeptDetails property and GetAllGLCodes , Detailglpostlog Methods 
 *  Manoj 08/19/2025 Added GetGLref Method
 *  Manoj 08/22/2025 Added listofGlTransactforbalancesheet Method
 *  Manoj 08/26/2025 Added GetLastLog,checkglpostlog Methods 
 *  Manoj 08/27/2025 Added checkglpostDoc,iSGlLogExists,GetNextGLNo methods
 *  Manoj 08/28/2025 Added GetGlTypeByCode methods
 *  Manoj 09/01/2025 Added GetGLAcctTypes Method
 *  Manoj 09/02/2025 Added  SearchGLDepts,CheckValidGLDept Methods
 *  Manoj 09/08/2025 Added GetGlCodeAndName,getGLClassData,GetDefaultGrpsFrmUpsIns Methods
 *  Manoj 09/09/2025 Added  CheckGLClassExist,iSGlAcc Methods
 *  Manoj 09/10/2025 Added  setupGlcodesbyClasses,DeleteGLclass Methods
 *  Manoj 09/15/2025 Added GetTypes Methods
 *  Manoj 09/16/2025 Added AddGLAcct,CheckValidGLAcct,DeleteGLAcct Methods
 *  Manoj 09/18/2025 AddedLogNoFromInvoiceDoc
 */
using Microsoft.Data.SqlClient;
using System.Data;

namespace YJWebCoreMVC.Services
{
    public class GLAcctService
    {
        private readonly ConnectionProvider _connectionProvider;
        private readonly HelperCommonService _helperCommonService;

        public GLAcctService(ConnectionProvider connectionProvider, HelperCommonService helperCommonService, IWebHostEnvironment env)
        {
            _connectionProvider = connectionProvider;
            _helperCommonService = helperCommonService;
        }


        public DataTable GetAllGLAccts()
        {
            return _helperCommonService.GetSqlData(@"select * from gl_accs order by acc");
        }
        public DataTable GetDeptDetails()
        {
            return _helperCommonService.GetSqlData("SELECT '' as GLdept UNION SELECT DEPT as GLdept FROM GL_DEPT with (nolock)  ORDER BY GLdept ASC");
        }
        public DataTable GetTrailBalnceLogs(string logno, string dept = "")
        {
            if (dept == "")
                return _helperCommonService.GetSqlData(@"SELECT A.CODE, B.NAME, A.LOG_NO, A.[DATE], A.DEPT, A.TRANSACT, 
                    A.[TYPE], A.INV_NO, A.NOTE, iif(amount<0, -amount, 0) AS DEBIT, iif(amount>0, amount, 0) AS CREDIT
                    FROM GLPOST A with (nolock) left JOIN GL_ACCS B with (nolock) ON B.ACC=A.CODE WHERE a.LOG_NO=@logno", "@logno", logno);

            return _helperCommonService.GetSqlData(@"SELECT A.CODE, B.NAME, A.LOG_NO, A.[DATE], A.DEPT, A.TRANSACT, 
                    A.[TYPE], A.INV_NO, A.NOTE, iif(amount<0, -amount, 0) AS DEBIT, iif(amount>0, amount, 0) AS CREDIT
                    FROM GLPOST A with (nolock) left JOIN GL_ACCS B with (nolock) ON B.ACC=A.CODE WHERE a.LOG_NO=@logno and A.Dept=@Dept", "@logno", logno, "@Dept", dept);
        }

        public DataTable GetAllGLCodes()
        {
            return _helperCommonService.GetSqlData("select trim(isnull(acc,'')) as acc from gl_accs");
        }
        public DataTable Detailglpostlog(string logno)
        {
            return _helperCommonService.GetSqlData(@"SELECT GP.LOG_NO , GP.DATE,GP.CODE, Gl.NAME,Gl.GLTYPE,Gp.DEPT, Gp.AMOUNT,iif(ISNULL(Gp.AMOUNT,0)<0, -ISNULL(Gp.AMOUNT,0), 0) AS DEBIT,
				iif(ISNULL(Gp.AMOUNT, 0) > 0, ISNULL(Gp.AMOUNT, 0), 0) AS CREDIT, Gp.NOTE FROM GL_ACCS Gl join  GLPOST GP ON Gl.ACC = GP.CODE where GP.LOG_NO = '" + logno + "'");
        }



        public DataTable GetGLref(string refinv_no, string Type = "")
        {
            return _helperCommonService.GetSqlData($"SELECT trim(isnull(CODE,'')) as CODE, isnull(NAME,'Invalid Acct#  ' + code) [NAME], AMOUNT AS DEBIT, AMOUNT AS CREDIT, DEPT, INV_NO AS REF, NOTE,TRANSACT, DATE,TYPE,LOG_NO  from glpost a left join gl_accs b on trim(a.code) = trim(b.acc) where Trimmed_inv_no = trim('{refinv_no.Trim()}') and A.[TYPE]=iif('{Type}'='',A.[Type],'{Type}')");
        }

        public DataTable listofGlTransactforbalancesheet(string fdate, string glcode)
        {
            return _helperCommonService.GetSqlData(@"SELECT A.CODE, B.NAME, A.LOG_NO, A.[DATE], A.DEPT, A.TRANSACT, A.[TYPE], A.INV_NO, 
                         A.NOTE, iif(amount<0, -amount, 0) AS DEBIT, iif(amount>0, amount, 0) AS CREDIT
                         FROM GLPOST A with (nolock) left JOIN GL_ACCS B with (nolock) ON B.ACC=A.CODE WHERE A.CODE = @glcode and 
                         cast(A.DATE as date) <=cast(@fdate as date) ORDER BY A.CODE,A.LOG_NO asc",
                                "@fdate", fdate, "@glcode", glcode);
        }

        public DataRow GetLastLog()
        {
            DataTable dataTable = _helperCommonService.GetSqlData("select top 1 log_no from glpost order by try_cast(log_no as int) desc");
            return _helperCommonService.GetRowOne(dataTable);
        }

        public DataTable checkglpostlog(string logno, bool Delete = false)
        {
            if (Delete)
                return _helperCommonService.GetSqlData(@"SELECT trim(isnull(CODE,'')) as CODE,isnull(NAME,'Invalid Acct# ' + code) NAME,GLTYPE,AMOUNT, DEPT, INV_NO as REF,  NOTE, LOG_NO,TRANSACT, DATE,TIME,A.[TYPE],cast(iSNULL([manual],0) as bit) [manual]  from glpost a  join gl_accs b on trim(a.code) = trim(b.acc) where log_no =@log_no", "@log_no", logno);
            return _helperCommonService.GetSqlData(@"SELECT trim(isnull(CODE,'')) as CODE, DEPT,isnull(NAME,'Invalid Acct# ' + code) NAME,GLTYPE, AMOUNT AS DEBIT, AMOUNT AS CREDIT, INV_NO as REF,  NOTE,LOG_NO, TRANSACT, DATE,TIME,A.[TYPE],cast(iSNULL([manual],0) as bit) [manual]  from glpost a left join gl_accs b on trim(a.code) = trim(b.acc) where log_no = @log_no", "@log_no", logno);
        }

        public DataTable checkglpostDoc(string docno)
        {
            return _helperCommonService.GetSqlData(@"SELECT trim(isnull(CODE,'')) as CODE,DEPT, isnull(NAME,'Invalid Acct# ' + code) NAME,GLTYPE,  AMOUNT AS DEBIT, AMOUNT AS CREDIT,  INV_NO AS REF, NOTE, 
                                LOG_NO, TRANSACT, DATE,TIME,A.[TYPE],cast(iSNULL([manual],0) as bit) [manual]  from glpost a left join gl_accs b on trim(a.code) = trim(b.acc)
                                where Trimmed_inv_no = trim('" + docno.Trim() + "')");
        }
        public bool iSGlLogExists(string log_no)
        {
            DataTable dt = _helperCommonService.GetSqlData($"SELECT 1 FROM GLPOST WHERE isnull(TRIM(log_no), '') = TRIM(@log_no)", "@log_no", log_no);
            return _helperCommonService.DataTableOK(dt);
        }

        public string GetNextGLNo()
        {
            DataTable dataTable = _helperCommonService.GetSqlData(@"SELECT ISNULL(MAX(CAST(LOG_NO AS INT)),0)+1 LOG_NO FROM GLPOST");
            return Convert.ToString(dataTable.Rows[0]["LOG_NO"]);
        }

        public DataRow GetGlTypeByCode(string glcode)
        {
            return _helperCommonService.GetSqlRow("select top 1 * from gl_accs with (nolock) where acc=@glcode", "@glcode", glcode);
        }

        public DataTable GetGLAcctTypes()
        {
            return _helperCommonService.GetSqlData("SELECT[gl_type],IIF([pnl] = 'D','DEBIT',IIF([pnl] = 'C','CREDIT',('NONE'))) pnl,IIF([bal_sheet] = 'D','DEBIT',IIF([bal_sheet] = 'C','CREDIT',('NONE'))) bal_sheet FROM[GL_ACCT_TYPES]");
        }

        public DataTable SearchGLDepts()
        {
            return _helperCommonService.GetSqlData(@"SELECT DEPT,NAME,'' as STATUS FROM gl_dept order by dept");
        }

        public DataRow CheckValidGLDept(string dept)
        {
            return _helperCommonService.GetSqlRow("select *  From gl_dept Where dept=@dept", "@dept", dept);
        }

        public DataTable GetGlCodeAndName()
        {
            return _helperCommonService.GetSqlData(@"select trim(acc) acc, name from gl_accs");
        }

        public DataTable getGLClassData()
        {
            return _helperCommonService.GetSqlData(@"select trim(CLASS_GL) CLASS_GL,trim(ASSET_GL) ASSET_GL,trim(CLEAR_GL) CLEAR_GL,trim(COGS_GL) COGS_GL,trim(SALES_GL) SALES_GL,trim(DISC_GL) DISC_GL from CLASSGLS");
        }

        public DataTable GetDefaultGrpsFrmUpsIns()
        {
            return _helperCommonService.GetSqlData(@"select isnull(ASSET_GL,'') as ASSET_GL,
                    isnull(CLEAR_GL,'') as CLEAR_GL,isnull(COGS_GL,'') as COGS_GL,
                    isnull(SALES_GL,'') as SALES_GL, isnull(DISC_GL,'') as DISC_GL from ups_ins");
        }

        public DataTable CheckGLClassExist(string cCheckGLClass)
        {
            return _helperCommonService.GetSqlData(@"select * from styles where trim(class_gl)=@cglclass", "@cglclass", cCheckGLClass);
        }

        public bool iSGlAcc(String GlAcc)
        {
            DataTable dtRetInvNo = _helperCommonService.GetSqlData(@"select trim(acc) from gl_accs where trim(acc)=trim(@GlAcc)", "@GlAcc", GlAcc);
            return _helperCommonService.DataTableOK(dtRetInvNo);
        }

        public DataTable setupGlcodesbyClasses(string data1, string cAsset, string cClear, string cCogs, string cSales, string cDiscount)
        {
            return _helperCommonService.GetStoreProc("GROUP_GL", "@str1XmlData", data1, "@cAssetGl", cAsset, "@cClearGl", cClear, "@cCogsGl", cCogs, "@cSalesGl", cSales, "@cDiscountGl", cDiscount);
        }

        public DataTable DeleteGLclass(string code)
        {
            return _helperCommonService.GetSqlData(@"delete from classgls where class_gl = TRIM('" + code.Replace("'", "''") + "') ");
        }


        public DataTable GetTypes()
        {
            return _helperCommonService.GetSqlData("SELECT gl_type FROM GL_ACCT_TYPES order by gl_type");
        }
        public DataTable SearchGLAccts()
        {
            return _helperCommonService.GetSqlData(@"SELECT ACC,NAME,GLTYPE,PARENT_ACC,QB_ACC,SUB_TYPE,IIF([pnl] = 'D','DEBIT',IIF([pnl] = 'C','CREDIT',('NONE'))) pnl,IIF([bal_sheet] = 'D','DEBIT',IIF([bal_sheet] = 'C','CREDIT',('NONE'))) bal_sheet,SHW_INCOM,'' as STATUS FROM gl_accs order by acc");
        }


        public bool AddGLAcct(string xml)
        {
            if (string.IsNullOrWhiteSpace(xml))
                throw new ArgumentException("The GL account XML data cannot be null or empty.", nameof(xml));

            using (var connection = _connectionProvider.GetConnection())
            using (var command = new SqlCommand("AddGLAcct", connection))
            {
                // Configure the command object
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@GLAcct", SqlDbType.Xml) { Value = xml });

                // Open the connection, execute the command, and return the result
                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }

        public DataRow CheckValidGLAcct(string acc)
        {
            return _helperCommonService.GetSqlRow("select *  From gl_accs Where acc=@acc", "@acc", acc);
        }

        public void DeleteGLAcct(string acc, out string error)
        {
            _helperCommonService.GetStoreProc("DeleteGLAcct", "@acc", acc);
            error = "";
        }


        public String LogNoFromInvoiceDoc(String inv_no)
        {
            DataTable dt = _helperCommonService.GetSqlData($"Select top 1 inv_no,log_no from GLPOST where inv_no=@inv_no order by date desc", "@inv_no", _helperCommonService.Pad6(inv_no));
            return _helperCommonService.DataTableOK(dt) ? Convert.ToString(dt.Rows[0]["log_no"]) : "";
        }

    }
}
