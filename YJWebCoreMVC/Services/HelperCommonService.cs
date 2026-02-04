using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.Net;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using YJWebCoreMVC.Models;

namespace YJWebCoreMVC.Services
{
    public class HelperCommonService
    {
        private readonly ConnectionProvider _connectionProvider;
        private readonly GlobalSettingsService _globalSettingsService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IWebHostEnvironment _env;
        public string ip_1and1 = "216.250.117.69";
        public string ionosconnstring;

        public HelperCommonService(ConnectionProvider connectionProvider, GlobalSettingsService globalSettingsService, IHttpContextAccessor httpContextAccessor, IWebHostEnvironment env)
        {
            _connectionProvider = connectionProvider;
            _globalSettingsService = globalSettingsService;
            _httpContextAccessor = httpContextAccessor;
            _env = env;
            ionosconnstring = string.Format("Server={0};Database=Licensing;User Id=sa;Password=Javid456#;", ip_1and1);
        }

        public int LoggedUserLevel = 4, GIPTOTAL = 0, LastVersion = 0, IdleTimer = 0, noOfdecimals = 0, RefreshIntervalAuthCode = 7, Warranty = 0, _PCFeedVersion = 0, GiftExpDays, RoundOff;
        public string InvoiceNote = "NO REFUNDS 7 DAYS EXCHANGE ONLY ON unworn, unaltered, undamaged items.ONE TIME EXCHANGE ONLY. NO RETURNS OR EXCHANGE ARE ACCEPTED ON EARRINGS. LAYAWAY Purchase is being held for the person’s name on this invoice until the balance owed is paid in full. By signing this LAYAWAY agreement, I agree to make  payments according to the payment frequency printed above until the total balance owed is paid in full. If we do not receive full payment by the FINAL DUE DATE printed above, we will automatically cancel your layaway purchase. Monies paid will be forfeit and are non-refundable. Canceling a Layaway is subject to a 25% RESTOCKING FEE.          \n\n CUSTOMER’S SIGNATURE";
        public string SupportMailId = "Support@ishalinc.com";
        
        public string StoreCodeUse = "", FixedStoreCode = "", AccessToken = "", LoggedUser = "ADMIN", StoreCode = "", MemoNote = "", ImagesPath = "", _WorkingPrinter = "",
            tagPrinterPort = "", tagTemplateName = "", RepairNote = "", RepairInvoiceNote = "", RepairDisclaimer = "", selectedStore = "";
        public bool StoreCodeInUse = false, is_Malakov, is_Zhaveri, is_WatchKing, is_Glenn, Tel_0_prefix = false, is_StyleItem, is_Briony, ByFieldValue5 = false, ByFieldValue6 = false, is_Mahin, is_Loyalty, Can_Text, isNoTaxRepair, Read_Signature_CC, Read_Signature;
        public string _connString = "", CompanyEmail, mask_tel = "", StoreCodeInUse1 = "IT SOLUTIO", warrentynote = "", CompanyName, registerInUse = string.Empty, Cash_Register = "", CompanyAddr1 = "", CompanyAddr2 = "", CompanyTel = "", CompanyCity = "", CompanyZip = "", CompanyState = "", CompanyWebsite = "";
        
        public readonly DateTime DefStart = new DateTime(1900, 1, 1), DefEnd = new DateTime(9998, 12, 31);

        public bool DashBoardOpen = false, is_ret_inv_no = false, isopenmemo = false, IsPhysicalInv = false, closejb = false, isDueReport = false, Not_Stock = false, NotSideBySide1, is_Wrist, is_devam, iSSpecialOrder,
            IsStoreCustomers = false, NotSideBySide2, NotSideBySide3, is_logout = false, IsAddress_LandScape,
            Use_Warranty = false, do_tsc = false, MicrReport, NegativeInv, AutoSandH, NoMemo,
            EmailFailed = false, _FeedMinimized = false, is_oldtown = false, is_Lefk,
            app_closing = false, isFao, isJMcare, is_Maria_Br, is_Maria, is_Aqua, is_Test_Mode, iSFischer,
            DoCostCode = false, TagOdd = true, Tag2PerRow = false, is_RFID = false, iSSheikh, Is_Symphony, Is_Sona,
            is_greenleaf = false, NoPriceOnTag = false, is_RoyalMD, is_Alina, is_Exaurum,
            is_Prestigio, is_Precision, AskToSign, isTsc = false, isCitoh = false, isZebra = false,
            isGodex = false, is_Gautham, is_Canadian, is_Aed, is_Euro, is_Ruaa, is_CAD_Buy,
            is_Auto_stock = false, Zebra_RFID, iS_Jade, is_Holloway, is_Neb,
            is_DiamondDealer = false, is_Tigran, is_WatchDealer, is_AlexH, is_lucid = false, iS_EditableInvoiceNumber = false, is_Moi, iS_Sai, iS_emCity, iS_McAuley, iS_Erstwhile, is_Hungtoo,
            is_NewAppraisal, is_KingFL, is_Gary, is_PickUpNoMfg, iS_Corio, iS_Tigran, is_Adam,
            is_Whitman, iS_Shiva = false, Cash_10K, is_Sandsea, is_Singer,
            EnableAvalaraTaxCalc, EnableAvalaraAddressValidation, EnableAvalaraLogging, iS_JJGroup,
            iS_Geller, iS_Leohamel, is_Exchange, is_UseBOM, is_JosJacob, iS_Sree, iS_Anar, is_RK, SmallRFID,
            is_Etsy, iS_Ferko, is_Ram, is_JackW, test_mode = true, is_active_inactive = false, iS_Quality = false, blnAddCashToARegisterFromManager = false,
            blnIsManagerGetCashFromABank = false, blnIsImport = false, is_Single, iscustcode = false;
        public decimal SignBelow = 0, printerfont = 2, StoreSalesTax = 0;

        public string ShopifyAPI = "", ShopifyPwd = "", ShopifySecret = "", ShopifyURL = "";
        public decimal RfidPrintLeft, RfidPrintRight, RfidPrintTop, RfidPrintCinc, TagMultiplyer, TagReportTopR;

        public string Tag_place = "", Tag_text = "", Tag_text2 = "", Tag_place2 = "", Tag_text3 = "", Tag_place3 = "", Tag_text4 = "", Tag_place4 = "", TagLeft1 = "", TagLeft2 = "", TagLeft3 = "", TagLeft4 = "", TagLeft5 = "", TagLeft6 = "", TagLeft7 = "",
            TagLeft1A = "", TagLeft2A = "", TagLeft3A = "", TagLeft4A = "", TagLeft5A = "", TagLeft6A = "", TagLeft7A = "",
            TagLeft1B = "", TagLeft2B = "", TagLeft3B = "", TagLeft4B = "", TagLeft5B = "", TagLeft6B = "", TagLeft7B = "",
            TagLeft1C = "", TagLeft2C = "", TagLeft3C = "", TagLeft4C = "", TagLeft5C = "", TagLeft6C = "", TagLeft7C = "",
            TagLeft1D = "", TagLeft2D = "", TagLeft3D = "", TagLeft4D = "", TagLeft5D = "", TagLeft6D = "", TagLeft7D = "",
            TagLeft1E = "", TagLeft2E = "", TagLeft3E = "", TagLeft4E = "", TagLeft5E = "", TagLeft6E = "", TagLeft7E = "",
            TagRight1 = "", TagRight2 = "", TagRight3 = "", TagRight2A = "", TagRight2B = "", TagRight2C = "", TagRight2D = "", TagRight2E = "",
            TagRight1A = "", TagRight1B = "", TagRight1C = "", TagRight1D = "", TagRight1E = "",
            TagRight3A = "", TagRight3B = "", TagRight3C = "", TagRight3D = "", TagRight3E = "",
            TagRight4 = "", TagRight4A = "", TagRight4B = "", TagRight4C = "", TagRight4D = "", TagRight4E = "",
            TagRight5 = "", TagRight5A = "", TagRight5B = "", TagRight5C = "", TagRight5D = "", TagRight5E = "",
            TagRight6 = "", TagRight6A = "", TagRight6B = "", TagRight6C = "", TagRight6D = "", TagRight6E = "",
            TagRight7 = "", TagRight7A = "", TagRight7B = "", TagRight7C = "", TagRight7D = "", TagRight7E = "",
            TemTag_text = "", TemTag_text2 = "", TemTag_text3 = "", TemTag_text4 = "", RFID_Port, RFID_Printer_Port,
            Cost_Code = "", CostCodeDefaultChar = "", _ShowPrintMsg = "", StyleField1 = "", StyleField2 = "", StyleField3 = "", StyleField4 = "", StyleField5 = "", StyleField6 = "", StyleField7 = "", StyleField8 = "", StyleField9 = "", stk_prefix = "", jm_Store = "", JM_API_KEY = string.Empty, JM_API_PWD = string.Empty,
            StyleField10 = "", SquareClientSecret = "", SquareAppId = "", StyleField20 = "", StyleField21 = "", StyleField22 = "", StyleField23 = "", StyleField24 = "", StyleField25 = "", StyleField26 = "", StyleField27 = "", StyleField28 = "", StyleField29 = "", StyleCheck1, StyleCheck2, StyleCheck3, StyleCheck4, StyleCheck5, StyleCheck6, CustAttr1, CustAttr2, CustAttr3, CustAttr4, CustAttr5,
            CustAttr6, CustAttr7, CustAttr8, CustMultiAttr1, CustMultiAttr2, CustMultiAttr3, CustCheckAttr1, CustCheckAttr2, CustCheckAttr3, CustCheckAttr4, CustCheckAttr5, CustCheckAttr6, CustCheckAttr7, CustCheckAttr8, GL_AP_SHIPPING = "", GL_VENDORDISCOUNT = "", GL_AP_Insurance = "", GL_ASSET = "";
        List<MenuModel> menuModels = new();
        public string _licenseKey, NoChangeBefore = "", PrintMode = "", DefaultSalesman = "";

        //public RegionInfo cRegionInfo = null;
        //public CultureInfo info = null;
        public decimal TagReportTopR2, TagReportTopR3, LabelLeftMargin = 0, LabelLinesMargin = 0,
            estimateRepairValue = 0, TagReportLeft, TagReportCInc, TagReportLeft2,
            TagReportRight2, TagReportTop2, TagReportCInc2, TagReportLeft3, TagReportRight3, TagReportTop3, TagReportCInc3,
            BankBouncedFee = 0, CustBouncedFee = 0, pricerange1, pricerange2, pricerange3, pricerange4, pricerange5,
            pricerange6, pricerange7, pricerange8, VAT, ItemCostper, StoneCostPer, rndSalesTax0049 = 0.0049M, SSCostPer;
        public DateTime TokenExpireDate, LockDate, Etsy_Refresh_TokenDate;
        public enum ShippingType { FedEx = 1, UPS = 2, Brinks_FDX = 3, Brinks_UPS = 4, PP_Fedex = 5, PP_UPS = 6, MalcaAmit = 7, DHL = 8, Stamps = 9, USPS = 10, None = 0 };
        public string LicenseKey
        {
            get
            {
                if (string.IsNullOrEmpty(_licenseKey))
                {
                    // Fetch license key from database
                    string ModulesData = DecryptModule(GetLicenseKeyFromDatabase());
                    _licenseKey = ModulesData.Substring(15, 23);
                }
                return _licenseKey;
            }
            set
            {
                _licenseKey = value;
            }
        }

        private string GetLicenseKeyFromDatabase()
        {

            string query = "SELECT top 1 print_mode FROM ups_ins";
            using (var connection = _connectionProvider.GetConnection())
            {
                connection.Open();
                using (var command = new SqlCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return reader.GetString(0);
                        }
                    }
                }
            }

            // Handle case where no license key is found in the database
            return string.Empty;
        }
        public UPSINSModel GetUpsValues()
        {
            return _globalSettingsService.GetGlobalSettings();
        }

        public int GetMaxUsersForLicense()
        {
            var upsValues = GetUpsValues();
            string ModulesData = DecryptModule(upsValues.print_mode);
            string hostname = ModulesData.Substring(0, 15);
            string licensekey = ModulesData.Substring(15, 23);

            string url = $"https://licensing.ishalinc.com/getlicenses.asp?cdkey={Uri.EscapeDataString(licensekey)}";

            try
            {
                System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                using (var client = new System.Net.WebClient())
                {
                    // Optional: Set headers
                    client.Headers.Add("User-Agent", "LicenseChecker");

                    // Download the license count as plain text
                    string response = client.DownloadString(url).Trim();
                    int licenseCount;
                    if (int.TryParse(response, out licenseCount))
                    {
                        return licenseCount;
                    }
                }
            }
            catch (Exception)
            {
            }
            return -1; // Default if request or parsing fails
        }

        public enum Modules
        {
            RFID = 1, NegativeInventory = 2, Inv_Sort_By_Style = 3, QuickBooks = 4, NewDash = 5, Require_Ack = 6, Require_Invoice = 7,
            ModelImages = 8, No_Name_Inv = 9, Narrow_Printer = 10, Warranty = 11, HalfPage = 12, AutoDesc = 13,
            CreditApp = 14, NoPromiseDate = 15, Mahindra = 16, Bullion = 17, snh_taxable = 18, Auto_stock = 19, DWT = 20,
            AutoQty = 21, Left_justify = 22, SingleStore = 23, SN_Images = 24, Change_Sales_Tax = 25, StyleDetailCost = 26,
            Check_At_bottom = 27, Check_In_Middle = 28, Is_Square = 29, EstimateOnJob = 30, Ask_Printer = 31, Clover = 32,
            NoMemo = 33, Idex = 34, CC_Swipe = 35, Shopify = 36, NoTaxRepair = 37, DoubleSided = 38, Godex_save = 39,
            No_Repair_Job = 40, CardConnect = 41, Signature = 42, Comish_Password = 43, Dept_OM = 44, Mex_Dollar = 45,
            Alex_h = 46, CanText = 47, Gen_Text_Lbl = 48, Dymo = 49, Topaz = 50, Auto_PayMe = 51, Luxury_Tax = 52, Can_ScanDL = 53,
            TaxAfterTradeIn = 54, Installment = 55, USB_CashDrawer = 56, No_Cost_JobBag = 57, Label_3x6 = 58,
            JMcare = 59, Split_inv_Full_Comish = 60, Repair_3Part = 61, Old_Add_Image = 62, No_Date_Chage = 63, A4 = 64,
            CostCode = 65, Ind_Dollar = 66, TwoTagsPerRow = 67, WarnBelowMinimum = 68, GroupBasedOnType = 69,
            Progressive_commission = 70, LayawayNotInStock = 71, Invoice_6x4 = 72, InvoiceRepairPrice = 73,
            Shipping = 74, Unique_SN = 75, TaxRateByZip = 76, PriceBasedOnGold = 77, Label_4x6 = 78, Canada = 79,
            CommissionByDiscount = 80, CommissionByProfit = 81, DebitCard = 82, AskWeight = 83, Loyalty = 84,
            TagOnInvoice = 85, Currencies = 86, NoPhysicalStock = 87, AuthorizeNET = 88, AskForUser = 89, NoRepairEstimate = 90,
            Euro = 91, Instant_QBO = 92, Instant_QB_Desk = 93, VAT_Included = 94, ZPL_RFID = 95, DiscPerLine = 96,
            CatByGroup = 97, SayGST = 98, EPS_CC = 99, ShowOnAccount = 100, DiamondDealer = 101, KittyPlan = 102,
            PostCode = 103, SeparateStore = 104, MyNameOnJob = 105, UseBOM = 106, Signature_CC = 107, Wholesale = 108,
            PoIsModel = 109, StyleNoByCat = 110, EditInvoiceDates = 111, Podium = 112, Avalara = 113, PickUpNoMfg = 114,
            Bill_MultiCurr = 115, QBOE = 116, Geller = 117, OK_8000 = 118, ETSY = 119, Test_Mode = 120,
            Style_Item = 121, ShortBill = 122, UsePartNo = 123, StylesWeight = 124, PrintCheckLower = 125,
            LicenseBySession = 126, NegativeLayaway = 127
        };

        public SqlDataReader GetVendorMultiAttr(string[] attrNums)
        {
            if (attrNums == null || attrNums.Length == 0)
                throw new ArgumentException("attrNums cannot be null or empty", nameof(attrNums));
            SqlConnection conn = _connectionProvider.GetConnection();
            try
            {
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = conn;
                    command.CommandType = CommandType.Text;

                    StringBuilder queryBuilder = new StringBuilder();
                    List<SqlParameter> parameters = new List<SqlParameter>();

                    for (int i = 0; i < attrNums.Length; i++)
                    {
                        string paramName = "@Attr" + i;
                        if (i > 0) queryBuilder.Append(" UNION ");
                        queryBuilder.Append($"SELECT DISTINCT ATTR_VAL AS AttrVal, ATTR_NUM FROM VEN_ATTR with (nolock) WHERE ATTR_NUM = {paramName}");
                        parameters.Add(new SqlParameter(paramName, SqlDbType.Int) { Value = attrNums[i] });
                    }

                    queryBuilder.Append(" ORDER BY 2");

                    command.CommandText = queryBuilder.ToString();
                    command.Parameters.AddRange(parameters.ToArray());

                    conn.Open();
                    return command.ExecuteReader(CommandBehavior.CloseConnection); // Ensures connection is closed when the reader is disposed
                }
            }
            catch
            {
                conn.Dispose(); // Ensure connection cleanup on failure
                throw;
            }
        }

        private void GenerateRibbonBarGroups(XmlNode rootNode, int parentId)
        {
            foreach (XmlNode node in rootNode.ChildNodes)
            {
                if (!CheckNodeModuleAccess(node))
                {
                    continue;
                }
                string fileName;
                string folderPath;
                string filePath;
                string imgname;

                fileName = node.Attributes["image"] != null ? node.Attributes["image"].Value + ".png" : "";
                folderPath = Path.Combine(_env.WebRootPath, "images"); // Adjust the folder path as needed
                filePath = Path.Combine(folderPath, fileName);
                imgname = "";
                if (System.IO.File.Exists(filePath))
                {
                    imgname = fileName;
                }
                menuModels.Add(new MenuModel()
                {
                    MenuId = Convert.ToInt32(node.Attributes["Name"].Value),
                    ParentMenuId = parentId,
                    Title = node.Attributes["Text"] != null ? node.Attributes["Text"].Value.Replace(":", "<br>") : "-",
                    weblink = node.Attributes["weblink"] != null ? node.Attributes["weblink"].Value : "",
                    OnClick = node.Attributes["OnClick"] != null ? node.Attributes["OnClick"].Value : "#",
                    ImageName = imgname,
                    ImageIcon = node.Attributes["icon"] != null ? node.Attributes["icon"].Value : ""
                });
                if (node.HasChildNodes)
                    GenerateRibbonBarGroupsItems(node, Convert.ToInt32(node.Attributes["Name"].Value));
            }
        }

        private void GenerateRibbonBarGroupsItems(XmlNode rootNode, int parentId)
        {
            foreach (XmlNode node in rootNode.ChildNodes)
            {
                if (!CheckNodeModuleAccess(node))
                    continue;
                string fileName;
                string folderPath;
                string filePath;
                string imgname;

                fileName = node.Attributes["image"] != null ? node.Attributes["image"].Value + ".png" : "";
                folderPath = Path.Combine(_env.WebRootPath, "images"); // Adjust the folder path as needed
                filePath = Path.Combine(folderPath, fileName);
                imgname = "";
                if (System.IO.File.Exists(filePath))
                    imgname = fileName;
                menuModels.Add(new MenuModel()
                {
                    MenuId = Convert.ToInt32(node.Attributes["Name"].Value),
                    ParentMenuId = parentId,
                    Title = node.Attributes["Text"] != null ? node.Attributes["Text"].Value.Replace(":", "<br>") : "-",
                    weblink = node.Attributes["weblink"] != null ? node.Attributes["weblink"].Value : "",
                    OnClick = node.Attributes["OnClick"] != null ? node.Attributes["OnClick"].Value : "#",
                    ImageName = imgname,
                    ImageIcon = node.Attributes["icon"] != null ? node.Attributes["icon"].Value : ""
                });
                if (node.HasChildNodes)
                    AddRibbonbarGrpItems(node, Convert.ToInt32(node.Attributes["Name"].Value));
            }
        }

        public DateTime? TryDateTimeParse(string text)
        {
            DateTime date;
            if (DateTime.TryParse(text, out date))
                return date;
            return null;
        }

        private void AddRibbonbarGrpItems(XmlNode rootNode, int parentId)
        {

            try
            {
                var companyName = _httpContextAccessor.HttpContext?.Session.GetString("COMPANYNAME") as string;

                if (string.IsNullOrEmpty(companyName))
                {
                    _httpContextAccessor.HttpContext?.Response.Redirect("~/Login/Index");
                    return;
                }
                foreach (XmlNode node in rootNode.ChildNodes)
                {
                    if (!CheckNodeModuleAccess(node))
                    {
                        continue;
                    }
                    menuModels.Add(new MenuModel()
                    {
                        MenuId = node.Attributes["Name"] != null ? Convert.ToInt32(node.Attributes["Name"].Value) : 0,
                        ParentMenuId = parentId,
                        Title = node.Attributes["Text"] != null ? node.Attributes["Text"].Value : "",
                        weblink = node.Attributes["weblink"] != null ? node.Attributes["weblink"].Value : "",
                        OnClick = node.Attributes["OnClick"] != null ? node.Attributes["OnClick"].Value : "#",
                        AskPassword = string.Equals(node.Attributes["askpassword"]?.Value, "yes", StringComparison.OrdinalIgnoreCase)
                    });

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

        }

        public void MsgBox(string msgText)
        {
            /*
             * System.Media.SystemSounds.Asterisk.Play();
            msgText = Left(msgText.Trim(), 500);
            string lang = msgText;
            if (!string.IsNullOrWhiteSpace(Language) && Language.ToUpper() == "ENGLISH")
                lang = msgText.IndexOf("|-|") == 0 ? msgText.Replace("|-|", "") : Translate(msgText);
            RadMessageBox.Show(lang, Application.ProductName, MessageBoxButtons.OK, msgIcon);
            */
            Console.WriteLine(msgText);
        }

        public DataTable GetSqlData(string CommandText,
            string param_name = "", string param_value = "",
            string param_name2 = "", string param_value2 = "",
            string param_name3 = "", string param_value3 = "",
            string param_name4 = "", string param_value4 = "",
            string param_name5 = "", string param_value5 = "",
            string param_name6 = "", string param_value6 = "",
            string param_name7 = "", string param_value7 = "",
            string param_name8 = "", string param_value8 = "",
            string param_name9 = "", string param_value9 = "",
            string param_name10 = "", string param_value10 = "")
        {
            DataTable dataTable = new DataTable();

            using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter())
            {
                SqlCommand command = new SqlCommand
                {
                    Connection = _connectionProvider.GetConnection(),
                    CommandType = CommandType.Text,
                    CommandText = CommandText,
                    CommandTimeout = 1200
                };

                var parameters = new ValueTuple<string, string>[]
                {
                    new ValueTuple<string, string>(param_name, param_value),
                    new ValueTuple<string, string>(param_name2, param_value2),
                    new ValueTuple<string, string>(param_name3, param_value3),
                    new ValueTuple<string, string>(param_name4, param_value4),
                    new ValueTuple<string, string>(param_name5, param_value5),
                    new ValueTuple<string, string>(param_name6, param_value6),
                    new ValueTuple<string, string>(param_name7, param_value7),
                    new ValueTuple<string, string>(param_name8, param_value8),
                    new ValueTuple<string, string>(param_name9, param_value9),
                    new ValueTuple<string, string>(param_name10, param_value10)
                };

                foreach (var param in parameters)
                    if (!string.IsNullOrWhiteSpace(param.Item1))
                        command.Parameters.AddWithValue(param.Item1, param.Item2 ?? (object)DBNull.Value);

                sqlDataAdapter.SelectCommand = command;
                sqlDataAdapter.Fill(dataTable);
            }

            return dataTable;
        }

        public List<SelectListItem> GetAllSalesmansCodesList()
        {
            DataTable dataTable = GetSqlData("SELECT code from salesmen where iSNULL(inactive,0)=0 order by code asc");
            List<SelectListItem> salesmanList = new List<SelectListItem>();
            salesmanList.Add(new SelectListItem() { Text = "", Value = "" });
            if (DataTableOK(dataTable))
                foreach (DataRow dr in dataTable.Rows)
                    salesmanList.Add(new SelectListItem() { Text = dr["Code"].ToString().Trim(), Value = dr["Code"].ToString().Trim() });
            return salesmanList;
        }

        public List<SelectListItem> GetAllStoreCodesList()
        {
            List<SelectListItem> storeList = new List<SelectListItem>
            {
                new SelectListItem { Text = "", Value = "" }
            };

            try
            {
                using (SqlConnection conn = _connectionProvider.GetConnection())
                using (SqlCommand cmd = new SqlCommand("SELECT DISTINCT CODE FROM [stores]", conn))
                using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    foreach (DataRow row in dt.Rows)
                    {
                        string code = row["CODE"].ToString().Trim();
                        storeList.Add(new SelectListItem { Text = code, Value = code });
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message); // Optional: log this instead in production
            }

            return storeList;
        }

        public List<SelectListItem> GetAllRegisterCodesList()
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
                    dataAdapter.SelectCommand.CommandText = "select distinct REGISTER from PAYMENTS where (register is not null) and (register != '') order by register";

                    // Fill the datatable From adapter
                    dataAdapter.Fill(dataTable);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                List<SelectListItem> salesmanList = new List<SelectListItem>();
                salesmanList.Add(new SelectListItem() { Text = "", Value = "" });
                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        salesmanList.Add(new SelectListItem() { Text = dr["REGISTER"].ToString().Trim(), Value = dr["REGISTER"].ToString().Trim() });
                    }
                }
                return salesmanList;
            }
        }

        public List<SelectListItem> GetAllCategories()
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
                    dataAdapter.SelectCommand.CommandText = "SELECT distinct category from styles where category != '' and category is not null order by category";
                    dataAdapter.Fill(dataTable);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                List<SelectListItem> salesmanList = new List<SelectListItem>();
                salesmanList.Add(new SelectListItem() { Text = "", Value = "" });
                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        salesmanList.Add(new SelectListItem() { Text = dr["category"].ToString().Trim(), Value = dr["category"].ToString().Trim() });
                    }
                }
                return salesmanList;
            }
        }

        public List<SelectListItem> GetAllSubCategories()
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
                    dataAdapter.SelectCommand.CommandText = "SELECT distinct subcategory as subcat from subcats where subcategory != '' and subcategory is not null order by subcat";
                    dataAdapter.Fill(dataTable);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                List<SelectListItem> salesmanList = new List<SelectListItem>();
                salesmanList.Add(new SelectListItem() { Text = "", Value = "" });
                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        salesmanList.Add(new SelectListItem() { Text = dr["subcat"].ToString().Trim(), Value = dr["subcat"].ToString().Trim() });
                    }
                }
                return salesmanList;
            }
        }

        public List<SelectListItem> GetAllBrands()
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
                    dataAdapter.SelectCommand.CommandText = "SELECT distinct brand from styles where brand != '' and brand is not null order by brand";

                    // Fill the datatable From adapter
                    dataAdapter.Fill(dataTable);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                List<SelectListItem> salesmanList = new List<SelectListItem>();
                salesmanList.Add(new SelectListItem() { Text = "", Value = "" });
                if (dataTable.Rows.Count > 0)
                    foreach (DataRow dr in dataTable.Rows)
                        salesmanList.Add(new SelectListItem() { Text = dr["brand"].ToString().Trim(), Value = dr["brand"].ToString().Trim() });
                return salesmanList;
            }
        }

        public List<SelectListItem> GetAllMetals()
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
                    dataAdapter.SelectCommand.CommandText = "SELECT distinct metal from styles where metal != '' and metal is not null order by metal";

                    // Fill the datatable From adapter
                    dataAdapter.Fill(dataTable);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                List<SelectListItem> salesmanList = new List<SelectListItem>();
                salesmanList.Add(new SelectListItem() { Text = "", Value = "" });
                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        salesmanList.Add(new SelectListItem() { Text = dr["metal"].ToString().Trim(), Value = dr["metal"].ToString().Trim() });
                    }
                }
                return salesmanList;
            }
        }

        public List<SelectListItem> GetAllVendors()
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
                    dataAdapter.SelectCommand.CommandText = "select distinct ACC AS cast_code from vendors where ACC <>'' and ACC is not null order by ACC";

                    // Fill the datatable From adapter
                    dataAdapter.Fill(dataTable);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                List<SelectListItem> salesmanList = new List<SelectListItem>();
                salesmanList.Add(new SelectListItem() { Text = "", Value = "" });
                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        salesmanList.Add(new SelectListItem() { Text = dr["cast_code"].ToString().Trim(), Value = dr["cast_code"].ToString().Trim() });
                    }
                }
                return salesmanList;
            }
        }

        public List<SelectListItem> GetAllGroups()
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
                    dataAdapter.SelectCommand.CommandText = "SELECT * FROM Groups order by 1";

                    // Fill the datatable From adapter
                    dataAdapter.Fill(dataTable);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                List<SelectListItem> salesmanList = new List<SelectListItem>();
                salesmanList.Add(new SelectListItem() { Text = "", Value = "" });
                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        salesmanList.Add(new SelectListItem() { Text = dr["GROUP"].ToString().Trim(), Value = dr["GROUP"].ToString().Trim() });
                    }
                }
                return salesmanList;
            }
        }

        public List<SelectListItem> GetAllStores()
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
                    dataAdapter.SelectCommand.CommandText = "SELECT DISTINCT CODE FROM [stores] where code != '' and code is not null  order by code asc ";

                    // Fill the datatable From adapter
                    dataAdapter.Fill(dataTable);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                List<SelectListItem> salesmanList = new List<SelectListItem>();
                salesmanList.Add(new SelectListItem() { Text = "", Value = "" });
                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        salesmanList.Add(new SelectListItem() { Text = dr["CODE"].ToString().Trim(), Value = dr["CODE"].ToString().Trim() });
                    }
                }
                return salesmanList;
            }
        }

        public List<SelectListItem> GetAllSources()
        {

            DataTable dataTable = GetSqlData("SELECT [source] as Sources FROM sources  ORDER BY Sources");
            List<SelectListItem> salesmanList = new List<SelectListItem>();
            salesmanList.Add(new SelectListItem() { Text = "", Value = "" });
            if (DataTableOK(dataTable))
                foreach (DataRow dr in dataTable.Rows)
                    salesmanList.Add(new SelectListItem() { Text = dr["Sources"].ToString().Trim(), Value = dr["Sources"].ToString().Trim() });
            return salesmanList;
        }

        public List<SelectListItem> GetAllGLClasses()
        {
            DataTable dataTable = GetSqlData("select trim(CLASS_GL) CLASS_GL,trim(ASSET_GL) ASSET_GL,ltrim(rtrim(CLEAR_GL)) CLEAR_GL,ltrim(rtrim(COGS_GL)) COGS_GL,trim(SALES_GL) SALES_GL,trim(DISC_GL) DISC_GL from CLASSGLS");
            List<SelectListItem> salesmanList = new List<SelectListItem>();
            salesmanList.Add(new SelectListItem() { Text = "", Value = "" });
            if (DataTableOK(dataTable))
                foreach (DataRow dr in dataTable.Rows)
                    salesmanList.Add(new SelectListItem() { Text = dr["CLASS_GL"].ToString().Trim(), Value = dr["CLASS_GL"].ToString().Trim() });
            return salesmanList;
        }

        public List<SelectListItem> GetAllCustomerCodes()
        {
            DataTable dataTable = GetSqlData("select acc,Id as cid from customer where 1=1 order by acc");
            List<SelectListItem> salesmanList = new List<SelectListItem>();
            if (DataTableOK(dataTable))
                foreach (DataRow dr in dataTable.Rows)
                    salesmanList.Add(new SelectListItem() { Text = dr["acc"].ToString().Trim(), Value = dr["CID"].ToString().Trim() });
            return salesmanList;
        }

        public bool CheckModuleEnabled(Modules module)
        {
            // Use the injected IHttpContextAccessor to access the HttpContext
            if (_httpContextAccessor.HttpContext?.Session != null &&
                _httpContextAccessor.HttpContext.Session.TryGetValue("PrintMode", out _))
            {
                PrintMode = Encoding.UTF8.GetString(_httpContextAccessor.HttpContext.Session.Get("PrintMode"));
            }

            if (string.IsNullOrWhiteSpace(PrintMode))
                return true;

            int optionindex = (int)Enum.Parse(typeof(Modules), module.ToString().Replace("-", "_"));
            string dec = DecryptModule(PrintMode);
            if (dec.Length > 38 + optionindex)
                return dec.Substring(38 + (optionindex - 1), 1) == "1";

            return false;
        }

        public string[] SplitIntoChunks(string text, int chunkSize, bool truncateRemaining)
        {
            string chunk = chunkSize.ToString();
            string pattern = truncateRemaining ? ".{" + chunk + "}" : ".{1," + chunk + "}";

            string[] chunks = null;
            if (chunkSize > 0 && !String.IsNullOrWhiteSpace(text))
                chunks = (from Match m in Regex.Matches(text, pattern) select m.Value).ToArray();

            return chunks;
        }

        public string DecryptModule(string value)
        {
            List<char> LicensingData = new List<char>();
            string[] chunks = SplitIntoChunks(value, 4, true);
            int ctr = 1;
            foreach (string str in chunks)
            {
                LicensingData.Add((char)(Convert.ToInt16(str.TrimStart(new Char[] { '0' })) / (ctr <= 126 ? ctr : ctr % 126)));
                ctr++;
            }
            return string.Join("", LicensingData.ToArray());
        }

        public List<SelectListItem> GetAllBrandsFromStyle()
        {

            DataTable dataTable = GetSqlData("select distinct BRAND from BRANDS where BRAND != '' and BRAND is not null order by BRAND");
            List<SelectListItem> salesmanList = new List<SelectListItem>();
            if (DataTableOK(dataTable))
                foreach (DataRow dr in dataTable.Rows)
                    salesmanList.Add(new SelectListItem() { Text = dr["brand"].ToString().Trim(), Value = dr["brand"].ToString().Trim() });
            return salesmanList;
        }

        public DataTable CheckValidBill(string inv_no)
        {
            DataTable dataTable = new DataTable();

            using (SqlDataAdapter dataAdapter = new SqlDataAdapter())
            {
                try
                {
                    dataAdapter.SelectCommand = new SqlCommand();
                    dataAdapter.SelectCommand.Connection = _connectionProvider.GetConnection();

                    dataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                    dataAdapter.SelectCommand.CommandText = "PRINTBILL";

                    dataAdapter.SelectCommand.Parameters.AddWithValue("@inv_no", inv_no);
                    // Fill the datatable From adapter
                    dataAdapter.Fill(dataTable);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
            return dataTable;
        }

        public List<SelectListItem> GetMultiStylesImages(string styles)
        {
            DataTable dataTable = GetSqlData("SELECT STYLE, DESCRIPTION FROM styl_images WHERE STYLE IN (" + styles + ")");
            List<SelectListItem> salesmanList = new List<SelectListItem>();
            salesmanList.Add(new SelectListItem() { });
            if (DataTableOK(dataTable))
                foreach (DataRow dr in dataTable.Rows)
                    salesmanList.Add(new SelectListItem() { Text = dr["STYLE"].ToString().Trim(), Value = dr["DESCRIPTION"].ToString().Trim() });
            return salesmanList;
        }

        public DataTable GetCogValues(DataTable DtCog)
        {
            if (DataTableOK(DtCog))
            {
                if (!DtCog.Columns.Contains("COGs"))
                    DtCog.Columns.Add("COGs", typeof(decimal));
                foreach (DataRow row in DtCog.Rows)
                {
                    string tst_inv = row["Inv_no"]?.ToString();
                    if (tst_inv != null && tst_inv == "TEST1111")
                    {

                    }
                    string invno = row["Inv_no"].ToString();
                    int invnolength = row["Inv_no"].ToString().Length - 6;
                    string Inv_no = "";
                    string cogval = "";

                    if (!string.IsNullOrEmpty(invno))  //invno != ""
                    {
                        int iLength = row["Inv_no"].ToString().Length;
                        if (!string.IsNullOrEmpty(iLength.ToString()))
                        {
                            try
                            {
                                int elength = row["Inv_no"].ToString().Length - 6;
                                if (elength >= -1)
                                    Inv_no = row["Inv_no"].ToString().Trim().Substring(elength);
                                else
                                    Inv_no = row["Inv_no"].ToString();
                            }
                            catch (Exception)
                            {
                            }
                        }
                        //Inv_no = row["Inv_no"].ToString().Substring(row["Inv_no"].ToString().Length - 6); row["inv_no"].ToString();
                        cogval = GetCogValues(Inv_no);
                    }
                    if (DecimalCheckForDBNull(cogval) != 0)// Dont check here return cogs vlaue with 0.00 becuase function retun cogs vlaue 0.00000 when add_cog=0.
                    {
                        row["COGs"] = cogval;
                        row["GrossProfit"] = Convert.ToDecimal(row["SubTotal"]) - Convert.ToDecimal(cogval);
                    }
                }
                DtCog.AcceptChanges();
            }
            return DtCog;
        }

        public decimal DecimalCheckForDBNull(object objval)
        {
            return objval != DBNull.Value && !string.IsNullOrWhiteSpace(Convert.ToString(objval)) ? Convert.ToDecimal(objval.ToString()) : 0;
        }

        public dynamic CheckForDBNull(object objval, Type varType)
        {
            switch (varType.FullName)
            {
                case "System.String":
                    return objval != DBNull.Value && objval != null ? objval.ToString() : (dynamic)"";
                case "System.Int32":
                    return objval != DBNull.Value && objval != null ? Convert.ToInt32(objval) : (dynamic)0;
                case "System.Decimal":
                    return objval != DBNull.Value && objval != null ? Convert.ToDecimal(objval) : (dynamic)0;
                case "System.Boolean":
                    return objval != DBNull.Value && objval != null ? Convert.ToBoolean(objval) : (dynamic)false;
                case "System.DateTime":
                    return objval != DBNull.Value && objval != null ? Convert.ToDateTime(objval) : (dynamic)DateTime.Now;
                default:
                    return objval != DBNull.Value && objval != null ? objval.ToString() : (dynamic)"";
            }
        }

        public string GetCogValues(string inv_no)
        {
            DataTable datatable = null;
            if (inv_no.Contains("Test"))
            {
                var invno = "'" + inv_no + "'";
                datatable = GetSqlData(@"select isnull(round((sum(pd.amount*pt.bank_fee/100)+max(isnull(i.t_cost,0)/([dbo].[noofsalesman](i.inv_no)))), 2),0) as Cog
                                from pay_item pd
                                join payments ph on ph.rtv_pay = 'p' and ph.inv_no = pd.pay_no
                                join PAYMENTTYPES pt on ph.PAYMENTTYPE = pt.PAYMENTTYPE
                                join invoice i on i.inv_no = pd.inv_no
                                where pd.rtv_pay = 'i' and pt.add_cog = 1 and pd.inv_no = " + invno);
            }

            return GetValue(datatable, "Cog");
        }

        public string GetValue(DataTable dt, string what)
        {
            if (DataTableOK(dt))
                return dt.Rows[0][what].ToString();
            return "";
        }

        public bool DataTableOK(DataTable dTable)
        {
            if (dTable == null)
                return false;
            return (dTable.Rows.Count > 0);
        }
        public bool DataTableOK(DataRow dTable)
        {
            if (dTable == null)
                return false;
            return (dTable.Table.Rows.Count > 0);
        }
        public bool DataTableOK(DataView dTable)
        {
            if (dTable == null)
                return false;
            return (dTable.Count > 0);
        }

        public DataRow GetSqlRow(string CommandText, string param_name1 = "", string param_value1 = "",
            string param_name2 = "", string param_value2 = "", string param_name3 = "", string param_value3 = "")
        {
            DataTable dataTable = new DataTable();
            DataRow dataRow;

            using (SqlDataAdapter dataAdapter = new SqlDataAdapter())
            {
                // Create the command and set its properties
                dataAdapter.SelectCommand = new SqlCommand();
                dataAdapter.SelectCommand.Connection = _connectionProvider.GetConnection();
                dataAdapter.SelectCommand.CommandType = CommandType.Text;
                dataAdapter.SelectCommand.CommandText = CommandText;
                if (!string.IsNullOrWhiteSpace(param_name1))
                    dataAdapter.SelectCommand.Parameters.AddWithValue(param_name1, param_value1);
                if (!string.IsNullOrWhiteSpace(param_name2))
                    dataAdapter.SelectCommand.Parameters.AddWithValue(param_name2, param_value2);
                if (!string.IsNullOrWhiteSpace(param_name3))
                    dataAdapter.SelectCommand.Parameters.AddWithValue(param_name3, param_value3);

                // Fill the datatable From adapter
                dataAdapter.Fill(dataTable);

                // Get the datarow from the table
                dataRow = GetRowOne(dataTable);

                return dataRow;
            }
        }

        public DataRow GetRowOne(DataTable dt)
        {
            return DataTableOK(dt) ? dt.Rows[0] : null;
        }

        public DataTable GetApCreditBillno(string creditno)
        {
            return GetSqlData(@"select INV_NO,AMOUNT from BILCHK where type='I' and pack_no in  (select  PACK  from APCREDIT WHERE trim(INV_NO)=@creditno) order by inv_no",
                "@creditno", creditno.Trim());
        }
        public DataTable GetStoreProc(string Procedurename, string param_name = "", string param_value = "",
            string param_name2 = "", string param_value2 = "", string param_name3 = "", string param_value3 = "",
            string param_name4 = "", string param_value4 = "", string param_name5 = "", string param_value5 = "",
            string param_name6 = "", string param_value6 = "", string param_name7 = "", string param_value7 = "",
            string param_name8 = "", string param_value8 = "", string param_name9 = "", string param_value9 = "",
            string param_name10 = "", string param_value10 = "", string param_name11 = "", string param_value11 = "",
            string param_name12 = "", string param_value12 = "", string param_name13 = "", string param_value13 = "",
            string param_name14 = "", string param_value14 = "", string param_name15 = "", string param_value15 = "",
            string param_name16 = "", string param_value16 = "", string param_name17 = "", string param_value17 = "",
            string param_name18 = "", string param_value18 = "", string param_name19 = "", string param_value19 = "",
            string param_name20 = "", string param_value20 = "", string param_name21 = "", string param_value21 = "",
            string param_name22 = "", string param_value22 = "", string param_name23 = "", string param_value23 = "",
            string param_name24 = "", string param_value24 = "", string param_name25 = "", string param_value25 = "",
            string param_name26 = "", string param_value26 = "")
        {
            DataTable dataTable = new DataTable();
            using (SqlDataAdapter SqlDataAdapter = new SqlDataAdapter())
            {
                // Create the command and set its properties
                SqlDataAdapter.SelectCommand = new SqlCommand();
                SqlDataAdapter.SelectCommand.Connection = _connectionProvider.GetConnection();
                SqlDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                if (!string.IsNullOrWhiteSpace(param_name))
                    SqlDataAdapter.SelectCommand.Parameters.AddWithValue(param_name, param_value);
                if (!string.IsNullOrWhiteSpace(param_name2))
                    SqlDataAdapter.SelectCommand.Parameters.AddWithValue(param_name2, param_value2);

                if (!string.IsNullOrWhiteSpace(param_name3))
                    SqlDataAdapter.SelectCommand.Parameters.AddWithValue(param_name3, param_value3);

                if (!string.IsNullOrWhiteSpace(param_name4))
                    SqlDataAdapter.SelectCommand.Parameters.AddWithValue(param_name4, param_value4);

                if (!string.IsNullOrWhiteSpace(param_name5))
                    SqlDataAdapter.SelectCommand.Parameters.AddWithValue(param_name5, param_value5);

                if (!string.IsNullOrWhiteSpace(param_name6))
                    SqlDataAdapter.SelectCommand.Parameters.AddWithValue(param_name6, param_value6);

                if (!string.IsNullOrWhiteSpace(param_name7))
                    SqlDataAdapter.SelectCommand.Parameters.AddWithValue(param_name7, param_value7);

                if (!string.IsNullOrWhiteSpace(param_name8))
                    SqlDataAdapter.SelectCommand.Parameters.AddWithValue(param_name8, param_value8);

                if (!string.IsNullOrWhiteSpace(param_name9))
                    SqlDataAdapter.SelectCommand.Parameters.AddWithValue(param_name9, param_value9);

                if (!string.IsNullOrWhiteSpace(param_name10))
                    SqlDataAdapter.SelectCommand.Parameters.AddWithValue(param_name10, param_value10);

                if (!string.IsNullOrWhiteSpace(param_name11))
                    SqlDataAdapter.SelectCommand.Parameters.AddWithValue(param_name11, param_value11);

                if (!string.IsNullOrWhiteSpace(param_name12))
                    SqlDataAdapter.SelectCommand.Parameters.AddWithValue(param_name12, param_value12);

                if (!string.IsNullOrWhiteSpace(param_name13))
                    SqlDataAdapter.SelectCommand.Parameters.AddWithValue(param_name13, param_value13);

                if (!string.IsNullOrWhiteSpace(param_name14))
                    SqlDataAdapter.SelectCommand.Parameters.AddWithValue(param_name14, param_value14);

                if (!string.IsNullOrWhiteSpace(param_name15))
                    SqlDataAdapter.SelectCommand.Parameters.AddWithValue(param_name15, param_value15);

                if (!string.IsNullOrWhiteSpace(param_name16))
                    SqlDataAdapter.SelectCommand.Parameters.AddWithValue(param_name16, param_value16);

                if (!string.IsNullOrWhiteSpace(param_name17))
                    SqlDataAdapter.SelectCommand.Parameters.AddWithValue(param_name17, param_value17);

                if (!string.IsNullOrWhiteSpace(param_name18))
                    SqlDataAdapter.SelectCommand.Parameters.AddWithValue(param_name18, param_value18);

                if (!string.IsNullOrWhiteSpace(param_name19))
                    SqlDataAdapter.SelectCommand.Parameters.AddWithValue(param_name19, param_value19);

                if (!string.IsNullOrWhiteSpace(param_name20))
                    SqlDataAdapter.SelectCommand.Parameters.AddWithValue(param_name20, param_value20);
                if (!string.IsNullOrWhiteSpace(param_name21))
                    SqlDataAdapter.SelectCommand.Parameters.AddWithValue(param_name21, param_value21);
                if (!string.IsNullOrWhiteSpace(param_name22))
                    SqlDataAdapter.SelectCommand.Parameters.AddWithValue(param_name22, param_value22);
                if (!string.IsNullOrWhiteSpace(param_name23))
                    SqlDataAdapter.SelectCommand.Parameters.AddWithValue(param_name23, param_value23);
                if (!string.IsNullOrWhiteSpace(param_name24))
                    SqlDataAdapter.SelectCommand.Parameters.AddWithValue(param_name24, param_value24);
                if (!string.IsNullOrWhiteSpace(param_name25))
                    SqlDataAdapter.SelectCommand.Parameters.AddWithValue(param_name25, param_value25);
                if (!string.IsNullOrWhiteSpace(param_name26))
                    SqlDataAdapter.SelectCommand.Parameters.AddWithValue(param_name26, param_value26);
                SqlDataAdapter.SelectCommand.CommandTimeout = 6000;

                // Assign the SQL to the command object
                SqlDataAdapter.SelectCommand.CommandText = Procedurename;

                // Fill the datatable from adapter
                SqlDataAdapter.Fill(dataTable);
                return dataTable;
            }
        }

        public DataTable GetBankAcc(string loggedstoreno = "")
        {
            return GetStoreProc("getdefaultbank", "@loggedstoreno", loggedstoreno);
        }

        public List<SelectListItem> GetAllBankCodes(string loggedstoreno = "")
        {

            DataTable dataTable = new DataTable();
            dataTable = GetBankAcc(loggedstoreno);
            List<SelectListItem> bankCodesList = new List<SelectListItem>();
            bankCodesList.Add(new SelectListItem() { Text = "", Value = "" });
            if (dataTable.Rows.Count > 0)
            {
                foreach (DataRow dr in dataTable.Rows)
                {
                    bankCodesList.Add(new SelectListItem() { Text = dr["CODE"].ToString().Trim(), Value = dr["CODE"].ToString().Trim() });
                }
            }
            return bankCodesList;
        }

        public List<SelectListItem> GetAllVendorStyles()
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
                    dataAdapter.SelectCommand.CommandText = "SELECT DISTINCT ISNULL(Vnd_Style,'') AS Vnd_style FROM STYLES WHERE Vnd_Style <> '' ORDER BY 1";

                    // Fill the datatable From adapter
                    dataAdapter.Fill(dataTable);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                List<SelectListItem> salesmanList = new List<SelectListItem>();
                salesmanList.Add(new SelectListItem() { Text = "", Value = "" });
                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        salesmanList.Add(new SelectListItem() { Text = dr["Vnd_style"].ToString().Trim(), Value = dr["Vnd_style"].ToString().Trim() });
                    }
                }
                return salesmanList;
            }
        }
        public List<SelectListItem> GetAllVendorsCodes()
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
                    dataAdapter.SelectCommand.CommandText = "select distinct cast_code from styles where cast_code <>'' and cast_code is not null order by cast_code";
                    //dataAdapter.SelectCommand.CommandText = "select distinct ACC AS cast_code from vendors where ACC <>'' and ACC is not null order by ACC";

                    // Fill the datatable From adapter
                    dataAdapter.Fill(dataTable);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                List<SelectListItem> salesmanList = new List<SelectListItem>();
                salesmanList.Add(new SelectListItem() { Text = "", Value = "" });
                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        salesmanList.Add(new SelectListItem() { Text = dr["cast_code"].ToString().Trim(), Value = dr["cast_code"].ToString().Trim() });
                    }
                }
                return salesmanList;
            }
        }

        public DataSet OnInitializeData_Combine_SP(string inv_no, bool isBriony, bool is_memo, bool @iSVatInclude, bool isopenmemo)
        {
            DataSet ds = new DataSet();
            using (SqlDataAdapter SqlDataAdapter = new SqlDataAdapter())
            {
                SqlDataAdapter.SelectCommand = new SqlCommand();
                SqlDataAdapter.SelectCommand.Connection = _connectionProvider.GetConnection();
                SqlDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter.SelectCommand.CommandText = "OnInitializeData_Combine_SP";
                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@Inv_no", inv_no);
                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@isBriony", isBriony);
                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@is_memo", is_memo);
                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@iSVatInclude", iSVatInclude);
                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@IsOpenMemo", isopenmemo);
                SqlDataAdapter.Fill(ds);
            }
            return ds;
        }

        public DataTable GetSalesSummaryReportDetails(string DateFrom, string DateTo, bool iSPartial, bool bypickdate, int type)
        {
            DataTable dataTable = new DataTable();

            using (SqlDataAdapter dataAdapter = new SqlDataAdapter())
            {
                // Create the command and set its properties
                dataAdapter.SelectCommand = new SqlCommand();
                dataAdapter.SelectCommand.Connection = _connectionProvider.GetConnection();
                dataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                dataAdapter.SelectCommand.CommandTimeout = 3000;
                dataAdapter.SelectCommand.CommandText = "SalesSummaryReport";
                dataAdapter.SelectCommand.Parameters.AddWithValue("@DateFrom", DateFrom);
                dataAdapter.SelectCommand.Parameters.AddWithValue("@DateTo", DateTo);
                dataAdapter.SelectCommand.Parameters.AddWithValue("@iSPartial", iSPartial);
                dataAdapter.SelectCommand.Parameters.AddWithValue("@bypickdate", bypickdate);
                dataAdapter.SelectCommand.Parameters.AddWithValue("@type", type);

                // Fill the datatable From adapter
                dataAdapter.Fill(dataTable);
                return dataTable;
            }
        }


        public DataTable GetQuoteData(string strQTNO)
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
                    dataAdapter.SelectCommand.CommandText = @"SELECT QN.DATE,QN.[USER] Salesman, QN.NOTE, Q.QN, Q.ACC, Q.[DATE], Q.STYLES, Q.[DESCRIPTION], Q.PRICE, Q.OPERATOR,Q.LOSTOPPORTUNITY,Q.Store_no,Q.ISPOTENTIALCUSTOMER,CAST(0  AS BIT)AS STATUSROW, QStatus, CallToCust_Date, CallToCust_Time FROM QUOTES Q LEFT JOIN qt_note QN ON QN.QN=Q.QN WHERE LTRIM(RTRIM(Q.QN))='" + strQTNO + "'";

                    dataAdapter.Fill(dataTable);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                return dataTable;
            }
        }

        public DataTable GetQtItems(string Qtno)
        {
            DataTable dataTable = new DataTable();

            using (SqlDataAdapter SqlDataAdapter = new SqlDataAdapter())
            {
                // Create the command and set its properties
                SqlDataAdapter.SelectCommand = new SqlCommand();
                SqlDataAdapter.SelectCommand.Connection = _connectionProvider.GetConnection();
                SqlDataAdapter.SelectCommand.CommandType = CommandType.Text;
                SqlDataAdapter.SelectCommand.CommandText = "select Style,Note [Desc],Price,Item_no from qt_items where qn=@Qtno";
                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@Qtno", Qtno);

                SqlDataAdapter.Fill(dataTable);
            }

            return dataTable;
        }

        public List<SalesQuotesWishlistModel> GetAllStyles()
        {

            DataTable dataTable = new DataTable();

            using (SqlDataAdapter dataAdapter = new SqlDataAdapter())
            {
                try
                {
                    dataAdapter.SelectCommand = new SqlCommand();
                    dataAdapter.SelectCommand.Connection = _connectionProvider.GetConnection();

                    dataAdapter.SelectCommand.CommandType = CommandType.Text;
                    dataAdapter.SelectCommand.CommandText = "SELECT STYLE,ISNULL([DESC],'')[DESC], ISNULL(PRICE,0)PRICE from styles  WHERE ISNULL(STYLE,'')<>'' and STYLE NOT IN (',','.','[',']') order by STYLE";
                    dataAdapter.Fill(dataTable);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                SalesQuotesWishlistModel objModel = new SalesQuotesWishlistModel();
                List<SalesQuotesWishlistModel> stylesList = new List<SalesQuotesWishlistModel>();

                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        stylesList.Add(new SalesQuotesWishlistModel() { STYLE = dr["STYLE"].ToString().Trim(), DESCRIPTION = dr["DESC"].ToString().Trim(), PRICE = dr["PRICE"] != DBNull.Value ? Convert.ToDecimal(dr["PRICE"]) : 0 });
                    }
                }
                return stylesList;
            }
        }

        public DataTable GetInvoiCostItems(String invNo)
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
                    dataAdapter.SelectCommand.CommandText = "select II.STYLE, II.QTY, II.PRICE, II.COST INTIME_COST, STY.T_COST STYLE_COST, II.INV_NO  from IN_ITEMS II JOIN STYLES STY ON II.STYLE = STY.STYLE where INV_NO ='" + invNo.PadLeft(6, ' ') + "'";

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

        public DataTable IsRepairInvoice(String invNo)
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
                    dataAdapter.SelectCommand.CommandText = "select 1 from INVOICE I JOIN IN_ITEMS II ON I.INV_NO = II.INV_NO where(II.REPAIR = 1 OR I.V_CTL_NO = 'REPAIR')  and I.INV_NO ='" + invNo.PadLeft(6, ' ') + "'";

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


        public DataTable SearchInvoice(string filter = "", bool isNoName = false)
        {
            string queryText = "";
            if (isNoName)
            {
                if (!string.IsNullOrWhiteSpace(filter))
                {
                    queryText = @"SELECT ISNULL(ID,0),INVOICE.INV_NO,INVOICE.ACC,INVOICE.NAME, 
						try_cast(customer.TEL as Nvarchar(30)) as TEL,INVOICE.DATE, IN_ITEMS.STYLE,
						IN_ITEMS.[DESC],GR_TOTAL, ISNULL(GR_TOTAL,0) - ISNULL(CREDITS,0) as BALANCE,[Message],
						INVOICE.INACTIVE FROM INVOICE LEFT OUTER JOIN CUSTOMER ON INVOICE.ACC = CUSTOMER.ACC     
						LEFT OUTER JOIN (SELECT INV_NO,MAX(STYLE)STYLE, MAX([DESC])[DESC]  
						FROM IN_ITEMS GROUP BY INV_NO)IN_ITEMS ON ((INVOICE.INV_NO))=((IN_ITEMS.INV_NO)) 
						where " + filter + " AND ISNULL(INVOICE.ACC,'') <>''ORDER BY DATE desc";
                }
                else
                {
                    queryText = @"SELECT ISNULL(ID,0),INVOICE.INV_NO,INVOICE.ACC,INVOICE.NAME,
					try_cast(customer.TEL as Nvarchar(30)) as TEL,INVOICE.DATE,IN_ITEMS.STYLE,
					IN_ITEMS.[DESC],GR_TOTAL, ISNULL(GR_TOTAL,0) - ISNULL(CREDITS,0) as BALANCE,[Message],
					INVOICE.INACTIVE FROM INVOICE LEFT JOIN CUSTOMER ON INVOICE.ACC = CUSTOMER.ACC 
					LEFT OUTER JOIN (SELECT INV_NO,MAX(STYLE)STYLE, MAX([DESC])[DESC]  
					FROM IN_ITEMS GROUP BY INV_NO)IN_ITEMS ON ((INVOICE.INV_NO))=((IN_ITEMS.INV_NO)) ORDER BY DATE desc";
                }


            }
            if (!string.IsNullOrWhiteSpace(filter))
            {
                queryText = @"SELECT ID,INVOICE.INV_NO,CUSTOMER.ACC,INVOICE.NAME, 
					try_cast(customer.TEL as Nvarchar(30)) as TEL,INVOICE.DATE, IN_ITEMS.STYLE,IN_ITEMS.[DESC],
					GR_TOTAL, ISNULL(GR_TOTAL,0) - ISNULL(CREDITS,0) as BALANCE,[Message],INVOICE.INACTIVE 
					FROM INVOICE INNER JOIN CUSTOMER ON INVOICE.ACC = CUSTOMER.ACC     
					LEFT OUTER JOIN (SELECT INV_NO,MAX(STYLE)STYLE, MAX([DESC])[DESC]  FROM IN_ITEMS GROUP BY INV_NO)
					IN_ITEMS ON ((INVOICE.INV_NO))=((IN_ITEMS.INV_NO)) 
					where " + filter + " AND ISNULL(INVOICE.ACC,'') <>''ORDER BY DATE desc";
            }
            else
            {
                queryText = @"SELECT ID,INVOICE.INV_NO,CUSTOMER.ACC,INVOICE.NAME,
				try_cast(customer.TEL as Nvarchar(30)) as TEL,INVOICE.DATE,IN_ITEMS.STYLE,IN_ITEMS.[DESC],
				GR_TOTAL, ISNULL(GR_TOTAL,0) - ISNULL(CREDITS,0) as BALANCE,[Message],INVOICE.INACTIVE 
				FROM INVOICE LEFT JOIN CUSTOMER ON INVOICE.ACC = CUSTOMER.ACC LEFT OUTER JOIN 
				(SELECT INV_NO,MAX(STYLE)STYLE, MAX([DESC])[DESC]  FROM IN_ITEMS GROUP BY INV_NO)IN_ITEMS 
				ON ((INVOICE.INV_NO))=((IN_ITEMS.INV_NO)) ORDER BY DATE desc";
            }

            DataTable dataTable = new DataTable();
            using (SqlDataAdapter SqlDataAdapter = new SqlDataAdapter())
            {
                // Create the command and set its properties
                SqlDataAdapter.SelectCommand = new SqlCommand();
                SqlDataAdapter.SelectCommand.Connection = _connectionProvider.GetConnection();
                SqlDataAdapter.SelectCommand.CommandType = CommandType.Text;

                SqlDataAdapter.SelectCommand.CommandTimeout = 3000;
                SqlDataAdapter.SelectCommand.CommandText = queryText;
                SqlDataAdapter.Fill(dataTable);
            }
            return dataTable;
        }

        public bool UpdateInTemCost(String invoiceItems, String invno)
        {
            using (SqlCommand dbCommand = new SqlCommand())
            {
                dbCommand.Connection = _connectionProvider.GetConnection();
                dbCommand.CommandType = CommandType.StoredProcedure;
                dbCommand.CommandText = "UpdateInTemCost";
                dbCommand.CommandTimeout = 12000;

                dbCommand.Parameters.AddWithValue("@inv_no", invno);
                SqlParameter parameter = new SqlParameter();
                parameter.ParameterName = "@invoiceItems";
                parameter.SqlDbType = System.Data.SqlDbType.Xml;
                parameter.Value = invoiceItems;
                dbCommand.Parameters.Add(parameter);

                // Open the connection, execute the query and close the connection
                dbCommand.Connection.Open();
                var rowsAffected = dbCommand.ExecuteNonQuery();
                dbCommand.Connection.Close();
                return rowsAffected > 0;
            }
        }

        #region  SalesTaxReport Methods

        public DataTable getdatafromdbbasedoncondition(string field, string table, string conditon)
        {
            return GetSqlData("select " + field + " from " + table + " where " + conditon);
        }

        public String GetRepairOrderNoFromInvoice(String invoiceNo)
        {
            DataTable dataTable = GetSqlData($"SELECT PON FROM INVOICE WHERE INV_NO=@InvNo AND V_CTL_NO='REPAIR'", "@InvNo", invoiceNo);
            return DataTableOK(dataTable) ? Convert.ToString(dataTable.Rows[0]["PON"]) : "";
        }

        public bool iSRepairReturn(String inv_no)
        {
            DataTable dtOption =
                GetSqlData($@"select 1 from invoice where inv_no=@inv_no and v_ctl_no='REPAIR' and gr_total<0", "@inv_no", inv_no);
            return DataTableOK(dtOption);
        }
        public DataTable GetTradeInDataByInvoice(string inv_no)
        {
            return GetSqlData("SELECT ACC,INV_NO,STORE_NO,DATE,TRADEINDESC,TRADEINAMT,SALESMAN1,NAME,store_no, iSCompanyName2 FROM INVOICE WHERE LTRIM(RTRIM(INV_NO))=@inv_no AND ISNULL(NULLIF(TRADEINAMT,0),0)>0", "@inv_no", inv_no.Trim());
        }
        public string GetUserGCNo(string INVNO)
        {
            DataTable dataTable = GetSqlData("Select isnull(nullif(UserGCNo,''),'') UserGCNo from StoreCreditVoucher where trim(isnull(nullif(CreditNo,''),''))=@INVNO ",
                "@INVNO", INVNO.Trim());
            return GetValue(dataTable, "UserGCNo");
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
        #endregion

        public List<SelectListItem> GetAllItemTypes()
        {

            DataTable dataTable = GetSqlData("select distinct item_type from ITEMTYPE where item_type != '' and item_type is not null order by item_type");
            List<SelectListItem> salesmanList = new List<SelectListItem>();
            salesmanList.Add(new SelectListItem() { Text = "", Value = "" });
            if (DataTableOK(dataTable))
                foreach (DataRow dr in dataTable.Rows)
                    salesmanList.Add(new SelectListItem() { Text = dr["item_type"].ToString().Trim(), Value = dr["item_type"].ToString().Trim() });
            return salesmanList;
        }

        public List<SelectListItem> GetAllSubBrands()
        {
            DataTable dataTable = GetSqlData("select distinct subbrand from SUBBRANDS where subbrand != '' and subbrand is not null order by subbrand");
            List<SelectListItem> salesmanList = new List<SelectListItem>();
            salesmanList.Add(new SelectListItem() { Text = "", Value = "" });
            if (DataTableOK(dataTable))
                foreach (DataRow dr in dataTable.Rows)
                    salesmanList.Add(new SelectListItem() { Text = dr["subbrand"].ToString().Trim(), Value = dr["subbrand"].ToString().Trim() });
            return salesmanList;
        }

        public List<SelectListItem> GetAllCenterStoneColor()
        {
            DataTable dataTable = GetSqlData("select distinct COLOR from DCOLORS where COLOR != '' and COLOR is not null order by COLOR");
            List<SelectListItem> salesmanList = new List<SelectListItem>();
            salesmanList.Add(new SelectListItem() { Text = "", Value = "" });
            if (DataTableOK(dataTable))
                foreach (DataRow dr in dataTable.Rows)
                    salesmanList.Add(new SelectListItem() { Text = dr["COLOR"].ToString().Trim(), Value = dr["COLOR"].ToString().Trim() });
            return salesmanList;
        }

        public List<SelectListItem> GetAllCenterStoneClarity()
        {

            DataTable dataTable = GetSqlData("select distinct CLARITY from CLARITIES where CLARITY != '' and CLARITY is not null order by CLARITY");
            List<SelectListItem> salesmanList = new List<SelectListItem>();
            salesmanList.Add(new SelectListItem() { Text = "", Value = "" });
            if (DataTableOK(dataTable))
                foreach (DataRow dr in dataTable.Rows)
                    salesmanList.Add(new SelectListItem() { Text = dr["CLARITY"].ToString().Trim(), Value = dr["CLARITY"].ToString().Trim() });
            return salesmanList;
        }

        public List<SelectListItem> GetAllCenterStoneShapes()
        {

            DataTable dataTable = GetSqlData("select distinct SHAPE from SHAPES where SHAPE != '' and SHAPE is not null order by SHAPE");
            List<SelectListItem> salesmanList = new List<SelectListItem>();
            salesmanList.Add(new SelectListItem() { Text = "", Value = "" });
            if (DataTableOK(dataTable))
                foreach (DataRow dr in dataTable.Rows)
                    salesmanList.Add(new SelectListItem() { Text = dr["SHAPE"].ToString().Trim(), Value = dr["SHAPE"].ToString().Trim() });
            return salesmanList;
        }

        public List<SelectListItem> GetAllCenterTypes()
        {

            DataTable dataTable = GetSqlData("select distinct CENTER_TYPE from CENTER_TYPES where CENTER_TYPE != '' and CENTER_TYPE is not null order by CENTER_TYPE");
            List<SelectListItem> salesmanList = new List<SelectListItem>();
            salesmanList.Add(new SelectListItem() { Text = "", Value = "" });
            if (DataTableOK(dataTable))
                foreach (DataRow dr in dataTable.Rows)
                    salesmanList.Add(new SelectListItem() { Text = dr["CENTER_TYPE"].ToString().Trim(), Value = dr["CENTER_TYPE"].ToString().Trim() });
            return salesmanList;
        }

        public string CheckValidCustomerCode(string ACC, bool is_glenn, bool iSWrist = false)
        {
            DataRow rw;
            if (iSWrist)
            {
                rw = GetSqlRow("select [NAME2] NAME From Customer Where trim(acc)=trim(@acc) or old_customer=trim(@acc)", "@acc", ACC);
                if (rw == null || String.IsNullOrWhiteSpace(Convert.ToString(rw["NAME"])))
                    rw = GetSqlRow("select *  From Customer Where trim(acc)=trim(@acc) or old_customer=trim(@acc)", "@acc", ACC);
                else
                    rw = GetSqlRow("select [NAME2] NAME, [ADDR2] ADDR1,[ADDR22] ADDR12,[CITY2] CITY1,[STATE2] STATE1,[ZIP2] ZIP1,ADDR13,[COUNTRY2] COUNTRY,[TEL2] TEL,*  From Customer Where trim(acc)=trim(@acc) or old_customer=trim(@acc)", "@acc", ACC);
            }
            rw = GetSqlRow("select *  From Customer Where trim(acc)=trim(@acc) or old_customer=trim(@acc)", "@acc", ACC);

            if (rw == null)
                return "0";
            return "1";
        }

        public string CheckValidPotCustomerCode(string ACC)
        {
            DataRow rw;
            rw = GetSqlRow("select *  From MAILING Where ltrim(rtrim(acc))=ltrim(rtrim(@acc))", "@acc", ACC);

            if (rw == null)
                return "0";
            return "1";
        }

        public List<SelectListItem> GetDistPaymentType()
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
                    dataAdapter.SelectCommand.CommandText = "select distinct PAYMENTTYPE from PAYMENTTYPES";

                    // Fill the datatable From adapter
                    dataAdapter.Fill(dataTable);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                List<SelectListItem> salesmanList = new List<SelectListItem>();
                //salesmanList.Add(new SelectListItem() { Text = "", Value = "" });
                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        salesmanList.Add(new SelectListItem() { Text = dr["PAYMENTTYPE"].ToString().Trim(), Value = dr["PAYMENTTYPE"].ToString().Trim() });
                    }
                }
                return salesmanList;
            }
        }


        public string GetNextSeqNo(string TableName, string FieldName, string MaxLimit, string PField, string MinLimit, string PValue)
        {
            var error = string.Empty;

            try
            {
                if (string.IsNullOrEmpty(TableName))
                    throw new System.ArgumentException("Tablename Not Defined");
                using (SqlCommand dbCommand = new SqlCommand())
                {
                    // Set the command object properties
                    dbCommand.Connection = _connectionProvider.GetConnection();
                    dbCommand.CommandType = CommandType.StoredProcedure;
                    dbCommand.CommandText = "GetNextKey";
                    dbCommand.CommandTimeout = 1200;

                    dbCommand.Parameters.AddWithValue("@tName", TableName.Trim());
                    dbCommand.Parameters.AddWithValue("@fName", FieldName.Trim());
                    dbCommand.Parameters.AddWithValue("@maxlimit", MaxLimit);
                    dbCommand.Parameters.AddWithValue("@pfield", PField.Trim());
                    dbCommand.Parameters.AddWithValue("@minlimit", MinLimit);
                    dbCommand.Parameters.AddWithValue("@pval", PValue.Trim());

                    SqlParameter outInvno = new SqlParameter("@lastinsertid", SqlDbType.NVarChar, 550);
                    outInvno.Direction = ParameterDirection.Output;
                    dbCommand.Parameters.Add(outInvno);

                    // Open the connection, execute the query and close the connection
                    dbCommand.Connection.Open();
                    var rowsAffected = dbCommand.ExecuteNonQuery();

                    dbCommand.Connection.Close();
                    string NextKey = ((string)outInvno.Value).PadLeft(6, ' ');
                    //this.IsCancelled = true;
                    return NextKey;

                }
            }
            catch (Exception ex)
            {
                MsgBox(ex.Message);
                //Error = ex.Message;
                return string.Empty;
            }
        }

        public string GetNextSeqNo()
        {
            var error = string.Empty;
            string TableName = string.Empty;
            string FieldName = string.Empty;
            string MaxLimit = string.Empty; string PField = string.Empty; string MinLimit = string.Empty; string PValue = string.Empty;
            bool IsCancelled = false;
            string NextKey = string.Empty;

            try
            {


                if (string.IsNullOrEmpty(TableName))
                    throw new System.ArgumentException("Tablename Not Defined");
                using (SqlCommand dbCommand = new SqlCommand())
                {
                    // Set the command object properties
                    dbCommand.Connection = _connectionProvider.GetConnection();
                    dbCommand.CommandType = CommandType.StoredProcedure;
                    dbCommand.CommandText = "GetNextKey";
                    dbCommand.CommandTimeout = 1200;

                    dbCommand.Parameters.AddWithValue("@tName", TableName.Trim());
                    dbCommand.Parameters.AddWithValue("@fName", FieldName.Trim());
                    dbCommand.Parameters.AddWithValue("@maxlimit", MaxLimit);
                    dbCommand.Parameters.AddWithValue("@pfield", PField.Trim());
                    dbCommand.Parameters.AddWithValue("@minlimit", MinLimit);
                    dbCommand.Parameters.AddWithValue("@pval", PValue.Trim());

                    SqlParameter outInvno = new SqlParameter("@lastinsertid", SqlDbType.NVarChar, 550);
                    outInvno.Direction = ParameterDirection.Output;
                    dbCommand.Parameters.Add(outInvno);

                    // Open the connection, execute the query and close the connection
                    dbCommand.Connection.Open();
                    var rowsAffected = dbCommand.ExecuteNonQuery();

                    dbCommand.Connection.Close();
                    NextKey = ((string)outInvno.Value).PadLeft(6, ' ');
                    return NextKey;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                error = ex.Message;
                return string.Empty;
            }
        }

        public DataTable DaySalesSummary(string FROMDATE, string DateTo, string SelGroup, string SelIndividual, bool IsPartial = false, bool ByWhichDate = false, String GkOption = "1", bool SayGst = false)
        {
            DataTable dataTable = new DataTable();
            using (SqlDataAdapter SqlDataAdapter = new SqlDataAdapter())
            {
                SqlDataAdapter.SelectCommand = new SqlCommand();
                SqlDataAdapter.SelectCommand.Connection = _connectionProvider.GetConnection();
                SqlDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter.SelectCommand.CommandText = "Day_Sales_Summary";
                SqlDataAdapter.SelectCommand.CommandTimeout = 6000;
                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@date1", FROMDATE);
                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@date2", DateTo);

                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@selGroup", SelGroup);
                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@selIndividual", SelIndividual);
                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@iSPartial", IsPartial);
                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@byPickupDate", ByWhichDate);
                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@GkOption", GkOption);
                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@isgst", SayGst);
                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@Fixstorename", FixedStoreCode);

                SqlDataAdapter.Fill(dataTable);
            }
            return dataTable;
        }

        public DataTable GetStores(string storecode = "", bool addEmptyStore = false)
        {
            if (!string.IsNullOrWhiteSpace(FixedStoreCode))
                return GetSqlData("SELECT * FROM [stores] where code=@code ", "@code", FixedStoreCode);
            if (!string.IsNullOrWhiteSpace(storecode))
                return GetSqlData("select top 1 * from stores where code = @storecode", "@storecode", storecode);
            return GetSqlData(!addEmptyStore ? "select * from stores where inactive  = 0" : "select '' code UNION select code from stores where inactive = 0 order by code");
        }
        public DataTable ListofVoidedChecks(int dateval, DateTime fromDate, DateTime toDate)
        {
            DataTable dataTable = new DataTable();

            using (SqlDataAdapter SqlDataAdapter = new SqlDataAdapter())
            {
                SqlDataAdapter.SelectCommand = new SqlCommand();
                SqlDataAdapter.SelectCommand.Connection = _connectionProvider.GetConnection();
                SqlDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter.SelectCommand.CommandText = "ListofVOidedChecks";
                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@DATEVAL", dateval);
                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@DATE1", fromDate);
                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@DATE2", toDate);

                SqlDataAdapter.Fill(dataTable);
            }
            return dataTable;
        }

        #region Report1099 By Vivek

        public DataTable GetReport1099(string dtFrom, string dtTo, string store = "")
        {

            DataTable dataTable = new DataTable();
            string[] fromdate = dtFrom.Split(' ');
            string dtf = fromdate[0];
            string[] todate = dtTo.Split(' ');
            string dtt = todate[0];

            using (SqlDataAdapter SqlDataAdapter = new SqlDataAdapter())
            {
                SqlDataAdapter.SelectCommand = new SqlCommand();
                SqlDataAdapter.SelectCommand.Connection = _connectionProvider.GetConnection();
                SqlDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter.SelectCommand.CommandText = "Report1099";

                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@CDATE1", dtf);
                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@CDATE2", dtt);
                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@store", store);

                SqlDataAdapter.SelectCommand.CommandTimeout = 0;

                SqlDataAdapter.Fill(dataTable);
            }
            return dataTable;
        }
        #endregion


        #region List of Invoices


        public string ServerDateFormat = "", SqlCulture = "", LocalDateFormat = "";
        //private object CultureInfo;

        public DateTime? StartDate(string sdate, DateTime Ddate)
        {
            if (string.IsNullOrWhiteSpace(sdate) || BadYear(Ddate))
                return new DateTime(1900, 01, 01);
            return setSQLDateTime(Ddate.Date);
        }

        public DateTime? EndDate(string sdate, DateTime Ddate)
        {
            if (string.IsNullOrWhiteSpace(sdate) || BadYear(Ddate))
                return new DateTime(9998, 12, 31);
            return setSQLDateTime(Ddate.Date);
        }
        public bool BadYear(DateTime tdate, bool CheckLock = false)
        {
            if (tdate.Year < 1800)
            {
                //MsgBox("Invalid Date: " + tdate);
                return true;
            }
            if (CheckLock && tdate < LockDate)
            {
                //MsgBox(GetLang("Date (") + tdate.Date.ToShortDateString() + ") cannot be before " +
                //   LockDate.Date.ToShortDateString());
                return true;
            }
            return false;
        }

        public DataTable GetListOfInvoices(string strSearch)
        {
            DataTable dataTable = new DataTable();

            using (SqlDataAdapter SqlDataAdapter = new SqlDataAdapter())
            {
                // Create the command and set its properties

                SqlDataAdapter.SelectCommand = new SqlCommand();
                SqlDataAdapter.SelectCommand.Connection = _connectionProvider.GetConnection();
                SqlDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter.SelectCommand.CommandTimeout = 3000;
                SqlDataAdapter.SelectCommand.CommandText = "InvoiceDetails";
                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@strSearch", strSearch);

                SqlDataAdapter.Fill(dataTable);
            }

            return dataTable;
        }
        public DateTime setSQLDateTime(DateTime date)
        {
            if (CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern.ToString() == GetSeverDateFormat().ToString())
                return Convert.ToDateTime(date);
            System.Threading.Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo(SqlCulture);
            return DateTime.Parse(date.ToString(), CultureInfo.GetCultureInfo(SqlCulture).DateTimeFormat);
        }

        public string GetSeverDateFormat(bool isLocal = false)
        {
            if (!isLocal && ServerDateFormat != "")
                return ServerDateFormat;
            DataRow dr;
            string sysFormat = !isLocal ? CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern : CheckForDBNull(CultureInfo.DefaultThreadCurrentCulture) == "" ? CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern : CultureInfo.DefaultThreadCurrentCulture.DateTimeFormat.ShortDatePattern;
            string retformat = "";

            if (isLocal)
            {
                switch (sysFormat.ToLower())
                {
                    case "m/d/yyyy":
                    case "mm/d/yyyy":
                    case "mm/dd/yyyy":
                    case "m/dd/yyyy":
                    case "d/m/yyyy":
                    case "dd/m/yyyy":
                    case "dd/mm/yyyy":
                    case "d/mm/yyyy":
                    case "yyyy/m/d":
                    case "yyyy/mm/d":
                    case "yyyy/mm/dd":
                    case "yyyy/m/dd":
                        LocalDateFormat = sysFormat.ToLower().Replace("m", "M");
                        break;
                    default:
                        LocalDateFormat = sysFormat;
                        break;
                }
                return LocalDateFormat;
            }

            dr = GetSqlRow("select * from sys.syslanguages where name =@@LANGUAGE");
            if (dr != null)
            {
                switch (dr["dateformat"].ToString().ToLower())
                {
                    case "mdy":
                        retformat = "MM/dd/yyyy";
                        break;
                    case "dmy":
                        retformat = "dd/MM/yyyy";
                        break;
                    case "ymd":
                        retformat = "yyyy/MM/dd";
                        break;
                }
                ServerDateFormat = retformat;
            }
            return ServerDateFormat;
        }
        #endregion


        public DataTable GetChecknoWithAccinPayTm(string reciptno = "", string Acc = "")
        {
            return GetSqlData($"select * from payments where  ltrim(rtrim(acc))=ltrim(rtrim('{EscapeSpecialCharacters(Acc)}')) and check_no='{reciptno}'  and PAYMENTTYPE = 'CHECK'");
        }
        public string EscapeSpecialCharacters(string Name, bool digits = false)
        {
            Name = Name.Trim();
            StringBuilder sb = new StringBuilder(Name.Length);
            for (int i = 0; i < Name.Length; i++)
            {
                char c = Name[i];
                if (digits)
                {
                    if (c >= '0' && c <= '9')
                        sb.Append(c);
                }
                else
                {
                    switch (c)
                    {
                        case ']':
                        case '[':
                        case '%':
                        case '*':
                            sb.Append("[" + c + "]");
                            break;
                        case '\'':
                            sb.Append("''");
                            break;
                        default:
                            sb.Append(c);
                            break;
                    }
                }
            }
            return sb.ToString();
        }
        public bool CheckReceiptExistOrNot(string RecNo, string RTV_PAY)
        {
            DataTable datatable = GetSqlData("SELECT * FROM PAYMENTS WHERE RTV_PAY = @RTV_PAY AND TRIM(INV_NO) = @RecNo",
                 "@RecNo", RecNo.Trim(), "@RTV_PAY", RTV_PAY);
            return (datatable.Rows.Count > 0);
        }

        public DataTable KingsSalesSummary(DateTime? posDate)
        {
            DataTable dt = new DataTable();
            using (SqlDataAdapter SqlDataAdapter = new SqlDataAdapter())
            {
                SqlDataAdapter.SelectCommand = new SqlCommand();
                SqlDataAdapter.SelectCommand.Connection = _connectionProvider.GetConnection();
                SqlDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter.SelectCommand.CommandText = "KingsSalesSummary";
                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@Store_No", StoreCodeInUse1);
                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@Date", posDate);
                SqlDataAdapter.Fill(dt);
            }
            return dt;
        }

        public DataTable ByPaymenttype(DateTime? posDdate)
        {
            DataTable dt = new DataTable();
            using (SqlDataAdapter SqlDataAdapter = new SqlDataAdapter())
            {
                SqlDataAdapter.SelectCommand = new SqlCommand();
                SqlDataAdapter.SelectCommand.Connection = _connectionProvider.GetConnection();
                SqlDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter.SelectCommand.CommandText = "ByPaymenttype";
                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@Store_No", StoreCodeInUse1);
                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@Date", posDdate);
                SqlDataAdapter.Fill(dt);
            }
            return dt;
        }
        public bool AddUpdateOpeningDrawer(List<DrawerDetails> lst, string store_no, string remark)
        {
            DataTable dataTable = new DataTable();
            using (SqlDataAdapter SqlDataAdapter = new SqlDataAdapter())
            {
                SqlDataAdapter.SelectCommand = new SqlCommand();
                SqlDataAdapter.SelectCommand.Connection = _connectionProvider.GetConnection();
                SqlDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter.SelectCommand.CommandText = "OldZReport";
                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@store_no", store_no);
                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@date", DateTime.Now);

                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@Cash100", DecimalCheckForDBNull(lst.Find(i => i.Sent == "100s").SentVal));
                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@Cash50", DecimalCheckForDBNull(lst.Find(i => i.Sent == "50s").SentVal));
                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@Cash20", DecimalCheckForDBNull(lst.Find(i => i.Sent == "20s").SentVal));
                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@Cash10", DecimalCheckForDBNull(lst.Find(i => i.Sent == "10s").SentVal));
                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@Cash5", DecimalCheckForDBNull(lst.Find(i => i.Sent == "5s").SentVal));
                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@Cash1", DecimalCheckForDBNull(lst.Find(i => i.Sent == "1s").SentVal));
                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@Credit", DecimalCheckForDBNull(lst.Find(i => i.Sent == "HCredit").SentVal));
                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@Checks", DecimalCheckForDBNull(lst.Find(i => i.Sent == "HCheck").SentVal));
                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@Financed", DecimalCheckForDBNull(lst.Find(i => i.Sent == "HFinanced").SentVal));
                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@Others", DecimalCheckForDBNull(lst.Find(i => i.Sent == "HOthers").SentVal));
                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@ExPaidOut", DecimalCheckForDBNull(lst.Find(i => i.Sent == "ExpencePaidOut").SentVal));
                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@CashDrawer", DecimalCheckForDBNull(lst.Find(i => i.Sent == "CashDrawer").SentVal));
                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@Remark", remark);
                SqlDataAdapter.SelectCommand.Connection.Open();
                bool bResult = SqlDataAdapter.SelectCommand.ExecuteNonQuery() > 0;
                SqlDataAdapter.SelectCommand.Connection.Close();
                return bResult;
            }
        }

        public DataTable OpeningDrawerCash(string store_no, DateTime? posDdate)
        {
            return GetSqlData(@"select top 1 cast(amount as decimal(15,2)) amount from OpeningDrawerCashAmount where cast(date as date)<= cast(dateadd(day, -1, cast(@date as date)) as date) and store_no=@store_no order by [date] desc", "@store_no", store_no, "@date", Convert.ToString(posDdate));
        }

        public DataTable SalesBySalesType(int RunFor, DateTime? posDate)
        {
            DataTable dt = new DataTable();
            using (SqlDataAdapter SqlDataAdapter = new SqlDataAdapter())
            {
                SqlDataAdapter.SelectCommand = new SqlCommand();
                SqlDataAdapter.SelectCommand.Connection = _connectionProvider.GetConnection();
                SqlDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter.SelectCommand.CommandText = RunFor == 1 ? "SalesPaymentTransactions" : "SalesBySalesType";
                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@Store_No", StoreCodeInUse1);
                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@Date", posDate);
                SqlDataAdapter.Fill(dt);
            }
            return dt;
        }

        public DataTable GetOpeningDrawer(DateTime? posDate, String store_no)
        {
            return GetSqlData(@"select * from OpeningDrawerCashAmount where cast(date as date)=cast(@posdate as date) and store_no = @store", "@posdate", Convert.ToString(posDate), "@store", store_no);
        }

        public DataTable GetSaleDetails(string StoreNo, DateTime? posdate)
        {
            return GetSqlData(@";with cte(gr_total,sales_tax,snh,taxable,isreturn)
								as (
								select gr_total, sales_tax, snh, taxable, cast(iif(isnull(gr_total,0)<0,1,0) as bit) isreturn
								from invoice where cast(date as date) between cast(@date as date) and cast(@date as date) and store_no = @store_no
								),
								cte2(gr_total, sales_tax, snh, taxable,isreturn) as (
								select (sum(ri.qty * ri.price) + max(r.sales_tax) - max(r.deduction))gr_total,max(r.sales_tax) sales_tax,0 snh,cast(1 as bit) taxable, cast(0 as bit) isreturn
								from repair r
								join rep_item ri on trim(ri.repair_no) = trim(r.repair_no)
								where cast(r.date as date) between cast(@date as date) and cast(@date as date) and r.store = @store_no
								and ri.is_tax = 1 and r.repair_no in(select distinct repair_no from rep_item where qty > shiped) and trim(r.repair_no) not in(select distinct trim(repair_no) from in_items where repair_no <> '')
								group by r.repair_no),
								cte3(gr_total,sales_tax,snh,taxable,isreturn)as(
								select (sum(ri.qty * ri.price)+max(r.sales_tax)-max(r.deduction))gr_total,max(r.sales_tax) sales_tax,0 snh,cast(0 as bit) taxable,cast(0 as bit) isreturn
								from repair r
								join rep_item ri on trim(ri.repair_no)=trim(r.repair_no)	
								where cast(r.date as date) between cast(@date as date) and cast(@date as date) and r.store=@store_no 
								and ri.is_tax=0 and r.repair_no in(select distinct repair_no from rep_item where qty> shiped) and trim(r.repair_no) not in(select distinct trim(repair_no) from in_items where repair_no<>'')
								group by r.repair_no)

								select sum(gr_total) gr_total,sum(sales_tax) sales_tax,sum(snh)snh,cast(taxable as bit) taxable,isreturn from(select * from cte union all select * from cte2 union all select * from cte3) a
								group by taxable,isreturn", "@store_no", StoreNo, "@date", Convert.ToString(posdate));
        }

        public DataSet ZReportingLabel(DateTime? posDdate)
        {
            DataSet ds = new DataSet();
            using (SqlDataAdapter SqlDataAdapter = new SqlDataAdapter())
            {
                SqlDataAdapter.SelectCommand = new SqlCommand();
                SqlDataAdapter.SelectCommand.Connection = _connectionProvider.GetConnection();
                SqlDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter.SelectCommand.CommandText = "ZReporting_Lable_Details";
                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@Store_No", StoreCodeInUse1);
                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@Date", posDdate);
                SqlDataAdapter.Fill(ds);
            }
            return ds;
        }

        public DataTable GetSroreCreditsHistory(string CreditNo, bool IsGiftCert)
        {
            DataTable dataTable = new DataTable();

            using (SqlDataAdapter SqlDataAdapter = new SqlDataAdapter())
            {
                // Create the command and set its properties
                SqlDataAdapter.SelectCommand = new SqlCommand();
                SqlDataAdapter.SelectCommand.Connection = _connectionProvider.GetConnection();
                SqlDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;

                // Assign the SQL to the command object
                SqlDataAdapter.SelectCommand.CommandText = @"GetSroreCreditsHistory";

                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@CreditNo", CreditNo.Trim());
                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@IsGiftCert", IsGiftCert ? 1 : 0);

                // Fill the table from adapter
                SqlDataAdapter.Fill(dataTable);
            }
            return dataTable;
        }

        public List<SelectListItem> GetAllPotentialCustomerCodes()
        {
            DataTable dataTable = GetSqlData("select acc,name from MAILING order by acc");
            List<SelectListItem> salesmanList = new List<SelectListItem>();
            if (DataTableOK(dataTable))
                foreach (DataRow dr in dataTable.Rows)
                    salesmanList.Add(new SelectListItem() { Text = dr["acc"].ToString().Trim(), Value = dr["name"].ToString().Trim() });
            return salesmanList;
        }

        public DataTable GetStateCityByZip(string zip)
        {
            return GetSqlData(@"Select city,state from zipcodes with(nolock) where zipcode=@ZIP", "@ZIP", zip);
        }


        public List<SelectListItem> GetReasons()
        {
            DataTable dataTable = GetSqlData($"select '' as reason union select reason from crdt_reason order by reason");
            List<SelectListItem> salesmanList = new List<SelectListItem>();
            if (DataTableOK(dataTable))
                foreach (DataRow dr in dataTable.Rows)
                    salesmanList.Add(new SelectListItem() { Text = dr["reason"].ToString().Trim(), Value = dr["reason"].ToString().Trim() });
            return salesmanList;
        }
        public string Left(string value, int maxLength)
        {
            if (string.IsNullOrWhiteSpace(value))
                return "";
            maxLength = Math.Abs(maxLength);

            return (value.Length <= maxLength
                   ? value
                   : value.Substring(0, maxLength)
                   );
        }
        public DataRow CheckStyle(string style)
        {
            DataRow results = null;
            style = style.Trim();
            if (style.Contains("_")) // To make it faster
                style = style.Substring(0, style.IndexOf("_", StringComparison.Ordinal));
            if (is_StyleItem)
            {
                results = GetSqlRow("select style from StkNos where stk_no = @style", "@style", style);
                if (DataTableOK(results))
                {
                    string stylFromStkNos = Convert.ToString(results["style"]);
                    results = GetSqlRow("select * from styles where style = @style and no_stkno <> 1", "@style", stylFromStkNos);
                }
            }
            if (!DataTableOK(results))
            {
                string nfld = ByFieldValue5 ? "fieldvalue5" : (ByFieldValue6 ? "fieldvalue6" : "oldbarcode");
                string qry = "select * from styles where style = @style or barcode = @style or ";
                results = GetSqlRow(qry + nfld + " = @style", "@style", style);
            }
            if (is_Mahin && results == null && style.Length > 5)
                results = GetSqlRow(@"select * from styles where style = @style", "@style",
                    Left(style, 3) + "-" + style.Substring(3));     // barcode read 123456789 we want 123-456789
            return results;
        }
        public DataTable GetUpsIns1Details()
        {
            return GetSqlData("SELECT * FROM UPS_INS1");
        }

        public DataTable GetVendors(string likeVendor = "")
        {
            if (likeVendor.Trim().Length == 0)
                return GetSqlData("SELECT '' as ACC UNION SELECT ACC FROM VENDORS");
            return GetSqlData("SELECT TOP 20 ACC FROM VENDORS WHERE ACC LIKE @vendor ORDER BY ACC", "@vendor", likeVendor + "%");
        }
        public DataTable GetpartshistByJobBag(string JobBag)
        {
            return GetSqlData("EXEC [GetpartshistByJobBag] @JobBag", "@JobBag", JobBag);
        }

        public DataTable AddNewPotentialCustNote(string acc, string user, string leveldet)
        {
            DataTable dataTable = new DataTable();

            using (SqlDataAdapter SqlDataAdapter = new SqlDataAdapter())
            {
                SqlDataAdapter.SelectCommand = new SqlCommand();
                SqlDataAdapter.SelectCommand.Connection = _connectionProvider.GetConnection();
                SqlDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter.SelectCommand.CommandText = "ADDPOTENTIALCUSTNOTES";
                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@ACC", acc);
                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@LOGGEDUSER", user);
                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@TBLPOTENTIALCUSTNOTES", leveldet);
                SqlDataAdapter.Fill(dataTable);
            }
            return dataTable;
        }
        public class UnicodeStringWriter : StringWriter
        {
            public override Encoding Encoding
            {
                get { return Encoding.Unicode; }
            }
        }

        public string GetDataTableXML(string tablename, DataTable dt)
        {
            foreach (DataColumn dc in dt.Columns)
            {
                if (dc.DataType == typeof(DateTime))
                    dc.DateTimeMode = DataSetDateTime.Unspecified;
            }
            dt.AcceptChanges();
            dt.TableName = tablename;

            using (TextWriter writer = new UnicodeStringWriter())
            {
                using (XmlWriter xmlWriter = XmlWriter.Create(writer))
                {
                    xmlWriter.WriteStartDocument();
                    dt.WriteXml(xmlWriter);
                }
                return writer.ToString().Replace(" xml:space=\"preserve\"", "");
            }
        }

        public DataTable GetSaleFollow(string saleman1, string store, DateTime? Fdate, DateTime? Tdate, bool Allstore, bool AllDate, bool followList = false)
        {
            DataTable dataTable = new DataTable();

            using (SqlDataAdapter dataAdapter = new SqlDataAdapter())
            {
                dataAdapter.SelectCommand = new SqlCommand();
                dataAdapter.SelectCommand.Connection = _connectionProvider.GetConnection();
                dataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                dataAdapter.SelectCommand.CommandText = followList ? "SalesFollowUp" : "SalesFollowsUp";
                dataAdapter.SelectCommand.Parameters.AddWithValue("@salemancode", saleman1);
                dataAdapter.SelectCommand.Parameters.AddWithValue("@store", store);
                dataAdapter.SelectCommand.Parameters.AddWithValue("@Fdate1", Fdate);
                dataAdapter.SelectCommand.Parameters.AddWithValue("@Tdate2", Tdate);
                dataAdapter.SelectCommand.Parameters.AddWithValue("@allstore", Allstore);
                dataAdapter.SelectCommand.Parameters.AddWithValue("@Alldate", AllDate);
                dataAdapter.Fill(dataTable);
                return dataTable.Rows.Count > 0 ? dataTable : null;
            }
        }
        public DataTable GetStoreBankDetails(string code)
        {
            return GetSqlData(@"select distinct CODE, deposit_bank, scrap_bank  from STORES  where code ='" + code.Trim() + "'");
        }
        public DataTable GetAllPaymentsTypes()
        {
            return GetSqlData("select * from PAYMENTTYPES");
        }

        public DataRow getUpsInsInfo()
        {
            return GetSqlRow("select * from UPS_INS");
        }

        public DataTable GetKeepRecDetails(String invno)
        {
            return GetSqlData(@"select cast([date] as date) [DATE],[TIME],WHO,WHAT,FILENAME,DEL_INV,ACC from keep_rec  where inv_no =@invno and [type]='I' order by [date] ", "@invno", invno);
        }


        public Hashtable GetPaymentMethods(bool is_return = false, bool is_repair = false, string option = "")
        {

            DataTable paymentTypesTable;

            paymentTypesTable = GetPaymentTypes(option == "10111");
            Hashtable ht = new Hashtable();

            for (int j = 1, i = paymentTypesTable.Rows.Count - 1; i >= 0; j++, i--)
            {
                if (!(bool)paymentTypesTable.Rows[i]["HIDE"])
                {
                    try
                    {
                        if (((string)paymentTypesTable.Rows[i]["PAYMENTTYPE"]).ToUpper() == "GIFT CARD")
                        {
                            if (!is_return)
                                ht.Add(j, (string)paymentTypesTable.Rows[i]["PAYMENTTYPE"]);
                        }
                        else if (((string)paymentTypesTable.Rows[i]["PAYMENTTYPE"]).ToUpper() == "LAYAWAY")
                        {
                            if (!is_return && !is_repair && option != "10110")
                                ht.Add(j, (string)paymentTypesTable.Rows[i]["PAYMENTTYPE"]);
                        }
                        else if (((string)paymentTypesTable.Rows[i]["PAYMENTTYPE"]).ToUpper() == "ON ACCOUNT (PAY LATER)")
                            ht.Add(j, (string)paymentTypesTable.Rows[i]["PAYMENTTYPE"]);
                        else if (((string)paymentTypesTable.Rows[i]["PAYMENTTYPE"]).ToUpper() == "CC SWIPE")
                        {
                            if (CheckModuleEnabled(Modules.CC_Swipe))
                                ht.Add(j, (string)paymentTypesTable.Rows[i]["PAYMENTTYPE"]);
                        }
                        else if (((string)paymentTypesTable.Rows[i]["PAYMENTTYPE"]).ToUpper() == "LOYALTY POINTS")
                        {
                            if (is_Loyalty)
                                ht.Add(j, (string)paymentTypesTable.Rows[i]["PAYMENTTYPE"]);
                        }
                        else
                            ht.Add(j, (string)paymentTypesTable.Rows[i]["PAYMENTTYPE"]);
                    }
                    catch (System.ArgumentException)
                    {
                        continue;
                    }
                }
            }
            return ht;
        }
        public DataTable GetPaymentTypes(bool iSLayaway = false)
        {
            return GetSqlData($@";WITH CTE_PAYMENT_METHOD(PAYMENTTYPE,FIXED,REQUIRES_DEPOSIT,BANK_FEE,TRANS_FEE, HIDE,ORDERED, LAYAWAYS,ADD_COG)
                                      AS
                                      (
                                      SELECT UPPER(PAYMENTTYPE)PAYMENTTYPE,MAX(CASE WHEN FIXED=1 THEN 1 ELSE 0 END) FIXED,
                                      MAX(CASE WHEN REQUIRES_DEPOSIT=1 THEN 1 ELSE 0 END) REQUIRES_DEPOSIT,MAX(BANK_FEE) BANK_FEE, MAX(TRANS_FEE) TRANS_FEE,  MAX(CASE WHEN HIDE=1 THEN 1 ELSE 0 END) HIDE, MAX(ORDERED) ORDERED,MAX(CASE WHEN LAYAWAYS=1 THEN 1 ELSE 0 END) LAYAWAYS ,max(CASE WHEN add_cog=1 THEN 1 ELSE 0 END) ADD_COG
                                      FROM PAYMENTTYPES GROUP BY PAYMENTTYPE HAVING ISNULL(PAYMENTTYPE,'') <> ''
                                      )	
                                      SELECT PAYMENTTYPE,CAST(FIXED AS BIT) FIXED,CAST(REQUIRES_DEPOSIT AS BIT) REQUIRES_DEPOSIT,CAST(BANK_FEE AS DECIMAL(8,4)) BANK_FEE, CAST(TRANS_FEE AS DECIMAL(8,4)) TRANS_FEE, CAST(HIDE AS BIT) HIDE,ORDERED, CAST(LAYAWAYS AS BIT) LAYAWAY,CAST(add_cog AS BIT) ADD_COG
                                      FROM CTE_PAYMENT_METHOD  WHERE HIDE=0 AND LAYAWAYS={(iSLayaway ? "1" : "LAYAWAYS")} ORDER BY ORDERED");
        }

        public DataTable GetRepNoteByParent(string ParentSku)
        {
            return GetSqlData("SELECT NOTE,HAS_BRANCH,PRICE,RUSH_PRICE,COST,SKU,PARENT_SKU,SKU AS OLD_SKU,HAS_BRANCH AS OLD_HAS_BRANCH, cast(iS_Fixed as bit) iS_Fixed FROM Repair_notes WHERE parent_sku=@ParentSku ORDER BY note ASC", "@ParentSku", ParentSku);
        }

        public DataRow GetRepNoteRecordBySKU(string sku)
        {
            DataTable dataTable = GetSqlData(@"SELECT * FROM REPAIR_NOTES WHERE SKU=@SKU", "@SKU", sku);
            return GetRowOne(dataTable);
        }
        public DataTable GetRepNoteSkus()
        {
            return GetSqlData("select SKU, NOTE from REPAIR_NOTES WHERE SKU<>''");
        }
        public string repair_desc(string repairCode)
        {
            DataTable dataTable = new DataTable();
            using (SqlDataAdapter dataAdapter = new SqlDataAdapter())
            {
                dataAdapter.SelectCommand = new SqlCommand();
                dataAdapter.SelectCommand.Connection = _connectionProvider.GetConnection();
                dataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                dataAdapter.SelectCommand.CommandText = "GetRepairDescription";
                dataAdapter.SelectCommand.Parameters.AddWithValue("@RepCode", repairCode);
                dataAdapter.Fill(dataTable);
                return GetValue(dataTable, "RepairDescription");
            }
        }
        public DataTable GetWarrentyDetails(string inv, string style, string warranty, string acc)
        {
            return GetSqlData(@"SELECT CW.ACC,CC.NAME,cw.Next_Check_Dt as DATE,CW.Inv_No As INVOICE,CW.Style, '' as Warranty ,'' as Inspenct,Purchase_Dt,'' as Expiration_Date  From Cust_Warranty Cw JOin CUSTOMER CC ON CC.ACC = Cw.Acc where trim(CW.Inv_No) =trim('" + inv.Trim() + "') And trim(CW.Style) =trim('" + style.Trim() + "')  and trim(Cw.Acc)=trim('" + acc.Trim() + "') and trim(Cw.Warranty)=trim('" + warranty.Trim() + "')");
        }

        public byte[] GetStoreImage(string store_code = "")
        {
            byte[] objContext = null;
            store_code = !string.IsNullOrEmpty(store_code) ? store_code : "";
            if (!string.IsNullOrEmpty(store_code))
            {
                using (SqlConnection con = _connectionProvider.GetConnection())
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = con;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "GetImage";
                        cmd.Parameters.AddWithValue("@stylename", EscapeSpecialCharacters(store_code));
                        SqlDataReader rdr = cmd.ExecuteReader();
                        while (rdr.Read())
                            objContext = (byte[])rdr[0];
                        rdr.Close();
                    }
                    con.Close();
                }
            }
            if (objContext == null)
            {
                using (SqlConnection con = _connectionProvider.GetConnection())
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = con;
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = "select top 1 company_logo from ups_ins";
                        SqlDataReader rdr = cmd.ExecuteReader();
                        while (rdr.Read())
                            objContext = (byte[])rdr[0];
                        rdr.Close();
                    }
                    con.Close();
                }
            }
            return objContext;
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
                    dtCloned.AsEnumerable().OfType<DataRow>().Where(r => ((r.Field<string>(fld)) != null) && (r.Field<string>(fld)) != string.Empty).ToList().ForEach(r => r[fld] = CheckForDBNull(Convert.ToDateTime(CheckForDBNull(r[fld])), "System.DateTime").ToString(GetSeverDateFormat(true)));
                    dtCloned.AcceptChanges();
                }

                return dtCloned;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string FormatTel(string Telno)
        {
            string TelNo2 = Regex.Replace(Telno, "[^0-9]", "");
            long tel_no;
            Int64.TryParse(TelNo2, out tel_no);
            if (tel_no != 0)
                TelNo2 = String.Format("{0:(###)###-####}", Convert.ToInt64(tel_no));
            else
                TelNo2 = string.Empty;
            return TelNo2;
        }

        public DataTable GetStoresDataForSetDefault(bool activeOnly = false, bool allStores = false, bool withShop = false, bool noText = false)
        {
            try
            {
                using (SqlCommand dbCommand = new SqlCommand())
                {
                    dbCommand.Connection = _connectionProvider.GetConnection(); 
                    dbCommand.CommandType = CommandType.Text;
                    dbCommand.CommandTimeout = 0;

                    if (noText)
                    {
                        if (!string.IsNullOrWhiteSpace(FixedStoreCode) && !allStores)
                        {
                            dbCommand.CommandText = "SELECT DISTINCT CODE FROM [stores] WHERE code = @code";
                            dbCommand.Parameters.AddWithValue("@code", FixedStoreCode);
                        }
                        else
                            dbCommand.CommandText = "SELECT '' as CODE UNION SELECT DISTINCT CODE FROM [stores] WHERE notext = 0 ORDER BY CODE";
                    }
                    else if (!withShop)
                    {
                        if (!string.IsNullOrWhiteSpace(FixedStoreCode) && !allStores)
                        {
                            dbCommand.CommandText = "SELECT DISTINCT CODE FROM [stores] WHERE code = @code";
                            dbCommand.Parameters.AddWithValue("@code", FixedStoreCode);
                        }
                        else if (activeOnly)
                            dbCommand.CommandText = "SELECT DISTINCT CODE FROM [stores] WHERE code != '' AND code IS NOT NULL ORDER BY CODE ASC";
                        else
                            dbCommand.CommandText = "SELECT '' as CODE UNION SELECT DISTINCT CODE FROM [stores] ORDER BY CODE";
                    }
                    else
                    {
                        if (!string.IsNullOrWhiteSpace(FixedStoreCode) && !allStores)
                        {
                            dbCommand.CommandText = "SELECT 'SHOP' as CODE UNION SELECT DISTINCT CODE FROM [stores] WHERE code = @code";
                            dbCommand.Parameters.AddWithValue("@code", FixedStoreCode);
                        }
                        else if (activeOnly)
                            dbCommand.CommandText = "SELECT 'SHOP' as CODE UNION SELECT DISTINCT CODE FROM [stores] WHERE code != '' AND code IS NOT NULL";
                        else
                            dbCommand.CommandText = "SELECT '' as CODE UNION SELECT 'SHOP' as CODE UNION SELECT DISTINCT CODE FROM [stores]";
                    }

                    DataTable storesData = new DataTable();
                    using (SqlDataAdapter adapter = new SqlDataAdapter(dbCommand))
                    {
                        dbCommand.Connection.Open();
                        adapter.Fill(storesData);
                        dbCommand.Connection.Close();
                    }

                    return storesData;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while fetching stores data: " + ex.Message);
            }
        }

        public DataTable GetSalesmen()
        {
            return GetSqlData("select '' as Code from salesmen union select code from salesmen where iSNULL(inactive,0)=0 order by code asc");
        }

        public DataTable GetAllSetters()
        {
            return GetSqlData("select name,freq_used from setters where inactive=0 order by name asc");
        }

        public DataTable GetDistinctRecords(string TableData, string RecordType = "", bool allcust = false, bool checkok = true)
        {
            DataTable dataTable = new DataTable();
            using (SqlDataAdapter SqlDataAdapter = new SqlDataAdapter())
            {
                SqlDataAdapter.SelectCommand = new SqlCommand();
                SqlDataAdapter.SelectCommand.Connection = _connectionProvider.GetConnection();
                SqlDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter.SelectCommand.CommandText = "GetDistinctRecords";

                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@TABLEDATA", TableData);
                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@RECTYPE", RecordType);
                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@ALLCUST", allcust);
                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@CHECKOKFORCUSTOMER", checkok);
                SqlDataAdapter.SelectCommand.Connection.Open();

                SqlDataAdapter.Fill(dataTable);
            }
            return dataTable;
        }

        public DataRow GetCustNamebyCode(string ACC)
        {
            return GetSqlRow("SELECT TOP 1 *  FROM Customer WHERE LTRIM(RTRIM(acc))=LTRIM(RTRIM(@acc))", "@acc", ACC);
        }

        public bool OkToText()
        {
            if (Can_Text)
            { return (true); }

            return false;

        }

        public dynamic CheckForDBNullUPS(DataTable dtDefault, string columnname, string typename = "System.String")
        {
            switch (typename)
            {
                case "System.String":
                    return dtDefault.Columns.Contains(columnname) && dtDefault.Rows[0][columnname] != DBNull.Value && dtDefault.Rows[0][columnname] != null && !string.IsNullOrWhiteSpace(Convert.ToString(dtDefault.Rows[0][columnname])) ? dtDefault.Rows[0][columnname].ToString() : string.Empty;
                case "System.Int32":
                    return dtDefault.Columns.Contains(columnname) && dtDefault.Rows[0][columnname] != DBNull.Value && !string.IsNullOrWhiteSpace(dtDefault.Rows[0][columnname].ToString()) ? Convert.ToInt32(dtDefault.Rows[0][columnname]) : 0;
                case "System.Decimal":
                    return dtDefault.Columns.Contains(columnname) && dtDefault.Rows[0][columnname] != DBNull.Value && !string.IsNullOrWhiteSpace(dtDefault.Rows[0][columnname].ToString()) ? Convert.ToDecimal(dtDefault.Rows[0][columnname]) : 0;
                case "System.Boolean":
                    return dtDefault.Columns.Contains(columnname) && dtDefault.Rows[0][columnname] != DBNull.Value && !string.IsNullOrWhiteSpace(dtDefault.Rows[0][columnname].ToString()) ? Convert.ToBoolean(Convert.ToInt16(dtDefault.Rows[0][columnname])) : false;
                case "System.DateTime":
                    return dtDefault.Columns.Contains(columnname) && dtDefault.Rows[0][columnname] != DBNull.Value && !string.IsNullOrWhiteSpace(dtDefault.Rows[0][columnname].ToString()) ? Convert.ToDateTime(dtDefault.Rows[0][columnname]) : DateTime.Now;
                default:
                    return dtDefault.Columns.Contains(columnname) && dtDefault.Rows[0][columnname] != DBNull.Value && !string.IsNullOrWhiteSpace(dtDefault.Rows[0][columnname].ToString()) ? dtDefault.Rows[0][columnname].ToString() : string.Empty;
            }
        }

        /*public string Decrypt(byte[] cipherText, byte[] Key, byte[] IV)
        {
            string plaintext = null;
            using (AesManaged aes = new AesManaged())
            {
                ICryptoTransform decryptor = aes.CreateDecryptor(Key, IV);
                using (MemoryStream ms = new MemoryStream(cipherText))
                {

                    using (CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                    {

                        using (StreamReader reader = new StreamReader(cs))
                            plaintext = reader.ReadToEnd();
                    }
                }
            }
            return plaintext;
        }*/

        public DataTable GetEmailSettings()
        {
            return GetSqlData("declare @email_signature nvarchar(400)=(select isnull(email_signature,'') from UPS_INS);select *,@email_signature as email_signature from email_settings");
        }

        public DataTable GetEmailSetupPerUser(string user)
        {
            return GetSqlData("select Email,[Password],SMTPPort,SMTPServer,UseSsl,displayname,oauth2,email_signature,SignatureIsJpg, SignatureJpg from PASSFILE where NAME = @user", "@user", user);
        }

        public string InvStyle(string p_style)
        {
            return p_style.Split('_')[0];
        }

        public decimal GetStyleStock(string strStyle, string store)
        {
            using (var connection = _connectionProvider.GetConnection())
            using (var command = new SqlCommand(@"
                SELECT ISNULL(STCK.IN_STOCK, 0) 
                FROM STOCK STCK
                INNER JOIN Styles STLS ON STCK.STYLE = STLS.STYLE
                WHERE STLS.STYLE = @STYLE AND STCK.STORE_NO = @STORE", connection))
            {
                command.CommandType = CommandType.Text;
                command.Parameters.AddWithValue("@STYLE", InvStyle(strStyle?.Trim()));
                command.Parameters.AddWithValue("@STORE", store?.Trim());
                connection.Open();
                var result = command.ExecuteScalar();
                return result != null ? Convert.ToDecimal(result) : 0;
            }
        }

        public String GetLastRepairUpdate(String repair)
        {
            String lastDate = String.Empty;
            DataTable dtLastUpdate = GetSqlData($"SELECT LastRepairUpdate FROM REPAIR WHERE 1=1 AND REPAIR_NO=@repair", "@repair", repair.Trim());
            return DataTableOK(dtLastUpdate) ? Convert.ToString(dtLastUpdate.Rows[0]["LastRepairUpdate"]) : lastDate;
        }

        public DataTable GetRepairItems(string ordnumber)
        {
            return GetSqlData(@"SELECT ITEM,iSNULL(STYLE,'') STYLE, STAT AS DESCRIPTION,VENDOR AS REFNO,BARCODE,SHIPED,NOTE,SIZE,QTY,PRICE,Disc_Per_Line,ISNULL(QTY*(PRICE * (100 - iSNULL(Disc_Per_Line,0)) / 100),0) AS G_TOT,0 as CRow, is_tax FROM REP_ITEM WHERE trim(REPAIR_NO) = trim(@REPAIR_NO)",
                "@REPAIR_NO", ordnumber);
        }

        public DataRow GetRepairItem(string ordnumber, bool isErepir = false)
        {
            ordnumber = "(" + string.Format("'{0}'", (ordnumber.Trim())).Replace(",", "','") + ")";

            if (isErepir)
            { return GetSqlRow(@"SELECT ITEM,REP_ITEM.SIZE,QTY, STAT AS DESCRIPTION,VENDOR AS REFNO,BARCODE,SHIPED,INV_NO,repair.repStatus FROM REP_ITEM left join repair on REP_ITEM.repair_no = repair.repair_no WHERE Repair.Inv_no in " + ordnumber); }
            return GetSqlRow(@"SELECT ITEM,REP_ITEM.SIZE,QTY, STAT AS DESCRIPTION,VENDOR AS REFNO,BARCODE,SHIPED,INV_NO,repair.repStatus FROM REP_ITEM left join repair on REP_ITEM.repair_no = repair.repair_no WHERE REP_ITEM.REPAIR_NO  in" + ordnumber);
        }

        public decimal StoreRateNotSame(String store)
        {
            DataTable dtSalesTaxRate = GetSqlData(@"select top 1 sales_tax from stores where code=@store", "@store", store);
            if (DataTableOK(dtSalesTaxRate))
                return DecimalCheckForDBNull(dtSalesTaxRate.Rows[0]["sales_tax"]);
            return 0;
        }

        public string GetSetter(String jobbag)
        {
            DataTable dtMFG = GetSqlData(@"SELECT top 1 SETTER from MFG where RIGHT('000000'+INV_NO,6) =  RIGHT('000000'+@jobbag,6) order by DATE desc", "@jobbag", jobbag);
            return DataTableOK(dtMFG) ? Convert.ToString(dtMFG.Rows[0]["SETTER"]) : "";
        }

        public string JobNormal(string JobBagNo)
        {
            int input;
            if (int.TryParse(JobBagNo, out input))
                return string.Format("{0}", JobBagNo.Trim().PadLeft(6, '0'));
            return string.Format("{0}", JobBagNo.Trim().PadLeft(7, '0'));
        }

        public DataTable checkJobBagIsAlreadySplittedOrNot(string jobbagno)
        {
            return GetSqlData("select * From lbl_bar Where ltrim(original) = ltrim(@bagno)", "@bagno", jobbagno);
        }

        public bool GetShipedFinRsv(string jobbagno, int qty = 0)
        {
            using (SqlDataAdapter dataAdapter = new SqlDataAdapter())
            {
                try
                {
                    // Create the command and set its properties
                    dataAdapter.SelectCommand = new SqlCommand();
                    dataAdapter.SelectCommand.Connection = _connectionProvider.GetConnection();
                    dataAdapter.SelectCommand.CommandType = CommandType.Text;

                    dataAdapter.SelectCommand.CommandText = "SELECT 1 FROM OR_ITEMS WHERE (SHIPED + FIN_RSV + @QTY)>QTY AND BARCODE =@JOBBAGNO";
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@JOBBAGNO", jobbagno);
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@QTY", qty);
                    //Connection Open
                    dataAdapter.SelectCommand.Connection.Open();
                    DataTable dataTable = new DataTable();
                    // Fill the datatable From adapter
                    dataAdapter.Fill(dataTable);
                    // Get the datarow from the table
                    return dataTable.Rows.Count > 0;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    ////Connection Close
                    dataAdapter.SelectCommand.Connection.Close();
                }
            }
        }

        public bool GetClosedJobBag(string jobbagno)
        {
            return GetSqlData("SELECT * FROM MFG WHERE  RIGHT('00000'+ CONVERT(VARCHAR,trim(INV_NO)),6) =RIGHT('00000'+ CONVERT(VARCHAR,trim(@JOBBAGNO)),6) AND (CLOSED_JOB = 1 or rcvd=1)",
                    "@JOBBAGNO", jobbagno).Rows.Count > 0;
        }

        public string ValidDate(string InDate)
        {
            DateTime Min_date, input_date;
            DateTime.TryParse("01/01/1900", out Min_date);
            if (!DateTime.TryParse(InDate, out input_date))
                input_date = Min_date;
            return (Min_date > input_date ? Min_date : input_date).ToShortDateString();
        }

        public bool iSRequiredSize(String Sku)
        {
            return GetSqlData($"select 1 from repair_notes where trim(SKU)=trim(@Sku) and Require_Size=1", "@Sku", Sku).Rows.Count > 0;
        }

        public DataTable GetDepositPayments(string inv_no, bool showlayaway = true, bool is_return = false)
        {
            DataTable dataTable = new DataTable();


            using (SqlDataAdapter dataAdapter = new SqlDataAdapter())
            {
                // Create the command and set its properties
                dataAdapter.SelectCommand = new SqlCommand();
                dataAdapter.SelectCommand.Connection = _connectionProvider.GetConnection();

                dataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                dataAdapter.SelectCommand.CommandText = "GetREPDepositDet";

                dataAdapter.SelectCommand.Parameters.AddWithValue("@inv_no", inv_no);
                dataAdapter.SelectCommand.Parameters.AddWithValue("@showlayaway", showlayaway);

                dataAdapter.Fill(dataTable);

                return dataTable;
            }
        }

        public String OldPaymentType(string PayNo)
        {
            if (String.IsNullOrWhiteSpace(PayNo))
                return "";

            DataTable dataTablePayment = GetSqlData($"select paymenttype from PAYMENTS where inv_no=@PayNo", "@PayNo", PayNo.PadLeft(6, ' '));
            return DataTableOK(dataTablePayment) ? dataTablePayment.Rows[0]["paymenttype"].ToString() : "";
        }

        public decimal DecimalCheckForNullDBNull(object objval)
        {
            return objval != DBNull.Value && objval != null && !String.IsNullOrWhiteSpace(Convert.ToString(objval)) ? Convert.ToDecimal(objval.ToString()) : 0;
        }

        public string GetRegisterNames()
        {
            var clientName = System.Environment.GetEnvironmentVariable("ClientName");

            string registername = "";
            registername = clientName != null ? clientName.ToString() : System.Environment.MachineName;
            if (String.IsNullOrEmpty(registername))
                registername = registerInUse;
            return registername;
        }

        public DataTable SalesNotax(string xml)
        {
            if (xml != "")
            {
                DataTable dataTable = new DataTable();

                using (SqlDataAdapter dataAdapter = new SqlDataAdapter())
                {
                    dataAdapter.SelectCommand = new SqlCommand();
                    dataAdapter.SelectCommand.Connection = _connectionProvider.GetConnection();
                    dataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;

                    dataAdapter.SelectCommand.CommandText = "SalesNoTaxDetails";
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@Salesnotax", xml);

                    dataAdapter.Fill(dataTable);

                    return dataTable.Rows.Count > 0 ? dataTable : null;
                }
            }
            else
                return GetSqlData("SELECT Reason FROM notax_reasons");
        }

        public bool CheckJobbagExists(string repNo)
        {
            DataTable dataTable = GetSqlData(@"select barcode from or_items where barcode=@cBar", "@cBar", repNo);
            if (dataTable.Rows.Count > 0)
            {
                object value = dataTable.Rows[0]["barcode"];
                return (value != DBNull.Value);
            }
            return false;
        }

        public bool iSTaxableSKU(String SKU)
        {
            if (String.IsNullOrWhiteSpace(SKU))
                return false;
            return DataTableOK(GetSqlData($"SELECT 1 FROM REPAIR_NOTES WHERE iSNULL(NoTax,0)=0 AND SKU=iSNULL(@SKU,'')", "@SKU", SKU));
        }

        public DataTable CheckValidOrderRepair(string Rpno)
        {
            Rpno = Rpno.Trim();
            return GetSqlData("select * from REPAIR where trim(repair_no) = @Rpno", "@Rpno", Rpno);
        }

        public string GetNextRepairNo(string repairno, string min = "", string max = "999999", bool iSSomeOnebeat = false)
        {
            using (var connection = _connectionProvider.GetConnection())
            using (var dbCommand = new SqlCommand("GetNextRepairNumber", connection))
            {
                dbCommand.CommandType = CommandType.StoredProcedure;

                dbCommand.Parameters.Add(new SqlParameter("@BARCODE", SqlDbType.NVarChar) { Value = repairno });
                dbCommand.Parameters.Add(new SqlParameter("@STORE_CODE", SqlDbType.NVarChar) { Value = StoreCodeInUse });
                dbCommand.Parameters.Add(new SqlParameter("@RepairNo", SqlDbType.NVarChar, 30) { Direction = ParameterDirection.Output });
                dbCommand.Parameters.Add(new SqlParameter("@minlimit", SqlDbType.NVarChar) { Value = min });
                dbCommand.Parameters.Add(new SqlParameter("@maxlimit", SqlDbType.NVarChar) { Value = max });
                dbCommand.Parameters.Add(new SqlParameter("@iSSomeOneBeat", SqlDbType.Bit) { Value = iSSomeOnebeat });
                connection.Open();
                dbCommand.ExecuteNonQuery();

                // Return the output value
                return dbCommand.Parameters["@RepairNo"].Value.ToString();
            }
        }

        public DataTable getCustomerData(string acc)
        {
            return GetSqlData("select * from customer where acc = @acc and  acc <> '' and acc is not null", "@acc", acc);
        }

        public bool iSDeleteScanDoc(String invno, String entiryType, String docType)
        {
            using (SqlDataAdapter SqlDataAdapter = new SqlDataAdapter())
            {
                SqlDataAdapter.SelectCommand = new SqlCommand();
                SqlDataAdapter.SelectCommand.Connection = _connectionProvider.GetConnection();
                SqlDataAdapter.SelectCommand.CommandText = "DELETE FROM SCANDOCS WHERE ENTITY=@ENTITY AND ENTITYTYPE=@ENTITYTYPE  AND DOC_TYPE=@DOC_TYPE";
                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@ENTITY", invno);
                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@ENTITYTYPE", entiryType);
                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@DOC_TYPE", docType);
                SqlDataAdapter.SelectCommand.Connection.Open();
                bool bResult = SqlDataAdapter.SelectCommand.ExecuteNonQuery() > 0;
                SqlDataAdapter.SelectCommand.Connection.Close();
                return true;
            }
        }

        public void AddSignature(byte[] fileData, string inv_no, string acutalName = "actual.jpg", string Entitytype = "CCSIGN", String doc_type = "")
        {
            DataTable dataTable = new DataTable();
            SqlConnection con = _connectionProvider.GetConnection();
            con.Open();

            try
            {
                using (SqlDataAdapter dataAdapter = new SqlDataAdapter())
                {
                    // Create the command and set its properties
                    dataAdapter.SelectCommand = new SqlCommand();
                    dataAdapter.SelectCommand.Connection = con;

                    dataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                    dataAdapter.SelectCommand.CommandText = "AddScanDoc";

                    string filename = string.Format("{0}{1}", DateTime.Now.ToString("yyyyMMddHHmmssfff"), Path.GetExtension(acutalName));

                    dataAdapter.SelectCommand.Parameters.Add("@entityid", SqlDbType.NVarChar).Value = inv_no;
                    dataAdapter.SelectCommand.Parameters.Add("@entitytype", SqlDbType.NVarChar).Value = Entitytype; //"CCSIGN"
                    dataAdapter.SelectCommand.Parameters.Add("@filename", SqlDbType.NVarChar).Value = filename;
                    dataAdapter.SelectCommand.Parameters.Add("@fData", SqlDbType.Image, fileData.Length).Value = fileData;
                    dataAdapter.SelectCommand.Parameters.Add("@doc_type", SqlDbType.NVarChar).Value = doc_type;

                    // Fill the datatable From adapter
                    dataAdapter.Fill(dataTable);

                }
                con.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Dispose();
            }
        }

        public bool UpdateRepairCustomerDetails(string repairno, CustomerModel customerModel)
        {
            using (SqlConnection conn = _connectionProvider.GetConnection())
            using (SqlCommand dbCommand = conn.CreateCommand())
            {
                dbCommand.CommandType = CommandType.Text;

                // Split repair numbers safely
                var parts = repairno.Split(',')
                                    .Select(r => r.Trim())
                                    .Where(r => !string.IsNullOrEmpty(r))
                                    .ToList();

                // Build parameter placeholders dynamically
                var paramNames = parts.Select((r, i) => $"@repair{i}").ToList();
                var inClause = string.Join(",", paramNames);

                // Build SQL with parameters
                dbCommand.CommandText = $@"
                    UPDATE repair
                    SET name = @name,
                        addr1 = @addr1,
                        addr2 = @addr2,
                        city = @city,
                        state = @state,
                        zip = @zip,
                        country = @country
                    WHERE LTRIM(RTRIM(repair_no)) IN ({inClause})";

                // Add parameters for customerModel
                dbCommand.Parameters.AddWithValue("@name", customerModel.NAME ?? (object)DBNull.Value);
                dbCommand.Parameters.AddWithValue("@addr1", customerModel.ADDR1 ?? (object)DBNull.Value);
                dbCommand.Parameters.AddWithValue("@addr2", customerModel.ADDR12 ?? (object)DBNull.Value);
                dbCommand.Parameters.AddWithValue("@city", customerModel.CITY1 ?? (object)DBNull.Value);
                dbCommand.Parameters.AddWithValue("@state", customerModel.STATE1 ?? (object)DBNull.Value);
                dbCommand.Parameters.AddWithValue("@zip", customerModel.ZIP1 ?? (object)DBNull.Value);
                dbCommand.Parameters.AddWithValue("@country", customerModel.COUNTRY ?? (object)DBNull.Value);

                // Add parameters for repair numbers
                for (int i = 0; i < parts.Count; i++)
                {
                    dbCommand.Parameters.AddWithValue(paramNames[i], parts[i]);
                }

                conn.Open();
                int rowsAffected = dbCommand.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }

        public string InsertOrderRepairdataInRepairItemsTable(RepairorderModel repairOrder)
        {
            using (var connection = _connectionProvider.GetConnection())
            using (var command = new SqlCommand("AddUpdateRepairOrder", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@OPTYPE", "NEW");
                command.Parameters.AddWithValue("@Flg", "RpTms");
                command.Parameters.AddWithValue("@person", repairOrder?.persons ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@operator", repairOrder?.OPERATOR ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@StockStyle", repairOrder?.StockStyle ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@iSRepairStock", repairOrder?.iSRepairStock ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@StoreNo", repairOrder?.STORE ?? (object)DBNull.Value);
                command.Parameters.Add("@RepairsItems", SqlDbType.Xml).Value = repairOrder?.STRDATAXML ?? (object)DBNull.Value;
                connection.Open();
                var rowsAffected = command.ExecuteNonQuery();
                return rowsAffected > 0 ? "1" : "0";
            }
        }

        public string InsertDataIntoOrderItemTable(RepairorderModel repairorder)
        {
            using (SqlCommand dbCommand = new SqlCommand())
            {
                dbCommand.Connection = _connectionProvider.GetConnection();

                dbCommand.CommandType = CommandType.StoredProcedure;
                dbCommand.CommandText = "AddUpdateRepairOrder";

                dbCommand.Parameters.AddWithValue("@OPTYPE", "NEW");
                dbCommand.Parameters.AddWithValue("@Flg", "Or_tms");
                dbCommand.Parameters.AddWithValue("@operator", repairorder.OPERATOR);
                dbCommand.Parameters.AddWithValue("@person", repairorder.persons);
                SqlParameter parameter = new SqlParameter();
                parameter.ParameterName = "@RepairsItems";
                parameter.SqlDbType = System.Data.SqlDbType.Xml;
                parameter.Value = repairorder.STRDATAXML;
                dbCommand.Parameters.Add(parameter);

                dbCommand.Connection.Open();
                var rowsAffected = dbCommand.ExecuteNonQuery();
                dbCommand.Connection.Close();
                return (rowsAffected > 0 ? "1" : "0");
            }
        }

        public string AddOrderRepairToRepairTable(RepairorderModel repairorder)
        {
            //string barcode = this.GetNextReceiptBarcodeNo(repairorder);

            using (SqlCommand dbCommand = new SqlCommand())
            {
                dbCommand.Connection = _connectionProvider.GetConnection();
                dbCommand.CommandType = CommandType.Text;
                dbCommand.CommandText = @"INSERT INTO REPAIR(REPAIR_NO,ACC,CUS_REP_NO,CUS_DEB_NO,DATE,RCV_DATE,MESSAGE,MESSAGE1,OPERATOR,NAME,ADDR1,ADDR2,CITY,STATE ,ZIP,CAN_DATE,COUNTRY,SNH,VIA_UPS,IS_COD,COD_TYPE,EARLY,ISSUE_CRDT,ShipType,RESIDENT,ESTIMATE,TAXABLE,SALES_TAX,salesman1,salesman2,REGISTER,Jeweler_note,deduction,store,NO_TAXRESION,Warranty_Repair,COMISH1,COMISH2,COMISHAMOUNT1,COMISHAMOUNT2,Sales_Fee_Amount,Sales_Fee_Rate,repStatus,Who,rep_size,rep_metal,StockStyle,SETTER,EMAIL,warranty_inv_no,style) 
                                          VALUES 
                                         (CAST(@REPAIR_NO AS NVARCHAR(7)),CAST(@ACC AS NVARCHAR(12)),CAST(@CUS_REP_NO AS NVARCHAR(12)),CAST(@CUS_DEB_NO AS NVARCHAR(10)),@DATE,@RCV_DATE,CAST(@MESSAGE AS NVARCHAR(150)),CAST(@MESSAGE1 AS NVARCHAR(150)),CASt(@OPERATOR AS NVARCHAR(10)),CAST(@NAME AS NVARCHAR(60)),CASt(@ADDR1 AS NVARCHAR(300)),CAST(@ADDR2 AS NVARCHAR(300)),CAST(@CITY AS NVARCHAR(30)),CAST(@STATE AS NVARCHAR(10)),CAST(@ZIP AS NVARCHAR(10)),@CAN_DATE,CAST(@COUNTRY AS NVARCHAR(15)),CAST(@SNH AS DECIMAL(9,2)), CAST(@VIA_UPS AS NVARCHAR(1)), CAST(@IS_COD AS BIT),CAST(@COD_TYPE AS NVARCHAR(1)),CAST(@EARLY AS NVARCHAR(1)),CAST(@ISSUE_CRDT AS BIT),CAST(@ShipType AS INT), CAST(@RESIDENT AS NVARCHAR(1)),CAST(@ESTIMATE AS DECIMAL(12,2)), CAST(@TAXABLE AS BIT), CAST(@SALES_TAX AS DECIMAL(12,2)), CAST(@SALESMAN1 AS NVARCHAR(4)), CAST(@SALESMAN2 AS NVARCHAR(4)),CAST(@REGISTER AS NVARCHAR(20)),CAST(@Jeweler_note AS NVARCHAR(300)),CAST(@deduction AS DECIMAL(10,2)), CAST(@store AS NVARCHAR(50)),CAST(@NO_TAXRESION AS NVARCHAR(500)),CAST(@Warranty_Repair AS BIT),CASt(@COMISH1 AS DECIMAL(5,2)),CAST(@COMISH2 AS DECIMAL(5,2)),CASt(@COMISHAMOUNT1 AS DECIMAL(12,2)),CAST(@COMISHAMOUNT2 AS DECIMAL(12,2)),CAST(@SalesFeeAmount AS DECIMAL(12,2)),CASt(@SalesFeeRate AS DECIMAL(12,2)),CASt(@repStatus AS NVARCHAR(1)),CASt(@Who AS NVARCHAR(60)),CAST(@repSize AS NVARCHAR(10)) ,CAST(@repMetal AS NVARCHAR(10)),isnull(@StockStyle,''),iSNULL(@Setter,''),iSNULL(@Email,''),iSNULL(@warranty_inv_no,''),iSNULL(@warranty_style,''))";
                dbCommand.Parameters.AddWithValue("@REPAIR_NO", repairorder.REPAIR_NO);
                dbCommand.Parameters.AddWithValue("@ACC", repairorder.ACC);
                dbCommand.Parameters.AddWithValue("@CUS_REP_NO", repairorder.CUS_REP_NO);
                dbCommand.Parameters.AddWithValue("@CUS_DEB_NO", repairorder.CUS_DEB_NO);
                dbCommand.Parameters.AddWithValue("@DATE", repairorder.DATE);
                dbCommand.Parameters.AddWithValue("@RCV_DATE", repairorder.RCV_DATE == null ? DBNull.Value : (object)repairorder.RCV_DATE);
                dbCommand.Parameters.AddWithValue("@MESSAGE", repairorder.MESSAGE);
                dbCommand.Parameters.AddWithValue("@MESSAGE1", repairorder.MESSAGE1);
                dbCommand.Parameters.AddWithValue("@OPEN", 0);
                dbCommand.Parameters.AddWithValue("@OPERATOR", repairorder.OPERATOR);
                dbCommand.Parameters.AddWithValue("@NAME", repairorder.NAME);
                dbCommand.Parameters.AddWithValue("@ADDR1", repairorder.ADDR1);
                dbCommand.Parameters.AddWithValue("@ADDR2", repairorder.ADDR2);
                dbCommand.Parameters.AddWithValue("@CITY", repairorder.CITY);
                dbCommand.Parameters.AddWithValue("@STATE", repairorder.STATE);
                dbCommand.Parameters.AddWithValue("@ZIP", repairorder.ZIP);
                dbCommand.Parameters.AddWithValue("@CAN_DATE", repairorder.CAN_DATE);
                dbCommand.Parameters.AddWithValue("@COUNTRY", repairorder.COUNTRY);
                dbCommand.Parameters.AddWithValue("@SNH", 0);
                dbCommand.Parameters.AddWithValue("@VIA_UPS", repairorder.SHIPED);
                dbCommand.Parameters.AddWithValue("@IS_COD", repairorder.IS_COD);
                dbCommand.Parameters.AddWithValue("@COD_TYPE", repairorder.COD_TYPE);
                dbCommand.Parameters.AddWithValue("@EARLY", repairorder.EARLY);
                dbCommand.Parameters.AddWithValue("@ISSUE_CRDT", repairorder.ISSUE_CRDT);
                dbCommand.Parameters.AddWithValue("@ShipType", 0);
                dbCommand.Parameters.AddWithValue("@RESIDENT", 0);
                dbCommand.Parameters.AddWithValue("@ESTIMATE", repairorder.ESTIMATE);
                dbCommand.Parameters.AddWithValue("@TAXABLE", repairorder.TAXABLE);
                dbCommand.Parameters.AddWithValue("@SALES_TAX", repairorder.SALES_TAX);
                dbCommand.Parameters.AddWithValue("@SALESMAN1", repairorder.SALESMAN1);
                dbCommand.Parameters.AddWithValue("@SALESMAN2", repairorder.SALESMAN2);
                dbCommand.Parameters.AddWithValue("@REGISTER", repairorder.CASH_REGISTER);
                dbCommand.Parameters.AddWithValue("@Jeweler_note", repairorder.Jeweler_Note);
                dbCommand.Parameters.AddWithValue("@deduction", repairorder.Deduction);
                dbCommand.Parameters.AddWithValue("@store", repairorder.STORE);
                dbCommand.Parameters.AddWithValue("@NO_TAXRESION", repairorder.TaxReason);
                dbCommand.Parameters.AddWithValue("@Warranty_Repair", repairorder.iSFromWarranty);
                dbCommand.Parameters.AddWithValue("@COMISH1", repairorder.COMISH1);
                dbCommand.Parameters.AddWithValue("@COMISH2", repairorder.COMISH2);
                dbCommand.Parameters.AddWithValue("@COMISHAMOUNT1", repairorder.COMISHAMOUNT1);
                dbCommand.Parameters.AddWithValue("@COMISHAMOUNT2", repairorder.COMISHAMOUNT2);
                dbCommand.Parameters.AddWithValue("@SalesFeeAmount", repairorder.SalesFeeAmount);
                dbCommand.Parameters.AddWithValue("@SalesFeeRate", repairorder.SalesFeeRate);
                dbCommand.Parameters.AddWithValue("@repStatus", repairorder.repStatus);
                dbCommand.Parameters.AddWithValue("@Who", repairorder.Who);
                dbCommand.Parameters.AddWithValue("@repSize", repairorder.RepSize);
                dbCommand.Parameters.AddWithValue("@repMetal", repairorder.RepMetal);
                dbCommand.Parameters.AddWithValue("@StockStyle", repairorder.StockStyle);
                dbCommand.Parameters.AddWithValue("@Setter", repairorder.persons);
                dbCommand.Parameters.AddWithValue("@Email", repairorder.email);
                dbCommand.Parameters.AddWithValue("@warranty_inv_no", repairorder.warranty_inv_no);
                dbCommand.Parameters.AddWithValue("@warranty_style", repairorder.warranty_style);

                dbCommand.Connection.Open();
                var rowsAffected = dbCommand.ExecuteNonQuery();
                dbCommand.Connection.Close();
                return (rowsAffected > 0 ? "1" : "0");
            }
        }

        public bool UpdateCustomerTel(string CustAcc, decimal CustTel = 0)
        {
            using (SqlCommand dbCommand = new SqlCommand())
            {
                dbCommand.Connection = _connectionProvider.GetConnection();
                dbCommand.CommandType = CommandType.Text;
                dbCommand.CommandText = @"UPDATE CUSTOMER SET TEL=@CustTel WHERE ACC=@CustAcc";
                dbCommand.Parameters.AddWithValue("@CustAcc", CustAcc);
                dbCommand.Parameters.AddWithValue("@CustTel", CustTel);
                dbCommand.Connection.Open();
                var rowsAffected = dbCommand.ExecuteNonQuery();
                dbCommand.Connection.Close();

                return rowsAffected > 0;
            }
        }

        public bool PaymentForRepair(string invno, string acc, string pcname, string grtotal, string paymentItems, string UserGCNo, string StoreCode, string Cash_Register, out string out_inv_no, bool ispayment = false, bool is_update = false, bool is_return = false, string storecodeinuse = "", String xmlDiscount = "")
        {
            if (!is_return)
            {
                using (SqlCommand dbCommand1 = new SqlCommand())
                {
                    dbCommand1.Connection = _connectionProvider.GetConnection();
                    dbCommand1.CommandType = CommandType.StoredProcedure;
                    dbCommand1.CommandText = "UPDATE_INVOICE";
                    dbCommand1.Parameters.AddWithValue("@INV_NO", invno);
                    dbCommand1.Parameters.AddWithValue("@From_Repair", true);
                    dbCommand1.CommandTimeout = 0;
                    dbCommand1.Connection.Open();
                    var rowAffected = dbCommand1.ExecuteNonQuery();
                    dbCommand1.Connection.Close();
                }
            }
            using (SqlCommand dbCommand = new SqlCommand())
            {
                // Set the command object properties
                dbCommand.Connection = _connectionProvider.GetConnection();
                dbCommand.CommandType = CommandType.StoredProcedure;
                dbCommand.CommandText = "PaymentForRepair";
                dbCommand.CommandTimeout = 0;
                Object invoicedate;
                invoicedate = DateTime.Now;

                dbCommand.Parameters.AddWithValue("@INV_NO", invno);
                dbCommand.Parameters.AddWithValue("@ACC", acc);
                dbCommand.Parameters.AddWithValue("@PCNAME", pcname);
                dbCommand.Parameters.AddWithValue("@DATE", invoicedate);
                dbCommand.Parameters.AddWithValue("@GR_TOTAL", grtotal);
                dbCommand.Parameters.AddWithValue("@ISPAYMENT", ispayment == true ? 1 : 0);
                dbCommand.Parameters.AddWithValue("@IS_RETURN", is_return == true ? 1 : 0);
                dbCommand.Parameters.AddWithValue("@IS_UPDATE", is_update == true ? 1 : 0);
                dbCommand.Parameters.Add("@OUT_INV_NO", SqlDbType.VarChar, 6).Direction = ParameterDirection.Output;
                dbCommand.Parameters.AddWithValue("@UserGCNO", UserGCNo);
                dbCommand.Parameters.AddWithValue("@StoreCode", StoreCode);
                dbCommand.Parameters.AddWithValue("@CASH_REG_CODE", Cash_Register);
                dbCommand.Parameters.AddWithValue("@storecodeinuse", storecodeinuse);

                SqlParameter parameter = new SqlParameter();
                parameter.ParameterName = "@TBLPAYMENTITEMS";
                parameter.SqlDbType = System.Data.SqlDbType.Xml;
                parameter.Value = paymentItems;
                dbCommand.Parameters.Add(parameter);

                parameter = new SqlParameter();
                parameter.ParameterName = "@TBLDISCOUNTITEMS";
                parameter.SqlDbType = System.Data.SqlDbType.Xml;
                parameter.Value = xmlDiscount;
                dbCommand.Parameters.Add(parameter);

                // Open the connection, execute the query and close the connection
                dbCommand.Connection.Open();
                var rowsAffected = dbCommand.ExecuteNonQuery();
                out_inv_no = Convert.ToString(dbCommand.Parameters["@OUT_INV_NO"].Value);

                dbCommand.Connection.Close();

                return rowsAffected > 0;
            }
        }

        public void DELUSEDPARTS(string JobBagNo, string LoggedUser, string Store_No)
        {
            GetStoreProc("Delete_Parts_Hist", "@JobBagNo", JobBagNo, "@LoggedUser", LoggedUser, "@Store_no", Store_No);
        }

        public void AddKeepRec(string description, byte[] stream = null, bool isInvoiceDeleted = false, string userName = "",
            string acc = "", string type = "", string inv_no = "")
        {
            using (var connection = _connectionProvider.GetConnection())
            using (var command = new SqlCommand("AddKeepRec", connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@CWHAT", description ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@LOGGEDUSER", string.IsNullOrWhiteSpace(userName) ? LoggedUser : userName);
                command.Parameters.AddWithValue("@isInvoiceDeleted", isInvoiceDeleted);

                command.Parameters.Add("@FILENAME", SqlDbType.NVarChar).Value = $"{DateTime.Now.Ticks}.pdf";
                command.Parameters.Add("@FDATA", SqlDbType.Image).Value = (object)stream ?? DBNull.Value;

                command.Parameters.AddWithValue("@ACC", acc ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@type", type ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@inv_no", inv_no ?? (object)DBNull.Value);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public DataTable GetInvoiceMasterDetailPO(string invno, bool isMemo, bool isBriony = false, bool isVatInclude = false, bool isOpenMemo = false)
        {
            var dataTable = new DataTable();
            using (var connection = _connectionProvider.GetConnection())
            using (var command = new SqlCommand(isBriony ? "GetMasterDetailInvoicePO_Briony" : "GetMasterDetailInvoicePO", connection))
            using (var adapter = new SqlDataAdapter(command))
            {
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@inv_no", invno ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@is_memo", isMemo ? 1 : 0);

                if (!isBriony)
                    command.Parameters.AddWithValue("@iSVatInclude", isVatInclude ? 1 : 0);

                command.Parameters.AddWithValue("@IsOpenMemo", isOpenMemo ? 1 : 0);

                adapter.Fill(dataTable);
            }
            return dataTable;
        }

        public bool GetBillShipDiff(string acc)
        {
            using (var connection = _connectionProvider.GetConnection())
            using (var command = new SqlCommand(
                @"SELECT ISNULL(different_ship, 0) FROM Customer with (nolock) WHERE acc = TRIM(@acc)", connection))
            {
                command.Parameters.AddWithValue("@acc", acc ?? (object)DBNull.Value);

                connection.Open();
                var result = command.ExecuteScalar();
                return result != null && Convert.ToBoolean(result);
            }
        }

        public string GetAddressLabelShip(string acc, string seperator)
        {
            // DataTable dataTable = new DataTable();
            //DataRow dataRow = GetSqlRow("select *  From Customer Where rtrim(acc)=rtrim(@acc)", "@acc", acc);
            DataRow dataRow = GetSqlRowForInvoice("select *  From Customer Where rtrim(acc)=rtrim(@acc)", "@acc", acc);
            if (dataRow != null)
            {
                string TelNo = FormatTel(CheckForDBNull(dataRow["tel2"]));
                return RemoveFromEnd(string.Format("{0}{10}{1}{10}{2}{10}{3}{10}{4} {5} {6} {7} {8}{10}{9}",
                    dataRow["name2"].ToString().Trim(),
                    dataRow["addr2"].ToString().Trim(),
                    dataRow["addr22"].ToString().Trim(),
                    dataRow["addr23"].ToString().Trim(),
                    dataRow["city2"].ToString().Trim(),
                    dataRow["state2"].ToString().Trim(),
                    dataRow["zip2"].ToString().Trim(),
                    dataRow["country2"].ToString().Trim(),
                    dataRow.Table.Columns.Contains("buyer") ? !string.IsNullOrWhiteSpace(dataRow["buyer"].ToString().Trim()) ? "Attn:" + dataRow["buyer"].ToString().Trim() : string.Empty : string.Empty,
                    TelNo != string.Empty ? "Tel:" + TelNo : string.Empty,
                    seperator).Replace(", , ", ", ").Replace("\n\n", "\n").Replace("\n\n", "\n"), seperator);
            }
            return "";
        }

        public DataTable GetInvoicePayments(string inv_no, bool noLayaway = true)
        {
            DataTable dataTable = new DataTable();
            bool showlayaway = noLayaway;
            bool is_return = false;
            string query = string.Empty;

            using (SqlDataAdapter dataAdapter = new SqlDataAdapter())
            {
                // Create the command and set its properties
                dataAdapter.SelectCommand = new SqlCommand();
                dataAdapter.SelectCommand.Connection = _connectionProvider.GetConnection();
                dataAdapter.SelectCommand.CommandType = CommandType.Text;
                if (is_return)
                {
                    query = string.Format(@"select DATE,METHOD,AMOUNT,NOTE,CURR_TYPE,CURR_RATE,CURR_AMOUNT FROM (select 1 as id, DATE,  case when ISNULL(PAYMENTTYPE,'')='Store Credit' then ISNULL(PAYMENTTYPE,'')+' (Credit# : '+B.PAYREFNO+')'   when  ISNULL(PAYMENTTYPE,'')='Gift Card' then ISNULL(PAYMENTTYPE,'')+' (GC# : '+NOTE+')'  else ISNULL(PAYMENTTYPE,'') end as METHOD,AMOUNT * {0} as AMOUNT,cast(NOTE as nvarchar(100)) as NOTE,CURR_TYPE,CURR_RATE,CURR_AMOUNT from PAY_ITEM a inner join payments b  on a.PAY_NO =  b.INV_NO where b.RTV_PAY = 'P' and a.RTV_PAY = 'I'  and try_CAST(a.inv_no as int) = try_CAST(@inv_no as INT) ", is_return ? -1 : 1);
                    if (showlayaway)
                        query += @" UNION select 2 as id, date,'Layaway',gr_total - CREDITS,cast('' as nvarchar(100)),'',0,0 from invoice where cast(inv_no as int) = cast(@inv_no as int) and LAYAWAY = 1 ) c order by date asc";
                }
                else
                {
                    query = string.Format(@"select DATE,METHOD,AMOUNT,NOTE,CURR_TYPE,CURR_RATE,CURR_AMOUNT FROM (select 1 as id, DATE,  case when ISNULL(PAYMENTTYPE,'')='Store Credit' then ISNULL(PAYMENTTYPE,'')+' (Credit# : '+B.PAYREFNO+')'   when  ISNULL(PAYMENTTYPE,'')='Gift Card' then ISNULL(PAYMENTTYPE,'')+' (GC# : '+NOTE+')'  else ISNULL(PAYMENTTYPE,'') end as METHOD,AMOUNT * {0} as AMOUNT,cast(NOTE as nvarchar(100)) as NOTE,CURR_TYPE,CURR_RATE,CURR_AMOUNT from PAY_ITEM a inner join payments b  on a.PAY_NO =  b.INV_NO where b.RTV_PAY = 'P' and a.RTV_PAY = 'I'  and try_cast(a.inv_no as int) = try_cast(@inv_no as int) ", is_return ? -1 : 1);

                    if (showlayaway)
                    {
                        query += @" UNION SELECT 2 as id, date,'Layaway', gr_total - CREDITS, cast('' as nvarchar(100)),'',0,0 from invoice where inv_no = @inv_no and LAYAWAY = 1 ";
                        query += @" UNION SELECT 3 as id, date,'On Account (Pay Later)', gr_total - CREDITS, cast('' as nvarchar(100)),'',0,0 from invoice where inv_no = @inv_no and PAYLATER = 1 and (gr_total - credits)<>0";
                        query += @" UNION SELECT 4 as id, A.DATE,   
						CASE 
						WHEN ISNULL(A.PAYMENTTYPE, '')= 'Store Credit' THEN ISNULL(A.PAYMENTTYPE, '')+' (Credit# : ' + B.PAYREFNO + ')'
						WHEN ISNULL(A.PAYMENTTYPE, '')= 'Gift Card' then ISNULL(A.PAYMENTTYPE, '')+' (GC# : ' +NOTE+')'
						ELSE ISNULL(A.PAYMENTTYPE, '') END AS METHOD, 
						A.PAID AS AMOUNT, B.NOTE AS NOTE,'', 0, 0 FROM PAID_RPR A 
						JOIN PAY_ITEM P ON P.PAY_NO=A.PAY_NO and P.RTV_PAY='D'
						JOIN PAYMENTS B ON B.INV_NO = P.PAY_NO
						WHERE A.REPINV_NO=' ' AND A.REPAIR_NO IN(SELECT trim(value) FROM STRING_SPLIT((SELECT ISNULL(PON,'') FROM INVOICE WHERE try_cast(inv_no as int)= try_cast(@inv_no as int) and v_ctl_no='REPAIR'), ',')
						WHERE trim(value) <> '')) c ";

                        String repairOrderNo = GetRepairOrderNoFromInvoice(inv_no);
                        is_return = iSRepairReturn(inv_no);
                        if (!String.IsNullOrWhiteSpace(repairOrderNo))
                        {
                            String[] actRepNo = repairOrderNo.Split(',');
                            foreach (String actRep in actRepNo)
                            {
                                if (!String.IsNullOrWhiteSpace(query))
                                    query += " UNION ";
                                query += $@" SELECT A.DATE,case 
								when ISNULL(A.PAYMENTTYPE,'')= 'Store Credit' then ISNULL(A.PAYMENTTYPE,'')+' (Credit# : ' + B.PAYREFNO + ')'
								when ISNULL(A.PAYMENTTYPE,'')= 'Gift Card' then ISNULL(A.PAYMENTTYPE,'')+' (GC# : ' +NOTE+')'
								else ISNULL(A.PAYMENTTYPE, '') end as METHOD, 
								A.PAID AS AMOUNT, B.NOTE ,'', 0, 0
								FROM PAID_RPR A 
								JOIN PAY_ITEM P ON P.PAY_NO=A.PAY_NO and P.RTV_PAY='D'
								JOIN PAYMENTS B ON B.INV_NO = P.PAY_NO
								WHERE ltrim(rtrim(A.REPAIR_NO)) = '{actRep.Trim()}' AND A.REPINV_NO = ' ' and 1={(is_return ? 0 : 1)}";
                            }
                        }
                        query += " ORDER BY date ASC";
                    }
                    else
                    {
                        query += ") c ORDER BY date";
                    }
                }
                dataAdapter.SelectCommand.CommandText = query;
                // Add the parameter to the parameter collection
                dataAdapter.SelectCommand.Parameters.AddWithValue("@inv_no", inv_no);
                // Fill the datatable From adapter
                dataAdapter.Fill(dataTable);
                return dataTable;
            }
        }

        public string RemoveFromEnd(string str, string toRemove)
        {
            if (str.EndsWith(toRemove))
                return Left(str, str.Length - toRemove.Length);
            return str;
        }

        public DataRow GetSqlRowForInvoice(string CommandText, string param_name1 = "", string param_value1 = "", string param_name2 = "", string param_value2 = "", string param_name3 = "", string param_value3 = "")
        {
            DataTable dataTable = new DataTable();
            DataRow dataRow;

            using (SqlDataAdapter dataAdapter = new SqlDataAdapter())
            {
                // Create the command and set its properties
                dataAdapter.SelectCommand = new SqlCommand();
                dataAdapter.SelectCommand.Connection = _connectionProvider.GetConnection();
                dataAdapter.SelectCommand.CommandType = CommandType.Text;
                dataAdapter.SelectCommand.CommandText = CommandText;
                if (!string.IsNullOrWhiteSpace(param_name1))
                    dataAdapter.SelectCommand.Parameters.AddWithValue(param_name1, param_value1);
                if (!string.IsNullOrWhiteSpace(param_name2))
                    dataAdapter.SelectCommand.Parameters.AddWithValue(param_name2, param_value2);
                if (!string.IsNullOrWhiteSpace(param_name1))
                    dataAdapter.SelectCommand.Parameters.AddWithValue(param_name3, param_value3);

                // Fill the datatable From adapter
                dataAdapter.Fill(dataTable);

                // Get the datarow from the table
                dataRow = GetRowOne(dataTable);

                return dataRow;
            }
        }

        public string GetNxtCheckNo(string bank)
        {
            Int32 nXtChek = 0;
            DataRow dataRow = GetSqlRow("select * from ( SELECT LAST_INV FROM bank_acc WHERE CODE='" + bank + "' union all select isnull(CHECK_NO,0) LAST_INV from CHECKS where bank='" + bank + "' ) a order by try_convert( int, LAST_INV ) desc");
            if (dataRow != null)
            {
                if (int.TryParse(CheckForDBNull(dataRow["LAST_INV"]), out nXtChek))
                    nXtChek++;
                else
                    nXtChek = 0;
            }
            return nXtChek.ToString();
        }

        public DataTable GetAllPaymentTypes()
        {
            return GetSqlData("SELECT UPPER(PAYMENTTYPE)PAYMENTTYPE,FIXED,REQUIRES_DEPOSIT,BANK_FEE,TRANS_FEE,ISNULL(HIDE,0) HIDE, ORDERED, CAST(ISNULL(LAYAWAYS,0) AS BIT) LAYAWAY,isnull(Add_Cog,'') as Add_COG,gl_acc FROM PAYMENTTYPES WHERE ISNULL(PAYMENTTYPE,'') <> '' ORDER BY ordered");
        }

        public DataTable GetMessages()
        {

            return GetSqlData("Select NAME,Message from messages");
        }
        public DataTable GetOccasiontype()
        {
            return GetSqlData("SELECT '' as occassion UNION SELECT occassion FROM occassion_types  ORDER BY occassion ASC");
        }

        public int GetAppraisalCount(string invoice)
        {
            return GetSqlData($"SELECT DISTINCT AppraisalId from appraisal  where trim(inv_no)=trim('{invoice}')").Rows.Count;
        }

        public DataTable GetInvoiceItemsForAppr(string inv_no)
        {
            return GetSqlData(@"Select Style,Price,[Desc],Acc from In_Items a inner join invoice b on a.inv_no = b.inv_no where a.inv_no = @inv_no",
                "@inv_no", inv_no);
        }

        public string Pad6(string what)
        {
            if (what.Trim().Length > 6)
                return what;
            return (what.Trim().PadLeft(6, ' '));
        }
        public DataTable GetAppraisal(string appriasalid, bool IsQuickAppr = false)
        {
            return GetSqlData(@" Select CONVERT(Bit, 'true') as [Select],Style,StyleDesc,Price,inv_no,iif(a.acc<>'',a.acc,c.acc) acc,AppraisalId,Cast(AppDate as Date) AppDate,StyleId,iif(a.AccName<>'',a.AccName,c.name)AccName,iif(a.Addr<>'',a.Addr,c.Addr1)Addr,iif(a.Addr1<>'',iif(a.Addr1=a.addr OR a.Addr1=c.addr1,'',a.addr1),iif(a.Addr=c.addr1 OR a.Addr1=c.addr1,'',c.addr1))Addr1,iif(a.City<>'',a.City,c.City1)City,iif(a.State<>'',a.State,c.State1)State,iif(a.Zip<>'',a.Zip,c.Zip1)Zip,c.EMAIL, c.tel, isnull(a.Store,'') Store, isnull(a.Salesman,'') Salesman from Appraisal a left join customer c on a.acc=c.ACC where ltrim(rtrim(AppraisalId)) = Ltrim(trim(@AppriasalID)) " + (IsQuickAppr ? " and  isquickappraisal=1" : "and  isquickappraisal=0") + "",
                "@AppriasalID", appriasalid);
        }
        public DataRow CheckValidCustomerCodeForAppraisal(string acc)
        {
            return GetSqlRow("SELECT TOP 1 *  FROM Customer WHERE LTRIM(RTRIM(acc))=LTRIM(RTRIM(@acc))", "@acc", acc);
        }

        public string GetAppraiserName()
        {
            try
            {
                using (SqlDataAdapter dbCommand = new SqlDataAdapter())
                {
                    // Create the command and set its properties
                    dbCommand.SelectCommand = new SqlCommand();
                    DataTable dt = new DataTable();
                    dbCommand.SelectCommand.Connection = _connectionProvider.GetConnection();
                    dbCommand.SelectCommand.CommandType = CommandType.Text;

                    dbCommand.SelectCommand.CommandText = @"select APPRAISER_NAME from ups_ins";
                    dbCommand.SelectCommand.Connection.Open();
                    dbCommand.Fill(dt);
                    return GetValue0(dt);
                }

            }
            catch (Exception)
            {
                return string.Empty;
            }
        }
        public string GetValue0(DataTable dt)
        {
            if (DataTableOK(dt))
                return dt.Rows[0][0].ToString();
            return "";
        }

        public bool UpdateAppraisal(string itemsxml, bool isnew, out string AppId, bool IsQuickAppr = false)
        {
            AppId = "";
            try
            {
                using (SqlCommand dbCommand = new SqlCommand())
                {
                    // Set the command object properties
                    dbCommand.Connection = _connectionProvider.GetConnection();
                    dbCommand.CommandType = CommandType.StoredProcedure;
                    dbCommand.CommandText = "AddUpdateAppraisal";
                    dbCommand.CommandTimeout = 0;

                    dbCommand.Parameters.AddWithValue("@isnew", isnew ? 1 : 0);

                    SqlParameter parameter = new SqlParameter();
                    parameter.ParameterName = "@tblAppItems";
                    parameter.SqlDbType = System.Data.SqlDbType.Xml;
                    parameter.Value = itemsxml;
                    dbCommand.Parameters.Add(parameter);

                    dbCommand.Parameters.Add("@out_appid", SqlDbType.VarChar, 6).Direction = ParameterDirection.Output;
                    dbCommand.Parameters.AddWithValue("@IsQuickAppr", IsQuickAppr ? 1 : 0);
                    // Open the connection, execute the query and close the connection
                    dbCommand.Connection.Open();
                    var rowsAffected = dbCommand.ExecuteNonQuery();
                    AppId = Convert.ToString(dbCommand.Parameters["@out_appid"].Value);
                    dbCommand.Connection.Close();

                    return rowsAffected > 0;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public string GetDetailsFromCostTab(string dStyle)
        {
            DataTable dataTable = new DataTable();

            dataTable = GetSqlData(@"SELECT (SELECT isnull(gold_wt,0) FROM styles WHERE Style = @dStyle) [Weight], isnull(qty,0) Qty, isnull(s.shape,'') Shape,
					isnull(s.[item_type],'') [Type], isnull(s.[ct_weight],0) CT_Weight, isnull(s.cut,'') Cut, isnull(s.color,'') CLR, isnull(s.quality,'') Clarity,
					Replace(isnull(s.size,''),'mm','') Size, isnull(s.Cert_Type,'') Cert_Type, isnull(s.Cert_No,'') Cert_No FROM styl_Raw sr 
					LEFT JOIN STYLES s ON s.Style = sr.Lot_No WHERE sr.Style = @dStyle and header_type='stone'", "@dStyle", dStyle);
            String detailsDesc = "";
            bool weightAdded = false;
            if (DataTableOK(dataTable))
            {
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {

                    if (!weightAdded)
                    {
                        decimal totGoldWeight = GetValueD(dataTable, "weight");
                        if (totGoldWeight > 0)
                        {
                            detailsDesc += Environment.NewLine + "Total weight : " + totGoldWeight + "gms" + Environment.NewLine;
                            weightAdded = true;
                        }
                        else
                            detailsDesc += Environment.NewLine;
                    }

                    if (DecimalCheckForDBNull(dataTable.Rows[i]["qty"]) > 0 && CheckForDBNull(dataTable.Rows[i]["Shape"]) == "" && CheckForDBNull(dataTable.Rows[i]["Type"]) == "")
                        detailsDesc += Environment.NewLine;
                    else if (DecimalCheckForDBNull(dataTable.Rows[i]["qty"]) > 0)
                        detailsDesc += " " + Environment.NewLine + NumberToText(Convert.ToInt64(DecimalCheckForDBNull(dataTable.Rows[i]["qty"])));
                    if (CheckForDBNull(dataTable.Rows[i]["Shape"]) != "")
                        detailsDesc += " " + CheckForDBNull(dataTable.Rows[i]["Shape"]) + (CheckForDBNull(dataTable.Rows[i]["Shape"]) != "" ? " Cut" : "");
                    if (CheckForDBNull(dataTable.Rows[i]["Type"]) != "")
                        detailsDesc += " " + CheckForDBNull(dataTable.Rows[i]["Type"]);
                    if (DecimalCheckForDBNull(dataTable.Rows[i]["CT_WEIGHT"]) > 0)
                        detailsDesc += " = " + DecimalCheckForDBNull(dataTable.Rows[i]["CT_WEIGHT"]).ToString("0.00") + " ct of";
                    if (CheckForDBNull(dataTable.Rows[i]["Cut"]) != "")
                        detailsDesc += " " + CheckForDBNull(dataTable.Rows[i]["Cut"]) + " Cut,";
                    if (CheckForDBNull(dataTable.Rows[i]["CLR"]) != "")
                        detailsDesc += " Color " + CheckForDBNull(dataTable.Rows[i]["CLR"]) + ",";
                    if (CheckForDBNull(dataTable.Rows[i]["Clarity"]) != "")
                        detailsDesc += " Clarity " + CheckForDBNull(dataTable.Rows[i]["Clarity"]) + ",";
                    if (CheckForDBNull(dataTable.Rows[i]["Size"]) != "")
                        detailsDesc += " measuring " + CheckForDBNull(dataTable.Rows[i]["Size"]) + " mm.";
                    if (CheckForDBNull(dataTable.Rows[i]["Cert_Type"]) != "")
                        detailsDesc += " " + CheckForDBNull(dataTable.Rows[i]["Cert_Type"]);
                    if (CheckForDBNull(dataTable.Rows[i]["Cert_No"]) != "")
                        detailsDesc += " Cert# : " + CheckForDBNull(dataTable.Rows[i]["Cert_No"]) + "";

                }
            }
            return detailsDesc;
        }

        public decimal GetValueD(DataTable dt, string what)
        {
            return DecimalCheckForDBNull(GetValue(dt, what));
        }

        public dynamic CheckForDBNull(object objval, object defaultvalue, string typename = "System.String")
        {
            switch (typename)
            {
                case "System.String":
                    return objval != DBNull.Value && objval != null ? objval.ToString() : (dynamic)defaultvalue;
                case "System.Int32":
                    return objval != DBNull.Value && objval != null ? Convert.ToInt32(objval) : (dynamic)defaultvalue;
                case "System.Decimal":
                    return objval != DBNull.Value && objval != null ? Convert.ToDecimal(objval) : (dynamic)defaultvalue;
                case "System.Boolean":
                    return objval != DBNull.Value && objval != null ? Convert.ToBoolean(objval) : (dynamic)defaultvalue;
                case "System.DateTime":
                    return objval != DBNull.Value && objval != null ? Convert.ToDateTime(objval) : (dynamic)defaultvalue;
                default:
                    return objval != DBNull.Value && objval != null ? objval.ToString() : (dynamic)defaultvalue;
            }
        }

        public string NumberToText(long number)
        {
            StringBuilder wordNumber = new StringBuilder();

            string[] powers = new string[] { "Thousand ", "Million ", "Billion " };
            string[] tens = new string[] { "Twenty", "Thirty", "Forty", "Fifty", "Sixty", "Seventy", "Eighty", "Ninety" };
            string[] ones = new string[] { "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine", "Ten",
                                       "Eleven", "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen", "Seventeen", "Eighteen", "Nineteen" };

            if (number == 0) { return "Zero"; }
            if (number < 0)
            {
                wordNumber.Append("Negative ");
                number = -number;
            }

            long[] groupedNumber = new long[] { 0, 0, 0, 0 };
            int groupIndex = 0;

            while (number > 0)
            {
                groupedNumber[groupIndex++] = number % 1000;
                number /= 1000;
            }

            for (int i = 3; i >= 0; i--)
            {
                long group = groupedNumber[i];

                if (group >= 100)
                {
                    wordNumber.Append(ones[group / 100 - 1] + " Hundred ");
                    group %= 100;

                    if (group == 0 && i > 0)
                        wordNumber.Append(powers[i - 1]);
                }

                if (group >= 20)
                {
                    if ((group % 10) != 0)
                        wordNumber.Append(tens[group / 10 - 2] + " " + ones[group % 10 - 1] + " ");
                    else
                        wordNumber.Append(tens[group / 10 - 2] + " ");
                }
                else if (group > 0)
                    wordNumber.Append(ones[group - 1] + " ");

                if (group != 0 && i > 0)
                    wordNumber.Append(powers[i - 1]);
            }

            return wordNumber.ToString().Trim();
        }

        public DataTable GetAppraisals(string sFilter)
        {
            return GetSqlData(string.Format("Select AppraisalId, acc, convert(nvarchar(10), AppDate, 101) as AppDate, Inv_no, Style, StyleDesc from Appraisal where {0} order by appdate desc", sFilter));
        }

        public DataTable GetAndDelAppraisalDetails(int appraisal, int Delete, int flag = 0)
        {
            DataTable dataTable = new DataTable();

            using (SqlDataAdapter SqlDataAdapter = new SqlDataAdapter())
            {
                // Create the command and set its properties
                SqlDataAdapter.SelectCommand = new SqlCommand();
                SqlDataAdapter.SelectCommand.Connection = _connectionProvider.GetConnection();
                SqlDataAdapter.SelectCommand.CommandType = CommandType.Text;
                if (flag == 1)
                {
                    SqlDataAdapter.SelectCommand.CommandText = @"Select AppraisalId,acc,AppDate,Inv_no,Style,StyleDesc from Appraisal where AppraisalId =@apl";

                    SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@apl", appraisal);
                }
                else if (flag == 2)
                {
                    SqlDataAdapter.SelectCommand.CommandText = @"DELETE FROM Appraisal WHERE AppraisalId = @apls";
                    SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@apls", Delete);
                }
                else
                    SqlDataAdapter.SelectCommand.CommandText = @"Select AppraisalId,acc,AppDate,Inv_no,Style,StyleDesc from Appraisal";

                SqlDataAdapter.Fill(dataTable);

                return dataTable;
            }
        }

        public DataTable GetUpsInsInformation()
        {
            return GetSqlData("select * from ups_ins");
        }

        public bool SaveAppraisalSignature(byte[] sigimage, out string error)
        {
            error = string.Empty;
            try
            {
                using (SqlCommand dbCommand = new SqlCommand())
                {
                    // Set the command object properties
                    dbCommand.Connection = _connectionProvider.GetConnection();
                    dbCommand.CommandType = CommandType.Text;
                    dbCommand.CommandTimeout = 3000;
                    dbCommand.CommandText = "update ups_ins set AppraisalSignature=@sigimage";
                    dbCommand.Parameters.Add("@sigimage", SqlDbType.Binary, -1);
                    if (sigimage == null)
                        dbCommand.Parameters["@sigimage"].Value = DBNull.Value;
                    else
                        dbCommand.Parameters["@sigimage"].Value = sigimage;

                    // Open the connection, execute the query and close the connection
                    dbCommand.Connection.Open();
                    var rowsAffected = dbCommand.ExecuteNonQuery();
                    dbCommand.Connection.Close();

                    return rowsAffected > 0;
                }
            }

            catch (Exception ex)
            {
                error = ex.Message;
                return false;
            }
        }

        public DataTable Getname()
        {
            return GetSqlData("select trim(isnull(acc,'')) as ACC,trim(isnull(NAME,'')) NAME  from gl_accs");
        }

        public List<SelectListItem> GetAllCenterStypes()
        {
            DataTable dataTable = GetSqlData("select distinct STYPE from CENTER_STYPES where STYPE != '' and STYPE is not null order by STYPE");
            List<SelectListItem> salesmanList = new List<SelectListItem>();
            salesmanList.Add(new SelectListItem() { Text = "", Value = "" });
            if (DataTableOK(dataTable))
                foreach (DataRow dr in dataTable.Rows)
                    salesmanList.Add(new SelectListItem() { Text = dr["STYPE"].ToString().Trim(), Value = dr["STYPE"].ToString().Trim() });
            return salesmanList;
        }

        public string NumberToCurrencyText(decimal number, MidpointRounding midpointRounding)
        {
            // Round the value just in case the decimal value is longer than two digits
            number = decimal.Round(number, 2, midpointRounding);

            // Divide the number into the whole and fractional part strings
            string[] arrNumber = number.ToString().Split('.');

            // Get the whole number text
            long wholePart = long.Parse(arrNumber[0]);
            string strWholePart = NumberToText(wholePart);

            // For amounts of zero dollars show 'No Dollars...' instead of 'Zero Dollars...'
            string wordNumber = (wholePart == 0 ? "No" : strWholePart) + (wholePart == 1 ? " Dollar " : " Dollars and ");

            // If the array has more than one element then there is a fractional part otherwise there isn't
            // just add 'No Cents' to the end
            if (arrNumber.Length > 1)
            {
                // If the length of the fractional element is only 1, add a 0 so that the text returned isn't,
                // 'One', 'Two', etc but 'Ten', 'Twenty', etc.
                long fractionPart = long.Parse((arrNumber[1].Length == 1 ? arrNumber[1] + "0" : arrNumber[1]));
                string strFarctionPart = NumberToText(fractionPart);

                wordNumber += (fractionPart == 0 ? " No" : strFarctionPart) + (fractionPart == 1 ? " Cent" : " Cents");
            }
            else
                wordNumber += "Even";

            return wordNumber;
        }

        public string Layawaynote()
        {
            DataTable dtLayawaynote = GetSqlData($"SELECT TOP 1 layawaynote FROM UPS_INS1");
            if (DataTableOK(dtLayawaynote))
                return Convert.ToString(dtLayawaynote.Rows[0]["layawaynote"]);
            return string.Empty;
        }

        public string GetAddressLabelFrom_InvoiceTbl(DataRow dataRow, DataRow CustRow, string seperator, bool isLandscape = false, string type = "I", bool isMemo = false)
        {
            if ((type == "CB" || type == "CS") && !DataTableOK(CustRow))
                return "";
            if ((type == "I") && !DataTableOK(dataRow))
                return "";

            DataTable bDt = getCustomerData(isMemo ? "" : dataRow["REGISTRY"].ToString());
            string brideName = GetValue(bDt, "NAME");
            string TelNo = DataTableOK(CustRow) ? CustRow["tel"].ToString() : "";
            var countDigits = TelNo.Count(x => Char.IsDigit(x));
            if (countDigits == 10)
            {
                TelNo = Regex.Replace(TelNo, "[^0-9]", "");
                long tel_no;
                Int64.TryParse(TelNo, out tel_no);
                if (tel_no == 0 && TelNo != "0")
                {
                    TelNo = string.Empty;
                }

            }
            string addr1 = type == "I" ? dataRow["addr1"].ToString().Trim() : CustRow[type == "CS" ? "addr2" : "addr1"].ToString().Trim();
            string addr2 = type == "I" ? dataRow["addr2"].ToString().Trim() : CustRow[type == "CS" ? "addr22" : "addr12"].ToString().Trim();
            string addr3 = type == "I" ? dataRow["addr3"].ToString().Trim() : CustRow[type == "CS" ? "addr23" : "addr13"].ToString().Trim();
            return RemoveFromEnd(string.Format("{0}{13}{1}{13}{2}{13}{3}{13}{4} {5} {6} {7}{13}{8}{13}{9}{13}{10}{13}{11}{13}{12}",
                type == "I" ? dataRow["name"].ToString().Trim() : CustRow[type == "CS" ? "name2" : "name"].ToString().Trim(),
                addr1.ToUpper().Contains("RESALE#") ? "" : addr1,
                addr2.ToUpper().Contains("RESALE#") ? "" : addr2,
                addr3.ToUpper().Contains("RESALE#") ? "" : addr3,
                type == "I" ? dataRow["city"].ToString().Trim() : CustRow[type == "CS" ? "city2" : "city1"].ToString().Trim(),
                type == "I" ? dataRow["state"].ToString().Trim() : CustRow[type == "CS" ? "state2" : "state1"].ToString().Trim(),
                type == "I" ? dataRow["zip"].ToString().Trim() : CustRow[type == "CS" ? "zip2" : "zip1"].ToString().Trim(),
                type == "I" ? dataRow["country"].ToString().Trim() : CustRow[type == "CS" ? "country2" : "country"].ToString().Trim(),
                $"{(addr1.ToUpper().Contains("RESALE#") ? addr1 + "\n" : "")}{(addr2.ToUpper().Contains("RESALE#") ? addr2 + "\n" : "")}{(addr3.ToUpper().Contains("RESALE#") ? addr3 + "\n" : "")}",
                 type == "I" ? dataRow.Table.Columns.Contains("buyer") ? !string.IsNullOrWhiteSpace(dataRow["buyer"].ToString().Trim()) ? "Attn:" + dataRow["buyer"].ToString().Trim() : string.Empty : string.Empty : CustRow.Table.Columns.Contains("buyer") ? !string.IsNullOrWhiteSpace(CustRow["buyer"].ToString().Trim()) ? "Attn:" + CustRow["buyer"].ToString().Trim() : string.Empty : string.Empty,
                TelNo != string.Empty && TelNo != "0" ? "Tel:" + TelNo : string.Empty,
                brideName != string.Empty && isLandscape ? "Bride's Name:" + brideName.Trim() : string.Empty,
                DataTableOK(CustRow) ? String.IsNullOrWhiteSpace(Convert.ToString(CustRow["email"])) ? "" : "Email:" + CustRow["email"].ToString().Trim() : "",
                seperator).Replace(", , ", ", ").Replace("\n\n", "\n").Replace("\n\n", "\n"), seperator);
        }

        public string GetStoreAddressByINovice_store(string storecode, string seperator, out string storename, bool no_email = false, string inv_no = "", DataTable dtStore = null)
        {
            DataTable storeInfo = DataTableOK(dtStore) ? dtStore : GetStores(storecode);
            long tel_no = 0;
            string TelNo = string.Empty;
            if (DataTableOK(storeInfo) && storeInfo != null)
            {
                DataRow dataRow = GetRowOne(storeInfo);
                storename = GetValue(storeInfo, "name");
                if (!String.IsNullOrWhiteSpace(inv_no) && iSCompany2Name(inv_no))
                    storename = CompanyName;

                TelNo = CheckForDBNull(dataRow["tel"]);
                if (no_email)
                    return string.Format("{0}{4}{1}{4}{2}{4}{3}", dataRow["addr1"].ToString().Trim(),
                        dataRow["addr2"].ToString().Trim(), dataRow["addr3"].ToString().Trim(),
                        dataRow["addr4"].ToString().Trim(), seperator).Replace("\n\n", "\n").
                        Replace(", , ", ", ").Replace("\n\n", "\n").Replace(", , ", ", ");

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
                if (dataRow != null)
                {
                    // string a = dataRow["addr1"].ToString();
                    string CompanyEmail = Convert.ToString(_httpContextAccessor.HttpContext?.Session.GetString("CompanyEmail"));
                    return string.Format("{0}{4}{1}{4}{2}{4}{3}{4}{5}{4}{6}", dataRow["addr1"].ToString().Trim(),
                   dataRow["addr2"].ToString().Trim(), dataRow["addr3"].ToString().Trim(),
                   dataRow["addr4"].ToString().Trim(), seperator, TelNo,
                   CompanyEmail).Replace("\n\n", "\n").
                   Replace(", , ", ", ").Replace("\n\n", "\n").Replace(", , ", ", ");
                    // return string.Empty;
                }
                else
                {
                    return string.Empty;
                }


            }
            storename = string.Empty;

            return string.Empty;
        }

        public bool iSCompany2Name(string invno)
        {
            DataTable dataTable = GetSqlData($"SELECT iSCompanyName2 FROM INVOICE WHERE trim(INV_NO)=@INVNO AND iSNULL(iSCompanyName2,0) = 1", "@INVNO", invno.Trim());
            return dataTable.Rows.Count > 0;
        }

        public DataSet OnLoadReportCombineSp(string inv_no, bool is_return, string RelatedInvoiceNo, bool noLayaway, string acc, bool isRepair, string storeno, bool isBriony, bool is_memo, bool iSVatInclude, bool IsOpenMemo)
        {
            DataSet ds = new DataSet();
            using (SqlDataAdapter SqlDataAdapter = new SqlDataAdapter())
            {
                SqlDataAdapter.SelectCommand = new SqlCommand();
                SqlDataAdapter.SelectCommand.Connection = _connectionProvider.GetConnection();
                SqlDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter.SelectCommand.CommandText = "load_report_combine_sp";
                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@Inv_no", inv_no);
                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@is_return", is_return);
                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@RelatedInvoiceNo", RelatedInvoiceNo);
                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@noLayaway", noLayaway);
                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@acc", acc);
                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@isRepair", isRepair);
                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@storecode", storeno);
                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@storecodeinuse", StoreCodeInUse);
                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@isBriony", isBriony);
                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@is_memo", is_memo);
                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@iSVatInclude", iSVatInclude);
                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@IsOpenMemo", IsOpenMemo);
                SqlDataAdapter.Fill(ds);
            }
            return ds;
        }

        public string GetAddressLabelShip(DataRow dataRow, string seperator, string LabelOn = "")
        {
            if (DataTableOK(dataRow))
            {
                string TelNo = FormatTel(LabelOn == "I" ? CheckForDBNull(dataRow["tel"]) : CheckForDBNull(dataRow["tel2"]));
                return RemoveFromEnd(string.Format("{0}{11}{1}{11}{2}{11}{3}{11}{4} {5} {6} {7}{11}{8}{11}{9}{11}{10}",
                    LabelOn == "I" ? dataRow["name"].ToString().Trim() : dataRow["name2"].ToString().Trim(),
                    LabelOn == "I" ? dataRow["addr1"].ToString().Trim().ToUpper().Contains("RESALE#") ? "" : dataRow["addr1"].ToString().Trim() : dataRow["addr2"].ToString().Trim(),
                    LabelOn == "I" ? dataRow["addr12"].ToString().Trim().ToUpper().Contains("RESALE#") ? "" : dataRow["addr12"].ToString().Trim() : dataRow["addr22"].ToString().Trim(),
                    LabelOn == "I" ? dataRow["addr13"].ToString().Trim().ToUpper().Contains("RESALE#") ? "" : dataRow["addr13"].ToString().Trim() : dataRow["addr23"].ToString().Trim(),
                    LabelOn == "I" ? dataRow["city1"].ToString().Trim() : dataRow["city2"].ToString().Trim(),
                    LabelOn == "I" ? dataRow["state1"].ToString().Trim() : dataRow["state2"].ToString().Trim(),
                    LabelOn == "I" ? dataRow["zip1"].ToString().Trim() : dataRow["zip2"].ToString().Trim(),
                    LabelOn == "I" ? dataRow["country"].ToString().Trim() : dataRow["country2"].ToString().Trim(),
                    LabelOn == "I" ? $"{(dataRow["addr1"].ToString().Trim().ToUpper().Contains("RESALE#") ? dataRow["addr1"].ToString().Trim() + "\n" : "")}{(dataRow["addr12"].ToString().Trim().ToUpper().Contains("RESALE#") ? dataRow["addr12"].ToString().Trim() + "\n" : "")}\n{(dataRow["addr13"].ToString().Trim().ToUpper().Contains("RESALE#") ? dataRow["addr13"].ToString().Trim() + "\n" : "")}" : "",
                    dataRow.Table.Columns.Contains("buyer") ? !string.IsNullOrWhiteSpace(dataRow["buyer"].ToString().Trim()) ? "Attn:" + dataRow["buyer"].ToString().Trim() : string.Empty : string.Empty,
                    TelNo != string.Empty ? "Tel:" + TelNo : string.Empty,
                    seperator).Replace(", , ", ", ").Replace("\n\n", "\n").Replace("\n\n", "\n"), seperator);
            }
            return "";
        }

        public DataTable GetCaseNo(List<string> styles)
        {
            var multi = GetSqlData(string.Format("SELECT style,case_no from styles where case_no <> 'Multi' and style in ('" + string.Join("','", styles) + "')" + "union select style, [case] as case_no from styles_case  where style in ('" + string.Join("','", styles) + "')"));
            var dtMulti = multi.AsEnumerable().GroupBy(a => a[0]).Where(gr => gr.Count() > 1).ToList();
            if (dtMulti.Any())
            {
                var dupStyles = dtMulti.Select(a => a.Key).ToList();
                foreach (var item in dupStyles)
                {
                    string caseNo = string.Empty;
                    string search = "style = '" + item + "'";
                    var del = multi.AsEnumerable().CopyToDataTable().Select(search).CopyToDataTable();
                    foreach (var row in del.AsEnumerable())
                    {
                        if (!string.IsNullOrWhiteSpace(row[1].ToString()))
                            caseNo += row[1].ToString() + ",";
                    }
                    int count = multi.Rows.Count;
                    for (int i = 0; i < multi.Rows.Count; i++)
                    {
                        DataRow dr = multi.Rows[i];
                        if (dr[0].ToString() == item.ToString())
                        {
                            dr.Delete();
                            multi.AcceptChanges();
                        }
                        count = multi.Rows.Count;
                    }
                    multi.Rows.Add(item, caseNo.TrimEnd(','));
                    multi.AcceptChanges();
                }
            }
            return multi;
        }

        public bool CheckCCVendor(string cVend)
        {
            return DataTableOK(GetSqlData("select * from vendors where ltrim(rtrim(acc))=ltrim(rtrim(@cVend)) and isnull(is_crd,0)=1",
                "@cVend", cVend));
        }

        public string CheckReport()
        {
            //var upsValues = HttpContext.Current.Application["GlobalSettings"] as UPS_INS;
            var micrStr = _httpContextAccessor.HttpContext?.Session.GetString("MICR");

            if (bool.TryParse(micrStr, out bool micr) && micr)
            {
                if (CheckModuleEnabled(Modules.Check_In_Middle))
                    return "rptCheckMicr_Middle.cshtml";
                if (CheckModuleEnabled(Modules.Check_At_bottom))
                    return "rptCheckMicr_Bottom.cshtml";
                return CheckModuleEnabled(Modules.PrintCheckLower) ? "rptCheckMicr_fsh.cshtml" : "rptCheckMicr.cshtml";
            }
            if (CheckModuleEnabled(Modules.Check_In_Middle))
                return "rptCheck_middle.cshtml";
            if (CheckModuleEnabled(Modules.Check_At_bottom))
                return "rptCheck_bottom.cshtml";
            return "rptCheck.cshtml";
        }

        public decimal GetCustomerCredit(string selCustomer)
        {
            using (var connection = _connectionProvider.GetConnection())
            using (var command = new SqlCommand(
                @"SELECT ISNULL(CREDIT, 0) 
                FROM CUSTOMER with (nolock)
                WHERE BILL_ACC = @BACC", connection))
            {
                command.CommandType = CommandType.Text;
                command.Parameters.AddWithValue("@BACC", selCustomer ?? (object)DBNull.Value);

                connection.Open();
                var result = command.ExecuteScalar();
                return result != null ? Convert.ToDecimal(result) : 0m;
            }
        }

        public DataTable GetHungtooInvoice(string invData)
        {
            var dataTable = new DataTable();

            using (var connection = _connectionProvider.GetConnection())
            using (var command = new SqlCommand("GetHungtooInvoice", connection))
            using (var adapter = new SqlDataAdapter(command))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@INVDATA", invData ?? (object)DBNull.Value);
                adapter.Fill(dataTable);
            }
            return dataTable;
        }

        public DataTable GetDistinctStates()
        {
            return GetSqlData("SELECT DISTINCT STATE1 AS STATE FROM CUSTOMER WHERE STATE1 IS NOT NULL AND STATE1 != '' order by state1");
        }

        public string GetNextCheckNoByBank(string bank)
        {
            try
            {
                DataTable dataTable = GetSqlData(@"SELECT CAST(LAST_INV AS bigint) + 1 AS LAST_INV 
                    FROM bank_acc WHERE LTRIM(RTRIM(CODE)) = @Bank AND ISNUMERIC(LAST_INV) > 0",
                    "@Bank", bank.Trim());

                if (DataTableOK(dataTable))
                {
                    string nextCheckNo = Convert.ToInt64(dataTable.Rows[0]["LAST_INV"]).ToString();
                    DataRow checkRow = GetSqlRow(
                        "SELECT * FROM checks WHERE @bank = bank AND LTRIM(RTRIM(check_no)) = @check",
                        "@bank", bank, "@check", nextCheckNo);

                    if (checkRow != null)
                    {
                        DataRow topCheckRow = GetSqlRow(
                            "SELECT ISNULL(MAX(CONVERT(BIGINT, check_no)), 0) AS TOPCHECKNO FROM CHECKS WHERE ISNUMERIC(check_no) = 1 AND bank = @bank",
                        "@bank", bank);
                        long longValue;
                        if (topCheckRow != null && long.TryParse(Convert.ToString(topCheckRow["TOPCHECKNO"]), out longValue))
                        {
                            long topCheckNo = long.Parse(Convert.ToString(topCheckRow["TOPCHECKNO"])) + 1;

                            //GetStoreProc("UPDATE BANK_ACC SET LAST_INV = @TOPCHECKNO where CODE = @bank", "@TOPCHECKNO", topCheckNo.ToString().Trim(), "@bank", bank);
                            //Commneted by Rahul 02/02/2025 Becuase here pass cmd instead of procedure;

                            using (SqlCommand dbCommand = new SqlCommand())
                            {
                                dbCommand.Connection = _connectionProvider.GetConnection();
                                dbCommand.CommandType = CommandType.Text;
                                dbCommand.CommandText = @"UPDATE BANK_ACC SET LAST_INV = @TOPCHECKNO where CODE = @bank";

                                dbCommand.Parameters.AddWithValue("@TOPCHECKNO", topCheckNo.ToString().Trim());
                                dbCommand.Parameters.AddWithValue("@bank", bank);

                                dbCommand.Connection.Open();
                                var rowsAffected = dbCommand.ExecuteNonQuery();
                            }
                            return Pad6(topCheckNo.ToString());
                        }
                    }
                }

                // If no valid check data was found
                long defaultCheckNo = DataTableOK(dataTable) ? Convert.ToInt64(dataTable.Rows[0]["LAST_INV"]) : 1;
                return Pad6(defaultCheckNo.ToString());
            }
            catch (Exception ex)
            {
                throw new Exception("Error getting the next check number by bank.", ex);
            }
        }
        public DataTable GetColumnOrderByForm(string cForm, string cFilter = "")
        {
            return GetSqlData("SELECT * FROM GridColumnOrder WHERE LTRIM(RTRIM(OPTION_NAME))=LTRIM(RTRIM(@cForm)) and FILTER_NAME=@cFilter and [USER]=@CUSER", "@cForm", cForm, "@cFilter", cFilter, "@CUSER", LoggedUser);
        }

        public DataTable SetColumnsOrder(DataTable table, params String[] columnNames)
        {
            int columnIndex = 0;
            foreach (var columnName in columnNames)
                table.Columns[columnName].SetOrdinal(columnIndex++);
            return table;
        }

        public DataTable SetColumnsOrder(DataTable table, DataTable dtColunOrdr)
        {
            foreach (DataRow dr in dtColunOrdr.Rows)
            {
                string columnName = CheckForDBNull(dr["COLUMN_NAME"]);
                int columnIndex = Convert.ToInt16(DecimalCheckForNullDBNull(dr["NEW_ORDER"]));
                if (table.Columns.Contains(columnName))
                    table.Columns[columnName].SetOrdinal(columnIndex);
            }
            return table;
        }

        public DataRow GetDatafromtblGivenvalue(string _tblName = "", string _tblField = "", string _TblFvalue = "", string _tblField1 = "", string _TblFvalue1 = "")
        {
            if (!string.IsNullOrEmpty(_tblField1) && !string.IsNullOrEmpty(_TblFvalue1))
                return GetSqlRow($"Select * from {_tblName} Where trim({_tblField}) ='{EscapeSpecialCharacters(_TblFvalue.Trim())}' and trim({_tblField1}) ='{EscapeSpecialCharacters(_TblFvalue1.Trim())}'");
            return GetSqlRow($"Select * from {_tblName} Where trim({_tblField}) ='{EscapeSpecialCharacters(_TblFvalue.Trim())}'");
        }

        public string GetValue(DataRow dtrow, string what)
        {
            return DataTableOK(dtrow) ? dtrow[what].ToString() : "";
        }

        public DataTable GetDetailsAlloc(string Vendor, string VndStyle, string grd1, string storeno = "", bool layaway = false, bool alloc = false, bool memod = false)
        {
            DataTable dataTable = new DataTable();

            using (SqlConnection connection = _connectionProvider.GetConnection())
            using (SqlCommand command = new SqlCommand("GetDetailsByVndStyle", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandTimeout = 3000;

                // Adding parameters with explicit data types
                command.Parameters.Add("@Vendor", SqlDbType.VarChar).Value = Vendor;
                command.Parameters.Add("@VndStl", SqlDbType.VarChar).Value = VndStyle;
                command.Parameters.Add("@Layaway", SqlDbType.Bit).Value = layaway;
                command.Parameters.Add("@Alloc", SqlDbType.Bit).Value = alloc;
                command.Parameters.Add("@Memod", SqlDbType.Bit).Value = memod;
                command.Parameters.Add("@storeno", SqlDbType.VarChar).Value = storeno;
                command.Parameters.Add("@grid1XmlData", SqlDbType.Xml).Value = grd1;

                // Use SqlDataAdapter to fill the DataTable
                using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(command))
                {
                    sqlDataAdapter.Fill(dataTable);
                }
            }
            return dataTable;
        }
        public DataTable GetInvoiceByStyle(string style, string storeNo, bool isShop = false, bool isStore = false)
        {
            DataTable dataTable = new DataTable();
            using (var connection = _connectionProvider.GetConnection())
            using (var dataAdapter = new SqlDataAdapter())
            {
                // Create the command and set its properties
                var command = new SqlCommand
                {
                    Connection = connection,
                    CommandType = CommandType.Text
                };

                // Build the SQL query
                string query = isShop
                    ? @"SELECT a.INV_NO, a.DATE, a.ACC 
                    FROM invoice a
                    JOIN in_items b ON a.inv_no = b.inv_no
                    WHERE a.pickupdate IS NULL
                    AND dbo.invstyle(style) = @style 
                    AND a.layaway = 0 
                    AND b.qty > 0 " +
                        (isStore ? "AND a.store_no = @storeno" : "")
                    : @"SELECT a.INV_NO, a.DATE, a.ACC, style, qty 
                    FROM invoice a
                    JOIN in_items b ON a.inv_no = b.inv_no
                    WHERE a.layaway = 1
                    AND dbo.invstyle(style) = @style " +
                        (isStore ? "AND a.store_no = @storeno" : "") +
                        " ORDER BY a.inv_no";

                command.CommandText = query;

                // Add parameters
                command.Parameters.AddWithValue("@style", style.Trim());
                if (isStore)
                    command.Parameters.AddWithValue("@storeno", storeNo);
                dataAdapter.SelectCommand = command;

                // Fill the DataTable from the adapter
                dataAdapter.Fill(dataTable);
            }
            return dataTable;
        }

        public List<SelectListItem> GetAllGLAccts()

        {
            DataTable dataTable = GetSqlData(@"select * from gl_accs order by acc");
            List<SelectListItem> salesmanList = new List<SelectListItem>();
            salesmanList.Add(new SelectListItem() { Text = "", Value = "" });
            if (DataTableOK(dataTable))
                foreach (DataRow dr in dataTable.Rows)
                    salesmanList.Add(new SelectListItem() { Text = dr["ACC"].ToString().Trim(), Value = dr["ACC"].ToString().Trim() });
            return salesmanList;
        }

        public DataRow CheckInvoiceorReturn(string invno)
        {
            return GetSqlRow("select * from in_items with (nolock) Where inv_no = @inv_no", "@inv_no", invno);
        }

        public bool iSReturnInvoice(string inv_no)
        {
            return (!DataTableOK(GetSqlData($"SELECT QTY FROM IN_ITEMS WHERE qty>0 and INV_NO =@INV_NO", "@INV_NO", inv_no.PadLeft(6, ' '))));
        }

        public bool GetPayAuthrized(string cPay)
        {
            using (var connection = _connectionProvider.GetConnection())
            using (var command = new SqlCommand(
                @"SELECT 1 
                    FROM payments with (nolock)
                    WHERE Trimmed_inv_no = TRIM(@cPay)
                    AND rtv_pay = 'P' 
                    AND authrized = 1", connection))
            {
                command.Parameters.AddWithValue("@cPay", cPay ?? (object)DBNull.Value);

                connection.Open();
                var result = command.ExecuteScalar();
                return result != null;
            }
        }

        public DataRow GetLabelDetails(string inv_no)
        {
            DataTable dataTable = GetSqlData("select * from ship_labels where inv_no=@inv_no", "@inv_no", inv_no);
            return GetRowOne(dataTable);
        }

        public DataTable GetReturnInvoiceDetails(string strInvno)
        {
            return GetSqlData(@"SELECT INTMS.QTY,INTMS.STYLE,INVS.STORE_NO FROM IN_ITEMS INTMS 
                INNER JOIN INVOICE INVS ON INTMS.INV_NO = INVS.INV_NO INNER JOIN STYLES STY ON STY.STYLE =[dbo].InvStyle(INTMS.STYLE) WHERE CAST(INTMS.INV_NO AS INT) = CAST(@INVNO AS INT) and STY.not_stock = 0", "@INVNO", strInvno.Trim());
        }

        public String AccFromInvoice(String inv_no)
        {
            DataTable dataTable = GetSqlData($"SELECT top 1 acc FROM INVOICE where inv_no=@inv_no", "@inv_no", inv_no);
            return DataTableOK(dataTable) ? Convert.ToString(dataTable.Rows[0]["acc"]) : "";
        }

        public bool GetLastSequenceNo(string inv_no)
        {
            DataTable dtSequence = GetSqlData("select 1 from sequence where try_cast(FieldValue as decimal(6,0))=try_cast(@inv_no as decimal(6,0))+1 and TableName='Invoice'", "@inv_no", inv_no);
            return DataTableOK(dtSequence);
        }

        public DataTable GetJobOfInvoice(string invno)
        {
            return GetSqlData(@"select distinct trim(repair_no) repair_no,cast(1 as bit) status from in_items ii
												join invoice i on i.inv_no=ii.inv_no
												where i.inv_no=@invno and isnull(ii.repair_no,'')<>'' and i.v_ctl_no<>'repair'
												union all
												SELECT trim(value) repair_no,cast(0 as bit) status FROM STRING_SPLIT((select distinct trim(pon) repair_no from invoice where inv_no=@invno and v_ctl_no='repair'), ',') WHERE RTRIM(value) <> ''", "@invno", invno.PadLeft(6, ' '));
        }

        public string GetRepairIobBag(string repairNo)
        {
            DataTable dtable = GetSqlData($"SELECT top 1 BARCODE FROM REP_ITEM WHERE [dbo].[GetBarcode](REPAIR_NO)=[dbo].[GetBarcode](@barcode)", "@barcode", repairNo);
            if (DataTableOK(dtable))
                return Convert.ToString(dtable.Rows[0]["BARCODE"]);
            return string.Empty;
        }

        public DataTable Reprintjobbag(string jobbagno, bool ismfg = false, string repairNo = "")
        {
            return GetStoreProc("PrintJobBag", "@REPAIR_NO", repairNo, "@STYLE", "", "@JOBBAGNO", jobbagno, "@ismfg", ismfg.ToString());
        }

        public DataTable GetSizeforjobbag(string jobbag)
        {
            return GetSqlData(@"SELECT top 1 TRIM(Size) as Size  FROM rep_item 
				where STUFF('000000', 6-LEN(TRIM(repair_no))+1, LEN(TRIM(repair_no)), TRIM(repair_no))=TRIM(@jobbag)  
				and TRIM(SIZE)!=''  order by Size", "@jobbag", jobbag);
        }
        public DataTable GetOrderRepairData(string currentrepno)
        {
            return GetStoreProc("GetOrderRepairData", "@REPAIR_NO", currentrepno);
        }
        public string GetAddressLabel(string acc, string seperator, bool NoCustName = false)
        {
            // DataTable dataTable = new DataTable();
            DataRow dataRow = GetSqlRow("select *  From Customer Where rtrim(acc)=rtrim(@acc)", "@acc", acc);

            if (dataRow != null)
            {
                string TelNo = "";
                if (CompanyName != null && CompanyName.ToUpper().Contains("CRISSON"))
                    TelNo = dataRow["tel"].ToString() == "0" || string.IsNullOrEmpty(dataRow["tel"].ToString()) ? "" : CheckForDBNull(dataRow["tel"].ToString().Length > 7 ? FormatTel(dataRow["tel"].ToString()) : FormatTel("441" + dataRow["tel"].ToString()));
                else
                    TelNo = FormatTel(CheckForDBNull(dataRow["tel"]));

                return RemoveFromEnd(string.Format((!NoCustName ? "{0}{11}{1}{11}{2}{11}{3}{11}{4} {5} {6} {7} {8}{11}{9}{11}{10}" : "{1}{11}{2}{11}{3}{11}{4} {5} {6} {7} {8}{11}{9}{11}{10}"),

                    //return RemoveFromEnd(string.Format("{0}{10}{1}{10}{2}{10}{3}{10}{4} {5} {6} {7} {8}{10}{9}",
                    dataRow["name"].ToString().Trim(),
                    dataRow["addr1"].ToString().Trim(),
                    dataRow["addr12"].ToString().Trim(),
                    dataRow["addr13"].ToString().Trim(),
                    dataRow["city1"].ToString().Trim(),
                    dataRow["state1"].ToString().Trim(),
                    dataRow["zip1"].ToString().Trim(),
                    dataRow["country"].ToString().Trim(),
                    dataRow.Table.Columns.Contains("buyer") ? !string.IsNullOrWhiteSpace(dataRow["buyer"].ToString().Trim()) ? "Attn:" + dataRow["buyer"].ToString().Trim() : string.Empty : string.Empty,
                    TelNo != string.Empty ? "Tel:" + TelNo : string.Empty,
                     String.IsNullOrWhiteSpace(Convert.ToString(dataRow["email"])) ? "" : "Email:" + dataRow["email"].ToString().Trim(),
                    seperator).Replace(", , ", ", ").Replace("\n\n", "\n").Replace("\n\n", "\n"), seperator);
            }
            return "";
        }

        public DataTable GetRepImgNames(string RepStyle)
        {
            return GetSqlData("SELECT STYLE, DESCRIPTION FROM STYL_IMAGES WHERE STYLE=@RepStyle", "@RepStyle", RepStyle.Trim());
        }

        public string GetRepImage(string p_style, string imgname)
        {
            string imagename = string.Empty;
            if (string.IsNullOrWhiteSpace(p_style) || string.IsNullOrWhiteSpace(imgname))
                return string.Empty;
            using (SqlConnection con = _connectionProvider.GetConnection())
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "GetRepairImages";
                    cmd.Parameters.AddWithValue("@stylename", p_style);
                    cmd.Parameters.AddWithValue("@imgname", imgname);
                    SqlDataReader rdr = cmd.ExecuteReader();
                    byte[] objContext = null;
                    while (rdr.Read())
                        objContext = (byte[])rdr[0];

                    rdr.Close();
                }
                con.Close();
            }
            return "File:" + imagename;
        }

        public string GetStoreAddress(string storecode, string seperator, out string storename)
        {
            // string c = storecode != string.Empty ? storecode : StoreCodeInUse;
            string c = storecode != string.Empty ? storecode : "";
            DataTable storeInfo = GetStores(c);
            string TelNo;
            if (storeInfo.Rows.Count == 1)
            {
                DataRow dataRow = storeInfo.Rows[0];
                storename = dataRow["name"].ToString().Trim();
                TelNo = FormatTel(CheckForDBNull(dataRow["tel"]));

                return string.Format("{0}{5}{1}{5}{2}{5}{3}{5}{4}{5}{6}", dataRow["addr1"].ToString().Trim(), dataRow["addr2"].ToString().Trim(), dataRow["addr3"].ToString().Trim(), dataRow["addr4"].ToString().Trim(), TelNo != string.Empty ? "Tel:" + TelNo : string.Empty, seperator, CompanyEmail).Replace("\n\n", "\n").Replace(", , ", ", ");
            }
            storename = CompanyName;
            TelNo = FormatTel(CompanyTel);

            return string.Format("{0}{5}{1}{5}{2}{5}{3}{5}{4}{5}{6}", CompanyAddr1, CompanyAddr2, CompanyCity, CompanyState, CompanyZip, TelNo, seperator, CompanyEmail).Replace("\n\n", "\n").Replace(", , ", ", ");
        }
        public DataTable GetReservePartsOnJobbag(string repair_no)
        {
            return GetSqlData("SELECT CODE PART, NOTE, CAST(CHANGE AS DECIMAL(5,0)) AS QTY, CAST(CHANGE_WT AS DECIMAL(9,2)) AS [WEIGHT] FROM PARTS_HIST WHERE JOB_BAG = right(Concat('000000', @REPAIR_NO), 7) AND ON_JOBBAG = 1", "@REPAIR_NO", repair_no.Trim());
        }

        public DataTable GetJbbagCustdetail(string _Invno = "", string Optionname = "", string strInvNo = "")
        {
            if (Optionname == "Inv")
                return GetSqlData($@"Select distinct II.Style,iI.[desc] as Description,Cast('' as nvarchar(400)) As Style_Image,Ii.warranty,
                                case when exists(select 1 from in_items where isnull(IsSpecialItem,0)=1 and trimmed_inv_no=i.trimmed_inv_no) then 'Y' else 'N' end Special,
                                S.vnd_style,s.fingersize, s.cast_code,'' WStyle,'' Warranty_inv_no,'' RWarranty
                                from in_items ii
                                left join INVOICE I on i.inv_no = ii.inv_no
                                left join styles s on  s.style = case when isnull(ii.IsSpecialItem,0)=1 then ii.specialStyle else ii.invStyle end
                                where (trim(i.inv_no) = trim(@inv_no) or trim(ii.Repair_no)=trim(@inv_no)) ", "@inv_no", _Invno);

            if (Optionname == "Rep")
                return GetSqlData($@"select top 1 iif(ri.STYLE!='',ri.STYLE,r.style) as Style,'' as Description, Cast('' as nvarchar(400)) As Style_Image, isnull(ii.Warranty,'') as Warranty,
                                case when exists(select 1 from in_items where isnull(IsSpecialItem,0)=1 and trimmed_inv_no=ii.trimmed_inv_no) then 'Y' else 'N' end Special,
                                S.vnd_style,s.fingersize, s.cast_code,isnull(R.Style,'') WStyle,isnull(R.Warranty_inv_no,'') Warranty_inv_no, 
                                [dbo].[GetJMCateWarrantyFromStyle](R.Warranty_inv_no,R.Style) as RWarranty, 
                                isnull(s.CERT_NO,'') as CERT_NO, isnull(s.CERT_TYPE,'') as CERT_TYPE
                                from repair R
                                left join in_items ii on r.repair_no=ii.repair_no
                                left join rep_item ri on ri.repair_no=r.repair_no
                                left Join Styles S On S.style = case when isnull(r.style,'')='' then ri.style else r.style end
                                where trim(r.repair_no)=trim(@inv_no) or (trim(r.inv_no)=trim(@strInv_no) and isnull(r.inv_no,'')<>'' and trim(@strInv_no)<>'')", "@inv_no", _Invno, "@strInv_no", strInvNo);

            if (Optionname == "RepInv")
            {
                return GetSqlData($@"select top 1 c.acc, R.name,R.EMAIL,I.Inv_no,iif(i.salesman1 != '', i.salesman1, iif(i.salesman2 != '', i.salesman2, iif(i.salesman3 != '', i.salesman3, i.salesman4))) as Saleman,cast(c.tel as Nvarchar(50)) As Tel
                                    , r.can_date as outDate,RCV_DATE,R.addr1 addr2, R.addr2 addr22, addr23, R.city city2, R.state state2, R.zip zip2, i.PickUpDate as PickUpDate,r.style
                                    ,(Select cast(cast(month as nvarchar(2)) + '/' + cast(dat as nvarchar(2)) + '/' + cast(Year(Getdate()) as nvarchar(4)) as date)  from occassions where acc = c.acc and(type = 'ANN' or type = 'ANV')) as DateAnni
                                    , isnull(i.ship_by, '') ship_by, case when isnull(i.inv_no,'')= '' then r.surprise else isnull(i.surprise, 0) end surprise, R.Country,case when isnull(can_date,'')<>'' then iif(r.can_date>r.date and cast(r.can_date as date)>dateadd(day,1,cast(r.[date] as date)),dateadd(day,-2,cast(r.can_date as date)),r.can_date) else r.can_date end comp_date,
                                    cast(isnull(i.PICKED,0) as bit) PICKED, [dbo].[ShippingStatus](isnull(i.ship_by,''),isnull(i.picked,0),i.layaway) ShippingStatus,r.snh, r.Operator,'' as Cert_Type, '' as Shape,'' as warranty, case when exists(select 1 from in_items where iSSpecialItem=1 and trimmed_inv_no=i.trimmed_inv_no) then 'Y' else'N' end Special,
                                    isnull(r.Style,'') WStyle,isnull(r.Warranty_inv_no,'') Warranty_inv_no,
                                    [dbo].[GetJMCateWarrantyFromStyle](r.Warranty_inv_no,r.Style) as RWarranty
                                    from invoice i
                                    join repair r on trim(r.repair_no) in(select trim(value) from string_split(i.pon, ',') where rtrim(value) <> '')
                                    join customer c on c.ACC = i.acc
                                    where i.inv_no =@inv_no", "@inv_no", _Invno);
            }

            if (is_Singer)
                return GetSqlData($@"Select top 1 C.acc, R.name,R.EMAIL, I.Inv_no, iif(r.salesman1!='',r.salesman1,iif(r.salesman2!='',r.salesman2,'')) as Saleman,cast(c.tel as Nvarchar(50)) As Tel
                                ,r.can_date as outDate,RCV_DATE,R.addr1 addr2,R.addr2 addr22,addr23,R.city city2,R.state state2,R.zip zip2,i.PickUpDate as PickUpDate,r.style 
                                ,(Select cast(cast(month as nvarchar(2))+'/'+cast(dat as nvarchar(2))+'/'+cast(Year(Getdate()) as nvarchar(4)) as date)  from occassions where acc=c.acc and  (type='ANN' or type='ANV')) as DateAnni
                                ,case when isnull(r.ship_by,'')='' then i.ship_by else r.ship_by end ship_by, case when isnull(i.inv_no,'')='' then r.surprise else isnull(i.surprise,0) end surprise,R.Country,case when isnull(can_date,'')<>'' then iif(r.can_date>r.date and cast(r.can_date as date)>dateadd(day,1,cast(r.[date] as date)) ,dateadd(day,-2,cast(r.can_date as date)),r.can_date) else r.can_date end comp_date																																																																																			 
                                ,cast(case when isnull(r.inv_no,'')='' then case when isnull((select sum(qty-shiped) from rep_item where repair_no=r.repair_no),0)=0 then 1 else 0 end else isnull(i.picked,0) end as bit) PICKED,
                                isnull(It.warranty,'') as warranty,
                                [dbo].[ShippingStatus](case when isnull(r.inv_no,'')='' then isnull(r.ship_by,'') else isnull(i.ship_by,'') end,case 
								when isnull(r.inv_no,'')='' then case when isnull((select sum(qty-shiped) 
								from rep_item where repair_no=r.repair_no),0)=0 then 1 else 0 end else cast(isnull(i.picked,0) as bit) end,0) ShippingStatus,
								R.snh, R.Operator, isnull(s.Cert_Type,'') Cert_Type, isnull(s.Shape,'') Shape,
                                case when exists(select 1 from in_items where iSSpecialItem=1 and trimmed_inv_no=i.trimmed_inv_no) then 'Y' else'N' end Special,
                                isnull(R.Style,'') WStyle,isnull(R.Warranty_inv_no,'') Warranty_inv_no,[dbo].[GetJMCateWarrantyFromStyle](R.Warranty_inv_no,R.Style) as RWarranty,
                                case when (isnull(r.ship_by,'')='' OR isnull(r.ship_by,'')='N') then i.ship_by else r.ship_by end inv_ship_by,
                                case when (isnull(r.ship_by,'')='' OR isnull(r.ship_by,'')='N') then i.shiptype else r.shiptype end inv_ship_type
                                from Repair R 
                                left join INVOICE I on
                                (r.warranty_inv_no=I.inv_no or r.INV_NO=I.inv_no or trim(r.repair_no) in(select trim(value) from string_split(i.pon, ',') where rtrim(value) <> '')) Join Customer C on r.acc=c.acc
                                left join In_items It  On I.inv_no=It.inv_no
								left Join Styles S On S.style=it.style
                                where trim(r.repair_no) = trim(@inv_no) order by i.date desc", "@inv_no", _Invno);

            return GetSqlData($@"Select top 1 C.acc, R.name,R.EMAIL,  I.Inv_no,iif(i.salesman1!='',i.salesman1,iif(i.salesman2!='',i.salesman2,iif(i.salesman3!='',i.salesman3,i.salesman4))) as Saleman,cast(c.tel as Nvarchar(50)) As Tel
                                ,r.can_date as outDate,RCV_DATE,R.addr1 addr2,R.addr2 addr22,addr23,R.city city2,R.state state2,R.zip zip2,i.PickUpDate as PickUpDate,r.style 
                                ,(Select cast(cast(month as nvarchar(2))+'/'+cast(dat as nvarchar(2))+'/'+cast(Year(Getdate()) as nvarchar(4)) as date)  from occassions where acc=c.acc and  (type='ANN' or type='ANV')) as DateAnni
                                ,case when isnull(r.ship_by,'')='' then i.ship_by else r.ship_by end ship_by, case when isnull(i.inv_no,'')='' then r.surprise else isnull(i.surprise,0) end surprise,R.Country,case when isnull(can_date,'')<>'' then iif(r.can_date>r.date and cast(r.can_date as date)>dateadd(day,1,cast(r.[date] as date)) ,dateadd(day,-2,cast(r.can_date as date)),r.can_date) else r.can_date end comp_date
                                ,cast(case when isnull(r.inv_no,'')='' then case when isnull((select sum(qty-shiped) from rep_item where repair_no=r.repair_no),0)=0 then 1 else 0 end else isnull(i.picked,0) end as bit) PICKED,
                                isnull(It.warranty,'') as warranty,
                                [dbo].[ShippingStatus](case when isnull(r.inv_no,'')='' then isnull(r.ship_by,'') else isnull(i.ship_by,'') end,case 
								when isnull(r.inv_no,'')='' then case when isnull((select sum(qty-shiped) 
								from rep_item where repair_no=r.repair_no),0)=0 then 1 else 0 end else cast(isnull(i.picked,0) as bit) end,0) ShippingStatus,
								R.snh, R.Operator, isnull(s.Cert_Type,'') Cert_Type, isnull(s.Shape,'') Shape,
                                case when exists(select 1 from in_items where iSSpecialItem=1 and trimmed_inv_no=i.trimmed_inv_no) then 'Y' else'N' end Special,
                                isnull(R.Style,'') WStyle,isnull(R.Warranty_inv_no,'') Warranty_inv_no,[dbo].[GetJMCateWarrantyFromStyle](R.Warranty_inv_no,R.Style) as RWarranty
                                from Repair R 
                                left join INVOICE I on
                                (r.warranty_inv_no=I.inv_no or r.INV_NO=I.inv_no or trim(r.repair_no) in(select trim(value) from string_split(i.pon, ',') where rtrim(value) <> '')) Join Customer C on r.acc=c.acc
                                left join In_items It  On I.inv_no=It.inv_no
								left Join Styles S On S.style=it.style
                                where trim(r.repair_no) = trim(@inv_no) order by i.date desc", "@inv_no", _Invno);
        }

        public int GetInvoiceLength(string table, string colname, string InvoiceNo)
        {
            DataRow invoicerow = null;
            invoicerow = GetSqlRow("SELECT TOP 1 LEN(" + colname + ") as inv_no FROM " + table + " where ltrim(rtrim(" + colname + ")) =ltrim(rtrim('" + InvoiceNo + "'))");
            return invoicerow == null ? 0 : Convert.ToInt32(invoicerow["inv_no"].ToString());
        }

        public bool iSSpecialOrderWithPickdup(String invNo)
        {
            DataTable dataTable = GetSqlData($"SELECT 1 FROM INVOICE I LEFT JOIN IN_ITEMS II ON I.INV_NO = II.INV_NO where(II.IsSpecialItem = 1) AND iSNULL(I.PICKED,0)=1 AND I.INV_NO =@InvNo", "@InvNo", invNo);
            return DataTableOK(dataTable);
        }

        public bool iSLayaway(string invno)
        {
            DataTable dataTable = GetSqlData($"SELECT 1 FROM INVOICE WHERE INV_NO=@INVNO AND LAYAWAY=1", "@INVNO", invno);
            return dataTable.Rows.Count > 0;
        }

        public string GetPaymentInvNo(string invno)
        {
            DataTable dt = GetSqlData($"SELECT TOP 1 INV_NO FROM PAYMENTS WHERE 1=1 AND INV_NO IN(SELECT PAY_NO FROM PAY_ITEM WHERE INV_NO='{invno}' AND RTV_PAY='I')");
            if (DataTableOK(dt))
                return Convert.ToString(dt.Rows[0]["INV_NO"]);
            return string.Empty;
        }

        public string GetInvoiceAccUpdate(string invNo = "", string newAcc = "", string accUpdate = "")
        {
            var accUpdateValue = string.IsNullOrWhiteSpace(accUpdate) ? "Invoice" :
                                 (accUpdate == "Repair" || accUpdate == "Repairvalid" || accUpdate == "MultiPay") ? accUpdate :
                                 "Invoice";


            using (var connection = _connectionProvider.GetConnection())
            using (var dataAdapter = new SqlDataAdapter("UpdateAccAsPerInvAndAcc", connection))
            {
                dataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                dataAdapter.SelectCommand.Parameters.AddWithValue("@inv_noNRep", invNo);
                dataAdapter.SelectCommand.Parameters.AddWithValue("@NewAcc", newAcc);
                dataAdapter.SelectCommand.Parameters.AddWithValue("@AccUpdate", accUpdateValue);
                var dataTable = new DataTable();
                dataAdapter.Fill(dataTable);
                return GetValue0(dataTable);
            }
        }

        public bool iSPickedUp(string rep_no)
        {
            return GetSqlData($"SELECT repair_no FROM REP_ITEM WHERE trim(REPAIR_NO) = trim(@rep_no) AND SHIPED > 0", "@rep_no", rep_no).Rows.Count > 0;

        }

        public bool IsValidInvoiceNo(string strInvoice)
        {
            DataTable dataTable = GetSqlData("SELECT * FROM INVOICE WHERE trim(INV_NO)=@INVNO", "@INVNO", strInvoice.Trim());
            return dataTable.Rows.Count > 0;
        }

        public bool iSRepairOrderExists(string REPAIRORDER_NO)
        {
            return GetSqlData($"SELECT repair_no FROM REP_ITEM WHERE REPAIR_NO = '{REPAIRORDER_NO}'").Rows.Count > 0;
        }

        public string CancelLayawayPayment(string inv_no, string layawayid, decimal amount, string method, string slctOption = "")
        {
            DataTable dataTable = new DataTable();

            using (SqlConnection conn = _connectionProvider.GetConnection())
            using (SqlCommand command = new SqlCommand("CancelLayawayPayment", conn))
            {
                command.CommandType = CommandType.StoredProcedure;


                command.Parameters.Add(new SqlParameter("@INV_NO", SqlDbType.VarChar, 50) { Value = inv_no.Trim() });
                command.Parameters.Add(new SqlParameter("@LAYAWAYID", SqlDbType.VarChar, 50) { Value = layawayid.Trim() });
                command.Parameters.Add(new SqlParameter("@AMOUNT", SqlDbType.Decimal) { Value = amount });
                command.Parameters.Add(new SqlParameter("@LOGGED_USER", SqlDbType.VarChar, 50) { Value = LoggedUser });
                command.Parameters.Add(new SqlParameter("@Method", SqlDbType.VarChar, 50) { Value = method.Trim() });
                command.Parameters.Add(new SqlParameter("@CASH_REGISTER", SqlDbType.VarChar, 50) { Value = Cash_Register });
                command.Parameters.Add(new SqlParameter("@STORE", SqlDbType.VarChar, 50) { Value = StoreCode });
                command.Parameters.Add(new SqlParameter("@DeleteOrRefund", SqlDbType.VarChar, 50) { Value = slctOption ?? (object)DBNull.Value });

                using (SqlDataAdapter dataAdapter = new SqlDataAdapter(command))
                    dataAdapter.Fill(dataTable);
            }
            return GetValue0(dataTable);
        }

        public bool iSFixedStore(String store_no)
        {
            if (!String.IsNullOrWhiteSpace(FixedStoreCode) &&
                !String.IsNullOrWhiteSpace(store_no))
                return FixedStoreCode.ToLower().Trim() != store_no.ToLower().Trim();
            return false;
        }

        public DataTable GetInvoicePayment(string invNo, bool showLayaway = true, bool isReturn = false,
            bool isFromReturn = false, bool iSRefund = false)
        {
            using (var connection = _connectionProvider.GetConnection())
            using (var command = new SqlCommand("GetInvoicePayments", connection))
            using (var dataAdapter = new SqlDataAdapter(command))
            {

                command.CommandType = CommandType.StoredProcedure;
                command.CommandTimeout = 6000;
                command.Parameters.Add(new SqlParameter("@inv_no", SqlDbType.VarChar) { Value = invNo });
                command.Parameters.Add(new SqlParameter("@showlayaway", SqlDbType.Bit) { Value = showLayaway });
                command.Parameters.Add(new SqlParameter("@is_return", SqlDbType.Bit) { Value = isReturn });
                command.Parameters.Add(new SqlParameter("@iSFromReturn", SqlDbType.Bit) { Value = isFromReturn });
                command.Parameters.Add(new SqlParameter("@iSRefund", SqlDbType.Bit) { Value = iSRefund });
                var dataTable = new DataTable();
                dataAdapter.Fill(dataTable);
                return dataTable;
            }
        }
        public bool CheckNochangedate(DateTime? cdate)
        {
            if (NoChangeBefore != null)
            {
                DateTime nochangedate;
                DateTime.TryParse(NoChangeBefore, out nochangedate);
                if (nochangedate != null)
                    return nochangedate > cdate;
            }
            return false;
        }

        public DataSet GetListOfJobEstimates(string setter, bool lDetail = false, int OpenDone = 0)
        {
            using (var connection = _connectionProvider.GetConnection())
            using (var command = new SqlCommand("ListofjobEstimates", connection))
            using (var adapter = new SqlDataAdapter(command))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandTimeout = 3000;

                // Add parameters
                command.Parameters.AddWithValue("@SETTERNAME", setter);
                command.Parameters.AddWithValue("@lDetail", lDetail ? 1 : 0);
                command.Parameters.AddWithValue("@OpenDoneAll", OpenDone);

                var dataSet = new DataSet();
                adapter.Fill(dataSet);
                return dataSet;
            }
        }

        public DataRow GetScrapGoldLog(string logno)
        {
            return GetSqlRow(@"SELECT * FROM SCRAP WHERE INV_NO=@logno", "@logno", logno);
        }

        public string TelFormat(string ctel)
        {
            if (ctel.Length < 2)
                return "";
            if (ctel.Length == 10)
                ctel = Left(ctel, 3) + "-" + ctel.Substring(3, 3) + "-" + ctel.Substring(6, 4);
            return ctel;
        }

        public DataTable GetStoreNames()
        {
            if (!string.IsNullOrWhiteSpace(FixedStoreCode))
                return GetSqlData(@"select Code,Name from stores with (nolock) where code=@code ", "@code", FixedStoreCode);
            return GetSqlData(@"select Code,Name from stores with (nolock) where code != '' and code is not null and INACTIVE=0 order by Code");
        }

        public DataTable GetSourcesDetails(out bool Isresult, string SaveorDelete = "", string xml = "", string source = "", string option = "", string optFrom = "", string optTo = "", string loggedUser = "")
        {
            Isresult = true;
            DataTable dts = null;
            if (SaveorDelete == "DELETE")
                return GetSqlData(@"DELETE FROM sources WHERE [source] = TRIM('" + source.Replace("'", "''") + "') ");

            if (SaveorDelete == "Save")
            {

                using (var connection = _connectionProvider.GetConnection())
                using (var command = new SqlCommand("SourcesTable", connection))
                using (var dataAdapter = new SqlDataAdapter(command))
                {

                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandTimeout = 6000;
                    command.Parameters.Add(new SqlParameter("@Sources", SqlDbType.Xml) { Value = xml });
                    var dataTable = new DataTable();
                    dataAdapter.Fill(dataTable);
                    return dataTable;

                }
            }

            if (SaveorDelete == "COMBINE")
            {
                using (SqlConnection sqlConnection = _connectionProvider.GetConnection())
                using (SqlCommand dbCommand = new SqlCommand("CombineSources", sqlConnection))
                {
                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.AddWithValue("@OPTION", option);
                    dbCommand.Parameters.AddWithValue("@OPT_FROM", optFrom);
                    dbCommand.Parameters.AddWithValue("@OPT_TO", optTo);
                    dbCommand.Parameters.AddWithValue("@LOGGEDUSER", loggedUser);

                    // Open the connection
                    sqlConnection.Open();

                    var rowsAffected = dbCommand.ExecuteNonQuery();
                }
                return dts;  // Returning the DataTable
            }
            if (SaveorDelete == "CheckCustomer")
                return GetSqlData(string.Format("Select top 1 [source] from customer with(nolock) where [Source] = '" + @source.ToString().Replace("'", "''") + "'  order by [source] desc"), "@strValue", source.Replace("'", "''"));
            return GetSqlData(@"SELECT isnull([source],'') as source FROM sources ORDER BY [source]");
        }

        public DataSet GetTimeSpentjobbag(string jobBag = "", DateTime? startDate = null, DateTime? endDate = null, string person = "")
        {
            if (string.IsNullOrWhiteSpace(jobBag))
                jobBag = DBNull.Value.ToString(); // Set to DBNull if jobBag is not provided

            var dataSet = new DataSet();

            using (var connection = _connectionProvider.GetConnection())
            using (var sqlDataAdapter = new SqlDataAdapter("TimeSpentjobbag", connection))

            {
                sqlDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                sqlDataAdapter.SelectCommand.CommandTimeout = 3000;

                // Add parameters with null handling for optional parameters
                sqlDataAdapter.SelectCommand.Parameters.AddWithValue("@Jobbag", string.IsNullOrWhiteSpace(jobBag) ? "" : jobBag);
                sqlDataAdapter.SelectCommand.Parameters.AddWithValue("@SETTERNAME", string.IsNullOrWhiteSpace(person) ? "" : person);
                sqlDataAdapter.SelectCommand.Parameters.AddWithValue("@Fdate", startDate ?? (object)DBNull.Value);
                sqlDataAdapter.SelectCommand.Parameters.AddWithValue("@Tdate", endDate ?? (object)DBNull.Value);

                connection.Open();
                sqlDataAdapter.Fill(dataSet);
            }
            return dataSet;
        }

        public void AddJobbagNotes(string xml, string jobbag)
        {
            using (var connection = _connectionProvider.GetConnection())
            using (var dataAdapter = new SqlDataAdapter("JbbgNote", connection))
            {
                dataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                dataAdapter.SelectCommand.Parameters.AddWithValue("@jobbag", jobbag);
                dataAdapter.SelectCommand.Parameters.AddWithValue("@jobbagnote", xml);
                var dataTable = new DataTable();
                dataAdapter.Fill(dataTable);
            }
        }

        public bool UpdateJobBagPrice(string RepairNo, decimal Price)
        {
            using (var connection = _connectionProvider.GetConnection())
            using (var command = new SqlCommand("UpdateJobBagPrice", connection))
            {
                // Setup the command and parameters
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@RepairNo", RepairNo);
                command.Parameters.AddWithValue("@Price", Price);

                // Open the connection, execute the command, and return whether the operation was successful
                connection.Open();
                var rowsAffected = command.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }

        public bool Chk_OpenAccount(string cacc)
        {
            DataTable dataTable = GetSqlData(@"select on_account from customer where acc=@cacc", "@cacc", cacc);
            if (dataTable.Rows.Count > 0)
            {
                object value = dataTable.Rows[0]["on_account"];
                if (value == DBNull.Value)
                    return false;
                return Convert.ToBoolean(dataTable.Rows[0]["on_account"].ToString());
            }
            return false;
        }

        public string iSCostLessThanEqlZero(string invNo, DataTable invoiceItems)
        {
            StringBuilder stringBuilder = new StringBuilder();
            if (DataTableOK(invoiceItems))
            {
                foreach (DataRow row in invoiceItems.Rows)
                {
                    DataRow styleInfo = CheckStyle(Convert.ToString(row["Style"]));
                    if (styleInfo != null && DecimalCheckForDBNull(styleInfo["t_cost"]) <= 0 && Convert.ToString(styleInfo["Style"]).ToUpper() != "REPAIR CHARGES")
                        stringBuilder.AppendLine($"Total Cost Should Not Be Zero Of Style# {Convert.ToString(row["Style"])}");
                }
                return stringBuilder.ToString();
            }
            return iSCostLessThanEqlZero("", GetSqlData($"SELECT STY.T_COST,IIT.STYLE,* FROM IN_ITEMS IIT JOIN STYLES STY ON IIT.STYLE = STY.STYLE WHERE INV_NO ='{invNo}' and STY.COST = 0")).ToString();
        }

        public string GetLang(string msgText)
        {
            //if (string.IsNullOrWhiteSpace(Language) || Language.ToUpper() == "ENGLISH")
            return msgText;
            //return string.Format("{0}", Translate(msgText));
        }

        public void ChargeCC(DataRow dgv, string inv_no, string acc, bool is_return, string statusLabel, out string errormessage, out bool success, out bool iSAskForSignature, out byte[] signimg, bool iSEdit = false, string doc_type = "", string zipCode = "", bool signBelow = false)
        {

            signimg = null;
            success = iSAskForSignature = false;
            errormessage = "";
            /*
                string sPaymentMthod = dgv != null ? CheckForDBNull(dgv["Method"], typeof(string).ToString()) : "";
                string sReponse = dgv != null ? CheckForDBNull(dgv["Note"], typeof(string).ToString()) : "";
                if (sPaymentMthod.Trim().ToUpper() == "CC SWIPE" && !CheckModuleEnabled(Modules.CC_Swipe))
                {
                    MsgBox(GetLang("Contact Ishal Inc. To Enable Credit Card Processing"));
                    dgv.Cells["Note"].Value = "Failed";
                    return;
                }
                double ccamt = Convert.ToDouble(CheckForDBNull(dgv.Cells["Amount"].Value, typeof(decimal).ToString()));
                if (ccamt == 0)
                {
                    MsgBox(GetLang("$0 amount credit card charge is invalid."));
                    dgv.Cells["Note"].Value = "Failed";
                    return;
                }
                if (sPaymentMthod.Trim().ToUpper() == "VIRTUAL CC TERMINAL" &&
                    (string.IsNullOrWhiteSpace(sReponse) || sReponse.ToLower() == "false" || sReponse == "Failed" || sReponse == "0" || (sReponse.Contains("Auth") && is_return)))
                {
                    if (statusLabel != null)
                    {
                        statusLabel.Visible = true;
                        statusLabel.Refresh();
                    }

                    var objVCC = new frmCardConnectVCC(inv_no, acc, ccamt, is_return, sReponse.Replace("Auth#", "").Trim(), zipCode);
                    objVCC.ShowDialog();
                    success = false;
                    if (objVCC._success && !string.IsNullOrWhiteSpace(objVCC._retref))
                    {
                        dgv.Cells["Note"].Value = string.Format("Auth# {0}", objVCC._retref.Trim());
                        success = true;
                        iSAskForSignature = (Read_Signature || Read_Signature_CC) && !iSEdit && signBelow;
                    }

                    if (statusLabel != null)
                    {
                        statusLabel.Visible = false;
                        statusLabel.Refresh();
                    }
                    if (!success)
                    {
                        errormessage += string.Format("\nCredit Card Payment Failed Amount: {0}: ", ccamt);
                        dgv.Cells["Note"].Value = "Failed";
                        MsgBox(GetLang("Virtual CC terminal payment did not go thru."));
                        return;
                    }
                }

                else if (sPaymentMthod.Trim().ToUpper() == "CC SWIPE" &&
                    (string.IsNullOrWhiteSpace(sReponse) || sReponse.ToLower() == "false" || sReponse == "Failed" || sReponse == "0" || (sReponse.Contains("Auth") && is_return)))
                {
                    if (statusLabel != null)
                    {
                        statusLabel.Visible = true;
                        statusLabel.Refresh();
                    }
                    bool timeout = false;
                    ProcessCard oCC = new ProcessCard();
                    oCC.ccAmount = ccamt * (is_return ? -1 : 1);
                    oCC.ccRefNo = string.Format("INV#{0}-{1}", inv_no, dgv.Index);
                    oCC.isDebitCard = false;
                    oCC.doSignOnCard = (Read_Signature || Read_Signature_CC) &&
                        !iSEdit && signBelow;

                    oCC.doInclAddr = false;
                    oCC.transactionnote = string.Format("{0}-{1}", CompanyName, inv_no);
                    oCC.transactionID = sReponse.Replace("Auth#", "").Trim();
                    //success = oCC.ProcessCharge(!signBelow, out timeout);

                    if (statusLabel != null)
                    {
                        statusLabel.Visible = false;
                        statusLabel.Refresh();
                    }

                    if (!success)
                    {
                        if (timeout)
                        {
                            errormessage += string.Format("\nCredit Card Payment Failed Amount: {0}: due to timeout", ccamt);
                            dgv.Cells["Note"].Value = "Failed";
                            MsgBox(GetLang("Credit card swipe payment did not go thru due to timeout."));
                        }
                        else
                        {
                            errormessage += string.Format("\nCredit Card Payment Failed Amount: {0}: ", ccamt);
                            dgv.Cells["Note"].Value = "Failed";
                            MsgBox(GetLang("Credit card swipe payment did not go thru."));
                        }
                        return;
                    }

                    for (int c = 0; c < dgv.Cells.Count; c++)
                    {
                        if (dgv.Cells[c].ColumnInfo.Name.ToUpper() == "ASKEDSIGATURE")
                        {
                            dgv.Cells["askedSigature"].Value = oCC.signatureImage != null;
                            break;
                        }
                    }

                    if (oCC.signatureImage != null)
                    {
                        using (var ms = new MemoryStream())
                        {
                            oCC.signatureImage.Save(ms, oCC.signatureImage.RawFormat);
                            signimg = ms.ToArray();
                            if (signimg != null)
                                AddSignature(signimg, inv_no, string.Format("sign_{0}.jpg", inv_no), "CCSIGN", doc_type);
                        }
                    }

                    dgv.Cells["Note"].Value = string.Format("Auth# {0}", oCC.retRef);
                    oCC.Dispose();
                }
            */
        }

        public DataRow ValidateCustomerCreditlimit(string acc, string inv_no)
        {
            inv_no = inv_no.Trim().PadLeft(6, ' ');
            return GetSqlRow("SELECT ISNULL(iSNULL((SELECT SUM(GR_TOTAL-CREDITS) FROM INVOICE WHERE BACC=@acc AND INV_NO NOT IN(@inv_no)),0)- iSNULL((SELECT SUM(PAID-APPLIED) FROM PAYMENTS WHERE ACC=@acc),0),0) - iSNULL((select sum(PAID-APPLIED) from PAYMENTS where inv_no in(select PAY_NO from pay_item where inv_no=@inv_no)),0) BALANCE",
                "@ACC", acc, "@INV_NO", inv_no);
        }

        public string GetStoreNo(string invno, String forPayment)
        {
            DataTable dataTable = new DataTable();
            if (forPayment == "I")
                dataTable = GetSqlData($"SELECT STORE_NO FROM INVOICE WHERE INV_NO=@inv_no", "@inv_no", invno);
            else if (forPayment == "R")
                dataTable = GetSqlData($";declare @var nvarchar(max); SET @var=@inv_no; SELECT STORE as STORE_NO FROM REPAIR WHERE REPAIR_NO IN((select [value] from string_split(@var,',')))", "@inv_no", invno);

            if (DataTableOK(dataTable))
                return Convert.ToString(dataTable.Rows[0]["STORE_NO"]);
            return "";
        }

        public byte[] LocalStoreImg()
        {
            //string imagePath = $"{AppDomain.CurrentDomain.BaseDirectory}\\StoreLogo.png";
            string tempPath = Path.Combine(Path.GetTempPath(), "logo.png");
            byte[] imageBytes = File.ReadAllBytes(tempPath);
            if (imageBytes != null && imageBytes.Length >= 100)
                return imageBytes;
            return GetStoreImage();
        }

        public DataTable GetAllCustomerForFinanceCharge()
        {
            try
            {
                DataTable dataTable = new DataTable();

                using (SqlConnection connection = _connectionProvider.GetConnection())
                using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter())
                {
                    // Create the command and set its properties
                    sqlDataAdapter.SelectCommand = new SqlCommand("GetAllCustomerForFinanceCharge", connection)
                    {
                        CommandType = CommandType.StoredProcedure,
                        CommandTimeout = 12000
                    };

                    // Fill the datatable from adapter
                    sqlDataAdapter.Fill(dataTable);
                }

                return dataTable;
            }
            catch (Exception)
            {
                // Log the exception if needed
                throw;
            }
        }

        public bool CalculateFinanceCharges(out string retInv)
        {
            retInv = string.Empty;

            try
            {
                using (var connection = _connectionProvider.GetConnection())
                using (var dbCommand = new SqlCommand("CalculateFinanceCharges", connection) { CommandType = CommandType.StoredProcedure })
                {
                    dbCommand.Parameters.AddWithValue("@USERACC", LoggedUser);

                    var outInvNo = new SqlParameter("@RETVAL", SqlDbType.NVarChar, Int32.MaxValue)
                    {
                        Direction = ParameterDirection.Output
                    };
                    dbCommand.Parameters.Add(outInvNo);

                    connection.Open();
                    int rowsAffected = dbCommand.ExecuteNonQuery();
                    retInv = outInvNo.Value.ToString();
                    return rowsAffected > 0;
                }
            }
            catch (Exception ex)
            {
                // Rethrow with additional context if necessary
                throw new Exception("Error calculating finance charges.", ex);
            }
        }

        public DataTable GetMasterDetailInvoice_FinanceCharge(string invno)
        {
            return GetStoreProc("GetMasterDetailInvoice_FinanceCharge", "@inv_no", invno);
        }

        public DataTable getInventoryCountByVendor(string store, bool Vendor, bool IncludeItemsOnMemo, bool isDetails = false)
        {
            using (var connection = _connectionProvider.GetConnection())
            using (var command = new SqlCommand("getInventoryCountbyCat", connection))
            using (var adapter = new SqlDataAdapter(command))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Store", store);
                command.Parameters.AddWithValue("@isgroupby_SubCat", false);
                command.Parameters.AddWithValue("@isgroupby_Vendor", Vendor);
                command.Parameters.AddWithValue("@isIncludeItemsOnMemo", IncludeItemsOnMemo);
                command.Parameters.AddWithValue("@isDetails", isDetails);

                var dataTable = new DataTable();
                adapter.Fill(dataTable);
                return dataTable;
            }
        }
        private bool CheckNodeModuleAccess(XmlNode node)
        {
            if (node.Attributes["UserLevel"] != null)
            {
                int menuLevel;
                if (int.TryParse(node.Attributes["UserLevel"].Value, out menuLevel))
                {
                    if (LoggedUserLevel < menuLevel)
                        return false;
                }
            }

            if (node.Attributes["ShowOnlyFor"] != null)
            {
                string value = node.Attributes["ShowOnlyFor"].Value;
                object sessionValue = null;

                if (_httpContextAccessor.HttpContext != null && _httpContextAccessor.HttpContext?.Session != null)
                    sessionValue = _httpContextAccessor.HttpContext?.Session.GetString(value);

                if (sessionValue is bool)
                {
                    if (!(bool)sessionValue)
                        return false;
                }
                else if (Enum.IsDefined(typeof(Modules), value))
                {
                    Modules module =
                        (Modules)Enum.Parse(typeof(Modules), value, true);

                    if (!CheckModuleEnabled(module))
                        return false;
                }
                /*else if (!GetVariableValue(value))
                {
                    return false;
                }*/
            }

            if (node.Attributes["ShowExcept"] != null)
            {
                string value = node.Attributes["ShowExcept"].Value;
                object sessionValue = null;

                if (_httpContextAccessor.HttpContext != null && _httpContextAccessor.HttpContext.Session != null)
                    sessionValue = _httpContextAccessor.HttpContext?.Session.GetString(value);

                if (sessionValue is bool)
                {
                    if ((bool)sessionValue)
                        return false;
                }
                else if (Enum.IsDefined(typeof(Modules), value))
                {
                    Modules module =
                        (Modules)Enum.Parse(typeof(Modules), value, true);

                    if (CheckModuleEnabled(module))
                        return false;
                }
                /*else if (GetVariableValue(value))
                {
                    return false;
                }*/
            }

            return true;
        }

        /*public bool GetVariableValue(string variable_name)
        {
            FieldInfo field = typeof(Helper).GetField(variable_name, BindingFlags.Public | BindingFlags.Static);
            return (bool?)field?.GetValue(null) ?? false;
        }*/

        public DataTable ListOfRepairOrdersReport(string REPAIR_NO, string FROMACCCODE, string TOACCCODE, string FROMDATE, string TODATE, string FROMREPAIRNUMBER, string TOREPAIRNUMBER, string ISOPEN, string OREDERBY, string salesman, string rStore = "", int pickupstatus = 2, string repStatus = "", string iSCustForRep = "", int dateValue = 0, string keyword = "")
        {
            return GetStoreProc("LISTOFREPAIRS", "@FROMACCCODE", FROMACCCODE.Trim(), "@TOACCCODE", TOACCCODE.Trim(), "@FROMDATE", FROMDATE.Trim(), "@TODATE", TODATE.Trim(), "@FROMREPAIRNUMBER", FROMREPAIRNUMBER.Trim(), "@TOREPAIRNUMBER", TOREPAIRNUMBER.Trim(), "@ISOPEN", ISOPEN.Trim(), "@SALESMAN", salesman.Trim(), "@STORE", rStore.Trim(), "@PICKUPSTATUS", pickupstatus.ToString(), "@repStatus", repStatus, "@iSCustForRep", iSCustForRep, "@dateValue", dateValue.ToString(), "@keyword", keyword);
        }

        public String GetRepairNoByCustRepNo(String custRepairNo)
        {
            DataTable dt = GetSqlData("select TOP 1 REPAIR_NO FROM repair with (nolock) where cus_rep_no=@custRepairNo", "@custRepairNo", custRepairNo.Trim());
            return (DataTableOK(dt)) ? Convert.ToString(dt.Rows[0]["REPAIR_NO"]) : String.Empty;
        }

        public bool GetPickedOrNot(string iNVNo)
        {
            return (GetSqlData($"SELECT 1 FROM INVOICE WHERE INV_NO=@iNVNo AND (PICKED=1 OR (GR_TOTAL-CREDITS)=0)", "@inVNo", iNVNo).Rows.Count > 0);
        }

        public string GetAddressLabelfrom_repairorder(string inv_no, string acc, string seperator)
        {
            // DataTable dataTable = new DataTable();
            DataRow dataRow = GetSqlRow("select iif(isnull(R.NAME,'')='',C.[NAME],R.NAME) [name],R.addr1,R.addr2,C.addr23,R.city,R.state,R.zip,R.country,C.buyer,C.TEL,C.email From repair R left outer join customer C on(C.acc=R.acc) Where trim(repair_no)='" + inv_no.Trim() + "'");
            if (dataRow != null)
            {
                string TelNo = FormatTel(CheckForDBNull(dataRow["tel"]));

                return RemoveFromEnd(string.Format("{0}{11}{1}{11}{2}{11}{3}{11}{4} {5} {6} {7} {8}{11}{9}{11}{10}",
                    dataRow["name"].ToString().Trim(),
                    dataRow["addr1"].ToString().Trim(),
                    dataRow["addr2"].ToString().Trim(),
                    dataRow["addr23"].ToString().Trim(),
                    dataRow["city"].ToString().Trim(),
                    dataRow["state"].ToString().Trim(),
                    dataRow["zip"].ToString().Trim(),
                    dataRow["country"].ToString().Trim(),
                    dataRow.Table.Columns.Contains("buyer") ? !string.IsNullOrWhiteSpace(dataRow["buyer"].ToString().Trim()) ? "Attn:" + dataRow["buyer"].ToString().Trim() : string.Empty : string.Empty,
                    TelNo != string.Empty ? TelNo : string.Empty,
                     String.IsNullOrWhiteSpace(Convert.ToString(dataRow["email"])) ? "" : dataRow["email"].ToString().Trim(),
                    seperator).Replace(", , ", ", ").Replace("\n\n", "\n").Replace("\n\n", "\n"), seperator);
            }
            return "";
        }

        public string GetCurrSysFormatDate(object cDate)
        {
            try
            {
                string returnDate = "";
                string sqlFormat = GetSeverDateFormat();
                string sysFormat = GetSeverDateFormat(true);

                if (sqlFormat.Replace("/", "-") != sysFormat.Replace("/", "-"))
                {
                    string[] keys = sqlFormat.Replace("/", "-").Split('-');
                    string[] values = Convert.ToString(cDate).Replace("/", "-").Split('-');

                    Dictionary<string, string> sqlDict = new Dictionary<string, string>();
                    string[] sysKeys = sysFormat.Replace("/", "-").Split('-');

                    for (int i = 0; i < keys.Length; i++)
                        sqlDict.Add(keys[i], values[i]);

                    for (int i = 0; i < sysKeys.Length; i++)
                    {
                        switch (sysKeys[i].ToLower())
                        {
                            case "dd":
                            case "d":
                                sysKeys[i] = sqlDict["dd"];
                                break;
                            case "mm":
                            case "m":
                                sysKeys[i] = sqlDict["MM"];
                                break;
                            case "yyyy":
                                sysKeys[i] = sqlDict["yyyy"];
                                break;
                        }

                        if (!string.IsNullOrWhiteSpace(returnDate))
                            returnDate += "/";
                        returnDate += sysKeys[i];
                    }
                }
                return String.IsNullOrWhiteSpace(returnDate) ? Convert.ToString(cDate) : returnDate;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetRepDepositPayments(string inv_no, string RelatedInvoiceNo = "")
        {
            string query = string.Format(@"SELECT A.DATE,case 
										when ISNULL(A.PAYMENTTYPE,'')='Store Credit' then ISNULL(A.PAYMENTTYPE,'')+' (Credit# : '+B.PAYREFNO+')'   
										when  ISNULL(A.PAYMENTTYPE,'')='Gift Card' then ISNULL(A.PAYMENTTYPE,'')+' (GC# : '+ISNULL((SELECT iif(len(trim(ISNULL(UserGCNo,'')))= 0,TRIM( B.PAYREFNO),TRIM(UserGCNo)) 
										FROM StoreCreditVoucher WHERE TRIM(CreditNo)=TRIM( B.PAYREFNO)),'')+')' 
										else ISNULL(A.PAYMENTTYPE,'') end as METHOD, 
										A.PAID AS AMOUNT, B.NOTE,B.CURR_RATE,B.CURR_TYPE,B.CURR_AMOUNT FROM PAID_RPR A JOIN PAYMENTS B ON A.PAY_NO=B.INV_NO WHERE trim(A.REPAIR_NO)=trim(@inv_no) AND A.REPINV_NO=@REPINV");

            if (!String.IsNullOrWhiteSpace(RelatedInvoiceNo))
            {
                if (!String.IsNullOrWhiteSpace(query))
                    query += " UNION ";
                query += $" SELECT  DATE, PAYMENTTYPE METHOD, PAID AS AMOUNT, NOTE,CURR_RATE,CURR_TYPE,CURR_AMOUNT  FROM PAYMENTS WHERE INV_NO IN(SELECT PAY_NO FROM PAY_ITEM WHERE INV_NO='{RelatedInvoiceNo}' and rtv_pay='d') AND RTV_PAY='P'";
            }
            return GetSqlData(query, "@inv_no", inv_no, "@REPINV", " ");
        }

        public DataTable GetJobDetials(string jobBagNumber, string repNo = "")
        {
            var dataTable = new DataTable();
            using (var connection = _connectionProvider.GetConnection())
            using (var command = new SqlCommand())
            using (var adapter = new SqlDataAdapter(command))
            {
                connection.Open();
                command.Connection = connection;
                command.CommandType = CommandType.Text;

                if (!string.IsNullOrWhiteSpace(repNo))
                {
                    command.CommandText = "SELECT * FROM OR_ITEMS with (nolock) WHERE PON = @repNo";
                    command.Parameters.Add(new SqlParameter("@repNo", SqlDbType.NVarChar) { Value = repNo });
                }
                else
                {
                    command.CommandText = "SELECT * FROM OR_ITEMS with (nolock) WHERE BARCODE = @jobBagNumber";
                    command.Parameters.Add(new SqlParameter("@jobBagNumber", SqlDbType.NVarChar) { Value = jobBagNumber });
                }
                adapter.Fill(dataTable);
            }
            return dataTable;
        }

        public DataTable getInventoryCountByCategory(string store, bool subCatGroup, bool includeItemsOnMemo, bool noNegativeCheck, bool isDetails = false)
        {
            using (var connection = _connectionProvider.GetConnection())
            using (var command = new SqlCommand("getInventoryCountbyCat", connection))
            using (var adapter = new SqlDataAdapter(command))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Store", store);
                command.Parameters.AddWithValue("@isgroupby_SubCat", subCatGroup);
                command.Parameters.AddWithValue("@isIncludeItemsOnMemo", includeItemsOnMemo);

                command.Parameters.AddWithValue("@NoNegativeStock", noNegativeCheck);
                command.Parameters.AddWithValue("@isDetails", isDetails);
                command.CommandTimeout = 9000;
                var dataTable = new DataTable();
                adapter.Fill(dataTable);
                return dataTable;
            }
        }

        public bool CheckStyle(string style, out string retstyle, out bool isBar)
        {
            string _unused1, _unused5, _unused6;
            decimal _unused2, _unused3, _unused4;


            if (string.IsNullOrWhiteSpace(style))
            {
                retstyle = "";
                isBar = false;
                return false;
            }
            return CheckStyle(style, out retstyle, out isBar, out _unused1, out _unused2, out _unused3, out _unused4, out _unused5, out _unused6, 0);
        }

        public bool CheckStyle(string style, out string retstyle, out bool isBar, out string piece, out decimal price, out decimal tagprice, out decimal cost, out string styledesc, out string popupnote, decimal discount = 0)

        {
            isBar = false;
            piece = "Y";
            retstyle =
                styledesc =
                popupnote = string.Empty;
            price =
                tagprice =
                cost = 0;
            if (string.IsNullOrWhiteSpace(style))
                return false;

            DataRow stylerow = CheckStyle(style);

            if (stylerow == null)
                return false;
            retstyle = CheckForDBNull(stylerow["style"]);
            isBar = (CheckForDBNull(stylerow["barcode"].ToString()) == style) || CheckModuleEnabled(Modules.AutoQty) ||
                (DataTableOK(stylerow) && is_StyleItem);
            styledesc = CheckForDBNull(stylerow["desc"]);
            popupnote = CheckForDBNull(stylerow["popupnote"]);
            Not_Stock = CheckForDBNull(stylerow["NOT_STOCK"], typeof(bool).ToString());
            price = CheckForDBNull(stylerow["price"], typeof(decimal).ToString()) * (1 - discount / 100);
            tagprice = CheckForDBNull(stylerow["price"], typeof(decimal).ToString());

            cost = CheckForDBNull(stylerow["cost"], typeof(decimal).ToString());
            return true;
        }

        public DataTable getInventoryCountByGLClass(bool storegroup, bool IncludeItemsOnMemo)
        {
            using (var connection = _connectionProvider.GetConnection())
            using (var command = new SqlCommand("getInventoryCountbyGL_Class", connection))
            using (var adapter = new SqlDataAdapter(command))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@isgroupby_store", storegroup);
                command.Parameters.AddWithValue("@isIncludeItemsOnMemo", IncludeItemsOnMemo);

                var dataTable = new DataTable();
                adapter.Fill(dataTable);
                return dataTable;
            }
        }

        public DataTable getInventoryByGLClassDetails(string glcode, string store, bool includeMemo)
        {
            var dataTable = new DataTable();

            using (var connection = _connectionProvider.GetConnection())
            using (var command = new SqlCommand("GetInventoryByStyleStore", connection))
            using (var dataAdapter = new SqlDataAdapter(command))
            {
                command.CommandType = CommandType.StoredProcedure;

                // Add parameters with proper naming conventions
                command.Parameters.AddWithValue("@storeno", store);
                command.Parameters.AddWithValue("@classgl", glcode);
                command.Parameters.AddWithValue("@isIncludememo", includeMemo);
                command.Parameters.AddWithValue("@IsInventoryByGLClass", 1);

                // Fill the DataTable
                dataAdapter.Fill(dataTable);
            }

            return dataTable;
        }

        public List<SelectListItem> GetCertType()
        {
            DataTable dataTable = GetSqlData("SELECT cert_type  From [certs]");
            List<SelectListItem> certTypeList = new List<SelectListItem>();
            certTypeList.Add(new SelectListItem() { Text = "", Value = "" });
            if (DataTableOK(dataTable))
                foreach (DataRow dr in dataTable.Rows)
                    certTypeList.Add(new SelectListItem() { Text = dr["cert_type"].ToString().Trim(), Value = dr["cert_type"].ToString().Trim() });
            return certTypeList;
        }

        public List<SelectListItem> GetStyleQuality()
        {
            DataTable dataTable = GetSqlData("SELECT Distinct QUALITY  AS QUALITY from [styles] where isnull(QUALITY,'')<>''");
            List<SelectListItem> styleQualityList = new List<SelectListItem>();
            styleQualityList.Add(new SelectListItem() { Text = "", Value = "" });
            if (DataTableOK(dataTable))
                foreach (DataRow dr in dataTable.Rows)
                    styleQualityList.Add(new SelectListItem() { Text = dr["QUALITY"].ToString().Trim(), Value = dr["QUALITY"].ToString().Trim() });
            return styleQualityList;
        }

        public List<SelectListItem> GetDiamondShape()
        {
            DataTable dataTable = GetSqlData("SELECT  SHAPE  AS SHAPE from [DSHAPES]");
            List<SelectListItem> diamondShapeList = new List<SelectListItem>();
            diamondShapeList.Add(new SelectListItem() { Text = "", Value = "" });
            if (DataTableOK(dataTable))
                foreach (DataRow dr in dataTable.Rows)
                    diamondShapeList.Add(new SelectListItem() { Text = dr["SHAPE"].ToString().Trim(), Value = dr["SHAPE"].ToString().Trim() });
            return diamondShapeList;
        }

        public List<SelectListItem> GetDiamondColor()
        {
            DataTable dataTable;
            string qry = "SELECT top 1000 COLOR, setorder from [DCOLORS] order by setorder, color";
            dataTable = GetSqlData("select color from( " + qry + ") tt");
            List<SelectListItem> diamondColorList = new List<SelectListItem>();
            diamondColorList.Add(new SelectListItem() { Text = "", Value = "" });
            if (DataTableOK(dataTable))
                foreach (DataRow dr in dataTable.Rows)
                    diamondColorList.Add(new SelectListItem() { Text = dr["COLOR"].ToString().Trim(), Value = dr["COLOR"].ToString().Trim() });
            return diamondColorList;
        }

        public List<SelectListItem> getstoresdataforsetdefault(bool ActiveOnly = false, bool allstores = false, bool withShop = false, bool NoText = false)
        {
            DataTable dataTable;
            if (NoText)
            {
                if (!string.IsNullOrWhiteSpace(FixedStoreCode) && !allstores)
                    dataTable = GetSqlData("SELECT DISTINCT CODE FROM [stores] where code=@code ", "@code", FixedStoreCode);
                else
                    dataTable = GetSqlData("SELECT DISTINCT CODE FROM [stores] where notext=0  ORDER BY CODE ");
            }
            if (!withShop)
            {
                if (!string.IsNullOrWhiteSpace(FixedStoreCode) && !allstores)
                    dataTable = GetSqlData("SELECT DISTINCT CODE FROM [stores] where code=@code ", "@code", FixedStoreCode);
                else if (ActiveOnly)
                    dataTable = GetSqlData("select distinct code from stores where code != '' and code is not null  order by code asc ");
                else
                    dataTable = GetSqlData("SELECT DISTINCT CODE FROM [stores] ORDER BY CODE ");
            }
            if (!string.IsNullOrWhiteSpace(FixedStoreCode) && !allstores)
                dataTable = GetSqlData("SELECT 'SHOP' as CODE UNION SELECT DISTINCT CODE FROM [stores] where code=@code ", "@code", FixedStoreCode);
            else if (ActiveOnly)
                dataTable = GetSqlData("SELECT 'SHOP' as code UNION select distinct code from stores where code != '' and code is not null ");
            else
                dataTable = GetSqlData("SELECT 'SHOP' as CODE UNION SELECT DISTINCT CODE FROM [stores] ");

            List<SelectListItem> storesDataList = new List<SelectListItem>();
            storesDataList.Add(new SelectListItem() { Text = "", Value = "" });
            if (DataTableOK(dataTable))
                foreach (DataRow dr in dataTable.Rows)
                    storesDataList.Add(new SelectListItem() { Text = dr["CODE"].ToString().Trim(), Value = dr["CODE"].ToString().Trim() });
            return storesDataList;
        }

        public DataTable SearchProformaInvoice(bool onlyNonconverted = false, string dtFilter = "1=1")
        {
            if (!onlyNonconverted)
                return GetSqlData(@"SELECT ID, INV_NO, C.ACC, C.NAME, try_cast(TEL as NVARCHAR(50)) TEL, CAST(INV.DATE AS DATE) DATE, C.ADDR1, STATE1, ZIP1, GR_TOTAL AS Flag, INV.STORE_NO STORE FROM Proforma_INVOICE INV INNER JOIN CUSTOMER C ON TRIM(INV.ACC) = TRIM(C.ACC) AND " + dtFilter + " ORDER BY DATE");
            return GetSqlData(@"SELECT ID, INV_NO, C.ACC, C.NAME, try_cast(TEL as NVARCHAR(50)) TEL, CAST(INV.DATE AS DATE) DATE, C.ADDR1, STATE1, ZIP1, GR_TOTAL AS Flag, INV.STORE_NO STORE FROM Proforma_INVOICE INV INNER JOIN CUSTOMER C ON TRIM(INV.ACC) = TRIM(C.ACC) WHERE ISNULL(CONVERTED_INV_NO,'') = '' AND ISNULL(FROM_MEMO_NO,'') = '' AND " + dtFilter + "  ORDER BY DATE");
        }

        public DataTable GetCustomerofProformaInvoice(string invno)
        {
            return GetSqlData("SELECT isnull(ACC,'') CUST_ACC FROM Proforma_INVOICE WHERE Trimmed_inv_no=@inv_no AND ISNULL(CONVERTED_INV_NO,'') = '' AND ISNULL(FROM_MEMO_NO,'') ='' ", "@inv_no", invno.Trim());
        }

        public bool ModifyCustomer_Proforma(string invno, string oldAcc, string newAcc)
        {
            using (var connection = _connectionProvider.GetConnection())
            using (var command = new SqlCommand(@"
			UPDATE Proforma_Invoice 
			SET ACC = @NEW_ACC, BACC = @NEW_ACC 
			WHERE INV_NO = @INV_NO AND ACC = @OLD_ACC; 

			UPDATE Proforma_Invoice 
			SET 
				[NAME] = ISNULL(cust.[NAME], ''), 
				[ADDR1] = ISNULL(cust.[ADDR1], ''), 
				[ADDR2] = ISNULL(cust.[ADDR12], ''), 
				[ADDR3] = ISNULL(cust.[ADDR13], ''), 
				[CITY] = ISNULL(cust.[CITY1], ''), 
				[STATE] = ISNULL(cust.[STATE1], ''), 
				[ZIP] = ISNULL(cust.[ZIP1], ''), 
				[COUNTRY] = ISNULL(cust.[COUNTRY], '') 
			FROM Proforma_Invoice Pinv 
			INNER JOIN Customer cust ON TRIM(Pinv.ACC) = TRIM(cust.ACC)
			WHERE Pinv.INV_NO = @INV_NO AND Pinv.ACC = @NEW_ACC;",
                connection))
            {
                command.CommandType = CommandType.Text;

                command.Parameters.AddRange(new[]
                {
                new SqlParameter("@INV_NO", invno),
                new SqlParameter("@OLD_ACC", oldAcc),
                new SqlParameter("@NEW_ACC", newAcc)
                });

                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();

                return rowsAffected > 0;
            }
        }

        public DataTable GetinventorybyCaseNum(string store, bool isNotDetails = false, string caseNo = "", string venCode = "", bool ignoreStylesWOCase = false)
        {
            DataTable dataTable = new DataTable();
            using (var connection = _connectionProvider.GetConnection())
            using (var command = new SqlCommand("getInventoryCountbyCase", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandTimeout = 3000;
                // Add parameters
                command.Parameters.AddWithValue("@Store", store);
                command.Parameters.AddWithValue("@isnotdetails", isNotDetails);
                command.Parameters.AddWithValue("@caseno", caseNo);
                command.Parameters.AddWithValue("@vcode", venCode);
                command.Parameters.AddWithValue("@SingleStore", string.IsNullOrEmpty(FixedStoreCode) ? "0" : "1");
                command.Parameters.AddWithValue("@ignoreStylesWOCase", ignoreStylesWOCase ? "1" : "0");
                // Open connection and execute the command
                connection.Open();
                using (var sqlDataAdapter = new SqlDataAdapter(command))
                    sqlDataAdapter.Fill(dataTable);
            }
            return dataTable;
        }

        public DateTime LastCheck = Convert.ToDateTime("1900-01-01");
        public void GetDefaultValuesTag()
        {
            TimeSpan timediff = DateTime.UtcNow - LastCheck;  // Don't check more than once every 2 seconds
            if (timediff.TotalSeconds < 2)
                return;
            LastCheck = DateTime.UtcNow;
            try
            {
                DataTable dataTable1 = GetSqlData("select * from ups_ins1 with (nolock)");
                DataTable dataTable = GetSqlData("select * from ups_ins with (nolock)");

                if (DataTableOK(dataTable))
                {
                    //RFID Printer Margins
                    RfidPrintLeft = CheckForDBNullUPS(dataTable, "RfidPrintLeft", typeof(decimal).FullName); // Updated to 1.23 from image
                    RfidPrintRight = CheckForDBNullUPS(dataTable, "RfidPrintRight", typeof(decimal).FullName); // Updated to 4.567 from image
                    RfidPrintTop = CheckForDBNullUPS(dataTable, "RfidPrintTop", typeof(decimal).FullName); // Updated to 3.256 from image
                    RfidPrintCinc = CheckForDBNullUPS(dataTable, "RfidPrintCinc", typeof(decimal).FullName); // Updated to 3.333 from image (Distance between lines)

                    NotSideBySide1 = CheckForDBNullUPS(dataTable, "not_Sidebyside", typeof(bool).FullName);

                    TagMultiplyer = CheckForDBNull(dataTable.Rows[0]["tag_multiplier"], 1, typeof(decimal).FullName);

                    NoPriceOnTag = CheckForDBNullUPS(dataTable, "no_tagprice", "System.Boolean");
                    TagLeft1 = CheckForDBNullUPS(dataTable, "tag_left1");
                    TagLeft2 = CheckForDBNullUPS(dataTable, "tag_left2");
                    TagLeft3 = CheckForDBNullUPS(dataTable, "tag_left3");
                    TagLeft4 = CheckForDBNullUPS(dataTable, "tag_left4");
                    TagLeft5 = CheckForDBNullUPS(dataTable, "tag_left5");
                    TagLeft6 = CheckForDBNullUPS(dataTable1, "tag_left6");
                    TagLeft7 = CheckForDBNullUPS(dataTable1, "tag_left7");

                    TagLeft1A = CheckForDBNullUPS(dataTable, "tag_left1A");
                    TagLeft2A = CheckForDBNullUPS(dataTable, "tag_left2A");
                    TagLeft3A = CheckForDBNullUPS(dataTable, "tag_left3A");
                    TagLeft4A = CheckForDBNullUPS(dataTable, "tag_left4A");
                    TagLeft5A = CheckForDBNullUPS(dataTable, "tag_left5A");
                    TagLeft6A = CheckForDBNullUPS(dataTable1, "tag_left6A");
                    TagLeft7A = CheckForDBNullUPS(dataTable1, "tag_left7A");

                    TagLeft1B = CheckForDBNullUPS(dataTable, "tag_left1B");
                    TagLeft2B = CheckForDBNullUPS(dataTable, "tag_left2B");
                    TagLeft3B = CheckForDBNullUPS(dataTable, "tag_left3B");
                    TagLeft4B = CheckForDBNullUPS(dataTable, "tag_left4B");
                    TagLeft5B = CheckForDBNullUPS(dataTable, "tag_left5B");
                    TagLeft6B = CheckForDBNullUPS(dataTable1, "tag_left6B");
                    TagLeft7B = CheckForDBNullUPS(dataTable1, "tag_left7B");

                    TagRight1 = CheckForDBNullUPS(dataTable, "TAG_RIGHT1");
                    TagRight2 = CheckForDBNullUPS(dataTable, "Tag_Right2");
                    TagRight3 = CheckForDBNullUPS(dataTable, "Tag_Right3");

                    TagRight4 = CheckForDBNullUPS(dataTable, "Tag_Right4");
                    TagRight5 = CheckForDBNullUPS(dataTable, "Tag_Right5");
                    TagRight6 = CheckForDBNullUPS(dataTable1, "Tag_Right6");
                    TagRight7 = CheckForDBNullUPS(dataTable1, "Tag_Right7");

                    TagRight4A = CheckForDBNullUPS(dataTable, "Tag_Right4A");
                    TagRight5A = CheckForDBNullUPS(dataTable, "Tag_Right5A");
                    TagRight6A = CheckForDBNullUPS(dataTable1, "Tag_Right6A");
                    TagRight7A = CheckForDBNullUPS(dataTable1, "Tag_Right7A");
                    TagRight1A = CheckForDBNullUPS(dataTable1, "Tag_Right1A");
                    TagRight3A = CheckForDBNullUPS(dataTable1, "Tag_Right3A");
                    TagRight2A = CheckForDBNullUPS(dataTable, "Tag_Right2A");

                    TagRight4B = CheckForDBNullUPS(dataTable, "Tag_Right4B");
                    TagRight5B = CheckForDBNullUPS(dataTable, "Tag_Right5B");
                    TagRight6B = CheckForDBNullUPS(dataTable1, "Tag_Right6B");
                    TagRight7B = CheckForDBNullUPS(dataTable1, "Tag_Right7B");
                    TagRight1B = CheckForDBNullUPS(dataTable1, "Tag_Right1B");
                    TagRight3B = CheckForDBNullUPS(dataTable1, "Tag_Right3B");
                    TagRight2B = CheckForDBNullUPS(dataTable, "Tag_Right2B");

                    Tag_place = CheckForDBNullUPS(dataTable, "TAG_PLACE");
                    Tag_text = CheckForDBNullUPS(dataTable, "TAG_TEXT");

                    Tag_place2 = CheckForDBNullUPS(dataTable1, "TAG_PLACE2");
                    Tag_text2 = CheckForDBNullUPS(dataTable1, "TAG_TEXT2");
                    Tag_place3 = CheckForDBNullUPS(dataTable1, "TAG_PLACE3");
                    Tag_text3 = CheckForDBNullUPS(dataTable1, "TAG_TEXT3");
                    Tag_place4 = CheckForDBNullUPS(dataTable1, "TAG_PLACE4");
                    Tag_text4 = CheckForDBNullUPS(dataTable1, "TAG_TEXT4");

                    TagLeft1C = CheckForDBNullUPS(dataTable1, "tag_left1C");
                    TagLeft2C = CheckForDBNullUPS(dataTable1, "tag_left2C");
                    TagLeft3C = CheckForDBNullUPS(dataTable1, "tag_left3C");
                    TagLeft4C = CheckForDBNullUPS(dataTable1, "tag_left4C");
                    TagLeft5C = CheckForDBNullUPS(dataTable1, "tag_left5C");
                    TagLeft6C = CheckForDBNullUPS(dataTable1, "tag_left6C");
                    TagLeft7C = CheckForDBNullUPS(dataTable1, "tag_left7C");

                    TagRight1C = CheckForDBNullUPS(dataTable1, "Tag_Right1C");
                    TagRight3C = CheckForDBNullUPS(dataTable1, "Tag_Right3C");
                    TagRight2C = CheckForDBNullUPS(dataTable, "Tag_Right2C");
                    TagRight4C = CheckForDBNullUPS(dataTable, "Tag_Right4C");
                    TagRight5C = CheckForDBNullUPS(dataTable, "Tag_Right5C");
                    TagRight6C = CheckForDBNullUPS(dataTable1, "Tag_Right6C");
                    TagRight7C = CheckForDBNullUPS(dataTable1, "Tag_Right7C");

                    TagLeft1D = CheckForDBNullUPS(dataTable1, "tag_left1D");
                    TagLeft2D = CheckForDBNullUPS(dataTable1, "tag_left2D");
                    TagLeft3D = CheckForDBNullUPS(dataTable1, "tag_left3D");
                    TagLeft4D = CheckForDBNullUPS(dataTable1, "tag_left4D");
                    TagLeft5D = CheckForDBNullUPS(dataTable1, "tag_left5D");
                    TagLeft6D = CheckForDBNullUPS(dataTable1, "tag_left6D");
                    TagLeft7D = CheckForDBNullUPS(dataTable1, "tag_left7D");

                    TagRight1D = CheckForDBNullUPS(dataTable1, "Tag_Right1D");
                    TagRight3D = CheckForDBNullUPS(dataTable1, "Tag_Right3D");
                    TagRight2D = CheckForDBNullUPS(dataTable, "Tag_Right2D");
                    TagRight4D = CheckForDBNullUPS(dataTable, "Tag_Right4D");
                    TagRight5D = CheckForDBNullUPS(dataTable, "Tag_Right5D");
                    TagRight6D = CheckForDBNullUPS(dataTable1, "Tag_Right6D");
                    TagRight7D = CheckForDBNullUPS(dataTable1, "Tag_Right7D");

                    TagLeft1E = CheckForDBNullUPS(dataTable1, "tag_left1E");
                    TagLeft2E = CheckForDBNullUPS(dataTable1, "tag_left2E");
                    TagLeft3E = CheckForDBNullUPS(dataTable1, "tag_left3E");
                    TagLeft4E = CheckForDBNullUPS(dataTable1, "tag_left4E");
                    TagLeft5E = CheckForDBNullUPS(dataTable1, "tag_left5E");
                    TagLeft6E = CheckForDBNullUPS(dataTable1, "tag_left6E");
                    TagLeft7E = CheckForDBNullUPS(dataTable1, "tag_left7E");

                    TagRight1E = CheckForDBNullUPS(dataTable1, "Tag_Right1E");
                    TagRight3E = CheckForDBNullUPS(dataTable1, "Tag_Right3E");
                    TagRight2E = CheckForDBNullUPS(dataTable, "Tag_Right2E");
                    TagRight4E = CheckForDBNullUPS(dataTable, "Tag_Right4E");
                    TagRight5E = CheckForDBNullUPS(dataTable, "Tag_Right5E");
                    TagRight6E = CheckForDBNullUPS(dataTable1, "Tag_Right6E");
                    TagRight7E = CheckForDBNullUPS(dataTable1, "Tag_Right7E");

                    RFID_Port = CheckForDBNullUPS(dataTable, "Rfid_Port");

                    RFID_Printer_Port = CheckForDBNullUPS(dataTable, "RFID_PRINTER_PORT");

                    StyleField1 = CheckForDBNullUPS(dataTable, "StyleField1");
                    StyleField2 = CheckForDBNullUPS(dataTable, "StyleField2");
                    StyleField3 = CheckForDBNullUPS(dataTable, "StyleField3");
                    StyleField4 = CheckForDBNullUPS(dataTable, "StyleField4");
                    StyleField5 = CheckForDBNullUPS(dataTable, "StyleField5");

                    StyleField6 = CheckForDBNullUPS(dataTable1, "StyleField6");
                    StyleField7 = CheckForDBNullUPS(dataTable1, "StyleField7");
                    StyleField8 = CheckForDBNullUPS(dataTable1, "StyleField8");


                    Tag_place = CheckForDBNullUPS(dataTable, "TAG_PLACE");
                    Tag_text = CheckForDBNullUPS(dataTable, "TAG_TEXT");

                    Tag_place2 = CheckForDBNullUPS(dataTable1, "TAG_PLACE2");
                    Tag_text2 = CheckForDBNullUPS(dataTable1, "TAG_TEXT2");
                    Tag_place3 = CheckForDBNullUPS(dataTable1, "TAG_PLACE3");
                    Tag_text3 = CheckForDBNullUPS(dataTable1, "TAG_TEXT3");
                    Tag_place4 = CheckForDBNullUPS(dataTable1, "TAG_PLACE4");
                    Tag_text4 = CheckForDBNullUPS(dataTable1, "TAG_TEXT4");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public SelectListItem[] GetStoresInventoryByCase(string storecode = "", bool addEmptyStore = false)
        {
            DataTable dt = new DataTable();
            if (!string.IsNullOrWhiteSpace(FixedStoreCode))
                dt = GetSqlData("SELECT * FROM [stores] with (nolock) where code=@code ", "@code", FixedStoreCode);
            else if (!string.IsNullOrWhiteSpace(storecode))
                dt = GetSqlData("select top 1 * from stores with (nolock) where code = @storecode", "@storecode", storecode);
            else
                dt = GetSqlData(addEmptyStore ? "select * from stores with (nolock) where inactive = 0" : "select * from stores with (nolock) where inactive = 0");

            IEnumerable<SelectListItem> items = dt.AsEnumerable().Select(row => new SelectListItem
            {
                Value = row.Field<string>("code"),
                Text = row.Field<string>("code")
            });

            if (addEmptyStore)
            {
                items = new[] { new SelectListItem { Value = "", Text = "" } }.Concat(items);
            }

            return items.ToArray();
        }

        public string UpsJobbagPrinter { get; set; }
        public string upsAddressLabelPrinter { get; set; }
        public DataTable GetAllFromTagTemplate(string templateName = "")
        {
            if (templateName == "")
                return GetSqlData("SELECT * from tag_template with (nolock) ORDER BY TEMPLATENAME");
            return GetSqlData(string.Format("SELECT * from tag_template where TEMPLATENAME = '{0}'", templateName.Replace("'", "''")));
        }
        public bool Modi_Cust(string oldacc, string newacc, bool newship, bool accexist)
        {
            try
            {
                GetStoreProc("CHNG_ACC", "@old_ship", oldacc, "@cacc", newacc, "@new_ship", newship.ToString(), "@acc_exist", accexist.ToString());
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public void CheckZebraCitohTscGodex(bool isLabel = false)
        {
            DataRow drTagMargins;
            if (isLabel)
                drTagMargins = GetSqlRow("SELECT * FROM TAG_PRINTER with (nolock) WHERE ISLABEL = 1");
            else
                drTagMargins = GetSqlRow("SELECT * FROM TAG_PRINTER with (nolock) WHERE TRIM(NAME) = @NAME", "@NAME", WorkingPrinter.Trim());

            if (drTagMargins != null)
            {
                isTsc = CheckForDBNull(drTagMargins["TSC"], "System.Boolean");
                isCitoh = CheckForDBNull(drTagMargins["CITOH"], "System.Boolean");
                isZebra = CheckForDBNull(drTagMargins["ZEBRA"], "System.Boolean");
                isGodex = CheckForDBNull(drTagMargins["GODEX"], "System.Boolean");
            }
        }
        public string WorkingPrinter
        {
            get { return _WorkingPrinter; }
            set
            {
                if (value != null)
                    _WorkingPrinter = value;
            }
        }
        public void SetPrinterMargins(out decimal tagprinterleft, out decimal tagprinterright, out decimal tagprintertop, out decimal tagprintercinc, out decimal tagprintertopR, bool isLabel, out decimal tagprinterlength)
        {
            tagprinterleft = tagprinterright = tagprintertop = tagprintercinc = tagprintertopR = tagprinterlength = 0;
            DataRow drTagMargins;

            if (isLabel)
                drTagMargins = GetSqlRow("SELECT * FROM TAG_PRINTER with (nolock) WHERE ISLABEL = 1");
            else
                drTagMargins = GetSqlRow("SELECT * FROM TAG_PRINTER with (nolock) WHERE TRIM(NAME) = @NAME", "@NAME", WorkingPrinter.Trim());

            if (drTagMargins != null)
            {
                tagprinterleft = CheckForDBNull(drTagMargins["TAG_LEFT"], "System.Decimal");
                tagprinterright = CheckForDBNull(drTagMargins["TAG_RIGHT"], "System.Decimal");
                tagprintertop = CheckForDBNull(drTagMargins["TAG_TOP"], "System.Decimal");
                tagprintercinc = CheckForDBNull(drTagMargins["TAG_DISTANCE"], "System.Decimal");
                tagprinterlength = CheckForDBNull(drTagMargins["tag_length"], "System.Decimal");
                if (CheckForDBNull(drTagMargins["NOT_SIDEBYSIDE"], "System.Boolean"))
                    tagprintertopR = CheckForDBNull(drTagMargins["TOP_LEFT"], "System.Decimal");
                else
                    tagprintertopR = CheckForDBNull(drTagMargins["TAG_TOP"], "System.Decimal");
                if (Tag2PerRow && !TagOdd)
                {
                    tagprintertopR += CheckForDBNull(drTagMargins["TagEvenOffset_Y"], "System.Decimal");
                    tagprintertop += CheckForDBNull(drTagMargins["TagEvenOffset_Y"], "System.Decimal");
                    tagprinterleft += CheckForDBNull(drTagMargins["TagEvenOffset_X"], "System.Decimal");
                    tagprinterright += CheckForDBNull(drTagMargins["TagEvenOffset_X"], "System.Decimal");
                }
                printerfont = CheckForDBNull(drTagMargins["FONTNAME"], "System.Decimal");
                tagPrinterPort = CheckForDBNull(drTagMargins["PORT"]);
            }
            else
            {
                throw new Exception("Cannot find the printer margins for printer: " + WorkingPrinter.Trim());
            }

        }
        public void Tag_Start(StringBuilder printtag, decimal tagprinterlength = 0)
        {
            if (!TagOdd && Tag2PerRow)
                return;
            if (isZebra)
            {
                printtag.Append("^XA");
                printtag.AppendLine();
                printtag.Append("^MMT");
                printtag.AppendLine();
                printtag.Append("^MNY");
                printtag.AppendLine();
                printtag.Append("^LL115^FS");
                printtag.AppendLine();
                printtag.Append("^XZ");
                printtag.AppendLine();
                printtag.Append("^XA");
                printtag.AppendLine();
                return;
            }
            if (isTsc)
            {
                printtag.Append("CLS");
                printtag.AppendLine();
                return;
            }
            if (isGodex)
            {
                printtag.Append("^H19");
                printtag.AppendLine();
                printtag.Append("^S4");
                printtag.AppendLine();
                printtag.Append("~R900");
                printtag.AppendLine();
                printtag.Append("^Q11,6");
                printtag.AppendLine();
                printtag.Append("^E11");
                printtag.AppendLine();
                printtag.Append("^W90");
                printtag.AppendLine();
                if (tagprinterlength > 0)
                {
                    printtag.Append(string.Format("^Q{0}", Decimal.ToInt32(tagprinterlength).ToString().Trim()));
                    printtag.AppendLine();
                }
                printtag.Append("^L");
                printtag.AppendLine();
                return;
            }
            if (isCitoh && CompanyName.ToUpper().Contains("CHIPP"))
            {
                printtag.Append(string.Format("{0}{1}{2}", "", "~M000", Environment.NewLine));
                printtag.Append(string.Format("{0}{1}{2}", "", "~O0215", Environment.NewLine));
                printtag.Append(string.Format("{0}{1}{2}", "", "~L", Environment.NewLine));
                printtag.Append(string.Format("{0}{1}{2}", "", "D11", Environment.NewLine));
                printtag.Append(string.Format("{0}{1}{2}", "", "PE", Environment.NewLine));
                printtag.Append(string.Format("{0}{1}{2}", "", "SE", Environment.NewLine));
                printtag.Append(string.Format("{0}{1}{2}", "", "H17", Environment.NewLine));
                return;
            }
            if (isCitoh)
            {
                printtag.Append(string.Format("{0}{1}{2}", (char)2, "c0000", Environment.NewLine));
                printtag.Append(string.Format("{0}{1}{2}", (char)2, "e", Environment.NewLine));
                printtag.Append(string.Format("{0}{1}{2}", (char)2, "O0215", Environment.NewLine));
                printtag.Append(string.Format("{0}{1}{2}", (char)2, "f290", Environment.NewLine));
                printtag.Append(string.Format("{0}{1}{2}{3}{4}", (char)1, "D", (char)2, "L", Environment.NewLine));
                printtag.Append(string.Format("{0}{1}{2}", (char)2, "L", Environment.NewLine));
                printtag.Append(string.Format("{0}{1}{2}", "", "D11", Environment.NewLine));
                printtag.Append(string.Format("{0}{1}{2}", "", "PC", Environment.NewLine));
                printtag.Append(string.Format("{0}{1}{2}", "", "SC", Environment.NewLine));
                printtag.Append(string.Format("{0}{1}{2}", "", "H30", Environment.NewLine));
                return;
            }
            printtag.Append("N");
            printtag.AppendLine();
            printtag.Append("D13");
            printtag.AppendLine();
            printtag.Append("R0,0");
            printtag.AppendLine();
            printtag.Append("ZT");
            printtag.AppendLine();
            printtag.Append("oRE,125");
            printtag.AppendLine();
        }
        public bool CheckZebra()
        {
            DataRow drPrinterDetails = GetSqlRow("select * from tag_printer with (nolock) where name = @name", "@name", WorkingPrinter.Trim());
            if (drPrinterDetails != null)
                return (CheckForDBNull(drPrinterDetails["ZEBRA"], "System.Boolean") == true);
            return false;
        }
        public void add_tag(StringBuilder printtag, decimal left, decimal top, string what, bool bold = false, string fontsize = "")
        {
            if (!string.IsNullOrWhiteSpace(what))
            {
                printtag.Append(say_tag(left, top, what, fontsize));
                printtag.AppendLine();
            }
        }

        private string say_tag(decimal x, decimal y, string txt, string fontsize = "")
        {
            if (string.IsNullOrWhiteSpace(txt))
                return "";
            if (isZebra)
                return (say_zebra(x, y, txt));
            if (isGodex)
                return say_godex(x, y, txt);
            if (isCitoh)
                return (say_citoh(x, y, txt, (printerfont.ToString() == "3") ? "8" : "6"));
            if (isTsc)
                return (say_tsc(x, y, txt));
            return (say_tlp(x, y, txt, fontsize));
        }
        private string say_zebra(decimal x, decimal y, string txt, bool do_rfid = false)
        {
            if (string.IsNullOrWhiteSpace(txt))
                return "";
            string pf = !do_rfid ?
                (((printerfont).ToString() == "3") ? "D" : "B") + "N,10,7" :
                "0,30,30";
            return string.Format("^FO{0},{1}^A" + pf + "^FD{2}^FS",
                Decimal.ToInt32(x).ToString().Trim(), Decimal.ToInt32(y).ToString().Trim(),
                txt.Replace(@"""", @"\"""));
        }
        private string say_godex(decimal x, decimal y, string txt)
        {

            return string.Format("AT,{0},{1},25,20,0,0,0,0,{2}", Decimal.ToInt32(x).ToString().Trim(), Decimal.ToInt32(y).ToString().Trim(), txt);
        }

        private string say_citoh(decimal x, decimal y, string txt, string font = "6")
        {
            string cfont = ((printerfont).ToString() == "1" ? "4" : font);
            return string.Format("{0}{1}{2}{3}", "1911A0" + cfont, Decimal.ToInt32(y).ToString().Trim().PadLeft(4, '0'), Decimal.ToInt32(x).ToString().Trim().PadLeft(4, '0'), txt);
        }
        private string say_tsc(decimal x, decimal y, string txt)
        {
            string cfont = ((printerfont).ToString() == "1" ? "\"2\"" : "\"3\"");
            if (is_Ram)
                return string.Format("TEXT {0},{1},{2},180,1,1,\"{3}\"", Decimal.ToInt32(x).ToString().Trim(),
                    Decimal.ToInt32(y + 35).ToString().Trim(), cfont, txt.Replace(@"""", @"\"""));  // add +35 to eliminate space between barcode and text
            return string.Format("TEXT {0},{1},{2},0,1,1,\"{3}\"", Decimal.ToInt32(x).ToString().Trim(), Decimal.ToInt32(y).ToString().Trim(), cfont, txt.Replace(@"""", @"\"""));
        }

        private string say_tlp(decimal x, decimal y, string txt, string fontsize = "")
        {
            if (string.IsNullOrWhiteSpace(txt))
                return "";
            string printfont = "2";
            if (fontsize != "")
                printfont = fontsize;
            else if ((printerfont).ToString() != "")
                printfont = (printerfont).ToString();
            string cfont = (is_Moi && printfont == "1") ? "1,2,1" : string.Format("{0},1,1", printfont);
            return string.Format("A{0},{1},0," + cfont + ",N,\"{2}\"", Decimal.ToInt32(x).ToString().Trim(),
                Decimal.ToInt32(y).ToString().Trim(), txt.Replace(@"""", @"\""").Replace("�", "}"));
        }

        public void Tag_End(StringBuilder printtag)
        {
            if (Tag2PerRow)
                TagOdd = !TagOdd;
            if (!TagOdd && CheckModuleEnabled(Modules.TwoTagsPerRow))
                return;

            if (isZebra)
            {
                printtag.Append(string.Format("^XZ"));
                printtag.AppendLine();
            }
            else if (isGodex)
            {
                printtag.Append(string.Format("E"));
                printtag.AppendLine();
            }
            else if (isTsc)
            {
                printtag.Append(string.Format("PRINT 1,1"));
                printtag.AppendLine();
            }
            else if (isCitoh)
            {
                printtag.Append(string.Format("Q0001"));
                printtag.AppendLine();
                printtag.Append(string.Format("E"));
                printtag.AppendLine();
            }
            else
            {
                printtag.Append(string.Format("P{0}", 1));
                printtag.AppendLine();
            }
        }
        public string WriteTag(int noOfTags, StringBuilder printtag)
        {
            try
            {
                string folder = @"C:\Temp";
                if (!Directory.Exists(folder))
                    Directory.CreateDirectory(folder);

                string filename = $"tagprint_{DateTime.Now:yyyyMMdd_HHmmssfff}.txt";
                string filePath = Path.Combine(folder, filename);

                // Convert to bytes preserving control characters
                byte[] bytes = Encoding.GetEncoding(28591).GetBytes(printtag.ToString());

                using (var fs = new FileStream(filePath, FileMode.Create, FileAccess.Write))
                {
                    for (int i = 0; i < noOfTags; i++)
                    {
                        fs.Write(bytes, 0, bytes.Length);
                    }
                }

                string strTagPrinter = tagPrinterPort;
                if (string.IsNullOrWhiteSpace(strTagPrinter))
                    throw new Exception("Printer port not defined for selected printer.");

                ExecuteCopyCommand(filePath, strTagPrinter);
                saveDefaultPrintersToPassFile();

                // ✅ return the file path, not just message
                return filePath;
            }
            catch (Exception)
            {
                throw;
            }
        }


        private void ExecuteCopyCommand(string filePath, string strTagPrinter)
        {
            string SrPathshow = $"/c copy \"{filePath}\" \"{strTagPrinter}\"";
            var startInfo = new ProcessStartInfo
            {
                FileName = "cmd.exe",
                Arguments = SrPathshow,
                WindowStyle = ProcessWindowStyle.Hidden
            };
            AddKeepRec($"final Step path{SrPathshow}");
            using (var process = new Process { StartInfo = startInfo })
            {
                process.EnableRaisingEvents = true;

                if (!File.Exists(@"C:\autopass.txt"))
                {
                    AddKeepRec($"autopasss is true file will be delete ");
                    process.Exited += (sender, e) =>
                    {
                        try { File.Delete(filePath); } catch { }
                    };
                }
                process.Start();
                AddKeepRec($"process done to create file last here {SrPathshow} ");
            }
        }

        private void saveDefaultPrintersToPassFile()
        {
            GetSqlData("Update PassFile set TAGPRINTERNO = @WorkingPrinter,TEMPLATENAME = @templateName  where [name] = @username",
                "@WorkingPrinter", WorkingPrinter, "@templateName", tagTemplateName, "@username", LoggedUser);
        }

        public void OptimiseTagTemplates(string template)
        {
            DataTable dtTemplate = GetSqlData("SELECT [tag_left1],[tag_left2],[tag_left3],[tag_left4],[tag_left1A],[tag_left2A],[tag_left3A],[tag_left4A],[tag_left1B],[tag_left2B],[tag_left3B],[tag_left4B],[tag_left5],[tag_left5A],[tag_left5B],[TAG_RIGHT4],[TAG_RIGHT4A],[TAG_RIGHT4B],[TAG_RIGHT5],[TAG_RIGHT5A],[TAG_RIGHT5B] FROM tag_template WHERE templatename = @template", "@template", template);
            if (DataTableOK(dtTemplate))
            {
                for (int iCol = 0; iCol <= dtTemplate.Columns.Count - 1; iCol++)
                {
                    if (CheckForDBNull(dtTemplate.Rows[0][iCol]) != "")
                    {
                        DataTable dtField = GetSqlData(string.Format("SELECT IIF(COL_LENGTH('Styles','{0}') IS NOT NULL,'Field Exists','Field Does Not Exist') Status", dtTemplate.Rows[0][iCol].ToString()));
                        string _pleace = dtTemplate.Rows[0][iCol].ToString();
                        if (CheckForDBNull(dtField.Rows[0][0]) == "Field Does Not Exist")
                        {
                            if (!_pleace.Contains("_PLACE"))
                            {
                                using (SqlCommand dbCommand1 = new SqlCommand())
                                {
                                    dbCommand1.Connection = _connectionProvider.GetConnection();
                                    dbCommand1.CommandType = CommandType.Text;
                                    dbCommand1.CommandText = "UPDATE tag_template SET " + CheckForDBNull(dtTemplate.Columns[iCol]) + "='' WHERE templatename=@template";
                                    dbCommand1.Parameters.AddWithValue("@TEMPLATE", template);
                                    dbCommand1.CommandTimeout = 5000;
                                    dbCommand1.Connection.Open();
                                    var rowAffected = dbCommand1.ExecuteNonQuery();
                                    dbCommand1.Connection.Close();
                                }
                            }
                        }
                    }
                }
            }
        }

        public object PrintTags(string style, int nooftags, bool is_rfid = false, int show_code = 0, bool noPrice = false)
        {
            DataRow drStyle = CheckStyle(style);
            NoPriceOnTag = NoPriceOnTag ? NoPriceOnTag : noPrice;
            string cfld = is_Briony ? "fieldvalue6" : "fieldvalue5";
            if (DoCostCode && (CheckForDBNull(drStyle[cfld]) == ""))
            {
                string costCode = Get_CostCode(Convert.ToDouble(CheckForDBNull(drStyle["t_cost"], "System.Decimal")));
                drStyle[cfld] = costCode;
                GetSqlRow("UPDATE STYLES SET " + cfld + " = @COSTCODE WHERE STYLE = @STYLE", "@STYLE", style, "@COSTCODE", costCode);
            }

            if (drStyle != null)
            {
                string cstyle;
                cstyle = style.Trim();
                decimal diamondwt = 0, colorwt = 0;
                decimal.TryParse(Convert.ToString(CheckForDBNull(drStyle["stone_wt"])), out diamondwt);
                decimal.TryParse(Convert.ToString(CheckForDBNull(drStyle["color_wt"])), out colorwt);

                string diamondinfo = diamondwt > 0 ? string.Format(Showdia() + "{0:N2} {1}",
                    Convert.ToDecimal(drStyle["stone_wt"]), drStyle["DQLTY"].ToString()) : string.Empty;
                string colorinfo = colorwt > 0 ? ShowCol() + string.Format("{0:N2} {1}",
                    Convert.ToDecimal(drStyle["color_wt"]), drStyle["CQLTY"].ToString()) : string.Empty;
                // string cBar = drStyle["barcode"].ToString();
                decimal dPrice = drStyle["price"] != DBNull.Value ? Math.Round((Convert.ToDecimal(drStyle["price"]) * Convert.ToDecimal(TagMultiplyer)), 2) : 0;

                if (CheckModuleEnabled(Modules.Ind_Dollar))
                {
                    decimal currencyRate = 0;
                    DataRow drCurrencies = GetSqlRow("SELECT TOP 1 * FROM CURRENCIES with (nolock) ORDER BY DATE DESC");
                    if (drCurrencies != null)
                        currencyRate = CheckForDBNull(drCurrencies["RATE"], "System.Decimal");
                    dPrice = drStyle["price"] != DBNull.Value ? Math.Round((Convert.ToDecimal(drStyle["price"]) * Convert.ToDecimal(currencyRate)), 2) : 0;
                }
                string price1;

                GetDollerandNotDecimalValueForTag(out price1, dPrice);

                drStyle.Table.Columns.Remove("Price");
                if (!drStyle.Table.Columns.Contains("Price"))
                    drStyle.Table.Columns.Add("Price", typeof(string));
                if (price1 != "0.00")
                    foreach (DataRow dr in drStyle.Table.Rows)
                        dr["Price"] = price1;
                string tag1 = string.Empty, tag2 = string.Empty, tag3 = string.Empty, tag4 = string.Empty,
                    tag5 = string.Empty, tag6 = string.Empty, tag7 = string.Empty, TagRight1Line = string.Empty,
                    TagRight2Line = string.Empty,
                    TagRight3Line = string.Empty, TagRight4Line = string.Empty, TagRight5Line = string.Empty,
                    TagRight6Line = string.Empty, TagRight7Line = string.Empty;
                TemTag_text = ""; TemTag_text2 = ""; TemTag_text3 = ""; TemTag_text4 = "";


                if (tagTemplateName != "")
                {
                    DataTable dt = GetAllFromTagTemplate(tagTemplateName);
                    if (DataTableOK(dt))
                    {
                        NoPriceOnTag = noPrice || CheckForDBNullUPS(dt, "no_price", "System.Boolean");
                        TagLeft1 = CheckForDBNullUPS(dt, "tag_left1");
                        TagLeft2 = CheckForDBNullUPS(dt, "tag_left2");
                        TagLeft3 = CheckForDBNullUPS(dt, "tag_left3");
                        TagLeft4 = CheckForDBNullUPS(dt, "tag_left4");
                        TagLeft5 = CheckForDBNullUPS(dt, "tag_left5");
                        TagLeft6 = CheckForDBNullUPS(dt, "tag_left6");
                        TagLeft7 = CheckForDBNullUPS(dt, "tag_left7");

                        TagRight1 = CheckForDBNullUPS(dt, "TAG_RIGHT1");
                        TagRight2 = CheckForDBNullUPS(dt, "Tag_Right2");
                        TagRight3 = CheckForDBNullUPS(dt, "Tag_Right3");

                        TagRight4 = CheckForDBNullUPS(dt, "tag_right4");
                        TagRight5 = CheckForDBNullUPS(dt, "tag_right5");
                        TagRight6 = CheckForDBNullUPS(dt, "tag_right6");
                        TagRight7 = CheckForDBNullUPS(dt, "tag_right7");

                        TagLeft1A = CheckForDBNullUPS(dt, "tag_left1A");
                        TagLeft2A = CheckForDBNullUPS(dt, "tag_left2A");
                        TagLeft3A = CheckForDBNullUPS(dt, "tag_left3A");
                        TagLeft4A = CheckForDBNullUPS(dt, "tag_left4A");
                        TagLeft5A = CheckForDBNullUPS(dt, "tag_left5A");
                        TagLeft6A = CheckForDBNullUPS(dt, "tag_left6A");
                        TagLeft7A = CheckForDBNullUPS(dt, "tag_left7A");

                        TagRight1A = CheckForDBNullUPS(dt, "tag_right1A");
                        TagRight2A = CheckForDBNullUPS(dt, "tag_right2A");
                        TagRight3A = CheckForDBNullUPS(dt, "tag_right3A");
                        TagRight4A = CheckForDBNullUPS(dt, "tag_right4A");
                        TagRight5A = CheckForDBNullUPS(dt, "tag_right5A");
                        TagRight6A = CheckForDBNullUPS(dt, "tag_right6A");
                        TagRight7A = CheckForDBNullUPS(dt, "tag_right7A");

                        TagLeft1B = CheckForDBNullUPS(dt, "tag_left1B");
                        TagLeft2B = CheckForDBNullUPS(dt, "tag_left2B");
                        TagLeft3B = CheckForDBNullUPS(dt, "tag_left3B");
                        TagLeft4B = CheckForDBNullUPS(dt, "tag_left4B");
                        TagLeft5B = CheckForDBNullUPS(dt, "tag_left5B");
                        TagLeft6B = CheckForDBNullUPS(dt, "tag_left6B");
                        TagLeft7B = CheckForDBNullUPS(dt, "tag_left7B");

                        TagRight1B = CheckForDBNullUPS(dt, "tag_right1B");
                        TagRight2B = CheckForDBNullUPS(dt, "tag_right2B");
                        TagRight3B = CheckForDBNullUPS(dt, "tag_right3B");
                        TagRight4B = CheckForDBNullUPS(dt, "tag_right4B");
                        TagRight5B = CheckForDBNullUPS(dt, "tag_right5B");
                        TagRight6B = CheckForDBNullUPS(dt, "tag_right6B");
                        TagRight7B = CheckForDBNullUPS(dt, "tag_right7B");
                        TemTag_text = CheckForDBNullUPS(dt, "tag_Text");
                        TemTag_text2 = CheckForDBNullUPS(dt, "tag_Text2");
                        TemTag_text3 = CheckForDBNullUPS(dt, "tag_Text3");
                        TemTag_text4 = CheckForDBNullUPS(dt, "tag_Text4");

                        TagLeft1C = CheckForDBNullUPS(dt, "tag_left1C");
                        TagLeft2C = CheckForDBNullUPS(dt, "tag_left2C");
                        TagLeft3C = CheckForDBNullUPS(dt, "tag_left3C");
                        TagLeft4C = CheckForDBNullUPS(dt, "tag_left4C");
                        TagLeft5C = CheckForDBNullUPS(dt, "tag_left5C");
                        TagLeft6C = CheckForDBNullUPS(dt, "tag_left6C");
                        TagLeft7C = CheckForDBNullUPS(dt, "tag_left7C");

                        TagRight1C = CheckForDBNullUPS(dt, "tag_right1C");
                        TagRight2C = CheckForDBNullUPS(dt, "tag_right2C");
                        TagRight3C = CheckForDBNullUPS(dt, "tag_right3C");
                        TagRight4C = CheckForDBNullUPS(dt, "tag_right4C");
                        TagRight5C = CheckForDBNullUPS(dt, "tag_right5C");
                        TagRight6C = CheckForDBNullUPS(dt, "tag_right6C");
                        TagRight7C = CheckForDBNullUPS(dt, "tag_right7C");

                        TagLeft1D = CheckForDBNullUPS(dt, "tag_left1D");
                        TagLeft2D = CheckForDBNullUPS(dt, "tag_left2D");
                        TagLeft3D = CheckForDBNullUPS(dt, "tag_left3D");
                        TagLeft4D = CheckForDBNullUPS(dt, "tag_left4D");
                        TagLeft5D = CheckForDBNullUPS(dt, "tag_left5D");
                        TagLeft6D = CheckForDBNullUPS(dt, "tag_left6D");
                        TagLeft7D = CheckForDBNullUPS(dt, "tag_left7D");

                        TagRight1D = CheckForDBNullUPS(dt, "tag_right1D");
                        TagRight2D = CheckForDBNullUPS(dt, "tag_right2D");
                        TagRight3D = CheckForDBNullUPS(dt, "tag_right3D");
                        TagRight4D = CheckForDBNullUPS(dt, "tag_right4D");
                        TagRight5D = CheckForDBNullUPS(dt, "tag_right5D");
                        TagRight6D = CheckForDBNullUPS(dt, "tag_right6D");
                        TagRight7D = CheckForDBNullUPS(dt, "tag_right7D");

                        TagLeft1E = CheckForDBNullUPS(dt, "tag_left1E");
                        TagLeft2E = CheckForDBNullUPS(dt, "tag_left2E");
                        TagLeft3E = CheckForDBNullUPS(dt, "tag_left3E");
                        TagLeft4E = CheckForDBNullUPS(dt, "tag_left4E");
                        TagLeft5E = CheckForDBNullUPS(dt, "tag_left5E");
                        TagLeft6E = CheckForDBNullUPS(dt, "tag_left6E");
                        TagLeft7E = CheckForDBNullUPS(dt, "tag_left7E");

                        TagRight1E = CheckForDBNullUPS(dt, "tag_right1E");
                        TagRight2E = CheckForDBNullUPS(dt, "tag_right2E");
                        TagRight3E = CheckForDBNullUPS(dt, "tag_right3E");
                        TagRight4E = CheckForDBNullUPS(dt, "tag_right4E");
                        TagRight5E = CheckForDBNullUPS(dt, "tag_right5E");
                        TagRight6E = CheckForDBNullUPS(dt, "tag_right6E");
                        TagRight7E = CheckForDBNullUPS(dt, "tag_right7E");
                    }//tag_place,tag_text,tag_place2,tag_text2,tag_place3,tag_text3,tag_place4,tag_text4

                }

                tag1 = GetTagFieldValue(drStyle, TagLeft1, TagLeft1A, TagLeft1B, TagLeft1C, TagLeft1D, TagLeft1E);
                tag2 = GetTagFieldValue(drStyle, TagLeft2, TagLeft2A, TagLeft2B, TagLeft2C, TagLeft2D, TagLeft2E);
                tag3 = GetTagFieldValue(drStyle, TagLeft3, TagLeft3A, TagLeft3B, TagLeft3C, TagLeft3D, TagLeft3E);
                tag4 = GetTagFieldValue(drStyle, TagLeft4, TagLeft4A, TagLeft4B, TagLeft4C, TagLeft4D, TagLeft4E);
                tag5 = GetTagFieldValue(drStyle, TagLeft5, TagLeft5A, TagLeft5B, TagLeft5C, TagLeft5D, TagLeft5E);
                tag6 = GetTagFieldValue(drStyle, TagLeft6, TagLeft6A, TagLeft6B, TagLeft6C, TagLeft6D, TagLeft6E);
                tag7 = GetTagFieldValue(drStyle, TagLeft7, TagLeft7A, TagLeft7B, TagLeft7C, TagLeft7D, TagLeft7E);

                TagRight1Line = GetTagFieldValue(drStyle, TagRight1, TagRight1A, TagRight1B, TagRight1C, TagRight1D, TagRight1E);
                TagRight2Line = GetTagFieldValue(drStyle, TagRight2, TagRight2A, TagRight2B, TagRight2C, TagRight2D, TagRight2E);
                TagRight3Line = GetTagFieldValue(drStyle, TagRight3, TagRight3A, TagRight3B, TagRight3C, TagRight3D, TagRight3E);
                TagRight4Line = GetTagFieldValue(drStyle, TagRight4, TagRight4A, TagRight4B, TagRight4C, TagRight4D, TagRight4E);
                TagRight5Line = GetTagFieldValue(drStyle, TagRight5, TagRight5A, TagRight5B, TagRight5C, TagRight5D, TagRight5E);
                TagRight6Line = GetTagFieldValue(drStyle, TagRight6, TagRight6A, TagRight6B, TagRight6C, TagRight6D, TagRight6E);
                TagRight7Line = GetTagFieldValue(drStyle, TagRight7, TagRight7A, TagRight7B, TagRight7C, TagRight7D, TagRight7E);


                return PrintTags(tag1, tag2, tag3, tag4, tag5, drStyle["IS_DWT_POT"] == DBNull.Value ? false : Convert.ToBoolean(drStyle["IS_DWT_POT"]),
                    drStyle["IS_CWT_POT"] == DBNull.Value ? false : Convert.ToBoolean(drStyle["IS_CWT_POT"]),
                    diamondinfo, colorinfo, cstyle, dPrice, drStyle["barcode"].ToString(), nooftags, is_rfid, show_code,
                    NoPriceOnTag, TagRight4Line: TagRight4Line, TagRight5Line: TagRight5Line, tag6: tag6, tag7: tag7, TagRight6Line: TagRight6Line, TagRight7Line: TagRight7Line,
                    TagRight1Line: TagRight1Line, TagRight2Line: TagRight2Line, TagRight3Line: TagRight3Line);
            }
            else
                throw new Exception("Invalid Style #" + style.Trim());
        }

        private string Showdia()
        {
            return (is_Moi || is_AlexH) ? "" : (is_Exchange ? "DCTW:" : "D:");
        }

        private string ShowCol()
        {
            return (is_AlexH) ? "" : "C:";
        }
        public string Get_CostCode(double cost)
        {
            string cost_code = string.Empty;
            char[] arrcost = ((int)cost).ToString().ToCharArray();

            if (cost > 0 && !string.IsNullOrWhiteSpace(Cost_Code))
            {
                char[] arrCostCode = Cost_Code.ToCharArray();

                char lastchar = ' ';
                foreach (char chr in arrcost)
                {
                    if (lastchar == chr && CostCodeDefaultChar.Trim() != "")
                    {
                        cost_code += CostCodeDefaultChar;
                        lastchar = ' ';
                    }
                    else
                    {
                        cost_code += arrCostCode[Convert.ToInt32(chr.ToString())];
                        lastchar = chr;
                    }
                }
            }
            if (is_Briony)       // Briony wants 2nd 0 to be Z,...
                cost_code = cost_code.Replace("XXXX", "XZAA").Replace("XXX", "XZA").Replace("XX", "XZ");
            return cost_code;
        }
        public void GetDollerandNotDecimalValueForTag(out string StPrice, decimal price = 0)
        {
            if (is_Malakov)
            {
                StPrice = string.Format("{0:C0}", price).Replace("$", "P#").Replace(",", "").Replace(".", "");
                return;
            }
            string StrPrice = string.Format(FormatSetforAmount(), Convert.ToDecimal(price));
            DataTable dt = string.IsNullOrEmpty(tagTemplateName) ? GetUpsIns1Details() : GetAllFromTagTemplate(tagTemplateName);

            bool DollerNotshow = !string.IsNullOrEmpty(GetValue(dt, "IgnoreDollerforprice")) ? Convert.ToBoolean(GetValue(dt, "IgnoreDollerforprice")) : false;
            bool WithDecimalValue = !string.IsNullOrEmpty(GetValue(dt, "ignoredecimals")) ? Convert.ToBoolean(GetValue(dt, "ignoredecimals")) : false;
            if (StrPrice == "0.00")
            {
                StPrice = StrPrice;
                return;
            }
            StPrice = DollerNotshow ? Convert.ToString(StrPrice) : Convert.ToString("$" + StrPrice);

            if (WithDecimalValue && price.ToString().Length == StrPrice.Replace(",", "").Length)
                StPrice = StPrice.Substring(0, StPrice.Length - 3);
        }
        public string FormatSetforAmount(string DollerFormat1 = "")
        {
            if (DollerFormat1 == "")
                return "{0:N2}";
            if (DollerFormat1 == "c")
                return "{0:c}";
            return "{0:$#,##0.00}";
        }

        public string GetTagFieldValue(DataRow drStyle, string Left, string LeftA, string LeftB, string LeftC = "", string LeftD = "", string LeftE = "")
        {
            Left = Tag_Value(Left, drStyle);
            LeftA = Tag_Value(LeftA, drStyle);
            LeftB = Tag_Value(LeftB, drStyle);
            if (!string.IsNullOrEmpty(LeftC))
                LeftC = Tag_Value(LeftC, drStyle);
            if (!string.IsNullOrEmpty(LeftD))
                LeftD = Tag_Value(LeftD, drStyle);
            if (!string.IsNullOrEmpty(LeftE))
                LeftE = Tag_Value(LeftE, drStyle);
            string Tag_reurn = string.Format("{0} {1} {2} {3} {4} {5} ", Left, LeftA, LeftB, LeftC, LeftD, LeftE).Trim();
            return Tag_reurn;
        }
        private string Tag_Value(string Left, DataRow drStyle)
        {
            if (tagTemplateName != "") /// this all from tag template
            {
                if (Left == "_PLACE")
                    return TemTag_text;
                if (Left == "_PLACE2")
                    return TemTag_text2;
                if (Left == "_PLACE3")
                    return TemTag_text3;
                if (Left == "_PLACE4")
                    return TemTag_text4;
            }

            if (Left == "_PLACE") //_PLACE is from 'Tag_Place' fld in 'ups_ins' & ups_ins1
                return Tag_text;
            if (Left == "_PLACE2")
                return Tag_text2;
            if (Left == "_PLACE3")
                return Tag_text3;
            if (Left == "_PLACE4")
                return Tag_text4;

            if (Left.ToUpper() == "CASE_NO" && CheckForDBNull(drStyle["CASE_NO"]).ToUpper() == "MULTI")
            {
                DataRow drMultiCase = GetSqlRow("SELECT TOP 1 * FROM STYLES_CASE with (nolock) WHERE STYLE = @STYLE AND STORE = @STORECODE", "@STYLE", CheckForDBNull(drStyle["STYLE"]), "@STORECODE", string.IsNullOrWhiteSpace(selectedStore) ? StoreCodeInUse1 : selectedStore);
                if (drMultiCase != null)
                    Left = CheckForDBNull(drMultiCase["CASE"].ToString());
                return Left;
            }
            if (Left.ToUpper() == "STONERETAILPRICE" || Left.ToUpper() == "ITEMRETAILPRICE")
                return Tag_Price(Left, drStyle);
            //if (Left.ToUpper() == "DESC" && is_Exchange)
            //    return Left(CheckForDBNull(drStyle["DESC"], "System.String"), 13);
            if (Left == "GOLD_WT")
                return Tag_Gold(drStyle);
            if (Left == "DATE")
            {
                if (is_Exchange)
                {
                    DateTime cdate = CheckForDBNull(drStyle["DATE"], "System.DateTime");
                    return cdate.Month.ToString() + "/" + cdate.Year.ToString();
                }
                return CheckForDBNull(drStyle["DATE"], "System.DateTime").ToString("MM/dd/yyyy");
            }
            if (CheckModuleEnabled(Modules.Ind_Dollar) && Left == "PRICE")
                return Ind_Price(drStyle);
            return Tag_Wt(Left, drStyle);
        }

        private string Tag_Price(string Left, DataRow drStyle)
        {
            decimal STONE_MULT = CheckForDBNull(drStyle["STONE_MULT"], "System.Decimal");
            decimal GEM_COST = CheckForDBNull(drStyle["GEM_COST"], "System.Decimal");
            decimal T_COST = CheckForDBNull(drStyle["T_COST"], "System.Decimal");
            decimal MULTI = CheckForDBNull(drStyle["MULTI"], "System.Decimal");
            if (Left.ToUpper() == "STONERETAILPRICE")
                Left = Math.Round(STONE_MULT * GEM_COST, 2).ToString();
            else if (Left.ToUpper() == "ITEMRETAILPRICE")
                Left = Math.Round((T_COST - GEM_COST) * MULTI, 2).ToString();
            return Left;
        }
        private string Tag_Gold(DataRow drStyle)
        {
            return Convert.ToString(CheckForDBNull(drStyle["GOLD_WT"], "System.Decimal")) + ((CheckForDBNull(drStyle["IS_DWT"], "System.Boolean")) ? " DWT" : " GR");
        }
        private string Ind_Price(DataRow drStyle)
        {
            decimal currencyRate = 0;
            DataRow drCurrencies = GetSqlRow("SELECT TOP 1 * FROM CURRENCIES with (nolock) ORDER BY DATE DESC");
            if (drCurrencies != null)
                currencyRate = CheckForDBNull(drCurrencies["RATE"], "System.Decimal");
            return CheckForDBNull(drStyle["price"] != DBNull.Value ? Math.Round((Convert.ToDecimal(drStyle["PRICE"]) * Convert.ToDecimal(currencyRate)), 2) : 0);
        }
        private string Tag_Wt(string Ileft, DataRow drStyle)
        {
            Ileft = !string.IsNullOrWhiteSpace(Ileft) &&
                !string.IsNullOrWhiteSpace(CheckForDBNull(drStyle[Ileft])) ?
                string.Format("{0}{1}",
                Ileft.ToUpper() == "STONE_WT" ? Showdia() : Ileft.ToUpper() == "COLOR_WT" ?
                ShowCol() : string.Empty, drStyle[Ileft].GetType() == typeof(decimal) ? string.Format("{0:N2}", Convert.ToDecimal(CheckForDBNull(drStyle[Ileft]))) : CheckForDBNull(drStyle[Ileft])) : string.Empty;
            if ((Ileft == Showdia() + "0.00" || Ileft == "C:0.00"))
                Ileft = "";
            return Ileft;
        }
        public bool IsNullOrWhiteSpace(string value)
        {
            if ((object)value == null)
            {
                return true;
            }

            for (int i = 0; i < value.Length; i++)
            {
                if (!char.IsWhiteSpace(value[i]))
                {
                    return false;
                }
            }

            return true;
        }
        public object PrintTags(string tag1, string tag2, string tag3, string tag4, string tag5, bool isdwt_pot,
     bool iscwt_pot, string diamondinfo, string colorinfo, string style, decimal price, string barcode,
     int nooftags, bool is_rfid, int show_code = 0, bool noPrice = false, string TagRight4Line = "", string TagRight5Line = "",
     string tag6 = "", string tag7 = "", string TagRight6Line = "", string TagRight7Line = "", string TagRight1Line = "", string TagRight2Line = "", string TagRight3Line = "")
        {
            string strTagPrinter = string.Empty;
            GetDefaultValuesTag();
            if (_httpContextAccessor.HttpContext?.Session.GetString("COMPANYNAME") != null)
            {
                CompanyName = _httpContextAccessor.HttpContext?.Session.GetString("COMPANYNAME").ToString();
            }
            StringBuilder printtag = new StringBuilder();
            CheckZebraCitohTscGodex();
            int ctr = 0;
            string _GetBarcode = "";
            DataRow drow;
            if (string.IsNullOrEmpty(tagTemplateName))
                drow = GetDataFromTbl("Ups_Ins");
            else
                drow = GetDatafromtblGivenvalue("tag_template", "TEMPLATENAME", tagTemplateName);
            bool Move_barcode = Convert.ToBoolean(!string.IsNullOrEmpty(GetValue(drow, "MOVE_BARCODE")) ? Convert.ToBoolean(GetValue(drow, "MOVE_BARCODE")) : Convert.ToBoolean(false));
            DataTable dt = GetAllFromTagTemplate(tagTemplateName);
            DataTable dtups = getupsinsinformation();
            DataTable dtups1 = GetUpsIns1Details();
            int ColumCount = string.IsNullOrEmpty(tagTemplateName) ? dtups.Columns.Count : dt.Columns.Count;

            foreach (DataRow drs in string.IsNullOrEmpty(tagTemplateName) ? dtups.Rows : dt.Rows)
            {
                for (int i = 0; i < ColumCount; i++)
                {
                    string searchvalue = "BARCODE";
                    string Getvalue = drs[i].ToString();
                    if (searchvalue.ToUpper() == Getvalue.ToUpper())
                        _GetBarcode = string.IsNullOrEmpty(tagTemplateName) ? dtups.Columns[i].ColumnName.ToString() : dt.Columns[i].ColumnName.ToString();
                }
            }
            if (string.IsNullOrEmpty(_GetBarcode) && string.IsNullOrEmpty(tagTemplateName) && DataTableOK(dtups1))
            {
                foreach (DataRow drs in dtups1.Rows)
                {
                    for (int i = 0; i < dtups1.Columns.Count; i++)
                    {
                        string searchvalue = "BARCODE";
                        string Getvalue = drs[i].ToString();
                        if (searchvalue.ToUpper() == Getvalue.ToUpper())
                            _GetBarcode = dtups1.Columns[i].ColumnName.ToString();
                    }
                }
            }
            if (is_rfid)
            {
                decimal ccol2 = RfidPrintRight, ctop = RfidPrintTop, cinc = RfidPrintCinc;
                if (Zebra_RFID)
                    cinc = -cinc;
                printtag.AppendLine();
                if (Zebra_RFID)
                {
                    printtag.Append("^XA");
                    printtag.AppendLine();
                    printtag.Append("^MD10");
                }
                else if (SmallRFID)
                {
                    printtag.Append("CB");
                    printtag.AppendLine();
                    printtag.Append("SS3");
                    printtag.AppendLine();
                    printtag.Append("SD20");
                    printtag.AppendLine();
                    printtag.Append("SW850");
                    printtag.AppendLine();
                    printtag.Append("SOT");
                }
                else
                    printtag.Append("{cmF,1,A,R,E,175,387,\"SAMPLE\"|");
                printtag.AppendLine();
                printtag.Append(say_rfid(ctop, ccol2 + 4, style, style.Length <= 9 ? 9 : style.Length <= 1 ? 8 : 6));
                printtag.AppendLine();
                int i = Zebra_RFID ? 3 : 4;
                if (TagMultiplyer > 0 && price != 0 && !noPrice)
                {
                    printtag.Append(say_rfid(ctop - cinc * i, ccol2 + 4, string.Format("{0:C0}", price), 9));
                    printtag.AppendLine();
                }
                if (Zebra_RFID)
                {
                    add_barcode(printtag, ctop - cinc, ccol2 + 10, barcode, true);
                    i++;
                }
                else if (SmallRFID)
                    add_barcode(printtag, ctop - 22, ccol2 + 10, barcode, true);
                else
                    add_barcode(printtag, ctop - 22, ccol2, barcode, true);

                i++;

                string cRfidTagInfo;
                for (int ni = 4; ni < 8; ni++)
                {
                    cRfidTagInfo = ni == 4 ? TagRight4Line : ni == 5 ? TagRight5Line : ni == 6 ? TagRight6Line : ni == 7 ? TagRight7Line : "";
                    if (!string.IsNullOrWhiteSpace(cRfidTagInfo))
                    {
                        printtag.Append(say_rfid(ctop - cinc * i, ccol2 + 4, cRfidTagInfo, 6));
                        printtag.AppendLine();
                    }
                }

                decimal ccol = RfidPrintLeft;
                ctop -= 2;
                ctr = 1;

                for (int ni = 1; ni < 8; ni++)
                {
                    cRfidTagInfo = ni == 1 ? tag1 : ni == 2 ? tag2 : ni == 3 ? tag3 : ni == 4 ? tag4 : ni == 5 ? tag5 : ni == 6 ? tag6 : tag7;
                    if (!string.IsNullOrWhiteSpace(cRfidTagInfo))
                    {
                        printtag.Append(say_rfid(ctop - (ctr - 1) * cinc, ccol, cRfidTagInfo, 6));
                        printtag.AppendLine();
                        ctr++;
                    }
                }
                string epc = GetNextEPC(style);
                if (Zebra_RFID)
                {
                    printtag.Append("^RC^FD" + epc + "^FS");
                    printtag.AppendLine();
                    printtag.Append("^XZ");
                    printtag.AppendLine();
                }
                else if (SmallRFID)
                {
                    printtag.Append(say_epc(epc));
                    printtag.AppendLine();
                    printtag.Append("P1");
                    printtag.AppendLine();
                }
                else
                {
                    printtag.Append(say_epc(epc));
                    printtag.Append("}");
                    printtag.AppendLine();
                    printtag.AppendLine();
                    printtag.Append("{cmB,1,N,1|E,0,0,1,1,0,1|}");
                }
            }
            else
            {
                bool skip_blanks = is_oldtown || CheckForDBNull(CompanyName).ToUpper().Contains("TIGRAN");
                decimal tagprinterleft, tagprinterright, tagprintertop, tagprintercinc, tagprinterlength;

                SetPrinterMargins(out tagprinterleft, out tagprinterright, out tagprintertop, out tagprintercinc, out TagReportTopR, false, out tagprinterlength);

                Tag_Start(printtag, tagprinterlength);

                string cTagInfo;
                string _MovebarcodeValue = "";
                for (int ni = 1; ni < 8; ni++)
                {
                    cTagInfo = ni == 1 ? tag1 : ni == 2 ? tag2 : ni == 3 ? tag3 : ni == 4 ? tag4 : ni == 5 ? tag5 : ni == 6 ? tag6 : tag7;
                    if (Move_barcode)
                    {
                        _MovebarcodeValue = _GetBarcode.ToLower() == "tag_left1" ? tag1 : _GetBarcode.ToLower() == "tag_left2" ? tag2 : _GetBarcode.ToLower() == "tag_left3" ? tag3 : _GetBarcode.ToLower() == "tag_left4" ? tag4 : _GetBarcode.ToLower() == "tag_left5" ? tag5 : _GetBarcode.ToLower() == "tag_left6" ? tag6 : _GetBarcode.ToLower() == "tag_left7" ? tag7 : "";
                        if (_MovebarcodeValue != cTagInfo)
                            _MovebarcodeValue = "";
                    }
                    if (!string.IsNullOrWhiteSpace(cTagInfo))
                    {
                        if (isFao && ni == 4)
                            add_tag(printtag, tagprinterleft, TagReportTopR + tagprintercinc * 3, cTagInfo);
                        else if (!string.IsNullOrEmpty(_MovebarcodeValue) && Move_barcode)
                            add_barcode(printtag, tagprinterleft, TagReportTopR + tagprintercinc * ctr, _MovebarcodeValue, is_rfid);
                        else
                            add_tag(printtag, tagprinterleft, TagReportTopR + tagprintercinc * ctr, cTagInfo, true);
                        ctr++;
                    }
                    else if (skip_blanks)
                        ctr++;

                }
                int row_no = 0;
                string cstyle = style;
                if (is_Tigran)
                    cstyle = Left(cstyle, style.Length - 6); // do not print SN
                if (Move_barcode)
                {
                    string Mbarcode = _GetBarcode.ToUpper() == "TAG_RIGHT1" ? TagRight1Line : "";
                    if (TagRight1Line == Mbarcode && !string.IsNullOrEmpty(Mbarcode))
                        add_barcode(printtag, tagprinterright, tagprintertop + tagprintercinc * row_no++, Mbarcode, is_rfid);
                    else if (!string.IsNullOrEmpty(TagRight1Line))
                        add_tag(printtag, tagprinterright, tagprintertop + tagprintercinc * row_no++, TagRight1Line, true);
                }
                else
                    add_tag(printtag, tagprinterright, tagprintertop, cstyle + " " + TagRight1Line, true);


                if (Move_barcode)
                {
                    string Mbarcode = _GetBarcode.ToUpper() == "TAG_RIGHT2" ? TagRight2Line : "";

                    if (TagRight2Line == Mbarcode && !string.IsNullOrEmpty(Mbarcode))
                        add_barcode(printtag, tagprinterright, tagprintertop + tagprintercinc * row_no++, Mbarcode, is_rfid);
                    else if (!string.IsNullOrEmpty(TagRight2Line))
                        add_tag(printtag, tagprinterright, tagprintertop + tagprintercinc * row_no++, TagRight2Line, true);
                }
                row_no = is_Malakov ? 1 : 2;
                if (show_code == 1)
                {
                    if (Move_barcode)
                    {
                        string Mbarcode = _GetBarcode.ToUpper() == "TAG_RIGHT4" ? TagRight4Line : "";

                        if (TagRight4Line == Mbarcode && !string.IsNullOrEmpty(Mbarcode))
                            add_barcode(printtag, tagprinterright, tagprintertop + tagprintercinc * row_no++, Mbarcode, is_rfid);
                        else
                            add_tag(printtag, tagprinterright, tagprintertop + tagprintercinc * row_no++, TagRight4Line);
                    }
                    else
                        add_tag(printtag, tagprinterright, tagprintertop + tagprintercinc * row_no++, TagRight4Line);
                }
                else if (CheckModuleEnabled(Modules.Ind_Dollar))
                {
                    if (price != 0)
                    {
                        add_tag(printtag, tagprinterright, tagprintertop + tagprintercinc * row_no++,
                            string.Format("{0:n}", price) + " " + TagRight3Line, true);
                    }
                }
                else if (TagMultiplyer > 0 && price != 0 && !noPrice)
                {
                    string price1;
                    GetDollerandNotDecimalValueForTag(out price1, price);
                    if (Move_barcode)
                    {
                        string Mbarcode = _GetBarcode.ToUpper() == "TAG_RIGHT3" ? TagRight3Line : "";

                        if (TagRight3Line == Mbarcode && !string.IsNullOrEmpty(Mbarcode))
                            add_barcode(printtag, tagprinterright, tagprintertop + tagprintercinc * row_no++, Mbarcode, is_rfid);
                        else if (!string.IsNullOrEmpty(TagRight3Line))
                            add_tag(printtag, tagprinterright, tagprintertop + tagprintercinc * row_no++, TagRight3Line, true);
                    }
                    else
                        add_tag(printtag, tagprinterright, tagprintertop + tagprintercinc * row_no++,
                            price1 + " " + TagRight3Line, true);
                }
                else if (Convert.ToString(price) != TagRight3Line)
                {
                    if (!string.IsNullOrEmpty(TagRight3Line))
                        add_tag(printtag, tagprinterright, tagprintertop + tagprintercinc * row_no++, TagRight3Line, true);
                }
                else if (is_Moi)    // For Moi skip line even if no price
                    row_no++;
                if (_httpContextAccessor.HttpContext?.Session.GetString("COMPANYNAME") != null)
                {
                    CompanyName = _httpContextAccessor.HttpContext?.Session.GetString("COMPANYNAME").ToString();
                }
                if (CompanyName.ToUpper().Contains("DIAMONDS & DESIGN"))
                {
                    tag5 = string.Format("{0:C0}", price * 1.8m).Replace(",", "").Replace("$", "FLS");
                    add_tag(printtag, tagprinterright, tagprintertop + tagprintercinc * row_no, tag5, true);
                    row_no++;
                }

                if (show_code != 1 && TagRight4Line != "" && TagRight4Line != "null")
                {
                    if (Move_barcode)
                    {
                        string Mbarcode = _GetBarcode.ToUpper() == "TAG_RIGHT4" ? TagRight4Line : "";

                        if (TagRight4Line == Mbarcode && !string.IsNullOrEmpty(Mbarcode))
                            add_barcode(printtag, tagprinterright, tagprintertop + tagprintercinc * row_no++, Mbarcode, is_rfid);
                        else
                            add_tag(printtag, tagprinterright, tagprintertop + tagprintercinc * row_no++, TagRight4Line);
                    }
                    else
                        add_tag(printtag, tagprinterright, tagprintertop + tagprintercinc * row_no++, TagRight4Line);
                }

                string _MobarcodeValue = "";
                for (int ni = 5; ni < 8; ni++)
                {
                    cTagInfo = ni == 5 ? TagRight5Line : ni == 6 ? TagRight6Line : ni == 7 ? TagRight7Line : "";
                    if (Move_barcode)
                    {
                        _MobarcodeValue = _GetBarcode.ToUpper() == "TAG_RIGHT5" ? TagRight5Line : _GetBarcode.ToUpper() == "TAG_RIGHT6" ? TagRight6Line : _GetBarcode.ToUpper() == "TAG_RIGHT7" ? TagRight7Line : "";
                        if (_MobarcodeValue.Trim() != cTagInfo.Trim())
                            _MobarcodeValue = "";
                    }
                    if (!string.IsNullOrWhiteSpace(cTagInfo))
                    {
                        if (!string.IsNullOrEmpty(_MobarcodeValue) && Move_barcode)
                            add_barcode(printtag, tagprinterright, tagprintertop + tagprintercinc * row_no++, _MobarcodeValue, is_rfid);
                        else
                            add_tag(printtag, tagprinterright, tagprintertop + tagprintercinc * row_no++, cTagInfo);
                    }
                }
                if (!is_WatchDealer && !Move_barcode)
                    add_barcode(printtag, tagprinterright, tagprintertop + tagprintercinc * (is_Malakov ? 4 : 1), barcode, is_rfid);
                Tag_End(printtag);
            }


            // Instead of saving file, just return the built tag content
            string tagContent = "";
            for (int i = 1; i <= nooftags; i++)
            {
                tagContent += printtag.ToString() + Environment.NewLine;
            }

            return new PrintTagResult
            {
                Success = true,
                Message = $"{nooftags} Tag(s) generated successfully.",
                FileContent = tagContent
            };

        }

        public DataRow GetDataFromTbl(string tblname)
        {
            return GetSqlRow(@"SELECT * FROM " + tblname);
        }

        public DataTable getupsinsinformation()
        {
            return GetSqlData("select * from ups_ins");
        }

        private string say_rfid(decimal x, decimal y, string txt, int fieldsize = 7)
        {
            if (string.IsNullOrWhiteSpace(txt))
                return "";
            if (Zebra_RFID)
                return say_zebra(y, x, txt, true);
            if (SmallRFID)
                return string.Format("T{1},{0},1,1,1,0,2,N,N,'{3}'", Decimal.ToInt32(x).ToString().Trim(), Decimal.ToInt32(y).ToString().Trim(), fieldsize, txt.Replace(@"""", @"~034"));
            return string.Format("C,{0},{1},0,50,{2},{2},B,L,0,0,\"{3}\"|", Decimal.ToInt32(x).ToString().Trim(), Decimal.ToInt32(y).ToString().Trim(), fieldsize, txt.Replace(@"""", @"~034"));
        }
        private void add_barcode(StringBuilder printtag, decimal x, decimal y, string barcode, bool is_rfid)
        {
            printtag.Append(say_barcode(x, y, barcode, is_rfid));
            printtag.AppendLine();
        }
        private string say_barcode(decimal x, decimal y, string barcode, bool is_rfid)
        {
            if (string.IsNullOrWhiteSpace(barcode))
                return "";
            if (is_rfid)
            {
                if (Zebra_RFID)
                    return string.Format("^FO{0},{1}^BY1^BCN,50,N,N,N^FD{2}^FS", Decimal.ToInt32(y).ToString().Trim(), Decimal.ToInt32(x).ToString().Trim(), barcode);
                if (SmallRFID)
                    return string.Format("B1{1},{0},1,2,2,40,2,0,'{2}'",
                        Decimal.ToInt32(x).ToString().Trim(), Decimal.ToInt32(y).ToString().Trim(),
                        barcode);
                return string.Format("B,2,{0},F,{1},{2},3,13,16,0,L,0|\n", barcode.Length.ToString(),
                    Decimal.ToInt32(x).ToString().Trim(), Decimal.ToInt32(y).ToString().Trim()) +
                    string.Format("R,1,\"{0}\"|", barcode);
            }
            if (isZebra)
                return string.Format("^FO{0},{1}^BY1^BCN,15,N,N,N^FD>;{2}^FS", Decimal.ToInt32(x).ToString().Trim(), Decimal.ToInt32(y).ToString().Trim(), barcode);
            if (isCitoh)
                return string.Format("{0}{1}{2}B{3}", "1e11012", Decimal.ToInt32(y).ToString().Trim().PadLeft(4, '0'), Decimal.ToInt32(x).ToString().Trim().PadLeft(4, '0'), barcode);
            if (isGodex)
                return string.Format("BN,{0},{1},2,5,16,0,0,{2}", Decimal.ToInt32(x + 10).ToString().Trim(), Decimal.ToInt32(y).ToString().Trim(), barcode);
            if (isTsc)
            {
                if (is_Ram)
                    return string.Format("BARCODE {0},{1},\"25\",20,0,0,3,7,\"{2}\"", Decimal.ToInt32(x - 150).ToString().Trim(), Decimal.ToInt32(y).ToString().Trim(), barcode.Trim().PadLeft(6, '0'));
                return string.Format("BARCODE {0},{1},\"25\",20,0,0,3,7,\"{2}\"", Decimal.ToInt32(x).ToString().Trim(), Decimal.ToInt32(y).ToString().Trim(), barcode.Trim().PadLeft(6, '0'));
            }
            if (is_Malakov)
                return string.Format("B{0},{1},0,1,1,4,16,N,\"{2}\"", Decimal.ToInt32(x).ToString().Trim(), Decimal.ToInt32(y).ToString().Trim(), barcode.Trim().PadLeft(6, '0'));
            return string.Format("B{0},{1},0,1,2,5,16,N,\"{2}\"", Decimal.ToInt32(x).ToString().Trim(), Decimal.ToInt32(y).ToString().Trim(), barcode.Trim().PadLeft(6, '0'));
        }
        private string GetNextEPC(string style)
        {
            using (SqlConnection conn = _connectionProvider.GetConnection())
            using (SqlCommand dbCommand = new SqlCommand("GetNextEPC", conn))
            {
                dbCommand.CommandType = CommandType.StoredProcedure;

                // Add input parameter
                dbCommand.Parameters.AddWithValue("@style", style);

                // Add output parameter
                SqlParameter outNextEPC = new SqlParameter("@RETVAL", SqlDbType.NVarChar, 30)
                {
                    Direction = ParameterDirection.Output
                };
                dbCommand.Parameters.Add(outNextEPC);

                // Open connection and execute
                conn.Open();
                dbCommand.ExecuteNonQuery();

                // Return the output parameter value
                return outNextEPC.Value as string ?? string.Empty;
            }
        }
        private string say_epc(string txt)
        {
            if (string.IsNullOrWhiteSpace(txt))
                return "";
            if (SmallRFID)
                return string.Format(">RFLP,U,'08,00,00'\n>RFW,H,4,12,'" + txt + "'\n>RFLP,L,'A8,2A,00'");
            return string.Format("X,1,24,0|") + "\n" +
                string.Format("R,1,\"{0}\"|", txt.Replace(@"""", @"\"""));
        }

        public void EndPrintTag()
        {
            StringBuilder printtag = new StringBuilder();
            Tag_End(printtag);
            WriteTag(1, printtag);

        }
        public void CalibrateTagPrinter()
        {
            bool is_zebra = CheckZebra();
            string calibrationtext;
            if (is_zebra)
            {
                calibrationtext = @"
                                    ^XA
                                    ~JC
                                    ^XZ
                                    ";
            }
            else
            {
                calibrationtext = @"
                                    N
                                    Q230,25
                                    xa
                                    q710
                                    U
                                    ";
            }

            string filename = string.Format("tag_calibration_{0}.txt", (new Random()).Next(100000, 999999));
            string filePath = Path.Combine(Path.GetTempPath(), filename);
            using (StreamWriter sw = new StreamWriter(filePath))
            {
                sw.Flush();
                sw.Write(calibrationtext);
            }
            string strTagPrinter = GetPrinterPort();
            if (string.IsNullOrWhiteSpace(strTagPrinter))
            {
                Console.WriteLine("Printer Port not defined for Selected Printer.");
                return;
            }
            ExecuteCopyCommand(filePath, strTagPrinter);
        }

        public string GetPrinterPort(bool isLabel = false)
        {
            string tagPrinterPort = string.Empty;
            DataRow drTagPrinter;

            if (isLabel)
                drTagPrinter = GetSqlRow("SELECT * FROM TAG_PRINTER WITH (NOLOCK) WHERE ISLABEL = 1");
            else
                drTagPrinter = GetSqlRow("SELECT * FROM TAG_PRINTER WITH (NOLOCK) WHERE LTRIM(RTRIM(NAME)) = @NAME", "@NAME", WorkingPrinter.Trim());
            if (drTagPrinter != null && drTagPrinter.Table.Columns.Contains("PORT"))
                tagPrinterPort = CheckForDBNull(drTagPrinter["PORT"]);
            return tagPrinterPort;
        }

        public List<SelectListItem> fillPrinterItems()
        {
            List<SelectListItem> printerList = new List<SelectListItem>();

            DataTable dtPrinters = GetSqlData("SELECT NAME FROM TAG_PRINTER WITH (NOLOCK) WHERE ISLABEL <> 1 ORDER BY NAME");

            if (DataTableOK(dtPrinters))
            {
                foreach (DataRow row in dtPrinters.Rows)
                {
                    printerList.Add(new SelectListItem
                    {
                        Text = row["NAME"].ToString(),
                        Value = row["NAME"].ToString()
                    });
                }
            }

            return printerList;
        }

        public void deletePrinter(string name)
        {
            GetSqlData(@"DELETE TAG_PRINTER where name = @NAME", "@name", name);
        }
        public DataTable GetRepNoteSkus(string sku = "")
        {
            if (string.IsNullOrEmpty(sku))
                return GetSqlData("select [SKU] from REPAIR_NOTES WHERE SKU<>''");
            return GetSqlData($"select top 20 [SKU]  from REPAIR_NOTES WHERE SKU<>'' and sku like '{@sku.Replace("'", "''")}%'", "@sku", sku);
        }


        public string SaveRepairDetails(bool _isFromInv, OrderRepairModel repairorder, DataTable dtRepairitems, string invoicenumber, string optype = "")
        {
            using (SqlConnection conn = _connectionProvider.GetConnection())
            {
                conn.Open();
                SqlTransaction transaction = conn.BeginTransaction();  // Start transaction
                try
                {

                    if (_isFromInv)
                    {
                        for (int rows = 0; rows < dtRepairitems.Rows.Count; rows++)
                        {
                            string cItem = dtRepairitems.Rows[rows][0].ToString().Trim().PadRight(20, ' ');
                            if (string.IsNullOrWhiteSpace(dtRepairitems.Rows[rows][11].ToString()))
                                dtRepairitems.Rows[rows][12] = (CheckForDBNull(dtRepairitems.Rows[rows][12], typeof(decimal).ToString()) + 1);

                            using (SqlCommand dbCommand = new SqlCommand(@"INSERT INTO IN_SP_IT 
                            (INV_NO, [DESC], PRICE, QTY, LINE, RET_INV_NO, IS_TAX, REPAIR_NO, iSInventoryItem, INVSTYLE) 
                            VALUES (@INV_NO, @DESC, @PRICE, @QTY, @LINE, @RET_INV_NO, @IS_TAX, @REPAIR_NO, IIF(ISNULL(@STYLE, '') <> '', 1, 0), ISNULL(@STYLE, ''))", conn, transaction))
                            {
                                dbCommand.CommandType = CommandType.Text;
                                dbCommand.Parameters.Add(new SqlParameter("@INV_NO", SqlDbType.VarChar, 10) { Value = invoicenumber.PadLeft(10) });
                                dbCommand.Parameters.Add(new SqlParameter("@DESC", SqlDbType.VarChar, 400) { Value = cItem + CheckForDBNull(dtRepairitems.Rows[rows][6].ToString()) });
                                dbCommand.Parameters.Add(new SqlParameter("@PRICE", SqlDbType.Decimal)
                                {
                                    Value = _isFromInv ? "0" : ((DecimalCheckForNullDBNull(dtRepairitems.Rows[rows][8]) *
                                    CheckForDBNull(dtRepairitems.Rows[rows][9], typeof(decimal).ToString())).ToString())
                                });
                                dbCommand.Parameters.Add(new SqlParameter("@QTY", SqlDbType.Decimal) { Value = (DecimalCheckForNullDBNull(dtRepairitems.Rows[rows][8])).ToString() });
                                dbCommand.Parameters.Add(new SqlParameter("@LINE", SqlDbType.VarChar) { Value = dtRepairitems.Rows[rows][12].ToString() });
                                dbCommand.Parameters.Add(new SqlParameter("@RET_INV_NO", SqlDbType.VarChar, 20) { Value = "" });
                                dbCommand.Parameters.Add(new SqlParameter("@IS_TAX", SqlDbType.Bit) { Value = repairorder.IS_TAX });
                                dbCommand.Parameters.Add(new SqlParameter("@REPAIR_NO", SqlDbType.VarChar, 11) { Value = repairorder.REPAIR_NO.Trim() });
                                dbCommand.Parameters.Add(new SqlParameter("@STYLE", SqlDbType.VarChar, 30) { Value = Convert.ToString(dtRepairitems.Rows[rows][2]) });
                                int rowsAffected = dbCommand.ExecuteNonQuery();
                            }
                        }
                    }
                    using (var dbCommand = new SqlCommand("AddUpdateRepairOrder", conn, transaction))
                    {
                        dbCommand.CommandType = CommandType.StoredProcedure;

                        dbCommand.Parameters.AddWithValue("@OPTYPE", "NEW");
                        dbCommand.Parameters.AddWithValue("@Flg", "RpTms");
                        dbCommand.Parameters.AddWithValue("@person", repairorder.persons);
                        dbCommand.Parameters.AddWithValue("@operator", repairorder.OPERATOR);
                        dbCommand.Parameters.AddWithValue("@StockStyle", repairorder.StockStyle);
                        dbCommand.Parameters.AddWithValue("@iSRepairStock", repairorder.iSRepairStock);
                        dbCommand.Parameters.AddWithValue("@StoreNo", repairorder.STORE);

                        var parameter = new SqlParameter
                        {
                            ParameterName = "@RepairsItems",
                            SqlDbType = SqlDbType.Xml,
                            Value = repairorder.STRDATAXML
                        };
                        dbCommand.Parameters.Add(parameter);

                        int rowsAffected = dbCommand.ExecuteNonQuery();
                    }

                    using (var dbCommand = new SqlCommand("AddUpdateRepairOrder", conn, transaction))
                    {
                        dbCommand.CommandType = CommandType.StoredProcedure;
                        dbCommand.Parameters.Add("@OPTYPE", SqlDbType.VarChar, 10).Value = "NEW";
                        dbCommand.Parameters.Add("@Flg", SqlDbType.VarChar, 10).Value = "Or_tms";
                        dbCommand.Parameters.Add("@operator", SqlDbType.VarChar, 50).Value = repairorder.OPERATOR;
                        dbCommand.Parameters.Add("@person", SqlDbType.VarChar, 100).Value = repairorder.persons;
                        dbCommand.Parameters.Add("@StoreNo", SqlDbType.VarChar, 20).Value = repairorder.STORE;

                        // Add XML parameter explicitly
                        SqlParameter xmlParameter = new SqlParameter("@RepairsItems", SqlDbType.Xml)
                        {
                            Value = repairorder.STRDATAXML
                        };
                        dbCommand.Parameters.Add(xmlParameter);
                        var rowsAffected = dbCommand.ExecuteNonQuery();
                    }

                    using (SqlCommand dbCommand = new SqlCommand(@"INSERT INTO REPAIR(REPAIR_NO,ACC,CUS_REP_NO,CUS_DEB_NO,DATE,RCV_DATE,MESSAGE,MESSAGE1,OPERATOR,NAME,ADDR1,ADDR2,CITY,STATE ,ZIP,CAN_DATE,COUNTRY,SNH,VIA_UPS,IS_COD,COD_TYPE,EARLY,ISSUE_CRDT,ShipType,RESIDENT,ESTIMATE,TAXABLE,SALES_TAX,salesman1,salesman2,REGISTER,Jeweler_note,deduction,store,NO_TAXRESION,Warranty_Repair,COMISH1,COMISH2,COMISHAMOUNT1,COMISHAMOUNT2,Sales_Fee_Amount,Sales_Fee_Rate,repStatus,Who,rep_size,rep_metal,StockStyle,SETTER,EMAIL,warranty_inv_no,style,INV_NO,SHIP_BY,WEIGHT,INSURED,SURPRISE,Estimateready,LastRepairUpdate,iSWarrantyRepair) 
                                          VALUES 
                                         (CAST(@REPAIR_NO AS NVARCHAR(11)),CAST(@ACC AS NVARCHAR(15)),CAST(@CUS_REP_NO AS NVARCHAR(12)),CAST(@CUS_DEB_NO AS NVARCHAR(10)),@DATE,@RCV_DATE,CAST(@MESSAGE AS NVARCHAR(150)),CAST(@MESSAGE1 AS NVARCHAR(150)),CASt(@OPERATOR AS NVARCHAR(10)),CAST(@NAME AS NVARCHAR(60)),CASt(@ADDR1 AS NVARCHAR(300)),CAST(@ADDR2 AS NVARCHAR(300)),CAST(@CITY AS NVARCHAR(30)),CAST(@STATE AS NVARCHAR(10)),CAST(@ZIP AS NVARCHAR(10)),@CAN_DATE,CAST(@COUNTRY AS NVARCHAR(15)),CAST(@SNH AS DECIMAL(9,2)), CAST(@VIA_UPS AS NVARCHAR(1)), CAST(@IS_COD AS BIT),CAST(@COD_TYPE AS NVARCHAR(1)),CAST(@EARLY AS NVARCHAR(1)),CAST(@ISSUE_CRDT AS BIT),CAST(@ShipType AS INT), CAST(@RESIDENT AS NVARCHAR(1)),CAST(@ESTIMATE AS DECIMAL(12,2)), CAST(@TAXABLE AS BIT), CAST(@SALES_TAX AS DECIMAL(12,2)), CAST(@SALESMAN1 AS NVARCHAR(4)), CAST(@SALESMAN2 AS NVARCHAR(4)),CAST(@REGISTER AS NVARCHAR(20)),CAST(@Jeweler_note AS NVARCHAR(300)),CAST(@deduction AS DECIMAL(10,2)), CAST(@store AS NVARCHAR(50)),CAST(@NO_TAXRESION AS NVARCHAR(500)),CAST(@Warranty_Repair AS BIT),CASt(@COMISH1 AS DECIMAL(5,2)),CAST(@COMISH2 AS DECIMAL(5,2)),CASt(@COMISHAMOUNT1 AS DECIMAL(12,2)),CAST(@COMISHAMOUNT2 AS DECIMAL(12,2)),CAST(@SalesFeeAmount AS DECIMAL(12,2)),CASt(@SalesFeeRate AS DECIMAL(12,2)),CASt(@repStatus AS NVARCHAR(1)),CASt(@Who AS NVARCHAR(60)),CAST(@repSize AS NVARCHAR(10)) ,CAST(@repMetal AS NVARCHAR(10)),isnull(@StockStyle,''),iSNULL(@Setter,''),iSNULL(@Email,''),iSNULL(@warranty_inv_no,''),IIF(isnull(@warranty_inv_no,'')<>'',iSNULL(@warranty_style,''),iSNULL(@repair_style,'')),iSNULL(@INV_NO,''),isnull(@SHIP_BY,''),iSNull(@WEIGHT,0),iSNull(@INSURED,0), iSNULL(@SURPRISE,0),iSNULL(@iSEstRdy,0),GETDATE(),iSNULL(@iSWarrantyRepair,0))", conn, transaction))
                    {
                        dbCommand.Connection = conn;
                        dbCommand.CommandType = CommandType.Text;
                        dbCommand.Parameters.AddWithValue("@REPAIR_NO", repairorder.REPAIR_NO);
                        dbCommand.Parameters.AddWithValue("@ACC", repairorder.ACC);
                        dbCommand.Parameters.AddWithValue("@INV_NO", repairorder.INV_NO);
                        dbCommand.Parameters.AddWithValue("@CUS_REP_NO", repairorder.CUS_REP_NO);
                        dbCommand.Parameters.AddWithValue("@CUS_DEB_NO", repairorder.CUS_DEB_NO);
                        dbCommand.Parameters.AddWithValue("@DATE", repairorder.DATE);
                        dbCommand.Parameters.AddWithValue("@RCV_DATE", repairorder.RCV_DATE == null ? DBNull.Value : (object)repairorder.RCV_DATE);
                        dbCommand.Parameters.AddWithValue("@MESSAGE", !string.IsNullOrEmpty(repairorder._Warrnty_desc) ? repairorder._Warrnty_desc : repairorder.MESSAGE);
                        dbCommand.Parameters.AddWithValue("@MESSAGE1", repairorder.MESSAGE1);
                        dbCommand.Parameters.AddWithValue("@OPEN", 0);
                        dbCommand.Parameters.AddWithValue("@OPERATOR", repairorder.OPERATOR);
                        dbCommand.Parameters.AddWithValue("@NAME", repairorder.NAME);
                        dbCommand.Parameters.AddWithValue("@ADDR1", repairorder.ADDR1);
                        dbCommand.Parameters.AddWithValue("@ADDR2", repairorder.ADDR2);
                        dbCommand.Parameters.AddWithValue("@CITY", repairorder.CITY);
                        dbCommand.Parameters.AddWithValue("@STATE", repairorder.STATE);
                        dbCommand.Parameters.AddWithValue("@ZIP", repairorder.ZIP);
                        dbCommand.Parameters.AddWithValue("@CAN_DATE", repairorder.CAN_DATE);
                        dbCommand.Parameters.AddWithValue("@COUNTRY", repairorder.COUNTRY);
                        dbCommand.Parameters.AddWithValue("@SNH", repairorder.SNH);
                        dbCommand.Parameters.AddWithValue("@VIA_UPS", repairorder.SHIPED);
                        dbCommand.Parameters.AddWithValue("@IS_COD", repairorder.IS_COD);
                        dbCommand.Parameters.AddWithValue("@COD_TYPE", repairorder.COD_TYPE);
                        dbCommand.Parameters.AddWithValue("@EARLY", repairorder.EARLY);
                        dbCommand.Parameters.AddWithValue("@ISSUE_CRDT", repairorder.ISSUE_CRDT);
                        dbCommand.Parameters.AddWithValue("@ShipType", repairorder.SHIPTYPE);
                        dbCommand.Parameters.AddWithValue("@RESIDENT", 0);
                        dbCommand.Parameters.AddWithValue("@ESTIMATE", repairorder.ESTIMATE);
                        dbCommand.Parameters.AddWithValue("@TAXABLE", repairorder.TAXABLE);
                        dbCommand.Parameters.AddWithValue("@SALES_TAX", repairorder.SALES_TAX);
                        dbCommand.Parameters.AddWithValue("@SALESMAN1", repairorder.SALESMAN1);
                        dbCommand.Parameters.AddWithValue("@SALESMAN2", repairorder.SALESMAN2);
                        dbCommand.Parameters.AddWithValue("@REGISTER", repairorder.CASH_REGISTER);
                        dbCommand.Parameters.AddWithValue("@Jeweler_note", repairorder.Jeweler_Note);
                        dbCommand.Parameters.AddWithValue("@deduction", repairorder.Deduction);
                        dbCommand.Parameters.AddWithValue("@store", repairorder.STORE);
                        dbCommand.Parameters.AddWithValue("@NO_TAXRESION", repairorder.TaxReason);
                        dbCommand.Parameters.AddWithValue("@Warranty_Repair", repairorder.iSFromWarranty);
                        dbCommand.Parameters.AddWithValue("@COMISH1", repairorder.COMISH1);
                        dbCommand.Parameters.AddWithValue("@COMISH2", repairorder.COMISH2);
                        dbCommand.Parameters.AddWithValue("@COMISHAMOUNT1", repairorder.COMISHAMOUNT1);
                        dbCommand.Parameters.AddWithValue("@COMISHAMOUNT2", repairorder.COMISHAMOUNT2);
                        dbCommand.Parameters.AddWithValue("@SalesFeeAmount", repairorder.SalesFeeAmount);
                        dbCommand.Parameters.AddWithValue("@SalesFeeRate", repairorder.SalesFeeRate);
                        dbCommand.Parameters.AddWithValue("@repStatus", repairorder.repStatus);
                        dbCommand.Parameters.AddWithValue("@Who", repairorder.Who);
                        dbCommand.Parameters.AddWithValue("@repSize", repairorder.RepSize);
                        dbCommand.Parameters.AddWithValue("@repMetal", repairorder.RepMetal);
                        dbCommand.Parameters.AddWithValue("@StockStyle", repairorder.StockStyle);
                        dbCommand.Parameters.AddWithValue("@Setter", repairorder.persons);
                        dbCommand.Parameters.AddWithValue("@Email", repairorder.email);
                        dbCommand.Parameters.AddWithValue("@warranty_inv_no", repairorder.warranty_inv_no);
                        dbCommand.Parameters.AddWithValue("@warranty_style", repairorder.warranty_style);
                        dbCommand.Parameters.AddWithValue("@repair_style", repairorder.repair_style);
                        dbCommand.Parameters.AddWithValue("@SHIP_BY", repairorder.SHIP_BY);
                        dbCommand.Parameters.AddWithValue("@WEIGHT", repairorder.WEIGHT);
                        dbCommand.Parameters.AddWithValue("@INSURED", repairorder.INSURED);
                        dbCommand.Parameters.AddWithValue("@SURPRISE", repairorder.Surprise);
                        dbCommand.Parameters.AddWithValue("@iSEstRdy", repairorder.EstimateReady);
                        dbCommand.Parameters.AddWithValue("@iSWarrantyRepair", repairorder.iSWarrantyRepair);
                        var rowsAffected = dbCommand.ExecuteNonQuery();
                    }
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    // Rollback transaction if any error occurs
                    transaction.Rollback();
                    if (ex.Message.Contains("Cannot insert duplicate key in object 'dbo.REPAIR'"))
                    {
                        return "Msg 2627";
                    }
                    //MsgBox("Transaction rolled back due to an error: " + ex.Message, RadMessageIcon.Info);
                    return "100";
                }
            }
            return "1";
        }

        public bool DeleteFromTemp(string RepairNo, string formName)
        {
            using (SqlConnection conn = _connectionProvider.GetConnection())
            using (SqlCommand cmd = new SqlCommand(@"
                DELETE FROM [dbo].[tbl_temp_Repair] 
                WHERE 
                    (CASE WHEN UPPER(@frmname) = 'INVOICE' THEN tempRepairNumber ELSE LTRIM(RTRIM(tempRepairNumber)) END) = @RepairNo
                    AND frm_name = @frmname;", conn))
            {
                cmd.Parameters.AddWithValue("@RepairNo", RepairNo);
                cmd.Parameters.AddWithValue("@frmname", formName);

                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }


        public DataTable GetCustomerOrders(string acc, bool opens)
        {
            string selects = "select pon, cust_pon, acc, cast(order_date AS DATE) order_date,addr1,state,city,zip,cust_ref, unapproved from orders";
            string orderby = " order by order_date desc";
            string wheres = " where " + (opens ? "pon in (select pon from or_items where qty > shiped) " : "") +
                (!string.IsNullOrWhiteSpace(acc) ? "acc=@cacc" : "");

            if (!opens && string.IsNullOrWhiteSpace(acc))
                wheres = "";



            return GetSqlData(selects + wheres + orderby);
        }

        public DataTable CheckPoNumber(string PO)
        {
            DataTable dt = GetSqlData("SELECT isnull(ACC,'') CUST_ACC FROM orders WHERE TRIM(pon)=@pon", "@pon", PO.Trim()); ;
            return dt;
        }

        public bool isShippedPONItems(string PO)
        {
            return DataTableOK(
                GetSqlData("SELECT * FROM OR_ITEMS WHERE PON=@PON AND SHIPED>0", "@PON", PO));
        }

        public bool ModifyCustomerofPO(string pon, string oldAcc, string newAcc)
        {
            using (var connection = _connectionProvider.GetConnection())
            {
                const string query = @"
            UPDATE Orders 
            SET ACC = @NEW_ACC 
            WHERE PON = @PON AND ACC = @OLD_ACC;

            UPDATE O
            SET O.[NAME] = C.[NAME], 
                O.[ADDR1] = C.[ADDR1], 
                O.[ADDR2] = C.[ADDR12], 
                O.[CITY] = C.[CITY1], 
                O.[STATE] = C.[STATE1], 
                O.[ZIP] = C.[ZIP1], 
                O.[COUNTRY] = C.[COUNTRY]
            FROM Orders O
            INNER JOIN Customer C ON O.ACC = C.ACC
            WHERE O.PON = @PON AND O.ACC = @NEW_ACC;
        ";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add("@PON", SqlDbType.VarChar).Value = pon;
                    command.Parameters.Add("@OLD_ACC", SqlDbType.VarChar).Value = oldAcc;
                    command.Parameters.Add("@NEW_ACC", SqlDbType.VarChar).Value = newAcc;

                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }
        public bool DeleteRowsFromTable(string commandText)
        {
            using (var connection = _connectionProvider.GetConnection())
            using (var command = new SqlCommand(commandText, connection))
            {
                command.CommandType = CommandType.Text;
                command.CommandTimeout = 5000; // Consider setting an appropriate timeout instead of unlimited

                connection.Open();
                var rowsAffected = command.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }

        public DataTable setUpDiamondDetails(string templateName)
        {
            DataTable dt = GetSqlData(string.Format("SELECT * FROM DIAMONDLABEL_TEMPLATE where template_name = '{0}'", templateName.Replace("'", "''")));
            return dt;
        }

        public DataTable AlternativeDiamondTemplate()
        {
            DataTable dt = GetSqlData("SELECT * from DIAMONDLABEL_TEMPLATE");
            return dt;
        }

        public DataRow Getstockinfo(String Style, String StoreNo = "")
        {
            return GetSqlRow(" SELECT *,(in_stock-isnull(layaway,0)-isnull(in_shop,0))openstock FROM STOCK WHERE style=@style and store_no='" + (String.IsNullOrWhiteSpace(StoreNo) ? StoreCode : StoreNo) + "'",
                "@style", Style);
        }

        public DataTable GetStylehistByJobBag(String JobBagNo)
        {
            return GetSqlData(@"SELECT * FROM (SELECT S.STYLE Code, (CAST(In_Stock AS DECIMAL(10,2))+0 - CAST(SUM(ISNULL(H.CHANGE, 0)) AS DECIMAL(10,2))) AS In_Stock, 0 - CAST(SUM(ISNULL(H.CHANGE, 0)) AS DECIMAL(10,2)) AS Reserve_Stock," +
                                                            " (CAST(Wt_Stock AS DECIMAL(10, 2))+0 - CAST(SUM(ISNULL(H.WEIGHT, 0)) AS DECIMAL(10, 2))) AS Wt_Stock,   0 - CAST(SUM(ISNULL(H.WEIGHT, 0)) AS DECIMAL(10, 2)) AS Reserve_Wt" +
                                                            " FROM STOCK S LEFT JOIN STK_HIST H ON S.STYLE = H.STYLE WHERE S.store_no='" + StoreCode + "' " +
                                                            " and  H.WHAT='RESERVED FOR JOB BAG '+CONVERT(VARCHAR,'" + JobBagNo + "') " +
                                                            " GROUP BY S.STYLE, S.IN_STOCK, S.WT_STOCK)A WHERE (A.Reserve_Stock<>0.00 OR A.Reserve_Wt<>0.00) ",
                                                            "@JobBagNo", JobBagNo);
        }

        public bool isNonInventory(string pStyle)
        {
            DataTable dataTable = GetSqlData("SELECT ISNULL(NOT_STOCK,0) FROM STYLES WHERE STYLE=@pStyle AND ISNULL(NOT_STOCK,0)=1",
                "@pStyle", pStyle);
            return DataTableOK(dataTable);
        }

        public DataTable Checkvalidvendor(string vndcode)
        {
            return GetSqlData("select  * from vendors where acc=@vnd", "@vnd", vndcode);
        }

        public DataRow GetStylesInfo(String Style)
        {
            return GetSqlRow("SELECT style,ISNULL([DESC],'') AS [DESCRIPTION], ISNULL(IN_STOCK,0) AS IN_STOCK, ISNULL(WT_STOCK,0) AS WT_STOCK, COST,CAST_CODE,T_COST,Isnull(PRICE,0) PRICE,longdesc FROM STyles WHERE style =@style",
                "@style", Style);
        }

        public bool CheckstyleExists(String Style)
        {
            DataTable dataTable = GetSqlData(@"SELECT * FROM styles WHERE style=@style", "@style", Style);
            return (dataTable.Rows.Count > 0);
        }

        public DataTable CheckStyleInstock(string style, string storeno)
        {
            return GetSqlData("SELECT * FROM STOCK WHERE STYLE in (@style) and store_no=@storeno", "@style", style, "@storeno", storeno);
        }

        public bool CheckstyleExistsinStockforstore(String Style, String StoreNo = "", String JobBag = "")
        {
            DataTable dataTable;
            if (String.IsNullOrWhiteSpace(JobBag))
            {
                dataTable = GetSqlData(@"SELECT * FROM STOCK WHERE style=@style AND STORE_NO=@Store AND (ISNULL(IN_STOCK,0.00)<>0.00 OR ISNULL(WT_STOCK,0.00)<>0.00)",
                   "@style", Style, "@Store", String.IsNullOrWhiteSpace(StoreNo) ? StoreCode : StoreNo);
                return (dataTable.Rows.Count > 0);
            }
            dataTable = GetSqlData(@"SELECT * FROM STOCK WHERE style=@style AND STORE_NO=@Store", "@style", Style, "@Store", String.IsNullOrWhiteSpace(StoreNo) ? StoreCode : StoreNo);
            decimal existingStock = 0;
            if (DataTableOK(dataTable))
                existingStock = DecimalCheckForDBNull(dataTable.Rows[0]["IN_STOCK"]) + GetReserveStockForParts(Style, JobBag, StoreNo);
            return existingStock > 0;
        }

        public decimal GetReserveStockForParts(String style, String JobBag, String store)
        {
            DataTable dt = GetSqlData($"select SUM(CHANGE) QTY from PARTS_HIST WHERE  RIGHT('000000'+TRIM(JOB_BAG),6)=RIGHT('000000'+rTRIM(@JobBag),6) AND CODE LIKE @style AND STORE_NO=@store", "@style", style, "@JobBag", JobBag, "@store", store);
            return DataTableOK(dt) ? DecimalCheckForDBNull(dt.Rows[0]["QTY"]) : 0;
        }

        public DataTable GetReapirNotesDetails(string RepairNo = "")
        {
            return GetSqlData($" SELECT [USER],[DATE],[NOTES] FROM Repair_Order_Notes WHERE REPAIRNO='{RepairNo}' ORDER BY [DATE]");
        }

        public int RepairNoteUpdate(string repair, string RepairNotesXml)
        {
            int rowsAffected = 0;
            using (SqlCommand dbCommand = new SqlCommand())
            {
                dbCommand.Connection = _connectionProvider.GetConnection();
                dbCommand.CommandType = CommandType.StoredProcedure;
                dbCommand.CommandText = "RepairOrder_NotesUpdate";

                dbCommand.Parameters.AddWithValue("@RepairNo", repair);
                dbCommand.Parameters.AddWithValue("@RepairNotesXml  ", RepairNotesXml);

                dbCommand.Connection.Open();
                rowsAffected = Convert.ToInt32(dbCommand.ExecuteNonQuery());
                dbCommand.Connection.Close();
            }
            return rowsAffected;
        }


        public bool AddCustomer(string custAcc, CustomerModel customerModel = null, bool isOnAccount = false)
        {
            // Format the customer account number if it matches the 10-digit pattern
            if (Regex.IsMatch(custAcc, @"^\d{10}$"))
            {
                custAcc = Regex.Replace(custAcc, @"(\d{3})(\d{3})(\d{4})", "$1-$2-$3");
            }

            using (SqlConnection conn = _connectionProvider.GetConnection())
            using (SqlCommand cmd = new SqlCommand(@"
            INSERT INTO CUSTOMER 
            (ACC, BILL_ACC, TEL, NAME, ADDR1, ADDR12, ADDR13, CITY1, STATE1, ZIP1, COUNTRY, 
             NAME2, ADDR2, ADDR22, CITY2, STATE2, ZIP2, COUNTRY2, ON_ACCOUNT, EST_DATE, email, cell)
            VALUES 
            (REPLACE(REPLACE(CAST(@ACC AS NVARCHAR(15)), CHAR(10), ''), CHAR(13), ''), 
             REPLACE(REPLACE(CAST(@ACC AS NVARCHAR(15)), CHAR(10), ''), CHAR(13), ''), 
             CAST(@TEL AS NVARCHAR(30)), CAST(@NAME AS NVARCHAR(60)), CAST(@ADDR1 AS NVARCHAR(60)), 
             CAST(@ADDR12 AS NVARCHAR(60)), CAST(@ADDR13 AS NVARCHAR(60)), CAST(@CITY AS NVARCHAR(30)), 
             CAST(@STATE AS NVARCHAR(10)), CAST(@ZIP AS NVARCHAR(10)), CAST(@COUNTRY AS NVARCHAR(15)), 
             CAST(@NAME AS NVARCHAR(60)), CAST(@ADDR1 AS NVARCHAR(60)), CAST(@ADDR12 AS NVARCHAR(60)), 
             CAST(@CITY AS NVARCHAR(30)), CAST(@STATE AS NVARCHAR(10)), CAST(@ZIP AS NVARCHAR(10)), 
             CAST(@COUNTRY AS NVARCHAR(15)), @ON_ACCOUNT, GETDATE(), CAST(@EMAIL AS NVARCHAR(80)), 
             CAST(@Cell AS NVARCHAR(10)))", conn))
            {
                // Set Parameters
                cmd.Parameters.AddWithValue("@ACC", custAcc);
                cmd.Parameters.AddWithValue("@TEL", customerModel?.TEL ?? CheckForDBNull(GetCustTel(custAcc), typeof(string).FullName));
                cmd.Parameters.AddWithValue("@NAME", customerModel?.NAME ?? string.Empty);
                cmd.Parameters.AddWithValue("@ADDR1", customerModel?.ADDR1 ?? string.Empty);
                cmd.Parameters.AddWithValue("@ADDR12", customerModel?.ADDR12 ?? string.Empty);
                cmd.Parameters.AddWithValue("@ADDR13", customerModel?.ADDR13 ?? string.Empty);
                cmd.Parameters.AddWithValue("@CITY", customerModel?.CITY1 ?? string.Empty);
                cmd.Parameters.AddWithValue("@STATE", customerModel?.STATE1 ?? string.Empty);
                cmd.Parameters.AddWithValue("@ZIP", customerModel?.ZIP1 ?? string.Empty);
                cmd.Parameters.AddWithValue("@COUNTRY", customerModel?.COUNTRY ?? string.Empty);
                cmd.Parameters.AddWithValue("@ON_ACCOUNT", isOnAccount || (customerModel?.ON_ACCOUNT ?? false));
                cmd.Parameters.AddWithValue("@EMAIL", customerModel?.EMAIL ?? string.Empty);
                cmd.Parameters.AddWithValue("@Cell", customerModel?.CELL ?? string.Empty);

                // Execute Query
                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public string GetCustTel(string CustAcc)
        {
            decimal num;
            CustAcc = CustAcc.Replace("-", "");
            var iSNumeric = decimal.TryParse(CustAcc, out num);
            if (iSNumeric && CustAcc.Length == 10)
                return "" + Convert.ToDecimal(!string.IsNullOrWhiteSpace(CustAcc) ? CustAcc : "0");
            return "0";
        }

        public DataTable GetCreditNotesByAcc(string Acc, string CreditNo, bool IsEdit, string invno, bool IsGiftCert, string strstyl)
        {
            DataTable dataTable = new DataTable();

            // Using block for SqlConnection and SqlCommand to ensure proper disposal.
            using (SqlConnection connection = _connectionProvider.GetConnection())
            using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter())
            {
                // Create the command and set its properties
                sqlDataAdapter.SelectCommand = new SqlCommand();
                sqlDataAdapter.SelectCommand.Connection = connection;
                sqlDataAdapter.SelectCommand.CommandType = CommandType.Text;

                // Build the query dynamically based on conditions
                if (IsEdit)
                {
                    if (IsGiftCert)
                    {
                        sqlDataAdapter.SelectCommand.CommandText = @"SELECT SC.CreditNo, SC.Cust_Code, SC.Amount, SC.AvailableAmt, SC.Style, SC.UserGCNo
                    FROM StoreCreditVoucher SC
                    LEFT JOIN StoreCreditVoucherhistory SCV ON SCV.CreditNo = SC.CreditNo
                    AND ISNULL(SCV.Bal_Amt, 0) = ISNULL(SC.AvailableAmt, 0)
                    AND SCV.inv_no = @invno
                    AND SCV.style = SC.style
                    WHERE TRIM(SC.UserGCNo) = @CreditNo
                    AND TRIM(SC.UserGCNo) <> '' 
                    AND ISNULL(IsGiftCert, 0) = @IsGiftCert
                    ORDER BY 1";
                    }
                    else
                    {
                        sqlDataAdapter.SelectCommand.CommandText = @"SELECT SC.CreditNo, SC.Cust_Code, SC.Amount, SC.AvailableAmt, SC.Style, SC.UserGCNo
                    FROM StoreCreditVoucher SC
                    LEFT JOIN StoreCreditVoucherhistory SCV ON SCV.CreditNo = SC.CreditNo
                    AND ISNULL(SCV.Bal_Amt, 0) = ISNULL(SC.AvailableAmt, 0)
                    AND SCV.inv_no = @invno
                    AND SCV.style = SC.style
                    WHERE TRIM(SC.Cust_Code) = @Acc
                    AND ((TRIM(SC.CreditNo) = @CreditNo) OR (TRIM(SC.UserGCNo) = @CreditNo))
                    AND ISNULL(IsGiftCert, 0) = @IsGiftCert
                    AND SC.style = IIF(ISNULL(@strStyl, '') = '', SC.Style, ISNULL(@strStyl, ''))
                    ORDER BY 1";
                    }
                }
                else if (IsGiftCert)
                {
                    sqlDataAdapter.SelectCommand.CommandText = @"SELECT CreditNo, Cust_Code, Amount, AvailableAmt, UserGCNo, Style 
                    FROM StoreCreditVoucher
                    WHERE TRIM(UserGCNo) = @CreditNo
                    AND ISNULL(IsGiftCert, 0) = @IsGiftCert
                    AND TRIM(UserGCNo) <> ''
                    AND ISNULL(NULLIF(AvailableAmt, 0), 0) > 0
                    ORDER BY 1";
                }
                else
                {
                    sqlDataAdapter.SelectCommand.CommandText = @"SELECT CreditNo, Cust_Code, Amount, AvailableAmt, Style, UserGCNo
                    FROM StoreCreditVoucher
                    WHERE TRIM(Cust_Code) = @Acc
                    AND ((TRIM(CreditNo) = @CreditNo) OR (TRIM(UserGCNo) = @CreditNo))
                    AND ISNULL(IsGiftCert, 0) = @IsGiftCert
                    AND ISNULL(NULLIF(AvailableAmt, 0), 0) > 0
                    AND style = IIF(ISNULL(@strStyl, '') = '', Style, ISNULL(@strStyl, ''))
                    ORDER BY 1";
                }

                // Use SqlParameter with explicit types for better performance and safety
                sqlDataAdapter.SelectCommand.Parameters.Add(new SqlParameter("@Acc", SqlDbType.VarChar) { Value = Acc.Trim() });
                sqlDataAdapter.SelectCommand.Parameters.Add(new SqlParameter("@CreditNo", SqlDbType.VarChar) { Value = CreditNo.Trim() });
                sqlDataAdapter.SelectCommand.Parameters.Add(new SqlParameter("@invno", SqlDbType.VarChar) { Value = invno.Trim() });
                sqlDataAdapter.SelectCommand.Parameters.Add(new SqlParameter("@IsGiftCert", SqlDbType.Bit) { Value = IsGiftCert ? 1 : 0 });
                sqlDataAdapter.SelectCommand.Parameters.Add(new SqlParameter("@strStyl", SqlDbType.VarChar) { Value = strstyl.Trim() });

                // Open connection and fill the DataTable
                connection.Open();
                sqlDataAdapter.Fill(dataTable);
            }

            return dataTable;
        }

        public DataTable GetAllStoreCredits(string customer, DateTime? fromDate, DateTime? toDate, string option, bool isGiftCert, string creditNo)
        {
            DataTable dataTable = new DataTable();


            using (SqlDataAdapter SqlDataAdapter = new SqlDataAdapter())
            {
                // Set the command object properties
                SqlDataAdapter.SelectCommand = new SqlCommand();
                SqlDataAdapter.SelectCommand.Connection = _connectionProvider.GetConnection();
                SqlDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter.SelectCommand.CommandText = "GetAllSroreCredits";

                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@Acc", customer);
                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@FromDate", fromDate);
                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@ToDate", toDate);
                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@Type", option);
                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@IsGiftCert", isGiftCert);
                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@CreditNo", creditNo);

                SqlDataAdapter.SelectCommand.Connection.Open();
                SqlDataAdapter.SelectCommand.ExecuteNonQuery();
                SqlDataAdapter.SelectCommand.Connection.Close();
                SqlDataAdapter.Fill(dataTable);

                return dataTable;
            }
        }
        public bool IsRepairInvoiceGenerate(string rep_no)
        {
            DataTable dataTable = GetSqlData("SELECT SUM(iSNULL(QTY,0)) QTY FROM REP_INV WHERE trim(REP_NO)=trim(@rep_no)", "@rep_no", rep_no);
            return DataTableOK(dataTable) && DecimalCheckForDBNull(dataTable.Rows[0]["QTY"]) > 0;
        }

        public bool iSApproved(String RepNo)
        {
            return GetSqlData($"SELECT 1 FROM repair where trim(repair_no)=trim(@RepNo) and repStatus=@repStatus ", "@RepNo", RepNo, "@repStatus", "A").Rows.Count > 0;
        }

        public bool iSJobBagInShop(String REPAIR_NO)
        {
            DataRow row = GetSqlRow("SELECT ISNULL(SUM(IIF (DATE IS NOT NULL, QTY,  -QTY )),0) QTY FROM MFG WHERE INV_NO= RIGHT('00000'+ CONVERT(VARCHAR,@REPAIR_NO),6)", "@REPAIR_NO", REPAIR_NO.Trim());
            return DataTableOK(row) && DecimalCheckForDBNull(row["QTY"]) > 0;
        }

        public DataTable getOccasionsTypes()
        {
            return GetSqlData("select occassion from occassion_types order by occassion asc");
        }
        public void saveoccasionTypes(string occasions)
        {
            using (SqlConnection conn = _connectionProvider.GetConnection())
            using (SqlCommand dbCommand = new SqlCommand("AddEditOccasion", conn))
            {
                dbCommand.CommandType = CommandType.StoredProcedure;

                // Add XML parameter safely
                dbCommand.Parameters.Add(new SqlParameter("@INPUTXML", SqlDbType.Xml)
                {
                    Value = string.IsNullOrEmpty(occasions) ? (object)DBNull.Value : occasions
                });

                conn.Open();
                dbCommand.ExecuteNonQuery();
            }
        }
        public DataTable GetCustomerAddressAndContactAddress(String acc)
        {
            DataTable dtAddressDetails = new DataTable();

            // Using statement ensures proper disposal of both SqlConnection and SqlDataAdapter
            using (SqlConnection connection = _connectionProvider.GetConnection())
            using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter())
            {
                sqlDataAdapter.SelectCommand = new SqlCommand("GetCustomerAddressAndContactAddress", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                sqlDataAdapter.SelectCommand.Parameters.AddWithValue("@acc", acc);

                // Optionally, you can set command timeout
                sqlDataAdapter.SelectCommand.CommandTimeout = 30; // Adjust timeout as needed

                // Fill the DataTable with data
                sqlDataAdapter.Fill(dtAddressDetails);
            }
            return dtAddressDetails;
        }
        public DataTable GetUserName()
        {
            return GetSqlData("SELECT '' AS [NAME] UNION SELECT DISTINCT trim([NAME]) NAME FROM PASSFILE WHERE [NAME]<>'' ORDER BY [NAME]");
        }
        public DataTable GetStyles()
        {
            return GetSqlData("select style from styles with (nolock) order by style");
        }
        public DataTable GetVendorStyle()
        {
            return GetSqlData("SELECT DISTINCT VND_STYLE FROM STYLES with (nolock) WHERE LEN(TRIM(VND_STYLE))>0 ORDER BY VND_STYLE ");
        }
        public DataRow GetRepDeposit(string RepNo)
        {
            RepNo = "(" + string.Format("'{0}'", (RepNo.Trim())).Replace(",", "','") + ")";

            DataTable dataTable = GetSqlData(@"select isnull(sum(p.amount),0) as deposit from pay_item p                                                
                                               join payments ps on ps.inv_no=p.pay_no
                                               where p.rtv_pay='d' and trim(p.inv_no) in " + RepNo);
            return GetRowOne(dataTable);
        }

        public DataTable GetRepairDiscount(String MRepairNo, String Inv_no)
        {
            DataTable dataTable = new DataTable();

            using (SqlDataAdapter dataAdapter = new SqlDataAdapter())
            {
                // Create the command and set its properties
                dataAdapter.SelectCommand = new SqlCommand();
                dataAdapter.SelectCommand.Connection = _connectionProvider.GetConnection();
                dataAdapter.SelectCommand.CommandType = CommandType.Text;
                dataAdapter.SelectCommand.CommandText = @"select @Inv_no Inv_no,Discount,sum(Amount) Amount from Invoice_Discounts where inv_no IN (SELECT trim(value) FROM STRING_SPLIT(@MRepairNo, ',') WHERE trim(value) <> '') group by Discount";
                dataAdapter.SelectCommand.Parameters.AddWithValue("@MRepairNo", MRepairNo);
                dataAdapter.SelectCommand.Parameters.AddWithValue("@Inv_no", Inv_no);

                // Fill the datatable From adapter
                dataAdapter.Fill(dataTable);

                // Get the datarow from the table
                return dataTable;
            }
        }

        public DataTable GetRepairInvoicePayments(string invNo, bool showLayaway = true, bool isReturn = false, bool isFromReturn = false, bool iSRefund = false)
        {
            DataTable dataTable = new DataTable();

            using (SqlDataAdapter SqlDataAdapter = new SqlDataAdapter())
            {
                // Set the command object properties
                SqlDataAdapter.SelectCommand = new SqlCommand();
                SqlDataAdapter.SelectCommand.Connection = _connectionProvider.GetConnection();
                SqlDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter.SelectCommand.CommandText = "GetInvoicePayments";

                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@inv_no", invNo);
                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@showlayaway", showLayaway);
                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@is_return", isReturn);
                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@iSFromReturn", isFromReturn);
                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@iSRefund", iSRefund);

                SqlDataAdapter.SelectCommand.Connection.Open();
                SqlDataAdapter.SelectCommand.ExecuteNonQuery();
                SqlDataAdapter.SelectCommand.Connection.Close();
                SqlDataAdapter.Fill(dataTable);

                return dataTable;
            }
        }

        public string InsertDataIntoRepInvTable(RepairorderModel repairorder)
        {
            using (SqlConnection conn = _connectionProvider.GetConnection())
            using (SqlCommand dbCommand = new SqlCommand(@"INSERT INTO REP_INV (REP_NO,LINE,INV_NO,QTY,RET_INV_NO,iSInventoryItem) VALUES (@REP_NO,@LINE,@INV_NO,@QTY,@RET_INV_NO,iif(isnull(@STYLE,'')<>'',1,0))", conn))
            {

                dbCommand.Connection = conn;
                dbCommand.CommandType = CommandType.Text;

                dbCommand.Parameters.AddWithValue("@REP_NO", repairorder.REPAIR_NO);
                dbCommand.Parameters.AddWithValue("@LINE", repairorder.LINE);
                dbCommand.Parameters.AddWithValue("@INV_NO", repairorder.INV_NO.PadLeft(6));
                dbCommand.Parameters.AddWithValue("@QTY", repairorder.QTY);
                dbCommand.Parameters.AddWithValue("@RET_INV_NO", repairorder.Rtn_INV_NO);
                dbCommand.Parameters.AddWithValue("@STYLE", repairorder.STYLE);

                dbCommand.Connection.Open();
                var rowsAffected = dbCommand.ExecuteNonQuery();
                dbCommand.Connection.Close();
                return (rowsAffected > 0 ? "1" : "0");
            }
        }

        public bool DelRepInv(string INV_NO, string REPAIR_NO)
        {
            using (SqlDataAdapter SqlDataAdapter = new SqlDataAdapter())
            {
                SqlDataAdapter.SelectCommand = new SqlCommand();
                SqlDataAdapter.SelectCommand.Connection = _connectionProvider.GetConnection();
                SqlDataAdapter.SelectCommand.CommandText = "DELETE FROM REP_INV  WHERE  INV_NO = @INV_NO ";
                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@INV_NO", INV_NO);
                SqlDataAdapter.SelectCommand.Connection.Open();
                bool bResult = SqlDataAdapter.SelectCommand.ExecuteNonQuery() > 0;
                SqlDataAdapter.SelectCommand.Connection.Close();
                return true;
            }
        }

        public DataTable UpdateRpairOrderItemsTable(RepairorderModel repairorder, bool isrtn = false)
        {
            DataTable dataTable = new DataTable();

            using (SqlDataAdapter dataAdapter = new SqlDataAdapter())
            {
                dataAdapter.SelectCommand = new SqlCommand();
                dataAdapter.SelectCommand.Connection = _connectionProvider.GetConnection();
                dataAdapter.SelectCommand.CommandType = CommandType.Text;
                if (isrtn)
                {
                    if (!String.IsNullOrEmpty(repairorder.STYLE))
                        dataAdapter.SelectCommand.CommandText = @"UPDATE REP_ITEM SET [SHIPED] = [SHIPED] -  CAST(ISNULL(@SHIPED,0) AS DECIMAL(5,1)),QTY=CAST(iSNULL(@QTY,0) AS DECIMAL(5,1)),PRICE=((CAST(iSNULL(@PRICE,0) AS DECIMAL(11,2))) + ((CAST(iSNULL(@PRICE,0) AS DECIMAL(11,2)))/(100- iSNULL(Disc_Per_Line,0))* iSNULL(Disc_Per_Line,0)))  WHERE REPAIR_NO = @REPAIR_NO AND STYLE = @STYLE";
                    else
                        dataAdapter.SelectCommand.CommandText = @"UPDATE REP_ITEM SET [SHIPED] = [SHIPED] -  CAST(ISNULL(@SHIPED,0) AS DECIMAL(5,1)),QTY=CAST(iSNULL(@QTY,0) AS DECIMAL(5,1)),PRICE=((CAST(iSNULL(@PRICE,0) AS DECIMAL(11,2))) + ((CAST(iSNULL(@PRICE,0) AS DECIMAL(11,2)))/(100- iSNULL(Disc_Per_Line,0))* iSNULL(Disc_Per_Line,0)))  WHERE REPAIR_NO = @REPAIR_NO AND ITEM = @ITEM";
                }
                else if (!String.IsNullOrEmpty(repairorder.STYLE))
                    dataAdapter.SelectCommand.CommandText = @"UPDATE REP_ITEM SET [SHIPED] = [SHIPED] + CAST(ISNULL(@SHIPED,0) AS DECIMAL(5,1)),QTY=CAST(iSNULL(@QTY,0) AS DECIMAL(5,1)),PRICE=((CAST(iSNULL(@PRICE,0) AS DECIMAL(11,2))) + ((CAST(iSNULL(@PRICE,0) AS DECIMAL(11,2)))/(100- iSNULL(Disc_Per_Line,0))* iSNULL(Disc_Per_Line,0)))  WHERE REPAIR_NO = @REPAIR_NO AND STYLE = @STYLE";
                else
                    dataAdapter.SelectCommand.CommandText = @"UPDATE REP_ITEM SET [SHIPED] = [SHIPED] + CAST(ISNULL(@SHIPED,0) AS DECIMAL(5,1)),QTY=CAST(iSNULL(@QTY,0) AS DECIMAL(5,1)),PRICE=((CAST(iSNULL(@PRICE,0) AS DECIMAL(11,2))) + ((CAST(iSNULL(@PRICE,0) AS DECIMAL(11,2)))/(100- iSNULL(Disc_Per_Line,0))* iSNULL(Disc_Per_Line,0)))  WHERE REPAIR_NO = @REPAIR_NO AND ITEM = @ITEM";
                dataAdapter.SelectCommand.Parameters.AddWithValue("@SHIPED", repairorder.SHIPPED);
                dataAdapter.SelectCommand.Parameters.AddWithValue("@REPAIR_NO", repairorder.PON);
                dataAdapter.SelectCommand.Parameters.AddWithValue("@LINE", repairorder.LINE);
                dataAdapter.SelectCommand.Parameters.AddWithValue("@QTY", repairorder.QTY);
                dataAdapter.SelectCommand.Parameters.AddWithValue("@PRICE", repairorder.PRICE);
                dataAdapter.SelectCommand.Parameters.AddWithValue("@STYLE", repairorder.STYLE);
                dataAdapter.SelectCommand.Parameters.AddWithValue("@ITEM", repairorder.ITEM);

                dataAdapter.Fill(dataTable);

                // Get the datarow from the table
                return dataTable;
            }
        }

        public string UpdateRpairOrderItemsTableFromEditInvoice(RepairorderModel repairorder, bool is_tax)
        {
            DataTable dataTable = new DataTable();

            using (SqlDataAdapter dataAdapter = new SqlDataAdapter())
            {
                dataAdapter.SelectCommand = new SqlCommand();
                dataAdapter.SelectCommand.Connection = _connectionProvider.GetConnection();
                dataAdapter.SelectCommand.CommandType = CommandType.Text;

                if (is_tax)
                {
                    if (!String.IsNullOrEmpty(repairorder.STYLE))
                        dataAdapter.SelectCommand.CommandText = @"UPDATE REP_ITEM SET IS_TAX=isnull(@IS_TAX,0),QTY=CAST(iSNULL(@QTY,0) AS DECIMAL(5,1)),PRICE=((CAST(iSNULL(@PRICE,0) AS DECIMAL(11,2))) + ((CAST(iSNULL(@PRICE,0) AS DECIMAL(11,2)))/(100- iSNULL(Disc_Per_Line,0))* iSNULL(Disc_Per_Line,0))) WHERE REPAIR_NO = @REPAIR_NO AND BARCODE = @BARCODE AND STYLE = @STYLE";
                    else
                        dataAdapter.SelectCommand.CommandText = @"UPDATE REP_ITEM SET IS_TAX=isnull(@IS_TAX,0),QTY=CAST(iSNULL(@QTY,0) AS DECIMAL(5,1)),PRICE=((CAST(iSNULL(@PRICE,0) AS DECIMAL(11,2))) + ((CAST(iSNULL(@PRICE,0) AS DECIMAL(11,2)))/(100- iSNULL(Disc_Per_Line,0))* iSNULL(Disc_Per_Line,0))) WHERE REPAIR_NO = @REPAIR_NO AND BARCODE = @BARCODE AND ITEM = @ITEM";
                }
                else if (!String.IsNullOrEmpty(repairorder.STYLE))
                    dataAdapter.SelectCommand.CommandText = @"UPDATE REP_ITEM SET [SHIPED] = ([SHIPED] - @OLD_QTY) + @SHIPED, IS_TAX=isnull(@IS_TAX,0),QTY=@QTY,PRICE=(@PRICE + (@PRICE/(100- iSNULL(Disc_Per_Line,0))* iSNULL(Disc_Per_Line,0))) WHERE REPAIR_NO = @REPAIR_NO AND BARCODE = @BARCODE AND STYLE = @STYLE";
                else
                    dataAdapter.SelectCommand.CommandText = @"UPDATE REP_ITEM SET [SHIPED] = ([SHIPED] - @OLD_QTY) + @SHIPED, IS_TAX=isnull(@IS_TAX,0),QTY=@QTY,PRICE=(@PRICE + (@PRICE/(100- iSNULL(Disc_Per_Line,0))* iSNULL(Disc_Per_Line,0))) WHERE REPAIR_NO = @REPAIR_NO AND BARCODE = @BARCODE AND ITEM = @ITEM";
                dataAdapter.SelectCommand.Parameters.AddWithValue("@SHIPED", repairorder.SHIPPED);
                dataAdapter.SelectCommand.Parameters.AddWithValue("@REPAIR_NO", repairorder.PON);
                dataAdapter.SelectCommand.Parameters.AddWithValue("@OLD_QTY", Convert.ToDecimal(repairorder.OLD_QTY));
                dataAdapter.SelectCommand.Parameters.AddWithValue("@BARCODE", Convert.ToInt64(repairorder.BARCODE));
                dataAdapter.SelectCommand.Parameters.AddWithValue("@IS_TAX", repairorder.IS_TAX);
                dataAdapter.SelectCommand.Parameters.AddWithValue("@QTY", repairorder.QTY);
                dataAdapter.SelectCommand.Parameters.AddWithValue("@PRICE", repairorder.PRICE);
                dataAdapter.SelectCommand.Parameters.AddWithValue("@LINE", repairorder.LINE);
                dataAdapter.SelectCommand.Parameters.AddWithValue("@ITEM", repairorder.ITEM);
                dataAdapter.SelectCommand.Parameters.AddWithValue("@STYLE", repairorder.STYLE);
                dataAdapter.SelectCommand.Connection.Open();

                //dataAdapter.SelectCommand.Connection.Open();
                bool bResult = dataAdapter.SelectCommand.ExecuteNonQuery() > 0;
                dataAdapter.SelectCommand.Connection.Close();

                return (bResult ? "1" : "0");
            }
        }

        public string UpdOrderItemTableFrmRepInv(RepairorderModel repairorder, bool isRtn = false)
        {
            using (SqlDataAdapter dataAdapter = new SqlDataAdapter())
            {
                dataAdapter.SelectCommand = new SqlCommand();
                dataAdapter.SelectCommand.Connection = _connectionProvider.GetConnection();
                dataAdapter.SelectCommand.CommandType = CommandType.Text;
                if (isRtn)
                {
                    if (!String.IsNullOrEmpty(repairorder.STYLE))
                        dataAdapter.SelectCommand.CommandText = @"UPDATE OR_ITEMS SET SHIPED = (Cast(@SHIPED as decimal(5,1))-cast(@RQty as decimal(5,1))),QTY=CAST(iSNULL(@QTY,0) AS DECIMAL(5,1)),PRICE=((CAST(iSNULL(@PRICE,0) AS DECIMAL(11,2))) + ((CAST(iSNULL(@PRICE,0) AS DECIMAL(11,2)))/(100- iSNULL(DiscPerLine,0))* iSNULL(DiscPerLine,0))) WHERE PON = @PON AND BARCODE = @BARCODE AND INV_STYLE=@STYLE";
                    else
                        dataAdapter.SelectCommand.CommandText = @"UPDATE OR_ITEMS SET SHIPED = (Cast(@SHIPED as decimal(5,1))-cast(@RQty as decimal(5,1))),QTY=CAST(iSNULL(@QTY,0) AS DECIMAL(5,1)),PRICE=((CAST(iSNULL(@PRICE,0) AS DECIMAL(11,2))) + ((CAST(iSNULL(@PRICE,0) AS DECIMAL(11,2)))/(100- iSNULL(DiscPerLine,0))* iSNULL(DiscPerLine,0))) WHERE PON = @PON AND BARCODE = @BARCODE AND STYLE=@ITEM";
                }
                else if (!String.IsNullOrEmpty(repairorder.STYLE))
                    dataAdapter.SelectCommand.CommandText = @"UPDATE OR_ITEMS SET SHIPED = Cast(@SHIPED as decimal(5,1)),QTY=CAST(iSNULL(@QTY,0) AS DECIMAL(5,1)),PRICE=((CAST(iSNULL(@PRICE,0) AS DECIMAL(11,2))) + ((CAST(iSNULL(@PRICE,0) AS DECIMAL(11,2)))/(100- iSNULL(DiscPerLine,0))* iSNULL(DiscPerLine,0))) WHERE PON = @PON AND BARCODE = @BARCODE  AND INV_STYLE=@STYLE";
                else
                    dataAdapter.SelectCommand.CommandText = @"UPDATE OR_ITEMS SET SHIPED = Cast(@SHIPED as decimal(5,1)),QTY=CAST(iSNULL(@QTY,0) AS DECIMAL(5,1)),PRICE=((CAST(iSNULL(@PRICE,0) AS DECIMAL(11,2))) + ((CAST(iSNULL(@PRICE,0) AS DECIMAL(11,2)))/(100- iSNULL(DiscPerLine,0))* iSNULL(DiscPerLine,0))) WHERE PON = @PON AND BARCODE = @BARCODE  AND STYLE=@ITEM";
                dataAdapter.SelectCommand.Parameters.AddWithValue("@PON", "REP" + repairorder.REPAIR_NO);
                dataAdapter.SelectCommand.Parameters.AddWithValue("@BARCODE", repairorder.BARCODE);
                dataAdapter.SelectCommand.Parameters.AddWithValue("@SHIPED", repairorder.SHIPPED);
                dataAdapter.SelectCommand.Parameters.AddWithValue("@RQty", repairorder.QTY);
                dataAdapter.SelectCommand.Parameters.AddWithValue("@QTY", repairorder.QTY);
                dataAdapter.SelectCommand.Parameters.AddWithValue("@PRICE", repairorder.PRICE);
                dataAdapter.SelectCommand.Parameters.AddWithValue("@ITEM", repairorder.ITEM);
                dataAdapter.SelectCommand.Parameters.AddWithValue("@STYLE", repairorder.STYLE);
                dataAdapter.SelectCommand.Connection.Open();
                var rowsAffected = dataAdapter.SelectCommand.ExecuteNonQuery();
                dataAdapter.SelectCommand.Connection.Close();
                return (rowsAffected > 0 ? "1" : "0");
            }
        }
        public DataTable GetItemsBelowMinStock(string vendorstyle, string category, string subcat, string vendors, string group, string store, bool AllStores = false)
        {
            using (SqlConnection conn = _connectionProvider.GetConnection())
            using (SqlCommand cmd = new SqlCommand("GetItemsBelowMinStock", conn))
            using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 3000;

                cmd.Parameters.AddWithValue("@VENDORSTYLE", vendorstyle);
                cmd.Parameters.AddWithValue("@CATEGORY", category);
                cmd.Parameters.AddWithValue("@SUBCATEGORY", subcat);
                cmd.Parameters.AddWithValue("@VENDORS", vendors);
                cmd.Parameters.AddWithValue("@GROUP", group);
                cmd.Parameters.AddWithValue("@STORE", store);
                cmd.Parameters.AddWithValue("@IsAllStores", AllStores);

                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                return dataTable;
            }
        }
        public SqlDataReader GetSelectData(string[] tblname, string[] fldname, string category = "", string brand = "", bool bullion = false, string group = "", string itemtype = "", bool groupbasedontype = false, bool isbill = false)
        {

            if (tblname.Length == fldname.Length)
            {
                SqlCommand command = new SqlCommand(_connectionProvider.GetConnectionString());
                command.CommandType = CommandType.Text;
                string query = string.Empty;
                for (int ctr = 0; ctr < tblname.Length; ctr++)
                {
                    if (tblname[ctr] == "CATS")
                    {
                        if (CheckModuleEnabled(Modules.CatByGroup) || isbill)
                            query += string.Format("select '{0}' as TableName, '' as FieldValue union select '{0}', trim(ISNULL({1},'')) from {0} where (iSNULL([group],'') = '{2}' or iSNULL([group2],'') = '{2}' or iSNULL([group3],'') = '{2}' or iSNULL([group4],'') = '{2}' or (iSNULL([group],'')='' and iSNULL([group2],'')='' and iSNULL([group3],'')='' and iSNULL([group4],'')='')) and iSNULL(bullion,0) = '" + bullion + "' union ", tblname[ctr], fldname[ctr], group.Replace("'", "''"));//[GROUP] = '{2}' and [,category]
                        else
                            query += string.Format("select '{0}' as TableName, '' as FieldValue union select '{0}', trim(ISNULL({1},'')) from {0} where  iSNULL(bullion,0) = '" + bullion + "' union ", tblname[ctr], fldname[ctr], group.Replace("'", "''"));//[GROUP] = '{2}' and [,category]
                    }
                    else if (tblname[ctr] == "SUBCATS")
                        if (category != string.Empty)
                            query += string.Format("select '{0}' as TableName, '' as FieldValue union select '{0}', trim(ISNULL({1},'')) from {0} where category = '{2}' and bullion = '" + bullion + "' union ", tblname[ctr], fldname[ctr], category.Replace("'", "''"));
                        else
                            query += string.Format("select '{0}' as TableName, '' as FieldValue union select '{0}', trim(ISNULL({1},'')) from {0} where bullion = '" + bullion + "' union  ", tblname[ctr], fldname[ctr]);
                    else if (tblname[ctr] == "SUBBRANDS")
                    {
                        if (groupbasedontype)
                            query += string.Format("select '{0}' as TableName, '' as FieldValue union select '{0}', trim(ISNULL({1},'')) from {0} where (brand = '{2}' or  '" + brand + "'='') and bullion = '" + bullion + "' union ", tblname[ctr], fldname[ctr], brand.Replace("'", "''"));
                        else
                            query += string.Format("select '{0}' as TableName, '' as FieldValue union select '{0}', trim(ISNULL({1},'')) from {0} where brand = '{2}' and bullion = '" + bullion + "' union ", tblname[ctr], fldname[ctr], brand.Replace("'", "''"));
                    }
                    else if (tblname[ctr] == "METALS")
                        query += string.Format("select '{0}' as TableName, '' as FieldValue union select distinct '{0}', trim(ISNULL({1},'')) from {0} where bullion = '" + bullion + "' union ", tblname[ctr], fldname[ctr]);
                    else if (tblname[ctr] == "ITEMTYPE")
                        query += string.Format("select '{0}' as TableName, '' as FieldValue union select distinct '{0}', trim(ISNULL({1},'')) from {0} where hide = '" + bullion + "' union ", tblname[ctr], fldname[ctr]);
                    else if (tblname[ctr] == "groups")
                    {
                        if (groupbasedontype)
                            query += string.Format("select '[{0}]' as [TableName], '' as FieldValue union select '[{0}]', trim(ISNULL([{1}],'')) from [{0}] where (item_type='{2}' or  '" + itemtype + "'='') union ", tblname[ctr], fldname[ctr], itemtype.Replace("'", "''"));
                        else if (is_AlexH)
                            query += string.Format("select '[{0}]' as [TableName], '' as FieldValue union select '[{0}]', trim(ISNULL([{1}],'')) from [{0}] union ", tblname[ctr], fldname[ctr]);
                        else
                            query += string.Format("select '[{0}]' as [TableName], '' as FieldValue union select '[{0}]', trim(ISNULL([{1}],'')) from [{0}] where item_type='{2}' union ", tblname[ctr], fldname[ctr], itemtype.Replace("'", "''"));
                    }
                    else
                        query += string.Format("select '{0}' as TableName, '' as FieldValue union select '{0}', trim(ISNULL({1},'')) from {0} union ", tblname[ctr], fldname[ctr]);
                }

                command.CommandText = query.Substring(0, query.LastIndexOf("union")) + " order by FieldValue";
                command.Connection = _connectionProvider.GetConnection();
                command.Connection.Open();
                return command.ExecuteReader();
            }
            return null;
        }

        public DataTable getRepairreturnDtls(string Inv_no)
        {
            return GetSqlData(@"select * from REP_INV rtn with (nolock) right join  (select INVOICE.INV_NO,REP_INV.qty 
					from invoice with (nolock) inner join REP_INV with (nolock)  on invoice.INV_NO =REP_INV.INV_NO where Qty >0 and trim(REP_INV.inv_no) = trim(@Inv_no)) 
					items on rtn.RET_INV_NO = items.INV_NO  inner join invoice with (nolock) on items.INV_NO = invoice.INV_NO where (ISNULL(items.QTY,0) + ISNULL(rtn.QTY,0)) > 0",
                    "@Inv_no", Inv_no);
        }

        public bool getShopItemsForPickUp(string inv_no)
        {
            return DataTableOK(GetSqlData("Select * from in_items with (nolock) where repair=1 and Trimmed_inv_no=trim(@inv_no)",
                "@inv_no", inv_no));
        }

        public bool iSSpecial(String invNo)
        {
            return DataTableOK(GetSqlData($"SELECT 1 FROM INVOICE I with (nolock) LEFT JOIN IN_ITEMS II with (nolock) ON I.INV_NO = II.INV_NO where iSNull(II.IsSpecialItem,0) = 1 AND I.INV_NO =@InvNo", "@InvNo", invNo));
        }

        public DataRow GetStyleRow(string Style)
        {
            return GetSqlRow("select * from styles with (nolock) where style=@style", "@style", Style.Trim());
        }

        public string GetAccLicenceNo(string acc)
        {
            DataTable dtable = GetSqlData($"SELECT DRIVERLICENSE_NUMBER FROM CUSTOMER WHERE ACC=@acc", "@acc", acc);
            return (DataTableOK(dtable)) ? Convert.ToString(dtable.Rows[0]["DRIVERLICENSE_NUMBER"]) : string.Empty;
        }

        public bool AddSaveDraft(InvoiceModel invoice, string invoiceItems, string paymentItems, string discountItems, out string out_inv_no, bool ispayment = false, bool is_update = false, bool is_return = false, string storecodeinuse = "")
        {
            using (SqlCommand dbCommand = new SqlCommand())
            {
                dbCommand.Connection = _connectionProvider.GetConnection();
                dbCommand.CommandType = CommandType.StoredProcedure;
                dbCommand.CommandText = "AddSaveDraft";
                dbCommand.CommandTimeout = 12000;
                Object invoicedate;
                invoicedate = invoice.DATE.HasValue ? (object)invoice.DATE.Value : DBNull.Value;

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
                dbCommand.Parameters.AddWithValue("@M_Note", invoice.NOTE ?? string.Empty);
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
                dbCommand.Parameters.AddWithValue("@IS_INSTALLMENT", invoice.IS_INSTALLMENT == true ? 1 : 0);

                dbCommand.Parameters.AddWithValue("@SHIP_BY", invoice.SHIP_BY);
                dbCommand.Parameters.AddWithValue("@WEIGHT", invoice.WEIGHT);
                dbCommand.Parameters.AddWithValue("@INSURED", invoice.INSURED);
                dbCommand.Parameters.AddWithValue("@SHIPTYPE", invoice.SHIPTYPE);
                dbCommand.Parameters.AddWithValue("@TaxState", invoice.TaxState);
                dbCommand.Parameters.AddWithValue("@iSNotPickedUp", invoice.iSNotPickedUp == true ? 1 : 0);
                dbCommand.Parameters.AddWithValue("@Gold_Price", invoice.Gold_Price);
                dbCommand.Parameters.AddWithValue("@Silver_Price", invoice.Silver_Price);
                dbCommand.Parameters.AddWithValue("@DoNotChangePaymentStore", invoice.DoNotChangePaymentStore);
                dbCommand.Parameters.AddWithValue("@iSVatInclude", invoice.iSVatInclude);
                dbCommand.Parameters.AddWithValue("@GivenChange", invoice.GivenChange);
                dbCommand.Parameters.AddWithValue("@iSCompnayName2", invoice.iSCompanyName2 == true ? 1 : 0);

                SqlParameter parameter = new SqlParameter()
                {
                    ParameterName = "@TBLPOINVOICEITEMS",
                    SqlDbType = System.Data.SqlDbType.Xml,
                    Value = invoiceItems
                };
                dbCommand.Parameters.Add(parameter);

                parameter = new SqlParameter()
                {
                    ParameterName = "@TBLPAYMENTITEMS",
                    SqlDbType = System.Data.SqlDbType.Xml,
                    Value = paymentItems
                };
                dbCommand.Parameters.Add(parameter);

                parameter = new SqlParameter()
                {
                    ParameterName = "@TBLDISCOUNTITEMS",
                    SqlDbType = System.Data.SqlDbType.Xml,
                    Value = discountItems
                };
                dbCommand.Parameters.Add(parameter);

                // Open the connection, execute the query and close the connection
                dbCommand.Connection.Open();
                var rowsAffected = dbCommand.ExecuteNonQuery();
                out_inv_no = Convert.ToString(dbCommand.Parameters["@OUT_INV_NO"].Value);

                dbCommand.Connection.Close();
                return rowsAffected > 0;
            }
        }
        public List<SelectListItem> GetStyleItemType()
        {
            DataTable dataTable = new DataTable();
            using (SqlDataAdapter dataAdapter = new SqlDataAdapter())
            {
                try
                {
                    dataAdapter.SelectCommand = new SqlCommand();
                    dataAdapter.SelectCommand.Connection = _connectionProvider.GetConnection();
                    dataAdapter.SelectCommand.CommandType = CommandType.Text;
                    dataAdapter.SelectCommand.CommandText = "SELECT DISTINCT item_type FROM ITEMTYPE WHERE item_type <> '' AND hide = 0 ORDER BY item_type";
                    dataAdapter.Fill(dataTable);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                List<SelectListItem> itemTypeList = new List<SelectListItem>();
                itemTypeList.Add(new SelectListItem() { Text = " ", Value = " " });

                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow row in dataTable.Rows)
                    {
                        itemTypeList.Add(new SelectListItem()
                        {
                            Text = row["item_type"].ToString().Trim(),
                            Value = row["item_type"].ToString().Trim()
                        });
                    }
                }
                return itemTypeList;
            }
        }
        public List<SelectListItem> GetGLClassList()
        {
            DataTable dataTable = new DataTable();

            using (SqlDataAdapter dataAdapter = new SqlDataAdapter())
            {
                try
                {
                    dataAdapter.SelectCommand = new SqlCommand();
                    dataAdapter.SelectCommand.Connection = _connectionProvider.GetConnection();
                    dataAdapter.SelectCommand.CommandType = CommandType.Text;
                    dataAdapter.SelectCommand.CommandText = @"
                    SELECT TRIM(CLASS_GL) AS CLASS_GL 
                    FROM CLASSGLS 
                    WHERE CLASS_GL IS NOT NULL AND CLASS_GL <> '' 
                    ORDER BY CLASS_GL";

                    dataAdapter.Fill(dataTable);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                List<SelectListItem> classList = new List<SelectListItem>();
                classList.Add(new SelectListItem() { Text = "", Value = "" });
                foreach (DataRow row in dataTable.Rows)
                {
                    var value = row["CLASS_GL"].ToString().Trim();
                    classList.Add(new SelectListItem()
                    {
                        Text = value,
                        Value = value
                    });
                }

                return classList;
            }
        }
        public SqlDataReader GetSelectData(string[] tblname, string fldname)
        {
            var queryBuilder = new StringBuilder();
            for (int i = 0; i < tblname.Length; i++)
            {
                queryBuilder.Append($"SELECT @TableName{i} AS TableName, '' AS AttributeValue ");
                queryBuilder.Append($"UNION SELECT @TableName{i}, TRIM({fldname}) FROM {tblname[i]} ");
                if (i < tblname.Length - 1)
                    queryBuilder.Append("UNION ");
            }
            queryBuilder.Append("ORDER BY 1");

            var connection = _connectionProvider.GetConnection();
            var command = new SqlCommand(queryBuilder.ToString(), connection)
            {
                CommandType = CommandType.Text

            };
            // Add parameters for table names
            for (int i = 0; i < tblname.Length; i++)
                command.Parameters.AddWithValue($"@TableName{i}", tblname[i]);
            connection.Open();
            return command.ExecuteReader(CommandBehavior.CloseConnection);
        }
        public DataTable GetAttributes(string number)
        {
            return GetSqlData($"select '' Attrib union select trim(Attrib) Attrib from Attrib" + number + " order by 1");
        }

        public DataTable ConvertToDataTable<T>(List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);

            PropertyInfo[] props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (PropertyInfo prop in props)
            {
                Type propType = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;
                dataTable.Columns.Add(prop.Name, propType);
            }

            foreach (T item in items)
            {
                var values = new object[props.Length];
                for (int i = 0; i < props.Length; i++)
                {
                    values[i] = props[i].GetValue(item, null) ?? DBNull.Value;
                }
                dataTable.Rows.Add(values);
            }

            return dataTable;
        }
        public void SetRepairLimit(out string min, out string max)

        {
            string minlimit = "", maxlimit = "999999";



            if (_httpContextAccessor.HttpContext?.Session.GetString("COMPANYNAME") != null)
            {
                CompanyName = _httpContextAccessor.HttpContext?.Session.GetString("COMPANYNAME").ToString();
            }
            if (_httpContextAccessor.HttpContext?.Session.GetString("StoreCodeInUse") != null)
            {
                StoreCode = _httpContextAccessor.HttpContext?.Session.GetString("StoreCodeInUse").ToString();
            }

            if (CompanyName.ToUpper().Contains("EMERALD"))
            {
                minlimit = "700000";
            }
            else if (CompanyName.ToUpper().Contains("MCAU"))
            {
                minlimit = "25000";
            }
            else if (StoreCode.Contains("2") && isFao)
            {
                minlimit = "10000";
                maxlimit = "13000";
            }
            else if (CompanyName.ToUpper().Contains("ALINA"))
            {
                minlimit = "1000";
            }
            else if (CompanyName.ToUpper().Contains("SINGER"))
            {
                minlimit = "110000";
                maxlimit = "200000";
            }
            else if (iS_Quality)
            {
                minlimit = "800";
            }

            min = minlimit;
            max = maxlimit;
            //return (minlimit, maxlimit);
        }

        public DataTable GetInvoceTotalFromAddedOtherItems(string invno, bool isReturn)
        {
            return GetSqlData($@"select * FROM INVOICE I
                             JOIN IN_ITEMS II ON II.INV_NO = I.INV_NO
                             WHERE I.INV_NO=@inv_no
                             and II.STYLE
                             not in(select style from  REP_ITEM where trim(REPAIR_NO) in(SELECT trim(value) FROM STRING_SPLIT(I.PON, ',')
                             WHERE RTRIM(value) <> '') and REP_ITEM.iSInventoryItem=1)", "@inv_no", invno);
        }

        public DataTable GetRepairDetails(string repair_no)
        {
            return GetSqlData($"SELECT sum(sales_tax) tax from repair WHERE trim(repair_no) in(select trim(value) from STRING_SPLIT(@repair_no, ',') WHERE trim(value) <> '')", "@repair_no", repair_no);
        }

        public DataTable setDefaultBank()
        {
            return GetSqlData(@"SELECT CODE, BANK_ACC_NUM, BANK_NAME, USER_NAME, CAST(IS_DEFAULT AS BIT) AS IS_DEFAULT FROM BANK_ACC with (nolock) ORDER BY CODE ASC");
        }

        public DataTable getNoTaxReasons()
        {
            return GetSqlData("SELECT '' AS REASON UNION SELECT REASON FROM notax_reasons with (nolock)");
        }

        public DataTable getIN_sp_inDtls(string Inv_no)
        {
            return GetSqlData(@"select * from IN_SP_IT where inv_no=@Inv_no", "@Inv_no", Inv_no);
        }

        public DataTable getrep_invDtls(string Inv_no)
        {
            return GetSqlData(@"select * from Rep_INV where inv_no=@Inv_no", "@Inv_no", Inv_no);
        }

        public bool InsertIntoIn_SP_IT_FromInItem(string invno, string repairNo)
        {
            using (SqlCommand dbCommand = new SqlCommand())
            {
                dbCommand.Connection = _connectionProvider.GetConnection();
                dbCommand.CommandType = CommandType.StoredProcedure;
                dbCommand.CommandText = "Insert_In_SP_IT_Items";
                dbCommand.Parameters.AddWithValue("@INV_NO", invno);
                dbCommand.Parameters.AddWithValue("@REP_NO", repairNo);
                dbCommand.Connection.Open();
                var rowsAffected = dbCommand.ExecuteNonQuery();
                dbCommand.Connection.Close();
                return rowsAffected > 0;
            }
        }
        public class RepData
        {
            public DataTable rep_item { get; set; }
            public decimal discount { get; set; }
            public decimal sales_fee_rate { get; set; }
        }
        public RepData GetRepItemData(string repair_no)
        {
            RepData repdata = new RepData();
            repdata.rep_item = GetSqlData(@"select * from rep_item where repair_no=@repair_no", "@repair_no", repair_no.Trim());
            DataTable dtRepair = GetSqlData(@"select top 1 isnull(deduction,0) deduction, isnull(sales_fee_rate,0) sales_fee_rate from repair where repair_no=@repair_no", "@repair_no", repair_no.Trim());
            repdata.discount = DataTableOK(dtRepair) ? DecimalCheckForDBNull(dtRepair.Rows[0]["deduction"]) : 0;
            repdata.sales_fee_rate = DataTableOK(dtRepair) ? DecimalCheckForDBNull(dtRepair.Rows[0]["sales_fee_rate"]) : 0;
            return repdata;
        }

        public bool IsSales_taxStoreExists(string store_no)
        {
            DataTable dataTable = GetSqlData("SELECT * FROM SALES_TAX WHERE STORE_NO=@STORE_NO",
                "@STORE_NO", store_no);
            return (dataTable.Rows.Count > 0);
        }

        public bool IsSales_taxItem_typeExists(string item_type, string store_no)
        {
            DataTable dataTable = GetSqlData("SELECT * FROM SALES_TAX WHERE (isnull(Item_type,'')=@item_type OR isnull(Item_type,'')='') AND store_no=@store_no",
                "@item_type", item_type, "@store_no", store_no);
            return (dataTable.Rows.Count > 0);
        }

        public DataTable getsales_taxdata(string store_on, decimal unit_price, string item_type = "", bool is_Above_Min = false)
        {
            DataTable dataTable = new DataTable();
            bool isStoreExist = IsSales_taxStoreExists(store_on);
            bool isItem_TypeExists = IsSales_taxItem_typeExists(item_type, store_on);
            using (SqlDataAdapter SqlDataAdapter = new SqlDataAdapter())
            {
                // Create the command and set its properties
                SqlDataAdapter.SelectCommand = new SqlCommand();
                SqlDataAdapter.SelectCommand.Connection = _connectionProvider.GetConnection();
                SqlDataAdapter.SelectCommand.CommandType = CommandType.Text;

                // Assign the SQL to the command object
                string selectCommand = " AND 1=1";
                string selectCommand2 = "";
                if (isStoreExist)
                    selectCommand = "AND  STORE_NO=@STORENO";
                if (isItem_TypeExists)
                    selectCommand2 = string.Format(@"AND  isnull(item_type,'') = CASE
                                                            WHEN isnull(@item_type,'') <> '' AND
                                                            EXISTS(SELECT 1 FROM SALES_TAX
                                                            WHERE ISNULL(ITEM_TYPE, '') = isnull(@item_type, '') {0})
                                                            THEN @item_type
                                                            ELSE ''
                                                            END", selectCommand);
                SqlDataAdapter.SelectCommand.CommandText = string.Format(@"SELECT * FROM SALES_TAX 
                                                            WHERE  1=1 {1}
                                                            AND @UNIT_PRICE BETWEEN ISNULL(min_amount,0) AND ISNULL(max_amount,0)", selectCommand, selectCommand2);

                if (is_Above_Min)
                    SqlDataAdapter.SelectCommand.CommandText = string.Format(@"SELECT * FROM (SELECT *,DENSE_RANK() OVER(Order By [store_no]) as Row_index FROM SALES_TAX WHERE  
                                                            1=1 {1}
                                                            AND min_amount BETWEEN 0 AND ISNULL(@UNIT_PRICE,0)) A WHERE 1=1", selectCommand, selectCommand2);

                if (isStoreExist)
                {
                    SqlDataAdapter.SelectCommand.CommandText += "AND STORE_NO=@STORENO";
                    SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@STORENO", store_on.Trim());
                }
                else if (is_Above_Min && !isStoreExist)
                {
                    SqlDataAdapter.SelectCommand.CommandText += "AND Row_index=1";
                }
                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@UNIT_PRICE", unit_price);
                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@item_type", item_type);
                // Fill the datatable from adapter
                SqlDataAdapter.Fill(dataTable);
            }

            return dataTable;
        }

        public decimal getSales_taxValues(string store_name, decimal unit_price, decimal sQty, string item_type, out decimal sales_tax_1, out decimal sales_tax_2, out decimal sales_tax_3, bool isRepiar = false, bool iSReturn = false)
        {
            try
            {
                sales_tax_1 = sales_tax_2 = sales_tax_3 = 0;
                DataTable dt = getsales_taxdata(store_name, unit_price, item_type);
                if (DataTableOK(dt))
                {
                    decimal min_amount, max_amount, diff_amt = 0, tax;
                    min_amount = GetValueD(dt, "min_amount");
                    max_amount = GetValueD(dt, "max_amount");
                    if (CheckForDBNull(dt.Rows[0]["above_min1"], typeof(bool)))
                    {
                        dt = getsales_taxdata(store_name, unit_price, item_type, true);
                        if (DataTableOK(dt))
                        {
                            diff_amt = unit_price;
                            for (int i = dt.Rows.Count - 1; i >= 0; i--)
                            {
                                min_amount = Math.Round(CheckForDBNull(dt.Rows[i]["min_amount"], typeof(decimal).ToString()), 0);

                                if (CheckForDBNull(dt.Rows[i]["above_min1"], typeof(bool).ToString()))
                                {
                                    tax = Math.Round(((diff_amt - min_amount) * CheckForDBNull(dt.Rows[i]["rate1"],
                                        typeof(decimal).ToString()) / 100), 2);
                                    sales_tax_1 += Math.Round(tax + (Math.Sign(tax) > 1 ? 0.001M : -0.001M), 2) +
                                        GetValueD(dt, "base_tax1");
                                    diff_amt -= (diff_amt - min_amount);
                                    continue;
                                }
                                tax = ((diff_amt - min_amount) * CheckForDBNull(dt.Rows[i]["rate1"], typeof(decimal).ToString()) / 100);
                                sales_tax_1 += Math.Round(tax + (Math.Sign(tax) > 1 ? 0.001M : -0.001M), 2) +
                                    GetValueD(dt, "base_tax1");
                                break;
                            }
                        }
                    }
                    else
                        sales_tax_1 += Math.Round((unit_price * GetValueD(dt, "rate1") / 100) +
                            GetValueD(dt, "base_tax1"), 2);

                    dt = getsales_taxdata(store_name, unit_price, item_type);
                    if (CheckForDBNull(dt.Rows[0]["above_min2"], typeof(bool)))
                    {
                        dt = getsales_taxdata(store_name, unit_price, item_type, true);
                        if (DataTableOK(dt))
                        {
                            diff_amt = unit_price;
                            for (int i = dt.Rows.Count - 1; i >= 0; i--)
                            {
                                min_amount = Math.Round(CheckForDBNull(dt.Rows[i]["min_amount"], typeof(decimal).ToString()), 0);

                                if (CheckForDBNull(dt.Rows[i]["above_min2"], typeof(bool).ToString()))
                                {
                                    tax = ((diff_amt - min_amount) * CheckForDBNull(dt.Rows[i]["rate2"], typeof(decimal).ToString()) / 100);
                                    sales_tax_2 += Math.Round(tax + (Math.Sign(tax) > 1 ? 0.001M : -0.001M), 2) +
                                        GetValueD(dt, "base_tax2");
                                    diff_amt -= (diff_amt - min_amount);
                                    continue;
                                }
                                tax = ((diff_amt - min_amount) * CheckForDBNull(dt.Rows[i]["rate2"], typeof(decimal).ToString()) / 100);
                                sales_tax_2 += Math.Round(tax + (Math.Sign(tax) > 1 ? 0.001M : -0.001M), 2) +
                                    GetValueD(dt, "base_tax2");
                                break;
                            }
                        }
                    }
                    else
                        sales_tax_2 += Math.Round(((unit_price) * GetValueD(dt, "rate2") / 100) +
                            GetValueD(dt, "base_tax2"), 2);

                    dt = getsales_taxdata(store_name, unit_price, item_type);
                    if (CheckForDBNull(dt.Rows[0]["above_min3"], typeof(bool)))
                    {
                        dt = getsales_taxdata(store_name, unit_price, item_type, true);
                        if (DataTableOK(dt))
                        {
                            diff_amt = unit_price;
                            for (int i = dt.Rows.Count - 1; i >= 0; i--)
                            {
                                min_amount = Math.Round(CheckForDBNull(dt.Rows[i]["min_amount"], typeof(decimal).ToString()), 0);

                                if (CheckForDBNull(dt.Rows[i]["above_min3"], typeof(bool).ToString()))
                                {
                                    sales_tax_3 += Math.Round(((diff_amt - min_amount) * CheckForDBNull(dt.Rows[i]["rate3"], typeof(decimal).ToString()) / 100), 2);
                                    sales_tax_3 += GetValueD(dt, "base_tax3");
                                    diff_amt = min_amount;
                                    continue;
                                }
                                sales_tax_3 += Math.Round(((diff_amt - min_amount) * CheckForDBNull(dt.Rows[i]["rate3"], typeof(decimal).ToString()) / 100), 2);
                                sales_tax_3 += GetValueD(dt, "base_tax3");
                                break;
                            }
                        }
                    }
                    else
                        sales_tax_3 += Math.Round((unit_price * GetValueD(dt, "rate3") / 100) + GetValueD(dt, "base_tax3"), 2);
                }
                return Math.Round((sales_tax_1 + sales_tax_2 + sales_tax_3 + rndSalesTax0049) * sQty, 2);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool UpdateRepairTax(String repair_no, decimal sales_tax)
        {
            using (SqlCommand dbCommand = new SqlCommand())
            {
                dbCommand.Connection = _connectionProvider.GetConnection();
                dbCommand.CommandType = CommandType.Text;
                dbCommand.CommandText = @"update repair set sales_tax=isnull(@sales_tax,0) where trim(repair_no) = trim(@repair_no)";

                dbCommand.Parameters.AddWithValue("@repair_no", repair_no);
                dbCommand.Parameters.AddWithValue("@sales_tax", sales_tax);

                dbCommand.Connection.Open();
                var rowsAffected = dbCommand.ExecuteNonQuery();
                dbCommand.Connection.Close();
                return rowsAffected > 0;
            }
        }

        public DataTable GetSalesmanDetails(string orderrepairno)
        {
            DataTable dataTable = new DataTable();
            using (SqlDataAdapter dataAdapter = new SqlDataAdapter())
            {
                dataAdapter.SelectCommand = new SqlCommand();
                dataAdapter.SelectCommand.Connection = _connectionProvider.GetConnection();
                dataAdapter.SelectCommand.CommandType = CommandType.Text;
                dataAdapter.SelectCommand.CommandText = @"SELECT salesman1,salesman2,comish1,comish2,COMISHAMOUNT1,COMISHAMOUNT2 from REPAIR where trim(REPAIR_NO)=(select top 1 trim(value) from string_split(@orderrepairno, ',') where trim(value) <> '' order by value)";
                dataAdapter.SelectCommand.Parameters.AddWithValue("@orderrepairno", orderrepairno.Trim());
                dataAdapter.Fill(dataTable);
            }
            return dataTable;
        }

        public string GetNextRepairInvNo(string repairno)
        {
            using (SqlCommand dbCommand = new SqlCommand())
            {
                // Set the command object properties
                dbCommand.Connection = _connectionProvider.GetConnection();
                dbCommand.CommandType = CommandType.StoredProcedure;
                dbCommand.CommandText = "GetNextRepairInvNo";

                SqlParameter parameter = new SqlParameter();
                parameter.ParameterName = "@INV_NO";
                parameter.SqlDbType = System.Data.SqlDbType.NVarChar;
                parameter.Value = repairno;
                dbCommand.Parameters.Add(parameter);

                dbCommand.Parameters.Add("@RET_INV_NO", SqlDbType.NVarChar, 30);
                dbCommand.Parameters["@RET_INV_NO"].Direction = ParameterDirection.Output;

                dbCommand.Connection.Open();
                var rowsAffected = dbCommand.ExecuteNonQuery();
                dbCommand.Connection.Close();
                string retRepairInvNo = dbCommand.Parameters["@RET_INV_NO"].Value.ToString();
                return retRepairInvNo;
            }
        }

        public bool PickupStockStyle(String RepNo, bool iSReturn)
        {
            using (SqlCommand dbCommand = new SqlCommand())
            {
                dbCommand.Connection = _connectionProvider.GetConnection();
                dbCommand.CommandType = CommandType.StoredProcedure;
                dbCommand.CommandText = @"PickupRepairStyle";
                dbCommand.Parameters.AddWithValue("@RepNo", RepNo);
                dbCommand.Parameters.AddWithValue("@operator", LoggedUser);
                dbCommand.Parameters.AddWithValue("@StoreNo", StoreCodeInUse);
                dbCommand.Parameters.AddWithValue("@return", iSReturn);
                dbCommand.Connection.Open();
                var rowsAffected = dbCommand.ExecuteNonQuery();
                dbCommand.Connection.Close();
                return rowsAffected > 0;
            }
        }

        public bool RefundRepiarOrders(String inv_no)
        {
            DataTable dtPayments = GetSqlData(@"select name,date,method,amount,note,pay_no,inv_no from 
            (select 1 as id, date, isnull(paymenttype, '') as method, amount * 1 as amount, cast(b.note as nvarchar(100)) as note, a.pay_no as pay_no, d.name,a.inv_no from pay_item a inner
            join payments b on a.pay_no = b.inv_no inner join customer d on b.acc = d.acc where a.rtv_pay = 'd' and b.rtv_pay = 'p' 
            and trim(a.inv_no) in(select trim(value) from string_split((select top 1 pon from invoice where inv_no=@inv_no and v_ctl_no='repair'), ',') where rtrim(value) <> '')) 
            c order by id asc", "@inv_no", inv_no);

            string infomsg = string.Empty;
            for (int i = 0; i < dtPayments.Rows.Count; i++)
            {
                string Paymentid = Convert.ToString(dtPayments.Rows[i][5]);
                decimal amount = DecimalCheckForDBNull(dtPayments.Rows[i][3]);
                string Method = Convert.ToString(dtPayments.Rows[i][2]);
                String slectOption = "R";
                try
                {
                    RefundPayments(Rep_No: Convert.ToString(dtPayments.Rows[i]["inv_no"]), Paymentid: Paymentid, amount: amount, Method: Method, selctOption: slectOption);
                }
                catch
                {
                }
            }
            return true;
        }

        private void RefundPayments(string Rep_No, string Paymentid, decimal amount, string Method, String selctOption = "")
        {
            string repStatus = CancelRepairPayments(Rep_No, Paymentid, amount, Method, selctOption);
            if (repStatus.ToUpper() == "REFUNDED SUCCESSFULLY")
            {
                UpdateStoreCreditVoucher(Rep_No);
                UpdateStoreCreditVoucher(Paymentid);
            }
        }

        public void UpdateStoreCreditVoucher(string inv_no)
        {
            GetStoreProc("UPDATE_INVOICE", "@INV_NO", inv_no);
        }

        public string CancelRepairPayments(string repairNo, string paymentId, decimal amount, string method, string slctOption = "")
        {
            string msg = "";
            using (var connection = _connectionProvider.GetConnection())
            using (var dbCommand = new SqlCommand("CancelRepairPayments", connection))
            {
                dbCommand.CommandType = CommandType.StoredProcedure;
                // Add parameters
                dbCommand.Parameters.AddWithValue("@REPAIR_NO", repairNo.Trim());
                dbCommand.Parameters.AddWithValue("@PAYMENTID", paymentId.Trim());
                dbCommand.Parameters.AddWithValue("@AMOUNT", amount);
                dbCommand.Parameters.AddWithValue("@LOGGED_USER", LoggedUser);
                dbCommand.Parameters.AddWithValue("@Method", method.Trim());
                dbCommand.Parameters.AddWithValue("@CASH_REGISTER", Cash_Register);
                dbCommand.Parameters.AddWithValue("@STORE", StoreCode);
                dbCommand.Parameters.AddWithValue("@DeleteOrRefund", slctOption);

                connection.Open();
                using (var reader = dbCommand.ExecuteReader())
                {
                    if (reader.Read())
                        msg = CheckForDBNull(reader[0]);
                }
            }
            return msg;
        }

        public bool CloseRepairOrdes(string repairNos, string invNo = "")
        {
            using (var connection = _connectionProvider.GetConnection())
            using (var command = new SqlCommand("CloseRepairOrde", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandTimeout = 12000;
                command.Parameters.AddWithValue("@Order_nos", repairNos);
                command.Parameters.AddWithValue("@inv_no", invNo);
                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }

        public string getdepositno()
        {
            using (var connection = _connectionProvider.GetConnection())
            using (var dbCommand = new SqlCommand("getDepositno", connection))
            {
                dbCommand.CommandType = CommandType.StoredProcedure;
                dbCommand.CommandTimeout = 3000;

                dbCommand.Parameters.Add("@output", SqlDbType.Decimal).Direction = ParameterDirection.Output;

                connection.Open();
                dbCommand.ExecuteNonQuery();

                decimal? depositNo = dbCommand.Parameters["@output"].Value as decimal?;
                return depositNo?.ToString() ?? string.Empty;
            }
        }
        public DataTable GETDEPOSITBYCODE(string depno)
        {
            return GetStoreProc("GETDEPOSITBYCODE", "@DEPNO", depno);
        }
        public DataRow CheckDeposit(string depno)
        {
            return GetRowOne(GetStoreProc("checkdeposit", "@depno", depno));
        }
        public DataTable GetPaymentData(DateTime fdate, DateTime tdate, string store = "")
        {
            using (var connection = _connectionProvider.GetConnection())
            using (var command = new SqlCommand(@"SELECT P.INV_NO AS [Receipt No.], 
                                         P.CHK_DATE AS [Date], 
                                         P.ACC AS [Customer Code],
                                         P.Store_no AS StoreNo,
                                         P.PaymentType, 
                                         C.[NAME] AS Description, 
                                         P.PAID - P.DISCOUNT AS Amount, 
                                         CAST(0 AS BIT) AS Deposited, 
                                         P.RTV_PAY AS RTV_Pay 
                                  FROM PAYMENTS P with (nolock)
                                  LEFT JOIN CUSTOMER C with (nolock) ON P.ACC = C.ACC
                                  WHERE P.CHK_DATE between @fdate and @tdate 
                                    AND ISNULL(DEPNO, 0) = 0 
                                    AND UPPER(ISNULL(PaymentType, '')) = 'CHECK' 
                                    AND ISNULL(bank, '') = '' 
                                    AND chk_amt > 0 
                                    AND UPPER(P.RTV_PAY) = 'P'
                                    AND P.STORE_NO = IIF(@store = '', P.STORE_NO, @store)
                                  ORDER BY [Date] DESC, [Receipt No.] DESC", connection))
            using (var adapter = new SqlDataAdapter(command))
            {
                command.CommandType = CommandType.Text;

                // Use Add instead of AddWithValue to avoid type mismatches
                command.Parameters.Add("@fdate", SqlDbType.Date).Value = fdate;
                command.Parameters.Add("@tdate", SqlDbType.Date).Value = tdate;
                command.Parameters.Add("@store", SqlDbType.VarChar).Value = store;

                var dataTable = new DataTable();
                connection.Open();
                adapter.Fill(dataTable);

                return dataTable;
            }
        }

        public DataTable GetlistOfOpeninvoices(string store, string fromdate, string todate, bool Islayaway, bool IsSpecial, bool IsPaylater)
        {
            return GetSqlData("EXEC [Getlistofinvoices] @store,@FROMDATE,@TODATE,@IncludeLayaway,@IncludeSpecial,@IncludePaylater", "@store", store, "@FROMDATE", fromdate, "@TODATE", todate, "@IncludeLayaway", Islayaway.ToString(), "@IncludeSpecial", IsSpecial.ToString(), "@IncludePaylater", IsPaylater.ToString());
        }

        public DataTable GetListofOpenOrdersByCustomer(string acc)
        {
            return GetSqlData("EXEC [GetOpenOrdersByCustomer] @acc", "@acc", acc);
        }

        public DataTable GetListofUnDeposits(DateTime? FromDate, DateTime? ToDate, string storename)
        {
            var dataTable = new DataTable();

            using (var connection = _connectionProvider.GetConnection())
            using (var command = new SqlCommand("GetListofUnDeposits", connection))
            using (var adapter = new SqlDataAdapter(command))
            {
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@fdate", FromDate);
                command.Parameters.AddWithValue("@tdate", ToDate);
                command.Parameters.AddWithValue("@Store", storename);

                adapter.Fill(dataTable);
            }

            return dataTable;
        }

        public DataRow CheckJobbag(string cBarcode)
        {
            return GetSqlRow("select barcode from rep_item where barcode =[dbo].[GetBarcode](@cBarcode)and isnull(shiped,0)=0 union select barcode from lbl_bar where barcode = [dbo].[GetBarcode](@cBarcode) ", "@cBarcode", RemoveSpecialCharacters(cBarcode));
        }

        public DataRow ChkJobExists(string cBarcode, string cFrmStr, string cToStr)
        {
            return GetSqlRow("select JOBBAG from SEND_RPR where jobbag = @cBarcode AND ACKED=0",
                "@cBarcode", cBarcode, "@cFrmStr", cFrmStr, "@cToStr", cToStr);
        }

        public string RemoveSpecialCharacters(string str)
        {
            return Regex.Replace(str, "[^a-zA-Z0-9_.]+", "", RegexOptions.Compiled);
        }
        public DataRow GetJobbagQty(string cBarcode)
        {
            return GetSqlRow(@"select barcode ,sum(qty) qty from rep_item with (nolock) where (barcode = @cBarcode or 
				right('000000'+barcode,6) = right('000000'+@cBarcode,6)) and isnull(shiped,0)=0 group by barcode 
				union select barcode ,sum(qty) qty from lbl_bar with (nolock) where barcode = @cBarcode group by barcode", "@cBarcode", cBarcode);
        }

        public DataTable GetJobsForAck(string cShop)
        {
            return GetSqlData(@"SELECT DISTINCT JOBBAG, ACKED, FROM_STORE, QTY FROM SEND_RPR WHERE ACKED=0 AND TO_STORE=@cShop ORDER BY JOBBAG ASC", "@cShop", cShop);
        }

        public bool isValidRepairOrder(string repOrdNo = "")
        {
            DataTable dataTable = new DataTable();
            using (SqlDataAdapter SqlDataAdapter = new SqlDataAdapter())
            {
                SqlDataAdapter.SelectCommand = new SqlCommand();
                SqlDataAdapter.SelectCommand.Connection = _connectionProvider.GetConnection();
                SqlDataAdapter.SelectCommand.CommandType = CommandType.Text;
                SqlDataAdapter.SelectCommand.CommandText = "SELECT COUNT(*) FROM REPAIR WHERE REPAIR_NO=@repOrdNo";
                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@repOrdNo", repOrdNo);
                SqlDataAdapter.Fill(dataTable);

                if (dataTable.Rows.Count > 0)
                    foreach (DataRow dRow in dataTable.Rows)
                        return (Convert.ToInt32(dRow[0]) > 0);
            }
            return false;
        }

        public bool IsValidRepairInvoice(string repairNo)
        {
            return GetSqlData(@"SELECT RI.* FROM INVOICE I JOIN REP_INV RI ON I.PON=RI.REP_NO AND I.INV_NO=RI.INV_NO  WHERE ISNULL(I.PON,'')=@REPNO AND I.V_CTL_NO='REPAIR'", "@REPNO", repairNo).Rows.Count > 0;
        }

        public DataTable GetRepairOrderpayments(string repairNo)
        {
            return GetSqlData(@"select NAME,DATE,METHOD,AMOUNT,NOTE,PAY_NO FROM 
                                        (select 1 as id, DATE, ISNULL(PAYMENTTYPE, '') as METHOD, AMOUNT * 1 as AMOUNT, cast(b.NOTE as nvarchar(100)) as NOTE, a.PAY_NO as PAY_NO, d.name from PAY_ITEM a inner
                                        join payments b on a.PAY_NO = b.INV_NO inner join customer d on b.acc = d.acc where a.rtv_pay = 'D' and b.RTV_PAY = 'P' 
                                        and a.inv_no =@repairNo) c order by id asc", "@repairNo", repairNo);
        }

        public bool UpdateRepairCustomerDetails(String repairno, CustomerModelNew customerModel)
        {
            using (SqlCommand dbCommand = new SqlCommand())
            {
                // Set the command object properties
                dbCommand.Connection = _connectionProvider.GetConnection();
                dbCommand.CommandType = CommandType.Text;
                dbCommand.CommandText = $"update repair set name = '{customerModel.NAME}', addr1 = '{customerModel.ADDR1}', addr2 = '{customerModel.ADDR12}', city = '{customerModel.CITY1}', state = '{customerModel.STATE1}', zip = '{customerModel.ZIP1}', country = '{customerModel.COUNTRY}' where trim(repair_no)= trim('{repairno}')";

                // Open the connection, execute the query and close the connection
                dbCommand.Connection.Open();
                var rowsAffected = dbCommand.ExecuteNonQuery();
                dbCommand.Connection.Close();
                return rowsAffected > 0;
            }
        }

        public DataTable GetSelectData(string tblname, string fldname)
        {
            return GetSqlData(" SELECT '' AS [" + fldname + "] UNION SELECT DISTINCT TRIM([" + fldname + "]) FROM " + tblname + " where TRIM([" + fldname + "]) != '' order by [" + fldname + "]");
        }

        public DataTable ListJobLocation(string fromDate, string toDate, string location, string acc, bool lOpen, string FixedStoreCode = "", string _jobbag = "", bool Isnotpickedup = false)
        {
            return GetStoreProc("LISTOFJOBLOCATION", "@FROMDATE", fromDate, "@TODATE", toDate, "@LOCATION", location, "@OPENONLY", lOpen.ToString(), "@ACC", acc, "@FixedStoreCode", FixedStoreCode, "@Jobbg", _jobbag, "@Isnotpickedup", Isnotpickedup.ToString());
        }

        public bool iSRepInvExists(string Inv_No)
        {
            return GetSqlData($"SELECT 1 FROM IN_SP_IT WHERE TRIM(INV_NO)='{Inv_No}' ORDER BY INV_NO").Rows.Count > 0;
        }

        public bool iSReturnAlreadyRepairInvoice(string rep_inv_no)
        {
            return DataTableOK(GetSqlData("SELECT 1 FROM REP_INV WHERE ret_inv_no=@rep_inv_no", "@rep_inv_no", rep_inv_no));
        }

        public bool iSRefundedRepair(string rep_inv_no)
        {
            return DataTableOK(GetSqlData("SELECT 1 FROM REP_INV WHERE inv_no=@rep_inv_no and ret_inv_no<>''", "@rep_inv_no", rep_inv_no));
        }
        public void UpdateAInvoiceTCost(string inv_no)
        {
            GetStoreProc("update_invoice_t_cost", "@INV_NO", inv_no);
        }

        public bool isValidStyle(string Style)
        {
            using (SqlDataAdapter SqlDataAdapter = new SqlDataAdapter())
            {
                DataTable dataTable = GetSqlData("SELECT * FROM STYLES WHERE TRIM(ISNULL(NULLIF(STYLE,''),''))=@Style",
                    "@Style", Style.Trim());
                return (DataTableOK(dataTable));
            }
        }
        public bool isValidConsignment(string ConsNo)
        {
            return (DataTableOK(GetSqlData("SELECT * FROM APM WHERE TRIM(ISNULL(NULLIF(INV_NO,''),''))=@ConsNo",
                "@ConsNo", ConsNo.Trim())));
        }
        public DataTable GetBillConNo(bool IsBill)
        {
            if (IsBill)
                return GetSqlData(@"SELECT distinct TRIM(B.INV_NO)  INV_NO FROM BILLS B with (nolock) LEFT JOIN BIL_ITEM BI with (nolock) ON BI.INV_NO=B.INV_NO  LEFT JOIN Styles S with (nolock) ON S.STYLE=BI.STYLE WHERE len(TRIM(B.INV_NO))>0 AND CAST(B.INV_NO AS INT)>0 ORDER BY 1");
            return GetSqlData(@"SELECT distinct TRIM(B.INV_NO)  INV_NO FROM APM B with (nolock) LEFT JOIN APM_ITEM BI with (nolock) ON BI.INV_NO=B.INV_NO LEFT JOIN Styles S with (nolock) ON S.STYLE=BI.STYLE WHERE LEN(TRIM(B.INV_NO))>0 AND CAST(B.INV_NO AS INT)>0 ORDER BY 1");
        }

        public DataTable RepairNoteSkuDesc(string sku, string repair_no)
        {
            return GetSqlData($@"select SKU,case when isnull(final_note,'')<> '' then final_note else note end [DESC] from REPAIR_NOTES with(nolock) 
                        union 
                        select item SKU,note [DESC] from rep_item with(nolock) where trim(repair_no)=trim(@repair_no) and item not in(select SKU from REPAIR_NOTES with(nolock))
                        order by SKU", "@repair_no", repair_no);
        }

        public DataRow GetPartsInfo(String Partno)
        {
            return GetSqlRow("SELECT CODE, ISNULL([DESCRIPTION],'') AS [DESCRIPTION], ISNULL(IN_STOCK,0) AS IN_STOCK, ISNULL(WT_STOCK,0) AS WT_STOCK, COST, BY_WT,gl_code FROM PARTS WHERE CODE = @CODE",
                "@CODE", Partno);
        }

        public bool UpdateReservedParts(string jobBagNo, string loggedUser, string reservedParts, string storeNo, bool isUpdatePrice = false)
        {
            const string storedProcedureName = "UpdateReservedPartsForJobBag";

            using (var connection = _connectionProvider.GetConnection())
            using (var command = new SqlCommand(storedProcedureName, connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandTimeout = 5000;

                // Add parameters
                command.Parameters.Add(new SqlParameter("@tblResrvParts", SqlDbType.Xml) { Value = reservedParts });
                command.Parameters.Add(new SqlParameter("@JobBagNo", SqlDbType.NVarChar) { Value = jobBagNo });
                command.Parameters.Add(new SqlParameter("@LoggedUser", SqlDbType.NVarChar) { Value = loggedUser });
                command.Parameters.Add(new SqlParameter("@Store_No", SqlDbType.NVarChar) { Value = storeNo });
                command.Parameters.Add(new SqlParameter("@iSUpdatePrice", SqlDbType.Bit) { Value = isUpdatePrice });

                // Execute the command
                connection.Open();
                var rowsAffected = command.ExecuteNonQuery();

                return rowsAffected > 0;
            }
        }
        public DataTable GetCashRegisterHistByUser(DateTime? FromDate, DateTime? ToDate, string Store, string Register,
            out decimal Balance, bool IsManager, string Currency, string starttime, string endtime, string username, bool isbankcash)
        {
            DataTable dataTable = new DataTable();

            // Using a single using block for the connection and command
            using (var connection = _connectionProvider.GetConnection())
            using (var command = new SqlCommand("GetCashRegisterHistByUser", connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                // Adding parameters
                command.Parameters.AddWithValue("@FromDate", FromDate ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@ToDate", ToDate ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@Store", Store);
                command.Parameters.AddWithValue("@Register", Register);
                command.Parameters.AddWithValue("@IsManager", IsManager);
                command.Parameters.AddWithValue("@CURRENY", Currency);
                command.Parameters.AddWithValue("@StartTime", starttime);
                command.Parameters.AddWithValue("@EndTime", endtime);
                command.Parameters.AddWithValue("@Username", username);
                command.Parameters.AddWithValue("@IsbankCash", isbankcash);

                // Output parameter for Balance
                SqlParameter pBal = new SqlParameter("@Balance", SqlDbType.Decimal)
                {
                    Precision = 18,
                    Scale = 2,
                    Direction = ParameterDirection.Output
                };
                command.Parameters.Add(pBal);

                // Open connection and execute the query
                connection.Open();
                using (var sqlDataAdapter = new SqlDataAdapter(command))
                    sqlDataAdapter.Fill(dataTable);

                // Retrieve the Balance value from the output parameter
                Balance = pBal.Value != DBNull.Value ? (decimal)pBal.Value : 0;
            }
            return dataTable;
        }
        public bool checkIsSkuExisted(string sku)
        {
            return DataTableOK(GetSqlData(@"SELECT * FROM [DBO].[Repair_notes] WHERE SKU = @SKU", "@SKU", sku));
        }

        public bool iSDeclined(String RepNo)
        {
            return GetSqlData($"SELECT 1 FROM repair with (nolock) where trim(repair_no)=trim(@RepNo) and repStatus=@repStatus ", "@RepNo", RepNo, "@repStatus", "D").Rows.Count > 0;
        }

        public String iSRepairStockItem(String rep)
        {
            DataTable dataTable = GetSqlData($"select STockStyle from REPAIR with (nolock) where REPAIR_NO = trim(@rep)", "@rep", rep);
            return DataTableOK(dataTable) ? (Convert.ToString(dataTable.Rows[0]["STockStyle"])) : "";
        }

        public string UpdateRepairDetails(bool _isFromInv, RepairorderModel repairorderP, DataTable dtRepairitems, RepairorderModel repairorder, string invoicenumber, string optype = "", decimal tel = 0, bool iS1StPart = false, bool iSUpdateSetter = false, bool iSUpdateCustomer = false, PaymentRepair paymentRepair = null)
        {
            using (SqlConnection conn = _connectionProvider.GetConnection())
            {
                conn.Open();
                SqlTransaction transaction = conn.BeginTransaction();  // Start transaction
                try
                {
                    if (iS1StPart)
                    {
                        if (_isFromInv)
                        {
                            if (optype == "Update")
                            {
                                using (var dbCommand = new SqlCommand("DelFromInSpIt", conn, transaction))
                                {
                                    dbCommand.CommandType = CommandType.StoredProcedure;
                                    dbCommand.Parameters.AddWithValue("@INV_NO", invoicenumber);
                                    dbCommand.Parameters.AddWithValue("@REPAIRNO", repairorder.REPAIR_NO);
                                    var outDelRepStatus = new SqlParameter("@RETVAL", SqlDbType.NVarChar, 100)
                                    {
                                        Direction = ParameterDirection.Output
                                    };
                                    dbCommand.Parameters.Add(outDelRepStatus);
                                    dbCommand.CommandTimeout = 3000;
                                    dbCommand.ExecuteNonQuery();
                                }
                            }

                            for (int rows = 0; rows < dtRepairitems.Rows.Count; rows++)
                            {
                                string cItem = dtRepairitems.Rows[rows][0].ToString().Trim().PadRight(20, ' ');
                                if (string.IsNullOrWhiteSpace(dtRepairitems.Rows[rows][11].ToString()))
                                    dtRepairitems.Rows[rows][12] = (CheckForDBNull(dtRepairitems.Rows[rows][12], typeof(decimal).ToString()) + 1);

                                using (SqlCommand dbCommand = new SqlCommand(@"INSERT INTO IN_SP_IT 
                            (INV_NO, [DESC], PRICE, QTY, LINE, RET_INV_NO, IS_TAX, REPAIR_NO, iSInventoryItem, INVSTYLE) 
                            VALUES (@INV_NO, @DESC, @PRICE, @QTY, @LINE, @RET_INV_NO, @IS_TAX, @REPAIR_NO, IIF(ISNULL(@STYLE, '') <> '', 1, 0), ISNULL(@STYLE, ''))", conn, transaction))
                                {
                                    dbCommand.CommandType = CommandType.Text;
                                    dbCommand.Parameters.Add(new SqlParameter("@INV_NO", SqlDbType.VarChar, 8) { Value = invoicenumber.PadLeft(6) });
                                    dbCommand.Parameters.Add(new SqlParameter("@DESC", SqlDbType.VarChar, 400) { Value = cItem + CheckForDBNull(dtRepairitems.Rows[rows][6].ToString()) });
                                    dbCommand.Parameters.Add(new SqlParameter("@PRICE", SqlDbType.Decimal)
                                    {
                                        Value = _isFromInv ? "0" : ((DecimalCheckForNullDBNull(dtRepairitems.Rows[rows][8]) *
                                        CheckForDBNull(dtRepairitems.Rows[rows][9], typeof(decimal).ToString())).ToString())
                                    });
                                    dbCommand.Parameters.Add(new SqlParameter("@QTY", SqlDbType.Decimal) { Value = (DecimalCheckForNullDBNull(dtRepairitems.Rows[rows][8])).ToString() });
                                    dbCommand.Parameters.Add(new SqlParameter("@LINE", SqlDbType.VarChar) { Value = dtRepairitems.Rows[rows][12].ToString() });
                                    dbCommand.Parameters.Add(new SqlParameter("@RET_INV_NO", SqlDbType.VarChar, 20) { Value = "" });
                                    dbCommand.Parameters.Add(new SqlParameter("@IS_TAX", SqlDbType.Bit) { Value = repairorder.IS_TAX });
                                    dbCommand.Parameters.Add(new SqlParameter("@REPAIR_NO", SqlDbType.VarChar, 10) { Value = repairorder.REPAIR_NO.Trim() });
                                    dbCommand.Parameters.Add(new SqlParameter("@STYLE", SqlDbType.VarChar, 30) { Value = Convert.ToString(dtRepairitems.Rows[rows][2]) });
                                    int rowsAffected = dbCommand.ExecuteNonQuery();
                                }
                            }
                        }

                        using (var dbCommand = new SqlCommand("AddUpdateRepairOrder", conn, transaction))
                        {
                            dbCommand.CommandType = CommandType.StoredProcedure;
                            dbCommand.Parameters.AddWithValue("@OPTYPE", "EDIT");
                            dbCommand.Parameters.AddWithValue("@Flg", "RpTms");
                            dbCommand.Parameters.AddWithValue("@person", repairorderP.persons);
                            dbCommand.Parameters.AddWithValue("@operator", repairorderP.OPERATOR);
                            dbCommand.Parameters.AddWithValue("@repStatus", repairorderP.repStatus);
                            dbCommand.Parameters.AddWithValue("@repair_style", repairorderP.STYLE);
                            dbCommand.Parameters.AddWithValue("@taxable", repairorderP.TAXABLE);
                            dbCommand.Parameters.AddWithValue("@notax_reason", repairorderP.TaxReason);
                            var parameter = new SqlParameter
                            {
                                ParameterName = "@RepairsItems",
                                SqlDbType = SqlDbType.Xml,
                                Value = repairorderP.STRDATAXML
                            };
                            dbCommand.Parameters.Add(parameter);
                            dbCommand.ExecuteNonQuery();
                        }
                        using (SqlCommand dbCommand = new SqlCommand("AddUpdateRepairOrder", conn, transaction))
                        {
                            dbCommand.CommandType = CommandType.StoredProcedure;

                            dbCommand.Parameters.AddWithValue("@OPTYPE", "EDIT");
                            dbCommand.Parameters.AddWithValue("@Flg", "Or_tms");
                            dbCommand.Parameters.AddWithValue("@operator", repairorderP.OPERATOR);
                            dbCommand.Parameters.AddWithValue("@person", repairorderP.persons);
                            dbCommand.Parameters.AddWithValue("@repair_style", repairorderP.STYLE);
                            dbCommand.Parameters.AddWithValue("@taxable", repairorderP.TAXABLE);
                            dbCommand.Parameters.AddWithValue("@notax_reason", (object)repairorderP.TaxReason ?? DBNull.Value);

                            dbCommand.Parameters.Add(new SqlParameter
                            {
                                ParameterName = "@RepairsItems",
                                SqlDbType = SqlDbType.Xml,
                                Value = (object)repairorderP.STRDATAXML ?? DBNull.Value
                            });

                            dbCommand.ExecuteNonQuery();
                        }

                        using (var dbCommand = new SqlCommand(
                            @"UPDATE CUSTOMER SET TEL = @CustTel WHERE ACC = @CustAcc", conn, transaction))
                        {
                            dbCommand.Parameters.Add(new SqlParameter("@CustAcc", SqlDbType.VarChar) { Value = repairorder.ACC });
                            dbCommand.Parameters.Add(new SqlParameter("@CustTel", SqlDbType.VarChar) { Value = tel });
                            dbCommand.ExecuteNonQuery();
                        }
                    }
                    using (SqlCommand dbCommand = new SqlCommand(@"UPDATE REPAIR SET ACC = @ACC,CUS_REP_NO= @CUS_REP_NO,CUS_DEB_NO= @CUS_DEB_NO,DATE= @DATE,RCV_DATE= @RCV_DATE,MESSAGE= @MESSAGE,MESSAGE1= @MESSAGE1,OPERATOR= @OPERATOR,NAME= @NAME,ADDR1= @ADDR1,ADDR2= @ADDR2,CITY= @CITY,STATE= @STATE ,ZIP= @ZIP,CAN_DATE= @CAN_DATE,COUNTRY= @COUNTRY,SNH= @SNH,VIA_UPS= @VIA_UPS,IS_COD= @IS_COD,COD_TYPE= @COD_TYPE,EARLY= @EARLY,ISSUE_CRDT= @ISSUE_CRDT,SHIPTYPE=@SHIPTYPE,RESIDENT=@RESIDENT, ESTIMATE=@ESTIMATE,SALES_TAX=@Sales_tax,salesman1=@SALESMAN1,salesman2=@SALESMAN2,REGISTER=@REGISTER, Jeweler_note=@Jeweler_note,deduction=@Deduction, store=(case when isnull(store,'')='' then @store else store end),NO_TAXRESION=@NoTaxResion,COMISH1=@COMISH1,COMISH2=@COMISH2,COMISHAMOUNT1=@COMISHAMOUNT1,COMISHAMOUNT2=@COMISHAMOUNT2,Sales_Fee_Amount=@SalesFeeAmount, Sales_Fee_Rate=@SalesFeeRate, Who=@Who,repStatus=@repStatus,REP_SIZE=@repSize, REP_METAL=@repMetal,StockStyle=isnull(@StockStyle,''),setter=iif(iSNULL(@Setter,'')<>'',@Setter,Setter),email=iif(iSNULL(@Email,'')<>'',@Email,email),
                    SHIP_BY=iSNULL(@SHIP_BY,''),
                    WEIGHT=iSNULL(@WEIGHT,0),
                    INSURED=iSNULL(@INSURED,0),
                    SURPRISE=iSNULL(@SURPRISE,0),
                    Estimateready=iSNULL(@iSEstRdy,0),LastRepairUpdate=GETDATE(),
                    warranty_inv_no=isnull(@warranty_inv_no,''),style=isnull(@warranty_style,''),
                    iSWarrantyRepair = iSNULL(@iSWarrantyRepair,0)
                    WHERE REPAIR_NO = @REPAIR_NO", conn, transaction))
                    {
                        dbCommand.CommandType = CommandType.Text;
                        dbCommand.Parameters.AddWithValue("@REPAIR_NO", repairorder.REPAIR_NO);
                        dbCommand.Parameters.AddWithValue("@ACC", repairorder.ACC);
                        dbCommand.Parameters.AddWithValue("@CUS_REP_NO", repairorder.CUS_REP_NO);
                        dbCommand.Parameters.AddWithValue("@CUS_DEB_NO", repairorder.CUS_DEB_NO);
                        dbCommand.Parameters.AddWithValue("@DATE", repairorder.DATE);
                        dbCommand.Parameters.AddWithValue("@RCV_DATE", repairorder.RCV_DATE == null ? DBNull.Value : (object)repairorder.RCV_DATE);
                        dbCommand.Parameters.AddWithValue("@MESSAGE", !string.IsNullOrEmpty(repairorder._Warrnty_desc) ? repairorder._Warrnty_desc : repairorder.MESSAGE);
                        dbCommand.Parameters.AddWithValue("@MESSAGE1", repairorder.MESSAGE1);
                        dbCommand.Parameters.AddWithValue("@OPEN", 0);
                        dbCommand.Parameters.AddWithValue("@OPERATOR", repairorder.OPERATOR);
                        dbCommand.Parameters.AddWithValue("@NAME", repairorder.NAME);
                        dbCommand.Parameters.AddWithValue("@ADDR1", repairorder.ADDR1);
                        dbCommand.Parameters.AddWithValue("@ADDR2", repairorder.ADDR2);
                        dbCommand.Parameters.AddWithValue("@CITY", repairorder.CITY);
                        dbCommand.Parameters.AddWithValue("@STATE", repairorder.STATE);
                        dbCommand.Parameters.AddWithValue("@ZIP", repairorder.ZIP);
                        dbCommand.Parameters.AddWithValue("@CAN_DATE", repairorder.CAN_DATE);
                        dbCommand.Parameters.AddWithValue("@COUNTRY", repairorder.COUNTRY);
                        dbCommand.Parameters.AddWithValue("@SNH", repairorder.SNH);
                        dbCommand.Parameters.AddWithValue("@VIA_UPS", repairorder.SHIPED);
                        dbCommand.Parameters.AddWithValue("@IS_COD", repairorder.IS_COD);
                        dbCommand.Parameters.AddWithValue("@COD_TYPE", repairorder.COD_TYPE);
                        dbCommand.Parameters.AddWithValue("@EARLY", repairorder.EARLY);
                        dbCommand.Parameters.AddWithValue("@ISSUE_CRDT", repairorder.ISSUE_CRDT);
                        dbCommand.Parameters.AddWithValue("@SHIPTYPE", repairorder.SHIPTYPE);
                        dbCommand.Parameters.AddWithValue("@RESIDENT", 0);
                        dbCommand.Parameters.AddWithValue("@ESTIMATE", repairorder.ESTIMATE);
                        dbCommand.Parameters.AddWithValue("@Sales_tax", repairorder.SALES_TAX);
                        dbCommand.Parameters.AddWithValue("@SALESMAN1", repairorder.SALESMAN1);
                        dbCommand.Parameters.AddWithValue("@SALESMAN2", repairorder.SALESMAN2);
                        dbCommand.Parameters.AddWithValue("@REGISTER", repairorder.CASH_REGISTER);
                        dbCommand.Parameters.AddWithValue("@Jeweler_note", repairorder.Jeweler_Note);
                        dbCommand.Parameters.AddWithValue("@Deduction", repairorder.Deduction);
                        dbCommand.Parameters.AddWithValue("@store", repairorder.STORE);
                        dbCommand.Parameters.AddWithValue("@NoTaxResion", repairorder.TaxReason);
                        dbCommand.Parameters.AddWithValue("@COMISH1", repairorder.COMISH1);
                        dbCommand.Parameters.AddWithValue("@COMISH2", repairorder.COMISH2);
                        dbCommand.Parameters.AddWithValue("@COMISHAMOUNT1", repairorder.COMISHAMOUNT1);
                        dbCommand.Parameters.AddWithValue("@COMISHAMOUNT2", repairorder.COMISHAMOUNT2);
                        dbCommand.Parameters.AddWithValue("@SalesFeeAmount", repairorder.SalesFeeAmount);
                        dbCommand.Parameters.AddWithValue("@SalesFeeRate", repairorder.SalesFeeRate);
                        dbCommand.Parameters.AddWithValue("@Who", repairorder.Who);
                        dbCommand.Parameters.AddWithValue("@repStatus", repairorder.repStatus);
                        dbCommand.Parameters.AddWithValue("@repSize", repairorder.RepSize);
                        dbCommand.Parameters.AddWithValue("@repMetal", repairorder.RepMetal);
                        dbCommand.Parameters.AddWithValue("@StockStyle", repairorder.StockStyle);
                        dbCommand.Parameters.AddWithValue("@Setter", repairorder.persons);
                        dbCommand.Parameters.AddWithValue("@Email", repairorder.email);
                        dbCommand.Parameters.AddWithValue("@warranty_inv_no", repairorder.warranty_inv_no);
                        dbCommand.Parameters.AddWithValue("@warranty_style", repairorder.warranty_style);
                        dbCommand.Parameters.AddWithValue("@SHIP_BY", repairorder.SHIP_BY);
                        dbCommand.Parameters.AddWithValue("@WEIGHT", repairorder.WEIGHT);
                        dbCommand.Parameters.AddWithValue("@INSURED", repairorder.INSUREDDecimal);
                        dbCommand.Parameters.AddWithValue("@SURPRISE", repairorder.Surprise);
                        dbCommand.Parameters.AddWithValue("@iSEstRdy", repairorder.EstimateReady);
                        dbCommand.Parameters.AddWithValue("@iSWarrantyRepair", repairorder.iSWarrantyRepair);
                        dbCommand.ExecuteNonQuery();
                    }

                    if (iSUpdateSetter)
                    {
                        string jbbagno = repairorder.REPAIR_NO;

                        string jobbagno1 = JobNormal(jbbagno);
                        DataTable jobbaginfo = null;
                        using (SqlCommand cmd = new SqlCommand("PrintJobBag", conn, transaction))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.Add(new SqlParameter("@REPAIR_NO", ""));
                            cmd.Parameters.Add(new SqlParameter("@STYLE", ""));
                            cmd.Parameters.Add(new SqlParameter("@JOBBAGNO", jobbagno1));
                            cmd.Parameters.Add(new SqlParameter("@ismfg", false));
                            using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                            {
                                adapter.Fill(jobbaginfo);
                            }
                        }

                        DataTable jobbagissplitted = null;
                        using (SqlCommand cmd = new SqlCommand(@"select* from lbl_bar where original = @jobbagno", conn, transaction))
                        {
                            cmd.CommandType = CommandType.Text;
                            cmd.Parameters.Add(new SqlParameter("@jobbagno", jobbagno1));
                            using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                            {
                                adapter.Fill(jobbagissplitted);
                            }
                        }

                        if (DataTableOK(jobbaginfo))
                        {
                            decimal? cntQty = dtRepairitems.AsEnumerable().OfType<DataRow>().Where(x => x.Field<decimal?>("QTY") != 0).Sum(row => row.Field<decimal?>("QTY"));
                            DataTable dtGrid = new DataTable();
                            using (var command = new SqlCommand("FrmGiveOutJobBagWoSplit", conn, transaction))
                            {
                                command.CommandType = CommandType.StoredProcedure;
                                command.Parameters.Add(new SqlParameter("@jobbagno", SqlDbType.VarChar) { Value = jobbagno1 });
                                using (var sqlDataAdapter = new SqlDataAdapter(command))
                                    sqlDataAdapter.Fill(dtGrid);
                            }

                            DataTable dataTable = new DataTable();
                            string transNotesList = GetDataTableXML("TRANSNOTES", dtGrid);
                            using (var command = new SqlCommand("updatejobbag", conn, transaction))
                            {
                                command.CommandType = CommandType.StoredProcedure;
                                command.CommandTimeout = 10000;

                                command.Parameters.AddWithValue("@jobbagno", jobbagno1.Trim());
                                command.Parameters.AddWithValue("@newsettername", repairorder.persons.Trim());
                                command.Parameters.AddWithValue("@Qty", cntQty);
                                command.Parameters.AddWithValue("@Weight", 0);
                                command.Parameters.AddWithValue("@LoggedUser", LoggedUser);
                                command.Parameters.AddWithValue("@NOTESDATA", transNotesList ?? string.Empty);
                                command.Parameters.AddWithValue("@isJobbagComplted", false);
                                command.Parameters.AddWithValue("@Logno", string.Empty);
                                command.Parameters.AddWithValue("@deltranno", string.Empty);
                                command.Parameters.AddWithValue("@DueDate", DBNull.Value);
                                using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                                    adapter.Fill(dataTable);
                            }

                        }

                    }

                    if (iSUpdateCustomer)
                    {
                        using (var command = new SqlCommand(@"UPDATE CUSTOMER SET NAME = @NAME,ADDR1 = @ADDR1,ADDR12 = @ADDR2,CITY1 = @CITY,STATE1 = @STATE,
                        ZIP1 = @ZIP,COUNTRY = @COUNTRY WHERE ACC = @ACC", conn, transaction))
                        {
                            command.CommandType = CommandType.Text;
                            command.Parameters.Add(new SqlParameter("@NAME", SqlDbType.NVarChar) { Value = repairorder.NAME });
                            command.Parameters.Add(new SqlParameter("@ADDR1", SqlDbType.NVarChar) { Value = repairorder.ADDR1 });
                            command.Parameters.Add(new SqlParameter("@ADDR2", SqlDbType.NVarChar) { Value = repairorder.ADDR2 });
                            command.Parameters.Add(new SqlParameter("@CITY", SqlDbType.NVarChar) { Value = repairorder.CITY });
                            command.Parameters.Add(new SqlParameter("@STATE", SqlDbType.NVarChar) { Value = repairorder.STATE });
                            command.Parameters.Add(new SqlParameter("@ZIP", SqlDbType.NVarChar) { Value = repairorder.ZIP });
                            command.Parameters.Add(new SqlParameter("@COUNTRY", SqlDbType.NVarChar) { Value = repairorder.COUNTRY });
                            command.Parameters.Add(new SqlParameter("@ACC", SqlDbType.NVarChar) { Value = repairorder.ACC });
                            command.ExecuteNonQuery();
                        }
                    }

                    string out_inv_no = string.Empty;

                    using (var updateCommand = new SqlCommand("UPDATE_INVOICE", conn, transaction))
                    {
                        updateCommand.CommandType = CommandType.StoredProcedure;
                        updateCommand.Parameters.AddWithValue("@INV_NO", paymentRepair.cRepNo);
                        updateCommand.Parameters.AddWithValue("@From_Repair", true);
                        updateCommand.CommandTimeout = 5000;
                        updateCommand.ExecuteNonQuery();
                    }

                    using (var paymentCommand = new SqlCommand("PaymentForRepair", conn, transaction))
                    {
                        paymentCommand.CommandType = CommandType.StoredProcedure;
                        paymentCommand.CommandTimeout = 5000;

                        paymentCommand.Parameters.AddWithValue("@INV_NO", paymentRepair.cRepNo);
                        paymentCommand.Parameters.AddWithValue("@ACC", paymentRepair.cAcc);
                        paymentCommand.Parameters.AddWithValue("@PCNAME", paymentRepair.pcname);
                        paymentCommand.Parameters.AddWithValue("@DATE", DateTime.Now);
                        paymentCommand.Parameters.AddWithValue("@GR_TOTAL", paymentRepair.total);
                        paymentCommand.Parameters.AddWithValue("@ISPAYMENT", paymentRepair.ispayment ? 1 : 0);
                        paymentCommand.Parameters.AddWithValue("@IS_RETURN", paymentRepair.is_return ? 1 : 0);
                        paymentCommand.Parameters.AddWithValue("@IS_UPDATE", paymentRepair.is_update ? 1 : 0);
                        paymentCommand.Parameters.Add("@OUT_INV_NO", SqlDbType.VarChar, 10).Direction = ParameterDirection.Output;
                        paymentCommand.Parameters.AddWithValue("@UserGCNO", paymentRepair.UserGCNo);
                        paymentCommand.Parameters.AddWithValue("@StoreCode", paymentRepair.StoreCode);
                        paymentCommand.Parameters.AddWithValue("@CASH_REG_CODE", paymentRepair.Cash_Register);
                        paymentCommand.Parameters.AddWithValue("@storecodeinuse", paymentRepair.StoreCodeInUse);

                        // Payment items and discount items as XML
                        paymentCommand.Parameters.Add("@TBLPAYMENTITEMS", SqlDbType.Xml).Value = paymentRepair.paymentItems;
                        paymentCommand.Parameters.Add("@TBLDISCOUNTITEMS", SqlDbType.Xml).Value = paymentRepair.xmlDiscount;

                        paymentCommand.ExecuteNonQuery();
                        out_inv_no = Convert.ToString(paymentCommand.Parameters["@OUT_INV_NO"].Value);
                    }

                    string description1 = "Repair Order #" + repairorder.REPAIR_NO.Trim() + " Edited For " + repairorder.ACC.Trim();
                    byte[] stream = null;
                    using (var dbCommand = new SqlCommand("AddKeepRec", conn, transaction))
                    {
                        dbCommand.CommandType = CommandType.StoredProcedure;
                        dbCommand.Parameters.AddWithValue("@CWHAT", description1);
                        dbCommand.Parameters.AddWithValue("@LOGGEDUSER", LoggedUser);
                        dbCommand.Parameters.AddWithValue("@isInvoiceDeleted", false);
                        dbCommand.Parameters.AddWithValue("@ACC", string.Empty);
                        dbCommand.Parameters.AddWithValue("@type", string.Empty);
                        dbCommand.Parameters.AddWithValue("@inv_no", string.Empty);
                        dbCommand.Parameters.Add("@FILENAME", SqlDbType.NVarChar).Value = $"{DateTime.Now.Ticks}.pdf";
                        dbCommand.Parameters.Add("@FDATA", SqlDbType.Image).Value = stream ?? (object)DBNull.Value;
                        dbCommand.ExecuteNonQuery();
                    }
                    string repStatus = "Repair Order #" + repairorder.REPAIR_NO.Trim() + " Was " + repairorder.repStatus;
                    using (var dbCommand = new SqlCommand("AddKeepRec", conn, transaction))
                    {
                        dbCommand.CommandType = CommandType.StoredProcedure;
                        dbCommand.Parameters.AddWithValue("@CWHAT", repStatus);
                        dbCommand.Parameters.AddWithValue("@LOGGEDUSER", LoggedUser);
                        dbCommand.Parameters.AddWithValue("@isInvoiceDeleted", false);
                        dbCommand.Parameters.AddWithValue("@ACC", string.Empty);
                        dbCommand.Parameters.AddWithValue("@type", string.Empty);
                        dbCommand.Parameters.AddWithValue("@inv_no", string.Empty);
                        dbCommand.Parameters.Add("@FILENAME", SqlDbType.NVarChar).Value = $"{DateTime.Now.Ticks}.pdf";
                        dbCommand.Parameters.Add("@FDATA", SqlDbType.Image).Value = stream ?? (object)DBNull.Value;
                        dbCommand.ExecuteNonQuery();
                    }
                    transaction.Commit();
                }
                catch (Exception)
                {
                    // Rollback transaction if any error occurs
                    transaction.Rollback();
                    return "";
                }
            }
            return "1";
        }

        public bool GetGlcodesByAcc(string glCode)
        {
            using (SqlConnection connection = _connectionProvider.GetConnection())
            using (SqlCommand command = new SqlCommand("SELECT 1 FROM gl_accs with (nolock) WHERE acc = @Glcode", connection))
            {
                command.CommandType = CommandType.Text;
                command.CommandTimeout = 3000;
                command.Parameters.Add("@Glcode", SqlDbType.VarChar).Value = glCode;
                connection.Open();
                object result = command.ExecuteScalar();
                return result != null;
            }
        }

        public bool AddEditBankReconTemplate(string TEMPLATE_NAME, bool isedit, string date, string amount, string credit, string debit, string check_no, string description, string Glcode, string dept)
        {
            using (SqlConnection connection = _connectionProvider.GetConnection())
            using (SqlCommand dbCommand = new SqlCommand())
            {
                dbCommand.Connection = connection;
                dbCommand.CommandType = CommandType.Text;

                dbCommand.CommandText = isedit ?
                    @"UPDATE BANKRECON_TEMPLATE SET TEMPLATE_NAME=@TEMPLATE_NAME,DATE=@DATE,AMOUNT=@AMOUNT,CREDIT=@CREDIT,DEBIT=@DEBIT,CHECK_NO=@CHECK_NO,DESCRIPTION=@DESCRIPTION,Glcode=@Glcode,Dept=@Dept" :
                    @"INSERT INTO BANKRECON_TEMPLATE (TEMPLATE_NAME,DATE,AMOUNT,CREDIT,DEBIT,CHECK_NO,DESCRIPTION,Glcode,Dept) 
                    VALUES(@TEMPLATE_NAME,@date,@amount,@credit,@debit,@check_no,@description,@Glcode,@Dept)";

                dbCommand.Parameters.AddWithValue("@TEMPLATE_NAME", TEMPLATE_NAME);
                dbCommand.Parameters.AddWithValue("@date", date);
                dbCommand.Parameters.AddWithValue("@amount", amount);
                dbCommand.Parameters.AddWithValue("@credit", credit);
                dbCommand.Parameters.AddWithValue("@debit", debit);
                dbCommand.Parameters.AddWithValue("@check_no", check_no);
                dbCommand.Parameters.AddWithValue("@description", description);
                dbCommand.Parameters.AddWithValue("@Glcode", Glcode);
                dbCommand.Parameters.AddWithValue("@dept", dept);

                connection.Open();
                int rowsAffected = dbCommand.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }
        public DataTable GetStylesValue(string Style = "")
        {
            return GetSqlData($"SELECT style,size as Measurements,SHAPE+GRADE As SHAPE_GRADE,ct_weight as Weight,QUALITY as Clarity,COLOR as COLOR,CERT_NO,DATE,[DESC] as Descriptions,Price FROM STYLES WHERE STYLE ='{Style.Trim()}'");

        }
        public DataTable GetStyles(string likeexp, bool lByVndStyle = false)
        {
            if (lByVndStyle)
                return GetSqlData(@"select distinct top 1  style, DATE from styles where ltrim(rtrim(vnd_style)) =ltrim(rtrim(@style)) order by date desc ", "@style", likeexp);
            return GetSqlData("select distinct top 20 style from styles where style like @style order by style",
                "@style", likeexp + "%");
        }
        public bool UpdateTOStatement(string MESG = "", string printDiamo = "")
        {
            using (SqlConnection connection = _connectionProvider.GetConnection())
            using (SqlCommand dbCommand = new SqlCommand())
            {
                dbCommand.Connection = connection;
                dbCommand.CommandType = CommandType.Text;

                if (!string.IsNullOrEmpty(printDiamo))
                {
                    dbCommand.CommandText = "UPDATE UPS_INS1 SET Declaration_Msg = @DeclarationMsg";
                    dbCommand.Parameters.AddWithValue("@DeclarationMsg", printDiamo.Trim());
                }
                else
                {
                    dbCommand.CommandText = "UPDATE UPS_INS SET Statement_MSG = @MSG";
                    dbCommand.Parameters.AddWithValue("@MSG", MESG);
                }
                connection.Open();
                return dbCommand.ExecuteNonQuery() > 0;
            }
        }
        public string GetImage(string styleName)
        {
            if (string.IsNullOrWhiteSpace(styleName))
                return string.Empty;

            styleName = styleName.Replace("/", "").Trim();

            byte[] imageData = null;

            using (var con = _connectionProvider.GetConnection())
            using (var cmd = new SqlCommand("GetImage", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@stylename", styleName);

                con.Open();
                using (var reader = cmd.ExecuteReader(CommandBehavior.SingleRow))
                {
                    if (reader.Read())
                        imageData = reader[0] as byte[];
                }
            }

            if (imageData == null || imageData.Length == 0)
                return string.Empty;

            return Convert.ToBase64String(imageData);
        }
        public DataTable listOfLocForRepairStatus(string Repair)
        {
            // Initialize the DataTable to hold the result
            DataTable dataTable = new DataTable();

            // Use a using block to ensure resources are properly disposed of
            using (SqlConnection connection = _connectionProvider.GetConnection())
            using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter())
            {
                // Create the command and set its properties
                using (SqlCommand command = new SqlCommand("StatusOfRepairBylocation", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Explicitly define parameter types for better performance and safety
                    command.Parameters.Add(new SqlParameter("@RepairNo", SqlDbType.NVarChar) { Value = Repair.Trim() });
                    command.Parameters.Add(new SqlParameter("@OPENONLY", SqlDbType.Int) { Value = 0 });

                    // Set the command's SelectCommand for the adapter
                    sqlDataAdapter.SelectCommand = command;

                    // Fill the DataTable with data
                    sqlDataAdapter.Fill(dataTable);
                }
            }

            return dataTable;
        }

        public DataTable ListOfRepairOrdersByAcc(string CACC, bool openonly)
        {
            return GetSqlData(@"select max(R.REPAIR_NO) as REPAIR_NO, max(R.ACC) AS ACC, max(date) as DATE, 
                sum(ri.qty) as QTY, SUM(isnull(ri.qty,0) - isnull(ri.[shiped],0)) as [OPEN] 
                FROM REPAIR R  with (nolock) inner join  REP_ITEM RI  with (nolock) on  R.REPAIR_NO = RI.REPAIR_NO 
                WHERE isnumeric(R.REPAIR_no)=1 and R.ACC=@CACC  " + (openonly ? " and ri.qty>ri.shiped " : "") +
                " group by ri.repair_no  ORDER BY RI.REPAIR_NO DESC", "@CACC", CACC);
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

            String cmd = $@"SELECT REPAIR.REPAIR_NO,REP_ITEM.LINE, iif(iSNULL(REP_ITEM.ITEM,'')='',REP_ITEM.STYLE,REP_ITEM.ITEM) ITEM, 
                IIF(@isplit='True' , iSNULL((SELECT SUM(QTY) QTY FROM LBL_BAR  with (nolock) 
                WHERE BARCODE in {repNos} and style=REP_ITEM.ITEM   GROUP BY BARCODE),0), REP_ITEM.QTY) QTY, 
                CAST(IIF(ISNULL(REPAIR.INV_NO,'')  = '',(REP_ITEM.PRICE * (100 - REP_ITEM.Disc_Per_Line) / 100),(REP_ITEM.PRICE * (100 - REP_ITEM.Disc_Per_Line) / 100)) AS DECIMAL(15,2)) as PRICE, REP_ITEM.NOTE,REP_ITEM.SHIPED,REP_ITEM.STAT,REP_ITEM.VENDOR,replicate('0', 6 - len(REP_ITEM.BARCODE)) + cast (REP_ITEM.BARCODE as varchar) AS BARCODE,REP_ITEM.SIZE,repair.NAME,cast(repair.DATE as date) as DATE,repair.ADDR1,repair.COUNTRY,repair.MESSAGE,repair.ACC,repair.ISSUE_CRDT,repair.is_cod,repair.cod_type,repair.early,repair.ADDR2,repair.CITY,repair.STATE,repair.ZIP,cast(repair.CAN_DATE as date) as CAN_DATE ,cast(repair.RCV_DATE as date) as RCV_DATE,cast(repair.DATE as date) as DATE,repair.CUS_REP_NO,repair.CUS_DEB_NO,(REP_ITEM.QTY - REP_ITEM.SHIPED) as [OPEN] ,c.NAME2 as name2, c.addr2 as addr2, c.addr22 as addr22, c.city2 as city2, c.state2 as state2, c.zip2 as zip2,c.Tel,c.Email,repair.shiptype,repair.resident, repair.estimate, repair.taxable, case when trim(repair.inv_no)<>'' then 0 else repair.sales_tax end sales_tax,repair.Message1,repair.salesman1,II.REPAIR_NOTE,repair.inv_no,repair.Jeweler_note,repair.deduction,repair.store,repair.no_taxresion,repair.salesman2,repair.comish1,repair.comish2,repair.comishamount1,repair.comishamount2,repair.Sales_Fee_Amount,repair.Sales_Fee_Rate,repair.repStatus,rep_size,rep_metal,REPAIR.setter, iSNULL(REP_ITEM.Disc_Per_Line,0) Disc_Per_Line,
                repair.warranty_inv_no,repair.style,repair.ship_by,repair.weight,repair.insured,repair.snh,repair.surprise,repair.Estimateready,repair.operator
                FROM REPAIR with (nolock) 
                left join REP_ITEM with (nolock) on REPAIR.REPAIR_NO = REP_ITEM.REPAIR_NO 
                inner join customer c with (nolock) on c.ACC = repair.ACC  LEFT join IN_ITEMS II on REPAIR.INV_NO = II.INV_NO  AND II.STYLE=REP_ITEM.ITEM   
                where right('00000000'+repair.REPAIR_NO,9) in " + (isplit ? splitrepNos : repNos);
            if (iSOnJobbag)
            {
                cmd += " union " + $@"select  R.REPAIR_NO,PH.LINE, ph.CODE ITEM, 
                                    cast(PH.CHANGE as decimal(11,2)) QTY, CAST(0 AS DECIMAL(15,2)) as PRICE, 
                                    PH.NOTE,cast(0 as decimal(11,2)) SHIPED,'' STAT,'' VENDOR,PH.JOB_BAG AS BARCODE,
                                    '' SIZE,R.NAME,cast(R.DATE as date) as DATE,R.ADDR1,R.COUNTRY,R.MESSAGE,R.ACC,R.ISSUE_CRDT,R.is_cod,R.cod_type,R.early,R.ADDR2,R.CITY,R.STATE,R.ZIP,cast(R.CAN_DATE as date) as CAN_DATE ,cast(R.RCV_DATE as date) as RCV_DATE,cast(R.DATE as date) as DATE,R.CUS_REP_NO,R.CUS_DEB_NO,(0 - 0) as [OPEN] ,c.NAME2 as name2, c.addr2 as addr2, c.addr22 as addr22, c.city2 as city2, c.state2 as state2, c.zip2 as zip2,c.Tel,c.Email,R.shiptype,R.resident, R.estimate, R.taxable, case when R.inv_no<>'' then 0 else R.sales_tax end sales_tax,R.Message1,R.salesman1,'' REPAIR_NOTE,R.inv_no,R.Jeweler_note,R.deduction,R.store,R.no_taxresion,R.salesman2,R.comish1,R.comish2,R.comishamount1,R.comishamount2,R.Sales_Fee_Amount,R.Sales_Fee_Rate,R.repStatus,rep_size,rep_metal,R.setter, 0 Disc_Per_Line,
                                    R.warranty_inv_no,R.style,R.ship_by,R.weight,R.insured,R.snh,R.surprise,R.Estimateready,r.operator
                                    from PARTS_HIST PH with (nolock) 
                                    JOIN REPAIR R with (nolock) on [dbo].[getbarcode](R.REPAIR_NO)=[dbo].[getbarcode](PH.JOB_BAG)
                                    join CUSTOMER C with (nolock) on C.ACC=R.ACC
                                    where right('00000000'+PH.JOB_BAG,9) in {(isplit ? splitrepNos : repNos)}  and code not in(select concat('CODE ',ITEM) from REP_ITEM where  right('0000000'+REPAIR_NO,7) in {(isplit ? splitrepNos : repNos)} )
                                    and ISNULL(PH.ON_JOBBAG,0)=1";
            }
            return GetSqlData($@"{cmd}", "@REPAIR_NO", repNos, "@isplit", isplit.ToString());//iSNULL(REP_ITEM.STYLE,'')='' AND
        }

        public DataTable GetAllRepairTableDataForInvoice(string ordnumber, bool isErepair = false)
        {
            ordnumber = "(" + string.Format("'{0}'", (ordnumber.Trim())).Replace(",", "','") + ")";
            if (isErepair)
                return GetSqlData(@"SELECT iSNULL(RI.ITEM,'') ITEM,RI.STYLE,RI.SIZE,RI.NOTE AS DESCRIPTION,RI.QTY AS RPRQTY,
                    (RI.QTY - RI.SHIPED) as [OPEN],(RI.QTY - RI.SHIPED) AS RSRVD,convert(int, isnull(RI.QTY,0)) QTY ,
                    (iSNULL(RI.PRICE,0) * (100 - iSNULL(RI.Disc_Per_Line,0)) / 100) AS CHARGE,RI.VENDOR AS REFNO,RI.BARCODE,RI.LINE, 
                    CAST(0 AS DECIMAL(12,2)) AS G_TOT,convert(int, RI.QTY) as INVQTY1,NULL AS INVOICEQTY, IS_TAX,R.SNH 
                    FROM REP_ITEM RI  with (nolock) LEFT JOIN REPAIR R  with (nolock) ON RI.REPAIR_NO = R.REPAIR_NO  WHERE R.Inv_no in" + ordnumber);// AND (RI.QTY - RI.SHIPED) != 0";
            return GetSqlData(@"SELECT RI.Repair_No,iSNULL(RI.ITEM,'') ITEM,RI.STYLE,RI.SIZE,RI.NOTE AS DESCRIPTION,RI.QTY AS RPRQTY,
                (RI.QTY - RI.SHIPED) as [OPEN],(RI.QTY - RI.SHIPED) AS RSRVD,convert(int, isnull(RI.QTY,0)) QTY ,
                (iSNULL(RI.PRICE,0) * (100 - iSNULL(RI.Disc_Per_Line,0)) / 100) AS CHARGE,RI.VENDOR AS REFNO,RI.BARCODE,RI.LINE, 
                CAST(0 AS DECIMAL(12,2)) AS G_TOT,convert(int, RI.QTY) as INVQTY1,NULL AS INVOICEQTY, IS_TAX, R.SNH 
                FROM REP_ITEM RI  with (nolock) LEFT JOIN REPAIR R  with (nolock) ON RI.REPAIR_NO = R.REPAIR_NO  WHERE RI.REPAIR_NO in" + ordnumber);// AND (RI.QTY - RI.SHIPED) != 0";
        }
        public DataRow GetCustAttrib()
        {
            return GetSqlRow("Select CustAttribCheck1, CustAttribCheck2, CustAttribCheck3, CustAttribCheck4, CustAttribCheck5, CustAttribCheck6, CustAttribCheck7, CustAttribCheck8 FROM ups_ins1 with(nolock)");
        }
        public DataTable GetCustomerAttribs(string[] attr)
        {
            DataTable dataTable = new DataTable();
            using (SqlConnection conn = _connectionProvider.GetConnection())
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandType = CommandType.Text;

                    StringBuilder queryBuilder = new StringBuilder();
                    List<SqlParameter> parameters = new List<SqlParameter>();

                    for (int i = 0; i < attr.Length; i++)
                    {
                        string paramName = "@Attr" + i;
                        queryBuilder.Append($"SELECT DISTINCT ISNULL(ATTR_VAL, '') AS AttrVal FROM CUST_ATT with (nolock) WHERE ATTR_NUM = {paramName} UNION ");
                        parameters.Add(new SqlParameter(paramName, SqlDbType.Int) { Value = attr[i] });
                    }

                    // Remove the last "UNION" and add "ORDER BY 1"
                    if (queryBuilder.Length > 6) // " UNION ".Length = 6
                        queryBuilder.Length -= 6;
                    queryBuilder.Append(" ORDER BY 1");
                    cmd.CommandText = queryBuilder.ToString();
                    cmd.Parameters.AddRange(parameters.ToArray());
                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        adapter.Fill(dataTable);
                }
            }
            return dataTable;
        }
        public bool DeleteAttribute(string attNum, string attVal)
        {
            using (var connection = _connectionProvider.GetConnection())
            using (var command = new SqlCommand("DELETE FROM cust_att WHERE attr_num = @attrNum AND attr_val = @attrVal", connection))
            {
                command.CommandType = CommandType.Text;
                command.Parameters.AddWithValue("@attrNum", attNum);
                command.Parameters.AddWithValue("@attrVal", attVal);

                connection.Open();
                return command.ExecuteNonQuery() > 0;
            }
        }

        public DataRow GetInvoiceByInvNo(string invno)
        {
            return (GetSqlRow(@"Select top 1 i.*,it.memo_no,ISNULL(it.IsSpecialItem,0) IsSpecialItem,it.fpon 
                                           from invoice i  with (nolock) left join (select * from IN_ITEMS with (nolock) where Trimmed_inv_no =@inv_no) it on i.inv_no = it.inv_no 
                                           Where i.trimmed_inv_no = @inv_no", "@inv_no", invno.Trim()));
        }


        public InvoiceModel MapDataRowToInvoiceModel(DataRow row)
        {
            if (row == null) return null;

            var model = new InvoiceModel();

            // === Identifiers ===
            model.INV_NO = row.Table.Columns.Contains("INV_NO") ? row["INV_NO"]?.ToString() : null;
            model.ReceiptNumber = model.INV_NO;

            model.ACC = row.Table.Columns.Contains("ACC") ? row["ACC"]?.ToString() : null;
            model.BACC = row.Table.Columns.Contains("BACC") ? row["BACC"]?.ToString() : null;

            // === Store ===
            if (row.Table.Columns.Contains("STORE_NO"))
            {
                model.STORE_NO = row["STORE_NO"]?.ToString();
                model.store = model.STORE_NO;
            }
            // === Note ===
            if (row.Table.Columns.Contains("MESSAGE"))
            {
                model.NOTE = row["MESSAGE"]?.ToString();
            }

            // === Dates ===
            if (row.Table.Columns.Contains("INV_DATE") && row["INV_DATE"] != DBNull.Value)
                model.INV_DATE = Convert.ToDateTime(row["INV_DATE"]);
            else if (row.Table.Columns.Contains("DATE") && row["DATE"] != DBNull.Value)
                model.DATE = Convert.ToDateTime(row["DATE"]);

            // === Sales Reps ===
            model.SalesRep1 = row.Table.Columns.Contains("SALESMAN1") ? row["SALESMAN1"]?.ToString().Trim() : null;
            model.SalesRep2 = row.Table.Columns.Contains("SALESMAN2") ? row["SALESMAN2"]?.ToString().Trim() : null;
            model.SalesRep3 = row.Table.Columns.Contains("SALESMAN3") ? row["SALESMAN3"]?.ToString().Trim() : null;
            model.SalesRep4 = row.Table.Columns.Contains("SALESMAN4") ? row["SALESMAN4"]?.ToString().Trim() : null;

            // === Customer ===
            model.NAME = row.Table.Columns.Contains("NAME") ? row["NAME"]?.ToString() : null;
            model.CustomerName = model.NAME;
            model.Address1 = row.Table.Columns.Contains("ADDR1") ? row["ADDR1"]?.ToString() : null;
            model.Address2 = row.Table.Columns.Contains("ADDR2") ? row["ADDR2"]?.ToString() : null;
            model.Address3 = row.Table.Columns.Contains("ADDR3") ? row["ADDR3"]?.ToString() : null;
            model.City = row.Table.Columns.Contains("CITY") ? row["CITY"]?.ToString() : null;
            model.State = row.Table.Columns.Contains("STATE") ? row["STATE"]?.ToString() : null;
            model.ZipCode = row.Table.Columns.Contains("ZIP") ? row["ZIP"]?.ToString() : null;
            model.Country = row.Table.Columns.Contains("COUNTRY") ? row["COUNTRY"]?.ToString() : null;
            model.AllowTradeIn = Convert.ToBoolean(row.Table.Columns.Contains("TRADEIN") ? row["TRADEIN"] : 0);



            // === Totals / Summary directly on InvoiceModel ===
            if (row.Table.Columns.Contains("TOTAL") && row["TOTAL"] != DBNull.Value)
                model.Total = Convert.ToDecimal(row["TOTAL"]);

            //if (row.Table.Columns.Contains("DEDUCTION") && row["DEDUCTION"] != DBNull.Value)
            //    //model.TotalDiscount = Convert.ToDecimal(row["DEDUCTION"]);
            //model.TotalDiscount = Math.Abs(Convert.ToDecimal(row["DEDUCTION"]));

            if (row.Table.Columns.Contains("SCRAP_LOGNO"))
                model.ScrapLogNo = row["SCRAP_LOGNO"]?.ToString();


            model.DEDUCTION = Math.Abs(Convert.ToDecimal(row["DEDUCTION"]));

            if (row.Table.Columns.Contains("SNH") && row["SNH"] != DBNull.Value)
                model.Shipping = Convert.ToDecimal(row["SNH"]);


            if (row.Table.Columns.Contains("TRADE_IN") && row["TRADE_IN"] != DBNull.Value)
                model.TradeIn = Convert.ToDecimal(row["TRADE_IN"]);

            if (row.Table.Columns.Contains("Sales_tax_rate") && row["Sales_tax_rate"] != DBNull.Value)
                model.SalesTaxPercent = Convert.ToDecimal(row["Sales_tax_rate"]);
            else
                model.SalesTaxPercent = 10;

            if (row.Table.Columns.Contains("SALES_TAX") && row["SALES_TAX"] != DBNull.Value)
                model.SalesTaxAmount = Convert.ToDecimal(row["SALES_TAX"]);

            if (row.Table.Columns.Contains("Sales_Fee_Rate") && row["Sales_Fee_Rate"] != DBNull.Value)
                model.SalesFeePercent = Convert.ToDecimal(row["Sales_Fee_Rate"]);

            if (row.Table.Columns.Contains("Sales_Fee_Amount") && row["Sales_Fee_Amount"] != DBNull.Value)
                model.SalesFee = Convert.ToDecimal(row["Sales_Fee_Amount"]);

            if (row.Table.Columns.Contains("GR_TOTAL") && row["GR_TOTAL"] != DBNull.Value)
                model.GrandTotal = Convert.ToDecimal(row["GR_TOTAL"]);

            // === Derived fields ===
            //model.SubTotal = model.Total - model.TotalDiscount;
            //model.GrandTotal = model.SubTotal + model.Shipping + model.SalesTaxAmount + model.SalesFee - model.TradeIn;


            return model;
        }

        public DataSet GetLoadInvoiceCombineQuery(string invNo, bool includeInvNo, bool isReturn, bool isRepair, bool isVatInclude,
            bool showLayaway, bool isFromReturn, bool isRepairStatus, bool isDraftSave, bool jmCare, string invDate, string cacc, string salesman, string stoteno, string bulino)

        {
            var ds = new DataSet();
            const string query = "load_invoice_combine_sp";
            using (var connection = _connectionProvider.GetConnection())
            using (var command = new SqlCommand(query, connection))
            using (var adapter = new SqlDataAdapter(command))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Inv_no", invNo);
                command.Parameters.AddWithValue("@includeinv_no", includeInvNo);
                command.Parameters.AddWithValue("@is_return", isReturn);
                command.Parameters.AddWithValue("@IS_REPAIR", isRepair);
                command.Parameters.AddWithValue("@iSVatInclude", isVatInclude);
                command.Parameters.AddWithValue("@showlayaway", showLayaway);
                command.Parameters.AddWithValue("@iSFromReturn", isFromReturn);
                command.Parameters.AddWithValue("@iSRepair", isRepairStatus);
                command.Parameters.AddWithValue("@JMCare", jmCare);
                command.Parameters.AddWithValue("@invdate", setSQLDateTime(Convert.ToDateTime(invDate)));
                command.Parameters.AddWithValue("@cacc", cacc);
                command.Parameters.AddWithValue("@iSDraft", isDraftSave);
                command.Parameters.AddWithValue("@salesman", salesman);
                command.Parameters.AddWithValue("@stoteno", stoteno);
                command.Parameters.AddWithValue("@bulino", bulino);
                adapter.Fill(ds);
            }
            return ds;
        }
        /*
                public DataTable getstoresdataforsetdefault(bool ActiveOnly = false, bool allstores = false, bool withShop = false, bool NoText = false, bool StoreInactive = false)
                {
                    if (StoreInactive)
                        return GetSqlData("select '' as CODE UNION SELECT 'SHOP' as CODE UNION SELECT DISTINCT CODE FROM [stores] with (nolock) where inactive=0");
                    if (NoText)
                    {
                        if (!string.IsNullOrWhiteSpace(FixedStoreCode) && !allstores)
                            return GetSqlData("SELECT DISTINCT CODE FROM [stores] with (nolock) where code=@code ", "@code", FixedStoreCode);
                        return GetSqlData("select '' as CODE UNION SELECT DISTINCT CODE FROM [stores] with (nolock) where notext=0  ORDER BY CODE ");
                    }
                    if (!withShop)
                    {
                        if (!string.IsNullOrWhiteSpace(FixedStoreCode) && !allstores)
                            return GetSqlData("SELECT DISTINCT CODE FROM [stores] with (nolock) where code=@code ", "@code", FixedStoreCode);
                        if (ActiveOnly)
                            return GetSqlData("select distinct code from stores with (nolock) where code != '' and code is not null  order by code asc ");
                        return GetSqlData("select '' as CODE UNION SELECT DISTINCT CODE FROM [stores] with (nolock) ORDER BY CODE ");
                    }
                    if (!string.IsNullOrWhiteSpace(FixedStoreCode) && !allstores)
                        return GetSqlData("SELECT 'SHOP' as CODE UNION SELECT DISTINCT CODE FROM [stores] with (nolock) where code=@code ", "@code", FixedStoreCode);
                    if (ActiveOnly)
                        return GetSqlData("SELECT 'SHOP' as code UNION select distinct code from stores with (nolock) where code != '' and code is not null ");
                    return GetSqlData("select '' as CODE UNION SELECT 'SHOP' as CODE UNION SELECT DISTINCT CODE FROM [stores] with (nolock) ");
                }
        */
        public bool iSInvoicePickedUp(string inv_no)
        {
            DataTable dtPickedUp = GetSqlData(@"select 1 from invoice where trimmed_inv_no=trim(@inv_no) and PickUpDate is not null", "@inv_no", inv_no);
            return DataTableOK(dtPickedUp);
        }
        public bool iSUsedStoreCreditAnyWhere(String InvNo)
        {
            DataTable dtLastUpdate = GetSqlData($"select * from StoreCreditVoucherHistory with(nolock) where trim(CreditNo) IN(SELECT trim(CreditNo) FROM StoreCreditVoucherHistory where Inv_No=@INV_NO) AND Inv_No NOT IN(@INV_NO)", "@INV_NO", InvNo);
            return DataTableOK(dtLastUpdate);
        }
        public bool iSEditReturnInvoiceInRegularOption(String inv_no)
        {
            DataTable dtRetiurned = GetSqlData($@"select 1 from invoice WITH (NOLOCK) where  inv_no=@inv_no and iSNULL(iSReturn,0)=1", "@inv_no", inv_no);
            return DataTableOK(dtRetiurned);
        }
        public bool CheckCustomerWithStore(string acc)
        {
            DataTable dataTable = GetSqlData("select * from customer with (nolock) where trim(acc)=@acc and store_no=@storecode ",
                "@storecode", StoreCodeInUse1, "@acc", acc.Trim());
            return DataTableOK(dataTable);
        }
        public bool iSPickupDate(string inv_no)
        {
            return DataTableOK(GetSqlData($@"select 1 from invoice i with(nolock)
                                                join in_items ii with(nolock) on i.inv_no = ii.inv_no
                                                where isnull(JM_SALES_ID, '') <> '' and ii.warranty <> '' and lower(ii.warranty) <> 'none'
                                                and i.inv_no=@InvNo", "@InvNo", inv_no));
        }
        public decimal GetOptionNo(String inv_no)
        {
            DataTable dtOption =
                GetSqlData($@"select invoice_type from invoice with (nolock) where inv_no=@inv_no", "@inv_no", inv_no);
            return DataTableOK(dtOption) ? DecimalCheckForDBNull(dtOption.Rows[0]["invoice_type"]) : 0;
        }

        public bool iSFromOldPo(string inv_no)
        {
            DataTable dtScheme = GetSqlData(@"select 1 from old_pos where trim(job_no)=trim(@inv_no)", "@inv_no", inv_no);
            return DataTableOK(dtScheme);
        }
        public DataTable GetCheckRecforReconcile(string excelData, string bank, int step = 0, bool isExcelData = false)
        {
            var dataTable = new DataTable();

            using (var connection = _connectionProvider.GetConnection())
            using (var dataAdapter = new SqlDataAdapter())
            {
                var command = new SqlCommand("GetCheckRecforReconcile", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                command.Parameters.AddWithValue("@ExcelData", excelData);
                command.Parameters.AddWithValue("@Bank", bank);
                command.Parameters.AddWithValue("@step", step);
                command.Parameters.AddWithValue("@isexceldata", isExcelData);

                dataAdapter.SelectCommand = command;

                dataAdapter.Fill(dataTable);
            }

            return dataTable;
        }
        public bool AddBankRecImportRecords(string bank, string LoggedUser, string xmldata)
        {
            using (SqlConnection connection = _connectionProvider.GetConnection())
            using (SqlCommand command = new SqlCommand("AddBankRecImportRecords", connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add("@BANK", SqlDbType.VarChar).Value = bank;
                command.Parameters.Add("@LOGGED_USER", SqlDbType.VarChar).Value = LoggedUser;

                SqlParameter xmlParameter = new SqlParameter("@ExcelData", SqlDbType.Xml)
                {
                    Value = xmldata
                };
                command.Parameters.Add(xmlParameter);

                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }
        public DataTable Getbankreconwithlogno(string reconlogno)
        {
            return GetSqlData("SELECT * FROM BANK with (nolock) WHERE TRIM(LOG_RECON)=TRIM(@reconlogno)", "@reconlogno", reconlogno);
        }
        public bool UpdateStyleofspecialorder(string data)
        {
            using (var connection = _connectionProvider.GetConnection())
            using (var command = new SqlCommand("UpdateStyleofspecialorder", connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@dtData", data);
                command.Parameters.AddWithValue("@USER", LoggedUser);

                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();

                return rowsAffected > 0;
            }
        }

        public string GetBeforeUnderScore(string p_style)
        {
            return (InvStyle(p_style.Trim().Replace(" ", "_")));
        }

        public string GetBeforeHyphen(string p_style)
        {
            string style = p_style.Split('-')[0];
            return style.Length >= 2 ? style : p_style;
        }

        public DataTable GetPieceCost(string style, string store)
        {
            return GetSqlData("SELECT TOP 1 ISNULL(S.T_Cost,0.0) T_Cost, t.In_Stock In_Stock, iif(isnull(s.CLASS_GL,'')='','ASSET', isnull(s.CLASS_GL,'')) CLASS_GL FROM Stock t with(nolock) LEFT JOIN STYLES S with (nolock) on t.style = s.style WHERE t.Style = dbo.InvStyle(@Style) AND t.STORE_NO=@store", "@style", style, "@store", store);
        }
        public decimal GetBCost(string style)
        {
            return GetValueD(GetSqlData("SELECT TOP 1 ISNULL(T_Cost,0.0) T_Cost FROM Styles with (nolock) WHERE Style=dbo.InvStyle(@Style)", "@style", style), "T_Cost");
        }
        public bool BreakAPiece(string bStyle, string bStore, string brokendata)
        {
            using (var connection = _connectionProvider.GetConnection())
            using (var dbCommand = new SqlCommand("BreakAPiece", connection))
            {
                dbCommand.CommandType = CommandType.StoredProcedure;

                dbCommand.Parameters.Add(new SqlParameter("@STYLE", SqlDbType.NVarChar) { Value = bStyle });
                dbCommand.Parameters.Add(new SqlParameter("@STORE_NO", SqlDbType.NVarChar) { Value = bStore });
                dbCommand.Parameters.Add(new SqlParameter("@LOGGEDUSER", SqlDbType.NVarChar) { Value = LoggedUser });
                dbCommand.Parameters.Add(new SqlParameter("@BROKENDATA", SqlDbType.NVarChar) { Value = brokendata });

                connection.Open();
                return dbCommand.ExecuteNonQuery() > 0;
            }
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
        public bool DeleteRowGivenTFV(string tblName, string fieldName, string fieldValue)
        {
            string query = $"DELETE FROM {tblName} WHERE {fieldName} = @FieldValue";

            using (var connection = _connectionProvider.GetConnection())
            using (var command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@FieldValue", fieldValue);
                connection.Open();
                return command.ExecuteNonQuery() > 0;
            }
        }
        public bool UpdateInactve(string XML)
        {
            using (SqlConnection connection = _connectionProvider.GetConnection())
            using (SqlCommand sqlCommand = new SqlCommand("updateActiveorinActive", connection))
            {
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.CommandTimeout = 3000;
                SqlParameter parameter = new SqlParameter
                {
                    ParameterName = "@xmlActive",
                    SqlDbType = SqlDbType.Xml,
                    Value = XML
                };
                sqlCommand.Parameters.Add(parameter);
                connection.Open();
                bool rowsAffected = sqlCommand.ExecuteNonQuery() > 0;
                return rowsAffected;
            }
        }

        public string GetNewPackageStyles(string group, string style, bool isNewDashEnabled, bool isSave, string itemType = "")
        {
            return GetValue(GetStoreProc("CreateStylesForPackage_ByGroup", "@Group", group.Trim(), "@IsNewDash", isNewDashEnabled.ToString(), "@STYLES", style.Trim(), "@IsSave", isSave.ToString(), "@ItemType", itemType), "RETURN_STYLE");
        }
        public void CheckNegetiveInvOfStyles_MakePackge(string XML, out string retMsg)
        {
            retMsg = string.Empty;
            using (SqlConnection con = _connectionProvider.GetConnection())
            using (SqlDataAdapter dataAdapter = new SqlDataAdapter())
            {
                dataAdapter.SelectCommand = new SqlCommand("CheckNegetiveInvOfStyles_MakePackge", con)
                {
                    CommandType = CommandType.StoredProcedure
                };

                dataAdapter.SelectCommand.Parameters.AddWithValue("@STORE", StoreCodeInUse1);

                SqlParameter xmlParam = new SqlParameter("@XML", SqlDbType.Xml)
                {
                    Value = XML
                };
                dataAdapter.SelectCommand.Parameters.Add(xmlParam);

                SqlParameter outLogNo = new SqlParameter("@returnlog", SqlDbType.NVarChar, int.MaxValue)
                {
                    Direction = ParameterDirection.Output
                };
                dataAdapter.SelectCommand.Parameters.Add(outLogNo);

                con.Open();
                DataTable dataTable = new DataTable();
                dataAdapter.Fill(dataTable);
                retMsg = outLogNo.Value != DBNull.Value ? outLogNo.Value.ToString() : string.Empty;
            }
        }

        public List<SelectListItem> GetDistinctStatesList()
        {
            DataTable dataTable = GetSqlData("SELECT DISTINCT STATE1 AS STATE FROM CUSTOMER WHERE STATE1 IS NOT NULL AND STATE1 != '' ORDER BY STATE1");

            List<SelectListItem> stateList = new List<SelectListItem>();
            stateList.Add(new SelectListItem() { Text = "", Value = "" });

            if (DataTableOK(dataTable))
            {
                foreach (DataRow dr in dataTable.Rows)
                {
                    string state = dr["STATE"].ToString().Trim();
                    stateList.Add(new SelectListItem() { Text = state, Value = state });
                }
            }

            return stateList;
        }

        public DataTable GETMSG()
        {
            return GetSqlData("SELECT STatement_MSG FROM UPS_INS");
        }
        public DataTable SearchInvoiceCustomers()
        {
            return GetStoreProc("CustomerStatementAll");
        }
        public DataTable SearchInvoiceCustomersDueDays()
        {
            return GetStoreProc("CustomerStatementAll_DueDays");
        }
        public DataTable GL_CODE_AMOUNT(string strChequeNo, string strBank, out string GL_CODE, out decimal AMOUNT)
        {
            DataTable dataTable = new DataTable();
            GL_CODE = string.Empty;
            AMOUNT = 0;

            using (SqlConnection connection = _connectionProvider.GetConnection())
            using (SqlCommand command = new SqlCommand("GET_GL_CODE_AMOUT", connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                // Input parameters
                command.Parameters.AddWithValue("@CHEQUENO", strChequeNo);
                command.Parameters.AddWithValue("@BANK", strBank);

                // Output parameters
                SqlParameter glCodeParam = new SqlParameter("@GLCODE", SqlDbType.VarChar, 30)
                {
                    Direction = ParameterDirection.Output
                };
                command.Parameters.Add(glCodeParam);

                SqlParameter amountParam = new SqlParameter("@AMOUNT", SqlDbType.Decimal)
                {
                    Direction = ParameterDirection.Output
                };
                command.Parameters.Add(amountParam);
                connection.Open();
                command.ExecuteNonQuery();
                GL_CODE = glCodeParam.Value != DBNull.Value ? glCodeParam.Value.ToString() : string.Empty;
                AMOUNT = amountParam.Value != DBNull.Value ? Convert.ToDecimal(amountParam.Value) : 0;
                using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                {
                    adapter.Fill(dataTable);
                }
            }
            return dataTable;
        }

        public void GetInvoiceMemoNote(string acc, out string invoicenote, out string memonote)
        {
            invoicenote = memonote = string.Empty;
            using (SqlDataAdapter dataAdapter = new SqlDataAdapter())
            {
                DataTable dtInvoiceMemoNotes = new DataTable();
                dataAdapter.SelectCommand = new SqlCommand();
                dataAdapter.SelectCommand.Connection = _connectionProvider.GetConnection();
                dataAdapter.SelectCommand.CommandType = CommandType.Text;
                dataAdapter.SelectCommand.CommandText = "SELECT TOP 1 invoicenote, memonote FROM invoicenotes WHERE acc = @acc";
                dataAdapter.SelectCommand.Parameters.AddWithValue("@acc", acc);

                dataAdapter.Fill(dtInvoiceMemoNotes);

                if (dtInvoiceMemoNotes.Rows.Count > 0)
                {
                    invoicenote = CheckForDBNull(dtInvoiceMemoNotes.Rows[0]["invoicenote"]);
                    memonote = CheckForDBNull(dtInvoiceMemoNotes.Rows[0]["memonote"]);
                }
            }
        }

        public DataTable GetMasterDetailProfInvoice(string invno, int sortby, decimal module = 0)
        {
            DataTable dataTable = new DataTable();
            using (SqlConnection conn = _connectionProvider.GetConnection())
            using (SqlCommand cmd = new SqlCommand("GetMasterDetailProfInvoice", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 600;

                cmd.Parameters.Add("@inv_no", SqlDbType.VarChar, 50).Value = invno;
                cmd.Parameters.Add("@Module", SqlDbType.Decimal).Value = module;
                cmd.Parameters.Add("@sortby", SqlDbType.Int).Value = sortby;

                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                    dataTable.Load(reader);
            }
            return dataTable;
        }

        public string ShipBy(string shipby, int nShpMethod = 0)
        {
            string shipvia = "None";
            bool isFedex = nShpMethod == 1;
            bool isUPS = nShpMethod == 2;
            bool isUSPS = nShpMethod == 10;
            if (isFedex)
            {
                switch (shipby)
                {
                    case "FO":
                        shipvia = "First Overnight";
                        break;
                    case "PO":
                        shipvia = "Priority Overnight";
                        break;
                    case "SO":
                        shipvia = "Standard Overnight";
                        break;
                    case "2A":
                        shipvia = "2Day A.M.";
                        break;
                    case "2":
                        shipvia = "2Day";
                        break;
                    case "E":
                        shipvia = "Express Saver";
                        break;
                    case "G":
                        shipvia = "Ground";
                        break;
                    case "H":
                        shipvia = "Home Delivery";
                        break;
                    case "GE":
                        shipvia = "Ground Economy";
                        break;
                    case "SF":
                        shipvia = "Saturday First Overnight";
                        break;
                    case "SP":
                        shipvia = "Saturday Priority Overnight";
                        break;

                }
            }
            else if (isUPS)
            {
                switch (shipby)
                {
                    case "E":
                        shipvia = "Next Day Air Early";
                        break;
                    case "A":
                        shipvia = "Next Day Air";
                        break;
                    case "S":
                        shipvia = "Next Day Air Saver";
                        break;
                    case "2A":
                        shipvia = "2nd Day Air A.M.";
                        break;
                    case "2":
                        shipvia = "2nd Day Air";
                        break;
                    case "3":
                        shipvia = "3 Day Select";
                        break;
                    case "G":
                        shipvia = "Ground";
                        break;
                    case "SD":
                        shipvia = "Saturday Delivery";
                        break;
                }
            }
            else if (isUSPS)
            {
                switch (shipby)
                {
                    case "E":
                        shipvia = "Priority Mail Express";
                        break;
                    case "M":
                        shipvia = "Priority Mail";
                        break;
                    case "G":
                        shipvia = "Ground Advantage";
                        break;
                    case "C":
                        shipvia = "First-Class Mail";
                        break;
                }

            }
            return shipvia;
        }

        public DataTable GetProformaDeposit(string Perrforma_Inv)
        {
            return GetSqlData(string.Format("SELECT SUM(ISNULL(PAID,0)) tot_dep FROM PAYMENTS with (nolock) WHERE PROFORMA={0}", Perrforma_Inv));
        }

        public DataTable GetSalesmanDetailsForProformaInvoice(string orderrepairno)
        {
            DataTable dataTable = new DataTable();
            using (SqlDataAdapter dataAdapter = new SqlDataAdapter())
            {
                dataAdapter.SelectCommand = new SqlCommand();
                dataAdapter.SelectCommand.Connection = _connectionProvider.GetConnection();
                dataAdapter.SelectCommand.CommandType = CommandType.Text;
                dataAdapter.SelectCommand.CommandText = @"SELECT salesman1,salesman2,comish1,comish2,COMISHAMOUNT1,COMISHAMOUNT2 from REPAIR where trim(REPAIR_NO)=(select top 1 trim(value) from string_split(@orderrepairno, ',') where trim(value) <> '' order by value)";
                dataAdapter.SelectCommand.Parameters.AddWithValue("@orderrepairno", orderrepairno.Trim());
                dataAdapter.Fill(dataTable);
            }
            return dataTable;
        }

        public string GetShippingTypeByIndex(int index)
        {
            if (Enum.IsDefined(typeof(ShippingType), index))
                return ((ShippingType)index).ToString();
            else
                return string.Empty;
        }

        public DataTable GetPackageStyles(string strPackage)
        {
            if (!string.IsNullOrWhiteSpace(strPackage))
                return GetSqlData("select pkg_style,style,Qty from Package  with (nolock) where trim(pkg_style) = @Package order by pkg_style", "@Package", strPackage);
            return GetSqlData("select distinct pkg_style from Package  with (nolock) order by pkg_style");
        }
        public DataRow CheckValidVendorCode(string acc)
        {
            return GetSqlRow("select * From vendors with (nolock) Where acc=@acc or oldvendorcode=@acc order by acc", "@acc", acc.Trim());
        }
        public string GetGLDESC(string glAcc)
        {
            DataTable dataTable = GetSqlData("select isnull(nullif(name,''),'') from gl_accs where trim(acc)=@glAcc", "@glAcc", glAcc.Trim());
            return GetValue0(dataTable);
        }
        public List<SelectListItem> GetAllAccnName()
        {
            List<SelectListItem> GlItem = new List<SelectListItem>();

            DataTable dtGLAccts = Getname();
            if (DataTableOK(dtGLAccts))
            {
                foreach (DataRow dr in dtGLAccts.Rows)
                {
                    string code = dr["ACC"].ToString().Trim();
                    string accountName = dr["NAME"].ToString().Trim();
                    GlItem.Add(new SelectListItem
                    {
                        Text = $"{code} - {accountName}",
                        Value = code
                    });
                }
            }
            return GlItem;
        }
        public List<SelectListItem> GetAllDept()
        {
            DataTable dataTable = new DataTable();
            dataTable = GetSqlData("SELECT DEPT From GL_DEPT with (nolock)");
            List<SelectListItem> GlDept = new List<SelectListItem>();
            GlDept.Add(new SelectListItem() { Text = "", Value = "" });
            if (dataTable.Rows.Count > 0)
            {
                foreach (DataRow dr in dataTable.Rows)
                {
                    GlDept.Add(new SelectListItem() { Text = dr["DEPT"].ToString().Trim(), Value = dr["DEPT"].ToString().Trim() });
                }
            }
            return GlDept;

        }
        public List<string> GetAllBankCode(string loggedstoreno = "")
        {
            DataTable dtbank = GetSqlData("EXEC getdefaultbank @loggedstoreno", "@loggedstoreno", loggedstoreno);

            List<string> Bankacc = dtbank.AsEnumerable()
                               .Select(row => row.Field<string>("code"))
                               .ToList();
            return Bankacc;
        }
        public bool IssueCheckWOBillOrVendor(string acc, string name, string chkNo, DateTime date, string bank, decimal amount,
            string glCode, string notes, out string invNo, out string error, string storeNo = "", string loggedUser = "",
            string billGl = null)
        {
            invNo = error = string.Empty;
            using (var connection = _connectionProvider.GetConnection())
            using (var dbCommand = new SqlCommand("IssueCheckWOBillOrVendor", connection))
            {
                // Set command properties
                dbCommand.CommandType = CommandType.StoredProcedure;
                dbCommand.CommandTimeout = 5000;

                // Add parameters
                dbCommand.Parameters.AddWithValue("@ACC", acc);
                dbCommand.Parameters.AddWithValue("@NAME", name);
                dbCommand.Parameters.AddWithValue("@CHK_NO", chkNo);
                dbCommand.Parameters.AddWithValue("@DATE", date);
                dbCommand.Parameters.AddWithValue("@BANK", bank);
                dbCommand.Parameters.AddWithValue("@AMOUNT", amount);
                dbCommand.Parameters.AddWithValue("@GL_CODE", glCode);
                dbCommand.Parameters.AddWithValue("@NOTES", notes);
                dbCommand.Parameters.AddWithValue("@STORENO", storeNo);
                dbCommand.Parameters.AddWithValue("@loggeduser", loggedUser);

                // Add optional XML parameter
                dbCommand.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@BILLGL",
                    SqlDbType = SqlDbType.Xml,
                    Value = billGl
                });

                // Add output parameter
                var outInvNo = new SqlParameter("@RETVAL", SqlDbType.NVarChar, 10)
                {
                    Direction = ParameterDirection.Output
                };
                dbCommand.Parameters.Add(outInvNo);

                // Open the connection and execute the command
                connection.Open();
                int rowsAffected = dbCommand.ExecuteNonQuery();
                invNo = (string)outInvNo.Value;

                return rowsAffected > 0;
            }
        }

        public DataTable GetInvntLogData(string logno)
        {
            return GetSqlData("SELECT INV_NO,DATE,FROM_STORE,STORE_NO,STYLE,QTY,ACKED FROM INC_STORE with (nolock) WHERE Trimmed_inv_no=TRIM(@CINV)",
                "@CINV", logno);
        }
        public bool UpdateAckDetails(string logno, string loggedUser, bool lDoGlTran, string ackLog,
            bool isSymphony = false, bool isAckMod = false, string dateOfAck = "", bool isStyleItem = false)
        {
            const string procedureName = "ACKSTORINVT";

            using (var connection = _connectionProvider.GetConnection())
            using (var command = new SqlCommand(procedureName, connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandTimeout = 5000;

                command.Parameters.Add(new SqlParameter("@TBLACKDATA", SqlDbType.Xml) { Value = ackLog });
                command.Parameters.AddWithValue("@CLOG", logno);
                command.Parameters.AddWithValue("@LoggedUser", loggedUser);
                command.Parameters.AddWithValue("@lDoGlTran", lDoGlTran);
                command.Parameters.AddWithValue("@IsSymphony", isSymphony);
                command.Parameters.AddWithValue("@isackmod", isAckMod);
                command.Parameters.AddWithValue("@DateofAck", dateOfAck);
                command.Parameters.AddWithValue("@IsStyleItem", isStyleItem);

                connection.Open();
                var rowsAffected = command.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }

        public DataTable GetEmpCodes()
        {
            return GetSqlData("SELECT code FROM employee with (nolock) ORDER BY code");
        }
        public DataTable GetNameByACC(string acc)
        {
            return GetSqlData(@"select ACC,Name from GL_ACCS where ACC=@acc", "@acc", acc.Trim());
        }
        public DataTable ToDataTable<T>(List<T> items)
        {
            DataTable dt = new DataTable(typeof(T).Name);
            if (items == null || items.Count == 0)
                return dt;

            var props = typeof(T).GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);

            foreach (var prop in props)
            {
                dt.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            }

            foreach (var item in items)
            {
                var values = new object[props.Length];
                for (int i = 0; i < props.Length; i++)
                {
                    values[i] = props[i].GetValue(item, null);
                }
                dt.Rows.Add(values);
            }
            return dt;
        }
        public DataRow CheckValidBillingAcct(string billacc)
        {
            return GetSqlRow("select *  From Customer with (nolock) Where rtrim(bill_acc)= rtrim(@bill_acc) and bill_acc = acc", "@bill_acc", billacc);
        }
        public DataRow GetProformaInvoice(string invno)
        {
            return GetSqlRow("SELECT TOP 1 i.*, t.memo_no, t.by_wt FROM Proforma_INVOICE i LEFT JOIN Proforma_IN_ITEMS t ON i.inv_no = t.inv_no WHERE i.inv_no=@inv_no", "@inv_no", invno);
        }
        public decimal ShippingHandling(string czip, string cviaups, string cearly, string cstate, bool resident,
           decimal total, string viacod, int weight)
        {
            using (SqlConnection connection = _connectionProvider.GetConnection())
            using (SqlCommand command = new SqlCommand("GetShipping", connection))
            {
                // Set the command properties																 
                command.CommandType = CommandType.StoredProcedure;

                // Add input parameters
                command.Parameters.AddWithValue("@CZIP", czip);
                command.Parameters.AddWithValue("@CVIAUPS", cviaups);
                command.Parameters.AddWithValue("@CSTATE", cstate);
                command.Parameters.AddWithValue("@CEARLY", cearly);
                command.Parameters.AddWithValue("@RES", resident);
                command.Parameters.AddWithValue("@TOTAL", total);
                command.Parameters.AddWithValue("@VIACOD", viacod);
                command.Parameters.AddWithValue("@WEIGHT", weight);

                // Add output parameter
                SqlParameter outShipHandling = new SqlParameter("@RETVAL", SqlDbType.Decimal)
                {
                    Direction = ParameterDirection.Output
                };
                command.Parameters.Add(outShipHandling);
                connection.Open();
                command.ExecuteNonQuery();

                // Return 0 if the value is DBNull or null
                return outShipHandling.Value != DBNull.Value ? (decimal)outShipHandling.Value : 0;
            }
        }

        public string ShipViaChar(string ShipVia_Label, int nShpMethod = 0)
        {
            string ShipVia_Char = "N";
            bool isFedex = nShpMethod == 1;
            bool isUPS = nShpMethod == 2;
            bool isUSPS = nShpMethod == 10;

            if (isFedex)
            {
                switch (ShipVia_Label)
                {
                    case "First Overnight":
                        ShipVia_Char = "FO";
                        break;
                    case "Priority Overnight":
                        ShipVia_Char = "PO";
                        break;
                    case "Standard Overnight":
                        ShipVia_Char = "SO";
                        break;
                    case "2Day A.M.":
                        ShipVia_Char = "2A";
                        break;
                    case "2Day":
                        ShipVia_Char = "2";
                        break;
                    case "Express Saver":
                        ShipVia_Char = "E";
                        break;
                    case "Ground":
                        ShipVia_Char = "G";
                        break;
                    case "Home Delivery":
                        ShipVia_Char = "H";
                        break;
                    case "Ground Economy":
                        ShipVia_Char = "GE";
                        break;
                    case "Saturday First Overnight":
                        ShipVia_Char = "SF";
                        break;
                    case "Saturday Priority Overnight":
                        ShipVia_Char = "SP";
                        break;
                }
            }
            else if (isUPS)
            {
                switch (ShipVia_Label)
                {
                    case "Next Day Air Early":
                        ShipVia_Char = "E";
                        break;
                    case "Next Day Air":
                        ShipVia_Char = "A";
                        break;
                    case "Next Day Air Saver":
                        ShipVia_Char = "S";
                        break;
                    case "2nd Day Air A.M.":
                        ShipVia_Char = "2A";
                        break;
                    case "2nd Day Air":
                        ShipVia_Char = "2";
                        break;
                    case "3 Day Select":
                        ShipVia_Char = "3";
                        break;
                    case "Ground":
                        ShipVia_Char = "G";
                        break;
                    case "Saturday Delivery":
                        ShipVia_Char = "SD";
                        break;
                }
            }
            else if (isUSPS)
            {
                switch (ShipVia_Label)
                {
                    case "Priority Mail Express":
                        ShipVia_Char = "ME";
                        break;
                    case "Priority Mail":
                        ShipVia_Char = "M";
                        break;
                    case "Ground Advantage":
                        ShipVia_Char = "G";
                        break;
                    case "First-Class Mail":
                        ShipVia_Char = "C";
                        break;
                }
            }
            return ShipVia_Char;
        }

        public bool AddProformaInvoice(ProformaInvoiceModel pinvoice, string invoiceItems, string boxinfo, string logggeduser, bool is_edit, decimal? gold_price, out string out_inv_no)
        {
            using (SqlCommand dbCommand = new SqlCommand())
            {
                dbCommand.Connection = _connectionProvider.GetConnection();
                dbCommand.CommandType = CommandType.StoredProcedure;
                dbCommand.CommandText = "AddProformaInvoice";
                dbCommand.CommandTimeout = 5000;
                Object invoicedate;

                if (pinvoice.DATE == null)
                    invoicedate = DBNull.Value;
                else
                    invoicedate = pinvoice.DATE;

                dbCommand.Parameters.AddWithValue("@INV_NO", pinvoice.INV_NO);
                dbCommand.Parameters.AddWithValue("@BACC", pinvoice.BACC);
                dbCommand.Parameters.AddWithValue("@ACC", pinvoice.ACC);
                dbCommand.Parameters.AddWithValue("@ADD_COST", pinvoice.ADD_COST);
                dbCommand.Parameters.AddWithValue("@DEDUCTION", pinvoice.DEDUCTION);
                dbCommand.Parameters.AddWithValue("@SNH", pinvoice.SNH);
                dbCommand.Parameters.AddWithValue("@DATE", invoicedate);
                dbCommand.Parameters.AddWithValue("@PON", pinvoice.PON);
                dbCommand.Parameters.AddWithValue("@MESSAGE", pinvoice.MESSAGE);
                dbCommand.Parameters.AddWithValue("@GR_TOTAL", pinvoice.GR_TOTAL);
                dbCommand.Parameters.AddWithValue("@ADDR1", pinvoice.ADDR1);
                dbCommand.Parameters.AddWithValue("@ADDR2", pinvoice.ADDR2);
                dbCommand.Parameters.AddWithValue("@ADDR3", pinvoice.ADDR3);
                dbCommand.Parameters.AddWithValue("@CITY", pinvoice.CITY);
                dbCommand.Parameters.AddWithValue("@STATE", pinvoice.STATE);
                dbCommand.Parameters.AddWithValue("@ZIP", pinvoice.ZIP);
                dbCommand.Parameters.AddWithValue("@COUNTRY", pinvoice.COUNTRY);
                dbCommand.Parameters.AddWithValue("@VIA_UPS", pinvoice.VIA_UPS);
                dbCommand.Parameters.AddWithValue("@IS_COD", pinvoice.IS_COD);
                dbCommand.Parameters.AddWithValue("@WEIGHT", pinvoice.WEIGHT);
                dbCommand.Parameters.AddWithValue("@COD_TYPE", pinvoice.COD_TYPE);
                dbCommand.Parameters.AddWithValue("@CUST_PON", pinvoice.CUST_PON);
                dbCommand.Parameters.AddWithValue("@SHIP_BY", pinvoice.SHIP_BY);
                dbCommand.Parameters.AddWithValue("@TERM", pinvoice.TERM);
                dbCommand.Parameters.AddWithValue("@OPERATOR", pinvoice.OPERATOR);
                dbCommand.Parameters.AddWithValue("@NAME", pinvoice.NAME);
                dbCommand.Parameters.AddWithValue("@EARLY", pinvoice.EARLY);
                dbCommand.Parameters.AddWithValue("@INSURED", pinvoice.INSURED);
                dbCommand.Parameters.AddWithValue("@PERCENT", pinvoice.PERCENT);
                dbCommand.Parameters.AddWithValue("@MAN_SHIP", pinvoice.MAN_SHIP == true ? 1 : 0);
                dbCommand.Parameters.AddWithValue("@RESIDENT", pinvoice.RESIDENT);
                dbCommand.Parameters.AddWithValue("@IS_FDX", pinvoice.IS_FDX == true ? 1 : 0);
                dbCommand.Parameters.AddWithValue("@IS_DEB", pinvoice.IS_DEB == true ? 1 : 0);
                dbCommand.Parameters.AddWithValue("@SAMPLE", pinvoice.SAMPLE);
                dbCommand.Parameters.AddWithValue("@LOGGEDINUSER", logggeduser);
                dbCommand.Parameters.AddWithValue("@SHIPTYPE", pinvoice.SHIPTYPE);
                dbCommand.Parameters.AddWithValue("@GOLD_PRICE", gold_price);
                dbCommand.Parameters.AddWithValue("@TrackNo", pinvoice.UPSTRAK);
                dbCommand.Parameters.AddWithValue("@IS_UPDATE", is_edit == true ? 1 : 0);
                dbCommand.Parameters.AddWithValue("@NO_BOX", pinvoice.NO_BOX);
                dbCommand.Parameters.AddWithValue("@IS_CCRD", pinvoice.CCRD);
                dbCommand.Parameters.AddWithValue("@DP", pinvoice.DP);
                dbCommand.Parameters.AddWithValue("@DED_PER", pinvoice.DED_PER);
                dbCommand.Parameters.AddWithValue("@TYPE", pinvoice.TYPE);
                dbCommand.Parameters.AddWithValue("@LAPTOP", pinvoice.LAPTOP);
                dbCommand.Parameters.AddWithValue("@STORE", pinvoice.STORE_NO);

                SqlParameter parameter = new SqlParameter();
                parameter.ParameterName = "@TBLPOINVOICEITEMS";
                parameter.SqlDbType = System.Data.SqlDbType.Xml;
                parameter.Value = invoiceItems;
                dbCommand.Parameters.Add(parameter);

                parameter = new SqlParameter();
                parameter.ParameterName = "@TBLBOXINFO";
                parameter.SqlDbType = System.Data.SqlDbType.Xml;
                parameter.Value = boxinfo;
                dbCommand.Parameters.Add(parameter);

                dbCommand.Parameters.Add("@OUT_INV_NO", SqlDbType.VarChar, 6).Direction = ParameterDirection.Output;

                dbCommand.Connection.Open();
                var rowsAffected = dbCommand.ExecuteNonQuery();
                out_inv_no = Convert.ToString(dbCommand.Parameters["@OUT_INV_NO"].Value);

                dbCommand.Connection.Close();

                return rowsAffected > 0;
            }
        }

        public string RemoveBraceAndHyphenForTelFax(string TelFaxNo = "")
        {
            return new string(TelFaxNo.Where(c => !"( )-".Contains(c)).ToArray()).Trim();
        }
        public DataTable getChecksalesman(string scode)
        {
            return GetSqlData("select * from salesmen with(nolock) where code = @scode", "@scode", scode);
        }
        public SqlDataReader GetSubEvents(string[] tblname, string[] fldname, string events = "")
        {
            if (tblname == null || fldname == null || tblname.Length != fldname.Length)
                throw new ArgumentException("SqlDataReader: Table and field name arrays must be non-null and have the same length.");

            var queryBuilder = new StringBuilder();

            for (int i = 0; i < tblname.Length; i++)
            {
                string tableName = tblname[i];
                string fieldName = fldname[i];

                if (tableName == "EVENTS")
                {
                    queryBuilder.AppendLine(
                        $"SELECT '{tableName}' AS TableName, '' AS FieldValue " +
                        $"UNION SELECT '{tableName}', TRIM({fieldName}) FROM {tableName} WHERE MAINEVENT = @events " +
                        "UNION ");
                }
                else
                {
                    queryBuilder.AppendLine(
                        $"SELECT '{tableName}' AS TableName, '' AS FieldValue " +
                        $"UNION SELECT '{tableName}', TRIM({fieldName}) FROM {tableName} " +
                        "UNION ");
                }
            }
            string query = queryBuilder.ToString().TrimEnd("UNION ".ToCharArray());
            int LastUnionIndex = query.LastIndexOf("UNION", StringComparison.OrdinalIgnoreCase);
            if (LastUnionIndex != -1)
                query = query.Substring(0, LastUnionIndex);
            query += " ORDER BY 1";
            var connection = _connectionProvider.GetConnection();
            var command = new SqlCommand(query, connection)
            {
                CommandType = CommandType.Text
            };

            if (!string.IsNullOrEmpty(events))
                command.Parameters.AddWithValue("@events", events);
            connection.Open();
            return command.ExecuteReader(CommandBehavior.CloseConnection);
        }

        public DataTable GetPInvoiceItemsByInvNo(string inv_no)
        {
            return GetStoreProc("GetPInvoiceItemsByInvNo", "@INV_NO", inv_no);
        }

        public DataRow StyleinfoBasedonStore(string stylno, string storeno, string Option = "", string StrDate = "")
        {
            DataTable dataTable = null;
            if (Option == "EditInventoryAs")
                dataTable = GetSqlData("select sum(change) as in_stock from STK_HIST  where ltrim(Rtrim(STYLE))=ltrim(Rtrim(@stylno))  and  TRIM(store_no)=TRIM(@Storeno) AND cast(DATE as date) <= cast(@Date as Date) Group  by STYLE"
                    , "@stylno", stylno.Trim(), "@Storeno", storeno, "@Date", StrDate);
            else
                dataTable = GetSqlData("SELECT ST.*,ISNULL(S.COST,0)COST FROM STOCK ST,STYLES S WHERE ST.STYLE=S.STYLE AND TRIM(ST.STYLE)=TRIM(@CSTYL) AND TRIM(ST.STORE_NO)=TRIM(@CSTORE) AND ST.STORE_NO<>''",
                    "@CSTYL", stylno, "@CSTORE", storeno);
            return GetRowOne(dataTable);
        }

        public void AddStockInventory(DataTable data, string loggedUser, string storeNo, bool isStyleItem = false)
        {
            // Validate inputs early
            if (data == null)
                throw new ArgumentNullException(nameof(data));
            if (string.IsNullOrWhiteSpace(loggedUser))
                throw new ArgumentException("Logged user cannot be null or empty.", nameof(loggedUser));
            if (string.IsNullOrWhiteSpace(storeNo))
                throw new ArgumentException("Store number cannot be null or empty.", nameof(storeNo));

            // Use `using` to ensure resources are released properly
            using (var connection = _connectionProvider.GetConnection())
            using (var command = new SqlCommand("AddStockInventory", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandTimeout = 6000; // Set timeout as needed

                // Add parameters with proper types
                command.Parameters.Add(new SqlParameter("@TBLRTVITEMS", SqlDbType.Structured) { Value = data });
                command.Parameters.Add(new SqlParameter("@LOGGEDUSER", SqlDbType.NVarChar, 50) { Value = loggedUser });
                command.Parameters.Add(new SqlParameter("@CSTORE", SqlDbType.NVarChar, 10) { Value = storeNo });
                command.Parameters.Add(new SqlParameter("@IsStyleItem", SqlDbType.Bit) { Value = isStyleItem });

                connection.Open();

                // Using a DataAdapter to fill a DataTable
                using (var adapter = new SqlDataAdapter(command))
                {
                    var resultTable = new DataTable();
                    adapter.Fill(resultTable);
                }
            }
        }

        public DataTable getstoresdata()
        {
            return GetSqlData(@"select CODE, DEPT, NAME, ADDR1, ADDR2, ADDR3, ADDR4, TEL, SALES_TAX,CCMID,CCHSN,CCUSERNAME,CCPASSWD, INACTIVE, bank_acc, feedback_link, STORE_LOGO,invoicenote,INVOICESMSTEXT,REPAIRSMSTEXT,sq_location,deposit_bank,scrap_bank,city,state,zip from stores with (nolock) order by code");
        }
        public DataTable GetAllDepts()
        {
            return GetSqlData("select dept from gl_dept order by dept");
        }
        public DataTable GetBankAccounts()
        {
            return GetSqlData("SELECT '' AS bank_Acc UNION select distinct CODE as bank_acc from bank_Acc with (nolock) ORDER BY bank_acc");
        }
        public DataTable GetBankAccShow(string scrap = "")
        {
            if (scrap != "")
                return GetSqlData("SELECT '' AS Scrap_bank UNION select distinct CODE as Scrap_bank from bank_Acc with (nolock) ORDER BY Scrap_bank");
            return GetSqlData("SELECT '' AS deposit_bank UNION select distinct CODE as deposit_bank from bank_Acc with (nolock) ORDER BY deposit_bank");
        }
        public DataTable GetInactiveStoresFromUpsIns()
        {
            return GetSqlData(@"select no_stores from ups_ins with (nolock)");
        }
        public string RemoveSpecialCharactersspace(string str)
        {
            return Regex.Replace(str, @"[~`!@#$%^&*()-+=|\':;.,<>/?]", "", RegexOptions.Compiled);
        }
        public DataTable AddStoreData(string leveldet)
        {
            return GetStoreProc("ADDSTORES", "@STORETAB", leveldet);
        }
        public DataTable DeleteStore(string code)
        {
            return GetSqlData(@"delete from stores with (rowlock) where code = TRIM('" + code.Replace("'", "''") + "') ");
        }
        public string TelFotmatSetforTextbox(string telValue, string AccValue = "", string StrFormat = "")
        {
            if (string.IsNullOrWhiteSpace(telValue))
                return string.Empty;

            string mask = string.IsNullOrEmpty(StrFormat) ? mask_tel : StrFormat;
            string cleanedTel = RemoveSpecialCharactersspace(telValue).Replace(" ", "");

            if (is_Zhaveri)
            {
                cleanedTel = cleanedTel.Replace("(", "").Replace(")", "").Replace("-", "").Replace(".", "");
                cleanedTel = string.IsNullOrEmpty(cleanedTel) ? "" : "1" + cleanedTel.Trim();
                mask = "#.###.###.####";
            }

            return SetTellMaskHere(cleanedTel.Trim(), mask, AccValue);
        }

        public string SetTellMaskHere(string Tellnumber, string mask, string acc = "")
        {
            if (string.IsNullOrWhiteSpace(Tellnumber) || Tellnumber == "0")
            {
                if (!string.IsNullOrEmpty(acc))
                    Tellnumber = GetCustTel(acc);

                if (string.IsNullOrEmpty(Tellnumber))
                    return string.Empty;
            }

            Tellnumber = RemoveSpecialCharactersspace(Tellnumber.Trim()).Replace(" ", "");

            if (Tel_0_prefix.ToString() == "True" && Tellnumber.Length < 10)
                Tellnumber = "0" + Tellnumber;

            // Apply simple mask if needed
            string formatted = Tellnumber;
            if (!string.IsNullOrEmpty(mask) && Tellnumber.Length >= 10)
            {
                try
                {
                    // Example: mask = "(###) ###-####"
                    formatted = ApplyMask(Tellnumber, mask);
                }
                catch { /* fallback to raw */ }
            }

            return formatted;
        }

        private string ApplyMask(string input, string mask)
        {
            // Replaces # in mask with digits from input
            char[] result = mask.ToCharArray();
            int i = 0;
            for (int j = 0; j < result.Length && i < input.Length; j++)
            {
                if (result[j] == '#')
                    result[j] = input[i++];
            }
            return new string(result);
        }

        public bool RenameStorename(string oldStoreName, string newStoreName = "")
        {
            using (var connection = _connectionProvider.GetConnection())
            using (var dbCommand = new SqlCommand("RenameStore", connection))
            {
                dbCommand.CommandType = CommandType.StoredProcedure;

                dbCommand.Parameters.AddWithValue("@StroldStore", oldStoreName);
                dbCommand.Parameters.AddWithValue("@StrStoreName", newStoreName);

                connection.Open();
                return dbCommand.ExecuteNonQuery() > 0;
            }
        }
        public DataTable AddStyleImage(byte[] fileData, string storeCode, string filename)
        {
            using (SqlConnection con = _connectionProvider.GetConnection())
            {
                using (SqlDataAdapter da = new SqlDataAdapter("AddStyleImage", con))
                {
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.Add("@fData", SqlDbType.Image).Value = fileData;
                    da.SelectCommand.Parameters.Add("@style", SqlDbType.NVarChar).Value = storeCode;
                    da.SelectCommand.Parameters.Add("@desc", SqlDbType.NVarChar).Value = filename;
                    da.SelectCommand.Parameters.Add("@imgfilename", SqlDbType.NVarChar).Value = filename;
                    da.SelectCommand.Parameters.Add("@is_default", SqlDbType.Bit).Value = true;
                    da.SelectCommand.Parameters.Add("@orig_name", SqlDbType.NVarChar).Value = filename;
                    da.SelectCommand.Parameters.Add("@lastfolderpath", SqlDbType.NVarChar).Value = "";
                    da.SelectCommand.Parameters.Add("@use_orginname", SqlDbType.Bit).Value = true;

                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    return dt;
                }
            }
        }

        public Tuple<byte[], int> GetImageData(string storeCode)
        {
            using (SqlConnection con = _connectionProvider.GetConnection())
            using (SqlCommand cmd = new SqlCommand("GetImage", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@stylename", storeCode);
                con.Open();

                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    if (rdr.Read())
                    {
                        return new Tuple<byte[], int>(
                            (byte[])rdr[0],
                            Convert.ToInt32(rdr[1])
                        );
                    }
                }
            }

            return null;
        }

        public void ExecuteNonQuery(string query, params SqlParameter[] parameters)
        {
            using (SqlConnection con = _connectionProvider.GetConnection())
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                if (parameters != null)
                    cmd.Parameters.AddRange(parameters);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }
        public DataTable savedefaultstores(string store1, string store2, string store3, string store4)
        {
            return GetSqlData("update ups_ins set store ='" + store1 + "',store2='" + store2 + "',store3='" + store3 + "',store4='" + store4 + "'");
        }
        public DataTable getdefaultstores()
        {
            return GetSqlData("select store,store2,store3,store4 from ups_ins with (nolock)");
        }
        public DataTable GetStoreText(string code)
        {
            return GetSqlData(@"select distinct CODE, INVOICESMSTEXT, REPAIRSMSTEXT from STORES where code ='" + code.Trim() + "'");
        }
        public bool CheckActiveStyle(string invStyle)
        {
            return DataTableOK(GetSqlData("SELECT style FROM styles with (nolock) WHERE is_active=1 and style = @invStyle ", "@invStyle", invStyle));
        }

        public DataRow GetPriceByPriceFile(string prcfile, string styleno)
        {
            return GetSqlRow("SELECT price FROM prices with (nolock) WHERE file_no=@prcfile AND style=DBO.InvStyle(@styleno)", "@prcfile", prcfile, "@styleno", styleno);
        }

        public DataTable GetOptionData(string TblName, string fldname)
        {
            if (TblName == "CLARITIES")
                return GetSqlData("SELECT  TRIM(clarity) as optionname ,cast([order] as int) as [order],1 AS STATUS FROM CLARITIES order by [order]");
            if (TblName == "SUBCATS1")
                return GetSqlData("SELECT SUBCATEGORY AS optionname,1 AS STATUS  FROM SUBCATS WHERE CATEGORY= '" + fldname + "' ORDER BY 1");
            if (TblName == "DCOLORS" && fldname == "COLOR")
                return GetSqlData($"SELECT TRIM(color) as optionname,setorder,1 AS STATUS FROM DCOLORS order by setorder,color");
            return GetSqlData("SELECT TRIM(" + fldname + ") as optionname,1 AS STATUS FROM " + TblName + " order by " + fldname);
        }
        public DataTable AddNewOption(string TblName, string colname, string user, string leveldet, bool bullion = false, bool isPriceBasedOnGold = false)
        {
            return GetStoreProc("AddOrEditOptionValues", "@PLEVEL", TblName, "@FLDNAME", colname, "@LOGGEDUSER", user, "@BULLION", bullion.ToString(), "@OptionTable", leveldet, "@IsPriceBasedOnGold", isPriceBasedOnGold.ToString());
        }
        public DataTable DeleteOptionVal(string TblName, string fldname, string fldvalue)
        {
            return GetSqlData(@"delete from " + TblName + " where [" + fldname + "] = '" + fldvalue.Replace("'", "''") + "' ");
        }
        public DataTable GetItemTypes()
        {
            return GetSqlData(@"select * from ITEMTYPE with (nolock) order by 1 ");
        }
        public bool UpdateItemtypesValues(string tableName, String fieldName, string newValue, string oldValue, string CombineItemtype = "", string Opt_to = "", string Opt_frm = "")
        {
            try
            {
                using (SqlCommand dbCommand = new SqlCommand())
                {
                    dbCommand.Connection = _connectionProvider.GetConnection();
                    dbCommand.CommandType = CommandType.StoredProcedure;
                    dbCommand.CommandText = CombineItemtype == "Combine" ? "CombineItemType" : "UpdateItemTypeValues";
                    if (CombineItemtype == "Combine")
                    {
                        dbCommand.Parameters.AddWithValue("@OPTION", "ItemType");
                        dbCommand.Parameters.AddWithValue("@OPT_TO", Opt_to);
                        dbCommand.Parameters.AddWithValue("@OPT_FROM", Opt_frm);
                        dbCommand.Parameters.AddWithValue("@LOGGEDUSER", LoggedUser);
                    }
                    else
                    {
                        dbCommand.Parameters.AddWithValue("@tableName", tableName);
                        dbCommand.Parameters.AddWithValue("@fieldName", fieldName);
                        dbCommand.Parameters.AddWithValue("@newValue", newValue);
                        dbCommand.Parameters.AddWithValue("@oldValue", oldValue);
                    }
                    dbCommand.Connection.Open();
                    var rowsAffected = dbCommand.ExecuteNonQuery();
                    dbCommand.Connection.Close();

                    return rowsAffected > 0;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
        public DataRow Store_invnt(string cStoreNo, string cstylez)
        {
            return GetRowOne(GetSqlData(@"select (cast(isnull(in_stock,0) as decimal(8,0))-cast(isnull(LAYAWAY,0) as decimal(8,0))-cast(isnull(in_shop,0) as decimal(8,0))-cast(isnull(transit_out,0) as decimal(8,0))) as instock, 
                                        cast(transit_out as decimal(8,2)) as in_transit, cast(transit_in as decimal(8,0)) as transit_in 
                                        from stock with (nolock) where style=@style and store_no=@store", "@style", cstylez, "@store", cStoreNo));
        }
        public DataTable GetItemsFromBill(string billno, bool isApm)
        {
            if (isApm)
                return GetSqlData(@"SELECT STYLE, PCS, STORE_NO FROM APM_ITEM WHERE Trimmed_inv_no = '" + billno.Trim() + "' order by len(style),style asc");
            return GetSqlData(@"SELECT STYLE, PCS, STORE_NO FROM BIL_ITEM WHERE Trimmed_inv_no = '" + billno.Trim() + "' order by len(style),style asc");
        }
        public DataRow Checkattrused(string TblName, string colname, string colvalue)
        {
            return GetSqlRow(@"SELECT * FROM " + TblName + " WHERE " + colname + " = @colvalue ", "@colvalue", colvalue);
        }
        public bool SaveXmlValues(string xml)
        {
            using (var connection = _connectionProvider.GetConnection())
            using (var sqlCommand = new SqlCommand("Item_typeDetails", connection))
            {
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@ITEMTYPE",
                    SqlDbType = SqlDbType.Xml,
                    Value = xml
                });

                connection.Open();
                return sqlCommand.ExecuteNonQuery() > 0;
            }
        }
        public DataTable ConvertXmlToDataTable(string xmlData)
        {
            if (string.IsNullOrWhiteSpace(xmlData))
                return null;
            try
            {
                using (StringReader stringReader = new StringReader(xmlData))
                {
                    DataSet ds = new DataSet();
                    ds.ReadXml(stringReader);
                    if (ds.Tables.Count > 0)
                        return ds.Tables[0];
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error converting XML to DataTable: " + ex.Message);
            }
            return null;
        }
        public DataTable GetValuesSendTablenameandcolumnname(string tblname, string fldname, string aliasname = "", string conditionName = "", string conditionVlues = "", bool normaltable = false)// this method we use for anywr just passcolumn name and table name 
        {
            if (normaltable && conditionName == "" && conditionVlues == "")
                return GetSqlData($"SELECT distinct isnull(RTRIM({fldname}),'') as {aliasname}  FROM {tblname}  order by {fldname}");
            else if (normaltable)
                return GetSqlData($"SELECT TRIM({fldname}) as {aliasname}  FROM {tblname} where TRIM({conditionName})=TRIM('{conditionVlues}')  order by {fldname}");
            return GetSqlData($"SELECT TRIM({fldname}) as ItemType  FROM {tblname}  order by {fldname}");
        }
        public DataTable UpdateHideForItemType(int DBbit, string ItemType)
        {
            return GetSqlData(@"UPDATE ITEMTYPE SET HIDE=" + DBbit.ToString() + " WHERE ITEM_TYPE =" + @"'"
                                     + ItemType.Replace("'", "''") + @"'" + "AND HIDE<>" + DBbit);
        }
        public DataTable AddNewClarityOption(string TblName, string colname, string user, DataTable leveldet)
        {
            var dataTable = new DataTable();
            using (var connection = _connectionProvider.GetConnection())
            using (var command = new SqlCommand("AddOrEditClarityOptionValues", connection))
            using (var adapter = new SqlDataAdapter(command))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@PLEVEL", TblName);
                command.Parameters.AddWithValue("@FLDNAME", colname);
                command.Parameters.AddWithValue("@LOGGEDUSER", user);
                command.Parameters.AddWithValue("@TBLCLARITYOPTIONFILE", leveldet);
                adapter.Fill(dataTable);
            }
            return dataTable;
        }
        public bool ManageItemType(string action, string itemNameOrOldType, string newTypeValue = null)
        {
            if (string.IsNullOrWhiteSpace(action) || string.IsNullOrWhiteSpace(itemNameOrOldType))
                return false;

            string query = string.Empty;

            string oldVal = itemNameOrOldType.Replace("'", "''");
            string newVal = newTypeValue != null ? newTypeValue.Replace("'", "''") : string.Empty;

            switch (action.ToLower())
            {
                case "delete":
                    query = $"DELETE FROM ITEMTYPE WHERE ITEM_TYPE = '{oldVal}'";
                    break;

                case "update":
                    if (string.IsNullOrWhiteSpace(newTypeValue))
                        return false;
                    query = $"UPDATE styles SET ITEM_TYPE = '{newVal}' WHERE ITEM_TYPE = '{oldVal}'";
                    break;

                default:
                    return false;
            }

            using (SqlCommand dbCommand = new SqlCommand())
            {
                dbCommand.Connection = _connectionProvider.GetConnection();
                dbCommand.CommandType = CommandType.Text;
                dbCommand.CommandText = query;

                dbCommand.Connection.Open();
                int rowsAffected = dbCommand.ExecuteNonQuery();
                dbCommand.Connection.Close();

                return rowsAffected > 0;
            }
        }
        public DataTable GetStyleUpsIns1()
        {
            return GetSqlData("SELECT StyleCost1,StyleCost2,StyleCost3,StyleCost4,StyleCost5,StyleCost6,StyleCost7,StyleCost8,StyleCost9,StyleCost10,Gift_ExpDays,RepairDays,cellno,Finance_Day,Late_Fee,Esp_Rules,item_cost_plus,Stone_cost_plus,hourlycharge,NextRoundOff,sign_below, ccFee, silverFee, ss_cost_plus,ETSY_CLIENT_ID,ETSY_CLIENT_SECRET,ETSY_CALLBACK_URL,ETSY_SHOP_ID,NoChangeBefore,referred_per,Loyalty_max_per FROM UPS_INS1 with(nolock)");
        }
        public bool UpdateDefaultValue(string[] fieldnames, object[] fieldvalues)
        {
            string updatequery = string.Format("Update Ups_Ins set ");
            int ctr = 0;
            foreach (string fld in fieldnames)
            {
                updatequery += string.Format("{0}=", fld);
                switch (fieldvalues[ctr].GetType().ToString())
                {

                    case "System.String":
                        updatequery += string.Format("'{0}',", fieldvalues[ctr]);
                        break;
                    case "System.Int32":
                        updatequery += string.Format("{0},", fieldvalues[ctr]);
                        break;
                    case "System.Decimal":
                        updatequery += string.Format("{0},", fieldvalues[ctr]);
                        break;
                    case "System.Boolean":
                        updatequery += string.Format("{0},", System.Convert.ToBoolean(fieldvalues[ctr]) ? 1 : 0);
                        break;
                    case "System.DateTime":
                        updatequery += string.Format("'{0},'", System.Convert.ToDateTime(fieldvalues[ctr]).ToString("YYYYMMDD"));
                        break;
                    default:
                        updatequery += string.Format("'{0},'", fieldvalues[ctr]);
                        break;
                }

                ctr++;
            }
            updatequery = updatequery.Trim(',');
            try
            {
                using (SqlCommand dbCommand = new SqlCommand())
                {
                    // Set the command object properties
                    dbCommand.Connection = _connectionProvider.GetConnection();
                    dbCommand.CommandType = CommandType.Text;
                    dbCommand.CommandText = updatequery;

                    dbCommand.Connection.Open();
                    var rowsAffected = dbCommand.ExecuteNonQuery();
                    dbCommand.Connection.Close();

                    return rowsAffected > 0;
                }
            }
            catch (Exception)
            {
                return false;
            }

        }
        public bool UpdateStyleCost(params string[] styleCosts)
        {
            if (styleCosts == null || styleCosts.Length != 10)
                throw new ArgumentException("Exactly 10 style cost values must be provided.", nameof(styleCosts));

            using (var connection = _connectionProvider.GetConnection())
            using (var command = new SqlCommand())
            {
                command.Connection = connection;
                command.CommandType = CommandType.Text;
                command.CommandText = @"UPDATE ups_ins1 
                                SET StyleCost1=@StyleCost1, StyleCost2=@StyleCost2, StyleCost3=@StyleCost3, 
                                    StyleCost4=@StyleCost4, StyleCost5=@StyleCost5, StyleCost6=@StyleCost6, 
                                    StyleCost7=@StyleCost7, StyleCost8=@StyleCost8, StyleCost9=@StyleCost9, 
                                    StyleCost10=@StyleCost10";

                for (int i = 0; i < styleCosts.Length; i++)
                {
                    command.Parameters.Add(new SqlParameter($"@StyleCost{i + 1}", SqlDbType.NVarChar) { Value = styleCosts[i] ?? (object)DBNull.Value });
                }

                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }
        public DataTable GetChangeOptionData(string TblName, string fldname, bool bullion)
        {
            if (Is_Sona && TblName == "METALS")
                return GetSqlData($"SELECT METAL as optionname, MULTIPLIER as [MULTIPLIER] FROM " + TblName + " where bullion = 0 order by METAL");

            if (CheckModuleEnabled(Modules.PriceBasedOnGold) && TblName == "METALS")
                return GetSqlData($"SELECT METAL as optionname, CAST(ISNULL(LABOR,0) AS DECIMAL(6,2)) LABOR, CAST(ISNULL(MULTIPLIER,0) AS DECIMAL(8,5)) AS [MULTIPLIER] FROM " + TblName + " where bullion = 0 order by METAL");

            if (is_Singer && TblName == "METALS")
                return GetSqlData($"SELECT UPPER(METAL) as optionname, CODE FROM " + TblName + " where bullion = 0 order by METAL");

            if (Is_Sona && TblName == "CATS")
                return GetSqlData($"SELECT CATEGORY optionname, PREMIUM, PROFIT, LABOR FROM " + TblName + " where bullion = 0 order by CATEGORY");

            if (TblName == "GROUPS")
            {
                if (CheckModuleEnabled(Modules.PriceBasedOnGold))
                    return GetSqlData($"SELECT [group] as optionname,prefix,ISNULL(use_dash, 0) as use_dash, ISNULL(Labor,0) Labor " + " FROM groups where bullion=" + (bullion ? "1" : "0") + " order by [group]");
                return GetSqlData($"SELECT [group] as optionname,prefix,ISNULL(use_dash, 0) as use_dash " + " FROM groups where bullion=" + (bullion ? "1" : "0") + " order by [group]");
            }

            if (TblName == "CLARITIES")
                return GetSqlData("SELECT  TRIM(clarity) as optionname ,cast([order] as int) as [order] " + " FROM CLARITIES order by [order]");

            if (bullion)
                return GetSqlData("SELECT TRIM(" + fldname + ") as optionname FROM " + TblName + " where bullion = 1 order by " + fldname + "");

            if (is_Singer && TblName == "CATS")
                return GetSqlData($"SELECT distinct category as optionname,TRIM(isnull([Group],'')) as [Group],TRIM(isnull([Group2],'')) as [Group2],TRIM(isnull([Group3],'')) as [Group3],LTRIM(RTRIM(isnull([Group4],''))) as [Group4],prefix,LTRIM(RTRIM(isnull([NOTES],''))) as [Description],LABOR, PRODUCTSTEM  FROM " + TblName + " where bullion = 0 order by category");

            if (TblName == "CATS")
                return GetSqlData($"SELECT distinct category as optionname,TRIM(isnull([Group],'')) as [Group],TRIM(isnull([Group2],'')) as [Group2],TRIM(isnull([Group3],'')) as [Group3],LTRIM(RTRIM(isnull([Group4],''))) as [Group4],prefix,LTRIM(RTRIM(isnull([NOTES],''))) as [Description],LABOR  FROM " + TblName + " where bullion = 0 order by category");

            if (TblName == "CASES")
                return GetSqlData("select case_no as optionname from (SELECT *, ROW_NUMBER() over(order by try_convert(int, case_no)) rno FROM CASES where ISNUMERIC(case_no) = 1 " + " union all SELECT *, 100000 + ROW_NUMBER() over(order by substring(case_no, 1, 1))rno FROM CASES where ISNUMERIC(case_no) = 0)a order by rno");

            if (CheckModuleEnabled(Modules.PriceBasedOnGold) && TblName == "CENTER_TYPES")
                return GetSqlData($"SELECT  CENTER_TYPE as optionname, CAST(ISNULL(LABOR,0) AS DECIMAL(6,2)) LABOR FROM CENTER_TYPES where bullion = 0 order by CENTER_TYPE");

            if (TblName == "dcolors" && fldname == "color")
                return GetSqlData($"SELECT  TRIM(color) as optionname,setorder FROM DCOLORS order by setorder, color");

            if (TblName == "CENTER_STYPES" || TblName == "METALCOLORS")
                return GetSqlData("SELECT TRIM(" + fldname + ") as optionname FROM " + TblName + " order by " + fldname);

            return GetSqlData("SELECT TRIM(" + fldname + ") as optionname FROM " + TblName + " where bullion = 0  order by " + fldname);
        }
        public DataTable GetChangeOptionDataWithItemGroup(string item_type, string group, bool iSBoolean)
        {
            return GetSqlData($"SELECT TRIM(CATEGORY) as optionname, TRIM([Group]) as [Group],TRIM([Group2]) as [Group2],TRIM([Group3]) as [Group3],TRIM([Group4]) as [Group4], Prefix, TRIM([NOTES]) as [Description], Labor FROM CATS with(nolock) where item_type= '{(item_type == string.Empty ? "Jewelry" : item_type)}' and [group]='{group}' and bullion='{(iSBoolean ? 1 : 0)}' order by CATEGORY");
        }
        public DataTable GetChangeOptionDataWithItem(string item_type, bool iSBoolean, bool isPriceBasedOnGold = false)
        {
            if (!isPriceBasedOnGold)
                return GetSqlData($"SELECT TRIM([GROUP]) as optionname,prefix,ISNULL(use_dash, 0) as use_dash from[dbo].[groups] with(nolock) where item_type= '{(string.IsNullOrWhiteSpace(item_type) ? "Jewelry" : item_type)}' and bullion='{(iSBoolean ? 1 : 0)}' ORDER BY [group]");
            return GetSqlData($"SELECT TRIM([GROUP]) as optionname,prefix,ISNULL(use_dash, 0) as use_dash, ISNULL(Labor,0) Labor from[dbo].[groups] with(nolock) where item_type= '{(string.IsNullOrWhiteSpace(item_type) ? "Jewelry" : item_type)}' and bullion='{(iSBoolean ? 1 : 0)}' ORDER BY [group]");

        }
        public DataTable GetSelectDataLefk(string tblname, string fldname, string whrcond)
        {
            return GetSqlData("SELECT '' AS [Attrib] UNION SELECT Atr_Value Attrib FROM " + tblname + " WHERE TRIM(" + fldname + ")=TRIM('" + whrcond + "')",
                "@tblname", tblname, "@fldname", fldname, "@whrcond", whrcond);
        }
        public bool CombineOptions(string option, string optFrom, string optTo, string loggedUser)
        {
            using (var connection = _connectionProvider.GetConnection())
            using (var command = new SqlCommand("CombineOptions", connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                // Add parameters
                command.Parameters.AddWithValue("@OPTION", option);
                command.Parameters.AddWithValue("@OPT_FROM", optFrom);
                command.Parameters.AddWithValue("@OPT_TO", optTo);
                command.Parameters.AddWithValue("@LOGGEDUSER", loggedUser);
                connection.Open();
                return command.ExecuteNonQuery() > 0;
            }
        }
        public DataTable CheckOptionData(string TblName, string fldname, string value, string itemtype = "", string itemgroup = "", bool isgroupbasedontype = false)
        {
            if (value.Contains("'"))
                value = value.Replace("'", "''");

            if (TblName == "CLARITIES")
                return GetSqlData("SELECT TRIM(clarity) as optionname ,cast([order] as int) as [order], 1 as STATUS FROM CLARITIES where clarity='" + value + "' order by [order]");
            if (TblName == "SUBKIND")
                return GetSqlData("SELECT TRIM(" + fldname + ") as optionname, 1 as STATUS  FROM " + TblName + " where kind='" + value + "' order by " + fldname);
            if (TblName == "GROUP" && itemtype != "" && isgroupbasedontype)
                return GetSqlData("SELECT TRIM(" + fldname + ") as optionname, 1 as STATUS  FROM " + TblName + " where " + fldname + "='" + value + "' and item_type='" + itemtype + "' order by " + fldname);
            if (TblName == "CATS" && isgroupbasedontype)
                return GetSqlData("SELECT TRIM(" + fldname + ") as optionname, 1 as STATUS  FROM " + TblName + " where " + fldname + "='" + value + "' and (item_type='" + itemtype + "' OR '" + itemtype + "'='') and (([group]='" + itemgroup + "' OR '" + itemgroup + "'='') or ([group2]='" + itemgroup + "' OR '" + itemgroup + "'='') or ([group3]='" + itemgroup + "' OR '" + itemgroup + "'='') or ([group4]='" + itemgroup + "' OR '" + itemgroup + "'='') ) order by " + fldname);
            return GetSqlData("SELECT TRIM(" + fldname + ") as optionname, 1 as STATUS  FROM " + TblName + " where " + fldname + "='" + value + "' order by " + fldname);
        }
        public DataTable DeleteGroupWithItemTypeGroup(string catg, string item_type, string group)
        {
            return GetSqlData($"delete from cats with(nolock) where CATEGORY = TRIM('{catg}') and [group] = TRIM('{group}') and item_type = TRIM('{(string.IsNullOrWhiteSpace(item_type) ? "Jewelry" : item_type)}') ");
        }
        public DataTable DeleteGroupWithItemType(string group, string item_type)
        {
            return GetSqlData($"delete from groups with(nolock) where [group] = TRIM('{group}') and item_type = TRIM('{(string.IsNullOrWhiteSpace(item_type) ? "Jewelry" : item_type)}') ");
        }
        public DataTable AddOrEditCatValues(string user, string leveldet, bool bullion = false, string item_type = "", string group = "")
        {
            using (var connection = _connectionProvider.GetConnection())
            using (var command = new SqlCommand("AddOrEditCatsValues", connection))
            using (var adapter = new SqlDataAdapter(command))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@LOGGEDUSER", user);
                command.Parameters.AddWithValue("@CatTable", leveldet);
                command.Parameters.AddWithValue("@bullion", bullion);
                command.Parameters.AddWithValue("@item_type", item_type);
                command.Parameters.AddWithValue("@group", group);

                var dataTable = new DataTable();
                adapter.Fill(dataTable);
                return dataTable;
            }
        }
        public DataTable AddOrEditGroupValues(string user, string leveldet, bool bullion = false, string item_type = "")
        {
            return GetStoreProc("AddOrEditGroupValues", "@LOGGEDUSER", user, "@GroupTable", leveldet, "@bullion", bullion.ToString(), "@item_type", item_type);
        }
        public DataTable ConvertListToDataTable(List<Dictionary<string, object>> rows)
        {
            DataTable dt = new DataTable();

            if (rows == null || rows.Count == 0)
                return dt;

            foreach (var key in rows[0].Keys)
                dt.Columns.Add(key);

            foreach (var row in rows)
            {
                var dr = dt.NewRow();
                foreach (var kv in row)
                    dr[kv.Key] = kv.Value ?? DBNull.Value;

                dt.Rows.Add(dr);
            }

            return dt;
        }
        public bool IsValidGroup(string group)
        {
            return DataTableOK(GetSqlRow("select * from groups where [group] = @group", "@group", group.ToUpper()));
        }
        public DataTable editinventoryasofdate(DateTime Asofdate, string Inventorydata, string loggeduser, string storeno, bool isgladd, bool isStyleItem)
        {
            var dataTable = new DataTable();

            using (var connection = _connectionProvider.GetConnection())
            using (var dataAdapter = new SqlDataAdapter())
            {
                // Set up the command with all necessary parameters
                var command = new SqlCommand("EditStockAsofDate", connection)
                {
                    CommandType = CommandType.StoredProcedure,
                    CommandTimeout = 3000
                };

                command.Parameters.AddWithValue("@Asofdate", Asofdate);
                command.Parameters.AddWithValue("@TBLEDITINVENTORYITEMS", Inventorydata);
                command.Parameters.AddWithValue("@LOGGEDUSER", loggeduser);
                command.Parameters.AddWithValue("@CSTORE", storeno);
                command.Parameters.AddWithValue("@isgladd", isgladd);
                command.Parameters.AddWithValue("@isStyleItem", isStyleItem);

                dataAdapter.SelectCommand = command;

                // Fill the DataTable using the dataAdapter
                dataAdapter.Fill(dataTable);
            }

            return dataTable;
        }
        public IEnumerable<SelectListItem> GetStoreNameSelectList()
        {
            var dt = GetStoreNames();
            var list = new List<SelectListItem>();

            foreach (DataRow row in dt.Rows)
            {
                string code = row["Code"].ToString();
                string name = row["Name"].ToString();

                list.Add(new SelectListItem
                {
                    Value = code,
                    //Text = $"{code} - {name}"     
                    Text = code,
                });
            }

            return list;
        }
        public bool PutLogReceived(string receivedLogNumbers)
        {
            using (SqlConnection conn = _connectionProvider.GetConnection())
            using (SqlCommand cmd = new SqlCommand("SetLogReceived", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@RcevedLogNumbers", receivedLogNumbers);

                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }
        public DataRow GetstockinfoByStore(String Style, string Store)
        {
            return GetSqlRow(" SELECT * FROM STOCK with (nolock) WHERE style=@style and store_no=@store", "@style", Style, "@store", Store);
        }

        public DataTable GetScrapsforprocess(string fromdate, string todate, bool isprcall, string Stores, int received, bool isNotprocessedStockorStock = false)
        {
            return GetStoreProc("GetScrapsforprocess", "@fromdate", fromdate.ToString(), "@todate", todate.ToString(), "@isprcall", isprcall.ToString(), "@Stores", Stores, "@received", received.ToString(), "@isNotprocessedStockorStock", isNotprocessedStockorStock.ToString());
        }

        public bool UpdateProcessScraps(string XML, int IsStyleorScrap = 0, string Style = "", bool IsnewStyle = false)
        {
            using (var connection = _connectionProvider.GetConnection())
            using (var dbCommand = new SqlCommand("UpdateProcessScraps", connection))
            {
                dbCommand.CommandType = CommandType.StoredProcedure;

                // Add parameters with explicit types to avoid any issues with SQL type inference
                dbCommand.Parameters.Add(new SqlParameter("@SCRAPITEMS", SqlDbType.Xml) { Value = XML });
                dbCommand.Parameters.Add(new SqlParameter("@IsStyleorScrap", SqlDbType.Int) { Value = IsStyleorScrap });
                dbCommand.Parameters.Add(new SqlParameter("@Style", SqlDbType.Text) { Value = Style });
                dbCommand.Parameters.Add(new SqlParameter("@Storecode", SqlDbType.Text) { Value = StoreCode });
                dbCommand.Parameters.Add(new SqlParameter("@LOGGEDUSER", SqlDbType.Text) { Value = LoggedUser });
                dbCommand.Parameters.Add(new SqlParameter("@IsnewStyle", SqlDbType.Bit) { Value = IsnewStyle });

                connection.Open();
                var rowsAffected = dbCommand.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }

        public DataTable GetListBoxDataForInventory(string tblname, string fldname)
        {
            return GetSqlData(" SELECT DISTINCT " + fldname + " FROM " + tblname + "  with (nolock) where '" + fldname + "' != '' order by " + fldname + " ");
        }

        public DataTable GetTransferDetails(string CINV)
        {
            return GetSqlData("SELECT INV_NO,DATE,FROM_STORE,STORE_NO,STYLE,QTY,IIF(ACKED='1','Yes','No')ACKED FROM INC_STORE with (nolock) WHERE Trimmed_inv_no=@CINV",
                "@CINV", CINV.Trim());
        }


        public void JmRenewal(string cJMID, string salesID, string cTransID, string planSKU)
        {
            try
            {
                HttpWebRequest httpWebRequest;
                HttpWebResponse httpResponse;

                var cMain = new jmextension
                {
                    saleId = salesID,
                    transactionId = "renew" + cTransID,
                    storeCode = jm_Store,
                    sku = planSKU
                };
                Encoding encoding = new UTF8Encoding();
                System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Ssl3 | System.Net.SecurityProtocolType.Tls | System.Net.SecurityProtocolType.Tls11 | System.Net.SecurityProtocolType.Tls12;
                var plainCredentials = System.Text.Encoding.UTF8.GetBytes(JM_API_KEY.Trim() + ":" + JM_API_PWD.Trim());
                string key = "Basic " + System.Convert.ToBase64String(plainCredentials);
                string json = JsonConvert.SerializeObject(cMain);
                httpWebRequest = (HttpWebRequest)WebRequest.Create("https://uat-jmcareplanapi.jewelersmutual.com/warranty/sales/renewal");
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = "POST";
                httpWebRequest.Headers.Add("Authorization", $"{key}");
                httpWebRequest.Headers.Add("Cache-Control", "no-cache");
                httpWebRequest.Headers.Add("Postman-Token", "Keep-Alive");
                httpWebRequest.ProtocolVersion = HttpVersion.Version11;
                encoding = new UTF8Encoding();
                byte[] data = Encoding.ASCII.GetBytes(json);
                httpWebRequest.ContentLength = data.Length;

                Stream requestStream = httpWebRequest.GetRequestStream();
                requestStream.Write(data, 0, data.Length);
                requestStream.Close();
                dynamic retValue;

                try
                {
                    httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();

                    if (httpResponse.StatusCode == HttpStatusCode.OK)
                        using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                            retValue = JsonConvert.DeserializeObject(streamReader.ReadToEnd());
                }
                catch (WebException ex)
                {
                    if (ex.Status == WebExceptionStatus.ProtocolError)
                    {
                        HttpWebResponse err = ex.Response as HttpWebResponse;
                        if (err != null)
                        {
                            var htmlResponse = new StreamReader(err.GetResponseStream()).ReadToEnd();
                            retValue = JsonConvert.DeserializeObject(htmlResponse);
                            LogError(retValue.errors[0]["description"].ToString());

                        }
                    }
                }
                catch (Exception ex)
                {
                    LogError(ex.Message);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void JmCancel(string cJMID, string salesID, string cTransID)
        {
            try
            {
                HttpWebRequest httpWebRequest;
                HttpWebResponse httpResponse;

                var cMain = new uploadrequest
                {
                    saleId = salesID,
                    cancellationDate = DateTime.Now.ToString("yyyy-MM-dd"),
                    cancellationTransactionId = cTransID
                };
                Encoding encoding = new UTF8Encoding();
                System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Ssl3 | System.Net.SecurityProtocolType.Tls | System.Net.SecurityProtocolType.Tls11 | System.Net.SecurityProtocolType.Tls12;
                var plainCredentials = System.Text.Encoding.UTF8.GetBytes(JM_API_KEY.Trim() + ":" + JM_API_PWD.Trim());
                string key = "Basic " + System.Convert.ToBase64String(plainCredentials);
                string json = JsonConvert.SerializeObject(cMain);
                httpWebRequest = (HttpWebRequest)WebRequest.Create("https://uat-jmcareplanapi.jewelersmutual.com/warranty/sales/cancel");
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = "POST";
                httpWebRequest.Headers.Add("Authorization", $"{key}");
                httpWebRequest.Headers.Add("Cache-Control", "no-cache");
                httpWebRequest.Headers.Add("Postman-Token", "Keep-Alive");
                httpWebRequest.ProtocolVersion = HttpVersion.Version11;
                encoding = new UTF8Encoding();
                byte[] data = Encoding.ASCII.GetBytes(json);
                httpWebRequest.ContentLength = data.Length;

                Stream requestStream = httpWebRequest.GetRequestStream();
                requestStream.Write(data, 0, data.Length);
                requestStream.Close();
                dynamic retValue;
                try
                {
                    httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();

                    if (httpResponse.StatusCode == HttpStatusCode.OK)
                    {
                        using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                        {
                            var result = streamReader.ReadToEnd();
                            retValue = JsonConvert.DeserializeObject(result);
                            SaveCancelJMInfo(cJMID, salesID, cTransID, DecimalCheckForNullDBNull(retValue["refundAmount"]));
                        }
                    }
                }
                catch (WebException ex)
                {
                    if (ex.Status == WebExceptionStatus.ProtocolError)
                    {
                        var err = ex.Response as HttpWebResponse;
                        if (err != null)
                        {
                            var htmlResponse = new StreamReader(err.GetResponseStream()).ReadToEnd();
                            retValue = JsonConvert.DeserializeObject(htmlResponse);
                            LogError(retValue.errors[0]["description"].ToString());
                        }
                    }
                }
                catch (Exception ex)
                {
                    LogError(ex.Message);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool SaveCancelJMInfo(string jmtransactid, string jmsalesid, string jmcancelid, decimal jmrefund)
        {
            using (SqlConnection conn = _connectionProvider.GetConnection())
            using (SqlCommand dbCommand = new SqlCommand("UPDATEJM_CANCEL", conn))
            {
                dbCommand.CommandType = CommandType.StoredProcedure;

                dbCommand.Parameters.Add(new SqlParameter("@jmtransactid", SqlDbType.VarChar, 50) { Value = jmtransactid });
                dbCommand.Parameters.Add(new SqlParameter("@jmsalesid", SqlDbType.VarChar, 50) { Value = jmsalesid });
                dbCommand.Parameters.Add(new SqlParameter("@jmcancelid", SqlDbType.VarChar, 50) { Value = jmcancelid });
                dbCommand.Parameters.Add(new SqlParameter("@jmrefund", SqlDbType.Decimal) { Value = jmrefund });

                conn.Open();
                int rowsAffected = dbCommand.ExecuteNonQuery();

                return rowsAffected > 0;
            }
        }
        public void LogError(string message)
        {
            System.Diagnostics.Debug.WriteLine(message);
        }

        public DataTable getDistinctCategories()
        {
            return GetSqlData("select null as category from cats union select distinct category from cats order by category");
        }

        public DataTable getGLClassData()
        {
            return GetSqlData(@"select trim(CLASS_GL) CLASS_GL,trim(ASSET_GL) ASSET_GL,trim(CLEAR_GL) CLEAR_GL,trim(COGS_GL) COGS_GL,trim(SALES_GL) SALES_GL,trim(DISC_GL) DISC_GL from CLASSGLS");
        }

        public bool MassEditStockInventory(string stock, string loggedUser, string storeNo, bool isStyleItem = false)
        {
            using (var connection = _connectionProvider.GetConnection())
            using (var dbCommand = new SqlCommand("MassEditStockInventory", connection) { CommandType = CommandType.StoredProcedure })
            {
                dbCommand.Parameters.AddRange(new[]
                {
                new SqlParameter("@stockedit", SqlDbType.Xml) { Value = stock },
                new SqlParameter("@loggeduser", SqlDbType.NVarChar) { Value = loggedUser },
                new SqlParameter("@storeno", SqlDbType.NVarChar) { Value = storeNo },
                new SqlParameter("@isStyleItem", SqlDbType.Bit) { Value = isStyleItem }
                });

                connection.Open();
                return dbCommand.ExecuteNonQuery() > 0;
            }
        }

        public bool CheckSequence(string TableName, string FieldName, string PField, string PValue, string MaxLimit, string MinLimit, string NextKey)
        {

            // Exit early if NextKey is not defined
            if (string.IsNullOrWhiteSpace(NextKey))
                return false;
            try
            {
                // Ensure TableName is defined
                if (string.IsNullOrWhiteSpace(TableName))
                    throw new ArgumentException("Table name not defined");

                // Trim values once for use in parameters
                var tableName = TableName.Trim();
                var fieldName = FieldName.Trim();
                var pField = PField.Trim();
                var pValue = PValue.Trim();

                // Use using to ensure proper disposal of the connection and command
                using (var connection = _connectionProvider.GetConnection())
                using (var dbCommand = new SqlCommand("CheckSequence", connection))
                {
                    // Set command properties
                    dbCommand.CommandType = CommandType.StoredProcedure;

                    // Add parameters with explicit types to avoid issues with AddWithValue
                    dbCommand.Parameters.Add(new SqlParameter("@tName", SqlDbType.NVarChar) { Value = tableName });
                    dbCommand.Parameters.Add(new SqlParameter("@fName", SqlDbType.NVarChar) { Value = fieldName });
                    dbCommand.Parameters.Add(new SqlParameter("@fieldval", SqlDbType.NVarChar) { Value = NextKey });
                    dbCommand.Parameters.Add(new SqlParameter("@maxlimit", SqlDbType.Int) { Value = MaxLimit });
                    dbCommand.Parameters.Add(new SqlParameter("@pfield", SqlDbType.NVarChar) { Value = pField });
                    dbCommand.Parameters.Add(new SqlParameter("@pval", SqlDbType.NVarChar) { Value = pValue });
                    dbCommand.Parameters.Add(new SqlParameter("@minlimit", SqlDbType.Int) { Value = MinLimit });

                    // Open connection, execute the command, and handle exceptions automatically
                    connection.Open();
                    //dbCommand.ExecuteNonQuery();
                    var rowsAffected = dbCommand.ExecuteNonQuery();
                    dbCommand.Connection.Close();
                    return rowsAffected > 0;
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An error occurred while checking the sequence.", ex);
            }
        }

        public DataTable GetStyleCase(bool addEmpty = false)
        {
            if (!addEmpty)
                return GetSqlData($"SELECT DISTINCT CASE_NO FROM CASES with (nolock) ORDER BY CASE_NO");
            return GetSqlData($"SELECT '' CASE_NO UNION SELECT DISTINCT CASE_NO FROM CASES with (nolock) ORDER BY CASE_NO");
        }

        public DataTable GetCaseNoofStyle(string styleNo, string store = "")
        {
            if (store == "")
                return GetSqlData(@"SELECT ISNULL(CASE_NO,'') CASE_NO FROM STYLES WHERE STYLE = TRIM('" + styleNo.Replace("'", "''") + "') ");
            DataTable dataTable = GetSqlData("SELECT ISNULL([CASE],'') CASE_NO FROM STYLES_CASE WHERE STYLE = LTRIM(RTRIM('" + styleNo.Replace("'", "''") + "')) AND STORE = LTRIM(RTRIM('" + store.Replace("'", "''") + "'))");
            if (dataTable.Rows.Count > 0)
                return dataTable;
            return GetSqlData(@"SELECT ISNULL([CASE_NO],'') CASE_NO FROM STYLES WHERE STYLE = LTRIM(RTRIM('" + styleNo.Replace("'", "''") + "'))");
        }

        public bool UpdateCaseofStyles(bool singlestore, string store_no, string casedata)
        {
            using (SqlConnection conn = _connectionProvider.GetConnection())
            using (SqlCommand cmd = new SqlCommand("UpdateCaseofStyles", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@IS_SINGLESTORE", SqlDbType.Bit).Value = singlestore;
                cmd.Parameters.Add("@STORE_NO", SqlDbType.VarChar, 50).Value = store_no;
                cmd.Parameters.Add("@STYLESCASEDATA", SqlDbType.NVarChar, -1).Value = casedata; // -1 for MAX length

                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public DataTable GetJMCareData(bool lFrmInvoice = false)
        {
            if (lFrmInvoice)
                return GetSqlData("SELECT ISNULL(CODE,'') CODE, ISNULL(PRICE,0) PRICE, IIF(ISNULL(IS_WATCH,0)=1, CAST('WATCHES' AS NVARCHAR(10)), CAST('' AS NVARCHAR(10))) IS_WATCH,IIF(ISNULL(IS_LIFETIME,0)=1,CAST('LIFETIME' AS NVARCHAR(10)), CAST('3 YEAR' AS NVARCHAR(10))) IS_LIFETIME FROM JMCARE with(nolock)");
            return GetSqlData("SELECT ISNULL(CODE,'') CODE, ISNULL(MIN_PRICE,0) MIN_PRICE, ISNULL(MAX_PRICE,0) MAX_PRICE, ISNULL(PRICE,0) PRICE, IIF(ISNULL(IS_WATCH,0)=1, CAST('WATCHES' AS NVARCHAR(10)), CAST('' AS NVARCHAR(10))) IS_WATCH, IIF(ISNULL(IS_LIFETIME,0)=1,CAST('LIFETIME' AS NVARCHAR(10)), CAST('3 YEAR' AS NVARCHAR(10))) IS_LIFETIME FROM JMCARE with(nolock)");
        }
        public DataTable AddUpdateJMCWarranties(string JMCareData)
        {
            return GetStoreProc("AddUpdateJMCWarrantie", "@JMCAREDATA", JMCareData);
        }
        public DataTable DeleteJMCWarrantie(string code)
        {
            return GetSqlData(@"DELETE FROM JMCARE WHERE CODE = TRIM('" + code.Replace("'", "''") + "') ");
        }
        public DataTable Disclaimers_get()
        {
            return GetSqlData("SELECT CODE, BODY FROM Disclaimers ORDER BY CODE");
        }
        public bool Disclaimers_Update(string disclaimersData)
        {
            using (SqlCommand dbCommand = new SqlCommand())
            {
                // Set the command object properties
                dbCommand.Connection = _connectionProvider.GetConnection();
                dbCommand.CommandType = CommandType.StoredProcedure;
                dbCommand.CommandText = "Disclaimers_Update";
                dbCommand.CommandTimeout = 180;

                SqlParameter parameter = new SqlParameter()
                {
                    ParameterName = "@DisclaimerItems",
                    SqlDbType = System.Data.SqlDbType.Xml,
                    Value = disclaimersData
                };
                dbCommand.Parameters.Add(parameter);

                dbCommand.Connection.Open();
                var rowsAffected = dbCommand.ExecuteNonQuery();
                dbCommand.Connection.Close();

                return rowsAffected > 0;

            }
        }

        public DataRow GetInvoiceBySaveDraft(string invno)
        {
            return GetSqlRow("select top 1 i.*,it.memo_no,iSNULL(it.IsSpecialItem,0) IsSpecialItem from draft_invoice i left join (select * from draft_IN_ITEMS where INV_NO =@inv_no and IsSpecialItem=1) it on i.inv_no = it.inv_no Where i.inv_no = @inv_no", "@inv_no", invno);
        }

        public DataTable GetDraftInvoices(String filter)
        {
            return GetSqlData(@"SELECT ID, I.INV_NO, CUSTOMER.ACC, I.NAME,
				try_cast(customer.TEL as Nvarchar(30)) as TEL, I.DATE, DI.STYLE, DI.[DESC],GR_TOTAL,[Message]
				FROM DRAFT_INVOICE I JOIN CUSTOMER ON I.ACC = CUSTOMER.ACC
				LEFT OUTER JOIN(SELECT INV_NO, MAX(STYLE)STYLE, MAX([DESC])[DESC]  FROM DRAFT_IN_ITEMS GROUP BY INV_NO)
				DI ON((I.INV_NO))= ((DI.INV_NO))
				where " + filter + " AND ISNULL(I.ACC,'') <> ''ORDER BY DATE desc");
        }

        public bool DeleteSaveDraft(string invno)
        {
            using (var connection = _connectionProvider.GetConnection())
            using (var command = new SqlCommand())
            {
                command.Connection = connection;
                command.CommandText = @"
				DELETE FROM draft_invoice WHERE inv_no = @inv_no;
				DELETE FROM draft_in_items WHERE inv_no = @inv_no;
				DELETE FROM draft_Invoice_Discounts WHERE inv_no = @inv_no;";
                command.Parameters.AddWithValue("@inv_no", Pad6(invno));

                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }

        class jmextension
        {
            public string saleId { get; set; }
            public string transactionId { get; set; }
            public string storeCode { get; set; }
            public string sku { get; set; }

        }
        class uploadrequest
        {
            public string saleId { get; set; }
            public string cancellationDate { get; set; }
            public string cancellationTransactionId { get; set; }
        }


    }

    public class PaymentRepair
    {
        public string cRepNo { get; set; }
        public string cAcc { get; set; }
        public string pcname { get; set; }
        public string total { get; set; }
        public string paymentItems { get; set; }
        public string UserGCNo { get; set; }
        public string StoreCode { get; set; }
        public string Cash_Register { get; set; }
        public string StoreCodeInUse { get; set; }
        public string xmlDiscount { get; set; }
        public bool ispayment { get; set; }
        public bool is_return { get; set; }
        public bool is_update { get; set; }
    }

    public class CustomerModel
    {
        public string ACC { get; set; }
        public string BILL_ACC { get; set; }
        public string NAME { get; set; }
        public string ADDR1 { get; set; }
        public string ADDR12 { get; set; }
        public string ADDR13 { get; set; }
        public string CITY1 { get; set; }
        public string STATE1 { get; set; }
        public string ZIP1 { get; set; }
        public string COUNTRY { get; set; }
        public decimal TEL { get; set; }
        public string CELL { get; set; } = string.Empty;
        public bool ON_ACCOUNT { get; set; }
        public string old_customer { get; set; }
        public string EMAIL { get; set; }
        public DateTime? DOB { get; set; } = null;
        public string driverlicense_state { get; set; } = string.Empty;
        public string driverlicense_number { get; set; } = string.Empty;
        public bool declined { get; set; } = false;
        public string Store_no { get; set; }
        public string Non_Taxable { get; set; }
        public IEnumerable<SelectListItem> AllStatesList { get; set; }
        public IEnumerable<SelectListItem> AllCountriesList { get; set; }
        public IEnumerable<SelectListItem> AllSalesManList { get; set; }
        public IEnumerable<SelectListItem> MainEventList { get; set; }
        public IEnumerable<SelectListItem> SubEventList { get; set; }
        public IEnumerable<SelectListItem> AllStores { get; set; }


    }

    public class CustomerAttribute
    {
        public string Attr1 { get; set; }
        public string Attr2 { get; set; }
        public string Attr3 { get; set; }
        public string Attr4 { get; set; }
        public string Attr5 { get; set; }
        public string Attr6 { get; set; }
        public string Attr7 { get; set; }
        public string Attr8 { get; set; }
        public string[] Attr9 { get; set; }
        public string[] Attr10 { get; set; }
        public string[] Attr11 { get; set; }

        public string checkBoxLabel1 { get; set; }
        public string checkBoxLabel2 { get; set; }
        public string checkBoxLabel3 { get; set; }
        public string checkBoxLabel4 { get; set; }
        public string checkBoxLabel5 { get; set; }
        public string checkBoxLabel6 { get; set; }
        public string checkBoxLabel7 { get; set; }
        public string checkBoxLabel8 { get; set; }

        public bool chkAttrib1Val1 { get; set; }
        public bool chkAttrib1Val2 { get; set; }
        public bool chkAttrib1Val3 { get; set; }
        public bool chkAttrib1Val4 { get; set; }
        public bool chkAttrib1Val5 { get; set; }
        public bool chkAttrib1Val6 { get; set; }
        public bool chkAttrib1Val7 { get; set; }
        public bool chkAttrib1Val8 { get; set; }
    }
    public class DrawerDetails
    {
        public String Sent { get; set; }
        public decimal SentVal { get; set; }

    }
    public class PrintTagResult
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public string FilePath { get; set; }
        public string FileContent { get; set; }
    }

}
