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
