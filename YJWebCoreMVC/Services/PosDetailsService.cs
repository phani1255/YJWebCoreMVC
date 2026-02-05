//-- Neetha    10/21/2024 Added Sales Tax report Methods.//
//-- Neetha    10/24/2024 Added ListofDiscounts().//
//-- Neetha    10/25/2024 Moved methods from castordmodel to PosDetailsModel.//
//-- Neetha    11/07/2024 Added GetSalesTaxDataDetailsForPreview to show subtotals in preview.//
//-- Neetha    11/12/2024 Added city column in salestaxreport preview.//
//-- Neetha    12/13/2024 Added List of Customer Warranties Methods.//
//-- Neetha    12/17/2024 Tax state filter functionality changes.//
//-- Neetha    04/15/2025 Added Delete An Invoice methods.
//-- Chakri    12/29/2025 Added GetJmCareWannatyInvoice, Extendedjmcarewarrantyinvoice, GetWarrantyInvoice and Getsalestax methods.
//-- Chakri    01/06/2026 Added InvoiceWarrantyStyles, iSWarrantyInvoice and related properties.

using Microsoft.Data.SqlClient;
using System.Data;
using System.Net;
using YJWebCoreMVC.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace YJWebCoreMVC.Services
{
    public class PosDetailsService
    {
        private readonly ConnectionProvider _connectionProvider;
        private readonly HelperCommonService _helperCommonService;

        public PosDetailsService(ConnectionProvider connectionProvider, HelperCommonService helperCommonService)
        {
            _connectionProvider = connectionProvider;
            _helperCommonService = helperCommonService;
        }

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
                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@isDataPerState", isDataPerState);
                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@byPickupDate", ByPickupDate);

                SqlDataAdapter.Fill(dataTable);
            }
            return dataTable;
        }

        public PosDetailsModel GetInvoicePrintofSalesTaxReport(string inv_no, bool is_memo, bool isBriony = false, bool iSVatInclude = false, bool IsOpenMemo = false)
        {
            string salesman = "", register = "";
            PosDetailsModel cashOrdModel = new PosDetailsModel();

            DataTable dt = GetInvoiceMasterDetailPO(inv_no, is_memo, isBriony, iSVatInclude, IsOpenMemo);
            if (dt != null && dt.Rows.Count > 0)
            {
                DataView dvInvoice = new DataView(dt);

                string fmessage = string.Empty;

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

        public PosDetailsModel GetSalesTaxDataDetailsForPreview(string fdate, string tdate, string storename, bool IncludeInactive = false, bool IsnoSalesTax = false, bool IncludeNotaxInvoices = false, string TaxState = "",
         bool IsIncParialPay = false, bool isDataPerState = false, bool ByPickupDate = false, bool IschkAllDates = true)
        {
            PosDetailsModel objModel = new PosDetailsModel();
            List<PosDetailsModel> salesTaxList = new List<PosDetailsModel>();
            List<PosDetailsModel> NosalesTaxList = new List<PosDetailsModel>();
            DataTable dataForSalesTaxReport = getsalesTaxData(fdate, tdate, storename, IncludeInactive, false, IncludeNotaxInvoices, "", IsIncParialPay, isDataPerState, ByPickupDate, IschkAllDates);
            if (dataForSalesTaxReport != null && dataForSalesTaxReport.Rows.Count > 0)
            {
                foreach (DataRow dr in dataForSalesTaxReport.Rows)
                {
                    salesTaxList.Add(new PosDetailsModel()
                    {   //keep doing for all fields like this
                        InvoiceNo = dr["Inv_no"].ToString(),
                        SaleDate = _helperCommonService.TryDateTimeParse(dr["Date"].ToString()),
                        GrandTotal = dr["Gr_Total"].ToString(),
                        salesTaxAmount = dr["Sales_Tax"].ToString(),
                        NetAmount = dr["Net"].ToString(),
                        StoreName = dr["Store"].ToString(),
                        CityName = dr["CITY"].ToString(),
                        StateName = dr["STATE"].ToString(),
                        salesTaxRate = dr["sales_tax_rate"].ToString()
                    });
                }
                if (!string.IsNullOrEmpty(TaxState))
                {
                    salesTaxList = salesTaxList.Where(item => item.StateName.ToUpper() == TaxState.ToUpper()).ToList();
                }
                objModel.getSalesTaxList = salesTaxList;
            }

            DataTable nosaletaxinfo = getsalesTaxData(fdate, tdate, storename, IncludeInactive, true, IncludeNotaxInvoices, "", IsIncParialPay, isDataPerState, ByPickupDate);
            if (nosaletaxinfo != null && nosaletaxinfo.Rows.Count > 0)
            {
                foreach (DataRow dr in nosaletaxinfo.Rows)
                {
                    NosalesTaxList.Add(new PosDetailsModel()
                    {
                        //keep doing for all fields like this
                        NoTaxReason = dr["NoTax_Reason"].ToString(),
                        NoTaxReasonGrandTotal = dr["Gr_Total"].ToString()
                    });


                    objModel.NoTaxReasonGrandTotal1 += Convert.ToDecimal(dr["Gr_Total"].ToString());
                }
                objModel.NoSalesTaxReasonList = NosalesTaxList;
            }

            if (fdate != "" && tdate != "")
            {
                DateTime fromDT = Convert.ToDateTime(fdate);
                DateTime toDT = Convert.ToDateTime(tdate);
                objModel.fromDate = fromDT.ToShortDateString();
                objModel.toDate = toDT.ToShortDateString();
            }
            return objModel;
        }

        public DataTable GetCustomerWarranty(DateTime? FromDt, DateTime? ToDt, string ACC = "", bool isesp = false, int pas = 0, bool filterwithNxtInspDate = false, string salesman1 = "")
        {
            DataTable dataTable = new DataTable();

            using (SqlDataAdapter dataAdapter = new SqlDataAdapter())
            {

                dataAdapter.SelectCommand = new SqlCommand();
                dataAdapter.SelectCommand.Connection = _connectionProvider.GetConnection();
                dataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                dataAdapter.SelectCommand.CommandText = @"GetCustomerWarranty";


                dataAdapter.SelectCommand.Parameters.AddWithValue("@FromDt", FromDt);
                dataAdapter.SelectCommand.Parameters.AddWithValue("@ToDt", ToDt);
                dataAdapter.SelectCommand.Parameters.AddWithValue("@ACC", string.IsNullOrWhiteSpace(ACC.Trim()) ? null : ACC.Trim());
                dataAdapter.SelectCommand.Parameters.AddWithValue("@isesp", isesp);
                dataAdapter.SelectCommand.Parameters.AddWithValue("@pastdou", pas);
                dataAdapter.SelectCommand.Parameters.AddWithValue("@filterwithNxtCheckDate", filterwithNxtInspDate);
                dataAdapter.SelectCommand.Parameters.AddWithValue("@salesman", salesman1);

                dataAdapter.Fill(dataTable);


                return dataTable;
            }
        }
        public DataTable GetSalesmen()
        {

            return _helperCommonService.GetSqlData("select Code from salesmen union select code from salesmen where iSNULL(inactive,0)=0 order by code asc");
        }

        public DataTable GetMessages()
        {

            return _helperCommonService.GetSqlData("Select NAME,Message from messages");
        }
        public DataTable SearchCustomersForRepair(string acc)
        {
            return _helperCommonService.GetSqlData(@"select iif(isnull(different_ship,0)=0,coalesce(nullif([name],''),nullif(name2,'')),[name]) [name],name2,
                                            iif(isnull(different_ship,0)=0,coalesce(nullif(tel,0),nullif(tel2,0)),tel) tel,email,
                                            iif(isnull(different_ship,0)=0,coalesce(nullif(addr1,''),nullif(addr2,'')),addr2)addr2,
                                            iif(isnull(different_ship,0)=0,coalesce(nullif(addr12,''),nullif(addr22,'')),addr22)addr22,
                                            iif(isnull(different_ship,0)=0,coalesce(nullif(city1,''),nullif(city2,'')),city2) city2,
                                            iif(isnull(different_ship,0)=0,coalesce(nullif(state1,''),nullif(state2,'')),state2)state2,
                                            iif(isnull(different_ship,0)=0,coalesce(nullif(zip1,''),nullif(zip2,'')),zip2)zip2,
                                            iif(isnull(different_ship,0)=0,coalesce(nullif(country,''),nullif(country2,'')),country2)country2 from customer 
                                            WHERE ACC = @ACC", "@ACC", acc);
        }

        public DataTable partshistByJobBag(string JobBagNo)
        {
            return _helperCommonService.GetpartshistByJobBag(JobBagNo);
        }

        public DataTable GetInvoiceItems(string invno, bool includeinvoiceno = false, bool is_return = false, bool iSVatInclude = false)
        {
            DataTable dataTable = new DataTable();

            using (SqlDataAdapter dataAdapter = new SqlDataAdapter())
            {

                dataAdapter.SelectCommand = new SqlCommand();
                dataAdapter.SelectCommand.Connection = _connectionProvider.GetConnection();
                dataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                dataAdapter.SelectCommand.CommandText = "GetInvoiceItems";


                dataAdapter.SelectCommand.Parameters.AddWithValue("@INV_NO", invno);
                dataAdapter.SelectCommand.Parameters.AddWithValue("@INCLUDEINV_NO", includeinvoiceno ? 1 : 0);
                dataAdapter.SelectCommand.Parameters.AddWithValue("@IS_RETURN", is_return ? 1 : 0);
                dataAdapter.SelectCommand.Parameters.AddWithValue("@iSVatInclude", iSVatInclude ? 1 : 0);

                dataAdapter.Fill(dataTable);

                return dataTable;
            }
        }
        public DataTable GetInvoicePayments(string inv_no, bool showlayaway = true, bool is_return = false, bool iSFromReturn = false)
        {
            DataTable dataTable = new DataTable();
            using (SqlDataAdapter dataAdapter = new SqlDataAdapter())
            {

                dataAdapter.SelectCommand = new SqlCommand();
                dataAdapter.SelectCommand.Connection = _connectionProvider.GetConnection();

                dataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                dataAdapter.SelectCommand.CommandText = "GetInvoicePayments";

                dataAdapter.SelectCommand.Parameters.AddWithValue("@inv_no", inv_no);
                dataAdapter.SelectCommand.Parameters.AddWithValue("@showlayaway", showlayaway);
                dataAdapter.SelectCommand.Parameters.AddWithValue("@is_return", is_return);
                dataAdapter.SelectCommand.Parameters.AddWithValue("@iSFromReturn", iSFromReturn);
                dataAdapter.Fill(dataTable);
                return dataTable;
            }
        }


        public string DelRepairOrder(string Repair_no, string Operator = "")
        {
            using (SqlCommand dbCommand = new SqlCommand())
            {
                dbCommand.Connection = _connectionProvider.GetConnection();
                dbCommand.CommandType = CommandType.StoredProcedure;
                dbCommand.CommandText = "DelRepOrder";
                dbCommand.Parameters.AddWithValue("@REPAIRORDER_NO", Repair_no.Trim());
                dbCommand.Parameters.AddWithValue("@Operator", Operator);
                SqlParameter outDelRepStatus = new SqlParameter("@RETVAL", SqlDbType.NVarChar, 100);
                outDelRepStatus.Direction = ParameterDirection.Output;
                dbCommand.Parameters.Add(outDelRepStatus);
                dbCommand.Connection.Open();
                var rowsAffected = dbCommand.ExecuteNonQuery();
                dbCommand.Connection.Close();
                string ABC = outDelRepStatus.Value.ToString();
                return outDelRepStatus.Value.ToString();
            }
        }
        public string DeleteOrderInvoiceDataIntoInSpItTable(string invoicenumber, string repairno = "")
        {
            using (SqlCommand dbCommand = new SqlCommand())
            {
                dbCommand.Connection = _connectionProvider.GetConnection();
                dbCommand.CommandType = CommandType.StoredProcedure;
                dbCommand.CommandText = "DelFromInSpIt";
                dbCommand.Parameters.AddWithValue("@INV_NO", invoicenumber);
                dbCommand.Parameters.AddWithValue("@REPAIRNO", repairno);
                SqlParameter outDelRepStatus = new SqlParameter("@RETVAL", SqlDbType.NVarChar, 100);
                outDelRepStatus.Direction = ParameterDirection.Output;
                dbCommand.Parameters.Add(outDelRepStatus);
                dbCommand.CommandTimeout = 3000;

                dbCommand.Connection.Open();
                var rowsAffected = dbCommand.ExecuteNonQuery();
                dbCommand.Connection.Close();
                string ABC = outDelRepStatus.Value.ToString();
                return outDelRepStatus.Value.ToString();
            }
        }
        public bool DeleteInvoice(string inv_no, bool is_sfm, bool is_ret_inv_no, string loggeduser, string store_no, out string error)
        {
            error = string.Empty;
            try
            {
                using (SqlCommand dbCommand1 = new SqlCommand())
                {
                    dbCommand1.Connection = _connectionProvider.GetConnection();
                    dbCommand1.CommandType = CommandType.StoredProcedure;
                    dbCommand1.CommandText = "UPDATE_INVOICE";
                    dbCommand1.Parameters.AddWithValue("@INV_NO", inv_no);
                    dbCommand1.Parameters.AddWithValue("@iSDelete", true);
                    dbCommand1.CommandTimeout = 0;
                    dbCommand1.Connection.Open();
                    var rowAffected = dbCommand1.ExecuteNonQuery();
                    dbCommand1.Connection.Close();
                }

                using (SqlCommand dbCommand = new SqlCommand())
                {

                    dbCommand.Connection = _connectionProvider.GetConnection();
                    dbCommand.CommandType = CommandType.StoredProcedure;
                    dbCommand.CommandText = "DeleteInvoice";
                    dbCommand.CommandTimeout = 3000;


                    dbCommand.Parameters.AddWithValue("@inv_no", inv_no);
                    dbCommand.Parameters.AddWithValue("@is_sfm", is_sfm ? 1 : 0);
                    dbCommand.Parameters.AddWithValue("@is_ret_inv_no", is_ret_inv_no ? 1 : 0);
                    dbCommand.Parameters.AddWithValue("@LOGGEDUSER", loggeduser);
                    dbCommand.Parameters.AddWithValue("@STORE_NO", store_no);

                    dbCommand.Connection.Open();
                    var rowsAffected = dbCommand.ExecuteNonQuery();
                    dbCommand.Connection.Close();

                    return rowsAffected > 0;
                }
            }
            catch (Exception ex)
            {
                error = ex.Message.Split(new char[] { '\r', '\n' })[0];
                return false;
            }
        }

        private void GetPPAuthDetails()
        {
            DataTable dtupsins1 = _helperCommonService.GetUpsIns1Details();
            string cUserName = _helperCommonService.CheckForDBNullUPS(dtupsins1, "PPAccount");
            string cPasswd = _helperCommonService.CheckForDBNullUPS(dtupsins1, "PPPwd");

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls
               | SecurityProtocolType.Tls11
               | SecurityProtocolType.Tls12
               | SecurityProtocolType.Ssl3;


        }

        public DataTable GetJmCareWannatyInvoice(string custcode, bool Isrefund = false)
        {
            return _helperCommonService.GetSqlData("SELECT I.INV_NO,ACC,DATE,STYLE,QTY,PRICE,COST,warranty,warranty_cost,Extendorrefund,STORE_NO,SNH,TRADEINAMT,DEDUCTION,isnull(Sales_Tax,0)Sales_Tax,isnull(Sales_Tax1,0)Sales_Tax1,isnull(Sales_Tax2,0)Sales_Tax2,isnull(Sales_Tax3,0)Sales_Tax3,isnull(Sales_Tax_Rate,0)Sales_Tax_Rate, IT.JM_TRANSACT  FROM INVOICE I with (nolock) , IN_ITEMS IT with (nolock) WHERE I.INV_NO=IT.INV_NO and i.acc= iif(@custcode='',i.acc,@custcode) and IT.jm_cancelid='' " + (!Isrefund ? " AND STYLE NOT LIKE '%EXTEND JMCARE WARRANTY%' " : "") + " AND STYLE NOT LIKE '%RETURN JMCARE%'  " + (Isrefund ? " AND ExtendOrRefund='' " : "") + " AND ISNULL(IT.warranty,'')<>'' AND IT.warranty IN (SELECT DISTINCT CODE FROM JMCARE) ORDER BY DATE ", "@custcode", custcode);
        }
        public bool Extendedjmcarewarrantyinvoice(string invoiceitems, out string error, string PaymentMethod = "", bool Isrefund = false, bool IsWarrantyRefund = false)
        {
            error = string.Empty;
            using (SqlConnection connection = _connectionProvider.GetConnection())
            using (SqlCommand dbCommand = new SqlCommand("SaveExtendedjmcarewarrantyinvoice", connection))
            {
                dbCommand.CommandType = CommandType.StoredProcedure;
                dbCommand.CommandTimeout = 3000;

                // Add parameters
                dbCommand.Parameters.Add(new SqlParameter("@INVOICEITEMS", SqlDbType.Xml) { Value = invoiceitems });
                dbCommand.Parameters.AddWithValue("@operator", _helperCommonService.LoggedUser);
                dbCommand.Parameters.AddWithValue("@storeno", _helperCommonService.StoreCode);
                dbCommand.Parameters.AddWithValue("@Isrefund", Isrefund);
                dbCommand.Parameters.AddWithValue("@PaymentMethod", PaymentMethod);
                dbCommand.Parameters.AddWithValue("@IsWarrantyRefund", IsWarrantyRefund);

                connection.Open();
                int rowsAffected = dbCommand.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }
        public decimal Getsalestax(string style, string storename, decimal shipping, decimal tradeinamount, decimal discount, decimal qty, decimal price, decimal salestaxrate, out decimal salesTax1, out decimal salesTax2, out decimal salesTax3)
        {
            decimal outsalestax = 0;
            decimal totalCount = 0;
            decimal subTotal = 0;

            salesTax1 = 0;
            salesTax2 = 0;
            salesTax3 = 0;

            bool isLuxuryTax = _helperCommonService.CheckModuleEnabled(HelperCommonService.Modules.Luxury_Tax);
            bool noTax = false;
            string type = string.Empty;

            if (!string.IsNullOrWhiteSpace(style))
            {
                DataRow dr = _helperCommonService.CheckStyle(style);
                if (dr != null)
                {
                    noTax = _helperCommonService.CheckForDBNull(dr["NO_TAX"], false, typeof(bool).ToString());
                    type = _helperCommonService.CheckForDBNull(dr["ITEM_TYPE"]);

                    if (!noTax && type.Trim().ToUpper() != "GIFT CARD")
                        totalCount += qty * price;
                }
                else
                {
                    totalCount += qty * price;
                }
            }

            if (isLuxuryTax && price > 0 && !noTax)
            {
                decimal t1, t2, t3 = 0;
                outsalestax += _helperCommonService.getSales_taxValues(
                    storename,
                    price,
                    qty,
                    type,
                    out t1,
                    out t2,
                    out t3);

                salesTax1 += t1;
                salesTax2 += t2;
                salesTax3 += t3;
            }

            subTotal += qty * price;

            if (isLuxuryTax && _helperCommonService.CheckModuleEnabled(HelperCommonService.Modules.snh_taxable))
            {
                decimal sales_tax_1, sales_tax_2 = 0, sales_tax_3 = 0;
                outsalestax += _helperCommonService.getSales_taxValues(
                    storename,
                    shipping,
                    shipping > 0 ? 1 : 0,
                    type,
                    out sales_tax_1, out sales_tax_2, out sales_tax_3);
            }

            if (isLuxuryTax && _helperCommonService.CheckModuleEnabled(HelperCommonService.Modules.TaxAfterTradeIn))
            {
                outsalestax = subTotal != 0
                    ? outsalestax * ((subTotal - tradeinamount) / subTotal)
                    : 0;
            }
            else if (!isLuxuryTax)
            {
                decimal taxableAmount = totalCount - tradeinamount + discount;

                if (_helperCommonService.CheckModuleEnabled(HelperCommonService.Modules.snh_taxable))
                    taxableAmount += shipping;

                outsalestax = taxableAmount * salestaxrate / 100;
            }

            return outsalestax;
        }
        public DataTable GetWarrantyInvoice(string Invoicenum)
        {
            return _helperCommonService.GetSqlData("SELECT I.INV_NO,ACC,DATE,STYLE,QTY,PRICE,COST,warranty,warranty_cost,Extendorrefund,STORE_NO,SNH,TRADEINAMT,DEDUCTION,isnull(Sales_Tax,0)Sales_Tax,isnull(Sales_Tax1,0)Sales_Tax1,isnull(Sales_Tax2,0)Sales_Tax2,isnull(Sales_Tax3,0)Sales_Tax3,isnull(Sales_Tax_Rate,0)Sales_Tax_Rate,IT.JM_TRANSACT  FROM INVOICE I with (nolock) , IN_ITEMS IT with (nolock) WHERE I.INV_NO=IT.INV_NO and trim(i.inv_no)=trim(@Invoicenum) AND STYLE NOT LIKE '%RETURN WARRANTY%' AND IT.warranty IN (SELECT DISTINCT CODE FROM WARRANTIES with (nolock)) ORDER BY DATE ", "@Invoicenum", Invoicenum);
        }
        public bool iSWarrantyInvoice(string invNo)
        {
            return _helperCommonService.DataTableOK(_helperCommonService.GetSqlData($"select 1 from IN_ITEMS with (nolock) where INV_NO=@InvNo and warranty<>''", "@InvNo", invNo));
        }
        public DataTable InvoiceWarrantyStyles(string invno)
        {
            return _helperCommonService.GetSqlData(@"Select IIT.Style,IIT.Warranty,IIT.Warranty_Cost,IT.Date from in_items IIT  with (nolock) join INVOICE IT with (nolock) ON IT.INV_NO = IIT.INV_NO  where IT.inv_no =@invno", "@invno", invno);
        }





    }
}
