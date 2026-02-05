/*
   *  Created by Manoj 19-May-2025
   *  20-May-2025 Manoj Added CharityDonations and getListofCharityDonations methods
   *  23-May-2025 Manoj Fixed Date and DDL Filter issue while fetch data 
 */
using System.Data;

namespace YJWebCoreMVC.Services
{
    public class SalesPOSReportsService
    {
        private readonly ConnectionProvider _connectionProvider;
        private readonly HelperCommonService _helperCommonService;

        public SalesPOSReportsService(ConnectionProvider connectionProvider, HelperCommonService helperCommonService)
        {
            _connectionProvider = connectionProvider;
            _helperCommonService = helperCommonService;
        }

        public DataTable getCharitiesList()
        {
            return _helperCommonService.GetSqlData("select * from charities with (nolock) order by charity");
        }

        public DataTable getCharityDonations(string sFilter = "")
        {
            string query = "select inv_no,CAST(date AS date) as date,name,charity,charity_amount from invoice with (nolock) where charity_amount > 0";


            if (!string.IsNullOrEmpty(sFilter))
            {
                query = "select inv_no,CAST(date AS date) as date,name,charity,charity_amount from invoice with (nolock) where charity_amount > 0" + sFilter;
            }


            return _helperCommonService.GetSqlData(query);
        }
    }
}
