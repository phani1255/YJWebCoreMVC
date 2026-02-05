/*
    Created by Manoj 29-Apr-2025
    * Manoj 30-Apr-2025 Added new listofjobscompleted  Method
    * Manoj 02-May-2025 Added GetListofOpenJobs Method
    * Manoj 05-May-2025 Added SummarizedListofOpenJobs Method
    * Manoj 06-May-2025 Added GetFilterList Method
    * Manoj 08-May-2025 Added GetListofPromisedvsCompletedDates Mehtod
    * Manoj 12-May-2025 Added GetListofRepairJobs Method
    * Manoj 26-May-2025 Added reprintjobbag,checkJobBagIsSplitOrNot,checkJobBagSendRecToShop,GETHISTORYOFJOBBAG Methods
    * Manoj 27-May-2025 Added GetJobbagNotes Method
    * Manoj 28-May-2025 Added GetpartshistByJobBag Method
    * Dharani 10/13/2025 Added CheckValidMfgDepts, resetallsetters, deletersettername, getmfgdepts methods and PersonModel
    * Dharani 10/14/2025 Added getalldepts, CheckValidDept, UpdateDept methods and DepartmentViewModel
    * Phanindra 10/16/2025 Added checkJobBagIsAlreadySplittedOrNot, GetInforationBasedOnJobBagFromlbl, ChceckJobbagNumber, SaveJobBafInfoInMfgTable, getGivenJobBagaData, savereturnjobdata methods
    * Phanindra 10/24/2025 Added updJobNAddToStk, updJobNAddToRsv
    * Manoj 10/28/2025 Added GetJobsForAck Method
    * Phanindra 10/30/2025 Added checkJobBagIsAlreadySplittedOrNotForPrint Method
    * Phanindra 11/19/2025 Added checkrcvobbag method
 */
using Microsoft.Data.SqlClient;
using System.Data;

namespace YJWebCoreMVC.Services
{
    public class MfgService
    {
        private readonly ConnectionProvider _connectionProvider;
        private readonly HelperCommonService _helperCommonService;

        public MfgService(ConnectionProvider connectionProvider, HelperCommonService helperCommonService)
        {
            _connectionProvider = connectionProvider;
            _helperCommonService = helperCommonService;
        }

        public DataTable getallsetters(bool isfrmperson = false)
        {
            return _helperCommonService.GetSqlData("select s.NAME,s.freq_used,s.DEPT,s.gl_code,g.NAME as accname,'1' as STATUS, s.Login_Code as LoginCode, s.InActive  from SETTERS s LEFT OUTER JOIN GL_ACCS g ON s.gl_code=g.ACC where  s.inactive=case when '" + isfrmperson + "'='true' then s.inactive else 0 end order by s.NAME ASC");
        }
        public DataTable listofjobsgiventoaperson(string settername, string fromdate, string todate, string datecondition)
        {
            return _helperCommonService.GetStoreProc("listofjobsgiventoaperson", "@settername", settername,
                "@fromdate", fromdate, "@todate", todate, "@DATECONDITION", datecondition);
        }

        public DataTable listofjobscompleted(string fromdate, string todate)
        {
            return _helperCommonService.GetStoreProc("listofcompletedjobs", "@fromdate", fromdate, "@todate", todate);
        }

        public DataTable GetListofOpenJobs(string fromdate, string todate, string summaryBy, string filter, string frmDueDate, string toDueDate, string IsFor = "", int Pagesize = 0, int PageNum = 0, string acc = "", bool IsAll = false, bool IsPastDue = false, bool IsPersonDueDate = false)
        {
            return _helperCommonService.GetStoreProc("GetListOfOpenJobs", "@FROMDATE", fromdate, "@TODATE", todate, "@FROMDUEDATE", frmDueDate, "@TODUEDATE", toDueDate, "@SUMMARYBY", summaryBy, "@FILTER", filter, "@ISFOR", IsFor, "@PageSize", Pagesize.ToString(), "@PageNumber", PageNum.ToString(), "@ACC", acc, "@IsAll", IsAll.ToString(), "@IsPastDue", IsPastDue.ToString(), "@IsPersonDueDate", IsPersonDueDate.ToString());
        }

        public DataTable SummarizedListofOpenJobs(string fromdate, string todate, string summaryBy, string filter, string frmDueDate, string toDueDate, bool IsPastDue = false, bool IsPersonDueDate = false)
        {
            return _helperCommonService.GetStoreProc("SummarizedListOfOpenJobs", "@FROMDATE", fromdate, "@TODATE", todate, "@FROMDUEDATE", frmDueDate, "@TODUEDATE", toDueDate, "@SUMMARYBY", summaryBy, "@FILTER", filter, "@IsPastDue", IsPastDue.ToString(), "@IsPersonDueDate", IsPersonDueDate.ToString());
        }

        public DataTable GetFilterList(string filter)
        {
            return _helperCommonService.GetSqlData("SELECT DISTINCT [" + filter + "] AS Filter FROM SETTERS ORDER BY 1");
        }


        public DataTable GetListofPromisedvsCompletedDates(string fdate, string tdate, bool AllDates = false)
        {
            return _helperCommonService.GetSqlData("select distinct d.repair_no, acc, cast(r.DATE as date) date, cast(CAN_DATE as date) promised, cast(m.idate as date) completed from rep_item d join repair r on d.repair_no = r.repair_no join mfg m on m.inv_no = d.barcode and m.closed = 1 where CAST(r.date AS DATE) between '" + fdate + "' AND '" + tdate + "' order by repair_no");
        }

        public DataTable GetListofRepairJobs(bool isreadyForPickup = false)
        {
            string qry;

            if (isreadyForPickup)
                qry = @"SELECT R.REPAIR_NO, MAX(R.ACC) ACC, MAX(R.NAME) NAME, MAX(R.DATE) DATE, max(RI.QTY) as QTY, max(ri.qty - ri.shiped) as [OPEN],
                               MAX(R.RCV_DATE) RCV_DATE, MAX(R.CAN_DATE) CAN_DATE,MAX(CAST(M.CLOSED_JOB AS INT)) [STATUS]
                               FROM REPAIR R with (nolock) inner join  REP_ITEM RI with (nolock) on  R.REPAIR_NO = RI.REPAIR_NO
                               inner join or_items oi on oi.barcode=ri.BARCODE
                               inner join MFG M on M.INV_NO = ri.BARCODE
                               inner join customer C on C.ACC = R.ACC
                               where isnull(ri.shiped,0)=0 and oi.RCVD>0
                               group by r.repair_no";
            else
                qry = @"SELECT R.REPAIR_NO, MAX(R.ACC) ACC, MAX(R.NAME) NAME, MAX(R.DATE) DATE, max(RI.QTY) as QTY, max(ri.qty - ri.shiped) as [OPEN],
                               MAX(R.RCV_DATE) RCV_DATE, MAX(R.CAN_DATE) CAN_DATE,MAX(CAST(0 AS INT)) [STATUS] FROM REPAIR R with (nolock)
                               inner join  REP_ITEM RI with (nolock) on  R.REPAIR_NO = RI.REPAIR_NO
                               inner join customer C on C.ACC = R.ACC
                               where isnull(shiped,0)=0 and isnull(trim(ri.BARCODE),'') not in (select isnull(Trimmed_inv_no,'') from mfg with (nolock))
                               group by r.repair_no";

            return _helperCommonService.GetSqlData(qry);
        }

        public DataTable reprintjobbag(string jobbagno, bool ismfg = false, string repairNo = "")
        {
            return _helperCommonService.GetStoreProc("PrintJobBag", "@REPAIR_NO", repairNo, "@STYLE", "", "@JOBBAGNO", jobbagno, "@ismfg", ismfg.ToString());
        }

        public DataTable checkJobBagIsSplitOrNot(string jobbagno)
        {
            return _helperCommonService.GetSqlData("select * from lbl_bar where original= @jobbagno", "@jobbagno", jobbagno);
        }

        public DataTable checkJobBagSendRecToShop(string jobbagno)
        {
            return _helperCommonService.GetSqlData(@"select * from SEND_RPR with(nolock) where jobbag=@jobbagno", "@jobbagno", jobbagno);
        }

        public DataTable GETHISTORYOFJOBBAG(string jbnumber, string USERNAME = "")
        {
            return _helperCommonService.GetStoreProc("FrmGiveOutJobBag", "@jobbagno", jbnumber, "@USERNAME", USERNAME);
        }

        public DataTable GetJobbagNotes(string Jobbag = "")
        {
            return _helperCommonService.GetSqlData($"Select Job_no,Date,Who,What as Note from Job_notes Where Job_no='{Jobbag}'");
        }
        public DataTable GetpartshistByJobBag(string JobBag)
        {
            return _helperCommonService.GetSqlData("EXEC [GetpartshistByJobBag] @JobBag", "@JobBag", JobBag);
        }
        public DataRow CheckValidMfgDepts(string dept)
        {
            DataTable dataTable = _helperCommonService.GetSqlData("select *  From mfg_dept Where dept=@dept", "@dept", dept);
            return dataTable.Rows.Count > 0 ? dataTable.Rows[0] : null;
        }
        public void resetallsetters(string setterinfo)
        {
            _helperCommonService.GetStoreProc("UpdateSetters", "@TBLRTVITEMS", setterinfo);
        }
        public void deletersettername(string settername)
        {
            _helperCommonService.GetSqlData("delete from setters where name ='" + settername + "'");
        }
        public DataTable getmfgdepts()
        {
            return _helperCommonService.GetSqlData("select distinct dept from mfg_dept order by dept asc");
        }
        public DataTable getalldepts()
        {
            return _helperCommonService.GetSqlData("select *,'1' as STATUS from mfg_dept order by dept asc");
        }
        public DataRow CheckValidDept(string dept)
        {
            DataTable dataTable = _helperCommonService.GetSqlData("select *  From mfg_dept Where dept=@dept", "@dept", dept);
            return dataTable.Rows.Count > 0 ? dataTable.Rows[0] : null;
        }
        public bool UpdateDept(string depts)
        {
            using (var connection = _connectionProvider.GetConnection())
            using (var dbCommand = new SqlCommand("UpdateDept", connection))
            {
                dbCommand.CommandType = CommandType.StoredProcedure;

                // Add the XML parameter
                dbCommand.Parameters.AddWithValue("@GLDEPT", depts);
                connection.Open();
                var rowsAffected = dbCommand.ExecuteNonQuery();

                return rowsAffected > 0;
            }
        }

        public DataTable checkJobBagIsAlreadySplittedOrNot(string jobbagno)
        {
            return _helperCommonService.GetSqlData("select * From lbl_bar with (nolock) Where gen_barcode = [dbo].[GetBarcode](@bagno)", "@bagno", jobbagno);
        }

        public DataRow GetInforationBasedOnJobBagFromlbl(string bagnumber, bool isjobgiven = false)
        {
            if (isjobgiven)
                return _helperCommonService.GetSqlRow("select lb.barcode,oi.note,((select ISNULL(sum(iIf([TIME] != '',QTY,-QTY)),0) from mfg where  [dbo].[GetBarcode](inv_no) = [dbo].[GetBarcode](@bagnumber))) as qty,lb.pon,lb.style,((select ISNULL(sum(iIf([TIME] != '',QTY,-QTY)),0) from mfg where  [dbo].[GetBarcode](inv_no) = [dbo].[GetBarcode](@bagnumber))) as [open] from lbl_bar lb left join or_items oi on  [dbo].[GetBarcode](lb.barcode) =  [dbo].[GetBarcode](oi.barcode) where  [dbo].[GetBarcode](lb.barcode) =  [dbo].[GetBarcode](@bagnumber) and ((select ISNULL(sum(iIf([TIME] != '',QTY,-QTY)),0) from mfg where  [dbo].[GetBarcode](inv_no) = [dbo].[GetBarcode](@bagnumber))) >0",
                    "@bagnumber", bagnumber);
            return _helperCommonService.GetSqlRow("select lb.barcode,oi.note,(lb.qty -(select ISNULL(sum(iIf([TIME] != '',QTY,-QTY)),0) from mfg where  [dbo].[GetBarcode](inv_no) = [dbo].[GetBarcode](@bagnumber))) as qty,lb.pon,lb.style,(lb.qty -(select ISNULL(sum(iIf([TIME] != '',QTY,-QTY)),0) from mfg where  [dbo].[GetBarcode](inv_no) = [dbo].[GetBarcode](@bagnumber))) as [open] from lbl_bar lb left join or_items oi on  [dbo].[GetBarcode](lb.barcode) =  [dbo].[GetBarcode](oi.barcode) where  [dbo].[GetBarcode](lb.barcode) =  [dbo].[GetBarcode](@bagnumber) and (lb.qty -(select ISNULL(sum(iIf([TIME] != '',QTY,-QTY)),0) from mfg where  [dbo].[GetBarcode](inv_no) = [dbo].[GetBarcode](@bagnumber))) >0",
              "@bagnumber", bagnumber);
        }

        public DataRow ChceckJobbagNumber(string bagnumber, bool isjobgiven = false)
        {
            if (isjobgiven)
                return _helperCommonService.GetSqlRow("select lb.barcode,lb.note,((select ISNULL(sum(iIf([TIME] != '',QTY,-QTY)),0) from mfg where [dbo].[GetBarcode](inv_no) = [dbo].[GetBarcode](@bagnumber))) as qty,lb.pon,lb.style,((select ISNULL(sum(iIf([TIME] != '',QTY,-QTY)),0) from mfg where [dbo].[GetBarcode](inv_no) = [dbo].[GetBarcode](@bagnumber))) as [open] from or_items lb where [dbo].[GetBarcode](lb.barcode) = [dbo].[GetBarcode](@bagnumber) and((select ISNULL(sum(iIf([TIME] != '',QTY,-QTY)),0) from mfg where [dbo].[GetBarcode](inv_no) = [dbo].[GetBarcode](@bagnumber))) > 0",
                    "@bagnumber", bagnumber);
            return _helperCommonService.GetSqlRow("select lb.barcode,lb.note,(lb.qty - (select ISNULL(sum(iIf([TIME] != '',QTY,-QTY)),0) from mfg where [dbo].[GetBarcode](inv_no) = [dbo].[GetBarcode](@bagnumber))) as qty,lb.pon,lb.style,(lb.qty - (select ISNULL(sum(iIf([TIME] != '',QTY,-QTY)),0) from mfg where [dbo].[GetBarcode](inv_no) = [dbo].[GetBarcode](@bagnumber))) as [open] from or_items lb where [dbo].[GetBarcode](lb.barcode) = [dbo].[GetBarcode](@bagnumber) and(lb.qty - (select ISNULL(sum(iIf([TIME] != '',QTY,-QTY)),0) from mfg where [dbo].[GetBarcode](inv_no) = [dbo].[GetBarcode](@bagnumber))) > 0",
               "@bagnumber", bagnumber);
        }

        public void SaveJobBafInfoInMfgTable(string jobbagno, DataTable dt, string settername, string loggedusername, string deductcast, bool isjobgiven, DateTime? DDate)
        {
            // Use `using` for proper disposal of resources
            using (var connection = _connectionProvider.GetConnection())
            using (var command = new SqlCommand("GiveJobToPerson", connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                // Add parameters
                command.Parameters.AddWithValue("@JOBBAGNO", jobbagno);
                command.Parameters.AddWithValue("@MFGINFO", dt);
                command.Parameters.AddWithValue("@SETTERNAME", settername);
                command.Parameters.AddWithValue("@LOGGEDUSER", loggedusername);
                command.Parameters.AddWithValue("@DEDUCTCAST", deductcast);
                command.Parameters.AddWithValue("@isjobgiven", isjobgiven);
                command.Parameters.AddWithValue("@DueDate", DDate);

                // Output parameter
                var outInvNo = new SqlParameter("@RETVAL", SqlDbType.VarChar, 1000)
                {
                    Direction = ParameterDirection.Output
                };
                command.Parameters.Add(outInvNo);

                // Execute the command
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public DataRow getGivenJobBagaData(string barcode, string setter)
        {
            return _helperCommonService.GetSqlRow("select m.inv_no as jobno,max(m.note) as note,max(iif(m.rcv_date is null, m.qty, 0)) - max(iif(m.rcv_date is not null, m.qty, 0))  AS qty, max(oi.pon) as pon,  max(oi.style) as style,  max(iif(m.rcv_date is null, m.qty, 0)) - max(iif(m.rcv_date is not null, m.qty, 0))  AS[OPEN], max(m.vend_inv) as vend_inv,  max(m.vinv_dte) as vinv_dte  from mfg m LEFT JOIN or_items oi on RIGHT('000000'+m.inv_no,6) = RIGHT('000000'+oi.barcode,6) WHERE RIGHT('000000'+M.INV_NO,6) = RIGHT('000000'+@bagnumber,6) AND [dbo].[GetBarcode](oi.barcode) = [dbo].[GetBarcode](@bagnumber) AND m.SETTER = @SETTER group by m.inv_no union  select  m.inv_no as jobno,  max(m.note) as note, max(iif(m.rcv_date is null, m.qty, 0)) - max(iif(m.rcv_date is not null, m.qty, 0))  AS qty,  max(oi.pon) as pon,  max(oi.style) as style, max(iif(m.rcv_date is null, m.qty, 0)) - max(iif(m.rcv_date is not null, m.qty, 0))  AS[OPEN], max(m.vend_inv) as vend_inv,  max(m.vinv_dte) as vinv_dte  from mfg m LEFT JOIN LBL_BAR oi on m.inv_no = oi.barcode  WHERE RIGHT('000000'+M.INV_NO,6) = RIGHT('000000'+@bagnumber,6) AND OI.BARCODE = @bagnumber AND m.SETTER = @SETTER group by m.inv_no",
                "@bagnumber", barcode, "@SETTER", setter);
        }

        public string savereturnjobdata(string log_no, string setter, string loggeduser, string dt, string opt)
        {
            // Use `using` to ensure proper disposal of resources
            using (var connection = _connectionProvider.GetConnection())
            using (var command = new SqlCommand("GiveJobBackToPerson", connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                // Add input parameters
                command.Parameters.AddWithValue("@JOBBAGNO", log_no);
                command.Parameters.AddWithValue("@MFGINFO", dt);
                command.Parameters.AddWithValue("@SETTERNAME", setter);
                command.Parameters.AddWithValue("@OPT", opt);
                command.Parameters.AddWithValue("@LOGGEDUSER", loggeduser);

                // Output parameter
                var outInvNo = new SqlParameter("@RETVAL", SqlDbType.VarChar, 1000)
                {
                    Direction = ParameterDirection.Output
                };
                command.Parameters.Add(outInvNo);

                // Execute the command
                connection.Open();
                command.ExecuteNonQuery();

                // Return the output parameter value
                return outInvNo.Value.ToString();
            }
        }

        public DataTable updJobNAddToRsv(string jbnumber, string settername, decimal Weight, string logno = "", string storeno = "")
        {
            DataTable dataTable = new DataTable();

            using (var connection = _connectionProvider.GetConnection())
            using (var command = new SqlCommand("updgiveoutjob", connection))
            using (var adapter = new SqlDataAdapter(command))
            {
                command.CommandType = CommandType.StoredProcedure;

                // Add parameters
                command.Parameters.AddWithValue("@jobbagno", jbnumber);
                command.Parameters.AddWithValue("@newsettername", settername);
                command.Parameters.AddWithValue("@fin_rsv", 1);
                command.Parameters.AddWithValue("@Weight", Weight);
                command.Parameters.AddWithValue("@logno", logno);
                command.Parameters.AddWithValue("@storeno", storeno);

                // Fill the DataTable if data is returned
                adapter.Fill(dataTable);
            }
            return dataTable;
        }

        public DataTable updJobNAddToStk(string jbnumber, string settername, decimal Weight, string logno = "", string storeno = "")
        {
            DataTable dataTable = new DataTable();

            using (var connection = _connectionProvider.GetConnection())
            using (var command = new SqlCommand("updgiveoutjob", connection))
            using (var adapter = new SqlDataAdapter(command))
            {
                command.CommandType = CommandType.StoredProcedure;

                // Add parameters
                command.Parameters.AddWithValue("@jobbagno", jbnumber);
                command.Parameters.AddWithValue("@newsettername", settername);
                command.Parameters.AddWithValue("@fin_rsv", 0);
                command.Parameters.AddWithValue("@Weight", Weight);
                command.Parameters.AddWithValue("@logno", logno);
                command.Parameters.AddWithValue("@storeno", storeno);

                // Fill the DataTable if data is returned
                adapter.Fill(dataTable);
            }
            return dataTable;
        }

        public DataTable GetJobsForAck(string cShop)
        {
            return _helperCommonService.GetSqlData(@"SELECT DISTINCT JOBBAG, ACKED, FROM_STORE, QTY FROM SEND_RPR WHERE ACKED=0 AND TO_STORE=@cShop ORDER BY JOBBAG ASC", "@cShop", cShop);
        }

        public DataTable checkJobBagIsAlreadySplittedOrNotForPrint(string jobbagno)
        {
            return _helperCommonService.GetSqlData("select m.transact,m.inv_no as jobno,o.style,m.note,s.cast_name,m.date,m.time,m.qty,s.stone_wt as stoneeach,(m.qty * s.stone_wt) as totalstone,(m.qty * s.cost) as totalcost  from mfg m left join or_items o on substring(m.inv_no,1,6) = substring(o.barcode,1,6) left join styles s on s.style = o.style where m.log_no = @bagnumber", "@bagnumber", jobbagno);
        }

        public DataTable checkrcvobbag(string jobbagno)
        {
            return _helperCommonService.GetSqlData("select m.transact,m.inv_no,oi.style,m.note,s.vnd_style,m.rcv_date,m.rcv_time,m.qty,s.gold_wt,0 as labor_cost from mfg m left join or_items oi on oi.barcode = m.inv_no left join styles s on oi.style = s.style where m.log_no = @bagnumber", "@bagnumber", jobbagno);
        }
    }
}
