// Hemanth 08/29/2024 created new model
// hemanth 08/30/2024 function naming chaged due to violation issue
// Hemanth 08/31/2024 removed unnessasary calls and show common details from helper
/*
 * Chakri   04/10/2025 Added saveFollowTypes method.
 * Chakri   04/11/2025 Added GetAllSalesman method.
 * chakri   04/22/2025 Added GetReferralLoyaltyPoints.
 * chakri   04/28/2025 Added CheckValidOrderRepair, DelRepairOrder.
 * chakri   04/30/2025 Added GetInvoiceNumberBasedOnRepairNumber, GetInvoiceHeaderInformatioBasedOnInvoiceNumber and DeleteInvoice.
 * chakri   05/02/2025 Added DelScrapGold method.
 * chakri   05/05/2025 Added GetCashoutItems method.
 * chakri   05/06/2025 Added GetProformaInvoice and DeleteProformaInvoice.
 * chakri   05/07/2025 Added CheckValidCustomerCode and DeleteCustomer methods.
 * chakri   05/07/2025 Added GetSalesmancode and DeleteSalesmanacc methods.
 * chakri   05/09/2025 Added Try, catch in DeleteCustomer method.
 * chakri   05/12/2025 Added GetMemoByInvNo and DeleteMemo methods.
 * chakri   05/13/2025 Added GetRtvHeadInfo,DeleteRtv and CheckPayItems methods.
 * chakri   05/14/2025 Added Try, Catch in DeleteMemo method.
 * chakri   05/15/2025 Added GetPotentialCustomerByAcc and GetLotInfo, DelALotNO, iSUsedImagesToOthereStyle methods.
 * chakri   05/20/2025 Added CheckValidStoreCreditorGiftCard, CancelStoreCreditorGiftCard, GetAllSroreCredits methods.
 * chakri   05/20/2025 Added CheckValidOrder and DeleteCustPO methods.
 * chakri   05/20/2025 Added CheckValidBill, GetVendorNameByCode and DeleteBill methods.
 * chakri   05/21/2025 Added  Try, Catch in DeleteBill method.
 * chakri   05/22/2025 Added CheckStyleInstockqty method.
 * Phanindra 06/04/2025 Added CheckValidBillingAcct method
 * Phanindra 08/26/2025 Added UpdateCustomerRecord method
 * Hemanth   10/03/2025 Added GetEmail method
 */
using System.Data;

namespace YJWebCoreMVC.Services
{
    public class CustomerService
    {
        private readonly ConnectionProvider _connectionProvider;
        private readonly HelperService _helperService;

        public CustomerService(ConnectionProvider connectionProvider, HelperService helperService)
        {
            _connectionProvider = connectionProvider;
            _helperService = helperService;
        }

        public DataTable getAllStates()
        {
            return _helperService.HelperCommon.GetSqlData("select distinct STATE1 from CUSTOMER where STATE1 != '' and STATE1 is not null order by STATE1");
        }

        public DataTable getAllCountries()
        {
            return _helperService.HelperCommon.GetSqlData("select distinct COUNTRY from CUSTOMER where COUNTRY != '' and COUNTRY is not null order by COUNTRY");
        }
        public DataTable getAllSalesmans()
        {
            return _helperService.HelperCommon.GetSqlData("select distinct CODE from SALESMEN where CODE != '' and CODE is not null order by CODE");
        }

        public DataTable GetListBoxData(string TblName, string fldname, string MixwithEvent = "")
        {
            if (MixwithEvent != "")
                return _helperService.HelperCommon.GetSqlData(" SELECT DISTINCT SUBEVENT FROM " + TblName + " where  " + fldname + "= '" + MixwithEvent + "' order by SUBEVENT");
            return _helperService.HelperCommon.GetSqlData(" SELECT DISTINCT " + fldname + " FROM " + TblName + " order by " + fldname);
        }

    }
}
