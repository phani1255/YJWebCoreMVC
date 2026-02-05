/* Hemanth 08/19/2025 created new model */
using System.Data;

namespace YJWebCoreMVC.Services
{
    public class AppraisalsService
    {
        private readonly HelperCommonService _helperCommonService;

        public AppraisalsService(HelperCommonService helperCommonService)
        {
            _helperCommonService = helperCommonService;
        }
        public DataTable GetListofAppraisals(string date1, string date2, string FixedstoreCode = "")
        {
            return _helperCommonService.GetSqlData($"SELECT AppraisalID, AppDate AS [Date1], ACC as Customer, AccName AS [Name], Store, Salesman AS Salesrep, AppDate AS [Date] FROM Appraisal with (nolock) WHERE CAST(AppDate AS DATE) BETWEEN @DATE1 AND @DATE2 and (Store = '{FixedstoreCode}' or '{FixedstoreCode}'  ='')", "@DATE1", getdate(date1), "@DATE2", getdate(date2));
        }

        private string getdate(string date1)
        {
            return date1?.Split(' ')[0] ?? string.Empty;
        }
    }
}
