using System.Data;

namespace YJWebCoreMVC.Services
{
    public class CustomerReturnsService
    {
        private readonly ConnectionProvider _connectionProvider;
        private readonly HelperCommonService _helperCommonService;

        public CustomerReturnsService(ConnectionProvider connectionProvider, HelperCommonService helperCommonService, IWebHostEnvironment env)
        {
            _connectionProvider = connectionProvider;
            _helperCommonService = helperCommonService;
        }


        public DataTable GetRtvHeadInfo(string rtvno, string billingacctno)
        {
            return _helperCommonService.GetStoreProc("RtvHead", "@RTVNO", rtvno, "@BILLINGNO", billingacctno);
        }
        public DataTable GetRtvItemInfo(string rtvno, string billingacctno)
        {
            return _helperCommonService.GetStoreProc("RtvItems", "@RTVNO", rtvno.Trim(), "@BILLINGNO", billingacctno);
        }
        public DataTable GetRtvDescription(string rtvno, string billingacctno)
        {
            return _helperCommonService.GetSqlData(@"SELECT PAY_NO FROM PAY_ITEM WHERE INV_NO = @RTVNO AND RTV_PAY = 'R'", "@RTVNO", rtvno, "@BILLINGNO", billingacctno);
        }

    }
}
