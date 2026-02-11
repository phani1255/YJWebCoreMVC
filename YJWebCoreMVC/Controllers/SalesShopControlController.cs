/*
 * Created by Manoj 29-Apr-2025
 *  Manoj 30-Apr-2025 Added new ListOfCompletedJobs and GetListofCompletedJobs Methods
 *  Manoj 02-May-2025 Added listofopenjobstoeachperson and getlistofopenjobstoeachperson Methods
 *  Manoj 05-May-2025 Added GetFilteredSummarizedListofOpenJobs Method
 *  Manoj 06-May-2025 Added GetPersonsFilterList Method
 *  Manoj 07-May-2025 Added ListOfJobsDoneByaPerson Method
 *  Manoj 08-May-2025 Added PromisedvsCompletedDates and ListofPromisedvsCompletedDates Methods
 *  Manoj 09-May-2025 Added ListOfRepairJobsNotSentToAnyPerson Method
 *  Manoj 12-May-2025 Added GetListOfRepairJobsNotSentToAnyPerson Method
 *  Manoj 16-May-2025 Added ListOfRepairJobsReadForPickup Method
 *  Manoj 21-May-2025 Added ListEstimates Method
 *  Manoj 22-May-2025 Added GetAllListOfJobEstimates Method TimeSpentOnJobBags
 *  Manoj 26-May-2025 Added GetListofHistoryOfJobBag and HistoryOfJob  Methods
 *  Manoj 27-May-2025 Added GetListJobbagNotes And TimeSpentOnJobBags and GetListTimeSpentjobbag  Methods
 *  Manoj 28-May-2025 Added  GetListOfPartsUsedJobbag Method
 *  Manoj 29-May-2025 Added saveHistoryRepairJobNotes Method
 *  Manoj 02-june-2025 Fixed saveHistoryRepairJobNotes Method to read request stream properly And save notes correctly
 *  Manoj 04-June-2025 fixed saveHistoryRepairJobNotes  Json  response to return success or failure;
 *  Manoj 07-July-2025 Added  ComparisionOfTime,GetListOfTimeComparision,GetTimeComparision
 *  Manoj 27-Aug-2025 Added extra parameter in HistoryOfJob
 *  Hemanth   09-Sep-2025 Hemanth hide jobbag and refresh options from list of locations
 *  Phanindra 10/09/2025 Added GiveOutJobBags, SaveGiveJobToPerson, GiveJobToAPerson, SaveGiveJobToAPerson, GetJobBackFromPerson, SaveGetJobBackFromPerson
 *  Dharani   10/10/2025 Added Persons method
 *  Dharani   10/13/2025 Added GetAllSetters, GetGLCodes, GetEmpCodes, GetDeptCodes, CheckValidMfgDept, GetGLNameByACC, SavePersons and DeletePerson methods
 *  Dharani   10/14/2025 Added AddEditDept, Getalldepts, CheckValidDepat and SaveDepartments methods
 *  Phanindra 10/16/2025 Added GetCustNotesBasedOnRepairNo method
 *  Phanindra 10/24/2025 added SaveGiveOutJobBags, NoteUpdate, loadGiveOutJobBagDetails, AddRecordToHistory, saveJobAddToStock, RemoveLeadingZero, AddToHistory
 *  Phanindra 10/26/2025 added SaveReturnJobBack, ValidateGivenJobBag, BuildWorkingDataTable, ProcessPickupAndStock, ResolveOpenSetter, AddRecordToHistoryPersonBack, AddToStockOrReserve
 *  Manoj     10/28/2025 Added  penAClosedJobBag,getopenCloseJobNo,openCloseJobBagNo,OpenClosedJob,RepairRcvd,GetListOfJobsForAck,SaveRepRcvInShop,RepRcvInShop Methods
 *  Phanindra 10/29/2025 Added SaveCompletedReadyForPickup, SetReadyForPickup, IsCompletedJob and modified saveJobAddToStock.
 *  Manoj     10/31/2025 Added SentBackFromShop
 *  Manoj     11/03/2025 Added Send2Shop,SaveTransferJobBacktoShop,GetCheckRprJob,CheckValidJobbag
 * Phanindra  11/04/2025 Added ReprintJobbag, PrintGiveJobToAPerson methods and updated GiveJobToAPerson, GetJobBackFromPerson
 * Phanindra  11/10/2025 modified PrintGiveJobToAPerson method
 * Manoj      11/14/2025 fixed DateTime nav char issue on  SaveTransferJobBacktoShop method
 * Phanindra  11/19/2025 Added PrintGetJobBackFromPerson, modified SaveGiveOutJobBags, AddRecordToHistory method
 * Phanindra  11/27/2025 modified AddRecordToHistory method
 * Manoj      12/24/2025 Added ReadActualHours,ValidateJobBag,CheckOpenJobExist,updateReadActualHours
 * Manoj      12/25/2025 Added CalculateQueuedJobs,GetListCalcDaysForQueuedJobs,CalculateQueuedJobsGenerateReport,PrintViewerReport Methods
 * Manoj      12/26/2025 Modified updateReadActualHours,CheckOpenJobExist Methods jobbag validation return message
 * Manoj      01/07/2026 Added GetListOfActualDuration,SaveAdjustActualDuration Methods
 * Manoj      01/09/2026 Modified GetListOfActualDuration Method to update jobBag Value format before fetching the data
 * Manoj      01/09/2026 Added Estimates
 * Manoj      01/12/2026 Added GetListOfEstimates,GetListOfEstimateTemplates
 * Manoj      01/19/2026 Added viewEstimateTemp Method
 * Manoj      01/20/2026 Added SaveEstimateTemplates,UpdateEstimateTemplateData Methods
 * Manoj      01/21/2026 Added saveUpdateEstimates Method
 * Manoj      01/22/2026 Added DeleteEstimateTemplate Methods
 * Manoj      01/29/2026 Modified GetListOfEstimates form image data
 */

using Microsoft.Reporting.WebForms;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using YJWeb.Filters;
using YJWeb.Helpers;
using YJWeb.Models;
using YJWeb.ReportEngine;

namespace YJWeb.Controllers
{
    public class SalesShopControlController : ReportPrintingController
    {
        MfgModel mfgModel = new MfgModel();
        public ActionResult listofjobsgiventoaperson()
        {
            DataTable dataTable = mfgModel.getallsetters();
            List<SelectListItem> salesmanList = new List<SelectListItem>();
            salesmanList.Add(new SelectListItem() { Text = "All", Value = "" });
            if (dataTable.Rows.Count > 0)
            {
                foreach (DataRow dr in dataTable.Rows)
                {
                    salesmanList.Add(new SelectListItem() { Text = dr["NAME"].ToString().Trim(), Value = dr["NAME"].ToString().Trim() });
                }
            }
            mfgModel.setters = salesmanList;
            return View(mfgModel);
        }

        public string GetListofJobsGiventoaPerson(string settername, string fromdate, string todate, string datecondition)
        {
            DataTable data = mfgModel.listofjobsgiventoaperson(settername, fromdate, todate, datecondition);
            return JsonConvert.SerializeObject(data);
        }

        public ActionResult ListOfCompletedJobs()
        {
            return View();
        }

        public string GetListofCompletedJobs(string fromdate, string todate)
        {
            DataTable data = mfgModel.listofjobscompleted(fromdate, todate);
            return JsonConvert.SerializeObject(data);
        }

        public ActionResult listofopenjobstoeachperson()
        {
            DataTable dataTable = mfgModel.getallsetters();
            List<SelectListItem> openJobs = new List<SelectListItem>();
            openJobs.Add(new SelectListItem() { Text = "All", Value = "" });

            if (dataTable.Rows.Count > 0)
            {
                foreach (DataRow dr in dataTable.Rows)
                {
                    openJobs.Add(new SelectListItem() { Text = dr["Name"].ToString().Trim(), Value = dr["Name"].ToString().Trim() });
                }
            }
            mfgModel.setters = openJobs;
            return View(mfgModel);
        }

        public string getlistofopenjobstoeachperson(string fromdate, string todate, string summaryBy, string filter, string frmDueDate, string toDueDate, bool IsPastDue, bool IsPersonDueDate)
        {

            DataTable data = mfgModel.GetListofOpenJobs(fromdate, todate, summaryBy, filter, frmDueDate, toDueDate, "", 0, 0, "", false, IsPastDue, IsPersonDueDate);
            return JsonConvert.SerializeObject(data);

        }

        public string GetFilteredSummarizedListofOpenJobs(string fromdate, string todate, string summaryBy, string filter, string frmDueDate, string toDueDate, bool IsPastDue, bool IsPersonDueDate)
        {
            DataTable data = mfgModel.SummarizedListofOpenJobs(fromdate, todate, summaryBy, filter, frmDueDate, toDueDate, IsPastDue, IsPersonDueDate);
            return JsonConvert.SerializeObject(data);
        }

        public string GetPersonsFilterList(string filter)
        {
            DataTable data = mfgModel.GetFilterList(filter);
            return JsonConvert.SerializeObject(data);

        }

        public ActionResult ListOfJobsDoneByaPerson()
        {
            DataTable dataTable = mfgModel.getallsetters();

            List<SelectListItem> setter = new List<SelectListItem>();
            setter.Add(new SelectListItem() { Text = "All", Value = "" });

            if (dataTable.Rows.Count > 0)
            {
                foreach (DataRow dr in dataTable.Rows)
                {
                    setter.Add(new SelectListItem() { Text = dr["Name"].ToString().Trim(), Value = dr["Name"].ToString().Trim() });
                }
            }
            mfgModel.setters = setter;
            return View(mfgModel);
        }

        public ActionResult PromisedvsCompletedDates()
        {
            return View();
        }

        public string ListofPromisedvsCompletedDates(string fdate, string tdate, bool AllDates = false)
        {
            DataTable data = mfgModel.GetListofPromisedvsCompletedDates(fdate, tdate, AllDates);

            return JsonConvert.SerializeObject(data);
        }

        public ActionResult ListOfRepairJobsNotSentToAnyPerson()
        {
            return View("~/Views/Repairs/ListOfRepairJobsNotSentToAnyPerson.cshtml");
        }

        public string GetListOfRepairJobsNotSentToAnyPerson(bool isreadyForPickup)
        {
            DataTable data = MfgModel.GetListofRepairJobs(isreadyForPickup);

            return JsonConvert.SerializeObject(data);
        }


        public ActionResult ListOfRepairJobsReadForPickup()
        {
            return View("~/Views/Repairs/ListOfRepairJobsReadForPickup.cshtml");
        }

        public ActionResult ListEstimates()
        {
            DataTable data = mfgModel.getallsetters();

            List<SelectListItem> setter = new List<SelectListItem>();

            if (data.Rows.Count > 0)
            {
                foreach (DataRow dr in data.Rows)
                {
                    setter.Add(new SelectListItem() { Text = dr["Name"].ToString().Trim(), Value = dr["Name"].ToString().Trim() });
                }
            }

            mfgModel.setters = setter;
            return View("~/Views/Estimates/ListEstimates.cshtml", mfgModel);
        }

        public string GetAllListOfJobEstimates(string setter, bool lDetail, int OpenDone)
        {
            DataSet dtSet = Helper.GetListOfJobEstimates(setter, lDetail, OpenDone);
            return JsonConvert.SerializeObject(dtSet);
        }


        public ActionResult HistoryOfJob(string JobbagNumber = "", string IsFromRepair = "")
        {
            ViewBag.Jobbag = JobbagNumber;
            ViewBag.IsFromRepair = (IsFromRepair == "1") ? "readonly='readonly'" : "";
            ViewBag.DisplayButton = (IsFromRepair == "1") ? "style='display:none;'" : "";
            return View();
        }

        public string GetListofHistoryOfJobBag(string jobbagno)
        {
            DataTable jobbaginfo = mfgModel.reprintjobbag(jobbagno, true);
            DataTable jobbagissplitted = MfgModel.checkJobBagIsSplitOrNot(jobbagno);
            DataTable chkJobBagSender = MfgModel.checkJobBagSendRecToShop(jobbagno);


            if (Helper.DataTableOK(jobbaginfo) || Helper.DataTableOK(jobbagissplitted) || Helper.DataTableOK(chkJobBagSender))
            {
                DataTable result = MfgModel.GETHISTORYOFJOBBAG(jobbagno, Helper.LoggedUser);

                var data = new Dictionary<string, object>();

                if (jobbaginfo.Rows.Count > 0)
                {
                    data.Add("jobbaginfo", jobbaginfo);
                }

                data.Add("result", result);

                if (Helper.CompanyName != null || Helper.CompanyName != "")
                {
                    data.Add("CompanyName", Helper.CompanyName);
                }
                if (Helper.CompanyAddr1 != null || Helper.CompanyAddr1 != "")
                {
                    data.Add("storeAddress", Helper.CompanyAddr1);
                }
                if (Helper.CompanyAddr2 != null || Helper.CompanyAddr2 != "")
                {
                    data.Add("storeAddress2", Helper.CompanyAddr2);
                }
                if (Helper.CompanyTel != null || Helper.CompanyTel != "")
                {
                    data.Add("storePhone", Helper.CompanyTel);
                }
                if (Helper.GetStoreImage() != null)
                {
                    data.Add("storeImage", Convert.ToBase64String(Helper.GetStoreImage()));
                }

                return JsonConvert.SerializeObject(data);
            }
            else
            {
                DataTable result = MfgModel.GETHISTORYOFJOBBAG("", "");
                return JsonConvert.SerializeObject(result);
            }
        }

        public string GetListJobbagNotes(string Jobbag)
        {
            DataTable data = MfgModel.GetJobbagNotes(Jobbag);
            return JsonConvert.SerializeObject(data);
        }

        public string GetListOfPartsUsedJobbag(string JobBagno)
        {
            DataTable dt = MfgModel.GetpartshistByJobBag(JobBagno);

            decimal totalPriceSum = 0;

            if (Helper.DataTableOK(dt) && dt.Rows.Count > 0)
            {
                var result = dt.Compute("sum(total_price)", string.Empty);

                if (result != DBNull.Value)
                {
                    totalPriceSum = Convert.ToDecimal(result);
                }
            }
            Helper.UpdateJobBagPrice(JobBagno.TrimStart('0'), totalPriceSum);
            return JsonConvert.SerializeObject(dt);

        }

        [HttpPost]
        public JsonResult saveHistoryRepairJobNotes(string xml, string jobbag)
        {
            bool success = true;

            using (var reader = new StreamReader(Request.InputStream))
            {
                xml = reader.ReadToEnd().Trim();
            }
            if (!string.IsNullOrEmpty(xml) && !string.IsNullOrEmpty(jobbag))
            {
                Helper.AddJobbagNotes(xml, jobbag);
                success = true;
            }
            else
            {
                success = false;
            }

            return Json(new { success = success });

        }
        public ActionResult TimeSpentOnJobBags()
        {
            DataTable data = mfgModel.getallsetters();

            List<SelectListItem> person = new List<SelectListItem>();
            person.Add(new SelectListItem() { Text = "All", Value = "" });

            if (data.Rows.Count > 0)
            {
                foreach (DataRow dr in data.Rows)
                {
                    person.Add(new SelectListItem() { Text = dr["Name"].ToString().Trim(), Value = dr["Name"].ToString().Trim() });
                }
            }
            mfgModel.setters = person;
            return View(mfgModel);
        }

        public string GetListTimeSpentjobbag(string jobbagno1, string FromDate, string ToDate, string strPerson)
        {
            DataSet data = Helper.GetTimeSpentjobbag(jobbagno1, Convert.ToDateTime(FromDate), Convert.ToDateTime(ToDate), strPerson);
            return JsonConvert.SerializeObject(data);
        }

        public ActionResult ComparisionOfTime()
        {

            DataTable PersonList = mfgModel.getallsetters();

            List<SelectListItem> persons = new List<SelectListItem>();

            if (PersonList.Rows.Count > 0)
            {
                foreach (DataRow dr in PersonList.Rows)
                {
                    persons.Add(new SelectListItem() { Text = dr["Name"].ToString().Trim(), Value = dr["Name"].ToString().Trim() });
                }
            }

            mfgModel.setters = persons;
            return View("~/Views/Estimates/ComparisionOfTime.cshtml", mfgModel);
        }

        public string GetListOfTimeComparision(string cPerson, DateTime cfrmDate1, DateTime ctoDate2)
        {


            DataSet compareData = GetTimeComparision(cPerson, Helper.setSQLDateTime(cfrmDate1), Helper.setSQLDateTime(ctoDate2));


            string result;

            if (compareData.Tables.Count > 0)
            {
                result = JsonConvert.SerializeObject(compareData);
            }
            else
            {
                result = JsonConvert.SerializeObject(new { success = false, message = "No Data Found" });
            }
            return result;
        }

        public static DataSet GetTimeComparision(string cPerson, DateTime cDate1, DateTime cDate2)
        {
            DataSet dataSet = new DataSet();
            using (SqlConnection conn = new SqlConnection(Helper.GetConnectionString()))
            using (var sqlDataAdapter = new SqlDataAdapter())
            {
                sqlDataAdapter.SelectCommand = new SqlCommand("GetTimeComparision", conn)
                {
                    CommandType = CommandType.StoredProcedure,
                    CommandTimeout = 3000
                };

                // Add parameters
                sqlDataAdapter.SelectCommand.Parameters.AddWithValue("@SETTERNAME", cPerson);
                sqlDataAdapter.SelectCommand.Parameters.AddWithValue("@cDate1", cDate1);
                sqlDataAdapter.SelectCommand.Parameters.AddWithValue("@cDate2", cDate2);

                // Fill the dataset from the adapter
                sqlDataAdapter.Fill(dataSet);
            }
            return dataSet;
        }


        #region Phanindra Methods

        public ActionResult GiveOutJobBags()
        {
            return View();
        }

        public JsonResult SaveGiveOutJobBags(string jobBagNo, List<JobBagHistory> history, string setterName, DateTime duedate, string txtStyleNum, decimal txtQty = 0, string deltransactno = "")
        {
            bool isFromOpenJobs = false;
            try
            {
                if (string.IsNullOrWhiteSpace(jobBagNo))
                    return Json(new { success = false, message = "JobBag # Required." });

                if (Helper.GetClosedJobBag(jobBagNo))
                {
                    return Json(new { success = false, message = "Job is already closed." });
                }

                NoteUpdate(history);

                if (!string.IsNullOrEmpty(setterName) || !string.IsNullOrEmpty(deltransactno))
                    AddRecordToHistory(setterName, jobBagNo, duedate, txtStyleNum, txtQty, deltransactno);


                bool flag = false;
                //bool isClosed = false;

                if (!flag)
                {
                    //isClosed = true;

                    if (!isFromOpenJobs)
                        return Json(new { success = true, message = "History Updated Successfully." });

                    //LoadSpecialOrderProcessedData(jobBagNo);
                    flag = false;
                }
                return Json(new { success = true, message = "History Updated Successfully." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Error" + ex.Message });
            }
        }

        private void NoteUpdate(List<JobBagHistory> historyList)
        {
            if (historyList == null || historyList.Count == 0)
                return;

            foreach (var row in historyList)
            {
                string strPerson = !string.IsNullOrWhiteSpace(row.GiveToPerson)
                    ? row.GiveToPerson
                    : row.TakeBackFrom;

                string transact = row.Transact ?? "";  // You can include this field in your JSON if needed
                string note1 = row.Note1 ?? "";
                string note2 = row.Note2 ?? "";
                string dueDate = row.DueDate ?? "";

                Helper.UpdatemfgNotes(strPerson, transact, note1, note2, dueDate);
            }
        }

        public JsonResult loadGiveOutJobBagDetails(string jobBagNo)
        {
            string jobbagno1 = Helper.JobNormal(jobBagNo);

            string retmsg;
            Helper.CheckJobBagValidity(jobBagNo, out retmsg);
            if (retmsg != "OK")
            {
                return Json(new
                {
                    success = false,
                    message = "This Job Bag Already Closed."
                }, JsonRequestBehavior.AllowGet);
            }

            DataTable jobbagInfo = Helper.reprintjobbag(jobbagno1, true);
            DataTable allSetters = Helper.GetAllSetters();
            DataTable allSettersForGrid = Helper.GetAllSettersForGrid();
            DataTable result = Helper.GETHISTORYOFJOBBAGForGiveOut(jobbagno1);

            var jobbagInfoData = jobbagInfo.AsEnumerable()
                .Select(row => jobbagInfo.Columns
                    .Cast<DataColumn>()
                    .ToDictionary(col => col.ColumnName, col => row[col]))
                .ToList();

            var allSettersData = allSetters.AsEnumerable()
                .Select(row => allSetters.Columns
                    .Cast<DataColumn>()
                    .ToDictionary(col => col.ColumnName, col => row[col]))
                .ToList();

            var allSettersForGridData = allSettersForGrid.AsEnumerable()
                .Select(row => allSettersForGrid.Columns
                    .Cast<DataColumn>()
                    .ToDictionary(col => col.ColumnName, col => row[col]))
                .ToList();

            var resultData = result.AsEnumerable()
                .Select(row => result.Columns
                    .Cast<DataColumn>()
                    .ToDictionary(col => col.ColumnName, col => row[col]))
                .ToList();

            return Json(new
            {
                success = true,
                jobbagInfo = jobbagInfoData,
                allSetters = allSettersData,
                history = resultData
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult AddRecordToHistory(string settername, string jobbagno, DateTime duedate, string txtStyleNum, decimal txtQty = 0, string deltransactno = "")
        {
            try
            {
                string normalizedJobBagNo = Helper.JobNormal(jobbagno);

                DataTable jobbaginfo = Helper.reprintjobbag(normalizedJobBagNo, true);
                if (!Helper.DataTableOK(jobbaginfo))
                {
                    DataTable isSplit = Helper.checkJobBagIsSplitOrNot(normalizedJobBagNo);
                    if (Helper.DataTableOK(isSplit))
                        return Json(new { success = false, message = "JobBag # is Split." });
                    else
                        return Json(new { success = false, message = "Invalid JobBag #." });
                }

                bool exists = Helper.GetJbbagExistCurrentsetter(normalizedJobBagNo, Helper.EscapeSpecialCharacters(settername));
                if (exists)
                {
                    return Json(new { success = false, message = $"JobBag {normalizedJobBagNo} is already given to this person." });
                }

                // Assume job not closed or completed check done elsewhere
                bool YesJobbagCompl = false;
                bool isJobCompleted = false; // You can fetch this from DB if needed
                if (isJobCompleted)
                {
                    YesJobbagCompl = true;
                }

                var result = Helper.GETHISTORYOFJOBBAGForGiveOut(normalizedJobBagNo); // Replace with your real data if required

                Helper.UpdateHistory(
                    normalizedJobBagNo,
                    settername,
                    Helper.CheckForDBNull(txtQty, typeof(decimal).ToString()), // Example
                    0,
                    Helper.GetDataTableXML("TRANSNOTES", result),
                    YesJobbagCompl,
                    "",
                    deltransactno,
                    duedate
                );

                return Json(new { success = true, message = "Job history updated successfully." });
            }
            catch (SqlException ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult saveJobAddToStock(string jobbagno, string settername = "", int fin_rsrv = 0)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(jobbagno))
                    return Json(new { success = false, message = "JobBag # is required." });

                if (IsCompletedJob(jobbagno))
                    return Json(new { success = false, message = "JobBag was already completed." });

                if (Helper.GetClosedJobBag(jobbagno))
                    return Json(new { success = false, message = "This Job Bag is already closed." }, JsonRequestBehavior.AllowGet);

                if (!Helper.iSJobbagIsPaidFull(true, jobbagno) && !Helper.iSJobbagIsPaidFull(false, jobbagno))
                    return Json(new { success = false, message = "JobBag not paid in full, cannot complete and add to stock." });

                //string lastUpdate = Helper.GetLastRepairUpdate(jobbagno.TrimStart('0'));
                //if (!string.IsNullOrEmpty(lastUpdate) && lastUpdate != Helper.GetLastKnownLocalUpdate(jobbagno))
                //    return Json(new { success = false, message = "JobBag was modified by another user. Please reload." });

                // Validate closing state
                if (!Helper.iSValidForCloseRepair(jobbagno))
                {
                    bool closed = Helper.CloseRepairOrdes(jobbagno);
                    if (!closed)
                        return Json(new { success = false, message = "Unable to close the repair order." });
                    Helper.AddKeepRec($"JobBag #{jobbagno} was added to stock", null, false, "", "", "", jobbagno);
                }

                // 🔁 Reuse shared function here
                var result = AddToHistory(jobbagno, settername, fin_rsrv: fin_rsrv);

                return Json(new { success = result.Key, message = result.Value });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Error: " + ex.Message });
            }
        }

        public string RemoveLeadingZero(string what)
        {
            return Helper.Pad6(what.TrimStart('0'));
        }

        public KeyValuePair<bool, string> AddToHistory(string jobbagno, string settername, int fin_rsrv)
        {

            MfgModel mfgModel = new MfgModel();
            try
            {
                string normalizedJobBagNo = Helper.JobNormal(jobbagno);
                DataTable jobbaginfo = Helper.reprintjobbag(normalizedJobBagNo, true);

                if (!Helper.DataTableOK(jobbaginfo))
                {
                    DataTable jobbagSplit = Helper.checkJobBagIsSplitOrNot(normalizedJobBagNo);
                    if (Helper.DataTableOK(jobbagSplit))
                        return new KeyValuePair<bool, string>(false, "JobBag is already split.");
                    else
                        return new KeyValuePair<bool, string>(false, "Invalid JobBag #.");
                }

                // Get invoice info
                string repairinvstyle = "";
                DataTable dtChkInv = Helper.CheckValidOrderRepair(jobbagno);
                if (Helper.DataTableOK(dtChkInv))
                {
                    string invNo = Convert.ToString(dtChkInv.Rows[0]["inv_no"]);
                    if (!string.IsNullOrEmpty(invNo))
                    {
                        DataTable dtInv = Helper.CheckInvoice(invNo);
                        if (Helper.DataTableOK(dtInv))
                        {
                            repairinvstyle = Convert.ToString(dtInv.Rows[0]["style"]);
                            if (repairinvstyle.StartsWith("SPECIAL-"))
                                repairinvstyle = "";
                        }
                    }
                }

                // Update job to stock or reserve
                if (fin_rsrv == 1)
                    mfgModel.updJobNAddToRsv(jobbagno, settername, 0, "", Helper.StoreCode);
                else
                    mfgModel.updJobNAddToStk(jobbagno, settername, 0, "", Helper.StoreCode);

                // Check if HOUSE account and auto pickup
                //if (Helper.DataTableOK(dtChkInv) &&
                //    Convert.ToString(dtChkInv.Rows[0]["ACC"]).ToUpper() == "HOUSE")
                //{
                //    Helper.MarkRepairAsPickedUp(jobbagno);
                //    string desc = $"Repair Order #{jobbagno} Picked Up From Job Bag Given Out Option For HOUSE";
                //    Helper.AddKeepRec(desc);
                //}

                // Update style cost (if required)
                Helper.AddJobBagStyleCost(jobbagno, repairinvstyle);

                return new KeyValuePair<bool, string>(true, "History updated successfully.");
            }
            catch (Exception ex)
            {
                return new KeyValuePair<bool, string>(false, "Error: " + ex.Message);
            }
        }

        public ActionResult GetCustNotesBasedOnRepairNo(string repair_no)
        {
            DataTable dt = Helper.GetSqlData($"select acc from repair with (nolock) where repair_no = '{repair_no.TrimStart('0')}'");
            string acc = Helper.GetValue(dt, "Acc");
            //new frmNewNotes(Helper.GetValue(dt, "Acc"), false, this).Show();

            //CustomerModelNew objModel = new CustomerModelNew();
            //DataTable dt1 = objModel.ShowCustomerNotes(acc);
            //return JsonConvert.SerializeObject(dt1);

            SalesLayawaysController objModel1 = new SalesLayawaysController();
            List<string> followUpTypes = objModel1.getFollowUpTypes();
            ViewBag.ACC = acc;
            ViewBag.Type = followUpTypes;
            var data = objModel1.loadall(acc);
            ViewBag.Notes = data;
            return PartialView("_CustomerNotes", data);
        }


        public ActionResult GiveJobToAPerson()
        {
            string TableName = "MFG";
            string FieldName = "LOG_NO";
            string MaxLimit = "999999";
            string MinLimit = "";
            string PField = string.Empty;
            string PValue = string.Empty;

            ViewBag.logNo = Helper.GetNextSeqNo(TableName, FieldName, MaxLimit, PField, MinLimit, PValue);

            MfgModel mfgModel = new MfgModel();

            DataTable dataTable = mfgModel.getallsetters();
            List<SelectListItem> persons = new List<SelectListItem>();
            //persons.Add(new SelectListItem() { Text = "All", Value = "" });
            if (dataTable.Rows.Count > 0)
            {
                foreach (DataRow dr in dataTable.Rows)
                {
                    persons.Add(new SelectListItem() { Text = dr["NAME"].ToString().Trim(), Value = dr["NAME"].ToString().Trim() });
                }
            }
            mfgModel.setters = persons;
            return View(mfgModel);
        }

        [HttpGet]
        public JsonResult loadGiveJobBagDetails(string jobBagNo, bool isReceivedBack = false, string person = "")
        {

            MfgModel mfgModel = new MfgModel();

            try
            {
                string bagno = Helper.JobNormal(jobBagNo);

                if (!Helper.IsValidBag(bagno))
                    return Json(new { success = false, message = "Invalid Job Bag #." }, JsonRequestBehavior.AllowGet);

                DataTable dtt = mfgModel.checkJobBagIsAlreadySplittedOrNot(bagno);
                if (Helper.DataTableOK(dtt))
                    return Json(new { success = false, message = "This Job Bag has been split already." }, JsonRequestBehavior.AllowGet);

                if (Helper.GetShipedFinRsv(bagno.Length > 6 ? bagno.Remove(bagno.Length - 1) : bagno))
                    return Json(new { success = false, message = "This Job Bag qty already reserved/shipped." }, JsonRequestBehavior.AllowGet);

                if (Helper.GetClosedJobBag(bagno))
                    return Json(new { success = false, message = "This Job Bag is already closed." }, JsonRequestBehavior.AllowGet);

                if (Helper.GetSetter(bagno) == person)
                    return Json(new { success = false, message = "This Job Bag already with this person; cannot send again." }, JsonRequestBehavior.AllowGet);

                DataRow infoRow = mfgModel.GetInforationBasedOnJobBagFromlbl(bagno, isReceivedBack);
                if (infoRow == null)
                    infoRow = mfgModel.ChceckJobbagNumber(bagno, isReceivedBack);

                if (infoRow == null)
                    return Json(new { success = false, message = "JobBag already assigned to another person. Please get it back first." }, JsonRequestBehavior.AllowGet);

                var result = new
                {
                    success = true,
                    data = new
                    {
                        Style = Convert.ToString(infoRow["style"]),
                        Qty = Convert.ToString(infoRow["qty"]),
                        open = Convert.ToString(infoRow["open"]),
                        jobNo = Convert.ToString(infoRow["barcode"]),
                        pon = Convert.ToString(infoRow["pon"]),
                    }
                };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Error: " + ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult SaveGiveJobToPerson(SaveJobBagRequest request)
        {
            MfgModel mfgModel = new MfgModel();
            try
            {
                if (string.IsNullOrWhiteSpace(request.Person))
                    return Json(new { success = false, message = "Person cannot be blank." });

                if (string.IsNullOrWhiteSpace(request.JobBagNo))
                    return Json(new { success = false, message = "Log No. (Job Bag No.) cannot be blank." });

                int flag = 0;
                string loggedUser = Helper.LoggedUser;

                foreach (var item in request.JobBagItems)
                {
                    string jobNo = Helper.CheckForDBNull(item.JobNo, typeof(string).FullName);
                    string jobAssignPerson = Helper.GetSetter(jobNo);

                    if (jobAssignPerson == request.Person)
                    {
                        return Json(new { success = false, message = $"This Job Bag {jobNo} already with this person. Cannot send it again." });
                    }

                    if (!string.IsNullOrWhiteSpace(jobAssignPerson) && jobAssignPerson != request.Person && !request.IsReceivedBack)
                    {
                        return Json(new { success = false, message = $"This Job Bag {jobNo} is assigned to another person. Please mark it as received back first." });
                    }

                    if (item.Qty > 0)
                        flag = 1;
                }

                if (flag == 0)
                    return Json(new { success = false, message = "No items to save." });

                // Set due date
                DateTime dueDate = request.DueDate ?? DateTime.Now.AddDays(2);
                if (DateTime.Now > dueDate)
                    dueDate = DateTime.Now.AddDays(2);

                // Convert list to DataTable
                DataTable jobBagData = new DataTable();
                jobBagData.Columns.Add("JobNo");
                jobBagData.Columns.Add("Note");
                jobBagData.Columns.Add("Qty", typeof(decimal));
                jobBagData.Columns.Add("Pon");
                jobBagData.Columns.Add("Style");
                jobBagData.Columns.Add("Open", typeof(decimal));


                foreach (var item in request.JobBagItems)
                {
                    jobBagData.Rows.Add(item.JobNo, item.Notes, item.Qty, item.Pon, item.Style, item.OpenQty);
                }

                mfgModel.SaveJobBafInfoInMfgTable(request.JobBagNo, jobBagData, request.Person, loggedUser, "N", request.IsReceivedBack, dueDate);

                //if (request.DeletedJobBags != null)
                //{
                //    foreach (var del in request.DeletedJobBags)
                //        mfgService.DeleteInfoInMfgTable(del, request.JobBagNo);
                //}

                return Json(new
                {
                    success = true,
                    message = "Job bag details saved successfully.",
                    printUrl = Url.Action("PrintJobBag", "Print", new { jobBagNo = request.JobBagNo, setterName = request.Person })
                });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Error: " + ex.Message });
            }
        }


        public ActionResult GetJobBackFromPerson()
        {
            string TableName = "MFG";
            string FieldName = "LOG_NO";
            string MaxLimit = "999999";
            string MinLimit = "";
            string PField = string.Empty;
            string PValue = string.Empty;

            ViewBag.logNo = Helper.GetNextSeqNo(TableName, FieldName, MaxLimit, PField, MinLimit, PValue);

            MfgModel mfgModel = new MfgModel();

            DataTable dataTable = mfgModel.getallsetters();
            List<SelectListItem> persons = new List<SelectListItem>();
            //persons.Add(new SelectListItem() { Text = "All", Value = "" });
            if (dataTable.Rows.Count > 0)
            {
                foreach (DataRow dr in dataTable.Rows)
                {
                    persons.Add(new SelectListItem() { Text = dr["NAME"].ToString().Trim(), Value = dr["NAME"].ToString().Trim() });
                }
            }
            mfgModel.setters = persons;
            return View(mfgModel);
        }

        [HttpGet]
        public JsonResult ValidateGivenJobBag(string jobBagNo, string setterName)
        {
            try
            {
                string bagno = Helper.JobNormal(jobBagNo);

                DataRow dtt = mfgModel.getGivenJobBagaData(bagno, setterName);

                if (dtt == null)
                    return Json(new { success = false, message = "Invalid Job Bag #." }, JsonRequestBehavior.AllowGet);

                DataTable jobbaginfo = mfgModel.reprintjobbag(bagno, true);
                if (Helper.DataTableOK(jobbaginfo))
                {
                    DataTable result = Helper.GETHISTORYOFJOBBAGForGiveOut(bagno);
                    if (Helper.DataTableOK(result))
                    {
                        if (!Helper.iSOpenJobBag(bagno, setterName))
                        {
                            return Json(new
                            {
                                success = false,
                                message = "This JobBag is not with this setter."
                            }, JsonRequestBehavior.AllowGet);
                        }

                        DataRow[] closedRows = result.Select("Closed_job = 'true'");
                        if (closedRows.Length > 0)
                        {
                            return Json(new
                            {
                                success = false,
                                message = "This JobBag already added to stock or reserved on PO."
                            }, JsonRequestBehavior.AllowGet);
                        }
                    }
                }

                var resultFinal = new
                {
                    success = true,
                    data = new
                    {
                        JobNo = Convert.ToString(dtt["jobno"]),
                        Style = Convert.ToString(dtt["style"]),
                        Qty = Convert.ToString(dtt["qty"]),
                        OPEN = Convert.ToString(dtt["OPEN"]),
                        note = Convert.ToString(dtt["note"]),
                        vend_inv = Convert.ToString(dtt["vend_inv"]),
                        vinv_dte = Convert.ToString(dtt["vinv_dte"]),
                        pon = Convert.ToString(dtt["pon"]),
                    }
                };
                return Json(resultFinal, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Error: " + ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }


        public JsonResult SaveReturnJobBack(ReturnJobRequest request)
        {
            MfgModel mfgModel = new MfgModel();
            try
            {
                string loggedUser = Helper.LoggedUser ?? "webuser";
                string opt = "3";

                if (string.IsNullOrWhiteSpace(request.LogNo))
                    return Json(new { success = false, message = "Log No. cannot be blank." });

                if (string.IsNullOrWhiteSpace(request.Setter))
                    return Json(new { success = false, message = "Setter cannot be blank." });

                if (request.JobItems == null || request.JobItems.Count == 0)
                    return Json(new { success = false, message = "No Job Bag(s) found." });

                var data = BuildWorkingDataTable(request.JobItems);

                foreach (DataRow dr in data.Rows)
                {
                    string jobNo = Convert.ToString(dr["jobno"]);
                    if (!string.IsNullOrWhiteSpace(jobNo))
                        dr["Closed"] = Helper.GetClosedJobBag(jobNo);
                }

                if (!data.AsEnumerable().Any(r => Convert.ToDecimal(r["qty"]) > 0))
                    return Json(new { success = false, message = "No Job Bag(s) found with Valid Qty." });

                var firstJobNo = data.AsEnumerable()
                                     .Select(r => Convert.ToString(r["jobno"]))
                                     .FirstOrDefault(x => !string.IsNullOrWhiteSpace(x));
                if (!string.IsNullOrWhiteSpace(firstJobNo))
                {
                    string assignedSetter = Helper.GetSetter(firstJobNo);
                    if (!string.IsNullOrWhiteSpace(assignedSetter) && assignedSetter != request.Setter)
                    {
                        return Json(new
                        {
                            success = false,
                            message = $"No Job Bag(s) found with person {request.Setter}, selected person should be {assignedSetter}."
                        });
                    }
                }

                var pickup = ProcessPickupAndStock(data, request.LogNo, request.Setter);

                string xml = Helper.GetDataTableXML("mfgitems", data);
                mfgModel.savereturnjobdata(request.LogNo, request.Setter, loggedUser, xml, opt);

                var needStyle = data.AsEnumerable()
                                    .Where(r => Convert.ToBoolean(r["AddStock"]) || Convert.ToBoolean(r["RsvForInvoice"]))
                                    .Select(r => Convert.ToString(r["jobno"]))
                                    .Distinct()
                                    .ToList();

                return Json(new
                {
                    success = true,
                    message = "Saved successfully.",
                    printUrl = Url.Action("PrintJobBack", "Print", new { logNo = request.LogNo, setter = request.Setter }),
                    stockJobs = needStyle,
                    notifications = new
                    {
                        emailSent = pickup.EmailSent,
                        textSent = pickup.TextSent,
                        textFailed = pickup.TextFailed
                    }
                });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Error: " + ex.Message });
            }
        }

        private DataTable BuildWorkingDataTable(List<ReturnJobItem> items)
        {
            var dt = new DataTable();
            dt.Columns.Add("jobno", typeof(string));
            dt.Columns.Add("qty", typeof(decimal));
            dt.Columns.Add("AddStock", typeof(bool));
            dt.Columns.Add("RsvForInvoice", typeof(bool));
            dt.Columns.Add("Closed", typeof(bool));
            dt.Columns.Add("Email", typeof(bool));
            dt.Columns.Add("Text", typeof(bool));

            foreach (var it in items)
            {
                dt.Rows.Add(
                    it.JobNo?.Trim(),
                    it.Qty,
                    it.AddStock,
                    it.RsvForInvoice,
                    false,                 // Closed will be recomputed
                    it.Email,
                    it.Text
                );
            }
            return dt;
        }

        private class PickupResult
        {
            public int EmailSent { get; set; }
            public int TextSent { get; set; }
            public int TextFailed { get; set; }
        }

        private PickupResult ProcessPickupAndStock(DataTable data, string logNo, string uiSetter)
        {
            var result = new PickupResult();

            bool anyStockOrRsv = data.AsEnumerable()
                                     .Any(r => Convert.ToBoolean(r["AddStock"]) || Convert.ToBoolean(r["RsvForInvoice"]));

            if (anyStockOrRsv)
            {
                var openRows = data.AsEnumerable()
                                   .Where(r => !Convert.ToBoolean(r["Closed"]))
                                   .ToList();

                foreach (var row in openRows)
                {
                    string jobNo = Convert.ToString(row["jobno"]);
                    if (string.IsNullOrWhiteSpace(jobNo)) continue;

                    bool isAddToStock = Convert.ToBoolean(row["AddStock"]);
                    bool wantEmail = Convert.ToBoolean(row["Email"]);
                    bool wantText = Convert.ToBoolean(row["Text"]);

                    decimal dQty = 0, dRcv = 0;
                    var status = Helper.CheckJobBagStatus(jobNo);
                    if (status != null)
                    {
                        dQty = Helper.CheckForDBNull(status["QTY"], typeof(decimal));
                        dRcv = Helper.CheckForDBNull(status["RCV"], typeof(decimal));
                    }
                    if (dQty <= dRcv) continue; // nothing to do

                    string setterFromHistory = ResolveOpenSetter(jobNo) ?? uiSetter;

                    if (isAddToStock)
                    {
                        AddToStockOrReserve(setterFromHistory, 0, jobNo, logNo);
                        if (wantEmail)
                        {
                            //SendMailorText(uiSetter, jobNo, "Mail");
                        }
                        if (wantText)
                        {
                            //SendMailorText(uiSetter, jobNo, "Text");
                        }
                    }
                    else
                    {
                        AddToStockOrReserve(setterFromHistory, 1, jobNo, logNo);
                    }
                }
            }
            else
            {
                foreach (DataRow row in data.Rows)
                {
                    string jobNo = Convert.ToString(row["jobno"]);
                    decimal qty = Convert.ToDecimal(row["qty"]);
                    if (!string.IsNullOrWhiteSpace(jobNo) && qty > 0)
                    {
                        AddRecordToHistoryPersonBack(uiSetter, jobNo, qty, logNo);
                    }
                }
            }

            return result;
        }

        private string ResolveOpenSetter(string jobNo)
        {
            var hist = Helper.GETHISTORYOFJOBBAGForGiveOut(jobNo);
            if (Helper.DataTableOK(hist))
            {
                foreach (DataRow r in hist.Rows)
                {
                    var dateRcvd = Convert.ToString(r["dateRCVD"]);
                    if (string.IsNullOrWhiteSpace(dateRcvd))
                        return Convert.ToString(r["giventoperson"]);
                }
            }
            return null;
        }

        private void AddRecordToHistoryPersonBack(string setterName, string jobBagNo, decimal qty, string logNo)
        {
            string normalized = Helper.JobNormal(jobBagNo);
            var jobInfo = mfgModel.reprintjobbag(normalized);
            if (!Helper.DataTableOK(jobInfo)) return;

            decimal dQty = 0, dRcv = 0;
            var status = Helper.CheckJobBagStatus(normalized);
            if (status != null)
            {
                dQty = Helper.CheckForDBNull(status["QTY"], typeof(decimal));
                dRcv = Helper.CheckForDBNull(status["RCV"], typeof(decimal));
            }
            if (dQty <= dRcv) return;

            try
            {
                Helper.UpdateHistory(normalized, setterName, qty, 0, "", false, logNo);
            }
            catch (SqlException ex) when (ex.Message.Contains("JobBag # Already Given to Current Person."))
            {
                //
            }
        }
        private void AddToStockOrReserve(string setterName, int fin_rsrv, string jobBagNo, string logNo)
        {
            string normalized = Helper.JobNormal(jobBagNo);
            var jobInfo = mfgModel.reprintjobbag(normalized);
            if (!Helper.DataTableOK(jobInfo)) return;

            if (fin_rsrv == 1)
                mfgModel.updJobNAddToRsv(normalized, setterName, 0, logNo, Helper.StoreCode);
            else
                mfgModel.updJobNAddToStk(normalized, setterName, 0, logNo, Helper.StoreCode);
        }

        //private void SendMailorText(string setter, string jobbag, string optSend)
        //{
        //    try
        //    {
        //        // ✅ Get customer info linked to this job bag
        //        DataRow drCust = Helper.GetCustEmail(jobbag);
        //        if (drCust == null)
        //        {
        //            // Optionally log this or show message
        //            return;
        //        }

        //        string jbAcc = Convert.ToString(drCust["acc"]);
        //        string jbName = Convert.ToString(drCust["name"]);

        //        switch (optSend.ToUpper())
        //        {
        //            case "MAIL":
        //                {
        //                    string strMsg = $"Your repair order #{jobbag} is ready for pickup.";
        //                    string jbEmail = Convert.ToString(drCust["email"]);

        //                    if (!string.IsNullOrWhiteSpace(jbEmail))
        //                    {
        //                        try
        //                        {
        //                            using (var smtp = new SmtpClient())
        //                            {
        //                                var mail = new MailMessage
        //                                {
        //                                    To = { jbEmail },
        //                                    Subject = "Repair Order Ready for Pickup",
        //                                    Body = strMsg,
        //                                    IsBodyHtml = false
        //                                };
        //                                smtp.Send(mail);
        //                            }

        //                            dtSendStatus.Rows.Add(jobbag, "EMAIL", "SENT");
        //                        }
        //                        catch (Exception)
        //                        {
        //                            dtSendStatus.Rows.Add(jobbag, "EMAIL", "FAILED");
        //                        }
        //                    }
        //                    break;
        //                }

        //            case "TEXT":
        //                {
        //                    string upsMessage = "";

        //                    DataRow drStoreMessage = Helper.GetSqlRow(
        //                        $"SELECT TOP 1 REPAIRSMSTEXT FROM STORES WHERE CODE = '{Helper.StoreCode.Replace("'", "''")}'"
        //                    );

        //                    if (drStoreMessage != null)
        //                    {
        //                        string messageFromStores = Helper.CheckForDBNull(drStoreMessage["REPAIRSMSTEXT"]);
        //                        if (!string.IsNullOrWhiteSpace(messageFromStores))
        //                            upsMessage = messageFromStores;
        //                    }

        //                    if (string.IsNullOrWhiteSpace(upsMessage))
        //                    {
        //                        DataTable dtups_ins = Helper.GettimeSaver();
        //                        if (Helper.DataTableOK(dtups_ins))
        //                            upsMessage = Helper.CheckForDBNull(dtups_ins.Rows[0]["REPAIRSMSTEXT"]);
        //                    }

        //                    if (Regex.IsMatch(upsMessage, @"\{\s*(last|first|name)\s*\}", RegexOptions.IgnoreCase))
        //                    {
        //                        upsMessage = Regex.Replace(upsMessage, @"\{\s*name\s*\}", jbName, RegexOptions.IgnoreCase);
        //                        upsMessage = Regex.Replace(upsMessage, @"\{\s*first\s*\}", Regex.Match(jbName, @"^.*?(?=\s|$)").ToString(), RegexOptions.IgnoreCase);
        //                        upsMessage = Regex.Replace(upsMessage, @"\{\s*last\s*\}", Regex.Match(jbName, @"\w+$").ToString(), RegexOptions.IgnoreCase);
        //                    }

        //                    if (string.IsNullOrWhiteSpace(upsMessage))
        //                        upsMessage = $"Dear {jbName}, your repair order #{jobbag} is ready for pickup.";

        //                    bool smsSent = Helper.SendSMS("", upsMessage, jbAcc, true);

        //                    dtSendStatus.Rows.Add(jobbag, "TEXT", smsSent ? "SENT" : "FAILED");
        //                    break;
        //                }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        // Log or ignore based on your design
        //        Console.WriteLine($"Error sending message for job {jobbag}: {ex.Message}");
        //    }
        //}



        #endregion


        public ActionResult Persons()
        {
            return View();
        }
        public string GetAllSetters(bool isfrmperson = true)
        {
            MfgModel objmodel = new MfgModel();
            DataTable data = objmodel.getallsetters(isfrmperson);
            DataTable dtUsers = Helper.GetEmpCodes();

            if (data == null || data.Rows.Count == 0)
                return JsonConvert.SerializeObject(data);

            HashSet<string> validCodes = new HashSet<string>(
                dtUsers?.AsEnumerable().Select(r => r.Field<string>("code") ?? string.Empty) ?? Enumerable.Empty<string>(),
                StringComparer.OrdinalIgnoreCase
            );

            foreach (DataRow row in data.Rows)
            {
                string loginCode = row["LoginCode"]?.ToString() ?? string.Empty;
                if (!validCodes.Contains(loginCode))
                {
                    row["LoginCode"] = DBNull.Value;
                }
            }
            return JsonConvert.SerializeObject(data);
        }
        [HttpGet]
        public JsonResult GetGLCodes()
        {
            DataTable dtd = Helper.GetSqlData("SELECT ACC, Name FROM GL_ACCS ORDER BY acc");
            return Json(JsonConvert.SerializeObject(dtd), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetEmpCodes()
        {
            DataTable dtUsers = Helper.GetEmpCodes();
            return Json(JsonConvert.SerializeObject(dtUsers), JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult GetDeptCodes()
        {
            MfgModel objmodel = new MfgModel();
            DataTable dtUsers = objmodel.getmfgdepts();
            var deptList = dtUsers.AsEnumerable()
                .Select(row => new { Dept = row["dept"].ToString() })
                .ToList();

            return Json(deptList, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult CheckValidMfgDept(string dept)
        {
            MfgModel model = new MfgModel();
            DataRow result = model.CheckValidMfgDepts(dept);
            bool isValid = Helper.DataTableOK(result);
            return Json(isValid);
        }

        [HttpPost]
        public JsonResult GetGLNameByACC(string acc)
        {
            DataTable dt = Helper.GetNameByACC(acc);
            var name = Helper.DataTableOK(dt) ? dt.Rows[0]["Name"].ToString() : "";
            return Json(new { Name = name });
        }

        [HttpPost]
        public JsonResult SavePersons(List<PersonModel> persons)
        {
            try
            {
                MfgModel mfg = new MfgModel();
                DataTable dt = Helper.ToDataTable(persons);
                mfg.resetallsetters(Helper.GetDataTableXML("grid1XmlData", dt));
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, error = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult DeletePerson(string name)
        {
            try
            {
                MfgModel mfg = new MfgModel();
                mfg.deletersettername(name.Replace("'", "''"));
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, error = ex.Message });
            }
        }
        public ActionResult AddEditDept()
        {
            return View();
        }
        public string Getalldepts()
        {
            MfgModel objmodel = new MfgModel();
            DataTable data = objmodel.getalldepts();
            return JsonConvert.SerializeObject(data);
        }
        [HttpPost]
        public JsonResult CheckValidDepat(string dept)
        {
            try
            {
                MfgModel objmodel = new MfgModel();
                var exists = objmodel.CheckValidDept(dept);
                bool isValid = exists == null;
                return Json(isValid);
            }
            catch
            {
                return Json(false);
            }
        }

        [HttpPost]
        public JsonResult SaveDepartments(List<DepartmentViewModel> departments)
        {
            if (departments == null || departments.Count == 0)
                return Json(new { success = false, error = "No data to update" });

            try
            {
                MfgModel objmodel = new MfgModel();
                foreach (var d in departments)
                {
                    if (string.IsNullOrWhiteSpace(d.dept))
                        return Json(new { success = false, error = "Enter Dept name" });

                    if (string.IsNullOrWhiteSpace(d.hours))
                        return Json(new { success = false, error = "Enter Hours" });

                    if (d.hours.Length > 7)
                        return Json(new { success = false, error = "Invalid length for Hours" });

                    // Only check duplicates if Dept was changed
                    if (!string.Equals(d.dept, d.Prev, StringComparison.OrdinalIgnoreCase) &&
                        objmodel.CheckValidDept(d.dept) != null)
                    {
                        return Json(new { success = false, error = $"Department {d.dept} already exists" });
                    }
                }

                DataTable dt = new DataTable("mfg_dept");
                dt.Columns.Add("dept");
                dt.Columns.Add("hours");
                dt.Columns.Add("STATUS");
                dt.Columns.Add("Prev");

                foreach (var d in departments)
                {
                    var row = dt.NewRow();
                    row["dept"] = d.dept.ToUpper().Trim();
                    row["hours"] = d.hours.Trim();
                    row["STATUS"] = d.STATUS ?? "A";

                    if (d.STATUS != "A")
                    {
                        if (!dt.Columns.Contains("Prev"))
                            dt.Columns.Add("Prev");

                        row["Prev"] = string.IsNullOrWhiteSpace(d.Prev) ? d.dept.ToUpper().Trim() : d.Prev.ToUpper().Trim();
                    }
                    dt.Rows.Add(row);
                }

                objmodel.UpdateDept(Helper.GetDataTableXML("mfg_dept", dt));

                return Json(new { success = true, message = "Department(s) updated successfully" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, error = ex.Message });
            }
        }

        #region Sales Shop Control/OpenAClosedJobBag
        public ActionResult OpenAClosedJobBag()
        {
            return View();
        }
        public JsonResult getopenCloseJobNo(string jobbagno)
        {
            object msg = "";

            string jobbagno1 = Helper.JobNormal(jobbagno);
            if (!Helper.GetClosedJobBag(jobbagno1))
            {
                msg = new { code = false, message = "This job bag does not exist or it is not closed." };
            }
            else
            {
                msg = new { code = true, message = "success" };
            }

            return Json(msg);
        }


        public JsonResult openCloseJobBagNo(string jobbagno1)
        {
            OpenClosedJob(jobbagno1);
            Helper.AddKeepRec($"Jobbag#:- {jobbagno1} Opened");
            return Json(new { code = true, message = "Jobbag opened successfully." });
        }

        public static void OpenClosedJob(string Inv_no)
        {
            Helper.GetSqlData($@"Delete from  mfg  where [dbo].[GetBarcode](Trimmed_inv_no) = [dbo].[GetBarcode](trim(@jobbag)) and closed_job=1", "@jobbag", Inv_no);
            Helper.GetSqlData($@"update or_items set rcvd=0, shiped=0, fin_rsv=0 where [dbo].[GetBarcode](Trimmed_barcode)=[dbo].[GetBarcode](trim(@jobbag))", "@jobbag", Inv_no);
            Helper.GetSqlData($@"update rep_item set shiped= iif(exists (select * from rep_inv where [dbo].[GetBarcode](trim(rep_no))=[dbo].[GetBarcode](trim(@jobbag))),shiped,0) where [dbo].[GetBarcode](trim(repair_no))=[dbo].[GetBarcode](trim(@jobbag))", "@jobbag", Inv_no);
        }
        #endregion

        #region Sales Shop Control/RepairRcvd
        public ActionResult RepairRcvd()
        {
            return View("~/Views/Repairs/RepairRcvd.cshtml");
        }

        public string GetListOfJobsForAck(string cShop)
        {
            DataTable data = MfgModel.GetJobsForAck(cShop);
            return JsonConvert.SerializeObject(data);
        }

        public JsonResult SaveRepRcvInShop(string tableData, string toShop)
        {
            object msg = "";

            DataTable repTable = new DataTable();


            DataTable dtGridTable = JsonConvert.DeserializeObject<DataTable>(tableData);
            string str1XmlData = string.Empty;

            if (Helper.DataTableOK(dtGridTable))
                str1XmlData = Helper.GetDataTableXML("str1XmlData", dtGridTable);


            try
            {
                repTable = (RepRcvInShop(str1XmlData, toShop, Helper.LoggedUser));
            }
            catch (Exception ex)
            {
                msg = new { code = false, message = ex.Message };
            }

            if (repTable != null && repTable.Rows.Count > 0)
            {
                msg = new { code = true, message = "Received Repairs Successfully", datatable = JsonConvert.SerializeObject(repTable) };

            }
            return Json(msg);
        }

        public DataTable RepRcvInShop(string data1, string toShop, string username, bool lSentBack = false)
        {
            DataTable dataTable = new DataTable();

            using (var connection = new SqlConnection(Helper.connString))
            using (var dbCommand = new SqlCommand("RCVREPAIRINSHOP", connection))
            using (var dbAdapter = new SqlDataAdapter(dbCommand))
            {
                // Configure the command
                dbCommand.CommandType = CommandType.StoredProcedure;

                // Add parameters with explicit types
                dbCommand.Parameters.Add("@str1XmlData", SqlDbType.NVarChar).Value = data1;
                dbCommand.Parameters.Add("@cToStore", SqlDbType.NVarChar).Value = toShop;
                dbCommand.Parameters.Add("@username", SqlDbType.NVarChar).Value = username;
                dbCommand.Parameters.Add("@lBack", SqlDbType.Bit).Value = lSentBack;

                // Open the connection and fill the DataTable
                connection.Open();
                dbAdapter.Fill(dataTable);
            }
            return dataTable;
        }
        #endregion


        public JsonResult SaveCompletedReadyForPickup(string jobbagno, string settername = "", int fin_rsrv = 0, string communicationType = "")
        {
            if (jobbagno != "")
            {
                if (IsCompletedJob(jobbagno))
                {
                    return Json(new { success = false, message = "JobBag was already completed." });
                }

                if (Helper.GetClosedJobBag(jobbagno))
                    return Json(new { success = false, message = "This Job Bag is already closed." }, JsonRequestBehavior.AllowGet);

                this.SetReadyForPickup(jobbagno, communicationType);
                AddToHistory(jobbagno, settername, 1);
                Helper.UpdateiSRepairAddedToStock(jobbagno);
                Helper.AddKeepRec("Job Bag# " + jobbagno + " was marked as ready to pickup", null, false, "", "", "", jobbagno);

                return Json(new { success = false, message = "job Updated Successfully." });
            }
            else
                return Json(new { success = false, message = "JobBag # is required." });
        }

        private void SetReadyForPickup(string jobBGNO, string communicationType)
        {
            Helper.GetReadyForPickup(jobBGNO);

            string strMsg = "Your repair order # " + jobBGNO + " is ready for pickup.";
            DataRow drCust = Helper.GetCustEmail(jobBGNO);
            if (communicationType == "Email")
            {
                //new frmEmailReport(drCust["email"].ToString(), strMsg, drCust["acc"].ToString(), drCust["name"].ToString(), strMsg, false, false, true)
                //{
                //    MdiParent = this.MdiParent,
                //}.ShowDialog();
            }
            else if (communicationType == "Text" && Helper.OkToText())
            {
                string upsMessage = "";
                DataRow drStoreMessage = Helper.GetSqlRow("SELECT TOP 1 REPAIRSMSTEXT FROM STORES with (nolock) WHERE CODE = '" + Helper.StoreCode.Replace("'", "''") + "'");
                if (drStoreMessage != null)
                {
                    string messageFromStores = Helper.CheckForDBNull(drStoreMessage["REPAIRSMSTEXT"]);
                    if (messageFromStores != "")
                        upsMessage = messageFromStores;
                }

                if (upsMessage.Trim() == "")
                {
                    DataTable dtups_ins = Helper.GettimeSaver();
                    if (Helper.DataTableOK(dtups_ins))
                        upsMessage = Helper.CheckForDBNull(dtups_ins.Rows[0]["REPAIRSMSTEXT"]);
                }

                if (Regex.IsMatch(upsMessage, @"\{\s*(last|first|name)\s*\}", RegexOptions.IgnoreCase)) //when upmessage has {name} or {first} or {last} 
                {
                    string custname = Helper.CheckForDBNull(drCust["name"]).Trim();
                    upsMessage = Regex.Replace(upsMessage, @"\{\s*name\s*\}", custname, RegexOptions.IgnoreCase);
                    upsMessage = Regex.Replace(upsMessage, @"\{\s*first\s*\}", Regex.Match(custname, @"^.*?(?=\s|$)").ToString(), RegexOptions.IgnoreCase); //it takes the first word from custname upt
                    upsMessage = Regex.Replace(upsMessage, @"\{\s*last\s*\}", Regex.Match(custname, @"\w+$").ToString(), RegexOptions.IgnoreCase);
                }
                //Helper.SendSMS("", upsMessage, Helper.CheckForDBNull(drCust["acc"]));
            }
        }

        private bool IsCompletedJob(string jobbagno)
        {
            DataRow drJobBagStatus = Helper.CheckJobBagStatus(jobbagno);
            if (drJobBagStatus != null)
            {
                Decimal dQty = Helper.CheckForDBNull(drJobBagStatus["QTY"].ToString(), typeof(decimal));
                Decimal dRcv = Helper.CheckForDBNull(drJobBagStatus["RCV"].ToString(), typeof(decimal));

                bool isjobCompleted = (dQty <= dRcv);

                return isjobCompleted;

            }
            return false;
        }

        #region Shop Control/SentBackFromShop

        public ActionResult SentBackFromShop()
        {
            mfgModel.setters = Helper.getstoresdataforsetdefault(true, true);
            ViewBag.LoggedUser = Session["STORE_CODE"].ToString();
            return View("~/Views/Repairs/SentBackFromShop.cshtml", mfgModel);
        }

        public JsonResult CheckValidJobbag(string cBarcode)
        {
            object msg = "";
            DataRow stylerow = Helper.CheckJobbag(cBarcode);
            if (stylerow == null)
            {
                msg = new { code = false, message = "Invalid Jobbag#" };
            }
            return Json(msg);
        }

        public JsonResult GetCheckRprJob(string cJobbag, string frmShop, string toStore)
        {
            OrderRepairModel objmodel = new OrderRepairModel();
            object msg = "";

            string Error = string.Empty;
            string cJob = (cJobbag ?? string.Empty).Trim().PadLeft(6, '0');

            try
            {
                Error = (objmodel.CheckRprJob(cJob, toStore, frmShop));
                if (string.IsNullOrWhiteSpace(Error))
                {
                    msg = new { code = true, message = "Success" };
                }
            }
            catch (Exception ex)
            {
                msg = new { code = false, message = ex.Message };
            }
            if (!string.IsNullOrWhiteSpace(Error))
            {
                msg = new { code = false, message = Error };
            }
            return Json(msg);
        }

        public JsonResult SaveTransferJobBacktoShop(string tableData, string frmStore, string toStore)
        {
            object msg = "";

            DataTable dtGridTable = JsonConvert.DeserializeObject<DataTable>(tableData);

            string str1XmlData = string.Empty;
            if (Helper.DataTableOK(dtGridTable))
                str1XmlData = Helper.GetDataTableXML("str1XmlData", dtGridTable);
            DataTable repTable = new DataTable();


            DataRow dtqty = Helper.GetJobbagQty(dtGridTable.Rows[0][0].ToString());

            DateTime txtDate = DateTime.Now;
            DateTime? tDate;
            if (string.IsNullOrEmpty(Convert.ToString(txtDate).Trim()))
                tDate = Helper.DefStart;
            else
                tDate = Helper.setSQLDateTime(txtDate);


            decimal qty = Convert.ToDecimal(dtqty["qty"].ToString());

            try
            {
                repTable = (Send2Shop(str1XmlData, toStore, frmStore, qty, tDate.ToString(), Helper.LoggedUser, true));
                if (Helper.DataTableOK(repTable))
                {
                    msg = new { code = true, message = "Success", DataTable = JsonConvert.SerializeObject(repTable) };
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

        public DataTable Send2Shop(string data1, string frmStore, string toShop, decimal qty, string cDat, string username, bool lSentBack = false)
        {
            var dataTable = new DataTable();

            // Use 'using' statements to ensure the connection and command are disposed correctly
            using (var connection = new SqlConnection(Helper.connString))
            using (var command = new SqlCommand("REPAIRTOSHOP", connection))
            {
                // Configure command
                command.CommandType = CommandType.StoredProcedure;

                // Add parameters with explicit data types for better performance
                command.Parameters.Add("@str1XmlData", SqlDbType.NVarChar).Value = data1;
                command.Parameters.Add("@cFrmStore", SqlDbType.NVarChar).Value = frmStore;
                command.Parameters.Add("@cToStore", SqlDbType.NVarChar).Value = toShop;
                command.Parameters.Add("@qty", SqlDbType.Decimal).Value = qty;
                command.Parameters.Add("@cDate", SqlDbType.NVarChar).Value = cDat;
                command.Parameters.Add("@username", SqlDbType.NVarChar).Value = username;
                command.Parameters.Add("@lBack", SqlDbType.Bit).Value = lSentBack;

                // Open connection just before executing the query
                connection.Open();

                // Use a DataAdapter to fill the DataTable
                using (var dataAdapter = new SqlDataAdapter(command))
                {
                    dataAdapter.Fill(dataTable);
                }
            }

            return dataTable;
        }

        #endregion



        public ActionResult ReprintJobbag()
        {
            MfgModel mfgModel = new MfgModel();
            ViewBag.iSNoPromisDate = Helper.CheckModuleEnabled(Helper.Modules.NoPromiseDate);
            ViewBag.JobBagNo = "";
            return View();
        }

        public ActionResult PrintGiveJobToAPerson(string jobbagno, string settername = "")
        {
            MfgModel mfgModel = new MfgModel();
            ViewBag.jbnumber = jobbagno;
            ViewBag.setername = settername;
            DataTable dtt = Helper.checkJobBagIsAlreadySplittedOrNotForPrint(jobbagno);

            string[] reqdate;
            string rptdate = "";
            foreach (DataRow dr in dtt.Rows)
            {
                if (dr["date"].ToString() != "" && dr["date"].ToString() != null)
                {
                    reqdate = dr["date"].ToString().Split(' ');
                    rptdate = reqdate[0];
                }
            }
            ViewBag.givenOnNo = rptdate;
            ViewBag.dtt = dtt;
            return View("~/Views/Reports/rptGiveJobToAPerson.cshtml");
        }

        public ActionResult PrintGetJobBackFromPerson(string jobbagno, string settername = "")
        {
            MfgModel mfgModel = new MfgModel();
            ViewBag.jbnumber = jobbagno;
            ViewBag.setername = settername;
            DataTable dtt = mfgModel.checkrcvobbag(jobbagno);

            ViewBag.dtt = dtt;
            return View("~/Views/Reports/rptRecJobbag.cshtml");
        }

        #region SalesShopControl/ReadActualHours
        [SessionCheck("UserId")]
        public ActionResult ReadActualHours(bool closejb = false)
        {
            Helper.closejb = closejb;
            DataTable dataTable = mfgModel.getallsetters();
            List<SelectListItem> salesmanList = new List<SelectListItem>();
            if (dataTable.Rows.Count > 0)
            {
                foreach (DataRow dr in dataTable.Rows)
                {
                    salesmanList.Add(new SelectListItem() { Text = dr["NAME"].ToString().Trim(), Value = dr["NAME"].ToString().Trim() });
                }
            }
            mfgModel.setters = salesmanList;
            return View("~/Views/Estimates/ReadActualHours.cshtml", mfgModel);
        }

        [SessionCheck("UserId")]
        public JsonResult ValidateJobBag(string jobBagNo)
        {
            //reprintjobbag

            object msg = "";

            string jbbagno = jobBagNo, cJobBag = "";
            if (!string.IsNullOrWhiteSpace(jbbagno))
            {
                int input;
                if (int.TryParse(jbbagno, out input))
                    cJobBag = string.Format("{0}", jbbagno.PadLeft(6, '0'));
                else
                    cJobBag = string.Format("{0}", jbbagno.PadLeft(7, '0'));

                jbbagno = cJobBag;
                DataTable dtJobinfo = Helper.reprintjobbag(cJobBag, true);
                if (!Helper.DataTableOK(dtJobinfo))
                {
                    /*Helper.MsgBox(Helper.GetLang("Invalid Jobbag"), Telerik.WinControls.RadMessageIcon.Info);
                    txtJobbag.Text = string.Empty;
                    this.ActiveControl = txtJobbag;*/
                    msg = new { code = false, message = "Invalid Jobbag" };

                }
                else
                {
                    msg = new { code = true, message = jbbagno };
                }

            }

            return Json(msg);
        }

        [SessionCheck("UserId")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult CheckOpenJobExist(string Person, string cJob, bool CompleteJob)
        {
            object msg = "";
            string cPerson = Person;
            string cJobBag = cJob;
            bool lCompleteJob = CompleteJob;

            if (string.IsNullOrWhiteSpace(cPerson))
            {
                return Json(new { code = false, isOpenCheckBag = false, message = "Missing Person" });
            }
            if (string.IsNullOrWhiteSpace(cJobBag))
            {
                return Json(new { code = false, isOpenCheckBag = false, message = "Missing Jobbag#" });
            }
            if (Helper.GetLoginCodeBySetter(cPerson) == "")
            {

                return Json(new { code = false, isOpenCheckBag = false, message = "Employee Code Not Found For This Setter, Can Not Proceed " });
            }
            if (lCompleteJob && !Helper.CheckJobOpen(cPerson, cJobBag))
            {

                return Json(new { code = false, isOpenCheckBag = false, message = "Not Found Open Job For Setter : " + cPerson + " And JobBag# " + cJobBag });
            }
            if (!lCompleteJob && Helper.CheckJobOpen(cPerson, cJobBag))
            {

                return Json(new { code = false, isOpenCheckBag = false, message = "Open Job Already Exist For Setter : " + cPerson + " And JobBag# " + cJobBag });
            }

            if (!lCompleteJob && Helper.CheckOpenJobByPerson(cPerson))
            {

                msg = new { code = false, isOpenCheckBag = true, message = "Another Jobbag Was Opened For This Setter. Do You Want To Proceed?" };
                // lCloseExistingAndOpenNew = true;
            }
            else
            {
                msg = new { code = true, message = "Success" };
            }
            return Json(msg);
        }

        [SessionCheck("UserId")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult updateReadActualHours(string Person, string cJob, bool JobComplete, bool CloseExistingAndOpenNew)
        {
            object msg = "";
            bool lCompleteJob = JobComplete;
            string cPerson = Person;
            string cJobBag = cJob;
            bool lCloseExistingAndOpenNew = CloseExistingAndOpenNew;
            try
            {
                string error = string.Empty;
                if (Helper.UpdateActualHours(cPerson, cJobBag, lCompleteJob, lCloseExistingAndOpenNew, out error))
                {
                    msg = new { code = true, message = "Data Updated Successfully" };
                }
                else
                    //Helper.MsgBox(error);
                    msg = new { code = false, message = error };
            }
            catch (Exception ex)
            {
                Helper.MsgBox(ex.ToString());
                msg = new { code = false, message = ex.ToString() };
            }
            return Json(msg);
        }


        #endregion

        #region SalesShopControl/CalculateQueuedJobs

        [SessionCheck("UserId")]
        public ActionResult CalculateQueuedJobs()
        {
            return View("~/Views/Estimates/CalculateQueuedJobs.cshtml");
        }

        [SessionCheck("UserId")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public string GetListCalcDaysForQueuedJobs(decimal nHoursPerDay)
        {

            DataTable dataprint = Helper.CalcDaysForQueuedJobs(nHoursPerDay);


            return JsonConvert.SerializeObject(dataprint);
        }
        //PrintViewer,CalculateQueuedJobsGenerateReport,CalculateQueuedJobs
        public ActionResult CalculateQueuedJobsGenerateReport(string type)
        {
            DataTable dtReport = Helper.PrintDataTable;

            var listParam = new List<ReportParameter>();
            listParam.Add(new ReportParameter("rpReportDate", DateTime.Now.ToString(Helper.GetSeverDateFormat(true))));

            return GenerateReport(type,
               Server.MapPath("~/Reports/rptCalculateQueuedJobs.rdlc"),
               new List<ReportDataSourceItem>
               {
             new ReportDataSourceItem { DataSetName = "DataSet1", Data = dtReport }
                },
                "Calculate Days To Clear Jobs",
                false,
                listParam
           );
        }

        public ActionResult PrintViewer()
        {
            ViewBag.PdfUrl = Url.Action("CalculateQueuedJobsGenerateReport", new { type = "preview" });
            return View("~/Views/Shared/RdlcPrintViewer.cshtml");
        }

        #endregion
        #region SalesShopControl/AdjustActualHours

        [SessionCheck("UserId")]
        public ActionResult AdjustActualHours()
        {
            return View("~/Views/Estimates/AdjustActualHours.cshtml");
        }

        [SessionCheck("UserId")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public string GetListOfActualDuration(string cJob)
        {

            int input;
            string cJobBag;

            if (int.TryParse(cJob, out input))
                cJobBag = string.Format("{0}", cJob.PadLeft(6, '0'));
            else
                cJobBag = string.Format("{0}", cJob.PadLeft(7, '0'));

            cJob = cJobBag;
            DataTable data = Helper.GetActualDuration(cJob);
            return JsonConvert.SerializeObject(data);
        }


        [SessionCheck("UserId")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult SaveAdjustActualDuration(string cJobbag, string tableString)
        {
            object msg = "";
            DataTable dtActualDuration = JsonConvert.DeserializeObject<DataTable>(tableString);

            if (cJobbag != "" && dtActualDuration.Rows.Count > 0)
            {
                string error;
                if (Helper.AdjustActualDuration(cJobbag, Helper.GetDataTableXML("grid1XmlData", dtActualDuration), out error))
                {
                    //Helper.MsgBox("Data Updated Successfully");
                    msg = new { code = true, message = "Data Updated Successfully" };
                }
                else
                    //Helper.MsgBox(error);
                    msg = new { code = false, message = error };
            }
            else
                //Helper.MsgBox("JobBag # Required.", Telerik.WinControls.RadMessageIcon.Info);
                msg = new { code = false, message = "JobBag # Required." };

            return Json(msg);
        }

        #endregion

        #region SalesShopControl / Estimates

        [SessionCheck("UserId")]
        public ActionResult Estimates()
        {
            DataTable dataTable = mfgModel.getallsetters();

            List<SelectListItem> salesmanList = new List<SelectListItem>();
            List<SelectListItem> frequentlyUsed = new List<SelectListItem>();
            int i = 1;
            if (dataTable.Rows.Count > 0)
            {
                foreach (DataRow dr in dataTable.Rows)
                {

                    bool freqUsed = dr["freq_used"] != DBNull.Value && Convert.ToBoolean(dr["freq_used"]);

                    if (freqUsed)
                    {
                        frequentlyUsed.Add(new SelectListItem
                        {
                            Text = dr["NAME"].ToString().Trim(),
                            Value = dr["NAME"].ToString().Trim()
                        });

                        i++;
                        if (i > 6) break;
                    }
                    salesmanList.Add(new SelectListItem
                    {
                        Text = dr["NAME"].ToString().Trim(),
                        Value = dr["NAME"].ToString().Trim()
                    });
                }
            }

            mfgModel.setters = salesmanList;
            ViewBag.frequentlyUsed = frequentlyUsed;

            DataTable dtTemplates = Helper.GetEstimateTemplates();
            List<SelectListItem> EstimateTemplates = new List<SelectListItem>();
            if (Helper.DataTableOK(dtTemplates))
            {
                foreach (DataRow dr in dtTemplates.Rows)
                    EstimateTemplates.Add(new SelectListItem() { Text = dr["Template_Name"].ToString().Trim(), Value = dr["Template_Name"].ToString().Trim() });
            }


            ViewBag.EstimateTemplates = EstimateTemplates;

            return View("~/Views/Estimates/Estimates.cshtml", mfgModel);
        }

        [SessionCheck("UserId")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult GetListOfEstimates(string cJob)
        {
            object msg = "";
            string cJobBag;
            string ImagePath;
            int input;
            DataTable dtGrid1 = new DataTable();

            DataTable allSettersForGrid = Helper.GetAllSettersForGrid();
            var allSettersForGridData = allSettersForGrid.AsEnumerable()
               .Select(row => allSettersForGrid.Columns
                   .Cast<DataColumn>()
                   .ToDictionary(col => col.ColumnName, col => row[col]))
               .ToList();
            ViewBag.allSettersForGridData = allSettersForGridData;
            if (int.TryParse(cJob, out input))
                cJobBag = string.Format("{0}", cJob.PadLeft(6, '0'));
            else
                cJobBag = string.Format("{0}", cJob.PadLeft(7, '0'));

            string jobbagno1 = cJobBag;
            DataTable jobbaginfo = mfgModel.reprintjobbag(jobbagno1, true);
            DataTable jobbagissplitted = Helper.checkJobBagIsSplitOrNot(jobbagno1);
            string cStyle = "";
            // imageAccordion1.Visible = false;
            if (jobbaginfo.Rows.Count > 0)
            {
                dtGrid1 = Helper.GetEstimatesByJob(jobbagno1);
                /* radGroupBox1.Visible = saveButton1.Visible = btnSaveTemplate.Visible = cmbCopyFrom.Visible = btnTemplate.Visible = lblTemplate.Visible = true;
                 setGrid();*/

                int count = jobbaginfo.Rows.Count - 1;
                cStyle = jobbaginfo.Rows[count].Field<string>(0);

                /*imageAccordion1.Visible = true;
                imageAccordion1.Style = cStyle;*/
                //ImagePath = cStyle;

                ImagePath = cStyle;

                DataTable dt = Helper.checkImageInStyimages("REP" + cJobBag.Trim());
                DataTable dt1 = Helper.GetRepairItemwithjobbag(cJobBag);
                if (Helper.DataTableOK(dt))
                    //this.imageAccordion1.Style = "REP" + cJobBag.Trim();
                    ImagePath = "REP" + cJobBag.Trim();
                else
                    //this.imageAccordion1.Style = dt1.Rows.Count > 0 ? "REP" + dt1.Rows[0]["repair_no"].ToString() : "";
                    ImagePath = dt1.Rows.Count > 0 ? "REP" + dt1.Rows[0]["repair_no"].ToString() : "";

                msg = new { code = true, message = "Success", estimateTable = JsonConvert.SerializeObject(dtGrid1), allSettersForGrid = allSettersForGridData, image = ImagePath };
            }
            else
            {
                dtGrid1 = Helper.GetEstimatesByJob("");
                /*radGridView1.DataSource = dtGrid1;
                cJobBag  = "";
                jobbagno.Focus();
                Helper.MsgBox(jobbagissplitted.Rows.Count > 0 ? "JobBag # Already Split." : "Invalid JobBag #", Telerik.WinControls.RadMessageIcon.Info);*/
                msg = new { code = false, message = jobbagissplitted.Rows.Count > 0 ? "JobBag # Already Split." : "Invalid JobBag #" };
            }

            return Json(msg);
        }

        [SessionCheck("UserId")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult GetListOfEstimateTemplates(string cTemplate)
        {
            object msg = "";
            DataTable dtGrid1 = new DataTable();
            if (string.IsNullOrWhiteSpace(cTemplate))
            {
                /*Helper.MsgBox("Please Select Template", Telerik.WinControls.RadMessageIcon.Info);*/
                return Json(new { code = false, message = "Please Select Template" });
            }

            DataTable dtEstTmplt = Helper.GetEstimateTemplates(cTemplate);
            if (Helper.DataTableOK(dtEstTmplt))
            {
                if (dtEstTmplt.Columns.Contains("Template_Name"))
                    dtEstTmplt.Columns.Remove("Template_Name");
                dtGrid1 = dtEstTmplt;

                msg = new { code = true, message = "Success", TemplateDataTable = JsonConvert.SerializeObject(dtGrid1) };
            }
            else
                //Helper.MsgBox("No Records Found", Telerik.WinControls.RadMessageIcon.Info);
                msg = new { code = false, message = "No Records Found" };

            return Json(msg);
        }

        [SessionCheck("UserId")]
        public ActionResult viewEstimateTemp()
        {
            DataTable dtTemplates = Helper.GetEstimateTemplates();
            List<SelectListItem> EstimateTemplates = new List<SelectListItem>();
            if (Helper.DataTableOK(dtTemplates))
            {
                foreach (DataRow dr in dtTemplates.Rows)
                    EstimateTemplates.Add(new SelectListItem() { Text = dr["Template_Name"].ToString().Trim(), Value = dr["Template_Name"].ToString().Trim() });
            }


            ViewBag.EstimateTemplates2 = EstimateTemplates;
            return PartialView("~/Views/Estimates/_GetEstimateTemplate.cshtml");
        }

        [SessionCheck("UserId")]
        [HttpPost]
        public JsonResult SaveEstimateTemplates(string cTemplate, bool isNameAlreadyExist = false)
        {
            object msg = "";

            if (string.IsNullOrWhiteSpace(cTemplate))
            {

                return Json(new { code = false, message = "Missing Template Name." });
            }

            DataTable data = Helper.GetEstimateTemplates(cTemplate);

            //!Helper.IsSure("")
            if (Helper.DataTableOK(data) && !isNameAlreadyExist)
                return Json(new { code = true, isNameExist = true, message = "Template Name Already Exist. Do You Want To OverWrite " });

            msg = new { code = true, isNameExist = false, message = cTemplate };

            return Json(msg);
            //cEstTemplate = txtTemplate.Text;
        }

        [SessionCheck("UserId")]
        [HttpPost]
        public JsonResult UpdateEstimateTemplateData(string cTemplate, string grid1XmlData)
        {
            object msg = "";
            string error;

            DataTable dtGrid1 = JsonConvert.DeserializeObject<DataTable>(grid1XmlData);

            if (Helper.UpdateEstimateTemplate(cTemplate, Helper.GetDataTableXML("grid1XmlData", dtGrid1), out error))
            {
                DataTable dtTemplates = Helper.GetEstimateTemplates();
                List<SelectListItem> EstimateTemplates = new List<SelectListItem>();
                if (Helper.DataTableOK(dtTemplates))
                {
                    foreach (DataRow dr in dtTemplates.Rows)
                        EstimateTemplates.Add(new SelectListItem() { Text = dr["Template_Name"].ToString().Trim(), Value = dr["Template_Name"].ToString().Trim() });
                }


                msg = new { code = true, message = "Template Saved Successfully", templates = EstimateTemplates };
            }

            return Json(msg);
        }

        [SessionCheck("UserId")]
        [HttpPost]
        public JsonResult saveUpdateEstimates(string cJob, string grid1XmlData)
        {
            object msg = "";
            string error;

            DataTable dtGrid1 = JsonConvert.DeserializeObject<DataTable>(grid1XmlData);

            if (cJob != "")
            {
                bool lSuccess = false;
                lSuccess = Helper.UpdateEstimates(cJob, Helper.GetDataTableXML("grid1XmlData", dtGrid1), out error);
                if (lSuccess)
                {
                    //Helper.MsgBox("Data Updated Successfully");
                    msg = new { code = true, message = "Data Updated Successfully" };
                }
                else
                    //Helper.MsgBox(error);
                    msg = new { code = false, message = error };
            }
            else
                //Helper.MsgBox("JobBag # Required.", Telerik.WinControls.RadMessageIcon.Info);
                msg = new { code = false, message = "JobBag # Required." };

            return Json(msg);

        }

        [SessionCheck("UserId")]
        [HttpPost]
        public JsonResult DeleteEstimateTemplate(string cTemplate, bool isSureCheck = false)
        {
            object msg = "";
            string strTemplateName = string.Empty;
            try
            {
                strTemplateName = cTemplate;


                DataTable data = Helper.GetEstimateTemplates(strTemplateName);

                if (Helper.DataTableOK(data))
                {
                    if (!isSureCheck)
                    {
                        return Json(new { code = true, isSureCheck = true, message = $"Are You Sure You Want To Delete The Template: \"{strTemplateName}\" ?" });
                    }
                    else
                    {
                        if (Helper.DeleteEstTemplate(strTemplateName))
                        {

                            msg = new { code = true, message = "Template Deleted Successfully." };
                        }
                        else
                        {

                            msg = new { code = false, message = "Error in Delete Template." };
                        }
                    }


                }
                else
                {

                    msg = new { code = false, message = "Invalid Template Name" };
                }
            }
            catch (Exception ex)
            {
                msg = new { code = false, message = ex.Message };
            }

            return Json(msg);
        }


        #endregion
    }

    public class SaveJobBagRequest
    {
        public string JobBagNo { get; set; }
        public string Person { get; set; }
        public bool IsReceivedBack { get; set; }
        public DateTime? DueDate { get; set; }
        public List<JobBagItem> JobBagItems { get; set; }
        public List<string> DeletedJobBags { get; set; }
    }

    public class JobBagItem
    {
        public string JobNo { get; set; }
        public decimal Qty { get; set; }
        public string Style { get; set; }
        public string Pon { get; set; }
        public decimal OpenQty { get; set; }
        public string Notes { get; set; }
    }

    public class JobBagHistory
    {
        public string GiveToPerson { get; set; }
        public string TakeBackFrom { get; set; }
        public string Transact { get; set; }
        public string DateGiven { get; set; }
        public string TimeGiven { get; set; }
        public string DateRcvd { get; set; }
        public string TimeRcvd { get; set; }
        public decimal QtyGiven { get; set; }
        public decimal QtyRcvd { get; set; }
        public decimal WtGiven { get; set; }
        public decimal WtRcvd { get; set; }
        public string DueDate { get; set; }
        public string Note1 { get; set; }
        public string Note2 { get; set; }
    }
    public class ReturnJobRequest
    {
        public string LogNo { get; set; }
        public string Setter { get; set; }                // person
        public DateTime? DueDate { get; set; }            // optional, if you need it
        public List<ReturnJobItem> JobItems { get; set; }
    }

    public class ReturnJobItem
    {
        public string JobNo { get; set; }
        public decimal Qty { get; set; }
        public bool AddStock { get; set; }
        public bool RsvForInvoice { get; set; }
        public bool Closed { get; set; }                  // client may ignore; server recomputes anyway
        public bool Email { get; set; }                   // optional flags coming from UI
        public bool Text { get; set; }                    // optional flags coming from UI
    }
}