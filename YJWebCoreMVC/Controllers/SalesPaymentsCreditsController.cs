/*
 *  Created By Phanindra
 *  Phanindra 10/23/2024 Added DeleteQuotes, DeleteQuoteNo methods
 *  Phanindra 11/06/2024 Added EditCashReceipt, GetPayment, GetReceiptEditData
 *  Phanindra 11/18/2024 Added ModifyAcc_ofReceipt, CheckOKReceiptToModifyCustomer, GetCustomerofReceipt, ModifyAccofReceipt, DeleteReceipt, CheckDeleteCashReceipt, DeleteCashReceipt methods
 *  Phanindra 11/28/2024 Added CustomerRefund, AddEditCredit, GetCreditData, AddEditCreditDetails, DeleteCredit
 *  Phanindra 12/04/2024 Worked on issues related to Modify ACC for receipt method.
 *  Phanindra 12/09/2024 Worked on fixing issues with ModifyACC, Reprint ,Add and Edit Cash receipt methods.
 *  Phanindra 12/11/2024 Added methods ReprintCredit, PrintCredit, CheckCreditAdj and GetCredit methods.
 *  Phanindra 12/16/2024 Modified GetPayment, Added AddAdjReceivable, GetAdjReceivableData, CheckReceiptExistOrNot, SaveAdjRcvable, EditAdjRcvable, ReprintAdjRcvable, DeleteAdjRcvable, PrintAdjRcvable, SaveEditAdjRcvable, DeleteAdjRcvable methods
 *  Phanindra 12/25/2024 Modified PrintReceipt method to send customer name to report.
 *  Phanindra 12/30/2024 Fixed next seq number issue and print related changes.
 *  Phanindra 01/10/2025 Added bank related info in customer refund page
 *  Phanindra 02/27/2025 Worked on series issue in Customer refund and started working on printcheck
 *  Phanindra 03/26/2025 Modified AddCashReceipt to pass ups_ins values
 *  Phanindra 04/06/2025 Added GetNextCheckNoByBank method.
 *  Phanindra 04/24/2025 Fixed issues in Save customer refund functionality in saveReceipt function
 *  Phanindra 02/10/2026 migrated controller to YJCore
 */
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data;
using YJWebCoreMVC.Filters;
using YJWebCoreMVC.Models;
using YJWebCoreMVC.Services;

namespace YJWebCoreMVC.Controllers
{
    [SessionCheck("UserId")]
    public class SalesPaymentsCreditsController : Controller
    {
        private readonly HelperCommonService _helperCommonService;
        private readonly SalesPaymentsCreditsService _salesPaymentsCreditsService;
        private readonly GlobalSettingsService _globalSettingsService;
        private readonly BankAccService _bankAccService;
        public SalesPaymentsCreditsController(HelperCommonService helperCommonService, SalesPaymentsCreditsService salesPaymentsCreditsService, GlobalSettingsService globalSettingsService, BankAccService bankAccService)
        {
            _helperCommonService = helperCommonService;
            _salesPaymentsCreditsService = salesPaymentsCreditsService;
            _globalSettingsService = globalSettingsService;
            _bankAccService = bankAccService;
        }

        public IActionResult AddCashReceipt()
        {
            SalesPaymentsCreditsModel objModel = new SalesPaymentsCreditsModel();

            objModel.CustomerCodes = _helperCommonService.GetAllCustomerCodes();
            objModel.PaymentTypes = _helperCommonService.GetDistPaymentType();
            objModel.AllBankCodes = _helperCommonService.GetAllBankCodes();
            objModel.DefaultBanks = _salesPaymentsCreditsService.GetDefaultBank();
            DataTable dtBankAcc = _helperCommonService.GetBankAcc(HttpContext.Session.GetString("STORE_CODE"));
            DataRow[] foundStyle = dtBankAcc.Select("is_default = '" + true + "'");
            //var upsValues = this.HttpContext.Application["GlobalSettings"] as UPS_INS;
            //ViewBag.MICR = upsValues.MICR.ToString();
            var upsValues = _globalSettingsService.GetGlobalSettings();
            ViewBag.MICR = upsValues.MICR.ToString();

            ViewBag.Title = "Add Cash Receipt";

            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("ReceiptNo")))
            {
                ViewBag.ReceiptNo = HttpContext.Session.GetString("ReceiptNo");
            }
            else
            {
                string ReceiptNo = _helperCommonService.GetNextSeqNo("PAYMENTS", "Inv_no", "500000", "RTV_PAY", "300000", "P");
                ViewBag.ReceiptNo = ReceiptNo;
                HttpContext.Session.SetString("ReceiptNo", ReceiptNo);
            }

            return View(objModel);
        }


        public string GetReceiptData(string acc, string receipt_no, bool lRefund = false, bool isccswipe = false)
        {
            var data = _helperCommonService.GetStoreProc("GetReceiptData", "@acc", acc, "@receipt_no", receipt_no, "@lRefund", lRefund.ToString(), "@isccswipe", isccswipe.ToString());
            return JsonConvert.SerializeObject(data);
        }


        public string SaveReceipt(string dtPayment, string acc, string receipt_no, DateTime? pay_date, DateTime? chk_date, string bank, string checkno, decimal chk_amt, decimal discount, bool showmemo, string pcname, string PaymentsTypes, string PaymentNote, string Cash_Register, string StoreCode, string loggeduser = "", string storecodeinuse = "", bool isRefund = false)
        {
            //SalesPaymentsCreditsModel objModel = new SalesPaymentsCreditsModel();
            var data = _salesPaymentsCreditsService.SaveCashReceipt(dtPayment, acc, receipt_no, pay_date, chk_date, bank, checkno, chk_amt, discount, showmemo, pcname, PaymentsTypes, PaymentNote, Cash_Register, StoreCode, loggeduser, storecodeinuse, isRefund);
            //return JsonConvert.SerializeObject(data);
            if (data == "Success")
            {
                if (PaymentsTypes == "CHECK")
                {
                    //BankAccModel bankAccModel = new BankAccModel();
                    DataRow dRow = _bankAccService.GetBankBycode(bank);

                    if (_helperCommonService.DataTableOK(dRow))
                    {
                        DateTime fdate = Convert.ToDateTime(pay_date);
                        DateTime tdate = Convert.ToDateTime(pay_date.Value.AddDays(1).Date);
                        DateTime depositdate = Convert.ToDateTime(chk_date);
                        string bcode = bank;
                        DataTable dtInvoiceItems = _bankAccService.ShowPaymentData(bcode, fdate, tdate);
                        if (_helperCommonService.DataTableOK(dtInvoiceItems) && dtInvoiceItems.Columns.Contains("Deposited"))
                        {
                            var rowsToUpdate = dtInvoiceItems.AsEnumerable();
                            foreach (var row in rowsToUpdate)
                                row["Deposited"] = true;
                        }
                        if (_helperCommonService.DataTableOK(dtInvoiceItems))
                        {
                            DataTable invItm1 = _bankAccService.AddDeposits(bcode, fdate, tdate, depositdate, _helperCommonService.GetDataTableXML("DEPOSIT_DATA", dtInvoiceItems), false, _helperCommonService.StoreCode, _helperCommonService.LoggedUser);
                        }

                    }
                }
                _helperCommonService.AddKeepRec("Add Receipt# " + receipt_no, null, false, "", acc);

            }
            return data;
        }


        public IActionResult EditCashReceipt()
        {
            SalesPaymentsCreditsModel objModel = new SalesPaymentsCreditsModel();

            objModel.CustomerCodes = _helperCommonService.GetAllCustomerCodes();
            objModel.PaymentTypes = _helperCommonService.GetDistPaymentType();
            objModel.AllBankCodes = _helperCommonService.GetAllBankCodes();

            ViewBag.Title = "Edit Cash Receipt";

            return View(objModel);
        }


        public string GetPayment(string inv_no, string rtv_pay = "")
        {
            if (rtv_pay == "")
            {
                var data = _salesPaymentsCreditsService.GetPayment(inv_no);
                return JsonConvert.SerializeObject(data);
            }
            else
            {
                var data = _salesPaymentsCreditsService.GetPayment(inv_no, rtv_pay);
                return JsonConvert.SerializeObject(data);
            }

        }


        public string GetReceiptEditData(string acc, string receipt_no, bool lRefund = false, bool isccswipe = false)
        {
            var data = _helperCommonService.GetStoreProc(lRefund ? "GetReceiptDataEdit_Refund" : "GetReceiptDataEdit", "@acc", acc, "@receipt_no", receipt_no, "@isccswipe", isccswipe.ToString());
            return JsonConvert.SerializeObject(data);
        }

        public IActionResult ReprintReceipt(string inv_no)
        {
            return View();
        }


        public IActionResult PrintCashReceipt(string inv_no)
        {
            SalesPaymentsCreditsModel objModel = new SalesPaymentsCreditsModel();
            objModel.ReceiptInfo = _salesPaymentsCreditsService.GetPayment(inv_no);
            var CustData = _helperCommonService.GetCustNamebyCode(objModel.ReceiptInfo["ACC"].ToString());
            ViewBag.CustomerName = "";
            if (CustData != null)
            {
                ViewBag.CustomerName = CustData["NAME"].ToString();
            }
            objModel.Note = "";
            objModel.Credit_No = inv_no;
            DataTable data = _salesPaymentsCreditsService.GetPayItems(inv_no);
            ViewBag.DvPayItems = new DataView(data);
            return View(objModel);
        }

        public IActionResult ModifyAcc_ofReceipt()
        {
            SalesPaymentsCreditsModel objModel = new SalesPaymentsCreditsModel();

            objModel.CustomerCodes = _helperCommonService.GetAllCustomerCodes();
            return View(objModel);
        }
        public string ModifyAccofReceipt(string recno, string OldAcc, string NewAcc)
        {
            string error = "";
            string result = "";
            var custRow = _salesPaymentsCreditsService.CheckValidCustomerCode(NewAcc);
            if (custRow == null)
            {
                error = "Invalid Customer Code";
            }
            else
            {
                string checkError = CheckOKReceiptToModifyCustomer(recno);
                if (!string.IsNullOrEmpty(checkError))
                {
                    error = checkError;
                }
                else
                {
                    result = _salesPaymentsCreditsService.ModifyCustomerCodeofReceipt(recno, OldAcc, NewAcc);
                }
            }
            var response = new { result = result, error = error };
            return JsonConvert.SerializeObject(response);
        }

        public string GetCustomerofReceipt(string recno)
        {
            string acc = "";
            DataTable data = _salesPaymentsCreditsService.GetCustomerofReceipt(recno);
            if (data.Rows.Count > 0)
            {
                acc = data.Rows[0]["ACC"].ToString();
            }

            var error = CheckOKReceiptToModifyCustomer(recno);
            var result = new { acc = acc, error = error };
            return JsonConvert.SerializeObject(result);
        }
        public string CheckOKReceiptToModifyCustomer(string recno)
        {
            string error = "";
            DataTable data = _salesPaymentsCreditsService.CheckOKReceiptToModifyCustomer(recno);
            if (!(data.Rows.Count > 0))
            {
                error = "Customer code can not modify for this receipt#";
            }
            return error;
        }


        public IActionResult DeleteReceipt()
        {
            return View();
        }


        public string CheckDeleteCashReceipt(string inv_no)
        {
            SalesPaymentsCreditsModel objModel = new SalesPaymentsCreditsModel();
            DataRow drPayment = _salesPaymentsCreditsService.GetPayment(inv_no);
            string message = "";
            if (drPayment == null)
            {
                message = "Invalid Receipt No.";
                var result = new { message = message };
                return JsonConvert.SerializeObject(result);
            }
            else
            {
                bool lRefund = Convert.ToBoolean(drPayment["IS_REFUND"]);
                string lname = lRefund ? "Refund" : "Receipt";
                DataRow drPaidComm = _salesPaymentsCreditsService.GetPaymentCommison(inv_no);
                if (drPaidComm != null)
                {
                    message = "Can not delete this " + lname + " because commission was paid on it already.";
                    var result1 = new { message = message };
                    return JsonConvert.SerializeObject(result1);
                }

                DateTime date = Convert.ToDateTime(drPayment["DATE"]);
                string cdate = date.Date.ToString("MM-dd-yyyy");
                message = "Customer: " + drPayment["ACC"].ToString().Trim() + "\nDate: " + cdate + "\nPaid: " + drPayment["paid"]
                + "\nAre you sure you want to delete the " + lname + "?";
                var result2 = new { acc = drPayment["ACC"].ToString().Trim(), paid = Convert.ToDecimal(drPayment["paid"]), transact = drPayment["transact"].ToString(), message = message };
                return JsonConvert.SerializeObject(result2);
            }

        }


        public string DeleteCashReceipt(string acc, string receipt_no, decimal paid, string transact)
        {
            string response = _salesPaymentsCreditsService.DeleteCashReceipt(acc, receipt_no, paid, transact);
            return response;
        }

        public IActionResult CustomerRefund()
        {
            SalesPaymentsCreditsModel objModel = new SalesPaymentsCreditsModel();

            objModel.CustomerCodes = _helperCommonService.GetAllCustomerCodes();
            objModel.PaymentTypes = _helperCommonService.GetDistPaymentType();
            objModel.AllBankCodes = _helperCommonService.GetAllBankCodes();
            objModel.DefaultBanks = _salesPaymentsCreditsService.GetDefaultBank();
            DataTable dtBankAcc = _helperCommonService.GetBankAcc(HttpContext.Session.GetString("STORE_CODE"));
            DataRow[] foundStyle = dtBankAcc.Select("is_default = '" + true + "'");

            objModel.DefaultBank = string.Empty;
            DataRow drBank = _helperCommonService.GetSqlRow("SELECT TOP 1 CODE FROM BANK_ACC WHERE REFUND_DEFAULT = 1");
            if (_helperCommonService.DataTableOK(drBank))
                objModel.DefaultBank = drBank["CODE"].ToString();

            ViewBag.Title = "Customer Refund";
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("ReceiptNo")))
            {
                ViewBag.ReceiptNo = HttpContext.Session.GetString("ReceiptNo");
            }
            else
            {
                string ReceiptNo = _helperCommonService.GetNextSeqNo("PAYMENTS", "Inv_no", "500000", "RTV_PAY", "300000", "P");
                ViewBag.ReceiptNo = ReceiptNo;
                HttpContext.Session.SetString("ReceiptNo", ReceiptNo);
            }
            ViewBag.PageType = "Refund";
            return View("AddCashReceipt", objModel);
        }


        public IActionResult AddEditCredit()
        {
            SalesPaymentsCreditsModel objModel = new SalesPaymentsCreditsModel();

            objModel.CustomerCodes = _helperCommonService.GetAllCustomerCodes();
            objModel.AllReasons = _helperCommonService.GetReasons();
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("CreditNo")))
            {
                ViewBag.CreditNo = HttpContext.Session.GetString("CreditNo");
            }
            else
            {
                string CreditNo = _helperCommonService.GetNextSeqNo("payments", "Inv_no", "600000", "RTV_PAY", "400000", "C");
                ViewBag.CreditNo = CreditNo;
                HttpContext.Session.SetString("CreditNo", CreditNo);
            }
            return View(objModel);
        }
        [HttpGet]
        public string GetCreditData(string txtBillAcc = "", string txtCreditNo = "", bool chkShowMemo = false)
        {
            DataTable dtCreditData = _salesPaymentsCreditsService.GetCreditData(txtBillAcc, txtCreditNo);
            foreach (DataRow row in dtCreditData.Rows)
            {
                if (!row["APPAMT"].Equals(row["APP_AMOUNT"]))
                {
                    row["APPAMT"] = row["APP_AMOUNT"];
                }
            }
            if (!chkShowMemo)
            {
                var filteredRows = dtCreditData.Select("REF_TYPE <> 'MEMO'");

                if (filteredRows.Any())
                {
                    dtCreditData = filteredRows.CopyToDataTable();
                }
                else
                {
                    dtCreditData = dtCreditData.Clone();
                }
                dtCreditData.DefaultView.Sort = "ref_date ASC, IREFNO ASC";
                dtCreditData = dtCreditData.DefaultView.ToTable();
            }
            DataRow selectedRow = null;
            int rowIndex = -1;

            for (int i = 0; i < dtCreditData.Rows.Count; i++)
            {
                DataRow row = dtCreditData.Rows[i];
                if (row["APPFLG"].ToString() == "C")
                {
                    selectedRow = row;
                    rowIndex = i;
                    break;
                }
            }
            var result = new
            {
                Data = dtCreditData,
                SelectedRowIndex = rowIndex
            };
            var data = JsonConvert.SerializeObject(result);
            return data;
        }

        [HttpPost]
        public JsonResult AddEditCreditDetails(string dtCreditJson, string txtBillAcc, string txtCreditNo, DateTime? txtDate, DateTime? txtRefDate, string txtNote, string txtAmount, bool chkShowMemo, string ddlReason, string LoggedUser = "", string custRef = "")
        {
            JsonResult result = null;
            try
            {
                if (string.IsNullOrWhiteSpace(txtCreditNo))
                {
                    result = Json(new { success = false, message = "Credit No. should not be blank." });
                    return result;
                }

                if (string.IsNullOrWhiteSpace(txtBillAcc))
                {
                    result = Json(new { success = false, message = "Billing Account no. cannot be empty." });
                    JsonConvert.SerializeObject(result);
                }
                var dtCredit = JsonConvert.DeserializeObject<DataTable>(dtCreditJson);

                // Save Logic
                string StoreCodeInUse = HttpContext.Session.GetString("STORE_CODE");
                string error = string.Empty;
                var isSaved = _salesPaymentsCreditsService.SaveCredit(dtCredit, txtBillAcc, txtCreditNo, txtDate, txtRefDate, txtNote, txtAmount, "A", chkShowMemo, ddlReason, StoreCodeInUse, LoggedUser, custRef);
                if (isSaved == "Success")
                {
                    //string nextCreditNo = _helperCommonService.GetNextSeqNo("credits", "Inv_no", "", "", "", "");
                    result = Json(new
                    {
                        success = true,
                        message = "Credit saved successfully.",
                        //nextCreditNo,
                        //todo discuss on frmPrintCredit details;
                        printDialogUrl = Url.Action("frmPrintCredit", "Credit", new { txtCreditNo })
                    });

                    HttpContext.Session.SetString("CreditNo", null);
                    return result;
                }
                else
                {
                    result = Json(new { success = false, message = $"Unable to Save Receipt - {error}" });
                    return result;
                }
            }
            catch (Exception ex)
            {
                result = Json(new { success = false, message = $"An error occurred: {ex.Message}" });
                return result;
            }
        }


        public IActionResult DeleteCredit()
        {
            return View();
        }

        [HttpPost]
        public IActionResult DeleteCredit(string creditNo)
        {

            if (string.IsNullOrWhiteSpace(creditNo))
            {
                return Json(new { success = false, message = "Please enter Credit No." });
            }

            var creditData = _salesPaymentsCreditsService.GetCredit(creditNo);
            if (creditData == null)
            {
                return Json(new { success = false, message = "Invalid Credit No." });
            }

            var paymentData = _salesPaymentsCreditsService.GetPayment(creditNo, "C");
            if (paymentData != null && Convert.ToDecimal(paymentData["applied"]) > 0)
            {
                return Json(new { success = false, message = "This Credit Has Already Been Applied To Clear Invoices, Credit Can Not Be Deleted" });
            }

            string error;
            if (_salesPaymentsCreditsService.DeleteCredit(paymentData["acc"].ToString(), creditNo, Convert.ToDecimal(paymentData["paid"]), out error))
            {

                return Json(new { success = true, message = "Credit Deleted successfully" });
            }
            else
            {
                return Json(new { success = false, message = "Credit Deletion unsuccessful" });
            }
        }


        public IActionResult ReprintCredit(string inv_no)
        {
            return View();
        }


        public IActionResult PrintCredit(string inv_no)
        {
            SalesPaymentsCreditsModel objModel = new SalesPaymentsCreditsModel();
            bool isApCredit = false;
            objModel.ReceiptInfo = _salesPaymentsCreditsService.GetPayment(inv_no, "C");
            string rcots = string.Empty;
            DataTable data = _salesPaymentsCreditsService.GetCreditPayment(inv_no, "C", isApCredit);
            if (data == null)
                data = new DataTable();

            if (data.Rows.Count == 0)
                data.Rows.Add();
            else
            {
                foreach (DataRow drRow in data.DefaultView.ToTable(true, "pay_no").Rows)
                    if (!string.IsNullOrWhiteSpace(drRow["pay_no"].ToString()))
                        rcots += string.Format("{0},", drRow["pay_no"].ToString());
            }

            objModel.Rcots = rcots.TrimEnd(',');
            objModel.DtCreditDetails = _salesPaymentsCreditsService.GetCreditDetails(inv_no, isApCredit);

            objModel.DvPayItems = new DataView(data);
            objModel.Note = "";
            DataRow drCredit = _salesPaymentsCreditsService.GetCredit(inv_no);
            if (drCredit != null)
                objModel.Note = drCredit["Note"].ToString();
            objModel.Credit_No = inv_no;
            if (objModel.ReceiptInfo["RTV_PAY"].ToString() == "R")
            {
                objModel.RTV_PAY = "RTV";
            }
            else if (objModel.ReceiptInfo["RTV_PAY"].ToString() == "I")
            {
                objModel.RTV_PAY = "INVOICE";
            }
            else if (objModel.ReceiptInfo["RTV_PAY"].ToString() == "M")
            {
                objModel.RTV_PAY = "MEMO";
            }
            else if (objModel.ReceiptInfo["RTV_PAY"].ToString() == "P")
            {
                objModel.RTV_PAY = "RECEIVABLE";
            }
            else if (objModel.ReceiptInfo["RTV_PAY"].ToString() == "C")
            {
                objModel.RTV_PAY = "CREDIT";
            }
            ViewBag.upsInsInfo = _helperCommonService.getUpsInsInfo();
            return View(objModel);
        }

        public string CheckCreditAdj(string creditno, string iscreditordebit)
        {
            DataTable dtpayment = _salesPaymentsCreditsService.CheckCreditAdj1(creditno, iscreditordebit);
            if (dtpayment != null && dtpayment.Rows.Count > 0)
            {
                return "Applied Credit Cannot be Edited";
            }
            return "";
        }

        public string GetCredit(string inv_no)
        {
            SalesPaymentsCreditsModel objModel = new SalesPaymentsCreditsModel();
            DataRow drCredit = _salesPaymentsCreditsService.GetCredit(inv_no);
            var rowData = drCredit.Table.Columns.Cast<DataColumn>().ToDictionary(col => col.ColumnName, col => drCredit[col]);
            var data = JsonConvert.SerializeObject(rowData);
            return data;
        }

        public IActionResult AddAdjReceivable()
        {
            SalesPaymentsCreditsModel objModel = new SalesPaymentsCreditsModel();
            objModel.CustomerCodes = _helperCommonService.GetAllCustomerCodes();
            ViewBag.Title = "Record Adj. Receivable";
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("AdjRcvableNo")))
            {
                ViewBag.AdjRcvableNo = HttpContext.Session.GetString("AdjRcvableNo");
            }
            else
            {
                string AdjRcvableNo = _helperCommonService.GetNextSeqNo("PAYMENTS", "Inv_no", "", "RTV_PAY", null, "F");
                ViewBag.CreditNo = AdjRcvableNo;
                HttpContext.Session.SetString("AdjRcvableNo", AdjRcvableNo);
            }

            return View(objModel);
        }

        public string GetAdjReceivableData(string acc)
        {
            var data = _helperCommonService.GetStoreProc("GetAdjReceivableData", "@acc", acc);
            return JsonConvert.SerializeObject(data);
        }

        public bool CheckReceiptExistOrNot(string RecNo, string RTV_PAY)
        {
            DataTable datatable = _helperCommonService.GetSqlData("SELECT * FROM PAYMENTS WHERE RTV_PAY = @RTV_PAY AND TRIM(INV_NO) = @RecNo",
                 "@RecNo", RecNo.Trim(), "@RTV_PAY", RTV_PAY);
            return (datatable.Rows.Count > 0);
        }

        public string SaveAdjRcvable(string dtPaymentJson, string acc, string adj_no, DateTime? entry_date, bool showmemo)
        {
            var dtPayment = JsonConvert.DeserializeObject<DataTable>(dtPaymentJson);
            var data = _salesPaymentsCreditsService.SaveAdjRcvable(dtPayment, acc, adj_no, entry_date, showmemo);
            if (data == true)
            {
                HttpContext.Session.SetString("AdjRcvableNo", null);
                return "Success";
            }
            else
            {
                return "Fail";
            }
        }

        public IActionResult EditAdjRcvable()
        {
            SalesPaymentsCreditsModel objModel = new SalesPaymentsCreditsModel();
            objModel.CustomerCodes = _helperCommonService.GetAllCustomerCodes();
            ViewBag.Title = "Edit Adj. Receivable";
            return View(objModel);
        }

        public IActionResult ReprintAdjRcvable()
        {
            return View();
        }

        [HttpGet]
        public IActionResult DeleteAdjRcvable()
        {
            return View();
        }


        public IActionResult PrintAdjRcvable(string inv_no)
        {
            SalesPaymentsCreditsModel objModel = new SalesPaymentsCreditsModel();
            objModel.ReceiptInfo = _salesPaymentsCreditsService.GetPayment(inv_no, "F");
            objModel.Note = "";
            objModel.Credit_No = inv_no;
            DataTable data = _salesPaymentsCreditsService.GetPayItems(inv_no);
            ViewBag.DvPayItems = new DataView(data);

            return View(objModel);
        }

        public string SaveEditAdjRcvable(string dtPayment, string acc, string adj_no, DateTime? entry_date)
        {
            var data = _salesPaymentsCreditsService.EditAdjRcvable(dtPayment, acc, adj_no, entry_date);
            return data.ToString();
        }

        [HttpPost]
        public string DeleteAdjRcvable(string acc, string receipt_no, decimal paid, string transact)
        {
            var data = _salesPaymentsCreditsService.DeleteAdjRcvable(acc, receipt_no, paid, transact);
            if (data == true)
            {
                return "Receivable deleted successfully.";
            }
            else
            {
                return "Issue in deleting Receivable";
            }
        }

        public string GetNxtCheckNo(string bank = "")
        {
            return _helperCommonService.GetNxtCheckNo(bank);
        }


        public string PrintCheck(string checkNo, string bank)
        {
            string checkdate = string.Empty;
            DataTable dtCheckD = new DataTable();
            DataTable dtCheck = CheckValidCheckNo(checkNo, bank);
            dtCheckD = dtCheck;
            if (!_helperCommonService.DataTableOK(dtCheck))
                return "";
            dtCheck.Columns.Add("CAMOUNT", typeof(System.String), string.Empty);
            for (int i = 0; i < dtCheck.Rows.Count; i++)
                dtCheck.Rows[i]["CAMOUNT"] = _helperCommonService.NumberToCurrencyText(Convert.ToDecimal(dtCheck.Rows[i]["Amount"]), MidpointRounding.ToEven);
            dtCheck.AcceptChanges();

            /* To add columns to without bills to use same reports to with bills,,,to not getting sub reports error*/
            dtCheckD.Columns.Add("INV_NO", typeof(System.String), string.Empty);
            dtCheckD.Columns.Add("Type", typeof(System.String), string.Empty);
            dtCheckD.Columns.Add("date", typeof(System.String), string.Empty);
            dtCheckD.Columns.Add("VND_NO", typeof(System.String), string.Empty);
            dtCheckD.Columns.Add("DISCOUNT", typeof(System.String), string.Empty);
            dtCheck.AcceptChanges();

            string custemail = string.Empty;
            string strVendorCode = string.Empty;

            for (int i = 0; i < dtCheck.Rows.Count; i++)
            {
                checkdate = dtCheck.Rows[i]["DATE"] == DBNull.Value ? string.Empty : string.Format("{0:MM/dd/yyyy}", Convert.ToDateTime(dtCheck.Rows[i]["DATE"]));
                strVendorCode = _helperCommonService.CheckForDBNull(dtCheck.Rows[i]["Acc"].ToString(), typeof(string));
            }

            DataRow drvendor = CheckValidCustomerCode(strVendorCode, _helperCommonService.is_Glenn);

            //ReportPrinting objReportPrinting = new ReportPrinting();
            //Microsoft.Reporting.WinForms.ReportDataSource[] reportDataSourceCollection = new Microsoft.Reporting.WinForms.ReportDataSource[1];

            //Microsoft.Reporting.WinForms.ReportParameter[] reportParameterCollection;

            //reportDataSourceCollection[0] = new Microsoft.Reporting.WinForms.ReportDataSource();
            //reportDataSourceCollection[0].Name = "DataSet1";
            //reportDataSourceCollection[0].Value = dtCheck;

            //reportParameterCollection = new Microsoft.Reporting.WinForms.ReportParameter[4];

            //if (_helperCommonService.DataTableOK(drvendor) && strVendorCode != "NONE")
            //{
            //    reportParameterCollection[0] = new Microsoft.Reporting.WinForms.ReportParameter();
            //    reportParameterCollection[0].Name = "rpVendor";
            //    reportParameterCollection[0].Values.Add(string.Format("{0}", drvendor["name"].ToString()));

            //    string[] lines = new string[]
            //    {
            //        drvendor["name"].ToString(),
            //        drvendor["addr1"].ToString(),
            //        drvendor["addr12"].ToString(),
            //        string.Format("{0} {1} {2} {3}",_helperCommonService.CheckForDBNull(drvendor["City1"]),_helperCommonService.CheckForDBNull(drvendor["State1"]),_helperCommonService.CheckForDBNull(drvendor["Country"]),_helperCommonService.CheckForDBNull(drvendor["ZIP1"]))
            //    };

            //    IEnumerable<string> filledLines = lines.Where(s => !String.IsNullOrWhiteSpace(s));

            //    string address = string.Join(Environment.NewLine, filledLines);

            //    reportParameterCollection[1] = new Microsoft.Reporting.WinForms.ReportParameter();
            //    reportParameterCollection[1].Name = "rpAddress";
            //    reportParameterCollection[1].Values.Add(address);
            //}
            //else
            //{
            //    reportParameterCollection[0] = new Microsoft.Reporting.WinForms.ReportParameter();
            //    reportParameterCollection[0].Name = "rpVendor";
            //    reportParameterCollection[0].Values.Add(string.Format("{0}", dtCheck.Rows[0]["name"].ToString()));

            //    string[] lines = new string[]
            //       {
            //        string.Empty,
            //        string.Empty,
            //        string.Empty,
            //        string.Empty
            //       };

            //    IEnumerable<string> filledLines = lines.Where(s => !String.IsNullOrWhiteSpace(s));

            //    string address = string.Join(Environment.NewLine, filledLines);

            //    reportParameterCollection[1] = new Microsoft.Reporting.WinForms.ReportParameter();
            //    reportParameterCollection[1].Name = "rpAddress";
            //    reportParameterCollection[1].Values.Add(address);
            //}

            //reportParameterCollection[2] = new Microsoft.Reporting.WinForms.ReportParameter();
            //reportParameterCollection[2].Name = "checkdate";
            //reportParameterCollection[2].Values.Add(checkdate);

            //reportParameterCollection[3] = new Microsoft.Reporting.WinForms.ReportParameter();
            //reportParameterCollection[3].Name = "rpCheckNo";
            //reportParameterCollection[3].Values.Add(txtCheckNo.Text);

            //string subReportname = "rptCheckSR.rdlc";
            //string subReportIdName = "rptCheckSR";

            //objReportPrinting.report.SubreportProcessing += new SubreportProcessingEventHandler(localReport_SubreportProcessing);
            //objReportPrinting.reportViewer.LocalReport.SubreportProcessing += new SubreportProcessingEventHandler(localReport_SubreportProcessing);

            //_helperCommonService.PrintReport(objReportPrinting, "Print Check",
            //    _helperCommonService.CheckReport(),
            //    "Preview", reportDataSourceCollection, reportParameterCollection,
            //    subReportname, subReportIdName, custemail);
            return "";
        }

        public DataTable CheckValidCheckNo(string check_no, string bank)
        {
            string CommandText = "select CHECKS.*, CAST(BANK.CLRD AS BIT) as CLRD1 from checks LEFT JOIN Bank ON TRIM(Bank.inv_no)=TRIM(CHECKS.TRANSACT) WHERE TRIM(checks.bank)= @bank AND TRIM(CHECKS.check_no) = @chk_no";
            return _helperCommonService.GetSqlData(CommandText, "@chk_no", check_no.Trim(), "@bank", bank.Trim());
        }

        public DataRow CheckValidCustomerCode(string acc, bool is_glenn, bool iSWrist = false)
        {
            if (iSWrist)
            {
                DataRow rw = _helperCommonService.GetSqlRow("select [NAME2] NAME From Customer Where trim(acc)=trim(@acc) or old_customer=trim(@acc)", "@acc", acc);
                if (rw == null || string.IsNullOrWhiteSpace(Convert.ToString(rw["NAME"])))
                    return _helperCommonService.GetSqlRow("select *  From Customer Where trim(acc)=trim(@acc) or old_customer=trim(@acc)", "@acc", acc);
                return _helperCommonService.GetSqlRow("select [NAME2] NAME, [ADDR2] ADDR1,[ADDR22] ADDR12,[CITY2] CITY1,[STATE2] STATE1,[ZIP2] ZIP1,ADDR13,[COUNTRY2] COUNTRY,[TEL2] TEL,*  From Customer Where trim(acc)=trim(@acc) or old_customer=trim(@acc)", "@acc", acc);
            }
            return _helperCommonService.GetSqlRow("select *  From Customer Where trim(acc)=trim(@acc) or old_customer=trim(@acc)", "@acc", acc);
        }

        public string GetNextCheckNoByBank(string bank)
        {
            return _helperCommonService.GetNextCheckNoByBank(bank);
        }



    }
}
