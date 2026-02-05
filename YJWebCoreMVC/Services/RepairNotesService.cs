using System.Data;

namespace YJWebCoreMVC.Services
{
    public class RepairNotesService
    {
        private readonly ConnectionProvider _connectionProvider;
        private readonly HelperCommonService _helperCommonService;

        public RepairNotesService(ConnectionProvider connectionProvider, HelperCommonService helperCommonService)
        {
            _connectionProvider = connectionProvider;
            _helperCommonService = helperCommonService;
        }

        public DataTable GetAllNotes()
        {
            return _helperCommonService.GetSqlData(@"SELECT Note,Price FROM Repair_notes ORDER BY Note ASC");
        }
        public bool checkIsSkuExisted(string sku)
        {
            return _helperCommonService.DataTableOK(_helperCommonService.GetSqlData(@"SELECT * FROM [DBO].[Repair_notes] WHERE SKU = @SKU", "@SKU", sku));
        }
    }
}
