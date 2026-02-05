/*
 * Created by Vivek
 *  Vivek 09/20/2024 Created methods for getCheckPrintStatus and getPrintBill
 *  Vivek 09/27/2024 Created method CheckValidBill, getPrintBillDetailsNew
 *  Vivek 10/02/2024 Created method GetCreditDetails, GetCcrdDetails, getPrintBillDetailsNewForCredit
 *  Vivek 10/07/2024 added GetVendorNameByCode, GetBillsByVendorAcc, getPayablesBillDetailsNew methods
 *  Vivek 10/16/2024 getPrintBillDetails() modified
 *  Vivek 10/18/2024 Added GetInvoiceNoofBills, CheckValidBillByVendorInvoiceNo, Getsalesmandetails methods 
 *  Vivek 12/16/2024 Added GetCreditCardNames, GetStoreCodes, GetCcTransactions,CheckValidVendorCode, GetLogNo, SearchVendors, VendorCredit methods 
 *  Vivek 12/26/2024 Added ListofConsignments methods 
 *  Phanindra 01/23/2025 Added ListofChecks method
 *  Phanindra 02/13/2025 Changed ListofChecks to GetListofChecks and added ListofChecksDetailsShows
 *  Phanindra 02/27/2025 Worked on fixing GetLogNo function to check for Vendor annd store
 *  chakri    05/23/2025 Added CheckValidBillNo, CosignmentStyleInstockqty and DeleteConsignment methods.
 *  chakri    05/23/2025 Added CheckValidCheckNo, GetBankAccounts and CancelCreditCheck methods.
 *  chakri    05/27/2025 Added GetDetByCCLog, GetInvoiceByInvNo and DELCCLOG methods.
 *  Sravan    10/01/2025 Added new ApplyACheck()
 *  Sravan    12/02/2025 Added new  dbUpdatecheckNoInfo() and  dbChangecheckNoBank()
 *  Siva      12/04/2025 Added CheckValidSetter, GetAllSettersForGrid, CheckoldVendorCode,GetStateCityByZip
UpdateVenAttr, AddVenAttr, CheckAttrExists, AddVendor, GetVendorMax
 *  Siva      12/05/2025 Added UpdateVendor
 *  Sravan    12/11/2025 Added Class and method APCreditViewModel
 *  Sravan    01/05/2026 UpdateAPCredit() null error handle with string not empty
 *  Siva      01/05/2026 Changed GetCheckDetails GetccrdDetails 
 *  Sravan    01/08/2026 IssueCheckForAVendor() Added New
 *  Sravan    01/13/2026 PayBillByCC() and class for parameter use PayBillsByCCViewModel()
 *  Sravan    01/16/2026 Added new PayBillAllByCCRD() also added class 
 */
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using System.Data;
using YJWebCoreMVC.Models;

namespace YJWebCoreMVC.Services
{
    public class AccountsPayableService
    {
        private readonly ConnectionProvider _connectionProvider;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly HelperCommonService _helperCommonService;
        private readonly int _decimalPlaces;

        public DateTime? CHKDT { get; set; }
        public DateTime? BILLDT { get; set; }
        public string strformattedCHKDT { get; set; }
        public string strformattedBILLDT { get; set; }
        public decimal DISCOUNT { get; set; }
        public decimal PaidAmt { get; set; }
        public List<VendorDetails> lstVendorDetails { get; set; } = new List<VendorDetails>();
        public List<GLLogDetails> lstGLLogDetails { get; set; } = new List<GLLogDetails>();
        public List<CheckDetails> lstCheckDetails { get; set; } = new List<CheckDetails>();
        public List<CCRDDetails> lstCCRDDetails { get; set; } = new List<CCRDDetails>();
        public List<CreditDetails> lstCreditDetails { get; set; } = new List<CreditDetails>();
        public List<CreditBillDetails> lstCreditBillDetails { get; set; } = new List<CreditBillDetails>();
        public List<CreditBillNoDetails> lstCreditBillNoDetails { get; set; } = new List<CreditBillNoDetails>();

        public AccountsPayableService(ConnectionProvider connectionProvider, IHttpContextAccessor httpContextAccessor, HelperCommonService helperCommonService)
        {
            _connectionProvider = connectionProvider;
            _httpContextAccessor = httpContextAccessor;
            _helperCommonService = helperCommonService;
            _decimalPlaces = int.TryParse(_httpContextAccessor.HttpContext?.Session.GetString("DecimalsInPrices"), out int value)? value: 0;
        }

        public List<AccountsPayableModel> getVendorStatementDetails(string acc, bool openonly, DateTime? fromdate, DateTime? todate, string store = "", bool paybygold = false)
        {
            DataTable dataTable = new DataTable();
            List<AccountsPayableModel> lstVendorStatement = new List<AccountsPayableModel>();

            using (SqlDataAdapter dataAdapter = new SqlDataAdapter())
            {
                try
                {
                    dataAdapter.SelectCommand = new SqlCommand();
                    dataAdapter.SelectCommand.Connection = _connectionProvider.GetConnection();

                    dataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                    dataAdapter.SelectCommand.CommandText = "VendorStatement";

                    dataAdapter.SelectCommand.Parameters.AddWithValue("@acc", acc);
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@openonly", openonly ? 1 : 0);
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@date1", fromdate);
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@date2", todate);
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@store", store);
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@paybygold", paybygold ? 1 : 0);
                    // Fill the datatable From adapter
                    dataAdapter.Fill(dataTable);

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                DateTime date;
                string strFormattedDate;
                string strFormatteddue_date;
                foreach (DataRow dr in dataTable.Rows)
                {
                    date = DateTime.Parse(dr["date"].ToString());
                    strFormattedDate = date.ToString("yyyy-MM-dd");
                    date = DateTime.Parse(dr["due_date"].ToString());
                    strFormatteddue_date = date.ToString("yyyy-MM-dd");
                    lstVendorStatement.Add(new AccountsPayableModel()
                    {
                        ref_no = dr["ref_no"].ToString(),
                        vnd_no = dr["vnd_no"].ToString(),
                        strFormattedDate = strFormattedDate,
                        strFormatteddue_date = strFormatteddue_date,
                        inv_amount = Math.Round(Decimal.Parse(dr["inv_amount"].ToString()), 2),
                        credit = Math.Round(Decimal.Parse(dr["credit"].ToString()), 2),
                        payment = Math.Round(Decimal.Parse(dr["payment"].ToString()), 2),
                        balance = Math.Round(Decimal.Parse(dr["balance"].ToString()), 2),
                        bank = dr["bank"].ToString(),
                        paytype = dr["paytype"].ToString(),
                        bal_inv_amount = Math.Round(Decimal.Parse(dr["bal_inv_amount"].ToString()), 2),
                    });
                }
                return lstVendorStatement;
            }
        }

        public List<AccountsPayableModel> getVendorsatementtotalsDetails(string acc, bool paybygold = false)
        {
            DataTable dataTable = new DataTable();
            List<AccountsPayableModel> lstVendorStatement = new List<AccountsPayableModel>();

            using (SqlDataAdapter dataAdapter = new SqlDataAdapter())
            {
                try
                {
                    dataAdapter.SelectCommand = new SqlCommand();
                    dataAdapter.SelectCommand.Connection = _connectionProvider.GetConnection();

                    dataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                    dataAdapter.SelectCommand.CommandText = "GetVendorsatementtotals";

                    dataAdapter.SelectCommand.Parameters.AddWithValue("@ACC", acc);
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@paybygold", paybygold ? 1 : 0);
                    // Fill the datatable From adapter
                    dataAdapter.Fill(dataTable);

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                foreach (DataRow dr in dataTable.Rows)
                {
                    lstVendorStatement.Add(new AccountsPayableModel()
                    {
                        Tbill = Math.Round(Decimal.Parse(dr["Tbill"].ToString()), 2),
                        Tchecks = Math.Round(Decimal.Parse(dr["Tchecks"].ToString()), 2),
                        Tcredit = Math.Round(Decimal.Parse(dr["Tcredit"].ToString()), 2),
                    });
                }
                return lstVendorStatement;
            }
        }

        // public List<AccountsPayableModel> getPrintBillDetails(string VendorInvoice, string Vendor)

        public AccountsPayableModel getPrintBillDetails(string VendorInvoice, string Vendor, bool IsListOfBillsPrint = false)
        {
            AccountsPayableModel accountsPayableModel = new AccountsPayableModel();
            DataTable dataTable = new DataTable();
            DataTable dtGetBillGLDetails = new DataTable();
            DataTable dtGetCheckDetails = new DataTable();
            List<AccountsPayableModel> lstVendorStatement = new List<AccountsPayableModel>();

            using (SqlDataAdapter dataAdapter = new SqlDataAdapter())
            {
                try
                {
                    dataAdapter.SelectCommand = new SqlCommand();
                    dataAdapter.SelectCommand.Connection = _connectionProvider.GetConnection();

                    dataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                    dataAdapter.SelectCommand.CommandText = "PRINTBILLBYVENDORINVOICENO";

                    dataAdapter.SelectCommand.Parameters.AddWithValue("@VendorInvoice", VendorInvoice);
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@Vendor", Vendor);
                    // Fill the datatable From adapter
                    dataAdapter.Fill(dataTable);

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                //int decimalPlaces = int.TryParse(_httpContextAccessor.GetString("DecimalsInPrices"),out int value)? value: 0;

                DateTime date;
                string strFormattedDate;
                string strFormatteddue_date;
                string strFormattedEnterDate;
                foreach (DataRow dr in dataTable.Rows)
                {
                    date = DateTime.Parse(dr["DATE"].ToString());
                    strFormattedDate = date.ToString("yyyy-MM-dd");
                    date = DateTime.Parse(dr["DUE_DATE"].ToString());
                    strFormatteddue_date = date.ToString("yyyy-MM-dd");
                    date = DateTime.Parse(dr["ENTER_DATE"].ToString());
                    strFormattedEnterDate = date.ToString("yyyy-MM-dd");
                    lstVendorStatement.Add(new AccountsPayableModel()
                    {
                        INV_NO = dr["INV_NO"].ToString(),
                        ACC = dr["ACC"].ToString(),
                        AMOUNT = Math.Round(Decimal.Parse(dr["AMOUNT"].ToString()), 2),
                        strFormattedDate = strFormattedDate,
                        VND_NO = dr["VND_NO"].ToString(),
                        TERM = Math.Round(Decimal.Parse(dr["TERM"].ToString()), 2),
                        strFormatteddue_date = strFormatteddue_date,
                        BALANCE = dr["BALANCE"].ToString(),
                        strFormattedEnterDate = strFormattedEnterDate,
                        SFM = dr["SFM"].ToString(),
                        ON_QB = dr["ON_QB"].ToString(),
                        IS_COMISION = dr["IS_COMISION"].ToString(),
                        Gold_Price = dr["Gold_Price"].ToString(),
                        gl_log = dr["gl_log"].ToString(),
                        NOTE = dr["NOTE"].ToString(),
                        pay_terms = dr["pay_terms"].ToString(),
                        Shipping_Charge = dr["Shipping_Charge"].ToString(),
                        Other_Charge = dr["Other_Charge"].ToString(),
                        Term_Note = dr["Term_Note"].ToString(),
                        Discount = dr["Discount"].ToString(),
                        NoOfDay1 = dr["NoOfDay1"].ToString(),
                        DiscPercent1 = dr["DiscPercent1"].ToString(),
                        NoOfDay2 = dr["NoOfDay2"].ToString(),
                        DiscPercent2 = dr["DiscPercent2"].ToString(),
                        NoOfDay3 = dr["NoOfDay3"].ToString(),
                        DiscPercent3 = dr["DiscPercent3"].ToString(),
                        NO_OF_TERMS = dr["NO_OF_TERMS"].ToString(),
                        TERM_INTERVAL = dr["TERM_INTERVAL"].ToString(),
                        JobbagBill = dr["JobbagBill"].ToString(),
                        store_no = dr["store_no"].ToString(),
                        CVSFM_NO = dr["CVSFM_NO"].ToString(),
                        AddnChargeBillno = dr["AddnChargeBillno"].ToString(),
                        NotAdded = dr["NotAdded"].ToString(),
                        JobbagBillReturn = dr["JobbagBillReturn"].ToString(),
                        IS_SNH_VSFM = dr["IS_SNH_VSFM"].ToString(),
                        Gold_Balance = dr["Gold_Balance"].ToString(),
                        Gold_Wt = dr["Gold_Wt"].ToString(),
                        IsReturnItemsForJobbag = dr["IsReturnItemsForJobbag"].ToString(),
                        Insurance = dr["Insurance"].ToString(),
                        Curr_type = dr["Curr_type"].ToString(),
                        Curr_rate = dr["Curr_rate"].ToString(),
                        Amount_Curr = Math.Round(Decimal.Parse(dr["Amount_Curr"].ToString()), 2),// dr["Amount_Curr"].ToString(),
                        Balance_Curr = Math.Round(Decimal.Parse(dr["Balance_Curr"].ToString()), 2),// dr["Balance_Curr"].ToString(),
                        Shipping_Charge_Curr = Math.Round(Decimal.Parse(dr["Shipping_Charge_Curr"].ToString()), 2),// dr["Shipping_Charge_Curr"].ToString(),
                        Other_Charge_Curr = Math.Round(Decimal.Parse(dr["Other_Charge_Curr"].ToString()), 2),// dr["Other_Charge_Curr"].ToString(),
                        Discount_Curr = Math.Round(Decimal.Parse(dr["Discount_Curr"].ToString()), 2),// dr["Discount_Curr"].ToString(),
                        Insurance_Curr = Math.Round(Decimal.Parse(dr["Insurance_Curr"].ToString()), 2),// dr["Insurance_Curr"].ToString(),
                        STYLE = dr["STYLE"].ToString(),
                        VND_STYLE = dr["VND_STYLE"].ToString(),
                        PCS = Math.Round(Decimal.Parse(dr["PCS"].ToString()), 2),// dr["PCS"].ToString(),
                        WEIGHT = Math.Round(Decimal.Parse(dr["WEIGHT"].ToString()), 2),//dr["WEIGHT"].ToString(),
                        COST = Math.Round(Decimal.Parse(dr["COST"].ToString()), 2), //dr["COST"].ToString(),
                        BY_WT = dr["BY_WT"].ToString(),
                        DESC = dr["DESC"].ToString(),
                        JobNote = dr["JobNote"].ToString(),
                        StyleImage = dr["StyleImage"].ToString(),
                        PRICE = Math.Round(Decimal.Parse(dr["PRICE"].ToString()), 2), //dr["PRICE"].ToString(),
                        Order_no = dr["Order_no"].ToString(),
                        Jobbagno = dr["Jobbagno"].ToString(),
                        TotalAmount = Math.Round(Decimal.Parse(dr["PCS"].ToString()) * Decimal.Parse(dr["COST"].ToString()), 2),
                    });
                }
                accountsPayableModel.lstVendorStatement = lstVendorStatement;

                if (_helperCommonService.DataTableOK(dataTable))
                {
                    if (!IsListOfBillsPrint)
                    {
                        string strvendorInvoiceNo = dataTable.Rows[0]["INV_NO"].ToString();
                        dtGetBillGLDetails = GetBillGLDetails(strvendorInvoiceNo.Trim().PadLeft(6, ' '), true);
                        dtGetCheckDetails = GetCheckDetails(strvendorInvoiceNo.PadLeft(6, ' '));
                    }
                }

                #region GL Code Log Details List

                if (dtGetBillGLDetails != null && dtGetBillGLDetails.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtGetBillGLDetails.Rows)
                    {
                        lstGLLogDetails.Add(new GLLogDetails()
                        {
                            INV_NO = dr["INV_NO"].ToString(),
                            GL_CODE = dr["GL_CODE"].ToString(),
                            DESC = dr["DESC"].ToString(),
                            AMOUNT = Math.Round(Decimal.Parse(dr["AMOUNT"].ToString()), 2),
                            Not_Editable = dr["Not_Editable"].ToString(),
                            DEPT = dr["DEPT"].ToString(),
                            gl_log = dr["gl_log"].ToString(),
                        });
                    }
                    accountsPayableModel.lstGLLogDetails = lstGLLogDetails;
                }

                #endregion

                #region Check Bank Details List

                if (dtGetCheckDetails != null && dtGetCheckDetails.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtGetCheckDetails.Rows)
                    {
                        lstCheckDetails.Add(new CheckDetails()
                        {
                            CHECK_NO = dr["CHECK_NO"].ToString(),
                            BANK = dr["BANK"].ToString(),
                            AMOUNT = Math.Round(Decimal.Parse(dr["AMOUNT"].ToString()), 2),
                            DATE = Convert.ToDateTime(dr["DATE"].ToString()),
                            ACC = dr["ACC"].ToString(),
                            PACK = dr["PACK"].ToString(),
                            GL_CODE = dr["GL_CODE"].ToString(),
                            TRANSACT = dr["TRANSACT"].ToString(),
                            NAME = dr["TRANSACT"].ToString(),
                            ENTER_DATE = Convert.ToDateTime(dr["ENTER_DATE"].ToString()),
                            NOTE = dr["NOTE"].ToString(),
                            ON_QB = dr["ON_QB"].ToString(),
                            GLPOST_LOG = dr["GLPOST_LOG"].ToString(),
                            CLRD = dr["CLRD"].ToString(),
                            STORE_NO = dr["STORE_NO"].ToString(),
                        });
                    }
                    accountsPayableModel.lstCheckDetails = lstCheckDetails;
                }

                #endregion


                return accountsPayableModel;
            }
        }

        public List<AccountsPayableModel> getCheckPrintStatusDetails(string VendorInvoice, string Bank)
        {
            DataTable dataTable = new DataTable();
            List<AccountsPayableModel> lstVendorStatement = new List<AccountsPayableModel>();

            using (SqlDataAdapter dataAdapter = new SqlDataAdapter())
            {
                try
                {
                    dataAdapter.SelectCommand = new SqlCommand();
                    dataAdapter.SelectCommand.Connection = _connectionProvider.GetConnection();

                    dataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                    dataAdapter.SelectCommand.CommandText = "CheckPrintStatus";

                    dataAdapter.SelectCommand.Parameters.AddWithValue("@CHECKNO", VendorInvoice);
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@BANK", Bank);
                    // Fill the datatable From adapter
                    dataAdapter.Fill(dataTable);

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                foreach (DataRow dr in dataTable.Rows)
                {
                    strformattedCHKDT = strformattedBILLDT = "";
                    if (dr["CHKDT"] != DBNull.Value && !string.IsNullOrEmpty(dr["CHKDT"].ToString()))
                    {
                        CHKDT = DateTime.Parse(dr["CHKDT"].ToString());
                        strformattedCHKDT = CHKDT?.ToString("yyyy-MM-dd");
                    }
                    if (dr["BILLDT"] != DBNull.Value && !string.IsNullOrEmpty(dr["BILLDT"].ToString()))
                    {
                        BILLDT = DateTime.Parse(dr["BILLDT"].ToString());
                        strformattedBILLDT = BILLDT?.ToString("yyyy-MM-dd");
                    }
                    if (!string.IsNullOrEmpty(dr["DISCOUNT"].ToString()))
                    {
                        DISCOUNT = Math.Round(Decimal.Parse(dr["DISCOUNT"].ToString()), 2);
                    }
                    else
                    {
                        DISCOUNT = 0;
                    }
                    if (!string.IsNullOrEmpty(dr["PaidAmt"].ToString()))
                    {
                        PaidAmt = Math.Round(Decimal.Parse(dr["PaidAmt"].ToString()), 2);
                    }
                    else
                    {
                        PaidAmt = 0;
                    }

                    lstVendorStatement.Add(new AccountsPayableModel()
                    {
                        CHECK_NO = dr["CHECK_NO"].ToString(),
                        PACK_NO = dr["PACK_NO"].ToString(),
                        ACC = dr["ACC"].ToString(),
                        NAME = dr["NAME"].ToString(),
                        bank = dr["BANK"].ToString(),
                        strformattedCHKDT = strformattedCHKDT,
                        strformattedBILLDT = strformattedBILLDT,
                        AMOUNT = Math.Round(Decimal.Parse(dr["AMOUNT"].ToString()), 2),
                        INV_NO = dr["INV_NO"].ToString(),
                        CLRD = dr["CLRD"].ToString(),
                        VND_NO = dr["VND_NO"].ToString(),
                        DISCOUNT = DISCOUNT,
                        PaidAmt = PaidAmt,
                    });
                }
                return lstVendorStatement;
            }
        }

        public DataTable CheckValidBill(string inv_no)
        {
            return _helperCommonService.CheckValidBill(inv_no);
        }

        public AccountsPayableModel getPrintBillDetailsNew(string Nbillno, bool IsListOfBillsPrint = false)
        {
            AccountsPayableModel accountsPayableModel = new AccountsPayableModel();
            DataTable dtGetBillGLDetails = new DataTable();
            DataTable dtGetCheckDetails = new DataTable();
            DataTable dtGetccrdDetails = new DataTable();
            DataTable dtGetCreditDetails = new DataTable();

            DataTable dtBill = _helperCommonService.CheckValidBill(Nbillno);
            if (_helperCommonService.DataTableOK(dtBill))
            {
                if (!IsListOfBillsPrint)
                {
                    dtGetBillGLDetails = GetBillGLDetails(Nbillno, true);
                    dtGetCheckDetails = GetCheckDetails(Nbillno.PadLeft(6, ' '));
                    dtGetccrdDetails = GetccrdDetails(Nbillno.PadLeft(6, ' '));
                    dtGetCreditDetails = GetCreditDetails(Nbillno.PadLeft(6, ' '));
                }
            }

            #region Vendor Details List
            if (dtBill != null && dtBill.Rows.Count > 0)
            {
                foreach (DataRow dr in dtBill.Rows)
                {
                    lstVendorDetails.Add(new VendorDetails()
                    {
                        INV_NO = dr["INV_NO"].ToString(),
                        ACC = dr["ACC"].ToString(),
                        AMOUNT = Math.Round(Decimal.Parse(dr["AMOUNT"].ToString()), 2),
                        DATE = Convert.ToDateTime(dr["DATE"].ToString()),
                        ENTER_DATE = Convert.ToDateTime(dr["ENTER_DATE"].ToString()),
                        DUE_DATE = Convert.ToDateTime(dr["DUE_DATE"].ToString()),
                        VND_NO = dr["VND_NO"].ToString(),
                        TERM = Math.Round(Decimal.Parse(dr["TERM"].ToString()), 2),
                        //strFormatteddue_date = strFormatteddue_date,
                        BALANCE = dr["BALANCE"].ToString(),
                        //strFormattedEnterDate = strFormattedEnterDate,
                        SFM = dr["SFM"].ToString(),
                        ON_QB = dr["ON_QB"].ToString(),
                        IS_COMISION = dr["IS_COMISION"].ToString(),
                        Gold_Price = dr["Gold_Price"].ToString(),
                        gl_log = dr["gl_log"].ToString(),
                        NOTE = dr["NOTE"].ToString(),
                        pay_terms = dr["pay_terms"].ToString(),
                        Shipping_Charge = dr["Shipping_Charge"].ToString(),
                        Other_Charge = dr["Other_Charge"].ToString(),
                        Term_Note = dr["Term_Note"].ToString(),
                        Discount = dr["Discount"].ToString(),
                        NoOfDay1 = dr["NoOfDay1"].ToString(),
                        DiscPercent1 = dr["DiscPercent1"].ToString(),
                        NoOfDay2 = dr["NoOfDay2"].ToString(),
                        DiscPercent2 = dr["DiscPercent2"].ToString(),
                        NoOfDay3 = dr["NoOfDay3"].ToString(),
                        DiscPercent3 = dr["DiscPercent3"].ToString(),
                        NO_OF_TERMS = dr["NO_OF_TERMS"].ToString(),
                        TERM_INTERVAL = dr["TERM_INTERVAL"].ToString(),
                        JobbagBill = dr["JobbagBill"].ToString(),
                        store_no = dr["store_no"].ToString(),
                        CVSFM_NO = dr["CVSFM_NO"].ToString(),
                        AddnChargeBillno = dr["AddnChargeBillno"].ToString(),
                        NotAdded = dr["NotAdded"].ToString(),
                        JobbagBillReturn = dr["JobbagBillReturn"].ToString(),
                        IS_SNH_VSFM = dr["IS_SNH_VSFM"].ToString(),
                        Gold_Balance = dr["Gold_Balance"].ToString(),
                        Gold_Wt = dr["Gold_Wt"].ToString(),
                        IsReturnItemsForJobbag = dr["IsReturnItemsForJobbag"].ToString(),
                        Insurance = dr["Insurance"].ToString(),
                        Curr_type = dr["Curr_type"].ToString(),
                        Curr_rate = dr["Curr_rate"].ToString(),
                        Amount_Curr = Math.Round(Decimal.Parse(dr["Amount_Curr"].ToString()), 2),// dr["Amount_Curr"].ToString(),
                        Balance_Curr = Math.Round(Decimal.Parse(dr["Balance_Curr"].ToString()), 2),// dr["Balance_Curr"].ToString(),
                        Shipping_Charge_Curr = Math.Round(Decimal.Parse(dr["Shipping_Charge_Curr"].ToString()), 2),// dr["Shipping_Charge_Curr"].ToString(),
                        Other_Charge_Curr = Math.Round(Decimal.Parse(dr["Other_Charge_Curr"].ToString()), 2),// dr["Other_Charge_Curr"].ToString(),
                        Discount_Curr = Math.Round(Decimal.Parse(dr["Discount_Curr"].ToString()), 2),// dr["Discount_Curr"].ToString(),
                        Insurance_Curr = Math.Round(Decimal.Parse(dr["Insurance_Curr"].ToString()), 2),// dr["Insurance_Curr"].ToString(),
                        STYLE = dr["STYLE"].ToString(),
                        VND_STYLE = dr["VND_STYLE"].ToString(),
                        PCS = Math.Round(Decimal.Parse(dr["PCS"].ToString()), 2),// dr["PCS"].ToString(),
                        WEIGHT = Math.Round(Decimal.Parse(dr["WEIGHT"].ToString()), 2),//dr["WEIGHT"].ToString(),
                        COST = Math.Round(Decimal.Parse(dr["COST"].ToString()), 2), //dr["COST"].ToString(),
                        BY_WT = dr["BY_WT"].ToString(),
                        DESC = dr["DESC"].ToString(),
                        JobNote = dr["JobNote"].ToString(),
                        StyleImage = dr["StyleImage"].ToString(),
                        PRICE = Math.Round(Decimal.Parse(dr["PRICE"].ToString()), 2), //dr["PRICE"].ToString(),
                        Order_no = dr["Order_no"].ToString(),
                        Jobbagno = dr["Jobbagno"].ToString(),
                        TotalAmount = Math.Round(Decimal.Parse(dr["PCS"].ToString()) * Decimal.Parse(dr["COST"].ToString()), 2),
                    });
                }
                accountsPayableModel.lstVendorDetails = lstVendorDetails;
            }
            #endregion

            #region GL Code Log Details List

            if (dtGetBillGLDetails != null && dtGetBillGLDetails.Rows.Count > 0)
            {
                foreach (DataRow dr in dtGetBillGLDetails.Rows)
                {
                    lstGLLogDetails.Add(new GLLogDetails()
                    {
                        INV_NO = dr["INV_NO"].ToString(),
                        GL_CODE = dr["GL_CODE"].ToString(),
                        DESC = dr["DESC"].ToString(),
                        AMOUNT = Math.Round(Decimal.Parse(dr["AMOUNT"].ToString()), 2),
                        Not_Editable = dr["Not_Editable"].ToString(),
                        DEPT = dr["DEPT"].ToString(),
                        gl_log = dr["gl_log"].ToString(),
                    });
                }
                accountsPayableModel.lstGLLogDetails = lstGLLogDetails;
            }

            #endregion

            #region Check Bank Details List

            if (dtGetCheckDetails != null && dtGetCheckDetails.Rows.Count > 0)
            {
                foreach (DataRow dr in dtGetCheckDetails.Rows)
                {
                    lstCheckDetails.Add(new CheckDetails()
                    {
                        CHECK_NO = dr["CHECK_NO"].ToString(),
                        BANK = dr["BANK"].ToString(),
                        AMOUNT = Math.Round(Decimal.Parse(dr["AMOUNT"].ToString()), 2),
                        DATE = Convert.ToDateTime(dr["DATE"].ToString()),
                        ACC = dr["ACC"].ToString(),
                        PACK = dr["PACK"].ToString(),
                        GL_CODE = dr["GL_CODE"].ToString(),
                        TRANSACT = dr["TRANSACT"].ToString(),
                        NAME = dr["TRANSACT"].ToString(),
                        ENTER_DATE = Convert.ToDateTime(dr["ENTER_DATE"].ToString()),
                        NOTE = dr["NOTE"].ToString(),
                        ON_QB = dr["ON_QB"].ToString(),
                        GLPOST_LOG = dr["GLPOST_LOG"].ToString(),
                        CLRD = dr["CLRD"].ToString(),
                        STORE_NO = dr["STORE_NO"].ToString(),
                    });
                }
                accountsPayableModel.lstCheckDetails = lstCheckDetails;
            }

            #endregion

            #region CRD Details List

            if (dtGetccrdDetails != null && dtGetccrdDetails.Rows.Count > 0)
            {
                foreach (DataRow dr in dtGetccrdDetails.Rows)
                {
                    lstCCRDDetails.Add(new CCRDDetails()
                    {
                        LOG_NO = dr["LOG_NO"].ToString(),
                        CC_VENDOR = dr["CC_VENDOR"].ToString(),
                        DATE = Convert.ToDateTime(dr["DATE"].ToString()),
                        AMOUNT = Math.Round(Decimal.Parse(dr["AMOUNT"].ToString()), 2),
                        DISCOUNT = Math.Round(Decimal.Parse(dr["DISCOUNT"].ToString()), 2)
                    });
                }
                accountsPayableModel.lstCCRDDetails = lstCCRDDetails;
            }

            #endregion

            #region Credit Details List

            if (dtGetCreditDetails != null && dtGetCreditDetails.Rows.Count > 0)
            {
                foreach (DataRow dr in dtGetCreditDetails.Rows)
                {
                    lstCreditDetails.Add(new CreditDetails()
                    {
                        Credit = dr["Credit"].ToString(),
                        Note = dr["Note"].ToString(),
                        Date = Convert.ToDateTime(dr["Date"].ToString()),
                        Amount = Math.Round(Decimal.Parse(dr["Amount"].ToString()), 2),
                        PACK_NO = dr["PACK_NO"].ToString(),
                    });
                }
                accountsPayableModel.lstCreditDetails = lstCreditDetails;
            }

            //lstVendorStatement.Add(lstCreditDetails);

            #endregion


            return accountsPayableModel;
        }

        public AccountsPayableModel getPrintBillDetailsNewForCredit(string Invno, bool IsApCredit = true)
        {
            AccountsPayableModel accountsPayableModel = new AccountsPayableModel();
            DataTable dtGetCreditDetails = new DataTable();
            DataTable dtGetCreditBillDetails = new DataTable();

            List<AccountsPayableModel> lstVendorStatement = new List<AccountsPayableModel>();
            dtGetCreditDetails = GetCreditDetails(Invno, true);
            #region Bill and Address Details
            if (_helperCommonService.DataTableOK(dtGetCreditDetails))
            {
                foreach (DataRow dr in dtGetCreditDetails.Rows)
                {
                    lstCreditDetails.Add(new CreditDetails()
                    {
                        ACC = dr["ACC"].ToString(),
                        name = dr["name"].ToString(),
                        Addr1 = dr["Addr1"].ToString(),
                        Addr2 = dr["Addr12"].ToString(),
                        city1 = dr["city1"].ToString(),
                        Date = Convert.ToDateTime(dr["Date"].ToString()),
                        //Amount = Math.Round(Decimal.Parse(dr["Amount"].ToString()), 2),
                    });
                }
                accountsPayableModel.lstCreditDetails = lstCreditDetails;
            }
            #endregion

            #region Credit Bill Details List

            if (dtGetCreditBillDetails != null && dtGetCreditBillDetails.Rows.Count > 0)
            {
                foreach (DataRow dr in dtGetCreditBillDetails.Rows)
                {
                    lstCreditBillDetails.Add(new CreditBillDetails()
                    {
                        RTV_PAY = dr["RTV_PAY"].ToString() == "C" ? "CREDIT" : dr["RTV_PAY"].ToString(),
                        NOTE = dr["NOTE"].ToString(),
                        AMOUNT = Math.Round(Decimal.Parse(dr["AMOUNT"].ToString()), 2),
                        INV_NO = dr["INV_NO"].ToString(),
                        PAY_NO = dr["PAY_NO"].ToString(),
                    });
                }
                accountsPayableModel.lstCreditBillDetails = lstCreditBillDetails;
            }

            DataTable databill = _helperCommonService.GetApCreditBillno(Invno);
            string rcots = "";
            if (databill.Rows.Count > 0)
            {
                foreach (DataRow drRow in databill.DefaultView.ToTable(true, "inv_no").Rows)
                {
                    if (!string.IsNullOrWhiteSpace(drRow["inv_no"].ToString()))
                        rcots += string.Format("{0},", drRow["inv_no"].ToString());
                }
                foreach (DataRow dr in databill.Rows)
                {
                    lstCreditBillNoDetails.Add(new CreditBillNoDetails()
                    {
                        AMOUNT = Math.Round(Decimal.Parse(dr["AMOUNT"].ToString()), 2),
                        INV_NO = dr["INV_NO"].ToString()
                    });
                }
                accountsPayableModel.lstCreditBillNoDetails = lstCreditBillNoDetails;
            }
            #endregion


            return accountsPayableModel;
        }


        #region Data Bind Methods For Print
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

        public DataRow GetPayment(string inv_no)
        {
            return _helperCommonService.GetSqlRow("select * from payments where rtv_pay = 'P' and trim(inv_no) = trim(@inv_no)", "@inv_no", inv_no);
        }
        public DataTable GetCreditPayment(string inv_no, string rtv_pay, bool isApCredit)
        {
            if (isApCredit)
                return _helperCommonService.GetSqlData("select  'C' as RTV_PAY, '' AS PAY_NO, INV_NO, AMOUNT,MESSAGE AS NOTE  from APCREDIT WHERE INV_NO=@INV_NO order by inv_no",
                    "@inv_no", inv_no, "@rtv_pay", rtv_pay);
            return _helperCommonService.GetSqlData("select * from pay_item where pay_no in (select pay_no from pay_item where rtv_pay = @rtv_pay and inv_no = @inv_no) order by rtv_pay,inv_no",
                 "@inv_no", inv_no, "@rtv_pay", rtv_pay);
        }

        public DataTable CheckValidBill1(string inv_no)
        {
            return _helperCommonService.CheckValidBill(inv_no.Trim().PadLeft(6, ' '));
        }
        public DataTable GetBillGLDetails(string inv_no, bool IsForReport = false)
        {
            //return Data_helperCommonService.GetStoreProc("GetBillGLDetails", "@inv_no", inv_no, "@IsForReport", IsForReport.ToString());
            DataTable dataTable = new DataTable();

            using (SqlDataAdapter dataAdapter = new SqlDataAdapter())
            {
                try
                {
                    dataAdapter.SelectCommand = new SqlCommand();
                    dataAdapter.SelectCommand.Connection = _connectionProvider.GetConnection();

                    dataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                    dataAdapter.SelectCommand.CommandText = "GetBillGLDetails";

                    dataAdapter.SelectCommand.Parameters.AddWithValue("@inv_no", inv_no);
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@IsForReport", IsForReport.ToString());
                    // Fill the datatable From adapter
                    dataAdapter.Fill(dataTable);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                //return dataTable;
            }
            return dataTable;
        }
        public DataTable GetCheckDetails(string bill_no)
        {
            return _helperCommonService.GetSqlData("select * from CHECKS where pack in (select PACK_NO from BILCHK where inv_no =  @bill_no and type='I') order by enter_date asc",
                "@bill_no", bill_no);
        }
        public DataTable GetccrdDetails(string bill_no)
        {
            return _helperCommonService.GetSqlData("select LOG_NO,CC_VENDOR,DATE,AMOUNT,DISCOUNT from BIL_CCRD where bill_no=@bill_no ",
                "@bill_no", bill_no);
        }
        public DataTable GetCheckDetails1(string bill_no)
        {
            return _helperCommonService.GetSqlData("SELECT AMOUNT as APPLIEDAMOUNT,PACK_NO FROM BILCHK WHERE INV_NO=@bill_no  and type='I' and ltrim(rtrim(PACK_NO)) in (select ltrim(rtrim(PACK)) from CHECKS )",
                "@bill_no", bill_no);
        }
        public DataTable GetCreditDetails(string bill_no)
        {
            return _helperCommonService.GetSqlData("SELECT BC.INV_NO Credit,AC.DATE [Date],Bc.AMOUNT Amount,ac.MESSAGE Note,BC.PACK_NO FROM BILCHK BC , APCREDIT AC   WHERE BC.INV_NO=AC.INV_NO  AND BC.TYPE IN ('R') AND BC.PACK_NO IN (select PACK_NO from BILCHK where inv_no=@bill_no and type='I') order by ac.date asc",
                "@bill_no", bill_no);
        }
        public DataTable GetCreditDetails1(string bill_no)
        {
            return _helperCommonService.GetSqlData("SELECT AMOUNT as BILLEDAMOUNT,PACK_NO FROM BILCHK WHERE INV_NO=@bill_no  and type='I' and  ltrim(rtrim(PACK_NO)) in (SELECT ltrim(rtrim(BC.PACK_NO)) FROM BILCHK BC , APCREDIT AC WHERE BC.INV_NO=AC.INV_NO  AND BC.TYPE IN ('R') AND BC.PACK_NO IN (select PACK_NO from BILCHK where inv_no=@bill_no and type='I') )",
                "@bill_no", bill_no);
        }

        #endregion


        public string GetVendorNameByCode(string acc)
        {
            DataTable dataTable = _helperCommonService.GetSqlData(@"SELECT ISNULL(NAME,'') as VEND_NAME FROM VENDORS WHERE ACC=@ACC",
                "@acc", acc);
            return dataTable.Rows.Count > 0 ? dataTable.Rows[0]["VEND_NAME"].ToString() : string.Empty;
        }

        public DataTable GetBillsByVendorAcc(string acc, DateTime fromdate, DateTime todate, string Storeno = "")
        {
            DataTable dt = new DataTable();
            using (SqlDataAdapter dataAdapter = new SqlDataAdapter())
            {

                dataAdapter.SelectCommand = new SqlCommand();
                dataAdapter.SelectCommand.Connection = _connectionProvider.GetConnection();
                dataAdapter.SelectCommand.CommandType = CommandType.Text;
                dataAdapter.SelectCommand.CommandText = "Select  b.Inv_no,b.Acc,a.NAME AS Vend_Name, b.[Date],isnull(b.[Amount],0)[Amount],isnull(b.[Balance],0)[Balance] from Bills b left outer join vendors a on a.acc=b.acc where try_cast(b.[Date] as date) between try_cast(@fromDate as date) and try_cast(@toDate as date) and b.Acc=(case when @acc<>'' then @acc else b.Acc end) AND (@Store_no = '' or b.store_no = @Store_no)";

                dataAdapter.SelectCommand.Parameters.AddWithValue("@acc", acc);
                dataAdapter.SelectCommand.Parameters.AddWithValue("@fromDate", fromdate);
                dataAdapter.SelectCommand.Parameters.AddWithValue("@toDate", todate);
                dataAdapter.SelectCommand.Parameters.AddWithValue("@Store_no", Storeno);
                // Fill the datatable From adapter
                dataAdapter.Fill(dt);
                return dt;
            }
        }

        public AccountsPayableModel getPayablesBillDetailsNew(string Nbillno, bool IsListOfBillsPrint = false)
        {
            AccountsPayableModel accountsPayableModel = new AccountsPayableModel();
            DataTable dtVendorDetails = new DataTable();
            DataTable dtGetBillGLDetails = new DataTable();
            DataTable dtGetCheckDetails = new DataTable();
            DataTable dtGetccrdDetails = new DataTable();
            DataTable dtGetCreditDetails = new DataTable();

            DataTable dtBill = _helperCommonService.CheckValidBill(Nbillno);
            if (_helperCommonService.DataTableOK(dtBill))
            {
                if (!IsListOfBillsPrint)
                {
                    dtGetBillGLDetails = GetBillGLDetails(Nbillno, true);
                    dtGetCheckDetails = GetCheckDetails(Nbillno.PadLeft(6, ' '));
                    dtGetccrdDetails = GetccrdDetails(Nbillno.PadLeft(6, ' '));
                    dtGetCreditDetails = GetCreditDetails(Nbillno.PadLeft(6, ' '), true);
                }
            }

            #region Vendor Details List
            if (dtBill != null && dtBill.Rows.Count > 0)
            {
                foreach (DataRow dr in dtBill.Rows)
                {
                    string invno1 = dr["INV_NO"].ToString() == null ? "" : dr["INV_NO"].ToString();
                    string ACC1 = dr["ACC"].ToString() == null ? "" : dr["ACC"].ToString();
                    decimal AMOUNT1 = 0, TERM1 = 0, PCS1 = 0, WEIGHT1 = 0, COST1 = 0, PRICE1 = 0;
                    DateTime DATE1 = new DateTime();
                    DateTime ENTER_DATE1 = new DateTime();
                    DateTime DUE_DATE1 = new DateTime();
                    string VND_NO1 = "", SFM1 = "", ON_QB1 = "", IS_COMISION1 = "", Gold_Price1 = "", gl_log1 = "", BALANCE1a = "";
                    string NOTE1 = "", pay_terms1 = "";
                    if (!string.IsNullOrEmpty(dr["AMOUNT"].ToString()))
                        AMOUNT1 = Convert.ToDecimal(dr["AMOUNT"].ToString());
                    if (!string.IsNullOrEmpty(dr["DATE"].ToString()))
                        DATE1 = Convert.ToDateTime(dr["DATE"].ToString());
                    if (!string.IsNullOrEmpty(dr["ENTER_DATE"].ToString()))
                        ENTER_DATE1 = Convert.ToDateTime(dr["ENTER_DATE"].ToString());
                    if (!string.IsNullOrEmpty(dr["DUE_DATE"].ToString()))
                        DUE_DATE1 = Convert.ToDateTime(dr["DUE_DATE"].ToString());
                    if (!string.IsNullOrEmpty(dr["VND_NO"].ToString()))
                        VND_NO1 = dr["VND_NO"].ToString();
                    if (!string.IsNullOrEmpty(dr["TERM"].ToString()))
                        TERM1 = Convert.ToDecimal(dr["TERM"].ToString());
                    if (!string.IsNullOrEmpty(dr["BALANCE"].ToString()))
                        BALANCE1a = dr["BALANCE"].ToString();
                    if (!string.IsNullOrEmpty(dr["SFM"].ToString()))
                        SFM1 = dr["SFM"].ToString();
                    if (!string.IsNullOrEmpty(dr["ON_QB"].ToString()))
                        ON_QB1 = dr["ON_QB"].ToString();
                    if (!string.IsNullOrEmpty(dr["IS_COMISION"].ToString()))
                        IS_COMISION1 = dr["IS_COMISION"].ToString();
                    if (!string.IsNullOrEmpty(dr["gl_log"].ToString()))
                        gl_log1 = dr["gl_log"].ToString();

                    if (!string.IsNullOrEmpty(dr["PCS"].ToString()))
                        PCS1 = Convert.ToDecimal(dr["PCS"].ToString());
                    if (!string.IsNullOrEmpty(dr["WEIGHT"].ToString()))
                        WEIGHT1 = Convert.ToDecimal(dr["WEIGHT"].ToString());
                    if (!string.IsNullOrEmpty(dr["COST"].ToString()))
                        COST1 = Convert.ToDecimal(dr["COST"].ToString());
                    if (!string.IsNullOrEmpty(dr["PRICE"].ToString()))
                        PRICE1 = Convert.ToDecimal(dr["PRICE"].ToString());

                    string JobbagBill = dr["JobbagBill"].ToString() == null ? "false" : dr["JobbagBill"].ToString();
                    string JobbagBillReturn = dr["JobbagBillReturn"] == null ? "false" : dr["JobbagBillReturn"].ToString();
                    string Jobbagno = dr["Jobbagno"].ToString() == null ? "" : dr["Jobbagno"].ToString();
                    decimal TotalAmount1 = Math.Round((PCS1 * COST1), 2);
                    try
                    {
                        lstVendorDetails.Add(new VendorDetails()
                        {
                            INV_NO = invno1, //dr["INV_NO"].ToString(),
                            ACC = ACC1, //dr["ACC"].ToString(),
                            AMOUNT = AMOUNT1, //Math.Round(Decimal.Parse(dr["AMOUNT"].ToString()), 2),
                            DATE = DATE1, //Convert.ToDateTime(dr["DATE"].ToString()),
                            ENTER_DATE = ENTER_DATE1, //Convert.ToDateTime(dr["ENTER_DATE"].ToString()),
                            DUE_DATE = DUE_DATE1, //Convert.ToDateTime(dr["DUE_DATE"].ToString()),
                            VND_NO = VND_NO1, //dr["VND_NO"].ToString(),
                            TERM = TERM1, //Math.Round(Decimal.Parse(dr["TERM"].ToString()), 2),
                            BALANCE = BALANCE1a, //dr["BALANCE"].ToString(),
                            SFM = SFM1, //dr["SFM"].ToString(),
                            ON_QB = ON_QB1, //dr["ON_QB"].ToString(),
                            IS_COMISION = IS_COMISION1, //dr["IS_COMISION"].ToString(),
                            Gold_Price = Gold_Price1, //dr["Gold_Price"].ToString(),
                            gl_log = gl_log1, //dr["gl_log"].ToString(),
                            NOTE = NOTE1, //dr["NOTE"].ToString(),
                            pay_terms = pay_terms1, //dr["pay_terms"].ToString(),
                            STYLE = dr["STYLE"].ToString(),
                            VND_STYLE = dr["VND_STYLE"].ToString(),
                            DESC = dr["DESC"].ToString(),
                            store_no = dr["store_no"].ToString(),
                            PCS = PCS1,
                            WEIGHT = WEIGHT1,
                            COST = COST1,
                            PRICE = PRICE1,
                            TotalAmount = TotalAmount1,
                            JobbagBill = JobbagBill,
                            JobbagBillReturn = JobbagBillReturn,
                            Jobbagno = Jobbagno
                        });
                    }
                    catch (Exception)
                    {

                    }
                }
                accountsPayableModel.lstVendorDetails = lstVendorDetails;
            }
            #endregion

            #region GL Code Log Details List

            if (dtGetBillGLDetails != null && dtGetBillGLDetails.Rows.Count > 0)
            {
                foreach (DataRow dr in dtGetBillGLDetails.Rows)
                {
                    lstGLLogDetails.Add(new GLLogDetails()
                    {
                        INV_NO = dr["INV_NO"].ToString(),
                        GL_CODE = dr["GL_CODE"].ToString(),
                        DESC = dr["DESC"].ToString(),
                        AMOUNT = Math.Round(Decimal.Parse(dr["AMOUNT"].ToString()), 2),
                        Not_Editable = dr["Not_Editable"].ToString(),
                        DEPT = dr["DEPT"].ToString(),
                        gl_log = dr["gl_log"].ToString(),
                    });
                }
                accountsPayableModel.lstGLLogDetails = lstGLLogDetails;
            }

            #endregion

            #region Check Bank Details List

            if (dtGetCheckDetails != null && dtGetCheckDetails.Rows.Count > 0)
            {
                foreach (DataRow dr in dtGetCheckDetails.Rows)
                {
                    lstCheckDetails.Add(new CheckDetails()
                    {
                        CHECK_NO = dr["CHECK_NO"].ToString(),
                        BANK = dr["BANK"].ToString(),
                        AMOUNT = Math.Round(Decimal.Parse(dr["AMOUNT"].ToString()), 2),
                        DATE = Convert.ToDateTime(dr["DATE"].ToString()),
                        ACC = dr["ACC"].ToString(),
                        PACK = dr["PACK"].ToString(),
                        GL_CODE = dr["GL_CODE"].ToString(),
                        TRANSACT = dr["TRANSACT"].ToString(),
                        NAME = dr["TRANSACT"].ToString(),
                        ENTER_DATE = Convert.ToDateTime(dr["ENTER_DATE"].ToString()),
                        NOTE = dr["NOTE"].ToString(),
                        ON_QB = dr["ON_QB"].ToString(),
                        GLPOST_LOG = dr["GLPOST_LOG"].ToString(),
                        CLRD = dr["CLRD"].ToString(),
                        STORE_NO = dr["STORE_NO"].ToString(),
                    });
                }
                accountsPayableModel.lstCheckDetails = lstCheckDetails;
            }

            #endregion

            #region CRD Details List

            if (dtGetccrdDetails != null && dtGetccrdDetails.Rows.Count > 0)
            {
                foreach (DataRow dr in dtGetccrdDetails.Rows)
                {
                    lstCCRDDetails.Add(new CCRDDetails()
                    {
                        LOG_NO = dr["LOG_NO"].ToString(),
                        CC_VENDOR = dr["CC_VENDOR"].ToString(),
                        DATE = Convert.ToDateTime(dr["DATE"].ToString()),
                        AMOUNT = Math.Round(Decimal.Parse(dr["AMOUNT"].ToString()), 2),
                        DISCOUNT = Math.Round(Decimal.Parse(dr["DISCOUNT"].ToString()), 2)
                    });
                }
                accountsPayableModel.lstCCRDDetails = lstCCRDDetails;
            }

            #endregion

            #region Credit Details List

            if (dtGetCreditDetails != null && dtGetCreditDetails.Rows.Count > 0)
            {
                foreach (DataRow dr in dtGetCreditDetails.Rows)
                {
                    lstCreditDetails.Add(new CreditDetails()
                    {
                        Credit = dr["Credit"].ToString(),
                        Note = dr["Note"].ToString(),
                        Date = Convert.ToDateTime(dr["Date"].ToString()),
                        Amount = Math.Round(Decimal.Parse(dr["Amount"].ToString()), 2),
                        PACK_NO = dr["PACK_NO"].ToString(),
                        Addr1 = dr["Addr1"].ToString(),
                        Addr2 = dr["Addr2"].ToString(),
                        city1 = dr["city1"].ToString()
                    });
                }
                accountsPayableModel.lstCreditDetails = lstCreditDetails;
            }

            //lstVendorStatement.Add(lstCreditDetails);

            #endregion
            return accountsPayableModel;
        }


        public DataTable CheckPrintStatus(string strCheckNo, string strBank)
        {
            return _helperCommonService.GetStoreProc("CheckPrintStatus", "@CHECKNO", strCheckNo, "@BANK", strBank);
        }

        #region 608 - List of Bills By Vivek
        public IEnumerable<SelectListItem> VenderTypes1 { get; set; }
        public bool rbBillsListDetailedSummarySelection { get; set; }
        public bool rbBillsListDueInvoiceEntrySelection { get; set; }
        public bool rbBillCamera { get; set; }

        public DataTable ListofBills(string vendorcode1, string vendorcode2, string bill1, string bill2, string dateval, string date1, string date2, decimal amt1, decimal amt2, bool openonly, string glcode, string strSearchOption, bool breakbyterms = false, string store = "", bool includeCredits = false, bool chkAllDates = true)
        {
            return _helperCommonService.GetStoreProc("ListofBills", "@vendorcode1", vendorcode1, "@vendorcode2", vendorcode2, "@bill1", bill1, "@bill2", bill2, "@dateval", dateval.ToString(), "@date1", date1.ToString(), "@date2", date2.ToString(), "@amt1", amt1.ToString(), "@amt2", amt2.ToString(), "@openonly", openonly ? "1" : "0", "@glcode", glcode, "@SearchOption", strSearchOption, "@breakbyterms", breakbyterms.ToString(), "@store", store.ToString(), "@includeCredits", includeCredits ? "1" : "0");
        }

        #endregion

        private string customerAcc;
        public string BillNo { get; set; }
        public string VendorCode { get; set; }
        public string VendorName { get; set; }
        public AccountsPayableModel GetInvoiceNoofBills(string inv_no, string vendor = "")
        {
            AccountsPayableModel accountPayableModel = new AccountsPayableModel();
            string Nbillno = inv_no.Trim().PadLeft(6, ' ');
            DataTable dtBill = CheckValidBillByVendorInvoiceNo(Nbillno, vendor);

            DataRow drBill;
            if (dtBill != null && dtBill.Rows.Count > 0)
            {
                drBill = dtBill.Rows[0];
                DataView dvInvoice = new DataView(dtBill);
                string fmessage = string.Empty;
                string sman1 = "";
                string strVendor = GetVendorNameByCode(Convert.ToString(drBill["ACC"]));
                customerAcc = dtBill.Rows[0]["acc"].ToString().Trim();

                DataTable accinfo = Getsalesmandetails(customerAcc);
                string price = drBill["Price"].ToString();
                if (_helperCommonService.DataTableOK(accinfo))
                    foreach (DataRow dr in accinfo.Rows)
                        sman1 = dr["salesman1"].ToString();


                #region Main Details
                accountPayableModel.BillNo = inv_no;
                accountPayableModel.ACC = dtBill.Rows[0]["ACC"].ToString();
                accountPayableModel.VendorCode = dtBill.Rows[0]["ACC"].ToString();
                accountPayableModel.AMOUNT = Convert.ToDecimal(dtBill.Rows[0]["AMOUNT"].ToString());
                accountPayableModel.date = Convert.ToDateTime(dtBill.Rows[0]["DATE"].ToString());

                accountPayableModel.VND_NO = dtBill.Rows[0]["VND_NO"].ToString();
                accountPayableModel.due_date = Convert.ToDateTime(dtBill.Rows[0]["DUE_DATE"].ToString());
                accountPayableModel.balance = Convert.ToDecimal(dtBill.Rows[0]["BALANCE"].ToString());
                accountPayableModel.ENTER_DATE = dtBill.Rows[0]["ENTER_DATE"].ToString();
                accountPayableModel.pay_terms = dtBill.Rows[0]["PAY_TERMS"].ToString();

                #endregion

                #region Vendor Details List
                if (dtBill != null && dtBill.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtBill.Rows)
                    {
                        lstVendorDetails.Add(new VendorDetails()
                        {
                            Jobbagno = dr["Jobbagno"].ToString(),
                            JobNote = dr["JobNote"].ToString(),
                            COST = Math.Round(Decimal.Parse(dr["COST"].ToString()), 2),
                            PRICE = Math.Round(Decimal.Parse(dr["PRICE"].ToString()), 2),
                            TotalAmount = Math.Round(Decimal.Parse(dr["PCS"].ToString()) * Decimal.Parse(dr["COST"].ToString()), 2),
                            JobbagBill = dr["JobbagBill"].ToString(),
                            Amount_Curr = Math.Round(Decimal.Parse(dr["Amount_Curr"].ToString()), 2),
                            Balance_Curr = Math.Round(Decimal.Parse(dr["Balance_Curr"].ToString()), 2),
                            Shipping_Charge_Curr = Math.Round(Decimal.Parse(dr["Shipping_Charge_Curr"].ToString()), 2),
                            Other_Charge_Curr = Math.Round(Decimal.Parse(dr["Other_Charge_Curr"].ToString()), 2),
                            Discount_Curr = Math.Round(Decimal.Parse(dr["Discount_Curr"].ToString()), 2),
                            Insurance_Curr = Math.Round(Decimal.Parse(dr["Insurance_Curr"].ToString()), 2),
                            BALANCE = dr["BALANCE"].ToString(),
                            INV_NO = dr["INV_NO"].ToString(),
                            ACC = dr["ACC"].ToString(),
                            AMOUNT = Math.Round(Decimal.Parse(dr["AMOUNT"].ToString()), 2),
                            DATE = Convert.ToDateTime(dr["DATE"].ToString()),
                            ENTER_DATE = Convert.ToDateTime(dr["ENTER_DATE"].ToString()),
                            DUE_DATE = Convert.ToDateTime(dr["DUE_DATE"].ToString()),
                            VND_NO = dr["VND_NO"].ToString(),
                            TERM = Math.Round(Decimal.Parse(dr["TERM"].ToString()), 2),
                            //strFormatteddue_date = strFormatteddue_date,
                            NOTE = dr["NOTE"].ToString(),
                            pay_terms = dr["pay_terms"].ToString(),
                            Shipping_Charge = dr["Shipping_Charge"].ToString(),
                            Other_Charge = dr["Other_Charge"].ToString(),
                            Term_Note = dr["Term_Note"].ToString(),
                            Discount = dr["Discount"].ToString(),

                        });
                    }
                    accountPayableModel.lstVendorDetails = lstVendorDetails;
                }
                #endregion

                #region GL Code Log Details List
                DataTable dtGetBillGLDetails = GetBillGLDetails(Nbillno, true);
                if (dtGetBillGLDetails != null && dtGetBillGLDetails.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtGetBillGLDetails.Rows)
                    {
                        lstGLLogDetails.Add(new GLLogDetails()
                        {
                            INV_NO = dr["INV_NO"].ToString(),
                            GL_CODE = dr["GL_CODE"].ToString(),
                            DESC = dr["DESC"].ToString(),
                            AMOUNT = Math.Round(Decimal.Parse(dr["AMOUNT"].ToString()), 2),
                            Not_Editable = dr["Not_Editable"].ToString(),
                            DEPT = dr["DEPT"].ToString(),
                            gl_log = dr["gl_log"].ToString(),
                        });
                    }
                    accountPayableModel.lstGLLogDetails = lstGLLogDetails;
                }

                #endregion

            }
            return accountPayableModel;
        }

        public DataTable CheckValidBillByVendorInvoiceNo(string VendorInvoice, string Vendor)
        {
            return _helperCommonService.GetStoreProc("PRINTBILLBYVENDORINVOICENO", "@VendorInvoice", VendorInvoice, "@Vendor", Vendor);
        }
        public DataTable Getsalesmandetails(string acc)
        {
            return _helperCommonService.GetSqlData("select distinct salesman1 from customer where acc = @acc", "@acc", acc);
        }

        #region Credit Card Transaction
        public List<string> GetCreditCardNames()
        {
            try
            {
                using (SqlCommand dbCommand = new SqlCommand())
                {
                    dbCommand.Connection = _connectionProvider.GetConnection(); // Assuming _helperCommonService.GetConnectionString() gets the connection string
                    dbCommand.CommandType = CommandType.Text;
                    dbCommand.CommandTimeout = 0;

                    dbCommand.CommandText = @"SELECT DISTINCT BIL_CCRD.CC_VENDOR FROM BIL_CCRD WHERE BIL_CCRD.CC_VENDOR IS NOT NULL ORDER BY BIL_CCRD.CC_VENDOR";

                    dbCommand.Connection.Open();

                    List<string> creditCardNames = new List<string>();
                    using (SqlDataReader reader = dbCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            creditCardNames.Add(reader["CC_VENDOR"].ToString());
                        }
                    }

                    dbCommand.Connection.Close();
                    return creditCardNames;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while fetching credit card names: " + ex.Message);
            }
        }

        public List<string> GetStoreCodes(bool activeOnly = false, bool allStores = false, bool withShop = false, bool noText = false)
        {
            try
            {
                DataTable storesData = _helperCommonService.GetStoresDataForSetDefault(activeOnly, allStores, withShop, noText);

                return storesData.AsEnumerable()
                                 .Where(dr => dr["CODE"] != DBNull.Value) // Exclude null values
                                 .Select(dr => dr["CODE"].ToString())     // Convert to string
                                 .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while fetching store codes: " + ex.Message);
            }
        }

        public DataTable GetCcTransactions(string acc, DateTime fromDate, DateTime toDate, string vcode, string store = "")
        {
            try
            {
                using (SqlConnection connection = _connectionProvider.GetConnection())
                using (SqlCommand dbCommand = new SqlCommand())
                {
                    // Set up the command object
                    dbCommand.Connection = connection;
                    dbCommand.CommandType = CommandType.Text;
                    dbCommand.CommandTimeout = 0;

                    // SQL query
                    string queryString = @"
                SELECT 
                    BIL_CCRD.CC_VENDOR,
                    BIL_CCRD.LOG_NO,
                    BILLS.INV_NO, 
                    BILLS.ACC,
                    VENDORS.NAME,
                    BILLS.VND_NO,
                    BIL_CCRD.AMOUNT, 
                    BIL_CCRD.DISCOUNT  
                FROM 
                    BIL_CCRD
                LEFT JOIN 
                    BILLS ON BILLS.INV_NO = BIL_CCRD.BILL_NO 
                LEFT JOIN 
                    VENDORS ON BILLS.ACC = VENDORS.ACC
                WHERE 
                    CAST(BIL_CCRD.DATE AS DATE) BETWEEN @fromDate AND @toDate
                    AND (@store = '' OR BIL_CCRD.STORE_NO = @store)";

                    // Conditional filters for acc and vcode
                    //if (!string.IsNullOrEmpty(acc))
                    //{
                    //    queryString += " AND BIL_CCRD.CC_VENDOR = @acc";
                    //    dbCommand.Parameters.AddWithValue("@acc", acc);
                    //}
                    if (!string.IsNullOrEmpty(@acc))
                    {
                        queryString += " AND LTRIM(RTRIM(BILLS.ACC)) = @acc";
                        dbCommand.Parameters.AddWithValue("@acc", acc);
                    }

                    queryString += " ORDER BY BIL_CCRD.CC_VENDOR, BIL_CCRD.LOG_NO";

                    dbCommand.CommandText = queryString;

                    // Add parameters
                    dbCommand.Parameters.AddWithValue("@fromDate", fromDate);
                    dbCommand.Parameters.AddWithValue("@toDate", toDate);
                    dbCommand.Parameters.AddWithValue("@store", store ?? "");

                    DataTable resultTable = new DataTable();

                    // Execute the query
                    using (SqlDataAdapter adapter = new SqlDataAdapter(dbCommand))
                    {
                        connection.Open();
                        adapter.Fill(resultTable);
                    }

                    return resultTable;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while fetching credit card transactions: " + ex.Message);
            }
        }

        public DataRow CheckValidVendorCode(string acc)
        {
            return _helperCommonService.GetSqlRow("select *  From vendors Where acc=@acc or oldvendorcode=@acc order by acc", "@acc", acc.Trim());
        }

        public DataTable GetLogNo(int Log, DateTime fromDate, DateTime toDate, string ccrdacc, string storeno)
        {
            DataTable dataTable = new DataTable();

            // Define the query with parameterized query to prevent SQL injection
            string queryString = @"
        SELECT BIL_CCRD.CC_VENDOR, BIL_CCRD.LOG_NO, BILLS.INV_NO, BILLS.ACC, 
               VENDORS.NAME, BILLS.VND_NO, bil_ccrd.AMOUNT, bil_ccrd.DISCOUNT  
        FROM bil_ccrd
        LEFT JOIN BILLS ON BILLS.INV_NO = bil_ccrd.BILL_NO 
        LEFT JOIN VENDORS ON BILLS.ACC = VENDORS.ACC
        WHERE CAST(bil_ccrd.DATE AS DATE) BETWEEN CAST(@fromDate AS DATE) AND CAST(@toDate AS DATE) 
        AND bil_ccrd.CC_VENDOR =iif(@ccrdacc='',bil_ccrd.CC_VENDOR,@ccrdacc) AND bil_ccrd.store_no =iif(@storeno='',bil_ccrd.store_no,@storeno) ";

            if (Log > 0)
                queryString += " AND LTRIM(RTRIM(BIL_CCRD.LOG_NO)) = @Log";

            // Using statement ensures resources are cleaned up automatically
            using (SqlConnection connection = _connectionProvider.GetConnection())
            using (SqlCommand command = new SqlCommand(queryString, connection))
            using (SqlDataAdapter dataAdapter = new SqlDataAdapter(command))
            {
                // Add parameters for the query
                command.Parameters.AddWithValue("@fromDate", fromDate);
                command.Parameters.AddWithValue("@toDate", toDate);
                command.Parameters.AddWithValue("@ccrdacc", ccrdacc);
                command.Parameters.AddWithValue("@storeno", storeno);

                // If Log > 0, add the Log parameter to the command
                if (Log > 0)
                    command.Parameters.AddWithValue("@Log", Log);
                // Fill the DataTable with data
                dataAdapter.Fill(dataTable);
            }
            return dataTable;
        }

        public DataTable SearchVendors(bool isCreditCard = false, string attribFilter = "1=1", bool chkCCrd = false)
        {
            DataTable dataTable = new DataTable();
            string query = "";

            // Determine query based on input parameters
            if (chkCCrd)
            {
                if (isCreditCard)
                {
                    query = @"SELECT ACC, NAME, try_cast(TEL as NVARCHAR(30)) as TEL, EMAIL, ADDR11, STATE1, ZIP1, CITY1, TERM, GL_CODE 
                      FROM VENDORS 
                      WHERE IS_CRD = 1 AND " + attribFilter + " ORDER BY ACC";
                }
                else
                {
                    query = @"SELECT ACC, NAME, try_cast(TEL as NVARCHAR(30)) as TEL, EMAIL, ADDR11, STATE1, ZIP1, CITY1, TERM, GL_CODE 
                      FROM VENDORS 
                      WHERE IS_CRD = 0 AND " + attribFilter + " ORDER BY ACC";
                }
            }
            else if (isCreditCard)
            {
                query = @"SELECT ACC, NAME, try_cast(TEL as NVARCHAR(30)) as TEL, EMAIL, ADDR11, STATE1, ZIP1, CITY1, TERM, GL_CODE 
                  FROM VENDORS 
                  WHERE IS_CRD = 1 
                  ORDER BY ACC";
            }
            else
            {
                query = @"SELECT ACC, NAME, try_cast(TEL as NVARCHAR(30)) as TEL, EMAIL, ADDR11, STATE1, ZIP1, CITY1, TERM, GL_CODE 
                  FROM VENDORS 
                  WHERE " + attribFilter + "ORDER BY ACC";
            }

            // Execute query
            using (SqlConnection connection = _connectionProvider.GetConnection())
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Open connection
                    connection.Open();

                    // Fill the DataTable
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(dataTable);
                    }
                }
            }

            return dataTable;
        }

        #endregion

        #region List Of vendor Credits
        public DataTable VendorCredit(string acc, string fromdate, string todate, string store = "")
        {
            return _helperCommonService.GetStoreProc("ListAPCredits", "@cacc", acc, "@date1", fromdate.ToString(), "@date2", todate.ToString(), "@store", store);
        }
        #endregion

        public DataTable ListofConsignments(string vendorcode1, string bill1, string bill2, int dateval, string date1, string date2, decimal amt1, decimal amt2, bool openonly, string glcode, string strSearchOption, bool Chkdetails, string store = "")
        {
            return _helperCommonService.GetStoreProc("ListofConsignments", "@vendorcode1", vendorcode1, "@bill1", bill1, "@bill2", bill2, "@dateval", dateval.ToString(), "@date1", date1.ToString(), "@date2", date2.ToString(), "@amt1", amt1.ToString(),
                                            "@amt2", amt2.ToString(), "@openonly", openonly ? "1" : "0", "@glcode", glcode, "@SearchOption", strSearchOption, "@Chkdetails", Chkdetails ? "1" : "0", "@store", store);
        }

        public DataTable GetListofChecks(string vendorcode1, string check1, string check2, int dateval, DateTime? date1, DateTime? date2, decimal amt1, decimal amt2, string bank, string glcode, bool isAllGL, string strSearchOption, bool unapplied = false, string store = "", bool isincludecredits = false)
        {
            string sqlDate1 = date1.HasValue ? date1.Value.ToString("yyyy-MM-dd HH:mm:ss") : null;
            string sqlDate2 = date2.HasValue ? date2.Value.ToString("yyyy-MM-dd HH:mm:ss") : null;
            return _helperCommonService.GetStoreProc("ListofChecks", "@vendorcode1", vendorcode1, "@check1", check1, "@check2", check2, "@bank", bank, "@dateval", dateval.ToString(),
                "@date1", sqlDate1, "@date2", sqlDate2, "@amt1", amt1.ToString(), "@amt2", amt2.ToString(), "@glcode", glcode, "@isAllGLcode", isAllGL.ToString(), "@SearchOption", strSearchOption, "@unapplied", unapplied.ToString(), "@store", store.ToString(), "@isincludecredits", isincludecredits.ToString());
        }

        public DataTable ListofChecksDetailsShows(string vendorcode1, string check1, string check2, int dateval, DateTime? date1, DateTime? date2, decimal amt1, decimal amt2, string bank, string glcode, string strSearchOption, bool unapplied = false, string store = "", bool isincludecredits = false)
        {
            string sqlDate1 = date1.HasValue ? date1.Value.ToString("yyyy-MM-dd HH:mm:ss") : null;
            string sqlDate2 = date2.HasValue ? date2.Value.ToString("yyyy-MM-dd HH:mm:ss") : null;
            return _helperCommonService.GetStoreProc("ListofCheckShowDetails", "@vendorcode1", vendorcode1, "@check1", check1, "@check2", check2, "@bank", bank, "@dateval", dateval.ToString(),
                "@date1", sqlDate1, "@date2", sqlDate2, "@amt1", amt1.ToString(), "@amt2", amt2.ToString(), "@glcode", glcode, "@SearchOption", strSearchOption, "@unapplied", unapplied.ToString(), "@store", store.ToString(), "@isincludecredits", isincludecredits.ToString());
        }

        public DataTable CheckValidBillNo(string inv_no, string storeNo = "")
        {
            return _helperCommonService.GetSqlData(@"select a.*,b.style, s.IN_STOCK from apm a left join apm_item b on a.inv_no=b.inv_no left join stock s on b.style=s.style where a.inv_no = @inv_no and (s.store_no = @storeNo or @storeNo='') ",
                "@inv_no", inv_no, "@storeNo", storeNo);
        }

        public DataTable CosignmentStyleInstockqty(string inv_no)
        {
            return _helperCommonService.GetSqlData(@"SELECT bi.inv_no, bi.STYLE, BI.PCS QTY,(st.IN_STOCK - st.LAYAWAY- st.IN_SHOP) IN_STOCK 
                FROM APM_ITEM BI with (nolock)
                LEFT JOIN stock st with (nolock) on st.STYLE=dbo.invstyle(BI.STYLE) and st.store_no=bi.store_no  
                where bi.INV_NO=@inv_no order by bi.line_no, BI.STYLE", "@inv_no", inv_no);
        }

        public bool DeleteConsignment(string inv_no, string loggeduser, bool resetcost, out string error, bool IsStyleItem = false)
        {
            error = string.Empty;

            try
            {
                using (var connection = _connectionProvider.GetConnection())
                using (var dbCommand = new SqlCommand("DeleteConsignment", connection))
                {
                    dbCommand.CommandType = CommandType.StoredProcedure;

                    // Add parameters safely
                    dbCommand.Parameters.AddWithValue("@INV_NO", inv_no ?? (object)DBNull.Value);
                    dbCommand.Parameters.AddWithValue("@LOGGEDUSER", loggeduser ?? (object)DBNull.Value);
                    dbCommand.Parameters.AddWithValue("@RESETCOST", resetcost ? 1 : 0);
                    dbCommand.Parameters.AddWithValue("@IsStyleItem", IsStyleItem ? 1 : 0);

                    connection.Open();
                    int rowsAffected = dbCommand.ExecuteNonQuery();

                    return rowsAffected > 0;
                }
            }
            catch (Exception ex)
            {
                error = "Error deleting consignment: " + ex.Message;
                return false;
            }
        }

        public DataTable CheckValidCheckNo(string check_no, string bank)
        {
            string CommandText = "select CHECKS.*, CAST(BANK.CLRD AS BIT) as CLRD1 from checks with (nolock) LEFT JOIN Bank with (nolock) ON TRIM(Bank.inv_no)=TRIM(CHECKS.TRANSACT) WHERE TRIM(checks.bank)= @bank AND TRIM(CHECKS.check_no) = @chk_no";
            return _helperCommonService.GetSqlData(CommandText, "@chk_no", check_no.Trim(), "@bank", bank.Trim());
        }
        public DataTable GetBankAccounts(string MyStore = " ")
        {
            return _helperCommonService.GetSqlData("SELECT * FROM BANK_ACC where @MyStore='' or @MyStore=store_no or store_no='' order by code ", "@MyStore", MyStore);
        }

        public bool CancelCreditCheck(string acc, string invNo, string bank, string checkNo, string loggedUser, out string error)
        {
            error = string.Empty;

            try
            {
                using (SqlConnection connection = _connectionProvider.GetConnection())
                using (SqlCommand command = new SqlCommand("CancelCreditCheck", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandTimeout = 5000;

                    // Add parameters with explicit types
                    command.Parameters.Add(new SqlParameter("@acc", SqlDbType.NVarChar) { Value = acc });
                    command.Parameters.Add(new SqlParameter("@inv_no", SqlDbType.NVarChar) { Value = invNo });
                    command.Parameters.Add(new SqlParameter("@bank", SqlDbType.NVarChar) { Value = bank });
                    command.Parameters.Add(new SqlParameter("@check_no", SqlDbType.NVarChar) { Value = checkNo });
                    command.Parameters.Add(new SqlParameter("@LoggedUser", SqlDbType.NVarChar) { Value = loggedUser });

                    // Open connection and execute command
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();

                    return rowsAffected > 0;
                }
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return false;
            }
        }

        public DataTable GetDetByCCLog(string logno)
        {
            return _helperCommonService.GetSqlData("select * from bil_ccrd where log_no=@log", "@log", logno);
        }

        public DataRow GetInvoiceByInvNo(string invno)
        {
            return (_helperCommonService.GetSqlRow(@"select top 1 i.*,it.memo_no,iSNULL(it.IsSpecialItem,0) IsSpecialItem,it.fpon 
                from invoice i  with (nolock)
                left join (select * from IN_ITEMS with (nolock) where Trimmed_inv_no =@inv_no) it on i.inv_no = it.inv_no 
                    Where trim(i.inv_no) = @inv_no", "@inv_no", invno.Trim()));
        }
        public bool DELCCLOG(string LOGNO, out string error)
        {
            error = string.Empty;
            try
            {
                using (SqlCommand dbCommand = new SqlCommand())
                {
                    // Set the command object properties
                    dbCommand.Connection = _connectionProvider.GetConnection();
                    dbCommand.CommandType = CommandType.StoredProcedure;
                    dbCommand.CommandText = "DELETECCLOG";

                    // Add the input parameter to the parameter collection
                    dbCommand.Parameters.AddWithValue("@LOGNO", LOGNO);
                    dbCommand.Connection.Open();
                    var rowsAffected = dbCommand.ExecuteNonQuery();
                    dbCommand.Connection.Close();

                    return rowsAffected > 0;
                }
            }
            catch (Exception ex)
            {
                error = ex.Message.Split(new char[] { '\r', '\n' })[0];
                return false;
            }
        }
        public bool ApplyACheck(string apCredit, string apCreditGL, decimal enteredCheckAmt, string vendorCode,
            out string packNo, out string error, string store, string loggedUser)
        {
            packNo = error = string.Empty;

            using (var connection = _connectionProvider.GetConnection())
            using (var command = new SqlCommand("ApplyACheck", connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                // Add parameters
                command.Parameters.AddWithValue("@APCredit", apCredit);
                command.Parameters.AddWithValue("@APCreditGL", apCreditGL);
                command.Parameters.AddWithValue("@EnteredCheckAmt", enteredCheckAmt);
                command.Parameters.AddWithValue("@VENDORCODE", vendorCode.Trim());
                command.Parameters.AddWithValue("@store", store);
                command.Parameters.AddWithValue("@loggeduser", loggedUser);

                // Output parameter
                var outLogNo = new SqlParameter("@RETVAL", SqlDbType.NVarChar, 10)
                {
                    Direction = ParameterDirection.Output
                };
                command.Parameters.Add(outLogNo);

                // Set command timeout (if needed, set a specific value rather than 0)
                command.CommandTimeout = 5000;

                // Open the connection, execute the query, and retrieve the output
                connection.Open();
                var rowsAffected = command.ExecuteNonQuery();
                packNo = outLogNo.Value.ToString();
                return rowsAffected > 0;
            }
        }
        public int dbChangecheckNoBank(string oldCheckNo, string newCheckNo, string oldBank, string newBank, string glDetails = "")
        {
            using (SqlConnection connection = _connectionProvider.GetConnection())
            using (SqlCommand command = new SqlCommand("ChangeCheck_Bank", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandTimeout = 5000;

                // Add parameters with explicit types
                command.Parameters.Add(new SqlParameter("@OldCheckNo", SqlDbType.NVarChar) { Value = oldCheckNo.Trim() });
                command.Parameters.Add(new SqlParameter("@OldBank", SqlDbType.NVarChar) { Value = oldBank });
                command.Parameters.Add(new SqlParameter("@NewCheckNo", SqlDbType.NVarChar) { Value = newCheckNo.Trim() });
                command.Parameters.Add(new SqlParameter("@NewBank", SqlDbType.NVarChar) { Value = newBank });
                command.Parameters.Add(new SqlParameter("@gldetails", SqlDbType.Xml) { Value = glDetails });

                // Open connection, execute command, and retrieve the result
                connection.Open();
                return Convert.ToInt32(command.ExecuteScalar());
            }
        }
        public int dbUpdatecheckNoInfo(string checkNo, string bank, decimal amount, string note, string acc, DateTime date,
           string glDetails = "")
        {
            using (var connection = _connectionProvider.GetConnection())
            using (var dbCommand = new SqlCommand("UpdateCheckInfo", connection))
            {
                // Set command type and timeout
                dbCommand.CommandType = CommandType.StoredProcedure;
                dbCommand.CommandTimeout = 3000;

                // Add parameters
                dbCommand.Parameters.AddWithValue("@CheckNo", checkNo.Trim());
                dbCommand.Parameters.AddWithValue("@bank", bank);
                dbCommand.Parameters.AddWithValue("@amount", amount);
                dbCommand.Parameters.AddWithValue("@acc", acc);
                dbCommand.Parameters.AddWithValue("@date", date);
                dbCommand.Parameters.AddWithValue("@note", note);

                dbCommand.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@gldetails",
                    SqlDbType = SqlDbType.Xml,
                    Value = glDetails
                });

                // Open the connection, execute the query, and return the result
                connection.Open();
                return Convert.ToInt32(dbCommand.ExecuteScalar());
            }
        }
        public DataRow GetVendorMax(Int64 minval, Int64 maxval)
        {
            return _helperCommonService.GetSqlRow(@"SELECT Isnull(NULLIF(( CASE WHEN Max(dbo.Getfirstnumeric(acc)) <  try_convert(int, @Min)  THEN @min ELSE Max(dbo.Getfirstnumeric(acc)) END),  try_convert(int, @Min)),  try_convert(int, @Min)) AS acc FROM VENDORS with (nolock) WHERE Isnull(dbo.Getnonnumeric(acc), 0) <> 0 AND try_convert(int, acc) IS NOT NULL AND Isnull(dbo.Getnonnumeric(acc), 0) < try_convert(int, @Max)", "@min", minval.ToString(), "@Max", maxval.ToString());
        }
        public bool AddVendor(VendorModel vendor)
        {
            using (SqlCommand dbCommand = new SqlCommand())
            {
                // Set the command object properties
                dbCommand.Connection = _connectionProvider.GetConnection();
                dbCommand.CommandType = CommandType.StoredProcedure;
                dbCommand.CommandText = "AddVendor";

                dbCommand.Parameters.AddWithValue("@ACC", vendor.ACC ?? "");
                dbCommand.Parameters.AddWithValue("@NAME", vendor.NAME ?? "");
                dbCommand.Parameters.AddWithValue("@TEL", vendor.TEL ?? 0);
                dbCommand.Parameters.AddWithValue("@TEL2", vendor.TEL2 ?? 0);
                dbCommand.Parameters.AddWithValue("@FAX", vendor.FAX ?? 0);
                dbCommand.Parameters.AddWithValue("@ADDR11", vendor.ADDR11 ?? "");
                dbCommand.Parameters.AddWithValue("@ADDR12", vendor.ADDR12 ?? "");
                dbCommand.Parameters.AddWithValue("@CITY1", vendor.CITY1 ?? "");
                dbCommand.Parameters.AddWithValue("@STATE1", vendor.STATE1 ?? "");
                dbCommand.Parameters.AddWithValue("@ZIP1", vendor.ZIP1 ?? "");
                dbCommand.Parameters.AddWithValue("@COUNTRY", vendor.COUNTRY ?? "");
                dbCommand.Parameters.AddWithValue("@ADDR21", vendor.ADDR21 ?? "");
                dbCommand.Parameters.AddWithValue("@ADDR22", vendor.ADDR22 ?? "");
                dbCommand.Parameters.AddWithValue("@CITY2", vendor.CITY2 ?? "");
                dbCommand.Parameters.AddWithValue("@STATE2", vendor.STATE2 ?? "");
                dbCommand.Parameters.AddWithValue("@ZIP2", vendor.ZIP2 ?? "");
                dbCommand.Parameters.AddWithValue("@CONTACT", vendor.CONTACT ?? "");
                dbCommand.Parameters.AddWithValue("@DNB", vendor.DNB ?? "");
                dbCommand.Parameters.AddWithValue("@SOUNDX", vendor.SOUNDX ?? "");
                dbCommand.Parameters.AddWithValue("@GL_CODE", vendor.GL_CODE ?? "");
                dbCommand.Parameters.AddWithValue("@LiabilityGL_CODE", vendor.gl_ap ?? "");
                dbCommand.Parameters.AddWithValue("@NOTE", vendor.NOTE ?? "");
                dbCommand.Parameters.AddWithValue("@TERM", vendor.TERM ?? 0);
                dbCommand.Parameters.AddWithValue("@NOTE1", vendor.NOTE1 ?? "");
                dbCommand.Parameters.AddWithValue("@EMAIL", vendor.EMAIL ?? "");
                dbCommand.Parameters.AddWithValue("@WEBSITE", vendor.WEBSITE ?? "");
                dbCommand.Parameters.AddWithValue("@OS_TEL", vendor.OS_TEL1 ?? "0");
                dbCommand.Parameters.AddWithValue("@OS_TEL2", vendor.OS_TEL2 ?? "0");
                dbCommand.Parameters.AddWithValue("@CASTER", vendor.CASTER ?? false);
                dbCommand.Parameters.AddWithValue("@FAX_EMAIL", vendor.FAX_EMAIL ?? 0);
                dbCommand.Parameters.AddWithValue("@CAST_COPY", vendor.CAST_COPY ?? 0);
                dbCommand.Parameters.AddWithValue("@CAST_EMAIL", vendor.CAST_EMAIL ?? "");
                dbCommand.Parameters.AddWithValue("@CAST_FAX", vendor.CAST_FAX ?? "");
                dbCommand.Parameters.AddWithValue("@FIN_VND", vendor.FIN_VND ?? false);
                dbCommand.Parameters.AddWithValue("@OS_FAX", vendor.OS_FAX ?? "");
                dbCommand.Parameters.AddWithValue("@OUR_ACCT", vendor.OUR_ACCT ?? "");
                dbCommand.Parameters.AddWithValue("@ON_QB", vendor.ON_QB ?? false);
                dbCommand.Parameters.AddWithValue("@IS_CRD", vendor.IS_CRD ?? false);

                dbCommand.Parameters.AddWithValue("@Cellno", vendor.Cellno ?? "");
                dbCommand.Parameters.AddWithValue("@Whatsappno", vendor.Whatsappno ?? "");
                dbCommand.Parameters.AddWithValue("@GroupAccno", vendor.GroupAccno ?? "");
                dbCommand.Parameters.AddWithValue("@Groupname", vendor.Groupname ?? "");
                dbCommand.Parameters.AddWithValue("@GroupAccno1", vendor.GroupAccno1 ?? "");
                dbCommand.Parameters.AddWithValue("@Groupname1", vendor.Groupname1 ?? "");
                dbCommand.Parameters.AddWithValue("@Userid", vendor.Userid ?? "");
                dbCommand.Parameters.AddWithValue("@Password", vendor.Password ?? "");
                dbCommand.Parameters.AddWithValue("@OldVendorcode", vendor.OldVendorcode ?? "");

                dbCommand.Parameters.AddWithValue("@HAS_RECURRING", vendor.HAS_RECURRING ?? false);
                dbCommand.Parameters.AddWithValue("@recurring_day", vendor.recurring_day);
                dbCommand.Parameters.AddWithValue("@recur_last_bill", vendor.recur_last_bill);
                dbCommand.Parameters.AddWithValue("@recur_final_bill", vendor.recur_final_bill);
                dbCommand.Parameters.AddWithValue("@recur_gl_code1", vendor.recur_gl_code1 ?? "");
                dbCommand.Parameters.AddWithValue("@recur_gl_code2", vendor.recur_gl_code2 ?? "");
                dbCommand.Parameters.AddWithValue("@recur_gl_code3", vendor.recur_gl_code3 ?? "");
                dbCommand.Parameters.AddWithValue("@recur_gl_code4", vendor.recur_gl_code4 ?? "");
                dbCommand.Parameters.AddWithValue("@recur_amount1", vendor.recur_amount1 ?? 0);
                dbCommand.Parameters.AddWithValue("@recur_amount2", vendor.recur_amount2 ?? 0);
                dbCommand.Parameters.AddWithValue("@recur_amount3", vendor.recur_amount3 ?? 0);
                dbCommand.Parameters.AddWithValue("@recur_amount4", vendor.recur_amount4 ?? 0);
                dbCommand.Parameters.AddWithValue("@IS1099", vendor.Is1099);
                dbCommand.Parameters.AddWithValue("@TAX_ID", vendor.TAX_ID ?? "");
                dbCommand.Parameters.AddWithValue("@MultiAttr1Val", vendor.MultiAttr1Val ?? "");
                dbCommand.Parameters.AddWithValue("@NAME3", vendor.NAME3 ?? "");
                dbCommand.Parameters.AddWithValue("@ADDR31", vendor.ADDR31 ?? "");
                dbCommand.Parameters.AddWithValue("@ADDR32", vendor.ADDR32 ?? "");
                dbCommand.Parameters.AddWithValue("@ADDR33", vendor.ADDR33 ?? "");
                dbCommand.Parameters.AddWithValue("@Depositonly", vendor.Depositonly ?? false);
                dbCommand.Parameters.AddWithValue("@SalesrepTel", vendor.SalesrepTel ?? "");
                dbCommand.Parameters.AddWithValue("@PAY_BY_GOLD", vendor.PAY_BY_GOLD ?? false);
                dbCommand.Parameters.AddWithValue("@PRIVATE_SLLER", vendor.PRIVATE_SLLER ?? false);
                dbCommand.Parameters.AddWithValue("@Vndno", vendor.Vndno ?? "");
                dbCommand.Parameters.AddWithValue("@VndMk", vendor.VndMarkup ?? 0);
                dbCommand.Parameters.AddWithValue("@Setter", vendor.Setter ?? "");
                dbCommand.Parameters.AddWithValue("@NoOfDay1", vendor.NoOfDay1);
                dbCommand.Parameters.AddWithValue("@DiscPercent1", vendor.DiscPercent1);
                dbCommand.Parameters.AddWithValue("@Extension", vendor.Extension);

                // Open the connection, execute the query and close the connection
                dbCommand.Connection.Open();
                var rowsAffected = dbCommand.ExecuteNonQuery();
                dbCommand.Connection.Close();

                return rowsAffected > 0;
            }
        }

        public DataRow CheckAttrExists(string attr)
        {
            return _helperCommonService.GetSqlRow("select *  From VEN_ATTR Where trim(attr_val)=@attr  order by attr_val", "@attr", attr.Trim());
        }
        public bool AddVenAttr(string attr)
        {
            using (var connection = _connectionProvider.GetConnection())
            using (var command = new SqlCommand("INSERT INTO VEN_ATTR (ATTR_NUM, ATTR_VAL) VALUES (1, @attr)", connection))
            {
                command.CommandType = CommandType.Text;
                command.Parameters.AddWithValue("@attr", attr);

                connection.Open();
                return command.ExecuteNonQuery() > 0;
            }
        }

        public bool UpdateVenAttr(string attr, string oldAttr)
        {
            using (var connection = _connectionProvider.GetConnection())
            using (var command = new SqlCommand("UPDATE VEN_ATTR SET ATTR_VAL = @newAttr WHERE ATTR_VAL = @oldAttr", connection))
            {
                command.CommandType = CommandType.Text;
                command.Parameters.AddWithValue("@newAttr", attr);
                command.Parameters.AddWithValue("@oldAttr", oldAttr);

                connection.Open();
                return command.ExecuteNonQuery() > 0;
            }
        }
        public DataTable GetStateCityByZip(string zipode)
        {
            return _helperCommonService.GetSqlData(@"Select city,state from zipcodes with(nolock) where zipcode=@ZIP", "@ZIP", zipode);
        }
        public DataRow CheckoldVendorCode(string acc)
        {
            return _helperCommonService.GetSqlRow("select *  From vendors with (nolock) Where oldvendorcode=@acc  order by oldvendorcode", "@acc", acc.Trim());
        }
        public List<SelectListItem> GetAllSettersForGrid()
        {
            DataTable dtsets = _helperCommonService.GetSqlData("select name from setters with (nolock) where inactive=0 order by name asc");


            List<SelectListItem> AllSetters = dtsets.AsEnumerable()
            .Select(row => new SelectListItem
            {
                Value = row.Field<string>("name"),
                Text = row.Field<string>("name")
            })
            .ToList();

            return AllSetters;
        }
        public DataTable CheckValidSetter(string setter)
        {
            return _helperCommonService.GetSqlData("select name from setters with (nolock) where inactive=0 and trim([name])=@setter", "@setter", setter.Trim());

        }
        public bool UpdateVendor(VendorModel vendor)
        {
            using (SqlCommand dbCommand = new SqlCommand())
            {
                // Set the command object properties
                dbCommand.Connection = _connectionProvider.GetConnection();
                dbCommand.CommandType = CommandType.StoredProcedure;
                dbCommand.CommandText = "UpdateVendor";

                dbCommand.Parameters.AddWithValue("@ACC", vendor.ACC ?? "");
                dbCommand.Parameters.AddWithValue("@NAME", vendor.NAME ?? "");
                dbCommand.Parameters.AddWithValue("@TEL", vendor.TEL ?? 0);
                dbCommand.Parameters.AddWithValue("@TEL2", vendor.TEL2 ?? 0);
                dbCommand.Parameters.AddWithValue("@FAX", vendor.FAX ?? 0);
                dbCommand.Parameters.AddWithValue("@ADDR11", vendor.ADDR11 ?? "");
                dbCommand.Parameters.AddWithValue("@ADDR12", vendor.ADDR12 ?? "");
                dbCommand.Parameters.AddWithValue("@CITY1", vendor.CITY1 ?? "");
                dbCommand.Parameters.AddWithValue("@STATE1", vendor.STATE1 ?? "");
                dbCommand.Parameters.AddWithValue("@ZIP1", vendor.ZIP1 ?? "");
                dbCommand.Parameters.AddWithValue("@COUNTRY", vendor.COUNTRY ?? "");
                dbCommand.Parameters.AddWithValue("@ADDR21", vendor.ADDR21 ?? "");
                dbCommand.Parameters.AddWithValue("@ADDR22", vendor.ADDR22 ?? "");
                dbCommand.Parameters.AddWithValue("@CITY2", vendor.CITY2 ?? "");
                dbCommand.Parameters.AddWithValue("@STATE2", vendor.STATE2 ?? "");
                dbCommand.Parameters.AddWithValue("@ZIP2", vendor.ZIP2 ?? "");
                dbCommand.Parameters.AddWithValue("@CONTACT", vendor.CONTACT ?? "");
                dbCommand.Parameters.AddWithValue("@DNB", vendor.DNB ?? "");
                dbCommand.Parameters.AddWithValue("@SOUNDX", vendor.SOUNDX ?? "");
                dbCommand.Parameters.AddWithValue("@GL_CODE", vendor.GL_CODE ?? "");
                dbCommand.Parameters.AddWithValue("@LiabilityGL_CODE", vendor.gl_ap ?? "");
                dbCommand.Parameters.AddWithValue("@NOTE", vendor.NOTE ?? "");
                dbCommand.Parameters.AddWithValue("@TERM", vendor.TERM ?? 0);
                dbCommand.Parameters.AddWithValue("@NOTE1", vendor.NOTE1 ?? "");
                dbCommand.Parameters.AddWithValue("@EMAIL", vendor.EMAIL ?? "");
                dbCommand.Parameters.AddWithValue("@WEBSITE", vendor.WEBSITE ?? "");
                dbCommand.Parameters.AddWithValue("@OS_TEL", vendor.OS_TEL1 ?? "");
                dbCommand.Parameters.AddWithValue("@OS_TEL2", vendor.OS_TEL2 ?? "");
                dbCommand.Parameters.AddWithValue("@CASTER", vendor.CASTER ?? false);
                dbCommand.Parameters.AddWithValue("@FAX_EMAIL", vendor.FAX_EMAIL ?? 0);
                dbCommand.Parameters.AddWithValue("@CAST_COPY", vendor.CAST_COPY ?? 0);
                dbCommand.Parameters.AddWithValue("@CAST_EMAIL", vendor.CAST_EMAIL ?? "");
                dbCommand.Parameters.AddWithValue("@CAST_FAX", vendor.CAST_FAX ?? "");
                dbCommand.Parameters.AddWithValue("@FIN_VND", vendor.FIN_VND ?? false);
                dbCommand.Parameters.AddWithValue("@OS_FAX", vendor.OS_FAX ?? "");
                dbCommand.Parameters.AddWithValue("@OUR_ACCT", vendor.OUR_ACCT ?? "");
                dbCommand.Parameters.AddWithValue("@ON_QB", vendor.ON_QB ?? false);
                dbCommand.Parameters.AddWithValue("@IS_CRD", vendor.IS_CRD ?? false);

                dbCommand.Parameters.AddWithValue("@Cellno", vendor.Cellno ?? "");
                dbCommand.Parameters.AddWithValue("@Whatsappno", vendor.Whatsappno ?? "");
                dbCommand.Parameters.AddWithValue("@GroupAccno", vendor.GroupAccno ?? "");
                dbCommand.Parameters.AddWithValue("@Groupname", vendor.Groupname ?? "");
                dbCommand.Parameters.AddWithValue("@GroupAccno1", vendor.GroupAccno1 ?? "");
                dbCommand.Parameters.AddWithValue("@Groupname1", vendor.Groupname1 ?? "");
                dbCommand.Parameters.AddWithValue("@Userid", vendor.Userid ?? "");
                dbCommand.Parameters.AddWithValue("@Password", vendor.Password ?? "");
                dbCommand.Parameters.AddWithValue("@OldVendorcode", vendor.OldVendorcode ?? "");

                dbCommand.Parameters.AddWithValue("@HAS_RECURRING", vendor.HAS_RECURRING ?? false);
                dbCommand.Parameters.AddWithValue("@recurring_day", vendor.recurring_day);
                dbCommand.Parameters.AddWithValue("@recur_last_bill", vendor.recur_last_bill);
                dbCommand.Parameters.AddWithValue("@recur_final_bill", vendor.recur_final_bill);
                dbCommand.Parameters.AddWithValue("@recur_gl_code1", vendor.recur_gl_code1 ?? "");
                dbCommand.Parameters.AddWithValue("@recur_gl_code2", vendor.recur_gl_code2 ?? "");
                dbCommand.Parameters.AddWithValue("@recur_gl_code3", vendor.recur_gl_code3 ?? "");
                dbCommand.Parameters.AddWithValue("@recur_gl_code4", vendor.recur_gl_code4 ?? "");
                dbCommand.Parameters.AddWithValue("@recur_amount1", vendor.recur_amount1 ?? 0);
                dbCommand.Parameters.AddWithValue("@recur_amount2", vendor.recur_amount2 ?? 0);
                dbCommand.Parameters.AddWithValue("@recur_amount3", vendor.recur_amount3 ?? 0);
                dbCommand.Parameters.AddWithValue("@recur_amount4", vendor.recur_amount4 ?? 0);
                dbCommand.Parameters.AddWithValue("@IS1099", vendor.Is1099);
                dbCommand.Parameters.AddWithValue("@TAX_ID", vendor.TAX_ID ?? "");
                dbCommand.Parameters.AddWithValue("@MultiAttr1Val", vendor.MultiAttr1Val ?? "");
                dbCommand.Parameters.AddWithValue("@NAME3", vendor.NAME3 ?? "");
                dbCommand.Parameters.AddWithValue("@ADDR31", vendor.ADDR31 ?? "");
                dbCommand.Parameters.AddWithValue("@ADDR32", vendor.ADDR32 ?? "");
                dbCommand.Parameters.AddWithValue("@ADDR33", vendor.ADDR33 ?? "");
                dbCommand.Parameters.AddWithValue("@Depositonly", vendor.Depositonly ?? false);
                dbCommand.Parameters.AddWithValue("@SalesrepTel", vendor.SalesrepTel ?? "");
                dbCommand.Parameters.AddWithValue("@PAY_BY_GOLD", vendor.PAY_BY_GOLD ?? false);
                dbCommand.Parameters.AddWithValue("@PRIVATE_SLLER", vendor.PRIVATE_SLLER ?? false);

                dbCommand.Parameters.AddWithValue("@Vndno", vendor.Vndno ?? "");
                dbCommand.Parameters.AddWithValue("@VndMk", vendor.VndMarkup ?? 0);
                dbCommand.Parameters.AddWithValue("@Setter", vendor.Setter ?? "");
                dbCommand.Parameters.AddWithValue("@NoOfDay1", vendor.NoOfDay1);
                dbCommand.Parameters.AddWithValue("@DiscPercent1", vendor.DiscPercent1);
                dbCommand.Parameters.AddWithValue("@Extension", vendor.Extension ?? "");
                // Open the connection, execute the query and close the connection
                dbCommand.Connection.Open();
                var rowsAffected = dbCommand.ExecuteNonQuery();
                dbCommand.Connection.Close();

                return rowsAffected > 0;
            }
        }
        public bool IssueCheckForAVendor(DataTable tblAPCredit, DataTable tblAPCheck, string vendorcode, out string packno, out string error, string cUser = "", string storecode = "", bool paybygold = false)
        {
            packno = error = string.Empty;

            try
            {
                using (var connection = _connectionProvider.GetConnection())
                using (var dbCommand = new SqlCommand("IssueCheckForAVendor", connection))
                {
                    // Set the command type and timeout
                    dbCommand.CommandType = CommandType.StoredProcedure;
                    dbCommand.CommandTimeout = 5000;

                    // Add structured parameters
                    dbCommand.Parameters.Add(new SqlParameter("@TBLAPCREDIT", SqlDbType.Structured) { Value = tblAPCredit });
                    dbCommand.Parameters.Add(new SqlParameter("@TBLAPCHECK", SqlDbType.Structured) { Value = tblAPCheck });

                    // Add scalar parameters
                    dbCommand.Parameters.AddWithValue("@VENDORCODE", vendorcode);
                    dbCommand.Parameters.AddWithValue("@loggeduser", cUser);
                    dbCommand.Parameters.AddWithValue("@storecode", storecode);
                    dbCommand.Parameters.AddWithValue("@paybygold", paybygold);

                    // Add output parameter
                    var outLogNo = new SqlParameter("@RETVAL", SqlDbType.NVarChar, 10)
                    {
                        Direction = ParameterDirection.Output
                    };
                    dbCommand.Parameters.Add(outLogNo);

                    // Open the connection and execute the command
                    connection.Open();
                    int rowsAffected = dbCommand.ExecuteNonQuery();

                    // Retrieve the output parameter value
                    packno = outLogNo.Value as string ?? string.Empty;

                    return rowsAffected > 0;
                }
            }
            catch (Exception ex)
            {
                error = ex.Message;
                packno = string.Empty;
                return false;
            }
        }
        public bool PayBillByCC(string billsString, string vendorcode, string ccvendor, DateTime ccdate, decimal ccAmount, string storeno, out string out_no, out string error, string LoggedUser = "")
        {
            // Initialize output variables
            out_no = error = string.Empty;

            const string storedProcedureName = "BILLPAYBYUSINGCREDITCARD";

            using (var connection = _connectionProvider.GetConnection())
            using (var command = new SqlCommand(storedProcedureName, connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandTimeout = 5000;

                // Add input parameters
                command.Parameters.Add(new SqlParameter("@bilXmlData", SqlDbType.Xml) { Value = billsString });
                command.Parameters.Add(new SqlParameter("@VENDORCODE", SqlDbType.NVarChar) { Value = vendorcode });
                command.Parameters.Add(new SqlParameter("@CCVENDOR", SqlDbType.NVarChar) { Value = ccvendor });
                command.Parameters.Add(new SqlParameter("@CCDATE", SqlDbType.DateTime) { Value = ccdate });
                command.Parameters.Add(new SqlParameter("@CCAMOUNT", SqlDbType.Decimal) { Value = ccAmount });
                command.Parameters.Add(new SqlParameter("@STORENO", SqlDbType.NVarChar) { Value = storeno });
                command.Parameters.Add(new SqlParameter("@LoggedUser", SqlDbType.NVarChar) { Value = LoggedUser ?? (object)DBNull.Value });

                // Add output parameter
                var outLogNo = new SqlParameter("@RETVAL", SqlDbType.NVarChar, 6) { Direction = ParameterDirection.Output };
                command.Parameters.Add(outLogNo);

                // Execute the command
                connection.Open();
                var rowsAffected = command.ExecuteNonQuery();
                out_no = outLogNo.Value as string;

                return rowsAffected > 0;
            }
        }
        public bool PayBillAllByCCRD(string billsString, string ccrdsString, string storeno, out string outNo,
            out string error, string loggedUser = "")
        {
            outNo = string.Empty;
            error = string.Empty;

            try
            {
                using (SqlConnection connection = _connectionProvider.GetConnection())
                using (SqlCommand command = new SqlCommand("BILLPAYALLBYUSINGCCRD", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandTimeout = 5000;

                    // Add parameters with explicit types
                    command.Parameters.Add(new SqlParameter("@billXmlData", SqlDbType.Xml) { Value = billsString });
                    command.Parameters.Add(new SqlParameter("@ccrdXmlData", SqlDbType.Xml) { Value = ccrdsString });
                    command.Parameters.Add(new SqlParameter("@STORENO", SqlDbType.NVarChar) { Value = storeno });
                    command.Parameters.Add(new SqlParameter("@LoggedUser", SqlDbType.NVarChar) { Value = loggedUser });

                    // Output parameter
                    SqlParameter outLogNo = new SqlParameter("@RETVAL", SqlDbType.NVarChar, 10000)
                    {
                        Direction = ParameterDirection.Output
                    };
                    command.Parameters.Add(outLogNo);

                    // Open the connection and execute the command
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();

                    // Retrieve the output parameter value
                    outNo = Convert.ToString(outLogNo.Value);

                    return rowsAffected > 0;
                }
            }
            catch (Exception ex)
            {
                error = ex.Message;
                outNo = string.Empty;
                return false;
            }
        }
    }
}
