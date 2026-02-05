using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Globalization;
using YJWebCoreMVC.Models;

namespace YJWebCoreMVC.Services
{
    public class CastOrdService
    {

        private readonly ConnectionProvider _connectionProvider;
        private readonly HelperCommonService _helperCommonService;

        public CastOrdService(ConnectionProvider connectionProvider, HelperCommonService helperCommonService, IWebHostEnvironment env)
        {
            _connectionProvider = connectionProvider;
            _helperCommonService = helperCommonService;
        }

        public decimal WEIGHT { get; set; }
        public decimal SHIPED { get; set; }
        public decimal QTY { get; set; }
        public decimal Qty_Open { get; set; }
        public string ship_status { get; set; }
        public bool confirmed { get; set; }
        public string Approved { get; set; }
        public DateTime Approve_Date { get; set; }
        public string Approve_Note { get; set; }
        public decimal SELL_PRICE { get; set; }

        public List<CastOrdModel> getAllPOs()
        {
            DataTable dataTable = new DataTable();
            List<CastOrdModel> lstCastOrd = new List<CastOrdModel>();

            using (SqlDataAdapter dataAdapter = new SqlDataAdapter())
            {
                try
                {
                    // Create the command and set its properties
                    dataAdapter.SelectCommand = new SqlCommand();
                    dataAdapter.SelectCommand.Connection = _connectionProvider.GetConnection();

                    dataAdapter.SelectCommand.CommandType = CommandType.Text;
                    dataAdapter.SelectCommand.CommandText = "SELECT *, (QTY-ISNULL(SHIPED,0)) AS Qty_Open FROM CAST_ORD WHERE TRIM(INV_NO)!='' AND RCVD < QTY ORDER BY CONVERT(INT,INV_NO) ASC";

                    // Fill the datatable From adapter
                    dataAdapter.Fill(dataTable);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                // Get the datarow from the table
                //foreach (DataRow dr in dataTable.Select("INV_NO='     1'").CopyToDataTable().Rows)
                foreach (DataRow dr in dataTable.Rows)
                {
                    decimal shipped;
                    decimal sell_price;
                    if (dr["SHIPED"] != DBNull.Value && Decimal.TryParse(dr["SHIPED"].ToString(), out shipped))
                    {
                        SHIPED = shipped;
                    }
                    else
                    {
                        SHIPED = 0; // Default value
                    }
                    if (dr["SELL_PRICE"] != DBNull.Value && Decimal.TryParse(dr["SELL_PRICE"].ToString(), out sell_price))
                    {
                        SELL_PRICE = sell_price;
                    }
                    else
                    {
                        SELL_PRICE = 0; // Default value
                    }
                    decimal qtyOpen;
                    if (dr["Qty_Open"] != DBNull.Value && Decimal.TryParse(dr["Qty_Open"].ToString(), out qtyOpen))
                        Qty_Open = qtyOpen;
                    else
                        Qty_Open = 0; // Default value


                    lstCastOrd.Add(new CastOrdModel()
                    {
                        //keep doing for all fields like this
                        INV_NO = dr["Inv_no"].ToString().Trim(),
                        ACC = dr["ACC"].ToString(),
                        //PON = dr["PON"].ToString(),
                        vnd_style = dr["VND_STYLE"].ToString(),
                        QTY = Decimal.Parse(dr["QTY"].ToString()),
                        RCVD = Decimal.Parse(dr["RCVD"].ToString()),
                        SELL_PRICE = SELL_PRICE,
                        OrderDate = _helperCommonService.TryDateTimeParse(dr["DATE"].ToString()),
                        DUE_DATE = _helperCommonService.TryDateTimeParse(dr["DUE_DATE"].ToString()),
                        SHIPED = SHIPED,
                        Qty_Open = Qty_Open,

                    });
                    ;
                }
                return lstCastOrd;
            }
        }

        public List<CastOrdModel> getNewPOs()
        {
            DataTable dataTable = new DataTable();
            List<CastOrdModel> lstCastOrd = new List<CastOrdModel>();

            using (SqlDataAdapter dataAdapter = new SqlDataAdapter())
            {
                try
                {
                    // Create the command and set its properties
                    dataAdapter.SelectCommand = new SqlCommand();
                    dataAdapter.SelectCommand.Connection = _connectionProvider.GetConnection();

                    dataAdapter.SelectCommand.CommandType = CommandType.Text;
                    dataAdapter.SelectCommand.CommandText = "SELECT INV_NO, MIN(VND_STYLE) as STYLE,MIN(DATE) as OrderDate, MIN(DUE_DATE) AS DUE_DATE, MIN(ACC) AS ACC, SUM(QTY) AS QTY, SUM(SHIPED) AS SHIPED, Approved FROM CAST_ORD  WHERE TRIM(INV_NO)!='' AND RCVD < QTY AND Approved='' GROUP BY INV_NO, Approved ";
                    //dataAdapter.SelectCommand.CommandText = "UPDATE cast_ord SET confirmed=1 WHERE inv_no=@inv_no";
                    // Fill the datatable From adapter
                    dataAdapter.Fill(dataTable);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                // Get the datarow from the table
                foreach (DataRow dr in dataTable.Rows)
                {
                    lstCastOrd.Add(new CastOrdModel()
                    {
                        //keep doing for all fields like this
                        INV_NO = dr["Inv_no"].ToString(),
                        ACC = dr["ACC"].ToString(),
                        ////PON = dr["PON"].ToString(),
                        OrderDate = _helperCommonService.TryDateTimeParse(dr["OrderDate"].ToString()),
                        DUE_DATE = _helperCommonService.TryDateTimeParse(dr["DUE_DATE"].ToString()),
                        QTY = Decimal.Parse(dr["QTY"].ToString()),
                        //SHIPED = Decimal.Parse(dr["SHIPED"].ToString()),
                        vnd_style = dr["STYLE"].ToString(),

                    }); ;
                    ;

                }
                return lstCastOrd;
            }
        }

        public List<CastOrdModel> getConfirmedPos()
        {
            DataTable dataTable = new DataTable();
            List<CastOrdModel> lstCastOrd = new List<CastOrdModel>();

            using (SqlDataAdapter dataAdapter = new SqlDataAdapter())
            {
                try
                {
                    // Create the command and set its properties
                    dataAdapter.SelectCommand = new SqlCommand();
                    dataAdapter.SelectCommand.Connection = _connectionProvider.GetConnection();

                    dataAdapter.SelectCommand.CommandType = CommandType.Text;
                    dataAdapter.SelectCommand.CommandText = "SELECT INV_NO ,MIN(VND_STYLE) as STYLE,MIN(DATE) as OrderDate, MIN(DUE_DATE) AS DUE_DATE, MIN(ACC) AS ACC, SUM(QTY) AS QTY, SUM(SHIPED) AS SHIPED, Approved FROM CAST_ORD  WHERE TRIM(INV_NO)!='' AND RCVD < QTY AND Approved=1 GROUP BY INV_NO, Approved";

                    // Fill the datatable From adapter
                    dataAdapter.Fill(dataTable);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                // Get the datarow from the table
                foreach (DataRow dr in dataTable.Rows)
                {
                    ship_status = "";
                    QTY = Decimal.Parse(dr["QTY"].ToString());
                    //SHIPED = Decimal.Parse(dr["SHIPED"].ToString());
                    if (SHIPED == 0)
                    {
                        ship_status = "Not Shipped";
                    }
                    if (QTY <= SHIPED)
                    {
                        ship_status = "Full Shipped";
                    }
                    if (SHIPED > 0 && SHIPED < QTY)
                    {
                        ship_status = "Partial Shipped";
                    }

                    lstCastOrd.Add(new CastOrdModel()
                    {
                        //keep doing for all fields like this
                        INV_NO = dr["Inv_no"].ToString(),
                        ACC = dr["ACC"].ToString(),
                        //PON = dr["PON"].ToString(),
                        vnd_style = dr["STYLE"].ToString(),
                        OrderDate = _helperCommonService.TryDateTimeParse(dr["OrderDate"].ToString()),
                        DUE_DATE = _helperCommonService.TryDateTimeParse(dr["DUE_DATE"].ToString()),
                        ship_status = ship_status,

                    });

                }
                return lstCastOrd;
            }
        }


        public List<CastOrdModel> getVendorPODetails(int INV_NO)
        {
            DataTable dataTable = new DataTable();
            List<CastOrdModel> lstCastOrd = new List<CastOrdModel>();

            using (SqlDataAdapter dataAdapter = new SqlDataAdapter())
            {
                try
                {
                    // Create the command and set its properties
                    dataAdapter.SelectCommand = new SqlCommand();
                    dataAdapter.SelectCommand.Connection = _connectionProvider.GetConnection();

                    dataAdapter.SelectCommand.CommandType = CommandType.Text;
                    dataAdapter.SelectCommand.CommandText = "SELECT * FROM CAST_ORD  WHERE TRIM(INV_NO)!='' AND RCVD < QTY AND INV_NO=@inv_no";
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@inv_no", INV_NO);

                    // Fill the datatable From adapter
                    dataAdapter.Fill(dataTable);

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                // Get the datarow from the table
                foreach (DataRow dr in dataTable.Rows)
                {
                    decimal shipped;
                    if (dr["SHIPED"] != DBNull.Value && Decimal.TryParse(dr["SHIPED"].ToString(), out shipped))
                    {
                        SHIPED = shipped;
                    }
                    else
                    {
                        SHIPED = 0; // Default value
                    }
                    decimal qty;
                    if (Decimal.TryParse(dr["QTY"].ToString(), NumberStyles.Any, CultureInfo.InvariantCulture, out qty))
                    {
                        QTY = qty;
                    }
                    else
                    {
                        QTY = Decimal.Parse(dr["QTY"].ToString());
                    }
                    Qty_Open = QTY - SHIPED;

                    lstCastOrd.Add(new CastOrdModel()
                    {
                        //keep doing for all fields like this
                        INV_NO = dr["Inv_no"].ToString().Trim(),
                        vnd_style = dr["vnd_style"].ToString(),
                        ACC = dr["ACC"].ToString(),
                        OrderDate = _helperCommonService.TryDateTimeParse(dr["DATE"].ToString()),
                        //PON = dr["PON"].ToString(),
                        QTY = QTY,
                        RCVD = Decimal.Parse(dr["RCVD"].ToString()),
                        DUE_DATE = _helperCommonService.TryDateTimeParse(dr["DUE_DATE"].ToString()),
                        SHIPED = SHIPED,
                        Approved = dr["Approved"].ToString(),
                        //Approve_Date = Approve_Date,
                        //Approve_Date = Convert.ToDateTime(dr["Approve_Date"].ToString()),
                        Qty_Open = Qty_Open,

                    }); ;
                    ;

                }
                return lstCastOrd;
            }
        }


        public List<CastOrdModel> DownloadPo(int INV_NO)
        {
            DataTable dataTable = new DataTable();
            List<CastOrdModel> lstCastOrd = new List<CastOrdModel>();

            using (SqlDataAdapter dataAdapter = new SqlDataAdapter())
            {
                try
                {
                    // Create the command and set its properties
                    dataAdapter.SelectCommand = new SqlCommand();
                    dataAdapter.SelectCommand.Connection = _connectionProvider.GetConnection();

                    dataAdapter.SelectCommand.CommandType = CommandType.Text;
                    dataAdapter.SelectCommand.CommandText = "SELECT * FROM CAST_ORD  WHERE TRIM(INV_NO)!='' AND RCVD < QTY AND INV_NO=@inv_no";
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@inv_no", INV_NO);

                    // Fill the datatable From adapter
                    dataAdapter.Fill(dataTable);

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                // Get the datarow from the table
                foreach (DataRow dr in dataTable.Rows)
                {
                    lstCastOrd.Add(new CastOrdModel()
                    {
                        //keep doing for all fields like this
                        INV_NO = dr["Inv_no"].ToString(),
                        ACC = dr["ACC"].ToString(),
                        PON = dr["PON"].ToString(),
                        vnd_style = dr["VND_STYLE"].ToString(),

                    }); ;
                    ;

                }
                return lstCastOrd;
            }
        }


        public string confirmPo(string INV_NO)
        {
            DataTable dataTable = new DataTable();
            List<CastOrdModel> lstCastOrd = new List<CastOrdModel>();

            using (SqlDataAdapter dataAdapter = new SqlDataAdapter())
            {
                try
                {
                    // Create the command and set its properties
                    dataAdapter.SelectCommand = new SqlCommand();
                    dataAdapter.SelectCommand.Connection = _connectionProvider.GetConnection();

                    dataAdapter.SelectCommand.CommandType = CommandType.Text;
                    dataAdapter.SelectCommand.CommandText = "UPDATE CAST_ORD SET Approved=1, Approve_Date=getdate() WHERE TRIM(INV_NO)=@inv_no";
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@inv_no", INV_NO);

                    // Fill the datatable From adapter
                    dataAdapter.Fill(dataTable);

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                return "Success";
            }
        }


        public string updatePoQty(string INV_NO, int qty, string ACC, string vnd_style, decimal sell_price)
        {
            DataTable dataTable = new DataTable();
            List<CastOrdModel> lstCastOrd = new List<CastOrdModel>();

            using (SqlConnection connection = _connectionProvider.GetConnection())
            {
                connection.Open();
                try
                {
                    using (SqlCommand command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandType = CommandType.Text;
                        command.CommandText = "UPDATE CAST_ORD SET shiped= @qty , SELL_PRICE=@sell_price WHERE TRIM(INV_NO)=@INV_NO AND ACC=@ACC AND VND_STYLE=@VSTYLE";

                        // Add parameters
                        command.Parameters.AddWithValue("@qty", Convert.ToDecimal(qty));
                        command.Parameters.AddWithValue("@sell_price", Convert.ToDecimal(sell_price));
                        command.Parameters.AddWithValue("@INV_NO", INV_NO);
                        //command.Parameters.AddWithValue("@", INV_NO);
                        command.Parameters.AddWithValue("@ACC", ACC);
                        command.Parameters.AddWithValue("@VSTYLE", vnd_style);

                        string query = command.CommandText;
                        // Execute the command
                        int rowsAffected = command.ExecuteNonQuery();

                        // Check if any rows were affected
                        if (rowsAffected > 0)
                        {
                            return "Success";
                        }
                        else
                        {
                            return "Failed";
                        }
                    }

                }
                catch (Exception)
                {
                    return "failed";
                }
            }
        }

        public string updatePoDueDate(string INV_NO, DateTime due_date)
        {
            DataTable dataTable = new DataTable();
            List<CastOrdModel> lstCastOrd = new List<CastOrdModel>();

            using (SqlConnection connection = _connectionProvider.GetConnection())
            {
                connection.Open();

                try
                {
                    using (SqlCommand command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandType = CommandType.Text;
                        command.CommandText = "UPDATE CAST_ORD SET DUE_DATE=@due_date WHERE trim(INV_NO)=trim(@INV_NO)";

                        // Add parameters
                        string formattedDate = due_date.ToString("yyyyMMdd");
                        command.Parameters.AddWithValue("@due_date", formattedDate);
                        command.Parameters.AddWithValue("@INV_NO", INV_NO);

                        // Execute the command
                        int rowsAffected = command.ExecuteNonQuery();

                        // Check if any rows were affected
                        if (rowsAffected > 0)
                        {
                            return "Success";
                        }
                        else
                        {
                            return "Failed";
                        }
                    }
                }
                catch (Exception)
                {

                    return "Failed";
                }
            }

        }


        #region Sales Tax Report print methods
        public string StoreName { get; set; }
        public IEnumerable<SelectListItem> StoresList { get; set; }
        public bool rbDateSelection { get; set; }
        public DataTable getsalesTaxData(string fromdate, string todate, string storename, bool includeInactive = false, bool noSalesTax = false, bool includeNotaxInvoices = false, string taxState = "", bool IncParialPay = false, bool isDataPerState = false, bool ByPickupDate = false, bool ChkAllDates = true)
        {
            DataTable dataTable = new DataTable();

            using (SqlDataAdapter SqlDataAdapter = new SqlDataAdapter())
            {
                SqlDataAdapter.SelectCommand = new SqlCommand();
                SqlDataAdapter.SelectCommand.Connection = _connectionProvider.GetConnection();
                SqlDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter.SelectCommand.CommandTimeout = 6000;
                SqlDataAdapter.SelectCommand.CommandText = !noSalesTax ? "GetSalesTaxData" : "GetNoTaxReasonwiseInvoiceTotal";
                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@FromDate", fromdate);
                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@ToDate", todate);
                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@StoreName", storename);
                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@IncludeInactive", includeInactive);
                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@includeNotaxInvoices", includeNotaxInvoices);
                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@TaxState", taxState);
                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@iSPartial", IncParialPay);
                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@isDataPerState", @isDataPerState);
                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@byPickupDate", ByPickupDate);

                SqlDataAdapter.Fill(dataTable);
            }
            return dataTable;
        }
        #endregion

        #region Print

        public CastOrdModel GetInvoicePrintofSalesTaxReport(string inv_no, bool is_memo, bool isBriony = false, bool iSVatInclude = false, bool IsOpenMemo = false)
        {
            string salesman = "", register = "";
            CastOrdModel cashOrdModel = new CastOrdModel();
            //DataRow invoiceRow = GetInvoiceByInvNo(inv_no.Trim().PadLeft(6, ' '));
            DataTable dt = GetInvoiceMasterDetailPO(inv_no, is_memo, isBriony, iSVatInclude, IsOpenMemo);
            if (dt != null && dt.Rows.Count > 0)
            {
                DataView dvInvoice = new DataView(dt);

                string fmessage = string.Empty;
                //fmessage = Convert.ToString(invoiceRow["Message"]).Trim();
                //foreach (DataRow row in data.Rows)
                //    row["StyleImage"] = _helperCommonService.GetImage(row["Style"].ToString());
                string invoicenote = string.Empty;
                if (string.IsNullOrWhiteSpace(fmessage.Trim()))
                {
                    invoicenote = !string.IsNullOrWhiteSpace(invoicenote) ? invoicenote : _helperCommonService.InvoiceNote;
                    if (!string.IsNullOrWhiteSpace(invoicenote))
                        fmessage += Environment.NewLine + invoicenote.Trim();
                }

                salesman = dvInvoice[0]["salesman"].ToString();
                DataTable smaninfo = _helperCommonService.getdatafromdbbasedoncondition("name", "salesmen", "code='" + salesman + "'");
                if (smaninfo.Rows.Count > 0)
                {
                    DataView dvInvoice1 = new DataView(smaninfo);
                    salesman = dvInvoice1[0]["name"].ToString();
                }
                DataTable registerinfo = _helperCommonService.getdatafromdbbasedoncondition("REGISTER", "invoice", "inv_no='" + inv_no + "'");
                if (registerinfo.Rows.Count > 0)
                {
                    DataView dvInvoice2 = new DataView(registerinfo);
                    register = dvInvoice2[0]["REGISTER"].ToString();
                }
                string addrLabel;
                addrLabel = string.Format("{0}\n{1}\n{2}\n{3} {4} {5} {6}", dvInvoice[0]["cname"].ToString().Trim(), dvInvoice[0]["caddr1"].ToString().Trim(), dvInvoice[0]["caddr2"].ToString().Trim(), dvInvoice[0]["ccity"].ToString().Trim(), dvInvoice[0]["cstate"].ToString().Trim(), dvInvoice[0]["czip"].ToString().Trim(), dvInvoice[0]["ccountry"].ToString().Trim()).Replace("\n\n", "\n");

                string sSalesTax = string.Format("{0:C}", _helperCommonService.CheckForDBNull(dvInvoice[0]["sales_tax"], typeof(decimal).ToString()));
                string sGrandTotal = string.Format("{0:C}", _helperCommonService.CheckForDBNull(dvInvoice[0]["gr_total"], typeof(decimal).ToString()));
                string sTradeinAmt = string.Format("{0:C}", _helperCommonService.CheckForDBNull(dvInvoice[0]["tradeinamt"], typeof(decimal).ToString()));
                string paid = string.Format("{0:C}", _helperCommonService.CheckForDBNull(dvInvoice[0]["credits"], typeof(decimal).ToString()));

                string balancedue = string.Format("{0:C}", _helperCommonService.CheckForDBNull(dvInvoice[0]["gr_total"], typeof(decimal).ToString()) - _helperCommonService.CheckForDBNull(dvInvoice[0]["credits"], typeof(decimal).ToString()));

                string sSubtotal = string.Format("{0:C}", dvInvoice.Table.Compute("SUM(Amount)", "1=1") == DBNull.Value ? 0 : dvInvoice.Table.Compute("SUM(Amount)", "1=1"));
                string UserGCNo = _helperCommonService.GetUserGCNo(inv_no);

                DataTable dtSubReport = _helperCommonService.GetInvoicePayments(inv_no);
                DataTable tradeInData = _helperCommonService.GetTradeInDataByInvoice(inv_no);

                #region Main Details
                cashOrdModel.InvoiceNo = inv_no;
                cashOrdModel.ACC = dt.Rows[0]["ACC"].ToString();
                cashOrdModel.SaleDate1 = dt.Rows[0]["DATE"].ToString();
                cashOrdModel.Salesman = salesman;
                cashOrdModel.Register = register;
                #endregion

                #region Billing Address
                cashOrdModel.BillingAddr_Name = dt.Rows[0]["NAME"].ToString();
                cashOrdModel.BillingAddr_Addr1 = dt.Rows[0]["addr1"].ToString();
                cashOrdModel.BillingAddr_Addr2 = dt.Rows[0]["addr2"].ToString();
                cashOrdModel.BillingAddr_City = dt.Rows[0]["city"].ToString();
                cashOrdModel.BillingAddr_State = dt.Rows[0]["state"].ToString();
                cashOrdModel.BillingAddr_Country = dt.Rows[0]["country"].ToString();
                cashOrdModel.BillingAddr_ZipCode = dt.Rows[0]["zip"].ToString();
                #endregion

                #region Total Summary
                cashOrdModel.SubTotal = (sSubtotal);
                cashOrdModel.TradeInAmount = (sTradeinAmt);
                cashOrdModel.ShippingAmount = "0.00";
                cashOrdModel.salesTaxAmount = (sSalesTax);
                cashOrdModel.GrandTotal = (sGrandTotal);
                cashOrdModel.PaidAmount = (paid);
                cashOrdModel.BalanceDue = (balancedue);
                #endregion

                #region Grid details
                cashOrdModel.Style = dt.Rows[0]["STYLE"].ToString();
                cashOrdModel.Description = dt.Rows[0]["DESC"].ToString();
                cashOrdModel.CostQty = Convert.ToDecimal(dt.Rows[0]["QTY"].ToString());
                cashOrdModel.TagAmount = Convert.ToDecimal(dt.Rows[0]["TAG_PRICE"].ToString());
                cashOrdModel.Price = Convert.ToDecimal(dt.Rows[0]["PRICE"].ToString());
                cashOrdModel.Amount = Convert.ToDecimal(dt.Rows[0]["AMOUNT"].ToString());
                cashOrdModel.NoteMessage = fmessage;
                #endregion

                #region Payment details
                if (dtSubReport != null && dtSubReport.Rows.Count > 0)
                {
                    cashOrdModel.PaymentDate = dtSubReport.Rows[0]["DATE"].ToString();
                    cashOrdModel.PaymentType = dtSubReport.Rows[0]["METHOD"].ToString();
                    cashOrdModel.Payment = Convert.ToDecimal(dtSubReport.Rows[0]["AMOUNT"].ToString());
                    cashOrdModel.PaymentNote = dtSubReport.Rows[0]["NOTE"].ToString();
                    cashOrdModel.PaymentCurrType = dtSubReport.Rows[0]["CURR_TYPE"].ToString();
                    cashOrdModel.PaymentCurrRate = dtSubReport.Rows[0]["CURR_RATE"].ToString();
                    cashOrdModel.PaymentCurrAmount = Convert.ToDecimal(dtSubReport.Rows[0]["CURR_AMOUNT"].ToString());
                }
                #endregion
                //string emailid = _helperCommonService.CompanyEmail;
                //string companyname = _helperCommonService.CompanyName;
                //string telenumber = _helperCommonService.CompanyTel;
            }
            return cashOrdModel;
        }

        public DataTable GetInvoiceMasterDetailPO(string invno, bool is_memo, bool isBriony = false, bool iSVatInclude = false, bool IsOpenMemo = false)
        {
            DataTable dataTable = new DataTable();

            using (SqlDataAdapter dataAdapter = new SqlDataAdapter())
            {
                // Create the command and set its properties
                dataAdapter.SelectCommand = new SqlCommand();
                dataAdapter.SelectCommand.Connection = _connectionProvider.GetConnection();
                dataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                dataAdapter.SelectCommand.CommandText = !isBriony ? "GetMasterDetailInvoicePO" : "GetMasterDetailInvoicePO_Briony";
                dataAdapter.SelectCommand.CommandTimeout = 6000;

                // Add the parameter to the parameter collection
                dataAdapter.SelectCommand.Parameters.AddWithValue("@inv_no", invno);
                dataAdapter.SelectCommand.Parameters.AddWithValue("@is_memo", is_memo ? 1 : 0);
                if (!isBriony)
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@iSVatInclude", iSVatInclude ? 1 : 0);
                dataAdapter.SelectCommand.Parameters.AddWithValue("@IsOpenMemo", IsOpenMemo ? 1 : 0);

                // Fill the datatable From adapter
                dataAdapter.Fill(dataTable);

                return dataTable;
            }
        }

        public DataRow GetInvoiceByInvNo(string invno)
        {
            return (_helperCommonService.GetSqlRow(@"select top 1 i.*,it.memo_no,iSNULL(it.IsSpecialItem,0) IsSpecialItem,it.fpon 
                from invoice i 
                left join (select * from IN_ITEMS where trim(INV_NO) =@inv_no) it on i.inv_no = it.inv_no 
                    Where trim(i.inv_no) = @inv_no", "@inv_no", invno.Trim()));
        }

        #endregion

        #region List of Discounts
        public bool rbListofDiscountsSelection { get; set; }
        public DataTable ListofDiscounts(DateTime? date1, DateTime? date2, string store, string strSearchOption, bool isLocgroupby, bool isDiscodegroupby, bool IschkAllDates)
        {
            DataTable dataTable = new DataTable();
            using (SqlDataAdapter dataAdapter = new SqlDataAdapter())
            {
                // Create the command and set its properties
                dataAdapter.SelectCommand = new SqlCommand();
                dataAdapter.SelectCommand.Connection = _connectionProvider.GetConnection();
                dataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                dataAdapter.SelectCommand.CommandText = "ListofDiscounts";
                // Add the parameter to the parameter collection
                dataAdapter.SelectCommand.Parameters.AddWithValue("@date1", date1);
                dataAdapter.SelectCommand.Parameters.AddWithValue("@date2", date2);
                //dataAdapter.SelectCommand.Parameters.AddWithValue("@openonly", openonly ? 1 : 0);
                dataAdapter.SelectCommand.Parameters.AddWithValue("@SelectedStore", store);
                dataAdapter.SelectCommand.Parameters.AddWithValue("@SearchOption", strSearchOption);
                dataAdapter.SelectCommand.Parameters.AddWithValue("@isLocgroupby", isLocgroupby);
                dataAdapter.SelectCommand.Parameters.AddWithValue("@isDiscodegroupby", isDiscodegroupby);
                // Fill the datatable From adapter
                dataAdapter.Fill(dataTable);
                // Get the datarow from the table
                return dataTable;
            }
        }
        #endregion
    }
}
