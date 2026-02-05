/*
 *  Created by Manoj 03-June-2025
 *  03-June-2025 Manoj Added GetSalesmen and getstoresdataforsetdefault and SalesmanActivityReport methods
 *  04-June-2025 Manoj Added GetInvoiceByInvNo And GetInvoiceMasterDetailPO methods
 *  05-June-2025 Manoj Added GetInvoicePrintofSalesTaxReport methods
 *  13-June-2025 Manoj Added getAllSales, GetInvoieComisionAmount methods
 *  09-July-2025 Manoj Fixed SalesmanActivityReport method result issues
 *  10-July-2025 Manoj Added telephone_no,PON,ship_via,paid,store_no,ShippingAddr_Name,ShippingAddr_Addr1,ShippingAddr_Addr2,ShippingAddr_City,ShippingAddr_State,ShippingAddr_Country,ShippingAddr_ZipCode properties
 *  10-July-2025 Manoj Fixed GetInvoicePrintofSalesTaxReport for Memo preview type
 *  31-July-2025 Manoj Fixed GetInvoicePrintofSalesTaxReport Issues
 *  01-Aug-2025  Manoj Added GetMemoByInvNo,GetInvoiceMemoNote Methods and Added storeInfo,StoreLogoImage,StoreName Properties
 *  23-DEC-2025  Manoj Added OtherCharges,Deduction,StyleTable Propertied And modified GetInvoicePrintofSalesTaxReport for data table issues and  updated email issue in store Address
 */
using Microsoft.Data.SqlClient;
using System.Data;
using YJWebCoreMVC.Models;

namespace YJWebCoreMVC.Services
{
    public class SalesmenService
    {

        private readonly ConnectionProvider _connectionProvider;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly HelperCommonService _helperCommonService;
        private readonly IWebHostEnvironment _env;
        private readonly string _companyName;
        private readonly string _storeCodeInUse;
        private readonly string _loggedUser;

        public SalesmenService(ConnectionProvider connectionProvider, IHttpContextAccessor httpContextAccessor, HelperCommonService helperCommonService, IWebHostEnvironment env)
        {
            _connectionProvider = connectionProvider;
            _httpContextAccessor = httpContextAccessor;
            _helperCommonService = helperCommonService;
            _companyName = _httpContextAccessor.HttpContext?.Session.GetString("COMPANYNAME");
            _storeCodeInUse = _httpContextAccessor.HttpContext?.Session.GetString("STORE_CODE");
            _loggedUser = _httpContextAccessor.HttpContext?.Session.GetString("UserId");
            _env = env;
        }


        public DataTable GetSalesmen()
        {
            return _helperCommonService.GetSqlData("select '' as Code from salesmen with (nolock) union select code from salesmen with (nolock) where iSNULL(inactive,0)=0 order by code asc");
        }

        public DataTable getstoresdataforsetdefault(bool ActiveOnly = false, bool allstores = false, bool withShop = false, bool NoText = false)
        {
            string FixedStoreCode = "";

            if (!string.IsNullOrWhiteSpace(_helperCommonService.FixedStoreCode))
            {
                FixedStoreCode = _helperCommonService.FixedStoreCode;
            }

            if (NoText)
            {
                if (!string.IsNullOrWhiteSpace(FixedStoreCode) && !allstores)
                    return _helperCommonService.GetSqlData("SELECT DISTINCT CODE FROM [stores] where code=@code ", "@code", FixedStoreCode);
                return _helperCommonService.GetSqlData("select '' as CODE UNION SELECT DISTINCT CODE FROM [stores] where notext=0  ORDER BY CODE ");
            }
            if (!withShop)
            {
                if (!string.IsNullOrWhiteSpace(FixedStoreCode) && !allstores)
                    return _helperCommonService.GetSqlData("SELECT DISTINCT CODE FROM [stores] where code=@code ", "@code", FixedStoreCode);
                if (ActiveOnly)
                    return _helperCommonService.GetSqlData("select distinct code from stores where code != '' and code is not null  order by code asc ");
                return _helperCommonService.GetSqlData("select '' as CODE UNION SELECT DISTINCT CODE FROM [stores] ORDER BY CODE ");
            }
            if (!string.IsNullOrWhiteSpace(FixedStoreCode) && !allstores)
                return _helperCommonService.GetSqlData("SELECT 'SHOP' as CODE UNION SELECT DISTINCT CODE FROM [stores] where code=@code ", "@code", FixedStoreCode);
            if (ActiveOnly)
                return _helperCommonService.GetSqlData("SELECT 'SHOP' as code UNION select distinct code from stores where code != '' and code is not null ");
            return _helperCommonService.GetSqlData("select '' as CODE UNION SELECT 'SHOP' as CODE UNION SELECT DISTINCT CODE FROM [stores] ");

        }
        public DataTable SalesmanActivityReport(string salesman, string date1, string date2, bool unpaid, bool iscommreport,
       string paid_ref, bool Fullpaidinvoice = false, string byWhichDate = "", bool isCommbydiscount = false,
       bool isshowdetails = false, bool isCommbyprofit = false, bool isreplcementcost = false,
       bool isPostSalesmanActivity = false, bool isSummSalesBrand = false, string storecode = "", bool iSDevam = false)
        {
            DataTable dataTable = new DataTable();
            var ConnectionString = _connectionProvider.GetConnectionString();
            using (var connection = new SqlConnection(ConnectionString))
            using (var command = new SqlCommand())
            {
                command.Connection = connection;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandTimeout = 6000;
                command.CommandText = iSDevam ? "Devam_SalesmanActivity" :
                    (salesman == "optall" ? "SalesmanActivityAll" : "SALESMANACTIVITY");

                // Add parameters
                command.Parameters.AddWithValue("@SALESMAN", salesman);
                command.Parameters.AddWithValue("@DATE1", date1);
                command.Parameters.AddWithValue("@DATE2", date2);
                command.Parameters.AddWithValue("@UNPAID", unpaid ? 1 : 0);
                command.Parameters.AddWithValue("@ISCOMMREPORT", iscommreport ? 1 : 0);
                command.Parameters.AddWithValue("@PAID_REF", paid_ref ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@ISFULLPAID", Fullpaidinvoice);
                command.Parameters.AddWithValue("@BYWHICHDATE", string.IsNullOrEmpty(byWhichDate) ? (object)DBNull.Value : byWhichDate);
                command.Parameters.AddWithValue("@isCommbydiscount", isCommbydiscount);
                command.Parameters.AddWithValue("@isshowdetails", isshowdetails);
                command.Parameters.AddWithValue("@isCommbyprofit", isCommbyprofit);
                command.Parameters.AddWithValue("@isreplcementcost", isreplcementcost);
                command.Parameters.AddWithValue("@isPostSalesmanActivity", isPostSalesmanActivity);
                command.Parameters.AddWithValue("@isSummarySalesBrand", isSummSalesBrand);
                command.Parameters.AddWithValue("@STORENO", string.IsNullOrEmpty(storecode) ? (object)DBNull.Value : storecode);

                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    dataTable.Load(reader);
                }

                return dataTable.Rows.Count > 0 ? dataTable : null;
            }
        }

        public SalesmenModel GetInvoicePrintofSalesTaxReport(string inv_no, bool is_memo, bool isBriony = false, bool iSVatInclude = false, bool IsOpenMemo = false)
        {
            string salesman = "", register = "", customerAcc = "";
            SalesmenModel Salesmenactivity = new SalesmenModel();
            DataRow invoicenoRow = GetMemoByInvNo(inv_no);
            customerAcc = invoicenoRow["acc"].ToString();
            DataTable dt = GetInvoiceMasterDetailPO(inv_no, is_memo, isBriony, iSVatInclude, IsOpenMemo);
            if (dt != null && dt.Rows.Count > 0)
            {
                DataView dvInvoice = new DataView(dt);

                DataRow getInvoice = GetInvoiceByInvNo(inv_no);
                string fmessage = string.Empty;

                string invoicenote, memonote;
                if (getInvoice != null)
                    fmessage = getInvoice["Message"].ToString().Trim();

                GetInvoiceMemoNote(customerAcc, out invoicenote, out memonote);
                memonote = !string.IsNullOrWhiteSpace(memonote) ? memonote : _helperCommonService.GetValue(_helperCommonService.getupsinsinformation(), "MemoNote");
                if (!string.IsNullOrWhiteSpace(memonote))
                    fmessage += Environment.NewLine + memonote.Trim();
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


                string sOtherCharges = string.Format("{0:C}", _helperCommonService.CheckForDBNull(dvInvoice[0]["add_cost"], typeof(decimal).ToString()));
                string sShipAndHandling = string.Format("{0:C}", _helperCommonService.CheckForDBNull(dvInvoice[0]["snh"], typeof(decimal).ToString()));
                string sSalesTax = string.Format("{0:C}", _helperCommonService.CheckForDBNull(dvInvoice[0]["sales_tax"], typeof(decimal).ToString()));
                string sGrandTotal = string.Format("{0:C}", _helperCommonService.CheckForDBNull(dvInvoice[0]["gr_total"], typeof(decimal).ToString()));
                string sTradeinAmt = string.Format("{0:C}", _helperCommonService.CheckForDBNull(dvInvoice[0]["tradeinamt"], typeof(decimal).ToString()));
                string paid = string.Format("{0:C}", _helperCommonService.CheckForDBNull(dvInvoice[0]["credits"], typeof(decimal).ToString()));
                string sDeduction = string.Format("{0:C}", _helperCommonService.CheckForDBNull(dvInvoice[0]["deduction"], typeof(decimal).ToString()));
                string balancedue = string.Format("{0:C}", _helperCommonService.CheckForDBNull(dvInvoice[0]["gr_total"], typeof(decimal).ToString()) - _helperCommonService.CheckForDBNull(dvInvoice[0]["credits"], typeof(decimal).ToString()));

                string sSubtotal = string.Format("{0:C}", dvInvoice.Table.Compute("SUM(Amount)", "1=1") == DBNull.Value ? 0 : dvInvoice.Table.Compute("SUM(Amount)", "1=1"));
                string UserGCNo = _helperCommonService.GetUserGCNo(inv_no);

                DataTable dtSubReport = _helperCommonService.GetInvoicePayments(inv_no);
                DataTable tradeInData = _helperCommonService.GetTradeInDataByInvoice(inv_no);

                #region Main Details
                Salesmenactivity.InvoiceNo = inv_no;
                Salesmenactivity.ACC = dt.Rows[0]["ACC"].ToString();
                Salesmenactivity.SaleDate1 = dt.Rows[0]["DATE"].ToString();
                Salesmenactivity.Salesman = salesman;
                Salesmenactivity.Register = register;
                #endregion

                #region Billing Address
                Salesmenactivity.BillingAddr_Name = dt.Rows[0]["NAME"].ToString();
                Salesmenactivity.BillingAddr_Addr1 = dt.Rows[0]["addr1"].ToString();
                Salesmenactivity.BillingAddr_Addr2 = dt.Rows[0]["addr2"].ToString();
                Salesmenactivity.BillingAddr_City = dt.Rows[0]["city"].ToString();
                Salesmenactivity.BillingAddr_State = dt.Rows[0]["state"].ToString();
                Salesmenactivity.BillingAddr_Country = dt.Rows[0]["country"].ToString();
                Salesmenactivity.BillingAddr_ZipCode = dt.Rows[0]["zip"].ToString();

                string storeCode = dt.Rows[0]["store_no"].ToString();
                string storename;

                string storeInfoAddress = _helperCommonService.GetStoreAddressByINovice_store(storeCode, "\n", out storename);
                byte[] storeLogo = _helperCommonService.GetStoreImage(storeCode != "" ? storeCode : "");

                Salesmenactivity.storeInfo = storeInfoAddress;
                Salesmenactivity.StoreLogoImage = storeLogo;
                Salesmenactivity.StoreName = storename;
                #endregion

                string TelNo = _helperCommonService.FormatTel(
                       !string.IsNullOrWhiteSpace(dvInvoice[0]["tel"].ToString()) ? dvInvoice[0]["tel"].ToString() : "");
                if (dt.Rows[0]["CNAME"].ToString() != "")
                {
                    Salesmenactivity.ShippingAddr_Name = dt.Rows[0]["CNAME"].ToString();
                    Salesmenactivity.ShippingAddr_Addr1 = dt.Rows[0]["CADDR1"].ToString();
                    Salesmenactivity.ShippingAddr_Addr2 = dt.Rows[0]["CADDR2"].ToString();
                    Salesmenactivity.ShippingAddr_City = dt.Rows[0]["CCITY"].ToString();
                    Salesmenactivity.ShippingAddr_State = dt.Rows[0]["CSTATE"].ToString();
                    Salesmenactivity.ShippingAddr_Country = dt.Rows[0]["CCOUNTRY"].ToString();
                    Salesmenactivity.ShippingAddr_ZipCode = dt.Rows[0]["CZIP"].ToString();
                    Salesmenactivity.store_no = storeCode;
                    Salesmenactivity.ship_via = _helperCommonService.ShipBy(_helperCommonService.CheckForDBNull(dvInvoice[0]["ship_by"]).ToString().Trim()); ;
                    Salesmenactivity.telephone_no = TelNo;
                    Salesmenactivity.PON = dt.Rows[0]["PON"].ToString();
                }


                #region Total Summary
                Salesmenactivity.SubTotal = (sSubtotal);
                Salesmenactivity.TradeInAmount = (sTradeinAmt);
                Salesmenactivity.Deduction = (sDeduction);
                Salesmenactivity.OtherCharges = (sOtherCharges);
                Salesmenactivity.ShippingAmount = (sShipAndHandling);
                Salesmenactivity.salesTaxAmount = (sSalesTax);
                Salesmenactivity.GrandTotal = (sGrandTotal);
                Salesmenactivity.PaidAmount = (paid);
                Salesmenactivity.BalanceDue = (balancedue);
                #endregion

                #region Grid details
                Salesmenactivity.StyleTable = dt;

                Salesmenactivity.Style = dt.Rows[0]["STYLE"].ToString();
                Salesmenactivity.Description = dt.Rows[0]["DESC"].ToString();
                Salesmenactivity.CostQty = dt.Rows[0]["QTY"].ToString();
                Salesmenactivity.TagAmount = dt.Rows[0]["TAG_PRICE"].ToString();
                Salesmenactivity.Price = dt.Rows[0]["PRICE"].ToString();
                Salesmenactivity.Amount = dt.Rows[0]["AMOUNT"].ToString();
                Salesmenactivity.NoteMessage = fmessage;
                #endregion
                //Salesmenactivity.Terms = dt.Rows[0]["Term"].ToString();
                //Salesmenactivity.Salesman = dt.Rows[0]["SALESMAN1"].ToString();
                #region Payment details
                if (dtSubReport != null && dtSubReport.Rows.Count > 0)
                {
                    Salesmenactivity.PaymentDate = dtSubReport.Rows[0]["DATE"].ToString();
                    Salesmenactivity.PaymentType = dtSubReport.Rows[0]["METHOD"].ToString();
                    Salesmenactivity.Payment = dtSubReport.Rows[0]["AMOUNT"].ToString();
                    Salesmenactivity.PaymentNote = dtSubReport.Rows[0]["NOTE"].ToString();
                    Salesmenactivity.PaymentCurrType = dtSubReport.Rows[0]["CURR_TYPE"].ToString();
                    Salesmenactivity.PaymentCurrRate = dtSubReport.Rows[0]["CURR_RATE"].ToString();
                    Salesmenactivity.PaymentCurrAmount = dtSubReport.Rows[0]["CURR_AMOUNT"].ToString();
                }
                #endregion

            }
            return Salesmenactivity;
        }

        public DataRow GetMemoByInvNo(string memo_no)
        {
            return _helperCommonService.GetSqlRow("select top 1 i.*, it.memo_no,it.fpon from Memo i left join me_items it on i.memo_no = it.memo_no Where trim(i.memo_no) = trim(@memo_no)", "@memo_no", memo_no);
        }

        public DataTable GetInvoiceMasterDetailPO(string invno, bool is_memo, bool isBriony = false, bool iSVatInclude = false, bool IsOpenMemo = false)
        {
            var ConnectionString = _connectionProvider.GetConnectionString();
            using (var connection = new SqlConnection(ConnectionString))
            using (var command = new SqlCommand(isBriony ? "GetMasterDetailInvoicePO_Briony" : "GetMasterDetailInvoicePO", connection))
            using (var adapter = new SqlDataAdapter(command))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandTimeout = 6000;

                // Add parameters
                command.Parameters.AddWithValue("@inv_no", invno);
                command.Parameters.AddWithValue("@is_memo", is_memo ? 1 : 0);
                if (!isBriony)
                    command.Parameters.AddWithValue("@iSVatInclude", iSVatInclude ? 1 : 0);
                command.Parameters.AddWithValue("@IsOpenMemo", IsOpenMemo ? 1 : 0);

                // Fill and return DataTable
                var dataTable = new DataTable();
                adapter.Fill(dataTable);
                return dataTable;
            }
        }

        public DataRow GetInvoiceByInvNo(string invno)
        {
            return (_helperCommonService.GetSqlRow(@"select top 1 i.*,it.memo_no,iSNULL(it.IsSpecialItem,0) IsSpecialItem,it.fpon 
                from invoice i  with (nolock)
                left join (select * from IN_ITEMS with (nolock) where Trimmed_inv_no =@inv_no) it on i.inv_no = it.inv_no 
                    Where trim(i.inv_no) = @inv_no", "@inv_no", invno.Trim()));
        }

        public DataTable getAllSales()
        {
            return _helperCommonService.GetSqlData("select distinct CODE from SALESMEN with (nolock) where CODE != '' and CODE is not null order by CODE");
        }

        public DataSet GetInvoieComisionAmount(string salesMan = "", string byWhichDate = "", string fDate = "", string tDate = "")
        {
            using (SqlConnection conn = new SqlConnection(_connectionProvider.GetConnectionString()))
            using (var command = new SqlCommand("GETINVOICECOMISIONAMOUNT", conn))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandTimeout = 9000;

                command.Parameters.AddWithValue("@SALESMAN", salesMan);
                command.Parameters.AddWithValue("@BYWHICHDATE", byWhichDate);
                command.Parameters.AddWithValue("@FDATE", fDate);
                command.Parameters.AddWithValue("@TDATE", tDate);

                using (var adapter = new SqlDataAdapter(command))
                {
                    var dataSet = new DataSet();
                    adapter.Fill(dataSet);
                    return dataSet;
                }
            }
        }
        public void GetInvoiceMemoNote(string acc, out string invoicenote, out string memonote)
        {
            invoicenote = memonote = string.Empty;
            using (SqlDataAdapter dataAdapter = new SqlDataAdapter())
            {
                DataTable dtInvoiceMemoNotes = new DataTable();

                // Create the command and set its properties
                dataAdapter.SelectCommand = new SqlCommand();
                dataAdapter.SelectCommand.Connection = _connectionProvider.GetConnection();
                dataAdapter.SelectCommand.CommandType = CommandType.Text;
                dataAdapter.SelectCommand.CommandText = "select top 1 invoicenote, memonote from invoicenotes where acc = @acc";

                dataAdapter.SelectCommand.Parameters.AddWithValue("@acc", acc);

                // Fill the datatable From adapter
                dataAdapter.Fill(dtInvoiceMemoNotes);

                // Get the datarow from the table
                if (dtInvoiceMemoNotes.Rows.Count > 0)
                {
                    invoicenote = _helperCommonService.CheckForDBNull(dtInvoiceMemoNotes.Rows[0]["invoicenote"]);
                    memonote = _helperCommonService.CheckForDBNull(dtInvoiceMemoNotes.Rows[0]["memonote"]);
                }
            }
        }

    }
}
