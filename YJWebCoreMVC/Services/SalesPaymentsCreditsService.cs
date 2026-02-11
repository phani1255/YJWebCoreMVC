/*
 *  Created By Phanindra
 *  Phanindra 11/12/2024 - Added GetCustomerofReceipt, ModifyCustomerCodeofReceipt, CheckOKReceiptToModifyCustomer, CheckValidCustomerCode
 *  Phanindra 11/18/2024 Added GetPaymentCommison, DeleteCashReceipt method.
 *  Phanindra 11/28/2024 Added SaveCredit, GetCreditData,GetCredit, GetPayment, DeleteCredit
 *  Phanindra 12/09/2024 Worked on fixing issues with ModifyACC, Reprint ,Add and Edit Cash receipt methods.
 *  Phanindra 12/11/2024 Added CheckCreditAdj1 method
 *  Phanindra 12/16/2024 Added SaveAdjRcvable, EditAdjRcvable, GetPayItems, DeleteAdjRcvable
 *  Phanindra 24-Apr-2025 Added custRef prameter in SaveCredit method
 *  Phanindra 06/09/2025 Worked on fixing save issue for customer refund.
 *  Phanindra 06/23/2025 Worked on adding the Register value
 */
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using System.Data;

namespace YJWebCoreMVC.Services
{
    public class SalesPaymentsCreditsService
    {

        private readonly ConnectionProvider _connectionProvider;
        private readonly HelperCommonService _helperCommonService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SalesPaymentsCreditsService(ConnectionProvider connectionProvider, IHttpContextAccessor httpContextAccessor, HelperCommonService helperCommonService)
        {
            _connectionProvider = connectionProvider;
            _helperCommonService = helperCommonService;
            _httpContextAccessor = httpContextAccessor;
        }

        public string SaveCashReceipt(String dtPayment, string acc, string receipt_no, DateTime? pay_date, DateTime? chk_date, string bank, string checkno, decimal chk_amt, decimal discount, bool showmemo, string pcname, string PaymentsTypes, string PaymentNote, string Cash_Register, string StoreCode, string loggeduser = "", string storecodeinuse = "", bool isRefund = false)
        {
            var rowsAffected = 0;
            //error = string.Empty;
            using (SqlDataAdapter dataAdapter = new SqlDataAdapter())
            {
                try
                {
                    pcname = Environment.MachineName;
                    // Create the command and set its properties
                    dataAdapter.SelectCommand = new SqlCommand();
                    dataAdapter.SelectCommand.Connection = _connectionProvider.GetConnection();

                    dataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                    dataAdapter.SelectCommand.CommandText = "SaveReceipt";

                    dataAdapter.SelectCommand.Parameters.AddWithValue("@acc", acc ??= "");
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@receipt_no", receipt_no ??= "");
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@pay_date", pay_date);
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@chk_date", chk_date);
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@bank", bank ??= "");
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@checkno", checkno ??= "");
                    dataAdapter.SelectCommand.Parameters.Add(new SqlParameter("@chk_amt", SqlDbType.Decimal)
                    {
                        Precision = 18,
                        Scale = 2,
                        Value = chk_amt
                    });
                    dataAdapter.SelectCommand.Parameters.Add(new SqlParameter("@discount", SqlDbType.Decimal)
                    {
                        Precision = 18,
                        Scale = 2,
                        Value = discount
                    });
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@showmemo", showmemo ? 1 : 0);
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@PCNAME", pcname ??= "");

                    dataAdapter.SelectCommand.Parameters.AddWithValue("@PaymentType", PaymentsTypes ??= "");
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@PaymentNote", PaymentNote ??= "");
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@Cash_Register", Cash_Register ??= "");
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@StoreCode", StoreCode ??= "");
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@loggeduser", loggeduser ?? "");
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@storecodeinuse", storecodeinuse ??= "");
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@isRefund", isRefund ? 1 : 0);

                    SqlParameter parameter = new SqlParameter();
                    parameter.ParameterName = "@TBLPAYMENT";
                    parameter.SqlDbType = System.Data.SqlDbType.Xml;
                    parameter.Value = dtPayment;
                    dataAdapter.SelectCommand.Parameters.Add(parameter);

                    dataAdapter.SelectCommand.CommandTimeout = 0;
                    dataAdapter.SelectCommand.Connection.Open();
                    rowsAffected = dataAdapter.SelectCommand.ExecuteNonQuery();
                    dataAdapter.SelectCommand.Connection.Close();

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                if (rowsAffected > 0)
                {
                    _httpContextAccessor.HttpContext?.Session.SetString("ReceiptNo", "");
                    return "Success";
                }
                else
                {
                    return "Fail";
                }
            }
        }

        public DataRow GetPayment(string inv_no)
        {
            return _helperCommonService.GetSqlRow("select * from payments where rtv_pay = 'P' and trim(inv_no) = trim(@inv_no)", "@inv_no", inv_no);
        }

        public DataTable GetCustomerofReceipt(string recno)
        {
            return _helperCommonService.GetSqlData("SELECT * FROM PAYMENTS WHERE TRIM(INV_NO) = @rec_no", "@rec_no", recno.Trim());
        }

        public string ModifyCustomerCodeofReceipt(string recno, string OldAcc, string NewAcc)
        {
            int rowsAffected = 0;

            using (SqlDataAdapter dataAdapter = new SqlDataAdapter())
            {
                try
                {
                    dataAdapter.SelectCommand = new SqlCommand
                    {
                        Connection = _connectionProvider.GetConnection(),
                        CommandType = CommandType.Text,
                        CommandText = @"UPDATE PAYMENTS SET ACC = @NEW_ACC WHERE INV_NO = @REC_NO AND ACC = @OLD_ACC"
                    };

                    dataAdapter.SelectCommand.Parameters.AddWithValue("@REC_NO", recno);
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@OLD_ACC", OldAcc);
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@NEW_ACC", NewAcc);

                    dataAdapter.SelectCommand.Connection.Open();
                    rowsAffected = dataAdapter.SelectCommand.ExecuteNonQuery();
                    dataAdapter.SelectCommand.Connection.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                if (rowsAffected > 0)
                {
                    return "Success";
                }
                else
                {
                    return "Fail";
                }
            }

        }
        public DataTable CheckOKReceiptToModifyCustomer(string recno)
        {
            return _helperCommonService.GetSqlData("SELECT * FROM PAYMENTS WHERE TRIM(inv_no) NOT IN(SELECT TRIM(Pay_no) FROM Pay_item WHERE LTRIM(RTRIM(INV_NO)) = @rec_no) AND LTRIM(RTRIM(INV_NO)) = @rec_no", "@rec_no", recno.Trim());
        }

        public DataRow CheckValidCustomerCode(string acc, bool iSWrist = false)
        {
            if (iSWrist)
            {
                DataRow rw = _helperCommonService.GetSqlRow("select [NAME2] NAME From Customer Where trim(acc) = trim(@acc) or old_customer = trim(@acc)", "@acc", acc);

                return (rw == null || String.IsNullOrWhiteSpace(Convert.ToString(rw["NAME"])))
                    ? _helperCommonService.GetSqlRow("select * From Customer Where trim(acc) = trim(@acc) or old_customer = trim(@acc)", "@acc", acc)
                    : _helperCommonService.GetSqlRow("select [NAME2] NAME, [ADDR2] ADDR1, [ADDR22] ADDR12, [CITY2] CITY1, [STATE2] STATE1, [ZIP2] ZIP1, ADDR13, [COUNTRY2] COUNTRY, [TEL2] TEL, * From Customer Where trim(acc) = trim(@acc) or old_customer = trim(@acc)", "@acc", acc);
            }
            return _helperCommonService.GetSqlRow("select * From Customer Where trim(acc) = trim(@acc) or old_customer = trim(@acc)", "@acc", acc);

        }

        public DataRow GetPaymentCommison(string inv_no)
        {
            return _helperCommonService.GetSqlRow("select * from comision where inv_no = @inv_no and isnull(paid_ref,'') <> ''", "@inv_no", string.Format("P{0}", inv_no));
        }

        public string DeleteCashReceipt(string acc, string receipt_no, decimal paid, string transact)
        {

            try
            {
                using (SqlCommand dbCommand = new SqlCommand())
                {
                    // Set the command object properties
                    dbCommand.Connection = _connectionProvider.GetConnection();
                    dbCommand.CommandType = CommandType.StoredProcedure;
                    dbCommand.CommandText = "DeleteReceipt";

                    dbCommand.Parameters.AddWithValue("@acc", acc);
                    dbCommand.Parameters.AddWithValue("@receipt_no", receipt_no);
                    dbCommand.Parameters.AddWithValue("@paid", paid);
                    dbCommand.Parameters.AddWithValue("@transact", transact);

                    dbCommand.CommandTimeout = 0;
                    dbCommand.Connection.Open();
                    var rowsAffected = dbCommand.ExecuteNonQuery();
                    dbCommand.Connection.Close();

                    if (rowsAffected > 0)
                    {
                        return "Receipt Deleted Successfully";
                    }
                    else
                    {
                        return "Error in deleting receipt";
                    }

                }
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return error;
            }
        }

        public string SaveCredit(DataTable dtCredit, string txtBillAcc, string txtCreditNo, DateTime? txtDate, DateTime? txtRefDate, string txtNote, string txtAmount, string cred_code, bool chkShowMemo, string ddlReason, string StoreCodeInUse = "", string LoggedUser = "", string custRef = "")
        {
            int rowsAffected = 0;

            using (SqlDataAdapter dataAdapter = new SqlDataAdapter())
            {
                try
                {
                    dataAdapter.SelectCommand = new SqlCommand();
                    dataAdapter.SelectCommand.Connection = _connectionProvider.GetConnection();

                    dataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                    dataAdapter.SelectCommand.CommandText = "SaveCredit";

                    dataAdapter.SelectCommand.Parameters.AddWithValue("@acc", txtBillAcc ??= "");
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@credit_no", txtCreditNo ??= "");
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@date", txtDate);
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@rdate", txtRefDate);
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@note", txtNote ??= "");
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@amt", Convert.ToDecimal(txtAmount));
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@cred_code", cred_code ??= "");
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@showmemo", chkShowMemo);
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@storecodeinuse", StoreCodeInUse);
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@reason", ddlReason ??= "");
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@loggeduser", LoggedUser);
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@custRef", custRef ??= "");

                    SqlParameter parameter = new SqlParameter();
                    parameter.ParameterName = "@TBLPAYMENT";
                    parameter.SqlDbType = System.Data.SqlDbType.Structured;
                    parameter.Value = dtCredit;
                    dataAdapter.SelectCommand.Parameters.Add(parameter);

                    dataAdapter.SelectCommand.CommandTimeout = 0;

                    dataAdapter.SelectCommand.Connection.Open();
                    rowsAffected = dataAdapter.SelectCommand.ExecuteNonQuery();
                    dataAdapter.SelectCommand.Connection.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return "Error: " + ex.Message;
                }

                return rowsAffected > 0 ? "Success" : "Fail";
            }
        }

        public DataTable GetCreditData(string acc, string credit_no)
        {
            return _helperCommonService.GetStoreProc("GetCreditData", "@acc", acc, "@credit_no", credit_no);
        }
        public DataRow GetCredit(string inv_no)
        {
            return _helperCommonService.GetSqlRow("select * from credits where inv_no = @inv_no", "@inv_no", inv_no);
        }
        public DataRow GetPayment(string inv_no, string rtv_pay)
        {
            return _helperCommonService.GetSqlRow("select * from payments where rtv_pay = @rtv_pay and ltrim(Rtrim(inv_no)) = ltrim(Rtrim(@inv_no))", "@inv_no", inv_no, "@rtv_pay", rtv_pay);
        }
        public bool DeleteCredit(string acc, string creditNo, decimal paid, out string error)
        {
            try
            {
                error = string.Empty;
                using (SqlCommand dbCommand = new SqlCommand())
                {
                    dbCommand.Connection = _connectionProvider.GetConnection();
                    dbCommand.CommandType = CommandType.StoredProcedure;
                    dbCommand.CommandText = "DeleteCredit";
                    dbCommand.Parameters.AddWithValue("@acc", acc);
                    dbCommand.Parameters.AddWithValue("@credit_no", creditNo);
                    dbCommand.Parameters.AddWithValue("@paid", paid);
                    dbCommand.Connection.Open();
                    var rowsAffected = dbCommand.ExecuteNonQuery();
                    dbCommand.Connection.Close();

                    return rowsAffected > 0;
                }
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return false;
            }
        }
        // Added on 05-Dec-2024
        public DataTable GetCreditPayment(string inv_no, string rtv_pay, bool isApCredit)
        {
            if (isApCredit)
                return _helperCommonService.GetSqlData("select  'C' as RTV_PAY, '' AS PAY_NO, INV_NO, AMOUNT,MESSAGE AS NOTE  from APCREDIT WHERE INV_NO=@INV_NO order by inv_no",
                    "@inv_no", inv_no, "@rtv_pay", rtv_pay);
            return _helperCommonService.GetSqlData("select * from pay_item where pay_no in (select pay_no from pay_item where rtv_pay = @rtv_pay and inv_no = @inv_no) order by rtv_pay,inv_no",
                 "@inv_no", inv_no, "@rtv_pay", rtv_pay);
        }

        public DataTable GetCreditDetails(string inv_no, bool IsApCredit)
        {
            DataTable dataTable = new DataTable();

            using (SqlDataAdapter SqlDataAdapter = new SqlDataAdapter())
            {
                // Create the command and set its properties
                SqlDataAdapter.SelectCommand = new SqlCommand();
                SqlDataAdapter.SelectCommand.Connection = _connectionProvider.GetConnection();
                SqlDataAdapter.SelectCommand.CommandType = CommandType.Text;

                // Assign the SQL to the command object
                if (IsApCredit)
                    SqlDataAdapter.SelectCommand.CommandText = "SELECT APCREDIT.INV_NO,APCREDIT.ACC,APCREDIT.VND_NO,APCREDIT.DATE,APCREDIT.DATE as REF_DATE,APCREDIT.DATE as chk_date,APCREDIT.AMOUNT,'' as CRED_CODE,APCREDIT.NOTE1 AS NOTE,0 AS SHOWMEMO, VENDORS.name, VENDORS.ADDR11 as addr1, VENDORS.addr12, VENDORS.city1, VENDORS.state1,VENDORS.zip1, VENDORS.country, '' as check_no, APCREDIT.date,APCREDIT.gl_code from APCREDIT, VENDORS  WHERE APCREDIT.inv_no= @inv_no AND APCREDIT.acc= VENDORS.acc ";

                else
                    SqlDataAdapter.SelectCommand.CommandText = "SELECT credits.*, customer.name, customer.addr1, customer.addr12, customer.city1, customer.state1,customer.zip1, customer.country, payments.check_no, payments.chk_date from credits, payments, customer  WHERE credits.inv_no= @inv_no AND credits.acc= customer.acc AND credits.inv_no = payments.inv_no  and rtv_pay = 'C'";

                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@inv_no", inv_no);

                // Fill the table from adapter
                SqlDataAdapter.Fill(dataTable);
            }
            if (dataTable.Rows.Count > 0)
                return dataTable;
            else
                return null;
        }

        public List<SelectListItem> GetDefaultBank()
        {
            DataTable dataTable = new DataTable();

            using (SqlDataAdapter SqlDataAdapter = new SqlDataAdapter())
            {
                // Create the command and set its properties
                SqlDataAdapter.SelectCommand = new SqlCommand();
                SqlDataAdapter.SelectCommand.Connection = _connectionProvider.GetConnection();
                SqlDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;

                // Assign the SQL to the command object
                SqlDataAdapter.SelectCommand.CommandText = @"getdefaultbank";

                // Fill the table from adapter
                SqlDataAdapter.Fill(dataTable);
            }

            List<SelectListItem> bankCodesList = new List<SelectListItem>();
            bankCodesList.Add(new SelectListItem() { Text = "All", Value = "" });
            if (dataTable.Rows.Count > 0)
            {
                foreach (DataRow dr in dataTable.Rows)
                {
                    bankCodesList.Add(new SelectListItem() { Text = dr["CODE"].ToString().Trim(), Value = dr["CODE"].ToString().Trim() });
                }
            }
            return bankCodesList;
        }

        public DataTable CheckCreditAdj1(string creditno, string iscreditordebit)
        {
            DataTable dataTable = new DataTable();

            using (SqlDataAdapter dataAdapter = new SqlDataAdapter())
            {
                // Create the command and set its properties
                dataAdapter.SelectCommand = new SqlCommand();
                dataAdapter.SelectCommand.Connection = _connectionProvider.GetConnection();
                dataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                dataAdapter.SelectCommand.CommandTimeout = 3000;
                dataAdapter.SelectCommand.CommandText = "CheckCreditAdj";
                dataAdapter.SelectCommand.Parameters.AddWithValue("@creditno", creditno);
                dataAdapter.SelectCommand.Parameters.AddWithValue("@iscreditordebit", iscreditordebit);

                // Fill the datatable From adapter
                dataAdapter.Fill(dataTable);
                return dataTable;
            }
        }
        public bool SaveAdjRcvable(DataTable dtPayment, string acc, string adj_no, DateTime? entry_date, bool showmemo)
        {
            try
            {
                using (SqlCommand dbCommand = new SqlCommand())
                {
                    dbCommand.Connection = _connectionProvider.GetConnection();
                    dbCommand.CommandType = CommandType.StoredProcedure;
                    dbCommand.CommandText = "SaveAdjRcvable";

                    SqlParameter parameter = new SqlParameter();
                    parameter.ParameterName = "@TBLPAYMENT";
                    parameter.SqlDbType = System.Data.SqlDbType.Structured;
                    parameter.Value = dtPayment;
                    dbCommand.Parameters.Add(parameter);

                    dbCommand.Parameters.AddWithValue("@acc", acc);
                    dbCommand.Parameters.AddWithValue("@adj_no", adj_no);
                    dbCommand.Parameters.AddWithValue("@entry_date", entry_date);
                    dbCommand.Parameters.AddWithValue("@showmemo", showmemo);

                    dbCommand.CommandTimeout = 0;
                    dbCommand.Connection.Open();
                    var rowsAffected = dbCommand.ExecuteNonQuery();
                    dbCommand.Connection.Close();

                    return rowsAffected > 0;
                }
            }
            catch (Exception)
            {
                return false;
            }

        }

        public bool EditAdjRcvable(string dtPayment, string acc, string adj_no, DateTime? entry_date)
        {
            try
            {
                using (SqlCommand dbCommand = new SqlCommand())
                {
                    // Set the command object properties
                    dbCommand.Connection = _connectionProvider.GetConnection();
                    dbCommand.CommandType = CommandType.StoredProcedure;
                    dbCommand.CommandText = "EditAdjRcvable";

                    dbCommand.Parameters.AddWithValue("@TBLEDITRECEIPT", dtPayment);
                    dbCommand.Parameters.AddWithValue("@acc", acc ??= "");
                    dbCommand.Parameters.AddWithValue("@ADJ_NO", adj_no ??= "");
                    dbCommand.Parameters.AddWithValue("@entry_date", entry_date);

                    dbCommand.CommandTimeout = 0;
                    dbCommand.Connection.Open();
                    var rowsAffected = dbCommand.ExecuteNonQuery();
                    dbCommand.Connection.Close();

                    return rowsAffected > 0;
                }
            }
            catch (Exception)
            {
                return false;
            }

        }

        public DataTable GetPayItems(string pay_no, bool isPrintRcpt = false)
        {
            if (isPrintRcpt)
                return _helperCommonService.GetSqlData("select a.*,isnull(b.CURR_TYPE,'')CURR_TYPE,isnull(B.CURR_RATE,0.0000)CURR_RATE,isnull(B.CURR_AMOUNT,0.00)CURR_AMOUNT from pay_item a left join payments b   on a.INV_NO=b.inv_no and a.RTV_PAY=b.RTV_PAY  where ltrim(rtrim(a.pay_no)) =ltrim(rtrim(@pay_no))", "@pay_no", pay_no);
            return _helperCommonService.GetSqlData("select * from pay_item where pay_no =@pay_no", "@pay_no", pay_no);
        }

        public bool DeleteAdjRcvable(string acc, string receipt_no, decimal paid, string transact)
        {
            try
            {
                using (SqlCommand dbCommand = new SqlCommand())
                {
                    dbCommand.Connection = _connectionProvider.GetConnection();
                    dbCommand.CommandType = CommandType.StoredProcedure;
                    dbCommand.CommandText = "DeleteAdjRcvable";

                    dbCommand.Parameters.AddWithValue("@acc", acc);
                    dbCommand.Parameters.AddWithValue("@receipt_no", receipt_no);
                    dbCommand.Parameters.AddWithValue("@paid", paid);
                    dbCommand.Parameters.AddWithValue("@transact", transact);

                    dbCommand.CommandTimeout = 0;
                    dbCommand.Connection.Open();
                    var rowsAffected = dbCommand.ExecuteNonQuery();
                    dbCommand.Connection.Close();

                    return rowsAffected > 0;
                }
            }
            catch (Exception)
            {
                return false;
            }

        }
    }
}
