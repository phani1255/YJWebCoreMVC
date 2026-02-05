/*chakri 05/27/2025 created new Model.
 *chakri 05/27/2025 Added GetMemoHeadDetailsForVrtv and DeleteRtv methods.
 * 
 */
using System.Data;

namespace YJWebCoreMVC.Services
{
    public class PayablesReturnsService
    {

        private readonly ConnectionProvider _connectionProvider;
        private readonly HelperCommonService _helperCommonService;

        public PayablesReturnsService(ConnectionProvider connectionProvider, HelperCommonService helperCommonService)
        {
            _connectionProvider = connectionProvider;
            _helperCommonService = helperCommonService;
        }

        public DataTable GetMemoHeadDetailsForVrtv(string invno)
        {
            return _helperCommonService.GetSqlData("SELECT * FROM VRTV_HEAD WHERE Trimmed_inv_no = trim(@INVNO)", "@INVNO", invno.Trim());
        }
        public void DeleteRtv(string INV_NO, string reqtext, string LOGGEDUSER, bool IsStyleItem = false)
        {
            _helperCommonService.GetStoreProc("DELVRTV", "@INV_NO", INV_NO, "@REQUIREDTEXT", reqtext,
                 "@LOGGEDUSER", LOGGEDUSER, "@IsStyleItem", IsStyleItem.ToString());

        }
    }
}
