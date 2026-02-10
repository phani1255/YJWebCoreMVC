using Microsoft.Data.SqlClient;
using System.Data;
using YJWebCoreMVC.Models;

namespace YJWebCoreMVC.Services
{
    public class InvoiceService
    {
        private readonly ConnectionProvider _connectionProvider;
        private readonly HelperService _helper;

        public InvoiceService(ConnectionProvider connectionProvider, HelperService helper)
        {
            _connectionProvider = connectionProvider;
            _helper = helper;
        }

        public DataTable GetInvoiceItems(string invno, bool includeinvoiceno = false, bool is_return = false, bool iSVatInclude = false)
        {
            DataTable dataTable = new DataTable();
            using (SqlDataAdapter dataAdapter = new SqlDataAdapter())
            {
                dataAdapter.SelectCommand = new SqlCommand();
                dataAdapter.SelectCommand.Connection = new SqlConnection(_connectionProvider.GetConnectionString());
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
                dataAdapter.SelectCommand.Connection = new SqlConnection(_connectionProvider.GetConnectionString());
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

        public DataTable GetInvoiceDiscount(string inv_no, bool iSRepair = false)
        {
            DataTable dataTable = new DataTable();
            using (SqlDataAdapter dataAdapter = new SqlDataAdapter())
            {
                dataAdapter.SelectCommand = new SqlCommand();
                dataAdapter.SelectCommand.Connection = new SqlConnection(_connectionProvider.GetConnectionString());
                dataAdapter.SelectCommand.CommandType = CommandType.Text;
                dataAdapter.SelectCommand.CommandText = iSRepair ?
                    @"select Inv_no,trim(Discount) Discount,Amount from Invoice_Discounts where inv_no = trim(@inv_no)" :
                    @"select Inv_no,trim(Discount) Discount,Amount from Invoice_Discounts where RIGHT('     '+ CONVERT(VARCHAR,ltrim(rtrim(inv_no))),6) =RIGHT('     '+ CONVERT(VARCHAR,ltrim(rtrim(@inv_no))),6)";
                dataAdapter.SelectCommand.Parameters.AddWithValue("@inv_no", inv_no);
                dataAdapter.Fill(dataTable);
                return dataTable;
            }
        }

        public List<object> GetVendors(string term)
        {
            List<object> vendors = new List<object>();
            using (SqlConnection conn = new SqlConnection(_connectionProvider.GetConnectionString()))
            {
                string query = "SELECT TOP 10 acc, name, tel, ISNULL(our_acct, '') as our_acct FROM vendors WHERE acc LIKE @SearchTerm OR name LIKE @SearchTerm";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@SearchTerm", term + "%");
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            vendors.Add(new
                            {
                                Acc = reader["acc"].ToString(),
                                Name = reader["name"].ToString(),
                                Tel = reader["tel"].ToString(),
                                OurAcct = reader["our_acct"].ToString()
                            });
                        }
                    }
                }
            }
            return vendors;
        }

        public List<object> GetStyles(string term)
        {
            List<object> styles = new List<object>();
            using (SqlConnection conn = new SqlConnection(_connectionProvider.GetConnectionString()))
            {
                string query = "SELECT TOP 10 style, [Desc], Price, Isnull(cast_code,'') Vendor, ISNULL(VND_STYLE,'') Manufacturer, PopUpNote, not_stock NotStock, item_type ItemType FROM Styles WHERE style LIKE @SearchTerm";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@SearchTerm", term + "%");
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            bool notStock = false;
                            try
                            {
                                object notStockObj = reader["NotStock"];
                                if (notStockObj != DBNull.Value && notStockObj != null)
                                {
                                    notStock = Convert.ToBoolean(notStockObj);
                                }
                            }
                            catch
                            {
                                notStock = false;
                            }

                            styles.Add(new
                            {
                                StyleName = reader["Style"].ToString(),
                                Description = reader["Desc"].ToString(),
                                TagPrice = Convert.ToDecimal(reader["Price"]).ToString("0.00"),
                                Price = Convert.ToDecimal(reader["Price"]).ToString("0.00"),
                                Vendor = reader["Vendor"].ToString(),
                                Manufacturer = reader["Manufacturer"].ToString(),
                                PopUpNote = reader["PopUpNote"].ToString(),
                                ItemType = reader["ItemType"].ToString(),
                                NotStock = notStock
                            });
                        }
                    }
                }
            }
            return styles;
        }

        public object GetStyleDetails(string style)
        {
            object styleDetails = null;
            using (SqlConnection conn = new SqlConnection(_connectionProvider.GetConnectionString()))
            {
                string query = "SELECT TOP 1 style, [Desc], Price, no_tax, popupnote, not_stock NotStock, item_type ItemType FROM Styles WHERE style = @SearchTerm";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@SearchTerm", style);
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            object notStockValue = reader["NotStock"];
                            bool notStock = false;
                            if (notStockValue != DBNull.Value && notStockValue != null)
                            {
                                notStock = Convert.ToBoolean(notStockValue);
                            }

                            styleDetails = new
                            {
                                isValid = true,
                                StyleName = reader["Style"].ToString(),
                                Description = reader["Desc"].ToString(),
                                TagPrice = Convert.ToDecimal(reader["Price"]).ToString("0.00"),
                                Price = Convert.ToDecimal(reader["Price"]).ToString("0.00"),
                                NO_TAX = Convert.ToBoolean(reader["no_tax"]),
                                popupnote = reader["popupnote"].ToString(),
                                ItemType = reader["ItemType"].ToString(),
                                NotStock = notStock
                            };
                        }
                    }
                }
            }

            if (styleDetails == null)
            {
                styleDetails = new { isValid = false };
            }
            return styleDetails;
        }

        public List<object> GetPaymentTypes()
        {
            List<object> types = new List<object>();
            using (SqlConnection con = new SqlConnection(_connectionProvider.GetConnectionString()))
            {
                string query = "SELECT PaymentType PaymentTypeCode, PaymentType PaymentTypeName FROM PaymentTypes where Hide = 0 ORDER BY PaymentType";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            types.Add(new
                            {
                                Value = reader["PaymentTypeCode"].ToString(),
                                Text = reader["PaymentTypeName"].ToString()
                            });
                        }
                    }
                }
            }
            return types;
        }

        public List<object> GetDiscountTypes()
        {
            List<object> discountTypes = new List<object>();
            string query = "SELECT TRIM(Discount) AS Discount, [percentage], ISNULL(needs_pwd, 0) AS needs_pwd FROM discounts ORDER BY Discount";

            using (SqlConnection conn = new SqlConnection(_connectionProvider.GetConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            discountTypes.Add(new
                            {
                                Value = reader["Discount"].ToString(),
                                Text = reader["Discount"].ToString(),
                                Percentage = reader["percentage"],
                                NeedsPwd = reader["needs_pwd"]
                            });
                        }
                    }
                }
            }
            return discountTypes;
        }

        public List<object> GetSalesReps()
        {
            List<object> reps = new List<object>();
            using (SqlConnection con = new SqlConnection(_connectionProvider.GetConnectionString()))
            {
                string query = "SELECT code RepCode, code RepName FROM SALESMEN ORDER BY code";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            reps.Add(new
                            {
                                Value = reader["RepCode"].ToString(),
                                Text = reader["RepName"].ToString()
                            });
                        }
                    }
                }
            }
            return reps;
        }

        public List<object> GetStores()
        {
            List<object> stores = new List<object>();
            using (SqlConnection con = new SqlConnection(_connectionProvider.GetConnectionString()))
            {
                string query = "SELECT code StoreCode, code StoreName FROM Stores ORDER BY code";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            stores.Add(new
                            {
                                Value = reader["StoreCode"].ToString(),
                                Text = reader["StoreName"].ToString()
                            });
                        }
                    }
                }
            }
            return stores;
        }

        public DataTable GetStatusOfSpecialOrder(DateTime? fromDate, DateTime? toDate, string opt, string stores, string custCode, string salesman)
        {
            using (SqlConnection conn = new SqlConnection(_connectionProvider.GetConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand("GetStatusOfSpecialOrder", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@FromDate", fromDate ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@ToDate", toDate ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Opt", opt ?? "");
                    cmd.Parameters.AddWithValue("@Stores", stores ?? "");
                    cmd.Parameters.AddWithValue("@CustCode", custCode ?? "");
                    cmd.Parameters.AddWithValue("@Salesman", salesman ?? "");

                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        return dt;
                    }
                }
            }
        }

        public bool CancelLayaway(string invno, decimal restockfee, string payments, string loggeduser, string register, string store_no, bool _IsIssueStore = false, string bank = "", string checkNo = "", bool iSSpecial = false)
        {
            using (SqlCommand dbCommand = new SqlCommand())
            {
                dbCommand.Connection = new SqlConnection(_connectionProvider.GetConnectionString());
                dbCommand.CommandType = CommandType.StoredProcedure;
                dbCommand.CommandText = "CancelLayaway";

                dbCommand.Parameters.AddWithValue("@invno", invno);
                dbCommand.Parameters.AddWithValue("@restockfee", restockfee);
                dbCommand.Parameters.AddWithValue("@payments", payments);
                dbCommand.Parameters.AddWithValue("@loggeduser", loggeduser);
                dbCommand.Parameters.AddWithValue("@register", register);
                dbCommand.Parameters.AddWithValue("@store_code", store_no);
                dbCommand.Parameters.AddWithValue("@IsIssueStorePayments", _IsIssueStore);
                dbCommand.Parameters.AddWithValue("@BANK", bank);
                dbCommand.Parameters.AddWithValue("@CHECK_NO", checkNo);
                dbCommand.Parameters.AddWithValue("@iSFromSpecial", iSSpecial);

                dbCommand.Connection.Open();
                var rowsAffected = dbCommand.ExecuteNonQuery();
                dbCommand.Connection.Close();

                return rowsAffected > 0;
            }
        }

        public DataTable CheckValidCheckNo(string check_no, string bank)
        {
            string CommandText = "select CHECKS.*, CAST(BANK.CLRD AS BIT) as CLRD1 from checks with (nolock) LEFT JOIN Bank with (nolock) ON TRIM(Bank.inv_no)=TRIM(CHECKS.TRANSACT) WHERE TRIM(checks.bank)= @bank AND TRIM(CHECKS.check_no) = @chk_no";
            return __helperCommonService.HelperCommon.GetSqlData(CommandText, "@chk_no", check_no.Trim(), "@bank", bank.Trim());
        }

        public object GetSplOrderDetails(string style)
        {
            if (string.IsNullOrEmpty(style))
                return new { error = "Style is required" };

            object result = null;
            using (SqlConnection conn = new SqlConnection(_connectionProvider.GetConnectionString()))
            {
                string query = @"
                SELECT 
                ISNULL(styles.cast_code, '') AS VendorCode,
                ISNULL(styles.price, 0) AS Price,
                ISNULL(styles.vnd_style, '') AS ManufacturerStyle,
                ISNULL(styles.[Desc], '') AS StyleDescription,
                ISNULL(CAST(vendors.tel AS VARCHAR(20)), '') AS VendorPhone,
                ISNULL(vendors.our_acct, '') AS OurAccount
                FROM styles
                LEFT JOIN vendors ON styles.cast_code = vendors.acc
                WHERE styles.style = @Style";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Style", style);
                    conn.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            result = new
                            {
                                VendorCode = reader["VendorCode"].ToString(),
                                Price = Convert.ToDecimal(reader["Price"]),
                                ManufacturerStyle = reader["ManufacturerStyle"].ToString(),
                                VendorPhone = reader["VendorPhone"].ToString(),
                                OurAccount = reader["OurAccount"].ToString(),
                                StyleDescription = reader["StyleDescription"].ToString(),
                            };
                        }
                    }
                }
            }

            return result ?? new { message = "No record found" };
        }

        public List<object> GetAvailableCredits(string custCode, string type)
        {
            using (SqlConnection conn = new SqlConnection(_connectionProvider.GetConnectionString()))
            {
                string query = @"
            SELECT CreditNo, Cust_Code, Amount, AvailableAmt, Date, IsGiftCert, notes, Style
            FROM t_credits
            WHERE Cust_Code = @custCode
              AND IsGiftCert = CASE WHEN @type = 'Gift Card' THEN 1 ELSE 0 END
              AND AvailableAmt > 0";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@custCode", custCode);
                    cmd.Parameters.AddWithValue("@type", type);

                    conn.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    return dt.AsEnumerable().Select(r => new
                    {
                        CreditNo = r["CreditNo"].ToString(),
                        Amount = r["Amount"].ToString(),
                        AvailableAmt = r["AvailableAmt"].ToString(),
                        Date = r["Date"].ToString(),
                        notes = r["notes"].ToString(),
                        Style = r["Style"].ToString()
                    }).Cast<object>().ToList();
                }
            }
        }


        public DataTable GetSpecialOrderDetails(string strInvoiceNo, string sStyle = "")
        {
            try
            {
                using (var connection = new SqlConnection(_connectionProvider.GetConnectionString()))
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandType = CommandType.Text;

                    var query = @"
                SELECT DISTINCT 
                    I.INV_NO, I.DATE,
                    C.ACC AS C_ACC, C.NAME AS C_NAME, C.ADDR1 AS C_ADDR1, C.ADDR12 AS C_ADDR12, 
                    C.CITY1 AS C_CITY1, C.STATE1 AS C_STATE1, C.ZIP1 AS C_ZIP1, C.TEL AS C_TEL,
                    V.ACC AS V_ACC, V.NAME AS V_NAME, V.ADDR11 AS V_ADDR11, V.ADDR12 AS V_ADDR12, 
                    V.CITY1 AS V_CITY1, V.STATE1 AS V_STATE1, V.ZIP1 AS V_ZIP1, V.TEL AS V_TEL,
                    II.VENDOR, II.STYLE, I.STORE_NO, II.MANUFACTURER_NO, II.METAL_TYPE, 
                    II.FINGER_SIZE, II.[DESC], CAST(II.DUEDATE AS DATE) AS DUEDATE, 
                    II.CTR_STN_DIAMENSION, II.PRICE, S.NAME AS SALESMAN1, 
                    II.VendOrderDt, II.ExpDelDt, II.VendConfNo
                FROM IN_ITEMS II
                INNER JOIN INVOICE I ON I.INV_NO = II.INV_NO AND LTRIM(RTRIM(I.INV_NO)) = @INV_NO
                LEFT JOIN CUSTOMER C ON C.ACC = I.ACC
                LEFT JOIN VENDORS V ON V.ACC = II.VENDOR
                LEFT JOIN SALESMEN S ON S.CODE = I.SALESMAN1
                WHERE II.IsSpecialItem = 1";

                    if (!string.IsNullOrWhiteSpace(sStyle))
                    {
                        query += " AND II.STYLE = @Style";
                        command.Parameters.AddWithValue("@Style", sStyle.Trim());
                    }

                    command.CommandText = query;
                    command.Parameters.AddWithValue("@INV_NO", strInvoiceNo.Trim());

                    using (var dataAdapter = new SqlDataAdapter(command))
                    {
                        var dataTable = new DataTable();
                        dataAdapter.Fill(dataTable);
                        return dataTable;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving special order details.", ex);
            }
        }

        public bool AddEditInvoice(InvoiceModel invoice, string invoiceItems, string paymentItems, string discountItems, out string out_inv_no, bool ispayment = false, bool is_update = false, bool is_return = false, string storecodeinuse = "", string ShopifyOrderNo = "", string OrderDeskOrderNo = "")
        {
            out_inv_no = "";

            try
            {
                using (SqlCommand dbCommand1 = new SqlCommand())
                {
                    dbCommand1.Connection = new SqlConnection(_connectionProvider.GetConnectionString());
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
                    dbCommand.Connection = new SqlConnection(_connectionProvider.GetConnectionString());
                    dbCommand.CommandType = CommandType.StoredProcedure;
                    dbCommand.CommandText = "AddEditInvoice";
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

                    dbCommand.Parameters.AddWithValue("@Sales_Tax_Rate", invoice.SalesTaxPercent);
                    dbCommand.Parameters.AddWithValue("@Sales_fee_amount", invoice.SalesFee);
                    dbCommand.Parameters.AddWithValue("@Sales_fee_rate", invoice.SalesFeePercent);

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

                    dbCommand.Parameters.AddWithValue("@M_Note", invoice.NOTE);

                    dbCommand.Parameters.AddWithValue("@Email", invoice.Email);
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
                    dbCommand.Parameters.AddWithValue("@ShopifyOrderNumber", "");
                    dbCommand.Parameters.AddWithValue("@ExpectToShipDate", invoice.ExpetToShipDate);
                    dbCommand.Parameters.AddWithValue("@OrderDesk_OrderNumber", OrderDeskOrderNo);
                    dbCommand.Parameters.AddWithValue("@Source", invoice.Source);

                    SqlParameter parameter = new SqlParameter();
                    parameter.ParameterName = "@TBLPOINVOICEITEMS";
                    parameter.SqlDbType = SqlDbType.Xml;
                    parameter.Value = invoiceItems;
                    dbCommand.Parameters.Add(parameter);

                    parameter = new SqlParameter();
                    parameter.ParameterName = "@TBLPAYMENTITEMS";
                    parameter.SqlDbType = SqlDbType.Xml;
                    parameter.Value = paymentItems;
                    dbCommand.Parameters.Add(parameter);

                    parameter = new SqlParameter();
                    parameter.ParameterName = "@TBLDISCOUNTITEMS";
                    parameter.SqlDbType = SqlDbType.Xml;
                    parameter.Value = discountItems ?? "<DiscountItems></DiscountItems>";
                    dbCommand.Parameters.Add(parameter);

                    // Open the connection, execute the query and close the connection
                    dbCommand.Connection.Open();
                    var rowsAffected = dbCommand.ExecuteNonQuery();
                    out_inv_no = Convert.ToString(dbCommand.Parameters["@OUT_INV_NO"].Value);

                    dbCommand.Connection.Close();
                    return rowsAffected > 0;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private DataRow CheckCustomerExists(double tel1, double tel2, string cust_acc, string email)
        {
            DataTable dataTable = new DataTable();
            using (SqlDataAdapter SqlDataAdapter = new SqlDataAdapter())
            {
                SqlDataAdapter.SelectCommand = new SqlCommand();
                SqlDataAdapter.SelectCommand.Connection = new SqlConnection(_connectionProvider.GetConnectionString());
                SqlDataAdapter.SelectCommand.CommandType = CommandType.Text;

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

                SqlDataAdapter.Fill(dataTable);
            }
            return __helperCommonService.HelperCommon.GetRowOne(dataTable);
        }

        public List<object> GetStylesFromInvoice(string invoiceNo)
        {
            List<object> styles = new List<object>();
            using (SqlConnection conn = new SqlConnection(_connectionProvider.GetConnectionString()))
            {
                string query = @"
                SELECT DISTINCT 
                    ii.style AS StyleName,
                    ii.[desc] AS Description,
                    ii.price AS TagPrice,
                    ii.price AS Price,
                    '' AS Vendor,
                    '' AS Manufacturer,
                    '' AS PopUpNote,
                    item_type AS ItemType,
                    0 AS NotStock
                FROM IN_ITEMS ii
                WHERE ii.inv_no = @InvoiceNo
                AND ii.style IS NOT NULL
                AND ii.style != ''
                ORDER BY ii.style";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@InvoiceNo", invoiceNo.Trim());
                    conn.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            styles.Add(new
                            {
                                StyleName = reader["StyleName"].ToString(),
                                Description = reader["Description"].ToString(),
                                TagPrice = Convert.ToDecimal(reader["TagPrice"]).ToString("0.00"),
                                Price = Convert.ToDecimal(reader["Price"]).ToString("0.00"),
                                Vendor = reader["Vendor"].ToString(),
                                Manufacturer = reader["Manufacturer"].ToString(),
                                PopUpNote = reader["PopUpNote"].ToString(),
                                ItemType = reader["ItemType"].ToString(),
                                NotStock = Convert.ToBoolean(reader["NotStock"])
                            });
                        }
                    }
                }
            }
            return styles;
        }

        public object ValidateStyleInInvoice(string invoiceNo, string style, string customerCode = "")
        {
            using (SqlConnection conn = new SqlConnection(_connectionProvider.GetConnectionString()))
            {
                if (!string.IsNullOrEmpty(customerCode))
                {
                    string invoiceCustomerQuery = @"
                    SELECT COUNT(*) 
                    FROM invoice 
                    WHERE inv_no = @InvoiceNo 
                    AND acc = @CustomerCode";

                    using (SqlCommand cmd = new SqlCommand(invoiceCustomerQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@InvoiceNo", invoiceNo);
                        cmd.Parameters.AddWithValue("@CustomerCode", customerCode);

                        conn.Open();
                        int invoiceCount = Convert.ToInt32(cmd.ExecuteScalar());

                        if (invoiceCount == 0)
                        {
                            return new
                            {
                                isValid = false,
                                message = $"Invoice {invoiceNo} does not belong to customer {customerCode}."
                            };
                        }
                    }
                    conn.Close();
                }

                conn.Open();
                string styleQuery = @"
                SELECT 
                    ii.[desc] as Description,
                    ii.price as Price,
                    ii.tag_price as TagPrice,
                    ii.qty as OriginalQuantity,
                    ISNULL((
                        SELECT SUM(rii.qty) 
                        FROM IN_ITEMS rii 
                        WHERE rii.ret_inv_no = @InvoiceNo 
                        AND rii.style = @Style
                    ), 0) as AlreadyReturnedQty
                FROM IN_ITEMS ii
                WHERE ii.inv_no = @InvoiceNo
                AND ii.style = @Style";

                using (SqlCommand cmd = new SqlCommand(styleQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@InvoiceNo", invoiceNo);
                    cmd.Parameters.AddWithValue("@Style", style);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            decimal originalQty = Convert.ToDecimal(reader["OriginalQuantity"]);
                            decimal alreadyReturned = Convert.ToDecimal(reader["AlreadyReturnedQty"]);
                            decimal availableForReturn = originalQty - alreadyReturned;

                            if (originalQty > 0)
                            {
                                return new
                                {
                                    isValid = true,
                                    message = "Valid return item",
                                    data = new
                                    {
                                        Description = reader["Description"].ToString(),
                                        Price = Convert.ToDecimal(reader["Price"]).ToString("0.00"),
                                        TagPrice = Convert.ToDecimal(reader["TagPrice"]).ToString("0.00"),
                                        OriginalQuantity = originalQty,
                                        AlreadyReturned = alreadyReturned,
                                        AvailableForReturn = availableForReturn
                                    }
                                };
                            }
                            else
                            {
                                return new
                                {
                                    isValid = false,
                                    message = $"Style '{style}' quantity is 0 in invoice {invoiceNo}."
                                };
                            }
                        }
                        else
                        {
                            return new
                            {
                                isValid = false,
                                message = $"Style '{style}' was not found in invoice {invoiceNo}."
                            };
                        }
                    }
                }
            }
        }

        public List<object> CheckStockForLayaway(List<LayawayStockCheck> items, string storeNo)
        {
            List<object> outOfStockItems = new List<object>();

            using (SqlConnection conn = new SqlConnection(_connectionProvider.GetConnectionString()))
            {
                conn.Open();

                foreach (var item in items)
                {
                    if (string.IsNullOrWhiteSpace(item.Style))
                        continue;

                    string query = @"
                    SELECT 
                        ISNULL(in_stock, 0) AS AvailableStock
                    FROM stock 
                    WHERE style = @Style
                    AND store_no = @StoreNo";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Style", item.Style.Trim());
                        cmd.Parameters.AddWithValue("@StoreNo", storeNo);

                        decimal availableStock = 0;
                        var result = cmd.ExecuteScalar();
                        if (result != null && result != DBNull.Value)
                        {
                            availableStock = Convert.ToDecimal(result);
                        }

                        if (availableStock < item.Quantity)
                        {
                            outOfStockItems.Add(new
                            {
                                style = item.Style,
                                requestedQuantity = item.Quantity,
                                availableStock = availableStock,
                                store = storeNo
                            });
                        }
                    }
                }
            }

            return outOfStockItems;
        }

        public DataTable SearchInvoice(string filter = "", bool isNoName = false, bool OpenOnlyinv = false)
        {
            string openonly = OpenOnlyinv ? " and IN_ITEMS.RET_INV_NO='' " : "  and 1=1 ";
            if (isNoName)
            {
                if (OpenOnlyinv)
                    return (__helperCommonService.HelperCommon.GetSqlData(@"Select * from ( SELECT ISNULL(ID,0) As ID,INVOICE.INV_NO,INVOICE.ACC,INVOICE.NAME, 
                 try_cast(customer.TEL as Nvarchar(30)) as TEL,INVOICE.DATE, IN_ITEMS.STYLE,
                 IN_ITEMS.[DESC],GR_TOTAL, ISNULL(GR_TOTAL,0) - ISNULL(CREDITS,0) as BALANCE,[Message],
                 INVOICE.INACTIVE FROM INVOICE LEFT OUTER JOIN CUSTOMER ON INVOICE.ACC = CUSTOMER.ACC     
                 LEFT OUTER JOIN (SELECT INV_NO,MAX(STYLE)STYLE, MAX([DESC])[DESC] ,RET_INV_NO 
                 FROM IN_ITEMS GROUP BY INV_NO,RET_INV_NO)IN_ITEMS ON ((INVOICE.INV_NO))=((IN_ITEMS.INV_NO)) 
                 where " + filter + " " + openonly + " AND ISNULL(INVOICE.ACC,'') <>'')a where INV_NO  not in (select RET_INV_NO from in_items where RET_INV_NO <> '')  ORDER BY DATE desc"));
                if (!string.IsNullOrWhiteSpace(filter))
                    return (__helperCommonService.HelperCommon.GetSqlData(@"SELECT ISNULL(ID,0) As ID,INVOICE.INV_NO,INVOICE.ACC,INVOICE.NAME, 
                 try_cast(customer.TEL as Nvarchar(30)) as TEL,INVOICE.DATE, IN_ITEMS.STYLE,
                 IN_ITEMS.[DESC],GR_TOTAL, ISNULL(GR_TOTAL,0) - ISNULL(CREDITS,0) as BALANCE,[Message],
                 INVOICE.INACTIVE FROM INVOICE LEFT OUTER JOIN CUSTOMER ON INVOICE.ACC = CUSTOMER.ACC     
                 LEFT OUTER JOIN (SELECT INV_NO,MAX(STYLE)STYLE, MAX([DESC])[DESC]  
                 FROM IN_ITEMS GROUP BY INV_NO)IN_ITEMS ON ((INVOICE.INV_NO))=((IN_ITEMS.INV_NO)) 
                 where " + filter + " AND ISNULL(INVOICE.ACC,'') <>''ORDER BY DATE desc"));
                return (__helperCommonService.HelperCommon.GetSqlData(@"SELECT ISNULL(ID,0),INVOICE.INV_NO,INVOICE.ACC,INVOICE.NAME,
             try_cast(customer.TEL as Nvarchar(30)) as TEL,INVOICE.DATE,IN_ITEMS.STYLE,
             IN_ITEMS.[DESC],GR_TOTAL, ISNULL(GR_TOTAL,0) - ISNULL(CREDITS,0) as BALANCE,[Message],
             INVOICE.INACTIVE FROM INVOICE LEFT JOIN CUSTOMER ON INVOICE.ACC = CUSTOMER.ACC 
             LEFT OUTER JOIN (SELECT INV_NO,MAX(STYLE)STYLE, MAX([DESC])[DESC]  
             FROM IN_ITEMS GROUP BY INV_NO)IN_ITEMS ON ((INVOICE.INV_NO))=((IN_ITEMS.INV_NO)) ORDER BY DATE desc"));
            }
            if (OpenOnlyinv)
                return (__helperCommonService.HelperCommon.GetSqlData(@"Select * from (SELECT ID,INVOICE.INV_NO,CUSTOMER.ACC,INVOICE.NAME, 
             try_cast(customer.TEL as Nvarchar(30)) as TEL,INVOICE.DATE, IN_ITEMS.STYLE,IN_ITEMS.[DESC],
             GR_TOTAL, ISNULL(GR_TOTAL,0) - ISNULL(CREDITS,0) as BALANCE,[Message],INVOICE.INACTIVE 
             FROM INVOICE INNER JOIN CUSTOMER ON INVOICE.ACC = CUSTOMER.ACC     
             LEFT OUTER JOIN (SELECT INV_NO,MAX(STYLE)STYLE, MAX([DESC])[DESC],RET_INV_NO  FROM IN_ITEMS GROUP BY INV_NO,RET_INV_NO)
             IN_ITEMS ON ((INVOICE.INV_NO))=((IN_ITEMS.INV_NO)) 
             where " + filter + "" + openonly + " AND ISNULL(INVOICE.ACC,'') <>'')a where INV_NO  not in (select RET_INV_NO from in_items where RET_INV_NO <> '')  ORDER BY DATE desc"));
            if (!string.IsNullOrWhiteSpace(filter))
                return (__helperCommonService.HelperCommon.GetSqlData(@"SELECT ID,INVOICE.INV_NO,CUSTOMER.ACC,INVOICE.NAME, 
             try_cast(customer.TEL as Nvarchar(30)) as TEL,INVOICE.DATE, IN_ITEMS.STYLE,IN_ITEMS.[DESC],
             GR_TOTAL, ISNULL(GR_TOTAL,0) - ISNULL(CREDITS,0) as BALANCE,[Message],INVOICE.INACTIVE 
             FROM INVOICE INNER JOIN CUSTOMER ON INVOICE.ACC = CUSTOMER.ACC     
             LEFT OUTER JOIN (SELECT INV_NO,MAX(STYLE)STYLE, MAX([DESC])[DESC]  FROM IN_ITEMS GROUP BY INV_NO)
             IN_ITEMS ON ((INVOICE.INV_NO))=((IN_ITEMS.INV_NO)) 
             where " + filter + " AND ISNULL(INVOICE.ACC,'') <>''ORDER BY DATE desc"));
            return (__helperCommonService.HelperCommon.GetSqlData(@"SELECT ID,INVOICE.INV_NO,CUSTOMER.ACC,INVOICE.NAME,
         try_cast(customer.TEL as Nvarchar(30)) as TEL,INVOICE.DATE,IN_ITEMS.STYLE,IN_ITEMS.[DESC],
         GR_TOTAL, ISNULL(GR_TOTAL,0) - ISNULL(CREDITS,0) as BALANCE,[Message],INVOICE.INACTIVE 
         FROM INVOICE LEFT JOIN CUSTOMER ON INVOICE.ACC = CUSTOMER.ACC LEFT OUTER JOIN 
         (SELECT INV_NO,MAX(STYLE)STYLE, MAX([DESC])[DESC]  FROM IN_ITEMS GROUP BY INV_NO)IN_ITEMS 
         ON ((INVOICE.INV_NO))=((IN_ITEMS.INV_NO)) ORDER BY DATE desc"));
        }

        public DataRow GetInvoiceByInvNo(string invno)
        {
            return (__helperCommonService.HelperCommon.GetSqlRow(@"select top 1 i.*,it.memo_no,iSNULL(it.IsSpecialItem,0) IsSpecialItem,it.fpon 
         from invoice i  with (nolock)
         left join (select * from IN_ITEMS with (nolock) where Trimmed_inv_no =@inv_no) it on i.inv_no = it.inv_no 
             Where trim(i.inv_no) = @inv_no", "@inv_no", invno.Trim()));
        }



        public DataTable GetInvoicePayments(string invNo, bool showLayaway = true, bool isReturn = false,
            bool isFromReturn = false, bool iSRefund = false)
        {
            using (var connection = new SqlConnection(_connectionProvider.GetConnectionString()))
            using (var command = new SqlCommand("GetInvoicePayments", connection))
            using (var dataAdapter = new SqlDataAdapter(command))
            {
                // Set command properties
                command.CommandType = CommandType.StoredProcedure;
                command.CommandTimeout = 6000;

                // Add parameters with strongly-typed values
                command.Parameters.Add(new SqlParameter("@inv_no", SqlDbType.VarChar) { Value = invNo });
                command.Parameters.Add(new SqlParameter("@showlayaway", SqlDbType.Bit) { Value = showLayaway });
                command.Parameters.Add(new SqlParameter("@is_return", SqlDbType.Bit) { Value = isReturn });
                command.Parameters.Add(new SqlParameter("@iSFromReturn", SqlDbType.Bit) { Value = isFromReturn });
                command.Parameters.Add(new SqlParameter("@iSRefund", SqlDbType.Bit) { Value = iSRefund });

                // Fill DataTable
                var dataTable = new DataTable();
                dataAdapter.Fill(dataTable);
                return dataTable;
            }
        }

        public bool AddEditInvoice(InvoiceModel invoice, string invoiceItems, string paymentItems, string discountItems,
            out string out_inv_no, bool ispayment = false, bool is_update = false, bool is_return = false,
            string storecodeinuse = "")
        {
            using (var connection = new SqlConnection(_connectionProvider.GetConnectionString()))
            using (var dbCommand = new SqlCommand("UPDATE_INVOICE", connection))
            {
                // Configure the command
                dbCommand.CommandType = CommandType.StoredProcedure;
                dbCommand.CommandTimeout = 600; // 600 seconds (10 minutes)

                // Add parameters
                dbCommand.Parameters.AddWithValue("@INV_NO", !is_return ? invoice.INV_NO : invoice.RET_INV_NO);
                dbCommand.Parameters.AddWithValue("@From_Repair", false);
                dbCommand.Parameters.AddWithValue("@iSDelete", false);

                // Open connection and execute command
                connection.Open();
                dbCommand.ExecuteNonQuery();

            }
            //string sp_name = _helperCommonService.is_Test_Mode ? "AddEditInvoice_Optimised" : "AddEditInvoice";
            string sp_name = "AddEditInvoice";
            //if (_helperCommonService.is_Test_Mode || System.Environment.MachineName.ToUpper().Contains("JAVID-HM"))
            //    _helperCommonService.AddKeepRec("Save Invoice: SP name: " + sp_name);

            using (SqlCommand dbCommand = new SqlCommand())
            {
                dbCommand.Connection = new SqlConnection(_connectionProvider.GetConnectionString());
                dbCommand.CommandType = CommandType.StoredProcedure;
                dbCommand.CommandText = sp_name;
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
                dbCommand.Parameters.AddWithValue("@TAXABLE", (bool)invoice.TAXABLE ? 1 : 0);
                dbCommand.Parameters.AddWithValue("@SALES_TAX", invoice.SALES_TAX);
                dbCommand.Parameters.AddWithValue("@TRADEIN", (bool)invoice.TRADEIN ? 1 : 0);
                dbCommand.Parameters.AddWithValue("@TRADEINAMT", invoice.TRADEINAMT);
                dbCommand.Parameters.AddWithValue("@TRADEINDESC", invoice.TRADEINDESC);
                dbCommand.Parameters.AddWithValue("@SPECIAL", (bool)invoice.SPECIAL ? 1 : 0);
                dbCommand.Parameters.AddWithValue("@PICKED", (bool)invoice.PICKED ? 1 : 0);
                dbCommand.Parameters.AddWithValue("@TAXINCLUDED", (bool)invoice.TAXINCLUDED ? 1 : 0);
                dbCommand.Parameters.AddWithValue("@ISPAYMENT", (bool)ispayment ? 1 : 0);
                dbCommand.Parameters.AddWithValue("@IS_RETURN", (bool)is_return ? 1 : 0);
                dbCommand.Parameters.AddWithValue("@IS_UPDATE", (bool)is_update ? 1 : 0);
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
                dbCommand.Parameters.Add("@OUT_INV_NO", SqlDbType.VarChar, 6).Direction = ParameterDirection.Output;

                dbCommand.Parameters.AddWithValue("@EMI_INTERESTAMOUNT", invoice.EMI_INTERESTAMOUNT);
                dbCommand.Parameters.AddWithValue("@EMI_MONTHLYPAYMENT", invoice.EMI_MONTHLYPAYMENT);
                dbCommand.Parameters.AddWithValue("@EMI_NOOFINSTALLMENTS", invoice.EMI_NOOFINSTALLMENTS);
                dbCommand.Parameters.AddWithValue("@EMI_RATEOFINTEREST", invoice.EMI_RATEOFINTEREST);
                dbCommand.Parameters.AddWithValue("@IS_INSTALLMENT", invoice.IS_INSTALLMENT ? 1 : 0);

                dbCommand.Parameters.AddWithValue("@SHIP_BY", invoice.SHIP_BY);
                dbCommand.Parameters.AddWithValue("@WEIGHT", invoice.WEIGHT);
                dbCommand.Parameters.AddWithValue("@INSURED", invoice.INSURED);
                dbCommand.Parameters.AddWithValue("@SHIPTYPE", invoice.SHIPTYPE);
                dbCommand.Parameters.AddWithValue("@TaxState", invoice.TaxState);
                dbCommand.Parameters.AddWithValue("@iSNotPickedUp", invoice.iSNotPickedUp ? 1 : 0);
                dbCommand.Parameters.AddWithValue("@Gold_Price", invoice.Gold_Price);
                dbCommand.Parameters.AddWithValue("@Silver_Price", invoice.Silver_Price);
                dbCommand.Parameters.AddWithValue("@Plat_Price", invoice.Plat_Price);
                dbCommand.Parameters.AddWithValue("@DoNotChangePaymentStore", invoice.DoNotChangePaymentStore);
                dbCommand.Parameters.AddWithValue("@iSVatInclude", invoice.iSVatInclude);
                dbCommand.Parameters.AddWithValue("@GivenChange", invoice.GivenChange);
                dbCommand.Parameters.AddWithValue("@iSCompnayName2", invoice.iSCompanyName2 ? 1 : 0);
                dbCommand.Parameters.AddWithValue("@UPSTRAK", invoice.UPSTRAK);
                dbCommand.Parameters.AddWithValue("@CUST_PON", invoice.CUST_PON);
                dbCommand.Parameters.AddWithValue("@TYPE", invoice.type);
                dbCommand.Parameters.AddWithValue("@ShopifyOrderNumber", invoice.ShopifyOrderNumber);
                dbCommand.Parameters.AddWithValue("@CompanyName", invoice.CompanyName);
                dbCommand.Parameters.AddWithValue("@AvaTaxTranCode", invoice.AvataxTranCode);
                dbCommand.Parameters.AddWithValue("@COD_TYPE", invoice.COD_TYPE);
                dbCommand.Parameters.AddWithValue("@Surprise", invoice.Surprise);
                dbCommand.Parameters.AddWithValue("@Email", invoice.Email);
                dbCommand.Parameters.AddWithValue("@ExpectToShipDate", invoice.ExpetToShipDate);
                dbCommand.Parameters.AddWithValue("@Source", invoice.Source);
                dbCommand.Parameters.AddWithValue("@Source2", invoice.Source2);
                dbCommand.Parameters.AddWithValue("@CustAddedByShop", invoice.CustAddByShop);
                dbCommand.Parameters.AddWithValue("@iSDateSameAsPickup", invoice.iSDateSameAsPickup);
                dbCommand.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@TBLPOINVOICEITEMS",
                    SqlDbType = SqlDbType.Xml,
                    Value = invoiceItems
                });

                dbCommand.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@TBLPAYMENTITEMS",
                    SqlDbType = SqlDbType.Xml,
                    Value = paymentItems
                });

                dbCommand.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@TBLDISCOUNTITEMS",
                    SqlDbType = SqlDbType.Xml,
                    Value = discountItems
                });

                // Open the connection, execute the query and close the connection
                dbCommand.Connection.Open();
                var rowsAffected = dbCommand.ExecuteNonQuery();
                out_inv_no = Convert.ToString(dbCommand.Parameters["@OUT_INV_NO"].Value);

                dbCommand.Connection.Close();
                return rowsAffected > 0;
            }
        }

        public bool CheckInStock(string STYLE, string STORE, decimal rsrvd_qty = 0, bool iSPickup = false, bool iSMakeRepair = false)
        {
            using (var connection = new SqlConnection(_connectionProvider.GetConnectionString()))
            using (var command = new SqlCommand("InvoiceCheckInStock", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@PSTYLE", STYLE.Trim());
                command.Parameters.AddWithValue("@PSTORE", STORE.Trim());
                command.Parameters.AddWithValue("@rsrvd_qty", rsrvd_qty);
                command.Parameters.AddWithValue("@iSMakeRepair", iSMakeRepair);
                command.Parameters.AddWithValue("@iSPickup", iSPickup);
                connection.Open();
                var result = command.ExecuteScalar();
                return result != DBNull.Value && Convert.ToDecimal(result) > 0;
            }
        }



        public DataTable GetStateCityByZip(string strZip)
        {
            return __helperCommonService.HelperCommon.GetSqlData(@"Select city,state from zipcodes with(nolock) where zipcode=@ZIP", "@ZIP", strZip);
        }
        public DataRow CheckValidCustomerCode(string acc)
        {
            return __helperCommonService.HelperCommon.GetSqlRow("SELECT TOP 1 * FROM Customer with (nolock) WHERE acc=TRIM(@acc)", "@acc", acc);
        }
        public DataTable GetAllOpenSpecialOrder(DateTime? fromDt, DateTime? toDt, string stores, string acc, string salesman, string invNo, bool shopInvoice = false)
        {
            using (var connection = new SqlConnection(_connectionProvider.GetConnectionString()))
            using (var command = new SqlCommand("GetAllOpenSpecialOrder", connection))
            using (var adapter = new SqlDataAdapter(command))
            {
                command.CommandType = CommandType.StoredProcedure;

                // Add parameters with null handling
                command.Parameters.AddWithValue("@FromDt", fromDt ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@ToDt", toDt ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@Stores", stores.Trim());
                command.Parameters.AddWithValue("@ACC", acc.Trim());
                command.Parameters.AddWithValue("@Salesman", salesman.Trim());
                command.Parameters.AddWithValue("@InvNo", invNo.Trim());
                command.Parameters.AddWithValue("@ShopInvoice", shopInvoice ? 1 : 0);

                // Fill and return DataTable
                var dataTable = new DataTable();
                adapter.Fill(dataTable);
                return dataTable;
            }
        }

        public DataTable SearchRepairOrder(string filter = "", string hFilter = "1=1", bool isNoName = false)
        {
            return __helperCommonService.HelperCommon.GetStoreProc("GetRepairOrderData", "@nFilter", filter, "@hFilter", hFilter, "@isNoName", isNoName ? "1" : "0");
        }
        public DataRow CheckValidBillingAcct(string billacc)
        {
            return __helperCommonService.HelperCommon.GetSqlRow("select *  From Customer with (nolock) Where trim(bill_acc)= trim(@bill_acc) and bill_acc = acc", "@bill_acc", billacc);
        }


        public DataRow GetMemoByInvNo(string memo_no)
        {
            return __helperCommonService.HelperCommon.GetSqlRow("select top 1 i.*, it.memo_no,it.fpon from Memo i with (nolock) left join me_items it with (nolock) on i.memo_no = it.memo_no Where trim(i.memo_no) = trim(@memo_no)", "@memo_no", memo_no);
        }

        public DataRow GetProformaInvoice(string invno)
        {
            return __helperCommonService.HelperCommon.GetSqlRow("SELECT TOP 1 i.*, t.memo_no, t.by_wt FROM Proforma_INVOICE i LEFT JOIN Proforma_IN_ITEMS t ON i.inv_no = t.inv_no WHERE i.inv_no=@inv_no", "@inv_no", invno);
        }

        public bool CheckConvertedInvoice(string p_invno)
        {
            DataTable dataTable = __helperCommonService.HelperCommon.GetSqlData("SELECT * FROM Proforma_INVOICE WHERE Trimmed_inv_no=TRIM(@P_INVNO) AND LEN(TRIM(CONVERTED_INV_NO))>0 ",
                "@P_INVNO", p_invno);
            return (__helperCommonService.HelperCommon.DataTableOK(dataTable));
        }

        public bool ConvertProforma2RegularInvoice(string p_invno, string r_invno, string currUser, bool converted_from_Memo = false)
        {
            using (var connection = new SqlConnection(_connectionProvider.GetConnectionString()))
            using (var command = new SqlCommand("ConvertProforma2RegularInvoice", connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddRange(new[]
                {
         new SqlParameter("@Proforma_INV_NO", p_invno),
         new SqlParameter("@REGULAR_INV_NO", r_invno),
         new SqlParameter("@LOGGEDUSER", currUser),
         new SqlParameter("@MAKESFM", converted_from_Memo)
         });

                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();

                return rowsAffected > 0;
            }
        }

        public bool GoldSentToFactory(string cinv, string cDate, string cVendor, int dwtgr, string invoiceItems)
        {
            using (SqlConnection connection = new SqlConnection(_connectionProvider.GetConnectionString()))
            using (SqlCommand dbCommand = new SqlCommand("GOLDSENTTOFACTORY", connection))
            {
                dbCommand.CommandType = CommandType.StoredProcedure;
                dbCommand.CommandTimeout = 5000;

                dbCommand.Parameters.Add(new SqlParameter("@INV_NO", SqlDbType.VarChar, 50) { Value = cinv });
                dbCommand.Parameters.Add(new SqlParameter("@ACC", SqlDbType.VarChar, 50) { Value = cVendor });
                dbCommand.Parameters.Add(new SqlParameter("@DATE", SqlDbType.VarChar, 30) { Value = cDate });
                dbCommand.Parameters.Add(new SqlParameter("@DWTGR", SqlDbType.Int) { Value = dwtgr });
                dbCommand.Parameters.Add(new SqlParameter("@TBLPOINVOICEITEMS", SqlDbType.Xml) { Value = invoiceItems });

                connection.Open();
                int rowsAffected = dbCommand.ExecuteNonQuery();

                return rowsAffected > 0;
            }
        }

        public bool insertCustomerFromInvoice(CustomerModel customer)
        {
            try
            {
                using (SqlCommand dbCommand = new SqlCommand())
                {
                    dbCommand.Connection = new SqlConnection(_connectionProvider.GetConnectionString());
                    dbCommand.CommandType = CommandType.Text;
                    string isValidCustomer = __helperCommonService.HelperCommon.CheckValidCustomerCode(customer.ACC, __helperCommonService.HelperCommon.is_Glenn);
                    if (isValidCustomer == "0")//,CustCheckVal1
                        dbCommand.CommandText = @"INSERT INTO customer
             (ACC,BILL_ACC ,Name,Addr1,Addr12,Addr13,City1,State1,Zip1,Country,Email,store_no,tel,dob,non_taxable,ON_ACCOUNT,old_customer,declined,DRIVERLICENSE_NUMBER,ok_totext,ok_toemail,ok_tocall,ok_tomail,DRIVERLICENSE_STATE,Cell,
                            name2,addr2,addr22,city2,state2,zip2,tel2,country2) 
             VALUES (replace(replace(cast(@Acc as nvarchar(15)),char(10),''),char(13),'')
                     ,replace(replace(cast(@bacc as nvarchar(15)),char(10),''),char(13),'')
                     ,cast(@name as nvarchar(60))
                     ,cast(@addr1 as nvarchar(60))
                     ,cast(@addr12 as nvarchar(60))
                     ,cast(@addr13 as nvarchar(60))
                     ,cast(@city1 as nvarchar(30))
                     ,cast(@state1 as nvarchar(10))
                     ,cast(@zip1 as nvarchar(10))
                     ,cast(@country as nvarchar(15))
                     ,cast(@email as nvarchar(80))
                     ,cast(@store_no as nvarchar(50))
                     ,cast(@tel as nvarchar(30))
                     ,cast(@dob as date),cast(@Non_Taxable as bit),cast(@On_Account as bit)
                     ,cast(@old_customer as nvarchar(20))
                     ,iSNULL(@declined,0)
                     ,cast(@driverlicense_number as nvarchar(15)) 
                     ,1,1,1,1
                     ,iif(iSNULL(@driverlicense_number,'')<>'',@state1,'')
                     ,iSNULL(@Cell,'')
                     ,cast(@name as nvarchar(60))
                     ,cast(@addr1 as nvarchar(60))
                     ,cast(@addr12 as nvarchar(60))                            
                     ,cast(@city1 as nvarchar(30))
                     ,cast(@state1 as nvarchar(10))
                     ,cast(@zip1 as nvarchar(10))
                     ,cast(@tel as nvarchar(30))
                     ,cast(@country as nvarchar(15))
                     )";//,cast(@CustCheckVal1 as nvarchar(15))
                    else
                        dbCommand.CommandText = @"UPDATE customer SET 
                                     ACC=replace(replace(cast(@Acc as nvarchar(15)),char(10),''),char(13),''),
                                     BILL_ACC =replace(replace(cast(@bacc as nvarchar(15)),char(10),''),char(13),''),
                                     Name=cast(@name as nvarchar(60)),
                                     Addr1=cast(@Addr1 as nvarchar(60)),
                                     Addr12=cast(@Addr12 as nvarchar(60)),
                                     Addr13=cast(@Addr13 as nvarchar(60)),
                                     City1=cast(@city1 as nvarchar(30)),
                                     State1=cast(@state1 as nvarchar(10)),
                                     Zip1=cast(@zip1 as nvarchar(10)),
                                     Country=cast(@country as nvarchar(15)),
                                     Email=cast(@email as nvarchar(80)),
                                     store_no=cast(@store_no as nvarchar(5)),
                                     tel=cast(@tel as nvarchar(30)),
                                     dob=@dob,non_taxable=@Non_Taxable,
                                     ON_ACCOUNT=@On_Account,
                                     old_customer = cast(@old_customer as nvarchar(20)),
                                     declined=iSNULL(@declined,0),
                                     driverlicense_number=cast(iSNULL(@driverlicense_number,'') as nvarchar(15)),
                                     ok_totext=1,ok_toemail=1,ok_tocall=1,ok_tomail=1, 
                                     DRIVERLICENSE_STATE=iif(iSNULL(@driverlicense_number,'')<>'',cast(@state1 as nvarchar(10)),''), Cell =iif(iSNULL(@Cell,'')<>'',iSNULL(@Cell,''),Cell)
                                     ,name2=cast(@name as nvarchar(60))
                                     ,addr2=cast(@Addr1 as nvarchar(60))
                                     ,addr22=cast(@Addr12 as nvarchar(60))
                                     ,city2=cast(@city1 as nvarchar(30))
                                     ,state2=cast(@state1 as nvarchar(10))
                                     ,zip2=cast(@zip1 as nvarchar(10))
                                     ,tel2=cast(@tel as nvarchar(30))
                                     ,country2=cast(@country as nvarchar(15))
                                     WHERE ACC=@Acc";

                    dbCommand.Parameters.AddWithValue("@Acc", customer.ACC);
                    dbCommand.Parameters.AddWithValue("@bacc", customer.ACC ?? "");
                    dbCommand.Parameters.AddWithValue("@Name", customer.NAME ?? "");
                    dbCommand.Parameters.AddWithValue("@Addr1", customer.ADDR1 ?? "");
                    dbCommand.Parameters.AddWithValue("@Addr12", customer.ADDR12 ?? "");
                    dbCommand.Parameters.AddWithValue("@Addr13", customer.ADDR13 ?? "");
                    dbCommand.Parameters.AddWithValue("@City1", customer.CITY1 ?? "");
                    dbCommand.Parameters.AddWithValue("@State1", customer.STATE1 ?? "");
                    dbCommand.Parameters.AddWithValue("@Zip1", customer.ZIP1 ?? "");
                    dbCommand.Parameters.AddWithValue("@Country", customer.COUNTRY ?? "");
                    dbCommand.Parameters.AddWithValue("@Email", customer.EMAIL ?? "");
                    dbCommand.Parameters.AddWithValue("@store_no", customer.Store_no ?? "");
                    dbCommand.Parameters.AddWithValue("@tel", customer.TEL);
                    dbCommand.Parameters.AddWithValue("@dob", customer.DOB ?? DateTime.Now);
                    dbCommand.Parameters.AddWithValue("@Non_Taxable", customer.Non_Taxable == true ? 1 : 0);
                    dbCommand.Parameters.AddWithValue("@On_Account", customer.ON_ACCOUNT);
                    dbCommand.Parameters.AddWithValue("@old_customer", customer.old_customer ?? "");
                    dbCommand.Parameters.AddWithValue("@declined", customer.declined);
                    dbCommand.Parameters.AddWithValue("@driverlicense_number", customer.driverlicense_number ?? "");
                    dbCommand.Parameters.AddWithValue("@Cell", customer.CELL ?? "");
                    //dbCommand.Parameters.AddWithValue("@CustCheckVal1", _helperCommonService.is_AlexH ? "1" : "0");
                    // Open the connection, execute the query and close the connection
                    dbCommand.Connection.Open();
                    var rowsAffected = dbCommand.ExecuteNonQuery();
                    dbCommand.Connection.Close();
                    return rowsAffected > 0;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public DataTable getCustomers(string fname, string lname, string state, string city, string zip, string phone, string email, string optionvalue = "")
        {
            DataTable dataTable = new DataTable();
            string Query1 = "";
            using (SqlDataAdapter dataAdapter = new SqlDataAdapter())
            {
                try
                {
                    // Create the command and set its properties
                    dataAdapter.SelectCommand = new SqlCommand();
                    dataAdapter.SelectCommand.Connection = new SqlConnection(_connectionProvider.GetConnectionString());

                    dataAdapter.SelectCommand.CommandType = CommandType.Text;
                    if (optionvalue == "Invoice")
                        Query1 = "SELECT ACC,NAME,CITY1,STATE1,ZIP1,TEL,EMAIL,Addr1,Addr12,Addr13,Country FROM CUSTOMER WHERE NAME LIKE @fname AND NAME LIKE @lname AND STATE1 LIKE @state AND CITY1 LIKE @city AND ZIP1 LIKE @zip AND tel LIKE @phone AND EMAIL LIKE @email ORDER BY ACC ";
                    else
                        Query1 = "SELECT ACC,NAME,CITY1,STATE1,ZIP1,TEL,EMAIL FROM CUSTOMER WHERE NAME LIKE @fname AND NAME LIKE @lname AND STATE1 LIKE @state AND CITY1 LIKE @city AND ZIP1 LIKE @zip AND tel LIKE @phone AND EMAIL LIKE @email ORDER BY ACC ";
                    dataAdapter.SelectCommand.CommandText = Query1;

                    // Add the parameter to the parameter collection
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@fname", fname + "%");
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@lname", "%" + lname + "%");
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@state", "%" + state + "%");
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@city", "%" + city + "%");
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@zip", "%" + zip + "%");
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@phone", "%" + phone + "%");
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@email", "%" + email + "%");

                    // Fill the datatable From adapter
                    dataAdapter.Fill(dataTable);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                // Get the datarow from the table
                return dataTable.Rows.Count > 0 ? dataTable : null;
            }
        }

        public DataTable GetProcessSpecialOrders(string cmbStores, bool allstores, DateTime fromdate, DateTime todate, bool statusFilter)
        {
            using (SqlConnection conn = new SqlConnection(_connectionProvider.GetConnectionString()))
            {
                string query = @"
                    SELECT DISTINCT 
                        i.inv_no,
                        ii.style,
                        ii.[desc],
                        ii.vendor,
                        CASE 
                            WHEN ii.isdlvrd = 1 THEN 'Delivered'
                            WHEN ii.VendOrderDt IS NOT NULL THEN 'Ordered'
                            ELSE 'Pending'
                        END AS status,
                        i.date,
                        i.store_no,
                        ii.VendOrderDt,
                        ii.ExpDelDt
                    FROM IN_ITEMS ii
                    INNER JOIN INVOICE i ON i.INV_NO = ii.INV_NO
                    WHERE ii.IsSpecialItem = 1
                    AND i.DATE BETWEEN @fromdate AND @todate";

                if (!allstores && !string.IsNullOrWhiteSpace(cmbStores))
                {
                    query += " AND i.STORE_NO = @store";
                }

                if (statusFilter)
                {
                    query += " AND ii.isdlvrd = 0";
                }

                query += " ORDER BY i.date DESC";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@fromdate", fromdate);
                    cmd.Parameters.AddWithValue("@todate", todate);

                    if (!allstores && !string.IsNullOrWhiteSpace(cmbStores))
                    {
                        cmd.Parameters.AddWithValue("@store", cmbStores);
                    }

                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        return dt;
                    }
                }
            }
        }


        public string GetAccfromInvoice(string inv_no)
        {
            DataTable dtable = __helperCommonService.HelperCommon.GetSqlData($"SELECT top 1 ACC FROM INVOICE WHERE inv_no='{inv_no}'");
            if (__helperCommonService.HelperCommon.DataTableOK(dtable))
                return Convert.ToString(dtable.Rows[0]["ACC"]);
            return string.Empty;
        }

        public bool CheckNochangedate(DateTime cdate)
        {
            DateTime nochangedate;
            if (DateTime.TryParse(__helperCommonService.HelperCommon.NoChangeBefore, out nochangedate))
            {
                return cdate < nochangedate;
            }
            return false;
        }

        public bool IsCanceled(string inv_no)
        {
            return __helperCommonService.HelperCommon.DataTableOK(__helperCommonService.HelperCommon.GetSqlData($@"select 1 from invoice with (nolock) where inv_no=@inv_no and isnull(is_deb,0)=1", "@inv_no", inv_no));
        }


    }

    public class LayawayStockCheck
    {
        public string Style { get; set; }
        public decimal Quantity { get; set; }
    }
}