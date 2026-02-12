//-- Neetha    04/29/2025 Createed New controller.//
//-- Neetha    05/06/2025 Changes in alert messages.//
//-- Neetha    05/09/2025 Added CancelLaway Methods.//
//   Javid     05/09/2025 Fix build error
//   Neetha    05/13/2025 Changes in deletelayaway,cancelLaway methods, removed unnecessary code.
//   Neetha    05/15/2025 Popup format for CancelLayawayPayments invoice cancelling.
//   Neetha    05/19/2025 Store dropdown changes in CancelLayawayPayments and CancelLayaway methods.
//-- Neetha    05/28/2025 Added List of all layaways methods.//
//-- Neetha    06/04/2025 Added DeleteNotes, DeleteNote, ShowCustomerNotes, ShowPotentialCustomerNotes, openPotentialCustNotes, getFollowUpTypes, openAddEditType, AddType, UpdateType, DeleteType, ShowType, AddTypes, UpdateTypes, DeleteTypes, ShowTypes, SaveNotes, PreviewCustNotes methods.
//-- Manoj     06/09/2025 Added PastDueLayawayPayments
//-- Manoj     06/10/2025 Added GetPastDueLayawayPayments,GetPastDueLayawayPaymentsSamad,GetListOfPastDueLayawayPayments Models
//-- Manoj     06/12/2025 Added SendPastDueLaywayPaymentsEmail,SendPastDueLaywayPaymentsSMS,UpdateCustMaintenance Methods 
//-- Manoj     06/16/2025 Fixed Issues on GetListOfPastDueLayawayPayments for adding dublicate records on response 
//-- Manoj     06/17/2025 Added GetAllListofLayawayPayments,ListofLayawayPayments,GetListofLayawayPayments Methods
//-- Manoj     06/18/2025 Fixed issues on GetListOfPastDueLayawayPayments for adding dublicate records on response 
//-- Manoj     06/19/2025 Added additionality functionality in GetListOfPastDueLayawayPayments for Layaway Payments That are Due
//-- Manoj     06/23/2025 Fixed issues on PastDueLayawayPayments for GetListOfPastDueLayawayPayments funcionality
//   Phanindra 06/24/2025 Added LayawayPayments, GetLayawayPayments, LoadPaymentFormData, PaymentLayawayRow, SaveLaywaypayments, iSEnoughStock
//-- Manoj     06/27/2025 Fixed Isuues on Report Headings Store Details on PastDueLayawayPayments and GetAllListofLayawayPayments
//   Phanindra 07/20/2025 Modified SaveLaywaypayments and LoadPaymentFormData methods
//   Phanindra 08/01/2025 Modified storecodeinuse to use session value.
//   Phanindra 08/14/2025 Updated SaveLaywaypayments method and added fields in PaymentLayawayRow
//   Phanindra 08/19/2025 Fixed iSPicked confirmation issue in SaveLayawayPayment method.
//   Phanindra 08/26/2025 Removed conditions from SaveLaywaypayments and kept them in html page
//   Phanindra 09/14/2025 Worked on SaveLaywaypayments method for fixing save related issues.
//   Phanindra 09/21/2025 Worked on SaveLaywaypayments to add the functionality from Refund Repair Order form
//   Phanindra 10/03/2025 modified SaveLaywaypayments method.
//   Phanindra 10/16/2025 added public to loadall method
//   Manoj     10/24/2025 Added LayawayFollowup,GetListofAllLayawaysforFollowup,GetListofLayawaysforFollowup,SaveUpdateNoCalltext methods
//   Manoj     10/25/2025 Added UpdateNoCalltext,getLayawaySMSHistory,GetLayawayfollowupSMS,getListOfLayawayHistory,getListOfLayawayFollowUpNotes,GetLayawayfollowupNotes,DeleteLayawayfollowupNote methods
//   Manoj     10/27/2025 Adde getLaywayFollowUpAddNotes,SaveLayawayfollowupNotes methods
//   Manoj     12/03/2025 Added  New parameter for SaveLaywaypayments for cancel posted data 
//   Hemanth   01/30/2026 Added SpecialOrderPayments,GetSpecialOrderPayments,extra parameter in SaveLaywaypayments
//   Phanindra 02/11/2026 Converted to YJCore.

using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using System.Collections;
using System.Data;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using YJWebCoreMVC.Models;
using YJWebCoreMVC.Services;

namespace YJWebCoreMVC.Controllers
{
    public class SalesLayawaysController : Controller
    {
        private DataRow invoiceRow, customerRow, upsInsRow, drMasterDetail;
        private string acc = string.Empty;
        DataSet dsInitData = new DataSet();
        private DataTable dtSubReport = null, DrMasterDetail2, dtNotes, dtUsers, tradeInData = null, dtEmiRcvable = null, dtSubReport2, dtMasterDetail,
            dtSubReportAlexPaymet, dt, dt1, data, dtPayment, dtSubReportImages, dtrepcostdata = new DataTable(), dtStores;
        //InvoiceModel objModel = new InvoiceModel();
        //bool iSSpecialOrder = true;
        string LoggedUser = "ADMIN", Cash_Register = "", StoreCode = "";
        public string NoChangeBefore = "";
        string FixedStoreCode = "";
        byte[] oldinvoicestream;
        bool isclosing = false, is_repair = false;
        int InvoiceLen = 6;
        SqlConnection conninvoice;
        private string errorMessage = string.Empty, cdate = string.Empty;
        private decimal decAlreadyDonePaymnet = 0;
        string accname;
        bool blnIsPotentialCust = false, isSaved = false, isClosing = false;

        private readonly HelperCommonService _helperCommonService;
        private readonly ConnectionProvider _connectionProvider;
        private readonly SalesLayAwaysService _salesLayAwaysService;
        private readonly InvoiceService _invoiceService;
        private readonly CustomerService _customerService;
        private readonly OrderRepairService _orderRepairService;
        public SalesLayawaysController(HelperCommonService helperCommonService, ConnectionProvider connectionProvider, SalesLayAwaysService salesLayAwaysService, InvoiceService invoiceService, CustomerService customerService, OrderRepairService orderRepairService)
        {
            _helperCommonService = helperCommonService;
            _connectionProvider = connectionProvider;
            _salesLayAwaysService = salesLayAwaysService;
            _invoiceService = invoiceService;
            _customerService = customerService;
            _orderRepairService = orderRepairService;
        }

        #region Sales - Cancel A Layaway
        [HttpGet]
        public JsonResult GetCheckNoByBank(string bankCode)
        {
            if (string.IsNullOrWhiteSpace(bankCode))
                return Json(new { checkNo = "" });

            string checkNo = _helperCommonService.GetNextCheckNoByBank(bankCode);
            return Json(new { success = true, checkNo = checkNo });
        }

        public ViewResult CancelLayaway()
        {
            ViewBag.StoreData = _helperCommonService.GetAllStoreCodesList();
            DataTable dtbankAccts = _helperCommonService.GetBankAcc(_helperCommonService.StoreCodeInUse1);
            List<string> bankCodes = dtbankAccts.AsEnumerable()
                                    .Select(Row => Row.Field<string>("code"))
                                    .ToList();
            ViewBag.BankAcc = bankCodes;
            DataRow row = _helperCommonService.GetSqlRow("SELECT TOP 1 CODE FROM BANK_ACC WHERE REFUND_DEFAULT = 1");
            string refundBank = row != null ? Convert.ToString(row["CODE"]) : "";
            ViewBag.selectedBank = refundBank;
            if (!string.IsNullOrWhiteSpace(refundBank))
                ViewBag.check = _helperCommonService.GetNextCheckNoByBank(refundBank);
            ViewBag.paymentmethods = _helperCommonService.GetPaymentMethods(false, true).Values.Cast<string>().ToList();

            return View();
        }
        public string GetAccfromInvoice(string inv_no)
        {
            DataTable dtable = _helperCommonService.GetSqlData($"SELECT top 1 ACC FROM INVOICE WHERE inv_no='{inv_no}'");
            return (_helperCommonService.DataTableOK(dtable)) ? Convert.ToString(dtable.Rows[0]["ACC"]) : string.Empty;
        }
        public JsonResult CancelLaywayDeatils(string invoice)
        {


            bool txtrestfee = true;
            var txtBank = "";
            var txtCheckNo = "";

            var lblLayawaySpecialBalance = "";
            var lblBalance = lblLayawaySpecialBalance = "";
            try
            {
                if (string.IsNullOrWhiteSpace(invoice))
                {
                    return Json(new { success = false, message = "Enter Invoice #" });


                }

                var InvoiceLen = _helperCommonService.GetInvoiceLength("invoice", "inv_no", invoice);
                if (_helperCommonService.iSSpecialOrder)
                {
                    if (_helperCommonService.iSSpecialOrderWithPickdup(invoice.PadLeft(6, ' ')))
                    {
                        return Json(new { success = false, message = $"Special Order# {invoice.PadLeft(6, ' ')} Is Already Picked Up." });

                    }

                    if (_helperCommonService.iSLayaway(invoice.PadLeft(6, ' ')))
                    {
                        return Json(new { success = false, message = $"Invoice# {invoice.PadLeft(6, ' ')} Is Layaway, Please Use Cancel Layaway Option." });

                    }
                }

                DataRow drIsLayaway = _helperCommonService.iSSpecialOrder ? _helperCommonService.GetSqlRow("SELECT 1 FROM INVOICE I LEFT JOIN IN_ITEMS II ON I.INV_NO = II.INV_NO where(II.IsSpecialItem = 1) AND I.INV_NO =@InvNo", "@InvNo", invoice.PadLeft(6, ' ')) : _helperCommonService.GetSqlRow("select * from invoice  where LAYAWAY= 1 and Trimmed_inv_no=@invno", "@invno", invoice.Trim());
                if (drIsLayaway == null)
                {
                    return Json(new { success = false, message = $"Invoice# does not exist or it is not a {(_helperCommonService.iSSpecialOrder ? "Special Order" : "Layaway")}." });

                }

                dt = dtPayment = _helperCommonService.GetInvoicePayments(invoice.Trim().PadLeft(InvoiceLen, ' '), true);

                decAlreadyDonePaymnet = _helperCommonService.DataTableOK(dtPayment) ? dtPayment.AsEnumerable().Where(x => !String.IsNullOrEmpty(Convert.ToString(x.Field<string>("METHOD"))) && Convert.ToString(x.Field<string>("METHOD")).ToUpper() != "LAYAWAY").Sum(row => _helperCommonService.DecimalCheckForDBNull(row.Field<decimal?>("AMOUNT"))) : 0;

                acc = GetAccfromInvoice(invoice.PadLeft(InvoiceLen, ' '));
                dt.Columns.Add("RefundMethod", typeof(string));
                dt.Columns.Add("RefundAmount", typeof(decimal));
                dt.Columns["RefundMethod"].SetOrdinal(7);
                dt.Columns["NOTE"].SetOrdinal(3);
                dt1 = dt.Copy();
                if (_helperCommonService.DataTableOK(dt) || _helperCommonService.iSSpecialOrder)
                {
                    for (int i = dt.Rows.Count - 1; i >= 0; i--)
                    {
                        dt.Rows[i]["RefundAmount"] = dt.Rows[i]["Amount"];
                        dt.Rows[i]["RefundMethod"] = dt.Rows[i]["Method"];

                        if (dt.Rows[i]["Method"].ToString() == "Layaway")
                        {
                            lblLayawaySpecialBalance = $"{(_helperCommonService.iSSpecialOrder ? "Special Order" : "Layaway")} Balance : " + dt.Rows[i]["Amount"].ToString();
                            dt.Rows.RemoveAt(i);

                        }
                    }

                    dt.AcceptChanges();
                    if (_helperCommonService.DataTableOK(dt))
                        txtrestfee = false;

                    DataRow[] founddup_method = null;
                    founddup_method = dt.Select("RefundMethod ='CHECK'");
                    if (founddup_method != null && founddup_method.Length > 0)
                    {

                        if (!string.IsNullOrWhiteSpace(txtBank))
                            txtCheckNo = _helperCommonService.GetNextCheckNoByBank(txtBank);
                    }

                }
                else
                {
                    return Json(new { success = false, message = "Invalid Invoice or Not Layaway.Please check again" });

                }

                var dataList = dt.AsEnumerable()
                   .Select(row => dt.Columns.Cast<DataColumn>()
                   .ToDictionary(col => col.ColumnName, col => row[col]))
                   .ToList();
                return Json(new { success = true, lblLayawaySpecialBalance = lblLayawaySpecialBalance, dt = dataList });


            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public bool iSCanceled(String inv_no)
        {
            return _helperCommonService.DataTableOK(_helperCommonService.GetSqlData($@"select 1 from invoice  with (nolock) where inv_no=@inv_no and isnull(is_deb,0)=1", "@inv_no", inv_no));
        }
        public DataTable CheckValidCheckNo(string check_no, string bank)
        {
            string CommandText = "select CHECKS.*, CAST(BANK.CLRD AS BIT) as CLRD1 from checks with (nolock) LEFT JOIN Bank with (nolock) ON TRIM(Bank.inv_no)=TRIM(CHECKS.TRANSACT) WHERE TRIM(checks.bank)= @bank AND TRIM(CHECKS.check_no) = @chk_no";
            return _helperCommonService.GetSqlData(CommandText, "@chk_no", check_no.Trim(), "@bank", bank.Trim());
        }
        public class LayawayItem
        {
            public string Col0 { get; set; }
            public string Col1 { get; set; }
            public string Col2 { get; set; }
            public string Col3 { get; set; }
            public string Col4 { get; set; }
            public string Col5 { get; set; }
            public string Col6 { get; set; }
        }


        public JsonResult deletelayaway(string invoice, string refreshButton, string txtrestfee, string txtBank, string txtCheckNo, List<LayawayItem> tableData, bool Isconfirm)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(invoice))
                {
                    return Json(new { success = false, message = "Invoice # Cannot be blank." });
                }
                dt = dtPayment = _helperCommonService.GetInvoicePayments(invoice.Trim().PadLeft(InvoiceLen, ' '), true);
                decAlreadyDonePaymnet = _helperCommonService.DataTableOK(dtPayment) ? dtPayment.AsEnumerable().Where(x => !String.IsNullOrEmpty(Convert.ToString(x.Field<string>("METHOD"))) && Convert.ToString(x.Field<string>("METHOD")).ToUpper() != "LAYAWAY").Sum(row => _helperCommonService.DecimalCheckForDBNull(row.Field<decimal?>("AMOUNT"))) : 0;

                acc = GetAccfromInvoice(invoice.PadLeft(InvoiceLen, ' '));
                dt.Columns.Add("RefundMethod", typeof(string));
                dt.Columns.Add("RefundAmount", typeof(decimal));
                dt.Columns["RefundMethod"].SetOrdinal(7);
                dt.Columns["NOTE"].SetOrdinal(3);
                for (int i = dt.Rows.Count - 1; i >= 0; i--)
                {
                    if (dt.Rows[i]["Method"].ToString() == "Layaway")
                    {
                        dt.Rows.RemoveAt(i);

                    }
                }
                int rowCount = Math.Min(dt.Rows.Count, tableData.Count);

                for (int i = 0; i < rowCount; i++)
                {
                    var item = tableData[i];
                    dt.Rows[i]["RefundAmount"] = _helperCommonService.iSSpecialOrder ? item.Col6 : item.Col5;
                    dt.Rows[i]["RefundMethod"] = _helperCommonService.iSSpecialOrder ? item.Col4 : item.Col3;
                    dt.Rows[i]["NOTE"] = _helperCommonService.iSSpecialOrder ? item.Col5 : item.Col4;
                    if (dt.Rows[i]["Method"].ToString() == "Layaway")
                    {
                        dt.Rows.RemoveAt(i);
                        i--;
                    }
                }

                dt.AcceptChanges();
                dt1 = dt.Copy();
                DataRow drIsLayaway = _helperCommonService.iSSpecialOrder ? _helperCommonService.GetSqlRow("SELECT 1 FROM INVOICE I LEFT JOIN IN_ITEMS II ON I.INV_NO = II.INV_NO where(II.IsSpecialItem = 1) AND I.INV_NO =@InvNo", "@InvNo", invoice.PadLeft(6, ' ')) : _helperCommonService.GetSqlRow("select * from invoice  where LAYAWAY= 1 and Trimmed_inv_no=@invno", "@invno", invoice.Trim());
                if (drIsLayaway == null)
                {
                    return Json(new { success = false, message = $"invoice# does not exist, or it is not a {(_helperCommonService.iSSpecialOrder ? "special order" : "layaway")}." });
                }

                if (refreshButton.ToString().ToUpper().Contains("*"))
                {
                    return Json(new { success = false, message = "Please click on refresh first" });

                }

                DataRow drInvoice = GetInvoiceByInvNo(invoice.PadLeft(6, ' '));
                if (_helperCommonService.iSFixedStore(_helperCommonService.DataTableOK(drInvoice) ? Convert.ToString(drInvoice["store_no"]) : ""))
                {
                    return Json(new { success = false, message = "We can not cancel invoice from another store" });
                }

                if (_helperCommonService.iSSpecialOrder && iSCanceled(invoice.PadLeft(6, ' ')))
                {
                    return Json(new { success = false, message = "This special order already cancelled" });

                }

                bool iSAskForSignature = false;
                if (_helperCommonService.DataTableOK(dt))
                {
                    dt.AcceptChanges();

                    var distinctValues = dt.AsEnumerable().Where(x => String.IsNullOrWhiteSpace(x.Field<String>("RefundMethod")));
                    if (distinctValues.Count() > 0)
                    {
                        return Json(new { success = false, message = "Refund Method empty in One or more records, Please mention refund method in each row" });

                    }
                }
                if (_helperCommonService.DataTableOK(dt1))
                    dt1.AcceptChanges();
                if (_helperCommonService.DataTableOK(dt1) || _helperCommonService.iSSpecialOrder)
                {
                    decimal amount = 0, Ramount = 0;
                    bool lNoRefundMethod = false;
                    string cRefundMethod = string.Empty;
                    if (_helperCommonService.DataTableOK(dt))
                    {
                        foreach (DataRow row in dt.Rows)
                        {
                            amount += _helperCommonService.CheckForDBNull(row["Amount"], typeof(decimal).ToString());
                            Ramount += _helperCommonService.CheckForDBNull(row["RefundAmount"], typeof(decimal).ToString());
                            cRefundMethod = _helperCommonService.CheckForDBNull(row["RefundMethod"]);
                            if (!lNoRefundMethod && string.IsNullOrWhiteSpace(cRefundMethod))
                                lNoRefundMethod = true;
                        }
                        Ramount += _helperCommonService.DecimalCheckForDBNull(txtrestfee);
                        if (amount != Ramount)
                        {
                            return Json(new { success = false, message = "Refund Amount + Stock fee (" + Ramount + ") must match Invoice Amount (" + amount + ")" });
                        }
                    }
                    else if (!_helperCommonService.DataTableOK(dt))
                        dt = new DataTable();

                    if (decAlreadyDonePaymnet != Ramount)
                    {
                        return Json(new { success = false, message = "The refund amount should match the amount of the original invoice." });

                    }

                    bool capture_sign = _helperCommonService.Read_Signature;
                    bool alreadysigned = false, iSCardPayment = false, iSSignBelow = false;
                    byte[] signature = null;

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        bool paymentsuccess;
                        string pay_no = _helperCommonService.CheckForDBNull(dt.Rows[i]["pay_no"]);
                        string sReponse = _helperCommonService.CheckForDBNull(dt.Rows[i]["Note"], typeof(string).ToString());
                        decimal ccamt = _helperCommonService.DecimalCheckForDBNull(dt.Rows[i]["RefundAmount"]);
                        string PayMethod = dt.Rows[i]["Method"].ToString().Trim().ToUpper();

                        if (!string.IsNullOrWhiteSpace(pay_no) && sReponse.Contains("Auth") && ccamt != 0 &&
                            (PayMethod == "CC SWIPE" || PayMethod == "VIRTUAL CC TERMINAL"))
                        {
                            iSSignBelow = (PayMethod == "CC SWIPE" && ccamt > _helperCommonService.SignBelow) ? true : iSSignBelow;
                            iSCardPayment = true;

                            alreadysigned = capture_sign;
                        }
                        cdate = _helperCommonService.CheckForDBNull(dt.Rows[i]["date"], typeof(string).FullName);
                        if (CheckNochangedate(Convert.ToDateTime(cdate)))
                        {
                            string spclay = _helperCommonService.iSSpecialOrder ? "Special Order" : "Layaway";
                            return Json(new { success = false, message = "Cannot Cancel " + spclay + " created before " + Convert.ToDateTime(_helperCommonService.NoChangeBefore).ToShortDateString() });

                        }
                    }

                    if (!_helperCommonService.CheckModuleEnabled(HelperCommonService.Modules.CC_Swipe))
                        iSSignBelow = true;


                    DataRow[] founddup_method = null;
                    founddup_method = _helperCommonService.DataTableOK(dt) ? dt.Select("RefundMethod ='CHECK'") : null;
                    if (founddup_method != null && founddup_method.Length > 0)
                    {
                        if (string.IsNullOrWhiteSpace(txtBank) || string.IsNullOrWhiteSpace(txtCheckNo))
                        {
                            return Json(new { success = false, message = "Bank/Check# Should not be empty " });
                        }

                        DataTable dtCheck = CheckValidCheckNo(txtCheckNo, txtBank);
                        if (_helperCommonService.DataTableOK(dtCheck))
                        {
                            return Json(new { success = false, message = "Check# already exists." });
                        }
                    }

                    dt.AcceptChanges();
                    bool _IsIssueStore = dt.AsEnumerable().OfType<DataRow>().Where(row => row.Field<string>("REFUNDMETHOD").ToUpper().Contains("STORE CREDIT")).Count() > 0;
                    invoice = _helperCommonService.Pad6(invoice);
                    if (Ramount > 0)
                    {
                        if (!Isconfirm)
                        {
                            if (_IsIssueStore && Ramount > 0)
                            {
                                return Json(new { success = false, flg = 1, message = $"Are you sure you want to cancel and issue store credit for the refund?" });
                            }
                            else
                            {
                                return Json(new { success = false, flg = 1, message = $"Are you sure you want to cancel the {(_helperCommonService.iSSpecialOrder ? "special order" : "layaway")}? " });
                            }
                        }
                        if (_helperCommonService.DataTableOK(dt))
                        {
                            if (!_IsIssueStore && lNoRefundMethod && dt.Select("REFUNDMETHOD='CHECK'").Length <= 0)
                            {
                                return Json(new { success = false, message = "Refund Method Should not be empty" });

                            }
                        }
                        DataTable dtpay = (DataTable)dt;

                        decimal _TotalRefundAmount = _helperCommonService.DataTableOK(dtpay) ? _helperCommonService.DecimalCheckForDBNull(dtpay.Compute("SUM(RefundAmount)", string.Empty)) : 0;
                        decimal _Restockfee = Convert.ToDecimal(txtrestfee);

                        string pcname = _helperCommonService.GetRegisterNames();
                        string cancelPayInvNoNo = _helperCommonService.GetPaymentInvNo(invoice);
                        String acc = _helperCommonService.AccFromInvoice(invoice);

                        if (dtpay == null)
                            dtpay = new DataTable();

                        if (CancelLayaway(invoice, Convert.ToDecimal(txtrestfee), _helperCommonService.GetDataTableXML("Payment", dtpay), _helperCommonService.LoggedUser, pcname, _helperCommonService.StoreCodeInUse1, _IsIssueStore, txtBank, txtCheckNo, _helperCommonService.iSSpecialOrder) || !_helperCommonService.DataTableOK(dtpay))
                        {
                            bool isDeletedInvoice = true;
                            for (int m = 0; _helperCommonService.DataTableOK(dtpay) && m < dtpay.Rows.Count; m++)
                                _helperCommonService.AddKeepRec(string.Format($"{(_helperCommonService.iSSpecialOrder ? "Special Order" : "Layaway")}# {invoice} was deleted and  refund amount is ${Convert.ToString(dtpay.Rows[m]["amount"])} with payment method {Convert.ToString(dtpay.Rows[m]["refundmethod"]).ToLower()}."), oldinvoicestream, isDeletedInvoice, "", acc, "I", invoice);

                            if (_helperCommonService.DecimalCheckForDBNull(_Restockfee) > 0)
                                _helperCommonService.AddKeepRec(string.Format($"{(_helperCommonService.iSSpecialOrder ? "Special Order" : "Layaway")}# {invoice} was deleted and refund restock fee is ${Convert.ToString(_Restockfee)}"), oldinvoicestream, isDeletedInvoice, "", acc, "I", invoice);

                            if (!_helperCommonService.DataTableOK(dtpay))
                                _helperCommonService.AddKeepRec(string.Format($"{(_helperCommonService.iSSpecialOrder ? "Special Order" : "Layaway")}# {invoice} was deleted without payment."), oldinvoicestream, isDeletedInvoice, "", acc, "I", invoice);

                            return Json(new { success = false, message = $"{(_helperCommonService.iSSpecialOrder ? "Special Order" : "Layaway")} Canceled." });

                        }
                        else
                            return Json(new { success = false, message = $"Error while Canceling {(_helperCommonService.iSSpecialOrder ? "Special Order" : "Layaway")}." });
                    }
                    else if (_IsIssueStore)
                        return Json(new { success = false, message = "If you don't want to refund with store credit, please select another refund method." });
                }
                else
                    return Json(new { success = false, message = "No Data Found." });
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Json(new { success = false, message = "" });
        }
        public bool CancelLayaway(string invno, decimal restockfee, string payments, string loggedUser, string register, string storeNo, bool isIssueStore = false, string bank = "", string checkNo = "", bool isSpecialOrder = false)
        {
            using (var connection = _connectionProvider.GetConnection())
            using (var dbCommand = new SqlCommand("CancelLayaway", connection) { CommandType = CommandType.StoredProcedure })
            {
                dbCommand.Parameters.AddRange(new[]
                {
                new SqlParameter("@invno", SqlDbType.NVarChar) { Value = invno },
                new SqlParameter("@restockfee", SqlDbType.Decimal) { Value = restockfee },
                new SqlParameter("@payments", SqlDbType.Xml) { Value = payments },
                new SqlParameter("@loggeduser", SqlDbType.NVarChar) { Value = loggedUser },
                new SqlParameter("@register", SqlDbType.NVarChar) { Value = register },
                new SqlParameter("@store_code", SqlDbType.NVarChar) { Value = storeNo },
                new SqlParameter("@IsIssueStorePayments", SqlDbType.Bit) { Value = isIssueStore },
                new SqlParameter("@BANK", SqlDbType.NVarChar) { Value = bank },
                new SqlParameter("@CHECK_NO", SqlDbType.NVarChar) { Value = checkNo },
                new SqlParameter("@iSFromSpecial", SqlDbType.Bit) { Value = isSpecialOrder }
                });
                dbCommand.CommandTimeout = 6000;
                connection.Open();
                return dbCommand.ExecuteNonQuery() > 0;
            }
        }
        #endregion


        #region Sales - Cancel A Layaway Payments
        public ViewResult CancelLayawayPayments()
        {
            ViewBag.StoreData = _helperCommonService.GetAllStoreCodesList();
            return View();
        }

        public JsonResult Searchinvoice(string invoiceNo)
        {

            if (string.IsNullOrWhiteSpace(invoiceNo))
            {
                return Json(new { success = false, message = "Enter Invoice #" });
            }
            if (_helperCommonService.IsValidInvoiceNo(invoiceNo))
            {
                int InvoiceLen = _helperCommonService.GetInvoiceLength("INVOICE", "INV_NO", invoiceNo);
                DataTable dtcheckcancel = CheckLayawayCancel(invoiceNo.Trim().PadLeft(InvoiceLen, ' '), true);
                if (_helperCommonService.DataTableOK(dtcheckcancel))
                {
                    return Json(new { success = false, message = "This is not a Layaway.." });

                }
                dtSubReport = GetInvoicecancelLayawayPayments(invoiceNo.Trim().PadLeft(InvoiceLen, ' '));
                HttpContext.Session.SetString("MyDataTable", JsonConvert.SerializeObject(dtSubReport));
                if (!_helperCommonService.DataTableOK(dtSubReport))
                    return Json(new { success = false, message = "No Payments found.." });
                var jsonData = dtSubReport.AsEnumerable()
                    .Select(row => dtSubReport.Columns.Cast<DataColumn>()
                        .ToDictionary(col => col.ColumnName, col => row[col] is DBNull ? null : row[col]))
                    .ToList();
                return Json(new { success = true, message = "", data = jsonData });
            }
            else
            {
                return Json(new { success = false, message = "Invoice does not exist" });
            }
        }
        public DataTable CheckLayawayCancel(string invno, bool flag)
        {
            return _helperCommonService.GetSqlData(@"select * from invoice  where LAYAWAY=0 and inv_no=@invno", "@invno", invno);
        }

        public DataTable GetInvoicecancelLayawayPayments(string inv_no)
        {
            string query = string.Format(@"select NAME,DATE,METHOD,AMOUNT,NOTE,PAY_NO FROM (select 1 as id, DATE,ISNULL(PAYMENTTYPE,'') as METHOD,AMOUNT * {0} as AMOUNT,cast(b.NOTE as nvarchar(100)) as NOTE,a.PAY_NO as PAY_NO ,d.name from PAY_ITEM a inner join payments b on a.PAY_NO =  b.INV_NO inner join customer d on b.acc=d.acc where a.rtv_pay='I' and b.RTV_PAY = 'P'  and a.inv_no =@inv_no ", 1);
            query += @" ) c order by id asc";
            return _helperCommonService.GetSqlData(query, "@inv_no", inv_no);
        }

        public DataRow GetInvoiceByInvNo(string invno)
        {
            return (_helperCommonService.GetSqlRow(@"select top 1 i.*,it.memo_no,iSNULL(it.IsSpecialItem,0) IsSpecialItem,it.fpon 
                from invoice i  with (nolock)
                left join (select * from IN_ITEMS with (nolock) where Trimmed_inv_no =@inv_no) it on i.inv_no = it.inv_no 
                    Where trim(i.inv_no) = @inv_no", "@inv_no", invno.Trim()));
        }
        public bool iSFixedStore(String store_no)
        {
            if (!String.IsNullOrWhiteSpace(FixedStoreCode) &&
                !String.IsNullOrWhiteSpace(store_no))
                return FixedStoreCode.ToLower().Trim() != store_no.ToLower().Trim();
            return false;
        }

        public void UpdateStoreCreditVoucher(string inv_no)
        {
            _helperCommonService.GetStoreProc("UPDATE_INVOICE", "@INV_NO", inv_no);
        }
        private string delaway(string invoice, string layawayid, decimal amount, string Method, string selctOption = "")
        {
            string delStatus = _helperCommonService.CancelLayawayPayment(invoice, layawayid, amount, Method, selctOption);

            if (delStatus.ToUpper() == "DELETED SUCCESSFULLY" || delStatus.ToUpper() == "REFUNDED SUCCESSFULLY")
            {
                UpdateStoreCreditVoucher(invoice);
                UpdateStoreCreditVoucher(layawayid);
                return delStatus;

            }

            return delStatus;
        }
        public bool CheckNochangedate(DateTime cdate)
        {
            DateTime nochangedate;
            if (DateTime.TryParse(NoChangeBefore, out nochangedate))
                return nochangedate > cdate;
            return false;
        }
        public JsonResult deleteButton1_Click(string invoice, bool isChecked, string slectTOption)
        {

            DataRow drInvoice = GetInvoiceByInvNo(invoice.PadLeft(6, ' '));
            if (iSFixedStore(_helperCommonService.DataTableOK(drInvoice) ? Convert.ToString(drInvoice["store_no"]) : ""))
            {
                return Json(new { success = false, message = "We can not delete payment from another store" });

            }

            string layawayid = "";
            decimal amount = 0;
            bool Isselect = false;
            bool refundsuccess = false;
            string infomsg = string.Empty;
            string checkoutauth;
            var tabledata = new DataTable();
            var sessionData = HttpContext.Session.GetString("MyDataTable");

            if (!string.IsNullOrEmpty(sessionData))
            {
                tabledata = JsonConvert.DeserializeObject<DataTable>(sessionData);
            }
            tabledata.AcceptChanges();
            string Method = "";
            string chkboxcheck = "";

            for (int i = 0; i < tabledata.Rows.Count; i++)
            {
                DataRow dr = tabledata.Rows[0];
                chkboxcheck = isChecked.ToString();
                layawayid = tabledata.Rows[i][5].ToString();
                amount = Convert.ToDecimal(tabledata.Rows[i][3].ToString());
                DateTime parsedDate;
                DateTime date = DateTime.TryParse(tabledata.Rows[i][1]?.ToString(), out parsedDate) ? parsedDate : DateTime.MinValue;

                string cdate = date.Date.ToString("MM-dd-yyyy");
                DateTime pdate = DateTime.ParseExact(cdate, "MM-dd-yyyy", System.Globalization.CultureInfo.InvariantCulture);
                Method = _helperCommonService.CheckForDBNull(tabledata.Rows[i][2]);

                if (chkboxcheck == "True")
                {

                    if (CheckNochangedate(pdate))
                    {
                        return Json(new { success = false, message = "Cannot cancel layaway payment created before " + Convert.ToDateTime(NoChangeBefore).ToShortDateString() });

                    }

                    Isselect = true;


                    if (!String.IsNullOrWhiteSpace(slectTOption))
                    {
                        String selectOption = slectTOption == "Refund" ? "R" : "D";
                        if (Method.ToUpper() == "CC SWIPE")
                        {
                            if (amount > 0)
                            {

                                checkoutauth = _helperCommonService.CheckForDBNull(dr["Note"]);
                                checkoutauth = checkoutauth.Replace("Auth#", "").Trim();
                                if (refundsuccess)
                                {
                                    infomsg += string.Format("Amount Refunded with Ref # {0}");
                                    delaway(invoice, layawayid, amount, Method, selectOption);
                                }
                                else
                                    infomsg += string.Format("Unable to refund CC Amount {0}", amount);

                            }
                            if (!string.IsNullOrWhiteSpace(infomsg)) { }
                        }
                        else
                        {
                            string message = delaway(invoice, layawayid, amount, Method, selectOption);
                            return Json(new { success = true, message = message });
                        }
                    }
                }
            }

            return Json(new { success = false, message = "" });
        }

        #endregion

        #region Sales - List of all layaways 
        public ViewResult ListofLayaways()
        {
            ViewBag.Stores = _helperCommonService.GetStoreNames();
            return View();
        }
        public DataTable SearchInvoice(string filter = "", bool isNoName = false, bool OpenOnlyinv = false)
        {
            string openonly = OpenOnlyinv ? " and IN_ITEMS.RET_INV_NO='' " : "  and 1=1 ";
            if (isNoName)
            {
                if (OpenOnlyinv)
                    return (_helperCommonService.GetSqlData(@"Select * from ( SELECT ISNULL(ID,0) As ID,INVOICE.INV_NO,INVOICE.ACC,INVOICE.NAME, 
                        try_cast(customer.TEL as Nvarchar(30)) as TEL,INVOICE.DATE, IN_ITEMS.STYLE,
                        IN_ITEMS.[DESC],GR_TOTAL, ISNULL(GR_TOTAL,0) - ISNULL(CREDITS,0) as BALANCE,[Message],
                        INVOICE.INACTIVE FROM INVOICE LEFT OUTER JOIN CUSTOMER ON INVOICE.ACC = CUSTOMER.ACC     
                        LEFT OUTER JOIN (SELECT INV_NO,MAX(STYLE)STYLE, MAX([DESC])[DESC] ,RET_INV_NO 
                        FROM IN_ITEMS GROUP BY INV_NO,RET_INV_NO)IN_ITEMS ON ((INVOICE.INV_NO))=((IN_ITEMS.INV_NO)) 
                        where " + filter + " " + openonly + " AND ISNULL(INVOICE.ACC,'') <>'')a where INV_NO  not in (select RET_INV_NO from in_items where RET_INV_NO <> '')  ORDER BY DATE desc"));
                if (!string.IsNullOrWhiteSpace(filter))
                    return (_helperCommonService.GetSqlData(@"SELECT ISNULL(ID,0) As ID,INVOICE.INV_NO,INVOICE.ACC,INVOICE.NAME, 
                        try_cast(customer.TEL as Nvarchar(30)) as TEL,INVOICE.DATE, IN_ITEMS.STYLE,
                        IN_ITEMS.[DESC],GR_TOTAL, ISNULL(GR_TOTAL,0) - ISNULL(CREDITS,0) as BALANCE,[Message],
                        INVOICE.INACTIVE FROM INVOICE LEFT OUTER JOIN CUSTOMER ON INVOICE.ACC = CUSTOMER.ACC     
                        LEFT OUTER JOIN (SELECT INV_NO,MAX(STYLE)STYLE, MAX([DESC])[DESC]  
                        FROM IN_ITEMS GROUP BY INV_NO)IN_ITEMS ON ((INVOICE.INV_NO))=((IN_ITEMS.INV_NO)) 
                        where " + filter + " AND ISNULL(INVOICE.ACC,'') <>''ORDER BY DATE desc"));
                return (_helperCommonService.GetSqlData(@"SELECT ISNULL(ID,0),INVOICE.INV_NO,INVOICE.ACC,INVOICE.NAME,
                    try_cast(customer.TEL as Nvarchar(30)) as TEL,INVOICE.DATE,IN_ITEMS.STYLE,
                    IN_ITEMS.[DESC],GR_TOTAL, ISNULL(GR_TOTAL,0) - ISNULL(CREDITS,0) as BALANCE,[Message],
                    INVOICE.INACTIVE FROM INVOICE LEFT JOIN CUSTOMER ON INVOICE.ACC = CUSTOMER.ACC 
                    LEFT OUTER JOIN (SELECT INV_NO,MAX(STYLE)STYLE, MAX([DESC])[DESC]  
                    FROM IN_ITEMS GROUP BY INV_NO)IN_ITEMS ON ((INVOICE.INV_NO))=((IN_ITEMS.INV_NO)) ORDER BY DATE desc"));
            }
            if (OpenOnlyinv)
                return (_helperCommonService.GetSqlData(@"Select * from (SELECT ID,INVOICE.INV_NO,CUSTOMER.ACC,INVOICE.NAME, 
                    try_cast(customer.TEL as Nvarchar(30)) as TEL,INVOICE.DATE, IN_ITEMS.STYLE,IN_ITEMS.[DESC],
                    GR_TOTAL, ISNULL(GR_TOTAL,0) - ISNULL(CREDITS,0) as BALANCE,[Message],INVOICE.INACTIVE 
                    FROM INVOICE INNER JOIN CUSTOMER ON INVOICE.ACC = CUSTOMER.ACC     
                    LEFT OUTER JOIN (SELECT INV_NO,MAX(STYLE)STYLE, MAX([DESC])[DESC],RET_INV_NO  FROM IN_ITEMS GROUP BY INV_NO,RET_INV_NO)
                    IN_ITEMS ON ((INVOICE.INV_NO))=((IN_ITEMS.INV_NO)) 
                    where " + filter + "" + openonly + " AND ISNULL(INVOICE.ACC,'') <>'')a where INV_NO  not in (select RET_INV_NO from in_items where RET_INV_NO <> '')  ORDER BY DATE desc"));
            else if (!string.IsNullOrWhiteSpace(filter))
                return (_helperCommonService.GetSqlData(@"SELECT ID,INVOICE.INV_NO,CUSTOMER.ACC,INVOICE.NAME, 
                    try_cast(customer.TEL as Nvarchar(30)) as TEL,INVOICE.DATE, IN_ITEMS.STYLE,IN_ITEMS.[DESC],
                    GR_TOTAL, ISNULL(GR_TOTAL,0) - ISNULL(CREDITS,0) as BALANCE,[Message],INVOICE.INACTIVE 
                    FROM INVOICE INNER JOIN CUSTOMER ON INVOICE.ACC = CUSTOMER.ACC     
                    LEFT OUTER JOIN (SELECT INV_NO,MAX(STYLE)STYLE, MAX([DESC])[DESC]  FROM IN_ITEMS GROUP BY INV_NO)
                    IN_ITEMS ON ((INVOICE.INV_NO))=((IN_ITEMS.INV_NO)) 
                    where " + filter + " AND ISNULL(INVOICE.ACC,'') <>''ORDER BY DATE desc"));
            return (_helperCommonService.GetSqlData(@"SELECT ID,INVOICE.INV_NO,CUSTOMER.ACC,INVOICE.NAME,
                try_cast(customer.TEL as Nvarchar(30)) as TEL,INVOICE.DATE,IN_ITEMS.STYLE,IN_ITEMS.[DESC],
                GR_TOTAL, ISNULL(GR_TOTAL,0) - ISNULL(CREDITS,0) as BALANCE,[Message],INVOICE.INACTIVE 
                FROM INVOICE LEFT JOIN CUSTOMER ON INVOICE.ACC = CUSTOMER.ACC LEFT OUTER JOIN 
                (SELECT INV_NO,MAX(STYLE)STYLE, MAX([DESC])[DESC]  FROM IN_ITEMS GROUP BY INV_NO)IN_ITEMS 
                ON ((INVOICE.INV_NO))=((IN_ITEMS.INV_NO)) ORDER BY DATE desc"));
        }
        public JsonResult ListofLayawaysDetails(string cboStores)
        {


            try
            {
                if (cboStores == "")
                    data = SearchInvoice("Layaway = 1");
                else
                    data = SearchInvoice(string.Format("Layaway = 1 AND invoice.Store_no = '{0}'", cboStores));

                if (data == null)
                    return Json(new { success = false, message = "Invalid Invoice or Not Layaway. Please check again" });

                var dataList = data.AsEnumerable()
                   .Select(row => data.Columns.Cast<DataColumn>()
                   .ToDictionary(col => col.ColumnName, col => row[col]))
                   .ToList();

                return Json(new { success = true, dt = dataList });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }
        public PartialViewResult GetSalesTaxBillForInvoiceNo(string cboStores, string inv_no)
        {
            if (conninvoice == null)
            {
                conninvoice = _connectionProvider.GetConnection();
                conninvoice.Open();
            }
            else if (conninvoice.State == ConnectionState.Closed)
            {
                conninvoice.Open();
            }
            if (string.IsNullOrWhiteSpace(cboStores))
                data = SearchInvoice("Layaway = 1");
            else
                data = SearchInvoice(string.Format("Layaway = 1 AND invoice.Store_no = '{0}'", cboStores));

            if (data == null || data.Rows.Count == 0)
            {
                ViewBag.ErrorMessage = "Invalid Invoice or Not Layaway. Please check again.";
                return PartialView("../Shared/_ListOfLayAwaysInvoicePrint", new List<Dictionary<string, object>>());
            }
            string query = @"SELECT TOP 1 i.*, it.memo_no, ISNULL(it.IsSpecialItem, 0) AS IsSpecialItem, it.fpon
                            FROM invoice i
                            LEFT JOIN in_items it ON i.inv_no = it.inv_no
                            WHERE i.inv_no = @inv_no";

            invoiceRow = _helperCommonService.GetSqlRow(query, "@inv_no", inv_no);
            customerRow = CheckValidBillingAcct(
                invoiceRow["bacc"] != DBNull.Value ? invoiceRow["bacc"].ToString() : string.Empty
            );
            dsInitData = OnInitializeData_Combine_SP(inv_no, _helperCommonService.is_Briony, false, false/*iSVatInclude*/, false);
            upsInsRow = _helperCommonService.DataTableOK(dsInitData.Tables[2]) ? dsInitData.Tables[2].Rows[0] : null;
            dtrepcostdata = dsInitData.Tables[3];
            dtStores = dsInitData.Tables[4];
            dtSubReport2 = GetLayawayPayments(inv_no);
            if (!_helperCommonService.DataTableOK(dtMasterDetail))
                dtMasterDetail = dsInitData.Tables[5];

            string xmlData = _helperCommonService.GetDataTableXML("INVHDATA", dtMasterDetail).Replace("NewDataSet", "DocumentElement");
            drMasterDetail = _helperCommonService.is_Hungtoo && !is_repair ? _helperCommonService.GetRowOne(GetHungtooInvoice(xmlData, GetPayCount(inv_no))) : _helperCommonService.GetRowOne(dtMasterDetail);

            var dataList = data.AsEnumerable()
               .Select(row => data.Columns.Cast<DataColumn>()
               .ToDictionary(col => col.ColumnName, col => row[col]))
               .ToList();
            var model = new SalesLayAwaysModel
            {
                DataList = dataList,
                InvoiceRow = invoiceRow,
                CustomerRow = customerRow,
                UpsInsRow = upsInsRow,
                Dtrepcostdata = dtrepcostdata,
                DtStores = dtStores,
                DtSubReport2 = dtSubReport2,
                DrMasterDetail = drMasterDetail,
                DrMasterDetail2 = dsInitData.Tables[5]
            };
            return PartialView("../Shared/_ListOfLayAwaysInvoicePrint", model);
        }
        public DataTable GetHungtooInvoice(string invData, int intPayCount = 0)
        {
            DataTable dataTable = new DataTable();
            using (SqlConnection conn = _connectionProvider.GetConnection())
            using (SqlCommand cmd = new SqlCommand("GetHungtooInvoice", conn))
            using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@INVDATA", SqlDbType.VarChar, 255).Value = invData;
                cmd.Parameters.Add("@intPayCount", SqlDbType.Int).Value = intPayCount;

                adapter.Fill(dataTable);
            }
            return dataTable;
        }
        private int GetPayCount(string inv_no)
        {
            if (_helperCommonService.is_Hungtoo && !is_repair)
            {
                DataTable dtPayDetails = _helperCommonService.GetInvoicePayments(inv_no, !_helperCommonService.is_oldtown);
                return _helperCommonService.DataTableOK(dtPayDetails) ? dtPayDetails.Rows.Count : 0;
            }
            return 0;
        }
        private DataSet OnInitializeData_Combine_SP(string inv_no, bool isBriony, bool is_memo, bool iSVatInclude, bool isopenmemo)
        {
            var ds = new DataSet();
            using (var command = new SqlCommand("OnInitializeData_Combine_SP", conninvoice))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@Inv_no", SqlDbType.VarChar) { Value = inv_no });
                command.Parameters.Add(new SqlParameter("@isBriony", SqlDbType.Bit) { Value = isBriony });
                command.Parameters.Add(new SqlParameter("@is_memo", SqlDbType.Bit) { Value = is_memo });
                command.Parameters.Add(new SqlParameter("@iSVatInclude", SqlDbType.Bit) { Value = iSVatInclude });
                command.Parameters.Add(new SqlParameter("@IsOpenMemo", SqlDbType.Bit) { Value = isopenmemo });

                using (var adapter = new SqlDataAdapter(command))
                {
                    adapter.Fill(ds);
                }
            }
            return ds;
        }
        public DataRow CheckValidBillingAcct(string billacc)
        {
            return _helperCommonService.GetSqlRow("select *  From Customer with (nolock) Where rtrim(bill_acc)= rtrim(@bill_acc) and bill_acc = acc", "@bill_acc", billacc);
        }
        public DataTable GetLayawayPayments(string inv_no)
        {
            DataTable dataTable = new DataTable();
            bool is_return = false;
            string query;

            using (SqlDataAdapter dataAdapter = new SqlDataAdapter())
            {
                dataAdapter.SelectCommand = new SqlCommand();
                dataAdapter.SelectCommand.Connection = _connectionProvider.GetConnection();
                dataAdapter.SelectCommand.CommandType = CommandType.Text;
                if (is_return)
                {
                    query = string.Format(@"SELECT DATE, METHOD, AMOUNT, CASHIER FROM (SELECT 1 as id, DATE, CASE WHEN ISNULL(PAYMENTTYPE,'')='Store Credit' THEN ISNULL(PAYMENTTYPE,'')+' (Credit# : '+B.PAYREFNO+')' WHEN ISNULL(PAYMENTTYPE,'')='Gift Card' THEN ISNULL(PAYMENTTYPE,'')+' (GC# : '+NOTE+')'  else ISNULL(PAYMENTTYPE,'') END AS METHOD, AMOUNT * {0} AS AMOUNT, CAST(Register AS nvarchar(100)) AS CASHIER FROM PAY_ITEM a INNER JOIN payments b ON a.PAY_NO = b.INV_NO WHERE b.RTV_PAY = 'P' AND a.RTV_PAY = 'I' AND TRY_CAST(a.inv_no AS int) = TRY_CAST(@inv_no AS INT)", is_return ? -1 : 1);

                    query += @" UNION SELECT 2 AS id, date, 'Layaway', gr_total - CREDITS, CAST('' AS nvarchar(100)) FROM invoice WHERE CAST(inv_no AS int) = CAST(@inv_no AS int) AND LAYAWAY = 1 ) c order by date asc";
                }
                else
                {
                    query = string.Format(@"select DATE ,METHOD, AMOUNT, CASHIER FROM (SELECT 1 as id, DATE, case when ISNULL(PAYMENTTYPE,'')='Store Credit' then ISNULL(PAYMENTTYPE,'')+' (Credit# : '+B.PAYREFNO+')' WHEN ISNULL(PAYMENTTYPE,'')='Gift Card' THEN ISNULL(PAYMENTTYPE,'')+' (GC# : '+NOTE+')'  else ISNULL(PAYMENTTYPE,'') END AS METHOD, AMOUNT * {0} AS AMOUNT, CAST(Register AS nvarchar(100)) AS CASHIER FROM PAY_ITEM a INNER JOIN payments b ON a.PAY_NO = b.INV_NO WHERE b.RTV_PAY = 'P' AND a.RTV_PAY = 'I' AND TRY_CAST(a.inv_no AS int) = TRY_CAST(@inv_no AS INT)", is_return ? -1 : 1);

                    query += @" UNION SELECT 2 AS ID, date, 'Layaway', gr_total - CREDITS, CAST('' as nvarchar(100)) FROM invoice WHERE inv_no = @inv_no and LAYAWAY = 1 ";
                    query += @" UNION SELECT 3 AS ID, date, 'On Account (Pay Later)', gr_total - CREDITS, CAST('' as nvarchar(100)) FROM invoice where inv_no = @inv_no and PAYLATER = 1 AND (gr_total - credits)<>0";
                    query += @" UNION SELECT 4 AS ID, A.DATE,   
                        CASE 
                        WHEN ISNULL(A.PAYMENTTYPE, '')= 'Store Credit' THEN ISNULL(A.PAYMENTTYPE, '')+' (Credit# : ' + B.PAYREFNO + ')'
                        WHEN ISNULL(A.PAYMENTTYPE, '')= 'Gift Card' then ISNULL(A.PAYMENTTYPE, '')+' (GC# : ' +NOTE+')'
                        ELSE ISNULL(A.PAYMENTTYPE, '') END AS METHOD, 
                        A.PAID AS AMOUNT, B.REGISTER AS CASHIER FROM PAID_RPR A JOIN PAYMENTS B ON A.PAY_NO = B.INV_NO 
                        WHERE A.REPINV_NO=' ' AND A.REPAIR_NO = (SELECT ISNULL(PON,'') FROM INVOICE WHERE TRY_CAST(inv_no AS int) = TRY_CAST(@inv_no as int) AND ISNULL(v_ctl_no,'') = 'REPAIR')) c ";

                    String repairOrderNo = _helperCommonService.GetRepairOrderNoFromInvoice(inv_no);
                    is_return = _helperCommonService.iSRepairReturn(inv_no);
                    if (!String.IsNullOrWhiteSpace(repairOrderNo))
                    {
                        String[] actRepNo = repairOrderNo.Split(',');
                        foreach (String actRep in actRepNo)
                        {
                            if (!String.IsNullOrWhiteSpace(query))
                                query += " UNION ";
                            query += $@" SELECT A.DATE, CASE 
                                WHEN ISNULL(A.PAYMENTTYPE,'')= 'Store Credit' THEN ISNULL(A.PAYMENTTYPE,'')+' (Credit# : ' + B.PAYREFNO + ')'
                                WHEN ISNULL(A.PAYMENTTYPE,'')= 'Gift Card' THEN ISNULL(A.PAYMENTTYPE,'')+' (GC# : ' +NOTE+')'
                                ELSE ISNULL(A.PAYMENTTYPE, '') END AS METHOD, 
                                A.PAID AS AMOUNT, B.REGISTER AS CASHIER
                                FROM PAID_RPR A JOIN PAYMENTS B ON A.PAY_NO = B.INV_NO WHERE TRIM(A.REPAIR_NO) = '{actRep.Trim()}' AND A.REPINV_NO = ' ' and 1={(is_return ? 0 : 1)}";
                        }
                    }
                    query += " ORDER BY date ASC";
                }

                dataAdapter.SelectCommand.CommandText = query;
                dataAdapter.SelectCommand.Parameters.AddWithValue("@inv_no", inv_no);
                dataAdapter.Fill(dataTable);
                return dataTable;
            }
        }
        
        [HttpPost]
        public JsonResult DeleteNotes(int ID)
        {
            var result = DeleteNote(ID);
            return Json(new { success = result });
        }
        public bool DeleteNote(int ID)
        {
            using (SqlCommand dbCommand = new SqlCommand())
            {
                dbCommand.Connection = _connectionProvider.GetConnection();
                dbCommand.CommandType = CommandType.Text;
                dbCommand.CommandText = $"DELETE FROM CUSTNOTE WHERE ID = '{ID}'";

                dbCommand.Connection.Open();
                int rowsAffected = dbCommand.ExecuteNonQuery();
                dbCommand.Connection.Close();
                return rowsAffected > 0;
            }
        }
        public DataTable ShowCustomerNotes(string acc)
        {
            return _helperCommonService.GetSqlData(@"select ID,WHO as [User] ,DTIME as Date,[TYPE] as Type ,NOTE as Note ,followup as FollowUp, completed as Completed, time as FollowUp_Time,reminder as Reminder from CUSTNOTE where acc= trim(@acc) order by DTIME", "@acc", acc);
        }
        public DataTable ShowPotentialCustomerNotes(string acc)
        {
            return _helperCommonService.GetSqlData(@"select WHO as [User] ,DTIME as Date,[TYPE] as Type ,NOTE as Note ,followup as FollowUp,completed as Completed from POTENTIALCUSTNOTE where acc= trim(@acc) order by DTIME", "@acc", acc);
        }
        [HttpGet]
        public PartialViewResult openPotentialCustNotes(string acc)
        {
            List<string> followUpTypes = _salesLayAwaysService.getFollowUpTypes();
            ViewBag.ACC = acc;
            ViewBag.Type = followUpTypes;
            var data = _salesLayAwaysService.loadall(acc, blnIsPotentialCust);
            ViewBag.Notes = data;
            return PartialView("../Shared/_CustomerNotes", data);
        }
        public List<string> getFollowUpTypes()
        {
            DataTable dt = _helperCommonService.GetStoreProc("GetFTypes");
            if (dt == null || dt.Rows.Count == 0)
            {
                return new List<string>();
            }
            List<string> followUpTypes = new List<string>();
            foreach (DataRow row in dt.Rows)
            {
                if (row["ftype"] != DBNull.Value)
                {
                    followUpTypes.Add(row["ftype"].ToString());
                }
            }
            return followUpTypes;
        }

        public PartialViewResult openAddEditType()
        {
            var Addedit = ShowTypes();
            ViewBag.Addedit = Addedit;
            return PartialView("../Shared/_AddEditType");
        }
        [HttpPost]
        public JsonResult AddType(string type)
        {
            var result = AddTypes(type);
            return Json(new { success = result });
        }

        [HttpPost]
        public JsonResult UpdateType(string oldType, string newType)
        {
            var result = UpdateTypes(oldType, newType);
            return Json(new { success = result });
        }

        [HttpPost]
        public JsonResult DeleteType(string type)
        {
            var result = DeleteTypes(type);
            return Json(new { success = result });
        }


        public DataTable ShowType()
        {
            return _helperCommonService.GetSqlData(@"SELECT UPPER(ISNULL(fType, '')) AS [Type] FROM followup_type");
        }

        public bool AddTypes(string type)
        {
            using (SqlCommand dbCommand = new SqlCommand())
            {
                dbCommand.Connection = _connectionProvider.GetConnection();
                dbCommand.CommandType = CommandType.Text;
                dbCommand.CommandText = $"INSERT INTO followup_type (fType) VALUES ('{type.ToUpper()}')";

                dbCommand.Connection.Open();
                int rowsAffected = dbCommand.ExecuteNonQuery();
                dbCommand.Connection.Close();
                return rowsAffected > 0;
            }
        }

        public bool UpdateTypes(string oldType, string newType)
        {
            using (SqlConnection conn = _connectionProvider.GetConnection())
            using (SqlCommand dbCommand = new SqlCommand("UPDATE followup_type SET fType = @newType WHERE fType = @oldType", conn))
            {
                dbCommand.CommandType = CommandType.Text;
                dbCommand.Parameters.Add("@newType", SqlDbType.VarChar).Value = newType.ToUpper();
                dbCommand.Parameters.Add("@oldType", SqlDbType.VarChar).Value = oldType.ToUpper();

                conn.Open();
                int rowsAffected = dbCommand.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }


        public bool DeleteTypes(string type)
        {
            using (SqlCommand dbCommand = new SqlCommand())
            {
                dbCommand.Connection = _connectionProvider.GetConnection();
                dbCommand.CommandType = CommandType.Text;
                dbCommand.CommandText = $"DELETE FROM followup_type WHERE fType = '{type.ToUpper()}'";

                dbCommand.Connection.Open();
                int rowsAffected = dbCommand.ExecuteNonQuery();
                dbCommand.Connection.Close();
                return rowsAffected > 0;
            }
        }
        public DataTable ShowTypes()
        {
            return _helperCommonService.GetSqlData(@"SELECT UPPER(ISNULL(fType, ''))[Type] FROM followup_type");
        }
        public class NoteModel
        {
            public int? ID { get; set; }
            public string ACC { get; set; }
            public string WHO { get; set; }
            public DateTime DTIME { get; set; }
            public string NOTE { get; set; }
            public string TYPE { get; set; }
            public DateTime? followup { get; set; }
            public bool completed { get; set; }
            public string time { get; set; }
            public bool reminder { get; set; }
        }
        [HttpPost]
        public JsonResult SaveNotes(List<NoteModel> notes)
        {
            string user = User.Identity.Name;

            using (SqlConnection conn = _connectionProvider.GetConnection())
            {
                conn.Open();

                foreach (var note in notes)
                {
                    SqlCommand cmd;

                    if (note.ID != null && note.ID > 0)
                    {

                        cmd = new SqlCommand(@"
                    UPDATE CUSTNOTE 
                    SET ACC = @ACC,
                        WHO = @WHO,
                        DTIME = @DTIME,
                        NOTE = @NOTE,
                        TYPE = @TYPE,
                        followup = @followup,
                        completed = @completed,
                        time = @time,
                        reminder = @reminder
                    WHERE ID = @ID", conn);

                        cmd.Parameters.AddWithValue("@ID", note.ID.Value);
                    }
                    else
                    {
                        cmd = new SqlCommand(@"
                    INSERT INTO CUSTNOTE 
                    (ACC, WHO, DTIME, NOTE, TYPE, followup, completed, time, reminder)
                    VALUES 
                    (@ACC, @WHO, @DTIME, @NOTE, @TYPE, @followup, @completed, @time, @reminder)", conn);
                    }

                    cmd.Parameters.AddWithValue("@ACC", note.ACC ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@WHO", note.WHO);
                    cmd.Parameters.AddWithValue("@DTIME", note.DTIME == DateTime.MinValue ? DateTime.Now : note.DTIME);
                    cmd.Parameters.AddWithValue("@NOTE", note.NOTE ?? "");
                    cmd.Parameters.AddWithValue("@TYPE", note.TYPE ?? "");
                    cmd.Parameters.AddWithValue("@followup", note.followup == DateTime.MinValue ? DateTime.Now : note.followup);
                    cmd.Parameters.AddWithValue("@completed", note.completed);
                    cmd.Parameters.AddWithValue("@time", note.time ?? "");
                    cmd.Parameters.AddWithValue("@reminder", note.reminder);

                    cmd.ExecuteNonQuery();
                }
            }

            return Json(new { success = true });
        }

        public PartialViewResult PreviewCustNotes(string acc)
        {
            DataTable dtCustFollowup = _salesLayAwaysService.loadall(acc, blnIsPotentialCust);

            dtCustFollowup.Columns.Add("Customercode", typeof(string));
            dtCustFollowup.Columns.Add("Name", typeof(string));
            dtCustFollowup.Columns.Add("Tel", typeof(string));

            foreach (DataRow row in dtCustFollowup.Rows)
            {
                row["Customercode"] = acc;
                row["Name"] = "";
                row["Tel"] = "";
            }

            ViewBag.CustomerFollowupData = dtCustFollowup;
            return PartialView("../Shared/_previewcustomerfollowup");
        }
        #endregion

        #region Customers - SalesReps - Past Due Layway payments
        public IActionResult PastDueLayawayPayments(bool isDueReport = false)
        {
            _helperCommonService.isDueReport = isDueReport;
            ViewBag.storeList = _helperCommonService.GetAllStoreCodesList();
            string storeCode = HttpContext.Session.GetString("STORE_CODE");

            byte[] storeLogo = _helperCommonService.GetStoreImage(storeCode != "" ? storeCode : "");

            if (storeLogo != null && storeLogo.Length > 0)
            {
                ViewBag.StoreLogo = Convert.ToBase64String(storeLogo);
            }
            else
            {
                ViewBag.StoreLogo = null;
            }

            DataTable storeInfo = _helperCommonService.GetStores(storeCode);
            if (_helperCommonService.DataTableOK(storeInfo))
            {
                ViewBag.StoreName = _helperCommonService.CheckForDBNull(storeInfo.Rows[0]["name"].ToString());
                ViewBag.StoreAddress1 = _helperCommonService.CheckForDBNull(storeInfo.Rows[0]["addr1"].ToString());
                ViewBag.storeAddress2 = _helperCommonService.CheckForDBNull(storeInfo.Rows[0]["addr2"].ToString());
                ViewBag.StorePhone = _helperCommonService.CheckForDBNull(storeInfo.Rows[0]["tel"].ToString());
            }
            return View();
        }

        public string GetListOfPastDueLayawayPayments(string noOfDays, string store, bool allFilters = false, bool noneFilters = false, bool mailFilters = false,
            bool emailFilters = false, bool textFilters = false, bool callFilters = false, bool isDueLayway = false, bool allStores = false)
        {
            SalesLayAwaysModel model = new SalesLayAwaysModel();
            //bool isValidPastDue = isDueLayway == true ? false:true;

            DataTable pastDueList = new DataTable();
            DataTable filteredData = new DataTable();
            if (!isDueLayway)
            {
                DateTime todate = DateTime.Now;
                int NoofDays = string.IsNullOrWhiteSpace(noOfDays) ? 0 : Convert.ToInt32(noOfDays);
                if (NoofDays > 0)
                {
                    DateTime SetTotalDate = todate.AddDays(-NoofDays);
                    string date = SetTotalDate.ToString("yyyy-MM-dd");

                    if (allStores)
                    {
                        pastDueList = GetPastDueLayawayPayments("PAST", date);
                    }
                    else
                    {
                        pastDueList = GetPastDueLayawayPaymentsSamad("PAST", date, store);
                    }
                }
            }
            else
            {
                DateTime todate = DateTime.Now;
                int NoofDays = 0, nUPSDue_Days = 0;

                DataTable UPSDue_Days = _salesLayAwaysService.GettimeSaver();
                nUPSDue_Days = Convert.ToInt32(UPSDue_Days.Rows[0]["LayawayPay_DueDays"].ToString());

                if (noOfDays != ">30")
                    NoofDays = string.IsNullOrWhiteSpace(noOfDays) ? 0 : Convert.ToInt32(noOfDays);
                else
                    NoofDays = 31;

                DateTime SetTotalDate = todate.AddDays(-nUPSDue_Days);
                string date = SetTotalDate.ToString("yyyy-MM-dd");

                if (allStores)
                    pastDueList = GetPastDueLayawayPayments("DUE", date, NoofDays);
                else
                    pastDueList = GetPastDueLayawayPaymentsSamad("DUE", date, store, NoofDays);

            }

            DataTable filteredCallData = new DataTable();
            DataTable filteredTextData = new DataTable();
            DataTable filteredEmailData = new DataTable();
            DataTable filteredMailData = new DataTable();
            DataTable filteredAllData = new DataTable();
            DataTable filteredNoneData = new DataTable();
            filteredData.Merge(pastDueList);

            if (allFilters || noneFilters || mailFilters || emailFilters || textFilters || noneFilters || callFilters)
            {
                if (callFilters && _helperCommonService.DataTableOK(pastDueList)) // call
                {
                    var filterData = pastDueList.AsEnumerable().Where(i => i.Field<bool>("CALL").ToString().ToUpper() == "TRUE");
                    if (filterData.Any())
                    {
                        filteredCallData = filterData.CopyToDataTable();
                        if (filteredData != null)
                            filteredData.Merge(filteredCallData);
                        //filteredData = filteredCallData;
                        else
                            filteredData = filterData.CopyToDataTable();
                    }
                }
            }

            if (textFilters && _helperCommonService.DataTableOK(pastDueList))//TEXT
            {
                var filterData = pastDueList.AsEnumerable().Where(i => i.Field<bool>("TEXT").ToString().ToUpper() == "TRUE");
                if (filterData.Any())
                {
                    filteredTextData = filterData.CopyToDataTable();
                    if (filteredData != null)
                        filteredData.Merge(filteredTextData);
                    else
                        filteredData = filterData.CopyToDataTable();
                }
            }

            if (emailFilters && _helperCommonService.DataTableOK(pastDueList))//EMAIL
            {
                var filterData = pastDueList.AsEnumerable().Where(i => i.Field<bool>("EMAIL").ToString().ToUpper() == "TRUE");

                if (filterData.Any())
                {
                    filteredEmailData = filterData.CopyToDataTable();
                    if (filteredData != null)
                        filteredData.Merge(filteredEmailData);
                    else
                        filteredData = filterData.CopyToDataTable();
                }
            }

            if (allFilters && _helperCommonService.DataTableOK(pastDueList)) //ALL
            {
                var filterData = pastDueList.AsEnumerable().Where(i => i.Field<bool>("CALL").ToString().ToUpper() == "TRUE" && i.Field<bool>("TEXT").ToString().ToUpper() == "TRUE" && i.Field<bool>("EMAIL").ToString().ToUpper() == "TRUE" && i.Field<bool>("MAIL").ToString().ToUpper() == "TRUE");

                if (filterData.Any())
                {
                    filteredAllData = filterData.CopyToDataTable();
                    filteredData = filterData.CopyToDataTable();
                    filteredAllData.AcceptChanges();

                    var filterDataText = filteredAllData.AsEnumerable().Where(i => i.Field<bool>("TEXT").ToString().ToUpper() == "TRUE");
                    var filterDataMail = filteredAllData.AsEnumerable().Where(i => i.Field<bool>("MAIL").ToString().ToUpper() == "TRUE");
                    var filterDataEmail = filteredAllData.AsEnumerable().Where(i => i.Field<bool>("EMAIL").ToString().ToUpper() == "TRUE");

                    if (filterData.Any())
                    {
                        filteredTextData = filterDataText.CopyToDataTable();
                        filteredMailData = filterDataMail.CopyToDataTable();
                        filteredEmailData = filterDataEmail.CopyToDataTable();
                    }
                }
            }

            if (noneFilters && _helperCommonService.DataTableOK(pastDueList)) //None
            {
                var filterData = pastDueList.AsEnumerable().Where(i => i.Field<bool>("CALL").ToString().ToUpper() == "FALSE" && i.Field<bool>("TEXT").ToString().ToUpper() == "FALSE" && i.Field<bool>("EMAIL").ToString().ToUpper() == "FALSE" && i.Field<bool>("MAIL").ToString().ToUpper() == "FALSE");

                if (filterData.Any())
                {
                    filteredNoneData = filterData.CopyToDataTable();
                    filteredData = filterData.CopyToDataTable();
                }
            }

            return JsonConvert.SerializeObject(filteredData);

        }

        public string getListOfMessages()
        {
            SalesLayAwaysModel messageData = new SalesLayAwaysModel();
            DataTable messageList = _salesLayAwaysService.GetMessages();

            return JsonConvert.SerializeObject(messageList);
        }
        public DataTable GetPastDueLayawayPayments(string optName, string pDate, int noofdays = 0, bool ListofSpecialOrder = false)
        {
            DataTable dataTable = new DataTable();
            string storedProcedure = optName == "PAST" ? "GetPastLayawayPayments" : "GetDueLayawayPayments";

            using (SqlConnection conn = _connectionProvider.GetConnection())
            using (var command = new SqlCommand(storedProcedure, conn))
            using (var adapter = new SqlDataAdapter(command))
            {
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add(new SqlParameter("@PDATE", SqlDbType.NVarChar) { Value = pDate });
                command.Parameters.Add(new SqlParameter("@LISTSPORDER", SqlDbType.Bit) { Value = ListofSpecialOrder });

                if (optName == "DUE")
                    command.Parameters.Add(new SqlParameter("@noofdays", SqlDbType.Int) { Value = noofdays });

                adapter.Fill(dataTable);
            }

            return dataTable;
        }
        public DataTable GetPastDueLayawayPaymentsSamad(string optName, string pDate, string storeNo, int noofdays = 0, bool ListofSpecialOrders = false)
        {
            DataTable dataTable = new DataTable();
            string storedProcedure = optName == "PAST" ? "GetPastLayawayPayments_Samad" : "GetDueLayawayPayments_Samad";

            using (SqlConnection conn = _connectionProvider.GetConnection())
            using (var command = new SqlCommand(storedProcedure, conn))
            using (var adapter = new SqlDataAdapter(command))
            {
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add(new SqlParameter("@PDATE", SqlDbType.NVarChar) { Value = pDate });
                command.Parameters.Add(new SqlParameter("@LISTSPORDER", SqlDbType.Bit) { Value = ListofSpecialOrders });
                command.Parameters.Add(new SqlParameter("@Store", SqlDbType.NVarChar) { Value = storeNo });

                if (optName == "DUE")
                {
                    command.Parameters.Add(new SqlParameter("@noofdays", SqlDbType.Int) { Value = noofdays });
                }

                adapter.Fill(dataTable);
            }

            return dataTable;
        }

        public bool UpdateCustMaintenance(string uInvNo, string uField, int uSetValue = 0)
        {
            string columnName;

            switch (uField)
            {
                case "CALL":
                    columnName = "OK_TOCALL";
                    break;
                case "TEXT":
                    columnName = "OK_TOTEXT";
                    break;
                case "EMAIL":
                    columnName = "OK_TOEMAIL";
                    break;
                case "MAIL":
                    columnName = "OK_TOMAIL";
                    break;
                default:
                    throw new ArgumentException("Invalid field type", nameof(uField));
            }

            string query = $@"
                UPDATE CUSTOMER 
                SET {columnName} = @uSetValue 
                WHERE ACC = (SELECT TOP 1 ACC FROM INVOICE with (nolock) WHERE inv_NO = @uInvNo)";

            using (SqlConnection conn = _connectionProvider.GetConnection())
            using (var dbCommand = new SqlCommand(query, conn))
            {
                dbCommand.Parameters.Add(new SqlParameter("@uInvNo", SqlDbType.NVarChar) { Value = uInvNo });
                dbCommand.Parameters.Add(new SqlParameter("@uSetValue", SqlDbType.Int) { Value = uSetValue });

                conn.Open();
                int rowsAffected = dbCommand.ExecuteNonQuery();

                return rowsAffected > 0;
            }
        }
        #endregion

        #region SMS and Email
        public IActionResult SendPastDueLaywayPaymentsSMS()
        {
            bool Nisesp = false;
            string strsmssub = string.Empty;
            var txtMessage = "";
            var SmsCount = 0;
            _helperCommonService.GetDefaultValues(HttpContext.Session);
            _helperCommonService.Can_Text = _helperCommonService.CheckModuleEnabled(HelperCommonService.Modules.CanText);
            DataTable dtstores = _helperCommonService.GetStores(_helperCommonService.StoreCodeInUse1);
            //string s = dtstores.Rows[0]["notext"].ToString();
            ViewBag.subject = "YJewel Support Request From : " + _helperCommonService.CompanyName;
            if (_helperCommonService.DataTableOK(dtstores) && dtstores.Rows[0]["notext"] != DBNull.Value)

                if (dtstores.Rows[0]["notext"].ToString() == "True")
                {
                    _helperCommonService.Can_Text = false;
                }
            if (!_helperCommonService.OkToText())
                return Json(new { success = false, message = "To enable texting please contact Ishal Inc.\nWould like me to ask Ishal Inc. to contact you?", subject = ViewBag.subject });
            DataView dvwarr = new DataView();

            //SmsCount = 0;
            //if (_helperCommonService.Can_Text)
            //{
            //    return Json(new { success = false, message = "SMS sending is not allowed for this store." });
            //}
            if (_helperCommonService.DataTableOK(dvwarr))
            {
                DataRow[] dtChecked = dvwarr.ToTable().Select("Select = 1");
                SmsCount = dtChecked.Length;
                if (dtChecked.Length == 0)
                {
                    Console.WriteLine("No customers selected to send SMS");
                    return Json(new { success = false, message = "An error occurred while processing the request." });
                }
                if (Nisesp)
                    strsmssub = _helperCommonService.CompanyName + " Just Sent " + SmsCount + " ESP SMS's";
                else
                    strsmssub = _helperCommonService.CompanyName + " Just Sent " + SmsCount + " Customer Warranties SMS's";
                if (txtMessage == "" || txtMessage == "Enter text here...")
                {

                    return Json(new { success = false, message = "An error occurred while processing the request." });
                }

                //if (dtChecked.Length > 25 &&
                //    !_helperCommonService.("You have selected to send a text to a large group of customers, there may be additional texting charges if you over your monthly quota. Are you sure you want to proceed?"))
                //    return;

                DataTable dtCheck = new DataTable();
                dtCheck.Columns.Add("ACC");
                dtCheck.Columns.Add("Cell");
                dtCheck.Columns.Add("Error");

                foreach (DataRow dr in dtChecked)
                {
                    string cellNo = "";
                    string acc = _helperCommonService.CheckForDBNull(dr["ACC"]);
                    cellNo = (_helperCommonService.CheckForDBNull(dr["CELL"]) != "") ? _helperCommonService.CheckForDBNull(dr["CELL"]) : _helperCommonService.CheckForDBNull(dr["TEL"]);

                    cellNo = Regex.Replace(cellNo, @"[^\d]", "").Trim();

                    string errorMsg = "";
                    if (cellNo == "")
                        errorMsg = "Blank cell number.";

                    if (errorMsg == "" && !Regex.IsMatch(cellNo, @"^\d{10}$"))
                        errorMsg = "Invalid cell number.";

                    //if (errorMsg == "")
                    //{
                    //    if (!_helperCommonService.SendSMS(cellNo, txtMessage.Text.Trim(), acc, true))
                    //        errorMsg = _helperCommonService.invalidMessageSMS;
                    //}
                    dtCheck.Rows.Add(acc, cellNo, errorMsg);
                }

                //if (dtCheck.Select("Error <> ''").Length == 0)
                //{
                //    _helperCommonService.MsgBox(_helperCommonService.GetLang($"SMS successfully sent to {SmsCount} selected customers"));
                //    this.Close();
                //    return;
                //}

                string showErrorMsg = "SMS sent successfully to " + dtCheck.Select("Error = ''").Length
                                    + " Valid Cell# (Out of " + dtCheck.Rows.Count + "),\n" + dtCheck.Select("Error <> ''").Length + " Invalid Customers cell# Found: \n";
                foreach (DataRow dr in dtCheck.Select("Error <> ''"))
                    showErrorMsg += _helperCommonService.CheckForDBNull(dr["ACC"]) + ": " + _helperCommonService.CheckForDBNull(dr["Error"]) + "\n";

                //_helperCommonService.MsgBox(showErrorMsg, Telerik.WinControls.RadMessageIcon.Error);
                return Json(new { success = false, message = "An error occurred while processing the request." });

            }
            else
                return Json(new { success = false, message = "An error occurred while processing the request." });
            //_helperCommonService.MsgBox(_helperCommonService.GetLang("No customers selected to send SMS"), Telerik.WinControls.RadMessageIcon.Info);
        }

        // private string[] scopes = { DriveService.Scope.Drive };
        string serviceAccountEmail = "wjewel@wjewel-179613.iam.gserviceaccount.com";
        string User_emailAddrss = string.Empty;
        string User_Password = string.Empty;
        string User_SMTPServer = string.Empty;
        string User_displayname = string.Empty;
        string User_Signature = string.Empty;
        int User_SMTPPort = 25;
        bool User_UseSSL = false;
        bool User_UseOAuth2 = false;
        private string Email_Signature = string.Empty;
        private string messageBody = string.Empty;
        public IActionResult SendPastDueLaywayPaymentsEmail(string subject, string message)
        {
            try
            {
                //Cursor.Current = Cursors.WaitCursor;
                //if (txtMessage.Text.Trim() == string.Empty)
                //{
                ////_helperCommonService.MsgBox(_helperCommonService.GetLang("Body message cannot be blank."), RadMessageIcon.Exclamation);
                //return Json(new { success = false, message = "Body message cannot be blank." });

                //}

                if (!string.IsNullOrEmpty(_helperCommonService.SupportMailId))
                {
                    DataTable dtEmailSettings_User = _helperCommonService.GetEmailSetupPerUser(_helperCommonService.LoggedUser);
                    if (dtEmailSettings_User.Rows.Count > 0)
                    {
                        User_emailAddrss = _helperCommonService.CheckForDBNull(dtEmailSettings_User.Rows[0]["Email"]);
                        User_Password = _helperCommonService.CheckForDBNull(dtEmailSettings_User.Rows[0]["Password"]);
                        User_SMTPPort = _helperCommonService.CheckForDBNull(dtEmailSettings_User.Rows[0]["SMTPPort"], typeof(Int32).ToString());
                        User_SMTPServer = _helperCommonService.CheckForDBNull(dtEmailSettings_User.Rows[0]["SMTPServer"]);
                        User_UseSSL = _helperCommonService.CheckForDBNull(dtEmailSettings_User.Rows[0]["UseSSL"], typeof(bool).ToString());
                        User_displayname = _helperCommonService.CheckForDBNull(dtEmailSettings_User.Rows[0]["displayname"]);
                        User_UseOAuth2 = _helperCommonService.CheckForDBNull(dtEmailSettings_User.Rows[0]["oauth2"], typeof(bool).ToString());
                        User_Signature = _helperCommonService.CheckForDBNull(dtEmailSettings_User.Rows[0]["email_signature"]);
                    }

                    if (string.IsNullOrEmpty(User_emailAddrss.Trim()))
                    {
                        DataTable dtEmailSetting = _helperCommonService.GetEmailSettings();
                        if (dtEmailSetting.Rows.Count > 0)
                        {
                            using (MailMessage mail = new MailMessage())
                            {
                                using (SmtpClient smtpServer = new SmtpClient((string)dtEmailSetting.Rows[0]["smtpserver"]))
                                {
                                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                                    smtpServer.SendCompleted += (s, ef) => smtpServer.Dispose();

                                    mail.From = new MailAddress((string)dtEmailSetting.Rows[0]["email"]);
                                    mail.To.Add(_helperCommonService.SupportMailId);
                                    mail.Subject = subject;
                                    mail.Body = message;

                                    smtpServer.DeliveryMethod = SmtpDeliveryMethod.Network;
                                    smtpServer.UseDefaultCredentials = false;
                                    smtpServer.Port = Convert.ToInt32(dtEmailSetting.Rows[0]["smtpport"]);
                                    smtpServer.Credentials = new System.Net.NetworkCredential((string)dtEmailSetting.Rows[0]["email"], (string)dtEmailSetting.Rows[0]["password"]);
                                    smtpServer.EnableSsl = (bool)dtEmailSetting.Rows[0]["usessl"];

                                    smtpServer.Send(mail);
                                    return Json(new { success = true, message = "Mail Sent Successfully." });
                                }
                            }
                        }
                        else
                        {
                            return Json(new { success = false, message = "Email Settings not defined." });
                        }
                    }
                    else
                    {
                        try
                        {
                            //SendEmail(User_SMTPServer, User_emailAddrss, _helperCommonService.SupportMailId, subject, t, User_SMTPPort, User_Password, User_UseSSL, User_displayname, User_UseOAuth2, User_Signature);
                        }
                        catch (Exception ex)
                        {
                            //_helperCommonService.MsgBox(_helperCommonService.GetLang(ex.Message), RadMessageIcon.Error);
                            //this.DialogResult = DialogResult.Cancel;
                            //Cursor.Current = Cursors.Default;
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;

                //Cursor.Current = Cursors.Default;
            }
            return Json(new { success = false, message = "Email Settings not defined." });

        }
        #endregion

        #region List Of Layway Payments
        public IActionResult ListofLayawayPayments()
        {
            SalesLayAwaysModel dataModel = new SalesLayAwaysModel();
            dataModel.CustomerCodes = _helperCommonService.GetAllCustomerCodes();

            string storeCode = HttpContext.Session.GetString("STORE_CODE");

            byte[] storeLogo = _helperCommonService.GetStoreImage(storeCode != "" ? storeCode : "");

            if (storeLogo != null && storeLogo.Length > 0)
            {
                ViewBag.StoreLogo = Convert.ToBase64String(storeLogo);
            }
            else
            {
                ViewBag.StoreLogo = null;
            }

            DataTable storeInfo = _helperCommonService.GetStores(storeCode);
            if (_helperCommonService.DataTableOK(storeInfo))
            {
                ViewBag.StoreName = _helperCommonService.CheckForDBNull(storeInfo.Rows[0]["name"].ToString());
                ViewBag.StoreAddress1 = _helperCommonService.CheckForDBNull(storeInfo.Rows[0]["addr1"].ToString());
                ViewBag.storeAddress2 = _helperCommonService.CheckForDBNull(storeInfo.Rows[0]["addr2"].ToString());
                ViewBag.StorePhone = _helperCommonService.CheckForDBNull(storeInfo.Rows[0]["tel"].ToString());
            }
            return View(dataModel);
        }

        public string GetAllListofLayawayPayments(string ReportFor, string custcode, string invno,
            string fromdate, string todate, bool isalldate)
        {
            DataTable data = GetListofLayawayPayments(ReportFor, custcode, invno, fromdate, todate, isalldate);
            return JsonConvert.SerializeObject(data);
        }
        public DataTable GetListofLayawayPayments(
            string ReportFor = "", string custcode = "", string invno = "",
            string fromdate = "", string todate = "", bool isalldate = false)
        {
            DataTable dataTable = new DataTable();
            using (SqlConnection conn = _connectionProvider.GetConnection())
            using (var command = new SqlCommand("GetListofLayawayPayments", conn))
            using (var adapter = new SqlDataAdapter(command))
            {
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add(new SqlParameter("@CUSTCODE", SqlDbType.NVarChar) { Value = custcode });
                command.Parameters.Add(new SqlParameter("@INVNO", SqlDbType.NVarChar) { Value = invno });
                command.Parameters.Add(new SqlParameter("@FDATE", SqlDbType.NVarChar) { Value = fromdate });
                command.Parameters.Add(new SqlParameter("@TDATE", SqlDbType.NVarChar) { Value = todate });
                command.Parameters.Add(new SqlParameter("@REPORTFOR", SqlDbType.NVarChar) { Value = ReportFor });
                command.Parameters.Add(new SqlParameter("@ISALLDATE", SqlDbType.Bit) { Value = isalldate });

                adapter.Fill(dataTable);
            }

            return dataTable;
        }
        #endregion


        public IActionResult LayawayPayments()
        {
            return View();
        }

        public string GetLayawayPayments(string StrLayaway, bool islayawayfollowup, string txtTel, string txtAcc, string txtInvoice, string txtNameStartsWith, string txtNameContains, DateTime fromDate, DateTime toDate, string txtAddress, string txtState, string txtZip, string txtAmount1, string txtAmount2)
        {
            string sFilter = "1=1";
            InvoiceModel invoiceModel = new InvoiceModel();
            if (StrLayaway != "")
            {
                sFilter += StrLayaway;
            }
            else
            {
                if (islayawayfollowup)
                    sFilter += string.Format(" AND {0} = '{2}' AND {1} = '{3}'", "CUSTOMER.acc", "invoice.inv_no", _helperCommonService.EscapeSpecialCharacters(txtAcc), _helperCommonService.EscapeSpecialCharacters(txtInvoice));
                else
                {
                    //string telvalue = _helperCommonService.RemoveSpecialCharactersspace(txtTel);
                    if (!string.IsNullOrWhiteSpace(txtAcc))
                        sFilter += string.Format(" AND {0} LIKE '%{1}%'", "CUSTOMER.acc", _helperCommonService.EscapeSpecialCharacters(txtAcc));
                    if (!string.IsNullOrWhiteSpace(txtInvoice))
                        sFilter += string.Format(" AND {0} LIKE '%{1}%'", "ltrim(rtrim(INVOICE.INV_NO))", _helperCommonService.EscapeSpecialCharacters(txtInvoice));
                    if (!string.IsNullOrWhiteSpace(txtNameStartsWith))
                        sFilter += string.Format(" AND {0} LIKE '%{1}%'", "CUSTOMER.name", _helperCommonService.EscapeSpecialCharacters(txtNameStartsWith));
                    if (!string.IsNullOrWhiteSpace(txtNameContains))
                        sFilter += string.Format(" AND {0} LIKE '%{1}%'", "CUSTOMER.name", _helperCommonService.EscapeSpecialCharacters(txtNameContains));
                    if (!string.IsNullOrWhiteSpace(txtTel))
                        sFilter += string.Format(" AND {0} LIKE '%{1}%'", "tel", txtTel);

                    sFilter += string.Format(" AND Layaway = 1");

                    sFilter += string.Format(" And ({0} between '{1:MM/dd/yyyy}' ", "date", fromDate);
                    sFilter += string.Format(" And '{0:MM/dd/yyyy}' )", toDate);

                    if (!string.IsNullOrWhiteSpace(txtAddress))
                        sFilter += string.Format(" AND {0} LIKE '%{1}%'", "CUSTOMER.addr1", _helperCommonService.EscapeSpecialCharacters(txtAddress));
                    if (!string.IsNullOrWhiteSpace(txtState))
                        sFilter += string.Format(" AND {0} LIKE '%{1}%'", "state1", _helperCommonService.EscapeSpecialCharacters(txtState));
                    if (!string.IsNullOrWhiteSpace(txtZip))
                        sFilter += string.Format(" AND {0} LIKE '%{1}%'", "zip1", _helperCommonService.EscapeSpecialCharacters(txtZip));
                    if (Convert.ToDecimal(txtAmount2) > 0)
                        sFilter += string.Format(" AND (gr_total >= {0} AND gr_total <= {1})", string.Format("{0:0.00}", Convert.ToDecimal(txtAmount1)), string.Format("{0:0.00}", Convert.ToDecimal(txtAmount2)));
                }
                if (!string.IsNullOrWhiteSpace(_helperCommonService.FixedStoreCode))
                    sFilter += string.Format(" AND {0}  = '{1}' ", "invoice.Store_no", _helperCommonService.FixedStoreCode);
            }

            DataTable data = _invoiceService.SearchInvoice(sFilter);

            if (!_helperCommonService.DataTableOK(data))
            {
                return "No Records Found";
            }
            return JsonConvert.SerializeObject(data);
        }

        public string LoadPaymentFormData(string inv_no)
        {
            decimal balance = 0, totalInvoiceValue = 0, totalbalance = 0;
            string cacc;
            bool _IsSpeicalOrder = false;
            bool iSNoDateChange = _helperCommonService.CheckModuleEnabled(HelperCommonService.Modules.No_Date_Chage);

            InvoiceModel invoiceModel = new InvoiceModel();
            //OrderRepairModel orderRepairModel = new OrderRepairModel();

            DataRow invoiceRow = _invoiceService.GetInvoiceByInvNo(inv_no);
            DataTable invoiceItems = _invoiceService.GetInvoiceItems(inv_no);
            DataTable discountItems = _invoiceService.GetInvoiceDiscount(inv_no);
            if (invoiceRow != null)
            {
                totalInvoiceValue = _helperCommonService.CheckForDBNull(invoiceRow["gr_total"], typeof(decimal).ToString());
                cacc = _helperCommonService.CheckForDBNull(invoiceRow.Table.Columns.Contains("bacc") ? Convert.ToString(invoiceRow["bacc"]) : Convert.ToString(invoiceRow["acc"]));
                balance = _helperCommonService.DecimalCheckForDBNull(invoiceRow["gr_total"]) - _helperCommonService.DecimalCheckForDBNull(invoiceRow["credits"]);
            }

            totalbalance = balance;
            DataTable paymentItems = _invoiceService.GetInvoicePayments(inv_no, false);
            paymentItems.Columns.Add("isold", typeof(bool)).DefaultValue = false;

            foreach (DataRow row in paymentItems.Rows)
                row["isold"] = true;
            paymentItems.AcceptChanges();
            string Payfor = _IsSpeicalOrder ? "Special Order" : "Layaway";
            string lblPaymentTitle = string.Format(Payfor + " Payments (Balance : {0:$#,##0.00})", balance);

            DataTable dt = new DataTable();
            dt.Columns.Add("Method", typeof(string));
            dt.Columns.Add("isReadOnly", typeof(string));
            //Hashtable paymentMethods = _helperCommonService.GetPaymentMethods(true, false, _helperCommonService.CheckModuleEnabled(_helperCommonService.Modules.Alex_h) ? "10111" : "");
            Hashtable paymentMethods = _helperCommonService.GetPaymentMethods(true, false, "");
            //var orderedList = paymentMethods.Cast<DictionaryEntry>()
            //        .OrderBy(entry => (int)entry.Key)
            //        .Select(entry => new { ID = (int)entry.Key, PaymentType = (string)entry.Value })
            //        .ToList();

            if (paymentItems != null)
            {
                if (paymentItems.Columns.Contains("CURR_TYPE"))
                    paymentItems.Columns.Remove("CURR_TYPE");

                if (paymentItems.Columns.Contains("CURR_RATE"))
                    paymentItems.Columns.Remove("CURR_RATE");

                if (paymentItems.Columns.Contains("CURR_AMOUNT"))
                    paymentItems.Columns.Remove("CURR_AMOUNT");

                //dataGridView2.DataSource = null;
                //dataGridView2.DataSource = paymentItems;
                //dataGridView2.GridBehavior = new CustomGridBehavior();

                paymentItems.Columns.Add("AUTHRIZED", typeof(bool)).DefaultValue = false;
                paymentItems.Columns["askedSigature"].SetOrdinal(14);
                foreach (DataRow row in paymentItems.Rows)
                {
                    string cPayNo = row["PAY_NO"].ToString();
                    row["AUTHRIZED"] = _helperCommonService.GetPayAuthrized(cPayNo);
                }
                paymentItems.AcceptChanges();

                //GridViewSummaryRowItem summary = new GridViewSummaryRowItem();
                //summary.Add(new GridViewSummaryItem("DATE", "Total:", GridAggregateFunction.First));
                //summary.Add(new GridViewSummaryItem("AMOUNT", _helperCommonService.FormatSetforAmount("$"), GridAggregateFunction.Sum));

                //dataGridView2.SummaryRowsTop.Add(summary);

                //this.dataGridView2.MasterTemplate.ShowTotals = true;
                //this.dataGridView2.MasterView.SummaryRows[0].PinPosition = PinnedRowPosition.Bottom;
                //dataGridView2.Columns[0].HeaderText = _helperCommonService.GetLang("Date");

                //((GridViewDateTimeColumn)dataGridView2.Columns[0]).CustomFormat = "d";
                //dataGridView2.Columns[1].HeaderText = _helperCommonService.GetLang("Method");
                //dataGridView2.Columns[2].HeaderText = _helperCommonService.GetLang("Amount");
                //dataGridView2.Columns[2].DataSourceNullValue = 0;
                //dataGridView2.Columns[0].Width = dataGridView2.Columns[1].Width = dataGridView2.Columns[2].Width = 50;

                //dataGridView2.Columns[4].HeaderText = _helperCommonService.GetLang("Note");
                //dataGridView2.Columns[4].Width = 100;


                foreach (DictionaryEntry item in paymentMethods)
                {
                    string bacc = string.IsNullOrEmpty(invoiceRow.Table.Columns.Contains("bacc") ? Convert.ToString(invoiceRow["bacc"]) : Convert.ToString(invoiceRow["acc"])) ? "" : Convert.ToString(invoiceRow["acc"].ToString());
                    if ((_helperCommonService.iSLayaway(_helperCommonService.Pad6(inv_no)) || !_helperCommonService.Chk_OpenAccount(bacc)) && Convert.ToString(item.Value).ToUpper() == "ON ACCOUNT (PAY LATER)")
                        continue;

                    dt.Rows.Add(item.Value.ToString().ToUpper());
                }

                //foreach (DataRow dr in paymentItems.Rows)
                //{
                //    if (dr["Method"].ToString() != "CC terminal" && dr["Method"].ToString() != "CC Swipe")
                //        dr["Method"] = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(dr["Method"].ToString().ToLower());

                //    DataRow[] NotFoundMethod = dt.Select("Method = '" + dr["METHOD"].ToString() + "'");
                //    if (NotFoundMethod.Length == 0)
                //        dr["Method"] = "Other";

                //    dr["Method"] = dr["Method"].ToString().ToUpper();
                //}

                foreach (DataRow dr in paymentItems.Rows)
                {
                    string originalMethod = dr["Method"].ToString(); // Keep original

                    if (originalMethod != "CC terminal" && originalMethod != "CC Swipe")
                        originalMethod = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo
                            .ToTitleCase(originalMethod.ToLower());

                    DataRow[] NotFoundMethod = dt.Select("Method = '" + originalMethod.Replace("'", "''") + "'");

                    if (NotFoundMethod.Length == 0)
                        dr["Method"] = "Other";
                    else
                        dr["Method"] = originalMethod.ToUpper(); // Final assignment
                }

                //DataView dv = new DataView(dt);

                //dataGridView2.Columns.Remove(dataGridView2.Columns[1]);
                //var colType = new GridViewComboBoxColumn()
                //{
                //    DataSource = dv,
                //    DisplayMember = "Method",
                //    ValueMember = "Method",
                //    FieldName = "Method",
                //    HeaderText = _helperCommonService.GetLang("Payment Method"),
                //    Width = 120,
                //    AutoCompleteMode = AutoCompleteMode.Suggest,
                //    DropDownStyle = RadDropDownStyle.DropDownList,
                //    FilteringMode = GridViewFilteringMode.ValueMember
                //};
                //colType.Sort(RadSortOrder.Descending, true);
                //dataGridView2.Columns.Add(colType);
                //dataGridView2.Columns.Move(dataGridView2.Columns.Count - 1, 1);

                //dataGridView2.Columns[3].IsVisible = false;

                //dataGridView2.EnterKeyMode = RadGridViewEnterKeyMode.EnterMovesToNextCell;
                //dataGridView2.NewRowEnterKeyMode = RadGridViewNewRowEnterKeyMode.EnterMovesToNextCell;
                //dataGridView2.GridBehavior = new CustomGridBehavior();
                //dataGridView2.Columns["CreditNo"].IsVisible = dataGridView2.Columns["CreditAmt"].IsVisible =
                //dataGridView2.Columns["EnteredAmt"].IsVisible = dataGridView2.Columns["NEW_CreditNo"].IsVisible =
                //dataGridView2.Columns["NEW_CreditAmt"].IsVisible = dataGridView2.Columns["NEW_EnteredAmt"].IsVisible =
                //dataGridView2.Columns["isold"].IsVisible = dataGridView2.Columns["PAY_NO"].IsVisible =
                //dataGridView2.Columns["AUTHRIZED"].IsVisible = dataGridView2.Columns["style"].IsVisible = dataGridView2.Columns["askedSigature"].IsVisible = false;
                //dataGridView2 = _helperCommonService.GetGridData(dataGridView2);
            }

            if (iSNoDateChange)
            {
                //foreach (GridViewDataRowInfo row in dataGridView2.Rows)
                //{
                //    if (!(Convert.ToString(row.Cells[0].Value) != string.Empty && Convert.ToDateTime(row.Cells[0].Value).ToString("MM/dd/yyyy") == DateTime.Now.ToString("MM/dd/yyyy")))
                //        foreach (GridViewCellInfo cell in row.Cells)
                //            cell.ReadOnly = true;
                //}

                foreach (DataRow row in dt.Rows)
                {
                    var cellValue = Convert.ToString(row[0]);

                    // Check if cell value is not empty and is today's date
                    DateTime cellDate;
                    bool isEditable = !string.IsNullOrEmpty(cellValue) && DateTime.TryParse(cellValue, out cellDate) && cellDate.Date == DateTime.Now.Date;

                    // Set "ReadOnly" column value to "true" or "false"
                    row["isReadOnly"] = isEditable ? false : true;
                }

            }
            //Hashtable paymentMethods = _helperCommonService.GetPaymentMethods(true, false, _helperCommonService.CheckModuleEnabled(_helperCommonService.Modules.Alex_h) ? "10111" : "");
            var result = new
            {
                paymentItems = paymentItems,
                Payfor = Payfor,
                balance = balance,
                paymentMethods = paymentMethods
            };
            string json = JsonConvert.SerializeObject(result);
            return json;
            //return JsonConvert.SerializeObject(dt);
        }

        public class PaymentLayawayRow
        {
            public string Date { get; set; }
            public string Method { get; set; }
            public string Amount { get; set; }
            public string Note { get; set; }
            public string isold { get; set; }
            public string pay_no { get; set; }
            public string CreditNo { get; set; }
            public string CreditAmt { get; set; }
            public string EnteredAmt { get; set; }
            public string NEW_CreditAmt { get; set; }
            public string NEW_EnteredAmt { get; set; }
            public string Style { get; set; }
            public string askedSignature { get; set; }
            public string AUTHRIZED { get; set; }
        }


        [HttpPost]
        public string SaveLaywaypayments(List<PaymentLayawayRow> paymentDataList, decimal totalbalance, string inv_no, bool paymentDone = false, bool payToAnotherStore = false, bool iSPicked = false, bool isRepairPayment = false, bool iSPickedConfirmFrom = false, bool isCancel = false, bool _IsSpeicalOrder = false)
        {

            DataTable paymentItems = new DataTable();
            paymentItems.Columns.Add("DATE", typeof(DateTime));
            paymentItems.Columns.Add("METHOD");
            paymentItems.Columns.Add("AMOUNT");
            paymentItems.Columns.Add("NOTE");
            paymentItems.Columns.Add("SALESMAN");
            paymentItems.Columns.Add("isold");
            paymentItems.Columns.Add("pay_no");
            paymentItems.Columns.Add("AUTHRIZED");
            paymentItems.Columns.Add("CreditNo");
            paymentItems.Columns.Add("CreditAmt");
            paymentItems.Columns.Add("EnteredAmt");
            paymentItems.Columns.Add("NEW_CreditAmt");
            paymentItems.Columns.Add("NEW_EnteredAmt");
            paymentItems.Columns.Add("Style");
            paymentItems.Columns.Add("askedSignature");

            InvoiceModel invoiceModel = new InvoiceModel();
            DataRow invoiceRow = null;

            if (!isRepairPayment)
            {
                invoiceRow = _invoiceService.GetInvoiceByInvNo(inv_no);
            }
            else
            {
                DataTable repOrderData = _invoiceService.SearchRepairOrder(" cast(r.repair_no as nvarchar(7)) = cast(" + inv_no + " as nvarchar(7))");

                DataRow[] orderRows = repOrderData.Select();
                if (orderRows.Length == 0)
                {
                    var resultnew = new
                    {
                        code = "Fail",
                        message = "No records Found."
                    };
                    return JsonConvert.SerializeObject(resultnew);
                }

                invoiceRow = orderRows[0];
            }


            foreach (var item in paymentDataList)
            {
                paymentItems.Rows.Add(item.Date, item.Method, item.Amount, item.Note, false, item.isold, item.pay_no, false, item.CreditNo, item.CreditAmt, item.EnteredAmt, item.NEW_CreditAmt, item.NEW_EnteredAmt, item.Style, item.askedSignature);
            }

            bool iSAskForSignature = false;
            byte[] signature = null;
            //paymentItems.AcceptChanges();
            paymentItems.Rows.Add(DateTime.Now, "", 0, "");

            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < paymentItems.Rows.Count; i++)
            {
                if (Convert.ToString(paymentItems.Rows[i]["Date"]) != string.Empty && Convert.ToDateTime(paymentItems.Rows[i]["Date"]) < Convert.ToDateTime("1900/01/01"))
                    sb.Append(i + ",");
            }

            if (sb.Length > 0)
            {
                var result = new
                {
                    code = "Fail",
                    message = "Invalid Date on Row#:\n" + sb.ToString()
                };
                return JsonConvert.SerializeObject(result);
            }

            paymentItems.AcceptChanges();
            DataRow[] checknew = paymentItems.Select(" isold = 'false'");

            decimal chkBal = paymentItems.AsEnumerable().OfType<DataRow>().Where(row => Convert.ToString(row.Field<string>("METHOD")) == "LAYAWAY").Sum(row => _helperCommonService.DecimalCheckForDBNull(row["AMOUNT"]));
            if (checknew.Length == 0 && chkBal != totalbalance)
            {
                var result = new
                {
                    code = "Fail",
                    message = "Nothing to Update. Please add payment"
                };
                return JsonConvert.SerializeObject(result);
            }

            if (_helperCommonService.iSFixedStore(_helperCommonService.DataTableOK(invoiceRow) ? Convert.ToString(invoiceRow["store_no"]).Trim() : ""))
            {
                var result = new
                {
                    code = "Fail",
                    message = "We can not make payment for another store"
                };
                return JsonConvert.SerializeObject(result);
            }

            decimal bal1 = 0, bal2 = 0;
            foreach (DataRow r in paymentItems.Rows)
            {
                if (r["Method"].ToString() != "Layaway")
                {
                    bal1 = Convert.ToDecimal(r["Amount"]);
                    bal2 += bal1;
                }
            }

            decimal totalInvoiceValue = _helperCommonService.CheckForDBNull(invoiceRow["GR_TOTAL"], typeof(decimal).ToString());
            bal2 = totalInvoiceValue - bal2;

            if (bal2 < 0)
            {
                var result = new
                {
                    code = "Fail",
                    message = "Amount Should be Equal to Invoice amount."
                };
                return JsonConvert.SerializeObject(result);
            }
            //paymentItems.AcceptChanges();

            DataTable invoiceItems = _invoiceService.GetInvoiceItems(inv_no);

            if (bal2 == 0 && _helperCommonService.Is_Symphony)
            {
                string zeroCostStyles = _helperCommonService.iSCostLessThanEqlZero(inv_no, invoiceItems);
                if (zeroCostStyles != string.Empty)
                {
                    var result = new
                    {
                        code = "Fail",
                        message = $"{zeroCostStyles}"
                    };
                    return JsonConvert.SerializeObject(result);
                }
            }
            string RadWaitCC = "";
            string cacc = _helperCommonService.CheckForDBNull(invoiceRow.Table.Columns.Contains("bacc") ? Convert.ToString(invoiceRow["bacc"]) : Convert.ToString(invoiceRow["acc"]));
            string errorMessage = string.Empty;
            if (paymentItems.Rows.Count > 0 && paymentDone == false)
            {
                for (int i = 0; i < paymentItems.Rows.Count; i++)
                {

                    foreach (var col in new[] { "Amount", "CreditAmt", "EnteredAmt", "NEW_CreditAmt", "NEW_EnteredAmt" })
                    {
                        if (paymentItems.Columns.Contains(col))
                        {
                            var val = paymentItems.Rows[i][col]?.ToString().Trim();

                            if (string.IsNullOrWhiteSpace(val) || val.Equals("null", StringComparison.OrdinalIgnoreCase))
                            {
                                paymentItems.Rows[i][col] = "0.00"; // force valid decimal
                            }
                        }
                    }

                    string pay_no = _helperCommonService.CheckForDBNull(paymentItems.Rows[i]["pay_no"]);
                    string sReponse = _helperCommonService.CheckForDBNull(paymentItems.Rows[i]["Note"], typeof(string).ToString());
                    if (string.IsNullOrWhiteSpace(pay_no) || !sReponse.Contains("Auth"))
                    {
                        string PayMethod = paymentItems.Rows[i]["Method"].ToString().Trim().ToUpper();
                        if (PayMethod == "CC SWIPE" || PayMethod == "VIRTUAL CC TERMINAL")
                        {
                            bool paymentsuccess = false;
                            _helperCommonService.ChargeCC(paymentItems.Rows[i], inv_no, cacc, false, RadWaitCC, out errorMessage, out paymentsuccess, out iSAskForSignature, out signature, false, "I");
                            if (!paymentsuccess)
                            {
                                var result = new
                                {
                                    code = "Fail",
                                    message = "Payment Failed"
                                };
                                return JsonConvert.SerializeObject(result);
                            }
                        }
                    }
                }
            }

            if (_helperCommonService.DataTableOK(paymentItems))
            {
                paymentItems.AcceptChanges();
                DataRow dW = _helperCommonService.ValidateCustomerCreditlimit(invoiceRow.Table.Columns.Contains("bacc") ? Convert.ToString(invoiceRow["bacc"]) : Convert.ToString(invoiceRow["acc"]), _helperCommonService.Pad6(inv_no));
                decimal CustBalnce = _helperCommonService.DecimalCheckForDBNull(dW["Balance"]);
                //CustomersModel customersModel = new CustomersModel();
                DataRow CustDtlCopy = _customerService.CheckValidBillingAcct(invoiceRow.Table.Columns.Contains("bacc") ? Convert.ToString(invoiceRow["bacc"]) : Convert.ToString(invoiceRow["acc"]));
                for (int i = 0; i < paymentItems.Rows.Count; i++)
                {
                    if (CustDtlCopy != null && Convert.ToString(paymentItems.Rows[i]["Method"]) == "ON ACCOUNT (PAY LATER)" &&
                        (_helperCommonService.DecimalCheckForDBNull(paymentItems.Rows[i]["Amount"]) + CustBalnce) > _helperCommonService.DecimalCheckForDBNull(CustDtlCopy["Credit"]))
                        errorMessage += string.Format("{0}\n", "Customer is Over Credit Limit");
                }

                if (!String.IsNullOrWhiteSpace(errorMessage))
                {
                    var result = new
                    {
                        code = "Fail",
                        message = errorMessage
                    };
                    return JsonConvert.SerializeObject(result);
                }
            }

            if (_helperCommonService.CheckForDBNull(HttpContext.Session.GetString("StoreCodeInUse")) != _helperCommonService.GetStoreNo(inv_no, (isRepairPayment ? "R" : "I")) && payToAnotherStore == false)
            {
                string diffStore = _helperCommonService.GetStoreNo(inv_no, (isRepairPayment ? "R" : "I"));

                var result1 = new
                {
                    code = "Confirm",
                    confirmField = "payToAnotherStore",
                    paymentDone = true,
                    balance2 = bal2,
                    isPickedMsg = $"Mark this {(_IsSpeicalOrder ? "special order" : "Layaway")} as picked up?",
                    message = $"Invoice belongs to store {diffStore}, payments will be added to store {HttpContext.Session.GetString("StoreCodeInUse")} should I continue?"
                };
                return JsonConvert.SerializeObject(result1);
            }
            if (bal2 == 0 && iSPicked == false && iSPickedConfirmFrom == false && !isCancel)
            {
                var result = new
                {
                    code = "Confirm",
                    confirmField = "iSPicked",
                    paymentDone = true,
                    balance2 = bal2,
                    message = $"Mark this {(_IsSpeicalOrder ? "special order" : "Layaway")} as picked up?",
                };
                return JsonConvert.SerializeObject(result);
            }

            //if (_helperCommonService.StoreCodeInUse.ToString() != _helperCommonService.GetStoreNo(inv_no, (isRepairPayment ? "R" : "I")) &&
            //    !_helperCommonService.IsSure($"Invoice belongs to store {_helperCommonService.GetStoreNo(inv_no, (isRepairPayment ? "R" : "I"))}, payments will be added to store {_helperCommonService.StoreCodeInUse} should I continue?"))
            //    return "";

            if (bal2 > 0)
                paymentItems.Rows.Add(DateTime.Now, "Layaway", bal2, "");

            //if (bal2 == 0 && _helperCommonService.IsSure($"Mark this {(_IsSpeicalOrder ? "special order" : "Layaway")} as picked up?"))

            if (bal2 == 0 && iSPicked == true)
            {
                if (!_helperCommonService.CheckModuleEnabled(HelperCommonService.Modules.NegativeInventory))
                {
                    errorMessage += iSEnoughStock(invoiceItems, "You cannot pick up the order.");
                    if (!String.IsNullOrEmpty(errorMessage))
                    {
                        var result = new
                        {
                            code = "Fail",
                            message = errorMessage
                        };
                        return JsonConvert.SerializeObject(result);
                    }
                }
                decimal actualSalesTaxRate = _helperCommonService.StoreRateNotSame(Convert.ToString(invoiceRow["store_no"]));
                if (!_helperCommonService.CheckModuleEnabled(HelperCommonService.Modules.VAT_Included) && _helperCommonService.DecimalCheckForDBNull(invoiceRow["sales_tax_rate"]) != actualSalesTaxRate)
                {
                    var result = new
                    {
                        code = "Fail",
                        message = $"Made invoice store is {Convert.ToString(invoiceRow["store_no"])} && sales tax rate is {_helperCommonService.DecimalCheckForDBNull(invoiceRow["sales_tax_rate"])} but, actual sales tax rate is {actualSalesTaxRate} so you can not pickup invoice from here please use edit invoice option."
                    };
                    return JsonConvert.SerializeObject(result);
                }
                iSPicked = true;
            }

            if (!isRepairPayment)
            {
                string pcname = _helperCommonService.GetRegisterNames();
                paymentItems.AcceptChanges();
                //this.lblPaymentTitle.Text = string.Format((isRepairPayment ? "Repair" : _IsSpeicalOrder ? "Special Order" : "Layaway") + " Payments (Balance: {0:$#,##0.00})", bal2);
                var InvoiceModel = new InvoiceModel()
                {
                    INV_NO = inv_no,
                    ACC = _helperCommonService.CheckForDBNull(invoiceRow["acc"]),
                    BACC = invoiceRow.Table.Columns.Contains("bacc") ? _helperCommonService.CheckForDBNull(invoiceRow["bacc"]) : "",
                    DATE = _helperCommonService.CheckForDBNull(invoiceRow["date"], typeof(DateTime).ToString()),
                    PON = "NONE",
                    GR_TOTAL = _helperCommonService.CheckForDBNull(invoiceRow["gr_total"], typeof(decimal).ToString()),
                    NAME = _helperCommonService.CheckForDBNull(invoiceRow["name"]),
                    ADDR1 = _helperCommonService.CheckForDBNull(invoiceRow["addr1"]),
                    ADDR2 = _helperCommonService.CheckForDBNull(invoiceRow["addr2"]),
                    ADDR3 = _helperCommonService.CheckForDBNull(invoiceRow["addr3"]),
                    CITY = _helperCommonService.CheckForDBNull(invoiceRow["city"]),
                    STATE = _helperCommonService.CheckForDBNull(invoiceRow["state"]),
                    DEDUCTION = _helperCommonService.CheckForDBNull(invoiceRow["DEDUCTION"], 0, typeof(decimal).ToString()),
                    ZIP = _helperCommonService.CheckForDBNull(invoiceRow["zip"]),
                    COUNTRY = _helperCommonService.CheckForDBNull(invoiceRow["country"]),
                    OPERATOR = _helperCommonService.CheckForDBNull(invoiceRow["operator"]),
                    SALESMAN1 = _helperCommonService.CheckForDBNull(invoiceRow["SALESMAN1"]),
                    SALESMAN2 = _helperCommonService.CheckForDBNull(invoiceRow["SALESMAN2"]),
                    STORE_NO = _helperCommonService.CheckForDBNull(invoiceRow["STORE_NO"]),
                    TAXABLE = _helperCommonService.CheckForDBNull(invoiceRow["TAXABLE"], typeof(bool).ToString()),
                    SALES_TAX = _helperCommonService.CheckForDBNull(invoiceRow["SALES_TAX"], typeof(decimal).ToString()),
                    TRADEIN = _helperCommonService.CheckForDBNull(invoiceRow["TRADEIN"], typeof(bool).ToString()),
                    TRADEINAMT = _helperCommonService.CheckForDBNull(invoiceRow["TRADEINAMT"], typeof(decimal).ToString()),
                    TRADEINDESC = _helperCommonService.CheckForDBNull(invoiceRow["TRADEINDESC"]),
                    SPECIAL = _helperCommonService.CheckForDBNull(invoiceRow["SPECIAL"], typeof(bool).ToString()),
                    PICKED = iSPicked,

                    TAXINCLUDED = _helperCommonService.CheckForDBNull(invoiceRow["TAXINCLUDED"], typeof(bool).ToString()),
                    CASH_REG_CODE = string.IsNullOrWhiteSpace(_helperCommonService.Cash_Register.Trim()) ? _helperCommonService.GetRegisterNames() : _helperCommonService.Cash_Register,
                    CASH_REG_STORE = _helperCommonService.StoreCode,
                    SYSTEMNAME = pcname
                };
                string out_inv_no = string.Empty;
                //InvoiceModel invObjModel = new InvoiceModel();
                DataTable discountItems = _invoiceService.GetInvoiceDiscount(inv_no);
                if (_invoiceService.AddEditInvoice(InvoiceModel, _helperCommonService.GetDataTableXML("InvoiceItems", invoiceItems), _helperCommonService.GetDataTableXML("PaymentItems", paymentItems), _helperCommonService.GetDataTableXML("DiscountItems", discountItems), out out_inv_no, true, true, false, HttpContext.Session.GetString("StoreCodeInUse")))
                {
                    //_helperCommonService.MsgBox(_helperCommonService.GetLang("Payment Updated Successfully"), RadMessageIcon.Info);
                    //this.DialogResult = DialogResult.OK;

                    if (iSPicked)
                        _helperCommonService.AddKeepRec($"Invoice# {inv_no.Trim().PadLeft(6, ' ')} picked up (Layaway)", null, false, _helperCommonService.LoggedUser, "", "I", inv_no.Trim().PadLeft(6, ' '));

                    //var objInvoicePrint = new frmReprintInvoice(inv_no.Trim().PadLeft(6, ' '));
                    //objInvoicePrint.is_reprint = true;
                    //objInvoicePrint.MdiParent = this.MdiParent;
                    //objInvoicePrint.ShowDialog();
                    //this.Dispose();
                    var result = new
                    {
                        code = "Success",
                        message = "Payment Updated Successfully",
                        next = "showReprintInvoice",
                        inv_no = inv_no.Trim().PadLeft(6, ' ')
                    };
                    return JsonConvert.SerializeObject(result);
                }
                else
                {
                    var result = new
                    {
                        code = "Fail",
                        message = "There was problem updating payment",
                    };
                    return JsonConvert.SerializeObject(result);
                }
            }
            else
            {
                bool is_OrdeNotPickedup = false;
                string pcname = _helperCommonService.GetRegisterNames();

                OrderRepairModel orderrepairModel = new OrderRepairModel();
                string out_inv_no = string.Empty;
                string cAcc = _helperCommonService.CheckForDBNull(invoiceRow["acc"]);
                decimal total = _helperCommonService.CheckForDBNull(invoiceRow["gr_total"], typeof(decimal).ToString());
                if (_orderRepairService.PaymentForRepair(inv_no, cAcc, pcname, Convert.ToString(total), _helperCommonService.GetDataTableXML("PaymentItems", paymentItems), "", _helperCommonService.StoreCode, string.IsNullOrWhiteSpace(_helperCommonService.Cash_Register.Trim()) ? _helperCommonService.GetRegisterNames() : _helperCommonService.Cash_Register, out out_inv_no, false, true, false, HttpContext.Session.GetString("StoreCodeInUse")))
                {

                    string description1 = "Payment towards Repair Order #" + inv_no.Trim() + " added For " + cAcc + "";
                    _helperCommonService.AddKeepRec(description1);
                    //_helperCommonService.MsgBox(_helperCommonService.GetLang("Payment Updated Successfully"), RadMessageIcon.Info);
                    //var objPrintRepairOrder = new frmPrintOneRepairorderDetails(this.inv_no)
                    //{
                    //    MdiParent = this.MdiParent
                    //};
                    //objPrintRepairOrder.ShowDialog();
                    //this.Dispose();
                    //is_OrdeNotPickedup = _helperCommonService.CheckRepairOrder(inv_no);

                    //if (!is_OrdeNotPickedup)
                    //    return;

                    //if (_helperCommonService.IsSure("Do you want to mark this repair order as picked up?"))
                    //    new frmEditRepairOrderInvoiceBasedOnInvoceId(this.inv_no, 0, false, false, true, true);
                    var result = new
                    {
                        code = "Success",
                        message = "Payment Updated Successfully"
                    };

                    return JsonConvert.SerializeObject(result);
                }
                else
                {
                    var result = new
                    {
                        code = "Fail",
                        message = "There was problem updating payment",
                    };
                    return JsonConvert.SerializeObject(result);
                }

            }

        }


        private String iSEnoughStock(DataTable invoiceItems, String msg)
        {
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < invoiceItems.Rows.Count; i++)
            {
                if (_helperCommonService.DecimalCheckForDBNull(invoiceItems.Rows[i]["Qty"]) > 0 && _helperCommonService.CheckForDBNull(invoiceItems.Rows[i]["NOT_STOCK"], 0, typeof(decimal).ToString()) == 0)
                {
                    InvoiceModel invoiceModel = new InvoiceModel();
                    String Style = _helperCommonService.InvStyle(_helperCommonService.CheckForDBNull(invoiceItems.Rows[i]["STYLE"].ToString().Trim()));
                    bool iSStock = _invoiceService.CheckInStock(Style, _helperCommonService.StoreCodeInUse1, _helperCommonService.DecimalCheckForDBNull(invoiceItems.Rows[i]["Qty"]), true);
                    if (!iSStock && !String.IsNullOrEmpty(Convert.ToString(invoiceItems.Rows[i]["STYLE"])))
                    {
                        stringBuilder.Append(string.Format("NOT ENOUGH ITEMS IN STOCK FOR STYLE: {0} IN STORE {1}", Style, HttpContext.Session.GetString("StoreCodeInUse")));
                        stringBuilder.Append("\n");
                        stringBuilder.Append(msg);
                    }
                }
            }
            return Convert.ToString(stringBuilder);
        }

        public IActionResult SpecialOrderPayments()
        {
            return View();
        }

        public string GetSpecialOrderPayments(string StrLayaway, bool islayawayfollowup, string txtTel, string txtAcc, string txtInvoice, string txtNameStartsWith, string txtNameContains, DateTime fromDate, DateTime toDate, string txtAddress, string txtState, string txtZip, string txtAmount1, string txtAmount2)
        {
            string sFilter = "1=1";

            if (!string.IsNullOrWhiteSpace(txtAcc))
                sFilter += string.Format(" AND {0} = '{1}'", "CUSTOMER.acc", _helperCommonService.EscapeSpecialCharacters(txtAcc));
            if (!string.IsNullOrWhiteSpace(txtInvoice))
                sFilter += string.Format(" AND {0} = '{1}'", "ltrim(rtrim(INVOICE.INV_NO))", _helperCommonService.EscapeSpecialCharacters(txtInvoice.Trim()));

            if (!string.IsNullOrWhiteSpace(txtNameStartsWith))
                sFilter += string.Format(" AND {0} LIKE '{1}%'", "CUSTOMER.name", _helperCommonService.EscapeSpecialCharacters(txtNameStartsWith));

            if (!string.IsNullOrWhiteSpace(txtNameContains))
                sFilter += string.Format(" AND {0} LIKE '%{1}%'", "CUSTOMER.name", _helperCommonService.EscapeSpecialCharacters(txtNameContains));

            if (!string.IsNullOrWhiteSpace(txtTel))
                sFilter += string.Format(" AND {0} = {1}", "tel", _helperCommonService.EscapeSpecialCharacters(txtTel, true));
            sFilter += string.Format(" AND SPECIALITEM = 1 AND  PICKUPDATE IS NULL");

            DateTime? fromDate1;
            if (!string.IsNullOrWhiteSpace(fromDate.ToString()))
                fromDate1 = Convert.ToDateTime(fromDate);
            else
                fromDate1 = _helperCommonService.DefStart;

            DateTime? toDate1;
            if (!string.IsNullOrWhiteSpace(toDate.ToString()))
                toDate1 = Convert.ToDateTime(toDate);
            else
                toDate1 = _helperCommonService.DefEnd; ;

            sFilter += string.Format(" And ({0} >= '{1:MM/dd/yyyy}' ", "date", fromDate1);
            sFilter += string.Format(" And {0} <= '{1:MM/dd/yyyy}' )", "date", toDate1);

            if (!string.IsNullOrWhiteSpace(txtAddress))
                sFilter += string.Format(" AND {0} LIKE '%{1}%'", "CUSTOMER.addr1", _helperCommonService.EscapeSpecialCharacters(txtAddress));

            if (!string.IsNullOrWhiteSpace(txtState))
                sFilter += string.Format(" AND {0} LIKE '%{1}%'", "state1", _helperCommonService.EscapeSpecialCharacters(txtState));

            if (!string.IsNullOrWhiteSpace(txtZip))
                sFilter += string.Format(" AND {0} LIKE '%{1}%'", "zip1", _helperCommonService.EscapeSpecialCharacters(txtZip));

            if (Convert.ToDecimal(txtAmount2) > 0)
                sFilter += string.Format(" AND (gr_total >= {0} AND gr_total <= {1})", string.Format("{0:0.00}", Convert.ToDecimal(txtAmount1)), string.Format("{0:0.00}", Convert.ToDecimal(txtAmount2)));



            DataTable data = _helperCommonService.GetSpeicalorderinvoicedata(sFilter);

            if (!_helperCommonService.DataTableOK(data))
            {
                return "No Records Found";
            }
            return JsonConvert.SerializeObject(data);
        }
        #region Layaway Followup 

        public IActionResult LayawayFollowup()
        {
            SalesLayAwaysModel dataModel = new SalesLayAwaysModel();
            dataModel.CustomerCodes = _helperCommonService.GetAllCustomerCodes();
            ViewBag.Stores = _helperCommonService.GetStoreNames();
            ViewBag.GetMessages = _helperCommonService.GetMessages();
            return View(dataModel);
        }


        public string GetListofAllLayawaysforFollowup(string ccode, string fdate, string tdate, int lastpayment, int lastnote,
            int lastsms, string spoketo, string leftmessage, string nocomm, string store, bool issmsfilter, string smsfdate,
            string smstdate, int finalduedate, bool Nocalltext = false)
        {
            DataTable data = GetListofLayawaysforFollowup(ccode, fdate, tdate, Convert.ToInt32(lastpayment), Convert.ToInt32(lastnote),
             Convert.ToInt32(lastsms), spoketo, leftmessage, nocomm, store, issmsfilter, smsfdate,
             smstdate, Convert.ToInt32(finalduedate), Nocalltext);

            return JsonConvert.SerializeObject(data);
        }


        public DataTable GetListofLayawaysforFollowup(string ccode, string fdate, string tdate, int lastpayment, int lastnote,
           int lastsms, string spoketo, string leftmessage, string nocomm, string store, bool issmsfilter, string smsfdate,
           string smstdate, int finalduedate, bool Nocalltext = false)
        {
            DataTable dataTable = new DataTable();

            // Parse the dates directly into DateTime objects
            DateTime parsedFDate = DateTime.Parse(fdate.Split(' ')[0]);
            DateTime parsedTDate = DateTime.Parse(tdate.Split(' ')[0]);
            DateTime parsedSmsFDate = DateTime.Parse(smsfdate.Split(' ')[0]);
            DateTime parsedSmsTDate = DateTime.Parse(smstdate.Split(' ')[0]);

            using (var connection = _connectionProvider.GetConnection())
            using (var dataAdapter = new SqlDataAdapter())
            {
                // Create and configure the command
                var command = new SqlCommand("LayawayFollowup", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                // Add parameters with parsed DateTime values
                command.Parameters.AddWithValue("@ccode", ccode);
                command.Parameters.AddWithValue("@fdate", parsedFDate.ToString("yyyy-MM-dd"));
                command.Parameters.AddWithValue("@tdate", parsedTDate.ToString("yyyy-MM-dd"));
                command.Parameters.AddWithValue("@lastpayment", lastpayment);
                command.Parameters.AddWithValue("@lastnote", lastnote);
                command.Parameters.AddWithValue("@lastsms", lastsms);
                command.Parameters.AddWithValue("@spoketo", spoketo);
                command.Parameters.AddWithValue("@leftmessage", leftmessage);
                command.Parameters.AddWithValue("@nocomm", nocomm);
                command.Parameters.AddWithValue("@store", store);
                command.Parameters.AddWithValue("@issmsfilter", issmsfilter);
                command.Parameters.AddWithValue("@smsfdate", parsedSmsFDate.ToString("yyyy-MM-dd"));
                command.Parameters.AddWithValue("@smstdate", parsedSmsTDate.ToString("yyyy-MM-dd"));
                command.Parameters.AddWithValue("@finalduedate", finalduedate);
                command.Parameters.AddWithValue("@Nocalltext", Nocalltext);

                // Set the command for the data adapter
                dataAdapter.SelectCommand = command;

                // Fill the DataTable using the data adapter
                dataAdapter.Fill(dataTable);
            }

            return dataTable;
        }

        public JsonResult SaveUpdateNoCalltext(string tableData)
        {
            object msg = "";
            DataTable dtSendDatainXML = JsonConvert.DeserializeObject<DataTable>(tableData);

            if (_helperCommonService.DataTableOK(dtSendDatainXML))
            {
                string XML = _helperCommonService.GetDataTableXML("Nocalltext12", dtSendDatainXML);
                if (UpdateNoCalltext(XML))
                {
                    msg = new
                    {
                        code = true,
                        message = "Updated Successfully"
                    };
                }
            }
            else
            {
                msg = new
                {
                    code = false,
                    message = "No Data Found"
                };
            }
            return Json(msg);

        }

        public bool UpdateNoCalltext(string XML)
        {
            using (SqlConnection connection = _connectionProvider.GetConnection())
            using (SqlCommand sqlCommand = new SqlCommand("updatenocallText", connection))
            {
                sqlCommand.CommandType = CommandType.StoredProcedure;

                // Add the XML parameter
                sqlCommand.Parameters.Add(new SqlParameter("@xmlNocaltext", SqlDbType.Xml) { Value = XML });

                connection.Open();
                bool rowsAffected = sqlCommand.ExecuteNonQuery() > 0;
                return rowsAffected;
            }
        }

        public IActionResult getLayawaySMSHistory(string invoice)
        {
            DataTable dt = GetLayawayfollowupSMS(invoice);
            ViewBag.LayawaySMSHistory = dt;
            return PartialView("_LayawayFollowupSMSHistory");
        }

        public DataTable GetLayawayfollowupSMS(string invno)
        {
            return _helperCommonService.GetSqlData("select cast( [date] as datetime)+cast([time] as datetime) [Date/Time],'' as Received,Message as Sent  from layawaysms where trim(invoiceno)=trim(@invno)",
                "@invno", invno);
        }

        public IActionResult getListOfLayawayHistory(string invoice)
        {
            DataTable dt = _helperCommonService.GetInvoicePayments(invoice, false);
            ViewBag.LayawayHistory = dt;

            return PartialView("_LayawayFollowupHistory");
        }


        public IActionResult getListOfLayawayFollowUpNotes(string invoice)
        {
            DataTable dt = GetLayawayfollowupNotes(invoice);
            ViewBag.FollowUpNotes = dt;
            ViewBag.invNo = invoice;
            return PartialView("_LayawayFollowupNotes");
        }

        public DataTable GetLayawayfollowupNotes(string invno)
        {

            return _helperCommonService.GetSqlData("select cast(0 as bit) Sel, cast( [date] as datetime)+cast([time] as datetime) [Date/Time],'" + _helperCommonService.LoggedUser + "' as [User],Note,cast(iif(commun_method='SPOKETO',1,0) as bit)SpokeTo,cast(iif(commun_method='LEFTMESSAGE',1,0) as bit)LEFTMESSAGE,cast(iif(commun_method='NOCOMMUNICATION',1,0) as bit)NOCOMMU  from layawaynotes where trim(invoiceno)=trim(@invno)",
                "@invno", invno);
        }

        public JsonResult DeleteLayawayfollowupNote(string invno, string commmethod, string date)
        {
            object msg;
            try
            {
                using (SqlCommand dbCommand = new SqlCommand())
                {
                    // Set command properties
                    dbCommand.Connection = _connectionProvider.GetConnection();
                    dbCommand.CommandType = CommandType.Text;

                    dbCommand.CommandText = @"DELETE FROM layawaynotes 
                               WHERE TRIM(invoiceno) = TRIM(@invno)
                               AND CAST([date] AS DATETIME) + CAST([time] AS DATETIME) = @date 
                               AND commun_method = @commmethod";
                    dbCommand.Parameters.AddWithValue("@invno", invno);
                    dbCommand.Parameters.AddWithValue("@commmethod", commmethod);
                    dbCommand.Parameters.AddWithValue("@date", date);

                    // Open connection, execute the query, and close the connection
                    dbCommand.Connection.Open();
                    var rowsAffected = dbCommand.ExecuteNonQuery();
                    dbCommand.Connection.Close();

                    if (rowsAffected > 0)
                    {
                        msg = new
                        {
                            code = true,
                            message = "Record deleted successfully."
                        };
                    }
                    else
                    {
                        throw new Exception("Unexpected error while deleting record.");
                    }
                }
            }
            catch (Exception ex)
            {
                msg = new
                {
                    code = false,
                    message = ex.Message
                };
            }
            return Json(msg);
        }

        public IActionResult getLaywayFollowUpAddNotes(string invoice)
        {
            ViewBag.invNo = invoice;
            ViewBag.LoggedUser = _helperCommonService.LoggedUser;
            return PartialView("_LaywayFollowupAddNote");
        }
        public JsonResult SaveLayawayfollowupNotes(string invno, string date, string commmethod, string message, string time)
        {
            try
            {
                using (SqlCommand dbCommand = new SqlCommand())
                {
                    // Create the command and set its properties
                    // dbCommand.SelectCommand = new SqlCommand();
                    dbCommand.Connection = _connectionProvider.GetConnection();
                    dbCommand.CommandType = CommandType.Text;

                    dbCommand.CommandText = @"INSERT INTO layawaynotes(invoiceno,date,note,commun_method,time) VALUES (@invno,@date,@message,@commmethod,@time)";
                    dbCommand.Parameters.AddWithValue("@invno", invno);
                    dbCommand.Parameters.AddWithValue("@date", date);
                    dbCommand.Parameters.AddWithValue("@commmethod", commmethod);
                    dbCommand.Parameters.AddWithValue("@message", message);
                    dbCommand.Parameters.AddWithValue("@time", time);
                    // Fill the table from adapter
                    dbCommand.Connection.Open();
                    var rowsAffected = dbCommand.ExecuteNonQuery();
                    dbCommand.Connection.Close();
                    return Json(new { code = true, message = rowsAffected > 0 });
                }
            }
            catch (Exception ex)
            {
                return Json(new { code = false, message = ex.Message });
            }
        }


        #endregion
    }
}