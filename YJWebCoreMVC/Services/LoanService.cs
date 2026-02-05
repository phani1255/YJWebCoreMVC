/*chakri 06/11/2025 created new Model.
 *chakri 06/11/2025 Added GetLoanInfo, CheckValidCustomerCode and DeleteLoanInfo methods.
 *chakri 06/19/2025 Added GetLoansData,GetLoanBalanceAmount, GetPreviousRecords,DeleteLoanPaymentRecord, GetLastInterestAppliedDate methods.
 *chakri 06/23/2025 Added GetLoanInterestRecords and DeleteLoanInterestRecord methods.
 */

using Microsoft.Data.SqlClient;
using System.Data;

namespace YJWebCoreMVC.Services
{
    public class LoanService
    {
        private readonly ConnectionProvider _connectionProvider;
        private readonly HelperCommonService _helperCommonService;

        public LoanService(ConnectionProvider connectionProvider, HelperCommonService helperCommonService)
        {
            _connectionProvider = connectionProvider;
            _helperCommonService = helperCommonService;
        }

        public DataRow GetLoanInfo(string LoanNo)
        {
            return _helperCommonService.GetSqlRow("SELECT LOAN_NO, ACC, [DATE], LOAN_AMOUNT, INT_RATE, NOTE FROM LOAN WHERE TRIM(LOAN_NO) = TRIM(@loanNo) ORDER BY CAST(LOAN_NO AS INT)", "@loanNo", LoanNo);
        }
        public DataRow CheckValidCustomerCode(string acc)
        {
            return _helperCommonService.GetSqlRow("SELECT TOP 1 * FROM Customer with (nolock) WHERE acc=TRIM(@acc)", "@acc", acc);
        }

        public bool DeleteLoanInfo(string LoanNo)
        {
            DataTable dataTable = new DataTable();

            using (SqlDataAdapter SqlDataAdapter = new SqlDataAdapter())
            {
                SqlDataAdapter.SelectCommand = new SqlCommand();
                SqlDataAdapter.SelectCommand.Connection = _connectionProvider.GetConnection();
                SqlDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter.SelectCommand.CommandText = "DeleteLoanInfo";

                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@LOAN_NO", LoanNo);

                SqlDataAdapter.SelectCommand.Connection.Open();
                var rowsAffected = SqlDataAdapter.SelectCommand.ExecuteNonQuery();
                SqlDataAdapter.SelectCommand.Connection.Close();

                return rowsAffected > 0;
            }
        }
        public DataTable GetLoansData(bool checkBalance = false, string _acc = "")
        {
            if (!checkBalance)
                return _helperCommonService.GetSqlData("SELECT LOAN_NO, ACC, [DATE], LOAN_AMOUNT, INT_RATE, NOTE FROM LOAN ORDER BY CAST(LOAN_NO AS INT)");
            return _helperCommonService.GetSqlData($"SELECT L.LOAN_NO LOAN_NO, L.ACC ACC, L.[DATE] [DATE], L.LOAN_AMOUNT LOAN_AMOUNT, L.INT_RATE INT_RATE, ISNULL(L.NOTE, '') NOTE FROM Loan L WHERE (L.LOAN_AMOUNT + ISNULL((SELECT SUM(ISNULL(AMOUNT, 0)) FROM LOAN_INTEREST WHERE TRIM(LOAN_NO) = TRIM(L.LOAN_NO)),0)- ISNULL((SELECT SUM(ISNULL(AMOUNT, 0)) FROM LOAN_PAYMENTS WHERE TRIM(LOAN_NO) = TRIM(L.LOAN_NO)),0)) >0 and (l.ACC='{_acc}' or '{_acc}'='')  ORDER BY CAST(TRIM(L.LOAN_NO) AS INT)");
        }
        public DataRow GetLoanBalanceAmount(string LoanNo)
        {
            return _helperCommonService.GetSqlRow("SELECT L.LOAN_AMOUNT + ISNULL((SELECT SUM(ISNULL(AMOUNT, 0)) FROM LOAN_INTEREST WHERE TRIM(LOAN_NO) = TRIM(L.LOAN_NO)), 0) - ISNULL((SELECT SUM(ISNULL(AMOUNT, 0)) FROM LOAN_PAYMENTS WHERE TRIM(LOAN_NO) = TRIM(L.LOAN_NO)),0) BAL_AMOUNT FROM Loan L WHERE TRIM(L.LOAN_NO) = TRIM(@loanNo)", "@loanNo", LoanNo);
        }
        public DataTable GetPreviousRecords(string LoanNo, bool isPayment = false, bool isPenalty = false)
        {
            if (isPayment)
                return _helperCommonService.GetSqlData("SELECT [DATE], AMOUNT, TRANS_ID FROM LOAN_PAYMENTS WHERE TRIM(LOAN_NO) = TRIM(@loanNo) ORDER BY [DATE], TRANS_ID", "@loanNo", LoanNo);
            else if (isPenalty)
                return _helperCommonService.GetSqlData("SELECT [DATE], AMOUNT FROM LOAN_INTEREST WHERE TRIM(LOAN_NO) = TRIM(@loanNo) AND ISNULL(IS_PENALTY,0)=1 ORDER BY [DATE]", "@loanNo", LoanNo);
            else
                return _helperCommonService.GetSqlData("SELECT [DATE], AMOUNT FROM LOAN_INTEREST WHERE TRIM(LOAN_NO) = TRIM(@loanNo) AND ISNULL(IS_PENALTY,0)=0 ORDER BY [DATE]", "@loanNo", LoanNo);

        }
        public bool DeleteLoanPaymentRecord(string LoanNo, int TransID)
        {
            DataTable dataTable = new DataTable();
            using (SqlDataAdapter SqlDataAdapter = new SqlDataAdapter())
            {
                SqlDataAdapter.SelectCommand = new SqlCommand();
                SqlDataAdapter.SelectCommand.Connection = _connectionProvider.GetConnection();
                SqlDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter.SelectCommand.CommandText = "DeleteLoanPaymentRecord";

                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@LOAN_NO", LoanNo);
                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@TRANS_ID", TransID);

                SqlDataAdapter.SelectCommand.Connection.Open();
                var rowsAffected = SqlDataAdapter.SelectCommand.ExecuteNonQuery();
                SqlDataAdapter.SelectCommand.Connection.Close();

                return rowsAffected > 0;
            }
        }
        public DataRow GetLastInterestAppliedDate(string LoanNo)
        {
            return _helperCommonService.GetSqlRow("Select max(date) mDate from loan_interest where ltrim(rtrim(loan_no))=ltrim(rtrim(@LoanNo))", "@LoanNo", LoanNo);
        }

        public DataTable GetLoanInterestRecords()
        {
            return _helperCommonService.GetSqlData("SELECT LOAN_NO, [DATE] DATE, [DATE] TIME, AMOUNT, NOTE, Trans_ID FROM LOAN_INTEREST WHERE ISNULL(IS_PENALTY,0)=0 AND ISNULL([NOTE],'')<>'' ORDER BY [DATE]");
        }
        public bool DeleteLoanInterestRecord(string LoanNo, int TransID)
        {
            DataTable dataTable = new DataTable();
            using (SqlDataAdapter SqlDataAdapter = new SqlDataAdapter())
            {
                SqlDataAdapter.SelectCommand = new SqlCommand();
                SqlDataAdapter.SelectCommand.Connection = _connectionProvider.GetConnection();
                SqlDataAdapter.SelectCommand.CommandType = CommandType.Text;
                SqlDataAdapter.SelectCommand.CommandText = "DELETE FROM GLPOST WHERE TRIM(Transact) IN(SELECT TOP 2 TRIM(Transact) FROM GLPost WHERE Trimmed_inv_no=TRIM(@LOAN_NO) AND [TYPE]= 'L' AND NOTE = 'Loan Interest') AND [TYPE] = 'L' AND NOTE = 'Loan Interest' AND LOG_NO=(SELECT ISNULL(GLPost_LogNo,'') FROM LOAN_INTEREST WHERE TRIM(LOAN_NO) = TRIM(@LOAN_NO) AND TRANS_ID=@TRANS_ID AND ISNULL(GLPost_LogNo,'')<>'');DELETE FROM LOAN_INTEREST WHERE TRIM(LOAN_NO) = TRIM(@LOAN_NO) AND TRANS_ID=@TRANS_ID";
                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@LOAN_NO", LoanNo);
                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@TRANS_ID", TransID);

                SqlDataAdapter.SelectCommand.Connection.Open();
                var rowsAffected = SqlDataAdapter.SelectCommand.ExecuteNonQuery();
                SqlDataAdapter.SelectCommand.Connection.Close();
                return rowsAffected > 0;
            }
        }
    }
}
