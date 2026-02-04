using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using System.Data;
using YJWebCoreMVC.Models;
using YJWebCoreMVC.Services;

namespace YJWebCoreMVC.Controllers
{
    public class ListOfItemsSoldController : Controller
    {

        private readonly HelperCommonService _helperCommonService;
        private readonly ListOfItemsSoldService _listOfItemsSoldService;
        private readonly RepairService _repairService;
        private readonly IWebHostEnvironment _env;
        private readonly ConnectionProvider _connectionProvider;

        public ListOfItemsSoldController(HelperCommonService helperCommonService, ListOfItemsSoldService listOfItemsSoldService, IWebHostEnvironment env,RepairService repairService, ConnectionProvider connectionProvider)
        {
            _helperCommonService = helperCommonService;
            _listOfItemsSoldService = listOfItemsSoldService;
            _env = env;
            _repairService = repairService;
            _connectionProvider = connectionProvider;
        }
        public ListOfItemsSoldController()
        {

        }

        ImagesController objImgController = new ImagesController();
        public IActionResult Index()
        {
            ViewBag.SalesmanData = _helperCommonService.GetAllSalesmansCodesList();
            ViewBag.StoreData = _helperCommonService.GetAllStoreCodesList();
            ViewBag.RegisterData = _helperCommonService.GetAllRegisterCodesList();
            return View("ListOfItemsSold");
        }

        public string GetListOfSoldItems(string FROMDATE, string DateTo, string Store, string Sales, bool IsLaywayUnpaid, bool saletax, bool IsInvoicedReport, bool DateValueCheck)
        {
            ListOfItemsSoldModel objModel = new ListOfItemsSoldModel();
            var data = _listOfItemsSoldService.GetListOfSoldItemsDetails(FROMDATE, DateTo, Store, Sales, IsLaywayUnpaid, saletax, IsInvoicedReport, DateValueCheck);
            return JsonConvert.SerializeObject(data);
        }

        public string GetListOfItemsSoldDetails(string FROMDATE, string DateTo, string Store, string Sales, bool IsLaywayUnpaid, bool saletax, bool IsInvoicedReport, bool DateValueCheck)
        {
            ListOfItemsSoldModel objModel = new ListOfItemsSoldModel();
            var data = _listOfItemsSoldService.GetListOfSoldItemsDetails(FROMDATE, DateTo, Store, Sales, IsLaywayUnpaid, saletax, IsInvoicedReport, DateValueCheck);
            return JsonConvert.SerializeObject(data);
        }

        public IActionResult ListOfOpenOrders()
        {
            ViewBag.SalesmanData = _helperCommonService.GetAllSalesmansCodesList();
            ViewBag.StoreData = _helperCommonService.GetAllStoreCodesList();
            ViewBag.RegisterData = _helperCommonService.GetAllRegisterCodesList();
            return View("ListOfOpenOrders");
        }

        public string GetListOfOpenOrderDetails(string FROMDATE, string DateTo, string Store, string Sales, bool repair, bool specialorder, bool layaway, bool IsLaywayUnpaid, string prom_Fromdate, string prom_Todate, bool IsOpenQuotes)
        {
            ListOfItemsSoldModel objModel = new ListOfItemsSoldModel();
            var data = _listOfItemsSoldService.GetListOfOpenOrderDetailsItemsDetails(FROMDATE, DateTo, Store, Sales, repair, specialorder, layaway, IsLaywayUnpaid, prom_Fromdate, prom_Todate, IsOpenQuotes);
            return JsonConvert.SerializeObject(data);
        }

        public IActionResult ListOfReceipts()
        {
            ViewBag.SalesmanData = _helperCommonService.GetAllSalesmansCodesList();
            ViewBag.StoreData = _helperCommonService.GetAllStoreCodesList();
            ViewBag.RegisterData = _helperCommonService.GetAllRegisterCodesList();
            return View("ListOfReceipts");
        }

        public string GetListOfReceiptDetails(string FROMDATE, string DateTo, string Store, string Sales, bool IsLaywayUnpaid, string register, bool ByPickupDate)
        {
            ListOfItemsSoldModel objModel = new ListOfItemsSoldModel();
            var data = _listOfItemsSoldService.GetReceiptListDetails(FROMDATE, DateTo, Store, Sales, IsLaywayUnpaid, register, ByPickupDate);
            return JsonConvert.SerializeObject(data);
        }

        public IActionResult StoreTotalSales()
        {
            ViewBag.StoreData = _helperCommonService.GetAllStoreCodesList();
            return View("StoreTotalSales");
        }

        public string GetListOfSoterTotalSalesDetails(string FROMDATE, string DateTo, string Store, bool IsLaywayUnpaid, bool ByPickupDate)
        {
            ListOfItemsSoldModel objModel = new ListOfItemsSoldModel();
            var data = _listOfItemsSoldService.GetStoreTotalSalesListDetails(FROMDATE, DateTo, Store, IsLaywayUnpaid, ByPickupDate);
            return JsonConvert.SerializeObject(data);
        }

        public IActionResult SalesBySalesman()
        {
            ViewBag.SalesmanData = _helperCommonService.GetAllSalesmansCodesList();
            ViewBag.StoreData = _helperCommonService.GetAllStoreCodesList();
            ViewBag.StyleBrandData = _helperCommonService.GetAllBrands();
            ViewBag.BrandData = _helperCommonService.GetAllBrandsFromStyle();
            ViewBag.CategoryData = _helperCommonService.GetAllCategories();
            return View("SalesBySalesman");
        }

        public string GetListOfSalesBySalesman(string Store = "", string Sales = "", bool IsLaywayUnpaid = false, string ByWhichDate = "", string FROMDATE = "", string DateTo = "", string Brand = "", string Category = "", string Selbrand = "", bool IsComision = false, bool IsCost = false, bool CommByDiscount = false, bool IsCommbyProfit = false, bool Is_DiamondDealer = false)
        {
            CommByDiscount = false;
            IsCommbyProfit = false;
            ListOfItemsSoldModel objModel = new ListOfItemsSoldModel();
            var data = new Dictionary<string, DataTable>();
            var sales1 = _listOfItemsSoldService.GetSalesBySalesmanDetails(Store, Sales, IsLaywayUnpaid, ByWhichDate, FROMDATE, DateTo, Brand, Category, Selbrand, IsComision, IsCost, CommByDiscount, IsCommbyProfit, Is_DiamondDealer);
            data.Add("sales", sales1);
            var store = this.GetSalesmanDetails(sales1);
            data.Add("salesman", store);
            return JsonConvert.SerializeObject(data);
        }

        public DataTable GetSalesmanDetails(DataTable sales)
        {
            string[] fldNames = { "Date" };
            DataTable dt1 = this.GetSysFormattedDateOnReport(sales, fldNames);
            DataTable dtSummarySales = new DataTable();
            dtSummarySales.Columns.Add("SALESMAN", typeof(string));
            dtSummarySales.Columns.Add("TOTALAMOUNT", typeof(decimal));
            dtSummarySales.Columns.Add("COMISH", typeof(decimal));
            dtSummarySales.Columns.Add("BRANDQTY", typeof(decimal));
            dtSummarySales.Columns.Add("BRANDAMOUNT", typeof(decimal));
            dtSummarySales.Columns.Add("OTHERAMOUNT", typeof(decimal));
            DataTable dt = sales.Clone();

            (new List<String> { "1", "2", "3", "4" }).ForEach(m =>
            {
                String salesman = $"Salesman{m}";
                String salesmanShare = $"SalesmanShare{m}";
                int Cnt = sales.AsEnumerable().Where(rs => rs.Field<String>(salesman) != String.Empty).ToList().Count;
                if (Cnt > 0)
                {
                    dtSummarySales = sales.AsEnumerable().Where(rs => rs.Field<String>(salesman) != String.Empty)
                                .GroupBy(r => new { SALESMAN = r[salesman] })
                                .Select(x =>
                                {
                                    var row = dtSummarySales.NewRow();
                                    row["SALESMAN"] = x.Max(r => r[salesman]);
                                    row["TotalAmount"] = x.Sum(r => _helperCommonService.DecimalCheckForDBNull(r[salesmanShare]));
                                    row["COMISH"] = x.Sum(r => _helperCommonService.DecimalCheckForDBNull(r["COMM_AMOUNT"]));
                                    row["BRANDQTY"] = 0;
                                    row["BRANDAMOUNT"] = 0;
                                    row["OTHERAMOUNT"] = 0;
                                    return row;
                                }).CopyToDataTable();


                    dt.Merge(dtSummarySales);
                }
            });
            if (dt != null && dt.Rows.Count > 0)
            {
                dt = dt.AsEnumerable().Where(rs => rs.Field<String>("SALESMAN") != String.Empty)
                                    .GroupBy(r => new { SALESMAN = r["SALESMAN"] })
                                    .Select(x =>
                                    {
                                        var row = dtSummarySales.NewRow();
                                        row["SALESMAN"] = x.Max(r => r["SALESMAN"]);
                                        row["TotalAmount"] = x.Sum(r => _helperCommonService.DecimalCheckForDBNull(r["TotalAmount"]));
                                        row["COMISH"] = x.Sum(r => _helperCommonService.DecimalCheckForDBNull(r["COMISH"]));
                                        row["BRANDQTY"] = 0;
                                        row["BRANDAMOUNT"] = 0;
                                        row["OTHERAMOUNT"] = 0;
                                        return row;
                                    }).CopyToDataTable();
            }

            int noOfdecimals = 0;
            foreach (DataRow rw in dt.Rows)
            {
                decimal qty = Convert.ToDecimal(string.Format("{0:N" + noOfdecimals + "}", sales.AsEnumerable().OfType<DataRow>().Where(r => (r.Field<string>("salesman1") == Convert.ToString(rw["SALESMAN"]) || r.Field<string>("salesman2") == Convert.ToString(rw["SALESMAN"]) || r.Field<string>("salesman3") == Convert.ToString(rw["SALESMAN"]) || r.Field<string>("salesman4") == Convert.ToString(rw["SALESMAN"]))).ToList().Sum(r => _helperCommonService.DecimalCheckForDBNull(r.Field<decimal?>("QTY")))));
                rw["BRANDAMOUNT"] = sales.AsEnumerable().OfType<DataRow>().Where(r => Convert.ToString(r.Field<string>("BRAND")) != String.Empty && (r.Field<string>("salesman1") == Convert.ToString(rw["SALESMAN"]) || r.Field<string>("salesman2") == Convert.ToString(rw["SALESMAN"]) || r.Field<string>("salesman3") == Convert.ToString(rw["SALESMAN"]) || r.Field<string>("salesman4") == Convert.ToString(rw["SALESMAN"]))).ToList().Sum(r => (_helperCommonService.DecimalCheckForDBNull(r.Field<decimal?>("QTY")) * _helperCommonService.DecimalCheckForDBNull(r.Field<decimal?>("PRICE"))));
                rw["OTHERAMOUNT"] = sales.AsEnumerable().OfType<DataRow>().Where(r => Convert.ToString(r.Field<string>("BRAND")) == String.Empty && (r.Field<string>("salesman1") == Convert.ToString(rw["SALESMAN"]) || r.Field<string>("salesman2") == Convert.ToString(rw["SALESMAN"]) || r.Field<string>("salesman3") == Convert.ToString(rw["SALESMAN"]) || r.Field<string>("salesman4") == Convert.ToString(rw["SALESMAN"]))).ToList().Sum(r => (_helperCommonService.DecimalCheckForDBNull(r.Field<decimal?>("QTY")) * _helperCommonService.DecimalCheckForDBNull(r.Field<decimal?>("PRICE"))));
                rw["BRANDQTY"] = qty;
            }
            return dt;
        }

        public DataTable GetSysFormattedDateOnReport(DataTable dt, string[] fieldName)
        {
            try
            {
                int cnt = 0;
                foreach (DataColumn col in dt.Columns)
                    if (col.DataType == typeof(DateTime))
                        cnt++;

                if (fieldName == null || cnt == 0)
                    return dt;

                DataTable dtCloned = dt.Clone();
                foreach (string fld in fieldName)
                    dtCloned.Columns[fld].DataType = typeof(string);

                foreach (DataRow row in dt.Rows)
                    dtCloned.ImportRow(row);

                foreach (string fld in fieldName)
                {
                    dtCloned.AsEnumerable().OfType<DataRow>().Where(r => ((r.Field<string>(fld)) != null) && (r.Field<string>(fld)) != string.Empty).ToList().ForEach(r => r[fld] = this.CheckForDBNull(Convert.ToDateTime(this.CheckForDBNull(r[fld])), "System.DateTime").ToString());
                    dtCloned.AcceptChanges();
                }

                return dtCloned;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public dynamic CheckForDBNull(object objval, string typename = "System.String")
        {

            switch (typename)
            {
                case "System.String":
                    return objval != DBNull.Value && objval != null && !string.IsNullOrWhiteSpace(objval.ToString()) ? objval.ToString() : string.Empty;
                case "System.Int32":
                    return objval != DBNull.Value && objval != null && !string.IsNullOrWhiteSpace(objval.ToString()) ? Convert.ToInt32(objval) : 0;
                case "System.Decimal":
                    return objval != DBNull.Value && objval != null && !string.IsNullOrWhiteSpace(objval.ToString()) ? Convert.ToDecimal(objval) : 0;
                case "System.Boolean":
                    return objval != DBNull.Value && objval != null && !string.IsNullOrWhiteSpace(objval.ToString()) ? Convert.ToBoolean(Convert.ToInt16(objval)) : false;
                case "System.DateTime":
                    return objval != DBNull.Value && objval != null && !string.IsNullOrWhiteSpace(objval.ToString()) ? Convert.ToDateTime(objval) : DateTime.Now;
                default:
                    return objval != DBNull.Value && objval != null && !string.IsNullOrWhiteSpace(objval.ToString()) ? objval.ToString() : string.Empty;
            }
        }

        public IActionResult StatementofInvoice(bool Statereturn = false)
        {
            if (Statereturn)
            {
                ViewBag.SalesmanData = _helperCommonService.GetAllSalesmansCodesList();
                ViewBag.StoreData = _helperCommonService.GetAllStoreCodesList();
                ViewBag.StyleBrandData = _helperCommonService.GetAllBrands();
                ViewBag.BrandData = _helperCommonService.GetAllBrandsFromStyle();
                ViewBag.CategoryData = _helperCommonService.GetAllCategories();
                ViewBag.ShowTitle = "List of Returns";
                ViewBag.ShowOptions = "style='display:none;'";
                ViewBag.Listreturn = true;
                ViewBag.ShowInactive = "0";
            }
            else
            {
                ViewBag.SalesmanData = _helperCommonService.GetAllSalesmansCodesList();
                ViewBag.StoreData = _helperCommonService.GetAllStoreCodesList();
                ViewBag.StyleBrandData = _helperCommonService.GetAllBrands();
                ViewBag.BrandData = _helperCommonService.GetAllBrandsFromStyle();
                ViewBag.CategoryData = _helperCommonService.GetAllCategories();
                ViewBag.ShowTitle = "STATEMENT OF INVOICE";
                ViewBag.ShowOptions = "";
                ViewBag.Listreturn = false;
                ViewBag.ShowInactive = "1";
            }

            ListOfItemsSoldModel objModel = new ListOfItemsSoldModel();
            objModel.CustomerCodes = _helperCommonService.GetAllCustomerCodes();
            return View("StatementofInvoice", objModel);
        }

        public string GetStatmentOfInvoice(string Ccode, string FROMDATE, string DateTo, string Store, string Sales, string State, string Country, string AmountFrom, string AmountTo, bool ChkSpecifyAmount, bool ChkOpenInvoicesOnly, bool ChkExcludeRepairInvoiceofZeroDollor, bool layaway, bool ChkshowshopifyOnly, bool Listreturn = false)
        {
            int level = 4;


            ListOfItemsSoldModel objModel = new ListOfItemsSoldModel();
            var data = _listOfItemsSoldService.GetStatmentOfInvoice(FROMDATE, DateTo);
            var dt = new DataTable();

            if (Ccode != "")
                data = data.Select($"ACC='{Ccode}'").CopyToDataTable();
            if (State != "")
                data = data.Select($"state1='{State}'").CopyToDataTable();
            if (Country != "")
                data = data.Select($"country='{Country}'").CopyToDataTable();
            if (Store != "")
                data = data.Select("STORE_NO='" + Store + "'").CopyToDataTable();
            if (Sales != "")
                data = data.Select($"(SALESMAN1 = '{Sales}' OR SALESMAN2 = '{Sales}')").CopyToDataTable();
            if (Listreturn)
                data = data.Select("(gr_total < 0)").CopyToDataTable();

            if (level < 4 && !Listreturn)
                data = data.Select($"inactive=0").CopyToDataTable();

            if (!Listreturn)
            {
                if (ChkSpecifyAmount)
                {
                    decimal fd = Convert.ToDecimal(AmountFrom);
                    decimal td = Convert.ToDecimal(AmountTo);

                    data = data.Select("(gr_total >=  " + fd + "  AND gr_total <= " + td + ")").CopyToDataTable();

                }

                if (ChkOpenInvoicesOnly)
                    data = data.Select("(gr_total >  credits)").CopyToDataTable();

                if (ChkExcludeRepairInvoiceofZeroDollor)
                    data = data.Select("(NOT(GR_TOTAL = 0 AND IS_REPAIR = 'REPAIR'))").CopyToDataTable();


                if (!layaway && !ChkshowshopifyOnly)
                    data = data.Select("(LAYAWAY = 0 AND (SPECIAL = 0 OR CREDITS>GR_TOTAL))").CopyToDataTable();

                if (data.Rows.Count > 0 && ChkshowshopifyOnly)
                {
                    DataRow[] filtered_rows = data.Select("SHOPIFY_ORD <>''");
                    if (filtered_rows.Length > 0)
                        data = filtered_rows.CopyToDataTable();
                    else
                        data.Clear();
                }
            }
            if (data.Rows.Count > 0)
            {
                DataRow[] dataRows = data.Select().OrderBy(u => u["DATE"]).ToArray();
                data = dataRows.CopyToDataTable();
            }
            return JsonConvert.SerializeObject(data);
        }

        public IActionResult DaysOnHandInventory()
        {
            ViewBag.StyleBrandData = _helperCommonService.GetAllBrands();
            ViewBag.BrandData = _helperCommonService.GetAllBrandsFromStyle();
            ViewBag.CategoryData = _helperCommonService.GetAllCategories();
            ViewBag.SubCategoryData = _helperCommonService.GetAllSubCategories();
            ViewBag.VenderTypes = _helperCommonService.GetAllVendors();
            ViewBag.MetalTypes = _helperCommonService.GetAllMetals();
            ViewBag.BrandTypes = _helperCommonService.GetAllBrandsFromStyle();

            ListOfItemsSoldModel objModel = new ListOfItemsSoldModel();
            objModel.CustomerCodes = _helperCommonService.GetAllCustomerCodes();

            return View("DaysOnHandInventory", objModel);
        }

        public string GetDaysOnHandInventory(string Ccode, string FROMDATE, string DateTo, string Category, string SubCategory, string Metal, string Brand, string FromStyle, string ToStyle, string Vendor, Int32 NoOfDays = 0, bool SummByModel = false)
        {
            ListOfItemsSoldModel objModel = new ListOfItemsSoldModel();
            var data = _listOfItemsSoldService.GetDaysOnHandInventoryDetails(Ccode, FROMDATE, DateTo, Category, SubCategory, Metal, Brand, FromStyle, ToStyle, Vendor, NoOfDays, SummByModel);
            return JsonConvert.SerializeObject(data);
        }

        public IActionResult VendorSalesReport()
        {
            ViewBag.VenderTypes = _helperCommonService.GetAllVendors();
            ListOfItemsSoldModel objModel = new ListOfItemsSoldModel();
            objModel.CustomerCodes = _helperCommonService.GetAllCustomerCodes();

            return View("~/Views/Analysis/VendorSalesReport.cshtml", objModel);
        }

        public string GetVendorSalesReport(string Ccode, string FROMDATE, string DateTo)
        {

            DateTime d = Convert.ToDateTime(DateTo).AddDays(1).AddSeconds(-1);
            string todate = d.ToString();
            ListOfItemsSoldModel objModel = new ListOfItemsSoldModel();
            var data = _listOfItemsSoldService.GetVendorSalesReportData(Ccode, FROMDATE, todate);
            return JsonConvert.SerializeObject(data);
        }
        public IActionResult InstateOutStateSales()
        {
            ListOfItemsSoldModel objModel = new ListOfItemsSoldModel();
            objModel.CustomerCodes = _helperCommonService.GetAllCustomerCodes();

            return View("~/Views/Analysis/InstateOutStateSales.cshtml", objModel);
        }
        public string GetInstateOutStateSales(string State, string FROMDATE, string DateTo)
        {
            ListOfItemsSoldModel objModel = new ListOfItemsSoldModel();
            var data = _listOfItemsSoldService.GetInstateOutStateSalesDetails(State, FROMDATE, DateTo);

            return JsonConvert.SerializeObject(data);
        }
        public IActionResult StateSales()
        {
            return View("~/Views/Analysis/StateSales.cshtml");
        }

        public string GetStateSales(string FROMDATE, string DateTo, bool ByPickupDate, bool IsLaywayUnpaid)
        {
            ListOfItemsSoldModel objModel = new ListOfItemsSoldModel();
            var data = _listOfItemsSoldService.GetStateSalesDetails(FROMDATE, DateTo, ByPickupDate, IsLaywayUnpaid);

            return JsonConvert.SerializeObject(data);
        }

        public IActionResult VendorStylesPerformance()
        {
            ViewBag.GroupData = _helperCommonService.GetAllGroups();
            ViewBag.StyleBrandData = _helperCommonService.GetAllBrands();
            ViewBag.BrandData = _helperCommonService.GetAllBrandsFromStyle();
            ViewBag.CategoryData = _helperCommonService.GetAllCategories();
            ViewBag.SubCategoryData = _helperCommonService.GetAllSubCategories();
            ViewBag.VenderTypes = _helperCommonService.GetAllVendors();
            ViewBag.MetalTypes = _helperCommonService.GetAllMetals();
            ViewBag.BrandTypes = _helperCommonService.GetAllBrandsFromStyle();
            return View("~/Views/Analysis/VendorStylesPerformance.cshtml");
        }

        public string GetVendorStylePerformanceDetails(string Group, string Category, string SubCategory, string Metal, string Vendor, string FROMDATE, string DateTo, string Purcfdate, string Purctdate, bool SummByModel = false, bool Is_DiamondDealer = false)
        {
            ListOfItemsSoldModel objModel = new ListOfItemsSoldModel();
            var data = _listOfItemsSoldService.GetVendorStylePerformenctDetails(Group, Category, SubCategory, Metal, Vendor, FROMDATE, DateTo, Purcfdate, Purctdate, SummByModel, Is_DiamondDealer);
            return JsonConvert.SerializeObject(data);
        }

        public IActionResult TotalSalesPerState()
        {
            ViewBag.StoreData = _helperCommonService.GetAllStoreCodesList();
            return View("~/Views/Analysis/TotalSalesPerState.cshtml");
        }

        public string GetTotalSalesPerState(string FROMDATE, string DateTo, string Store, bool IncludeInactive = false, bool NoSalesTax = false, bool IncludeNotaxInvoices = false, string TaxState = "", bool IncParialPay = false)
        {
            ListOfItemsSoldModel objModel = new ListOfItemsSoldModel();
            var data = _listOfItemsSoldService.GetTotalSalesPerStateDetails(FROMDATE, DateTo, Store, IncludeInactive, NoSalesTax, IncludeNotaxInvoices, "", IncParialPay);
            if (data.Rows.Count > 0 && TaxState != "")
            {
                DataRow[] filtered_rows = data.Select("STATE ='" + TaxState + "'");
                if (filtered_rows.Length > 0)
                    data = filtered_rows.CopyToDataTable();
                else
                    data.Clear();
            }
            return JsonConvert.SerializeObject(data);
        }

        public IActionResult TotalHourlySales()
        {
            ViewBag.StoreData = _helperCommonService.GetAllStoreCodesList();
            return View("~/Views/Analysis/TotalHourlySales.cshtml");
        }

        public string GetTotalHourlySales(string FROMDATE, string DateTo, string Store, string Day)
        {
            ListOfItemsSoldModel objModel = new ListOfItemsSoldModel();
            var data = _listOfItemsSoldService.GetTotalHourlySalesDetails(FROMDATE, DateTo, Store, Day);
            return JsonConvert.SerializeObject(data);
        }
        public IActionResult TotalWeeklySales()
        {
            ViewBag.StoreData = _helperCommonService.GetAllStoreCodesList();
            return View("~/Views/Analysis/TotalWeeklySales.cshtml");
        }
        public string GetTotalWeeklySales(string FROMDATE, string DateTo, string Store)
        {
            ListOfItemsSoldModel objModel = new ListOfItemsSoldModel();
            var data = _listOfItemsSoldService.GetTotalWeeklySalesDetails(FROMDATE, DateTo, Store);
            return JsonConvert.SerializeObject(data);
        }
        public IActionResult SaleProfitPerStore()
        {

            ViewBag.StyleBrandData = _helperCommonService.GetAllBrands();
            ViewBag.BrandData = _helperCommonService.GetAllBrandsFromStyle();
            ViewBag.CategoryData = _helperCommonService.GetAllCategories();
            ViewBag.SubCategoryData = _helperCommonService.GetAllSubCategories();
            ViewBag.VenderTypes = _helperCommonService.GetAllVendors();
            ViewBag.MetalTypes = _helperCommonService.GetAllMetals();
            ViewBag.BrandTypes = _helperCommonService.GetAllBrandsFromStyle();
            ViewBag.StoreData = _helperCommonService.GetAllStoreCodesList();
            ViewBag.SalesmanData = _helperCommonService.GetAllSalesmansCodesList();

            return View("~/Views/Analysis/SaleProfitPerStore.cshtml");
        }

        public string GetSalesProfitPerStore(string Ccode, string FROMDATE, string DateTo, string Category, string SubCategory, string Metal, string Brand, string FromStyle, string ToStyle, string Vendor, string Store, bool WithGP = false, bool SeparateSM = false, bool IsSalesCOG = false, string ByWhichDate = "", bool IsLayaway = false, String Sales = "", bool Monthproft = false, bool ISLaySpe = false, bool Isinclbankfee = false)
        {
            ListOfItemsSoldModel objModel = new ListOfItemsSoldModel();
            var data = _listOfItemsSoldService.GetSalesProfitPerStoreDetails(Ccode, FROMDATE, DateTo, Category, SubCategory, Metal, Brand, FromStyle, ToStyle, Vendor, Store, WithGP, SeparateSM, IsSalesCOG, ByWhichDate, IsLayaway, Sales, Monthproft, ISLaySpe, Isinclbankfee);
            return JsonConvert.SerializeObject(data);
        }

        public IActionResult SalesCOGProfit()
        {
            ViewBag.StyleBrandData = _helperCommonService.GetAllBrands();
            ViewBag.BrandData = _helperCommonService.GetAllBrandsFromStyle();
            ViewBag.CategoryData = _helperCommonService.GetAllCategories();
            ViewBag.SubCategoryData = _helperCommonService.GetAllSubCategories();
            ViewBag.VenderTypes = _helperCommonService.GetAllVendors();
            ViewBag.MetalTypes = _helperCommonService.GetAllMetals();
            ViewBag.BrandTypes = _helperCommonService.GetAllBrands();
            ViewBag.StoreData = _helperCommonService.GetAllStoreCodesList();
            ViewBag.SalesmanData = _helperCommonService.GetAllSalesmansCodesList();
            return View("~/Views/Analysis/SalesCOGProfit.cshtml");
        }

        public string GetSalesCOGProfit(string Ccode, string FROMDATE, string DateTo, string Category, string SubCategory, string Metal, string Brand, string FromStyle, string ToStyle, string Vendor, string Store, bool WithGP = false, bool SeparateSM = false, bool IsSalesCOG = false, string ByWhichDate = "", bool IsLayaway = false, String Sales = "", bool Monthproft = false, bool ISLaySpe = false, bool Isinclbankfee = false, bool SimpleExcel = false)
        {

            ListOfItemsSoldModel objModel = new ListOfItemsSoldModel();
            DataTable data;
            if (SimpleExcel)
            {
                data = _listOfItemsSoldService.GetStoreDetailsSimplifiedExecel(Ccode, FROMDATE, DateTo, Category, SubCategory, Metal, Brand, FromStyle, ToStyle, Vendor, Store, WithGP, SeparateSM, IsSalesCOG, ByWhichDate, IsLayaway, Sales, Monthproft, ISLaySpe, Isinclbankfee);
                return JsonConvert.SerializeObject(data);
            }
            data = _listOfItemsSoldService.GetSalesProfitPerStoreDetails(Ccode, FROMDATE, DateTo, Category, SubCategory, Metal, Brand, FromStyle, ToStyle, Vendor, Store, WithGP, SeparateSM, IsSalesCOG, ByWhichDate, IsLayaway, Sales, Monthproft, ISLaySpe, Isinclbankfee);
            return JsonConvert.SerializeObject(data);
        }

        public IActionResult DetailedCOG()
        {
            ViewBag.StoreData = _helperCommonService.GetAllStoreCodesList();
            return View("~/Views/Analysis/DetailedCOG.cshtml");
        }

        public string GetDetailedCOG(string FROMDATE, string DateTo, string Store, string ByWhichDate, bool layaway = false, bool IsCost = false)
        {
            ListOfItemsSoldModel objModel = new ListOfItemsSoldModel();
            var data = _listOfItemsSoldService.GetDetailedCOGDetails(FROMDATE, DateTo, Store, ByWhichDate, layaway, IsCost);
            return JsonConvert.SerializeObject(data);
        }

        public IActionResult SalesProfitPerSource()
        {
            ViewBag.StoreData = _helperCommonService.GetAllStoreCodesList();
            return View("~/Views/Analysis/SalesProfitPerSource.cshtml");

        }

        public string GetSalesProfitPerSource(string FROMDATE, string DateTo, string Store, string ByWhichDate, string Details = "", string Sources = "")
        {
            ListOfItemsSoldModel objModel = new ListOfItemsSoldModel();
            var data = _listOfItemsSoldService.GetSalesProfitPerStoreDetails(FROMDATE, DateTo, Store, ByWhichDate, Details, Sources);
            return JsonConvert.SerializeObject(data);
        }

        public IActionResult SalesProfitByCity()
        {
            ViewBag.StyleBrandData = _helperCommonService.GetAllBrands();
            ViewBag.BrandData = _helperCommonService.GetAllBrandsFromStyle();
            ViewBag.CategoryData = _helperCommonService.GetAllCategories();
            ViewBag.SubCategoryData = _helperCommonService.GetAllSubCategories();
            ViewBag.VenderTypes = _helperCommonService.GetAllVendors();
            ViewBag.MetalTypes = _helperCommonService.GetAllMetals();
            ViewBag.BrandTypes = _helperCommonService.GetAllBrands();
            ViewBag.StoreData = _helperCommonService.GetAllStoreCodesList();
            ViewBag.SalesmanData = _helperCommonService.GetAllSalesmansCodesList();
            return View("~/Views/Analysis/SalesProfitByCity.cshtml");

        }
        public string GetSalesProfitByCity(string Ccode, string FROMDATE, string DateTo, string Category, string SubCategory, string Metal, string Brand, string FromStyle, string ToStyle, string Vendor, string Sales, string ByWhichDate = "")
        {
            ListOfItemsSoldModel objModel = new ListOfItemsSoldModel();
            var data = _listOfItemsSoldService.GetSalesProfitByCityDetails(Ccode, FROMDATE, DateTo, Category, SubCategory, Metal, Brand, FromStyle, ToStyle, Vendor, Sales, ByWhichDate);
            return JsonConvert.SerializeObject(data);
        }

        public IActionResult MonthlySalesProfit()
        {
            ViewBag.StoreData = _helperCommonService.GetAllStoreCodesList();
            return View("~/Views/Analysis/MonthlySalesProfit.cshtml");
        }

        public string GetMonthlySalesProfit(string FROMDATE, string DateTo, string Store, string ByWhichDate, bool layaway = false)
        {
            ListOfItemsSoldModel objModel = new ListOfItemsSoldModel();
            var data = _listOfItemsSoldService.GetSalesProfitPerStoreDetails("", FROMDATE, DateTo, "", "", "", "", "", "", "", Store, false, false, false, ByWhichDate, false, "", true, layaway);
            return JsonConvert.SerializeObject(data);
        }

        public IActionResult SalesReportForReorder()
        {
            ListOfItemsSoldModel objModel = new ListOfItemsSoldModel();
            objModel.CustomerCodes = _helperCommonService.GetAllCustomerCodes();
            ViewBag.Vendors = _helperCommonService.GetAllVendorsCodes();
            ViewBag.VendorStyles = _helperCommonService.GetAllVendorStyles();
            return View("~/Views/Analysis/SalesReportForReorder.cshtml", objModel);
        }
        public string GetSalesReportForReorder(string Ccode, string FROMDATE, string DateTo, string FromStyle, string ToStyle, string Vendor, string VendorStyle, bool IsLayaway = false)
        {
            ListOfItemsSoldModel objModel = new ListOfItemsSoldModel();
            var data = _listOfItemsSoldService.GetSalesReportForReorderDetails(Ccode, FROMDATE, DateTo, "", "", "", "", FromStyle, ToStyle, Vendor, VendorStyle, IsLayaway);
            return JsonConvert.SerializeObject(data);
        }

        public IActionResult SalesSummaryReport()
        {
            return View("~/Views/Analysis/SalesSummaryReport.cshtml");
        }

        public string GetSalesSummaryReport(string FROMDATE, string DateTo, bool ByWhichDate, bool IncParialPay = false)
        {

            var data = new Dictionary<string, DataTable>();

            var sales = _helperCommonService.GetSalesSummaryReportDetails(FROMDATE, DateTo, IncParialPay, ByWhichDate, 1);
            data.Add("sales", sales);
            var store = _helperCommonService.GetSalesSummaryReportDetails(FROMDATE, DateTo, IncParialPay, ByWhichDate, 2);
            data.Add("store", store);
            var salesman = _helperCommonService.GetSalesSummaryReportDetails(FROMDATE, DateTo, IncParialPay, ByWhichDate, 3);
            data.Add("salesman", salesman);
            return JsonConvert.SerializeObject(data);
        }

        public IActionResult UpdateInvoiceCost()
        {
            ViewBag.StoreData = _helperCommonService.GetAllStoreCodesList();
            return View("UpdateInvoiceCost");
        }

        public string GetInvoiCostItemsDetails(string Inv_no)
        {
            DataTable repainInvoice = _helperCommonService.IsRepairInvoice(Inv_no);

            if (repainInvoice.Rows.Count > 0)
            {
                var data = new
                {
                    invStatus = new[]
                    {
                        new
                        {
                            isRepairItem = true,
                            content =""
                        }
                    }
                };
                return JsonConvert.SerializeObject(data);
            }
            else
            {
                var data = new
                {
                    invStatus = new[]
                     {
                        new
                        {
                            isRepairItem = false,
                            content = _helperCommonService.GetInvoiCostItems(Inv_no)
                        }
                    }
                };
                return JsonConvert.SerializeObject(data);
            }
        }

        public IActionResult GetInvoiceDetails(string FROMDATE, string DateTo, string Ccode, string FName, string LName, string Address, string Tel, string State, string Zip, string Store, string TotalFrom, string TotalTo, bool ItemsNotPreviouslyReturned = false, bool GetOnlyActiveInactive = false, bool IsforSpecial = false, bool LayAways = false, bool chkIncludeRegular = false, bool chkIncludeSpecial = false, bool chkIncludeRepair = false, bool chkIncludeLayaway = false, bool ismemo = false)
        {

            bool ActiveOrInactive = false;
            bool isreturn = false;

            string sFilter = "1=1";

            if (!string.IsNullOrEmpty(Ccode))
                sFilter += string.Format(" AND {0} LIKE '%{1}%'", "CUSTOMER.acc", Ccode);

            if (!string.IsNullOrEmpty(FName))
                sFilter += string.Format(" AND {0} LIKE '{1}%'", "CUSTOMER.name", FName);

            if (!string.IsNullOrEmpty(LName))
                sFilter += string.Format(" AND {0} LIKE '%{1}%'", "CUSTOMER.name", LName);

            if (!string.IsNullOrEmpty(Tel))
                sFilter += string.Format(" AND {0} = {1}", "CUSTOMER.tel", Tel);

            sFilter += string.Format(" And (CAST({0} AS DATE) >= '{1:MM/dd/yyyy}' ", "date", FROMDATE);
            sFilter += string.Format(" And CAST({0} AS DATE) <= '{1:MM/dd/yyyy}' )", "date", DateTo);

            if (!string.IsNullOrEmpty(Address))
                sFilter += string.Format(" AND {0} LIKE '%{1}%'", "CUSTOMER.addr1", Address);

            if (!string.IsNullOrEmpty(State))
                sFilter += string.Format(" AND {0} LIKE '%{1}%'", "state1", State);

            if (!string.IsNullOrEmpty(Zip))
                sFilter += string.Format(" AND {0} LIKE '%{1}%'", "zip1", Zip);

            if (TotalTo != "" && TotalFrom != "" && Convert.ToDecimal(TotalTo) > 0)
                sFilter += string.Format(" AND (gr_total >= {0} AND gr_total <= {1})", string.Format("{0:0.00}", Convert.ToDecimal(TotalFrom)), string.Format("{0:0.00}", Convert.ToDecimal(TotalTo)));

            if (ItemsNotPreviouslyReturned)
            {
                sFilter += " AND gr_total > 0";
            }

            if (GetOnlyActiveInactive)
            {
                sFilter += string.Format(" AND INVOICE.Inactive = {0} ", (ActiveOrInactive ? 0 : 1));
            }
            string memoOrInvoice = "";
            memoOrInvoice = ismemo ? "MEMO.STORE_NO" : "INVOICE.STORE_NO";
            sFilter += string.Format(" AND " + memoOrInvoice + "=IIF({0}=1," + memoOrInvoice + ",'{1}')", (Store == "") ? 1 : 0, Store);


            if (ismemo)
            {
                var data = SearchMemo(sFilter);
                return PartialView("~/Views/Shared/_SearchMemoCodeResult.cshtml", data);
            }
            List<string> invoiceTypeConditions = new List<string>();
            //  If called from the Special Order form
            if (IsforSpecial)
            {
                sFilter += " AND invoice.picked = 0 AND is_deb = 0 AND dbo.iSSpecialInvoice(invoice.inv_no) = 1";
            }
            else
            {

                if (chkIncludeRegular)
                    invoiceTypeConditions.Add("invoice.inactive = 0 AND INVOICE.LAYAWAY = 0 AND INVOICE.V_CTL_NO <> 'REPAIR' AND dbo.iSSpecialInvoice(INVOICE.INV_NO) = 0");

                if (chkIncludeRepair)
                    invoiceTypeConditions.Add("INVOICE.V_CTL_NO = 'REPAIR'");

                if (LayAways || chkIncludeLayaway)
                    invoiceTypeConditions.Add("INVOICE.LAYAWAY = 1");

                if (IsforSpecial || chkIncludeSpecial)
                    invoiceTypeConditions.Add("invoice.picked = 0 AND is_deb = 0 AND dbo.iSSpecialInvoice(invoice.inv_no) = 1");


            }

            if (invoiceTypeConditions.Count > 0)
            {
                string combinedCondition = "(" + string.Join(" OR ", invoiceTypeConditions) + ")";
                sFilter += " AND " + combinedCondition;
            }

            if (isreturn)
                sFilter += " AND INVOICE.INV_NO IN (SELECT INV_NO from IN_ITEMS where qty < 0 )";


            else if (!isreturn)
                sFilter += "AND INVOICE.INV_NO IN (SELECT INV_NO FROM IN_ITEMS UNION SELECT INV_NO FROM rep_inv)";


            var dtInvoice = _helperCommonService.SearchInvoice(sFilter, ItemsNotPreviouslyReturned);
            return PartialView("~/Views/Shared/_SearchInvoiceCodeResult.cshtml", dtInvoice);
        }

        public DataTable SearchMemo(string filter = "")
        {
            if (!string.IsNullOrWhiteSpace(filter))
                return _helperCommonService.GetSqlData(string.Format(@"SELECT ID,MEMO_NO,CUSTOMER.ACC,CUSTOMER.NAME,stuff(stuff(customer.TEL, 4, 0, '-'), 8, 0, '-') TEL,MEMO.DATE,CUSTOMER.ADDR1,STATE1,ZIP1,GR_TOTAL,PON,[Message] FROM MEMO INNER JOIN CUSTOMER ON MEMO.ACC = CUSTOMER.ACC where {0} ORDER BY DATE", filter));
            return _helperCommonService.GetSqlData(@"SELECT ID,MEMO_NO,CUSTOMER.ACC,CUSTOMER.NAME,stuff(stuff(customer.TEL, 4, 0, '-'), 8, 0, '-') TEL,MEMO.DATE,CUSTOMER.ADDR1,STATE1,ZIP1,GR_TOTAL,PON,[Message] FROM MEMO INNER JOIN CUSTOMER ON MEMO.ACC = CUSTOMER.ACC ORDER BY DATE");
        }

        
        public bool UpdateInvoiceCostWithStyleCost(string Inv_no, [FromBody] string JsonData)
        {
            return _helperCommonService.UpdateInTemCost(JsonData, Inv_no.Trim().PadLeft(6, ' '));
        }

        public IActionResult RcblCreditByTimeFrame()
        {
            ViewBag.CustomerCodes = _helperCommonService.GetAllCustomerCodes();
            ViewBag.RegisterData = _helperCommonService.GetAllRegisterCodesList();
            ViewBag.StoreData = _helperCommonService.GetAllStoreCodesList();
            ViewBag.PaymentTypesData = _helperCommonService.GetDistPaymentType();
            return View("~/Views/ListOfItemsSold/RcblCreditByTimeFrame.cshtml");
        }

        public string GetRcblCreditByTimeFrame(string Ccode, string ByWhichDate, string FROMDATE, string DateTo, bool FinanceChargeCB, bool Chksummarybyptype, string Register, string Store, string DdlPaymentsTypes, bool ChkReceipts, bool ChkCredits, bool ChkRepairOnly)
        {

            if (Chksummarybyptype)
            {
                string Ccode2 = "ZZ";
                if (Ccode == "")
                    Ccode2 = "ZZ";
                else
                    Ccode2 = Ccode;
                string Trantype = "";
                if (ChkReceipts)
                    Trantype += "P,";

                if (ChkCredits)
                    Trantype += "C,";

                bool repair = ChkRepairOnly;
                ListOfItemsSoldModel objModel = new ListOfItemsSoldModel();
                var data = _listOfItemsSoldService.SummaryByPayment(Ccode, Ccode2, ByWhichDate, FROMDATE, DateTo, Trantype, Register, false, repair);

                if (data != null && data.Rows.Count > 0 && Store != "")
                {
                    DataRow[] filtered_rows = data.Select("STORE_NO = '" + Store + "'");
                    if (filtered_rows.Length > 0)
                        data = filtered_rows.CopyToDataTable();
                    else
                        data.Clear();
                }

                string[] Npaymenttypes = DdlPaymentsTypes.Split(',');
                DdlPaymentsTypes = "";
                foreach (string item in Npaymenttypes)
                {
                    if (item != "")
                        DdlPaymentsTypes += "'" + item + "',";
                }

                if (data != null && data.Rows.Count > 0 && DdlPaymentsTypes != "")
                {
                    DataRow[] filtered_rows = data.Select("Paymenttype in ( " + DdlPaymentsTypes.Substring(0, DdlPaymentsTypes.Length - 1) + ")");
                    if (filtered_rows.Length > 0)
                    {
                        data = filtered_rows.CopyToDataTable();
                    }
                    else
                    {
                        data.Clear();
                    }
                }

                return JsonConvert.SerializeObject(data);

            }
            else
            {
                string Ccode2 = "ZZ";
                if (Ccode == "")
                {
                    Ccode2 = "ZZ";
                }
                else
                {
                    Ccode2 = Ccode;
                }
                string Trantype = "";
                if (ChkReceipts)
                    Trantype += "P,";

                if (ChkCredits)
                    Trantype += "C,";


                ListOfItemsSoldModel objModel = new ListOfItemsSoldModel();

                var data = _listOfItemsSoldService.GetRcvableCreditByTimeFrame(Ccode, Ccode2, ByWhichDate, FROMDATE, DateTo, Trantype, Register, ChkRepairOnly ? 1 : 0);
                if (data != null && data.Rows.Count > 0 && Store != "")
                {
                    DataRow[] filtered_rows = data.Select("STORE_NO = '" + Store + "'");
                    if (filtered_rows.Length > 0)
                    {
                        data = filtered_rows.CopyToDataTable();
                    }
                    else
                    {
                        data.Clear();
                    }
                }
                if (data != null && data.Rows.Count > 0 && Register != "")
                {
                    DataRow[] filtered_rows = data.Select("REGISTER = '" + Register + "'");
                    if (filtered_rows.Length > 0)
                    {
                        data = filtered_rows.CopyToDataTable();
                    }
                    else
                    {
                        data.Clear();
                    }
                }
                if (data != null && data.Rows.Count > 0 && Ccode != "")
                {
                    DataRow[] filtered_rows = data.Select("ACC = '" + Ccode + "'");
                    if (filtered_rows.Length > 0)
                    {
                        data = filtered_rows.CopyToDataTable();
                    }
                    else
                    {
                        data.Clear();
                    }
                }
                string[] Npaymenttypes = DdlPaymentsTypes.Split(',');
                DdlPaymentsTypes = "";
                foreach (string item in Npaymenttypes)
                {
                    if (item != "")
                    {
                        DdlPaymentsTypes += "'" + item + "',";
                    }
                }

                if (data != null && data.Rows.Count > 0 && DdlPaymentsTypes != "")
                {
                    DataRow[] filtered_rows = data.Select("Paymenttype in ( " + DdlPaymentsTypes.Substring(0, DdlPaymentsTypes.Length - 1) + ")");
                    if (filtered_rows.Length > 0)
                    {
                        data = filtered_rows.CopyToDataTable();
                    }
                    else
                    {
                        data.Clear();
                    }
                }
                return JsonConvert.SerializeObject(data);
            }
        }

        public IActionResult SummeryByPaymentType()
        {
            ViewBag.CustomerCodes = _helperCommonService.GetAllCustomerCodes();
            ViewBag.RegisterData = _helperCommonService.GetAllRegisterCodesList();
            ViewBag.StoreData = _helperCommonService.GetAllStoreCodesList();
            ViewBag.PaymentTypesData = _helperCommonService.GetDistPaymentType();
            ViewBag.CurrencyModuleVisible = "style=display:none;";
            return View("~/Views/ListOfItemsSold/SummeryByPaymentType.cshtml");
        }

        public string GetSummaryByPaymentType(string Ccode, string ByWhichDate, string FROMDATE, string DateTo, bool Chksummarybyptype, string Register, string Store, string DdlPaymentsTypes, bool ChkReceipts, bool ChkCredits, bool chkShowByCurrency = false)
        {
            string Ccode2 = "ZZ";
            ListOfItemsSoldModel objModel;
            DataTable data;
            string[] Npaymenttypes;
            string Trantype = "";
            if (Chksummarybyptype)
            {
                if (Ccode == "")
                    Ccode2 = "ZZ";
                else
                    Ccode2 = Ccode;
                if (ChkReceipts)
                    Trantype += "P,";
                if (ChkCredits)
                    Trantype += "C,";

                bool allstore = (Store == "");

                objModel = new ListOfItemsSoldModel();
                data = _listOfItemsSoldService.SummaryByPayment(Ccode, Ccode2, ByWhichDate, FROMDATE, DateTo, Trantype, Register, allstore, false, chkShowByCurrency, Store);
                if (_helperCommonService.DataTableOK(data) && Store != "")
                {
                    DataRow[] filtered_rows = data.Select("STORE_NO = '" + Store + "'");
                    if (filtered_rows.Length > 0)
                        data = filtered_rows.CopyToDataTable();
                    else
                        data.Clear();
                }

                Npaymenttypes = DdlPaymentsTypes.Split(',');
                DdlPaymentsTypes = "";
                foreach (string item in Npaymenttypes)
                    if (item != "")
                        DdlPaymentsTypes += "'" + item + "',";

                if (_helperCommonService.DataTableOK(data) && DdlPaymentsTypes != "")
                {
                    DataRow[] filtered_rows = data.Select("Paymenttype in ( " + DdlPaymentsTypes.Substring(0, DdlPaymentsTypes.Length - 1) + ")");
                    if (filtered_rows.Length > 0)
                        data = filtered_rows.CopyToDataTable();
                    else
                        data.Clear();
                }
                return JsonConvert.SerializeObject(data);
            }
            if (Ccode == "")
                Ccode2 = "ZZ";
            else
                Ccode2 = Ccode;
            Trantype = "";
            if (ChkReceipts)
                Trantype += "P,";

            if (ChkCredits)
                Trantype += "C,";


            objModel = new ListOfItemsSoldModel();

            data = _listOfItemsSoldService.GetRcvableCreditByTimeFrame(Ccode, Ccode2, ByWhichDate, FROMDATE, DateTo, Trantype, Register, 0, "", 0);

            if (data != null && data.Rows.Count > 0 && Store != "")
            {
                DataRow[] filtered_rows = data.Select("STORE_NO = '" + Store + "'");
                if (filtered_rows.Length > 0)
                    data = filtered_rows.CopyToDataTable();
                else
                    data.Clear();
            }
            if (data != null && data.Rows.Count > 0 && Register != "")
            {
                DataRow[] filtered_rows = data.Select("REGISTER = '" + Register + "'");
                if (filtered_rows.Length > 0)
                    data = filtered_rows.CopyToDataTable();
                else
                    data.Clear();
            }
            if (data != null && data.Rows.Count > 0 && Ccode != "")
            {
                DataRow[] filtered_rows = data.Select("ACC = '" + Ccode + "'");
                if (filtered_rows.Length > 0)
                {
                    data = filtered_rows.CopyToDataTable();
                }
                else
                {
                    data.Clear();
                }
            }
            Npaymenttypes = DdlPaymentsTypes.Split(',');
            DdlPaymentsTypes = "";
            foreach (string item in Npaymenttypes)
            {
                if (item != "")
                {
                    DdlPaymentsTypes += "'" + item + "',";
                }
            }

            if (data != null && data.Rows.Count > 0 && DdlPaymentsTypes != "")
            {
                DataRow[] filtered_rows = data.Select("Paymenttype in ( " + DdlPaymentsTypes.Substring(0, DdlPaymentsTypes.Length - 1) + ")");
                if (filtered_rows.Length > 0)
                {
                    data = filtered_rows.CopyToDataTable();
                }
                else
                {
                    data.Clear();
                }
            }
            return JsonConvert.SerializeObject(data);
        }

        public IActionResult DaySummary()
        {

            ViewBag.StoreData = _helperCommonService.GetAllStoreCodesList();
            ViewBag.SalesmanData = _helperCommonService.GetAllSalesmansCodesList();
            ViewBag.MerchandiseData = _helperCommonService.GetAllGroups();

            //ViewBag.CurrencyModuleVisible = _helperCommonService.CheckModuleEnabled(_helperCommonService.Modules.Currencies);
            ViewBag.CurrencyModuleVisible = "style=display:none;";
            return View("~/Views/ListOfItemsSold/DaySummary.cshtml");
        }

        public string GetDaySummary(string FROMDATE, string DateTo, string SelGroup, string SelIndividual, bool IsPartial = false, bool ByWhichDate = false, String GkOption = "1")
        {
            //SayGst = _helperCommonService.CheckModuleEnabled(_helperCommonService.Modules.SayGST);
            bool SayGst = false;
            var data = _helperCommonService.DaySalesSummary(FROMDATE, DateTo, SelGroup, SelIndividual, IsPartial, ByWhichDate, GkOption, SayGst);
            return JsonConvert.SerializeObject(data);
        }

        public IActionResult EODWorksheet(string FROMDATE = "", string DateTo = "", string Store = "")
        {
            ViewBag.StoreData = _helperCommonService.GetAllStoreCodesList();
            ViewBag.table1Headers =
                ViewBag.table1Body = "";
            ViewBag.fromDate =
                ViewBag.toDate = DateTime.Now.ToString("yyyy-MM-dd");
            if (FROMDATE != "" && DateTo != "" && Store != "")
            {
                ViewBag.fromDate = FROMDATE;
                ViewBag.toDate = DateTo;
                ListOfItemsSoldModel objModel = new ListOfItemsSoldModel();
                var data1 = _listOfItemsSoldService.EODSummary(FROMDATE, DateTo, Store);
                string[] columnNames = data1.Columns.Cast<DataColumn>()
                                                .Select(column => column.ColumnName)
                                                .ToArray();
                string dataTable1HeaderHtml = "<tr>";
                if (columnNames.Length > 0)
                    for (int i = 0; i < columnNames.Length; i++)
                        dataTable1HeaderHtml += "<th class=\"text-center\">" + columnNames[i] + "</th>";
                dataTable1HeaderHtml += "</tr>";
                string dataTable1BodyHtml = "";
                if (data1.Rows.Count > 0)
                {
                    int i = 0;
                    foreach (DataRow dr in data1.Rows)
                    {
                        if (i != data1.Rows.Count)
                        {
                            dataTable1BodyHtml += "<tr>";
                            for (int j = 0; j < columnNames.Length; j++)
                            {
                                string className = "";
                                if (columnNames[j] != "PAYMENTTYPE" && columnNames[j] != "paymenttype")
                                {
                                    className = "class='text-right'";
                                }
                                dataTable1BodyHtml += "<td " + className + ">" + dr[columnNames[j]] + "</td>";
                            }
                            dataTable1BodyHtml += "</tr>";
                        }
                        i++;
                    }

                }
                ViewBag.table1Headers = dataTable1HeaderHtml;
                ViewBag.table1Body = dataTable1BodyHtml;
                var data2 = _listOfItemsSoldService.EODWorkSheeetDetail(FROMDATE, DateTo, Store);
                ViewBag.table2Headers = "";
                ViewBag.table2Body = "";

                if (data2 != null)
                {
                    for (int i = data2.Rows.Count - 1; i >= 0; i--)
                    {
                        DataRow dr = data2.Rows[i];
                        if (dr["DETAIL"].ToString() == "Grand Total")
                            dr.Delete();
                    }
                    data2.AcceptChanges();

                    List<DataTable> result = data2.AsEnumerable()
                                                .GroupBy(row => row.Field<string>("DETAIL"))
                                                .Select(g => g.CopyToDataTable())
                                                .ToList();
                    string[] columnNames1 = data2.Columns.Cast<DataColumn>()
                                                .Select(column => column.ColumnName)
                                                .ToArray();

                    int maxTableRecords = 0;
                    for (int i = 0; i < result.Count; i++)
                    {

                        if (maxTableRecords < result[i].Rows.Count)
                        {
                            maxTableRecords = result[i].Rows.Count;
                        }
                    }
                    string htmlHeaderContent = "<tr>";

                    for (int r = 0; r < result.Count; r++)
                    {
                        DataTable req = result[r];
                        for (int j = 0; j < columnNames1.Length; j++)
                        {
                            htmlHeaderContent += "<td>" + columnNames1[j] + "</td>";
                        }

                    }
                    htmlHeaderContent += "</tr>";

                    string htmlBodyContent = "";

                    for (int k = 0; k < maxTableRecords; k++)
                    {
                        htmlBodyContent += "<tr>";
                        for (int tblCount = 0; tblCount < result.Count; tblCount++)
                        {
                            DataTable req1 = result[tblCount];

                            if (k < req1.Rows.Count)
                            {
                                DataRow dr = req1.Rows[k];
                                htmlBodyContent += "<td>" + dr["DETAIL"] + "</td>";
                                htmlBodyContent += "<td>" + dr["PAYMENTTYPE"] + "</td>";
                                htmlBodyContent += "<td>" + dr["INV#"] + "</td>";
                                htmlBodyContent += "<td>" + dr["PMT#"] + "</td>";
                                htmlBodyContent += "<td class='text-right'>" + dr["AMOUNT"] + "</td>";
                            }
                            else
                            {
                                htmlBodyContent += "<td></td><td></td><td></td><td></td><td></td>";
                            }

                        }
                        htmlBodyContent += "</tr>";


                    }
                    ViewBag.table2Headers = htmlHeaderContent;
                    ViewBag.table2Body = htmlBodyContent;
                }
                if (data1.Rows.Count > 0 && data2.Rows.Count > 0)
                {
                    ViewBag.showExcelButton = "1";
                }
            }
            ViewBag.Store = "8";
            if (Store != "")
            {
                var stores = _helperCommonService.GetAllStoreCodesList();
                int i = 0;
                int storeIndex = 0;
                foreach (var store in stores)
                {
                    if (store.Text == Store)
                    {
                        storeIndex = i;
                        break;
                    }
                    i++;
                }
                ViewBag.Store = storeIndex;
            }
            ViewBag.Store = 8;
            return View("~/Views/ListOfItemsSold/EODWorksheet.cshtml");
        }

        public string GetEODWorksheet(string FROMDATE, string DateTo, string Store)
        {
            ListOfItemsSoldModel objModel = new ListOfItemsSoldModel();

            var data = new Dictionary<string, DataTable>();

            var summary = _listOfItemsSoldService.EODSummary(FROMDATE, DateTo, Store);
            data.Add("summary", summary);
            var worksheet = _listOfItemsSoldService.EODWorkSheeetDetail(FROMDATE, DateTo, Store);

            DataTable sampleDataTable = new DataTable();
            sampleDataTable.Columns.Add("DETAIL", typeof(string));
            sampleDataTable.Columns.Add("PAYMENTTYPE", typeof(string));
            sampleDataTable.Columns.Add("INV", typeof(string));
            sampleDataTable.Columns.Add("PMT", typeof(string));
            sampleDataTable.Columns.Add("AMOUNT", typeof(string));
            //DataRow sampleDataRow;
            if (worksheet != null && worksheet.Rows.Count > 0)
            {
                for (int i = 0; i < worksheet.Rows.Count - 1; i++)
                {
                    sampleDataTable.Rows.Add(worksheet.Rows[i]["DETAIL"], worksheet.Rows[i]["PAYMENTTYPE"], worksheet.Rows[i]["INV#"], worksheet.Rows[i]["PMT#"], worksheet.Rows[i]["AMOUNT"]);

                }
            }
            data.Add("worksheet", sampleDataTable);
            return JsonConvert.SerializeObject(data);
        }

        public IActionResult ListOfStoreCredits()
        {
            ViewBag.CustomerCodes = _helperCommonService.GetAllCustomerCodes();
            ViewBag.FormTitle = "LIST OF  STORE  CREDITS";
            ViewBag.OpenOptionText = "Open Credits";
            ViewBag.FirstColumnLabel = "Store Credit#:";
            ViewBag.IsGiftCert = "0";
            //ViewData["IsGiftCert"] = false;,
            //string IsGiftCert = "0";
            return View("~/Views/ListOfItemsSold/ListOfStoreCredits.cshtml");
        }

        public string GetListOfStoreCredits(string FROMDATE, string DateTo, string Ccode, string StoreCredit, string OptionValue, bool IsGiftCert = false)
        {
            ListOfItemsSoldModel objModel = new ListOfItemsSoldModel();
            var data = _listOfItemsSoldService.GetAllSroreCredits(Ccode, FROMDATE, DateTo, OptionValue, IsGiftCert, StoreCredit);
            return JsonConvert.SerializeObject(data);
        }

        public IActionResult ListOfGiftCertificate()
        {
            ViewBag.CustomerCodes = _helperCommonService.GetAllCustomerCodes();
            ViewBag.FormTitle = "LIST OF GIFT CARDS";
            ViewBag.OpenOptionText = "Open Gift Cert";
            ViewBag.FirstColumnLabel = "Gift Cert#:";
            ViewBag.IsGiftCert = "1";
            return View("~/Views/ListOfItemsSold/ListOfStoreCredits.cshtml");
        }

        public IActionResult DayEndProcess()
        {

            ViewBag.ShowMainPanel = "0";
            ViewBag.TitleText = "Day End Process";
            DateTime? reportDate = null;
            reportDate = DateTime.Now;
            ViewBag.ReportDate = reportDate;
            DataTable byPaymnetType = _helperCommonService.ByPaymenttype(reportDate);
            return View("~/Views/ListOfItemsSold/DayEndProcess.cshtml");
        }

        public string GetDayEndProcess(int Txt100 = 0, int Txt50 = 0, int Txt20 = 0, int Txt10 = 0, int Txt5 = 0, int Txt1 = 0, int TxtCash = 0, int TxtTotal = 0, int TxtCredit = 0, int TxtChecks = 0, int TxtfinancedTotal = 0, int TxtOthers = 0, int TxtExpensePaidOut = 0, int TxtCashDrawer = 0, string TxtRemark = "")
        {

            DrawerDetails drawerDetails;
            List<DrawerDetails> lstOfSet = new List<DrawerDetails>();
            drawerDetails = new DrawerDetails();
            drawerDetails.Sent = "1s";
            drawerDetails.SentVal = _helperCommonService.DecimalCheckForDBNull(Txt1);
            lstOfSet.Add(drawerDetails);

            drawerDetails = new DrawerDetails();
            drawerDetails.Sent = "5s";
            drawerDetails.SentVal = _helperCommonService.DecimalCheckForDBNull(Txt5);
            lstOfSet.Add(drawerDetails);

            drawerDetails = new DrawerDetails();
            drawerDetails.Sent = "10s";
            drawerDetails.SentVal = _helperCommonService.DecimalCheckForDBNull(Txt10);
            lstOfSet.Add(drawerDetails);

            drawerDetails = new DrawerDetails();
            drawerDetails.Sent = "20s";
            drawerDetails.SentVal = _helperCommonService.DecimalCheckForDBNull(Txt20);
            lstOfSet.Add(drawerDetails);

            drawerDetails = new DrawerDetails();
            drawerDetails.Sent = "50s";
            drawerDetails.SentVal = _helperCommonService.DecimalCheckForDBNull(Txt50);
            lstOfSet.Add(drawerDetails);

            drawerDetails = new DrawerDetails();
            drawerDetails.Sent = "100s";
            drawerDetails.SentVal = _helperCommonService.DecimalCheckForDBNull(Txt100);
            lstOfSet.Add(drawerDetails);

            drawerDetails = new DrawerDetails();
            drawerDetails.Sent = "ExpencePaidOut";
            drawerDetails.SentVal = _helperCommonService.DecimalCheckForDBNull(TxtExpensePaidOut);
            lstOfSet.Add(drawerDetails);

            drawerDetails = new DrawerDetails();
            drawerDetails.Sent = "CashSale";
            drawerDetails.SentVal = _helperCommonService.DecimalCheckForDBNull(TxtCash);
            lstOfSet.Add(drawerDetails);

            drawerDetails = new DrawerDetails();
            drawerDetails.Sent = "CashDrawer";
            drawerDetails.SentVal = _helperCommonService.DecimalCheckForDBNull(TxtCashDrawer);
            lstOfSet.Add(drawerDetails);

            drawerDetails = new DrawerDetails();
            drawerDetails.Sent = "HCash";
            drawerDetails.SentVal = _helperCommonService.DecimalCheckForDBNull((_helperCommonService.DecimalCheckForDBNull(Txt100) * 100) + (_helperCommonService.DecimalCheckForDBNull(Txt50) * 50) +
            (_helperCommonService.DecimalCheckForDBNull(Txt20) * 20) + (_helperCommonService.DecimalCheckForDBNull(Txt10) * 10) +
            (_helperCommonService.DecimalCheckForDBNull(Txt5) * 5) + (_helperCommonService.DecimalCheckForDBNull(Txt1) * 1));
            lstOfSet.Add(drawerDetails);

            drawerDetails = new DrawerDetails();
            drawerDetails.Sent = "HCheck";
            drawerDetails.SentVal = _helperCommonService.DecimalCheckForDBNull(TxtChecks);
            lstOfSet.Add(drawerDetails);

            drawerDetails = new DrawerDetails();
            drawerDetails.Sent = "HFinanced";
            drawerDetails.SentVal = _helperCommonService.DecimalCheckForDBNull(TxtfinancedTotal);
            lstOfSet.Add(drawerDetails);

            drawerDetails = new DrawerDetails();
            drawerDetails.Sent = "HOthers";
            drawerDetails.SentVal = _helperCommonService.DecimalCheckForDBNull(TxtOthers);
            lstOfSet.Add(drawerDetails);

            DateTime? reportDate = DateTime.Now;

            DataTable dt = _helperCommonService.OpeningDrawerCash(_helperCommonService.StoreCodeInUse1, reportDate);
            drawerDetails = new DrawerDetails();
            drawerDetails.Sent = "OpenDrawerAmount";
            drawerDetails.SentVal = _helperCommonService.DecimalCheckForDBNull(_helperCommonService.DataTableOK(dt) ? dt.Rows[0]["amount"] : 0);
            lstOfSet.Add(drawerDetails);

            drawerDetails = new DrawerDetails();
            drawerDetails.Sent = "HCredit";
            drawerDetails.SentVal = _helperCommonService.DecimalCheckForDBNull(TxtCredit);
            lstOfSet.Add(drawerDetails);

            var data = _helperCommonService.AddUpdateOpeningDrawer(lstOfSet, _helperCommonService.StoreCodeInUse1, TxtRemark);
            return JsonConvert.SerializeObject(data);
        }

        public IActionResult OldPosReport()
        {
            ViewBag.ShowMainPanel = "1";
            ViewBag.TitleText = "Old PO'S Closing Report";
            return View("~/Views/ListOfItemsSold/DayEndProcess.cshtml");
        }

        public IActionResult PrintDayEndProcess(string PostDate = "")
        {
            decimal taxablesale = 0, notaxablesale = 0, taxableReturnSale = 0, notaxableReturnSale = 0, salestax = 0, salesnh = 0, salesRetuntax = 0, saleRetursnh = 0;

            DateTime? posDate = PostDate == "" ? DateTime.Now : Convert.ToDateTime(PostDate);
            DataTable byPaymnetType = _helperCommonService.ByPaymenttype(posDate);
            decimal? credit = byPaymnetType.AsEnumerable().OfType<DataRow>().Where(x =>
            Convert.ToString(x.Field<string>("paymenttype")).ToLower() == "master card" || Convert.ToString(x.Field<string>("paymenttype")).ToLower() == "debit card" ||
            Convert.ToString(x.Field<string>("paymenttype")).ToLower() == "visa card" || Convert.ToString(x.Field<string>("paymenttype")).ToLower() == "amex card" ||
            Convert.ToString(x.Field<string>("paymenttype")).ToLower() == "apple pay" || Convert.ToString(x.Field<string>("paymenttype")).ToLower() == "discover" ||
            Convert.ToString(x.Field<string>("paymenttype")).ToLower() == "outside usa card" || Convert.ToString(x.Field<string>("paymenttype")).ToLower() == "cc swipe"
            || Convert.ToString(x.Field<string>("paymenttype")).ToLower() == "cc terminal" || Convert.ToString(x.Field<string>("paymenttype")).ToLower() == "virtual cc terminal"
            || Convert.ToString(x.Field<string>("paymenttype")).ToLower() == "acima"
            ).Sum(x => x.Field<decimal?>("amount"));


            DataTable dtGetOldPosDetails = _helperCommonService.GetOpeningDrawer(posDate, _helperCommonService.StoreCodeInUse1);

            string hs = "";
            string fs = "";
            string tws = "";
            string ts = "";
            string fis = "";
            string os = "";

            if (_helperCommonService.DataTableOK(dtGetOldPosDetails))
            {
                ViewBag.txt1 = _helperCommonService.DecimalCheckForDBNull(dtGetOldPosDetails.Rows[0]["Cash1"]);
                os = _helperCommonService.DecimalCheckForDBNull(dtGetOldPosDetails.Rows[0]["Cash1"]).ToString();

                ViewBag.txt5 = _helperCommonService.DecimalCheckForDBNull(dtGetOldPosDetails.Rows[0]["Cash5"]);
                fis = _helperCommonService.DecimalCheckForDBNull(dtGetOldPosDetails.Rows[0]["Cash5"]).ToString();

                ViewBag.txt10 = _helperCommonService.DecimalCheckForDBNull(dtGetOldPosDetails.Rows[0]["Cash10"]);
                ts = _helperCommonService.DecimalCheckForDBNull(dtGetOldPosDetails.Rows[0]["Cash10"]).ToString();

                ViewBag.txt20 = _helperCommonService.DecimalCheckForDBNull(dtGetOldPosDetails.Rows[0]["Cash20"]);
                tws = _helperCommonService.DecimalCheckForDBNull(dtGetOldPosDetails.Rows[0]["Cash20"]).ToString();

                ViewBag.txt50 = _helperCommonService.DecimalCheckForDBNull(dtGetOldPosDetails.Rows[0]["Cash50"]);
                fs = _helperCommonService.DecimalCheckForDBNull(dtGetOldPosDetails.Rows[0]["Cash50"]).ToString();

                ViewBag.txt100 = _helperCommonService.DecimalCheckForDBNull(dtGetOldPosDetails.Rows[0]["Cash100"]);
                hs = _helperCommonService.DecimalCheckForDBNull(dtGetOldPosDetails.Rows[0]["Cash100"]).ToString();

                ViewBag.txtCredit = _helperCommonService.DecimalCheckForDBNull(dtGetOldPosDetails.Rows[0]["Credit"]);
                ViewBag.txtChecks = _helperCommonService.DecimalCheckForDBNull(dtGetOldPosDetails.Rows[0]["Checks"]);
                ViewBag.txtfinancedTotal = _helperCommonService.DecimalCheckForDBNull(dtGetOldPosDetails.Rows[0]["Financed"]);
                ViewBag.txtOthers = _helperCommonService.DecimalCheckForDBNull(dtGetOldPosDetails.Rows[0]["Others"]);
                ViewBag.txtExpensePaidOut = _helperCommonService.DecimalCheckForDBNull(dtGetOldPosDetails.Rows[0]["ExPaidOut"]);
                ViewBag.txtCashDrawer = _helperCommonService.DecimalCheckForDBNull(dtGetOldPosDetails.Rows[0]["amount"]);
                ViewBag.txtRemark = Convert.ToString(dtGetOldPosDetails.Rows[0]["Remark"]);
                //CashTotal();
            }
            else
            {
                ViewBag.txt1 = "0";
                ViewBag.txt5 = "0";
                ViewBag.txt10 = "0";
                ViewBag.txt20 = "0";
                ViewBag.txt50 = "0";
                ViewBag.txt100 = "0";
                ViewBag.txtCredit = "0.00";
                ViewBag.txtChecks = "0.00";
                ViewBag.txtfinancedTotal = "0.00";
                ViewBag.txtOthers = "0.00";
                ViewBag.txtExpensePaidOut = "0.00";
                ViewBag.txtCashDrawer = "0.00";
                ViewBag.txtRemark = "0.00";
            }

            string lblTodayDate = Convert.ToDateTime(posDate).ToString("dddd,MMMM dd, yyyy");
            ViewBag.todayDate = lblTodayDate;

            DataTable summaryBySaleType = _helperCommonService.SalesBySalesType(0, posDate);
            string summaryBySaleTypeHtml = "";
            string summaryBySaleTypeHtmlPrint = "";
            decimal totalAmount = 0;
            decimal totalQty = 0;
            if (summaryBySaleType.Rows.Count > 0)
            {
                foreach (DataRow dr in summaryBySaleType.Rows)
                {
                    summaryBySaleTypeHtml += "<tr><td>" + dr["SalesType"].ToString() + "</td><td class='text-right'>"
                                    + dr["NoOfSales"].ToString() + "</td><td class='text-right'>"
                                    + dr["Amount"].ToString() + "</td></tr>";

                    summaryBySaleTypeHtmlPrint += "<tr><td>" + dr["SalesType"].ToString() + "</td><td class='text-right'>"
                                    + dr["NoOfSales"].ToString() + "</td><td class='text-right'>"
                                    + dr["Amount"].ToString() + "</td></tr>";

                    if (dr["NoOfSales"] != null)
                    {
                        totalQty += Convert.ToDecimal(dr["NoOfSales"]);
                    }
                    if (dr["Amount"] != null)
                    {
                        totalAmount += Convert.ToDecimal(dr["Amount"]);
                    }
                }
                summaryBySaleTypeHtml += "<tr><td><b>Total</b></td><td class='text-right'><b>" + totalQty + "</b></td><td class='text-right'><b>" + totalAmount + "</b></td></tr>";
            }
            ViewBag.summaryBySaleType = summaryBySaleTypeHtml;

            DataTable bestsellerdetails = _helperCommonService.KingsSalesSummary(posDate);
            string bestSellerDetailsHtml = "";
            decimal totalAmount1 = 0;
            decimal totalCost = 0;
            decimal totalProfilt = 0;

            string bestSellerDetailsHtmllPrint = "";

            if (bestsellerdetails.Rows.Count > 0)
            {
                foreach (DataRow dr in bestsellerdetails.Rows)
                {
                    string ct = " ";
                    if (Convert.ToString(dr["Category"]) != "")
                    {
                        ct = dr["Category"].ToString();
                    }
                    bestSellerDetailsHtml += "<tr><td>" + ct + "</td><td>"
                        + Convert.ToString(dr["Metal"]) + "</td><td class='text-right'>"
                        + Convert.ToString(dr["Amount"]) + "</td><td class='text-right'>"
                        + Convert.ToString(dr["Cost"]) + "</td><td class='text-right'>"
                        + Convert.ToString(dr["Profit"]) + "</td></tr>";

                    bestSellerDetailsHtmllPrint += "<tr><td>"
                        + Convert.ToString(dr["Metal"]) + "</td><td class='text-right'>"
                        + Convert.ToString(dr["NoOfSales"]) + "</td><td class='text-right'>"
                        + Convert.ToString(dr["Amount"]) + "</td></tr>";

                    if (dr["Amount"] != null)
                    {
                        totalAmount1 += Convert.ToDecimal(dr["Amount"]);
                    }
                    if (dr["Cost"] != null)
                    {
                        totalCost += Convert.ToDecimal(dr["Cost"]);
                    }
                    if (dr["Profit"] != null)
                    {
                        totalProfilt += Convert.ToDecimal(dr["Profit"]);
                    }
                }
                bestSellerDetailsHtml += "<tr><td><b>Total</b></td><td></td><td class='text-right'><b>" + totalAmount1 + "</b></td><td class='text-right'><b>" + totalCost + "</b></td><td class='text-right'><b>" + totalProfilt + "</b></td></tr>";
            }
            ViewBag.bestSellerDetailsType = bestSellerDetailsHtml;

            string paymentTRansactionHtml = "";
            DataTable dtInvoiceItems = _helperCommonService.SalesBySalesType(1, posDate);
            decimal invoiceAmount = 0;
            if (dtInvoiceItems.Rows.Count > 0)
            {
                foreach (DataRow dr in dtInvoiceItems.Rows)
                {
                    if (dr["Amount"] != null)
                    {
                        invoiceAmount += Convert.ToDecimal(dr["Amount"]);
                    }
                    paymentTRansactionHtml += "<tr><td>" + dr["TransactionNo"].ToString() + "</td><td>"
                        + dr["InvoiceNo"].ToString() + "</td><td>"
                        + dr["SalesType"].ToString() + "</td><td  class='text-right'>"
                        + dr["Amount"].ToString() + "</td></tr>";
                }

                paymentTRansactionHtml += "<tr><td><b>Total</b></td><td></td><td></td><td class='text-right'><b>" + invoiceAmount + "</b></td></tr>";
            }

            ViewBag.paymentTRansactionType = paymentTRansactionHtml;

            string txtCharge = Convert.ToString($"${(_helperCommonService.DecimalCheckForDBNull(credit)):n}").PadLeft(8, ' ');
            ViewBag.txtCharge = txtCharge;
            DataTable data = _helperCommonService.GetSaleDetails(_helperCommonService.StoreCodeInUse1, posDate);
            ViewBag.lblTaxableSale = "$0.00";
            ViewBag.lblNontaxableSale = "$0.00";
            ViewBag.lblSubTotalSale = "$0.00";
            ViewBag.lblSalestaxSale = "$0.00";
            ViewBag.lblSNHSale = "$0.00";
            ViewBag.lblSalesTotalSale = "$0.00";

            ViewBag.lblTaxableSaleReturn = "$0.00";
            ViewBag.lblNonTaxableReturn = "$0.00";
            ViewBag.lblReturnSubTotal = "$0.00";
            ViewBag.lblSalesTaxReturn = "$0.00";
            ViewBag.lblShippingReturn = "$0.00";
            ViewBag.lblTotalSaleReturn = "$0.00";

            ViewBag.lblNetSubtotal = "$0.00";
            ViewBag.lblNetSalesTax = "$0.00";
            ViewBag.lblNetShippingTotal = "$0.00";
            ViewBag.lblNetTotalSale = "$0.00";

            if (_helperCommonService.DataTableOK(data))
            {
                taxablesale = data.AsEnumerable().OfType<DataRow>().Where(x => _helperCommonService.CheckForDBNull(x.Field<bool?>("TAXABLE"), typeof(bool)) && _helperCommonService.DecimalCheckForDBNull(x.Field<decimal?>("GR_TOTAL")) > 0).Sum(x => _helperCommonService.DecimalCheckForDBNull(x.Field<decimal?>("GR_TOTAL")) - _helperCommonService.DecimalCheckForDBNull(x.Field<decimal?>("SALES_TAX")) - _helperCommonService.DecimalCheckForDBNull(x.Field<decimal?>("SNH")));
                ViewBag.lblTaxableSale = $"${taxablesale:n}".PadLeft(8, ' ');

                notaxablesale = data.AsEnumerable().OfType<DataRow>().Where(x => !_helperCommonService.CheckForDBNull(x.Field<bool?>("TAXABLE"), typeof(bool)) && _helperCommonService.DecimalCheckForDBNull(x.Field<decimal?>("GR_TOTAL")) > 0).Sum(x => _helperCommonService.DecimalCheckForDBNull(x.Field<decimal?>("GR_TOTAL")) - _helperCommonService.DecimalCheckForDBNull(x.Field<decimal?>("SALES_TAX")) - _helperCommonService.DecimalCheckForDBNull(x.Field<decimal?>("SNH")));
                ViewBag.lblNontaxableSale = $"${notaxablesale:n}".PadLeft(8, ' ');
                ViewBag.lblSubTotalSale = $"${(taxablesale + notaxablesale):n}".PadLeft(8, ' ');

                salestax = data.AsEnumerable().OfType<DataRow>().Where(x => _helperCommonService.DecimalCheckForDBNull(x.Field<decimal?>("GR_TOTAL")) > 0).Sum(x => _helperCommonService.DecimalCheckForDBNull(x.Field<decimal?>("SALES_TAX")));
                ViewBag.lblSalestaxSale = Convert.ToString($"${salestax:n}").PadLeft(8, ' ');

                salesnh = data.AsEnumerable().OfType<DataRow>().Where(x => _helperCommonService.DecimalCheckForDBNull(x.Field<decimal?>("GR_TOTAL")) > 0).Sum(x => _helperCommonService.DecimalCheckForDBNull(x.Field<decimal?>("SNH")));
                ViewBag.lblSNHSale = Convert.ToString($"${salesnh:n}").PadLeft(8, ' ');

                ViewBag.lblSalesTotalSale = $"${(taxablesale + notaxablesale + salestax + salesnh):n}".PadLeft(8, ' ');

                /*===========================Return==============================*/
                taxableReturnSale = data.AsEnumerable().OfType<DataRow>().Where(x => _helperCommonService.CheckForDBNull(x.Field<bool?>("TAXABLE"), typeof(bool)) && _helperCommonService.DecimalCheckForDBNull(x.Field<decimal?>("GR_TOTAL")) < 0).Sum(x => _helperCommonService.DecimalCheckForDBNull(x.Field<decimal?>("GR_TOTAL")) - _helperCommonService.DecimalCheckForDBNull(x.Field<decimal?>("SALES_TAX")) - _helperCommonService.DecimalCheckForDBNull(x.Field<decimal?>("SNH")));
                ViewBag.lblTaxableSaleReturn = $"(${taxableReturnSale * -1:n})".PadLeft(8, ' ');

                notaxableReturnSale = data.AsEnumerable().OfType<DataRow>().Where(x => !_helperCommonService.CheckForDBNull(x.Field<bool?>("TAXABLE"), typeof(bool)) && _helperCommonService.DecimalCheckForDBNull(x.Field<decimal?>("GR_TOTAL")) < 0).Sum(x => _helperCommonService.DecimalCheckForDBNull(x.Field<decimal?>("GR_TOTAL")) - _helperCommonService.DecimalCheckForDBNull(x.Field<decimal?>("SALES_TAX")) - _helperCommonService.DecimalCheckForDBNull(x.Field<decimal?>("SNH")));
                ViewBag.lblNonTaxableReturn = $"(${notaxableReturnSale * -1:n})".PadLeft(8, ' ');

                ViewBag.lblReturnSubTotal = $"(${(taxableReturnSale + notaxableReturnSale) * -1:n})".PadLeft(8, ' ');

                salesRetuntax = data.AsEnumerable().OfType<DataRow>().Where(x => _helperCommonService.DecimalCheckForDBNull(x.Field<decimal?>("GR_TOTAL")) < 0).Sum(x => _helperCommonService.DecimalCheckForDBNull(x.Field<decimal?>("SALES_TAX")));
                ViewBag.lblSalesTaxReturn = Convert.ToString($"(${salesRetuntax * -1:n})").PadLeft(8, ' ');

                saleRetursnh = data.AsEnumerable().OfType<DataRow>().Where(x => _helperCommonService.DecimalCheckForDBNull(x.Field<decimal?>("GR_TOTAL")) < 0).Sum(x => _helperCommonService.DecimalCheckForDBNull(x.Field<decimal?>("SNH")));
                ViewBag.lblShippingReturn = Convert.ToString($"(${saleRetursnh * -1:n})").PadLeft(8, ' ');

                ViewBag.lblTotalSaleReturn = $"(${(taxableReturnSale + notaxableReturnSale + salesRetuntax + saleRetursnh) * -1:n})".PadLeft(8, ' ');

                /*==========================Net Total==================================*/
                ViewBag.lblNetSubtotal = $"${(taxablesale + notaxablesale) + (taxableReturnSale + notaxableReturnSale):n}".PadLeft(8, ' ');
                ViewBag.lblNetSalesTax = Convert.ToString($"${(salestax + salesRetuntax):n}").PadLeft(8, ' ');
                ViewBag.lblNetShippingTotal = Convert.ToString($"${(salesnh + saleRetursnh):n}").PadLeft(8, ' ');
                ViewBag.lblNetTotalSale = ViewBag.lblSalesNetAmount = $"${(taxablesale + notaxablesale + salestax + salesnh) + (taxableReturnSale + notaxableReturnSale + salesRetuntax + saleRetursnh):n}".PadLeft(8, ' ');

            }

            DataSet dsZReportLbl = _helperCommonService.ZReportingLabel(posDate);
            decimal receiveableAmt = 0, amountDue = 0;
            string paidOut = "";
            string StoreCreditOut = "";
            string Check = "";
            string GiftCardIn = "";
            if (dsZReportLbl != null)
            {
                amountDue = (_helperCommonService.DataTableOK(dsZReportLbl.Tables[0]) ? _helperCommonService.DecimalCheckForDBNull(dsZReportLbl.Tables[0].Rows[0]["amount"]) : 0);
                paidOut = $"({Convert.ToString($"${(_helperCommonService.DataTableOK(dsZReportLbl.Tables[2]) ? dsZReportLbl.Tables[2].Rows[0]["amount"] : "0.00"):n}")})".PadLeft(8, ' ');
                ViewBag.txtAmountDue = Convert.ToString($"${amountDue:n}").PadLeft(8, ' ');
                receiveableAmt = (_helperCommonService.DataTableOK(dsZReportLbl.Tables[1]) ? _helperCommonService.DecimalCheckForDBNull(dsZReportLbl.Tables[1].Rows[0]["amount"]) : 0);
                ViewBag.txtReceiveableAmt = Convert.ToString($"${receiveableAmt:n}").PadLeft(8, ' ');
                ViewBag.txtPaidOut = $"({Convert.ToString($"${(_helperCommonService.DataTableOK(dsZReportLbl.Tables[2]) ? dsZReportLbl.Tables[2].Rows[0]["amount"] : "0.00"):n}")})".PadLeft(8, ' ');
                ViewBag.txtGiftCardIn = Convert.ToString($"${(_helperCommonService.DataTableOK(dsZReportLbl.Tables[3]) ? dsZReportLbl.Tables[3].Rows[0]["amount"] : "00"):n}").PadLeft(8, ' ');
                GiftCardIn = Convert.ToString($"${(_helperCommonService.DataTableOK(dsZReportLbl.Tables[3]) ? dsZReportLbl.Tables[3].Rows[0]["amount"] : "00"):n}").PadLeft(8, ' ');
                ViewBag.txtStoreCreditOut = Convert.ToString($"(${(_helperCommonService.DataTableOK(dsZReportLbl.Tables[4]) ? dsZReportLbl.Tables[4].Rows[0]["amount"] : "00"):n})").PadLeft(8, ' ');
                StoreCreditOut = Convert.ToString($"(${(_helperCommonService.DataTableOK(dsZReportLbl.Tables[4]) ? dsZReportLbl.Tables[4].Rows[0]["amount"] : "00"):n})").PadLeft(8, ' ');
            }

            DrawerDetails drawerDetails;
            List<DrawerDetails> lstOfSet = new List<DrawerDetails>();
            drawerDetails = new DrawerDetails();
            drawerDetails.Sent = "1s";
            drawerDetails.SentVal = _helperCommonService.DecimalCheckForDBNull(ViewBag.txt1);
            lstOfSet.Add(drawerDetails);

            drawerDetails = new DrawerDetails();
            drawerDetails.Sent = "5s";
            drawerDetails.SentVal = _helperCommonService.DecimalCheckForDBNull(ViewBag.txt5);
            lstOfSet.Add(drawerDetails);

            drawerDetails = new DrawerDetails();
            drawerDetails.Sent = "10s";
            drawerDetails.SentVal = _helperCommonService.DecimalCheckForDBNull(ViewBag.txt10);
            lstOfSet.Add(drawerDetails);

            drawerDetails = new DrawerDetails();
            drawerDetails.Sent = "20s";
            drawerDetails.SentVal = _helperCommonService.DecimalCheckForDBNull(ViewBag.txt20);
            lstOfSet.Add(drawerDetails);

            drawerDetails = new DrawerDetails();
            drawerDetails.Sent = "50s";
            drawerDetails.SentVal = _helperCommonService.DecimalCheckForDBNull(ViewBag.txt50);
            lstOfSet.Add(drawerDetails);

            drawerDetails = new DrawerDetails();
            drawerDetails.Sent = "100s";
            drawerDetails.SentVal = _helperCommonService.DecimalCheckForDBNull(ViewBag.txt100);
            lstOfSet.Add(drawerDetails);

            drawerDetails = new DrawerDetails();
            drawerDetails.Sent = "ExpencePaidOut";
            drawerDetails.SentVal = _helperCommonService.DecimalCheckForDBNull(ViewBag.txtExpensePaidOut);
            lstOfSet.Add(drawerDetails);

            drawerDetails = new DrawerDetails();
            drawerDetails.Sent = "CashSale";
            drawerDetails.SentVal = _helperCommonService.DecimalCheckForDBNull(ViewBag.txtCash);
            lstOfSet.Add(drawerDetails);

            drawerDetails = new DrawerDetails();
            drawerDetails.Sent = "CashDrawer";
            drawerDetails.SentVal = _helperCommonService.DecimalCheckForDBNull(ViewBag.txtCashDrawer);
            lstOfSet.Add(drawerDetails);

            drawerDetails = new DrawerDetails();
            drawerDetails.Sent = "HCash";
            drawerDetails.SentVal = _helperCommonService.DecimalCheckForDBNull((_helperCommonService.DecimalCheckForDBNull(ViewBag.txt100) * 100) + (_helperCommonService.DecimalCheckForDBNull(ViewBag.txt50) * 50) +
            (_helperCommonService.DecimalCheckForDBNull(ViewBag.txt20) * 20) + (_helperCommonService.DecimalCheckForDBNull(ViewBag.txt10) * 10) +
            (_helperCommonService.DecimalCheckForDBNull(ViewBag.txt5) * 5) + (_helperCommonService.DecimalCheckForDBNull(ViewBag.txt1) * 1));
            lstOfSet.Add(drawerDetails);

            drawerDetails = new DrawerDetails();
            drawerDetails.Sent = "HCheck";
            drawerDetails.SentVal = _helperCommonService.DecimalCheckForDBNull(ViewBag.txtChecks);
            lstOfSet.Add(drawerDetails);

            drawerDetails = new DrawerDetails();
            drawerDetails.Sent = "HFinanced";
            drawerDetails.SentVal = _helperCommonService.DecimalCheckForDBNull(ViewBag.txtfinancedTotal);
            lstOfSet.Add(drawerDetails);

            drawerDetails = new DrawerDetails();
            drawerDetails.Sent = "HOthers";
            drawerDetails.SentVal = _helperCommonService.DecimalCheckForDBNull(ViewBag.txtOthers);
            lstOfSet.Add(drawerDetails);

            DateTime? reportDate = DateTime.Now;

            DataTable dt = _helperCommonService.OpeningDrawerCash(_helperCommonService.StoreCodeInUse1, reportDate);
            drawerDetails = new DrawerDetails();
            drawerDetails.Sent = "OpenDrawerAmount";
            drawerDetails.SentVal = _helperCommonService.DecimalCheckForDBNull(_helperCommonService.DataTableOK(dt) ? dt.Rows[0]["amount"] : 0);
            lstOfSet.Add(drawerDetails);

            drawerDetails = new DrawerDetails();
            drawerDetails.Sent = "HCredit";
            drawerDetails.SentVal = _helperCommonService.DecimalCheckForDBNull(ViewBag.txtCredit);
            lstOfSet.Add(drawerDetails);

            decimal? cash = byPaymnetType.AsEnumerable().OfType<DataRow>().Where(x => Convert.ToString(x.Field<string>("paymenttype")).ToLower() == "cash").Sum(x => x.Field<decimal?>("amount"));
            decimal? check = byPaymnetType.AsEnumerable().OfType<DataRow>().Where(x =>
            Convert.ToString(x.Field<string>("paymenttype")).ToLower() == "check" || Convert.ToString(x.Field<string>("paymenttype")).ToLower() == "zellepay" ||
            Convert.ToString(x.Field<string>("paymenttype")).ToLower() == "cashapp" || Convert.ToString(x.Field<string>("paymenttype")).ToLower() == "wire" || Convert.ToString(x.Field<string>("paymenttype")).ToLower() == "paypal").Sum(x => x.Field<decimal?>("amount"));
            ViewBag.txtCheck = Convert.ToString($"${(check):n}").PadLeft(8, ' ');
            Check = Convert.ToString($"${(check):n}").PadLeft(8, ' ');

            decimal? financed = byPaymnetType.AsEnumerable().OfType<DataRow>().Where(x =>
            Convert.ToString(x.Field<string>("paymenttype")).ToLower() == "synchrony" || Convert.ToString(x.Field<string>("paymenttype")).ToLower() == "snap"
            || Convert.ToString(x.Field<string>("paymenttype")).ToLower() == "klarna" || Convert.ToString(x.Field<string>("paymenttype")).ToLower() == "afterpay"
            || Convert.ToString(x.Field<string>("paymenttype")).ToLower() == "terrace finance" || Convert.ToString(x.Field<string>("paymenttype")).ToLower() == "finance charge ter").Sum(x => x.Field<decimal?>("amount"));
            ViewBag.txtFinanced = Convert.ToString($"${(financed):n}").PadLeft(8, ' ');

            decimal? other = byPaymnetType.AsEnumerable().OfType<DataRow>().Where(x =>
            Convert.ToString(x.Field<string>("paymenttype")).ToLower() == "house account" || Convert.ToString(x.Field<string>("paymenttype")).ToLower() == "other"
            || Convert.ToString(x.Field<string>("paymenttype")).ToLower() == "on account (pay later)").Sum(x => x.Field<decimal?>("amount"));
            ViewBag.txtOther = Convert.ToString($"${(other):n}").PadLeft(8, ' ');


            string netdayendcashstatus = "", checkStatus = "", chargesStatus = "", financeStatus = "", otherStatus = "";
            string NetDayEndCash = "";

            decimal netdayamount = _helperCommonService.DecimalCheckForDBNull(lstOfSet.Find(i => i.Sent == "OpenDrawerAmount").SentVal) + (_helperCommonService.DecimalCheckForDBNull(cash) - (lstOfSet.Find(i => i.Sent == "ExpencePaidOut").SentVal)) - (lstOfSet.Find(i => i.Sent == "CashDrawer").SentVal);
            ViewBag.txtNetDayEndCash = netdayamount >= 0 ? Convert.ToString($"${netdayamount:n}").PadLeft(8, ' ') : Convert.ToString($"(${netdayamount * -1:n})").PadLeft(8, ' ');
            NetDayEndCash = netdayamount >= 0 ? Convert.ToString($"${netdayamount:n}").PadLeft(8, ' ') : Convert.ToString($"(${netdayamount * -1:n})").PadLeft(8, ' ');
            ViewBag.lblCS = _helperCommonService.DecimalCheckForDBNull(lstOfSet.Find(i => i.Sent == "HCash").SentVal) == _helperCommonService.DecimalCheckForDBNull(netdayamount) ? "" : _helperCommonService.DecimalCheckForDBNull(lstOfSet.Find(i => i.Sent == "HCash").SentVal) < _helperCommonService.DecimalCheckForDBNull(netdayamount) ? "SHORT" : "OVER";
            netdayendcashstatus = _helperCommonService.DecimalCheckForDBNull(lstOfSet.Find(i => i.Sent == "HCash").SentVal) == _helperCommonService.DecimalCheckForDBNull(cash) ? $"VARIANCE      $0.00      OK" : _helperCommonService.DecimalCheckForDBNull(lstOfSet.Find(i => i.Sent == "HCash").SentVal) < _helperCommonService.DecimalCheckForDBNull(cash) ? $"VARIANCE      (${_helperCommonService.DecimalCheckForDBNull(cash) - _helperCommonService.DecimalCheckForDBNull(lstOfSet.Find(i => i.Sent == "HCash").SentVal)})      SHORT" : $"VARIANCE      ${_helperCommonService.DecimalCheckForDBNull(lstOfSet.Find(i => i.Sent == "HCash").SentVal) - _helperCommonService.DecimalCheckForDBNull(cash)}      OVER";

            ViewBag.lblCC = _helperCommonService.DecimalCheckForDBNull(lstOfSet.Find(i => i.Sent == "HCredit").SentVal) == _helperCommonService.DecimalCheckForDBNull(credit) ? "" : _helperCommonService.DecimalCheckForDBNull(lstOfSet.Find(i => i.Sent == "HCredit").SentVal) < _helperCommonService.DecimalCheckForDBNull(credit) ? "SHORT" : "OVER";
            chargesStatus = _helperCommonService.DecimalCheckForDBNull(lstOfSet.Find(i => i.Sent == "HCredit").SentVal) == _helperCommonService.DecimalCheckForDBNull(credit) ? $"VARIANCE      $0.00      OK" : _helperCommonService.DecimalCheckForDBNull(lstOfSet.Find(i => i.Sent == "HCredit").SentVal) < _helperCommonService.DecimalCheckForDBNull(credit) ? $"VARIANCE      (${_helperCommonService.DecimalCheckForDBNull(credit) - _helperCommonService.DecimalCheckForDBNull(lstOfSet.Find(i => i.Sent == "HCredit").SentVal)})      SHORT" : $"VARIANCE      ${_helperCommonService.DecimalCheckForDBNull(lstOfSet.Find(i => i.Sent == "HCredit").SentVal) - _helperCommonService.DecimalCheckForDBNull(credit)}      OVER";

            ViewBag.lblCHS = _helperCommonService.DecimalCheckForDBNull(lstOfSet.Find(i => i.Sent == "HCheck").SentVal) == 0 && _helperCommonService.DecimalCheckForDBNull(check) == 0 ? "" : _helperCommonService.DecimalCheckForDBNull(lstOfSet.Find(i => i.Sent == "HCheck").SentVal) < _helperCommonService.DecimalCheckForDBNull(check) ? "SHORT" : "OVER";
            checkStatus = _helperCommonService.DecimalCheckForDBNull(lstOfSet.Find(i => i.Sent == "HCheck").SentVal) == _helperCommonService.DecimalCheckForDBNull(check) ? $"VARIANCE      $0.00      OK" : _helperCommonService.DecimalCheckForDBNull(lstOfSet.Find(i => i.Sent == "HCheck").SentVal) < _helperCommonService.DecimalCheckForDBNull(check) ? $"VARIANCE      (${_helperCommonService.DecimalCheckForDBNull(check) - _helperCommonService.DecimalCheckForDBNull(lstOfSet.Find(i => i.Sent == "HCheck").SentVal)})      SHORT" : $"VARIANCE      ${_helperCommonService.DecimalCheckForDBNull(lstOfSet.Find(i => i.Sent == "HCheck").SentVal) - _helperCommonService.DecimalCheckForDBNull(check)}      OVER";

            ViewBag.lblStatusOfFinanced = _helperCommonService.DecimalCheckForDBNull(lstOfSet.Find(i => i.Sent == "HFinanced").SentVal) == _helperCommonService.DecimalCheckForDBNull(financed) ? "" : _helperCommonService.DecimalCheckForDBNull(lstOfSet.Find(i => i.Sent == "HFinanced").SentVal) < _helperCommonService.DecimalCheckForDBNull(financed) ? "SHORT" : "OVER";
            financeStatus = _helperCommonService.DecimalCheckForDBNull(lstOfSet.Find(i => i.Sent == "HFinanced").SentVal) == _helperCommonService.DecimalCheckForDBNull(financed) ? $"VARIANCE      $0.00      OK" : _helperCommonService.DecimalCheckForDBNull(lstOfSet.Find(i => i.Sent == "HFinanced").SentVal) < _helperCommonService.DecimalCheckForDBNull(financed) ? $"VARIANCE      (${_helperCommonService.DecimalCheckForDBNull(financed) - _helperCommonService.DecimalCheckForDBNull(lstOfSet.Find(i => i.Sent == "HFinanced").SentVal)})      SHORT" : $"VARIANCE      ${_helperCommonService.DecimalCheckForDBNull(lstOfSet.Find(i => i.Sent == "HFinanced").SentVal) - _helperCommonService.DecimalCheckForDBNull(financed)}      OVER";

            ViewBag.txtStatusOfOthers = _helperCommonService.DecimalCheckForDBNull(lstOfSet.Find(i => i.Sent == "HOthers").SentVal) == 0 && _helperCommonService.DecimalCheckForDBNull(other) == 0 ? "" : _helperCommonService.DecimalCheckForDBNull(lstOfSet.Find(i => i.Sent == "HOthers").SentVal) < _helperCommonService.DecimalCheckForDBNull(other) ? "SHORT" : "OVER";
            otherStatus = _helperCommonService.DecimalCheckForDBNull(lstOfSet.Find(i => i.Sent == "HOthers").SentVal) == _helperCommonService.DecimalCheckForDBNull(other) ? $"VARIANCE      $0.00      OK" : _helperCommonService.DecimalCheckForDBNull(lstOfSet.Find(i => i.Sent == "HOthers").SentVal) < _helperCommonService.DecimalCheckForDBNull(other) ? $"VARIANCE      (${_helperCommonService.DecimalCheckForDBNull(other) - _helperCommonService.DecimalCheckForDBNull(lstOfSet.Find(i => i.Sent == "HOthers").SentVal)})      SHORT" : $"VARIANCE      ${_helperCommonService.DecimalCheckForDBNull(lstOfSet.Find(i => i.Sent == "HOthers").SentVal) - _helperCommonService.DecimalCheckForDBNull(other)}      OVER";

            decimal? sysm = (netdayamount + credit + check + financed + other), hcount = (_helperCommonService.DecimalCheckForDBNull(lstOfSet.Find(i => i.Sent == "HCash").SentVal) + _helperCommonService.DecimalCheckForDBNull(lstOfSet.Find(i => i.Sent == "HCredit").SentVal) + _helperCommonService.DecimalCheckForDBNull(lstOfSet.Find(i => i.Sent == "HCheck").SentVal) + _helperCommonService.DecimalCheckForDBNull(lstOfSet.Find(i => i.Sent == "HFinanced").SentVal) + _helperCommonService.DecimalCheckForDBNull(lstOfSet.Find(i => i.Sent == "HOthers").SentVal));
            //ViewBag.txtNetDayVaiance = "";
            if (sysm != null && hcount != null)
            {
                // ViewBag.txtNetDayVaiance = (hcount - sysm) >= 0 ? Convert.ToString($"${(hcount - sysm):n}").PadLeft(8, ' ') : Convert.ToString($"${(sysm - hcount):n}").PadLeft(8, ' ');
                // ViewBag.lblNetDayVar.Text = sysm == hcount ? "" : _helperCommonService.DecimalCheckForDBNull(sysm) < _helperCommonService.DecimalCheckForDBNull(hcount) ? "OVER" : "SHORT";
                // ViewBag.netdayvariance = _helperCommonService.DecimalCheckForDBNull(sysm) == _helperCommonService.DecimalCheckForDBNull(hcount) ? $"VARIANCE      $0.00      OK" : _helperCommonService.DecimalCheckForDBNull(sysm) < _helperCommonService.DecimalCheckForDBNull(hcount) ? $"VARIANCE      ${_helperCommonService.DecimalCheckForDBNull(hcount) - _helperCommonService.DecimalCheckForDBNull(sysm)}      OVER" : $"VARIANCE      (${_helperCommonService.DecimalCheckForDBNull(sysm) - _helperCommonService.DecimalCheckForDBNull(hcount)})      SHORT";

            }

            string NetDayAmount = "";
            string NetOfItemSold = "";
            NetDayAmount = (netdayamount + credit + check + financed + other) < 0 ? Convert.ToString($"(${(netdayamount + credit + check + financed + other) * -1:n})").PadLeft(8, ' ') : Convert.ToString($"${(netdayamount + credit + check + financed + other):n}").PadLeft(8, ' ');
            string NtOfSale = $"NET # OF SALES ${_helperCommonService.DecimalCheckForDBNull(summaryBySaleType.Compute("SUM(AMOUNT)", String.Empty))}";
            ViewBag.txtNetDayAmount = (netdayamount + credit + check + financed + other) < 0 ? Convert.ToString($"(${(netdayamount + credit + check + financed + other) * -1:n})").PadLeft(8, ' ') : Convert.ToString($"${(netdayamount + credit + check + financed + other):n}").PadLeft(8, ' ');
            ViewBag.txtPaidIn = Convert.ToString($"${_helperCommonService.DecimalCheckForDBNull(dtInvoiceItems.Compute("SUM(Amount)", string.Empty))}").PadLeft(8, ' '); //Convert.ToString($"${(taxablesale + notaxablesale + salestax + salesnh) + (taxableReturnSale + notaxableReturnSale + salesRetuntax + saleRetursnh) + receiveableAmt - amountDue:n}").PadLeft(8, ' ');
            ViewBag.lblNetOfSale = $"NET # OF SALES ${_helperCommonService.DecimalCheckForDBNull(summaryBySaleType.Compute("SUM(AMOUNT)", String.Empty))}";
            ViewBag.lblNetOfItemSold = $"NET # OF ITEM SOLD {_helperCommonService.DecimalCheckForDBNull(summaryBySaleType.Compute("SUM(NoOfSales)", String.Empty))}";
            NetOfItemSold = $"NET # OF ITEM SOLD {_helperCommonService.DecimalCheckForDBNull(summaryBySaleType.Compute("SUM(NoOfSales)", String.Empty))}";
            string txtPaidIn1 = Convert.ToString($"${_helperCommonService.DecimalCheckForDBNull(dtInvoiceItems.Compute("SUM(Amount)", string.Empty))}").PadLeft(8, ' ');
            string OpeninDrawerCashAmount = "";
            string CashSale = "";
            string TotalDayEndCash = "";
            string ClosingDrawerAmount = "";
            string HCash = "";
            string Charges = "";
            string HCheck = "";
            string PayOutCash = "";
            string HCountTotal = "";
            if (lstOfSet != null)
            {
                var a = lstOfSet.Find(i => i.Sent == "1s").SentVal;
                OpeninDrawerCashAmount = Convert.ToString($"${(lstOfSet.Find(i => i.Sent == "OpenDrawerAmount").SentVal):n}").PadLeft(8, ' ');
                ViewBag.txtPayOutCash = Convert.ToString($"${(lstOfSet.Find(i => i.Sent == "ExpencePaidOut").SentVal):n}").PadLeft(8, ' ');
                PayOutCash = Convert.ToString($"${(lstOfSet.Find(i => i.Sent == "ExpencePaidOut").SentVal):n}").PadLeft(8, ' ');
                ViewBag.txtCashSale = Convert.ToString($"${cash:n}").PadLeft(8, ' ');
                CashSale = Convert.ToString($"${cash:n}").PadLeft(8, ' ');
                ClosingDrawerAmount = ViewBag.lblAtClosing = Convert.ToString($"${(lstOfSet.Find(i => i.Sent == "CashDrawer").SentVal):n}").PadLeft(8, ' ');
                ViewBag.txtClosingDrawerAmount = ViewBag.lblAtClosing = Convert.ToString($"${(lstOfSet.Find(i => i.Sent == "CashDrawer").SentVal):n}").PadLeft(8, ' ');
                ViewBag.txtOpeninDrawerCashAmount = Convert.ToString($"${(lstOfSet.Find(i => i.Sent == "OpenDrawerAmount").SentVal):n}").PadLeft(8, ' ');

                ViewBag.txtTotalDayEndCash = Convert.ToString($"${_helperCommonService.DecimalCheckForDBNull(lstOfSet.Find(i => i.Sent == "OpenDrawerAmount").SentVal) + (_helperCommonService.DecimalCheckForDBNull(cash) - (lstOfSet.Find(i => i.Sent == "ExpencePaidOut").SentVal)):n}").PadLeft(8, ' ');
                TotalDayEndCash = Convert.ToString($"${_helperCommonService.DecimalCheckForDBNull(lstOfSet.Find(i => i.Sent == "OpenDrawerAmount").SentVal) + (_helperCommonService.DecimalCheckForDBNull(cash) - (lstOfSet.Find(i => i.Sent == "ExpencePaidOut").SentVal)):n}").PadLeft(8, ' ');

                ViewBag.txtHCash = Convert.ToString($"${(lstOfSet.Find(i => i.Sent == "HCash").SentVal):n}").PadLeft(8, ' ');
                HCash = Convert.ToString($"${(lstOfSet.Find(i => i.Sent == "HCash").SentVal):n}").PadLeft(8, ' ');
                ViewBag.txtCharges = Convert.ToString($"${(lstOfSet.Find(i => i.Sent == "HCredit").SentVal):n}").PadLeft(8, ' ');
                Charges = Convert.ToString($"${(lstOfSet.Find(i => i.Sent == "HCredit").SentVal):n}").PadLeft(8, ' ');

                ViewBag.txtHCheck = Convert.ToString($"${(lstOfSet.Find(i => i.Sent == "HCheck").SentVal):n}").PadLeft(8, ' ');
                HCheck = Convert.ToString($"${(lstOfSet.Find(i => i.Sent == "HCheck").SentVal):n}").PadLeft(8, ' ');
                ViewBag.HCFinanced = Convert.ToString($"${(lstOfSet.Find(i => i.Sent == "HFinanced").SentVal):n}").PadLeft(8, ' ');
                ViewBag.HCOther = Convert.ToString($"${(lstOfSet.Find(i => i.Sent == "HOthers").SentVal):n}").PadLeft(8, ' ');

                ViewBag.txtHCountTotal = ViewBag.txtFinalNetDayAmount = Convert.ToString($"${(_helperCommonService.DecimalCheckForDBNull(lstOfSet.Find(i => i.Sent == "HCash").SentVal) + _helperCommonService.DecimalCheckForDBNull(lstOfSet.Find(i => i.Sent == "HCheck").SentVal) + _helperCommonService.DecimalCheckForDBNull(lstOfSet.Find(i => i.Sent == "HCredit").SentVal) + _helperCommonService.DecimalCheckForDBNull(lstOfSet.Find(i => i.Sent == "HFinanced").SentVal) + _helperCommonService.DecimalCheckForDBNull(lstOfSet.Find(i => i.Sent == "HOthers").SentVal)):n}").PadLeft(8, ' ');
                HCountTotal = Convert.ToString($"${(_helperCommonService.DecimalCheckForDBNull(lstOfSet.Find(i => i.Sent == "HCash").SentVal) + _helperCommonService.DecimalCheckForDBNull(lstOfSet.Find(i => i.Sent == "HCheck").SentVal) + _helperCommonService.DecimalCheckForDBNull(lstOfSet.Find(i => i.Sent == "HCredit").SentVal) + _helperCommonService.DecimalCheckForDBNull(lstOfSet.Find(i => i.Sent == "HFinanced").SentVal) + _helperCommonService.DecimalCheckForDBNull(lstOfSet.Find(i => i.Sent == "HOthers").SentVal)):n}").PadLeft(8, ' ');
            }

            string printText = "";
            /*if (summaryBySaleTypeHtmlPrint != "" && summaryBySaleTypeHtmlPrint != null)
            {*/
            printText += "<table width='450px'><tr><td style='text-align:center;'><b>SALES SUMMARY BY SALES TYPE</b></td></tr></table>";
            printText += "<table width='450px'>";
            printText += "<tr><td width='150px'><b>SALE TYPE</b></td><td width='150px'><b>#OF SALES</b></td><td width='150px'><b>AMOUNT</b></td></tr>";
            printText += summaryBySaleTypeHtmlPrint;
            printText += "</table>";
            //}

            /*if (bestSellerDetailsHtmllPrint != "" && bestSellerDetailsHtmllPrint != null)
            {*/
            printText += "<table width='450px'><tr><td style='text-align:center;'><b>SALES SUMMARY BY DEPARTMENT</b></td></tr></table>";
            printText += "<table>";
            printText += "<tr><td width='150px'><b>DEPARTMENT</b></td><td width='150px'><b>#OF SALES</b></td><td width='150px'><b>AMOUNT</b></td></tr>";
            printText += bestSellerDetailsHtmllPrint;
            printText += "</table>";
            //}

            /* if (bestSellerDetailsHtmllPrint != "" && bestSellerDetailsHtmllPrint != null)
             {*/
            printText += "<table width='450px'>";
            printText += "<tr><td width='110px'></td><td width='110px'><b>SALES</b></td><td width='110px'><b>RETURN</b></td><td width='110px'><b>NET</b></td></tr>";
            printText += "<tr><td><b>TAXABLE</b></td><td class='text-right'>" + Convert.ToString($"${taxablesale:n}").PadLeft(8, ' ') + "</td><td class='text-right'>" + Convert.ToString($"${taxableReturnSale:n}").PadLeft(8, ' ') + "</td><td></td></tr>";
            printText += "<tr><td><b>NONTAXABLE</b></td><td class='text-right'>" + Convert.ToString($"${notaxablesale:n}").PadLeft(8, ' ') + "</td><td class='text-right'>" + Convert.ToString($"${notaxableReturnSale:n}").PadLeft(8, ' ') + "</td><td></td></tr>";
            printText += "<tr><td><b>SUB TOTAL</b></td><td class='text-right'>" + Convert.ToString($"${(taxablesale + notaxablesale):n}").PadLeft(8, ' ') + "</td><td class='text-right'>" + Convert.ToString($"${(taxableReturnSale + notaxableReturnSale):n}").PadLeft(8, ' ') + "</td><td class='text-right'>" + Convert.ToString($"${(taxablesale + notaxablesale) + (taxableReturnSale + notaxableReturnSale):n}").PadLeft(8, ' ') + "</td></tr>";
            printText += "<tr><td><b>SALES TAX</b></td><td class='text-right'>" + Convert.ToString($"${salestax:n}").PadLeft(8, ' ') + "</td><td class='text-right'>" + Convert.ToString($"${salesRetuntax:n}").PadLeft(8, ' ') + "</td><td class='text-right'>" + Convert.ToString($"${(salestax + salesRetuntax):n}").PadLeft(8, ' ') + "</td></tr>";
            printText += "<tr><td><b>SHIPPING</b></td><td class='text-right'>" + Convert.ToString($"${salesnh:n}").PadLeft(8, ' ') + "</td><td class='text-right'>" + Convert.ToString($"${saleRetursnh:n}").PadLeft(8, ' ') + "</td><td class='text-right'>" + Convert.ToString($"${(salesnh + saleRetursnh):n}").PadLeft(8, ' ') + "</td></tr>";
            printText += "<tr><td><b>TOTAL SALE</b></td><td class='text-right'>" + Convert.ToString($"${(taxablesale + notaxablesale + salestax + salesnh):n}").PadLeft(8, ' ') + "</td><td class='text-right'>" + Convert.ToString($"${(taxableReturnSale + notaxableReturnSale):n}").PadLeft(8, ' ') + "</td><td class='text-right'>" + Convert.ToString($"${(taxablesale + notaxablesale + salestax + salesnh) + (taxableReturnSale + notaxableReturnSale + salesRetuntax + saleRetursnh):n}").PadLeft(8, ' ') + "</td></tr>";
            printText += "</table>";

            printText += "<table width='450px'>";
            printText += "<tr><td width='225px'><b>SALE NET AMOUNT</b></td><td class='text-right' width='225px'>" + Convert.ToString($"${(taxablesale + notaxablesale + salestax + salesnh) + (taxableReturnSale + notaxableReturnSale + salesRetuntax + saleRetursnh):n}").PadLeft(8, ' ') + "</td></tr>";
            printText += "<tr><td><b>AMOUNT BALANCE</b></td><td class='text-right'>" + Convert.ToString($"${amountDue:n}").PadLeft(8, ' ') + "</td></tr>";
            printText += "</table>";

            printText += "<table width='450px'>";
            printText += "<tr><td width='225px'><b>ACCOUNT RECEIVEABLE</b></td><td class='text-right' width='225px'>" + Convert.ToString($"${receiveableAmt:n}").PadLeft(8, ' ') + "</td></tr>";
            printText += "<tr><td><b>AMOUNT PAID IN</b></td><td class='text-right'>" + txtPaidIn1 + "</td></tr>";
            printText += "<tr><td><b>REFUND AMOUNT PAID OUT</b></td><td class='text-right'>" + paidOut + "</td></tr>";
            printText += "<tr><td><b>STORE CREDIT OUT</b></td><td class='text-right'>" + StoreCreditOut + "</td></tr>";
            printText += "<tr><td><b>OPENING DRAWER CASH</b></td><td class='text-right'>" + OpeninDrawerCashAmount + "</td></tr>";
            printText += "<tr><td><b>CASH</b></td><td class='text-right'>" + CashSale + "</td></tr>";
            printText += "<tr><td><b>TOTAL DAY END CASH</b></td><td class='text-right'>" + TotalDayEndCash + "</td></tr>";
            printText += "<tr><td><b>CLOSING DRAWER CASH</b></td><td class='text-right'>" + ClosingDrawerAmount + "</td></tr>";
            printText += "</table>";

            printText += "<table width='450px'>";
            printText += "<tr><td width='150px'></td><td width='150px'><b>COUNTED</b></td><td width='150px'><b>SYSTEM</b></td></tr>";
            printText += "<tr><td><b>NET DAY END CASH</b></td><td class='text-right'>" + HCash + "</td><td class='text-right'>" + NetDayEndCash + "</td></tr>";
            printText += "<tr><td colspan='3'><b>" + netdayendcashstatus + "</b></td></tr>";
            printText += "<tr><td><b>CHARGE</b></td><td class='text-right'>" + Charges + "</td><td class='text-right'>" + txtCharge + "</td></tr>";
            printText += "<tr><td colspan='3'><b>" + chargesStatus + "</b></td></tr>";
            printText += "<tr><td><b>CHECK</b></td><td class='text-right'>" + HCheck + "</td><td class='text-right'>" + Check + "</td></tr>";
            printText += "<tr><td colspan='3'><b>" + checkStatus + "</b></td></tr>";
            printText += "<tr><td><b>EXPENCE PAID OUT</b></td><td></td><td class='text-right'>" + PayOutCash + "</td></tr>";
            printText += "<tr><td><b>NET DAY AMOUNT</b></td><td class='text-right'>" + HCountTotal + "</td><td class='text-right'>" + NetDayAmount + "</td></tr>";
            printText += "<tr><td colspan='3'><b>" + netdayendcashstatus + "</td></tr>";
            printText += "</table>";

            printText += "<table width='450px'>";
            printText += "<tr><td width='225px'><b>FINAL NET DAY AMOUNT</b></td><td class='text-right' width='225px'>$0.00</td></tr>";
            printText += "<tr><td><b>CRDIT COUPON/GIFT CARD IN</b></td><td class='text-right'>" + GiftCardIn + "</td></tr>";
            printText += "</table>";

            printText += "<table width='450px'>";
            printText += "<tr><td colspan='6' width='225px'><b>CASH BREAKUP</b></td></tr>";
            printText += "<tr><td width='75px'><b>100S</b></td><td width='75px'><b>50S</b></td><td width='75px'><b>20S</b></td><td width='75px'><b>10S</b></td><td width='75px'><b>5S</b></td><td width='75px'><b>1S</b></td></tr>";
            printText += "<tr><td class='text-right'>" + hs + "</td><td class='text-right'>" + fs + "</td><td class='text-right'>" + tws + "</td><td class='text-right'>" + ts + "</td><td class='text-right'>" + fis + "</td><td class='text-right'>" + os + "</td></tr>";
            printText += "</table>";

            printText += "<table width='450px'>";
            printText += "<tr><td  width='450px'>" + NtOfSale + "</td></tr>";
            printText += "<tr><td >" + NetOfItemSold + "</td></tr>";
            printText += "</table>";
            // }

            ViewBag.PrintHtml = "<div width='100%' padding-left= '37%'    background= 'white'>" + printText + "</div>";
            return View("~/Views/ListOfItemsSold/PrintDayEndProcess.cshtml");
        }

        public string GetOldPostData(string OldPosDate = "")
        {
            DateTime reportDate = Convert.ToDateTime(OldPosDate);
            DataTable data = _helperCommonService.GetOpeningDrawer(reportDate, _helperCommonService.StoreCodeInUse1);

            return JsonConvert.SerializeObject(data);
        }

        public IActionResult StoreCreditHistory(string creditNUmber = "", bool isGift = false)
        {
            ListOfItemsSoldModel objModel = new ListOfItemsSoldModel();
            DataTable dt = null;
            ViewBag.FirstColumnLabel = "Store Credit#";
            ViewBag.FormTitle = "STORE  CREDIT  HISTORY";
            ViewBag.ShowNotesColumn = "";
            string tableHeaders = "";
            if (isGift)
            {
                ViewBag.FirstColumnLabel = "Gift Cert#:";
                ViewBag.FormTitle = "GIFT  CREDIT  HISTORY";
                tableHeaders = "<tr><th class=\"text-center\">Gift Cert#</th>" +
                    "<th class=\"text-center\">Date</th><th class=\"text-center\">cust_code</th>" +
                    "<th class=\"text-center\">Used_Amt</th><th class=\"text-center\">Bal_Amt</th>" +
                    "<th class=\"text-center\">Inv_No</th><th>Notes</th></tr>";


            }
            else
            {
                tableHeaders += "<tr><th class=\"text-center\">Store Credit#</th>" +
                    "<th class=\"text-center\">Date</th><th class=\"text-center\">cust_code</th>" +
                    "<th class=\"text-center\">Used_Amt</th><th class=\"text-center\">Bal_Amt</th>" +
                    "<th class=\"text-center\">Inv_No</th></tr>";
            }
            ViewBag.Headers = tableHeaders;
            ViewBag.ShowNotesColumn = "<th>Notes</th>";
            string storeCreditHistoryHtml = "";
            if (creditNUmber != "")
            {
                dt = _helperCommonService.GetSroreCreditsHistory(creditNUmber, isGift);
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        string currentDate = "";
                        if (dr["date"].ToString() != "" && dr["date"].ToString() != null)
                        {
                            string[] cd = dr["date"].ToString().Split(' ');
                            currentDate = cd[0];
                        }

                        if (isGift)
                        {
                            storeCreditHistoryHtml += "<tr><td>"
                            + dr["CREDITNO"].ToString() + "</td><td>"
                            + currentDate + "</td><td>"
                            + dr["cust_code"].ToString() + "</td><td class='text-right'>"
                            + dr["Used_Amt"].ToString() + "</td><td  class='text-right'>"
                            + dr["Bal_Amt"].ToString() + "</td><td>"
                            + dr["Inv_No"].ToString() + "</td><td>" + dr["Inv_No"].ToString() + "</td>"
                            + "</tr>";
                        }
                        else
                        {
                            storeCreditHistoryHtml += "<tr><td>"
                            + dr["CREDITNO"].ToString() + "</td><td>"
                            + currentDate + "</td><td>"
                            + dr["cust_code"].ToString() + "</td><td class='text-right'>"
                            + dr["Used_Amt"].ToString() + "</td><td  class='text-right'>"
                            + dr["Bal_Amt"].ToString() + "</td><td>"
                            + dr["Inv_No"].ToString() + "</td>"
                            + "</tr>";
                        }

                    }
                }

            }
            ViewBag.DataItems = storeCreditHistoryHtml;
            return View("~/Views/ListOfItemsSold/StoreCreditHistory.cshtml");
        }

        public IActionResult MTDReport()
        {
            ViewBag.fromDate = DateTime.Now.ToString("yyyy-MM-01");
            ViewBag.toDate = DateTime.Now.ToString("yyyy-MM-dd");
            return View("~/Views/Analysis/MTDReport.cshtml");
        }

        public IActionResult GetMTDReportDetails(DateTime FROMDATE, DateTime DateTo, bool IncParialPay)
        {
            DateTime? fromDate = FROMDATE;
            DateTime? toDate = DateTo;

            string dtF = Convert.ToString(fromDate);
            string dtT = Convert.ToString(toDate);

            ViewBag.fromDate = FROMDATE.ToString("yyyy-MM-dd");
            ViewBag.toDate = DateTo.ToString("yyyy-MM-dd");

            string strDateRange = IncParialPay ? "MTD Revenue Generated" : "MTD Finalized Sales";

            DateTime dt3 = new DateTime(fromDate.Value.Year - 1, fromDate.Value.Month, fromDate.Value.Day);
            DateTime dt4 = new DateTime(toDate.Value.Year - 1, toDate.Value.Month, toDate.Value.Day);

            DateTime dt1 = new DateTime(fromDate.Value.Year, fromDate.Value.Month, fromDate.Value.Day);
            DateTime dt2 = new DateTime(toDate.Value.Year, toDate.Value.Month, toDate.Value.Day);


            ListOfItemsSoldModel objModel = new ListOfItemsSoldModel();

            DataTable dtStoreCurr = _listOfItemsSoldService.GetMTDReport(dt1.ToShortDateString(), dt2.ToShortDateString(), "STORE", IncParialPay);
            DataTable dtStorePrev = _listOfItemsSoldService.GetMTDReport(dt3.ToShortDateString(), dt4.ToShortDateString(), "STORE", IncParialPay);
            DataTable dtStoreData = this.MergeDatatables(dtStoreCurr, dtStorePrev, fromDate.Value.Year.ToString(), (fromDate.Value.Year - 1).ToString());

            DataTable dtStore = _listOfItemsSoldService.GetMTDGrossProfitVar(_helperCommonService.GetDataTableXML("STOREDT", dtStoreData));
            DataTable dtSalesman = _listOfItemsSoldService.GetMTDReport(dt1.ToShortDateString(), dt2.ToShortDateString(), "SALESMAN", IncParialPay);
            DataTable dtDUCSales = _listOfItemsSoldService.GetMTDReport(dt1.ToShortDateString(), dt2.ToShortDateString(), "DUC", IncParialPay);
            DataTable dtREPDUCSales = _listOfItemsSoldService.GetMTDReport(dt1.ToShortDateString(), dt2.ToShortDateString(), "REPDUC", IncParialPay);


            List<DataTable> result = dtStore.AsEnumerable()
                                                .OrderBy(u => u.Field<int>("ord"))
                                                .GroupBy(row => row.Field<string>("Grouped"))
                                                .Select(g => g.CopyToDataTable())
                                                .ToList();
            string Content = "";

            for (int i = 0; i < result.Count; i++)
            {
                DataTable dd = result[i];


                string[] columnSummary = result[i].AsEnumerable().OrderBy(u => u.Field<int>("ord")).OrderBy(u => u.Field<string>("Year")).Select(s => s.Field<string>("Year")).ToArray<string>();

                Content += "<div><table><tr style='background-color:#ddd;'><td width='140px'></td>";
                for (int k = 0; k < columnSummary.Length; k++)
                {
                    Content += "<td width='140px'><b>" + columnSummary[k] + "</b></td>";
                }
                Content += "</tr>";

                DataTable dt = result[i];
                int rowsInTable = dt.Rows.Count;
                string company = dt.Rows[0]["Grouped"].ToString();
                if (company != "")
                {
                    string count = (columnSummary.Length + 1).ToString();
                    Content = Content + "<tr style='background-color:#ddd;'><td colspan='" + count + "'><b>" + company + "</b></td></tr>";
                }
                dt.Columns.Remove("Grouped");
                dt.Columns.Remove("ord");
                dt.Columns.Remove("Year");

                string[] columnNames = dt.Columns.Cast<DataColumn>()
                                                .Select(column => column.ColumnName)
                                                .ToArray();
                int columnLength = columnNames.Length;


                for (int j = 0; j < columnLength - 1; j++)
                {
                    string columnName = columnNames[j];
                    decimal[] columnSummary1 = dt.AsEnumerable().Select(s => s.Field<decimal>(columnName)).ToArray<decimal>();
                    string displayColumnNam = "";
                    if (j == 0)
                    {
                        displayColumnNam = "<b>GROSS</b>";
                    }
                    if (j == 1)
                    {
                        displayColumnNam = "<b>GP$</b>";
                    }
                    if (j == 2)
                    {
                        displayColumnNam = "<b>GP%</b>";
                    }

                    Content = Content + "<tr><td width='140px'>" + displayColumnNam + "</td>";
                    for (int k = 0; k < columnSummary1.Length; k++)
                    {
                        Content = Content + "<td width='140px' style='text-align:right;'>" + string.Format("{0:#,##0.00}", columnSummary1[k]) + "</td>";
                    }
                    Content += "</tr>";
                }

                Content += "</table></div>";

            }

            string Content1 = "";
            if (dtSalesman != null)
            {
                Content1 = "<div><table><tr style='background-color:#ddd;'><td width='140px;'><b>Emp#</b></td>" +
                "<td width='140px;'><b>Employee</b></td>" +
                "<td width='140px;'><b>MTD Sales</b></td>" +
                "<td width='140px;'><b>Sales Tax</b></td>" +
                "<td width='140px;'><b>Net Sales</b></td>" +
                "<td width='140px;'><b>MTD GP$</b></td>" +
                "<td width='140px;'><b>MTD GP%</b></td></tr>";

                decimal SummaryGross = 0;
                decimal SummarySalesTax = 0;
                decimal SummaryNetSales = 0;
                decimal SummaryProfit = 0;
                decimal Profit_Per = 0;

                foreach (DataRow dr in dtSalesman.Rows)
                {
                    Content1 = Content1 + "<tr>"
                                + "<td>" + dr["Grouped"] + "</td>"
                                + "<td>" + dr["Individual"] + "</td>"
                                + "<td style='text-align:right;'>" + string.Format("{0:#,##0.00}", dr["Gross"]) + "</td>"
                                + "<td style='text-align:right;'>" + string.Format("{0:#,##0.00}", dr["SalesTax"]) + "</td>"
                                + "<td style='text-align:right;'>" + string.Format("{0:#,##0.00}", dr["NetSales"]) + "</td>"
                                + "<td style='text-align:right;'>" + string.Format("{0:#,##0.00}", dr["Profit"]) + "</td>"
                                + "<td style='text-align:right;'>" + string.Format("{0:#,##0.00}", dr["Profit_Per"]) + "</td></tr>";

                    SummaryGross += Convert.ToDecimal(dr["Gross"]);
                    SummarySalesTax += Convert.ToDecimal(dr["SalesTax"]);
                    SummaryNetSales += Convert.ToDecimal(dr["NetSales"]);
                    SummaryProfit += Convert.ToDecimal(dr["Profit"]);
                    Profit_Per += Convert.ToDecimal(dr["Profit_Per"]);
                }
                Content1 = Content1 + "<tr>"
                                + "<td colspan='1'><b>Total</b></td>"
                                + "<td></td>"
                                + "<td style='text-align:right;'><b>" + string.Format("{0:#,##0.00}", SummaryGross) + "</b></td>"
                                + "<td style='text-align:right;'><b>" + string.Format("{0:#,##0.00}", SummarySalesTax) + "</b></td>"
                                + "<td style='text-align:right;'><b>" + string.Format("{0:#,##0.00}", SummaryNetSales) + "</b></td>"
                                + "<td style='text-align:right;'><b>" + string.Format("{0:#,##0.00}", SummaryProfit) + "</b></td>"
                                + "<td style='text-align:right;'><b>" + string.Format("{0:#,##0.00}", Profit_Per) + "</b></td></tr></table></div>";
            }

            string Content2 = "";
            if (dtDUCSales != null)
            {
                string[] fd = dtF.Split(' ');
                string[] td = dtT.Split(' ');
                Content2 = "<div><table><tr style='background-color:#ddd;'><td width='750px;'><b>MTD DUC Sales MTD Revnue Revnue Gnarated From : " + fd[0] + " to " + td[0] + " </b></tr></table>";

                Content2 = Content2 + "<table>" +
                    "<tr style='background-color:#ddd;'><td width='250px;'><b>Emp#</b></td>" +
               "<td width='250px;'><b>Employee</b></td>" +
               "<td width='250px;'><b>DUC Sales</b></td></tr>";
                decimal ducSummaryGross = 0;
                foreach (DataRow dr in dtDUCSales.Rows)
                {
                    Content2 = Content2 + "<tr>"
                                + "<td>" + dr["Grouped"] + "</td>"
                                + "<td>" + dr["Individual"] + "</td>"
                                + "<td style='text-align:right;'>" + string.Format("{0:#,##0.00}", dr["Gross"]) + "</td></tr>";

                    ducSummaryGross += Convert.ToDecimal(dr["Gross"]);
                }

                Content2 = Content2 + "<tr>"
                                 + "<td colspan='1'><b>Total</b></td>"
                                 + "<td></td>"
                                 + "<td style='text-align:right;'><b>" + string.Format("{0:#,##0.00}", ducSummaryGross) + "</b></td></tr>";
                Content2 = Content2 + "</table></div>";

            }

            string Content3 = "";
            if (dtDUCSales != null)
            {
                string[] fd = dtF.Split(' ');
                string[] td = dtT.Split(' ');
                Content3 = "<div><table><tr style='background-color:#ddd;'><td width='750px;'><b>MTD REPDUC Sales MTD Revnue Gnarated From : " + fd[0] + " to " + td[0] + " </b></tr></table>";
                Content3 = Content3 + "<table>" +
               "<tr style='background-color:#ddd;'><td width='250px;'><b>Emp#</b></td>" +
               "<td width='250px;'><b>Employee</b></td>" +
               "<td width='250px;'><b>Sales</b></td></tr>";
                decimal rpSummary = 0;
                foreach (DataRow dr in dtREPDUCSales.Rows)
                {
                    Content3 = Content3 + "<tr>"
                                + "<td>" + dr["Grouped"] + "</td>"
                                + "<td>" + dr["Individual"] + "</td>"
                                + "<td style='text-align:right;'>" + string.Format("{0:#,##0.00}", dr["Gross"]) + "</td></tr>";

                    rpSummary += Convert.ToDecimal(dr["Gross"]);

                }
                Content3 = Content3 + "<tr>"
                                  + "<td colspan='1'><b>Total</b></td>"
                                  + "<td></td>"
                                  + "<td style='text-align:right;'><b>" + string.Format("{0:#,##0.00}", rpSummary) + "</b></td></tr>";
                Content3 = Content3 + "</table></div>";


            }
            string totalsContnt = "";
            if (dtStore != null)
            {
                var distinctIds = dtStore.AsEnumerable()
                                 .Select(s => new
                                 {
                                     Year = s.Field<string>("Year"),
                                     ord = s.Field<int>("ord")
                                 })
                                 .Distinct().ToList();
                if (distinctIds.Count > 0)
                {
                    //totalsContnt = totalsContnt + "<table><tr style='background-color:#ddd;'><td width='750px;'>Totals</td></tr></table>";
                    totalsContnt += "<table>";
                    totalsContnt += "<tr style='background-color:#ddd;'><td width='100px' ><b>Total</b></td>";
                    foreach (var itm in distinctIds)
                    {
                        string year = itm.Year;
                        int OrderNo = itm.ord;

                        totalsContnt = totalsContnt + "<td width='100px'><b>" + year + "</b></td>";
                    }
                    totalsContnt += "</tr>";
                    totalsContnt += "<tr><td><b>Gross</b></td>";
                    foreach (var itm in distinctIds)
                    {
                        string year = itm.Year;
                        int OrderNo = itm.ord;



                        DataRow[] filterData1 = dtStore.Select("Year='" + year + "' and ord=" + OrderNo);
                        decimal grossSum = 0;
                        if (filterData1.Length > 0)
                        {
                            foreach (DataRow item in filterData1)
                            {
                                grossSum += item.Field<decimal>("Gross");
                            }
                        }
                        totalsContnt += "<td style='text-align:right;'>" + string.Format("{0:#,##0.00}", grossSum) + "</td>";
                    }
                    totalsContnt += "</tr>";
                    totalsContnt += "<tr><td><b>Gp $</b></td>";
                    foreach (var itm in distinctIds)
                    {
                        string year = itm.Year;
                        int OrderNo = itm.ord;
                        DataRow[] filterData1 = dtStore.Select("Year='" + year + "' and ord=" + OrderNo);
                        decimal grossGp = 0;
                        if (filterData1.Length > 0)
                        {
                            foreach (DataRow item in filterData1)
                            {
                                grossGp += item.Field<decimal>("Profit");
                            }
                        }
                        totalsContnt += "<td style='text-align:right;'>" + string.Format("{0:#,##0.00}", grossGp) + "</td>";
                    }
                    totalsContnt += "</tr>";
                    totalsContnt += "<tr><td><b>Gp %</b></td>";
                    foreach (var itm in distinctIds)
                    {
                        string year = itm.Year;
                        int OrderNo = itm.ord;
                        DataRow[] filterData1 = dtStore.Select("Year='" + year + "' and ord=" + OrderNo);
                        decimal grossGpPr = 0;
                        if (filterData1.Length > 0)
                        {
                            foreach (DataRow item in filterData1)
                            {
                                grossGpPr += item.Field<decimal>("Profit_Per");
                            }
                        }
                        totalsContnt = totalsContnt + "<td style='text-align:right;'>" + string.Format("{0:#,##0.00}", grossGpPr) + "</td>";
                    }
                    totalsContnt += "</tr>";

                    totalsContnt += "</table>";
                }
            }
            ViewBag.htmlResults = Content + totalsContnt + Content1 + Content2 + Content3;
            //ViewBag.htmlResults = Content + totalsContnt + Content1 + Content2;
            return View("~/Views/Analysis/MTDReport.cshtml");
        }

        private DataTable MergeDatatables(DataTable dt1, DataTable dt2, string dYearCurr, string dYearPrev)
        {
            DataRow[] foundRow;
            string strStore = "";
            foreach (DataRow dr in dt1.Rows)
            {
                strStore = _helperCommonService.CheckForDBNull(dr["Grouped"]);
                foundRow = dt2.Select($"(GROUPED ='{_helperCommonService.EscapeSpecialCharacters(strStore)}')");
                if (foundRow.Length == 0)
                    dt2.Rows.Add(strStore, 0, dYearPrev, 0, 0, 0);
            }
            dt2.AcceptChanges();

            foreach (DataRow dr in dt2.Rows)
            {
                strStore = _helperCommonService.CheckForDBNull(dr["Grouped"]);
                foundRow = dt1.Select($"(GROUPED ='{_helperCommonService.EscapeSpecialCharacters(strStore)}')");
                if (foundRow.Length == 0)
                    dt1.Rows.Add(strStore, 0, dYearCurr, 0, 0, 0);
            }
            dt1.Merge(dt2);
            return dt1;
        }

        public IActionResult TodaysCashFlow()
        {
            return View("~/Views/Analysis/TodaysCashFlow.cshtml");
        }

        public string GetTodaysCashFlow(string FROMDATE, string DateTo)
        {
            ListOfItemsSoldModel objModel = new ListOfItemsSoldModel();
            DataTable dataTableCredit = _listOfItemsSoldService.GetRcvableCreditByTimeFrame("", "ZZ", "E", FROMDATE, DateTo, "P,C,", "");
            DataTable dataTableSummary = _listOfItemsSoldService.SummaryByPayment("", "ZZ", "E", FROMDATE, DateTo, "P,", "", true);
            DataTable dataTableRegister = _listOfItemsSoldService.ListofChecks("", "ZZ", "", "ZZ", 2, Convert.ToDateTime(FROMDATE), Convert.ToDateTime(DateTo), -999999999M, 999999999M, "", "", true, "DETAILS", false);

            var data = new Dictionary<string, DataTable>();

            data.Add("credit", dataTableCredit);
            data.Add("summary", dataTableSummary);
            data.Add("register", dataTableRegister);

            return JsonConvert.SerializeObject(data);
        }

        public IActionResult Follows()
        {
            ViewBag.StoreData = _helperCommonService.GetAllStoreCodesList();
            ViewBag.SalesmanData = _helperCommonService.GetAllSalesmansCodesList();
            return View("~/Views/Analysis/Follows.cshtml");
        }

        public string GetFollowsDetails(string FROMDATE, string DateTo, string Store, string Sales, bool AllDates)
        {
            bool allStores = false;
            if (Store == "ALL")
                allStores = true;
            DataTable dtFollows = _helperCommonService.GetSaleFollow(Sales, Store, Convert.ToDateTime(FROMDATE), Convert.ToDateTime(DateTo), allStores, AllDates, true);
            return JsonConvert.SerializeObject(dtFollows);
        }

        public IActionResult VoidedReceipts()
        {
            ViewBag.CustomerCodes = _helperCommonService.GetAllCustomerCodes();
            return View("~/Views/Invoice/VoidedReceipts.cshtml");
        }

        public string GetVoidedReceipts(string FROMDATE, string DateTo, string Ccode, string User)
        {
            string extraQuery = "";

            if (Ccode != "")
                extraQuery = " AND trim(acc) = '" + Ccode + "'";

            if (User != "")
                extraQuery += " AND trim(who) = '" + User + "'";

            DataTable dtKeepRec = _helperCommonService.GetSqlData(
                @"SELECT INV_NO,cast(DATE as date) DATE, TIME , WHO AS [USER], WHAT, Filename as ScanDoc 
                    FROM KEEP_REC WHERE DEL_INV = 1 and cast(date as date) between cast(@fromdate as date) 
                    and cast(@todate as date) " + extraQuery + " ORDER BY DATE,TIME DESC",
                    "@fromdate", FROMDATE, "@todate", DateTo);

            return JsonConvert.SerializeObject(dtKeepRec);
        }

        public IActionResult TrackChange(string InvoiceNumber)
        {
            DataTable data = _helperCommonService.GetKeepRecDetails(InvoiceNumber);
            string trackingData = "";
            string trackingDataReport = "";
            foreach (DataRow dr in data.Rows)
            {
                string currentDate = "";
                if (dr["DATE"].ToString() != "" && dr["DATE"].ToString() != null)
                {
                    string[] cd = dr["DATE"].ToString().Split(' ');
                    currentDate = cd[0];
                }
                trackingData += "<tr><td>"
                                + currentDate + "</td><td>"
                                + dr["TIME"].ToString() + "</td><td>"
                                + dr["WHO"].ToString() + "</td><td>"
                                + dr["WHAT"].ToString() + "</td><td>"
                                + dr["ACC"].ToString() + "</td><td>"
                                + "<a herf='javascript:void(0);' onclick='showmessage()'>View</a></td></tr>";

                trackingDataReport += "<tr><td>"
                                + currentDate + "</td><td>"
                                + dr["TIME"].ToString() + "</td><td>"
                                + dr["WHO"].ToString() + "</td><td>"
                                + dr["WHAT"].ToString() + "</td></tr>";


            }
            ViewBag.Inv_no = InvoiceNumber;
            ViewBag.Inv_data = trackingData;
            ViewBag.Inv_data_report = trackingDataReport;
            return View("~/Views/Admin/TrackChange.cshtml");
        }

        public IActionResult ListOfReturnedItems()
        {
            ViewBag.StoreData = _helperCommonService.GetAllStoreCodesList();
            ViewBag.SalesmanData = _helperCommonService.GetAllSalesmansCodesList();
            return View("~/Views/Invoice/ListOfReturnedItems.cshtml");
        }

        public string GetListOfReturnedItems(string FROMDATE, string DateTo, string SalesMan, string Store)
        {
            ListOfItemsSoldModel objModel = new ListOfItemsSoldModel();
            DataTable dtInvoiceItems = _listOfItemsSoldService.GetListofItemsReturnedDetails(FROMDATE, DateTo, Store, SalesMan);
            dtInvoiceItems.Columns.Add("Cons");
            dtInvoiceItems.Columns.Add("Vendor_Code");
            dtInvoiceItems.Columns.Add("Vendor_Memo");
            foreach (DataRow row in dtInvoiceItems.Rows)
            {
                string stylet = row["style"].ToString().Trim();
                DataTable dt = _listOfItemsSoldService.GetApmItemsFromStyle(stylet);
                List<string> Cons = new List<string>();
                List<string> vndpo = new List<string>();
                List<string> vndmemo = new List<string>();
                string resultantCons = string.Empty;
                string Rvndpo = string.Empty;
                string Rvndmemo = string.Empty;
                if (_helperCommonService.DataTableOK(dt))
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        Cons.Add(dr[0].ToString().Trim());
                        vndpo.Add(dr[1].ToString().Trim());
                        vndmemo.Add(dr[2].ToString().Trim());
                    }
                }
                resultantCons = string.Join("\n", Cons);
                Rvndpo = string.Join("\n", vndpo);
                Rvndmemo = string.Join("\n", vndmemo);

                row["Cons"] = resultantCons;
                row["Vendor_Code"] = Rvndpo;
                row["Vendor_Memo"] = Rvndmemo;
            }
            dtInvoiceItems.AcceptChanges();
            return JsonConvert.SerializeObject(dtInvoiceItems);
        }

        public string PrintProcess(string ProcesString)
        {

            DataTable filteredMailData = JsonConvert.DeserializeObject<DataTable>(ProcesString);
            DataTable dtmail = _helperCommonService.GetDistinctRecords(_helperCommonService.GetDataTableXML("datarows", filteredMailData), "MAIL", false, true);

            return JsonConvert.SerializeObject(dtmail);
        }

        public string CheckFileExistOrNot(string FileName)
        {
            ListOfItemsSoldModel objModel = new ListOfItemsSoldModel();
            DataTable upsIns = _listOfItemsSoldService.GetUpsInsTableDetails("SCANPATH");
            string scanPath = string.Empty;
            foreach (DataRow dr in upsIns.Rows)
            {
                scanPath = dr["SCANPATH"].ToString();
            }
            string FilePath = Path.Combine(scanPath, FileName);
            DataTable dt = _listOfItemsSoldService.CheckFileExistOrNot(FilePath);

            string status = "";

            if (Convert.ToInt32(dt.Rows[0][0]) == 0)
            {
                status = "NO";
            }
            else
            {
                DataTable fileData = _listOfItemsSoldService.GetFileContent(scanPath, FilePath);
                if (_helperCommonService.DataTableOK(fileData))
                {
                    byte[] objContext = (byte[])fileData.Rows[0][0];
                    byte[] fileContentWithOutNulls = null;
                    if (objContext.LongLength != 0)
                    {
                        long iter = objContext.LongLength - 1;
                        while (objContext[iter] == 0)
                            --iter;
                        fileContentWithOutNulls = new byte[iter + 1];
                        Array.Copy(objContext, fileContentWithOutNulls, iter + 1);
                    }
                    objContext = fileContentWithOutNulls;
                    string extension = Path.GetExtension(FileName).ToUpper();
                    if (objContext != null)
                    {
                        if (extension.ToUpper() != ".PDF")
                        {
                            using (MemoryStream mStream = new MemoryStream(objContext))
                            {
                                status = "NO";
                            }
                        }
                        else
                        {

                            string fileInFolderPath = Path.Combine(_env.ContentRootPath, "SCANNED/" + FileName);

                            if (!System.IO.File.Exists(fileInFolderPath))
                                System.IO.File.WriteAllBytes(fileInFolderPath, objContext);

                            status = FileName;
                        }
                    }
                }
            }

            return status;

        }

        public IActionResult GetInvoiceDetailsBasedOnInvNumber(string Invoice)
        {
            string result = "";
            DataRow invoiceRow;
            DataSet OnInitializeData = new DataSet();
            OnInitializeData = _helperCommonService.OnInitializeData_Combine_SP(Invoice, false, false, false, false);
            invoiceRow = _helperCommonService.DataTableOK(OnInitializeData.Tables[0]) ? OnInitializeData.Tables[0].Rows[0] : null;// this.invoiceService.GetInvoiceByInvNo(_helperCommonService.Pad6(this.txtInvoiceNo.Text));

            if (invoiceRow == null)
            {
                result = "Invalid Invoice#";
            }
            string customerAcc = invoiceRow["acc"].ToString();

            bool is_Installment = false;
            decimal installmentInterest = 0;

            if (_helperCommonService.CheckForDBNull(invoiceRow["IS_INSTALLMENT"], "System.Boolean"))
            {
                is_Installment = true;
                installmentInterest = _helperCommonService.CheckForDBNull(invoiceRow["EMI_INTERESTAMOUNT"], "System.Decimal");
            }
            string typemode = "";
            DataRow customerRow = _helperCommonService.DataTableOK(OnInitializeData.Tables[1]) ? OnInitializeData.Tables[1].Rows[0] : null;// customerService.CheckValidBillingAcct(invoiceRow["bacc"].ToString());

            //string addrLabelShip = _helperCommonService.GetAddressLabelFrom_InvoiceTbl(invoiceRow, customerRow, "\n", false, typemode);

            if (customerRow == null)
            {
                result = "Customer Record does not exist.";

            }

            DataRow upsInsRow = _helperCommonService.DataTableOK(OnInitializeData.Tables[2]) ? OnInitializeData.Tables[2].Rows[0] : null;// _helperCommonService.GetSqlRow("select top 1 * from ups_ins");
            DataTable dtrepcostdata = OnInitializeData.Tables[3];// _helperCommonService.GetpartshistByInvno(txtInvoiceNo.Text);
            DataTable dtStores = OnInitializeData.Tables[4];// _helperCommonService.GetStores(_helperCommonService.CheckForDBNull(invoiceRow["store_no"]));

            DataTable dtMasterDetail = new DataTable();
            if (!_helperCommonService.DataTableOK(dtMasterDetail))
                dtMasterDetail = OnInitializeData.Tables[5];// this.invoiceService.GetInvoiceMasterDetailPO(InvoiceNo, false, _helperCommonService.is_Briony, this.iSVatInclude);

            if (OnInitializeData != null && _helperCommonService.DataTableOK(OnInitializeData.Tables[5]))
            {
                dtMasterDetail = OnInitializeData.Tables[5];
            }

            DataRow drMasterDetail = _helperCommonService.GetRowOne(dtMasterDetail);

            DataRow drSalesManRow = _helperCommonService.DataTableOK(OnInitializeData.Tables[6]) ? OnInitializeData.Tables[6].Rows[0] : null;// _helperCommonService.GetSqlRow("select top 1 name,code,email from salesmen where code=@code", "@code", _helperCommonService.CheckForDBNull(invoiceRow["salesman1"]));

            string salesman = ""; string salesmanemail = ""; string customeremail = "";
            if (drSalesManRow != null)
            {
                salesman = !string.IsNullOrWhiteSpace(_helperCommonService.CheckForDBNull(drSalesManRow["name"])) ? _helperCommonService.CheckForDBNull(drSalesManRow["name"]) : _helperCommonService.CheckForDBNull(drSalesManRow["code"]);
                salesmanemail = _helperCommonService.CheckForDBNull(drSalesManRow["email"]);
            }
            salesman = _helperCommonService.CheckForDBNull(drMasterDetail["salesman"]).Trim() + "," +
                    _helperCommonService.CheckForDBNull(drMasterDetail["salesman2"]).Trim() + "," +
                    _helperCommonService.CheckForDBNull(drMasterDetail["salesman3"]).Trim();
            //Customer_name = _helperCommonService.CheckForDBNull(drMasterDetail["NAME"], typeof(string).ToString());
            //strSalesman = strSalesman.ToString().TrimStart(',').TrimEnd(',').Replace(",,", ",");
            if (customerRow != null)
                customeremail = _helperCommonService.CheckForDBNull(customerRow["email"]);

            string register = _helperCommonService.CheckForDBNull(invoiceRow["register"]);
            string custCode = drMasterDetail["ACC"].ToString();

            string rpReportType = string.Empty;

            if (_helperCommonService.CheckForDBNull(drMasterDetail["LAYAWAY"], typeof(bool).ToString()))
                rpReportType = "1";
            else if (_helperCommonService.CheckForDBNull(drMasterDetail["IsSpecialItem"], typeof(bool).ToString()) && !_helperCommonService.CheckForDBNull(drMasterDetail["PICKED"], typeof(bool).ToString()))
                rpReportType = "2";
            else
                rpReportType = "0";

            string pageNumber = "1";
            bool IsPicked = _helperCommonService.CheckForDBNull(drMasterDetail["PICKED"], typeof(bool).ToString());

            //string myName = 

            string date = drMasterDetail["date"].ToString();

            string companyname = "";
            string storecode = dtMasterDetail.Rows[0]["store_no"].ToString();
            DataTable storeInfo = _helperCommonService.GetStores(storecode);

            long tel_no = 0;
            string TelNo = string.Empty;
            string storename = "";
            string address = "";
            string myName = "";
            if (_helperCommonService.DataTableOK(storeInfo))
            {
                DataRow dataRow = _helperCommonService.GetRowOne(storeInfo);
                storename = _helperCommonService.GetValue(storeInfo, "name");

                if (tel_no != 0)
                {
                    if (Convert.ToString(tel_no).Length > 9)
                    {
                        string nums = String.Join("", tel_no);
                        TelNo = nums.Insert(0, "(").Insert(4, ")").Insert(5, " ").Insert(9, "-");
                    }
                    else
                        TelNo = String.Format("{0:(###)###-####}", Convert.ToInt64(tel_no));
                }

                string seperator = "<br>";
                DataTable dataTable = _helperCommonService.GetSqlData("select * from ups_ins");
                string CompanyEmail = _helperCommonService.CheckForDBNullUPS(dataTable, "Company_email");

                address = string.Format("{0}{4}{1}{4}{2}{4}{3}{4}{5}{4}{6}", dataRow["addr1"].ToString().Trim(),
                   dataRow["addr2"].ToString().Trim(), dataRow["addr3"].ToString().Trim(),
                   dataRow["addr4"].ToString().Trim(), seperator, TelNo,
                   CompanyEmail).Replace("\n\n", "\n").
                   Replace(", , ", ", ").Replace("\n\n", "\n").Replace(", , ", ", ");
            }
            string storeNumber = drMasterDetail["STORE_NO"].ToString();

            string addrLabel = "";
            if (_helperCommonService.GetBillShipDiff(drMasterDetail["acc"].ToString()))
                addrLabel = _helperCommonService.GetAddressLabelShip(_helperCommonService.CheckForDBNull(drMasterDetail["acc"]), "\n");

            string data = CreateInvoicHtmlWithImage(rpReportType, Invoice, custCode, date, salesman, register, pageNumber, storename, address, storeNumber, IsPicked, addrLabel, dtMasterDetail, drMasterDetail, dtStores);

            ViewBag.htmlResults = data;
            return View("~/Views/Analysis/ReprintInvoice.cshtml");
        }

        public string CreateInvoicHtmlWithImage(string rpReportType, string invoiceNumber, string custCode, string date, string salesRep, string register, string pageNumber, string myName, string address, string storeNumber, bool IsPicked, string addrLabel, DataTable dtMasterDetail, DataRow drMasterDetail, DataTable dtStores)
        {
            string rpReportTypeTxt = "";
            string headerLeft = "";
            string headerCentr = "";
            string headerRight = "";

            string custCode2 = "NOT PICKED UP";
            if (IsPicked)
            {
                custCode2 = "PICKED UP:";
            }

            byte[] bytStoreImage = _helperCommonService.GetStoreImage(storeNumber);

            if (rpReportType == "3")
            {
                rpReportTypeTxt = "CANCELLED LAYAWAY:";
            }
            else if (rpReportType == "2")
            {
                rpReportTypeTxt = "SPECIAL ORDER:";
            }
            else
            {
                rpReportTypeTxt = "INVOICE NO.:";
            }


            headerLeft += "<table><tr><td></td></tr></table>";

            headerCentr = headerCentr + "<table><tr><td style='text-align:center;'><b>" + myName + "</b></td></tr><tr><td style='text-align:center;'>" + address + "</td></tr></table>";

            headerRight += "<table>";

            string[] s1 = date.Split(' ');

            headerRight += "<tr><td>" + rpReportTypeTxt + "</td><td>" + invoiceNumber + "</td></tr>";
            headerRight += "<tr><td>CUST. CODE:</td><td>" + custCode + "</td></tr>";
            headerRight += "<tr><td>DATE:</td><td>" + s1[0] + "</td></tr>";
            headerRight += "<tr><td>SALES REP:</td><td>" + salesRep + "</td></tr>";
            headerRight += "<tr><td>REGISTER:</td><td>" + register + "</td></tr>";
            headerRight += "<tr><td>PAGE NO.:</td><td>" + pageNumber + "</td></tr>";

            headerRight += "</table>";
            string htmHeaderContent = "";

            htmHeaderContent += "<div class='row'>";
            htmHeaderContent += "<div class='col-md-4'>" + headerLeft + "</div>";
            htmHeaderContent += "<div class='col-md-4'>" + headerCentr + "</div>";
            htmHeaderContent += "<div class='col-md-4'>" + headerRight + "</div>";
            htmHeaderContent += "</div>";



            htmHeaderContent += "<div class='row' >";
            htmHeaderContent += "<div class='col-md-4'><b>Cust Code :</b><br>" + custCode2 + "</div><div class='col-md-4'></div>";
            htmHeaderContent += "<div class='col-md-3'><div class='row'>" +
                                                    "<div class='col-md-12'>" +
                                                    "<div class='row' style='background-color:#ddd'><div class='col-md-12'><b>Customer Address</b></div></div>" +
                                                    "<div class='row'><div class='col-md-12'>" + addrLabel + "</div></div>" +
                                                    "</div>" +
                                                    "</div>" +
                                                    "</div>";
            htmHeaderContent += "</div>";

            htmHeaderContent += "<div class='row' style='padding:5px;'>";
            htmHeaderContent += "<div class='col-md-12'></div>";
            htmHeaderContent += "</div>";

            htmHeaderContent += "<div class='row'><div class='col-md-12'><table width='100%' class='ta'><tr  style='background-color: #ccc;padding:5px;border: 1px solid #000'>";
            htmHeaderContent += "<td width='10%'><b>STYLE</b></td>";
            htmHeaderContent += "<td width='30%'><b>DESC</b></td>";
            htmHeaderContent += "<td width='15%'><b>QTY</b></td>";
            htmHeaderContent += "<td width='15%'><b>TAG</b></td>";
            htmHeaderContent += "<td width='15%'><b>PRICE</b></td>";
            htmHeaderContent += "<td width='15%'><b>AMOUNT</b></td>";
            htmHeaderContent += "</tr>";

            DataTable data = dtMasterDetail;

            if (data != null)
            {

                decimal qty = 0;
                foreach (DataRow dr in data.Rows)
                {
                    htmHeaderContent += "<tr>";
                    htmHeaderContent += "<td>" + dr["STYLE"].ToString() + "</td>";
                    htmHeaderContent += "<td>" + dr["DESC"].ToString() + "</td>";
                    htmHeaderContent += "<td style='text-align:right;'>" + dr["QTY"].ToString() + "</td>";
                    htmHeaderContent += "<td style='text-align:right;'>" + Convert.ToString($"${(dr["TAG_PRICE"]):n}") + "</td>";
                    htmHeaderContent += "<td style='text-align:right;'>" + Convert.ToString($"${(dr["PRICE"]):n}") + "</td>";
                    htmHeaderContent += "<td style='text-align:right;'>" + Convert.ToString($"${(dr["AMOUNT"]):n}") + "</td>";
                    htmHeaderContent += "</tr>";
                    qty += Convert.ToDecimal(dr["QTY"]);
                }

                htmHeaderContent += "<tr style='padding:5px;background-color: #ccc;'>";
                htmHeaderContent += "<td><b>Total</b></td>";
                htmHeaderContent += "<td></td>";
                htmHeaderContent += "<td style='text-align:right;'><b>" + qty.ToString() + "</b></td>";
                htmHeaderContent += "<td></td>";
                htmHeaderContent += "<td></td>";
                htmHeaderContent += "<td></td>";
                htmHeaderContent += "</tr>";
            }

            htmHeaderContent += "</table>";

            string sSalesTax = "", sTradeinAmt = "0";
            sSalesTax = Convert.ToString(_helperCommonService.CheckForDBNull(drMasterDetail["sales_tax"], typeof(decimal).ToString()));
            string sSubtotal = GetsSubtotal(dtMasterDetail, sSalesTax);

            sTradeinAmt = Convert.ToString(_helperCommonService.CheckForDBNull(drMasterDetail["tradeinamt"], typeof(decimal)));
            string sSNH = string.Format("{0:0.00}", _helperCommonService.DecimalCheckForDBNull(drMasterDetail["SNH"]));
            string sGrandTotal = Convert.ToString(Math.Round(_helperCommonService.CheckForDBNull(drMasterDetail["gr_total"], typeof(decimal)) * 1, 2));
            string paid = string.Format("{0:N2}", _helperCommonService.CheckForDBNull(drMasterDetail["credits"], typeof(decimal).ToString()));
            string balancedue = string.Format("{0:N2}", (_helperCommonService.CheckForDBNull(drMasterDetail["gr_total"], typeof(decimal)) -
               _helperCommonService.CheckForDBNull(drMasterDetail["credits"], typeof(decimal))) * 1, typeof(decimal).ToString());

            string invoicenote = "";
            invoicenote = dtStores.Rows[0]["invoicenote"].ToString();

            string subTotal = "<div class='row'><div class='col-md-6'><b>Sub Total:</b></div><div class='col-md-6' style='text-align:right;'>" + Convert.ToString($"${(sSubtotal):n}") + "</div></div>";
            subTotal = subTotal + "<div class='row'><div class='col-md-6'><b>Trade-In:</b></div><div class='col-md-6' style='text-align:right;'>" + Convert.ToString($"${(sTradeinAmt):n}") + "</div></div>";
            subTotal = subTotal + "<div class='row'><div class='col-md-6'><b>SNH:</b></div><div class='col-md-6' style='text-align:right;'>" + Convert.ToString($"${(sSNH):n}") + "</div></div>";
            subTotal = subTotal + "<div class='row'><div class='col-md-6'><b>Sales Tax:</b></div><div class='col-md-6' style='text-align:right;'>" + Convert.ToString($"${(sSalesTax):n}") + "</div></div>";
            subTotal = subTotal + "<div class='row'><div class='col-md-6'><b>Grand Total:</b></div><div class='col-md-6' style='text-align:right;'>" + Convert.ToString($"${(sGrandTotal):n}") + "</div></div>";
            subTotal = subTotal + "<div class='row'><div class='col-md-6'><b>Paid:</b></div><div class='col-md-6' style='text-align:right;'>" + Convert.ToString($"${(paid):n}") + "</div></div>";
            subTotal = subTotal + "<div class='row'><div class='col-md-6'><b>Balance Due:</b></div><div class='col-md-6' style='text-align:right;'>" + Convert.ToString($"${(balancedue):n}") + "</div></div>";

            htmHeaderContent += "<div class='row' style='padding:5px;'>";
            htmHeaderContent += "<div class='col-md-8' style='border: 1px solid #000;min-height:100px;'>" + invoicenote + "</div>";
            htmHeaderContent += "<div class='col-md-4' style='border: 1px solid #000;min-height:100px;'>" + subTotal + "</div>";
            htmHeaderContent += "</div>";

            htmHeaderContent += "<div class='row' style='padding:5px;'>";
            htmHeaderContent += "<div class='col-md-12'></div>";
            htmHeaderContent += "</div>";

            DataTable paymentInfo = _helperCommonService.GetInvoicePayments(invoiceNumber, true);

            htmHeaderContent += "<div class='row' style='padding:5px;'>";
            htmHeaderContent += "<div class='col-md-8'>";

            htmHeaderContent += "<table class='ta' width='100%'><tr style='padding:5px;background-color:#ccc;'>";
            htmHeaderContent += "<td width='20%'><b>Date</b></td>";
            htmHeaderContent += "<td width='40%'><b>Pay Method</b></td>";
            htmHeaderContent += "<td width='20%'><b>Amount In $</b></td>";
            htmHeaderContent += "<td width='20%'><b>Note</b></td>";
            htmHeaderContent += "</tr>";

            if (paymentInfo != null)
            {
                foreach (DataRow dr in paymentInfo.Rows)
                {
                    string[] s = dr["DATE"].ToString().Split(' ');
                    htmHeaderContent += "<tr style='padding:5px;'>";
                    htmHeaderContent += "<td>" + s[0] + "</td>";
                    htmHeaderContent += "<td>" + dr["METHOD"].ToString() + "</td>";
                    htmHeaderContent += "<td style='text-align:right;'>" + dr["AMOUNT"].ToString() + "</td>";
                    htmHeaderContent += "<td>" + dr["NOTE"].ToString() + "</td>";
                    htmHeaderContent += "</tr>";
                }

            }
            htmHeaderContent += "</table>";
            htmHeaderContent += "</div>";
            htmHeaderContent += "<div class='col-md-4'><div><b>Invoice No#: </b>" + invoiceNumber + "</div><div><b>Cust Code:</b>" + custCode + " </div></div>";
            htmHeaderContent += "</div>";



            return htmHeaderContent;
        }

        private String GetsSubtotal(DataTable dtMasterDetail, String sSalesTax)
        {
            return string.Format("{0:0.00}", _helperCommonService.CheckForDBNull(dtMasterDetail.Compute("SUM(Amount)", "1=1") == DBNull.Value ? 0 : dtMasterDetail.Compute("SUM(Amount)", "1=1"), typeof(decimal).ToString()));
        }

        public IActionResult AddPaymentTypes()
        {
            //DataTable dt = _helperCommonService.GetAllPaymentTypes();
            return View("~/Views/Ar/AddPaymentTypes.cshtml");
        }

        public string GetPaymentType()
        {
            DataTable dt = _helperCommonService.GetAllPaymentTypes();
            DataTable glCodes = _helperCommonService.Getname();

            var data = new
            {
                paymentStatus = new[]
                        {
                    new
                    {
                                paymentDetails = dt,
                                glCodesDetails = glCodes
                            }
                        }
            };

            return JsonConvert.SerializeObject(data);
        }

        public string SavePaymentType(string payTypes)
        {
            DataTable paymentTypesData = JsonConvert.DeserializeObject<DataTable>(payTypes);
            string status = "";
            if (paymentTypesData.Rows.Count == 0)
            {
                status = "No Record Found For PaymentTypes";
                return JsonConvert.SerializeObject(status);
            }

            foreach (DataRow row in paymentTypesData.Rows)
            {
                string payment = row["PAYMENTTYPE"].ToString();
                if (paymentTypesData.AsEnumerable().OfType<DataRow>().Where(x => x.Field<string>("PAYMENTTYPE") == payment).ToList().Count > 1)
                {
                    status = "Payment Type " + payment + " should not be more than one";
                    return JsonConvert.SerializeObject(status);

                }
            }

            string showline = "";

            foreach (DataRow dr in paymentTypesData.Rows)
            {
                bool Deposit = Convert.ToBoolean(dr["Requires_deposit"]);
                string Glcode = Convert.ToString(dr["Gl_acc"].ToString());
                string payty = Convert.ToString(dr["PAYMENTTYPE"].ToString());


                if (Deposit && Glcode != "")
                    showline += String.IsNullOrEmpty(payty) ? Convert.ToString(payty) : String.Concat(",", Convert.ToString(payty));
            }

            ListOfItemsSoldModel objModel = new ListOfItemsSoldModel();
            DataTable compare = _helperCommonService.GetAllPaymentTypes();
            bool tablesSame = AreTablesTheSame(paymentTypesData, compare);

            if (tablesSame)
            {
                status = "Data Updated successfully";
                return JsonConvert.SerializeObject(status);
            }
            else
            {
                DataTable dtReturn = _listOfItemsSoldService.Insert_DeletetPaymentType(_helperCommonService.GetDataTableXML("Payment", paymentTypesData), string.Empty);
                status = "Data Updated successfully";
                return JsonConvert.SerializeObject(status);
            }

        }

        public bool AreTablesTheSame(DataTable tbl1, DataTable tbl2)
        {
            if (tbl1.Rows.Count != tbl2.Rows.Count || tbl1.Columns.Count != tbl2.Columns.Count)
                return false;

            for (int i = 0; i < tbl1.Rows.Count; i++)
                for (int c = 0; c < tbl1.Columns.Count; c++)
                    if (!Equals(tbl1.Rows[i][c], tbl2.Rows[i][c]))
                        return false;
            return true;
        }

        public IActionResult Appraisals(string AppId = "")
        {
            ViewBag.StoreData = _helperCommonService.GetAllStoreCodesList();
            ViewBag.SalesmanData = _helperCommonService.GetAllSalesmansCodesList();
            ListOfItemsSoldModel objModel = new ListOfItemsSoldModel();
            objModel.CustomerCodes = _helperCommonService.GetAllCustomerCodes();
            ViewBag.AllStyles = _helperCommonService.GetAllStyles();

            string inv_no = "";
            string acc = "";
            string appraisrName = "";
            string store = "";
            string salesman = "";
            string Addr = "";
            string Addr1 = "";
            string City = "";
            string State = "";
            string Zip = "";
            string htmlContent = "";
            string appDate = "";
            string txtNam = "";
            string appraisalId = "";
            string styleId = "";

            if (AppId != "")
            {
                DataTable dt = _helperCommonService.GetAppraisal(AppId);
                appraisalId = AppId;
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        inv_no = dr["inv_no"].ToString();
                        acc = dr["acc"].ToString();
                        appraisrName = dr["AccName"].ToString();
                        store = dr["Store"].ToString();
                        salesman = dr["Salesman"].ToString();
                        Addr = dr["Addr"].ToString();
                        Addr1 = dr["Addr1"].ToString();
                        City = dr["City"].ToString();
                        State = dr["State"].ToString();
                        Zip = dr["Zip"].ToString();
                        appDate = Convert.ToDateTime(dr["AppDate"]).ToString("yyyy-MM-dd");
                        styleId = dr["StyleId"].ToString();

                        htmlContent += "<tr class='appraisalRow' onclick=\"updateDesc(this)\">";
                        if (dr["Select"].ToString() == "True")
                        {
                            htmlContent += "<td width=\"50px\"><input type=\"checkbox\" name=\"\" id=\"\" value=\"\" class=\"noInputBorders selectItem\" checked=\"checked\"></td>";
                        }
                        else
                        {
                            htmlContent += "<td width=\"50px\"><input type=\"checkbox\" name=\"\" id=\"\" value=\"\" class=\"noInputBorders selectItem\"></td>";
                        }

                        htmlContent += "<td width=\"150px\" style = \"text-align:right;\"><input type=\"text\" name=\"\" id=\"\" value='" + dr["Style"].ToString() + "' class=\"noInputBorders stylesList styleno\" onclick=\"updateDescVal()\"><div id=\"\" class=\"style-autocomplete-items\"></div></td>";
                        htmlContent += "<td style = \"\"><textarea name=\"\" id=\"\" class=\"noInputBorders styleDesc\" cols=\"80\" rows=\"1\" style = \"width:100%\">" + dr["StyleDesc"].ToString() + "</textarea></td>";
                        htmlContent += "<td width=\"150px\" style = \"text-align:right;\"><input type=\"number\" name=\"\" id=\"\" value='" + dr["Price"].ToString() + "'  class=\"text-right stylePrice\"></td>";
                        htmlContent += "<td width=\"150px\" style = \"text-align:right;display:none;\"><input type=\"number\" name=\"\" id=\"\" value='" + styleId + "'  class=\"text-right styleId\"></td>";
                        htmlContent += "</tr>";
                    }
                    txtNam = _helperCommonService.GetAppraiserName();
                }
            }

            ViewBag.inv_no = inv_no;
            ViewBag.acc = acc;
            ViewBag.appraisrName = appraisrName;
            ViewBag.store = store;
            ViewBag.salesman = salesman;
            ViewBag.Addr = Addr;
            ViewBag.Addr1 = Addr1;
            ViewBag.City = City;
            ViewBag.State = State;
            ViewBag.Zip = Zip;
            ViewBag.htmlContent = htmlContent;
            ViewBag.appDate = appDate;
            ViewBag.txtNam = txtNam;
            ViewBag.appraisalId = appraisalId;
            return View("~/Views/Appraisal/Appraisals.cshtml", objModel);
        }

        public string GetInvoiceDetailsInvNo(string Invno, string AppDate)
        {
            ListOfItemsSoldModel objModel = new ListOfItemsSoldModel();
            DataRow dr = _listOfItemsSoldService.GetInvoiceByInvNo(Invno);
            string status = "";
            DataRow addressRow = null;
            DataTable dtInvoiceItems = null;
            //var data = "";
            if (dr == null)
            {
                var data = new
                {
                    invStatus = new[]
                    {
                        new
                        {
                            status = "Invalid Invoice#",
                            address = "",
                            invoice = ""
                        }
                    }
                };
                return JsonConvert.SerializeObject(data);
            }
            else
            {
                if (_helperCommonService.GetAppraisalCount(Invno.Trim()) >= 2)
                {
                    var data = new
                    {
                        invStatus = new[]
                    {
                            new
                            {
                                status = "This invoice already added two appraisals, we can't add more than two.",
                                address = "",
                                invoice = ""
                            }
                        }
                    };
                    return JsonConvert.SerializeObject(data);
                }
                else
                {
                    dtInvoiceItems = _helperCommonService.GetInvoiceItemsForAppr(_helperCommonService.Pad6(Invno));
                    //dtInvoiceItems.AcceptChanges();
                    DataTable dt = _helperCommonService.GetAppraisal("XXXXX");
                    DataTable dtApp = _helperCommonService.GetAppraisal(_helperCommonService.Pad6(Invno));
                    foreach (DataRow dr1 in dtInvoiceItems.Rows)
                    {
                        DataRow[] row = dt.Select($"style='{dr1["style"]}'");
                        if (row.Length == 0)
                            dt.Rows.Add(false, _helperCommonService.CheckForDBNull(dr1["Style"]), _helperCommonService.CheckForDBNull(dr1["Desc"]), _helperCommonService.CheckForDBNull(dr1["Price"]), _helperCommonService.Pad6(Invno), _helperCommonService.CheckForDBNull(dr1["acc"]), "", AppDate, 0);
                    }
                    status = "";
                    string acc = _helperCommonService.CheckForDBNull(dr["acc"]);
                    addressRow = getcutshipaddress(acc);
                    string AppraiserName = _helperCommonService.GetAppraiserName();
                    string TableName = "Appraisal";
                    string FieldName = "Appraisalid";
                    string MaxLimit = "";
                    string MinLimit = "";
                    string PField = string.Empty;
                    string PValue = string.Empty;
                    string appraisalid = _helperCommonService.GetNextSeqNo(TableName, FieldName, MaxLimit, PField, MinLimit, PValue);
                    var data = new
                    {
                        invStatus = new[]
                        {
                            new
                            {
                                status = "",
                                appraiserName = AppraiserName,
                                customerCode = acc,
                                address = addressRow,
                                invoice = dtInvoiceItems,
                                nextAppraiserId = appraisalid
                            }
                        }
                    };

                    return JsonConvert.SerializeObject(data);
                }

            }
        }

        public DataRow getcutshipaddress(string acc)
        {
            DataRow custrow = _helperCommonService.CheckValidCustomerCodeForAppraisal(_helperCommonService.CheckForDBNull(acc));
            return custrow;
        }

        public string SaveAppraisal(string AppraisalData, string AppraisalDate, string AppraisalNo, bool SaveType = false, string AppraiserName = "")
        {
            DataTable dt = JsonConvert.DeserializeObject<DataTable>(AppraisalData);
            string AppId = "";
            string rptData = "";
            bool isNew = false;
            if (SaveType)
            {
                isNew = true;
                string TableName = "Appraisal";
                string FieldName = "Appraisalid";
                string MaxLimit = "";
                string MinLimit = "";
                string PField = string.Empty;
                string PValue = string.Empty;
                string newAppid = _helperCommonService.GetNextSeqNo(TableName, FieldName, MaxLimit, PField, MinLimit, PValue);
                foreach (DataRow row in dt.Rows)
                {
                    row[6] = newAppid;
                }
                dt.AcceptChanges();
                AppraisalNo = newAppid;
            }
            if (_helperCommonService.UpdateAppraisal(_helperCommonService.GetDataTableXML("AppItems", dt), isNew, out AppId))
            {
                rptData = PreparePrintData(AppId, AppraisalDate, AppraiserName);
            }
            else
            {
                rptData = PreparePrintData(AppraisalNo, AppraisalDate, AppraiserName);
            }

            return JsonConvert.SerializeObject(rptData);
        }

        public string GetCustomerAddress(string CustCode)
        {
            DataRow addressRow = getcutshipaddress(CustCode);
            string AppraiserName = _helperCommonService.GetAppraiserName();
            var data = new
            {
                invStatus = new[]
                        {
                            new
                            {
                                status = "",
                                appraiserName = AppraiserName,
                                address = addressRow
                            }
                        }
            };
            return JsonConvert.SerializeObject(data);
        }

        public string GetDetailsFromCostTab(string Style)
        {
            string desc = _helperCommonService.GetDetailsFromCostTab(Style).Trim();
            var data = new
            {
                invStatus = new[]
                        {
                            new
                            {
                                status = desc
                            }
                        }
            };
            return JsonConvert.SerializeObject(data);
        }

        public string PreparePrintData(string appid, string AppraisalDate, string AppraiserName)
        {
            bool NIsQuickAppr = false;
            DataTable dt = _helperCommonService.GetAppraisal(appid, NIsQuickAppr);
            byte[] storeLogo = _helperCommonService.GetStoreImage();
            string logo = "";

            if (storeLogo != null && storeLogo.Length > 0)
            {
                logo = "<img src='data:image/png;base64," + Convert.ToBase64String(storeLogo) + "' height='150px'>";
            }
            string htmlContent = string.Empty;
            string companyname = String.Empty;
            DataTable dataTable = _helperCommonService.GetSqlData("select * from ups_ins");
            string CompanyEmail = _helperCommonService.CheckForDBNullUPS(dataTable, "Company_email");
            string CompanyName = _helperCommonService.CheckForDBNullUPS(dataTable, "COMPANYNAME");
            string StoreCode = _helperCommonService.CheckForDBNullUPS(dataTable, "store");
            //DataTable storeInfo1 = _helperCommonService.GetStores(!string.IsNullOrWhiteSpace(StoreCode) ? StoreCode : _helperCommonService.FixedStoreCode);
            DataTable storeInfo1 = _helperCommonService.GetStores(!string.IsNullOrWhiteSpace(_helperCommonService.StoreCode) ? _helperCommonService.StoreCode : _helperCommonService.FixedStoreCode);
            string companyaddrLabel = ("" + storeInfo1.Rows[0]["name"].ToString().Trim() + "<br>" + storeInfo1.Rows[0]["addr1"].ToString().Trim() + "<br>" + storeInfo1.Rows[0]["addr2"].ToString().Trim() + "<br>" +
                       storeInfo1.Rows[0]["addr3"].ToString().Trim() + "<br>" + storeInfo1.Rows[0]["addr4"].ToString().Trim() + (_helperCommonService.CheckForDBNull(storeInfo1.Rows[0]["tel"]) == "" ? "" : "<br>Tel:-" + _helperCommonService.FormatTel(_helperCommonService.CheckForDBNull(storeInfo1.Rows[0]["tel"]))) + "<br>" + CompanyEmail);
            int i = 1;
            foreach (DataRow dr in dt.Rows)
            {
                string acc = dr["acc"].ToString();
                string style = dr["style"].ToString();
                string desc = dr["styledesc"].ToString().Replace("\n", "<br />");
                string name = dr["Accname"].ToString();
                string price = dr["price"].ToString();
                string addr1 = dr["addr"].ToString();
                string addr2 = dr["addr1"].ToString();
                string addrLabel = (addr1.Trim() + " " + addr2).Trim(); // + " " + city.Trim() + " " + state.Trim() + " " + zip.Trim()).Replace("  ", " ").Trim();

                JsonResult imagesResult = objImgController.GetImages(dr["STYLE"].ToString());
                var imagesList = imagesResult.Value as List<string>;
                string StyleImage = "";
                if (imagesList != null && imagesList.Any())
                {
                    StyleImage = "<img src='" + imagesList.First() + "' style='height:80px'>";
                }
                else
                {
                    StyleImage = null;
                }

                desc = "<div style='height:600px;'>" + desc + "</div><div>" + StyleImage + "<br>" + style + "</div>";

                DateTime appdate = _helperCommonService.CheckForDBNull(dr["appdate"], DateTime.Now, typeof(DateTime).ToString());
                if (style != "")
                {
                    htmlContent += "<div class='reportContent1' id='id-" + i + "' style='padding-bottom:20px;'><table style='background-color:#fff;border:1px solid #000;width:100%;'>";
                    htmlContent += "<tr><td width='30%'>" + logo + "</td><td style='text-align:center;'><h1><b>Appraisal</b></h1><br>" + companyaddrLabel + "</td><td width='30%'></td></tr>";
                    //htmlContent += "<tr><td width='200px'></td><td style='text-align:center;'></td><td width='200px' style='text-align:right;'>" + AppraisalDate + "</td></tr>";
                    htmlContent += "<tr><td colspan='3' style='padding:20px;'><b>To Whom It May Concern:</b><br>We herewith certify that we have this day carefully examined the following listed and described articles, the property of :</td></tr>";
                    htmlContent += "<tr><td colspan='3' style='padding:20px;'><b>NAME :" + name + "<br>ADDRESS :" + addrLabel + "</b></td></tr>";
                    //htmlContent += "<tr><td colspan='3'></td></tr>";
                    htmlContent += "<tr><td colspan='3' style='padding:20px;'>We estimate the value as listed for insurance or other purposes at the current retail value excluding Federal and other taxes. In making this Appraisal, we DO NOT agree to purchase or replace the articles.</td></tr>";
                    //htmlContent += "<tr><td colspan='3'></td></tr>";
                    htmlContent += "<tr><td colspan='3' style='padding:20px;'><table width='99%' height='840px'><tr><td style='border:1px solid #000;padding:0px;height: 18pxt;text-align:center;' width='80%'><b>DESCRIPTION</b></td><td style='border:1px solid #000;padding:0px;height: 18px;text-align:center;' width='19%'><b>APPRAISED VALUE</b></td></tr><tr><td style='border:1px solid #000;padding:5px 5px;vertical-align: top;' height='700px'>" + desc + "</td><td style='text-align:right;border:1px solid #000;padding:5px 5px;height:450px;vertical-align: top;'  height='200px'>$" + price + "</td></tr></table></td></tr>";
                    htmlContent += "<tr><td colspan='3' style='padding:20px;'>The foregoing Appraisal is made with the understanding that the Appraiser assumes no liability with respect to any action that may be taken on the basis of this Appraisal.</td></tr>";
                    htmlContent += "<tr><td style='padding:20px;'><b>APPRAISER:" + AppraiserName + "</b></td><td style='text-align:right;'></td><td style='text-align:left;'>" + AppraisalDate + "<br>DATE</td></tr>";
                    htmlContent += "</table></div>";
                    i = i + 1;
                }
            }
            //htmlContent = htmlContent;
            return htmlContent;
        }

        public IActionResult SearchAppraisal()
        {
            ViewBag.CustomerCodes = _helperCommonService.GetAllCustomerCodes();
            return View("~/Views/Appraisal/SearchAppraisal.cshtml");
        }

        public string GetAppraisalDetails(DateTime FROMDATE, DateTime DateTo, string AppraisalNumber, string CustomerCode)
        {
            string sFilter = "1=1";

            if (!string.IsNullOrEmpty(AppraisalNumber))
                sFilter += string.Format(" AND {0} = '{1}'", "ltrim(rtrim(AppraisalId))", _helperCommonService.EscapeSpecialCharacters(AppraisalNumber.Trim()));

            if ((!string.IsNullOrEmpty(AppraisalNumber)) && (!string.IsNullOrEmpty(CustomerCode) && CustomerCode.ToUpper() != "SHOW ALL"))
                sFilter += string.Format(" OR {0} = '{1}'", "acc", _helperCommonService.EscapeSpecialCharacters(CustomerCode));

            if ((string.IsNullOrEmpty(AppraisalNumber) && (!string.IsNullOrEmpty(CustomerCode) && CustomerCode.ToUpper() != "SHOW ALL")))
                sFilter += string.Format(" AND {0} = '{1}'", "acc", _helperCommonService.EscapeSpecialCharacters(CustomerCode));

            sFilter += string.Format(" And ({0} between {1:" + _helperCommonService.GetSeverDateFormat(true) + "} ", "CAST(AppDate as date)", "CAST('" + FROMDATE + "' as date)");
            sFilter += string.Format(" And {1:" + _helperCommonService.GetSeverDateFormat(true) + "} )", "CAST(appdate as date)", "CAST('" + DateTo + "' as date)");
            sFilter += string.Format(" And {0} = {1}", "isquickappraisal", 0);


            DataTable dataTable = _helperCommonService.GetAppraisals(sFilter);
            return JsonConvert.SerializeObject(dataTable);
        }

        public string CheckAppraisalId(string AppId = "")
        {
            string status = "0";

            if (AppId != "")
            {
                DataTable dt = _helperCommonService.GetAppraisal(AppId);
                if (dt.Rows.Count > 0)
                {
                    status = "1";
                }
            }
            var data = new
            {
                invStatus = new[]
                        {
                            new
                            {
                                AppStatus = status
                            }
                        }
            };
            return JsonConvert.SerializeObject(data);
        }

        public IActionResult DeleteAppraisal()
        {
            return View("~/Views/Appraisal/DeleteAppraisal.cshtml");
        }

        public IActionResult GetAllAppraisalDetails()
        {
            DataTable dt = _helperCommonService.GetAndDelAppraisalDetails(0, 0, 0);
            return PartialView("~/Views/Shared/_SearchAppraisalCode.cshtml", dt);
            // return JsonConvert.SerializeObject(dt);
        }

        public string DeleteApprasailId(string AppId)
        {
            int App = Convert.ToInt32(AppId.Trim());
            DataTable dt = _helperCommonService.GetAndDelAppraisalDetails(0, App, 2);
            var data = new
            {
                invStatus = new[]
                        {
                            new
                            {
                                AppStatus = "1"
                            }
                        }
            };
            return JsonConvert.SerializeObject(data);

        }

        public string CheckAppraisalIdForDelete(string AppId = "")
        {
            string status = "0";

            if (AppId != "")
            {
                try
                {
                    int App = Convert.ToInt32(AppId.Trim());
                    DataTable dt = _helperCommonService.GetAndDelAppraisalDetails(App, 0, 1);

                    if (dt.Rows.Count > 0)
                    {
                        status = "1";
                    }
                }
                catch (Exception)
                {
                    status = "0";
                }

            }
            var data = new
            {
                invStatus = new[]
                        {
                            new
                            {
                                AppStatus = status
                            }
                        }
            };
            return JsonConvert.SerializeObject(data);
        }

        public IActionResult AppraisalSignature()
        {
            DataTable upsdata = _helperCommonService.GetUpsInsInformation();
            byte[] fileData = null;

            if (_helperCommonService.DataTableOK(upsdata))
            {
                if (upsdata.Rows[0]["AppraisalSignature"] != DBNull.Value)
                    fileData = (byte[])upsdata.Rows[0]["AppraisalSignature"];

                if (fileData != null && fileData.Length > 0)
                {
                    string imreBase64Data = Convert.ToBase64String(fileData);
                    string imgDataURL = string.Format("data:image/png;base64,{0}", imreBase64Data);
                    //Passing image data in viewbag to view
                    ViewBag.ImageData = imgDataURL;

                }
            }
            return View("~/Views/Appraisal/AppraisalSignature.cshtml");
        }

        [HttpPost]
        public async Task<IActionResult> UploadFiles(List<IFormFile> files)
        {
            if (files == null || files.Count == 0)
                return Json("No files selected.");

            try
            {
                foreach (var file in files)
                {
                    if (file == null || file.Length == 0)
                        continue;

                    byte[] fileData;

                    // Ensure directory exists
                    var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "SCANNED");
                    if (!Directory.Exists(uploadPath))
                        Directory.CreateDirectory(uploadPath);

                    // Get safe filename
                    var fileName = Path.GetFileName(file.FileName);
                    var fullPath = Path.Combine(uploadPath, fileName);

                    // Save file to disk
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    // Read file into byte array
                    fileData = await System.IO.File.ReadAllBytesAsync(fullPath);

                    // Optional: delete file after reading
                    // System.IO.File.Delete(fullPath);

                    string error;
                    if (_helperCommonService.SaveAppraisalSignature(fileData, out error))
                    {
                        return Json("Signature Added");
                    }
                    else
                    {
                        return Json("Signature Added Unsuccessful");
                    }
                }

                return Json("Signature Added");
            }
            catch (Exception ex)
            {
                return Json("Error occurred. Error details: " + ex.Message);
            }
        }


        public IActionResult DeleteSignature()
        {
            string error;
            byte[] fileData = null;
            string status = "Signature Deleted Unsuccessful";
            if (_helperCommonService.SaveAppraisalSignature(fileData, out error))
            {
                status = "Signature Deleted Successful";
            }

            return Json(status);
        }

        public string DeletePaymentType(string payTypes)
        {
            string status = "1";

            ListOfItemsSoldModel objModel = new ListOfItemsSoldModel();
            if (_listOfItemsSoldService.CheckFixed(payTypes))
            {
                status = "This payment type cannot be deleted";
            }

            var data = new
            {
                invStatus = new[]
                        {
                            new
                            {
                                AppStatus = status
                            }
                        }
            };
            return JsonConvert.SerializeObject(data);
        }

        public IActionResult PrintInvoice(string inv_no = "", bool DisableLayout = false)
        {
            ViewBag.StoreData = _helperCommonService.GetAllStoreCodesList();
            bool isNarrowReport = _helperCommonService.CheckModuleEnabled(HelperCommonService.Modules.Narrow_Printer);
            ViewBag.ShowGiftOption = "";
            ViewBag.NarrowOption = "";
            if (isNarrowReport)
            {
                ViewBag.ShowGiftOption = "style='display:none;'";
                ViewBag.NarrowOption = "1";
            }
            ViewBag.inv_no = inv_no;
            ViewBag.DisableLayout = DisableLayout;
            return View("~/Views/Invoice/PrintInvoice.cshtml");
        }

        public string PrintReprintInvoiceData(string Inv_no, string Address, string Image, bool ChkByStyle, bool ChkLatestPayment, bool ChkSms, bool ChkGiftReceipt = false, bool ChkJobbag = false)
        {
            DataTable dtMasterDetail = new DataTable();
            string status = "";
            string printcontnt = "";
            string inv = Inv_no.Replace("'", "");
            DataSet OnInitializeData = new DataSet();
            OnInitializeData = _helperCommonService.OnInitializeData_Combine_SP(_helperCommonService.Pad6(inv), false, false, false, false);

            DataRow invoiceRow = _helperCommonService.DataTableOK(OnInitializeData.Tables[0]) ? OnInitializeData.Tables[0].Rows[0] : null;// this.invoiceService.GetInvoiceByInvNo(_helperCommonService.Pad6(this.txtInvoiceNo.Text));

            if (!string.IsNullOrWhiteSpace(Inv_no) && invoiceRow != null)
            {
                bool is_repair = false;
                string customerAcc = "";
                if (invoiceRow != null)
                {
                    string repString = _helperCommonService.CheckForDBNull(invoiceRow["V_CTL_NO"]);
                    is_repair = (repString.Trim() == "REPAIR");
                    customerAcc = invoiceRow["acc"].ToString();
                }



                string InvoiceNo = _helperCommonService.Pad6(inv);

                bool is_Installment = false;
                decimal installmentInterest = 0;

                if (invoiceRow != null && _helperCommonService.CheckForDBNull(invoiceRow["IS_INSTALLMENT"], "System.Boolean"))
                {
                    is_Installment = true;
                    installmentInterest = _helperCommonService.CheckForDBNull(invoiceRow["EMI_INTERESTAMOUNT"], "System.Decimal");
                }

                DataRow customerRow = _helperCommonService.DataTableOK(OnInitializeData.Tables[1]) ? OnInitializeData.Tables[1].Rows[0] : null;// customerService.CheckValidBillingAcct(invoiceRow["bacc"].ToString());

                if (customerRow == null)
                {
                    status = "Customer Record does not exist.";
                }

                DataTable dtrepcostdata = new DataTable();
                DataTable dtStores;

                DataRow upsInsRow = _helperCommonService.DataTableOK(OnInitializeData.Tables[2]) ? OnInitializeData.Tables[2].Rows[0] : null;// _helperCommonService.GetSqlRow("select top 1 * from ups_ins");
                dtrepcostdata = OnInitializeData.Tables[3];// _helperCommonService.GetpartshistByInvno(txtInvoiceNo.Text);
                dtStores = OnInitializeData.Tables[4];// _helperCommonService.GetStores(_helperCommonService.CheckForDBNull(invoiceRow["store_no"]));

                if (!_helperCommonService.DataTableOK(dtMasterDetail))
                    dtMasterDetail = OnInitializeData.Tables[5];// this.invoiceService.GetInvoiceMasterDetailPO(InvoiceNo, false, _helperCommonService.is_Briony, this.iSVatInclude);

                DataRow drMasterDetail = _helperCommonService.GetRowOne(dtMasterDetail);
                bool ismemo = false;
                string mssag = GetInvoiceMemoNote(upsInsRow, dtStores, is_repair, invoiceRow, ismemo, drMasterDetail);
                DataRow drSalesManRow = _helperCommonService.DataTableOK(OnInitializeData.Tables[6]) ? OnInitializeData.Tables[6].Rows[0] : null;// _helperCommonService.GetSqlRow("select top 1 name,code,email from salesmen where code=@code", "@code", _helperCommonService.CheckForDBNull(invoiceRow["salesman1"]));

                string salesman = "";
                string salesmanemail = "";

                string customeremail = "";
                string register = "";


                if (drSalesManRow != null)
                {
                    salesman = !string.IsNullOrWhiteSpace(_helperCommonService.CheckForDBNull(drSalesManRow["name"])) ? _helperCommonService.CheckForDBNull(drSalesManRow["name"]) : _helperCommonService.CheckForDBNull(drSalesManRow["code"]);
                    salesmanemail = _helperCommonService.CheckForDBNull(drSalesManRow["email"]);
                }

                if (customerRow != null)
                    customeremail = _helperCommonService.CheckForDBNull(customerRow["email"]);

                if (invoiceRow != null)
                    register = _helperCommonService.CheckForDBNull(invoiceRow["register"]);

                printcontnt = PrintInvoiceReportHtml(customerRow, invoiceRow, drMasterDetail, dtMasterDetail, Inv_no, mssag, Address, Image, ChkByStyle, ChkLatestPayment, ChkSms, ChkGiftReceipt, ChkJobbag);
            }
            else
            {
                // printcontnt = "0";
                var data = new
                {
                    invStatus = new[]
                        {
                            new
                            {
                                Page1 = "0"
                            }
                        }
                };
                printcontnt = JsonConvert.SerializeObject(data);
            }

            return printcontnt;
        }

        public string GetInvoiceMemoNote(DataRow upsInsRow, DataTable dtStores, bool is_repair, DataRow invoiceRow, bool ismemo, DataRow drMasterDetail)
        {
            string invoicenote = string.Empty;
            string memonote = string.Empty;
            string fmessage = string.Empty;
            int Copies = 1;

            if (upsInsRow != null)
            {
                invoicenote = _helperCommonService.CheckForDBNull(upsInsRow["invoicenote"]);
                if (is_repair)
                    invoicenote = _helperCommonService.CheckForDBNull(upsInsRow["repair_invoice_note"]);

                memonote = _helperCommonService.CheckForDBNull(upsInsRow["memonote"]);
                if (_helperCommonService.CheckForDBNull(upsInsRow["invoicecopies"], typeof(int).ToString()) > 0)
                    Copies = _helperCommonService.CheckForDBNull(upsInsRow["invoicecopies"], typeof(int).ToString());
            }

            if (invoiceRow != null)
            {
                fmessage = Convert.ToString(invoiceRow["Message"]).Trim();
            }

            string strstoreinvnote = string.Empty;
            if (dtStores.Rows.Count > 0)
                strstoreinvnote = dtStores.Rows[0]["invoicenote"].ToString();
            if (ismemo)
            {
                memonote = !string.IsNullOrWhiteSpace(memonote) ? memonote : _helperCommonService.MemoNote;
                if (!string.IsNullOrWhiteSpace(memonote))
                {
                    fmessage += Environment.NewLine + memonote.Trim();
                }

            }
            else
            {
                invoicenote = !is_repair ? (!string.IsNullOrWhiteSpace(strstoreinvnote) ? strstoreinvnote :
                    _helperCommonService.InvoiceNote) : invoicenote;
                if (drMasterDetail != null)
                    invoicenote = _helperCommonService.CheckForDBNull(drMasterDetail["LAYAWAY"], typeof(bool).ToString()) &&
                        !string.IsNullOrWhiteSpace(_helperCommonService.Layawaynote()) ? _helperCommonService.Layawaynote() : invoicenote;
                if (!string.IsNullOrWhiteSpace(invoicenote))
                    fmessage = invoicenote + Environment.NewLine + fmessage;
            }
            return fmessage;
        }

        public string PrintInvoiceReportHtml(DataRow customerRow, DataRow invoiceRow, DataRow drMasterDetail, DataTable dtMasterDetail, string invNo, string mssag, string Address, string Image, bool ChkByStyle, bool ChkLatestPayment, bool ChkSms, bool ChkGiftReceipt = false, bool ChkJobbag = false)
        {

            decimal currencies = 1;
            string customerAcc = _helperCommonService.CheckForDBNull(invoiceRow["acc"]);
            string date1 = _helperCommonService.CheckForDBNull(invoiceRow["DATE"], "System.DateTime").ToString("MM/dd/yyyy");

            string billingLabel = _helperCommonService.GetAddressLabelShip(customerRow, "<br>", "I");
            string shippingLabel = _helperCommonService.GetAddressLabelFrom_InvoiceTbl(invoiceRow, customerRow, "<br>", false, "I");

            if (Address == "customerShipTo")
            {
                //billingLabel = shippingLabel;
            }

            if (Address == "customerBillTo")
            {
                shippingLabel = billingLabel;
            }

            string sOtherCharges = string.Format("{0:N2}", _helperCommonService.CheckForDBNull(drMasterDetail["add_cost"], typeof(decimal).ToString()));
            string sShipAndHandling = string.Format("{0:N2}", _helperCommonService.CheckForDBNull(drMasterDetail["snh"], typeof(decimal).ToString()));
            string paid = string.Format("{0:N2}", _helperCommonService.CheckForDBNull(drMasterDetail["credits"], typeof(decimal).ToString()));
            string sSubtotal = string.Format("{0:N2}", dtMasterDetail.Compute("SUM(Amount)", "1=1") == DBNull.Value ? 0 : dtMasterDetail.Compute("SUM(Amount)", "1=1"));

            string balancedue = string.Format("{0:N2}", (_helperCommonService.CheckForDBNull(drMasterDetail["gr_total"], typeof(decimal)) -
               _helperCommonService.CheckForDBNull(drMasterDetail["credits"], typeof(decimal))) * currencies, typeof(decimal).ToString());
            string sTradeinAmt = "";
            bool is_repair = false;
            if (is_repair)
                sTradeinAmt = Convert.ToString(_helperCommonService.CheckForDBNull(drMasterDetail["snh"], typeof(decimal)) * currencies);
            else
                sTradeinAmt = Convert.ToString(_helperCommonService.CheckForDBNull(drMasterDetail["tradeinamt"], typeof(decimal)) * currencies);
            string sGrandTotal = Convert.ToString(Math.Round(_helperCommonService.CheckForDBNull(drMasterDetail["gr_total"], typeof(decimal)) * currencies, 2));
            string sSNH = string.Format("{0:0.00}", _helperCommonService.DecimalCheckForDBNull(drMasterDetail["SNH"]));
            string deduction = "";

            deduction = Convert.ToString(_helperCommonService.CheckForDBNull(drMasterDetail["deduction"], typeof(decimal).ToString()));

            string sTerm = string.Empty;
            if (drMasterDetail["is_cod"].ToString().Trim() == "Y")
                sTerm = "COD";
            else if (Convert.ToDecimal(drMasterDetail["term_pct1"]) == 100)
                sTerm = "NET " + drMasterDetail["term1"].ToString().Trim() + " DAYS";
            else
                for (int ctr = 1; ctr <= 8; ctr++)
                    if (Convert.ToDecimal(drMasterDetail["term_pct" + ctr.ToString()]) > 0)
                        sTerm += string.Format("{0},", drMasterDetail["term" + ctr.ToString()].ToString());
            sTerm = sTerm.Trim(new char[] { ',' });

            string CompanyName = _helperCommonService.CompanyName;

            string sSalesTax = Convert.ToString(_helperCommonService.CheckForDBNull(drMasterDetail["sales_tax"], typeof(decimal).ToString()));
            string sSalesTaxRate = Convert.ToString(Math.Round(_helperCommonService.DecimalCheckForDBNull(invoiceRow?["sales_tax_rate"]), 2));
            string sSalesFeeAmount = Convert.ToString(_helperCommonService.CheckForDBNull(drMasterDetail["sales_fee_amount"], typeof(decimal).ToString()));
            sSubtotal = GetsSubtotal(dtMasterDetail, sSalesTax);
            string companyname = "";

            invNo = invNo.Trim().PadLeft(6, ' ');
            bool isreturn = false;
            DataSet dsLoadReport = _helperCommonService.OnLoadReportCombineSp(invNo, isreturn, drMasterDetail != null ? Convert.ToString(drMasterDetail["PON"]).Trim() : "", true, drMasterDetail != null ? Convert.ToString(drMasterDetail["acc"]).Trim() : "", is_repair, drMasterDetail != null ? drMasterDetail["store_no"].ToString() : "", false, false, false, false);
            // DataTable payments = dsLoadReport.Tables[4];
            string rpstoreAddr = _helperCommonService.GetStoreAddressByINovice_store(drMasterDetail["store_no"].ToString(), "<br>", out companyname, false, Convert.ToString(invNo), dsLoadReport.Tables[8]);

            byte[] storeLogo;
            string logo = "";
            storeLogo = _helperCommonService.GetStoreImage(_helperCommonService.DataTableOK(dtMasterDetail) ? Convert.ToString(drMasterDetail["STORE_NO"]) : "");

            string strSalesman = "";
            string Customer_name = "";
            if (_helperCommonService.DataTableOK(drMasterDetail))
            {
                strSalesman = _helperCommonService.CheckForDBNull(drMasterDetail["salesman"]).Trim() + "," +
                    _helperCommonService.CheckForDBNull(drMasterDetail["salesman2"]).Trim() + "," +
                    _helperCommonService.CheckForDBNull(drMasterDetail["salesman3"]).Trim();
                Customer_name = _helperCommonService.CheckForDBNull(drMasterDetail["NAME"], typeof(string).ToString());
                strSalesman = strSalesman.ToString().TrimStart(',').TrimEnd(',').Replace(",,", ",");
            }
            string register = _helperCommonService.CheckForDBNull(invoiceRow["register"]);
            string repairNumber = drMasterDetail["REPAIR_NO"].ToString();

            //string company = _helperCommonService.CompanyWebsite;

            //string logo = "";
            if (storeLogo != null && storeLogo.Length > 0)
            {
                logo = "<img src='data:image/png;base64," + Convert.ToBase64String(storeLogo) + "' width='150px'>";
            }

            string ReportType = "0";

            if (_helperCommonService.CheckForDBNull(drMasterDetail["LAYAWAY"], typeof(bool).ToString()))
                ReportType = "1";
            else if (_helperCommonService.CheckForDBNull(drMasterDetail["IsSpecialItem"], typeof(bool).ToString()) && !_helperCommonService.CheckForDBNull(drMasterDetail["PICKED"], typeof(bool).ToString()))
                ReportType = "2";

            string firstColumn = "Invoice#:";
            string laywayTxt = "";
            if (Convert.ToBoolean(drMasterDetail["IsSpecialItem"]))
            {
                firstColumn = "Special Order:";
            }
            if (Convert.ToBoolean(drMasterDetail["LAYAWAY"]))
            {
                firstColumn = "LAYAWAY:";
                laywayTxt = "LAYAWAY-";
            }


            DataTable dataTable = _helperCommonService.GetSqlData("select * from ups_ins");
            string CompanyEmail = _helperCommonService.CheckForDBNullUPS(dataTable, "Company_email");
            CompanyName = _helperCommonService.CheckForDBNullUPS(dataTable, "COMPANYNAME");

            string showColumn = "display:none;";
            string showTag = "display:none;";
            //string barcod =  "*" + Microsoft.VisualBasic.Strings.trim(invNo) + "*";
            if (!Convert.ToBoolean(drMasterDetail["IsSpecialItem"]) || !Convert.ToBoolean(drMasterDetail["LAYAWAY"]))
            {
                showColumn = "";
            }
            string showImags = "display:none;";
            if (Image == "Images")
            {
                showImags = "";
            }
            else
            {
                showTag = "";
            }

            string showPrices = "";
            if (Image == "gift" || ChkGiftReceipt == true)
            {
                showPrices = "display:none;";
                showTag = "display:none;";
            }
            string reportType = "normal";
            //_helperCommonService.GetDefaultValues();
            bool isNarrowReport = _helperCommonService.CheckModuleEnabled(HelperCommonService.Modules.Narrow_Printer);
            if (isNarrowReport)
                reportType = "narrow";

            string content = "";

            if (reportType == "normal")
            {

                content += "<table style='width:1140px;vertical-align:top;'><tr>";
                content += "<td width='380px' style='text-align:center;'>" + logo + "</td>";
                content += "<td width='380px'><b>" + CompanyName + "<br>" + rpstoreAddr + "<br>" + CompanyEmail + "<b></td>";
                content += "<td width='380px'><table>"
                            + "<tr><td style='padding: 5px 5px;color:red;vertical-align: top;'>" + firstColumn + "</td><td style='padding: 5px 5px;color:red;'>" + invNo + "</td></tr>"
                            + "<tr><td style='padding: 5px 5px;'>CUST CODE :</td><td style='padding: 5px 5px;'>" + customerAcc + "</td></tr>"
                            + "<tr><td style='padding: 5px 5px;'>DATE :</td><td style='padding: 5px 5px;'>" + date1 + "</td></tr>"
                            + "<tr><td style='padding: 5px 5px;'>SALES REP :</td><td style='padding: 5px 5px;'>" + strSalesman + "</td></tr>"
                            + "<tr><td style='padding: 5px 5px;'>REGISTER :</td><td style='padding: 5px 5px;'>" + register + "</td></tr>"
                            + "<tr><td style='padding: 5px 5px;'>PAGE NO. :</td><td style='padding: 5px 5px;'>1</td></tr>"
                            + "</table></td></tr></table>";

                content += "<table><tr><td></td></tr></table><table  style='width:1140px'><tr>"
                            + "<td width='380px' style='padding: 5px 5px;border: 1px solid #000;vertical-align: top;'><table><tr><td style='padding: 5px 5px;background-color:#c6c6c6;' width='300px'><b style='background-color:#c6c6c6;'>Billing Address</b></td></tr><tr><td style='padding: 5px 5px;'>" + billingLabel + "</td></tr></table></td>"
                            + "<td width='380px' style='padding: 5px 5px;border: 1px solid #000;vertical-align: top;'><table><tr><td style='padding: 5px 5px;background-color:#c6c6c6;' width='300px'><b style='background-color:#c6c6c6;'>Shipping Address</b></td></tr><tr><td style='padding: 5px 5px;'>" + shippingLabel + "</td></tr></table></td>"
                            + "<td width='380px' style='padding: 5px 5px;" + showColumn + "'><b>" + laywayTxt + "NOT PICKEDUP</b></td>"
                            + "</tr></table>";

                content += "<table  style='width:1140px;'><tr><td></td></tr><table style='width:1140px;' ><tr style='background-color:#c6c6c6;'>"
                            + "<td width='160px' style='padding: 5px 5px;text-align:center;border: 1px solid #000;background-color:#c6c6c6;'>STYLE</td>"
                            + "<td width='240px' style='padding: 5px 5px;text-align:center;border: 1px solid #000;background-color:#c6c6c6;" + showImags + "'>IMAGE</td>"
                            + "<td width='300px' style='padding: 5px 5px;text-align:center;border: 1px solid #000;background-color:#c6c6c6;'>DESC</td>"
                            + "<td width='100px' style='padding: 5px 5px;text-align:center;border: 1px solid #000;background-color:#c6c6c6;'>QTY</td>"
                            + "<td width='100px' style='padding: 5px 5px;text-align:center;border: 1px solid #000;background-color:#c6c6c6;" + showTag + "'>TAG</td>"
                            + "<td width='100px' style='padding: 5px 5px;text-align:center;border: 1px solid #000;background-color:#c6c6c6;" + showPrices + "'>PRICE</td>"
                            + "<td width='100px' style='padding: 5px 5px;text-align:center;border: 1px solid #000;background-color:#c6c6c6;" + showPrices + "'>AMOUNT</td></tr>";
                decimal totalqty = 0;
                string StyleImage = "";
                foreach (DataRow dr in dtMasterDetail.Rows)
                {

                    JsonResult imagesResult = objImgController.GetImages(dr["STYLE"].ToString(), false);
                    if (dr["STYLE"].ToString() != "")
                    {
                        var imagesList = imagesResult.Value as List<string>;
                        if (imagesList != null && imagesList.Any())
                        {
                            StyleImage = imagesList.First();
                        }
                        else
                        {
                            StyleImage = null;
                            dr["StyleImage"] = "";
                        }

                        totalqty = totalqty + Convert.ToDecimal(dr["QTY"]);
                        content += "<tr>"
                                    + "<td width='160px' style='padding: 5px 5px;text-align:left;'>" + dr["STYLE"] + "</td>"
                                    + "<td width='240px' style='padding: 5px 5px;text-align:center;" + showImags + "'><img src='" + StyleImage + "' height='80px'></td>"
                                    + "<td width='300px' style='padding: 5px 5px;text-align:left;'>" + dr["DESC"] + "</td>"
                                    + "<td width='100px' style='padding: 5px 5px;text-align:right;'>" + dr["QTY"] + "</td>"
                                    + "<td width='100px' style='padding: 5px 5px;text-align:right;" + showTag + "'> $" + string.Format("{0:#,##0.00}", Convert.ToDecimal(dr["PRICE"])) + "</td>"
                                    + "<td width='100px' style='padding: 5px 5px;text-align:right;" + showPrices + "'> $" + string.Format("{0:#,##0.00}", Convert.ToDecimal(dr["PRICE"])) + "</td>"
                                    + "<td width='100px' style='padding: 5px 5px;text-align:right;" + showPrices + "'> $" + string.Format("{0:#,##0.00}", Convert.ToDecimal(dr["AMOUNT"])) + "</td>"
                                    + "</tr>";
                    }
                }

                content += "<tr style='background-color:#c6c6c6;'>"
                                + "<td width='150px' style='padding: 5px 5px;text-align:left;'></td>"
                                + "<td width='150px' style='padding: 5px 5px;text-align:center;" + showImags + "'></td>"
                                + "<td width='150px' style='padding: 5px 5px;text-align:left;'></td>"
                                + "<td width='150px' style='padding: 5px 5px;text-align:right;'>" + totalqty + "</td>"
                                + "<td width='150px' style='padding: 5px 5px;text-align:right;" + showTag + "'></td>"
                                + "<td width='150px' style='padding: 5px 5px;text-align:right;" + showPrices + "'></td>"
                                + "<td width='150px' style='padding: 5px 5px;text-align:right;" + showPrices + "'></td>"
                                + "</tr>";

                content += "</table>";

                content += "<br><table style='width:1140px'><tr><td width='840px' style='padding: 5px 5px;text-align:left;border: 1px solid #000;vertical-align: top;'>" + mssag
                            + "</td><td width='300px' style='padding: 5px 5px;text-align:left;border: 1px solid #000;'><table>"
                            + "<tr><td width='150px' style='padding: 5px 5px;'><b>Subtotal:</b></td><td width='150px' style='padding: 5px 15px;text-align:right;'><b>$ " + string.Format("{0:#,##0.00}", Convert.ToDecimal(sSubtotal)) + "</b></td></tr>"
                            + "<tr><td width='150px' style='padding: 5px 5px;'><b>Trade In:</b></td><td width='150px' style='padding: 5px 15px;text-align:right;'><b>$ " + string.Format("{0:#,##0.00}", Convert.ToDecimal(sTradeinAmt)) + "</b></td></tr>"
                            + "<tr><td width='150px' style='padding: 5px 5px;'><b>Shipping:</b></td><td width='150px' style='padding: 5px 15px;text-align:right;'><b>$ " + string.Format("{0:#,##0.00}", Convert.ToDecimal(sSNH)) + "</b></td></tr>"
                            + "<tr><td width='150px' style='padding: 5px 5px;'><b>Sales Tax:</b></td><td width='150px' style='padding: 5px 15px;text-align:right;'><b>$ " + string.Format("{0:#,##0.00}", Convert.ToDecimal(sSalesTax)) + "</b></td></tr>"
                            + "<tr><td width='150px' style='padding: 5px 5px;'><b>Grand Total:</b></td><td width='150px' style='padding: 5px 15px;text-align:right;'><b>$ " + string.Format("{0:#,##0.00}", Convert.ToDecimal(sGrandTotal)) + "</b></td></tr>"
                            + "<tr><td width='150px' style='padding: 5px 5px;'><b>Paid:</b></td><td width='150px' style='padding: 5px 15px;text-align:right;'><b>$ " + string.Format("{0:#,##0.00}", Convert.ToDecimal(paid)) + "</b></td></tr>"
                            + "<tr><td width='150px' style='padding: 5px 5px;'><b>Balance Due:</b></td><td width='150px' style='padding: 5px 15px;text-align:right;'><b>$ " + string.Format("{0:#,##0.00}", Convert.ToDecimal(balancedue)) + "</b></td></tr>"
                            + "</table></td></tr></table>";
                content += "<br><br>";
                DataTable payments = dsLoadReport.Tables[4];
                DataTable payments1 = GetPayCount(invNo);

                content += "<table style='width:400px;border: 1px solid #000;'>"
                        + "<tr><td colspan='4' width='400px' style='padding: 5px 5px;text-align: center;'><b>Payments</b></td></tr>"
                        + "<tr style='background-color:#c6c6c6;'><td width='100px' style='padding: 5px 5px;text-align:left;border: 1px solid #000;'>Date</td>"
                        + "<td width='100px' style='padding: 5px 5px;text-align:left;border: 1px solid #000;'>Paymthod</td>"
                        + "<td width='100px' style='padding: 5px 5px;text-align:left;border: 1px solid #000;'>Amount In$</td>"
                        + "<td width='100px' style='padding: 5px 5px;text-align:left;border: 1px solid #000;'>Notes</td></tr>";

                foreach (DataRow dr in payments.Rows)
                    content += "<tr><td style='padding: 5px 5px;'>" + _helperCommonService.CheckForDBNull(dr["DATE"], "System.DateTime").ToString("MM/dd/yyyy") + "</td><td style='padding: 5px 5px;'>" + dr["METHOD"] + "</td><td style='padding: 5px 15px;text-align:right;'>" + string.Format("{0:#,##0.00}", Convert.ToDecimal(dr["AMOUNT"])) + "</td><td style='padding: 5px 5px;'>" + dr["NOTE"] + "</td></tr>";
                content += "</table>";
            }
            else if (reportType == "narrow" && Image != "Images")
            {
                content += "<table><tr><td width='300px'>";

                content += "<table>";
                content += "<tr><td width='300px' style='text-align:center;'>" + logo + "</td></tr>";
                content += "<tr><td width='300px' style='text-align:center;'><b>" + CompanyName + "<br>" + rpstoreAddr + "<br>" + CompanyEmail + "</b></td></tr>";

                content += "<tr><td><table>";
                content += "<tr><td style='padding: 5px 5px;color:red;vertical-align: top;' width='150px'>" + firstColumn + "</td><td  width='150px' style='padding: 5px 5px;color:red;vertical-align: top;'>" + invNo + "</td></tr>";
                content += "<tr><td style='padding: 5px 5px;vertical-align: top;' width='150px'>CUST. CODE :</td><td  width='150px' style='padding: 5px 5px;vertical-align: top;'>" + customerAcc + "</td></tr>";
                content += "<tr><td style='padding: 5px 5px;vertical-align: top;' width='150px'>DATE :</td><td  width='150px' style='padding: 5px 5px;vertical-align: top;'>" + date1 + "</td></tr>";
                content += "<tr><td style='padding: 5px 5px;vertical-align: top;' width='150px'>SALES REP :</td><td  width='150px' style='padding: 5px 5px;vertical-align: top;'>" + strSalesman + "</td></tr>";
                content += "<tr><td style='padding: 5px 5px;vertical-align: top;' width='150px'>Customer Address :</td><td  width='150px' style='padding: 5px 5px;vertical-align: top;'>" + billingLabel + "</td></tr>";

                content += "<tr><td colspan='2' width='300px'>";
                content += "<table>";
                content += "<tr><td style='padding: 5px 5px;vertical-align: top;text-align:center;' width='100px'><b>STYLE</b></td><td  width='100px' style='padding: 5px 5px;vertical-align: top;text-align:center;'><b>QTY</b></td><td  width='100px' style='padding: 5px 5px;vertical-align: top;text-align:center;" + showPrices + "'><b>PRICE EACH</b></td></tr>";

                decimal totalqty = 0;
                decimal totalPrice = 0;
                foreach (DataRow dr in dtMasterDetail.Rows)
                {
                    if (dr["STYLE"].ToString() != "")
                    {
                        totalqty = totalqty + Convert.ToDecimal(dr["QTY"]);
                        totalPrice = totalPrice + Convert.ToDecimal(dr["AMOUNT"]);

                        content += "<tr>"
                                    + "<td width='100px' style='padding: 5px 5px;text-align:left;'>" + dr["STYLE"] + "</td>"
                                    + "<td width='100px' style='padding: 5px 5px;text-align:right;'>" + dr["QTY"] + "</td>"
                                    + "<td width='100px' style='padding: 5px 5px;text-align:right;" + showPrices + "'> $" + string.Format("{0:#,##0.00}", Convert.ToDecimal(dr["AMOUNT"])) + "</td>"
                                    + "</tr>";

                        content += "<tr><td colspan='3' style='padding: 5px 5px;text-align:left;'>" + dr["DESC"] + "</td></tr>";
                    }
                }
                content += "<tr><td width='100px' style='padding: 5px 5px;text-align:left;'><b>Total:</b></td><td width='100px' style='padding: 5px 5px;text-align:right;'>" + totalqty + "</td><td width='100px' style='padding: 5px 5px;text-align:right;" + showPrices + "'>" + string.Format("{0:#,##0.00}", totalPrice) + "</td></tr>";

                content += "</table>";
                content += "</td></tr>";

                content += "<!--<tr><td colspan='3' style='padding: 5px 5px;'><table><tr><td width='150px' style='padding: 5px 5px;text-align:left;'>Trade In :</td><td width='150px' style='padding: 5px 5px;text-align:right;' >$ " + string.Format("{0:#,##0.00}", Convert.ToDecimal(sTradeinAmt)) + "</td></tr></table></td></tr>-->";
                content += "<tr><td colspan='3' style='padding: 5px 5px;" + showPrices + "'><table><tr><td width='150px' style='padding: 5px 5px;text-align:left;'>Discount :</td><td width='150px' style='padding: 5px 5px;text-align:right;' >$ " + string.Format("{0:#,##0.00}", Convert.ToDecimal(deduction)) + "</td></tr></table></td></tr>";
                content += "<tr><td colspan='3' style='padding: 5px 5px;" + showPrices + "'><table><tr><td width='150px' style='padding: 5px 5px;text-align:left;'>Sales Tax :</td><td width='150px' style='padding: 5px 5px;text-align:right;' >$ " + string.Format("{0:#,##0.00}", Convert.ToDecimal(sSalesTax)) + "</td></tr></table></td></tr>";
                content += "<tr><td colspan='3' style='padding: 5px 5px;" + showPrices + "'><table><tr><td width='150px' style='padding: 5px 5px;text-align:left;'><b>Grand Total:</b></td><td width='150px' style='padding: 5px 5px;text-align:right;' ><b>$ " + string.Format("{0:#,##0.00}", Convert.ToDecimal(sGrandTotal)) + "</b></td></tr></table></td></tr>";
                content += "<tr><td colspan='3' style='padding: 5px 5px;" + showPrices + "'><table><tr><td width='150px' style='padding: 5px 5px;text-align:left;'><b>Paid :</b></td><td width='150px' style='padding: 5px 5px;text-align:right;' ><b>$ " + string.Format("{0:#,##0.00}", Convert.ToDecimal(paid)) + "</b></td></tr></table></td></tr>";
                content += "<tr><td colspan='3' style='padding: 5px 5px;" + showPrices + "'><table><tr><td width='150px' style='padding: 5px 5px;text-align:left;'><b>Balance : </b></td><td width='150px' style='padding: 5px 5px;text-align:right;' ><b>$ " + string.Format("{0:#,##0.00}", Convert.ToDecimal(balancedue)) + "</b></td></tr></table></td></tr>";

                content += "<tr><td colspan='3' style='padding: 5px 5px;" + showPrices + "'><table><tr><td width='150px' style='padding: 5px 5px;text-align:center;' colspan='2'><b>Payments </b></td></tr></table></td></tr>";
                DataTable payments1 = GetPayCount(invNo);
                if (payments1.Rows.Count > 0)
                {
                    foreach (DataRow dr in payments1.Rows)
                        content += "<tr><td colspan='3' style='padding: 5px 5px;'><table><tr><td style='padding: 5px 5px;' width='100px'>" + _helperCommonService.CheckForDBNull(dr["DATE"], "System.DateTime").ToString("MM/dd/yyyy") + "</td><td style='padding: 5px 5px;' width='100px'>" + dr["METHOD"] + "</td><td style='padding: 5px 15px;text-align:right;' width='100px'>" + string.Format("{0:#,##0.00}", Convert.ToDecimal(dr["AMOUNT"])) + "</td></tr></table></td></tr>";
                }
                content += "<tr><td colspan='3' style='padding: 5px 5px;'>" + mssag + "</td></tr>";
                content += "</table></td></tr>" +
                    "</td><td width='200px'></td><td width='200px'></td></tr></table>";
            }
            else if (reportType == "narrow" && Image == "Images")
            {
                if (Image == "Images")
                {
                    showImags = "";
                    showTag = "";
                    showPrices = "";
                    reportType = "normal";
                }
                content += "<table style='width:800px;vertical-align:top;'><tr><td width='200px' style='text-align:center;'>" + logo + "</td>";
                content += "<td width='300px' style='text-align:center;'><b>" + CompanyName + "<br>" + rpstoreAddr + "<br>" + CompanyEmail + "<b></td>";
                content += "<td width='300px'><table>"
                            + "<tr><td style='padding: 5px 5px;color:red;vertical-align: top;'>" + firstColumn + "</td><td style='padding: 5px 5px;color:red;'>" + invNo + "</td></tr>"
                            + "<tr><td style='padding: 5px 5px;'>CUST CODE :</td><td style='padding: 5px 5px;'>" + customerAcc + "</td></tr>"
                            + "<tr><td style='padding: 5px 5px;'>DATE :</td><td style='padding: 5px 5px;'>" + date1 + "</td></tr>"
                            + "<tr><td style='padding: 5px 5px;'>SALES REP :</td><td style='padding: 5px 5px;'>" + strSalesman + "</td></tr>"
                            + "<tr><td style='padding: 5px 5px;'>REGISTER :</td><td style='padding: 5px 5px;'>" + register + "</td></tr>"
                            + "<tr><td style='padding: 5px 5px;'>PAGE NO. :</td><td style='padding: 5px 5px;'>1</td></tr>"
                            + "</table></td></tr></table>";

                content += "<br><table  style='width:800px'><tr>"
                            + "<td width='200px' style='padding: 5px 5px;display:none;'><b style='display:none;'>" + laywayTxt + "NOT PICKEDUP</b></td>"
                            + "<td width='300px' style='padding: 5px 5px;border: 0px solid #000;vertical-align: top;'><table><tr><td style='padding: 5px 5px;background-color:#c6c6c6;display:none;'><b>Billing Address</b></td></tr><tr><td style='padding: 5px 5px;display:none;'>" + billingLabel + "</td></tr></table></td>"
                            + "<td width='300px' style='padding: 5px 5px;border: 1px solid #000;vertical-align: top;'><table><tr><td style='padding: 5px 5px;background-color:#c6c6c6;'><b>Customer Address</b></td></tr><tr><td style='padding: 5px 5px;'>" + billingLabel + "</td></tr></table></td>"
                            + "</tr></table>";

                content += "<br><table style='width:800px;' ><tr style='background-color:#c6c6c6;'>"
                            + "<td width='100px' style='padding: 5px 5px;text-align:center;border: 1px solid #000;'>STYLE</td>"
                            + "<td width='100px' style='padding: 5px 5px;text-align:center;border: 1px solid #000;" + showImags + "'>IMAGE</td>"
                            + "<td width='200px' style='padding: 5px 5px;text-align:center;border: 1px solid #000;'>DESC</td>"
                            + "<td width='60px' style='padding: 5px 5px;text-align:center;border: 1px solid #000;'>QTY</td>"
                            + "<td width='60px' style='padding: 5px 5px;text-align:center;border: 1px solid #000;" + showTag + "'>TAG</td>"
                            + "<td width='60px' style='padding: 5px 5px;text-align:center;border: 1px solid #000;" + showPrices + "'>PRICE</td>"
                            + "<td width='70px' style='padding: 5px 5px;text-align:center;border: 1px solid #000;" + showPrices + "'>AMOUNT</td></tr>";
                decimal totalqty = 0;
                string StyleImage = "";
                foreach (DataRow dr in dtMasterDetail.Rows)
                {
                    JsonResult imagesResult = objImgController.GetImages(dr["STYLE"].ToString(), false);
                    if (dr["STYLE"].ToString() != "")
                    {
                        var imagesList = imagesResult.Value as List<string>;
                        if (imagesList != null && imagesList.Any())
                        {
                            StyleImage = imagesList.First();
                        }
                        else
                        {
                            StyleImage = null;
                            dr["StyleImage"] = "";
                        }



                        totalqty = totalqty + Convert.ToDecimal(dr["QTY"]);
                        content += "<tr>"
                                    + "<td width='150px' style='padding: 5px 5px;text-align:left;'>" + dr["STYLE"] + "</td>"
                                    + "<td width='150px' style='padding: 5px 5px;text-align:center;" + showImags + "'><img src='" + StyleImage + "' height='80px'></td>"
                                    + "<td width='150px' style='padding: 5px 5px;text-align:left;'>" + dr["DESC"] + "</td>"
                                    + "<td width='150px' style='padding: 5px 5px;text-align:right;'>" + dr["QTY"] + "</td>"
                                    + "<td width='150px' style='padding: 5px 5px;text-align:right;" + showTag + "'> $" + string.Format("{0:#,##0.00}", Convert.ToDecimal(dr["PRICE"])) + "</td>"
                                    + "<td width='150px' style='padding: 5px 5px;text-align:right;" + showPrices + "'> $" + string.Format("{0:#,##0.00}", Convert.ToDecimal(dr["PRICE"])) + "</td>"
                                    + "<td width='150px' style='padding: 5px 5px;text-align:right;" + showPrices + "'> $" + string.Format("{0:#,##0.00}", Convert.ToDecimal(dr["AMOUNT"])) + "</td>"
                                    + "</tr>";
                    }
                }

                content += "<tr style='background-color:#c6c6c6;'>"
                                + "<td width='150px' style='padding: 5px 5px;text-align:left;'></td>"
                                + "<td width='150px' style='padding: 5px 5px;text-align:center;" + showImags + "'></td>"
                                + "<td width='150px' style='padding: 5px 5px;text-align:left;'></td>"
                                + "<td width='150px' style='padding: 5px 5px;text-align:right;'>" + totalqty + "</td>"
                                + "<td width='150px' style='padding: 5px 5px;text-align:right;" + showTag + "'></td>"
                                + "<td width='150px' style='padding: 5px 5px;text-align:right;" + showPrices + "'></td>"
                                + "<td width='150px' style='padding: 5px 5px;text-align:right;" + showPrices + "'></td>"
                                + "</tr>";

                content += "</table>";

                content += "<br><table style='width:800px'><tr><td width='500px' style='padding: 5px 5px;text-align:left;border: 1px solid #000;vertical-align: top;'>" + mssag
                            + "</td><td width='300px' style='padding: 5px 5px;text-align:left;border: 1px solid #000;'><table>"
                            + "<tr><td width='150px' style='padding: 5px 5px;'><b>Subtotal:</b></td><td width='150px' style='padding: 5px 15px;text-align:right;'><b>$ " + string.Format("{0:#,##0.00}", Convert.ToDecimal(sSubtotal)) + "</b></td></tr>"
                            + "<tr><td width='150px' style='padding: 5px 5px;'><b>Trade In:</b></td><td width='150px' style='padding: 5px 15px;text-align:right;'><b>$ " + string.Format("{0:#,##0.00}", Convert.ToDecimal(sTradeinAmt)) + "</b></td></tr>"
                            + "<tr><td width='150px' style='padding: 5px 5px;'><b>Sales Tax:</b></td><td width='150px' style='padding: 5px 15px;text-align:right;'><b>$ " + string.Format("{0:#,##0.00}", Convert.ToDecimal(sSalesTax)) + "</b></td></tr>"
                            + "<tr><td width='150px' style='padding: 5px 5px;'><b>Grand Total:</b></td><td width='150px' style='padding: 5px 15px;text-align:right;'><b>$ " + string.Format("{0:#,##0.00}", Convert.ToDecimal(sGrandTotal)) + "</b></td></tr>"
                            + "<tr><td width='150px' style='padding: 5px 5px;'><b>Paid:</b></td><td width='150px' style='padding: 5px 15px;text-align:right;'><b>$ " + string.Format("{0:#,##0.00}", Convert.ToDecimal(paid)) + "</b></td></tr>"
                            + "<tr><td width='150px' style='padding: 5px 5px;'><b>Balance Due:</b></td><td width='150px' style='padding: 5px 15px;text-align:right;'><b>$ " + string.Format("{0:#,##0.00}", Convert.ToDecimal(balancedue)) + "</b></td></tr>"
                            + "</table></td></tr></table>";
                content += "<br><br>";
                DataTable payments = dsLoadReport.Tables[4];
                DataTable payments1 = GetPayCount(invNo);

                content += "<table style='width:400px;border: 1px solid #000;'>"
                        + "<tr><td colspan='4' width='400px' style='padding: 5px 5px;text-align: center;'><b>Payments</b></td></tr>"
                        + "<tr style='background-color:#c6c6c6;'><td width='100px' style='padding: 5px 5px;text-align:left;border: 1px solid #000;'>Date</td>"
                        + "<td width='100px' style='padding: 5px 5px;text-align:left;border: 1px solid #000;'>Paymthod</td>"
                        + "<td width='100px' style='padding: 5px 5px;text-align:left;border: 1px solid #000;'>Amount In$</td>"
                        + "<td width='100px' style='padding: 5px 5px;text-align:left;border: 1px solid #000;'>Notes</td></tr>";

                foreach (DataRow dr in payments.Rows)
                {
                    content += "<tr><td style='padding: 5px 5px;'>" + _helperCommonService.CheckForDBNull(dr["DATE"], "System.DateTime").ToString("MM/dd/yyyy") + "</td><td style='padding: 5px 5px;'>" + dr["METHOD"] + "</td><td style='padding: 5px 15px;text-align:right;'>" + string.Format("{0:#,##0.00}", Convert.ToDecimal(dr["AMOUNT"])) + "</td><td style='padding: 5px 5px;'>" + dr["NOTE"] + "</td></tr>";
                }

                content += "</table>";
            }
            ChkJobbag = checkJobBagCount(invNo);

            var data = new
            {
                invStatus = new[]
                        {
                            new
                            {
                                Page1 = content,
                                Page2 = ChkJobbag,
                                inv_no = invNo
                            }
                        }
            };
            return JsonConvert.SerializeObject(data);
        }

        private DataTable GetPayCount(string InvoiceNo)
        {
            return _helperCommonService.GetInvoicePayments(InvoiceNo);
        }

        private bool checkJobBagCount(string InvoiceNo)
        {
            DataTable dtJobBag = _helperCommonService.GetJobOfInvoice(InvoiceNo);
            return (dtJobBag.Rows.Count > 0);
        }

        public IActionResult PrintJobBag(string InvoiceNo)
        {
            DataTable dtJobBag = _helperCommonService.GetJobOfInvoice(InvoiceNo);
            foreach (DataRow jobbag in dtJobBag.Rows)
            {
                if (String.IsNullOrEmpty(Convert.ToString(jobbag["repair_no"])))
                    continue;
                string data = PrintJobbag1(Convert.ToString(jobbag["repair_no"]), InvoiceNo);
                ViewBag.htmlResults = data;

            }
            return View("~/Views/Analysis/PrintJobBag.cshtml");
        }

        private string PrintJobbag1(string jobbag, string InvoiceNo)
        {

            string jbNumb = jobbag.Trim().PadLeft(6, '0');
            string repairjobbag = _helperCommonService.GetRepairIobBag(jbNumb);
            DataTable dtJobbaginfo = _helperCommonService.Reprintjobbag(repairjobbag, true, jobbag.Trim());
            string data = ShowJobBag(repairjobbag, "", jobbag.Trim(), InvoiceNo);
            return data;

        }

        private string ShowJobBag(string JobBagNo = "", string printOption = "", string claimRepairNo = "", string frminvno = "")
        {
            bool iSNoPromisDate = false;
            string content = "";
            string STYLE1 = "";

            iSNoPromisDate = _helperCommonService.CheckModuleEnabled(HelperCommonService.Modules.NoPromiseDate);
            if (JobBagNo != "")
            {
                string addrLabelShip = "", topAddess = "", Saleman = "", ODate = "", RDate = "";
                string jbagno = JobBagNo;
                DataTable jobbaginfo = null;
                DataTable data;
                string sentRepairNo = "";
                jobbaginfo = _helperCommonService.Reprintjobbag(jbagno, true, "");
                bool IsNarrowReport = _helperCommonService.CheckModuleEnabled(HelperCommonService.Modules.Narrow_Printer);
                string date = "", prmdate = "", salman = "", acc = "", custname = "", note = "", STYLE = "",
                       custtel = "", custEmail = "", inv_no = string.Empty, LongNote1 = string.Empty, LongNote2 = string.Empty, strBArcode = "", Certnumber = "", Certtyp = "",
                       isRUSH = string.Empty, ShipAdd = "", iShip_By = "", iSurprise = "";
                decimal qty = 0, price = 0.00M, price1 = 0.00M, Total = 0, DEPOSIT = 0, SALES_TAX = 0, salesFeeAmount = 0,
                       BalanceDue = 0, deduction = 0;
                if (_helperCommonService.DataTableOK(jobbaginfo))
                {
                    string RepairNo = jbagno;
                    if (jobbaginfo.AsEnumerable().OfType<DataRow>().Where(row => _helperCommonService.Pad6(Convert.ToString(row.Field<string>("REPAIR_NO"))).Contains(sentRepairNo.ToUpper()) && _helperCommonService.Pad6(Convert.ToString(row.Field<string>("barcode"))).Contains(jbagno)).ToList().Count > 0)
                    {
                        jobbaginfo = jobbaginfo.AsEnumerable().OfType<DataRow>().Where(row => _helperCommonService.Pad6(Convert.ToString(row.Field<string>("REPAIR_NO"))).Contains(sentRepairNo.ToUpper()) && _helperCommonService.Pad6(Convert.ToString(row.Field<string>("barcode"))).Contains(jbagno)).ToList().CopyToDataTable();
                    }
                    else
                        RepairNo = RepairNo.PadLeft(7, ' ');

                    bool issplitjobbag = false;
                    if (issplitjobbag)
                        RepairNo = jbagno;


                    bool _Is_picked = false;
                    string _ShipBy = "", Shiporpickup = "";
                    string Repno = _helperCommonService.GetValue(jobbaginfo, "REPAIR_NO");
                    DataTable dtstyl = _helperCommonService.GetJbbagCustdetail(frminvno, "Rep");
                    STYLE1 = _helperCommonService.GetValue(dtstyl, "Style");
                    string[] fldNames = { "date", "can_date" };
                    jobbaginfo = _helperCommonService.GetSysFormattedDateOnReport(jobbaginfo, fldNames);

                    foreach (DataRow dr in jobbaginfo.Rows)
                    {
                        price1 = dr["price"] != DBNull.Value ? Convert.ToDecimal(dr["price"].ToString()) : 0;
                        qty += _helperCommonService.DecimalCheckForDBNull(dr["qty"]);
                        price += price1;
                        date = dr["date"].ToString();
                        prmdate = dr["can_date"].ToString();
                        salman = dr["operator"].ToString();
                        acc = dr["acc"].ToString();
                        custname = dr["custname"].ToString();
                        note = dr["note"].ToString();
                        STYLE = dr["style"].ToString();
                        custtel = dr["tel"].ToString();
                        custEmail = dr["Email"].ToString();
                        LongNote1 = _helperCommonService.CheckForDBNull(dr["longnote"]).ToString();
                        LongNote2 = _helperCommonService.CheckForDBNull(dr["longnote1"]).ToString();

                        if (note.Contains("*RUSH*"))
                            isRUSH = "RUSH";
                        Total = Convert.ToDecimal(_helperCommonService.DecimalCheckForNullDBNull(dr["Total"].ToString()));
                        SALES_TAX = Convert.ToDecimal(_helperCommonService.DecimalCheckForNullDBNull(dr["SALES_TAX"].ToString()));
                        DEPOSIT = Convert.ToDecimal(_helperCommonService.DecimalCheckForNullDBNull(dr["DEPOSIT"].ToString()));
                        BalanceDue = Convert.ToString(dr["BalanceDue"]) == string.Empty ? Convert.ToDecimal(_helperCommonService.DecimalCheckForNullDBNull(dr["BalanceDue"].ToString())) : _helperCommonService.DecimalCheckForDBNull(dr["BalanceDue1"]);
                        inv_no = dr["Invoice"].ToString();
                        deduction = Convert.ToDecimal(_helperCommonService.DecimalCheckForNullDBNull(dr["Deduction"]));
                        salesFeeAmount = _helperCommonService.DecimalCheckForDBNull(dr["Sales_Fee_Amount"]);
                    }

                    DataTable details = creatdatagridbasedonrepid(RepairNo, issplitjobbag);

                    string combineNote = "", combineRepNote = "", ItemAsStyle = "", size = "";
                    decimal tltprice = 0;
                    string Jeweler_note = "";
                    DataTable envelopedetails = null;

                    foreach (DataRow row in details.Rows)
                    {
                        string style = row["ITEM"].ToString();
                        size = row["size"].ToString();
                        //envelopedetails = orderrepairService.RepairOrderEnvolapes(this.jobbagno.Text, style, size);
                        combineNote += row["note"].ToString() + Environment.NewLine;
                        ItemAsStyle += row["ITEM"].ToString() + Environment.NewLine;
                        tltprice += Math.Round(Convert.ToDecimal(_helperCommonService.DecimalCheckForNullDBNull(row["price"])), 2);
                        combineRepNote += row["REPAIR_NOTE"].ToString() + Environment.NewLine;
                        Jeweler_note = row["jeweler_note"].ToString();
                    }

                    data = _helperCommonService.GetOrderRepairData(JobBagNo.TrimStart('0'));
                    decimal invoice_balance = 0;
                    string billingLabel, shippingLabel, relatedInvoice = "", repairStore = string.Empty, Size1 = string.Empty;

                    DataTable dtrep = _helperCommonService.GetSizeforjobbag(jbagno);
                    if (_helperCommonService.DataTableOK(data))
                    {
                        relatedInvoice = _helperCommonService.CheckForDBNull(data.Rows[0]["acc"]);
                        shippingLabel = _helperCommonService.GetAddressLabel(relatedInvoice, "\n");
                        billingLabel = _helperCommonService.GetAddressLabel(relatedInvoice, "\n");
                        invoice_balance = _helperCommonService.CheckForDBNull(data.Rows[0]["balance"], 0, typeof(decimal).ToString());
                        repairStore = _helperCommonService.CheckForDBNull(data.Rows[0]["Store"]);
                    }

                    bool donotShowImageSR = false;
                    DataTable imgTable = _helperCommonService.GetRepImgNames("REP" + JobBagNo);
                    if (!_helperCommonService.DataTableOK(imgTable))
                    {
                        int jbNumber = 0;
                        int.TryParse(jbagno.Trim(), out jbNumber);
                        imgTable = _helperCommonService.GetRepImgNames("REP" + jbNumber.ToString());
                        if (!_helperCommonService.DataTableOK(imgTable))
                        {
                            string bagno = (jobbaginfo != null && jobbaginfo.Columns.Contains("REPAIR_NO")) ? "REP" + Convert.ToString(jobbaginfo.Rows[0]["REPAIR_NO"]) : "REP" + JobBagNo.Trim();
                            imgTable = _helperCommonService.GetRepImgNames(bagno);
                        }
                    }
                    DataTable dtSubReport2 = new DataTable();
                    dtSubReport2.TableName = "GetImage";
                    dtSubReport2.Columns.Add("RowId", typeof(int));
                    dtSubReport2.Columns.Add("ColId", typeof(int));
                    dtSubReport2.Columns.Add("Imagename", typeof(string));
                    dtSubReport2.Columns.Add("Style", typeof(string));
                    int ImagesPerRow = 6;
                    decimal rowctr = 1;
                    int Cntr = 1;
                    if (_helperCommonService.DataTableOK(imgTable))
                    {
                        foreach (DataRow row in imgTable.Rows)
                        {
                            if (Cntr == 4)
                                break;
                            if (_helperCommonService.GetRepImage(_helperCommonService.CheckForDBNull(row["Style"]), _helperCommonService.CheckForDBNull(row["DESCRIPTION"])) != "File:")
                            {
                                dtSubReport2.Rows.Add(Math.Ceiling(rowctr / ImagesPerRow), rowctr % ImagesPerRow == 0 ? ImagesPerRow : rowctr % ImagesPerRow, _helperCommonService.GetRepImage(row["Style"].ToString(), row["DESCRIPTION"].ToString()), "");
                                rowctr++;
                                Cntr++;
                            }
                            else
                            {
                                dtSubReport2.Rows.Add(1, 1, "", "");
                                donotShowImageSR = true;
                            }
                        }
                    }
                    else
                    {
                        dtSubReport2.Rows.Add(1, 1, "", "");
                        donotShowImageSR = true;
                    }
                }
                bool donotShowParts = false;
                DataTable dtParts = _helperCommonService.GetReservePartsOnJobbag(JobBagNo);
                if (!_helperCommonService.DataTableOK(dtParts))
                    donotShowParts = true;

                string companyname;
                data = _helperCommonService.GetOrderRepairData(JobBagNo.TrimStart('0'));
                string promised = _helperCommonService.DataTableOK(data) && Convert.ToString(data.Rows[0]["can_date"]) != string.Empty ? Convert.ToString(data.Rows[0]["can_date"]) : "";
                if (promised != "")
                {
                    promised = _helperCommonService.CheckForDBNull(data.Rows[0]["can_date"], "System.DateTime").ToString("MM/dd/yyyy");
                }
                string c = _helperCommonService.DataTableOK(data) && Convert.ToString(data.Rows[0]["Store"]) != string.Empty ? Convert.ToString(data.Rows[0]["Store"]) : "";
                string rpaddr1 = _helperCommonService.GetStoreAddress(c, "\n", out companyname);
                string Repno1 = _helperCommonService.GetValue(jobbaginfo, "REPAIR_NO");

                bool isNoCost, IsModEstimateOnJOb;
                isNoCost = false;
                IsModEstimateOnJOb = false;
                string ctl = custname.Trim() == "" || custtel.Trim() == "0" ? "" : "Tel: " + custtel;
                byte[] img;
                if (_helperCommonService.CompanyName != null && _helperCommonService.CompanyName.ToUpper().Contains("CRISSON"))
                    img = _helperCommonService.GetStoreImage(_helperCommonService.GetValue(jobbaginfo, "STORE"));

                string barcode = "";
                if (_helperCommonService.DataTableOK(data) && data.Rows[0]["BARCODE"].ToString() != "")
                    barcode = "Job Bag#: " + data.Rows[0]["BARCODE"].ToString();

                content = "<table><tr><td width='420px'><table>";
                content = content + "<tr><td style='text-align:center;' width='420px' colspan='2'><b>SHOP JOB BAG</b></td></tr>";
                content = content + "<tr><td style='text-align:left;font-size: 14px' width='210px'>Cust: " + custname + "</td><td style='text-align:left;font-size: 14px' width='210px'>Promised: " + promised + "</td></tr>";
                content = content + "<tr><td style='text-align:left;font-size: 14px' width='210px'>Customer Code: " + acc + "</td><td style='text-align:left;font-size: 14px' width='210px'>From Store: " + companyname + "</td></tr>";
                content = content + "<tr><td style='text-align:left;font-size: 14px' width='210px'>" + ctl + "</td><td style='text-align:left;font-size: 14px' width='210px'>Salesrep: " + salman + "</td></tr>";
                content = content + "<tr><td style='text-align:left;font-size: 14px' width='210px'>Order Date : " + date + "</td><td style='text-align:left;font-size: 14px' width='210px'>Claim #: " + Repno1 + "</td></tr>";
                content = content + "<tr><td style='text-align:left;font-size: 14px' width='210px'>" + barcode + "</td><td style='text-align:left;font-size: 14px' width='210px'></td></tr>";
                content = content + "</table></td></tr></table>";

                if (_helperCommonService.DataTableOK(data) && data.Rows.Count > 0)
                {
                    content = content + "<table>";
                    content = content + "<tr><td width='60px' style='border:1px solid #dddddd;font-size:12px;'><b>Code</b></td><td width='140px' style='border:1px solid #dddddd;font-size:12px;'><b>Description</b></td><td width='60px' style='border:1px solid #dddddd;font-size:12px;'><b>SIZE</b></td><td width='60px' style='border:1px solid #dddddd;font-size:12px;text-align:right;'><b>Qty</b></td><td width='100px' style='border:1px solid #dddddd;font-size:12px;text-align:right;'><b>Amount</b></td></tr>";
                    foreach (DataRow dr in data.Rows)
                    {
                        decimal tt = Convert.ToDecimal(dr["QTY"]) * Convert.ToDecimal(dr["PRICE"]);

                        content = content + "<tr><td width='60px' style='border:1px solid #dddddd;font-size:12px;'>" + dr["ITEM"].ToString() + "</td><td width='140px' style='border:1px solid #dddddd;font-size:12px;'>"
                                          + dr["NOTE"].ToString() + "</td><td width='60px' style='border:1px solid #dddddd;font-size:12px;'>"
                                          + dr["SIZE"].ToString() + "</td><td width='60px' style='border:1px solid #dddddd;font-size:12px;text-align:right;'>"
                                          + dr["QTY"].ToString() + "</td><td width='100px' style='border:1px solid #dddddd;font-size:12px;text-align:right;'>" + tt.ToString() + "</td></tr>";

                    }
                    content = content + "<table>";
                }

                content = content + "<table>";
                content = content + "<tr><td width='200px'></td><td width='140px' style='text-align:right;font-size: 14px;'>Total :</td><td width='80px' style='text-align:right;font-size: 14px;'>$ " + Total + "</td></tr>";
                content = content + "<tr><td width='200px'></td><td width='140px' style='text-align:right;font-size: 14px;'>Less Deposits :</td><td width='80px' style='text-align:right;font-size: 14px;'>$ " + DEPOSIT + "</td></tr>";
                content = content + "<tr><td width='200px'></td><td width='140px' style='text-align:right;font-size: 14px;'>Discount :</td><td width='80px' style='text-align:right;font-size: 14px;'>$ " + deduction.ToString() + "</td></tr>";
                content = content + "<tr><td width='200px'></td><td width='140px' style='text-align:right;font-size: 14px;'>Sales Tax :</td><td width='80px' style='text-align:right;font-size: 14px;'>$ " + SALES_TAX.ToString() + "</td></tr>";
                content = content + "<tr><td width='200px'></td><td width='140px' style='text-align:right;font-size: 14px;'>Balance Due :</td><td width='80px' style='text-align:right;font-size: 14px;'>$ " + BalanceDue.ToString() + "</td></tr>";
                content = content + "</table>";

                if (STYLE1 != "")
                {
                    JsonResult imagesResult = objImgController.GetImages(STYLE1);
                    var imagesList = imagesResult.Value as List<string>;
                    string StyleImage = "";
                    if (imagesList != null && imagesList.Any())
                    {
                        StyleImage = "<img src='" + imagesList.First() + "' style='height:80px'>";
                    }
                    else
                    {
                        StyleImage = null;
                    }

                    if (StyleImage != null)
                    {
                        content = content + "<table>";
                        content = content + "<tr><td width='420px' style='text-align:left;font-size: 12px;'>" + StyleImage + "</td></tr>";
                        content = content + "</table>";
                    }

                }
                content = content + "<table>";
                content = content + "<tr><td width='420px' style='text-align:left;font-size: 12px;'>Invoices " + frminvno + "</td></tr>";
                content = content + "</table>";
                content = content + "<table>";
                content = content + "<tr><td width='120px' style='text-align:left;font-size: 12px;'><b>Instructions : </b></td><td width='300px' style='text-align:right;font-size: 12px;border:1px solid #000;'>" + LongNote1 + "</td></tr>";
                content = content + "</table>";
            }
            return content;
        }

        public DataTable creatdatagridbasedonrepid(string currentrepno, bool isplit = false, bool iSOnJobbag = false)
        {
            string[] parts = Array.ConvertAll(currentrepno.Split(','), p => p.Trim());
            for (var i = 0; i < parts.Length; i++)
                parts[i] = parts[i].PadLeft(9, '0');
            string repNos = string.Join(",", parts);
            repNos = "(" + string.Format("'{0}'", (repNos.Trim().PadLeft(9, '0'))).Replace(",", "','") + ")";
            string splitrepNos = string.Join(",", parts);
            splitrepNos = "(" + string.Format("'{0}'", (splitrepNos.Trim().PadLeft(9, '0').Substring(0, 9))).Replace(",", "','") + ")";
            String cmd = $@"SELECT REPAIR.REPAIR_NO,REP_ITEM.LINE, iif(iSNULL(REP_ITEM.ITEM,'')='',REP_ITEM.STYLE,REP_ITEM.ITEM) ITEM, IIF(@isplit='True' , iSNULL((SELECT SUM(QTY) QTY FROM LBL_BAR WHERE BARCODE in {repNos} and style=REP_ITEM.ITEM   GROUP BY BARCODE),0), REP_ITEM.QTY) QTY, CAST(IIF(ISNULL(REPAIR.INV_NO,'')  = '',(REP_ITEM.PRICE * (100 - REP_ITEM.Disc_Per_Line) / 100),(REP_ITEM.PRICE * (100 - REP_ITEM.Disc_Per_Line) / 100)) AS DECIMAL(15,2)) as PRICE, REP_ITEM.NOTE,REP_ITEM.SHIPED,REP_ITEM.STAT,REP_ITEM.VENDOR,replicate('0', 6 - len(REP_ITEM.BARCODE)) + cast (REP_ITEM.BARCODE as varchar) AS BARCODE,REP_ITEM.SIZE,repair.NAME,cast(repair.DATE as date) as DATE,repair.ADDR1,repair.COUNTRY,repair.MESSAGE,repair.ACC,repair.ISSUE_CRDT,repair.is_cod,repair.cod_type,repair.early,repair.ADDR2,repair.CITY,repair.STATE,repair.ZIP,cast(repair.CAN_DATE as date) as CAN_DATE ,cast(repair.RCV_DATE as date) as RCV_DATE,cast(repair.DATE as date) as DATE,repair.CUS_REP_NO,repair.CUS_DEB_NO,(REP_ITEM.QTY - REP_ITEM.SHIPED) as [OPEN] ,c.NAME2 as name2, c.addr2 as addr2, c.addr22 as addr22, c.city2 as city2, c.state2 as state2, c.zip2 as zip2,c.Tel,c.Email,repair.shiptype,repair.resident, repair.estimate, repair.taxable, repair.sales_tax,repair.Message1,repair.salesman1,II.REPAIR_NOTE,repair.inv_no,repair.Jeweler_note,repair.deduction,repair.store,repair.no_taxresion,repair.salesman2,repair.comish1,repair.comish2,repair.comishamount1,repair.comishamount2,repair.Sales_Fee_Amount,repair.Sales_Fee_Rate,repair.repStatus,rep_size,rep_metal,REPAIR.setter, iSNULL(REP_ITEM.Disc_Per_Line,0) Disc_Per_Line,
        repair.warranty_inv_no,repair.style,repair.ship_by,repair.weight,repair.insured,repair.snh,repair.surprise,repair.Estimateready
        FROM REPAIR
        left join REP_ITEM on REPAIR.REPAIR_NO = REP_ITEM.REPAIR_NO
        inner join customer c on c.ACC = repair.ACC  LEFT join IN_ITEMS II on REPAIR.INV_NO = II.INV_NO  AND II.STYLE=REP_ITEM.ITEM
        where right('00000000'+repair.REPAIR_NO,9) in " + (isplit ? splitrepNos : repNos);
            if (iSOnJobbag)
            {
                cmd += " union " + $@"select  R.REPAIR_NO,PH.LINE, ph.CODE ITEM,
                            cast(PH.CHANGE as decimal(11,2)) QTY, CAST(0 AS DECIMAL(15,2)) as PRICE,
                            PH.NOTE,cast(0 as decimal(11,2)) SHIPED,'' STAT,'' VENDOR,PH.JOB_BAG AS BARCODE,
                            '' SIZE,R.NAME,cast(R.DATE as date) as DATE,R.ADDR1,R.COUNTRY,R.MESSAGE,R.ACC,R.ISSUE_CRDT,R.is_cod,R.cod_type,R.early,R.ADDR2,R.CITY,R.STATE,R.ZIP,cast(R.CAN_DATE as date) as CAN_DATE ,cast(R.RCV_DATE as date) as RCV_DATE,cast(R.DATE as date) as DATE,R.CUS_REP_NO,R.CUS_DEB_NO,(0 - 0) as [OPEN] ,c.NAME2 as name2, c.addr2 as addr2, c.addr22 as addr22, c.city2 as city2, c.state2 as state2, c.zip2 as zip2,c.Tel,c.Email,R.shiptype,R.resident, R.estimate, R.taxable, R.sales_tax,R.Message1,R.salesman1,'' REPAIR_NOTE,R.inv_no,R.Jeweler_note,R.deduction,R.store,R.no_taxresion,R.salesman2,R.comish1,R.comish2,R.comishamount1,R.comishamount2,R.Sales_Fee_Amount,R.Sales_Fee_Rate,R.repStatus,rep_size,rep_metal,R.setter, 0 Disc_Per_Line,
                            R.warranty_inv_no,R.style,R.ship_by,R.weight,R.insured,R.snh,R.surprise,R.Estimateready
                            from PARTS_HIST PH
                            JOIN REPAIR R on [dbo].[getbarcode](R.REPAIR_NO)=[dbo].[getbarcode](PH.JOB_BAG)
                            join CUSTOMER C on C.ACC=R.ACC
                            where right('00000000'+PH.JOB_BAG,9) in {(isplit ? splitrepNos : repNos)}  and code not in(select concat('CODE ',ITEM) from REP_ITEM where  right('0000000'+REPAIR_NO,7) in {(isplit ? splitrepNos : repNos)} )
                            and ISNULL(PH.ON_JOBBAG,0)=1";
            }
            return _helperCommonService.GetSqlData($@"{cmd}", "@REPAIR_NO", repNos, "@isplit", isplit.ToString());//iSNULL(REP_ITEM.STYLE,'')='' AND
        }

        public IActionResult ModifyCustomerCodeOfAnInvoice()
        {
            ViewBag.StoreData = _helperCommonService.GetAllStoreCodesList();
            ViewBag.CustomerCodes = _helperCommonService.GetAllCustomerCodes();
            return View("~/Views/Invoice/ModifyCustomerCodeOfAnInvoice.cshtml");
        }

        public string UpdateInvoiceAccDetails(string INV, string CCOD, string NCCOD)
        {
            _helperCommonService.GetInvoiceAccUpdate(INV, NCCOD, "Update");
            _helperCommonService.AddKeepRec("Acc modified on Invoice# " + INV + " From  " + CCOD.ToUpper().Trim() + " to " + NCCOD.ToUpper().Trim());

            return "Customer account modified successfully on Invoice#";
        }

        public string CheckValidCustomerCode(string NCCOD)
        {
            string status = _helperCommonService.CheckValidCustomerCode(NCCOD, false);
            return status;
        }

        public string GetInvoiceDetailsBasedOnInv(string Invno)
        {
            ListOfItemsSoldModel objModel = new ListOfItemsSoldModel();
            DataRow dr = _listOfItemsSoldService.GetInvoiceByInvNo(Invno);
            if (dr != null)
            {
                return dr["acc"].ToString();
            }
            else
            {
                return "";
            }
        }

        public IActionResult SummeryByRegister()
        {
            ListOfItemsSoldModel objModel = new ListOfItemsSoldModel();
            ViewBag.RegisterData = _helperCommonService.GetAllRegisterCodesList();
            objModel.AllStores = _helperCommonService.GetAllStores();
            return View(objModel);
        }

        public string GetSummaryByRegister(string datetype, string date1, string date2, string trantype, string registers)
        {
            DataTable data = _repairService.SummaryByRegister(datetype, date1, date2, trantype, registers);
            return JsonConvert.SerializeObject(data);
        }

        public IActionResult ListOfPickedUpItems()
        {
            ListOfItemsSoldModel objModel = new ListOfItemsSoldModel();
            ViewBag.Salesreps = _listOfItemsSoldService.getAllSales();
            objModel.AllStores = _helperCommonService.GetAllStores();
            return View(objModel);
        }

        public string GetListOfPickedUpItems(string Fdate, string Tdate, string store, string salesman, decimal Purchase)
        {
            DataTable data = _listOfItemsSoldService.GetListofPickedupItems(Fdate, Tdate, store, salesman, Purchase);
            return JsonConvert.SerializeObject(data);
        }

        public IActionResult InvoicedReport()
        {
            ListOfItemsSoldModel objModel = new ListOfItemsSoldModel();
            ViewBag.Salesreps = _listOfItemsSoldService.getAllSales();
            objModel.AllStores = _helperCommonService.GetAllStores();
            return View(objModel);
        }

        public string GetInvoiceReport(DateTime? FROMDATE, DateTime? TODATE, string Store, string Sales, bool IsLaywayUnpaid, bool saletax,
            bool IsInvoicedReport = false, bool _DateValueCheck = false)
        {
            DataTable data = _listOfItemsSoldService.GetInvoiceReportDetails(FROMDATE, TODATE, Store, Sales, IsLaywayUnpaid, saletax, IsInvoicedReport, _DateValueCheck);
            return JsonConvert.SerializeObject(data);
        }

        public IActionResult ListofWarrantySales()
        {
            ListOfItemsSoldModel objModel = new ListOfItemsSoldModel();

            return View();
        }
        public string GetListOfWarrantySales(DateTime? FromDt, DateTime? ToDt)
        {
            DataTable data = _listOfItemsSoldService.GetWarranrySales(FromDt, ToDt);
            return JsonConvert.SerializeObject(data);

        }
        public IActionResult GetAddDeleteCreditReason()
        {
            DataTable dtGLAccts = _helperCommonService.Getname();
            var sortedRows = dtGLAccts.AsEnumerable().OrderBy(row => row["ACC"]);
            dtGLAccts = sortedRows.CopyToDataTable();

            var glCodes = dtGLAccts.AsEnumerable().Select(row => new SelectListItem
            {
                Value = row["ACC"].ToString(),
                Text = row["NAME"].ToString()
            }).ToList();

            ViewBag.GlCodeList = glCodes.Select(x => new { value = x.Value, name = x.Text }).ToList();
            ViewBag.DropdownOptions = string.Join("", glCodes.Select(item =>
                $"<option value='{item.Value}' data-name='{item.Text.Substring(item.Text.IndexOf('-') + 1).Trim()}' data-acc='{item.Value}'>{item.Value} - {item.Text}</option>"
            ));
            var glListJson = dtGLAccts.AsEnumerable().Select(row => new
            {
                acc = row["ACC"].ToString(),
                name = row["NAME"].ToString()
            }).ToList();

            ViewBag.GLListJson = Newtonsoft.Json.JsonConvert.SerializeObject(glListJson);

            ListOfItemsSoldModel objmodel = new ListOfItemsSoldModel();
            DataTable dtReasons = _listOfItemsSoldService.checkglcodereason();

            var reasonsList = dtReasons.AsEnumerable().Select(row => new ListOfItemsSoldModel
            {
                _Reason = row["REASON"].ToString(),
                _Acc = row["GL_ACC"].ToString(),
                _Name = row["NAME"].ToString()
            }).ToList();
            var model = new ListOfItemsSoldModel
            {
                GlCodes = glCodes,
                ExistingReasons = reasonsList
            };

            return View("~/Views/Ar/AddDeleteCreditReason.cshtml", model);
        }

        [HttpPost]
        public JsonResult SaveCreditReasons([FromBody] string xmlData)
        {
            if (string.IsNullOrEmpty(xmlData))
            {
                return Json(new { success = false, message = "No data received." });
            }

            try
            {
                ListOfItemsSoldModel objmodel = new ListOfItemsSoldModel();

                bool saved = _listOfItemsSoldService.AddXmlvaluesave("AddEditCreditReason", "@creditreasonXml", xmlData);

                if (saved)
                {
                    return Json(new { success = true });
                }
                else
                {
                    return Json(new { success = false, message = "Save operation failed." });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult DeleteCreditReason(string reason, string glcode)
        {
            if (string.IsNullOrEmpty(reason) || string.IsNullOrEmpty(glcode))
                return Json(new { success = false, message = "Invalid Reason or GL Code." });
            try
            {
                bool result = _listOfItemsSoldService.DeleteCreditReason(reason, glcode);
                return Json(new { success = result });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }
        public class InvoiceData
        {
            public string INV_NO { get; set; }
            public string ACC { get; set; }
            public string NAME { get; set; }
            public string TEL { get; set; }
            public string DATE { get; set; }
            public string STYLE { get; set; }
            public string DESC { get; set; }
            public string GR_TOTAL { get; set; }
            public string BALANCE { get; set; }
            public string MESSAGE { get; set; }
            public bool? INACTIVE { get; set; }
        }

        [HttpPost]
        public IActionResult UpdateInactive([FromBody] List<InvoiceData> invoices)
        {
            if (invoices == null || invoices.Count == 0)
            {
                return Json(new { success = false, message = "No invoices found" });
            }

            DataTable dt = new DataTable();
            dt.Columns.Add("INV_NO");
            dt.Columns.Add("ACC");
            dt.Columns.Add("NAME");
            dt.Columns.Add("TEL");
            dt.Columns.Add("DATE");
            dt.Columns.Add("STYLE");
            dt.Columns.Add("DESC");
            dt.Columns.Add("GR_TOTAL");
            dt.Columns.Add("BALANCE");
            dt.Columns.Add("MESSAGE");
            dt.Columns.Add("INACTIVE");

            foreach (var inv in invoices)
            {
                dt.Rows.Add(
                    inv.INV_NO ?? string.Empty,
                    inv.ACC ?? string.Empty,
                    inv.NAME ?? string.Empty,
                    inv.TEL ?? string.Empty,
                    inv.DATE ?? string.Empty,
                    inv.STYLE ?? string.Empty,
                    inv.DESC ?? string.Empty,
                    inv.GR_TOTAL ?? string.Empty,
                    inv.BALANCE ?? string.Empty,
                    inv.MESSAGE ?? string.Empty,
                    inv.INACTIVE?.ToString() ?? string.Empty
                );
            }

            string xml = _helperCommonService.GetDataTableXML("Active", dt);

            if (UpdateInactve(xml))
            {
                return Json(new { success = true, message = "Updated successfully" });
            }

            return Json(new { success = false, message = "Update failed" });
        }


        public bool UpdateInactve(string XML)
        {
            using (SqlCommand sqlCommand = new SqlCommand())
            {
                sqlCommand.Connection = _connectionProvider.GetConnection();
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.CommandText = "updateActiveorinActive";

                SqlParameter parameter = new SqlParameter();
                parameter.ParameterName = "@xmlActive";
                parameter.SqlDbType = System.Data.SqlDbType.Xml;
                parameter.Value = XML;
                sqlCommand.Parameters.Add(parameter);

                // Open the connection, execute the query and close the connection
                sqlCommand.Connection.Open();
                bool rowsAffected = sqlCommand.ExecuteNonQuery() > 0;
                sqlCommand.Connection.Close();
                return rowsAffected;
            }
        }
        [HttpPost]
        public JsonResult UpdateInactive(string data)
        {
            try
            {
                if (string.IsNullOrEmpty(data))

                {
                    return Json(new { success = false, message = "No records found." });
                }

                // Parse the JSON string into a DataTable
                DataTable dt = new DataTable("Invoices");
                dt.Columns.Add("INV_NO");
                dt.Columns.Add("ACC");
                dt.Columns.Add("NAME");
                dt.Columns.Add("TEL");
                dt.Columns.Add("DATE");
                dt.Columns.Add("STYLE");
                dt.Columns.Add("DESC");
                dt.Columns.Add("GR_TOTAL");
                dt.Columns.Add("BALANCE");
                dt.Columns.Add("MESSAGE");
                dt.Columns.Add("INACTIVE");

                var invoices = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Dictionary<string, string>>>(data);

                foreach (var inv in invoices)
                {
                    dt.Rows.Add(
                        inv["INV_NO"], inv["ACC"], inv["NAME"], inv["TEL"], inv["DATE"],
                        inv["STYLE"], inv["DESC"], inv["GR_TOTAL"], inv["BALANCE"], inv["MESSAGE"],
                        inv.ContainsKey("INACTIVE") ? inv["INACTIVE"] : "false"
                    );
                }

                string xml = _helperCommonService.GetDataTableXML("Active", dt);
                bool updated = _helperCommonService.UpdateInactve(xml);
                if (updated)
                    return Json(new { success = true, message = _helperCommonService.GetLang("Updated Successfully") });
                return Json(new { success = false, message = "Update failed." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }
    }
}
