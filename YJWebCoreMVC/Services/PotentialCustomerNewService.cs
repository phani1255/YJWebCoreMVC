using System.Data;

namespace YJWebCoreMVC.Services
{
    public class PotentialCustomerNewService
    {
        private readonly ConnectionProvider _connectionProvider;
        private readonly HelperCommonService _helperCommonService;

        public PotentialCustomerNewService(ConnectionProvider connectionProvider, HelperCommonService helperCommonService)
        {
            _connectionProvider = connectionProvider;
            _helperCommonService = helperCommonService;
        }

        public DataTable CheckTemplateExistOrNot(string strTemplateName = "")
        {
            return _helperCommonService.GetSqlData(@"Select * from POTENTIAL_CUSTOMER_Template with(nolock) where Template_Name= @TemplateName", "@TemplateName", strTemplateName);
        }

        public DataTable CheckPotentialAccExistsOrNot_Import(string strXML)
        {
            return _helperCommonService.GetStoreProc("CheckPotentialAccExistsOrNot_Import", "@XML", strXML);
        }
    }
}
