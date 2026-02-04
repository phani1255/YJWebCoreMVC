/*
 *  Created By Chakri on 09-Jun-2025
 *  Chakri 06/09/2025 Added checkglpostlog and DeleteGLCode methods.
 *  Chakri 01/30/2026 Added GetAdjustInventoryDetails method.
 *  
 */
using Microsoft.Data.SqlClient;
using System.Data;

namespace YJWebCoreMVC.Services
{
    public class FinancialService
    {
        private readonly ConnectionProvider _connectionProvider;
        private readonly HelperCommonService _helperCommonService;

        public FinancialService(ConnectionProvider connectionProvider, HelperCommonService helperCommonService, IWebHostEnvironment env)
        {
            _connectionProvider = connectionProvider;
            _helperCommonService = helperCommonService;
        }


        public DataTable checkglpostlog(string logno, bool Delete = false)
        {
            if (Delete)
                return _helperCommonService.GetSqlData(@"SELECT trim(isnull(CODE,'')) as CODE,isnull(NAME,'Invalid Acct# ' + code) NAME,GLTYPE,AMOUNT, DEPT, INV_NO as REF,  NOTE, LOG_NO,TRANSACT, DATE,TIME,A.[TYPE],cast(iSNULL([manual],0) as bit) [manual]  from glpost a  join gl_accs b on trim(a.code) = trim(b.acc) where log_no =@log_no", "@log_no", logno);
            return _helperCommonService.GetSqlData(@"SELECT trim(isnull(CODE,'')) as CODE, DEPT,isnull(NAME,'Invalid Acct# ' + code) NAME,GLTYPE, AMOUNT AS DEBIT, AMOUNT AS CREDIT, INV_NO as REF,  NOTE,LOG_NO, TRANSACT, DATE,TIME,A.[TYPE],cast(iSNULL([manual],0) as bit) [manual]  from glpost a left join gl_accs b on trim(a.code) = trim(b.acc) where log_no = @log_no", "@log_no", logno);
        }
        public DataTable DeleteGLCode(string logno)
        {
            return _helperCommonService.GetSqlData("delete from GLPOST where log_no = @logno", "@logno", logno);
        }

        public DataTable GetAdjustInventoryDetails(bool isSummary, string assetGL = "", string dept = "")
        {
            using (var connection = _connectionProvider.GetConnection())
            using (var dataAdapter = new SqlDataAdapter("AdjustInventoryGL", connection))
            {
                dataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                dataAdapter.SelectCommand.Parameters.AddWithValue("@SUMMARY", isSummary);
                dataAdapter.SelectCommand.Parameters.AddWithValue("@ASSET_GL", assetGL);
                dataAdapter.SelectCommand.Parameters.AddWithValue("@DEPT", dept);
                var dataTable = new DataTable();
                dataAdapter.Fill(dataTable);
                return dataTable;
            }
        }

    }
}
