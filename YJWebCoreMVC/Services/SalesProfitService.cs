
//  Added by Neetha -  GetProfitByInvoice(), getTotalSaleProfitDetails()- 26 Aug 2024
//   Neetha -  08/27/2024 GetProfitDetailsByCustomer() 
//-- Neetha    08/28/2024 Added getTotalSaleProfitByCategoryMetalDetails() --//
//-- Neetha    08/29/2024 Added GetTotalSalesPerVendorDetails() --//
//-- Neetha    09/04/2024 Added getTotalSaleProfitByCategoryPricesDetails() --//
//-- Neetha    09/06/2024 Added () --getTotalSalesProfitBySalesrep//
//-- Neetha    09/09/2024 Added () --GetSalesCOGProfitBySalesman//
//-- Neetha    09/16/2024 changes in Date and pickupdate checkmarks in getTotalSaleProfitByCategoryPricesDetails() --//
//-- Neetha    09/19/2024  invoice# column added in getTotalSalesProfitBySalesrep//
//-- Neetha    09/19/2024 columns and checkmarks changes GetSalesCOGProfitBySalesman//
//-- Neetha    09/24/2024 added print images checkbox for GetVendorStylesAnalysisDetails.//
//-- Neetha    10/03/2024 Made changes related to store filter .//
//-- Neetha    10/11/2024 Added GetSalesCOGProfitReportDetails.//
//-- Neetha    10/14/2024 Added Sales COG Print Methods.//
//-- Neetha    01/20/2025 Added GetListOfOccasions().//
//-- Neetha    01/23/2025 Added GetAllCustomers Method.//
//-- Neetha    01/28/2025 Made changes in occasionlist filters and columns .//
//-- Neetha    02/17/2025 Added Sales / Customer Follow Up methods .//

using Microsoft.Data.SqlClient;
using System.Data;
using YJWebCoreMVC.Models;

namespace YJWebCoreMVC.Services
{


    public class SalesProfitService
    {
        private readonly ConnectionProvider _connectionProvider;
        private readonly HelperCommonService _helperCommonService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SalesProfitService(ConnectionProvider connectionProvider, IHttpContextAccessor httpContextAccessor, HelperCommonService helperCommonService)
        {
            _connectionProvider = connectionProvider;
            _helperCommonService = helperCommonService;
            _httpContextAccessor = httpContextAccessor;
        }

        #region Detailed Sales Profit methods 
        public List<SalesProfitModel> GetSalesProfitDetails(string ccode, string fdate, string tdate, string ct, string subcat, string metalval, string brandval, string fromstyle, string tostyle, string strVendor,
            string store, string group, bool ShowReorderInfo = false, string sources = "", string ClassGl = "", bool iSLaySpe = false, bool byPickupDate = false, bool CurrectInoviceCost = false, string Salesman = "", bool IsShowrepairinvoices = false, int intchkattr1 = -1, int intchkattr2 = -1, int intchkattr3 = -1, int intchkattr4 = -1, int intchkattr5 = -1, int intchkattr6 = -1)
        {
            var ObjSalesProfit = new SalesProfitModel();
            DataTable dataTable = new DataTable();
            List<SalesProfitModel> lstSalesProfitDetails = new List<SalesProfitModel>();

            using (SqlDataAdapter dataAdapter = new SqlDataAdapter())
            {
                try
                {
                    // Create the command and set its properties
                    dataAdapter.SelectCommand = new SqlCommand();
                    dataAdapter.SelectCommand.Connection = _connectionProvider.GetConnection();

                    dataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                    dataAdapter.SelectCommand.CommandText = ShowReorderInfo ? "det_prft_Reorder" : "det_prft";
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@date1", Convert.ToDateTime(fdate));
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@date2", Convert.ToDateTime(tdate));
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@brand", brandval);
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@category", ct);
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@subcat", subcat);
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@metal", metalval);
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@cacc", ccode);
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@do_sum", "0");
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@STYLEFROM", fromstyle);
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@STYLETO", tostyle);
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@Vendor", strVendor);
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@store", store);
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@group", group);
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@Sources", sources);
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@CLASS_GL", ClassGl);

                    dataAdapter.SelectCommand.Parameters.AddWithValue("@iSLaySpe", iSLaySpe ? "1" : "0");
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@ByWichDate", byPickupDate ? "PICKUPDATE" : "DATE");
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@CurrentorInvoiceCost", CurrectInoviceCost ? "1" : "0");
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@Salesman", Salesman);
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@IsShowrepairinvoices", IsShowrepairinvoices ? "1" : "0");
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@intchkattr1", intchkattr1.ToString());
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@intchkattr2", intchkattr2.ToString());
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@intchkattr3", intchkattr3.ToString());
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@intchkattr4", intchkattr4.ToString());
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@intchkattr5", intchkattr5.ToString());
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@intchkattr6", intchkattr6.ToString());
                    // Fill the datatable From adapter
                    dataAdapter.Fill(dataTable);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                int decimalPlaces = 2; // (int)HttpContext.Current.Session["DecimalsInPrices"];
                // Get the datarow from the table
                //foreach (DataRow dr in dataTable.Select("INV_NO='     1'").CopyToDataTable().Rows)

                if (!IsShowrepairinvoices)
                {
                    DataRow[] foundNoStyle = dataTable.Select("Style = '' OR Style is null ");

                    if (foundNoStyle.Length > 0)
                    {
                        foreach (DataRow dr1 in foundNoStyle)
                            dataTable.Rows.Remove(dr1);
                        dataTable.AcceptChanges();
                    }
                }

                DataTable dtest = dataTable;
                foreach (DataRow dr in dataTable.Rows)
                {
                    string accCode = "", InvNo = "", SaleDateInfo = "", Style1 = "", brandName1 = "", categoryName1 = "", subcategory1 = "";
                    string metalName1 = "", Description1 = "", StoreNo1 = "", CastCode1 = "", VndStyle1 = "", ClassGl1 = "", GroupName1 = "";
                    decimal Amount1 = 0, CostQty1 = 0, Cost1 = 0, Profit1 = 0, RetailAmount1 = 0; int stock1 = 0;
                    int Quantity1 = 0;
                    if (!IsShowrepairinvoices)
                    {
                        if (!string.IsNullOrEmpty(dr["acc"].ToString()))
                            accCode = dr["acc"].ToString();
                        if (!string.IsNullOrEmpty(dr["inv_no"].ToString()))
                            InvNo = dr["inv_no"].ToString();
                        //if (!string.IsNullOrEmpty(dr["date"].ToString()))
                        //    SaleDateInfo = dr["date"].ToString();
                        if (Convert.ToDateTime(dr["date"].ToString()) != DateTime.MinValue)
                            SaleDateInfo = dr["date"].ToString();
                        if (!string.IsNullOrEmpty(dr["style"].ToString()))
                            Style1 = dr["style"].ToString();

                        if (!ShowReorderInfo)
                        {
                            if (!string.IsNullOrEmpty(dr["brand"].ToString()))
                                brandName1 = dr["brand"].ToString();
                            if (!string.IsNullOrEmpty(dr["category"].ToString()))
                                categoryName1 = dr["category"].ToString();
                            if (!string.IsNullOrEmpty(dr["subcat"].ToString()))
                                subcategory1 = dr["subcat"].ToString();
                            if (!string.IsNullOrEmpty(dr["metal"].ToString()))
                                metalName1 = dr["metal"].ToString();
                            if (!string.IsNullOrEmpty(dr["group"].ToString()))
                                GroupName1 = dr["group"].ToString();
                            if (!string.IsNullOrEmpty(dr["Retail"].ToString()))
                                RetailAmount1 = Math.Round(Decimal.Parse(dr["Retail"].ToString()), decimalPlaces);
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(dr["days_instock"].ToString()))
                                stock1 = Convert.ToInt32(dr["days_instock"].ToString());
                        }

                        //decimal qtyValue = Convert.ToDecimal(dr["qty"]);
                        decimal qtyValue = 0;
                        if (!string.IsNullOrEmpty(dr["qty"].ToString()))
                            qtyValue = Convert.ToDecimal(dr["qty"].ToString());
                        Quantity1 = (int)Math.Round(qtyValue);
                        ObjSalesProfit.QuantityNew = Math.Round(qtyValue, decimalPlaces);

                        if (!string.IsNullOrEmpty(dr["desc"].ToString()))
                            Description1 = dr["desc"].ToString();
                        if (!string.IsNullOrEmpty(dr["store_no"].ToString()))
                            StoreNo1 = dr["store_no"].ToString();
                        if (!string.IsNullOrEmpty(dr["Cast_Code"].ToString()))
                            CastCode1 = dr["Cast_Code"].ToString();
                        if (!string.IsNullOrEmpty(dr["vnd_style"].ToString()))
                            VndStyle1 = dr["vnd_style"].ToString();
                        if (!string.IsNullOrEmpty(dr["class_gl"].ToString()))
                            ClassGl1 = dr["class_gl"].ToString();
                        //if (!string.IsNullOrEmpty(dr["qty"].ToString()))
                        //    Quantity1 =Convert.ToInt32(dr["qty"].ToString());
                        if (!string.IsNullOrEmpty(dr["amount"].ToString()))
                            Amount1 = Math.Round(Decimal.Parse(dr["amount"].ToString()), 2);
                        if (!string.IsNullOrEmpty(dr["cost_qty"].ToString()))
                            CostQty1 = Math.Round(Decimal.Parse(dr["cost_qty"].ToString()), 2);
                        //if (!string.IsNullOrEmpty(dr["cost"].ToString()))
                        //    Cost1 = Math.Round(Decimal.Parse(dr["cost"].ToString()), 2);
                        if (!ShowReorderInfo && (!string.IsNullOrEmpty(dr["cost"].ToString())))
                        {
                            Cost1 = Math.Round(CostQty1 * Decimal.Parse(dr["cost"].ToString()), 2);
                        }
                        else
                        {
                            //Cost1 = Math.Round(Decimal.Parse(dr["cost"].ToString()), 2);
                            if (!string.IsNullOrEmpty(dr["cost"].ToString()))
                                Cost1 = Math.Round(Convert.ToDecimal(dr["cost"].ToString()), 2);
                        }
                        if (!string.IsNullOrEmpty(dr["profit"].ToString()))
                            Profit1 = Math.Round(Decimal.Parse(dr["profit"].ToString()), 2);
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(dr["inv_no"].ToString()))
                            InvNo = dr["inv_no"].ToString();
                        if (Convert.ToDateTime(dr["date"].ToString()) != DateTime.MinValue)
                            SaleDateInfo = dr["date"].ToString();
                        if (!string.IsNullOrEmpty(dr["cost"].ToString()))
                            Cost1 = Math.Round(Decimal.Parse(dr["cost"].ToString()), decimalPlaces);
                        if (!string.IsNullOrEmpty(dr["profit"].ToString()))
                            Profit1 = Math.Round(Decimal.Parse(dr["profit"].ToString()), decimalPlaces);
                        if (!string.IsNullOrEmpty(dr["desc"].ToString()))
                            Description1 = dr["desc"].ToString();
                        if (!string.IsNullOrEmpty(dr["price"].ToString()))
                            Amount1 = Math.Round(Decimal.Parse(dr["price"].ToString()), decimalPlaces);
                        if (!string.IsNullOrEmpty(dr["totamount"].ToString()))
                            RetailAmount1 = Math.Round(Decimal.Parse(dr["totamount"].ToString()), decimalPlaces);
                    }


                    lstSalesProfitDetails.Add(new SalesProfitModel()
                    {
                        //keep doing for all fields like this

                        ACC = accCode,//dr["acc"].ToString(),
                        InvoiceNo = InvNo, //dr["inv_no"].ToString(),
                        SaleDate = _helperCommonService.TryDateTimeParse(SaleDateInfo),
                        SaleDate1 = Convert.ToDateTime(SaleDateInfo).ToShortDateString(),
                        Style = Style1, //dr["style"].ToString(),
                        Quantity = Quantity1, //Math.Round(Decimal.Parse(dr["qty"].ToString()), decimalPlaces),
                        Amount = Amount1, //Math.Round(Decimal.Parse(dr["amount"].ToString()), decimalPlaces),
                        QuantityNew = ObjSalesProfit.QuantityNew,
                        CostQty = CostQty1, //Math.Round(Decimal.Parse(dr["cost_qty"].ToString()), decimalPlaces),
                        Cost = Cost1, //Math.Round(Decimal.Parse(dr["cost"].ToString()), decimalPlaces),
                        Profit = Profit1, //Math.Round(Decimal.Parse(dr["profit"].ToString()), decimalPlaces),
                        brandName = brandName1, //dr["brand"].ToString(),
                        categoryName = categoryName1, //dr["category"].ToString(),
                        subcategory = subcategory1, //dr["subcat"].ToString(),
                        metalName = metalName1, //dr["metal"].ToString(),
                        Description = Description1.Trim(), //dr["desc"].ToString(),
                        StoreNo = StoreNo1, //dr["store_no"].ToString().Trim(),
                        CastCode = CastCode1, //dr["Cast_Code"].ToString().Trim(),
                        VndStyle = VndStyle1, //dr["vnd_style"].ToString().Trim(),
                        ClassGl = ClassGl1, //dr["class_gl"].ToString().Trim(),
                        RetailAmount = RetailAmount1, //Math.Round(Decimal.Parse(dr["Retail"].ToString()), decimalPlaces),
                        GroupName = GroupName1, //dr["group"].ToString().Trim()
                        InStock = stock1
                        //Qty_Open = Qty_Open,
                        //OrderDate = _helperCommonService.TryDateTimeParse(dr["OrderDate"].ToString()),
                    });

                }
                //return lstSalesProfitDetails.OrderBy(i => i.ACC).ToList();
                return lstSalesProfitDetails;
            }
        }

        #endregion

        #region Daily Sales Profit methods 

        public List<SalesProfitModel> GetDailySalesProfitDetails(string fdate, string tdate, string store, int allstore = 0)
        {
            DataTable dataTable = new DataTable();
            List<SalesProfitModel> lstSalesProfitDetails = new List<SalesProfitModel>();

            using (SqlDataAdapter dataAdapter = new SqlDataAdapter())
            {
                try
                {
                    // Create the command and set its properties
                    dataAdapter.SelectCommand = new SqlCommand();
                    dataAdapter.SelectCommand.Connection = _connectionProvider.GetConnection();

                    dataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                    dataAdapter.SelectCommand.CommandText = "dailySalesProfit";
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@date1", Convert.ToDateTime(fdate));
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@date2", Convert.ToDateTime(tdate));
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@Store", store);
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@Allstore", allstore);
                    // Fill the datatable From adapter
                    dataAdapter.Fill(dataTable);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                
                int decimalPlaces = int.TryParse(_httpContextAccessor.HttpContext?.Session.GetString("DecimalsInPrices"), out int value) ? value : 0; ;
                // Get the datarow from the table
                //foreach (DataRow dr in dataTable.Select("INV_NO='     1'").CopyToDataTable().Rows)
                foreach (DataRow dr in dataTable.Rows)
                {
                    string SaleDateInfo = "";
                    decimal Amount1 = 0, Cost1 = 0, Profit1 = 0;

                    if (Convert.ToDateTime(dr["date"].ToString()) != DateTime.MinValue)
                        SaleDateInfo = dr["date"].ToString();
                    if (!string.IsNullOrEmpty(dr["amount"].ToString()))
                        Amount1 = Math.Round(Decimal.Parse(dr["amount"].ToString()), decimalPlaces);
                    if (!string.IsNullOrEmpty(dr["cost"].ToString()))
                        Cost1 = Math.Round(Decimal.Parse(dr["cost"].ToString()), decimalPlaces);
                    if (!string.IsNullOrEmpty(dr["profit"].ToString()))
                        Profit1 = Math.Round(Decimal.Parse(dr["profit"].ToString()), decimalPlaces);


                    lstSalesProfitDetails.Add(new SalesProfitModel()
                    {
                        //keep doing for all fields like this
                        SaleDate = _helperCommonService.TryDateTimeParse(SaleDateInfo),
                        SaleDate1 = Convert.ToDateTime(SaleDateInfo).ToShortDateString(),
                        Amount = Amount1,
                        Cost = Cost1,
                        Profit = Profit1
                    });

                }
                return lstSalesProfitDetails;
            }
        }

        //public DataTable getdailyTotalSaleProfit(string fdate, string tdate, string store, int allstore = 0)
        //{
        //    return Data_helperCommonService.GetStoreProc("dailySalesProfit", "@date1", getdate(fdate), "@date2", getdate(tdate), "@Store", store, "@Allstore", Convert.ToString(allstore));
        //}
        #endregion

        #region Profit By Invoice methods 

        public List<SalesProfitModel> GetProfitByInvoice(string ccode, string fdate, string tdate, string ct, string subcat, string metalval, string brandval, string fromstyle, string tostyle, string strVendor,
            int intchkattr1 = -1, int intchkattr2 = -1, int intchkattr3 = -1, int intchkattr4 = -1, int intchkattr5 = -1, int intchkattr6 = -1)
        {
            var ObjSalesProfit = new SalesProfitModel();
            DataTable dataTable = new DataTable();
            List<SalesProfitModel> lstSalesProfitDetails = new List<SalesProfitModel>();

            using (SqlDataAdapter dataAdapter = new SqlDataAdapter())
            {
                try
                {
                    // Create the command and set its properties
                    dataAdapter.SelectCommand = new SqlCommand();
                    dataAdapter.SelectCommand.Connection = _connectionProvider.GetConnection();

                    dataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                    dataAdapter.SelectCommand.CommandText = "QtySold";
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@date1", Convert.ToDateTime(fdate));
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@date2", Convert.ToDateTime(tdate));
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@brand", brandval);
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@category", ct);
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@subcat", subcat);
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@metal", metalval);
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@cacc", ccode);
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@do_sum", "0");
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@STYLEFROM", fromstyle);
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@STYLETO", tostyle);
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@Vendor", strVendor);
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@intchkattr1", intchkattr1.ToString());
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@intchkattr2", intchkattr2.ToString());
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@intchkattr3", intchkattr3.ToString());
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@intchkattr4", intchkattr4.ToString());
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@intchkattr5", intchkattr5.ToString());
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@intchkattr6", intchkattr6.ToString());
                    // Fill the datatable From adapter
                    dataAdapter.Fill(dataTable);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                int decimalPlaces = int.TryParse(_httpContextAccessor.HttpContext?.Session.GetString("DecimalsInPrices"), out int value) ? value : 0; ;
                // Get the datarow from the table
                //foreach (DataRow dr in dataTable.Select("INV_NO='     1'").CopyToDataTable().Rows)
                foreach (DataRow dr in dataTable.Rows)
                {
                    string accCode = "", InvNo = "", SaleDateInfo = "", Style1 = "", brandName1 = "", categoryName1 = "", subcategory1 = "";
                    string metalName1 = "", Description1 = "", StoreNo1 = "", CastCode1 = "", VndStyle1 = "";


                    if (!string.IsNullOrEmpty(dr["acc"].ToString()))
                        accCode = dr["acc"].ToString();
                    if (!string.IsNullOrEmpty(dr["inv_no"].ToString()))
                        InvNo = dr["inv_no"].ToString();
                    //if (!string.IsNullOrEmpty(dr["date"].ToString()))
                    //    SaleDateInfo = dr["date"].ToString();
                    if (Convert.ToDateTime(dr["date"].ToString()) != DateTime.MinValue)
                        SaleDateInfo = dr["date"].ToString();
                    if (!string.IsNullOrEmpty(dr["style"].ToString()))
                        Style1 = dr["style"].ToString();

                    if (!string.IsNullOrEmpty(dr["brand"].ToString()))
                        brandName1 = dr["brand"].ToString();
                    if (!string.IsNullOrEmpty(dr["category"].ToString()))
                        categoryName1 = dr["category"].ToString();
                    if (!string.IsNullOrEmpty(dr["subcat"].ToString()))
                        subcategory1 = dr["subcat"].ToString();
                    if (!string.IsNullOrEmpty(dr["metal"].ToString()))
                        metalName1 = dr["metal"].ToString();


                    if (!string.IsNullOrEmpty(dr["desc"].ToString()))
                        Description1 = dr["desc"].ToString();
                    if (!string.IsNullOrEmpty(dr["store_no"].ToString()))
                        StoreNo1 = dr["store_no"].ToString();
                    if (!string.IsNullOrEmpty(dr["Cast_Code"].ToString()))
                        CastCode1 = dr["Cast_Code"].ToString();
                    if (!string.IsNullOrEmpty(dr["vnd_style"].ToString()))
                        VndStyle1 = dr["vnd_style"].ToString();
                    //if (!string.IsNullOrEmpty(dr["class_gl"].ToString()))
                    //    ClassGl1 = dr["class_gl"].ToString();
                    //if (!string.IsNullOrEmpty(dr["qty"].ToString()))
                    //    Quantity1 = Math.Round(Decimal.Parse(dr["qty"].ToString()), decimalPlaces);
                    //if (!string.IsNullOrEmpty(dr["amount"].ToString()))
                    //    Amount1 = Math.Round(Decimal.Parse(dr["amount"].ToString()), decimalPlaces);
                    //if (!string.IsNullOrEmpty(dr["cost_qty"].ToString()))
                    //    CostQty1 = Math.Round(Decimal.Parse(dr["cost_qty"].ToString()), decimalPlaces);
                    //if (!string.IsNullOrEmpty(dr["cost"].ToString()))
                    //    Cost1 = Math.Round(Decimal.Parse(dr["cost"].ToString()), decimalPlaces);
                    //if (!string.IsNullOrEmpty(dr["profit"].ToString()))
                    //    Profit1 = Math.Round(Decimal.Parse(dr["profit"].ToString()), decimalPlaces);

                    if (!string.IsNullOrEmpty(dr["inv_no"].ToString()))
                        InvNo = dr["inv_no"].ToString();
                    if (Convert.ToDateTime(dr["date"].ToString()) != DateTime.MinValue)
                        SaleDateInfo = dr["date"].ToString();
                    //if (!string.IsNullOrEmpty(dr["cost"].ToString()))
                    //    Cost1 = Math.Round(Decimal.Parse(dr["cost"].ToString()), decimalPlaces);
                    //if (!string.IsNullOrEmpty(dr["profit"].ToString()))
                    //    Profit1 = Math.Round(Decimal.Parse(dr["profit"].ToString()), decimalPlaces);
                    //if (!string.IsNullOrEmpty(dr["desc"].ToString()))
                    //    Description1 = dr["desc"].ToString();
                    //if (!string.IsNullOrEmpty(dr["price"].ToString()))
                    //    Amount1 = Math.Round(Decimal.Parse(dr["price"].ToString()), decimalPlaces);
                    //if (!string.IsNullOrEmpty(dr["totamount"].ToString()))
                    //    RetailAmount1 = Math.Round(Decimal.Parse(dr["totamount"].ToString()), decimalPlaces);



                    lstSalesProfitDetails.Add(new SalesProfitModel()
                    {
                        //keep doing for all fields like this

                        ACC = accCode,//dr["acc"].ToString(),
                        InvoiceNo = InvNo, //dr["inv_no"].ToString(),
                        SaleDate = _helperCommonService.TryDateTimeParse(SaleDateInfo),
                        SaleDate1 = Convert.ToDateTime(SaleDateInfo).ToShortDateString(),
                        Style = Style1, //dr["style"].ToString(),
                        Quantity = ObjSalesProfit.Quantity, //Math.Round(Decimal.Parse(dr["qty"].ToString()), decimalPlaces),
                        //Amount = Amount1, //Math.Round(Decimal.Parse(dr["amount"].ToString()), decimalPlaces),
                        //CostQty = CostQty1, //Math.Round(Decimal.Parse(dr["cost_qty"].ToString()), decimalPlaces),
                        //Cost = Cost1, //Math.Round(Decimal.Parse(dr["cost"].ToString()), decimalPlaces),
                        //Profit = Profit1, //Math.Round(Decimal.Parse(dr["profit"].ToString()), decimalPlaces),
                        brandName = brandName1, //dr["brand"].ToString(),
                        categoryName = categoryName1, //dr["category"].ToString(),
                        subcategory = subcategory1, //dr["subcat"].ToString(),
                        metalName = metalName1, //dr["metal"].ToString(),
                        //Description = Description1, //dr["desc"].ToString(),
                        StoreNo = ObjSalesProfit.StoreNo, //dr["store_no"].ToString().Trim(),
                        CastCode = CastCode1, //dr["Cast_Code"].ToString().Trim(),
                        VndStyle = VndStyle1, //dr["vnd_style"].ToString().Trim(),
                        //ClassGl = ClassGl1, //dr["class_gl"].ToString().Trim(),
                        //RetailAmount = RetailAmount1, //Math.Round(Decimal.Parse(dr["Retail"].ToString()), decimalPlaces),
                        //GroupName = GroupName1 //dr["group"].ToString().Trim()
                        //In_STOCK = Math.Round(Decimal.Parse(dr["IN_STOCK"].ToString()), 0),                    
                        //Qty_Open = Qty_Open,
                        //OrderDate = _helperCommonService.TryDateTimeParse(dr["OrderDate"].ToString()),
                    });

                }
                return lstSalesProfitDetails;
            }
        }

        public List<SalesProfitModel> getTotalSaleProfitDetails(string ccode, string fdate, string tdate, string ct, string subcat, string metalval, string brandval, string fromstyle, string tostyle, string strVendor,
            bool iSLayawaySpecial, string stores)
        {
            DataTable dataTable = new DataTable();
            List<SalesProfitModel> lstSalesProfitDetails = new List<SalesProfitModel>();

            using (SqlDataAdapter dataAdapter = new SqlDataAdapter())
            {
                try
                {
                    // Create the command and set its properties
                    dataAdapter.SelectCommand = new SqlCommand();
                    dataAdapter.SelectCommand.Connection = _connectionProvider.GetConnection();

                    dataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                    dataAdapter.SelectCommand.CommandText = "det_prft";
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@date1", Convert.ToDateTime(fdate));
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@date2", Convert.ToDateTime(tdate));
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@brand", brandval);
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@category", ct);
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@subcat", subcat);
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@metal", metalval);
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@cacc", ccode);
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@do_sum", "0");
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@STYLEFROM", fromstyle);
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@STYLETO", tostyle);
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@Vendor", strVendor);
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@iSLaySpe", iSLayawaySpecial ? "1" : "0");
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@store", stores);
                    // Fill the datatable From adapter
                    dataAdapter.Fill(dataTable);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                int decimalPlaces = int.TryParse(_httpContextAccessor.HttpContext?.Session.GetString("DecimalsInPrices"), out int value) ? value : 0; ;
                // Get the datarow from the table
                //foreach (DataRow dr in dataTable.Select("INV_NO='     1'").CopyToDataTable().Rows)
                foreach (DataRow dr in dataTable.Rows)
                {
                    string accCode = "", InvNo = "", SaleDateInfo = "", Style1 = "", brandName1 = "", categoryName1 = "", subcategory1 = "", Group1 = "";
                    string metalName1 = "", Description1 = "", StoreNo1 = "", CastCode1 = "", VndStyle1 = "", ClassGl1 = "";
                    decimal Quantity1 = 0, Amount1 = 0, CostQty1 = 0, Cost1 = 0, Profit1 = 0, RetailAmount1 = 0;


                    if (!string.IsNullOrEmpty(dr["acc"].ToString()))
                        accCode = dr["acc"].ToString();
                    if (!string.IsNullOrEmpty(dr["inv_no"].ToString()))
                        InvNo = dr["inv_no"].ToString();
                    //if (!string.IsNullOrEmpty(dr["date"].ToString()))
                    //    SaleDateInfo = dr["date"].ToString();
                    if (Convert.ToDateTime(dr["date"].ToString()) != DateTime.MinValue)
                        SaleDateInfo = dr["date"].ToString();
                    if (!string.IsNullOrEmpty(dr["style"].ToString()))
                        Style1 = dr["style"].ToString();
                    if (!string.IsNullOrEmpty(dr["brand"].ToString()))
                        brandName1 = dr["brand"].ToString();
                    if (!string.IsNullOrEmpty(dr["category"].ToString()))
                        categoryName1 = dr["category"].ToString();
                    if (!string.IsNullOrEmpty(dr["subcat"].ToString()))
                        subcategory1 = dr["subcat"].ToString();
                    if (!string.IsNullOrEmpty(dr["metal"].ToString()))
                        metalName1 = dr["metal"].ToString();
                    if (!string.IsNullOrEmpty(dr["desc"].ToString()))
                        Description1 = dr["desc"].ToString();
                    if (!string.IsNullOrEmpty(dr["store_no"].ToString()))
                        StoreNo1 = dr["store_no"].ToString();
                    if (!string.IsNullOrEmpty(dr["Cast_Code"].ToString()))
                        CastCode1 = dr["Cast_Code"].ToString();
                    if (!string.IsNullOrEmpty(dr["vnd_style"].ToString()))
                        VndStyle1 = dr["vnd_style"].ToString();
                    if (!string.IsNullOrEmpty(dr["class_gl"].ToString()))
                        ClassGl1 = dr["class_gl"].ToString();
                    if (!string.IsNullOrEmpty(dr["qty"].ToString()))
                        Quantity1 = Math.Round(Decimal.Parse(dr["qty"].ToString()), decimalPlaces);
                    if (!string.IsNullOrEmpty(dr["amount"].ToString()))
                        Amount1 = Math.Round(Decimal.Parse(dr["amount"].ToString()), decimalPlaces);
                    if (!string.IsNullOrEmpty(dr["cost_qty"].ToString()))
                        CostQty1 = Math.Round(Decimal.Parse(dr["cost_qty"].ToString()), decimalPlaces);
                    if (!string.IsNullOrEmpty(dr["cost"].ToString()))
                        Cost1 = Math.Round(Decimal.Parse(dr["cost"].ToString()), decimalPlaces);
                    if (!string.IsNullOrEmpty(dr["profit"].ToString()))
                        Profit1 = Math.Round(Decimal.Parse(dr["profit"].ToString()), decimalPlaces);
                    if (!string.IsNullOrEmpty(dr["Retail"].ToString()))
                        RetailAmount1 = Math.Round(Decimal.Parse(dr["Retail"].ToString()), decimalPlaces);
                    if (!string.IsNullOrEmpty(dr["Cast_Code"].ToString()))
                        CastCode1 = dr["Cast_Code"].ToString();
                    if (!string.IsNullOrEmpty(dr["vnd_style"].ToString()))
                        VndStyle1 = dr["vnd_style"].ToString();
                    if (!string.IsNullOrEmpty(dr["group"].ToString()))
                        Group1 = dr["group"].ToString();

                    lstSalesProfitDetails.Add(new SalesProfitModel()
                    {
                        //keep doing for all fields like this
                        ACC = accCode,
                        InvoiceNo = InvNo,
                        SaleDate = _helperCommonService.TryDateTimeParse(SaleDateInfo),
                        SaleDate1 = Convert.ToDateTime(SaleDateInfo).ToShortDateString(),
                        Style = Style1,
                        Quantity = Convert.ToInt32(Quantity1),
                        Amount = Amount1,
                        CostQty = CostQty1,
                        Cost = Cost1,
                        Profit = Profit1,
                        brandName = brandName1,
                        categoryName = categoryName1,
                        subcategory = subcategory1,
                        metalName = metalName1,
                        Description = Description1,
                        StoreNo = StoreNo1,
                        CastCode = CastCode1,
                        VndStyle = VndStyle1,
                        ClassGl = ClassGl1,
                        RetailAmount = RetailAmount1,
                        GroupName = Group1
                    });

                }
                return lstSalesProfitDetails;
            }
        }




        public List<SalesProfitModel> getTotalSaleProfitByInvoiceDetails(string ccode, string fdate, string tdate, string ct, string subcat, string metalval, string brandval, string fromstyle, string tostyle, string strVendor,
            bool iSLayawaySpecial, string stores)
        {
            DataTable dataTable = new DataTable();
            List<SalesProfitModel> lstSalesProfitDetails = new List<SalesProfitModel>();

            using (SqlDataAdapter dataAdapter = new SqlDataAdapter())
            {
                try
                {
                    // Create the command and set its properties
                    dataAdapter.SelectCommand = new SqlCommand();
                    dataAdapter.SelectCommand.Connection = _connectionProvider.GetConnection();

                    dataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                    dataAdapter.SelectCommand.CommandText = "det_prft";
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@date1", Convert.ToDateTime(fdate));
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@date2", Convert.ToDateTime(tdate));
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@brand", brandval);
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@category", ct);
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@subcat", subcat);
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@metal", metalval);
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@cacc", ccode);
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@do_sum", "1");
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@STYLEFROM", fromstyle);
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@STYLETO", tostyle);
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@Vendor", strVendor);
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@iSLaySpe", iSLayawaySpecial ? "1" : "0");
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@store", stores);
                    // Fill the datatable From adapter
                    dataAdapter.Fill(dataTable);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                //int decimalPlaces = (int)HttpContext.Current.Session["DecimalsInPrices"];
                int decimalPlaces = 2;
                // Get the datarow from the table
                //foreach (DataRow dr in dataTable.Select("INV_NO='     1'").CopyToDataTable().Rows)
                foreach (DataRow dr in dataTable.Rows)
                {
                    string accCode = "", InvNo = "", SaleDateInfo = "", StoreNo1 = "";
                    decimal Amount1 = 0, Cost1 = 0, Profit1 = 0, RetailAmount1 = 0;
                    decimal bankFee1 = 0, netProfit1 = 0;
                    string Salesman1 = "", name1 = "";

                    if (!string.IsNullOrEmpty(dr["acc"].ToString()))
                        accCode = dr["acc"].ToString();
                    if (!string.IsNullOrEmpty(dr["inv_no"].ToString()))
                        InvNo = dr["inv_no"].ToString();
                    if (Convert.ToDateTime(dr["date"].ToString()) != DateTime.MinValue)
                        SaleDateInfo = dr["date"].ToString();
                    if (!string.IsNullOrEmpty(dr["amount"].ToString()))
                        Amount1 = Math.Round(Decimal.Parse(dr["amount"].ToString()), decimalPlaces);
                    if (!string.IsNullOrEmpty(dr["cost"].ToString()))
                        Cost1 = Math.Round(Decimal.Parse(dr["cost"].ToString()), decimalPlaces);
                    if (!string.IsNullOrEmpty(dr["profit"].ToString()))
                        Profit1 = Math.Round(Decimal.Parse(dr["profit"].ToString()), decimalPlaces);
                    if (!string.IsNullOrEmpty(dr["BankFee"].ToString()))
                        bankFee1 = Math.Round(Decimal.Parse(dr["BankFee"].ToString()), decimalPlaces);
                    if (!string.IsNullOrEmpty(dr["NetProfit"].ToString()))
                        netProfit1 = Math.Round(Decimal.Parse(dr["NetProfit"].ToString()), decimalPlaces);
                    if (!string.IsNullOrEmpty(dr["Salesman"].ToString()))
                        Salesman1 = dr["Salesman"].ToString();
                    if (!string.IsNullOrEmpty(dr["store_no"].ToString()))
                        StoreNo1 = dr["store_no"].ToString();
                    if (!string.IsNullOrEmpty(dr["Retail"].ToString()))
                        RetailAmount1 = Math.Round(Decimal.Parse(dr["Retail"].ToString()), decimalPlaces);
                    if (!string.IsNullOrEmpty(dr["Name"].ToString()))
                        name1 = dr["Name"].ToString();

                    lstSalesProfitDetails.Add(new SalesProfitModel()
                    {
                        //keep doing for all fields like this
                        ACC = accCode,
                        InvoiceNo = InvNo,
                        SaleDate = _helperCommonService.TryDateTimeParse(SaleDateInfo),
                        SaleDate1 = Convert.ToDateTime(SaleDateInfo).ToShortDateString(),
                        Amount = Amount1,
                        Cost = Cost1,
                        Profit = Profit1,
                        BankFee = bankFee1,
                        NetProfit = netProfit1,
                        Salesman = Salesman1,
                        StoreNo = StoreNo1,
                        RetailAmount = RetailAmount1,
                        Name = name1
                    });

                }
                return lstSalesProfitDetails;//.OrderBy(o => o.SaleDate).ToList();
            }
        }

        #endregion




        #region Profict By Customer
        public List<SalesProfitModel> GetProfitDetailsByCustomer(string ccode, string fdate, string tdate, string ct, string subcat, string metalval, string brandval, string fromstyle, string tostyle, string strVendor)
        {
            DataTable dataTable = new DataTable();
            List<SalesProfitModel> lstSalesProfitDetails = new List<SalesProfitModel>();

            using (SqlDataAdapter dataAdapter = new SqlDataAdapter())
            {
                try
                {
                    // Create the command and set its properties
                    dataAdapter.SelectCommand = new SqlCommand();
                    dataAdapter.SelectCommand.Connection = _connectionProvider.GetConnection();

                    dataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                    dataAdapter.SelectCommand.CommandText = "det_prft";
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@date1", Convert.ToDateTime(fdate));
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@date2", Convert.ToDateTime(tdate));
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@brand", brandval);
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@category", ct);
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@subcat", subcat);
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@metal", metalval);
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@cacc", ccode);
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@do_sum", "2");
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@STYLEFROM", fromstyle);
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@STYLETO", tostyle);
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@Vendor", strVendor);
                    // Fill the datatable From adapter
                    dataAdapter.Fill(dataTable);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                int decimalPlaces = int.TryParse(_httpContextAccessor.HttpContext?.Session.GetString("DecimalsInPrices"), out int value) ? value : 0; ;
                // Get the datarow from the table
                //foreach (DataRow dr in dataTable.Select("INV_NO='     1'").CopyToDataTable().Rows)
                foreach (DataRow dr in dataTable.Rows)
                {
                    string accCode = "";
                    decimal Amount1 = 0, Cost1 = 0, Profit1 = 0;

                    if (!string.IsNullOrEmpty(dr["acc"].ToString()))
                        accCode = dr["acc"].ToString();
                    if (!string.IsNullOrEmpty(dr["amount"].ToString()))
                        Amount1 = Math.Round(Decimal.Parse(dr["amount"].ToString()), decimalPlaces);
                    if (!string.IsNullOrEmpty(dr["cost"].ToString()))
                        Cost1 = Math.Round(Decimal.Parse(dr["cost"].ToString()), decimalPlaces);
                    if (!string.IsNullOrEmpty(dr["profit"].ToString()))
                        Profit1 = Math.Round(Decimal.Parse(dr["profit"].ToString()), decimalPlaces);

                    lstSalesProfitDetails.Add(new SalesProfitModel()
                    {
                        //keep doing for all fields like this
                        ACC = accCode,
                        Amount = Amount1,
                        Cost = Cost1,
                        Profit = Profit1
                    });
                }
                return lstSalesProfitDetails;
            }
        }
        #endregion

        #region SaleProfit By category and Metal

        public List<SalesProfitModel> getTotalSaleProfitByCategoryMetalDetails(string ccode, string fdate, string tdate, string ct, string subcat, string metalval, string brandval, string fromstyle, string tostyle, string strVendor,
            bool isSummaryByCategory, bool bypickdate = false, string StrStore = "", int DblCatormetl = 0, bool iSInvoiceCost = false)
        {
            DataTable dataTable = new DataTable();
            List<SalesProfitModel> lstSalesProfitDetails = new List<SalesProfitModel>();

            using (SqlDataAdapter dataAdapter = new SqlDataAdapter())
            {
                try
                {
                    // Create the command and set its properties
                    dataAdapter.SelectCommand = new SqlCommand();
                    dataAdapter.SelectCommand.Connection = _connectionProvider.GetConnection();

                    dataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                    dataAdapter.SelectCommand.CommandText = "det_prft";
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@date1", Convert.ToDateTime(fdate));
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@date2", Convert.ToDateTime(tdate));
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@brand", brandval);
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@category", ct);
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@subcat", subcat);
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@metal", metalval);
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@cacc", ccode);
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@do_sum", "3");
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@STYLEFROM", fromstyle);
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@STYLETO", tostyle);
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@Vendor", strVendor);
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@sumbycat", isSummaryByCategory ? "1" : "0");
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@ByWichDate", bypickdate ? "PICKUPDATE" : "DATE");
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@Store", StrStore);
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@ByCatMtlDblcl", Convert.ToString(DblCatormetl));
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@iSInvoiceCost", iSInvoiceCost ? "1" : "0");
                    // Fill the datatable From adapter
                    dataAdapter.Fill(dataTable);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                int decimalPlaces = 2; // (int)HttpContext.Current.Session["DecimalsInPrices"];
                // Get the datarow from the table
                //foreach (DataRow dr in dataTable.Select("INV_NO='     1'").CopyToDataTable().Rows)
                foreach (DataRow dr in dataTable.Rows)
                {
                    string categoryName1 = "", subcategory1 = "";
                    string metalName1 = "", Description1 = "", StoreNo1 = "", CastCode1 = "", VndStyle1 = "", ClassGl1 = "";
                    decimal Quantity1 = 0, Amount1 = 0, CostQty1 = 0, Cost1 = 0, Profit1 = 0, RetailAmount1 = 0;

                    if (!string.IsNullOrEmpty(dr["Category"].ToString()))
                        categoryName1 = dr["Category"].ToString();
                    if (!isSummaryByCategory)
                    {
                        if (!string.IsNullOrEmpty(dr["Metal"].ToString()))
                            metalName1 = dr["Metal"].ToString();
                    }
                    if (!string.IsNullOrEmpty(dr["Amount"].ToString()))
                        Amount1 = Math.Round(Decimal.Parse(dr["Amount"].ToString()), decimalPlaces);
                    if (!string.IsNullOrEmpty(dr["Cost"].ToString()))
                        Cost1 = Math.Round(Decimal.Parse(dr["Cost"].ToString()), decimalPlaces);
                    if (!string.IsNullOrEmpty(dr["Profit"].ToString()))
                        Profit1 = Math.Round(Decimal.Parse(dr["Profit"].ToString()), decimalPlaces);

                    lstSalesProfitDetails.Add(new SalesProfitModel()
                    {
                        categoryName = categoryName1,
                        metalName = metalName1,
                        Amount = Amount1,
                        Cost = Cost1,
                        Profit = Profit1
                    });

                }
                return lstSalesProfitDetails;
            }
        }

        #endregion

        #region Total Sales Per Vendor Details
        
        public List<SalesProfitModel> GetTotalSalesPerVendorDetails(string fdate, string tdate, bool iSInvoiceCost = false)
        {
            DataTable dataTable = new DataTable();
            List<SalesProfitModel> lstSalesPerVendorDetails = new List<SalesProfitModel>();

            using (SqlDataAdapter dataAdapter = new SqlDataAdapter())
            {
                try
                {
                    // Create the command and set its properties
                    dataAdapter.SelectCommand = new SqlCommand();
                    dataAdapter.SelectCommand.Connection = _connectionProvider.GetConnection();

                    dataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                    dataAdapter.SelectCommand.CommandText = "TotalSalesPerVendor";
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@Fdate", Convert.ToDateTime(fdate));
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@Tdate", Convert.ToDateTime(tdate));
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@iSInvoiceCost", iSInvoiceCost ? "1" : "0");

                    // Fill the datatable From adapter
                    dataAdapter.Fill(dataTable);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                int decimalPlaces = 2; // (int)HttpContext.Current.Session["DecimalsInPrices"];
                // Get the datarow from the table
                foreach (DataRow dr in dataTable.Rows)
                {
                    string Vendor1 = "", vendorname1 = "";
                    decimal Sales1 = 0, Margin1 = 0, Cost1 = 0;

                    if (!string.IsNullOrEmpty(dr["VENDOR"].ToString()))
                        Vendor1 = dr["VENDOR"].ToString();
                    if (!string.IsNullOrEmpty(dr["SALES"].ToString()))
                        Sales1 = Math.Round(Decimal.Parse(dr["SALES"].ToString()), decimalPlaces);
                    if (!string.IsNullOrEmpty(dr["COST"].ToString()))
                        Cost1 = Math.Round(Decimal.Parse(dr["COST"].ToString()), decimalPlaces);
                    if (!string.IsNullOrEmpty(dr["MARGIN"].ToString()))
                        Margin1 = Math.Round(Decimal.Parse(dr["MARGIN"].ToString()), decimalPlaces);
                    if (!string.IsNullOrEmpty(dr["VENDOR NAME"].ToString()))
                        vendorname1 = dr["VENDOR NAME"].ToString();

                    lstSalesPerVendorDetails.Add(new SalesProfitModel()
                    {
                        Vendor = Vendor1,
                        Sales = Sales1,
                        Cost = Cost1,
                        Margin = Margin1,
                        vendorName = vendorname1,
                    });

                }
                return lstSalesPerVendorDetails;
            }
        }
        #endregion

        #region Vendor Styles Analysis Details
        public List<SalesProfitModel> GetVendorStylesAnalysisDetails(string Vendor, string fdate, string tdate, bool IsPurchasePrice = false, bool IsPrintImages = false)
        {
            DataTable dataTable = new DataTable();
            List<SalesProfitModel> lstVendorStyleAnalysisDetails = new List<SalesProfitModel>();

            using (SqlDataAdapter dataAdapter = new SqlDataAdapter())
            {
                try
                {
                    // Create the command and set its properties
                    dataAdapter.SelectCommand = new SqlCommand();
                    dataAdapter.SelectCommand.Connection = _connectionProvider.GetConnection();

                    dataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                    dataAdapter.SelectCommand.CommandText = "[DBO].[VENDORANALYSIS]";
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@vendorcode", Vendor);
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@datefrom", fdate); // Convert.ToDateTime(fdate));
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@dateto", tdate); // Convert.ToDateTime(tdate));
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@ispurchaseprice", IsPurchasePrice);
                    // Fill the datatable From adapter
                    dataAdapter.Fill(dataTable);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                int decimalPlaces = 2; // (int)HttpContext.Current.Session["DecimalsInPrices"];
                // Get the datarow from the table
                foreach (DataRow dr in dataTable.Rows)
                {
                    string Style1 = "", VENDOR_STYLE1 = "";
                    decimal Cost1 = 0, TOTAL_COST1 = 0, QTY_RECEIVED1 = 0, QTY_SOLD1 = 0, INVOICE_AMOUNT1 = 0, INVOICE_PROFIT1 = 0;
                    decimal IN_STOCK1 = 0, INSTOCK_VALUE1 = 0;
                    if (!string.IsNullOrEmpty(dr["STYLE"].ToString()))
                        Style1 = dr["STYLE"].ToString();
                    if (!string.IsNullOrEmpty(dr["VENDOR_STYLE"].ToString()))
                        VENDOR_STYLE1 = dr["VENDOR_STYLE"].ToString();
                    if (!string.IsNullOrEmpty(dr["COST"].ToString()))
                        Cost1 = Math.Round(Decimal.Parse(dr["COST"].ToString()), decimalPlaces);
                    if (!string.IsNullOrEmpty(dr["QTY_RECEIVED"].ToString()))
                        QTY_RECEIVED1 = Math.Round(Decimal.Parse(dr["QTY_RECEIVED"].ToString()), decimalPlaces);
                    if (!string.IsNullOrEmpty(dr["TOTAL_COST"].ToString()))
                        TOTAL_COST1 = Math.Round(Decimal.Parse(dr["TOTAL_COST"].ToString()), decimalPlaces);
                    if (!string.IsNullOrEmpty(dr["QTY_SOLD"].ToString()))
                        QTY_SOLD1 = Math.Round(Decimal.Parse(dr["QTY_SOLD"].ToString()), decimalPlaces);
                    if (!string.IsNullOrEmpty(dr["INVOICE_AMOUNT"].ToString()))
                        INVOICE_AMOUNT1 = Math.Round(Decimal.Parse(dr["INVOICE_AMOUNT"].ToString()), decimalPlaces);
                    if (!string.IsNullOrEmpty(dr["INVOICE_PROFIT"].ToString()))
                        INVOICE_PROFIT1 = Math.Round(Decimal.Parse(dr["INVOICE_PROFIT"].ToString()), decimalPlaces);
                    if (!string.IsNullOrEmpty(dr["IN_STOCK"].ToString()))
                        IN_STOCK1 = Math.Round(Decimal.Parse(dr["IN_STOCK"].ToString()), decimalPlaces);
                    if (!string.IsNullOrEmpty(dr["IN_STOCK_VALUE"].ToString()))
                        INSTOCK_VALUE1 = Math.Round(Decimal.Parse(dr["IN_STOCK_VALUE"].ToString()), decimalPlaces);

                    lstVendorStyleAnalysisDetails.Add(new SalesProfitModel()
                    {
                        Style = Style1,
                        VndStyle = VENDOR_STYLE1,
                        Cost = Cost1,
                        Qty_Received = QTY_RECEIVED1,
                        Total_Cost = TOTAL_COST1,
                        Qty_Sold = QTY_SOLD1,
                        Inv_Amount = INVOICE_AMOUNT1,
                        Inv_Profit = INVOICE_PROFIT1,
                        InStock = IN_STOCK1,
                        InStockValue = INSTOCK_VALUE1
                    });

                }
                return lstVendorStyleAnalysisDetails;
            }
        }
        #endregion

        #region SaleProfit By category and Price

        public List<SalesProfitModel> getTotalSaleProfitByCategoryPricesDetails(string ccode, string fdate, string tdate, string ct, string subcat, string metalval, string brandval, string fromstyle, string tostyle, string strVendor,
          decimal prange1, decimal prange2, decimal prange3, decimal prange4, decimal prange5, decimal prange6, decimal prange7, decimal prange8, string scode, bool isSummaryByCategory = false, bool bypickdate = false)
        {
            DataTable dataTable = new DataTable();
            List<SalesProfitModel> lstSalesProfitDetails = new List<SalesProfitModel>();

            using (SqlDataAdapter dataAdapter = new SqlDataAdapter())
            {
                try
                {
                    // Create the command and set its properties
                    dataAdapter.SelectCommand = new SqlCommand();
                    dataAdapter.SelectCommand.Connection = _connectionProvider.GetConnection();

                    dataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                    dataAdapter.SelectCommand.CommandText = "det_prft_by_price";
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@date1", Convert.ToDateTime(fdate));
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@date2", Convert.ToDateTime(tdate));
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@brand", brandval);
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@category", ct);
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@subcat", subcat);
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@metal", metalval);
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@cacc", ccode);
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@do_sum", 3);
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@STYLEFROM", fromstyle);
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@STYLETO", tostyle);
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@Vendor", strVendor);

                    dataAdapter.SelectCommand.Parameters.AddWithValue("@CSColor", "");
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@CSClarity", "");
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@CSShape", "");
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@CenterType", "");
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@CenterSize", "");

                    dataAdapter.SelectCommand.Parameters.AddWithValue("@prange1", prange1);
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@prange2", prange2);
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@prange3", prange3);
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@prange4", prange4);
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@prange5", prange5);
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@prange6", prange6);
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@prange7", prange7);
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@prange8", prange8);
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@scode", scode);
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@summaryCategory", isSummaryByCategory ? "1" : "0");
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@bypickdate", bypickdate);
                    // Fill the datatable From adapter
                    dataAdapter.Fill(dataTable);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                int decimalPlaces = 2;// (int)HttpContext.Current.Session["DecimalsInPrices"];
                // Get the datarow from the table
                //foreach (DataRow dr in dataTable.Select("INV_NO='     1'").CopyToDataTable().Rows)
                foreach (DataRow dr in dataTable.Rows)
                {
                    //string accCode = "", InvNo = "", SaleDateInfo = "", Style1 = "", brandName1 = "", categoryName1 = "", subcategory1 = "", Group1 = "";
                    //string metalName1 = "", Description1 = "", StoreNo1 = "", CastCode1 = "", VndStyle1 = "", ClassGl1 = "";
                    string categoryName1 = "", Price_Range1 = "";
                    decimal Amount1 = 0, Cost1 = 0, Profit1 = 0, OtherCharges1 = 0;
                    if (!isSummaryByCategory)
                    {
                        if (!string.IsNullOrEmpty(dr["category"].ToString()))
                            categoryName1 = dr["category"].ToString();
                    }
                    if (!string.IsNullOrEmpty(dr["pricerange"].ToString()))
                        Price_Range1 = dr["pricerange"].ToString();
                    if (!string.IsNullOrEmpty(dr["amount"].ToString()))
                        Amount1 = Math.Round(Decimal.Parse(dr["amount"].ToString()), decimalPlaces);
                    if (!string.IsNullOrEmpty(dr["cost"].ToString()))
                        Cost1 = Math.Round(Decimal.Parse(dr["cost"].ToString()), decimalPlaces);
                    if (!string.IsNullOrEmpty(dr["profit"].ToString()))
                        Profit1 = Math.Round(Decimal.Parse(dr["profit"].ToString()), decimalPlaces);
                    if (!string.IsNullOrEmpty(dr["OtherCharges"].ToString()))
                        OtherCharges1 = Math.Round(Decimal.Parse(dr["OtherCharges"].ToString()), decimalPlaces);

                    lstSalesProfitDetails.Add(new SalesProfitModel()
                    {
                        categoryName = categoryName1,
                        PriceRange = Price_Range1,
                        Amount = Amount1,
                        Cost = Cost1,
                        Profit = Profit1,
                        OtherCharges = OtherCharges1
                    });

                }
                return lstSalesProfitDetails;
            }
        }

        #endregion

        #region Sales Profit By Salesrep        

        public List<SalesProfitModel> getTotalSalesProfitBySalesrep(string storeName = "", string salesMan = "", bool isLayaway = false, string byWhichDate = "", string fdate = "", string tdate = "")
        {
            DataTable dataTable = new DataTable();
            List<SalesProfitModel> lstSalesProfitDetails = new List<SalesProfitModel>();

            using (SqlDataAdapter dataAdapter = new SqlDataAdapter())
            {
                try
                {
                    // Create the command and set its properties
                    dataAdapter.SelectCommand = new SqlCommand();
                    dataAdapter.SelectCommand.Connection = _connectionProvider.GetConnection();

                    dataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                    dataAdapter.SelectCommand.CommandText = "GETSALESPROFITBYSALESREP";
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@STORENAME", storeName);
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@SALESMAN", salesMan);
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@ISLAYAWAY", isLayaway);
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@BYWHICHDATE", byWhichDate);
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@FDATE", fdate);
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@TDATE", tdate);
                    // Fill the datatable From adapter
                    dataAdapter.Fill(dataTable);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                int decimalPlaces = 2;// (int)HttpContext.Current.Session["DecimalsInPrices"];
                // Get the datarow from the table
                //foreach (DataRow dr in dataTable.Select("INV_NO='     1'").CopyToDataTable().Rows)
                foreach (DataRow dr in dataTable.Rows)
                {
                    string accCode = "", InvNo = "", SaleDateInfo = "", Style1 = "", brandName1 = "", categoryName1 = "", subcategory1 = "", Group1 = "";
                    string metalName1 = "", Description1 = "", StoreNo1 = "", CastCode1 = "", VndStyle1 = "", ClassGl1 = "";
                    string Name1 = "", Price_Range1 = "";
                    decimal Quantity1 = 0, Price1 = 0, GR_TOTAL1 = 0, cost_qty1 = 0, Amount1 = 0, Cost1 = 0, Profit1 = 0, OtherCharges1 = 0;
                    decimal total1 = 0, totalcost1 = 0, SREPAMTSHARE1 = 0, SREPCOSTSHARE1 = 0;
                    decimal comish1a = 0, comish2a = 0, comish3a = 0, comish4a = 0;
                    string SREP1 = "";
                    decimal salesmanAmount1 = 0;
                    DateTime date1 = new DateTime(); DateTime lastpayDate = new DateTime();
                    if (!isLayaway)
                    {

                    }
                    if (!string.IsNullOrEmpty(dr["INV_NO"].ToString()))
                        InvNo = dr["INV_NO"].ToString();
                    if (!string.IsNullOrEmpty(dr["date"].ToString()))
                        date1 = Convert.ToDateTime(dr["date"]);
                    if (!string.IsNullOrEmpty(dr["LastPayDate"].ToString()))
                        lastpayDate = Convert.ToDateTime(dr["LastPayDate"]);
                    if (!string.IsNullOrEmpty(dr["acc"].ToString()))
                        accCode = dr["acc"].ToString();
                    if (!string.IsNullOrEmpty(dr["NAME"].ToString()))
                        Name1 = dr["NAME"].ToString();
                    if (!string.IsNullOrEmpty(dr["store_no"].ToString()))
                        StoreNo1 = dr["store_no"].ToString();
                    if (!string.IsNullOrEmpty(dr["style"].ToString()))
                        Style1 = dr["style"].ToString();
                    if (!string.IsNullOrEmpty(dr["qty"].ToString()))
                        Quantity1 = Math.Round(Decimal.Parse(dr["qty"].ToString()), 0);
                    if (!string.IsNullOrEmpty(dr["price"].ToString()))
                        Price1 = Math.Round(Decimal.Parse(dr["price"].ToString()), decimalPlaces);
                    if (!string.IsNullOrEmpty(dr["TOTAL"].ToString()))
                        total1 = Math.Round(Decimal.Parse(dr["TOTAL"].ToString()), decimalPlaces);
                    if (!string.IsNullOrEmpty(dr["GR_TOTAL"].ToString()))
                        GR_TOTAL1 = Math.Round(Decimal.Parse(dr["GR_TOTAL"].ToString()), decimalPlaces);
                    if (!string.IsNullOrEmpty(dr["COST"].ToString()))
                        Cost1 = Math.Round(Decimal.Parse(dr["COST"].ToString()), decimalPlaces);
                    if (!string.IsNullOrEmpty(dr["cost_qty"].ToString()))
                        cost_qty1 = Math.Round(Decimal.Parse(dr["cost_qty"].ToString()), decimalPlaces);
                    if (!string.IsNullOrEmpty(dr["tot_cost"].ToString()))
                        totalcost1 = Math.Round(Decimal.Parse(dr["tot_cost"].ToString()), decimalPlaces);
                    if (!string.IsNullOrEmpty(dr["profit"].ToString()))
                        Profit1 = Math.Round(Decimal.Parse(dr["profit"].ToString()), decimalPlaces);

                    if (dr["salesman2"].ToString() == salesMan)
                    {
                        dr["sales1shareCost"] = dr["sales2shareCost"];
                        dr["SalesmanShare1"] = dr["SalesmanShare2"];
                    }
                    if (dr["salesman3"].ToString() == salesMan)
                    {
                        dr["sales1shareCost"] = dr["sales3shareCost"];
                        dr["SalesmanShare1"] = dr["SalesmanShare3"];
                    }
                    //  if (dr["SREP"].ToString() == Srep)
                    //  {
                    //    dr["sales1shareCost"] = dr["sales3shareCost"];
                    //    dr["SalesmanShare1"] = dr["SalesmanShare3"];
                    // }

                    if (!string.IsNullOrEmpty(dr["salesman1"].ToString()))
                        SREP1 = dr["salesman1"].ToString();
                    if (!string.IsNullOrEmpty(dr["SalesmanShare1"].ToString()))
                        salesmanAmount1 = Math.Round(Decimal.Parse(dr["SalesmanShare1"].ToString()), decimalPlaces);

                    if (!string.IsNullOrEmpty(dr["SalesmanShare1"].ToString()))
                        SREPAMTSHARE1 = Math.Round(Decimal.Parse(dr["SalesmanShare1"].ToString()), decimalPlaces);
                    if (!string.IsNullOrEmpty(dr["sales1shareCost"].ToString()))
                        SREPCOSTSHARE1 = Math.Round(Decimal.Parse(dr["sales1shareCost"].ToString()), decimalPlaces);
                    if (!string.IsNullOrEmpty(dr["comish1"].ToString()))
                        comish1a = Math.Round(Decimal.Parse(dr["comish1"].ToString()), decimalPlaces);
                    if (!string.IsNullOrEmpty(dr["comish2"].ToString()))
                        comish2a = Math.Round(Decimal.Parse(dr["comish2"].ToString()), decimalPlaces);
                    if (!string.IsNullOrEmpty(dr["comish3"].ToString()))
                        comish3a = Math.Round(Decimal.Parse(dr["comish3"].ToString()), decimalPlaces);
                    if (!string.IsNullOrEmpty(dr["comish4"].ToString()))
                        comish4a = Math.Round(Decimal.Parse(dr["comish4"].ToString()), decimalPlaces);

                    lstSalesProfitDetails.Add(new SalesProfitModel()
                    {
                        InvoiceNo = InvNo,
                        SaleDate1 = date1.ToShortDateString(),
                        LastpayDate1 = lastpayDate.ToShortDateString(),
                        ACC = accCode,
                        Name = Name1,
                        StoreNo = StoreNo1,
                        Style = Style1,
                        Quantity = Convert.ToInt32(Quantity1),
                        Price = Price1,
                        Total = total1,
                        GrandTotal = GR_TOTAL1,
                        Cost = Cost1,
                        CostQty = cost_qty1,
                        Total_Cost = totalcost1,
                        Profit = Profit1,
                        SREPAMTSHARE = SREPAMTSHARE1,
                        //SREPCOSTSHARE = SREPCOSTSHARE1,
                        comish1 = comish1a,
                        comish2 = comish2a,
                        comish3 = comish3a,
                        comish4 = comish4a,

                        SREPNew = SREP1,
                        SREPCOSTSHARE = salesmanAmount1
                    });


                }
                return lstSalesProfitDetails;
            }
        }

        #endregion

        #region Sales COG/ Profit By Salesrep        

        
        public List<SalesProfitModel> GetSalesCOGProfitBySalesman(string storeName = "", string salesMan = "", bool isLayaway = false, string byWhichDate = "", string fdate = "", string tdate = "", string brand = "", string category = "", bool seperatebycategory = false, bool iSIncludeRepwarranty = false, bool isBankFee = false)
        {
            DataTable dataTable = new DataTable();
            List<SalesProfitModel> lstSalesProfitDetails = new List<SalesProfitModel>();

            using (SqlDataAdapter dataAdapter = new SqlDataAdapter())
            {
                try
                {
                    // Create the command and set its properties
                    dataAdapter.SelectCommand = new SqlCommand();
                    dataAdapter.SelectCommand.Connection = _connectionProvider.GetConnection();

                    dataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                    dataAdapter.SelectCommand.CommandText = "GetSalesCOGProfitBySalesman";
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@STORENAME", storeName);
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@SALESMAN", salesMan);
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@ISLAYAWAY", isLayaway);
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@BYWHICHDATE", byWhichDate);
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@FDATE", fdate);
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@TDATE", tdate);
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@BRAND", brand);
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@CATEGORY", category);
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@SEPARATEBYCAT", seperatebycategory);
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@iSInclude", iSIncludeRepwarranty);
                    // Fill the datatable From adapter
                    dataAdapter.Fill(dataTable);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                int decimalPlaces = 2;// (int)HttpContext.Current.Session["DecimalsInPrices"];


                if (isBankFee)
                    dataTable = _helperCommonService.GetCogValues(dataTable);

                // Get the datarow from the table
                //foreach (DataRow dr in dataTable.Select("INV_NO='     1'").CopyToDataTable().Rows)
                foreach (DataRow dr in dataTable.Rows)
                {
                    string accCode = "", InvNo = "", SaleDateInfo = "", Style1 = "", brandName1 = "", categoryName1 = "", subcategory1 = "", Group1 = "", SALESMAN3a = "", SALESMAN4a = "";
                    string metalName1 = "", Description1 = "", StoreNo1 = "", CastCode1 = "", VndStyle1 = "", ClassGl1 = "", Salesman1 = "", Layaway1 = "", Special1 = "";
                    string Name1 = "", Price_Range1 = "";
                    decimal Quantity1 = 0, Price1 = 0, GR_TOTAL1 = 0, cost_qty1 = 0, Amount1 = 0, Cost1 = 0, Profit1 = 0, OtherCharges1 = 0;
                    decimal total1 = 0, totalcost1 = 0, SREPAMTSHARE1 = 0, SREPCOSTSHARE1 = 0, COGs1 = 0, subtotal1 = 0, SalesmanRates3a = 0, SalesmanRates4a = 0;

                    DateTime date1 = new DateTime(); DateTime lastpayDate = new DateTime();

                    if (!isLayaway)
                    {

                    }
                    if (!string.IsNullOrEmpty(dr["INV_NO"].ToString()))
                        InvNo = dr["INV_NO"].ToString();
                    if (!string.IsNullOrEmpty(dr["DATE"].ToString()))
                        date1 = Convert.ToDateTime(dr["DATE"]);
                    if (!string.IsNullOrEmpty(dr["LastPayDate"].ToString()))
                        lastpayDate = Convert.ToDateTime(dr["LastPayDate"]);
                    // if (!string.IsNullOrEmpty(dr["acc"].ToString()))
                    //    accCode = dr["acc"].ToString();
                    if (!string.IsNullOrEmpty(dr["Name1"].ToString()))
                        Name1 = dr["Name1"].ToString();
                    if (!string.IsNullOrEmpty(dr["store_no"].ToString()))
                        StoreNo1 = dr["store_no"].ToString();
                    if (!string.IsNullOrEmpty(dr["Salesman"].ToString()))
                        Salesman1 = dr["Salesman"].ToString();
                    if (!string.IsNullOrEmpty(dr["LAYAWAY"].ToString()))
                        Layaway1 = dr["LAYAWAY"].ToString();
                    if (!string.IsNullOrEmpty(dr["SPECIAL"].ToString()))
                        Special1 = dr["SPECIAL"].ToString();
                    if (!string.IsNullOrEmpty(dr["BRAND"].ToString()))
                        brandName1 = dr["BRAND"].ToString();
                    if (!string.IsNullOrEmpty(dr["CATEGORY"].ToString()))
                        categoryName1 = dr["CATEGORY"].ToString();
                    if (!string.IsNullOrEmpty(dr["SubTotal"].ToString()))
                        subtotal1 = Math.Round(Decimal.Parse(dr["SubTotal"].ToString()), decimalPlaces);
                    if (!string.IsNullOrEmpty(dr["COGs"].ToString()))
                        COGs1 = Math.Round(Decimal.Parse(dr["COGs"].ToString()), decimalPlaces);
                    if (!string.IsNullOrEmpty(dr["GrossProfit"].ToString()))
                        Profit1 = Math.Round(Decimal.Parse(dr["GrossProfit"].ToString()), decimalPlaces);
                    if (!string.IsNullOrEmpty(dr["SALESMAN3"].ToString()))
                    {
                        string testval = dr["SALESMAN3"].ToString();
                        SALESMAN3a = dr["SALESMAN3"].ToString();// Math.Round(Decimal.Parse(dr["SALESMAN3"].ToString()), decimalPlaces);
                    }
                    if (!string.IsNullOrEmpty(dr["SALESMAN4"].ToString()))
                    {
                        string testval = dr["SALESMAN4"].ToString();
                        SALESMAN4a = dr["SALESMAN4"].ToString(); // Math.Round(Decimal.Parse(dr["SALESMAN4"].ToString()), decimalPlaces);
                    }
                    if (!string.IsNullOrEmpty(dr["SalesmanRates3"].ToString()))
                        SalesmanRates3a = Math.Round(Decimal.Parse(dr["SalesmanRates3"].ToString()), decimalPlaces);
                    if (!string.IsNullOrEmpty(dr["SalesmanRates4"].ToString()))
                        SalesmanRates4a = Math.Round(Decimal.Parse(dr["SalesmanRates4"].ToString()), decimalPlaces);

                    lstSalesProfitDetails.Add(new SalesProfitModel()
                    {
                        InvoiceNo = InvNo,
                        SaleDate1 = date1.ToShortDateString(),
                        LastpayDate1 = lastpayDate.ToShortDateString(),
                        //ACC = accCode,
                        Name = Name1,
                        StoreNo = StoreNo1,
                        Salesman = Salesman1,
                        layaway = Layaway1,
                        Special = Special1,
                        brandName = brandName1,
                        categoryName = categoryName1,
                        SubTotal = subtotal1,
                        COGs = COGs1,
                        GrossProfit = Profit1,
                        salesman3 = SALESMAN3a,
                        salesman4 = SALESMAN4a,
                        salesmanRate3 = SalesmanRates3a,
                        salesmanRate4 = SalesmanRates4a
                    });

                }
                return lstSalesProfitDetails;
            }
        }

        #endregion


        #region Sales COG/ Profit Report
        
        public DataTable GetSalesCOGProfitReportDetails(string ccode, string fdate, string tdate, string ct, string subct, string metalval, string brandval, string fromstyle, string tostyle, string strVendor, string store, bool withGP = false, bool separateSM = false, bool isSalesCOG = false, string byWhichDate = "", bool isLayaway = false, string salesMan = "", bool monthproft = false, bool iSLaySpe = false, bool isinclbankfee = false, bool isExport = false)
        {
            DataTable dataTable = new DataTable();
            List<SalesProfitModel> lstSalesProfitDetails = new List<SalesProfitModel>();

            using (SqlDataAdapter dataAdapter = new SqlDataAdapter())
            {
                try
                {
                    if (isExport)
                        dataTable = _helperCommonService.GetStoreProc("det_SalesCGOProfitSimpleExcel", "@date1", getdate(fdate), "@date2", getdate(tdate), "@brand", brandval, "@category", ct, "@subcat", subct, "@metal", metalval, "@cacc", ccode, "@STYLEFROM", fromstyle, "@STYLETO", tostyle, "@Vendor", strVendor, "@store", store, "@ByWichDate", byWhichDate, "@iSLayaway", isLayaway ? "1" : "0", "@SALESMAN", salesMan, "@isinclbankfee", isinclbankfee ? "1" : "0");
                    else
                    {
                        if (!isSalesCOG)
                            dataTable = _helperCommonService.GetStoreProc(withGP ? "det_profit" : "det_prft", "@date1", getdate(fdate), "@date2", getdate(tdate), "@brand", brandval, "@category", ct, "@subcat", subct, "@metal", metalval, "@cacc", ccode, "@do_sum", monthproft ? "7" : !separateSM ? "5" : "6", "@STYLEFROM", fromstyle, "@STYLETO", tostyle, "@Vendor", strVendor, "@store", store, "@iSLaySpe", iSLaySpe ? "1" : "0", "@ByWichDate", byWhichDate);
                        else
                            dataTable = _helperCommonService.GetStoreProc(withGP ? "det_COGProfit_GP" : "det_COGProfit", "@date1", getdate(fdate), "@date2", getdate(tdate), "@brand", brandval, "@category", ct, "@subcat", subct, "@metal", metalval, "@cacc", ccode, "@do_sum", !separateSM ? "5" : "6", "@STYLEFROM", fromstyle, "@STYLETO", tostyle, "@Vendor", strVendor, "@store", store, "@ByWichDate", byWhichDate, "@iSLayaway", isLayaway ? "1" : "0", "@SALESMAN", salesMan, "@isinclbankfee", isinclbankfee ? "1" : "0");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                return dataTable;
            }
        }

        private string getdate(string date1)
        {
            string[] Date1 = date1.Split(' ');
            return Date1[0];
        }
        #endregion


        #region Sales - List Of Occasions 

        public DataTable GetListOfOccasions(DateTime fromdate, DateTime todate)
        {
            DataTable dataTable = new DataTable();

            using (SqlDataAdapter SqlDataAdapter = new SqlDataAdapter())
            {
                // Create the command and set its properties
                SqlDataAdapter.SelectCommand = new SqlCommand();
                SqlDataAdapter.SelectCommand.Connection = _connectionProvider.GetConnection();
                SqlDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;

                // Assign the SQL to the command object

                SqlDataAdapter.SelectCommand.CommandText = @"GetListOfOccasionsByDate";

                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@FROMDATE", fromdate);
                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@TODATE", todate);
                // Fill the table from adapter
                SqlDataAdapter.Fill(dataTable);
            }

            return dataTable;
        }

        #endregion


        #region Sales - Potential Customer Report 

        public DataTable GetAllCustomers()
        {
            return _helperCommonService.GetSqlData(@"SELECT ACC,NAME,TEL,EMAIL,ADDR1,STATE1,ZIP1,CITY1,COUNTRY,JBT,STORES,SOURCE,SALESMAN,BUYER,WWW,NOTE1,NOTE2,TEL,FAX,EST_DATE FROM MAILING");
        }

        #endregion

        #region Sales / Customer Follow Up

        public DataTable ShowCustomerFollowup(string acc, DateTime fromdate, DateTime todate, int complete)
        {
            DataTable dataTable = new DataTable();

            using (SqlDataAdapter SqlDataAdapter = new SqlDataAdapter())
            {
                // Create the command and set its properties
                SqlDataAdapter.SelectCommand = new SqlCommand();
                SqlDataAdapter.SelectCommand.Connection = _connectionProvider.GetConnection();
                SqlDataAdapter.SelectCommand.CommandType = CommandType.Text;

                String strWhereCondition = string.Empty;
                if (acc == "ALL")
                {
                    // Assign the SQL to the command object
                    if (complete == 1)
                        strWhereCondition = string.Empty;
                    else
                        strWhereCondition = " AND CNOTE.completed = @complete ";
                }
                else
                {
                    // Assign the SQL to the command object
                    if (complete == 1)
                        strWhereCondition = " AND CNOTE.who= @acc";
                    else
                        strWhereCondition = " AND CNOTE.completed = @complete  and CNOTE.who = @acc";
                }

                //SqlDataAdapter.SelectCommand.CommandText = "  SELECT CNOTE.ID, CNOTE.ACC as Customercode ,CNOTE.WHO as [User] ,CNOTE.DTIME as Date,CNOTE.[TYPE] as Type ,CNOTE.NOTE as Note ,CNOTE.followup as FollowUp,CNOTE.time as Time, ";
                SqlDataAdapter.SelectCommand.CommandText = "  SELECT CNOTE.ID, CNOTE.ACC as Customercode ,CNOTE.WHO as [User] ,CNOTE.DTIME as Date,CNOTE.[TYPE] as Type ,CNOTE.NOTE as Note ,CNOTE.followup as FollowUp,CNOTE.time as Time, ";
                SqlDataAdapter.SelectCommand.CommandText += " CNOTE.reminder as Reminder,CNOTE.completed as Completed,CUST.Name, stuff(stuff(CUST.Tel, 4, 0, '-'), 8, 0, '-') AS Tel";
                SqlDataAdapter.SelectCommand.CommandText += " FROM CUSTNOTE CNOTE ";
                SqlDataAdapter.SelectCommand.CommandText += " LEFT JOIN CUSTOMER CUST ON CNOTE.ACC = CUST.ACC ";
                SqlDataAdapter.SelectCommand.CommandText += $" WHERE 1=1 AND CNOTE.followup <> '' and CNOTE.followup is not null and convert(varchar(8),CNOTE.followup,112) >= @fromdate and convert(varchar(8),CNOTE.followup,112) <= @todate {strWhereCondition} order by CNOTE.DTIME";

                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@acc", acc.Trim());
                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@fromdate", fromdate.ToString("yyyyMMdd"));
                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@todate", todate.ToString("yyyyMMdd"));
                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@complete", complete);

                // Fill the table from adapter
                SqlDataAdapter.Fill(dataTable);
            }
            return dataTable;
        }
        public DataRow CheckValidCustomerCode(string acc, bool is_glenn, bool iSWrist = false)
        {
            if (iSWrist)
            {
                DataRow rw = _helperCommonService.GetSqlRow("select [NAME2] NAME From Customer Where trim(acc)=trim(@acc) or old_customer=trim(@acc)", "@acc", acc);
                if (rw == null || String.IsNullOrWhiteSpace(Convert.ToString(rw["NAME"])))
                    return _helperCommonService.GetSqlRow("select *  From Customer Where trim(acc)=trim(@acc) or old_customer=trim(@acc)", "@acc", acc);
                else
                    return _helperCommonService.GetSqlRow("select [NAME2] NAME, [ADDR2] ADDR1,[ADDR22] ADDR12,[CITY2] CITY1,[STATE2] STATE1,[ZIP2] ZIP1,ADDR13,[COUNTRY2] COUNTRY,[TEL2] TEL,*  From Customer Where trim(acc)=trim(@acc) or old_customer=trim(@acc)", "@acc", acc);
            }
            return _helperCommonService.GetSqlRow("select *  From Customer Where trim(acc)=trim(@acc) or old_customer=trim(@acc)", "@acc", acc);
        }

        public List<string> getFollowUpTypes()
        {
            // Get the DataTable from the stored procedure
            DataTable dt = _helperCommonService.GetStoreProc("GetFTypes");

            // Ensure the DataTable is not null or empty
            if (dt == null || dt.Rows.Count == 0)
            {
                return new List<string>(); // Return an empty list if there's no data
            }

            List<string> followUpTypes = new List<string>();

            // Loop through each row in the DataTable and extract the "ftype" column value
            foreach (DataRow row in dt.Rows)
            {
                // Check if the column exists and is not null
                if (row["ftype"] != DBNull.Value)
                {
                    followUpTypes.Add(row["ftype"].ToString());
                }
            }

            return followUpTypes;
        }


        #endregion
    }
}
