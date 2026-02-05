// Sravan 12/17/2025 Create new 
// Sravan 12/24/2025 Added new GL_CODE_AMOUNT() this call from unapplychecks for gl records also added few more parameter its use in controller 
// Sravan 01/02/2026 msg was commet its not use for this Missing

using Microsoft.Data.SqlClient;
using System.Data;
using System.Text;
using YJWebCoreMVC.Models;

namespace YJWebCoreMVC.Services
{
    public class CheckOneVendorService
    {

        private readonly ConnectionProvider _connectionProvider;
        private readonly HelperCommonService _helperCommonService;
        private readonly HelperSravanService _helperSravanService;

        public CheckOneVendorService(ConnectionProvider connectionProvider, HelperCommonService helperCommonService, HelperSravanService helperSravanService, IWebHostEnvironment env)
        {
            _connectionProvider = connectionProvider;
            _helperCommonService = helperCommonService;
            _helperSravanService = helperSravanService;
        }


        public DataTable GetVendorCheckIssueData(string vendorcode, string Isissuecheck = "", string storeno = "", bool paybygold = false)
        {
            return _helperCommonService.GetStoreProc("GetVendorCheckIssueData", "@vendorcode", vendorcode, "@Isissuecheck", Isissuecheck.ToString(), "@storeno", storeno, "@paybygold", paybygold ? "1" : "0");
        }
        public DataTable GL_CODE_AMOUNT(string strChequeNo, string strBank, out string GL_CODE, out decimal AMOUNT)
        {
            DataTable dataTable = new DataTable();
            GL_CODE = string.Empty;
            AMOUNT = 0;

            using (SqlConnection connection = _connectionProvider.GetConnection())
            using (SqlCommand command = new SqlCommand("GET_GL_CODE_AMOUT", connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                // Input parameters
                command.Parameters.AddWithValue("@CHEQUENO", strChequeNo);
                command.Parameters.AddWithValue("@BANK", strBank);

                // Output parameters
                SqlParameter glCodeParam = new SqlParameter("@GLCODE", SqlDbType.VarChar, 30)
                {
                    Direction = ParameterDirection.Output
                };
                command.Parameters.Add(glCodeParam);

                SqlParameter amountParam = new SqlParameter("@AMOUNT", SqlDbType.Decimal)
                {
                    Direction = ParameterDirection.Output
                };
                command.Parameters.Add(amountParam);
                connection.Open();
                command.ExecuteNonQuery();
                GL_CODE = glCodeParam.Value != DBNull.Value ? glCodeParam.Value.ToString() : string.Empty;
                AMOUNT = amountParam.Value != DBNull.Value ? Convert.ToDecimal(amountParam.Value) : 0;
                using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                {
                    adapter.Fill(dataTable);
                }
            }
            return dataTable;
        }

        // -- Commented at YJCore Migration
        /*public void refreshButton2_Click(out StringBuilder sbStrReason1, out StringBuilder excludeCheck, out StringBuilder excludeBank)
        {
            try
            {
                sbStrReason1 = new StringBuilder(); excludeCheck = new StringBuilder(); excludeBank = new StringBuilder();
                if (string.IsNullOrWhiteSpace(Checkno) || string.IsNullOrWhiteSpace(banks))
                {
                    return;
                }
                DataTable dtCheck = _helperSravanService.CheckForExistingBankCheck(Checkno, banks);
                if (!_helperCommonService.DataTableOK(dtCheck))
                {
                    sbStrReason1.Append("\nBank : " + banks.Trim().PadRight(10) + "Check# : " + Checkno);
                    excludeCheck.Append("'" + Checkno.ToString().Trim() + "',");
                    excludeBank.Append("'" + banks.ToString().Trim() + "',");
                    return;
                }
                VendorCode = _helperCommonService.CheckForDBNull(dtCheck.Rows[0]["ACC"]);
                EnteredCheckAmt = Convert.ToDecimal(_helperCommonService.CheckForDBNull(dtCheck.Rows[0]["AMOUNT"], typeof(decimal)));
                txtBalChkAmt = EnteredCheckAmt; // Update the balance check amount field
                CheckDate = Convert.ToDateTime(_helperCommonService.CheckForDBNull(dtCheck.Rows[0]["DATE"], typeof(DateTime)));

                dtAPCredit = _helperSravanService.GetVendorCheckIssueData(VendorCode);
                string GL_CODE = "";
                decimal AMOUNT = 0;


                try
                {
                    decimal checkamt = EnteredCheckAmt;
                    if (_helperCommonService.DataTableOK(dtAPCredit))
                    {
                        foreach (DataRow dr in dtAPCredit.Rows)
                        {
                            decimal balance = _helperCommonService.CheckForDBNull(dr["Balance"].ToString(), typeof(decimal).FullName);
                            decimal payment = _helperCommonService.CheckForDBNull(dr["payment"].ToString(), typeof(decimal).FullName);
                            if (checkamt > 0)
                            {
                                if (checkamt >= balance)
                                {
                                    dr["payment"] = balance;
                                    checkamt -= balance;
                                }
                                else
                                {
                                    dr["payment"] = checkamt;
                                    checkamt -= checkamt;
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error in Update Balc and Payments : " + ex.Message, ex);
                }

                DataTable dtGLAccts = _helperCommonService.GL_CODE_AMOUNT(Checkno, banks, out GL_CODE, out AMOUNT);

                //if (string.IsNullOrEmpty(GL_CODE) || AMOUNT == 0)
                //{
                //    sbStrReason1.Append("\nMissing GL code or amount for the provided check.");
                //    return;
                //}


            }
            catch (Exception ex)
            {

                throw new Exception("Error in refreshButton2_Click: " + ex.Message, ex);
            }
        }*/
    }
}
