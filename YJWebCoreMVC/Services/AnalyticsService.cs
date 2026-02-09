/*
 * Phanindra 09/04/2024 Removed the common functions as we kept them in Helper
 * Phanindra 09/06/2024 Updated Prices to have 2 decimals
 * Phanindra 09/09/2024 Updated Prices to have 2 decimals wherever missed
 * Phanindra 09/10/2024 Added method for TotalMonthlySalesForACustomer form
 * Phanindra 09/22/2024 Added the missing columns in annualsalescomparison form
 * Manoj     12/01/2024 BrandsFromStyle property
 * Manoj     12/08/2025 Added  getInactiveStyles method
 * Manoj     12/11/2024 Added GetPartnerSales method
 * Manoj     01/27/2026 Added GetComponentSalesAnalysis Method
 */
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using System.Data;
using YJWebCoreMVC.Models;

namespace YJWebCoreMVC.Services
{
    public class AnalyticsService
    {
        private readonly ConnectionProvider _connectionProvider;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly HelperCommonService _helperCommonService;
        private readonly IWebHostEnvironment _env;
        private readonly string _companyName;
        private readonly string _storeCodeInUse;
        private readonly string _loggedUser;

        public AnalyticsService(ConnectionProvider connectionProvider, IHttpContextAccessor httpContextAccessor, HelperCommonService helperCommonService, IWebHostEnvironment env)
        {
            _connectionProvider = connectionProvider;
            _httpContextAccessor = httpContextAccessor;
            _helperCommonService = helperCommonService;
            _companyName = _httpContextAccessor.HttpContext?.Session.GetString("COMPANYNAME");
            _storeCodeInUse = _httpContextAccessor.HttpContext?.Session.GetString("STORE_CODE");
            _loggedUser = _httpContextAccessor.HttpContext?.Session.GetString("UserId");
            _env = env;
        }

        public List<AnalyticsModel> getQtyOnHandDetails(string ccode, string fdate, string tdate, string ct, string subcat, string metalval, string brandval, string fromstyle, string tostyle, string strVendor)
        {
            DataTable dataTable = new DataTable();
            List<AnalyticsModel> lstSoldorInStock = new List<AnalyticsModel>();

            using (SqlDataAdapter dataAdapter = new SqlDataAdapter())
            {
                try
                {
                    // Create the command and set its properties
                    dataAdapter.SelectCommand = new SqlCommand();
                    dataAdapter.SelectCommand.Connection = _connectionProvider.GetConnection();

                    dataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                    //dataAdapter.SelectCommand.CommandText = "qtysold_test";
                    dataAdapter.SelectCommand.CommandText = "QtySold";
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@date1", Convert.ToDateTime(fdate));
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@date2", Convert.ToDateTime(tdate));
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@brand", brandval ??= "");
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@category", ct ??= "");
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@subcat", subcat ??= "");
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@metal", metalval ??= "");
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@cacc", ccode ??= "");
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@STYLEFROM", fromstyle ??= "");
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@STYLETO", tostyle ??= "");
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@Vendor", strVendor ??= "");
                    // Fill the datatable From adapter
                    dataAdapter.Fill(dataTable);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                int decimalPlaces = 0;

                var value = _httpContextAccessor.HttpContext?.Session.GetString("DecimalsInPrices");

                if (!string.IsNullOrWhiteSpace(value))
                {
                    int.TryParse(value, out decimalPlaces);
                }
                // Get the datarow from the table
                //foreach (DataRow dr in dataTable.Select("INV_NO='     1'").CopyToDataTable().Rows)
                foreach (DataRow dr in dataTable.Rows)
                {
                    //SELL_PRICE = 0; // Default value
                    lstSoldorInStock.Add(new AnalyticsModel()
                    {
                        //keep doing for all fields like this

                        ACC = dr["acc"].ToString(),
                        //PON = dr["PON"].ToString(),
                        STYLE_NO = dr["style"].ToString(),
                        brand = dr["brand"].ToString(),
                        category = dr["category"].ToString(),
                        subcat = dr["subcat"].ToString(),
                        metal = dr["metal"].ToString(),
                        QTY = Math.Round(Decimal.Parse(dr["qty"].ToString()), 2),
                        PRICE = Math.Round(Decimal.Parse(dr["amount"].ToString()), 2),
                        On_memo = Math.Round(Decimal.Parse(dr["on_memo"].ToString()), 2),
                        In_STOCK = Math.Round(Decimal.Parse(dr["IN_STOCK"].ToString()), 2),
                        StoreNo = dr["Store_no"].ToString().Trim(),
                        //Qty_Open = Qty_Open,
                        //OrderDate = Helper.TryDateTimeParse(dr["OrderDate"].ToString()),
                    });
                }
                return lstSoldorInStock;
            }
        }

        public List<SelectListItem> getStockImages()
        {
            List<SelectListItem> imageList = new List<SelectListItem>();
            DataTable dataTable = new DataTable();
            using (SqlDataAdapter dataAdapter = new SqlDataAdapter())
            {
                try
                {
                    // Create the command and set its properties
                    dataAdapter.SelectCommand = new SqlCommand();
                    dataAdapter.SelectCommand.Connection = _connectionProvider.GetConnection();

                    dataAdapter.SelectCommand.CommandType = CommandType.Text;
                    dataAdapter.SelectCommand.CommandText = "select orig_name from styl_images";

                    // Fill the datatable From adapter
                    dataAdapter.Fill(dataTable);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                // Get the datarow from the table
                imageList.Add(new SelectListItem()
                {
                    Text = "All",
                    Value = "All"
                });
                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        imageList.Add(new SelectListItem()
                        {
                            //keep doing for all fields like this
                            Text = dr["cast_code"].ToString().Trim(),
                            Value = dr["cast_code"].ToString().Trim()
                        });
                    }
                }
                return imageList;
            }
        }

        public List<SelectListItem> getStockImages(string styleno)
        {
            List<SelectListItem> imagesList = new List<SelectListItem>();
            DataTable dataTable = new DataTable();
            using (SqlDataAdapter dataAdapter = new SqlDataAdapter())
            {
                try
                {
                    // Create the command and set its properties
                    dataAdapter.SelectCommand = new SqlCommand();
                    dataAdapter.SelectCommand.Connection = _connectionProvider.GetConnection();
                    dataAdapter.SelectCommand.CommandType = CommandType.Text;
                    //dataAdapter.SelectCommand.CommandText = "Select imagespath,* from ups_ins"; //"SELECT orig_name from styl_images";
                    dataAdapter.SelectCommand.CommandText = "SELECT orig_name from styl_images WHERE STYLE LIKE '" + styleno + "'";
                    // Fill the datatable From adapter
                    dataAdapter.Fill(dataTable);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            if (dataTable.Rows.Count > 0)
            {
                foreach (DataRow dr in dataTable.Rows)
                {
                    imagesList.Add(new SelectListItem()
                    {
                        //keep doing for all fields like this
                        Text = dr["orig_name"].ToString().Trim(),
                        Value = dr["orig_name"].ToString().Trim()
                    });
                }
            }
            return imagesList;
        }

        public List<AnalyticsModel> getBestSellerCategoryReportDetails(string ccode, string fdate, string tdate, string metalval, string brandval, string fromstyle, string tostyle, string strVendor)
        {
            DataTable dataTable = new DataTable();
            List<AnalyticsModel> lstbestSellerCategoryReport = new List<AnalyticsModel>();

            using (SqlDataAdapter dataAdapter = new SqlDataAdapter())
            {
                try
                {
                    // Create the command and set its properties
                    dataAdapter.SelectCommand = new SqlCommand();
                    dataAdapter.SelectCommand.Connection = _connectionProvider.GetConnection();

                    dataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                    dataAdapter.SelectCommand.CommandText = "BestSellCaregory";
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@date1", Convert.ToDateTime(fdate));
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@date2", Convert.ToDateTime(tdate));
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@brand", brandval ??= "");
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@metal", metalval ??= "");
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@cacc", ccode ??= "");
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@STYLEFROM", fromstyle ??= "");
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@STYLETO", tostyle ??= "");
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@Vendor", strVendor ??= "");
                    //dataAdapter.SelectCommand.Parameters.AddWithValue("@salcode", "");
                    // Fill the datatable From adapter
                    dataAdapter.Fill(dataTable);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                int decimalPlaces = 0;

                var value = _httpContextAccessor.HttpContext?.Session.GetString("DecimalsInPrices");

                if (!string.IsNullOrWhiteSpace(value))
                {
                    int.TryParse(value, out decimalPlaces);
                }
                // Get the datarow from the table
                //foreach (DataRow dr in dataTable.Select("INV_NO='     1'").CopyToDataTable().Rows)
                foreach (DataRow dr in dataTable.Rows)
                {
                    lstbestSellerCategoryReport.Add(new AnalyticsModel()
                    {
                        //keep doing for all fields like this
                        category = dr["category"].ToString(),
                        QTY = Math.Round(Decimal.Parse(dr["qty"].ToString()), 2),
                        PRICE = Math.Round(Decimal.Parse(dr["amount"].ToString()), 2),
                    });
                }
                return lstbestSellerCategoryReport;
            }
        }


        public List<AnalyticsModel> getBestSellerReportDetails(string ccode, string fdate, string tdate, string ct, string subcat, string metalval, string brandval, string fromstyle, string tostyle, string strVendor, string groupval)
        {
            DataTable dataTable = new DataTable();
            List<AnalyticsModel> lstBestSellerReport = new List<AnalyticsModel>();

            using (SqlDataAdapter dataAdapter = new SqlDataAdapter())
            {
                try
                {
                    // Create the command and set its properties
                    dataAdapter.SelectCommand = new SqlCommand();
                    dataAdapter.SelectCommand.Connection = _connectionProvider.GetConnection();

                    dataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                    dataAdapter.SelectCommand.CommandText = "BestSell";
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@date1", Convert.ToDateTime(fdate));
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@date2", Convert.ToDateTime(tdate));
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@brand", brandval ??= "");
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@category", ct ??= "");
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@subcat", subcat ??= "");
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@metal", metalval ??= "");
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@cacc", ccode ??= "");
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@do_best", 1);
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@STYLEFROM", fromstyle ??= "");
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@STYLETO", tostyle ??= "");
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@Vendor", strVendor ??= "");
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@group", groupval ??= "");

                    // Fill the datatable From adapter
                    dataAdapter.Fill(dataTable);

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                int decimalPlaces = 0;

                var value = _httpContextAccessor.HttpContext?.Session.GetString("DecimalsInPrices");

                if (!string.IsNullOrWhiteSpace(value))
                {
                    int.TryParse(value, out decimalPlaces);
                }
                // Get the datarow from the table
                //foreach (DataRow dr in dataTable.Select("INV_NO='     1'").CopyToDataTable().Rows)
                foreach (DataRow dr in dataTable.Rows)
                {
                    decimal QTY = 0;
                    decimal PRICE = 0;
                    if (!string.IsNullOrEmpty(dr["qty"].ToString()))
                    {
                        QTY = Math.Round(Decimal.Parse(dr["qty"].ToString()), 2);
                    }
                    if (!string.IsNullOrEmpty(dr["amount"].ToString()))
                    {
                        PRICE = Math.Round(Decimal.Parse(dr["amount"].ToString()), 2);
                    }
                    lstBestSellerReport.Add(new AnalyticsModel()
                    {
                        STYLE_NO = dr["style"].ToString(),
                        brand = dr["brand"].ToString(),
                        group = dr["group"].ToString(),
                        category = dr["category"].ToString(),
                        subcat = dr["subcat"].ToString(),
                        metal = dr["metal"].ToString(),
                        QTY = QTY,
                        PRICE = PRICE,
                    });
                }
                return lstBestSellerReport;
            }
        }

        public List<AnalyticsModel> getWorstSellerReportDetails(string ccode, string fdate, string tdate, string ct, string subcat, string metalval, string brandval, string fromstyle, string tostyle, string strVendor, string groupval)
        {
            DataTable dataTable = new DataTable();
            List<AnalyticsModel> lstWorstSellerReport = new List<AnalyticsModel>();

            using (SqlDataAdapter dataAdapter = new SqlDataAdapter())
            {
                try
                {
                    // Create the command and set its properties
                    dataAdapter.SelectCommand = new SqlCommand();
                    dataAdapter.SelectCommand.Connection = _connectionProvider.GetConnection();

                    dataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                    dataAdapter.SelectCommand.CommandText = "BestSell";
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@date1", Convert.ToDateTime(fdate));
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@date2", Convert.ToDateTime(tdate));
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@brand", brandval??= "");
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@category", ct ??= "");
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@subcat", subcat ??= "");
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@metal", metalval ??= "");
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@cacc", ccode ??= "");
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@do_best", 0);
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@STYLEFROM", fromstyle ??= "");
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@STYLETO", tostyle ??= "");
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@Vendor", strVendor ??= "");
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@group", groupval ??= "");
                    // Fill the datatable From adapter
                    dataAdapter.Fill(dataTable);

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                int decimalPlaces = 0;

                var value = _httpContextAccessor.HttpContext?.Session.GetString("DecimalsInPrices");

                if (!string.IsNullOrWhiteSpace(value))
                {
                    int.TryParse(value, out decimalPlaces);
                }
                // Get the datarow from the table
                //foreach (DataRow dr in dataTable.Select("INV_NO='     1'").CopyToDataTable().Rows)
                decimal QTY = 0;
                decimal PRICE = 0;
                foreach (DataRow dr in dataTable.Rows)
                {
                    if (!string.IsNullOrEmpty(dr["qty"].ToString()))
                    {
                        QTY = Math.Round(Decimal.Parse(dr["qty"].ToString()), 2);
                    }
                    if (!string.IsNullOrEmpty(dr["amount"].ToString()))
                    {
                        PRICE = Math.Round(Decimal.Parse(dr["amount"].ToString()), 2);
                    }
                    lstWorstSellerReport.Add(new AnalyticsModel()
                    {
                        STYLE_NO = dr["style"].ToString(),
                        brand = dr["brand"].ToString(),
                        category = dr["category"].ToString(),
                        subcat = dr["subcat"].ToString(),
                        metal = dr["metal"].ToString(),
                        QTY = QTY,
                        PRICE = PRICE,
                    });
                }
                return lstWorstSellerReport;
            }
        }


        public List<AnalyticsModel> getAnnualPaymentsReceivedDetails(string strFrom1, string strTo1, string strFrom2, string strTo2, string strFrom3, string strTo3)
        {
            DataTable dataTable = new DataTable();
            List<AnalyticsModel> lstAnnualPaymentsReceived = new List<AnalyticsModel>();

            using (SqlDataAdapter dataAdapter = new SqlDataAdapter())
            {
                try
                {
                    dataAdapter.SelectCommand = new SqlCommand();
                    dataAdapter.SelectCommand.Connection = _connectionProvider.GetConnection();

                    dataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                    dataAdapter.SelectCommand.CommandText = "Pay_Annual";
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@from1", strFrom1 ??= "");
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@thru1", strTo1 ??= "");
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@from2", strFrom2 ??= "");
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@thru2", strTo2 ??= "");
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@from3", strFrom3 ??= "");
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@thru3", strTo3 ??= "");
                    // Fill the datatable From adapter
                    dataAdapter.Fill(dataTable);

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                int decimalPlaces = 0;

                var value = _httpContextAccessor.HttpContext?.Session.GetString("DecimalsInPrices");

                if (!string.IsNullOrWhiteSpace(value))
                {
                    int.TryParse(value, out decimalPlaces);
                }

                foreach (DataRow dr in dataTable.Rows)
                {
                    lstAnnualPaymentsReceived.Add(new AnalyticsModel()
                    {
                        ACC = dr["acc"].ToString(),
                        name = dr["name"].ToString(),
                        paid1 = dr["paid1"].ToString(),
                        increase1 = dr["increase1"].ToString(),
                        paid2 = dr["paid2"].ToString(),
                        increase2 = dr["increase2"].ToString(),
                        paid3 = dr["paid3"].ToString(),
                    });
                }
                return lstAnnualPaymentsReceived;
            }
        }


        public List<AnalyticsModel> getAnnualSalesComparisonDetails(string strFrom1, string strTo1, string strFrom2, string strTo2, string strFrom3, string strTo3, string strchkLayawaysUnpaid, string striSByPickupDate, string strexclude)
        {
            DataTable dataTable = new DataTable();
            List<AnalyticsModel> lstAnnualPaymentsReceived = new List<AnalyticsModel>();

            using (SqlDataAdapter dataAdapter = new SqlDataAdapter())
            {
                try
                {
                    dataAdapter.SelectCommand = new SqlCommand();
                    dataAdapter.SelectCommand.Connection = _connectionProvider.GetConnection();

                    dataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                    dataAdapter.SelectCommand.CommandText = "invoice_annual";
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@from1", strFrom1 ??= "");
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@thru1", strTo1 ??= "");
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@from2", strFrom2 ??= "");
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@thru2", strTo2 ??= "");
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@from3", strFrom3 ??= "");
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@thru3", strTo3 ??= "");
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@iSLayawayUnpaid", strchkLayawaysUnpaid ??= "");
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@iSByPickupDate", striSByPickupDate ??= "");
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@Dealer", 0);
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@excludeDealersNonDealers", strexclude ??= "");
                    // Fill the datatable From adapter
                    dataAdapter.Fill(dataTable);

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                int decimalPlaces = 0;

                var value = _httpContextAccessor.HttpContext?.Session.GetString("DecimalsInPrices");

                if (!string.IsNullOrWhiteSpace(value))
                {
                    int.TryParse(value, out decimalPlaces);
                }

                DateTime date;
                string strFormattedDate;
                foreach (DataRow dr in dataTable.Rows)
                {
                    date = DateTime.Parse(dr["date"].ToString());
                    strFormattedDate = date.ToString("yyyy-MM-dd");
                    lstAnnualPaymentsReceived.Add(new AnalyticsModel()
                    {
                        ACC = dr["acc"].ToString(),
                        name = dr["name"].ToString(),
                        paid1 = dr["total1"].ToString(),
                        increase1 = dr["increase1"].ToString(),
                        paid2 = dr["total2"].ToString(),
                        increase2 = dr["increase2"].ToString(),
                        paid3 = dr["total3"].ToString(),
                        email = dr["Email"].ToString(),
                        Tel = dr["Tel"].ToString(),
                        Address1 = dr["Address1"].ToString(),
                        City = dr["City"].ToString(),
                        State = dr["State"].ToString(),
                        Zip = dr["Zip"].ToString(),
                        strFormattedDate = strFormattedDate,
                        Salesman = dr["Salesman"].ToString(),
                    });
                }
                return lstAnnualPaymentsReceived;
            }
        }


        public List<AnalyticsModel> getEndofMonthDetails(string strFrom1, string strTo1, string striSByPickupDate, string strstore)
        {
            DataTable dataTable = new DataTable();
            List<AnalyticsModel> lstEndofMonth = new List<AnalyticsModel>();

            using (SqlDataAdapter dataAdapter = new SqlDataAdapter())
            {
                try
                {
                    dataAdapter.SelectCommand = new SqlCommand();
                    dataAdapter.SelectCommand.Connection = _connectionProvider.GetConnection();

                    dataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                    if (strstore == "All" || strstore == "")
                    {
                        dataAdapter.SelectCommand.CommandText = "ENDOFMONTH";
                    }
                    else
                    {
                        dataAdapter.SelectCommand.CommandText = "ENDOFMONTHWithStores";
                        dataAdapter.SelectCommand.Parameters.AddWithValue("@Store", strstore ??= "");
                    }

                    dataAdapter.SelectCommand.Parameters.AddWithValue("@FROMDATE", strFrom1 ??= "");
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@TODATE", strTo1 ??= "");
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@byPickup", striSByPickupDate ??= "");
                    // Fill the datatable From adapter
                    dataAdapter.Fill(dataTable);

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                int decimalPlaces = 0;

                var value = _httpContextAccessor.HttpContext?.Session.GetString("DecimalsInPrices");

                if (!string.IsNullOrWhiteSpace(value))
                {
                    int.TryParse(value, out decimalPlaces);
                }

                foreach (DataRow dr in dataTable.Rows)
                {
                    if (dr["NAME"].ToString().Trim() != "")
                    {
                        decimal PRICE = 0;
                        if (!string.IsNullOrEmpty(dr["AMOUNT"].ToString()))
                        {
                            PRICE = Decimal.Parse(dr["AMOUNT"].ToString());
                        }
                        lstEndofMonth.Add(new AnalyticsModel()
                        {
                            name = dr["NAME"].ToString(),
                            PRICE = PRICE,

                        });
                    }

                }
                return lstEndofMonth;
            }
        }


        public List<AnalyticsModel> getTotalMonthlySalesDetails(string strFrom1, string strTo1, string strchkLayawaysUnpaid, string striSByPickupDate)
        {
            DataTable dataTable = new DataTable();
            List<AnalyticsModel> lstTotalMonthlySales = new List<AnalyticsModel>();

            using (SqlDataAdapter dataAdapter = new SqlDataAdapter())
            {
                try
                {
                    dataAdapter.SelectCommand = new SqlCommand();
                    dataAdapter.SelectCommand.Connection = _connectionProvider.GetConnection();

                    dataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                    dataAdapter.SelectCommand.CommandText = "Month_Sales";
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@date1", strFrom1);
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@date2", strTo1);
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@cacc", "");
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@iSLayawayUnpaid", strchkLayawaysUnpaid ??= "");
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@iSByPickupDate", striSByPickupDate ??= "");
                    // Fill the datatable From adapter
                    dataAdapter.Fill(dataTable);

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                int decimalPlaces = 0;

                var value = _httpContextAccessor.HttpContext?.Session.GetString("DecimalsInPrices");

                if (!string.IsNullOrWhiteSpace(value))
                {
                    int.TryParse(value, out decimalPlaces);
                }
                DateTime date;
                string strFormattedDate;
                foreach (DataRow dr in dataTable.Rows)
                {
                    decimal sales = 0, rtv = 0, credits = 0, net = 0, payments = 0;
                    if (!string.IsNullOrEmpty(dr["sales"].ToString()))
                    {
                        sales = Math.Round(Decimal.Parse(dr["sales"].ToString()), 2);
                    }
                    if (!string.IsNullOrEmpty(dr["rtv"].ToString()))
                    {
                        rtv = Math.Round(Decimal.Parse(dr["rtv"].ToString()), 2);
                    }
                    if (!string.IsNullOrEmpty(dr["credits"].ToString()))
                    {
                        credits = Math.Round(Decimal.Parse(dr["credits"].ToString()), 2);
                    }
                    if (!string.IsNullOrEmpty(dr["net"].ToString()))
                    {
                        net = Math.Round(Decimal.Parse(dr["net"].ToString()), 2);
                    }
                    if (!string.IsNullOrEmpty(dr["payments"].ToString()))
                    {
                        payments = Math.Round(Decimal.Parse(dr["payments"].ToString()), 2);
                    }
                    date = DateTime.Parse(dr["date"].ToString());
                    strFormattedDate = date.ToString("yyyy-MM-dd");
                    lstTotalMonthlySales.Add(new AnalyticsModel()
                    {
                        strFormattedDate = strFormattedDate,
                        sales = sales,
                        rtv = rtv,
                        credits = credits,
                        net = net,
                        payments = payments,
                    });
                }
                return lstTotalMonthlySales;
            }
        }


        public List<AnalyticsModel> getTotalMonthlySalesForACustomerDetails(string ccode, string strFrom1, string strTo1, string strchkLayawaysUnpaid, string striSByPickupDate)
        {
            DataTable dataTable = new DataTable();
            List<AnalyticsModel> lstTotalMonthlySales = new List<AnalyticsModel>();

            using (SqlDataAdapter dataAdapter = new SqlDataAdapter())
            {
                try
                {
                    dataAdapter.SelectCommand = new SqlCommand();
                    dataAdapter.SelectCommand.Connection = _connectionProvider.GetConnection();

                    dataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                    dataAdapter.SelectCommand.CommandText = "Month_Sales";
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@date1", strFrom1);
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@date2", strTo1);
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@cacc", ccode ??= "");
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@iSLayawayUnpaid", strchkLayawaysUnpaid ??= "");
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@iSByPickupDate", striSByPickupDate ??= "");
                    // Fill the datatable From adapter
                    dataAdapter.Fill(dataTable);

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                int decimalPlaces = 0;

                var value = _httpContextAccessor.HttpContext?.Session.GetString("DecimalsInPrices");

                if (!string.IsNullOrWhiteSpace(value))
                {
                    int.TryParse(value, out decimalPlaces);
                }

                DateTime date;
                string strFormattedDate;
                foreach (DataRow dr in dataTable.Rows)
                {
                    decimal sales = 0, rtv = 0, credits = 0, net = 0, payments = 0;
                    if (!string.IsNullOrEmpty(dr["sales"].ToString()))
                    {
                        sales = Math.Round(Decimal.Parse(dr["sales"].ToString()), 2);
                    }
                    if (!string.IsNullOrEmpty(dr["rtv"].ToString()))
                    {
                        rtv = Math.Round(Decimal.Parse(dr["rtv"].ToString()), 2);
                    }
                    if (!string.IsNullOrEmpty(dr["credits"].ToString()))
                    {
                        credits = Math.Round(Decimal.Parse(dr["credits"].ToString()), 2);
                    }
                    if (!string.IsNullOrEmpty(dr["net"].ToString()))
                    {
                        net = Math.Round(Decimal.Parse(dr["net"].ToString()), 2);
                    }
                    if (!string.IsNullOrEmpty(dr["payments"].ToString()))
                    {
                        payments = Math.Round(Decimal.Parse(dr["payments"].ToString()), 2);
                    }
                    date = DateTime.Parse(dr["date"].ToString());
                    strFormattedDate = date.ToString("yyyy-MM-dd");
                    lstTotalMonthlySales.Add(new AnalyticsModel()
                    {
                        strFormattedDate = strFormattedDate,
                        sales = sales,
                        rtv = rtv,
                        credits = credits,
                        net = net,
                        payments = payments,
                    });
                }
                return lstTotalMonthlySales;
            }
        }

        public DataTable getInactiveStyles(string ccode, int noOfDay, string ct, string subct, string metalval, string brandval, string fromstyle, string tostyle, string strVendor, string frmdate, string todate, string storeno = "")
        {
            return _helperCommonService.GetStoreProc("InactiveStyles", "@noOfDay", noOfDay.ToString(), "@brand", brandval ??= "", "@category", ct ??= "", "@subcat", subct ??= "", "@metal", metalval ??= "", "@cacc", ccode ??= "", "@STYLEFROM", fromstyle ??= "", "@STYLETO", tostyle ??= "", "@Vendor", strVendor ??= "", "@frmdate", frmdate ??= "", "@dateto", todate ??= "", "@StrStore", storeno ??= "");
        }
        private string getdate(string date1)
        {
            return date1?.Split(' ')[0] ?? string.Empty;
        }

        public DataTable GetPartnerSales(string fdate, string tdate)
        {
            return _helperCommonService.GetStoreProc("GetPartnerSales", "@date1", getdate(fdate), "@date2", getdate(tdate));
        }

        public DataTable GetComponentSalesAnalysis(string date1, string date2, bool IsDiamondDealer = false)
        {
            return _helperCommonService.GetStoreProc("GetComponentSalesAnalysis", "@dateFrom", getdate(date1), "@dateTo", getdate(date2), "@IsDiamondDealer", IsDiamondDealer ? "1" : "0");
        }
    }
}
