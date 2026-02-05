/* Manoj 09/04/2025 created DefaultAccountsModel.
 * Manoj 09/04/2025 Added  GlDefaultAccs Property,GetDefaultAccounts,GetDefaultAccountsFromDB Methods
 */

using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data;

namespace YJWebCoreMVC.Services
{
    public class DefaultAccountsService
    {
        private readonly ConnectionProvider _connectionProvider;
        private readonly HelperCommonService _helperCommonService;

        public DefaultAccountsService(ConnectionProvider connectionProvider, HelperCommonService helperCommonService, IWebHostEnvironment env)
        {
            _connectionProvider = connectionProvider;
            _helperCommonService = helperCommonService;
        }

        public IEnumerable<SelectListItem> GlDefaultAccs { get; set; }
        public DataTable GetDefaultAccounts()
        {
            return _helperCommonService.GetSqlData("select Acc,name from gl_accs ORDER BY ACC ASC ");
        }

        public DataRow GetDefaultAccountsFromDB()
        {
            return _helperCommonService.GetSqlRow(@"select gl_sales, gl_snh, gl_ar, gl_memoprice, gl_memocost, gl_cogs, gl_inventory, gl_undep_funds, gl_cashinbank, gl_slsinvnt, gl_comision, gl_ap, gl_cons,gl_inventory_transit,gl_Memo_Liability,gl_trade_in,gl_warranty_sales,gl_warranty_cogs,gl_inv_spl_order, gl_vendor_credit,gl_financed,gl_giftcard,gl_scrap, isnull(use_glcode,0) as use_glcode,isnull(gl_material,'') as gl_material,isnull(gl_over_head,'') as gl_over_head,isnull(gl_repair_sales,'') as gl_repair_sales,gl_inv_adjust,gl_sales_tax,gl_repair_labor,gl_repair_inventory,gl_rpr_lbr_adj,gl_ovh_adj,gl_storecredit from ups_ins");
        }
    }
}
