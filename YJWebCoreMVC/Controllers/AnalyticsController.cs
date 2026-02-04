using AspNetCore.Reporting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;
using System.Data;
using YJWebCoreMVC.Filters;
using YJWebCoreMVC.Models;
using YJWebCoreMVC.Services;

namespace YJWebCoreMVC.Controllers
{
    [SessionCheck("UserId")]
    public class AnalyticsController : Controller
    {
        private readonly HelperCommonService _helperCommonService;
        private readonly AnalyticsService _analyticsService;
        private readonly ListOfItemsSoldService _listOfItemsSoldService;
        private readonly IWebHostEnvironment _env;
        private readonly ConnectionProvider _connectionProvider;

        public AnalyticsController(HelperCommonService helperCommonService, AnalyticsService analyticsService, ListOfItemsSoldService listOfItemsSoldService, IWebHostEnvironment env, ConnectionProvider connectionProvider)
        {
            _helperCommonService = helperCommonService;
            _analyticsService = analyticsService;
            _listOfItemsSoldService = listOfItemsSoldService;
            _env = env;
            _connectionProvider = connectionProvider;
        }


        public IActionResult Index()
        {
            return View();
        }

        #region Sold or In Stock Methods
        
        public IActionResult StockOrHand()
        {
            AnalyticsModel objModel = new AnalyticsModel();

            objModel.CustomerCodes = _helperCommonService.GetAllCustomerCodes();
            objModel.CategoryTypes = _helperCommonService.GetAllCategories();
            objModel.SubCategoryTypes = _helperCommonService.GetAllSubCategories();
            objModel.BrandTypes = _helperCommonService.GetAllBrands();
            objModel.MetalTypes = _helperCommonService.GetAllMetals();
            objModel.VenderTypes = _helperCommonService.GetAllVendors();

            List<AnalyticsModel> lstsoldOrInStock = new List<AnalyticsModel>();
            objModel.getAllSoldorInStockList = lstsoldOrInStock;
            ViewBag.Title = "Quantity Sold / In Stock From Each Style";

            return View(objModel);
        }

        
        public string GetSoldOrInstockIndex(string ccode, string fdate, string tdate, string category, string subcat, string metalval, string brandval, string fromstyle, string tostyle, string strVendor)
        {
            var data = _analyticsService.getQtyOnHandDetails(ccode, fdate, tdate, category, subcat, metalval, brandval, fromstyle, tostyle, strVendor);
            return JsonConvert.SerializeObject(data);
        }

        
        public string GetStockImages(string styleno)
        {
            var lstImages = _analyticsService.getStockImages(styleno);
            return JsonConvert.SerializeObject(lstImages);
        }

        
        public string BindCustomerCodes(string CustomerCode)
        {
            var customercodes = _helperCommonService.GetAllCustomerCodes();
            return JsonConvert.SerializeObject(customercodes);
        }

        
        #endregion

        
        public IActionResult BestSellerCategoryReports()
        {
            AnalyticsModel objModel = new AnalyticsModel();

            objModel.CustomerCodes = _helperCommonService.GetAllCustomerCodes();
            objModel.CategoryTypes = _helperCommonService.GetAllCategories();
            objModel.SubCategoryTypes = _helperCommonService.GetAllSubCategories();
            objModel.BrandTypes = _helperCommonService.GetAllBrands();
            objModel.MetalTypes = _helperCommonService.GetAllMetals();
            objModel.VenderTypes = _helperCommonService.GetAllVendors();

            List<AnalyticsModel> lstbestSellerCategoryReport = new List<AnalyticsModel>();
            objModel.getBestSellerCategoryReport = lstbestSellerCategoryReport;
            ViewBag.Title = "Best Seller Category Reports";

            return View(objModel);
        }

        
        public string GetBestSellerCategoryReport(string ccode, string fdate, string tdate, string metalval, string brandval, string fromstyle, string tostyle, string strVendor)
        {
            var data = _analyticsService.getBestSellerCategoryReportDetails(ccode, fdate, tdate, metalval, brandval, fromstyle, tostyle, strVendor);
            return JsonConvert.SerializeObject(data);
        }

        
        public IActionResult BestSellerReports()
        {
            AnalyticsModel objModel = new AnalyticsModel();

            objModel.CustomerCodes = _helperCommonService.GetAllCustomerCodes();
            objModel.CategoryTypes = _helperCommonService.GetAllCategories();
            objModel.SubCategoryTypes = _helperCommonService.GetAllSubCategories();
            objModel.BrandTypes = _helperCommonService.GetAllBrands();
            objModel.MetalTypes = _helperCommonService.GetAllMetals();
            objModel.VenderTypes = _helperCommonService.GetAllVendors();
            objModel.AllGroups = _helperCommonService.GetAllGroups();

            List<AnalyticsModel> lstBestSellerReport = new List<AnalyticsModel>();
            objModel.getBestSellerReport = lstBestSellerReport;
            ViewBag.Title = "Best Seller Report";

            return View(objModel);
        }

        
        public string GetBestSellerReport(string ccode, string fdate, string tdate, string category, string subcat, string metalval, string brandval, string fromstyle, string tostyle, string strVendor, string groupval)
        {
            var data = _analyticsService.getBestSellerReportDetails(ccode, fdate, tdate, category, subcat, metalval, brandval, fromstyle, tostyle, strVendor, groupval);
            return JsonConvert.SerializeObject(data);
        }

        
        public IActionResult WorstSellerReports()
        {
            AnalyticsModel objModel = new AnalyticsModel();

            objModel.CustomerCodes = _helperCommonService.GetAllCustomerCodes();
            objModel.CategoryTypes = _helperCommonService.GetAllCategories();
            objModel.SubCategoryTypes = _helperCommonService.GetAllSubCategories();
            objModel.BrandTypes = _helperCommonService.GetAllBrands();
            objModel.MetalTypes = _helperCommonService.GetAllMetals();
            objModel.VenderTypes = _helperCommonService.GetAllVendors();
            objModel.AllGroups = _helperCommonService.GetAllGroups();

            List<AnalyticsModel> lstWorstSellerReport = new List<AnalyticsModel>();
            objModel.getWorstSellerReport = lstWorstSellerReport;
            ViewBag.Title = "Worst Seller Report";

            return View(objModel);
        }

        
        public string GetWorstSellerReport(string ccode, string fdate, string tdate, string category, string subcat, string metalval, string brandval, string fromstyle, string tostyle, string strVendor, string groupval)
        {
            var data = _analyticsService.getWorstSellerReportDetails(ccode, fdate, tdate, category, subcat, metalval, brandval, fromstyle, tostyle, strVendor, groupval);
            return JsonConvert.SerializeObject(data);
        }

        
        public IActionResult AnnualPaymentsReceived()
        {
            AnalyticsModel objModel = new AnalyticsModel();

            List<AnalyticsModel> lstAnnualPaymentsReceived = new List<AnalyticsModel>();
            objModel.getAnnualPaymentsReceived = lstAnnualPaymentsReceived;
            ViewBag.Title = "Annual Payments Received";

            return View(objModel);
        }

        
        public string GetAnnualPaymentsReceived(string strFrom1, string strTo1, string strFrom2, string strTo2, string strFrom3, string strTo3)
        {
            var data = _analyticsService.getAnnualPaymentsReceivedDetails(strFrom1, strTo1, strFrom2, strTo2, strFrom3, strTo3);
            return JsonConvert.SerializeObject(data);
        }

        
        public IActionResult AnnualSalesComparison()
        {
            AnalyticsModel objModel = new AnalyticsModel();

            List<AnalyticsModel> lstAnnualSalesComparison = new List<AnalyticsModel>();
            objModel.getAnnualSalesComparison = lstAnnualSalesComparison;
            ViewBag.Title = "Annual Sales Comparison";

            return View(objModel);
        }

        
        public string GetAnnualSalesComparison(string strFrom1, string strTo1, string strFrom2, string strTo2, string strFrom3, string strTo3, string strchkLayawaysUnpaid, string striSByPickupDate, string strexclude)
        {
            var data = _analyticsService.getAnnualSalesComparisonDetails(strFrom1, strTo1, strFrom2, strTo2, strFrom3, strTo3, strchkLayawaysUnpaid, striSByPickupDate, strexclude);
            return JsonConvert.SerializeObject(data);
        }

        
        public IActionResult EndofMonth()
        {
            AnalyticsModel objModel = new AnalyticsModel();

            objModel.AllStores = _helperCommonService.GetAllStores();

            List<AnalyticsModel> lstEndofMonth = new List<AnalyticsModel>();
            objModel.getEndofMonth = lstEndofMonth;
            ViewBag.Title = "End of Month Report";

            return View(objModel);
        }

        
        public string GetEndofMonth(string strFrom1, string strTo1, string striSByPickupDate, string strstore)
        {
            var data = _analyticsService.getEndofMonthDetails(strFrom1, strTo1, striSByPickupDate, strstore);
            return JsonConvert.SerializeObject(data);
        }

        
        public IActionResult TotalMonthlySales()
        {
            AnalyticsModel objModel = new AnalyticsModel();

            List<AnalyticsModel> lstTotalMonthlySales = new List<AnalyticsModel>();
            objModel.getTotalMonthlySales = lstTotalMonthlySales;
            ViewBag.Title = "Total Monthly Sales Report";

            return View(objModel);
        }

        
        public string GetTotalMonthlySales(string strFrom1, string strTo1, string strchkLayawaysUnpaid, string striSByPickupDate)
        {
            var data = _analyticsService.getTotalMonthlySalesDetails(strFrom1, strTo1, strchkLayawaysUnpaid, striSByPickupDate);
            return JsonConvert.SerializeObject(data);
        }

        
        public IActionResult TotalMonthlySalesForACustomer()
        {
            AnalyticsModel objModel = new AnalyticsModel();

            List<AnalyticsModel> lstTotalMonthlySalesForACustomer = new List<AnalyticsModel>();
            objModel.getTotalMonthlySalesForACustomer = lstTotalMonthlySalesForACustomer;
            ViewBag.Title = "Total Monthly Sales for a Customer";
            objModel.CustomerCodes = _helperCommonService.GetAllCustomerCodes();

            return View(objModel);
        }

        
        public string GetTotalMonthlySalesForACustomer(string ccode, string strFrom1, string strTo1, string strchkLayawaysUnpaid, string striSByPickupDate)
        {
            var data = _analyticsService.getTotalMonthlySalesForACustomerDetails(ccode, strFrom1, strTo1, strchkLayawaysUnpaid, striSByPickupDate);
            return JsonConvert.SerializeObject(data);
        }


        #region Analysis/SalesBySalesman
        
        public IActionResult SalesBySalesman()
        {
            AnalyticsModel objModel = new AnalyticsModel();
            objModel.CustomerCodes = _helperCommonService.GetAllSalesmansCodesList();
            objModel.AllStores = _helperCommonService.GetAllStores();
            objModel.CategoryTypes = _helperCommonService.GetAllCategories();
            objModel.BrandTypes = _helperCommonService.GetAllBrands();
            objModel.BrandsFromStyle = _helperCommonService.GetAllBrandsFromStyle();

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


            ViewBag.Title = "Sales By Salesrep";
            return View(objModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult GetListOfSalesBySalesmanDetails(string storeName = "", string salesMan = "", bool isLayaway = false, string byWhichDate = "", string fdate = "", string tdate = "",
                                                      string brand = "", string category = "", string Selbrand = "", bool IsComision = false, bool IsCost = false)
        {
            object msg = "";

            //DataTable dtInvoiceItems = new DataTable();

            DataTable dtInvoiceItems = _listOfItemsSoldService.GetSalesBySalesmanDetails(storeName, salesMan, isLayaway, byWhichDate,
                    fdate, tdate, brand, category, Selbrand, IsComision, IsCost, _helperCommonService.CheckModuleEnabled(HelperCommonService.Modules.CommissionByDiscount),
                    _helperCommonService.CheckModuleEnabled(HelperCommonService.Modules.CommissionByProfit), _helperCommonService.is_DiamondDealer);
            //_helperCommonService.resetOriginalCulture();
            ListOfItemsSoldController ListOfItemsSold = new ListOfItemsSoldController();
            DataTable store = ListOfItemsSold.GetSalesmanDetails(dtInvoiceItems);

            msg = new { code = true, dtInvoiceItems = JsonConvert.SerializeObject(dtInvoiceItems), storeDataTable = JsonConvert.SerializeObject(store) };

            return Json(msg);
        }


        #endregion

        #region Analysis/InactiveStyles



        public IActionResult InactiveStyles()
        {
            AnalyticsModel objModel = new AnalyticsModel();

            objModel.AllStores = _helperCommonService.GetAllStores();
            objModel.CategoryTypes = _helperCommonService.GetAllCategories();
            objModel.SubCategoryTypes = _helperCommonService.GetAllSubCategories();
            objModel.BrandTypes = _helperCommonService.GetAllBrands();
            objModel.MetalTypes = _helperCommonService.GetAllMetals();
            objModel.VenderTypes = _helperCommonService.GetAllVendorsCodes();

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

            //ViewBag.Title = "Inactive Styles";

            return View("~/Views/Analysis/InactiveStyles.cshtml", objModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public string GetListOfInactiveStyles(string ccode, int noOfDay, string ct, string subct, string metalval, string brandval, string fromstyle, string tostyle, string strVendor, string frmdate, string todate, string storeno = "")
        {
            DataTable data = _analyticsService.getInactiveStyles(ccode, noOfDay, ct, subct, metalval, brandval, fromstyle, tostyle, strVendor, frmdate, todate, storeno);
            if (!data.Columns.Contains("Image"))
                data.Columns.Add("Image", typeof(string));
            SetTableImages(data);

            HttpContext.Session.SetString("InactiveStylesTable", JsonConvert.SerializeObject(data));
            return JsonConvert.SerializeObject(data);
        }
        /*
                public IActionResult GenerateReport(string type, string astSoldDay, string daterange, string cat, string subcat, string brand, string metal, string vendor, string frmsty, string tostyl, bool isImageCheck = false)
                {

                    AnalyticsModel objModel = new AnalyticsModel();
                    //DataTable dtrapnetdata = Session["InactiveStylesTable"] as DataTable;
                    string json = HttpContext.Session.GetString("InactiveStylesTable");
                    DataTable dtrapnetdata = JsonConvert.DeserializeObject<DataTable>(json);

                    if (isImageCheck)
                    {
                        if (!dtrapnetdata.Columns.Contains("Image"))
                            dtrapnetdata.Columns.Add("Image", typeof(string));
                        SetTableImages(dtrapnetdata);
                    }
                    dtrapnetdata.AcceptChanges();
                    string resulttoreport = daterange + "\n" + cat + " " + subcat + " " + brand + " " + metal + "\n" + vendor + " " + frmsty + " " + tostyl;
                    DataView dvInactiveStylesr = new DataView(dtrapnetdata);


                    string storeCode = HttpContext.Session.GetString("STORE_CODE");
                    byte[] storeLogo = _helperCommonService.GetStoreImage(storeCode != "" ? storeCode : "");


                    var listParam = new List<ReportParameter>();
                    listParam.Add(new ReportParameter("rpLogo", Convert.ToBase64String(storeLogo)));
                    listParam.Add(new ReportParameter("rpMimeType", "image/png"));
                    listParam.Add(new ReportParameter("rpNoOdDays", astSoldDay));
                    listParam.Add(new ReportParameter("rpDtRange", resulttoreport));
                    listParam.Add(new ReportParameter("styleimageshoworhide", isImageCheck ? "1" : "0"));
                    listParam.Add(new ReportParameter("rpReportDate", DateTime.Now.ToString(_helperCommonService.GetSeverDateFormat(true))));


                    return GenerateReport(type,
                        Server.MapPath("~/Reports/rptInactiveStyles.rdlc"),
                        new List<ReportDataSourceItem>
                        {
                            new ReportDataSourceItem { DataSetName = "DataSet1", Data = dtrapnetdata }

                        },
                        "Inactive Styles",
                        true,
                        listParam
                    );
                }*/

        public IActionResult GenerateReport(string type,string astSoldDay,string daterange,string cat,string subcat,string brand,string metal,string vendor,string frmsty,string tostyl,bool isImageCheck = false)
        {
            string json = HttpContext.Session.GetString("InactiveStylesTable");

            if (string.IsNullOrEmpty(json))
                return BadRequest("Session data not found.");

            DataTable dtrapnetdata =
                JsonConvert.DeserializeObject<DataTable>(json);

            if (isImageCheck)
            {
                if (!dtrapnetdata.Columns.Contains("Image"))
                    dtrapnetdata.Columns.Add("Image", typeof(string));

                SetTableImages(dtrapnetdata);
            }

            dtrapnetdata.AcceptChanges();

            string resulttoreport =
                daterange + "\n" +
                cat + " " + subcat + " " + brand + " " + metal + "\n" +
                vendor + " " + frmsty + " " + tostyl;

            string storeCode = HttpContext.Session.GetString("STORE_CODE");
            byte[] storeLogo =
                _helperCommonService.GetStoreImage(storeCode ?? "");

            var parameters = new Dictionary<string, string>
            {
                { "rpLogo", Convert.ToBase64String(storeLogo) },
                { "rpMimeType", "image/png" },
                { "rpNoOdDays", astSoldDay },
                { "rpDtRange", resulttoreport },
                { "styleimageshoworhide", isImageCheck ? "1" : "0" },
                {
                    "rpReportDate",
                    DateTime.Now.ToString(
                        _helperCommonService.GetSeverDateFormat(true))
                }
            };

            string reportPath = Path.Combine(
                _env.WebRootPath,
                "Reports",
                "rptInactiveStyles.rdlc");

            LocalReport report = new LocalReport(reportPath);
            report.AddDataSource("DataSet1", dtrapnetdata);

            var result = report.Execute(
                RenderType.Pdf,
                1,
                parameters);
            string mimeType = "application/pdf";
            return File(
                result.MainStream,
                mimeType,
                "Inactive Styles.pdf");
        }

        public IActionResult PrintViewer(string type, string astSoldDay, string daterange, string cat, string subcat, string brand, string metal, string vendor12, string frmsty, string tostyl, bool isImageCheck = false)
        {

            ViewBag.PdfUrl = Url.Action(
                "GenerateReport",
                "Analysis",
                new
                {
                    type = type,
                    astSoldDay = astSoldDay,
                    daterange = daterange,
                    cat = cat,
                    subcat = subcat,
                    brand = brand,
                    metal = metal,
                    vendor12 = vendor12,
                    frmsty = frmsty,
                    tostyl = tostyl,
                    isImageCheck = isImageCheck
                }
            );
            return View("~/Views/Shared/RdlcPrintViewer.cshtml");
        }

        private void SetTableImages(DataTable dtSetImages)
        {
            ImagesController images = new ImagesController();

            string allstyles = string.Empty;
            foreach (DataRow row in dtSetImages.Rows)
                allstyles += string.Format("{0},", images.GetStyle(row["Style"].ToString()));

            allstyles = string.Join(",", allstyles.Split(',').Distinct().ToArray());

            DataTable dtInvoiceImages = GetInvoiceImages(allstyles);

            if (!dtSetImages.Columns.Contains("InvStyle"))
                dtSetImages.Columns.Add("InvStyle", typeof(string));

            foreach (DataRow drData in dtSetImages.Rows)
                drData["InvStyle"] = images.GetStyle(_helperCommonService.InvStyle(drData["Style"].ToString()));

            dtSetImages.AcceptChanges();
            foreach (DataRow drImage in dtInvoiceImages.Rows)
                dtSetImages.Select(string.Format("InvStyle = '{0}'", _helperCommonService.InvStyle(drImage["Style"].ToString()))).ToList<DataRow>().ForEach(r => r["Image"] = drImage["StyleImage"]);
        }

        public DataTable GetInvoiceImages(string p_styles)
        {
            DataTable dataTable = new DataTable();

            using (SqlDataAdapter dataAdapter = new SqlDataAdapter())
            {
                dataAdapter.SelectCommand = new SqlCommand();
                dataAdapter.SelectCommand.Connection = _connectionProvider.GetConnection();
                dataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                dataAdapter.SelectCommand.CommandTimeout = 5000;
                dataAdapter.SelectCommand.CommandText = "GetStyleInvoiceImages";
                dataAdapter.SelectCommand.Parameters.AddWithValue("@styles", p_styles);

                dataAdapter.Fill(dataTable);

                if (!dataTable.Columns.Contains("StyleImage"))
                    dataTable.Columns.Add("StyleImage", typeof(string));

                string imagename;
                byte[] objContext = null;
                foreach (DataRow row in dataTable.Rows)
                {
                    if (string.IsNullOrWhiteSpace(row["FileData"].ToString()))
                        continue;
                    objContext = (byte[])row["FileData"];
                    if (objContext != null)
                    {
                        using (MemoryStream ms = new MemoryStream(objContext))
                        {
                            using (Image image = Image.Load(ms))
                            {
                                imagename = Path.Combine(
                                    Path.GetTempPath(),
                                    $"{_helperCommonService.RemoveSpecialCharacters(
                                        _helperCommonService.CheckForDBNull(row["Style"]))}" +
                                    $"{DateTime.Now:yyyyMMddHHmmssfff}.jpg"
                                );

                                row["StyleImage"] = "File:" + imagename;
                                row["FileData"] = null;

                                if (System.IO.File.Exists(imagename))
                                    System.IO.File.Delete(imagename);

                                image.Save(imagename, new JpegEncoder());
                            }
                        }

                    }
                }

                if (dataTable.Columns.Contains("FileData"))
                    dataTable.Columns.Remove("FileData");
                dataTable.AcceptChanges();
            }
            return dataTable;
        }

        #endregion

        #region Analysis/SalesChartComparision

        public IActionResult SalesChartComparision()
        {
            SalesmenModel model = new SalesmenModel();
            DataTable dtsalesman = model.GetSalesmen();

            ViewBag.Salesmen = dtsalesman.AsEnumerable()
            .Select(r => new SelectListItem
            {
                Text = r["Code"].ToString(),
                Value = r["Code"].ToString()
            })
            .ToList();

            return View();
            //return View("~/Views/Analysis/SalesChartComparision.cshtml");
        }

        [HttpPost]
        public IActionResult RefreshSalesChart(SalesChartRequest model)
        {
            // STEP A: Validate salesman uniqueness
            var salesmen = new[] {
        model.Salesman1, model.Salesman2,
        model.Salesman3, model.Salesman4
    }
            .Where(s => !string.IsNullOrWhiteSpace(s))
            .ToList();

            if (!salesmen.Any())
                return Json(_helperCommonService.MsgBox("Please select at least one salesman.", "Info"));


            if (salesmen.Count != salesmen.Distinct().Count())
                return Json(_helperCommonService.MsgBox("Kindly select different salesman.", "Info"));


            // STEP B: Validate dates
            DateTime fromDate = model.FromDate ?? _helperCommonService.DefStart;
            DateTime toDate = model.ToDate ?? _helperCommonService.DefEnd;

            if (_helperCommonService.BadYear(fromDate) || _helperCommonService.BadYear(toDate))
                return Json(_helperCommonService.MsgBox("Invalid date range.", "Error"));

            DataTable TotalSales = this.getTotalMonthlySalesDetails("", fromDate.ToString(), toDate.ToString(), "");

            if (!_helperCommonService.DataTableOK(TotalSales))
                return Json(_helperCommonService.MsgBox("No Records Found.", "Info"));

            DataTable s1 = string.IsNullOrWhiteSpace(model.Salesman1) ? null : getTotalMonthlySalesDetails("", fromDate.ToString(), toDate.ToString(), model.Salesman1);
            DataTable s2 = string.IsNullOrWhiteSpace(model.Salesman2) ? null : getTotalMonthlySalesDetails("", fromDate.ToString(), toDate.ToString(), model.Salesman2);
            DataTable s3 = string.IsNullOrWhiteSpace(model.Salesman3) ? null : getTotalMonthlySalesDetails("", fromDate.ToString(), toDate.ToString(), model.Salesman3);
            DataTable s4 = string.IsNullOrWhiteSpace(model.Salesman4) ? null : getTotalMonthlySalesDetails("", fromDate.ToString(), toDate.ToString(), model.Salesman4);

            return Json(new
            {
                success = true,
                total = ConvertChart(TotalSales),
                s1 = ConvertChart(s1),
                s2 = ConvertChart(s2),
                s3 = ConvertChart(s3),
                s4 = ConvertChart(s4),
                labels = new
                {
                    s1 = model.Salesman1,
                    s2 = model.Salesman2,
                    s3 = model.Salesman3,
                    s4 = model.Salesman4
                }
            });
        }

        private List<object> ConvertChart(DataTable dt)
        {
            if (dt == null) return new List<object>();

            return dt.AsEnumerable()
                .Select(r => new
                {
                    date = Convert.ToDateTime(r["date"]),
                    net = Convert.ToDecimal(r["net"]),
                    payments = Convert.ToDecimal(r["payments"])
                })
                .ToList<object>();
        }

        public DataTable getTotalMonthlySalesDetails(string ccode, string fdate, string tdate, string scode)
        {
            return _helperCommonService.GetStoreProc("Month_Sales_SALESMAN", "@date1", getdate(fdate), "@date2", getdate(tdate), "@cacc", ccode, "@scode", scode);
        }
        private string getdate(string date1)
        {
            return date1?.Split(' ')[0] ?? string.Empty;
        }

        #endregion Analysis/SalesChartComparision

        #region Analysis/ComponentSalesAnalysis 


        public IActionResult ComponentSalesAnalysis()
        {
            return View("~/Views/Analysis/ComponentSalesAnalysis.cshtml");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult GetListOfComponentSalesAnalysis(string date1, string date2)
        {
            AnalyticsModel objModel = new AnalyticsModel();
            DataTable dtData = objModel.GetComponentSalesAnalysis(date1, date2, _helperCommonService.is_DiamondDealer);
            _helperCommonService.PrintDataTable = dtData;
            return Json(new { code = true, message = JsonConvert.SerializeObject(dtData) });
        }

        public IActionResult GenerateComponentSalesAnalysisReport(string type, string fromDate, string toDate, string daterange)
        {

            AnalyticsModel objModel = new AnalyticsModel();
            DataTable dtrapnetdata = objModel.GetComponentSalesAnalysis(fromDate, toDate, _helperCommonService.is_DiamondDealer);
            dtrapnetdata.AcceptChanges();
            DataView dvInactiveStylesr = new DataView(dtrapnetdata);


            string storeCode = HttpContext.Session.GetString("STORE_CODE");

            var listParam = new List<ReportParameter>();

            listParam.Add(new ReportParameter("rpDateRange", daterange));
            listParam.Add(new ReportParameter("rpReportDate", DateTime.Now.ToString(_helperCommonService.GetSeverDateFormat(true))));


            return GenerateReport(type,
                Server.MapPath("~/Reports/rptComponentSalesAnalysis.rdlc"),
                new List<ReportDataSourceItem>
                {
               new ReportDataSourceItem { DataSetName = "DataSet1", Data = dtrapnetdata }

                        },
                        "Component Sales Analysis Report",
                        false,
                        listParam
             );
        }

        public IActionResult ComponentSalesAnalysisPrintViewer(string type, string fromDate, string toDate, string daterange)
        {
            ViewBag.Mode = type;
            ViewBag.PdfUrl = Url.Action(
                "GenerateComponentSalesAnalysisReport",
                "Analysis",
                new
                {
                    type = type,
                    fromDate = fromDate,
                    toDate = toDate,
                    daterange = daterange,

                }
            );
            return View("~/Views/Shared/RdlcPrintViewer.cshtml");
        }


        #endregion
    }
}
