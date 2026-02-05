using Microsoft.Data.SqlClient;
using System.Data;

namespace YJWebCoreMVC.Services
{
    public class WholesaleMemosService
    {
        private readonly ConnectionProvider _connectionProvider;
        private readonly HelperCommonService _helperCommonService;

        public WholesaleMemosService(ConnectionProvider connectionProvider, HelperCommonService helperCommonService)
        {
            _connectionProvider = connectionProvider;
            _helperCommonService = helperCommonService;
        }

        public DataRow GetMemoByInvNo(string memo_no)
        {
            return _helperCommonService.GetSqlRow("select top 1 i.*, it.memo_no,it.fpon from Memo i with (nolock) left join me_items it with (nolock) on i.memo_no = it.memo_no Where trim(i.memo_no) = trim(@memo_no)", "@memo_no", memo_no);
        }

        public DataRow GetInvoiceByInvNo(string invno)
        {
            return (_helperCommonService.GetSqlRow(@"Select top 1 i.*,it.memo_no,ISNULL(it.IsSpecialItem,0) IsSpecialItem,it.fpon 
                                           from invoice i  with (nolock) left join (select * from IN_ITEMS with (nolock) where Trimmed_inv_no =@inv_no) it on i.inv_no = it.inv_no 
                                           Where i.trimmed_inv_no = @inv_no", "@inv_no", invno.Trim()));
        }
        public bool ModifyInvACC(string invNo, string custCode, string billingAcc, bool isMemo)
        {
            using (var connection = _connectionProvider.GetConnection())
            using (var command = new SqlCommand("MODIFYINVANDMEMCUSCODE", connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                // Add parameters
                command.Parameters.AddWithValue("@INVNO", invNo);
                command.Parameters.AddWithValue("@NEWCUS", custCode);
                command.Parameters.AddWithValue("@NEWBIL", billingAcc);
                command.Parameters.AddWithValue("@ISMEMO", isMemo ? 1 : 0);

                // Open connection, execute the query, and return the result
                connection.Open();
                return command.ExecuteNonQuery() > 0;
            }
        }
    }
}
