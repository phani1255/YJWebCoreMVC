using System.Data;

namespace YJWebCoreMVC.Services
{
    public class BankingBankAccountsService
    {
        private readonly HelperCommonService _helperCommonService;

        public BankingBankAccountsService(HelperCommonService helperCommonService)
        {
            _helperCommonService = helperCommonService;
        }


        public DataTable GetBankcode(string MyStore = "")
        {
            return _helperCommonService.GetSqlData(@"select '' as CODE From bank_acc union select code from bank_acc 
                where @MyStore='' or @MyStore=store_no or store_no='' order by code", "@MyStore", MyStore);
        }

        public DataRow FindBankCodeInTran(string bcode)
        {
            return _helperCommonService.GetSqlRow("select *  From bank Where bank = @CODE", "@CODE", bcode);
        }
        public DataTable GetBankBycode(string bcode)
        {
            return _helperCommonService.GetSqlData("SELECT * FROM BANK_ACC WHERE CODE = @CODE", "@CODE", bcode);
        }
        public void DeleteBankacc(string bcode)
        {
            _helperCommonService.GetStoreProc("DelBankAcc", "@CODE", bcode);
        }
    }
}
