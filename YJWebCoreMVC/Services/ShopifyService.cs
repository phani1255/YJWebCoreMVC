using Microsoft.Data.SqlClient;
using System.Data;
namespace YJWebCoreMVC.Services
{
    public class ShopifyService
    {

        private readonly ConnectionProvider _connectionProvider;
        private readonly HelperCommonService _helperCommonService;

        public ShopifyService(ConnectionProvider connectionProvider, HelperCommonService helperCommonService)
        {
            _connectionProvider = connectionProvider;
            _helperCommonService = helperCommonService;
        }

        public async Task<bool> IsValidShopifyCredentials(string url, string Apikey, String SecreatKey)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var credentials = System.Convert.ToBase64String(System.Text.Encoding.ASCII.GetBytes($"{Apikey}:{SecreatKey}"));
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", credentials);

                    client.BaseAddress = new Uri(url);

                    HttpResponseMessage response = await client.GetAsync("admin/shop.json");
                    return response.IsSuccessStatusCode;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public DataTable GetShopifyOrderNumbers()
        {
            return Helper.GetSqlData("SELECT LTRIM(RTRIM(ISNULL(PON,''))) as SHPY_ORDERNO FROM INVOICE WHERE ISNULL(PON,'')<>'' UNION SELECT LTRIM(RTRIM(SHOPIFYORDERNUMBER)) as SHPY_ORDERNO FROM INVOICE WHERE SHOPIFYORDERNUMBER<>''  ");
        }

        public DataTable GetInvoicePayments(string inv_no, bool showlayaway = true, bool is_return = false, bool iSFromReturn = false)
        {
            DataTable dataTable = new DataTable();
            using (SqlDataAdapter dataAdapter = new SqlDataAdapter())
            {
                // Create the command and set its properties
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

        private bool AddNewCustomer(ShopifySharp.Address bill_addr, ShopifySharp.Address ship_addr, string email, string Phone, string acc)
        {
            CustomerModelNew CustomersModel = new CustomerModelNew();
            {
            }
            ;

            try
            {
                CustomersModel.ID = 0;
                CustomersModel.ACC = acc;
                CustomersModel.NAME = Helper.CheckForNull<string>(bill_addr.Name);
                CustomersModel.ADDR1 = Helper.CheckForNull<string>(bill_addr.Address1);
                CustomersModel.ADDR12 = Helper.CheckForNull<string>(bill_addr.Address2);
                CustomersModel.ADDR13 = "";
                CustomersModel.CITY1 = Helper.CheckForNull<string>(bill_addr.City);
                CustomersModel.STATE1 = bill_addr.ProvinceCode == null ? "" : bill_addr.ProvinceCode.ToUpper();
                CustomersModel.ZIP1 = Helper.CheckForNull<string>(bill_addr.Zip);
                CustomersModel.TEL = GetPhoneNumber(bill_addr.Phone) ?? GetPhoneNumber(ship_addr.Phone);
                CustomersModel.TEL2 = Convert.ToDecimal(GetPhoneNumber(ship_addr.Phone));
                CustomersModel.COUNTRY = Helper.CheckForNull<string>(bill_addr.Country);
                CustomersModel.WWW = "";
                CustomersModel.EMAIL = Helper.CheckForNull<string>(email);
                CustomersModel.TAX_ID = "";
                CustomersModel.EST_DATE = DateTime.Now;
                CustomersModel.PRICE_FILE = "";
                CustomersModel.JBT = "";
                CustomersModel.NAME2 = Helper.CheckForNull<string>(ship_addr.Name);
                CustomersModel.BILL_ACC = acc;
                CustomersModel.ADDR2 = Helper.CheckForNull<string>(ship_addr.Address1);
                CustomersModel.ADDR22 = Helper.CheckForNull<string>(ship_addr.Address2);
                CustomersModel.ADDR23 = "";
                CustomersModel.CITY2 = Helper.CheckForNull<string>(ship_addr.City);
                CustomersModel.STATE2 = ship_addr.ProvinceCode == null ? "" : ship_addr.ProvinceCode.ToUpper();
                CustomersModel.ZIP2 = Helper.CheckForNull<string>(ship_addr.Zip);
                CustomersModel.FAX = 0;
                CustomersModel.COUNTRY2 = Helper.CheckForNull<string>(ship_addr.Country);
                CustomersModel.BUYER = "";
                CustomersModel.SHIP_VIA = "N";
                CustomersModel.IS_COD = "N";
                CustomersModel.COD_TYPE = "";
                CustomersModel.ON_MAIL = "";
                CustomersModel.RESIDENT = "Y";
                CustomersModel.NOTE = "";
                CustomersModel.INTEREST = 0;
                CustomersModel.LAST_INT = null;
                CustomersModel.GRACE = 0;
                CustomersModel.SALESMAN1 = "";
                CustomersModel.SALESMAN2 = "";
                CustomersModel.SALESMAN3 = "";
                CustomersModel.SALESMAN4 = "";
                CustomersModel.PERCENT1 = 0;
                CustomersModel.PERCENT2 = 0;
                CustomersModel.PERCENT3 = 0;
                CustomersModel.PERCENT4 = 0;
                CustomersModel.TERM1 = 0;
                CustomersModel.TERM2 = 0;
                CustomersModel.TERM3 = 0;
                CustomersModel.TERM4 = 0;
                CustomersModel.TERM5 = 0;
                CustomersModel.TERM6 = 0;
                CustomersModel.TERM7 = 0;
                CustomersModel.TERM8 = 0;
                CustomersModel.TERM_PCT1 = 100;
                CustomersModel.TERM_PCT2 = 0;
                CustomersModel.TERM_PCT3 = 0;
                CustomersModel.TERM_PCT4 = 0;
                CustomersModel.TERM_PCT5 = 0;
                CustomersModel.TERM_PCT6 = 0;
                CustomersModel.TERM_PCT7 = 0;
                CustomersModel.TERM_PCT8 = 0;
                CustomersModel.CREDIT = 0;
                CustomersModel.PERCENT = 0;
                CustomersModel.ON_HOLD = false;
                CustomersModel.INACTIVE = false;
                CustomersModel.REASON = "";
                CustomersModel.COLCSENT = false;
                CustomersModel.COLCDATE = null;
                CustomersModel.COLCRSN = "";
                CustomersModel.ATTR1VAL = "";
                CustomersModel.ATTR2VAL = "";
                CustomersModel.ATTR3VAL = "";
                CustomersModel.ATTR4VAL = "";
                CustomersModel.ATTR5VAL = "";
                CustomersModel.ATTR6VAL = "";
                CustomersModel.ATTR7VAL = "";
                CustomersModel.ATTR8VAL = "";
                CustomersModel.MULTIATTR1VAL = "";
                CustomersModel.MULTIATTR2VAL = "";
                CustomersModel.MULTIATTR3VAL = "";
                CustomersModel.CUSTCHECKVAL1 = false;
                CustomersModel.CUSTCHECKVAL2 = false;
                CustomersModel.CUSTCHECKVAL3 = false;
                CustomersModel.CUSTCHECKVAL4 = false;
                CustomersModel.CUSTCHECKVAL5 = false;
                CustomersModel.CUSTCHECKVAL6 = false;
                CustomersModel.CUSTCHECKVAL7 = false;
                CustomersModel.CUSTCHECKVAL8 = false;
                CustomersModel.CUSTATTRLABELS = CustomersModel.CUSTATTRLABELS;
                CustomersModel.DIVISION = "";
                CustomersModel.OSTEL = "";
                CustomersModel.POPUPNOTE = "";
                CustomersModel.WEB_USER = "";
                CustomersModel.WEB_PASSWORD = "";
                CustomersModel._Action = true;
                //return Helper.AddCustomer(CustomerModel);
                return (AddCustomer(CustomersModel));

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool AddCustomer(CustomerModelNew customer)
        {
            using (SqlCommand dbCommand = new SqlCommand())
            {
                // Set the command object properties
                dbCommand.Connection = new SqlConnection(Helper.connString);
                dbCommand.CommandType = CommandType.StoredProcedure;
                dbCommand.CommandText = "AddUpdateCustomer";

                Object estdate, lastint, colldate;
                if (customer.EST_DATE == null)
                    estdate = DBNull.Value;
                else
                    estdate = customer.EST_DATE;

                if (customer.LAST_INT == null)
                    lastint = DBNull.Value;
                else
                    lastint = customer.LAST_INT;

                if (customer.COLCDATE == null)
                    colldate = DBNull.Value;
                else
                    colldate = customer.COLCDATE;

                dbCommand.Parameters.AddWithValue("@Id", customer.ID);
                dbCommand.Parameters.AddWithValue("@Acc", customer.ACC);
                dbCommand.Parameters.AddWithValue("@Name", customer.NAME);
                dbCommand.Parameters.AddWithValue("@Addr1", customer.ADDR1);
                dbCommand.Parameters.AddWithValue("@Addr12", customer.ADDR12);
                dbCommand.Parameters.AddWithValue("@Addr13", customer.ADDR13);
                dbCommand.Parameters.AddWithValue("@City1", customer.CITY1);
                dbCommand.Parameters.AddWithValue("@State1", customer.STATE1);
                dbCommand.Parameters.AddWithValue("@Zip1", customer.ZIP1);
                dbCommand.Parameters.AddWithValue("@Tel", customer.TEL);
                dbCommand.Parameters.AddWithValue("@Country", customer.COUNTRY);
                dbCommand.Parameters.AddWithValue("@www", customer.WWW);
                dbCommand.Parameters.AddWithValue("@Email", customer.EMAIL);
                dbCommand.Parameters.AddWithValue("@PRICE_FILE", customer.PRICE_FILE);
                dbCommand.Parameters.AddWithValue("@EST_DATE", estdate);
                dbCommand.Parameters.AddWithValue("@JBT", customer.JBT);
                dbCommand.Parameters.AddWithValue("@NAME2", customer.NAME2);
                dbCommand.Parameters.AddWithValue("@BILL_ACC", customer.BILL_ACC);
                dbCommand.Parameters.AddWithValue("@ADDR2", customer.ADDR2);
                dbCommand.Parameters.AddWithValue("@ADDR22", customer.ADDR22);
                dbCommand.Parameters.AddWithValue("@ADDR23", customer.ADDR23);
                dbCommand.Parameters.AddWithValue("@CITY2", customer.CITY2);
                dbCommand.Parameters.AddWithValue("@STATE2", customer.STATE2);
                dbCommand.Parameters.AddWithValue("@ZIP2", customer.ZIP2);
                dbCommand.Parameters.AddWithValue("@FAX", customer.FAX);
                dbCommand.Parameters.AddWithValue("@Country2", customer.COUNTRY2);
                dbCommand.Parameters.AddWithValue("@BUYER", customer.BUYER);
                dbCommand.Parameters.AddWithValue("@SHIP_VIA", customer.SHIP_VIA);
                dbCommand.Parameters.AddWithValue("@ON_MAIL", customer.ON_MAIL);
                dbCommand.Parameters.AddWithValue("@RESIDENT", customer.RESIDENT);
                dbCommand.Parameters.AddWithValue("@IS_COD", customer.IS_COD);
                dbCommand.Parameters.AddWithValue("@COD_TYPE", customer.COD_TYPE);
                dbCommand.Parameters.AddWithValue("@NOTE", customer.NOTE);
                dbCommand.Parameters.AddWithValue("@INTEREST", customer.INTEREST);
                dbCommand.Parameters.AddWithValue("@LAST_INT", lastint);
                dbCommand.Parameters.AddWithValue("@GRACE", customer.GRACE);
                dbCommand.Parameters.AddWithValue("@SALESMAN1", customer.SALESMAN1);
                dbCommand.Parameters.AddWithValue("@SALESMAN2", customer.SALESMAN2);
                dbCommand.Parameters.AddWithValue("@SALESMAN3", customer.SALESMAN3);
                dbCommand.Parameters.AddWithValue("@SALESMAN4", customer.SALESMAN4);
                dbCommand.Parameters.AddWithValue("@PERCENT1", customer.PERCENT1);
                dbCommand.Parameters.AddWithValue("@PERCENT2", customer.PERCENT2);
                dbCommand.Parameters.AddWithValue("@PERCENT3", customer.PERCENT3);
                dbCommand.Parameters.AddWithValue("@PERCENT4", customer.PERCENT4);
                dbCommand.Parameters.AddWithValue("@TERM1", customer.TERM1);
                dbCommand.Parameters.AddWithValue("@TERM2", customer.TERM2);
                dbCommand.Parameters.AddWithValue("@TERM3", customer.TERM3);
                dbCommand.Parameters.AddWithValue("@TERM4", customer.TERM4);
                dbCommand.Parameters.AddWithValue("@TERM5", customer.TERM5);
                dbCommand.Parameters.AddWithValue("@TERM6", customer.TERM6);
                dbCommand.Parameters.AddWithValue("@TERM7", customer.TERM7);
                dbCommand.Parameters.AddWithValue("@TERM8", customer.TERM8);
                dbCommand.Parameters.AddWithValue("@TERM_PCT1", customer.TERM_PCT1);
                dbCommand.Parameters.AddWithValue("@TERM_PCT2", customer.TERM_PCT2);
                dbCommand.Parameters.AddWithValue("@TERM_PCT3", customer.TERM_PCT3);
                dbCommand.Parameters.AddWithValue("@TERM_PCT4", customer.TERM_PCT4);
                dbCommand.Parameters.AddWithValue("@TERM_PCT5", customer.TERM_PCT5);
                dbCommand.Parameters.AddWithValue("@TERM_PCT6", customer.TERM_PCT6);
                dbCommand.Parameters.AddWithValue("@TERM_PCT7", customer.TERM_PCT7);
                dbCommand.Parameters.AddWithValue("@TERM_PCT8", customer.TERM_PCT8);
                dbCommand.Parameters.AddWithValue("@CREDIT", customer.CREDIT);
                dbCommand.Parameters.AddWithValue("@PERCENT", customer.PERCENT);
                dbCommand.Parameters.AddWithValue("@ON_HOLD", customer.ON_HOLD);
                dbCommand.Parameters.AddWithValue("@INACTIVE", customer.INACTIVE);
                dbCommand.Parameters.AddWithValue("@REASON", customer.REASON);
                dbCommand.Parameters.AddWithValue("@COLCSENT", customer.COLCSENT);
                dbCommand.Parameters.AddWithValue("@COLCDATE", colldate);
                dbCommand.Parameters.AddWithValue("@COLCRSN", customer.COLCRSN);
                dbCommand.Parameters.AddWithValue("@ATTR1VAL", customer.ATTR1VAL);
                dbCommand.Parameters.AddWithValue("@ATTR2VAL", customer.ATTR2VAL);
                dbCommand.Parameters.AddWithValue("@ATTR3VAL", customer.ATTR3VAL);
                dbCommand.Parameters.AddWithValue("@ATTR4VAL", customer.ATTR4VAL);
                dbCommand.Parameters.AddWithValue("@ATTR5VAL", customer.ATTR5VAL);
                dbCommand.Parameters.AddWithValue("@ATTR6VAL", customer.ATTR6VAL);
                dbCommand.Parameters.AddWithValue("@ATTR7VAL", customer.ATTR7VAL);
                dbCommand.Parameters.AddWithValue("@ATTR8VAL", customer.ATTR8VAL);
                dbCommand.Parameters.AddWithValue("@MULTIATTR1VAL", customer.MULTIATTR1VAL);
                dbCommand.Parameters.AddWithValue("@MULTIATTR2VAL", customer.MULTIATTR2VAL);
                dbCommand.Parameters.AddWithValue("@MULTIATTR3VAL", customer.MULTIATTR3VAL);
                dbCommand.Parameters.AddWithValue("@CUSTCHECKVAL1", customer.CUSTCHECKVAL1);
                dbCommand.Parameters.AddWithValue("@CUSTCHECKVAL2", customer.CUSTCHECKVAL2);
                dbCommand.Parameters.AddWithValue("@CUSTCHECKVAL3", customer.CUSTCHECKVAL3);
                dbCommand.Parameters.AddWithValue("@CUSTCHECKVAL4", customer.CUSTCHECKVAL4);
                dbCommand.Parameters.AddWithValue("@CUSTCHECKVAL5", customer.CUSTCHECKVAL5);
                dbCommand.Parameters.AddWithValue("@CUSTCHECKVAL6", customer.CUSTCHECKVAL6);
                dbCommand.Parameters.AddWithValue("@CUSTCHECKVAL7", customer.CUSTCHECKVAL7);
                dbCommand.Parameters.AddWithValue("@CUSTCHECKVAL8", customer.CUSTCHECKVAL8);
                dbCommand.Parameters.AddWithValue("@DIVISION", customer.DIVISION);
                dbCommand.Parameters.AddWithValue("@OSTEL", customer.OSTEL);
                dbCommand.Parameters.AddWithValue("@POPUPNOTE", customer.POPUPNOTE);
                dbCommand.Parameters.AddWithValue("@TEL2", customer.TEL2);
                dbCommand.Parameters.AddWithValue("@WEB_USER", customer.WEB_USER);
                dbCommand.Parameters.AddWithValue("@WEB_PASSWORD", customer.WEB_PASSWORD);
                dbCommand.Parameters.AddWithValue("@SHIP_TYPE", customer.SHIP_TYPE);
                dbCommand.Parameters.AddWithValue("@ON_ACCOUNT", customer.ON_ACCOUNT);
                dbCommand.Parameters.AddWithValue("@PRIVATECUSTOMER", customer.ISPRIVATECUST);
                dbCommand.Parameters.AddWithValue("@MODEOFCONTACT", customer.CONTACTMODE);
                dbCommand.Parameters.AddWithValue("@Ring_Size_1", customer.Ring_Size_1);
                dbCommand.Parameters.AddWithValue("@Ring_Size_2", customer.Ring_Size_2);
                dbCommand.Parameters.AddWithValue("@different_ship", customer.different_ship);
                dbCommand.Parameters.AddWithValue("@RefbyAcc", customer.RefBy_acc);
                dbCommand.Parameters.AddWithValue("@oldCustomerCode", customer.oldCustomerCode);
                dbCommand.Parameters.AddWithValue("@DRIVERLICENSE_STATE", customer.driverlicense_state);

                dbCommand.Parameters.AddWithValue("@iDType", customer.iDType);
                dbCommand.Parameters.AddWithValue("@iDNumber", customer.iDNumber);
                dbCommand.Parameters.AddWithValue("@nation", customer.nation);
                dbCommand.Parameters.AddWithValue("@dob", customer.DOB);
                dbCommand.Parameters.AddWithValue("@height", customer.height);
                dbCommand.Parameters.AddWithValue("@weight", customer.weight);
                dbCommand.Parameters.AddWithValue("@hairColor", customer.hairColor);
                dbCommand.Parameters.AddWithValue("@eyeColor", customer.eyeColor);

                dbCommand.Parameters.AddWithValue("@DRIVERLICENSE_NUMBER", customer.driverlicense_number);
                dbCommand.Parameters.AddWithValue("@Store_no", customer.Store_no);
                dbCommand.Parameters.AddWithValue("@CELL", customer.CELL);
                dbCommand.Parameters.AddWithValue("@okTotxt", customer.ok_totext);
                dbCommand.Parameters.AddWithValue("@oktoEmail", customer.ok_toemail);
                dbCommand.Parameters.AddWithValue("@oktoMail", customer.ok_tomail);
                dbCommand.Parameters.AddWithValue("@oktocall", customer.ok_tocall);
                dbCommand.Parameters.AddWithValue("@TERM", customer.TERM_TEXT);
                dbCommand.Parameters.AddWithValue("@declined", customer.declined);
                dbCommand.Parameters.AddWithValue("@SpouseAcc", customer.SpouseAcc);

                dbCommand.Parameters.AddWithValue("@Id_Type", customer.Id_Type);
                dbCommand.Parameters.AddWithValue("@SEX", customer.Sex);
                dbCommand.Parameters.AddWithValue("@Race", customer.Race_WM);
                dbCommand.Parameters.AddWithValue("@EYE", customer.Eye);
                dbCommand.Parameters.AddWithValue("@HEIGHT_WM", customer.Height_WM);
                dbCommand.Parameters.AddWithValue("@NoReffal", customer._Noreffal);
                dbCommand.Parameters.AddWithValue("@Action", customer._Action);

                SqlParameter parameter = new SqlParameter()
                {
                    ParameterName = "@CUSTATTRLABELS",
                    SqlDbType = System.Data.SqlDbType.Xml,
                    Value = customer.CUSTATTRLABELS
                };
                dbCommand.Parameters.Add(parameter);

                dbCommand.Parameters.AddWithValue("@Non_Taxable", customer.Non_Taxable);
                dbCommand.Parameters.AddWithValue("@Tax_id", customer.TAX_ID);
                dbCommand.Parameters.AddWithValue("@Non_Taxable_Reason", customer.Non_Taxable_Reason);
                dbCommand.Parameters.AddWithValue("@No_Mass_SMS", customer.No_Mass_SMS);
                dbCommand.Parameters.AddWithValue("@CustSource", customer.Source);
                dbCommand.Parameters.AddWithValue("@Dealer", customer._Dealer);
                dbCommand.Parameters.AddWithValue("@DonotEmailst", customer._Donotemailst);
                // Open the connection, execute the query and close the connection
                dbCommand.Connection.Open();
                var rowsAffected = dbCommand.ExecuteNonQuery();
                dbCommand.Connection.Close();

                return rowsAffected > 0;
            }
        }
        public async Task<bool> AddNewInvoice(Order order)
        {
            /*if (Helper.DataTableOK(Helper.GetSqlRow("select * from invoice where isnull(pon,'')=@order", "@order", order.OrderNumber.ToString())))
                return false;*/
            string _Phone = string.Empty, _CustEmail = string.Empty;
            string _ShopifyOrderNo = order.OrderNumber.ToString();
            string _NextInvoice = string.Empty;

            if (order.BillingAddress == null && order.ShippingAddress == null)
                order.BillingAddress = new ShopifySharp.Address();

            ShopifySharp.Address ship_addr = order.ShippingAddress ?? order.BillingAddress;
            ShopifySharp.Address bill_addr = order.BillingAddress = order.BillingAddress ?? order.ShippingAddress; ;

            ShopifySharp.Customer po_customer = order.Customer;
            IEnumerable<ShopifySharp.LineItem> lineitems = order.LineItems;
            IEnumerable<ShopifySharp.TaxLine> taxlines = order.TaxLines;
            IEnumerable<ShopifySharp.DiscountCode> discountcodes = order.DiscountCodes;
            IEnumerable<ShopifySharp.DiscountApplication> discountapps = order.DiscountApplications;
            IEnumerable<ShopifySharp.ShippingLine> shippinglines = order.ShippingLines;

            if (Helper.IsValidInvoiceNo(_ShopifyOrderNo))
            {
                while (Helper.IsValidInvoiceNo(_ShopifyOrderNo))
                {
                    _NextInvoice = GetNextInvoice().PadLeft(6, ' ');//GetNextInvoiceNumber(_ShopifyOrderNo);
                    break;
                }
            }

            if (po_customer != null)
            {
                _Phone = Helper.CheckForDBNull(po_customer.Phone, typeof(string).FullName);
                _ShopifyCustId = Helper.CheckForDBNull(po_customer.Id, typeof(long).FullName);
                if (string.IsNullOrWhiteSpace(_Phone))
                    _CustEmail = Helper.CheckForDBNull(po_customer.Email, typeof(string).FullName);
            }

            if (string.IsNullOrEmpty(_ShopifyCustId) && !string.IsNullOrEmpty(_CustEmail))
            {
                DataRow drcheckCust = CheckCustomerExists(0, 0, "", _CustEmail);
                if (Helper.DataTableOK(drcheckCust))
                    _ShopifyCustId = Helper.CheckForDBNull(drcheckCust["acc"], typeof(string).FullName);
            }
            else
            {
                if (string.IsNullOrEmpty(_ShopifyCustId))
                {
                    DataRow drCust = Helper.getHighestCustAccValue(_MinLimit, _MaxLimit);
                    if (Helper.CheckForDBNull(drCust["acc"], typeof(string)) != "")
                    {
                        getmaxval = System.Convert.ToInt64(drCust["acc"].ToString());
                        if (getmaxval > 999999999999)
                        {
                            _ShopifyCustId = "";
                        }
                        value = System.Convert.ToInt64(drCust["acc"].ToString());
                        _ShopifyCustId = Math.Max(10000, (value + 1)).ToString();
                    }
                }
            }

            this.INVOICE_NO = !string.IsNullOrEmpty(_NextInvoice) ? _NextInvoice.PadLeft(6, ' ') : _ShopifyOrderNo.PadLeft(6, ' ');  //this.GetNextSeqNo().PadLeft(6, ' ');

            DataRow custRow = CheckCustomerExists(0, 0, _ShopifyCustId, "");
            string cust_acc, salesman;
            cust_acc = _ShopifyCustId;
            salesman = Helper.DataTableOK(Helper.GetSalesmancodeonly()) ? Helper.GetSalesmancodeonly().Rows[0]["CODE"].ToString() : "";
            if (custRow == null)
                AddNewCustomer(bill_addr, ship_addr, order.Email, _Phone, cust_acc);
            else
                cust_acc = Helper.CheckForDBNull(custRow["acc"]);

            DateTimeOffset orderDate = (DateTimeOffset)order.CreatedAt;
            InvoiceModel InvoiceModel = new InvoiceModel()
            {

                INV_NO = this.INVOICE_NO,
                ACC = cust_acc,
                BACC = cust_acc,
                ADD_COST = 0,
                SNH = 0,
                DATE = orderDate.Date,
                //DATE = DateTime.Now,
                PON = "NONE", //order.OrderNumber.ToString(),
                MESSAGE = "",
                GR_TOTAL = order.TotalPrice, //order.SubtotalPrice,

                ADDR1 = Helper.CheckForNull<string>(ship_addr.Address1),
                NAME = Helper.CheckForNull<string>(ship_addr.Name),
                ADDR2 = Helper.CheckForNull<string>(ship_addr.Address2),
                ADDR3 = "",
                CITY = Helper.CheckForNull<string>(ship_addr.City),
                STATE = ship_addr.ProvinceCode == null ? "" : ship_addr.ProvinceCode.ToUpper(),
                ZIP = ship_addr.Zip != null ? ship_addr.Zip.Trim() : "",
                COUNTRY = ship_addr.CountryCode == null ? "" : ship_addr.CountryCode.ToUpper(),
                OPERATOR = Helper.LoggedUser,
                SALESMAN1 = salesman,
                SALESMAN2 = "",
                SALESMAN3 = "",
                SALESMAN4 = "",
                STORE_NO = Helper.StoreCodeInUse1,
                //SALES_TAX = ((order.TotalPrice-order.TotalTax) * Helper.StoreSalesTax)/100, // order.TotalTax,
                TAXINCLUDED = false,
                CUST_PON = "",
                SHIP_BY = "",
                VIA_UPS = "N",
                IS_COD = "",
                WEIGHT = 0,
                COD_TYPE = "A",
                TERM1 = 0,
                TERM2 = 0,
                TERM3 = 0,
                TERM4 = 0,
                TERM5 = 0,
                TERM6 = 0,
                TERM7 = 0,
                TERM8 = 0,
                TERM_PCT1 = 100,
                TERM_PCT2 = 0,
                TERM_PCT3 = 0,
                TERM_PCT4 = 0,
                TERM_PCT5 = 0,
                TERM_PCT6 = 0,
                TERM_PCT7 = 0,
                TERM_PCT8 = 0,
                INSURED = 0,
                EARLY = "R",
                MAN_SHIP = false,
                RESIDENT = "Y",
                IS_FDX = false,
                IS_DEB = false,
                PERCENT = 0,
                SAMPLE = "",
                ShipType = 0,
                UPSTRAK = "",
                SYSTEMNAME = Helper.GetRegisterNames(),
                ShopifyStoreNo = _storeName,

            };
            // string customername = bill_addr.Name ?? (ship_addr.Name ?? order.Name);

            DataTable invoiceItems = GetInvoiceItems(this.INVOICE_NO, false, false, false);

            if (invoiceItems == null)
                return false;

            var columnsToRemove = new List<string> { "BY_WT" };
            var columnsToAdd = new Dictionary<string, Type>
            {
                { "ISBAR", typeof(bool) },
                { "Item_Type", typeof(string) },
                { "JMC_LIFETIME", typeof(string) },
                { "SalesTax1", typeof(decimal) },
                { "SalesTax2", typeof(decimal) },
                { "SalesTax3", typeof(decimal) },
                { "TotalCount", typeof(decimal) },
                { "SalesTax", typeof(decimal) },
                { "SubTotal", typeof(decimal) },
                { "RepCost", typeof(decimal) },
                { "UserGCNo", typeof(string) },
                { "REPAIR_NOTE", typeof(string) },
                { "REPAIR_NO", typeof(string) },
                { "WEIGHT", typeof(decimal) }
            };

            // Remove columns
            foreach (var column in columnsToRemove)
            {
                if (invoiceItems.Columns.Contains(column))
                {
                    invoiceItems.Columns.Remove(column);
                }
            }

            // Add columns
            foreach (var column in columnsToAdd)
            {
                if (!invoiceItems.Columns.Contains(column.Key))
                {
                    invoiceItems.Columns.Add(column.Key, column.Value);
                }
            }

            invoiceItems.Columns["WEIGHT"].SetOrdinal(5);

            DataTable paymentItems = GetInvoicePayments(INVOICE_NO);
            DataTable discountItems = GetInvoiceDiscount(INVOICE_NO);

            string returnstyle;
            bool isbar;
            int StyleSequnce = 1;

            foreach (ShopifySharp.LineItem orderItem in lineitems)
            {
                string productsku = orderItem.SKU != null && !string.IsNullOrEmpty(orderItem.SKU) ? orderItem.SKU : "SPECIAL";
                string Style_Spl = string.Empty;

                Style_Spl = productsku.ToUpper() == "SPECIAL" ? string.Concat("SPECIAL-", StyleSequnce.ToString("000")) : productsku;

                InvoiceModel.TAXABLE = orderItem.Taxable;

                if (!Helper.isValidStyle(productsku))
                //if (/*!Helper.is_RK &&*/ !Helper.CheckStyle(productsku, out returnstyle, out isbar))
                {
                    //Helper.MsgBox("style# " + productsku + " was not found, it was automatically added to inventory items");

                    decimal _ItemPrice = Convert.ToDecimal(orderItem.Price);
                    CreateNewStyle(productsku, orderItem.Title, _ItemPrice);
                }
                if (productsku.ToUpper() == "SPECIAL")
                    StyleSequnce++;


                invoiceItems.Rows.Add(new Object[]{
                            "",
                            Style_Spl,
                            //string.Format("SKU: {0} - {1}",orderItem.ProductId,orderItem.Title),
                            !string.IsNullOrEmpty(productsku) && productsku!="Misc" ? string.Concat(orderItem.Title,"(Style# : ",productsku,")") : orderItem.Title,
                            0 ,//SOLD_QTY
							orderItem.Quantity,//QTY
							0,//WEIGHT
							0,//GOLD
							0,//STONE
							0,//LABOR
							0,//NET_WT
							orderItem.Price,//TAG_PRICE
							order.TotalDiscounts > 0? order.TotalDiscounts : 0,//DISCOUNT
							orderItem.Price,//PRICE
							orderItem.Price,//EXT
							0,//REPAIR,
							"",//WARANTY,
							0,//WARANTY_COST
							//order.Store,//VENDOR
                            "", // vendor
							"",//MANFACTURER_NO
							"",//METAL TYPE
							"",//FINGER_SIZE
							Convert.ToDateTime(DateTime.Now.AddDays(14)),//due_date         
							"",//ctr_str_dimension
							1,//isspecialItem
							0,//isdlvrd,
							"",//repair_note
							Style_Spl,//specialstyle
							"",//repair_no
							0,//cdesc1
							0,//cdesc2
							0,//cdesc3
							0,//cdesc4
							0,//cost1
							0,//cost2
							0,//cost3
							0,//cost4
							0,//tcost
							0,//old_qty,
							System.DateTime.Now,//promised_date
							0,//rsrvd_qty
							0,//not_stock
							0,//in_stock
							0,//t_cost
							0,//min_price
                            0,//pickedupqty
                             "",// stkno
                            0,// dimond wt
                            0,//uncut diamondweight
							0,//isbar
							"",//item_type,
							"",//jmc_lifetime
							0,//salestax1
							0,//salestax2
							0,//salestax3
							orderItem.Price,//totalcount
							InvoiceModel.SALES_TAX,//salestax
							0,//subtotal
							0,//repcost
							""//user_gcno                         
						});
            }

            // string mtlXmlString = string.Empty;

            // string out_pon = string.Empty;
            string out_inv_no;
            decimal _WebSalesTaxRate = 0, _WebSalesTax = 0, _WebSnh = 0;
            DataRow drdiscount = null;
            foreach (ShopifySharp.TaxLine taxLine in taxlines)
            {
                _WebSalesTaxRate += (Convert.ToDecimal(taxLine.Rate) * 100);
                _WebSalesTax += Convert.ToDecimal(taxLine.Price);
            }

            foreach (ShopifySharp.ShippingLine shipLine in shippinglines)
                _WebSnh += Convert.ToDecimal(shipLine.Price);

            foreach (ShopifySharp.DiscountCode disc in discountcodes)
            {
                drdiscount = discountItems.NewRow();
                drdiscount["inv_no"] = this.INVOICE_NO;
                drdiscount["discount"] = disc.Type;
                drdiscount["amount"] = disc.Amount;
            }

            if (drdiscount != null)
                discountItems.Rows.Add(drdiscount);
            if (order.TotalTax > 0 && order.SubtotalPrice > 0)
                InvoiceModel.Sales_Tax_Rate = (order.TotalTax * 100) / order.SubtotalPrice;
            string ShopifyPayMethod = "", _FinPaymentmethod = "";

            InvoiceModel.Sales_Tax_Rate = (order.TotalTax * 100) / order.SubtotalPrice;


            var Transact_service = new TransactionService(Helper.ShopifyURL, Helper.ShopifySecret);
            var transactions = await Transact_service.ListAsync(order.Id.Value);
            paymentItems.Clear();
            if (transactions.Count() > 0)
            {
                foreach (var transaction in transactions)
                {
                    if (transaction.Status.ToString() == "success")
                    {
                        ShopifyPayMethod = transaction.Gateway;
                        /* if (ShopifyPayMethod != null && !string.IsNullOrEmpty(ShopifyPayMethod) && !ShopifyPayMethod.Contains("shopify"))
                             _FinPaymentmethod = string.Concat("SHOPIFY-", ShopifyPayMethod).Length > 30 ? string.Concat("SHFY-", ShopifyPayMethod.ToUpper())
                              : string.Concat("SHOPIFY-", ShopifyPayMethod.ToUpper());
                         else
                             _FinPaymentmethod = ShopifyPayMethod.ToUpper();

                         if (string.IsNullOrEmpty(_FinPaymentmethod))
                             _FinPaymentmethod = "SHOPIFY";*/

                        if (ShopifyPayMethod.Contains("Bread"))
                            _FinPaymentmethod = "BREAD";
                        else if (ShopifyPayMethod.Contains("Afterpay"))
                            _FinPaymentmethod = "AFTERPAY";
                        else if (ShopifyPayMethod.Contains("paypal"))
                            _FinPaymentmethod = "PAYPAL";

                        else if (ShopifyPayMethod.Contains("shopify"))
                            _FinPaymentmethod = "SHOPIFY";


                        ValidatePaymentType(_FinPaymentmethod);

                        decimal _TotTransactionsAmount = Helper.DataTableOK(paymentItems) ? Convert.ToDecimal(paymentItems.Compute("sum(amount)", "")) : 0;
                        DataRow drShopifyPayment = paymentItems.NewRow();
                        if (_TotTransactionsAmount < transaction.Amount)
                        {
                            DateTimeOffset PaydateTimeOffset = DateTimeOffset.Parse(transaction.CreatedAt.ToString(), null, DateTimeStyles.RoundtripKind);
                            drShopifyPayment["date"] = PaydateTimeOffset.DateTime; //DateTime.Now;
                            drShopifyPayment["method"] = _FinPaymentmethod;
                            drShopifyPayment["amount"] = transaction.Amount;
                        }

                        if (transaction.Gateway.ToString().Contains("gift_card"))
                        {

                            drShopifyPayment["Curr_Type"] = transaction.Currency;
                            drShopifyPayment["CreditAmt"] = transaction.Amount;
                            drShopifyPayment["EnteredAmt"] = transaction.Amount;
                            if (transaction.Receipt != null)
                            {
                                Newtonsoft.Json.Linq.JObject receipt = Newtonsoft.Json.Linq.JObject.Parse(transaction.Receipt.ToString());
                                var giftCardId = receipt["gift_card_id"].ToString().Trim();
                                var giftCardLastCharacters = receipt["gift_card_last_characters"].ToString().Trim();
                                var GiftCardNo = string.Concat(giftCardId, '-', giftCardLastCharacters.ToString().ToUpper());


                                if (!IsValidGiftCard(GiftCardNo))
                                {

                                    Helper.GetSqlData(@"insert into StoreCreditVoucher (CreditNo,Cust_Code,Amount,AvailableAmt,Date,IsGiftCert,UserGCNo,Style,IsShopify) 
                                            values (@CreditNo,@Cust_Code,@Amount,@AvailableAmt,@Date,@IsGiftCert,@UserGCNo,@Style,@IsShopify)",
                                            "@CreditNo", "",
                                            "@Cust_Code", _ShopifyCustId,
                                            "@Amount", (transaction.Amount * -1).ToString(),
                                            "@AvailableAmt", (transaction.Amount * -1).ToString(),
                                            "@Date", DateTime.Now.ToString(),
                                            "@IsGiftCert", Convert.ToString(1),
                                            "@UserGCNo", GiftCardNo,
                                            "@Style", GiftCardNo,
                                            "@IsShopify", Convert.ToString(1));


                                    Helper.GetSqlData(@"insert into StoreCreditVoucherHistory(CreditNo,Cust_Code,Amount,Used_Amt,Bal_Amt,Inv_No,inv_Date,Style,from_Shopify)
                                            values (@CreditNo,@Cust_Code,@Amount,@Used_Amt,@Bal_Amt,@Inv_No,@inv_Date,@Style,@from_Shopify)",
                                            "@CreditNo", "",
                                            "@Cust_Code", _ShopifyCustId,
                                            "@Amount", (transaction.Amount * -1).ToString(),
                                            "@Used_Amt", Convert.ToString(0),
                                            "@Bal_Amt", (transaction.Amount * -1).ToString(),
                                            "@Inv_No", "",
                                            "@inv_Date", DateTime.Now.ToString(),
                                            "@Style", GiftCardNo,
                                            "@from_Shopify", Convert.ToString(1));

                                }


                                drShopifyPayment["style"] = string.Concat(giftCardId, '-', giftCardLastCharacters.ToString().ToUpper());
                                drShopifyPayment["NOTE"] = string.Concat(giftCardId, '-', giftCardLastCharacters.ToString().ToUpper());

                            }
                            if (drShopifyPayment["method"].ToString() != "")
                                paymentItems.Rows.Add(drShopifyPayment);
                            paymentItems.AcceptChanges();

                        }
                        else
                        {
                            if (drShopifyPayment["method"].ToString() != "")
                                paymentItems.Rows.Add(drShopifyPayment);
                            paymentItems.AcceptChanges();
                        }
                    }

                }
            }

            InvoiceModel.SALES_TAX = _WebSalesTax;
            InvoiceModel.SNH = _WebSnh;
            String xmlInvoice = Helper.GetDataTableXML("InvoiceItems", invoiceItems);
            String xmlPayment = Helper.GetDataTableXML("PaymentItems", paymentItems);
            String xmlDiscount = Helper.GetDataTableXML("DiscountItems", discountItems);

            bool csuccess = AddInvoice(InvoiceModel, xmlInvoice, xmlPayment, xmlDiscount, out out_inv_no, false, false, false, Helper.StoreCodeInUse1, order.OrderNumber.ToString());
            if (csuccess)
                dtDownloadedOrders.Rows.Add(order.OrderNumber.ToString(), out_inv_no);
            return csuccess;
        }
        public static async Task<string> GetStoreNameAsync()
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Add("X-Shopify-Access-Token", Helper.ShopifySecret);
                client.BaseAddress = new Uri(Helper.ShopifyURL);

                var url = "/admin/api/2023-01/shop.json";
                var response = await client.GetAsync(url);

                // Log the full response for debugging
                var responseContent = await response.Content.ReadAsStringAsync();
                //Helper.AddKeepRec($"Response for {_ShopifyUrl}: {responseContent}", null, "Admin");

                if (!response.IsSuccessStatusCode)
                {
                    throw new HttpRequestException($"Response status code does not indicate success: {response.StatusCode} ({response.ReasonPhrase}).");
                }

                var jsonResponse = JsonDocument.Parse(responseContent);
                return jsonResponse.RootElement.GetProperty("shop").GetProperty("name").GetString();
            }
        }
        public static string GetNextInvoice()
        {
            return Helper.GetNextSeqNo("Invoice", "Inv_no", "100000", "", "1", "").PadLeft(6, ' ');
        }
        public async Task<bool> DownloadShopifyOrders(DateTime fromdate, DateTime todate, string jobId)
        {
            bool success = true;
            _storeName = await GetStoreNameAsync();

            dtDownloadedOrders = new DataTable();
            dtDownloadedOrders.Columns.Add("ORDER_NO", typeof(string));
            dtDownloadedOrders.Columns.Add("INVOICE_NO", typeof(string));

            string Nextinvoice = Helper.GetNextSeqNo("Invoice", "Inv_no", "100000", "", "1", "").PadLeft(6, ' ');
            HttpWebResponse response = null;

            Stream dataStream = null;
            StreamReader reader = null;

            try
            {
                IList<ShopifySharp.Order> orderList = new List<ShopifySharp.Order>();
                System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Ssl3 | System.Net.SecurityProtocolType.Tls | System.Net.SecurityProtocolType.Tls11 | System.Net.SecurityProtocolType.Tls12;

                //string startdate = DateTime.SpecifyKind(this.dateFrom.Value, DateTimeKind.Utc).ToString("o");
                //string enddate = DateTime.SpecifyKind(this.dateTo.Value, DateTimeKind.Utc).ToString("o");

                DateTime startDate = fromdate;
                DateTime endDate = todate;

                OrderService Ord_Service = new ShopifySharp.OrderService(Helper.ShopifyURL, Helper.ShopifySecret);
                List<ShopifySharp.Order> lstOrders = new List<ShopifySharp.Order>();
                var totalOrders = await Ord_Service.CountAsync();
                var startDateOffset = new DateTime(startDate.Year, startDate.Month, startDate.Day, 00, 00, 00, DateTimeKind.Utc); // Start of the day
                var endDateOffset = new DateTime(endDate.Year, endDate.Month, endDate.Day, 23, 59, 59, DateTimeKind.Utc);  // End of the day
                //var endDate1 = DateTime.Now;
                /* var page = await Ord_Service.ListAsync(new OrderListFilter { Limit = 250 });*/
                var page = await Ord_Service.ListAsync(new OrderListFilter
                {
                    Limit = 250,
                    CreatedAtMin = startDateOffset,
                    CreatedAtMax = endDateOffset,
                    //FinancialStatus="Paid",
                    //FulfillmentStatus= "unfulfilled"
                    Status = "any"
                });
                while (true)
                {
                    lstOrders.AddRange(page.Items);
                    if (!page.HasNextPage)
                        break;
                    page = await Ord_Service.ListAsync(page.GetNextPageFilter());
                }

                int totalcount = lstOrders.Count, counter = 0;

                DataTable dtShopifyOrderNos = GetShopifyOrderNumbers();
                if (!Helper.DataTableOK(dtShopifyOrderNos))
                    dtShopifyOrderNos.Rows.Add("test");

                var existingOrderNumbers = dtShopifyOrderNos.AsEnumerable().Select(row => row.Field<string>("SHPY_ORDERNO")).ToList();
                var FinallistOrders = default(List<ShopifySharp.Order>);
                if (existingOrderNumbers != null && existingOrderNumbers.Count > 0)
                {
                    FinallistOrders = (List<Order>)lstOrders.Where(order => !existingOrderNumbers.Contains(order.OrderNumber.ToString())).ToList();
                }
                else
                {
                    FinallistOrders = lstOrders;
                }
                BackgroundJobProgress.SetTotal(jobId, FinallistOrders.Count);
                if (FinallistOrders.Count == 0)
                {
                    TotalOrdersProcessed = 0;
                    return true;
                }
                bool finished = false;
                foreach (ShopifySharp.Order order in FinallistOrders)
                {
                    await AddNewInvoice(order);              // heavy work
                    BackgroundJobProgress.Increment(jobId); // increment AFTER
                }

                if (!unattended)
                {
                    if (Helper.DataTableOK(dtDownloadedOrders))
                    {
                        TotalOrdersProcessed = dtDownloadedOrders.Rows.Count;
                    }
                }
            }
            catch (Exception ex)
            {
                BackgroundJobProgress.SetError(jobId, ex.Message);
                success = false;
                return false;
            }
            finally
            {
                // Cleanup the streams and the response.
                if (reader != null)
                {
                    reader.Close();
                    dataStream.Close();
                    response.Close();
                }
            }
            return success;
        }

        private DataRow CheckCustomerExists(double tel1, double tel2, string cust_acc, string email)
        {
            DataTable dataTable = new DataTable();
            using (SqlDataAdapter SqlDataAdapter = new SqlDataAdapter())
            {
                // Create the command and set its properties
                SqlDataAdapter.SelectCommand = new SqlCommand();
                SqlDataAdapter.SelectCommand.Connection = new SqlConnection(Helper.connString);
                SqlDataAdapter.SelectCommand.CommandType = CommandType.Text;
                // Assign the SQL to the command object

                if (tel1 > 0 && tel2 > 0)
                    SqlDataAdapter.SelectCommand.CommandText = string.Format(@"select acc,bill_acc,name,tel,email from customer where tel  = {0} or tel = {1}", tel1, tel2);
                else if (tel1 > 0 && tel2 == 0)
                    SqlDataAdapter.SelectCommand.CommandText = string.Format(@"select acc,bill_acc,name,tel,email from customer where tel = {0}", tel1);
                else if (tel1 == 0 && tel2 > 0)
                    SqlDataAdapter.SelectCommand.CommandText = string.Format(@"select acc,bill_acc,name,tel,email from customer where tel = {0}", tel2);

                else if (!string.IsNullOrEmpty(email))
                    SqlDataAdapter.SelectCommand.CommandText = string.Format(@"select acc,bill_acc,name,tel,email from customer where email = '{0}'", email);
                else
                    SqlDataAdapter.SelectCommand.CommandText = string.Format(@"select acc,bill_acc,name,tel,email from customer where acc = '{0}'", cust_acc);


                // Fill the datatable from adapter
                SqlDataAdapter.Fill(dataTable);
            }
            return Helper.GetRowOne(dataTable);
        }

        public static decimal? GetPhoneNumber(string phone)
        {
            if (string.IsNullOrWhiteSpace(phone))
                return null;

            // Remove all non-numeric characters
            string digits = Regex.Replace(phone, @"\D", "");

            if (string.IsNullOrEmpty(digits))
                return null;

            // If 11 digits and starts with country code 1, strip it
            if (digits.Length == 11 && digits.StartsWith("1"))
                digits = digits.Substring(1);

            // Take only first 10 digits if longer
            if (digits.Length > 10)
                digits = digits.Substring(0, 10);

            decimal result;
            if (decimal.TryParse(digits, out result))
                return result;

            return null;
        }
        public static DataTable GetInvoiceItems(string invno, bool includeinvoiceno = false, bool is_return = false, bool iSVatInclude = false)
        {
            DataTable dataTable = new DataTable();

            using (SqlDataAdapter dataAdapter = new SqlDataAdapter())
            {
                // Create the command and set its properties
                dataAdapter.SelectCommand = new SqlCommand();
                dataAdapter.SelectCommand.Connection = new SqlConnection(Helper.connString);
                dataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                dataAdapter.SelectCommand.CommandText = "GetInvoiceItems_SingerShopify"; // common sp 

                // Add the parameter to the parameter collection
                dataAdapter.SelectCommand.Parameters.AddWithValue("@INV_NO", invno);
                dataAdapter.SelectCommand.Parameters.AddWithValue("@INCLUDEINV_NO", includeinvoiceno ? 1 : 0);
                dataAdapter.SelectCommand.Parameters.AddWithValue("@IS_RETURN", is_return ? 1 : 0);
                dataAdapter.SelectCommand.Parameters.AddWithValue("@iSVatInclude", iSVatInclude ? 1 : 0);

                // Fill the datatable From adapter
                dataAdapter.Fill(dataTable);

                return dataTable;
            }
        }
        public static DataTable GetInvoiceDiscount(string inv_no, bool iSRepair = false)
        {
            DataTable dataTable = new DataTable();

            using (SqlDataAdapter dataAdapter = new SqlDataAdapter())
            {
                // Create the command and set its properties
                dataAdapter.SelectCommand = new SqlCommand();
                dataAdapter.SelectCommand.Connection = new SqlConnection(Helper.connString);
                dataAdapter.SelectCommand.CommandType = CommandType.Text;
                dataAdapter.SelectCommand.CommandText = iSRepair ? @"select Inv_no,trim(Discount) Discount,Amount from Invoice_Discounts where inv_no = trim(@inv_no)" : @"select Inv_no,trim(Discount) Discount,Amount from Invoice_Discounts where RIGHT('     '+ CONVERT(VARCHAR,ltrim(rtrim(inv_no))),6) =RIGHT('     '+ CONVERT(VARCHAR,ltrim(rtrim(@inv_no))),6)";
                dataAdapter.SelectCommand.Parameters.AddWithValue("@inv_no", inv_no);

                // Fill the datatable From adapter
                dataAdapter.Fill(dataTable);

                // Get the datarow from the table
                return dataTable;
            }
        }

        private bool CreateNewStyle(string enteredStyle, string productTitle, decimal ItemPrice)
        {

            enteredStyle = Helper.InvStyle(enteredStyle);

            DiamondInventoryModel diamondinvModel = new DiamondInventoryModel()
            {
                STYLE = enteredStyle,
                DATE = DateTime.Now,
                DESC = productTitle,
                PIECE = "Y",
                LONGDESC = productTitle,
                PRICE = 0,
                NOT_STOCK = true,
                UPC = string.Empty,
                brand = string.Empty,
                CATEGORY = string.Empty,
                SUBCAT = string.Empty,
                METAL = string.Empty,
                VND_STYLE = string.Empty,
                CAST_NAME = string.Empty,
                CAST_CODE = string.Empty,
                center_type = string.Empty,
                SHAPE = string.Empty,
                center_size = string.Empty,
                GOLD_WT = 0,
                IS_DWT = false,
                LABOR_GR = 0,
                GOLDBASE = 0,
                COSTPER = 0,
                SLVR_WT = 0,
                SLVR_PRICE = 0,
                Silvper = 0,
                Plat_wt = 0,
                Plat_price = 0,
                Platper = 0,
                MIN_STOK = 0,
                IN_STOCK = 0,
                NOTE = string.Empty,
                NOTE1 = string.Empty,
                OVER_WT = false,
                STONE_WT = 0,
                DQLTY = string.Empty,
                COLOR_WT = 0,
                CQLTY = string.Empty,
                tag_info1 = string.Empty,
                tag_info2 = string.Empty,
                tag_info3 = string.Empty,
                CASTING = 0,
                SETTING = 0,
                //SETTING = settingcost(),
                ROD_CHRG = 0,
                MISC = 0,
                POLISH = 0,
                MISC1 = 0,
                LASER = 0,
                PCOMPLETE = false,
                COST = ItemPrice,
                DUTIES = 0,
                T_COST = ItemPrice,
                MULTI = 1,
                FIND1 = string.Empty,
                FIND2 = string.Empty,
                FIND3 = string.Empty,
                FIND_QTY1 = 0,
                FIND_QTY2 = 0,
                FIND_QTY3 = 0,
                FIND_COST = 0,
                SET_TYPE1 = string.Empty,
                SET1 = 0,
                SET_COST1 = 0,
                SET_TYPE2 = string.Empty,
                SET2 = 0,
                SET_COST2 = 0,
                SET_TYPE3 = string.Empty,
                SET3 = 0,
                SET_COST3 = 0,
                SET_TYPE4 = string.Empty,
                SET4 = 0,
                SET_COST4 = 0,
                SET_TYPE5 = string.Empty,
                SET5 = 0,
                SET_COST5 = 0,
                SET_TYPE6 = string.Empty,
                SET6 = 0,
                SET_COST6 = 0,
                SET_TYPE7 = string.Empty,
                SET7 = 0,
                SET_COST7 = 0,
                SET_TYPE8 = string.Empty,
                SET8 = 0,
                SET_COST8 = 0,
                Attrib1 = string.Empty,
                Attrib2 = string.Empty,
                Attrib3 = string.Empty,
                Attrib4 = string.Empty,
                Attrib5 = string.Empty,
                Attrib6 = string.Empty,
                Attrib7 = string.Empty,
                Attrib8 = string.Empty,
                Attrib9 = string.Empty,
                Attrib10 = string.Empty,
                Attrib11 = string.Empty,
                Attrib12 = string.Empty,
                Attrib13 = string.Empty,
                Attrib14 = string.Empty,
                Attrib15 = string.Empty,
                Attrib16 = string.Empty,
                Attrib17 = string.Empty,
                Attrib18 = string.Empty,
                COLOR_COST = 0,
                DIA_COST = 0,
                GOLD_COST = 0,
                GEM_COST = 0,
                RHODIUM = "N",
                SIZE = string.Empty,
                IS_ACTIVE = 1,
                FIELDTEXT1 = string.Empty,
                FIELDTEXT2 = string.Empty,
                FIELDTEXT3 = string.Empty,
                FIELDVALUE1 = 0,
                FIELDVALUE2 = 0,
                FIELDVALUE3 = 0,
                IMG_STYLE = string.Empty,
                STYLEATTRTEXT = string.Empty,
                //USE_SN = false,
            };
            string error;

            DataTable dtStoneData = Helper.GetStoneGridData(enteredStyle);

            StylesModel stylesModel = new StylesModel();
            return stylesModel.AddAStyle(diamondinvModel, "insert", Helper.LoggedUser, Helper.StoreCodeInUse1, dtStoneData == null ? string.Empty : Helper.GetDataTableXML("STONES", dtStoneData), null, out error, null, null);
        }

        public static void ValidatePaymentType(string Type)
        {
            Helper.GetSqlData("EXEC ValidatePaymentType @PaymentType", "@PaymentType", Type.ToString());
        }
        public static bool IsValidGiftCard(string GcNo)
        {
            return Helper.DataTableOK(Helper.GetSqlData("select * from StoreCreditVoucher where LTRIM(RTRIM(UserGCNo))=@GcNo", "@GcNo", GcNo.Trim()));
        }

        public static bool AddInvoice(InvoiceModel invoice, string invoiceItems, string paymentItems, string discountItems, out string out_inv_no, bool ispayment = false, bool is_update = false, bool is_return = false, string storecodeinuse = "", string ShopifyOrderNo = "")
        {
            using (SqlCommand dbCommand1 = new SqlCommand())
            {
                dbCommand1.Connection = new SqlConnection(Helper.connString);
                dbCommand1.CommandType = CommandType.StoredProcedure;
                dbCommand1.CommandText = "UPDATE_INVOICE";
                dbCommand1.CommandTimeout = 6000;
                if (!is_return)
                    dbCommand1.Parameters.AddWithValue("@INV_NO", invoice.INV_NO);
                else
                    dbCommand1.Parameters.AddWithValue("@INV_NO", invoice.RET_INV_NO);
                dbCommand1.Parameters.AddWithValue("@From_Repair", false);
                dbCommand1.CommandTimeout = 0;
                dbCommand1.Connection.Open();
                var rowAffected = dbCommand1.ExecuteNonQuery();
                dbCommand1.Connection.Close();
            }

            using (SqlCommand dbCommand = new SqlCommand())
            {
                dbCommand.Connection = new SqlConnection(Helper.connString);
                dbCommand.CommandType = CommandType.StoredProcedure;
                dbCommand.CommandText = "MakeInvoiceFromShopifyOrders";
                dbCommand.CommandTimeout = 12000;
                Object invoicedate;
                if (invoice.DATE == null)
                    invoicedate = DBNull.Value;
                else
                    invoicedate = invoice.DATE;

                dbCommand.Parameters.AddWithValue("@INV_NO", invoice.INV_NO);
                dbCommand.Parameters.AddWithValue("@BACC", invoice.BACC);
                dbCommand.Parameters.AddWithValue("@ACC", invoice.ACC);
                dbCommand.Parameters.AddWithValue("@DATE", invoicedate);
                dbCommand.Parameters.AddWithValue("@PON", invoice.PON);
                dbCommand.Parameters.AddWithValue("@GR_TOTAL", invoice.GR_TOTAL);
                dbCommand.Parameters.AddWithValue("@NAME", invoice.NAME);
                dbCommand.Parameters.AddWithValue("@ADDR1", invoice.ADDR1);
                dbCommand.Parameters.AddWithValue("@ADDR2", invoice.ADDR2);
                dbCommand.Parameters.AddWithValue("@ADDR3", invoice.ADDR3);
                dbCommand.Parameters.AddWithValue("@CITY", invoice.CITY);
                dbCommand.Parameters.AddWithValue("@STATE", invoice.STATE);
                dbCommand.Parameters.AddWithValue("@ZIP", invoice.ZIP);
                dbCommand.Parameters.AddWithValue("@COUNTRY", invoice.COUNTRY);
                dbCommand.Parameters.AddWithValue("@OPERATOR", invoice.OPERATOR);
                dbCommand.Parameters.AddWithValue("@SALESMAN1", invoice.SALESMAN1);
                dbCommand.Parameters.AddWithValue("@SALESMAN2", invoice.SALESMAN2);
                dbCommand.Parameters.AddWithValue("@STORE_NO", invoice.STORE_NO);
                dbCommand.Parameters.AddWithValue("@TAXABLE", invoice.TAXABLE == true ? 1 : 0);
                dbCommand.Parameters.AddWithValue("@SALES_TAX", invoice.SALES_TAX);
                dbCommand.Parameters.AddWithValue("@TRADEIN", invoice.TRADEIN == true ? 1 : 0);
                dbCommand.Parameters.AddWithValue("@TRADEINAMT", invoice.TRADEINAMT);
                dbCommand.Parameters.AddWithValue("@TRADEINDESC", invoice.TRADEINDESC);
                dbCommand.Parameters.AddWithValue("@SPECIAL", invoice.SPECIAL == true ? 1 : 0);
                dbCommand.Parameters.AddWithValue("@PICKED", invoice.PICKED == true ? 1 : 0);
                dbCommand.Parameters.AddWithValue("@TAXINCLUDED", invoice.TAXINCLUDED == true ? 1 : 0);
                dbCommand.Parameters.AddWithValue("@ISPAYMENT", ispayment == true ? 1 : 0);
                dbCommand.Parameters.AddWithValue("@IS_RETURN", is_return == true ? 1 : 0);
                dbCommand.Parameters.AddWithValue("@IS_UPDATE", is_update == true ? 1 : 0);
                dbCommand.Parameters.AddWithValue("@PCNAME", invoice.SYSTEMNAME);
                dbCommand.Parameters.AddWithValue("@UserGCNO", invoice.USERGCNO);
                dbCommand.Parameters.AddWithValue("@DEDUCTION", invoice.DEDUCTION);
                dbCommand.Parameters.AddWithValue("@IS_GLENN", invoice.IS_GLENN);
                dbCommand.Parameters.AddWithValue("@CASH_REG_CODE", invoice.CASH_REG_CODE);
                dbCommand.Parameters.AddWithValue("@CASH_REG_STORE", invoice.CASH_REG_STORE);
                dbCommand.Parameters.AddWithValue("@Charity", invoice.charity);
                dbCommand.Parameters.AddWithValue("@Charity_Amount", invoice.charity_amount);
                dbCommand.Parameters.AddWithValue("@Sales_Tax_Rate", invoice.Sales_Tax_Rate);
                dbCommand.Parameters.AddWithValue("@SALESMAN3", invoice.SALESMAN3);
                dbCommand.Parameters.AddWithValue("@SALESMAN4", invoice.SALESMAN4);
                dbCommand.Parameters.AddWithValue("@storecodeinuse", storecodeinuse);
                dbCommand.Parameters.AddWithValue("@SNH", invoice.SNH);

                dbCommand.Parameters.AddWithValue("@Bank", invoice.BANK);
                dbCommand.Parameters.AddWithValue("@Check_no", invoice.CHECK_NO);
                dbCommand.Parameters.AddWithValue("@Print_Check", invoice.print_Check);
                dbCommand.Parameters.AddWithValue("@Registry", invoice.Registry);

                dbCommand.Parameters.AddWithValue("@COMISH1", invoice.comish1);
                dbCommand.Parameters.AddWithValue("@COMISH2", invoice.comish2);
                dbCommand.Parameters.AddWithValue("@COMISH3", invoice.comish3);
                dbCommand.Parameters.AddWithValue("@COMISH4", invoice.comish4);

                dbCommand.Parameters.AddWithValue("@COMISHAMOUNT1", invoice.COMISHAMOUNT1);
                dbCommand.Parameters.AddWithValue("@COMISHAMOUNT2", invoice.COMISHAMOUNT2);
                dbCommand.Parameters.AddWithValue("@COMISHAMOUNT3", invoice.COMISHAMOUNT3);
                dbCommand.Parameters.AddWithValue("@COMISHAMOUNT4", invoice.COMISHAMOUNT4);

                dbCommand.Parameters.AddWithValue("@AMOUNT_TYPE", invoice.AMOUNT_TYPE == 2 ? 1 : 0);

                dbCommand.Parameters.AddWithValue("@PaymentFrequency", invoice.Payment_Frequency);
                dbCommand.Parameters.AddWithValue("@FinalPaymentDueDate", invoice.Final_Payment_Due_Date);

                dbCommand.Parameters.AddWithValue("@ISByAmount", invoice.ISByAmount);
                dbCommand.Parameters.AddWithValue("@Dept", invoice.DEPT);
                dbCommand.Parameters.AddWithValue("@DISC_PERCENT", invoice.DISC_PERCENT);

                dbCommand.Parameters.AddWithValue("@Sales_fee_rate", invoice.Sales_fee_Rate);
                dbCommand.Parameters.AddWithValue("@Sales_fee_amount", invoice.Sales_fee_Amount);
                dbCommand.Parameters.AddWithValue("@M_Note", invoice.NOTE);
                dbCommand.Parameters.AddWithValue("@SalesTax1", invoice.SalesTax1);
                dbCommand.Parameters.AddWithValue("@SalesTax2", invoice.SalesTax2);
                dbCommand.Parameters.AddWithValue("@SalesTax3", invoice.SalesTax3);
                dbCommand.Parameters.AddWithValue("@ScrapLogno", invoice.ScrapLogno);
                dbCommand.Parameters.AddWithValue("@NoTaxReason", invoice.noTax_reason);
                dbCommand.Parameters.Add("@OUT_INV_NO", SqlDbType.VarChar, 10).Direction = ParameterDirection.Output;

                dbCommand.Parameters.AddWithValue("@EMI_INTERESTAMOUNT", invoice.EMI_INTERESTAMOUNT);
                dbCommand.Parameters.AddWithValue("@EMI_MONTHLYPAYMENT", invoice.EMI_MONTHLYPAYMENT);
                dbCommand.Parameters.AddWithValue("@EMI_NOOFINSTALLMENTS", invoice.EMI_NOOFINSTALLMENTS);
                dbCommand.Parameters.AddWithValue("@EMI_RATEOFINTEREST", invoice.EMI_RATEOFINTEREST);
                dbCommand.Parameters.AddWithValue("@IS_INSTALLMENT", invoice.IS_INSTALLMENT == true ? 1 : 0);

                dbCommand.Parameters.AddWithValue("@SHIP_BY", invoice.SHIP_BY);
                dbCommand.Parameters.AddWithValue("@WEIGHT", invoice.WEIGHT);
                dbCommand.Parameters.AddWithValue("@INSURED", invoice.INSURED);
                dbCommand.Parameters.AddWithValue("@SHIPTYPE", invoice.SHIPTYPE);
                dbCommand.Parameters.AddWithValue("@TaxState", invoice.TaxState);
                dbCommand.Parameters.AddWithValue("@iSNotPickedUp", invoice.iSNotPickedUp == true ? 1 : 0);
                dbCommand.Parameters.AddWithValue("@Gold_Price", invoice.Gold_Price);
                dbCommand.Parameters.AddWithValue("@Silver_Price", invoice.Silver_Price);
                dbCommand.Parameters.AddWithValue("@Plat_Price", invoice.Plat_Price);
                dbCommand.Parameters.AddWithValue("@DoNotChangePaymentStore", invoice.DoNotChangePaymentStore);
                dbCommand.Parameters.AddWithValue("@iSVatInclude", invoice.iSVatInclude);
                dbCommand.Parameters.AddWithValue("@GivenChange", invoice.GivenChange);
                dbCommand.Parameters.AddWithValue("@iSCompnayName2", invoice.iSCompanyName2 == true ? 1 : 0);
                dbCommand.Parameters.AddWithValue("@UPSTRAK", invoice.UPSTRAK);
                //dbCommand.Parameters.AddWithValue("@ShopifyOrderNumber", string.Concat("T", ShopifyOrderNo));
                dbCommand.Parameters.AddWithValue("@ShopifyOrderNumber", ShopifyOrderNo);
                dbCommand.Parameters.AddWithValue("@ShopifyStoreName", invoice.ShopifyStoreNo);


                SqlParameter parameter = new SqlParameter();
                parameter.ParameterName = "@TBLPOINVOICEITEMS";
                parameter.SqlDbType = System.Data.SqlDbType.Xml;
                parameter.Value = invoiceItems;
                dbCommand.Parameters.Add(parameter);

                parameter = new SqlParameter();
                parameter.ParameterName = "@TBLPAYMENTITEMS";
                parameter.SqlDbType = System.Data.SqlDbType.Xml;
                parameter.Value = paymentItems;
                dbCommand.Parameters.Add(parameter);

                parameter = new SqlParameter();
                parameter.ParameterName = "@TBLDISCOUNTITEMS";
                parameter.SqlDbType = System.Data.SqlDbType.Xml;
                parameter.Value = discountItems;
                dbCommand.Parameters.Add(parameter);

                // Open the connection, execute the query and close the connection
                dbCommand.Connection.Open();
                var rowsAffected = dbCommand.ExecuteNonQuery();
                out_inv_no = Convert.ToString(dbCommand.Parameters["@OUT_INV_NO"].Value);

                dbCommand.Connection.Close();
                return rowsAffected > 0;
            }
        }
    }
}
