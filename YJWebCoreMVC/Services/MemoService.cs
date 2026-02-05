/*
 * Created by Manoj 06-25-2025
 * 06-25-2025 Manoj Added CustomerCodes Property
 * 06-26-2025 Manoj Added SearchRtvs Method
 * 06-26-2025 Manoj Resending File
 * 07-04-2025 Manoj Resending File
 * 07-04-2025 Manoj Added StyleItems property , GetStyleTrackingSummaryData , GetStyles and GetStyleTrackingData  methods
 * 07-10-2025 Manoj Added GetStatementofMemoData Method
 * Dharani 12/03/2025 Added GetMemoByInvNo method
 */
using System.Data;

namespace YJWebCoreMVC.Services
{
    public class MemoService
    {
        private readonly ConnectionProvider _connectionProvider;
        private readonly HelperCommonService _helperCommonService;

        public MemoService(ConnectionProvider connectionProvider, HelperCommonService helperCommonService)
        {
            _connectionProvider = connectionProvider;
            _helperCommonService = helperCommonService;
        }

        public DataTable SearchRtvs(string frmcustcode, string tocuscode, string fdate, string tdate, string sbydate, string smcode)
        {
            return _helperCommonService.GetStoreProc("ListOfRtv", "@FROMCUSTOMERCODE", frmcustcode, "@TOCUSTOMERCODE", tocuscode, "@FROMDATE", fdate, "@TODATE", tdate, "@REQUIREDDATEFIELD", sbydate, "@STORENO", smcode);
        }

        public DataTable GetStyleTrackingSummaryData(string styles, bool showinvoice, int memoval, string date1, string date2, bool iSSearchStyle = false, bool iSByAcc = false)
        {
            return _helperCommonService.GetStoreProc("InvoiceMemoStyleTrackingSummary", "@styleno", styles, "@onlyopenmemo", showinvoice ? "1" : "0", "@memoval", memoval.ToString(), "@date1", date1, "@date2", date2, "@ParentStyleSearch", iSSearchStyle ? "1" : "0", "@byBACC", iSByAcc ? "1" : "0");
        }

        public DataTable GetStyleTrackingData(string styles, bool showinvoice, int memoval, string date1, string date2, bool iSSearchStyle = false, bool byAcc = false)
        {
            return _helperCommonService.GetStoreProc("InvoiceMemoStyleTracking", "@styleno", styles, "@showinvoice", showinvoice ? "1" : "0", "@memoval", memoval.ToString(), "@date1", date1, "@date2", date2, "@ParentStyleSearch", iSSearchStyle ? "1" : "0", "@byBACC", byAcc ? "1" : "0");
        }

        public DataTable GetStyles()
        {
            return _helperCommonService.GetSqlData("select style from styles with (nolock) order by style");
        }
        public DataTable GetStatementofMemoData(string memo, bool onlyopenmemo, string date1, string date2, string storeno)
        {
            return _helperCommonService.GetStoreProc("StatementofMemo", "@memo", memo, "@onlyopenmemo", onlyopenmemo ? "1" : "0", "@date1", date1, "@date2", date2, "@CSTORE", storeno);
        }
        public DataRow GetMemoByInvNo(string memo_no)
        {
            return _helperCommonService.GetSqlRow("select top 1 i.*, it.memo_no,it.fpon from Memo i with (nolock) left join me_items it with (nolock) on i.memo_no = it.memo_no Where trim(i.memo_no) = trim(@memo_no)", "@memo_no", memo_no);
        }

    }
}
