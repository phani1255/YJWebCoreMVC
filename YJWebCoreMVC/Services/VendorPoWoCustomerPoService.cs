using System.Data;

namespace YJWebCoreMVC.Services
{
    public class VendorPoWoCustomerPoService
    {
        private readonly ConnectionProvider _connectionProvider;
        private readonly HelperCommonService _helperCommonService;

        public VendorPoWoCustomerPoService(ConnectionProvider connectionProvider, HelperCommonService helperCommonService)
        {
            _connectionProvider = connectionProvider;
            _helperCommonService = helperCommonService;
        }

        public DataTable getvendorPoinfobasedoninv(string inv)
        {
            return _helperCommonService.GetSqlData("select co.inv_no,co.date,co.style,co.size,co.qty,co.rcvd,co.acc,co.due_date,co.gold_price,s.price,co.Approved,co.Approve_Date,co.Approve_Note from CAST_ORD co left join styles s on s.style = co.style where trim(co.inv_no) in(@inv) ", "@inv", inv.Trim());
        }
        public DataTable getvendorPoinfobasedoninvwithvendorinfo(string inv)
        {
            return _helperCommonService.GetStoreProc("VendorPoInfoBasedOnInv", "@inv", inv);
        }

        public DataRow getvendorDetails(string vendorname)
        {
            return _helperCommonService.GetSqlRow("select name,addr11,addr12,city1,state1,zip1,tel,email from vendors where acc=@vendorname", "@vendorname", vendorname);
        }
        public DataTable getllpodetails(string selectedvendor, string fvendorpo, string tvendorpo, string fponumber, string tponumber, string fdate, string tdate, string summarydetails, string opendetails)
        {
            return _helperCommonService.GetStoreProc("ListofVendorPoWoCustomerPo", "@SELECTEDVENDOR", selectedvendor, "@FROMVENDORPO", fvendorpo, "@TOVENDORPO", tvendorpo, "@FROMPONUMBER", fponumber, "@TOPONUMBER", tponumber, "@FROMDATE", Convert.ToDateTime(fdate).ToString(),
                                            "@TODATE", Convert.ToDateTime(tdate).ToString(), "@SUMMERYOPTION", summarydetails, "@OPENOPTION", opendetails);

        }
    }
}
