using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using System.Data;

namespace YJWebCoreMVC.Services
{
    public class ListOfItemsSoldService
    {

        private readonly HelperService _helperService;
        private readonly ConnectionProvider _connectionProvider;
        private readonly HttpContextAccessor _httpContextAccessor;
        private readonly IWebHostEnvironment _env;

        public ListOfItemsSoldService(HelperService helperService, ConnectionProvider connectionProvider, HttpContextAccessor httpContextAccessor, IWebHostEnvironment env)
        {
            _helperService = helperService;
            _connectionProvider = connectionProvider;
            _httpContextAccessor = httpContextAccessor;
            _env = env;
        }

        public DataTable GetListOfSoldItemsDetails(string FROMDATE, string DateTo, string Store, string Sales, bool IsLaywayUnpaid, bool saletax, bool IsInvoicedReport, bool DateValueCheck)
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
                    dataAdapter.SelectCommand.CommandText = !IsInvoicedReport ? "GetStoreDetails" : "GetInvoicedReports";

                    dataAdapter.SelectCommand.Parameters.AddWithValue("@DATE1", FROMDATE);
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@DATE2", DateTo);
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@Store", Store);
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@Sales", Sales);
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@IsLaywayUnpaid", IsLaywayUnpaid);
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@SalesTax", saletax);
                    if (!IsInvoicedReport)
                        dataAdapter.SelectCommand.Parameters.AddWithValue("@ByPickup", DateValueCheck);
                    // Assign the SQL to the command object

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

        public DataTable GetListOfOpenOrderDetailsItemsDetails(string FROMDATE, string DateTo, string Store, string Sales, bool repair, bool specialorder, bool layaway, bool IsLaywayUnpaid, string prom_Fromdate, string prom_Todate, bool IsOpenQuotes = false)
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
                    dataAdapter.SelectCommand.CommandText = "GetOpenOrderDetails";
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@FROMDATE", FROMDATE);
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@TODATE", DateTo);
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@STORENO", Store.Trim());
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@SALESMAN1", Sales.Trim());
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@IsLaywayUnpaid", IsLaywayUnpaid);
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@reapair", repair);
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@specialorder", specialorder);
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@layaway", layaway);
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@OpenQuotes", IsOpenQuotes);
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@PROMISE_FROMDATE", prom_Fromdate);
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@PROMISE_TODATE", prom_Todate);


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

        public DataTable GetReceiptListDetails(string FROMDATE, string DateTo, string Store, string Sales, bool IsLaywayUnpaid, string register, bool ByPickupDate)
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
                    dataAdapter.SelectCommand.CommandText = "GetListOfReceipts";
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@DATE1", FROMDATE);
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@DATE2", DateTo);
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@Store", Store.Trim());
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@Sales", Sales.Trim());
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@IsLaywayUnpaid", IsLaywayUnpaid);
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@register", register);
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@BYPickup", ByPickupDate);

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

        public DataTable GetStoreTotalSalesListDetails(string FROMDATE, string DateTo, string Store, bool IsLaywayUnpaid, bool ByPickupDate)
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
                    dataAdapter.SelectCommand.CommandText = "GetTotalSalesPerStore";
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@DATE1", FROMDATE);
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@DATE2", DateTo);
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@Store", Store.Trim());
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@IsLaywayUnpaid", IsLaywayUnpaid);
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@ByPickup", ByPickupDate);

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

        public DataTable GetSalesBySalesmanDetails(string Store = "", string Sales = "", bool IsLaywayUnpaid = false, string ByWhichDate = "", string FROMDATE = "", string DateTo = "", string Brand = "", string Category = "", string Selbrand = "", bool IsComision = false, bool IsCost = false, bool CommByDiscount = false, bool IsCommbyProfit = false, bool Is_DiamondDealer = false)
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
                    dataAdapter.SelectCommand.CommandText = "GetSalesDetails";

                    dataAdapter.SelectCommand.Parameters.AddWithValue("@STORENAME", Store);
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@SALESMAN", Sales);
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@ISLAYAWAY", IsLaywayUnpaid);
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@BYWHICHDATE", ByWhichDate);
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@FDATE", FROMDATE);
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@TDATE", DateTo);
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@BRAND", Brand.Replace("'", "''"));
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@CATEGORY", Category.Replace("'", "''"));
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@BRAND1", Selbrand.Replace("'", "''"));
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@ISSHOWCOMISION", IsComision);
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@ISSHOWCOST", IsCost);
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@commbydiscount", CommByDiscount);
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@isCommbyprofit", IsCommbyProfit);//
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@Is_DiamondDealer", Is_DiamondDealer);


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

        public DataTable GetStatmentOfInvoice(string FROMDATE, string DateTo)
        {

            DataTable dataTable = new DataTable();
            //string CommandText = "SELECT INV_NO, INVOICE.BACC as BILL_ACC, CAST(INVOICE.DATE AS DATE) [DATE], IIF(RSFM IS NULL OR RSFM = '', GR_TOTAL, 0) AS INVOICE, IIF(RSFM IS NOT NULL OR RSFM <> '', GR_TOTAL, 0) AS SFM, ISNULL(CREDITS,0) AS CREDITS, GR_TOTAL-ISNULL(CREDITS, 0) AS BALANCE, ISNULL(RSFM,'') AS RSFM, GR_TOTAL, ISNULL(INVOICE.DEDUCTION, 0) AS DEDUCTIONS, CUSTOMER.NAME,CUST_PON,invoice.STATE state1,CUSTOMER.COUNTRY, INVOICE.SALESMAN1,INVOICE.SALESMAN2,CUSTOMER.SALESMAN3,CUSTOMER.SALESMAN4,CUSTOMER.ACC,INVOICE.STORE_NO,ISNULL(LAYAWAY,0) LAYAWAY,[dbo].[iSSpecialInvoice](invoice.INV_NO) SPECIAL, ISNULL(V_CTL_NO,'') IS_REPAIR, INVOICE.INACTIVE,INVOICE.ShopifyOrderNumber AS SHOPIFY_ORD, CUSTOMER.NAME,try_cast(CUSTOMER.TEL as nvarchar(50)) as ,iif(INVOICE.SALESMAN1 != '', INVOICE.SALESMAN1, iif(INVOICE.SALESMAN2 != '', INVOICE.SALESMAN2, iif(INVOICE.SALESMAN3 != '', INVOICE.SALESMAN3, INVOICE.SALESMAN4))) as Salesman FROM INVOICE INNER JOIN CUSTOMER ON INVOICE.ACC = CUSTOMER.ACC WHERE CAST(INVOICE.DATE AS DATE) BETWEEN CAST(@FROMDATE AS DATE) AND CAST(@TODATE AS DATE) ORDER BY INVOICE.[DATE]";
            string CommandText = "SELECT INV_NO, INVOICE.BACC as BILL_ACC, CAST(INVOICE.DATE AS DATE) [DATE], IIF(RSFM IS NULL OR RSFM = '', GR_TOTAL, 0) AS INVOICE, IIF(RSFM IS NOT NULL OR RSFM <> '', GR_TOTAL, 0) AS SFM, ISNULL(CREDITS, 0) AS CREDITS,GR_TOTAL-ISNULL(CREDITS, 0) AS BALANCE, ISNULL(RSFM, '') AS RSFM, GR_TOTAL,ISNULL(INVOICE.DEDUCTION, 0) AS DEDUCTIONS, CUSTOMER.NAME,CUST_PON,invoice.STATE state1, CUSTOMER.COUNTRY,INVOICE.SALESMAN1,INVOICE.SALESMAN2,CUSTOMER.SALESMAN3,CUSTOMER.SALESMAN4,CUSTOMER.ACC,INVOICE.STORE_NO,ISNULL(LAYAWAY,0) LAYAWAY,[dbo].[iSSpecialInvoice](invoice.INV_NO) SPECIAL, ISNULL(V_CTL_NO,'') IS_REPAIR, INVOICE.INACTIVE,INVOICE.ShopifyOrderNumber AS SHOPIFY_ORD, CUSTOMER.NAME,try_cast(CUSTOMER.TEL as nvarchar(50)) as TEL,iif(INVOICE.SALESMAN1 != '', INVOICE.SALESMAN1, iif(INVOICE.SALESMAN2 != '', INVOICE.SALESMAN2, iif(INVOICE.SALESMAN3 != '', INVOICE.SALESMAN3, INVOICE.SALESMAN4))) as Salesman FROM INVOICE INNER JOIN CUSTOMER ON INVOICE.ACC = CUSTOMER.ACC WHERE CAST(INVOICE.DATE AS DATE) BETWEEN CAST(@FROMDATE AS DATE) AND CAST(@TODATE AS DATE) ORDER BY INVOICE.[DATE] ";
            using (SqlDataAdapter dataAdapter = new SqlDataAdapter())
            {
                try
                {
                    // Create the command and set its properties
                    dataAdapter.SelectCommand = new SqlCommand();
                    dataAdapter.SelectCommand.Connection = _connectionProvider.GetConnection();

                    dataAdapter.SelectCommand.CommandType = CommandType.Text;
                    dataAdapter.SelectCommand.CommandText = CommandText;
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@FROMDATE", FROMDATE);
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@TODATE", DateTo);

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

        public DataTable GetDaysOnHandInventoryDetails(string Ccode, string FROMDATE, string DateTo, string Category, string SubCategory, string Metal, string Brand, string FromStyle, string ToStyle, string Vendor, int NoOfDays = 0, bool SummByModel = false)
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
                    dataAdapter.SelectCommand.CommandText = "DaysOnHands";

                    dataAdapter.SelectCommand.Parameters.AddWithValue("@date1", FROMDATE);
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@date2", DateTo);
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@brand", Brand);
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@category", Category.Replace("'", "''"));
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@subcat", SubCategory.Replace("'", "''"));
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@metal", Metal);
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@cacc", Ccode.Replace("'", "''"));
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@no_of_days", NoOfDays);
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@STYLEFROM", FromStyle);
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@STYLETO", ToStyle);
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@Vendor", Vendor);
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@SummaryByModel", (SummByModel ? 1 : 0));


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

        public DataTable GetVendorSalesReportData(string Ccode, string FROMDATE, string DateTo)
        {
            DataTable dataTable = new DataTable();
            using (SqlDataAdapter dataAdapter = new SqlDataAdapter())
            {
                dataAdapter.SelectCommand = new SqlCommand();
                dataAdapter.SelectCommand.Connection = _connectionProvider.GetConnection();
                dataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                dataAdapter.SelectCommand.CommandText = "GetVendorSalesReport";

                dataAdapter.SelectCommand.Parameters.AddWithValue("@ACC", Ccode);
                dataAdapter.SelectCommand.Parameters.AddWithValue("@date1", FROMDATE);
                dataAdapter.SelectCommand.Parameters.AddWithValue("@date2", DateTo);

                dataAdapter.Fill(dataTable);
                return dataTable;
            }
        }
        public DataTable GetInstateOutStateSalesDetails(string State, string FROMDATE, string DateTo)
        {
            DataTable dataTable = new DataTable();
            using (SqlDataAdapter dataAdapter = new SqlDataAdapter())
            {
                dataAdapter.SelectCommand = new SqlCommand();
                dataAdapter.SelectCommand.Connection = _connectionProvider.GetConnection();
                dataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                dataAdapter.SelectCommand.CommandText = "InStateOutStateSale";

                dataAdapter.SelectCommand.Parameters.AddWithValue("@STATE", State);
                dataAdapter.SelectCommand.Parameters.AddWithValue("@FROMDATE", FROMDATE);
                dataAdapter.SelectCommand.Parameters.AddWithValue("@TODATE", DateTo);

                dataAdapter.Fill(dataTable);
                return dataTable;
            }
        }
        public DataTable GetStateSalesDetails(string FROMDATE, string DateTo, bool ByPickupDate, bool IsLaywayUnpaid)
        {
            DataTable dataTable = new DataTable();
            using (SqlDataAdapter dataAdapter = new SqlDataAdapter())
            {
                dataAdapter.SelectCommand = new SqlCommand();
                dataAdapter.SelectCommand.Connection = _connectionProvider.GetConnection();
                dataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                dataAdapter.SelectCommand.CommandText = "TotSaleByState";

                dataAdapter.SelectCommand.Parameters.AddWithValue("@FROMDATE", FROMDATE);
                dataAdapter.SelectCommand.Parameters.AddWithValue("@TODATE", DateTo);
                dataAdapter.SelectCommand.Parameters.AddWithValue("@iSLayawayUnPicked", IsLaywayUnpaid.ToString());
                dataAdapter.SelectCommand.Parameters.AddWithValue("@iSByPickupDate", ByPickupDate.ToString());

                dataAdapter.Fill(dataTable);
                return dataTable;
            }
        }
        public DataTable GetVendorStylePerformenctDetails(string Group, string Category, string SubCategory, string Metal, string Vendor, string FROMDATE, string DateTo, string Purcfdate, string Purctdate, bool SummByModel = false, bool Is_DiamondDealer = false)
        {
            DataTable dataTable = new DataTable();
            using (SqlDataAdapter dataAdapter = new SqlDataAdapter())
            {
                dataAdapter.SelectCommand = new SqlCommand();
                dataAdapter.SelectCommand.Connection = _connectionProvider.GetConnection();
                dataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                dataAdapter.SelectCommand.CommandText = "GetVendorStylesPerformance";

                dataAdapter.SelectCommand.Parameters.AddWithValue("@group", Group);
                dataAdapter.SelectCommand.Parameters.AddWithValue("@category", Category);
                dataAdapter.SelectCommand.Parameters.AddWithValue("@subcat", SubCategory);
                dataAdapter.SelectCommand.Parameters.AddWithValue("@metal", Metal);
                dataAdapter.SelectCommand.Parameters.AddWithValue("@vendor", Vendor);
                dataAdapter.SelectCommand.Parameters.AddWithValue("@salesfdate", FROMDATE);
                dataAdapter.SelectCommand.Parameters.AddWithValue("@salestdate", DateTo);
                dataAdapter.SelectCommand.Parameters.AddWithValue("@purcfdate", Purcfdate);
                dataAdapter.SelectCommand.Parameters.AddWithValue("@purctdate", Purctdate);
                dataAdapter.SelectCommand.Parameters.AddWithValue("@sumbymodel", SummByModel);
                dataAdapter.SelectCommand.Parameters.AddWithValue("@IsDiamondDealer", Is_DiamondDealer);
                dataAdapter.Fill(dataTable);
                return dataTable;
            }
        }

        public DataTable GetTotalSalesPerStateDetails(string FROMDATE, string DateTo, string Store, bool IncludeInactive = false, bool NoSalesTax = false, bool IncludeNotaxInvoices = false, string TaxState = "", bool IncParialPay = false)
        {
            DataTable dataTable = new DataTable();

            using (SqlDataAdapter dataAdapter = new SqlDataAdapter())
            {
                dataAdapter.SelectCommand = new SqlCommand();
                dataAdapter.SelectCommand.Connection = _connectionProvider.GetConnection();
                dataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;

                dataAdapter.SelectCommand.CommandText = "GetTotalSalesPerState";
                dataAdapter.SelectCommand.Parameters.AddWithValue("@FromDate", FROMDATE);
                dataAdapter.SelectCommand.Parameters.AddWithValue("@ToDate", DateTo);
                dataAdapter.SelectCommand.Parameters.AddWithValue("@StoreName", Store);
                dataAdapter.SelectCommand.Parameters.AddWithValue("@IncludeInactive", IncludeInactive);
                dataAdapter.SelectCommand.Parameters.AddWithValue("@includeNotaxInvoices", IncludeNotaxInvoices);
                dataAdapter.SelectCommand.Parameters.AddWithValue("@TaxState", TaxState);
                dataAdapter.SelectCommand.Parameters.AddWithValue("@iSPartial", IncParialPay);
                dataAdapter.Fill(dataTable);
            }
            return dataTable;
        }

        public DataTable GetTotalHourlySalesDetails(string FROMDATE, string DateTo, string Store, string Day)
        {
            DataTable dataTable = new DataTable();

            using (SqlDataAdapter dataAdapter = new SqlDataAdapter())
            {
                dataAdapter.SelectCommand = new SqlCommand();
                dataAdapter.SelectCommand.Connection = _connectionProvider.GetConnection();
                dataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;

                dataAdapter.SelectCommand.CommandText = "Hourly_Sales";
                dataAdapter.SelectCommand.Parameters.AddWithValue("@date1", FROMDATE);
                dataAdapter.SelectCommand.Parameters.AddWithValue("@date2", DateTo);
                dataAdapter.SelectCommand.Parameters.AddWithValue("@day", Day);
                dataAdapter.SelectCommand.Parameters.AddWithValue("@store", Store);
                dataAdapter.Fill(dataTable);
            }
            return dataTable;
        }

        public DataTable GetTotalWeeklySalesDetails(string FROMDATE, string DateTo, string Store)
        {
            DataTable dataTable = new DataTable();

            using (SqlDataAdapter dataAdapter = new SqlDataAdapter())
            {
                dataAdapter.SelectCommand = new SqlCommand();
                dataAdapter.SelectCommand.Connection = _connectionProvider.GetConnection();
                dataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;

                dataAdapter.SelectCommand.CommandText = "Weekly_Sales";
                dataAdapter.SelectCommand.Parameters.AddWithValue("@date1", FROMDATE);
                dataAdapter.SelectCommand.Parameters.AddWithValue("@date2", DateTo);
                dataAdapter.SelectCommand.Parameters.AddWithValue("@store", Store);
                dataAdapter.Fill(dataTable);
            }
            return dataTable;
        }

        public DataTable GetSalesProfitPerStoreDetails(string Ccode, string FROMDATE, string DateTo, string Category, string SubCategory, string Metal, string Brand, string FromStyle, string ToStyle, string Vendor, string Store, bool WithGP = false, bool SeparateSM = false, bool IsSalesCOG = false, string ByWhichDate = "", bool IsLayaway = false, String Sales = "", bool Monthproft = false, bool ISLaySpe = false, bool Isinclbankfee = false)
        {

            string StoreProcedureName = "";
            if (!IsSalesCOG)
            {
                StoreProcedureName = WithGP ? "det_profit" : "det_prft";
                DataTable dataTable = new DataTable();

                using (SqlDataAdapter dataAdapter = new SqlDataAdapter())
                {
                    dataAdapter.SelectCommand = new SqlCommand();
                    dataAdapter.SelectCommand.Connection = _connectionProvider.GetConnection();
                    dataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;

                    dataAdapter.SelectCommand.CommandText = StoreProcedureName;
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@date1", FROMDATE);
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@date2", DateTo);
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@brand", Brand);
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@category", Category);
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@subcat", SubCategory);
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@metal", Metal);
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@cacc", Ccode);
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@do_sum", Monthproft ? "7" : !SeparateSM ? "5" : "6");
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@STYLEFROM", FromStyle);
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@STYLETO", ToStyle);
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@Vendor", Vendor);
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@store", Store);
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@iSLaySpe", ISLaySpe ? "1" : "0");
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@ByWichDate", ByWhichDate);
                    dataAdapter.Fill(dataTable);
                }
                return dataTable;
            }
            else
            {
                StoreProcedureName = WithGP ? "det_COGProfit_GP" : "det_COGProfit";
                DataTable dataTable = new DataTable();

                using (SqlDataAdapter dataAdapter = new SqlDataAdapter())
                {
                    dataAdapter.SelectCommand = new SqlCommand();
                    dataAdapter.SelectCommand.Connection = _connectionProvider.GetConnection();
                    dataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;

                    dataAdapter.SelectCommand.CommandText = StoreProcedureName;
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@date1", FROMDATE);
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@date2", DateTo);
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@brand", Brand);
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@category", Category);
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@subcat", SubCategory);
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@metal", Metal);
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@cacc", Ccode);
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@do_sum", !SeparateSM ? "5" : "6");
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@STYLEFROM", FromStyle);
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@STYLETO", ToStyle);
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@Vendor", Vendor);
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@store", Store);
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@ByWichDate", ByWhichDate);
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@iSLayaway", IsLayaway ? "1" : "0");
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@SALESMAN", Sales);
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@isinclbankfee", Isinclbankfee ? "1" : "0");
                    dataAdapter.Fill(dataTable);
                }
                return dataTable;
            }
        }

        public DataTable GetStoreDetailsSimplifiedExecel(string Ccode, string FROMDATE, string DateTo, string Category, string SubCategory, string Metal, string Brand, string FromStyle, string ToStyle, string Vendor, string Store, bool WithGP = false, bool SeparateSM = false, bool IsSalesCOG = false, string ByWhichDate = "", bool IsLayaway = false, String Sales = "", bool Monthproft = false, bool ISLaySpe = false, bool Isinclbankfee = false)
        {
            DataTable dataTable = new DataTable();

            using (SqlDataAdapter dataAdapter = new SqlDataAdapter())
            {
                dataAdapter.SelectCommand = new SqlCommand();
                dataAdapter.SelectCommand.Connection = _connectionProvider.GetConnection();
                dataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;

                dataAdapter.SelectCommand.CommandText = "det_SalesCGOProfitSimpleExcel";
                dataAdapter.SelectCommand.Parameters.AddWithValue("@date1", FROMDATE);
                dataAdapter.SelectCommand.Parameters.AddWithValue("@date2", DateTo);
                dataAdapter.SelectCommand.Parameters.AddWithValue("@brand", Brand);
                dataAdapter.SelectCommand.Parameters.AddWithValue("@category", Category);
                dataAdapter.SelectCommand.Parameters.AddWithValue("@subcat", SubCategory);
                dataAdapter.SelectCommand.Parameters.AddWithValue("@metal", Metal);
                dataAdapter.SelectCommand.Parameters.AddWithValue("@cacc", Ccode);
                dataAdapter.SelectCommand.Parameters.AddWithValue("@STYLEFROM", FromStyle);
                dataAdapter.SelectCommand.Parameters.AddWithValue("@STYLETO", ToStyle);
                dataAdapter.SelectCommand.Parameters.AddWithValue("@Vendor", Vendor);
                dataAdapter.SelectCommand.Parameters.AddWithValue("@store", Store);
                dataAdapter.SelectCommand.Parameters.AddWithValue("@ByWichDate", ByWhichDate);
                dataAdapter.SelectCommand.Parameters.AddWithValue("@iSLayaway", IsLayaway ? "1" : "0");
                dataAdapter.SelectCommand.Parameters.AddWithValue("@SALESMAN", Sales);
                dataAdapter.SelectCommand.Parameters.AddWithValue("@isinclbankfee", Isinclbankfee ? "1" : "0");
                dataAdapter.Fill(dataTable);
            }
            return dataTable;
        }

        public DataTable GetSalesProfitPerStoreDetails(string FROMDATE, string DateTo, string Store, string ByWhichDate, string Details = "", string Sources = "")
        {
            DataTable dataTable = new DataTable();

            using (SqlDataAdapter dataAdapter = new SqlDataAdapter())
            {
                dataAdapter.SelectCommand = new SqlCommand();
                dataAdapter.SelectCommand.Connection = _connectionProvider.GetConnection();
                dataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;

                dataAdapter.SelectCommand.CommandText = "SalesProfitPerSource";
                dataAdapter.SelectCommand.Parameters.AddWithValue("@DATE1", FROMDATE);
                dataAdapter.SelectCommand.Parameters.AddWithValue("@DATE2", DateTo);
                dataAdapter.SelectCommand.Parameters.AddWithValue("@Store", Store);
                if (Details == "Details")
                {
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@show", Details);
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@sources", Sources);
                }
                dataAdapter.SelectCommand.Parameters.AddWithValue("@BYWHICHDATE", ByWhichDate);
                dataAdapter.Fill(dataTable);
            }
            return dataTable;
        }
        public DataTable GetSalesProfitByCityDetails(string Ccode, string FROMDATE, string DateTo, string Category, string SubCategory, string Metal, string Brand, string FromStyle, string ToStyle, string Vendor, string Sales, string ByWhichDate = "")
        {
            DataTable dataTable = new DataTable();

            using (SqlDataAdapter dataAdapter = new SqlDataAdapter())
            {
                dataAdapter.SelectCommand = new SqlCommand();
                dataAdapter.SelectCommand.Connection = _connectionProvider.GetConnection();
                dataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                dataAdapter.SelectCommand.CommandTimeout = 6000;
                dataAdapter.SelectCommand.CommandText = "det_prft_by_city";

                dataAdapter.SelectCommand.Parameters.AddWithValue("@date1", FROMDATE);
                dataAdapter.SelectCommand.Parameters.AddWithValue("@date2", DateTo);
                dataAdapter.SelectCommand.Parameters.AddWithValue("@brand", Brand);
                dataAdapter.SelectCommand.Parameters.AddWithValue("@category", Category);
                dataAdapter.SelectCommand.Parameters.AddWithValue("@subcat", SubCategory);
                dataAdapter.SelectCommand.Parameters.AddWithValue("@metal", Metal);
                dataAdapter.SelectCommand.Parameters.AddWithValue("@cacc", Ccode);
                dataAdapter.SelectCommand.Parameters.AddWithValue("@do_sum", 3);
                dataAdapter.SelectCommand.Parameters.AddWithValue("@STYLEFROM", FromStyle);
                dataAdapter.SelectCommand.Parameters.AddWithValue("@STYLETO", ToStyle);
                dataAdapter.SelectCommand.Parameters.AddWithValue("@Vendor", Vendor);

                dataAdapter.SelectCommand.Parameters.AddWithValue("@CSColor", "");
                dataAdapter.SelectCommand.Parameters.AddWithValue("@CSClarity", "");
                dataAdapter.SelectCommand.Parameters.AddWithValue("@CSShape", "");
                dataAdapter.SelectCommand.Parameters.AddWithValue("@CenterType", "");
                dataAdapter.SelectCommand.Parameters.AddWithValue("@CenterSize", "");
                dataAdapter.SelectCommand.Parameters.AddWithValue("@scode", Sales);
                dataAdapter.SelectCommand.Parameters.AddWithValue("@BYWHICHDATE", ByWhichDate);
                dataAdapter.Fill(dataTable);
            }

            return dataTable;
        }

        public DataTable GetSalesReportForReorderDetails(string Ccode, string FROMDATE, string DateTo, string Category, string SubCategory, string Metal, string Brand, string FromStyle, string ToStyle, string Vendor, string VendorStyle, bool layaway)
        {

            DataTable dataTable = new DataTable();

            using (SqlDataAdapter dataAdapter = new SqlDataAdapter())
            {
                dataAdapter.SelectCommand = new SqlCommand();
                dataAdapter.SelectCommand.Connection = _connectionProvider.GetConnection();
                dataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                dataAdapter.SelectCommand.CommandTimeout = 6000;
                dataAdapter.SelectCommand.CommandText = "det_prft";

                dataAdapter.SelectCommand.Parameters.AddWithValue("@date1", FROMDATE);
                dataAdapter.SelectCommand.Parameters.AddWithValue("@date2", DateTo);
                dataAdapter.SelectCommand.Parameters.AddWithValue("@brand", Brand);
                dataAdapter.SelectCommand.Parameters.AddWithValue("@category", Category);
                dataAdapter.SelectCommand.Parameters.AddWithValue("@subcat", SubCategory);
                dataAdapter.SelectCommand.Parameters.AddWithValue("@metal", Metal);
                dataAdapter.SelectCommand.Parameters.AddWithValue("@cacc", Ccode);
                dataAdapter.SelectCommand.Parameters.AddWithValue("@do_sum", 8);
                dataAdapter.SelectCommand.Parameters.AddWithValue("@STYLEFROM", FromStyle);
                dataAdapter.SelectCommand.Parameters.AddWithValue("@STYLETO", ToStyle);
                dataAdapter.SelectCommand.Parameters.AddWithValue("@Vendor", Vendor);
                dataAdapter.SelectCommand.Parameters.AddWithValue("@vnd_style", VendorStyle);
                dataAdapter.SelectCommand.Parameters.AddWithValue("@iSLaySpe", layaway ? "1" : "0");
                dataAdapter.Fill(dataTable);
            }

            return dataTable;

        }
        public DataTable GetDetailedCOGDetails(string FROMDATE, string DateTo, string Store, string ByWhichDate, bool layaway = false, bool IsCost = false)
        {

            DataTable dataTable = new DataTable();

            using (SqlDataAdapter dataAdapter = new SqlDataAdapter())
            {
                dataAdapter.SelectCommand = new SqlCommand();
                dataAdapter.SelectCommand.Connection = _connectionProvider.GetConnection();
                dataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                dataAdapter.SelectCommand.CommandTimeout = 6000;
                dataAdapter.SelectCommand.CommandText = "detailed_cog";

                dataAdapter.SelectCommand.Parameters.AddWithValue("@Date1", FROMDATE);
                dataAdapter.SelectCommand.Parameters.AddWithValue("@Date2", DateTo);
                dataAdapter.SelectCommand.Parameters.AddWithValue("@Store", Store);
                dataAdapter.SelectCommand.Parameters.AddWithValue("@iSInvoiceCost", IsCost ? "1" : "0");
                dataAdapter.SelectCommand.Parameters.AddWithValue("@bypickdate", Convert.ToBoolean(ByWhichDate) ? "1" : "0");
                dataAdapter.SelectCommand.Parameters.AddWithValue("@isLayawaySpecial", layaway ? "1" : "0");
                dataAdapter.Fill(dataTable);
            }

            return dataTable;

        }

        public DataTable GetRcvableCreditByTimeFrame(string Ccode, string Ccode2, string ByWhichDate, string FROMDATE, string DateTo, string Trantype, string Register, int IsRepairOnly = 0, string CurrencyType = "", int IsShowCurrency = 0)
        {
            DataTable dataTable = new DataTable();

            using (SqlDataAdapter dataAdapter = new SqlDataAdapter())
            {
                dataAdapter.SelectCommand = new SqlCommand();
                dataAdapter.SelectCommand.Connection = _connectionProvider.GetConnection();
                dataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                dataAdapter.SelectCommand.CommandTimeout = 6000;
                dataAdapter.SelectCommand.CommandText = "GetRcvableCreditByTimeFrame";

                dataAdapter.SelectCommand.Parameters.AddWithValue("@acc1", Ccode);
                dataAdapter.SelectCommand.Parameters.AddWithValue("@acc2", Ccode2);
                dataAdapter.SelectCommand.Parameters.AddWithValue("@datetype", ByWhichDate);
                dataAdapter.SelectCommand.Parameters.AddWithValue("@date1", FROMDATE);
                dataAdapter.SelectCommand.Parameters.AddWithValue("@date2", DateTo);
                dataAdapter.SelectCommand.Parameters.AddWithValue("@trantype", Trantype);
                dataAdapter.SelectCommand.Parameters.AddWithValue("@registers", Register);
                dataAdapter.SelectCommand.Parameters.AddWithValue("@RepairPayments", IsRepairOnly.ToString());
                dataAdapter.SelectCommand.Parameters.AddWithValue("@CurrencyType", CurrencyType);
                dataAdapter.SelectCommand.Parameters.AddWithValue("@ShowByCurrency", IsShowCurrency.ToString());

                dataAdapter.Fill(dataTable);
            }

            return dataTable;
        }

        public DataTable SummaryByPayment(string Ccode, string Ccode2, string ByWhichDate, string FROMDATE, string DateTo, string Trantype, string Register, bool allstore = false, bool IsRepairOnly = false, bool ShowbyCurrency = false, string Store = "")
        {

            DataTable dataTable = new DataTable();

            using (SqlDataAdapter SqlDataAdapter = new SqlDataAdapter())
            {
                // Create the command and set its properties
                SqlDataAdapter.SelectCommand = new SqlCommand();
                SqlDataAdapter.SelectCommand.Connection = _connectionProvider.GetConnection();
                SqlDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;

                // Assign the SQL to the command object
                SqlDataAdapter.SelectCommand.CommandText = "GetRcvableCreditByTimeFrameBySummery";

                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@acc1", Ccode);
                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@acc2", Ccode2);
                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@datetype", ByWhichDate);

                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@date1", FROMDATE);
                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@date2", DateTo);
                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@trantype", Trantype);
                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@registers", Register);
                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@AllStore", allstore);
                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@RepairPayments", IsRepairOnly);
                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@ShowByCurrency", ShowbyCurrency);
                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@Storeno", Store);

                // Fill the table from adapter
                SqlDataAdapter.Fill(dataTable);
            }
            if (dataTable.Rows.Count > 0)
                return dataTable;
            return null;

        }

        public DataTable EODSummary(string FROMDATE, string DateTo, string Store)
        {
            DataTable dataTable = new DataTable();

            using (SqlDataAdapter SqlDataAdapter = new SqlDataAdapter())
            {
                // Create the command and set its properties

                SqlDataAdapter.SelectCommand = new SqlCommand();
                SqlDataAdapter.SelectCommand.Connection = _connectionProvider.GetConnection();
                SqlDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter.SelectCommand.CommandTimeout = 3000;

                // Assign the SQL to the command object
                SqlDataAdapter.SelectCommand.CommandText = "EODWorksheet";
                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@DateFrom", FROMDATE);
                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@DateTo", DateTo);
                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@Store", Store.Trim());
                SqlDataAdapter.Fill(dataTable);
            }

            return dataTable;
        }

        public DataTable EODWorkSheeetDetail(string FROMDATE, string DateTo, string Store)
        {
            DataTable dataTable = new DataTable();

            using (SqlDataAdapter SqlDataAdapter = new SqlDataAdapter())
            {
                // Create the command and set its properties

                SqlDataAdapter.SelectCommand = new SqlCommand();
                SqlDataAdapter.SelectCommand.Connection = _connectionProvider.GetConnection();
                SqlDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter.SelectCommand.CommandTimeout = 3000;

                // Assign the SQL to the command object
                SqlDataAdapter.SelectCommand.CommandText = "EODWorksheetDetail";
                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@DateFrom", FROMDATE);
                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@DateTo", DateTo);
                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@Store", Store.Trim());
                SqlDataAdapter.Fill(dataTable);
            }

            return dataTable;
        }

        public DataTable GetAllSroreCredits(string strCustomer, string FromDate, string ToDate, string Opt, bool IsGiftCert, string CreditNo)
        {
            DataTable dataTable = new DataTable();

            using (SqlDataAdapter SqlDataAdapter = new SqlDataAdapter())
            {
                // Create the command and set its properties
                SqlDataAdapter.SelectCommand = new SqlCommand();
                SqlDataAdapter.SelectCommand.Connection = _connectionProvider.GetConnection();
                SqlDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;

                // Assign the SQL to the command object
                SqlDataAdapter.SelectCommand.CommandText = @"GetAllSroreCredits";

                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@Acc", strCustomer.Trim());
                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@FromDate", FromDate);
                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@ToDate", ToDate);
                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@Type", Opt.Trim());
                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@IsGiftCert", IsGiftCert ? 1 : 0);
                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@CreditNo", CreditNo.Trim());

                // Fill the table from adapter
                SqlDataAdapter.Fill(dataTable);
            }
            return dataTable;
        }

        public DataTable GetMTDReport(string fDate, string tDate, string groupBy = "", bool IsPartial = false)
        {
            DataTable dataTable = new DataTable();

            using (SqlDataAdapter SqlDataAdapter = new SqlDataAdapter())
            {
                SqlDataAdapter.SelectCommand = new SqlCommand();
                SqlDataAdapter.SelectCommand.Connection = _connectionProvider.GetConnection();
                SqlDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter.SelectCommand.CommandTimeout = 6000;
                switch (groupBy)
                {
                    case "STORE":
                        SqlDataAdapter.SelectCommand.CommandText = "GetMTD_STORE";
                        break;
                    case "SALESMAN":
                        SqlDataAdapter.SelectCommand.CommandText = "GetMTD_SALESMAN";
                        break;
                    case "DUC":
                        SqlDataAdapter.SelectCommand.CommandText = "GetMTD_DUC";
                        break;
                    case "REPDUC":
                        SqlDataAdapter.SelectCommand.CommandText = "GetMTD_REPDUC";
                        break;
                }

                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@dateFrom", getdate(fDate));
                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@dateTo", getdate(tDate));
                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@iSPartial", IsPartial);

                SqlDataAdapter.Fill(dataTable);
            }
            return dataTable;
        }

        public DataTable GetMTDGrossProfitVar(string StoreData)
        {
            DataTable dataTable = new DataTable();

            using (SqlDataAdapter SqlDataAdapter = new SqlDataAdapter())
            {
                SqlDataAdapter.SelectCommand = new SqlCommand();
                SqlDataAdapter.SelectCommand.Connection = _connectionProvider.GetConnection();
                SqlDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter.SelectCommand.CommandTimeout = 6000;
                SqlDataAdapter.SelectCommand.CommandText = "GetMTD_STOREVAR";

                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@STOREDT", StoreData);

                SqlDataAdapter.Fill(dataTable);
            }
            return dataTable;
        }

        private string getdate(string date1)
        {
            string[] Date1 = date1.Split(' ');
            return Date1[0];
        }

        public DataTable ListofChecks(string Vendorcode1, string Vendorcode2, string Check1, string Check2, int Dateval, DateTime? Date1, DateTime? Date2, decimal Amt1, decimal Amt2, string Bank, string Glcode, bool IsAllGL, string StrSearchOption, bool Unapplied = false, string Store = "")
        {

            DataTable dataTable = new DataTable();

            using (SqlDataAdapter SqlDataAdapter = new SqlDataAdapter())
            {
                SqlDataAdapter.SelectCommand = new SqlCommand();
                SqlDataAdapter.SelectCommand.Connection = _connectionProvider.GetConnection();
                SqlDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter.SelectCommand.CommandTimeout = 6000;
                SqlDataAdapter.SelectCommand.CommandText = "ListofChecks";

                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@vendorcode1", Vendorcode1);
                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@check1", Check1);
                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@check2", Check2);
                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@bank", Bank);
                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@dateval", Dateval.ToString());
                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@date1", Date1.ToString());
                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@date2", Date2.ToString());
                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@amt1", Amt1.ToString());
                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@amt2", Amt2.ToString());
                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@glcode", Glcode);
                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@isAllGLcode", IsAllGL.ToString());
                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@SearchOption", StrSearchOption.ToString());
                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@unapplied", Unapplied.ToString());
                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@store", Store);

                SqlDataAdapter.Fill(dataTable);
            }
            return dataTable;
        }

        public DataTable GetListofItemsReturnedDetails(string FromDate, string ToDate, string Store, string Sales)
        {
            DataTable dataTable = new DataTable();

            using (SqlDataAdapter SqlDataAdapter = new SqlDataAdapter())
            {
                // Create the command and set its properties

                SqlDataAdapter.SelectCommand = new SqlCommand();
                SqlDataAdapter.SelectCommand.Connection = _connectionProvider.GetConnection();
                SqlDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@DATE1", FromDate);
                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@DATE2", ToDate);
                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@Store", Store);
                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@Sales", Sales);
                SqlDataAdapter.SelectCommand.CommandTimeout = 3000;

                // Assign the SQL to the command object
                SqlDataAdapter.SelectCommand.CommandText = "GetstoredetailsReturns";
                SqlDataAdapter.Fill(dataTable);
            }

            return dataTable;
        }

        public DataTable GetApmItemsFromStyle(string style)
        {
            return _helperService.HelperCommon.GetSqlData("SELECT distinct top 2 APM_ITEM.INV_NO, ACC, VND_NO FROM APM_ITEM JOIN APM ON APM.INV_NO = APM_ITEM.INV_NO WHERE STYLE = @style",
                "@style", style);
        }

        public DataTable CheckFileExistOrNot(string FileName)
        {
            DataTable dataTable = new DataTable();
            string CommandText = "select DBO.fn_FileExists('" + FileName + "') as IsExt";
            using (SqlDataAdapter dataAdapter = new SqlDataAdapter())
            {
                try
                {
                    // Create the command and set its properties
                    dataAdapter.SelectCommand = new SqlCommand();
                    dataAdapter.SelectCommand.Connection = _connectionProvider.GetConnection();

                    dataAdapter.SelectCommand.CommandType = CommandType.Text;
                    dataAdapter.SelectCommand.CommandText = CommandText;

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

        public DataTable GetUpsInsTableDetails(string ReqColumn)
        {
            DataTable dataTable = new DataTable();
            string CommandText = "select " + ReqColumn + " from ups_ins";
            using (SqlDataAdapter dataAdapter = new SqlDataAdapter())
            {
                try
                {
                    // Create the command and set its properties
                    dataAdapter.SelectCommand = new SqlCommand();
                    dataAdapter.SelectCommand.Connection = _connectionProvider.GetConnection();

                    dataAdapter.SelectCommand.CommandType = CommandType.Text;
                    dataAdapter.SelectCommand.CommandText = CommandText;

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

        public DataTable GetFileContent(string InvHistPath, string FileName)
        {
            DataTable dataTable = new DataTable();
            string CommandText = string.Format("SELECT * FROM OPENROWSET(BULK '{0}', SINGLE_BLOB) AS BLOB", Path.Combine(InvHistPath, FileName));
            using (SqlDataAdapter dataAdapter = new SqlDataAdapter())
            {
                try
                {
                    // Create the command and set its properties
                    dataAdapter.SelectCommand = new SqlCommand();
                    dataAdapter.SelectCommand.Connection = _connectionProvider.GetConnection();

                    dataAdapter.SelectCommand.CommandType = CommandType.Text;
                    dataAdapter.SelectCommand.CommandText = CommandText;

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

        public DataTable GetPaymentTypes(bool iSLayaway = false)
        {
            //Commented By Rahul 06/28/2021
            return _helperService.HelperCommon.GetSqlData("SELECT UPPER(PAYMENTTYPE)PAYMENTTYPE,FIXED,HIDE, ORDERED FROM PAYMENTTYPES WHERE ISNULL(PAYMENTTYPE,'') <> '' ORDER BY ordered");
            /*return _helperService.HelperCommon.GetSqlData($@";WITH CTE_PAYMENT_METHOD(PAYMENTTYPE,FIXED,REQUIRES_DEPOSIT,BANK_FEE,HIDE,ORDERED, LAYAWAYS,ADD_COG,TRANS_FEE)
                                            AS
                                            (
                                            SELECT UPPER(PAYMENTTYPE)PAYMENTTYPE,MAX(CASE WHEN FIXED=1 THEN 1 ELSE 0 END) FIXED,
                                            MAX(CASE WHEN REQUIRES_DEPOSIT=1 THEN 1 ELSE 0 END) REQUIRES_DEPOSIT,MAX(BANK_FEE) BANK_FEE,  MAX(CASE WHEN HIDE=1 THEN 1 ELSE 0 END) HIDE, MAX(ORDERED) ORDERED,MAX(CASE WHEN LAYAWAYS=1 THEN 1 ELSE 0 END) LAYAWAYS ,max(CASE WHEN add_cog=1 THEN 1 ELSE 0 END) ADD_COG,TRANS_FEE
                                            FROM PAYMENTTYPES GROUP BY PAYMENTTYPE HAVING ISNULL(PAYMENTTYPE,'') <> ''
                                            )	
                                            SELECT PAYMENTTYPE,CAST(FIXED AS BIT) FIXED,CAST(REQUIRES_DEPOSIT AS BIT) REQUIRES_DEPOSIT,CAST(BANK_FEE AS DECIMAL(8,4)) BANK_FEE, CAST(HIDE AS BIT) HIDE,ORDERED, CAST(LAYAWAYS AS BIT) LAYAWAY,CAST(add_cog AS BIT) ADD_COG,TRANS_FEE
                                            FROM CTE_PAYMENT_METHOD  WHERE HIDE=0 AND LAYAWAYS={(iSLayaway ? "1" : "LAYAWAYS")} ORDER BY ORDERED");*/
        }

        public DataTable Insert_DeletetPaymentType(string paymt_type, string DeletedPAYMENT)
        {
            DataTable dataTable = new DataTable();

            //string p = "<?xml version=\"1.0\" encoding=\"utf-16\"?><DocumentElement><Payment><PAYMENTTYPE>CASH</PAYMENTTYPE><FIXED>true</FIXED><REQUIRES_DEPOSIT>false</REQUIRES_DEPOSIT><BANK_FEE>1.7500</BANK_FEE><TRANS_FEE>10.00</TRANS_FEE><HIDE>false</HIDE><ORDERED>1</ORDERED><LAYAWAY>false</LAYAWAY><Add_COG>true</Add_COG></Payment><Payment><PAYMENTTYPE>CC SWIPE</PAYMENTTYPE><FIXED>true</FIXED><REQUIRES_DEPOSIT>false</REQUIRES_DEPOSIT><BANK_FEE>2.0000</BANK_FEE><TRANS_FEE>0.00</TRANS_FEE><HIDE>false</HIDE><ORDERED>2</ORDERED><LAYAWAY>false</LAYAWAY><Add_COG>false</Add_COG></Payment><Payment><PAYMENTTYPE>CC TERMINAL</PAYMENTTYPE><FIXED>true</FIXED><REQUIRES_DEPOSIT>false</REQUIRES_DEPOSIT><BANK_FEE>2.0000</BANK_FEE><TRANS_FEE>0.00</TRANS_FEE><HIDE>false</HIDE><ORDERED>3</ORDERED><LAYAWAY>false</LAYAWAY><Add_COG>false</Add_COG></Payment><Payment><PAYMENTTYPE>VIRTUAL CC TERMINAL</PAYMENTTYPE><FIXED>false</FIXED><REQUIRES_DEPOSIT>false</REQUIRES_DEPOSIT><BANK_FEE>0.0000</BANK_FEE><TRANS_FEE>0.00</TRANS_FEE><HIDE>false</HIDE><ORDERED>5</ORDERED><LAYAWAY>false</LAYAWAY><Add_COG>false</Add_COG></Payment><Payment><PAYMENTTYPE>ON ACCOUNT (PAY LATER)</PAYMENTTYPE><FIXED>false</FIXED><REQUIRES_DEPOSIT>true</REQUIRES_DEPOSIT><BANK_FEE>2.0000</BANK_FEE><TRANS_FEE>0.00</TRANS_FEE><HIDE>false</HIDE><ORDERED>8</ORDERED><LAYAWAY>false</LAYAWAY><Add_COG>false</Add_COG></Payment><Payment><PAYMENTTYPE>STORE CREDIT</PAYMENTTYPE><FIXED>true</FIXED><REQUIRES_DEPOSIT>true</REQUIRES_DEPOSIT><BANK_FEE>2.0000</BANK_FEE><TRANS_FEE>0.00</TRANS_FEE><HIDE>false</HIDE><ORDERED>9</ORDERED><LAYAWAY>false</LAYAWAY><Add_COG>false</Add_COG></Payment><Payment><PAYMENTTYPE>GIFT CARD</PAYMENTTYPE><FIXED>true</FIXED><REQUIRES_DEPOSIT>true</REQUIRES_DEPOSIT><BANK_FEE>2.0000</BANK_FEE><TRANS_FEE>0.00</TRANS_FEE><HIDE>false</HIDE><ORDERED>10</ORDERED><LAYAWAY>false</LAYAWAY><Add_COG>false</Add_COG></Payment><Payment><PAYMENTTYPE>REFERRAL POINTS</PAYMENTTYPE><FIXED>true</FIXED><REQUIRES_DEPOSIT>true</REQUIRES_DEPOSIT><BANK_FEE>2.0000</BANK_FEE><TRANS_FEE>0.00</TRANS_FEE><HIDE>false</HIDE><ORDERED>11</ORDERED><LAYAWAY>false</LAYAWAY><Add_COG>false</Add_COG></Payment><Payment><PAYMENTTYPE>LAYAWAY</PAYMENTTYPE><FIXED>true</FIXED><REQUIRES_DEPOSIT>false</REQUIRES_DEPOSIT><BANK_FEE>2.0000</BANK_FEE><TRANS_FEE>0.00</TRANS_FEE><HIDE>false</HIDE><ORDERED>12</ORDERED><LAYAWAY>false</LAYAWAY><Add_COG>false</Add_COG></Payment><Payment><PAYMENTTYPE>PAYPAL</PAYMENTTYPE><FIXED>true</FIXED><REQUIRES_DEPOSIT>true</REQUIRES_DEPOSIT><BANK_FEE>2.0000</BANK_FEE><TRANS_FEE>0.00</TRANS_FEE><HIDE>false</HIDE><ORDERED>13</ORDERED><LAYAWAY>false</LAYAWAY><Add_COG>false</Add_COG></Payment><Payment><PAYMENTTYPE>CHECK</PAYMENTTYPE><FIXED>true</FIXED><REQUIRES_DEPOSIT>true</REQUIRES_DEPOSIT><BANK_FEE>5.0000</BANK_FEE><TRANS_FEE>2.23</TRANS_FEE><HIDE>false</HIDE><ORDERED>22</ORDERED><LAYAWAY>false</LAYAWAY><Add_COG>false</Add_COG></Payment><Payment><PAYMENTTYPE>WELLS-12MO</PAYMENTTYPE><FIXED>false</FIXED><REQUIRES_DEPOSIT>true</REQUIRES_DEPOSIT><BANK_FEE>2.0000</BANK_FEE><TRANS_FEE>1.23</TRANS_FEE><HIDE>false</HIDE><ORDERED>23</ORDERED><LAYAWAY>false</LAYAWAY><Add_COG>false</Add_COG></Payment><Payment><PAYMENTTYPE>WELLS-6 MO</PAYMENTTYPE><FIXED>false</FIXED><REQUIRES_DEPOSIT>true</REQUIRES_DEPOSIT><BANK_FEE>2.0000</BANK_FEE><TRANS_FEE>0.00</TRANS_FEE><HIDE>false</HIDE><ORDERED>24</ORDERED><LAYAWAY>false</LAYAWAY><Add_COG>false</Add_COG></Payment><Payment><PAYMENTTYPE>LOYALTY POINTS</PAYMENTTYPE><FIXED>false</FIXED><REQUIRES_DEPOSIT>false</REQUIRES_DEPOSIT><BANK_FEE>0.0000</BANK_FEE><TRANS_FEE>0.00</TRANS_FEE><HIDE>false</HIDE><ORDERED>29</ORDERED><LAYAWAY>false</LAYAWAY><Add_COG>false</Add_COG></Payment><Payment><PAYMENTTYPE>OTHER</PAYMENTTYPE><FIXED>false</FIXED><REQUIRES_DEPOSIT>false</REQUIRES_DEPOSIT><BANK_FEE>0.0000</BANK_FEE><TRANS_FEE>0.00</TRANS_FEE><HIDE>false</HIDE><ORDERED>32</ORDERED><LAYAWAY>false</LAYAWAY><Add_COG>false</Add_COG></Payment><Payment><PAYMENTTYPE>TEST2</PAYMENTTYPE><FIXED>false</FIXED><REQUIRES_DEPOSIT>true</REQUIRES_DEPOSIT><BANK_FEE>0.0000</BANK_FEE><TRANS_FEE>0.00</TRANS_FEE><HIDE>false</HIDE><ORDERED>33</ORDERED><LAYAWAY>false</LAYAWAY><Add_COG>false</Add_COG></Payment><Payment><PAYMENTTYPE>AFTERPAY</PAYMENTTYPE><FIXED>true</FIXED><REQUIRES_DEPOSIT>false</REQUIRES_DEPOSIT><BANK_FEE>0.0000</BANK_FEE><TRANS_FEE>0.00</TRANS_FEE><HIDE>false</HIDE><ORDERED>34</ORDERED><LAYAWAY>false</LAYAWAY><Add_COG>false</Add_COG></Payment><Payment><PAYMENTTYPE>ORDERDESK</PAYMENTTYPE><FIXED>true</FIXED><REQUIRES_DEPOSIT>false</REQUIRES_DEPOSIT><BANK_FEE>0.0000</BANK_FEE><TRANS_FEE>0.00</TRANS_FEE><HIDE>false</HIDE><ORDERED>35</ORDERED><LAYAWAY>false</LAYAWAY><Add_COG>false</Add_COG></Payment></DocumentElement>";
            //string p = "<?xml version=\"1.0\" encoding=\"utf-16\"?><DocumentElement><Payment><PAYMENTTYPE>CASH</PAYMENTTYPE><FIXED>true</FIXED><REQUIRES_DEPOSIT>false</REQUIRES_DEPOSIT><BANK_FEE>1.7500</BANK_FEE><TRANS_FEE>10.00</TRANS_FEE><HIDE>false</HIDE><ORDERED>1</ORDERED><Add_COG>true</Add_COG><gl_acc></gl_acc></Payment><Payment><PAYMENTTYPE>CC SWIPE</PAYMENTTYPE><REQUIRES_DEPOSIT>false</REQUIRES_DEPOSIT><BANK_FEE>2.0000</BANK_FEE><TRANS_FEE>0.00</TRANS_FEE><HIDE>false</HIDE><ORDERED>2</ORDERED><Add_COG>false</Add_COG><gl_acc></gl_acc></Payment><Payment><PAYMENTTYPE>CC TERMINAL</PAYMENTTYPE><REQUIRES_DEPOSIT>false</REQUIRES_DEPOSIT><BANK_FEE>2.0000</BANK_FEE><TRANS_FEE>0.00</TRANS_FEE><HIDE>false</HIDE><ORDERED>3</ORDERED><Add_COG>false</Add_COG><gl_acc></gl_acc></Payment><Payment><PAYMENTTYPE>VIRTUAL CC TERMINAL</PAYMENTTYPE><REQUIRES_DEPOSIT>false</REQUIRES_DEPOSIT><BANK_FEE>0.0000</BANK_FEE><TRANS_FEE>0.00</TRANS_FEE><HIDE>false</HIDE><ORDERED>5</ORDERED><Add_COG>false</Add_COG><gl_acc></gl_acc></Payment><Payment><PAYMENTTYPE>ON ACCOUNT (PAY LATER)</PAYMENTTYPE><REQUIRES_DEPOSIT>true</REQUIRES_DEPOSIT><BANK_FEE>2.0000</BANK_FEE><TRANS_FEE>0.00</TRANS_FEE><HIDE>false</HIDE><ORDERED>8</ORDERED><Add_COG>false</Add_COG><gl_acc>1035</gl_acc></Payment><Payment><PAYMENTTYPE>STORE CREDIT</PAYMENTTYPE><REQUIRES_DEPOSIT>true</REQUIRES_DEPOSIT><BANK_FEE>2.0000</BANK_FEE><TRANS_FEE>0.00</TRANS_FEE><HIDE>false</HIDE><ORDERED>9</ORDERED><Add_COG>false</Add_COG><gl_acc></gl_acc></Payment><Payment><PAYMENTTYPE>GIFT CARD</PAYMENTTYPE><REQUIRES_DEPOSIT>true</REQUIRES_DEPOSIT><BANK_FEE>2.0000</BANK_FEE><TRANS_FEE>0.00</TRANS_FEE><HIDE>false</HIDE><ORDERED>10</ORDERED><Add_COG>false</Add_COG><gl_acc></gl_acc></Payment><Payment><PAYMENTTYPE>REFERRAL POINTS</PAYMENTTYPE><REQUIRES_DEPOSIT>true</REQUIRES_DEPOSIT><BANK_FEE>2.0000</BANK_FEE><TRANS_FEE>0.00</TRANS_FEE><HIDE>false</HIDE><ORDERED>11</ORDERED><Add_COG>false</Add_COG><gl_acc></gl_acc></Payment><Payment><PAYMENTTYPE>LAYAWAY</PAYMENTTYPE><REQUIRES_DEPOSIT>false</REQUIRES_DEPOSIT><BANK_FEE>2.0000</BANK_FEE><TRANS_FEE>0.00</TRANS_FEE><HIDE>false</HIDE><ORDERED>12</ORDERED><Add_COG>false</Add_COG><gl_acc>1011</gl_acc></Payment><Payment><PAYMENTTYPE>PAYPAL</PAYMENTTYPE><REQUIRES_DEPOSIT>true</REQUIRES_DEPOSIT><BANK_FEE>2.0000</BANK_FEE><TRANS_FEE>0.00</TRANS_FEE><HIDE>false</HIDE><ORDERED>13</ORDERED><Add_COG>false</Add_COG><gl_acc>1003</gl_acc></Payment><Payment><PAYMENTTYPE>CHECK</PAYMENTTYPE><REQUIRES_DEPOSIT>true</REQUIRES_DEPOSIT><BANK_FEE>5.0000</BANK_FEE><TRANS_FEE>2.23</TRANS_FEE><HIDE>false</HIDE><ORDERED>22</ORDERED><Add_COG>false</Add_COG><gl_acc>1100</gl_acc></Payment><Payment><PAYMENTTYPE>WELLS-12MO</PAYMENTTYPE><REQUIRES_DEPOSIT>true</REQUIRES_DEPOSIT><BANK_FEE>2.0000</BANK_FEE><TRANS_FEE>1.23</TRANS_FEE><HIDE>false</HIDE><ORDERED>23</ORDERED><Add_COG>false</Add_COG><gl_acc>1220</gl_acc></Payment><Payment><PAYMENTTYPE>WELLS-6 MO</PAYMENTTYPE><REQUIRES_DEPOSIT>true</REQUIRES_DEPOSIT><BANK_FEE>2.0000</BANK_FEE><TRANS_FEE>0.00</TRANS_FEE><HIDE>false</HIDE><ORDERED>24</ORDERED><Add_COG>false</Add_COG><gl_acc>1255</gl_acc></Payment><Payment><PAYMENTTYPE>LOYALTY POINTS</PAYMENTTYPE><REQUIRES_DEPOSIT>false</REQUIRES_DEPOSIT><BANK_FEE>0.0000</BANK_FEE><TRANS_FEE>0.00</TRANS_FEE><HIDE>false</HIDE><ORDERED>29</ORDERED><Add_COG>false</Add_COG><gl_acc></gl_acc></Payment><Payment><PAYMENTTYPE>OTHER</PAYMENTTYPE><REQUIRES_DEPOSIT>false</REQUIRES_DEPOSIT><BANK_FEE>0.0000</BANK_FEE><TRANS_FEE>0.00</TRANS_FEE><HIDE>false</HIDE><ORDERED>32</ORDERED><Add_COG>false</Add_COG><gl_acc></gl_acc></Payment><Payment><PAYMENTTYPE>TEST2</PAYMENTTYPE><REQUIRES_DEPOSIT>true</REQUIRES_DEPOSIT><BANK_FEE>0.0000</BANK_FEE><TRANS_FEE>0.00</TRANS_FEE><HIDE>false</HIDE><ORDERED>33</ORDERED><Add_COG>false</Add_COG><gl_acc></gl_acc></Payment><Payment><PAYMENTTYPE>AFTERPAY</PAYMENTTYPE><REQUIRES_DEPOSIT>false</REQUIRES_DEPOSIT><BANK_FEE>0.0000</BANK_FEE><TRANS_FEE>0.00</TRANS_FEE><HIDE>false</HIDE><ORDERED>34</ORDERED><Add_COG>false</Add_COG><gl_acc></gl_acc></Payment><Payment><PAYMENTTYPE>ORDERDESK</PAYMENTTYPE><REQUIRES_DEPOSIT>false</REQUIRES_DEPOSIT><BANK_FEE>0.0000</BANK_FEE><TRANS_FEE>0.00</TRANS_FEE><HIDE>false</HIDE><ORDERED>35</ORDERED><Add_COG>false</Add_COG><gl_acc></gl_acc></Payment></DocumentElement>";
            using (SqlDataAdapter dataAdapter = new SqlDataAdapter())
            {
                // Create the command and set its properties
                dataAdapter.SelectCommand = new SqlCommand();
                dataAdapter.SelectCommand.Connection = _connectionProvider.GetConnection();
                dataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                dataAdapter.SelectCommand.CommandText = @"DELETE_INSERT_PAYMENTS";
                dataAdapter.SelectCommand.Parameters.AddWithValue("@DeletedPAYMENT", DeletedPAYMENT);

                SqlParameter parameter = new SqlParameter();
                parameter.ParameterName = "@PAYMENTTYPE";
                parameter.SqlDbType = System.Data.SqlDbType.Xml;
                parameter.Value = paymt_type;
                dataAdapter.SelectCommand.Parameters.Add(parameter);

                dataAdapter.Fill(dataTable);
            }
            return dataTable;
        }

        public DataRow GetInvoiceByInvNo(string invno)
        {
            return (_helperService.HelperCommon.GetSqlRow(@"select top 1 i.*,it.memo_no,iSNULL(it.IsSpecialItem,0) IsSpecialItem,it.fpon 
                from invoice i 
                left join (select * from IN_ITEMS where trim(INV_NO) =@inv_no) it on i.inv_no = it.inv_no 
                    Where trim(i.inv_no) = @inv_no", "@inv_no", invno.Trim()));
        }

        public bool CheckFixed(string paymt_type)
        {
            DataTable dataTable = new DataTable();
            string CommandText = @"SELECT FIXED FROM PAYMENTTYPES WHERE PAYMENTTYPE=" + @"'" + paymt_type + @"' AND FIXED=1";
            dataTable = _helperService.HelperCommon.GetSqlData(CommandText);
            return _helperService.HelperCommon.DataTableOK(dataTable);
        }

        public List<SelectListItem> getAllSales()
        {
            DataTable dataTable = _helperService.HelperCommon.GetSqlData("select distinct CODE from SALESMEN where CODE != '' and CODE is not null order by CODE");

            List<SelectListItem> salesmanList = new List<SelectListItem>();
            salesmanList.Add(new SelectListItem() { Text = "All", Value = "" });
            if (dataTable.Rows.Count > 0)
            {
                foreach (DataRow dr in dataTable.Rows)
                {
                    salesmanList.Add(new SelectListItem() { Text = dr["CODE"].ToString().Trim(), Value = dr["CODE"].ToString().Trim() });
                }
            }
            return salesmanList;
        }

        public DataTable GetListofPickedupItems(string Fdate, string Tdate, string store = "", string salesman = "", decimal Purchase = 0)
        {
            DataTable dataTable = new DataTable();

            using (SqlDataAdapter dataAdapter = new SqlDataAdapter())
            {
                dataAdapter.SelectCommand = new SqlCommand();
                dataAdapter.SelectCommand.Connection = _connectionProvider.GetConnection();
                dataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                dataAdapter.SelectCommand.CommandText = "GetListOfpickedupItems";

                dataAdapter.SelectCommand.Parameters.AddWithValue("@DATE1", Fdate);
                dataAdapter.SelectCommand.Parameters.AddWithValue("@DATE2", Tdate);
                dataAdapter.SelectCommand.Parameters.AddWithValue("@Store", store);
                dataAdapter.SelectCommand.Parameters.AddWithValue("@Sales", salesman);
                dataAdapter.SelectCommand.Parameters.AddWithValue("@Purchase", Purchase);
                dataAdapter.SelectCommand.CommandTimeout = 6000;
                dataAdapter.Fill(dataTable);

                return dataTable.Rows.Count > 0 ? dataTable : null;
            }
        }

        public DataTable GetInvoiceReportDetails(
            DateTime? FROMDATE, DateTime? TODATE, string Store, string Sales, bool IsLaywayUnpaid, bool saletax,
            bool IsInvoicedReport = false, bool _DateValueCheck = false)
        {
            DataTable dataTable = new DataTable();

            // Use 'using' for automatic disposal of resources
            using (var connection = _connectionProvider.GetConnection())
            using (var command = new SqlCommand(!IsInvoicedReport ? "GetStoreDetails" : "GetInvoicedReports", connection))
            using (var adapter = new SqlDataAdapter(command))
            {
                // Set command properties
                command.CommandType = CommandType.StoredProcedure;
                command.CommandTimeout = 3000;

                // Add parameters using the helper method to avoid redundancy
                AddSqlParameter(command, "@DATE1", FROMDATE);
                AddSqlParameter(command, "@DATE2", TODATE);
                AddSqlParameter(command, "@Store", Store);
                AddSqlParameter(command, "@Sales", Sales);
                AddSqlParameter(command, "@IsLaywayUnpaid", IsLaywayUnpaid);
                AddSqlParameter(command, "@SalesTax", saletax);

                // Add conditional parameter if not InvoicedReport
                if (!IsInvoicedReport)
                {
                    AddSqlParameter(command, "@ByPickup", _DateValueCheck);
                }

                // Execute the query and fill the DataTable
                adapter.Fill(dataTable);
            }

            return dataTable;
        }


        public DataTable GetWarranrySales(DateTime? FromDt, DateTime? ToDt)
        {
            DataTable DtRetunValues = _helperService.HelperCommon.GetSqlData($@"SELECT ISNULL(it.INV_NO,'') As Invoice,ISNULL(I.DATE,'') As DATE,ISNULL(I.ACC,'') AS ACC,ISNULL(It.STYLE,'') AS STYLE,
                                         ISNULL(It.warranty, '') AS Warranty, isnull(It.warranty_cost, '') Warranty_Cost  
                                         FROM IN_ITEMS IT  join INVOICE I ON It.INV_NO = I.INV_NO
                                         Where It.warranty_cost <> 0 AND TRY_CAST(I.Date As Date) Between TRY_CAST('{_helperService.HelperCommon.setSQLDateTime(Convert.ToDateTime(FromDt))}' as Date) and TRY_CAST('{_helperService.HelperCommon.setSQLDateTime(Convert.ToDateTime(ToDt))}' as Date)");
            return DtRetunValues.Rows.Count > 0 ? DtRetunValues : null;
        }

        public void AddSqlParameter(SqlCommand command, string parameterName, object value)
        {
            if (value == null)
                command.Parameters.AddWithValue(parameterName, DBNull.Value);
            else
                command.Parameters.AddWithValue(parameterName, value);
        }

        public DataTable checkglcodereason()
        {
            return _helperService.HelperCommon.GetSqlData(@"SELECT REASON,GL_ACC,CAST(b.NAME AS NVARCHAR(20)) [NAME] from crdt_reason a with (nolock) left join gl_accs b on ltrim(rtrim(a.gl_acc)) = ltrim(rtrim(b.acc))");
        }

        public bool AddXmlvaluesave(string spName, string xmlParameterName, string xmlValue, string param1Name = "",
            string paramValue1 = "", string param2Name = "", string paramValue2 = "")
        {
            using (var connection = _connectionProvider.GetConnection())
            using (var sqlCommand = new SqlCommand(spName, connection))
            {
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.CommandTimeout = 3000;
                sqlCommand.Parameters.AddWithValue(xmlParameterName, xmlValue);
                if (!string.IsNullOrWhiteSpace(param1Name))
                    sqlCommand.Parameters.AddWithValue(param1Name, paramValue1);
                if (!string.IsNullOrWhiteSpace(param2Name))
                    sqlCommand.Parameters.AddWithValue(param2Name, paramValue2);
                connection.Open();
                int rowsAffected = sqlCommand.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }

        public bool DeleteCreditReason(string reason, string glType)
        {
            using (var connection = _connectionProvider.GetConnection())
            using (var dbCommand = new SqlCommand("DeleteCreditReason", connection))
            {
                dbCommand.CommandType = CommandType.StoredProcedure;
                dbCommand.CommandTimeout = 12000;
                dbCommand.Parameters.AddWithValue("@reason", reason);
                dbCommand.Parameters.AddWithValue("@gltype", glType);
                connection.Open();
                return dbCommand.ExecuteNonQuery() > 0;
            }
        }
    }
}
