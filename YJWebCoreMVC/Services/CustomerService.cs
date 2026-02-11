/*
 * Hemanth 08/29/2024 created new model
 * hemanth 08/30/2024 function naming chaged due to violation issue
 * Hemanth 08/31/2024 removed unnessasary calls and show common details from helper
 * Chakri   04/10/2025 Added saveFollowTypes method.
 * Chakri   04/11/2025 Added GetAllSalesman method.
 * chakri   04/22/2025 Added GetReferralLoyaltyPoints.
 * chakri   04/28/2025 Added CheckValidOrderRepair, DelRepairOrder.
 * chakri   04/30/2025 Added GetInvoiceNumberBasedOnRepairNumber, GetInvoiceHeaderInformatioBasedOnInvoiceNumber and DeleteInvoice.
 * chakri   05/02/2025 Added DelScrapGold method.
 * chakri   05/05/2025 Added GetCashoutItems method.
 * chakri   05/06/2025 Added GetProformaInvoice and DeleteProformaInvoice.
 * chakri   05/07/2025 Added CheckValidCustomerCode and DeleteCustomer methods.
 * chakri   05/07/2025 Added GetSalesmancode and DeleteSalesmanacc methods.
 * chakri   05/09/2025 Added Try, catch in DeleteCustomer method.
 * chakri   05/12/2025 Added GetMemoByInvNo and DeleteMemo methods.
 * chakri   05/13/2025 Added GetRtvHeadInfo,DeleteRtv and CheckPayItems methods.
 * chakri   05/14/2025 Added Try, Catch in DeleteMemo method.
 * chakri   05/15/2025 Added GetPotentialCustomerByAcc and GetLotInfo, DelALotNO, iSUsedImagesToOthereStyle methods.
 * chakri   05/20/2025 Added CheckValidStoreCreditorGiftCard, CancelStoreCreditorGiftCard, GetAllSroreCredits methods.
 * chakri   05/20/2025 Added CheckValidOrder and DeleteCustPO methods.
 * chakri   05/20/2025 Added CheckValidBill, GetVendorNameByCode and DeleteBill methods.
 * chakri   05/21/2025 Added  Try, Catch in DeleteBill method.
 * chakri   05/22/2025 Added CheckStyleInstockqty method.
 * Phanindra 06/04/2025 Added CheckValidBillingAcct method
 * Phanindra 08/26/2025 Added UpdateCustomerRecord method
 * Hemanth   10/03/2025 Added GetEmail method
 * Manoj     02/06/2025 Added CheckValidBill method
 * Dharani  02/09/026 Added AddOrEditSalesman, GetSalesmancode methods.
 * Phanindra 02/11/2026 Added missing methods from yjweb customermodel.cs file.
 */

using Microsoft.Data.SqlClient;
using System.Data;
using YJWebCoreMVC.Models;

namespace YJWebCoreMVC.Services
{
    public class CustomerService
    {
        private readonly ConnectionProvider _connectionProvider;
        private readonly HelperService _helperService;
        private string _CustErrormsg;

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

        //manoj 

        public DataTable CheckValidBill(string inv_no, bool ismulticurr = false)
        {
            return _helperService.HelperCommon.GetStoreProc("PRINTBILL", "@inv_no", inv_no.Trim().PadLeft(6, ' '), "@ismulticurr", ismulticurr.ToString());
        }
        public bool AddOrEditSalesman(string scode, string uname, string addr1, string addr2, string addr3, string ph1, string ph2, string ph3, string notes1, string notes2, string notes3, string email, string commission, bool incativeornot = false, string sfContact = "")
        {
            using (SqlConnection connection = _connectionProvider.GetConnection())
            using (SqlCommand dbCommand = new SqlCommand("AddOrEditSalesMan", connection))
            {
                dbCommand.CommandType = CommandType.StoredProcedure;

                // Add parameters with explicit types
                dbCommand.Parameters.AddWithValue("@SCODE", scode ?? (object)DBNull.Value);
                dbCommand.Parameters.AddWithValue("@UNAME", uname ?? (object)DBNull.Value);
                dbCommand.Parameters.AddWithValue("@ADDR1", addr1 ?? (object)DBNull.Value);
                dbCommand.Parameters.AddWithValue("@ADDR2", addr2 ?? (object)DBNull.Value);
                dbCommand.Parameters.AddWithValue("@ADDR3", addr3 ?? (object)DBNull.Value);
                dbCommand.Parameters.AddWithValue("@PHONE1", ph1 ?? (object)DBNull.Value);
                dbCommand.Parameters.AddWithValue("@PHONE2", ph2 ?? (object)DBNull.Value);
                dbCommand.Parameters.AddWithValue("@PHONE3", ph3 ?? (object)DBNull.Value);
                dbCommand.Parameters.AddWithValue("@NOTES1", notes1 ?? (object)DBNull.Value);
                dbCommand.Parameters.AddWithValue("@NOTES2", notes2 ?? (object)DBNull.Value);
                dbCommand.Parameters.AddWithValue("@NOTES3", notes3 ?? (object)DBNull.Value);
                dbCommand.Parameters.AddWithValue("@EMAIL", email ?? (object)DBNull.Value);
                dbCommand.Parameters.AddWithValue("@sfContact", sfContact ?? (object)DBNull.Value);
                dbCommand.Parameters.AddWithValue("@incativeornot", incativeornot);


                decimal commissionValue;
                if (string.IsNullOrWhiteSpace(commission) || !decimal.TryParse(commission, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out commissionValue))
                {
                    commissionValue = 0;
                }


                if (commissionValue < -99999999.99m || commissionValue > 99999999.99m)
                {
                    throw new ArgumentException("Commission value exceeds the allowed range for DECIMAL(10,2).");
                }

                SqlParameter commissionParam = new SqlParameter("@COMMISSION", SqlDbType.Decimal)
                {
                    Value = commissionValue,
                    Precision = 10,
                    Scale = 2
                };
                dbCommand.Parameters.Add(commissionParam);


                connection.Open();
                int rowsAffected = dbCommand.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }


        public DataRow GetSalesmancode(string lcode)
        {
            return _helperService.HelperCommon.GetSqlRow(@"SELECT * FROM SALESMEN WHERE CODE = @CODE", "@CODE", lcode);
        }

        // -- Added methods from CustomerService in yjweb

        public DataTable GetListOfInactiveCustomers(string Fromdate, string Todate, string Salesman)
        {
            DataTable dataTable = new DataTable();


            using (SqlDataAdapter dataAdapter = new SqlDataAdapter())
            {
                try
                {
                    // Create the command and set its properties
                    dataAdapter.SelectCommand = new SqlCommand();
                    dataAdapter.SelectCommand.Connection = _connectionProvider.GetConnection();

                    dataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                    dataAdapter.SelectCommand.CommandText = "INACTIVECUSTOMERS";

                    dataAdapter.SelectCommand.Parameters.AddWithValue("@FROMDATE", Fromdate);
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@TODATE", Todate);
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@SALESMANCODE", Salesman);
                    // Fill the datatable From adapter
                    dataAdapter.Fill(dataTable);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }


                return dataTable;
            }
        }


        public DataTable GetFollowupIUD(string Ftype1 = "", string OldType = "", string Optionname = "")
        {
            DataTable dataTable = new DataTable();


            using (SqlDataAdapter dataAdapter = new SqlDataAdapter())
            {
                try
                {
                    string Query = Optionname == "EDIT" ? " Update table set columnupate = givevalue where ft=fty" :
                        Optionname == "Insert" ? " insert into Table name() value() " : Optionname == "Delete" ? "delete from table where colum1  value1" : "select * from";
                    dataAdapter.SelectCommand = new SqlCommand();
                    dataAdapter.SelectCommand.Connection = _connectionProvider.GetConnection();

                    dataAdapter.SelectCommand.CommandType = CommandType.Text;
                    dataAdapter.SelectCommand.CommandText = Query;

                    dataAdapter.SelectCommand.Parameters.AddWithValue("@Ftype1", Ftype1);
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@OldType", OldType);

                    // Fill the datatable From adapter
                    dataAdapter.Fill(dataTable);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }


                return dataTable;
            }
        }

        public string saveFollowTypes(string FollowTypesXml)
        {
            using (SqlCommand dbCommand = new SqlCommand())
            {
                // Set the command object properties
                dbCommand.Connection = _connectionProvider.GetConnection();
                dbCommand.CommandType = CommandType.StoredProcedure;
                dbCommand.CommandText = "AddEditFollowUpTypes";
                dbCommand.CommandTimeout = 5000;

                SqlParameter parameter = new SqlParameter();
                parameter.ParameterName = "@INPUTXML";
                parameter.SqlDbType = System.Data.SqlDbType.Xml;
                parameter.Value = FollowTypesXml;
                dbCommand.Parameters.Add(parameter);

                // Open the connection, execute the query and close the connection
                dbCommand.Connection.Open();
                var rowsAffected = dbCommand.ExecuteNonQuery();
                dbCommand.Connection.Close();
                if (rowsAffected > 0)
                {
                    return "Followup Types Updated Successfully";
                }
                else
                {
                    return "fail";
                }
            }
        }
        public DataTable GetAllSalesman()
        {
            return _helperService.HelperCommon.GetSqlData("select NAME , CODE ,TEL, ADDR1, ADDR2, ADDR3, NOTE1 FROM [dbo].[SALESMEN] order by code");
        }


        public DataTable GetReferralLoyaltyPoints(string fromdate, string todate, string acc, bool iSLoyalty = false)
        {
            DataTable dataTable = new DataTable();
            using (SqlDataAdapter SqlDataAdapter = new SqlDataAdapter())
            {
                try
                {
                    SqlDataAdapter.SelectCommand = new SqlCommand();
                    SqlDataAdapter.SelectCommand.Connection = _connectionProvider.GetConnection();
                    SqlDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter.SelectCommand.CommandText = "GetHistoryOfReferralLoyaltyPoints";

                    SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@FromDate", fromdate);
                    SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@ToDate", todate);

                    SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@ACC", acc);
                    SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@iSLoyalty", iSLoyalty);

                    SqlDataAdapter.SelectCommand.Connection.Open();
                    SqlDataAdapter.SelectCommand.ExecuteNonQuery();
                    SqlDataAdapter.SelectCommand.Connection.Close();
                    SqlDataAdapter.Fill(dataTable);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                return dataTable;
            }
        }

        public DataRow CheckValidBillingAcct(string billacc)
        {
            return _helperService.HelperCommon.GetSqlRow("select *  From Customer with (nolock) Where rtrim(bill_acc)= rtrim(@bill_acc) and bill_acc = acc", "@bill_acc", billacc);
        }

        public DataTable CheckValidOrderRepair(string Repair_no)
        {
            Repair_no = Repair_no.Trim();
            return _helperService.HelperCommon.GetSqlData("select * from REPAIR where trim(repair_no) = @Rpno", "@Rpno", Repair_no);
        }

        public string DelRepairOrder(string Repair_no, String Operator = "")
        {
            using (SqlCommand dbCommand = new SqlCommand())
            {
                dbCommand.Connection = _connectionProvider.GetConnection();
                dbCommand.CommandType = CommandType.StoredProcedure;
                dbCommand.CommandText = "DelRepOrder";
                dbCommand.Parameters.AddWithValue("@REPAIRORDER_NO", Repair_no.Trim());
                dbCommand.Parameters.AddWithValue("@Operator", Operator);
                SqlParameter outDelRepStatus = new SqlParameter("@RETVAL", SqlDbType.NVarChar, 100);
                outDelRepStatus.Direction = ParameterDirection.Output;
                dbCommand.Parameters.Add(outDelRepStatus);
                dbCommand.Connection.Open();
                var rowsAffected = dbCommand.ExecuteNonQuery();
                dbCommand.Connection.Close();
                string ABC = outDelRepStatus.Value.ToString();
                return outDelRepStatus.Value.ToString();
            }
        }
        public DataTable GetInvoiceNumberBasedOnRepairNumber(string Rep_no)
        {
            return _helperService.HelperCommon.GetSqlData(@"SELECT top(1) inv_no FROM rep_inv where trim(REP_NO) = trim(@REP_NO)", "@REP_NO", Rep_no);
        }
        public DataTable GetInvoiceHeaderInformatioBasedOnInvoiceNumber(string Inv_no)
        {
            return _helperService.HelperCommon.GetSqlData(@"SELECT * FROM INVOICE WHERE INV_NO = @INV_NO AND v_ctl_no = 'REPAIR'",
                "@INV_NO", Inv_no.PadLeft(6));
        }
        public string DeleteInvoice(string INV_NO, string StoreCode = "", string Cash_Register = "", string Operator = "")
        {
            using (var connection = _connectionProvider.GetConnection())
            {
                connection.Open();
                using (var updateCommand = new SqlCommand("UPDATE_INVOICE", connection))
                {
                    updateCommand.CommandType = CommandType.StoredProcedure;
                    updateCommand.CommandTimeout = 5000;
                    updateCommand.Parameters.Add(new SqlParameter("@INV_NO", SqlDbType.NVarChar) { Value = INV_NO });
                    updateCommand.ExecuteNonQuery();
                }
                using (var deleteCommand = new SqlCommand("DelRepInvoice", connection))
                {
                    deleteCommand.CommandType = CommandType.StoredProcedure;
                    deleteCommand.CommandTimeout = 5000;

                    deleteCommand.Parameters.Add(new SqlParameter("@REPINV", SqlDbType.NVarChar) { Value = INV_NO });
                    deleteCommand.Parameters.Add(new SqlParameter("@StoreCode", SqlDbType.NVarChar) { Value = StoreCode ?? string.Empty });
                    deleteCommand.Parameters.Add(new SqlParameter("@Cash_Register", SqlDbType.NVarChar) { Value = Cash_Register?.Trim() ?? string.Empty });
                    deleteCommand.Parameters.Add(new SqlParameter("@Operator", SqlDbType.NVarChar) { Value = Operator ?? string.Empty });

                    var outDelRepStatus = new SqlParameter("@RETVAL", SqlDbType.NVarChar, 100)
                    {
                        Direction = ParameterDirection.Output
                    };
                    deleteCommand.Parameters.Add(outDelRepStatus);
                    deleteCommand.ExecuteNonQuery();
                    return outDelRepStatus.Value?.ToString() ?? string.Empty;
                }
            }

        }

        public bool DelScrapGold(string invno, bool IsCheckTobeDelete)
        {
            using (SqlCommand dbCommand = new SqlCommand())
            {
                dbCommand.Connection = _connectionProvider.GetConnection();
                dbCommand.CommandType = CommandType.StoredProcedure;
                dbCommand.CommandText = "DELSCRAPGOLD";
                dbCommand.CommandTimeout = 5000;
                dbCommand.Parameters.AddWithValue("@INV_NO", invno);
                dbCommand.Parameters.AddWithValue("@IsCheckTobeDelete", IsCheckTobeDelete);
                dbCommand.Connection.Open();
                var rowsAffected = dbCommand.ExecuteNonQuery();
                dbCommand.Connection.Close();

                return rowsAffected > 0;
            }
        }

        public DataTable GetCashoutItems(string invno, bool isreprint = false)
        {
            using (var connection = _connectionProvider.GetConnection())
            using (var command = new SqlCommand(@"SELECT b.INV_NO, STYLE, [DESC], QTY, PRICE, QTY * PRICE AS TOTAL, invoiceno, bank" + (isreprint ? ", Acc" : "") +
                @" FROM SCRAP A with (nolock)     INNER JOIN SCRAP_ITEMS B  with (nolock) ON A.INV_NO = B.INV_NO 
                  WHERE ISCASHOUT = 1 AND ltrim(Rtrim(B.INV_NO)) = ltrim(rtrim(@INV_NO))", connection))
            {
                command.CommandType = CommandType.Text;
                command.Parameters.AddWithValue("@INV_NO", invno);

                using (var adapter = new SqlDataAdapter(command))
                {
                    var dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    return dataTable;
                }
            }
        }

        public DataRow GetProformaInvoice(string invno)
        {
            return _helperService.HelperCommon.GetSqlRow("SELECT TOP 1 i.*, t.memo_no, t.by_wt FROM Proforma_INVOICE i LEFT JOIN Proforma_IN_ITEMS t ON i.inv_no = t.inv_no WHERE ltrim(Rtrim(i.inv_no))=ltrim(rtrim(@inv_no))", "@inv_no", invno);
        }

        public bool DeleteProformaInvoice(string inv_no, bool is_sfm, string loggeduser, out string error)
        {
            error = string.Empty;
            try
            {
                using (var connection = _connectionProvider.GetConnection())
                using (var command = new SqlCommand("DeleteProformaInvoice", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddRange(new[]
                    {
                       new SqlParameter("@inv_no", inv_no),
                        new SqlParameter("@is_sfm", is_sfm ? 1 : 0),
                        new SqlParameter("@LOGGEDUSER", loggeduser)
                    });

                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();

                    return rowsAffected > 0;
                }
            }
            catch (Exception ex)
            {
                error = ex.Message.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries).FirstOrDefault();
                return false;
            }
        }
        public DataRow CheckValidCustomerCode(string acc)
        {
            return _helperService.HelperCommon.GetSqlRow("select *  From Customer Where trim(acc)=trim(@acc) or old_customer=trim(@acc)", "@acc", acc);
        }

        public bool DeleteCustomer(string acc, out string error)
        {
            error = string.Empty; int rowsAffected = 0;
            try
            {

                using (var connection = _connectionProvider.GetConnection())
                using (var command = new SqlCommand("DeleteCustomer", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    // Add parameter with proper type definition
                    command.Parameters.AddWithValue("@acc", acc);
                    // Open connection and execute the command
                    connection.Open();
                    rowsAffected = command.ExecuteNonQuery();
                    // Return whether rows were affected                   
                }
            }
            catch (Exception ex)
            {
                _CustErrormsg = ex.Message.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries).FirstOrDefault();
            }
            return rowsAffected > 0;
        }

        public void DeleteSalesmanacc(string bcode, string LoggedUser)
        {
            _helperService.HelperCommon.GetSqlData("delete FROM [dbo].[SALESMEN] where code  = @CODE;INSERT INTO KEEP_REC ([DATE], [TIME], WHO, WHAT)  VALUES (GETDATE(), CONVERT(time, GETDATE()), '" + LoggedUser + "', 'SalesMan " + bcode + " was deleted')",
                "@CODE", bcode);
        }
        public DataRow GetMemoByInvNo(string memo_no)
        {
            DataTable dataTable = _helperService.HelperCommon.GetSqlData("select top 1 i.*, it.memo_no,it.fpon from Memo i left join me_items it on i.memo_no = it.memo_no Where ltrim(rtrim(i.memo_no)) = ltrim(rtrim(@memo_no))", "@memo_no", memo_no);
            return _helperService.HelperCommon.GetRowOne(dataTable);
        }

        public bool DeleteMemo(string inv_no, string loggeduser, out string error)
        {
            error = string.Empty; int rowsAffected = 0;
            try
            {
                // Use 'using' for proper resource management
                using (var connection = _connectionProvider.GetConnection())
                using (var dbCommand = new SqlCommand("DeleteMemo", connection))
                {
                    dbCommand.CommandType = CommandType.StoredProcedure;
                    dbCommand.Parameters.AddWithValue("@inv_no", inv_no ?? (object)DBNull.Value);
                    dbCommand.Parameters.AddWithValue("@LOGGEDUSER", loggeduser ?? (object)DBNull.Value);
                    connection.Open();
                    rowsAffected = dbCommand.ExecuteNonQuery();
                    // Return true if rows are affected, else false

                }

            }
            catch (Exception ex)
            {

                _CustErrormsg = ex.Message.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries).FirstOrDefault();
            }
            return rowsAffected > 0;

        }

        public DataTable GetRtvHeadInfo(string rtvno, string billingacctno)
        {
            return _helperService.HelperCommon.GetStoreProc("RtvHead", "@RTVNO", rtvno, "@BILLINGNO", billingacctno);
        }
        public bool DeleteRtv(string INV_NO, string reqtext, string LOGGEDUSER)
        {
            _helperService.HelperCommon.GetStoreProc("DELRTV", "@INV_NO", INV_NO, "@REQUIREDTEXT", reqtext, "@LOGGEDUSER", LOGGEDUSER);
            return true;
        }
        public DataTable CheckPayItems(string rtvno)
        {
            return _helperService.HelperCommon.GetSqlData(@"SELECT * FROM PAY_ITEM WHERE RTV_PAY = 'R' AND INV_NO = @rtvno ", "@rtvno", rtvno);
        }
        public bool GetPotentialCustomerByAcc(string PotentialACC, string loggedUser)
        {
            using (var connection = _connectionProvider.GetConnection())
            {
                const string query = @"
            DELETE FROM MAILING WHERE ACC = @ACC;
            UPDATE QUOTES 
            SET ISPOTENTIALCUSTOMER = 0 
            WHERE ACC = @ACC;
            INSERT INTO KEEP_REC ([DATE], [TIME], WHO, WHAT)  
            VALUES (SYSDATETIME(), CONVERT(time, SYSDATETIME()), @LoggedUser, @LogMessage);
        ";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add("@ACC", SqlDbType.VarChar).Value = PotentialACC;
                    command.Parameters.Add("@LoggedUser", SqlDbType.VarChar).Value = loggedUser;
                    command.Parameters.Add("@LogMessage", SqlDbType.VarChar).Value = $"Customer {PotentialACC} was deleted";

                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }

        public DataTable GetLotInfo(string lcode)
        {
            return _helperService.HelperCommon.GetSqlData("select * from styles with (nolock) where style = @lcode or barcode = @lcode or oldbarcode = @lcode", "@lcode", lcode);
        }
        public string DelALotNO(string lotCode)
        {
            using (var connection = _connectionProvider.GetConnection())
            using (var command = new SqlCommand("DelALotNO", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@LOTNO", SqlDbType.NVarChar, 100) { Value = lotCode });
                var returnParameter = new SqlParameter("@RETVAL", SqlDbType.NVarChar, 100)
                {
                    Direction = ParameterDirection.Output
                };
                command.Parameters.Add(returnParameter);
                command.CommandTimeout = 6000;
                connection.Open();
                command.ExecuteNonQuery();
                return returnParameter.Value.ToString();
            }
        }
        public bool iSUsedImagesToOthereStyle(String Style, out DataTable dtImages)
        {
            DataTable dtLastUpdate = _helperService.HelperCommon.GetSqlData($"select STYLE from STYL_IMAGES where DESCRIPTION IN(select[DESCRIPTION] from STYL_IMAGES where STYLE=@Style) group by STYLE", "@Style", Style.Replace("/", ""));
            dtImages = _helperService.HelperCommon.GetSqlData($"select [DESCRIPTION] from STYL_IMAGES where STYLE=@Style", "@Style", Style.Replace("/", ""));
            return _helperService.HelperCommon.DataTableOK(dtLastUpdate) && dtLastUpdate.Rows.Count == 1;
        }
        public DataTable CheckValidStoreCreditorGiftCard(string scgcNumber, bool isGC)
        {
            if (!isGC)
                return _helperService.HelperCommon.GetSqlData("SELECT * FROM storecreditvoucher WHERE TRIM(creditno) = TRIM(@scgcNumber) and isnull(isGiftCert,0)=0", "@scgcNumber", scgcNumber);
            return _helperService.HelperCommon.GetSqlData("SELECT * FROM storecreditvoucher WHERE TRIM(UserGCNo) = TRIM(@scgcNumber) and isnull(isGiftCert,0)=1", "@scgcNumber", scgcNumber);
        }

        public bool CancelStoreCreditorGiftCard(string scgcNumber, string cancelReason, bool isGiftCard, string loggedUser)
        {
            using (var connection = _connectionProvider.GetConnection())
            using (var command = new SqlCommand("CancelStoreCreditsGiftCards", connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                // Add parameters explicitly
                command.Parameters.Add(new SqlParameter("@SCGC", SqlDbType.VarChar) { Value = scgcNumber ?? (object)DBNull.Value });
                command.Parameters.Add(new SqlParameter("@cREASON", SqlDbType.VarChar) { Value = string.IsNullOrEmpty(cancelReason) ? (object)DBNull.Value : cancelReason });
                command.Parameters.Add(new SqlParameter("@isGC", SqlDbType.Bit) { Value = isGiftCard });
                command.Parameters.Add(new SqlParameter("@cUSER", SqlDbType.VarChar) { Value = string.IsNullOrEmpty(loggedUser) ? (object)DBNull.Value : loggedUser });

                // Open connection and execute
                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();

                return rowsAffected > 0;
            }
        }
        public DataTable GetAllSroreCredits(string strCustomer = "", string FromDate = "", string ToDate = "", string Opt = "", bool IsGiftCert = false, string CreditNo = "")
        {

            DataTable dataTable = new DataTable();
            try
            {
                using (SqlDataAdapter SqlDataAdapter = new SqlDataAdapter())
                {

                    // Create the command and set its properties
                    SqlDataAdapter.SelectCommand = new SqlCommand();
                    SqlDataAdapter.SelectCommand.Connection = _connectionProvider.GetConnection();
                    SqlDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;

                    // Assign the SQL to the command object
                    SqlDataAdapter.SelectCommand.CommandText = @"GetAllSroreCredits";

                    SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@Acc", string.IsNullOrEmpty(strCustomer) ? "" : strCustomer.Trim());
                    SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@FromDate", string.IsNullOrEmpty(FromDate) ? "" : FromDate);
                    SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@ToDate", string.IsNullOrEmpty(ToDate) ? "" : ToDate);
                    SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@Type", string.IsNullOrEmpty(Opt) ? "" : Opt.Trim());
                    SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@IsGiftCert", IsGiftCert ? 1 : 0);
                    SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@CreditNo", string.IsNullOrEmpty(CreditNo) ? "" : CreditNo.Trim());

                    // Fill the table from adapter
                    SqlDataAdapter.Fill(dataTable);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dataTable;
        }

        public DataTable CheckValidOrder(string pon)
        {
            return _helperService.HelperCommon.GetSqlData("select top 1 * from orders where pon like '%" + pon + "'");
        }
        public bool DeleteCustPO(string pon, string loggeduser, out string error)
        {
            error = string.Empty;
            try
            {
                using (SqlConnection connection = _connectionProvider.GetConnection())
                using (SqlCommand dbCommand = new SqlCommand("DeleteCustPO", connection))
                {
                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@PON", SqlDbType.VarChar, 50) { Value = pon });
                    dbCommand.Parameters.Add(new SqlParameter("@LOGGEDUSER", SqlDbType.VarChar, 50) { Value = loggeduser });

                    connection.Open();
                    int rowsAffected = dbCommand.ExecuteNonQuery();

                    return rowsAffected > 0;
                }
            }
            catch (Exception ex)
            {
                error = ex.Message.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)[0];
                return false;
            }
        }

        public string GetVendorNameByCode(string acc)
        {
            DataTable dataTable = _helperService.HelperCommon.GetSqlData(@"SELECT ISNULL(NAME,'') as VEND_NAME FROM VENDORS with (nolock) WHERE ACC=@ACC",
                "@acc", acc);
            return dataTable.Rows.Count > 0 ? dataTable.Rows[0]["VEND_NAME"].ToString() : string.Empty;
        }
        public bool DeleteBill(string invNo, string loggedUser, bool resetCost, out string error, bool isStyleItem = false)
        {
            error = string.Empty;
            try
            {
                using (SqlConnection connection = _connectionProvider.GetConnection())
                using (SqlCommand command = new SqlCommand("DeleteBill", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Configure parameters
                    command.Parameters.AddWithValue("@INV_NO", invNo);
                    command.Parameters.AddWithValue("@LOGGEDUSER", loggedUser);
                    command.Parameters.AddWithValue("@RESETCOST", resetCost);
                    command.Parameters.AddWithValue("@IsStyleItem", isStyleItem);

                    // Open connection and execute
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();

                    return rowsAffected > 0;
                }

            }
            catch (Exception ex)
            {
                error = ex.Message.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)[0];
                return false;
            }


        }
        public DataTable CheckStyleInstockqty(string inv_no)
        {
            return _helperService.HelperCommon.GetSqlData(@"SELECT bi.inv_no, bi.STYLE, BI.PCS QTY,(st.IN_STOCK - st.LAYAWAY- st.IN_SHOP) IN_STOCK 
                FROM BIL_ITEM BI with (nolock) 
                LEFT JOIN stock st with (nolock) on st.STYLE=dbo.invstyle(BI.STYLE) and st.store_no=bi.store_no 
                where ltrim(rtrim(bi.INV_NO))=ltrim(rtrim(@inv_no)) order by bi.line_no, BI.STYLE", "@inv_no", inv_no);
        }

        public bool UpdateCustomerRecord(CustomerModel customer, bool iSWrist = false)
        {
            if (string.IsNullOrWhiteSpace(customer.ADDR13))
                customer.ADDR13 = "";
            if (string.IsNullOrWhiteSpace(customer.EMAIL))
                customer.EMAIL = "";

            using (SqlCommand dbCommand = new SqlCommand())
            {
                // Set the command object properties
                dbCommand.Connection = _connectionProvider.GetConnection();
                dbCommand.CommandType = CommandType.Text;
                if (iSWrist)
                    dbCommand.CommandText = @"Update customer set Name2 = @Name, Addr2 =cast(@Addr1 as nvarchar(60)), Addr22 = cast(@Addr12 as nvarchar(60)),
                    Addr23 = IIF(@Addr13='', addr23, cast(@Addr13 as nvarchar(60))), City2 = cast(@City1 as nvarchar(30)), State2 = cast(@State1 as nvarchar(10)),Zip2 =cast(@Zip1 as nvarchar(10)),
                    Country2 = cast(@Country as nvarchar(15)), Email = IIF(@Email='', Email, cast(@Email as nvarchar(80))), DRIVERLICENSE_STATE = cast(@DLState as nvarchar(2)), 
                    DOB =iif(iSNULL(@dob,'')='',DOB,@dob), TEL2 = iif(isnull(@TELE,'')='', TEL2, cast(@TELE as nvarchar(30))),declined=iSNULL(@declined,0),driverlicense_number=iSNULL(cast(@driverlicense_number as nvarchar(15)),driverlicense_number) where replace(replace(acc,char(10),''),char(13),'') =replace(replace(@acc,char(10),''),char(13),'')";
                else
                    dbCommand.CommandText = @"Update customer set Name2 = @Name, Addr2 =cast(@Addr1 as nvarchar(60)), Addr22 = cast(@Addr12 as nvarchar(60)),
                    Addr23 = IIF(@Addr13='', addr23, cast(@Addr13 as nvarchar(60))), City2 = cast(@City1 as nvarchar(30)), State2 = cast(@State1 as nvarchar(10)),Zip2 =cast(@Zip1 as nvarchar(10)),
                    Country2 = cast(@Country as nvarchar(15)), Email = IIF(@Email='', Email, cast(@Email as nvarchar(80))), DRIVERLICENSE_STATE = cast(@DLState as nvarchar(2)), 
                    DOB =iif(iSNULL(@dob,'')='',DOB,@dob), TEL2 = iif(isnull(@TELE,'')='', TEL2, cast(@TELE as nvarchar(30))),declined=iSNULL(@declined,0),driverlicense_number=iSNULL(cast(@driverlicense_number as nvarchar(15)),driverlicense_number) 
                    ,name=iif(isnull(different_ship,0)=0,cast(@name as nvarchar(60)),name)
                    ,addr1=iif(isnull(different_ship,0)=0,cast(@Addr1 as nvarchar(60)),addr1)
                    ,addr12=iif(isnull(different_ship,0)=0,cast(@Addr12 as nvarchar(60)),addr12)
                    ,addr13=iif(isnull(different_ship,0)=0,cast(@Addr13 as nvarchar(60)),addr13)               
                    ,city1=iif(isnull(different_ship,0)=0,cast(@city1 as nvarchar(30)),city1)
                    ,state1=iif(isnull(different_ship,0)=0,cast(@DLState as nvarchar(10)),state1)
                    ,zip1=iif(isnull(different_ship,0)=0,cast(@zip1 as nvarchar(10)),zip1)
                    ,tel=iif(isnull(different_ship,0)=0,iif(@TELE='', NULL, cast(@TELE as nvarchar(30))),tel)
                    ,country=iif(isnull(different_ship,0)=0,cast(@country as nvarchar(15)),country)
                    where replace(replace(acc,char(10),''),char(13),'') =replace(replace(@acc,char(10),''),char(13),'')";

                dbCommand.Parameters.AddWithValue("@Acc", Convert.ToString(customer.ACC));
                dbCommand.Parameters.AddWithValue("@Name", Convert.ToString(customer.NAME.Replace("''", "'")));
                dbCommand.Parameters.AddWithValue("@Addr1", Convert.ToString(customer.ADDR1.Replace("''", "'")));
                dbCommand.Parameters.AddWithValue("@Addr12", Convert.ToString(customer.ADDR12.Replace("''", "'")));
                dbCommand.Parameters.AddWithValue("@Addr13", Convert.ToString(customer.ADDR13));
                dbCommand.Parameters.AddWithValue("@City1", Convert.ToString(customer.CITY1.Replace("''", "'")));
                dbCommand.Parameters.AddWithValue("@State1", Convert.ToString(customer.STATE1));
                dbCommand.Parameters.AddWithValue("@Zip1", Convert.ToString(customer.ZIP1));
                dbCommand.Parameters.AddWithValue("@Country", Convert.ToString(customer.COUNTRY));
                dbCommand.Parameters.AddWithValue("@Email", Convert.ToString(customer.EMAIL));
                dbCommand.Parameters.AddWithValue("@DLState", Convert.ToString(customer.driverlicense_state));
                dbCommand.Parameters.AddWithValue("@dob", customer.DOB.HasValue ? customer.DOB.Value : (object)DBNull.Value);
                dbCommand.Parameters.AddWithValue("@Tele", Convert.ToString(customer.TEL));
                dbCommand.Parameters.AddWithValue("@declined", Convert.ToString(customer.declined));
                dbCommand.Parameters.AddWithValue("@driverlicense_number", customer.driverlicense_number);
                // Open the connection, execute the query and close the connection
                dbCommand.Connection.Open();
                var rowsAffected = dbCommand.ExecuteNonQuery();
                dbCommand.Connection.Close();

                return rowsAffected > 0;
            }
        }

        public string GetEmail(string acc)
        {
            DataTable dataTable = _helperService.HelperCommon.GetSqlData(@"SELECT ISNULL(EMAIL,'') as EMAIL FROM CUSTOMER with (nolock) WHERE ACC=@ACC", "@ACC", acc);
            return _helperService.HelperCommon.DataTableOK(dataTable) ? dataTable.Rows[0]["email"].ToString() : string.Empty;
        }

    }
}
