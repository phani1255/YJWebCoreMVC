using HtmlAgilityPack;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using YJWebCoreMVC.Models;

namespace YJWebCoreMVC.Services
{
    public class HelperPhanindraService
    {

        private readonly ConnectionProvider _connectionProvider;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly HelperCommonService _helperCommonService;
        private readonly IWebHostEnvironment _env;
        private readonly string _companyName;
        private readonly string _storeCodeInUse;
        private readonly string _loggedUser;

        public HelperPhanindraService(ConnectionProvider connectionProvider, IHttpContextAccessor httpContextAccessor, HelperCommonService helperCommonService, IWebHostEnvironment env)
        {
            _connectionProvider = connectionProvider;
            _httpContextAccessor = httpContextAccessor;
            _helperCommonService = helperCommonService;
            _companyName = _httpContextAccessor.HttpContext?.Session.GetString("COMPANYNAME");
            _storeCodeInUse = _httpContextAccessor.HttpContext?.Session.GetString("STORE_CODE");
            _loggedUser = _httpContextAccessor.HttpContext?.Session.GetString("UserId");
            _env = env;
        }


        public decimal GetMetalPrice(DateTime date, string metal)
        {
            using (var connection = _connectionProvider.GetConnection())
            using (var command = new SqlCommand("GetMetalPrice", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@invdate", date.ToString("yyyyMMdd"));
                command.Parameters.AddWithValue("@metal", metal);

                connection.Open();
                var result = command.ExecuteScalar();
                connection.Close();

                return result != DBNull.Value ? Convert.ToDecimal(result) : 0;
            }
        }

        public DataTable getScrapGoldDtls()
        {
            return _helperCommonService.GetSqlData("SELECT GP_PERCENT,KARAT_REDUCTION,PAYOUT_PERCENT,WT_REDUCTION,SCRAP_DISCLAIMER From UPS_INS with (nolock)");
        }

        public string GetStoresScrapBank(string loggedStore = "")
        {
            string scrapBankValue = string.Empty;
            string query = string.IsNullOrWhiteSpace(loggedStore) ?
                "SELECT scrap_bank FROM STORES  with(nolock)" : "SELECT scrap_bank FROM STORES with(nolock) WHERE CODE = @STORE";

            using (SqlConnection conn = _connectionProvider.GetConnection())
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.CommandType = CommandType.Text;

                if (!string.IsNullOrWhiteSpace(loggedStore))
                    cmd.Parameters.Add("@STORE", SqlDbType.VarChar, 50).Value = loggedStore.Trim();

                conn.Open();
                object result = cmd.ExecuteScalar();
                scrapBankValue = result != null ? result.ToString() : string.Empty;
            }
            return scrapBankValue;
        }

        public bool AddEditScrapGold(ScrapGoldModel invoice, string invoiceItems, out string out_inv_no, bool is_update = false)
        {
            using (SqlCommand dbCommand = new SqlCommand())
            {
                // Set the command object properties
                dbCommand.Connection = _connectionProvider.GetConnection();
                dbCommand.CommandType = CommandType.StoredProcedure;
                dbCommand.CommandText = "AddEditScrap";
                dbCommand.CommandTimeout = 5000;
                Object invoicedate;
                if (invoice.DATE == null)
                    invoicedate = DBNull.Value;
                else
                    invoicedate = invoice.DATE;

                dbCommand.Parameters.AddWithValue("@INV_NO", invoice.INV_NO);
                dbCommand.Parameters.AddWithValue("@BACC", invoice.BACC);
                dbCommand.Parameters.AddWithValue("@ACC", invoice.ACC);
                dbCommand.Parameters.AddWithValue("@DATE", invoicedate);
                dbCommand.Parameters.AddWithValue("@GR_TOTAL", invoice.GR_TOTAL);
                dbCommand.Parameters.AddWithValue("@NAME", invoice.NAME);
                dbCommand.Parameters.AddWithValue("@ADDR1", invoice.ADDR1);
                dbCommand.Parameters.AddWithValue("@ADDR2", invoice.ADDR2);
                dbCommand.Parameters.AddWithValue("@ADDR3", invoice.ADDR3);
                dbCommand.Parameters.AddWithValue("@CITY", invoice.CITY);
                dbCommand.Parameters.AddWithValue("@STATE", invoice.STATE);
                dbCommand.Parameters.AddWithValue("@ZIP", invoice.ZIP);
                dbCommand.Parameters.AddWithValue("@COUNTRY", invoice.COUNTRY);
                dbCommand.Parameters.AddWithValue("@STORE_NO", invoice.STORE_NO);
                dbCommand.Parameters.AddWithValue("@OPERATOR", invoice.OPERATOR);
                dbCommand.Parameters.AddWithValue("@MESSAGE", invoice.MESSAGE);
                dbCommand.Parameters.AddWithValue("@MESSAGE1", invoice.MESSAGE1);
                dbCommand.Parameters.AddWithValue("@MESSAGE2", invoice.MESSAGE2);
                dbCommand.Parameters.AddWithValue("@CASH", invoice.CASH);
                dbCommand.Parameters.AddWithValue("@STORECREDIT", invoice.STORECREDIT);
                dbCommand.Parameters.AddWithValue("@CHECK", invoice.CHECK);
                dbCommand.Parameters.AddWithValue("@CHECK_NO", invoice.CHECK_NO);
                dbCommand.Parameters.AddWithValue("@ADD_COST", invoice.ADD_COST);
                dbCommand.Parameters.AddWithValue("@GOLDPRICE", invoice.GOLDPRICE);
                dbCommand.Parameters.AddWithValue("@SLVRPRICE", invoice.SILVERPRICE);
                dbCommand.Parameters.AddWithValue("@PLATPRICE", invoice.PLATPRICE);
                dbCommand.Parameters.AddWithValue("@PALADPRICE", invoice.PALLADPRICE);
                dbCommand.Parameters.AddWithValue("@IS_UPDATE", is_update ? 1 : 0);
                dbCommand.Parameters.AddWithValue("@DWT_GR", invoice.DWT_GR);
                dbCommand.Parameters.AddWithValue("@BANK", invoice.BANK);
                dbCommand.Parameters.AddWithValue("@SALESMAN", invoice.SALESMAN1);
                dbCommand.Parameters.AddWithValue("@CASH_REGISTER", invoice.CASH_REGISTER);
                dbCommand.Parameters.AddWithValue("@Email", invoice.Email);
                dbCommand.Parameters.AddWithValue("@IsUsetradein", invoice.IsUsetradein);
                dbCommand.Parameters.AddWithValue("@Agreeduponprice", invoice.Agreeduponprice);
                dbCommand.Parameters.AddWithValue("@iSAddedFromInvoice", invoice.iSAddedFromInvoice);
                dbCommand.Parameters.AddWithValue("@invoiceno", invoice.invoiceno);

                dbCommand.Parameters.Add("@OUT_INV_NO", SqlDbType.VarChar, 6).Direction = ParameterDirection.Output;
                dbCommand.Parameters.Add(new SqlParameter("@TBLPOINVOICEITEMS", SqlDbType.Xml)
                {
                    Value = invoiceItems
                });

                // Open the connection, execute the query and close the connection
                dbCommand.Connection.Open();
                var rowsAffected = dbCommand.ExecuteNonQuery();
                out_inv_no = Convert.ToString(dbCommand.Parameters["@OUT_INV_NO"].Value);

                dbCommand.Connection.Close();

                return rowsAffected > 0;
            }
        }

        public DataTable GetScrapGoldItems(string invno, bool includeinvoiceno = false, bool is_return = false)
        {
            var dataTable = new DataTable();
            using (var connection = _connectionProvider.GetConnection())
            using (var command = new SqlCommand("GetScrapItems", connection))
            using (var dataAdapter = new SqlDataAdapter(command))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@INV_NO", SqlDbType.VarChar, 50) { Value = invno });
                command.Parameters.Add(new SqlParameter("@INCLUDEINV_NO", SqlDbType.Bit) { Value = includeinvoiceno });
                command.Parameters.Add(new SqlParameter("@IS_RETURN", SqlDbType.Bit) { Value = is_return });

                dataAdapter.Fill(dataTable);
            }
            return dataTable;
        }

        public DataTable CheckValidStoreCreditorGiftCard(string scgcNumber, bool isGC)
        {
            if (!isGC)
                return _helperCommonService.GetSqlData("SELECT * FROM storecreditvoucher WHERE TRIM(creditno) = TRIM(@scgcNumber) and isnull(isGiftCert,0)=0", "@scgcNumber", scgcNumber);
            return _helperCommonService.GetSqlData("SELECT * FROM storecreditvoucher WHERE TRIM(UserGCNo) = TRIM(@scgcNumber) and isnull(isGiftCert,0)=1", "@scgcNumber", scgcNumber);
        }

        public bool IssueCheckWOBillOrVendor(
            string acc,
            string name,
            string chkNo,
            DateTime date,
            string bank,
            decimal amount,
            string purchaseNo,
            string store,
            string loggedUser,
            out string invNo,
            out string error)
        {
            invNo = string.Empty;
            error = string.Empty;

            try
            {
                using (SqlConnection connection = _connectionProvider.GetConnection())
                using (SqlCommand command = new SqlCommand("IssueACheckFromScrapGold", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Add parameters with explicit types
                    command.Parameters.Add(new SqlParameter("@ACC", SqlDbType.NVarChar) { Value = acc });
                    command.Parameters.Add(new SqlParameter("@NAME", SqlDbType.NVarChar) { Value = name });
                    command.Parameters.Add(new SqlParameter("@CHK_NO", SqlDbType.NVarChar) { Value = chkNo });
                    command.Parameters.Add(new SqlParameter("@DATE", SqlDbType.DateTime) { Value = date });
                    command.Parameters.Add(new SqlParameter("@BANK", SqlDbType.NVarChar) { Value = bank });
                    command.Parameters.Add(new SqlParameter("@AMOUNT", SqlDbType.Decimal) { Value = amount });
                    command.Parameters.Add(new SqlParameter("@PURCHASE_NUM", SqlDbType.NVarChar) { Value = purchaseNo });
                    command.Parameters.Add(new SqlParameter("@STORENO", SqlDbType.NVarChar) { Value = store });
                    command.Parameters.Add(new SqlParameter("@loggeduser", SqlDbType.NVarChar) { Value = loggedUser });

                    // Output parameter
                    SqlParameter outInvNo = new SqlParameter("@RETVAL", SqlDbType.NVarChar, 10)
                    {
                        Direction = ParameterDirection.Output
                    };
                    command.Parameters.Add(outInvNo);

                    // Open the connection, execute the command, and retrieve the output parameter
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    invNo = Convert.ToString(outInvNo.Value);

                    return rowsAffected > 0;
                }
            }
            catch (Exception ex)
            {
                error = ex.Message;
                invNo = string.Empty;
                return false;
            }
        }

        public string isOkToRetriveWebGoldPrice(bool iSBasedOnGoldOnly = false, string RetriveWebGoldPrice = "", string RetriveSilverPrice = "")
        {
            decimal GldPrc = _helperCommonService.DecimalCheckForNullDBNull(GetMetalPriceRowWise("GOLD", DateTime.Now));
            decimal SilverPrc = _helperCommonService.DecimalCheckForNullDBNull(GetMetalPriceRowWise("SILVER", DateTime.Now));
            decimal platinumPrc = _helperCommonService.DecimalCheckForNullDBNull(GetMetalPriceRowWise("PLATINUM", DateTime.Now));
            decimal PalladiumPrc = _helperCommonService.DecimalCheckForNullDBNull(GetMetalPriceRowWise("PALLADIUM", DateTime.Now));

            bool updateSilver = true;
            string strMsg1 = "";
            string strMsg2 = "";
            // There is an unknown bug that makes silver price same as gold, below if they are the same  we will retrieve.
            if ((GldPrc == 0 || SilverPrc == 0 || platinumPrc == 0 || PalladiumPrc == 0 || GldPrc <= SilverPrc * 2) &&
                !_companyName.ToUpper().Contains("SHIVA") && !_helperCommonService.iS_Sree)
            {
                if (RetriveWebGoldPrice == "Yes")
                {
                    decimal[] GSPrice = todayGoldPrice();
                    GldPrc = GSPrice != null ? _helperCommonService.DecimalCheckForDBNull(GSPrice[0]) : 0;
                    SilverPrc = GSPrice != null ? _helperCommonService.DecimalCheckForDBNull(GSPrice[1]) : 0;
                    platinumPrc = GSPrice != null ? _helperCommonService.DecimalCheckForDBNull(GSPrice[2]) : 0;
                    PalladiumPrc = GSPrice != null ? _helperCommonService.DecimalCheckForDBNull(GSPrice[3]) : 0;

                    string strMessage = $"Gold Price: {Convert.ToString(GldPrc)} / Silver Price: {Convert.ToString(SilverPrc)} / Platinum Price: {Convert.ToString(platinumPrc)} / Palladium Price: {Convert.ToString(PalladiumPrc)} Retrieved from the web.";
                    if (SilverPrc == 0)
                    {
                        updateSilverPrice((SilverPrc == 0) ? 2 : (SilverPrc == 0 ? 0 : 1));
                        strMessage += "\nAnd updated Silver prices from last update";
                    }
                    //MsgBox(strMessage);
                    strMsg1 = strMessage;
                    if (SilverPrc == 0)
                    {
                        //IF Silver or Silver_S Price was 0, so after updated and combined message..exit from here
                        //return "";
                        updateSilver = false;
                    }
                }
            }

            if (!iSBasedOnGoldOnly && SilverPrc == 0 && RetriveSilverPrice == "Yes" && updateSilver)
            {
                updateSilverPrice((SilverPrc == 0) ? 2 : (SilverPrc == 0 ? 0 : 1));
                strMsg2 = "Silver/SS Price updated.";
            }
            return strMsg1 + (string.IsNullOrEmpty(strMsg1) || string.IsNullOrEmpty(strMsg2) ? "" : "\n") + strMsg2;
        }

        public decimal GetMetalPriceRowWise(string metal, DateTime invDate)
        {
            const string query = @"SELECT TOP 1 ISNULL(Price, 0) AS Price FROM Gold_Prc with (nolock)
                WHERE metal = @Metal AND CAST([date] AS date) = @InvDate ORDER BY [date] DESC";

            using (var connection = _connectionProvider.GetConnection())
            using (var command = new SqlCommand(query, connection))
            {
                command.CommandType = CommandType.Text;

                // Add parameters with explicit types
                command.Parameters.Add(new SqlParameter("@Metal", SqlDbType.VarChar) { Value = metal });
                command.Parameters.Add(new SqlParameter("@InvDate", SqlDbType.Date) { Value = invDate });

                connection.Open();
                var result = command.ExecuteScalar();

                decimal price;
                return result != null && decimal.TryParse(result.ToString(), out price) ? price : 0m;
            }
        }

        public decimal[] todayGoldPrice()
        {
            decimal[] metalPrices = new decimal[] { 0, 0, 0, 0 };
            const string KitcoUrl = "https://www.kitco.com/price/precious-metals";
            const string GoldSilverUrl = "https://goldsilver.com/price-charts/";

            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            try
            {
                HtmlWeb web = new HtmlWeb();
                HtmlAgilityPack.HtmlDocument doc = web.Load(KitcoUrl);
                var priceNodes = doc.DocumentNode.SelectNodes("//ul");

                if (priceNodes != null && priceNodes.Count > 3)
                {
                    var metalHtml = priceNodes[3].InnerHtml;
                    metalPrices[0] = ParseMetalPrice(metalHtml, ">Gold</a></span><span>", 7);       // Gold
                    metalPrices[1] = ParseMetalPrice(metalHtml, ">Silver</a></span><span>", 5);     // Silver
                    metalPrices[2] = ParseMetalPrice(metalHtml, ">Platinum</a></span><span>", 5);   // Platinum
                    metalPrices[3] = ParseMetalPrice(metalHtml, ">Palladium</a></span><span>", 5);  // Palladium
                }

                // Fall back to alternative URL if any price is not retrieved
                if (metalPrices.Contains(0))
                {
                    HtmlAgilityPack.HtmlDocument docGoldSilver = web.Load(GoldSilverUrl);
                    var tdNodes = docGoldSilver.DocumentNode.SelectNodes("//td");

                    foreach (var tdNode in tdNodes)
                    {
                        string metalType = tdNode.InnerText.Trim().ToUpper();
                        decimal price = ParsePriceFromHtml(tdNode.InnerHtml);

                        switch (metalType)
                        {
                            case "GOLD":
                                metalPrices[0] = metalPrices[0] == 0 ? price : metalPrices[0];
                                break;
                            case "SILVER":
                                metalPrices[1] = metalPrices[1] == 0 ? price : metalPrices[1];
                                break;
                            case "PLATINUM":
                                metalPrices[2] = metalPrices[2] == 0 ? price : metalPrices[2];
                                break;
                            case "PALLADIUM":
                                metalPrices[3] = metalPrices[3] == 0 ? price : metalPrices[3];
                                break;
                        }
                    }
                }

                // Build message for missing prices
                var missingPrices = new StringBuilder();
                if (metalPrices[0] == 0) missingPrices.AppendLine("Gold Price");
                if (metalPrices[1] == 0) missingPrices.AppendLine("Silver Price");
                if (metalPrices[2] == 0) missingPrices.AppendLine("Platinum Price");
                if (metalPrices[3] == 0) missingPrices.AppendLine("Palladium Price");

                if (missingPrices.Length > 0)
                {
                    //MsgBox(GetLang($"{missingPrices} not Updated. Please contact Ishal Inc."));
                    return metalPrices;
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error retrieving metal prices", ex);
            }

            // Update each metal price in the database
            UpdateGoldSilverPrice(metalPrices[0], "GOLD");
            UpdateGoldSilverPrice(metalPrices[1], "SILVER");
            UpdateGoldSilverPrice(metalPrices[2], "PLATINUM");
            UpdateGoldSilverPrice(metalPrices[3], "PALLADIUM");

            return metalPrices;
        }

        public void updateSilverPrice(int which = 2) //which=0 for Silver Only,1 for SS only, 2 for both
        {
            string qrySilver = "", qrySS = "";
            if (which == 0 || which == 2)
                qrySilver = string.Format("BEGIN DECLARE @currPrice1 DECIMAL(18, 2)=20; SELECT @currPrice1 = ISNULL(price, 0) FROM gold_prc with(nolock) WHERE metal = '{0}' AND CAST(DATE AS DATE)= (SELECT MAX(CAST(DATE AS DATE)) FROM gold_prc with(nolock) WHERE metal = '{0}'); INSERT INTO gold_prc(DATE, PRICE, metal) VALUES(GETDATE(), @currPrice1, '{0}'); DELETE FROM gold_prc WHERE CAST([DATE] AS DATE) = CAST(GETDATE() AS DATE) AND metal='{0}' AND PRICE IS NULL; END; ", "SILVER");
            if (which == 1 || which == 2)
                qrySS = string.Format("BEGIN DECLARE @currPrice2 DECIMAL(18, 2)=20; SELECT @currPrice2 = ISNULL(price, 0) FROM gold_prc with(nolock) WHERE metal = '{0}' AND CAST(DATE AS DATE)= (SELECT MAX(CAST(DATE AS DATE)) FROM gold_prc with(nolock) WHERE metal = '{0}'); INSERT INTO gold_prc(DATE, PRICE, metal) VALUES(GETDATE(), @currPrice2, '{0}'); DELETE FROM gold_prc WHERE CAST([DATE] AS DATE) = CAST(GETDATE() AS DATE) AND metal='{0}' AND PRICE IS NULL; END; ", "SILVER_S");
            _helperCommonService.GetSqlData(qrySilver + qrySS);
        }

        // Helper methods for extracting and parsing metal prices
        private decimal ParseMetalPrice(string html, string marker, int length)
        {
            int startIndex = html.IndexOf(marker) + marker.Length;
            if (startIndex >= marker.Length)
            {
                string priceText = html.Substring(startIndex, length).Trim();
                return _helperCommonService.DecimalCheckForDBNull(priceText);
            }
            return 0;
        }

        private decimal ParsePriceFromHtml(string html)
        {
            string cleanedText = Regex.Replace(html, @"[^\d.,]", "").Trim();
            return _helperCommonService.DecimalCheckForDBNull(cleanedText);
        }

        public void UpdateGoldSilverPrice(decimal gPrice, string metal)
        {
            string query;
            var dateParameter = _helperCommonService.setSQLDateTime(DateTime.Now).Date;

            // Use "using" statements to ensure resources are released.
            using (var connection = _connectionProvider.GetConnection())
            using (var command = connection.CreateCommand())
            {
                // Retrieve existing price for the date and metal
                command.CommandText = "SELECT TOP 1 PRICE FROM GOLD_PRC  with (nolock) WHERE CAST([DATE] AS DATE) = @DATE AND METAL = @Metal";
                command.Parameters.Add(new SqlParameter("@Metal", SqlDbType.NVarChar) { Value = metal });
                command.Parameters.Add(new SqlParameter("@DATE", SqlDbType.DateTime) { Value = dateParameter });

                connection.Open();
                var result = command.ExecuteScalar();

                // Choose the query based on whether a price already exists for today
                if (result == null)
                    query = "INSERT INTO GOLD_PRC ([DATE], PRICE, METAL) VALUES (@DATE, @GOLDPRICE, @Metal)";
                else
                    query = "UPDATE GOLD_PRC SET PRICE = @GOLDPRICE WHERE CAST([DATE] AS DATE) = @DATE AND METAL = @Metal";

                // Reuse the command for the final execution
                command.CommandText = query;
                command.Parameters.Clear();
                command.Parameters.Add(new SqlParameter("@GOLDPRICE", SqlDbType.Decimal) { Value = gPrice });
                command.Parameters.Add(new SqlParameter("@Metal", SqlDbType.NVarChar) { Value = metal });
                command.Parameters.Add(new SqlParameter("@DATE", SqlDbType.DateTime) { Value = dateParameter });
                command.ExecuteNonQuery();

                _helperCommonService.AddKeepRec(metal + " Price Updated to " + gPrice);
            }
        }

        public DataTable GetCashoutItems(string invno, bool isreprint = false)
        {
            using (var connection = _connectionProvider.GetConnection())
            using (var command = new SqlCommand(@"
        SELECT b.INV_NO, STYLE, [DESC], QTY, PRICE, QTY * PRICE AS TOTAL, invoiceno, bank" +
                (isreprint ? ", Acc" : "") +
                @" FROM SCRAP A with (nolock)
          INNER JOIN SCRAP_ITEMS B  with (nolock) ON A.INV_NO = B.INV_NO 
          WHERE ISCASHOUT = 1 AND B.INV_NO = @INV_NO", connection))
            {
                command.CommandType = CommandType.Text;
                command.Parameters.AddWithValue("@INV_NO", invno);

                using (var adapter = new SqlDataAdapter(command))
                {
                    var dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    return dataTable;
                }
            }
        }

        public DataRow CheckStyleCashOut(string style)
        {
            if (String.IsNullOrWhiteSpace(style))
                return null;
            DataRow results;

            style = style.Trim();

            int underscoreIndex = style.IndexOf('_');
            if (underscoreIndex >= 0)
                style = style.Substring(0, underscoreIndex);

            if (_helperCommonService.is_StyleItem)
            {
                results = _helperCommonService.GetSqlRow("select style from StkNos with (nolock) where stk_no = @style", "@style", style);
                if (_helperCommonService.DataTableOK(results))
                {
                    results = _helperCommonService.GetSqlRow("select * from styles with (nolock) where style = @style", "@style",
                        Convert.ToString(results["style"]));
                }
                if (_helperCommonService.DataTableOK(results))
                    return results;
            }
            string nfld = _helperCommonService.ByFieldValue5 ? "fieldvalue5" : (_helperCommonService.ByFieldValue6 ? "fieldvalue6" : "oldbarcode");
            string qry = "select * from styles s with (nolock) left join styles2 s2 with (nolock) on s.style = s2.style where s.style = @style or s.barcode = @style or ";
            results = _helperCommonService.GetSqlRow(qry + nfld + " = @style", "@style", style);
            if (_helperCommonService.DataTableOK(results))
                return results;
            if ((_helperCommonService.is_Mahin || _helperCommonService.is_Singer) && style.Length > 5)
                results = _helperCommonService.GetSqlRow(@"select * from styles s with (nolock) left join styles2 s2 with (nolock) on s.style = s2.style where s.style = @style", "@style",
                    _helperCommonService.Left(style, 3) + "-" + style.Substring(3));     // barcode read 123456789 we want 123-456789
            return results;
        }


        public bool AddEditCashout(ScrapGoldModel invoice, string invoiceItems, out string out_inv_no, bool is_update = false, bool isCashout = false)
        {
            using (SqlCommand dbCommand = new SqlCommand())
            {
                // Set the command object properties
                dbCommand.Connection = _connectionProvider.GetConnection();
                dbCommand.CommandType = CommandType.StoredProcedure;
                dbCommand.CommandText = "AddEditCashout";
                dbCommand.CommandTimeout = 5000;
                Object invoicedate;
                if (invoice.DATE == null)
                    invoicedate = DBNull.Value;
                else
                    invoicedate = invoice.DATE;

                dbCommand.Parameters.AddWithValue("@INV_NO", invoice.INV_NO);
                dbCommand.Parameters.AddWithValue("@BACC", invoice.BACC);
                dbCommand.Parameters.AddWithValue("@ACC", invoice.ACC);
                dbCommand.Parameters.AddWithValue("@DATE", invoicedate);
                dbCommand.Parameters.AddWithValue("@GR_TOTAL", invoice.GR_TOTAL);
                dbCommand.Parameters.AddWithValue("@NAME", invoice.NAME);
                dbCommand.Parameters.AddWithValue("@ADDR1", invoice.ADDR1);
                dbCommand.Parameters.AddWithValue("@ADDR2", invoice.ADDR2);
                dbCommand.Parameters.AddWithValue("@ADDR3", invoice.ADDR3);
                dbCommand.Parameters.AddWithValue("@CITY", invoice.CITY);
                dbCommand.Parameters.AddWithValue("@STATE", invoice.STATE);
                dbCommand.Parameters.AddWithValue("@ZIP", invoice.ZIP);
                dbCommand.Parameters.AddWithValue("@COUNTRY", invoice.COUNTRY);
                dbCommand.Parameters.AddWithValue("@STORE_NO", invoice.STORE_NO);
                dbCommand.Parameters.AddWithValue("@OPERATOR", invoice.OPERATOR);
                dbCommand.Parameters.AddWithValue("@MESSAGE", invoice.MESSAGE);
                dbCommand.Parameters.AddWithValue("@MESSAGE1", invoice.MESSAGE1);
                dbCommand.Parameters.AddWithValue("@MESSAGE2", invoice.MESSAGE2);
                dbCommand.Parameters.AddWithValue("@CASH", invoice.CASH);
                dbCommand.Parameters.AddWithValue("@CHECK", invoice.CHECK);
                dbCommand.Parameters.AddWithValue("@CHECK_NO", invoice.CHECK_NO);
                dbCommand.Parameters.AddWithValue("@ADD_COST", invoice.ADD_COST);
                dbCommand.Parameters.AddWithValue("@GOLDPRICE", invoice.GOLDPRICE);
                dbCommand.Parameters.AddWithValue("@SLVRPRICE", invoice.SILVERPRICE);
                dbCommand.Parameters.AddWithValue("@PLATPRICE", invoice.PLATPRICE);
                dbCommand.Parameters.AddWithValue("@PALADPRICE", invoice.PALLADPRICE);
                dbCommand.Parameters.AddWithValue("@IS_UPDATE", is_update == true ? 1 : 0);
                dbCommand.Parameters.AddWithValue("@DWT_GR", invoice.DWT_GR);
                dbCommand.Parameters.AddWithValue("@BANK", invoice.BANK);
                dbCommand.Parameters.AddWithValue("@SALESMAN", invoice.SALESMAN1);
                dbCommand.Parameters.AddWithValue("@CASH_REGISTER", invoice.CASH_REGISTER);
                dbCommand.Parameters.AddWithValue("@Email", invoice.Email);
                dbCommand.Parameters.AddWithValue("@IsUsetradein", invoice.IsUsetradein);
                dbCommand.Parameters.AddWithValue("@Agreeduponprice", invoice.Agreeduponprice);

                dbCommand.Parameters.AddWithValue("@isCashout", isCashout);


                dbCommand.Parameters.Add("@OUT_INV_NO", SqlDbType.VarChar, 6).Direction = ParameterDirection.Output;

                dbCommand.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@TBLPOINVOICEITEMS",
                    SqlDbType = SqlDbType.Xml,
                    Value = invoiceItems
                });

                // Open the connection, execute the query and close the connection
                dbCommand.Connection.Open();
                var rowsAffected = dbCommand.ExecuteNonQuery();
                out_inv_no = Convert.ToString(dbCommand.Parameters["@OUT_INV_NO"].Value);

                dbCommand.Connection.Close();

                return rowsAffected > 0;
            }
        }

        public DataTable CheckValidCheckNo(string check_no, string bank)
        {
            string CommandText = "select CHECKS.*, CAST(BANK.CLRD AS BIT) as CLRD1 from checks with (nolock) LEFT JOIN Bank with (nolock) ON TRIM(Bank.inv_no)=TRIM(CHECKS.TRANSACT) WHERE TRIM(checks.bank)= @bank AND TRIM(CHECKS.check_no) = @chk_no";
            return _helperCommonService.GetSqlData(CommandText, "@chk_no", check_no.Trim(), "@bank", bank.Trim());
        }

        public DataRow GetGoldSentLog(string logno)
        {
            return _helperCommonService.GetSqlRow(@"SELECT * FROM GOLDSENT WHERE INV_NO=@logno", "@logno", logno);
        }

        public DataTable GetGoldItemsByLog(string logno)
        {
            return _helperCommonService.GetStoreProc("GetGoldItems", "@logno", logno);
        }

        public DataTable GetMetalKarats(string invoiceItems)
        {
            return _helperCommonService.GetStoreProc("GetMetalKarat", "@TBLPOINVOICEITEMS", invoiceItems);
        }

        public DataTable GetSpecialOrderItemsProcess(DateTime? FromDt, DateTime? ToDt, bool IsAll, string Stores)
        {
            DataTable dataTable = new DataTable();
            using (SqlConnection connection = _connectionProvider.GetConnection())
            using (SqlCommand command = new SqlCommand("GetSpecialOrderItemsProcess", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandTimeout = 6000;

                // Add parameters with explicit type definitions
                command.Parameters.Add("@FromDt", SqlDbType.DateTime).Value = (object)FromDt ?? DBNull.Value;
                command.Parameters.Add("@ToDt", SqlDbType.DateTime).Value = (object)ToDt ?? DBNull.Value;
                command.Parameters.Add("@IsAll", SqlDbType.Bit).Value = IsAll;
                command.Parameters.Add("@Stores", SqlDbType.VarChar).Value = string.IsNullOrWhiteSpace(Stores) ? (object)DBNull.Value : Stores.Trim();

                using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                {
                    connection.Open();
                    adapter.Fill(dataTable);
                }
            }
            return dataTable;
        }

        public DataTable GetSpecialOrderDetails(string strInvoiceNo, string sStyle = "")
        {
            try
            {
                var query = @" SELECT DISTINCT I.INV_NO, I.DATE, C.ACC AS C_ACC, C.NAME AS C_NAME, C.ADDR1 AS C_ADDR1, C.ADDR12 AS C_ADDR12, 
                C.CITY1 AS C_CITY1, C.STATE1 AS C_STATE1, C.ZIP1 AS C_ZIP1, C.TEL AS C_TEL, V.ACC AS V_ACC, V.NAME AS V_NAME, V.ADDR11 AS V_ADDR11, V.ADDR12 AS V_ADDR12, 
                V.CITY1 AS V_CITY1, V.STATE1 AS V_STATE1, V.ZIP1 AS V_ZIP1, V.TEL AS V_TEL, II.VENDOR, II.STYLE, I.STORE_NO, II.MANUFACTURER_NO, II.METAL_TYPE, 
                II.FINGER_SIZE, II.[DESC], CAST(II.DUEDATE AS DATE) AS DUEDATE, II.CTR_STN_DIAMENSION, II.PRICE, S.NAME AS SALESMAN1, II.VendOrderDt, II.ExpDelDt, II.VendConfNo
                FROM IN_ITEMS II with (nolock)
                INNER JOIN INVOICE I with (nolock) ON I.INV_NO = II.INV_NO AND LTRIM(RTRIM(I.INV_NO)) = @INV_NO
                LEFT JOIN CUSTOMER C with (nolock) ON C.ACC = I.ACC LEFT JOIN VENDORS V with (nolock) ON V.ACC = II.VENDOR
                LEFT JOIN SALESMEN S with (nolock) ON S.CODE = I.SALESMAN1  WHERE II.IsSpecialItem = 1";

                if (!string.IsNullOrWhiteSpace(sStyle))
                    query += " AND II.STYLE = @Style";

                if (!string.IsNullOrWhiteSpace(sStyle))
                    return _helperCommonService.GetSqlData(query, "@Style", sStyle.Trim(), "@INV_NO", strInvoiceNo.Trim());
                return _helperCommonService.GetSqlData(query, "@INV_NO", strInvoiceNo.Trim());

            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving special order details.", ex);
            }
        }
        public DataTable GetSentGold(string condition)
        {
            return _helperCommonService.GetStoreProc("GoldSentDetails", "@condition", condition);
        }

        public DataTable ListOfsentGold(string FROMDATE, string TODATE, string Fixedstore = "")
        {
            var dataTable = new DataTable();
            using (var connection = _connectionProvider.GetConnection())
            using (var command = new SqlCommand("ListOfsentGold", connection))
            using (var adapter = new SqlDataAdapter(command))
            {
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add(new SqlParameter("@FROMDATE", SqlDbType.VarChar, 30) { Value = FROMDATE.Trim() });
                command.Parameters.Add(new SqlParameter("@TODATE", SqlDbType.VarChar, 30) { Value = TODATE.Trim() });
                command.Parameters.Add(new SqlParameter("@Fixedstore", SqlDbType.VarChar, 50) { Value = (Fixedstore ?? "").Trim() });
                adapter.Fill(dataTable);
            }
            return dataTable;
        }

        public bool UpdateSpecialOrderItemsProcess(string XML)
        {
            using (var connection = _connectionProvider.GetConnection())
            using (var dbCommand = new SqlCommand(@"UpdateSpecialOrderItemsProcess", connection)
            {
                CommandType = CommandType.StoredProcedure
            })
            {
                // Add XML parameter
                SqlParameter parameter = new SqlParameter
                {
                    ParameterName = "@SPECIALORDERITEMS",
                    SqlDbType = SqlDbType.Xml
                };

                // Explicitly handle DBNull.Value
                if (string.IsNullOrEmpty(XML))
                    parameter.Value = DBNull.Value;
                else
                    parameter.Value = XML;

                dbCommand.Parameters.Add(parameter);

                connection.Open();
                int rowsAffected = dbCommand.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }

        public DataTable GetMetalPrices(DateTime selDate)
        {
            return _helperCommonService.GetStoreProc("GetLastPriceOfMetals", "@selDate", selDate.ToString());
        }
        public void UpdateDailyMetalPrices(DateTime selDate, string xml)
        {
            using (var connection = _connectionProvider.GetConnection())
            using (var command = new SqlCommand("UpdateDailyMetalPrices", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@selDate", selDate);
                command.Parameters.Add(new SqlParameter("@MPRICES", SqlDbType.Xml) { Value = xml });

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public bool updateScrapGoldDtls(decimal gp_percent, decimal karat_reduction, decimal payout_percent, decimal wt_reduction, string scrap_disclaimer)
        {
            using (SqlConnection connection = _connectionProvider.GetConnection())
            using (SqlCommand dbCommand = new SqlCommand())
            {
                dbCommand.Connection = connection;
                dbCommand.CommandType = CommandType.Text;

                dbCommand.CommandText = "UPDATE UPS_INS SET GP_PERCENT = @GpPercent, KARAT_REDUCTION = @KaratReduction, PAYOUT_PERCENT = @PayoutPercent, WT_REDUCTION = @WtReduction, SCRAP_DISCLAIMER = @ScrapDisclaimer";

                dbCommand.Parameters.AddWithValue("@GpPercent", gp_percent);
                dbCommand.Parameters.AddWithValue("@KaratReduction", karat_reduction);
                dbCommand.Parameters.AddWithValue("@PayoutPercent", payout_percent);
                dbCommand.Parameters.AddWithValue("@WtReduction", wt_reduction);
                dbCommand.Parameters.AddWithValue("@ScrapDisclaimer", scrap_disclaimer);

                connection.Open();
                var rowsAffected = dbCommand.ExecuteNonQuery();
                connection.Close();

                return rowsAffected > 0;
            }
        }

        public DataTable reprintjobbag(string jobbagno, bool ismfg = false, string repairNo = "")
        {
            return _helperCommonService.GetStoreProc("PrintJobBag", "@REPAIR_NO", repairNo, "@STYLE", "", "@JOBBAGNO", jobbagno, "@ismfg", ismfg.ToString());
        }

        public DataTable GetAllSettersForGrid()
        {
            return _helperCommonService.GetSqlData("select name,CAST(0 AS BIT) as freq_used from setters with (nolock) where inactive=0 order by name asc");
        }

        public DataTable GETHISTORYOFJOBBAGForGiveOut(string jbnumber)
        {
            return _helperCommonService.GetStoreProc("FrmGiveOutJobBagWoSplit", "@jobbagno", jbnumber);
        }

        public bool GetJbbagExistCurrentsetter(string JobbagNo, string SetterName)
        {
            string cqry = "select sum(iif(date is null, -qty, qty)) from mfg where inv_no = '" + JobbagNo + "' and setter = '" + SetterName + "'";
            DataTable dt1 = _helperCommonService.GetSqlData(cqry);
            int count;
            return (_helperCommonService.DataTableOK(dt1) && int.TryParse(_helperCommonService.GetValue0(dt1), out count) && count > 0);
        }

        public DataTable UpdateHistory(string jbnumber, string settername, decimal qty, decimal weight, string transNotesList = "", bool isJobbagCompleted = false, string logno = "", string deltranno = "", DateTime? dDate = null)
        {
            using (var connection = _connectionProvider.GetConnection())
            using (var command = new SqlCommand("updatejobbag", connection))
            using (var adapter = new SqlDataAdapter(command))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandTimeout = 10000;

                command.Parameters.AddWithValue("@jobbagno", jbnumber.Trim());
                command.Parameters.AddWithValue("@newsettername", settername.Trim());
                command.Parameters.AddWithValue("@Qty", qty);
                command.Parameters.AddWithValue("@Weight", weight);
                command.Parameters.AddWithValue("@LoggedUser", _loggedUser);
                command.Parameters.AddWithValue("@NOTESDATA", transNotesList ?? string.Empty);
                command.Parameters.AddWithValue("@isJobbagComplted", isJobbagCompleted);
                command.Parameters.AddWithValue("@Logno", logno ?? string.Empty);
                command.Parameters.AddWithValue("@deltranno", deltranno ?? string.Empty);
                command.Parameters.AddWithValue("@DueDate", (object)dDate ?? DBNull.Value);
                var dataTable = new DataTable();
                connection.Open();
                adapter.Fill(dataTable);
                return dataTable;
            }
        }

        public DataTable checkJobBagIsSplitOrNot(string jobbagno)
        {
            return _helperCommonService.GetSqlData("SELECT * FROM lbl_bar with (nolock) WHERE original = @jobbagno", "@jobbagno", jobbagno);
        }
        public bool iSJobbagIsPaidFull(bool option, string jobbag)
        {
            bool iSPaidFull = false;

            // Using 'using' to ensure SqlConnection and SqlCommand are disposed of properly
            using (SqlConnection connection = _connectionProvider.GetConnection())
            using (SqlCommand dbCommand = new SqlCommand("iSJobbagIsPaidFull", connection))
            {
                dbCommand.CommandType = CommandType.StoredProcedure;
                dbCommand.CommandTimeout = 12000;

                dbCommand.Parameters.AddWithValue("@option", option);
                dbCommand.Parameters.AddWithValue("@jobbag", jobbag);

                // Add output parameter
                dbCommand.Parameters.Add("@iSPaidFull", SqlDbType.Bit).Direction = ParameterDirection.Output;

                // Open connection
                connection.Open();

                // Execute the command
                dbCommand.ExecuteNonQuery();

                // Retrieve the value of the output parameter
                iSPaidFull = _helperCommonService.CheckForDBNull(dbCommand.Parameters["@iSPaidFull"].Value, typeof(bool).ToString());

                // Connection will be closed automatically at the end of the using block
            }
            return iSPaidFull;
        }

        public bool iSValidForCloseRepair(string repair_no)
        {
            return _helperCommonService.DataTableOK(_helperCommonService.GetSqlData(@"select 1 from in_items with (nolock) where isspecialitem=1 and inv_no in(select inv_no from in_items where trim(repair_no)=trim(@repair_no))", "@repair_no", repair_no));
        }

        public DataTable CheckInvoice(string invno, bool lOpenItems = false)
        {
            if (lOpenItems)
                return _helperCommonService.GetSqlData(@"select trim(style)style, orignal_style, rsrvd_qty from in_items with (nolock) where inv_no=@invno and rsrvd_qty=0", "@invno", invno);
            return _helperCommonService.GetSqlData(@"select trim(style)style, orignal_style, rsrvd_qty from in_items with (nolock) where inv_no=@invno", "@invno", invno);
        }

        public bool AddJobBagStyleCost(string JobBagNo, string Style)
        {
            using (var connection = _connectionProvider.GetConnection())
            using (var command = new SqlCommand("AddJobBagStyleCost", connection))
            {
                // Set up the command and add parameters
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@JobBagNo", JobBagNo);
                command.Parameters.AddWithValue("@Style", Style);
                command.Parameters.AddWithValue("@LOGGEDUSER", _loggedUser);
                command.Parameters.AddWithValue("@Store_no", _storeCodeInUse);

                // Open the connection, execute the command, and check if any rows were affected
                connection.Open();
                var rowsAffected = command.ExecuteNonQuery();

                return rowsAffected > 0; // Return true if the update was successful, otherwise false
            }
        }

        public bool IsValidBag(string jobbagno)
        {
            return _helperCommonService.DataTableOK(_helperCommonService.GetSqlData(@"SELECT barcode FROM  or_items  where [dbo].[GetBarcode](ltrim(rtrim(barcode))) =[dbo].[GetBarcode](ltrim(rtrim(@JOBBAGNO))) union all SELECT barcode FROM  lbl_bar  where [dbo].[GetBarcode](ltrim(rtrim(barcode))) =[dbo].[GetBarcode](ltrim(rtrim(@JOBBAGNO)))", "@JOBBAGNO", jobbagno));
        }

        public bool iSOpenJobBag(String jobbag, String setter)
        {
            return _helperCommonService.DataTableOK(_helperCommonService.GetSqlData(@"select ISNULL(sum(iIf([TIME] != '', QTY, -QTY)),0)qty from mfg with(nolock) where gen_inv_no =  [dbo].[GetBarcode](trim(@jobbag)) and setter=@setter", "@jobbag", jobbag, "@setter", setter));
        }

        public DataRow CheckJobBagStatus(string jobbagno)
        {
            return _helperCommonService.GetSqlRow("SELECT isnull(qty,0) QTY, isnull(Rcvd,0) RCV FROM OR_ITEMS with (nolock) WHERE barcode=@jobbagno", "@jobbagno", jobbagno);
        }

        public DataTable UpdatemfgNotes(string Strsetter, string strTrancat, string StrNote1, string StrNote2, string Due_Date)
        {
            if (_helperCommonService.is_Singer)
                return _helperCommonService.GetSqlData($"Update MFG set NOTE1 ='{_helperCommonService.EscapeSpecialCharacters(StrNote1)}', NOTE2 ='{_helperCommonService.EscapeSpecialCharacters(StrNote2)}', DUE_DATE= CAST('{Due_Date}' AS DATE) where SETTER='{_helperCommonService.EscapeSpecialCharacters(Strsetter)}' and TRANSACT='{strTrancat}'");
            return _helperCommonService.GetSqlData($"Update MFG set NOTE1 ='{_helperCommonService.EscapeSpecialCharacters(StrNote1)}', NOTE2 ='{_helperCommonService.EscapeSpecialCharacters(StrNote2)}' where SETTER='{_helperCommonService.EscapeSpecialCharacters(Strsetter)}' and TRANSACT='{strTrancat}'");
        }

        public void GetReadyForPickup(string jobbagno)
        {
            _helperCommonService.GetSqlData("UPDATE OR_ITEMS with (rowlock) SET RCVD=QTY WHERE barcode=@jobbagno", "@jobbagno", jobbagno);
        }

        public DataRow GetCustEmail(string jobbagno)
        {
            return _helperCommonService.GetSqlRow("SELECT TOP 1 c.acc, c.name, isnull(c.email,'') email FROM repair r with (nolock) LEFT JOIN rep_item ri with (nolock) ON r.repair_no = ri.repair_no LEFT JOIN customer c with (nolock) ON c.acc=r.acc  WHERE RIGHT('0000000'+ri.barcode,7) = RIGHT('0000000'+@jobbagno,7)", "@jobbagno", jobbagno);
        }

        public void UpdateiSRepairAddedToStock(string jobbag)
        {
            _helperCommonService.GetSqlData(@"UPDATE repair with (rowlock)  SET [iSRepairAddedToStock] = [iSRepairAddedToStock] WHERE GEN_REP_NO = [dbo].GETBARCODE(@JOBBAG);", "@JOBBAG", jobbag);
        }

        public DataTable GettimeSaver()
        {
            return _helperCommonService.GetSqlData("SELECT ltimer,* FROM UPS_INS");
        }

        public List<SelectListItem> GetAllMetalColors()
        {
            DataTable dataTable = _helperCommonService.GetSqlData("SELECT MetalColor FROM METALCOLORS  ORDER BY MetalColor");
            List<SelectListItem> salesmanList = new List<SelectListItem>();
            salesmanList.Add(new SelectListItem() { Text = "", Value = "" });
            if (_helperCommonService.DataTableOK(dataTable))
                foreach (DataRow dr in dataTable.Rows)
                    salesmanList.Add(new SelectListItem() { Text = dr["MetalColor"].ToString().Trim(), Value = dr["MetalColor"].ToString().Trim() });
            return salesmanList;
        }

        public List<SelectListItem> GetAllFingerSizes()
        {
            DataTable dataTable = _helperCommonService.GetSqlData("SELECT SIZE FROM SIZES  ORDER BY SIZE");
            List<SelectListItem> salesmanList = new List<SelectListItem>();
            salesmanList.Add(new SelectListItem() { Text = "", Value = "" });
            if (_helperCommonService.DataTableOK(dataTable))
                foreach (DataRow dr in dataTable.Rows)
                    salesmanList.Add(new SelectListItem() { Text = dr["SIZE"].ToString().Trim(), Value = dr["SIZE"].ToString().Trim() });
            return salesmanList;
        }

        public List<SelectListItem> GetAllTemplates()
        {
            DataTable dataTable = _helperCommonService.GetSqlData("SELECT TEMPLATENAME FROM tag_template  ORDER BY TEMPLATENAME");
            List<SelectListItem> salesmanList = new List<SelectListItem>();
            salesmanList.Add(new SelectListItem() { Text = "", Value = "" });
            if (_helperCommonService.DataTableOK(dataTable))
                foreach (DataRow dr in dataTable.Rows)
                    salesmanList.Add(new SelectListItem() { Text = dr["TEMPLATENAME"].ToString().Trim(), Value = dr["TEMPLATENAME"].ToString().Trim() });
            return salesmanList;
        }

        public List<SelectListItem> GetAllAutoDescTemplates()
        {
            DataTable dataTable = _helperCommonService.GetSqlData("SELECT TEMPLATENAME FROM autodesc_template  ORDER BY TEMPLATENAME");
            List<SelectListItem> salesmanList = new List<SelectListItem>();
            salesmanList.Add(new SelectListItem() { Text = "", Value = "" });
            if (_helperCommonService.DataTableOK(dataTable))
                foreach (DataRow dr in dataTable.Rows)
                    salesmanList.Add(new SelectListItem() { Text = dr["TEMPLATENAME"].ToString().Trim(), Value = dr["TEMPLATENAME"].ToString().Trim() });
            return salesmanList;
        }

        public List<SelectListItem> GetAllPrinters()
        {
            DataTable dataTable = _helperCommonService.GetSqlData("SELECT NAME FROM TAG_PRINTER  ORDER BY NAME");
            List<SelectListItem> salesmanList = new List<SelectListItem>();
            salesmanList.Add(new SelectListItem() { Text = "", Value = "" });
            if (_helperCommonService.DataTableOK(dataTable))
                foreach (DataRow dr in dataTable.Rows)
                    salesmanList.Add(new SelectListItem() { Text = dr["NAME"].ToString().Trim(), Value = dr["NAME"].ToString().Trim() });
            return salesmanList;
        }

        public List<SelectListItem> GetAllCulets()
        {
            DataTable dataTable = _helperCommonService.GetSqlData("SELECT CULET FROM CULET  ORDER BY CULET");
            List<SelectListItem> salesmanList = new List<SelectListItem>();
            salesmanList.Add(new SelectListItem() { Text = "", Value = "" });
            if (_helperCommonService.DataTableOK(dataTable))
                foreach (DataRow dr in dataTable.Rows)
                    salesmanList.Add(new SelectListItem() { Text = dr["CULET"].ToString().Trim(), Value = dr["CULET"].ToString().Trim() });
            return salesmanList;
        }

        public List<SelectListItem> GetAllGirdles()
        {
            DataTable dataTable = _helperCommonService.GetSqlData("SELECT girdle FROM girdle WHERE TRIM(girdle)!='' ORDER BY girdle ");
            List<SelectListItem> salesmanList = new List<SelectListItem>();
            salesmanList.Add(new SelectListItem() { Text = "", Value = "" });
            if (_helperCommonService.DataTableOK(dataTable))
                foreach (DataRow dr in dataTable.Rows)
                    salesmanList.Add(new SelectListItem() { Text = dr["girdle"].ToString().Trim(), Value = dr["girdle"].ToString().Trim() });
            return salesmanList;
        }

        public List<SelectListItem> GetAllPolish()
        {
            DataTable dataTable = _helperCommonService.GetSqlData("SELECT polish FROM polish  WHERE TRIM(polish)!='' ORDER BY polish");
            List<SelectListItem> salesmanList = new List<SelectListItem>();
            salesmanList.Add(new SelectListItem() { Text = "", Value = "" });
            if (_helperCommonService.DataTableOK(dataTable))
                foreach (DataRow dr in dataTable.Rows)
                    salesmanList.Add(new SelectListItem() { Text = dr["polish"].ToString().Trim(), Value = dr["polish"].ToString().Trim() });
            return salesmanList;
        }

        public List<SelectListItem> GetAllSymmetry()
        {
            DataTable dataTable = _helperCommonService.GetSqlData("SELECT symmetry FROM symmetry  WHERE TRIM(symmetry)!='' ORDER BY symmetry");
            List<SelectListItem> salesmanList = new List<SelectListItem>();
            salesmanList.Add(new SelectListItem() { Text = "", Value = "" });
            if (_helperCommonService.DataTableOK(dataTable))
                foreach (DataRow dr in dataTable.Rows)
                    salesmanList.Add(new SelectListItem() { Text = dr["symmetry"].ToString().Trim(), Value = dr["symmetry"].ToString().Trim() });
            return salesmanList;
        }

        public List<SelectListItem> GetAllCutGrades()
        {
            DataTable dataTable = _helperCommonService.GetSqlData("SELECT grades FROM grades  WHERE TRIM(grades)!='' ORDER BY grades");
            List<SelectListItem> salesmanList = new List<SelectListItem>();
            salesmanList.Add(new SelectListItem() { Text = "", Value = "" });
            if (_helperCommonService.DataTableOK(dataTable))
                foreach (DataRow dr in dataTable.Rows)
                    salesmanList.Add(new SelectListItem() { Text = dr["grades"].ToString().Trim(), Value = dr["grades"].ToString().Trim() });
            return salesmanList;
        }

        public List<SelectListItem> GetAllClarities()
        {
            DataTable dataTable = _helperCommonService.GetSqlData("SELECT CLARITY FROM clarities  WHERE TRIM(CLARITY)!='' ORDER BY CLARITY");
            List<SelectListItem> salesmanList = new List<SelectListItem>();
            salesmanList.Add(new SelectListItem() { Text = "", Value = "" });
            if (_helperCommonService.DataTableOK(dataTable))
                foreach (DataRow dr in dataTable.Rows)
                    salesmanList.Add(new SelectListItem() { Text = dr["CLARITY"].ToString().Trim(), Value = dr["CLARITY"].ToString().Trim() });
            return salesmanList;
        }

        public List<SelectListItem> GetAllFlourIntensities()
        {
            DataTable dataTable = _helperCommonService.GetSqlData("SELECT flour FROM flour  WHERE TRIM(flour)!='' ORDER BY flour");
            List<SelectListItem> salesmanList = new List<SelectListItem>();
            salesmanList.Add(new SelectListItem() { Text = "", Value = "" });
            if (_helperCommonService.DataTableOK(dataTable))
                foreach (DataRow dr in dataTable.Rows)
                    salesmanList.Add(new SelectListItem() { Text = dr["flour"].ToString().Trim(), Value = dr["flour"].ToString().Trim() });
            return salesmanList;
        }

        public List<SelectListItem> GetAllGemexFires()
        {
            DataTable dataTable = _helperCommonService.GetSqlData("SELECT Gemex_Fire FROM Gemex_Fire  WHERE TRIM(Gemex_Fire)!='' ORDER BY Gemex_Fire");
            List<SelectListItem> salesmanList = new List<SelectListItem>();
            salesmanList.Add(new SelectListItem() { Text = "", Value = "" });
            if (_helperCommonService.DataTableOK(dataTable))
                foreach (DataRow dr in dataTable.Rows)
                    salesmanList.Add(new SelectListItem() { Text = dr["Gemex_Fire"].ToString().Trim(), Value = dr["Gemex_Fire"].ToString().Trim() });
            return salesmanList;
        }

        public List<SelectListItem> GetAllGemexSparkles()
        {
            DataTable dataTable = _helperCommonService.GetSqlData("SELECT Gemex_Sparkle FROM Gemex_Sparkle  WHERE TRIM(Gemex_Sparkle)!='' ORDER BY Gemex_Sparkle");
            List<SelectListItem> salesmanList = new List<SelectListItem>();
            salesmanList.Add(new SelectListItem() { Text = "", Value = "" });
            if (_helperCommonService.DataTableOK(dataTable))
                foreach (DataRow dr in dataTable.Rows)
                    salesmanList.Add(new SelectListItem() { Text = dr["Gemex_Sparkle"].ToString().Trim(), Value = dr["Gemex_Sparkle"].ToString().Trim() });
            return salesmanList;
        }

        public List<SelectListItem> GetAllGemexBrilliances()
        {
            DataTable dataTable = _helperCommonService.GetSqlData("SELECT Gemex_Brilliance FROM Gemex_Brilliance  WHERE TRIM(Gemex_Brilliance)!='' ORDER BY Gemex_Brilliance");
            List<SelectListItem> salesmanList = new List<SelectListItem>();
            salesmanList.Add(new SelectListItem() { Text = "", Value = "" });
            if (_helperCommonService.DataTableOK(dataTable))
                foreach (DataRow dr in dataTable.Rows)
                    salesmanList.Add(new SelectListItem() { Text = dr["Gemex_Brilliance"].ToString().Trim(), Value = dr["Gemex_Brilliance"].ToString().Trim() });
            return salesmanList;
        }

        public List<SelectListItem> GetAllTemplateDiamonds()
        {
            DataTable dataTable = _helperCommonService.GetSqlData("SELECT template_name FROM DIAMONDLABEL_TEMPLATE  WHERE TRIM(template_name)!='' ORDER BY template_name");
            List<SelectListItem> salesmanList = new List<SelectListItem>();
            salesmanList.Add(new SelectListItem() { Text = "", Value = "" });
            if (_helperCommonService.DataTableOK(dataTable))
                foreach (DataRow dr in dataTable.Rows)
                    salesmanList.Add(new SelectListItem() { Text = dr["template_name"].ToString().Trim(), Value = dr["template_name"].ToString().Trim() });
            return salesmanList;
        }

        public void CheckJobBagValidity(string jobbagno, out string result)
        {
            try
            {
                using (var connection = _connectionProvider.GetConnection())
                using (var command = new SqlCommand("CheckJobBagValidity", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandTimeout = 3000;

                    command.Parameters.AddWithValue("@JOBBAGNO", jobbagno);

                    var outputParam = new SqlParameter("@RETVAL", SqlDbType.VarChar, 1000)
                    {
                        Direction = ParameterDirection.Output
                    };
                    command.Parameters.Add(outputParam);

                    connection.Open();
                    command.ExecuteNonQuery();
                    result = outputParam.Value?.ToString();
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }

        public DataTable checkJobBagIsAlreadySplittedOrNotForPrint(string jobbagno)
        {
            return _helperCommonService.GetSqlData("select m.transact,m.inv_no as jobno,o.style,m.note,s.cast_name,m.date,m.time,m.qty,s.stone_wt as stoneeach,(m.qty * s.stone_wt) as totalstone,(m.qty * s.cost) as totalcost  from mfg m left join or_items o on substring(m.inv_no,1,6) = substring(o.barcode,1,6) left join styles s on s.style = o.style where m.log_no = @bagnumber", "@bagnumber", jobbagno);
        }

        public List<SelectListItem> GetSelectDataforAttri(string tblname, string fldname, string selvalue, string whrcond)
        {
            DataTable dataTable = _helperCommonService.GetSqlData("SELECT " + selvalue + " FROM " + tblname + "  WHERE TRIM(" + fldname + ") = '" + whrcond + "' ORDER BY " + fldname);
            List<SelectListItem> salesmanList = new List<SelectListItem>();
            salesmanList.Add(new SelectListItem() { Text = "", Value = "" });
            if (_helperCommonService.DataTableOK(dataTable))
                foreach (DataRow dr in dataTable.Rows)
                    salesmanList.Add(new SelectListItem() { Text = dr[selvalue].ToString().Trim(), Value = dr[selvalue].ToString().Trim() });
            return salesmanList;
        }

        public List<SelectListItem> GetAttributesByTableName(string tblname, string fldname)
        {
            DataTable dataTable = _helperCommonService.GetSqlData("SELECT " + fldname + " FROM " + tblname + "  WHERE TRIM(" + fldname + ") != '' ORDER BY " + fldname);
            List<SelectListItem> salesmanList = new List<SelectListItem>();
            salesmanList.Add(new SelectListItem() { Text = "", Value = "" });
            if (_helperCommonService.DataTableOK(dataTable))
                foreach (DataRow dr in dataTable.Rows)
                    salesmanList.Add(new SelectListItem() { Text = dr[fldname].ToString().Trim(), Value = dr[fldname].ToString().Trim() });
            return salesmanList;
        }

        public List<SelectListItem> GetAllPartners()
        {
            DataTable dataTable = _helperCommonService.GetSqlData("SELECT * FROM partners  WHERE TRIM(name) != '' ORDER BY name");
            List<SelectListItem> salesmanList = new List<SelectListItem>();
            salesmanList.Add(new SelectListItem() { Text = "", Value = "" });
            if (_helperCommonService.DataTableOK(dataTable))
                foreach (DataRow dr in dataTable.Rows)
                    salesmanList.Add(new SelectListItem() { Text = dr["name"].ToString().Trim(), Value = dr["name"].ToString().Trim() });
            return salesmanList;
        }

        public List<SelectListItem> GetAllFindings()
        {
            DataTable dataTable = _helperCommonService.GetSqlData("SELECT * FROM FINDING  WHERE TRIM(CODE) != '' ORDER BY CODE");
            List<SelectListItem> salesmanList = new List<SelectListItem>();
            salesmanList.Add(new SelectListItem() { Text = "", Value = "" });
            if (_helperCommonService.DataTableOK(dataTable))
                foreach (DataRow dr in dataTable.Rows)
                    salesmanList.Add(new SelectListItem() { Text = dr["CODE"].ToString().Trim(), Value = dr["CODE"].ToString().Trim() });
            return salesmanList;
        }

        public List<SelectListItem> GetAllSetChrgTypes()
        {
            DataTable dataTable = _helperCommonService.GetSqlData("SELECT * FROM SET_CHRG  WHERE TRIM(TYPE) != '' ORDER BY TYPE");
            List<SelectListItem> salesmanList = new List<SelectListItem>();
            salesmanList.Add(new SelectListItem() { Text = "", Value = "" });
            if (_helperCommonService.DataTableOK(dataTable))
                foreach (DataRow dr in dataTable.Rows)
                    salesmanList.Add(new SelectListItem() { Text = dr["TYPE"].ToString().Trim(), Value = dr["TYPE"].ToString().Trim() });
            return salesmanList;
        }

        public List<SelectListItem> GetAllLaborChrgTypes()
        {
            DataTable dataTable = _helperCommonService.GetSqlData("SELECT * FROM LABOR_CHRG  WHERE TRIM(TYPE) != '' ORDER BY TYPE");
            List<SelectListItem> salesmanList = new List<SelectListItem>();
            salesmanList.Add(new SelectListItem() { Text = "", Value = "" });
            if (_helperCommonService.DataTableOK(dataTable))
                foreach (DataRow dr in dataTable.Rows)
                    salesmanList.Add(new SelectListItem() { Text = dr["TYPE"].ToString().Trim(), Value = dr["TYPE"].ToString().Trim() });
            return salesmanList;
        }

        public List<SelectListItem> GetAllSettersList()
        {
            DataTable dataTable = _helperCommonService.GetSqlData("SELECT * FROM SETTERS  WHERE TRIM(NAME) != '' ORDER BY NAME");
            List<SelectListItem> salesmanList = new List<SelectListItem>();
            salesmanList.Add(new SelectListItem() { Text = "", Value = "" });
            if (_helperCommonService.DataTableOK(dataTable))
                foreach (DataRow dr in dataTable.Rows)
                    salesmanList.Add(new SelectListItem() { Text = dr["NAME"].ToString().Trim(), Value = dr["NAME"].ToString().Trim() });
            return salesmanList;
        }

        public DataTable Checkstyleinfo(string style)
        {
            return _helperCommonService.GetSqlData("SELECT TOP 1 * FROM STYLES with (nolock) WHERE STYLE=TRIM(@style) OR TRIM(BARCODE)=TRIM(@style)", "@style", style);
        }

        public String GetStyleLastUpdate(String style)
        {
            String lastDate = String.Empty;
            DataTable dtStyleLastUpdate = _helperCommonService.GetSqlData($"SELECT LastUpdate FROM Styles with (nolock) WHERE Style=@Style", "@Style", style.Trim());
            return _helperCommonService.DataTableOK(dtStyleLastUpdate) ? Convert.ToString(dtStyleLastUpdate.Rows[0]["LastUpdate"]) : lastDate;
        }
        public bool IsSixDigitNumber(string text)
        {
            return text.Length == 6 && text.All(char.IsDigit);
        }

        public DataTable CheckForDuplicateBarcode(string Barcode, string styleNo)
        {
            return _helperCommonService.GetSqlData("SELECT top 1 style FROM styles WHERE barcode = '" + Barcode + "' and style <> '" + styleNo + "'");
        }
        public string Make_Raw_Mat(string lcColor, string lcShape, string lcSize)
        {
            return lcColor.Trim().PadRight(6) + lcShape.Trim().PadRight(3) + lcSize.PadRight(16);
        }

        public DataTable GetStyleImages(string Style)
        {
            DataTable dataTable = new DataTable();
            using (SqlConnection conn = _connectionProvider.GetConnection())
            using (SqlCommand cmd = new SqlCommand("GetStyleImages", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 600;

                cmd.Parameters.Add("@style", SqlDbType.VarChar, 50).Value = Style;

                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                    dataTable.Load(reader);
            }
            return dataTable;

        }
        public int GetLastStockNumber(string prefix = "")
        {
            DataTable dt = !string.IsNullOrEmpty(prefix) ? _helperCommonService.GetSqlData($@"SELECT ISNULL(MAX(CAST(SUBSTRING(stk_no, {prefix.Length + 1}, 8) AS INT)), 0) AS LastGeneratedNumber FROM StkNos WHERE stk_no LIKE '{prefix}%'")
                                                         : _helperCommonService.GetSqlData("SELECT ISNULL(MAX(CAST(stk_no AS INT)), 0) AS LastGeneratedNumber FROM StkNos WHERE TRY_CAST(stk_no AS INT) IS NOT NULL");
            foreach (DataRow dtRow in dt.Rows)
                return Convert.ToInt32(_helperCommonService.CheckForDBNull(dtRow["LastGeneratedNumber"]));
            return 0;
        }

        public decimal InsertStockNumbersIntoDatabase(List<string> stockNumbers, string style, decimal addToStock, string storeNo)
        {
            using (var connection = _connectionProvider.GetConnection())
            {
                connection.Open();

                using (var command = new SqlCommand(@"INSERT INTO StkNos (stk_no, style, in_stock, store_no) VALUES (@StockNumber, @Style, 1, @StoreNo);", connection))
                {
                    command.Parameters.Add(new SqlParameter("@StockNumber", SqlDbType.NVarChar));
                    command.Parameters.Add(new SqlParameter("@Style", SqlDbType.NVarChar));
                    command.Parameters.Add(new SqlParameter("@StoreNo", SqlDbType.NVarChar));
                    command.Parameters.Add(new SqlParameter("@LoggerUser", SqlDbType.NVarChar)).Value = _loggedUser;

                    command.Parameters["@Style"].Value = style;
                    command.Parameters["@StoreNo"].Value = storeNo;

                    foreach (var stockNumber in stockNumbers)
                    {
                        command.Parameters["@StockNumber"].Value = stockNumber;
                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                            ChangeStockForStockNumbers(style, stockNumber, storeNo, "Add/Edit Style");
                    }
                    DataTable dataTable = _helperCommonService.GetSqlData("select in_stock from styles with (nolock) where style = @style", "@style", style);
                    return _helperCommonService.GetValueD(dataTable, "in_stock");
                }
            }
        }

        public void ChangeStockForStockNumbers(string style, string stockNumber, string storeNo, string desc, decimal updateStock = 1)
        {
            _helperCommonService.GetStoreProc("ChangeStock", "@CSTYLE", style, "@DESCR", desc, "@CQTY", updateStock.ToString(), "@stockno", stockNumber, "@LOGGEDUSER", _loggedUser, "@STORE_NO", storeNo, "@IS_FIN", "0", "@CWEIGHT", "0", "@ISHIST", "0", "@CSIZE", "0");

        }

        public DataTable GetTableData(string TableName)
        {
            return _helperCommonService.GetSqlData($"SELECT * FROM {TableName}");
        }

        public DataTable GetStyleData(string Style)
        {
            return _helperCommonService.GetSqlData("select * from styles with (nolock) where style=@style", "@style", Style.Trim());
        }

        public List<SelectListItem> GetAllDisclaimers()
        {
            DataTable dataTable = _helperCommonService.GetSqlData("SELECT code FROM DISCLAIMERS ORDER BY code");
            List<SelectListItem> salesmanList = new List<SelectListItem>();
            salesmanList.Add(new SelectListItem() { Text = "", Value = "" });
            if (_helperCommonService.DataTableOK(dataTable))
                foreach (DataRow dr in dataTable.Rows)
                    salesmanList.Add(new SelectListItem() { Text = dr["code"].ToString().Trim(), Value = dr["code"].ToString().Trim() });
            return salesmanList;
        }

        public DataTable GetStoneGridDataForStylegemMaterial(string stylename, string Jbno = "", bool isbom = false)
        {
            if (isbom)
                return _helperCommonService.GetSqlData(string.Format("select trim(ISNULL(sr.material,'')) AS material,isnull(sr.lot_no,'') as lot_no, trim(ISNULL(sr.quality,'')) AS quality, trim(ISNULL(sr.stn_size,'')) as Size, cast(isnull(sr.Qty, 0) as decimal (5,2)) as PCS,  CAST(isnull(sr.twt, 0) as DECIMAL(15, 4)) as TotalWt, sr.by_pc, isnull(sr.pct,0) as Price, IIF(sr.by_pc=0,CAST(ROUND(isnull(sr.pct,0)* isnull(sr.twt,0),2) as DECIMAL(15,2)),CAST(ROUND(isnull(sr.Qty,0)* isnull(sr.pct,0),2) as DECIMAL(15,2))) AS Total, CAST(iSNULL(DoNotDeduct,0) AS BIT) DoNotDeduct, isnull(sr.note,'') as Note, isnull(sr.unit_price,0) as Unit_Price, sr.header_type,sr.store_code as Store_No from STYL_RAW sr LEFT OUTER JOIN(select distinct name, class, shape, size from raws) r on sr.raw_mat = r.name where (isnull(sr.MATERIAL,'')<>'' or isnull(sr.quality,'')<>'') AND sr.style = @style "), "@style", stylename);
            if (string.IsNullOrWhiteSpace(Jbno))
                return _helperCommonService.GetSqlData(string.Format("select trim(ISNULL(sr.material,'')) AS material,isnull(sr.lot_no,'') as lot_no, trim(ISNULL(sr.quality,'')) AS quality, trim(ISNULL(sr.stn_size,'')) as Size, cast(isnull(sr.Qty, 0) as decimal (6,2)) as PCS,  CAST(isnull(sr.twt, 0) as DECIMAL(15, 4)) as TotalWt, sr.by_pc, isnull(sr.pct,0) as Price, IIF(sr.by_pc=0,CAST(ROUND(isnull(sr.pct,0)* isnull(sr.twt,0),2) as DECIMAL(15,2)),CAST(ROUND(isnull(sr.Qty,0)* isnull(sr.pct,0),2) as DECIMAL(15,2))) AS Total, CAST(iSNULL(DoNotDeduct,0) AS BIT) DoNotDeduct, isnull(sr.note,'') as Note, isnull(sr.unit_price,0) as Unit_Price, sr.header_type,sr.store_code as Store_No from STYL_RAW sr LEFT OUTER JOIN(select distinct name, class, shape, size from raws) r on sr.raw_mat = r.name where sr.style = @style "), "@style", stylename);
            return _helperCommonService.GetSqlData(string.Format("select [description]  AS material,isnull(code,'') as lot_no, cast('' as NVARCHAR(100)) AS  quality, cast('' as NVARCHAR(100)) AS  Size, cast(isnull(CHANGE, 0) as decimal(5,2)) as PCS, CAST(isnull(CHANGE_WT, 0) as DECIMAL(15, 4)) as TotalWt, CAST(IIF(BY_WT = 1, 0, 1) AS BIT)by_pc, isnull(COST, 0) as Price, IIF(by_WT = 1, CAST(ROUND(isnull(cost, 0) * isnull(change_wt, 0), 2) as DECIMAL(15, 2)), CAST(ROUND(isnull(change, 0) * isnull(cost, 0), 2) as DECIMAL(15, 2))) AS Total, CAST(1 AS BIT) DoNotDeduct, isnull(note, '') as Note, isnull(price,0) as Unit_Price, cast('' as nvarchar(30))header_type,isnull(store_no,'') as Store_No from parts_hist where job_bag =@jobbag and isnull(code,'') not like 'code%' and isnull(code,'') not like 'Actual%'"), "@jobbag", Jbno);
        }

        public DataTable GetStoneGridData(string stylename, bool isbom = false)
        {
            if (isbom)
                return _helperCommonService.GetSqlData(@"select trim(r.CLASS) AS class, trim(r.SHAPE) AS Shape, trim(r.size) as Size, sr.clr as Shade, sr.grade as Grade, 
                    sr.clrty as Price_Level, isnull(sr.Qty, 0) as PCS, isnull(sr.weight, 0.00) as Wt_each, 
                    CAST(ROUND(isnull(sr.twt, 0), 2) as DECIMAL(10, 2)) as TotalWt,isnull(sr.pct,0) as Price_Ct, 
                    CAST(ROUND(isnull(sr.weight,0)* isnull(sr.pct,0),2) as DECIMAL(10,2)) as Each, 
                    CAST(ROUND(isnull(sr.twt,0)* isnull(sr.pct,0),2) as DECIMAL(10,2)) AS Total, isnull(sr.ctr, 0) as Ctr_Stn,
                    isnull(sr.lot_no,'') as lot_no, isnull(sr.note,'') as Note,sr.stone_type as stone_type 
                    from STYL_RAW sr  with (nolock) LEFT OUTER JOIN(select distinct name, class, shape, size from raws) r on sr.raw_mat = r.name where (isnull(sr.MATERIAL,'')='' and isnull(sr.quality,'')='') AND sr.style = @style ", "@style", stylename);
            return _helperCommonService.GetSqlData(@"select trim(r.CLASS) AS class, trim(r.SHAPE) AS Shape, trim(r.size) as Size, sr.clr as Shade, sr.grade as Grade, 
                sr.clrty as Price_Level, isnull(sr.Qty, 0) as PCS, isnull(sr.weight, 0.00) as Wt_each, 
                CAST(ROUND(isnull(sr.twt, 0), 2) as DECIMAL(10, 2)) as TotalWt,isnull(sr.pct,0) as Price_Ct, 
                CAST(ROUND(isnull(sr.weight,0)* isnull(sr.pct,0),2) as DECIMAL(10,2)) as Each, 
                CAST(ROUND(isnull(sr.twt,0)* isnull(sr.pct,0),2) as DECIMAL(10,2)) AS Total, isnull(sr.ctr, 0) as Ctr_Stn,
                isnull(sr.lot_no,'') as lot_no, isnull(sr.note,'') as Note,sr.store_code AS Store_No 
                from STYL_RAW sr  with (nolock) LEFT OUTER JOIN(select distinct name, class, shape, size from raws) r on sr.raw_mat = r.name where (sr.MATERIAL='' OR sr.MATERIAL IS NULL) AND sr.style = @style ", "@style", stylename);
        }

        public DataTable GetStockNumbersFromStkNos(string style, string storeNo)
        {
            return _helperCommonService.GetSqlData("select stk_no, in_stock, store_no from stknos with (nolock) where style = @style and store_no = @store order by stk_no", "@style", style, "@store", storeNo);
        }

        public bool IsExistCase(string cases)
        {
            return _helperCommonService.DataTableOK(_helperCommonService.GetSqlData($"SELECT TOP 1 * FROM CASES with (nolock) WHERE CASE_NO='{cases.Replace("'", "''")}' ORDER BY CASE_NO"));
        }

        private decimal ConvertCertNumeric(string value)
        {
            decimal outvalue;
            decimal.TryParse(value, out outvalue);
            return outvalue;
        }

        public void INSERTTOTABLE(string cTable, string cField, string cFieldValue)
        {
            _helperCommonService.GetStoreProc("CheckAndInsert", "@TABLE", cTable, "@FIELD", cField, "@FVALUE", cFieldValue);
        }

        public DataTable GetStylesCase(string style)
        {
            return _helperCommonService.GetSqlData($"SELECT [Store],[Case] FROM Styles_Case with(nolock) WHERE Style='{_helperCommonService.InvStyle(style)}' ORDER BY Store");
        }

        public DataTable GetStylesBasedOnPartNo(string partNo, string style, bool basedOnPartNo = false, string subPartNo = "")
        {
            if (!string.IsNullOrEmpty(subPartNo))
                return _helperCommonService.GetSqlData(string.Format("select style,part_no from styles with (nolock) where part_no like '{0}' and subpart like '{1}' order by style", partNo.Replace("'", "''"), subPartNo.Replace("'", "''")));
            if (basedOnPartNo)
                return _helperCommonService.GetSqlData(string.Format("select style,part_no from styles with (nolock) where part_no like '{0}' order by style", partNo.Replace("'", "''")));
            return _helperCommonService.GetSqlData("select STYLE,CAST_CODE,PART_NO,SUBPART,PRICE,IN_STOCK,[DESC] from styles with (nolock) where part_no = @partNo and style <> @style", "@partNo", partNo, "@style", style);
        }

        public DataTable getStoneSizes(string color, string shape)
        {
            return _helperCommonService.GetSqlData("SELECT '' AS SIZE UNION SELECT DISTINCT SIZE FROM RAWS with (nolock) WHERE CLASS = '" + color + "' AND SHAPE='" + shape + "' order by SIZE");
        }

        public string calc_size(string csize)
        {
            if (string.IsNullOrWhiteSpace(csize))
                return string.Empty;

            csize = csize.ToUpper().Trim();

            // Direct return cases
            if (csize.Contains("-") ||
                csize == "0" || csize == "00" || csize == "000" ||
                csize == "0000" || csize == "00000")
                return csize;

            char splitChar = 'X';
            string[] strArr = csize.Split(splitChar);
            List<decimal> parsedSizes = new List<decimal>();

            foreach (string part in strArr)
            {
                decimal num;
                if (decimal.TryParse(part, out num))
                    parsedSizes.Add(num);
                else
                    parsedSizes.Add(0);
            }

            // Convert to formatted size strings
            List<string> formattedSizes = parsedSizes
                .Select(sz => SzIt(Convert.ToDouble(sz)))
                .ToList();

            // Sort numerically
            formattedSizes.Sort(delegate (string a, string b)
            {
                return Convert.ToDecimal(a).CompareTo(Convert.ToDecimal(b));
            });

            return string.Join(splitChar.ToString(), formattedSizes);
        }


        private string SzIt(double cval)
        {
            cval = Math.Min(99.99, cval); // Ensure value does not exceed 99.99
            return ToTrimmedString(Math.Round((decimal)cval, 4));
        }

        public string ToTrimmedString(decimal target)
        {
            return target.ToString("G29"); // Removes unnecessary trailing zeros while keeping precision
        }

        public DataTable iSStyleItem(String stk_no, String store_no = "")
        {
            return _helperCommonService.GetSqlData($@"select stk_no,style,(in_stock - isnull([dbo].[UsedStkQty](s.stk_no,@store_no),0)) in_stock,store_no from StkNos S WITH (NOLOCK) where S.stk_no=@stk_no and S.store_no = case when isnull(@store_no,'')<>'' then @store_no else S.store_no end", "@stk_no", stk_no, "@store_no", store_no);
        }

        public bool CanDeleteStockNumber(string stockNumber)
        {
            string[] tables = { "in_items", "apm_item", "bil_item", "me_items", "rtv_item" };
            string query = "SELECT COUNT(*) FROM {0} WHERE stk_no = @StockNumber";

            using (SqlConnection connection = _connectionProvider.GetConnection())
            {
                connection.Open();
                foreach (string table in tables)
                {
                    string formattedQuery = string.Format(query, table);
                    using (SqlCommand command = new SqlCommand(formattedQuery, connection))
                    {
                        command.Parameters.AddWithValue("@StockNumber", stockNumber);
                        int count = (int)command.ExecuteScalar();
                        if (count > 0)
                            return false;
                    }
                }
            }
            return true;
        }

        public decimal DeleteStockNumber(string stockNumber, string style, string storeNo)
        {
            using (var connection = _connectionProvider.GetConnection())
            {
                connection.Open();
                using (var command = new SqlCommand("DELETE FROM StkNos WHERE stk_no = @StockNumber", connection))
                {
                    command.Parameters.Add("@StockNumber", SqlDbType.NVarChar).Value = stockNumber;
                    if (command.ExecuteNonQuery() > 0)
                    {
                        ChangeStockForStockNumbers(style, stockNumber, storeNo, "Add/Edit Style", -1);
                        return _helperCommonService.GetValueD(_helperCommonService.GetSqlData("select in_stock from styles with (nolock) where style = @style", "@style", style), "in_stock");
                    }
                }
            }
            return 0;
        }

        public DataTable GetDistinctColumnFromTable(string TableName, string columnName)
        {
            return _helperCommonService.GetSqlData($"SELECT DISTINCT {columnName} FROM {TableName}");
        }

        public DataRow GetCastingMultibyMetal(string metal)
        {
            string sql = string.Format("select isnull(multiplier,0) as multiplier from castingmetal where metal = '{0}'", metal);
            return _helperCommonService.GetSqlRow(sql);
        }

        public List<SelectListItem> GetAllVendorsNames()
        {

            DataTable dataTable = new DataTable();

            using (SqlDataAdapter dataAdapter = new SqlDataAdapter())
            {
                try
                {
                    // Create the command and set its properties
                    dataAdapter.SelectCommand = new SqlCommand();
                    dataAdapter.SelectCommand.Connection = _connectionProvider.GetConnection();

                    dataAdapter.SelectCommand.CommandType = CommandType.Text;
                    //dataAdapter.SelectCommand.CommandText = "select distinct cast_code from styles where cast_code <>'' and cast_code is not null order by cast_code";
                    dataAdapter.SelectCommand.CommandText = "select distinct ACC AS cast_code, NAME from vendors where ACC <>'' and ACC is not null order by ACC";

                    // Fill the datatable From adapter
                    dataAdapter.Fill(dataTable);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                List<SelectListItem> salesmanList = new List<SelectListItem>();
                salesmanList.Add(new SelectListItem() { Text = "All", Value = "" });
                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        salesmanList.Add(new SelectListItem() { Text = dr["cast_code"].ToString().Trim(), Value = dr["NAME"].ToString().Trim() });
                    }
                }
                return salesmanList;
            }
        }
        public DataTable GetSubBrandsByBrand(string brand)
        {
            return _helperCommonService.GetSqlData("select distinct subbrand from SUBBRANDS where subbrand != '' and subbrand is not null and brand='" + brand + "' order by subbrand");
        }

        public void UpdateStylesCase(string style, DataTable dtStyleCase)
        {
            if (!style.EndsWith("%"))
                style += "%";

            ExecuteSqlCommand("DELETE FROM STYLES_CASE WHERE STYLE LIKE @style", "@style", style);

            DataTable dtStyles = _helperCommonService.GetSqlData("SELECT STYLE FROM STYLES WHERE STYLE LIKE @style", "@style", style);
            if (!_helperCommonService.DataTableOK(dtStyles))
                return;

            DataTable dtBulk = new DataTable();
            dtBulk.Columns.Add("STYLE", typeof(string));
            dtBulk.Columns.Add("STORE", typeof(string));
            dtBulk.Columns.Add("CASE", typeof(string));

            foreach (DataRow styleRow in dtStyles.Rows)
            {
                string styleNo = Convert.ToString(styleRow["STYLE"]);

                foreach (DataRow rowCase in dtStyleCase.Rows)
                {
                    string storeNo = Convert.ToString(rowCase["STORE"]);
                    string caseNo = Convert.ToString(rowCase["CASE"]);

                    dtBulk.Rows.Add(styleNo, storeNo, caseNo);
                }
            }
            foreach (DataRow rowCase in dtStyleCase.Rows)
                dtBulk.Rows.Add(style, Convert.ToString(rowCase["STORE"]), Convert.ToString(rowCase["CASE"]));
            using (SqlConnection con = _connectionProvider.GetConnection())
            {
                con.Open();
                using (SqlBulkCopy bulk = new SqlBulkCopy(con))
                {
                    bulk.DestinationTableName = "STYLES_CASE";

                    bulk.ColumnMappings.Add("Style", "Style");
                    bulk.ColumnMappings.Add("Store", "Store");
                    bulk.ColumnMappings.Add("Case", "Case");


                    bulk.WriteToServer(dtBulk);
                }
            }
        }

        public bool ExecuteSqlCommand(
                string commandText,
                string param_name1 = "", string param_value1 = "",
                string param_name2 = "", string param_value2 = "",
                string param_name3 = "", string param_value3 = "",
                string param_name4 = "", string param_value4 = "",
                string param_name5 = "", string param_value5 = "",
                string param_name6 = "", string param_value6 = "",
                string param_name7 = "", string param_value7 = "",
                string param_name8 = "", string param_value8 = "",
                string param_name9 = "", string param_value9 = "",
                string param_name10 = "", string param_value10 = ""
            )
        {
            using (SqlConnection conn = _connectionProvider.GetConnection())
            using (SqlCommand cmd = new SqlCommand(commandText, conn))
            {
                conn.Open();
                cmd.Parameters.Clear();

                AddParam(cmd, param_name1, param_value1);
                AddParam(cmd, param_name2, param_value2);
                AddParam(cmd, param_name3, param_value3);
                AddParam(cmd, param_name4, param_value4);
                AddParam(cmd, param_name5, param_value5);
                AddParam(cmd, param_name6, param_value6);
                AddParam(cmd, param_name7, param_value7);
                AddParam(cmd, param_name8, param_value8);
                AddParam(cmd, param_name9, param_value9);
                AddParam(cmd, param_name10, param_value10);

                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }

        private void AddParam(SqlCommand cmd, string name, string value)
        {
            if (!string.IsNullOrWhiteSpace(name))
            {
                cmd.Parameters.Add(
                    new SqlParameter(name, value)
                );
            }
        }

    }
}
