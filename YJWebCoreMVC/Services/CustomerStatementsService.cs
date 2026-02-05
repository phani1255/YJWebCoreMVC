// Neetha    02/27/2025   Created New Model//
// Neetha    04/02/2025   Added  GetDetailedAgingAsOfADate, CustBalance methods. //
// Dharani   06/02/2025   Added GetArAsOfDateInfo method. 
// Dharani   06/03/2025   Added getMemoInvDiffPrice method. 
// Dharani   06/10/2025   Added Properties for finance charges form
// Manoj     07/23/2025   Added GetMonthlies method.
// Dharani   09/24/2025   Added CustomerStatement method.

using Microsoft.Data.SqlClient;
using System.Data;

namespace YJWebCoreMVC.Services
{
    public class CustomerStatementsService
    {
        private readonly ConnectionProvider _connectionProvider;
        private readonly HelperCommonService _helperCommonService;

        public CustomerStatementsService(ConnectionProvider connectionProvider, HelperCommonService helperCommonService, IWebHostEnvironment env)
        {
            _connectionProvider = connectionProvider;
            _helperCommonService = helperCommonService;
        }


        public DataTable GetRtvDetailsWithMemo()
        {
            return _helperCommonService.GetStoreProc("DetailedAging");
        }
        public DataRow CheckValidBillingAcct(string billacc)
        {
            return _helperCommonService.GetSqlRow("select *  From Customer Where rtrim(bill_acc)= rtrim(@bill_acc) and bill_acc = acc", "@bill_acc", billacc);
        }
        public DataRow GetInvoiceByInvNo(string invno)
        {
            return (_helperCommonService.GetSqlRow(@"select top 1 i.*,it.memo_no,iSNULL(it.IsSpecialItem,0) IsSpecialItem,it.fpon 
                from invoice i 
                left join (select * from IN_ITEMS where trim(INV_NO) =@inv_no) it on i.inv_no = it.inv_no 
                    Where trim(i.inv_no) = @inv_no", "@inv_no", invno.Trim()));
        }
        public DataTable GetDetailedAgingAsOfADate(string strSearch)
        {
            DataTable dataTable = new DataTable();
            using (SqlDataAdapter SqlDataAdapter = new SqlDataAdapter())
            {
                SqlDataAdapter.SelectCommand = new SqlCommand();
                SqlDataAdapter.SelectCommand.Connection = _connectionProvider.GetConnection();
                SqlDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter.SelectCommand.CommandTimeout = 3000;
                SqlDataAdapter.SelectCommand.CommandText = "DetailedAgingAsOfADate";
                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@strSearch", strSearch);
                SqlDataAdapter.Fill(dataTable);
            }

            return dataTable;
        }
        public void CustBalance(String asdate, String acc, out decimal bal)
        {

            DataTable dtBalance = _helperCommonService.GetSqlData(@"SELECT ACC,SUM(PAID) PAID FROM PAYMENTS WHERE CAST(DATE AS DATE) <= CAST(@asdate AS DATE) AND ACC=@acc AND (RTV_PAY<>'R' OR INV_NO IN(SELECT RTV_MEMO FROM RTV_HEAD WHERE INV_NO=PAYMENTS.INV_NO AND RTV_HEAD.RTV_MEMO=0)) GROUP BY ACC", "@asdate", asdate, "@acc", acc);
            bal = _helperCommonService.DataTableOK(dtBalance) ? _helperCommonService.DecimalCheckForDBNull(dtBalance.Rows[0]["PAID"]) : 0;
        }

        public DataTable GetArAsOfDateInfo(string datefrom, string dateto)
        {
            return _helperCommonService.GetStoreProc("ArAsOfDate", "@cdate", datefrom);
        }

        public DataTable getMemoInvDiffPrice(string acc)
        {
            using (var connection = _connectionProvider.GetConnection())
            using (var command = new SqlCommand("getMemoInvDiffPrice", connection))
            using (var adapter = new SqlDataAdapter(command))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandTimeout = 3000;

                // Add parameters
                command.Parameters.AddWithValue("@ACC", acc);

                var dataTable = new DataTable();
                adapter.Fill(dataTable);
                return dataTable;
            }
        }
        public DataTable GetMonthlies()
        {
            return _helperCommonService.GetSqlData("SELECT ISNULL(CUSTOMER_CODE,'')AS CUSTOMER_CODE,ISNULL(AMOUNT,'') AS AMOUNT ,ISNULL(FINAL_DATE,'') as FINAL_DATE,ISNULL(LAST_CHARGE,'')as LAST_CHARGE,ISNULL(DAY_OF_MONTH,'') AS DAY_OF_MONTH FROM MONTHLIES Order By CUSTOMER_CODE");
        }
        public DataTable CustomerStatement(string bacc, string date1, string date2, int optval, string sortby, bool showmemo, out decimal invoicetotal, out decimal sfmtotal, out decimal memototal, out decimal credittotal, out decimal paymenttotal, out decimal rtvtotal, out decimal invoicebalance, out decimal memobalance, out decimal rtvtotali, out decimal rtvtotalm, out decimal totalbalance, out decimal currentbal, out decimal bal30days, out decimal bal60days, out decimal bal90days, out decimal bal90plusdays, out decimal baldue_now, out decimal TotNotPickUP, bool IsDesc = false, bool showProforma = false, bool Isnotbreakbyduedate = false, bool isIncludeNotPickup = false)
        {
            DataTable dataTable = new DataTable();

            using (SqlDataAdapter SqlDataAdapter = new SqlDataAdapter())
            {

                SqlDataAdapter.SelectCommand = new SqlCommand()
                {
                    Connection = _connectionProvider.GetConnection(),
                    CommandType = CommandType.StoredProcedure,
                    CommandTimeout = 9000,
                    CommandText = "Customer_Statement"
                };
                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@bacc", bacc);
                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@date1", date1);
                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@date2", date2);
                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@optval", optval);
                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@memo", showmemo ? 1 : 0);
                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@performa", showProforma ? 1 : 0);
                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@SORTBY", sortby);
                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@IsDesc", IsDesc);
                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@NotbreakbyDuedate", Isnotbreakbyduedate ? 1 : 0);
                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@IncludeNotPickup", isIncludeNotPickup ? 1 : 0);
                SqlParameter pinvoicetotal = new SqlParameter("@invoicetotal", SqlDbType.Decimal) { Precision = 18, Scale = 2 };
                pinvoicetotal.Direction = ParameterDirection.Output;
                SqlDataAdapter.SelectCommand.Parameters.Add(pinvoicetotal);

                SqlParameter psfmtotal = new SqlParameter("@sfmtotal", SqlDbType.Decimal) { Precision = 18, Scale = 2 };
                psfmtotal.Direction = ParameterDirection.Output;
                SqlDataAdapter.SelectCommand.Parameters.Add(psfmtotal);

                SqlParameter pmemototal = new SqlParameter("@memototal", SqlDbType.Decimal) { Precision = 18, Scale = 2 };
                pmemototal.Direction = ParameterDirection.Output;
                SqlDataAdapter.SelectCommand.Parameters.Add(pmemototal);

                SqlParameter pcredittotal = new SqlParameter("@credittotal", SqlDbType.Decimal) { Precision = 18, Scale = 2 };
                pcredittotal.Direction = ParameterDirection.Output;
                SqlDataAdapter.SelectCommand.Parameters.Add(pcredittotal);

                SqlParameter ppaymenttotal = new SqlParameter("@paymenttotal", SqlDbType.Decimal) { Precision = 18, Scale = 2 };
                ppaymenttotal.Direction = ParameterDirection.Output;
                SqlDataAdapter.SelectCommand.Parameters.Add(ppaymenttotal);

                SqlParameter prtvtotal = new SqlParameter("@rtvtotal", SqlDbType.Decimal) { Precision = 18, Scale = 2 };
                prtvtotal.Direction = ParameterDirection.Output;
                SqlDataAdapter.SelectCommand.Parameters.Add(prtvtotal);

                SqlParameter pinvoicebalance = new SqlParameter("@invoicebalance", SqlDbType.Decimal) { Precision = 18, Scale = 2 };
                pinvoicebalance.Direction = ParameterDirection.Output;
                SqlDataAdapter.SelectCommand.Parameters.Add(pinvoicebalance);

                SqlParameter pmemobalance = new SqlParameter("@memobalance", SqlDbType.Decimal) { Precision = 18, Scale = 2 };
                pmemobalance.Direction = ParameterDirection.Output;
                SqlDataAdapter.SelectCommand.Parameters.Add(pmemobalance);

                SqlParameter prtvtotali = new SqlParameter("@rtvtotali", SqlDbType.Decimal) { Precision = 18, Scale = 2 };
                prtvtotali.Direction = ParameterDirection.Output;
                SqlDataAdapter.SelectCommand.Parameters.Add(prtvtotali);

                SqlParameter prtvtotalm = new SqlParameter("@rtvtotalm", SqlDbType.Decimal) { Precision = 18, Scale = 2 };
                prtvtotalm.Direction = ParameterDirection.Output;
                SqlDataAdapter.SelectCommand.Parameters.Add(prtvtotalm);

                SqlParameter ptotalbalance = new SqlParameter("@totalbalance", SqlDbType.Decimal) { Precision = 18, Scale = 2 };
                ptotalbalance.Direction = ParameterDirection.Output;
                SqlDataAdapter.SelectCommand.Parameters.Add(ptotalbalance);

                SqlParameter pcurbal = new SqlParameter("@currentbal", SqlDbType.Decimal) { Precision = 18, Scale = 2 };
                pcurbal.Direction = ParameterDirection.Output;
                SqlDataAdapter.SelectCommand.Parameters.Add(pcurbal);

                SqlParameter pbal30days = new SqlParameter("@30DAYSBAL", SqlDbType.Decimal) { Precision = 18, Scale = 2 };
                pbal30days.Direction = ParameterDirection.Output;
                SqlDataAdapter.SelectCommand.Parameters.Add(pbal30days);

                SqlParameter pbal60days = new SqlParameter("@60DAYSBAL", SqlDbType.Decimal) { Precision = 18, Scale = 2 };
                pbal60days.Direction = ParameterDirection.Output;
                SqlDataAdapter.SelectCommand.Parameters.Add(pbal60days);

                SqlParameter pbal90days = new SqlParameter("@90DAYSBAL", SqlDbType.Decimal) { Precision = 18, Scale = 2 };
                pbal90days.Direction = ParameterDirection.Output;
                SqlDataAdapter.SelectCommand.Parameters.Add(pbal90days);

                SqlParameter pbal90plusdays = new SqlParameter("@90PLUSDAYSBAL", SqlDbType.Decimal) { Precision = 18, Scale = 2 };
                pbal90plusdays.Direction = ParameterDirection.Output;
                SqlDataAdapter.SelectCommand.Parameters.Add(pbal90plusdays);

                SqlParameter pdue_now = new SqlParameter("@DUE_NOW", SqlDbType.Decimal) { Precision = 18, Scale = 2 };
                pdue_now.Direction = ParameterDirection.Output;
                SqlDataAdapter.SelectCommand.Parameters.Add(pdue_now);

                SqlParameter pnotpickup = new SqlParameter("@TotNotPickUP", SqlDbType.Decimal) { Precision = 18, Scale = 2 };
                pnotpickup.Direction = ParameterDirection.Output;
                SqlDataAdapter.SelectCommand.Parameters.Add(pnotpickup);

                SqlDataAdapter.Fill(dataTable);
                invoicetotal = pinvoicetotal.Value != DBNull.Value ? Convert.ToDecimal(pinvoicetotal.Value) : 0.0M;
                sfmtotal = psfmtotal.Value != DBNull.Value ? Convert.ToDecimal(psfmtotal.Value) : 0;
                memototal = pmemototal.Value != DBNull.Value ? Convert.ToDecimal(pmemototal.Value) : 0;
                credittotal = pcredittotal.Value != DBNull.Value ? Convert.ToDecimal(pcredittotal.Value) : 0;
                paymenttotal = ppaymenttotal.Value != DBNull.Value ? Convert.ToDecimal(ppaymenttotal.Value) : 0;
                rtvtotal = prtvtotal.Value != DBNull.Value ? Convert.ToDecimal(prtvtotal.Value) : 0;
                memobalance = pmemobalance.Value != DBNull.Value ? Convert.ToDecimal(pmemobalance.Value) : 0;
                invoicebalance = pinvoicebalance.Value != DBNull.Value ? Convert.ToDecimal(pinvoicebalance.Value) : 0;
                rtvtotali = prtvtotali.Value != DBNull.Value ? Convert.ToDecimal(prtvtotali.Value) : 0;
                rtvtotalm = prtvtotalm.Value != DBNull.Value ? Convert.ToDecimal(prtvtotalm.Value) : 0;
                totalbalance = ptotalbalance.Value != DBNull.Value ? Convert.ToDecimal(ptotalbalance.Value) : 0;
                currentbal = pcurbal.Value != DBNull.Value ? Convert.ToDecimal(pcurbal.Value) : 0;
                bal30days = pbal30days.Value != DBNull.Value ? Convert.ToDecimal(pbal30days.Value) : 0;
                bal60days = pbal60days.Value != DBNull.Value ? Convert.ToDecimal(pbal60days.Value) : 0;
                bal90days = pbal90days.Value != DBNull.Value ? Convert.ToDecimal(pbal90days.Value) : 0;
                bal90plusdays = pbal90plusdays.Value != DBNull.Value ? Convert.ToDecimal(pbal90plusdays.Value) : 0;
                baldue_now = pdue_now.Value != DBNull.Value ? Convert.ToDecimal(pdue_now.Value) : 0;
                TotNotPickUP = pnotpickup.Value != DBNull.Value ? Convert.ToDecimal(pnotpickup.Value) : 0;
            }

            return dataTable;
        }


    }
}
