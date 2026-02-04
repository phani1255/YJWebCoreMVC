using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.PixelFormats;
using System.Data;
using System.Numerics;
using System.Text;
using System.Xml.Serialization;
using YJWebCoreMVC.Models;

namespace YJWebCoreMVC.Services
{
    public class HelperSivaService
    {
        private readonly ConnectionProvider _connectionProvider;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly HelperCommonService _helperCommonService;
        private readonly IWebHostEnvironment _env;
        private readonly string _companyName;
        private readonly string _storeCodeInUse;
        private readonly string _loggedUser;

        public HelperSivaService(ConnectionProvider connectionProvider, IHttpContextAccessor httpContextAccessor, HelperCommonService helperCommonService, IWebHostEnvironment env)
        {
            _connectionProvider = connectionProvider;
            _httpContextAccessor = httpContextAccessor;
            _helperCommonService = helperCommonService;
            _companyName = _httpContextAccessor.HttpContext?.Session.GetString("COMPANYNAME");
            _storeCodeInUse = _httpContextAccessor.HttpContext?.Session.GetString("STORE_CODE");
            _loggedUser = _httpContextAccessor.HttpContext?.Session.GetString("UserId");
            _env = env;
        }

        public DataTable importedData = new DataTable();
        public List<SelectListItem> GetAllGroupsforbill()
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
        public List<SelectListItem> GetAllSubBrandsforbill()
        {
            DataTable dataTable = _helperCommonService.GetSqlData("select distinct subbrand from SUBBRANDS where subbrand != '' and subbrand is not null order by subbrand");
            List<SelectListItem> salesmanList = new List<SelectListItem>();

            if (_helperCommonService.DataTableOK(dataTable))
                foreach (DataRow dr in dataTable.Rows)
                    salesmanList.Add(new SelectListItem() { Text = dr["subbrand"].ToString().Trim(), Value = dr["subbrand"].ToString().Trim() });
            return salesmanList;
        }
        public List<SelectListItem> GetAllSubBrandsforbillbybrand(string brand)
        {
            DataTable dataTable = _helperCommonService.GetSqlData("select distinct subbrand from SUBBRANDS where isnull(subbrand,'')<>'' and brand=@brand order by subbrand", "@brand", brand.Trim());
            List<SelectListItem> salesmanList = new List<SelectListItem>();

            if (_helperCommonService.DataTableOK(dataTable))
                foreach (DataRow dr in dataTable.Rows)
                    salesmanList.Add(new SelectListItem() { Text = dr["subbrand"].ToString().Trim(), Value = dr["subbrand"].ToString().Trim() });
            return salesmanList;
        }
        public List<SelectListItem> GetAllCategoriesforbill()
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
        public List<SelectListItem> GetAllSubCategoriesforbill()
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
        public List<SelectListItem> GetAllSubCategoriesforbillbycat(string category)
        {
            DataTable dataTable = _helperCommonService.GetSqlData("select distinct subcategory as subcat from subcats where isnull(subcategory,'')<>'' and category=@category order by subcat", "@category", category.Trim());

            List<SelectListItem> salesmanList = new List<SelectListItem>();

            if (dataTable.Rows.Count > 0)
            {
                foreach (DataRow dr in dataTable.Rows)
                {
                    salesmanList.Add(new SelectListItem() { Text = dr["subcat"].ToString().Trim(), Value = dr["subcat"].ToString().Trim() });
                }
            }
            return salesmanList;

        }
        public List<SelectListItem> GetAllGLClassesforbill(bool isbill = false)
        {
            DataTable dataTable;
            if (isbill)
                dataTable = _helperCommonService.GetSqlData("select trim(CLASS_GL) CLASS_GL,trim(ASSET_GL) ASSET_GL,ltrim(rtrim(CLEAR_GL)) CLEAR_GL,ltrim(rtrim(COGS_GL)) COGS_GL,trim(SALES_GL) SALES_GL,trim(DISC_GL) DISC_GL from CLASSGLS where isnull(class_gl,'') not in ('LT MEMO','ST MEMO')");
            else
                dataTable = _helperCommonService.GetSqlData("select trim(CLASS_GL) CLASS_GL,trim(ASSET_GL) ASSET_GL,ltrim(rtrim(CLEAR_GL)) CLEAR_GL,ltrim(rtrim(COGS_GL)) COGS_GL,trim(SALES_GL) SALES_GL,trim(DISC_GL) DISC_GL from CLASSGLS where isnull(class_gl,'')  in ('LT MEMO','ST MEMO')");
            List<SelectListItem> salesmanList = new List<SelectListItem>();

            if (_helperCommonService.DataTableOK(dataTable))
                foreach (DataRow dr in dataTable.Rows)
                    salesmanList.Add(new SelectListItem() { Text = dr["CLASS_GL"].ToString().Trim(), Value = dr["CLASS_GL"].ToString().Trim() });
            return salesmanList;
        }
        public List<SelectListItem> GetAllMetalsforbill()
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
        public List<SelectListItem> GetAllCenterTypesforbill()
        {

            DataTable dataTable = _helperCommonService.GetSqlData("select distinct CENTER_TYPE from CENTER_TYPES where CENTER_TYPE != '' and CENTER_TYPE is not null order by CENTER_TYPE");
            List<SelectListItem> salesmanList = new List<SelectListItem>();

            if (_helperCommonService.DataTableOK(dataTable))
                foreach (DataRow dr in dataTable.Rows)
                    salesmanList.Add(new SelectListItem() { Text = dr["CENTER_TYPE"].ToString().Trim(), Value = dr["CENTER_TYPE"].ToString().Trim() });
            return salesmanList;
        }
        public List<SelectListItem> GetAllCenterStoneShapesforbill()
        {

            DataTable dataTable = _helperCommonService.GetSqlData("select distinct SHAPE from SHAPES where SHAPE != '' and SHAPE is not null order by SHAPE");
            List<SelectListItem> salesmanList = new List<SelectListItem>();

            if (_helperCommonService.DataTableOK(dataTable))
                foreach (DataRow dr in dataTable.Rows)
                    salesmanList.Add(new SelectListItem() { Text = dr["SHAPE"].ToString().Trim(), Value = dr["SHAPE"].ToString().Trim() });
            return salesmanList;
        }


        public List<SelectListItem> GetAllCenterStoneSizesforbill()
        {

            DataTable dataTable = _helperCommonService.GetSqlData("select distinct CENTER_SIZE from CENTER_SIZES where CENTER_SIZE != '' and CENTER_SIZE is not null order by CENTER_SIZE");
            List<SelectListItem> CENTERSIZE = new List<SelectListItem>();

            if (_helperCommonService.DataTableOK(dataTable))
                foreach (DataRow dr in dataTable.Rows)
                    CENTERSIZE.Add(new SelectListItem() { Text = dr["CENTER_SIZE"].ToString().Trim(), Value = dr["CENTER_SIZE"].ToString().Trim() });
            return CENTERSIZE;
        }
        public DataTable GetVendorStyleswithCastcode(string castcode)
        {
            return _helperCommonService.GetSqlData(@"select distinct vnd_style from styles where trim(vnd_style)<>'' and trim(cast_code)=@castcode",
                "@castcode", castcode.Trim());
        }
        public DataTable GetStyleDataByVndStyle(string Vnd_style, string Vnd_Code, bool IsNewDash, bool IsBill, bool isstylenobycat = false)
        {
            return _helperCommonService.GetStoreProc("GetStyleDataByVndStyle", "@Vnd_style", Vnd_style, "@Vnd_Code", Vnd_Code, "@IsNewDash", IsNewDash ? "1" : "0", "@IsBill", IsBill ? "1" : "0", "@isstylenobycat", isstylenobycat ? "1" : "0");
        }
        public DataTable GetStyleDataByVndStyle_Style(string enteredStyle, string enteredVendorStyle, string EnteredGroupName, bool IsNewDash, bool isstylenobycat = false, bool gisbill = false)
        {
            return _helperCommonService.GetStoreProc("GetStyleDataByVndStyle_Style", "@STYLE", enteredStyle.Trim(), "@Vnd_style", enteredVendorStyle.Trim(), "@Group", EnteredGroupName.Trim(), "@IsNewDash", IsNewDash ? "1" : "0", "@isstylenobycat", isstylenobycat ? "1" : "0", "@gisbill", gisbill ? "1" : "0");
        }
        public DataTable GetStyleDataByVndStyle_Group(string Vnd_style, string Group, string Vnd_Code, bool IsNewDash, bool IsBill, string itemtype = "", bool isstylenobycat = false)
        {
            DataTable dataTable = new DataTable();
            using (var connection = _connectionProvider.GetConnection())
            using (var command = new SqlCommand("GetStyleDataByVndStyle_Group", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Vnd_style", Vnd_style);
                command.Parameters.AddWithValue("@Group", Group);
                command.Parameters.AddWithValue("@Vnd_Code", Vnd_Code);
                command.Parameters.AddWithValue("@IsNewDash", IsNewDash ? 1 : 0);
                command.Parameters.AddWithValue("@IsBill", IsBill ? 1 : 0);
                command.Parameters.AddWithValue("@itemtype", itemtype);
                command.Parameters.AddWithValue("@isstylenobycat", isstylenobycat ? 1 : 0);
                command.CommandTimeout = 3000;

                using (var dataAdapter = new SqlDataAdapter(command))
                {
                    connection.Open();
                    dataAdapter.Fill(dataTable);
                }
            }
            return dataTable;
        }
        public List<SelectListItem> GetAllDeptforbill()
        {
            DataTable dataTable = new DataTable();
            dataTable = _helperCommonService.GetSqlData("SELECT DEPT From GL_DEPT with (nolock)");
            List<SelectListItem> GlDept = new List<SelectListItem>();

            if (dataTable.Rows.Count > 0)
            {
                foreach (DataRow dr in dataTable.Rows)
                {
                    GlDept.Add(new SelectListItem() { Text = dr["DEPT"].ToString().Trim(), Value = dr["DEPT"].ToString().Trim() });
                }
            }
            return GlDept;

        }
        public List<SelectListItem> GetAllBrandsforbill()
        {

            DataTable dataTable = _helperCommonService.GetSqlData("select distinct BRAND from BRANDS where BRAND != '' and BRAND is not null order by BRAND");
            List<SelectListItem> salesmanList = new List<SelectListItem>();
            if (_helperCommonService.DataTableOK(dataTable))
                foreach (DataRow dr in dataTable.Rows)
                    salesmanList.Add(new SelectListItem() { Text = dr["brand"].ToString().Trim(), Value = dr["brand"].ToString().Trim() });
            return salesmanList;
        }
        public List<SelectListItem> GetAllDisclaimersforbill()
        {
            DataTable dataTable = _helperCommonService.GetSqlData("SELECT code FROM DISCLAIMERS ORDER BY code");
            List<SelectListItem> salesmanList = new List<SelectListItem>();
            salesmanList.Add(new SelectListItem() { Text = "", Value = "" });
            if (_helperCommonService.DataTableOK(dataTable))
                foreach (DataRow dr in dataTable.Rows)
                    salesmanList.Add(new SelectListItem() { Text = dr["code"].ToString().Trim(), Value = dr["code"].ToString().Trim() });
            return salesmanList;
        }

        public DataTable UpdateStylesCost(BillModel bill, string billitems, string loggeduser, decimal? gold_price, bool IsNewDash)
        {
            return _helperCommonService.GetStoreProc("AddEditConsignmentBill_Dynamically", "@INV_NO", bill.INV_NO ?? "", "@ACC", bill.ACC ?? "", "@AMOUNT", bill.AMOUNT.ToString(), "@DATE", bill.DATE.ToString(), "@VND_NO", bill.VND_NO ?? "", "@TERM", bill.TERM.ToString(), "@DUE_DATE", bill.DUE_DATE.ToString(), "@BALANCE", bill.BALANCE.ToString(), "@ENTER_DATE", bill.ENTER_DATE.ToString(), "@SFM", bill.SFM.ToString(), "@ON_QB", bill.ON_QB.ToString(), "@LOGGEDUSER", loggeduser, "@GOLD_PRICE", gold_price.ToString(), "@IsNewDash", IsNewDash.ToString(), "@StoreNo", bill.Store_No ?? "", "@IsBill", "1", "@ISAUTODESC", bill.IsAutoDESC.ToString(), "@BILLITEMS", billitems);
        }

        public DataRow GetGroupPrefix(string group)
        {
            return _helperCommonService.GetSqlRow(@"select * from groups where  trim([group])=@group",
                "@group", group.Trim());
        }
        public DataRow GetGroupPrefixforcat(string group)
        {
            return _helperCommonService.GetSqlRow(@"select * from cats where  trim(group)=@group",
                "@group", group.Trim());
        }
        public DataTable ChkVendorStyleValidate(string vndstyle)
        {
            return _helperCommonService.GetSqlData("SELECT * FROM STYLES with (nolock) WHERE TRIM(VND_STYLE)=@VNDSTYLE", "@VNDSTYLE", vndstyle.Trim());
        }
        public string GetGLClassCode(string GLCode)
        {
            return _helperCommonService.GetValue0(_helperCommonService.GetSqlData("select ASSET_GL from CLASSGLS with (nolock) where trim(CLASS_GL)=@GLCode", "@GLCode", GLCode.Trim()));
        }

        public bool CheckExistsVndInv(string cVend, string cVndInv, string inv, bool isBill = false)
        {
            using (var connection = _connectionProvider.GetConnection())
            using (var command = new SqlCommand())
            {
                command.Connection = connection;
                command.CommandType = CommandType.Text;
                command.CommandText = isBill
                    ? "SELECT TOP 1 1 FROM bills WHERE LTRIM(RTRIM(acc)) = LTRIM(RTRIM(@cVend)) AND TRIM(vnd_no) = TRIM(@cVndInv) AND Trimmed_inv_no <> LTRIM(RTRIM(@invno))"
                    : "SELECT TOP 1 1 FROM apm WHERE LTRIM(RTRIM(acc)) = LTRIM(RTRIM(@cVend)) AND TRIM(vnd_no) = TRIM(@cVndInv) AND Trimmed_inv_no <> LTRIM(RTRIM(@invno))";

                command.Parameters.AddWithValue("@cVend", cVend);
                command.Parameters.AddWithValue("@cVndInv", cVndInv);
                command.Parameters.AddWithValue("@invno", inv);

                connection.Open();
                return command.ExecuteScalar() != null;
            }
        }
        public bool DeleteNewInsertedStyles(string styleXML, string currstyle = "")
        {
            using (var connection = _connectionProvider.GetConnection())
            using (var command = new SqlCommand("DeleteNewInsertedStyles", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@STYLES", SqlDbType.Xml) { Value = styleXML });
                command.Parameters.AddWithValue("@currstyle", currstyle);
                connection.Open();
                var rowsAffected = command.ExecuteNonQuery();

                return rowsAffected > 0;
            }
        }
        public string ChekBillConsExistOrNot(string invno, bool isBill, bool isvrtv = false)
        {
            using (var connection = _connectionProvider.GetConnection())
            using (var command = new SqlCommand("CheckBillConsExistOrNot", connection))
            {
                // Set command properties
                command.CommandType = CommandType.StoredProcedure;

                // Add input parameters
                command.Parameters.AddWithValue("@invno", invno);
                command.Parameters.AddWithValue("@Isbill", isBill);
                command.Parameters.AddWithValue("@isvrtv", isvrtv);

                // Add output parameter
                var outInvno = new SqlParameter("@Newid", SqlDbType.NVarChar, 550)
                {
                    Direction = ParameterDirection.Output
                };
                command.Parameters.Add(outInvno);

                // Open connection and execute
                connection.Open();
                command.ExecuteNonQuery();

                // Return the output parameter value
                return _helperCommonService.Pad6(outInvno.Value.ToString());
            }
        }
        public string PostGL(string logno, string note, DateTime date, DataTable dtGLPostItems, BillModel bill, bool IsBill = false, bool isBillforjobbag = false, bool isbillforaddcharge = false, string dtbillitems = "", bool isconsforaddcharge = false)
        {
            using (var connection = _connectionProvider.GetConnection())
            using (var dbCommand = new SqlCommand("Post_GL_for_use_glcode", connection))
            {
                dbCommand.CommandType = CommandType.StoredProcedure;
                dbCommand.CommandTimeout = 6000;

                dbCommand.Parameters.AddWithValue("@INVOICE_NO", bill.INV_NO);
                dbCommand.Parameters.AddWithValue("@ACC", bill.ACC);
                dbCommand.Parameters.AddWithValue("@TOTAMT", bill.AMOUNT);
                dbCommand.Parameters.AddWithValue("@DATE", bill.DATE);
                dbCommand.Parameters.AddWithValue("@VND_NO", bill.VND_NO ?? "");
                dbCommand.Parameters.AddWithValue("@TERM", bill.TERM);
                dbCommand.Parameters.AddWithValue("@DUE_DATE", bill.DUE_DATE);
                dbCommand.Parameters.AddWithValue("@BALANCE", bill.BALANCE);
                dbCommand.Parameters.AddWithValue("@ENTER_DATE", bill.ENTER_DATE);
                dbCommand.Parameters.AddWithValue("@LOGGEDUSER", _loggedUser);
                dbCommand.Parameters.AddWithValue("@SFM", bill.SFM);
                dbCommand.Parameters.AddWithValue("@ON_QB", bill.ON_QB);
                dbCommand.Parameters.AddWithValue("@StoreNo", bill.Store_No);
                dbCommand.Parameters.AddWithValue("@IsBill", IsBill);
                dbCommand.Parameters.AddWithValue("@PayTerms", bill.PaymentTerms ?? "");
                dbCommand.Parameters.AddWithValue("@SHIPPING_CH", bill.SHIPPING_CHARGE);
                dbCommand.Parameters.AddWithValue("@OTHER_CH", bill.OTHER_CHARGE);
                dbCommand.Parameters.AddWithValue("@logno", logno);
                dbCommand.Parameters.AddWithValue("@note", note);
                dbCommand.Parameters.AddWithValue("@isBillforjobbag", isBillforjobbag);
                dbCommand.Parameters.AddWithValue("@isbillforaddcharge", isbillforaddcharge);
                dbCommand.Parameters.AddWithValue("@ExisAddChrgBill", bill.ExisAddChrgBill);
                dbCommand.Parameters.AddWithValue("@IsChkNotAddToStock", bill.IsChkNotAddToStock);
                dbCommand.Parameters.AddWithValue("@IsJobbagBillReturn", bill.IsJobbagBillReturn);
                dbCommand.Parameters.AddWithValue("@Insurance", bill.Insurance);
                dbCommand.Parameters.AddWithValue("@isconsforaddcharge", isconsforaddcharge);

                // XML Parameters
                dbCommand.Parameters.Add(new SqlParameter("@transxml", SqlDbType.Xml)
                {
                    Value = _helperCommonService.GetDataTableXML("GLPostItems", dtGLPostItems)
                });

                dbCommand.Parameters.Add(new SqlParameter("@billitemsxml", SqlDbType.Xml)
                {
                    Value = dtbillitems
                });

                // Output parameter
                var outLogNo = new SqlParameter("@returnlog", SqlDbType.NVarChar, 6)
                {
                    Direction = ParameterDirection.Output
                };
                dbCommand.Parameters.Add(outLogNo);

                // Execute the command
                connection.Open();
                dbCommand.ExecuteNonQuery();

                return outLogNo.Value != DBNull.Value ? outLogNo.Value.ToString() : string.Empty;
            }
        }
        public string ToXml<T>(string rootName, List<T> list)
        {
            if (list == null || !list.Any())
                return "";

            var serializer = new XmlSerializer(typeof(List<T>), new XmlRootAttribute(rootName));
            var sw = new StringWriter();
            serializer.Serialize(sw, list);
            return sw.ToString();
        }
        public bool AddEditBill(BillModel bill, string billitems, string oldbillitems, string billgl, string loggeduser,
                          decimal? gold_price, bool IsNewDash, out string error)
        {
            error = string.Empty;

            try
            {
                using (var connection = _connectionProvider.GetConnection())
                using (var dbCommand = new SqlCommand("SaveAddEditConsignmentBill", connection))
                {
                    dbCommand.CommandType = CommandType.StoredProcedure;
                    dbCommand.CommandTimeout = 3000;

                    // Add parameters using a helper function
                    //void AddParameter(string name, object value) => dbCommand.Parameters.AddWithValue(name, value ?? DBNull.Value);
                    Action<string, object> AddParameter = (name, value) => dbCommand.Parameters.AddWithValue(name, value ?? DBNull.Value);

                    AddParameter("@INV_NO", bill.INV_NO);
                    AddParameter("@ACC", bill.ACC);
                    AddParameter("@AMOUNT", bill.AMOUNT);
                    AddParameter("@DATE", bill.DATE);
                    AddParameter("@VND_NO", bill.VND_NO ?? "");
                    AddParameter("@TERM", bill.TERM);
                    AddParameter("@DUE_DATE", bill.DUE_DATE);
                    AddParameter("@BALANCE", bill.BALANCE);
                    AddParameter("@ENTER_DATE", bill.ENTER_DATE);
                    AddParameter("@SFM", bill.SFM);
                    AddParameter("@ON_QB", bill.ON_QB);
                    AddParameter("@LOGGEDUSER", loggeduser);
                    AddParameter("@GOLD_PRICE", gold_price);
                    AddParameter("@IsNewDash", _helperCommonService.CheckModuleEnabled(HelperCommonService.Modules.NewDash));
                    AddParameter("@StoreNo", bill.Store_No);
                    AddParameter("@IsBill", true);
                    AddParameter("@PayTerms", bill.PaymentTerms ?? "");
                    AddParameter("@SHIPPING_CH", bill.SHIPPING_CHARGE);
                    AddParameter("@OTHER_CH", bill.OTHER_CHARGE);
                    AddParameter("@Discount", bill.Discount);
                    AddParameter("@TermNote", bill.TermNote);
                    AddParameter("@NoOfDay1", bill.NoOfDay1);
                    AddParameter("@NoOfDay2", bill.NoOfDay2);
                    AddParameter("@NoOfDay3", bill.NoOfDay3);
                    AddParameter("@DiscPercent1", bill.DiscPercent1);
                    AddParameter("@DiscPercent2", bill.DiscPercent2);
                    AddParameter("@DiscPercent3", bill.DiscPercent3);
                    AddParameter("@AutoDESC", bill.IsAutoDESC);
                    AddParameter("@VNDPOINVNO", bill.VNDPOINVNO);
                    AddParameter("@NOOFTERMS", bill.NOOFTERMS);
                    AddParameter("@TERMINTERVAL", bill.TERMINTERVAL);
                    AddParameter("@IsAddChargBill", bill.IsAddChargBill);
                    AddParameter("@ExisAddChrgBill", bill.ExisAddChrgBill);
                    AddParameter("@IsChkNotAddToStock", bill.IsChkNotAddToStock);
                    AddParameter("@Isupdatecostprice", bill.Isupdatecostprice);
                    AddParameter("@IsAutoFillTags", bill.IsAutoFillTags);
                    AddParameter("@Note", bill.Note);
                    AddParameter("@IsImportAutoDesc", bill.IsImportAutoDesc);
                    AddParameter("@IsImport", bill.IsImport);
                    AddParameter("@isstylenobycat", bill.isstylenobycat);
                    AddParameter("@Insurance", bill.Insurance);
                    AddParameter("@CurrencyType", bill.CurrencyType);
                    AddParameter("@CurrencyRate", bill.CurrencyRate);
                    AddParameter("@IsMultiCurr", bill.IsMultiCurr);
                    AddParameter("@IsVpoBill", bill.IsVpoBill);
                    AddParameter("@IsStyleItem", bill.IsStyleItem);
                    AddParameter("@IsVpoWoStylesBill", bill.IsVpoWoStylesBill);
                    AddParameter("@Castordno", bill.Castordno);
                    AddParameter("@IsDraft", bill.IsDraft);
                    AddParameter("@IsbillWoStyles", bill.IsbillWoStyles);

                    // Add XML parameters
                    dbCommand.Parameters.Add(new SqlParameter("@BILLITEMS", SqlDbType.Xml) { Value = billitems ?? (object)DBNull.Value });
                    dbCommand.Parameters.Add(new SqlParameter("@BILLGL", SqlDbType.Xml) { Value = billgl ?? (object)DBNull.Value });
                    dbCommand.Parameters.Add(new SqlParameter("@OLDBILLITEMS", SqlDbType.Xml) { Value = oldbillitems ?? (object)DBNull.Value });

                    // Execute
                    connection.Open();
                    int rowsAffected = dbCommand.ExecuteNonQuery();

                    return rowsAffected > 0;
                }
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return false;
            }
        }
        public void UpdateBills_GLLog(string INV_NO, string strPostGlCode)
        {
            _helperCommonService.GetSqlData("UPDATE BILLS SET gl_log=@PostGlCode WHERE Trimmed_inv_no=@INV_NO",
                "@INV_NO", INV_NO.Trim(), "@PostGlCode", strPostGlCode);
        }
        public decimal GetDefaultGoldRate()
        {
            return _helperCommonService.GetValueD(_helperCommonService.GetStoreProc("GetDefaultGoldRate"), "GOLD_PRICE");
        }
        public DataTable GetBillItemsByInvNo(string inv_no, bool ismulticurr = false)
        {
            return _helperCommonService.GetStoreProc("GetBillItemsByInvNo", "@inv_no", inv_no, "@ismulticurr", ismulticurr.ToString());
        }
        public DataTable GetConsignmentItemsByInvNo(string inv_no, bool ismulticurr = false)
        {
            return _helperCommonService.GetStoreProc("GetConsignmentItemsByInvNo", "@inv_no", inv_no, "@ismulticurr", ismulticurr.ToString());
        }
        public void GetGoldData(out decimal gold, out decimal silver, out decimal plat, out decimal multi)
        {
            gold = 0; silver = 0; plat = 0; multi = 0;
            SqlCommand command = new SqlCommand(_connectionProvider.GetConnectionString());
            command.CommandType = CommandType.Text;
            string query = "select gold,silver,plat,markup from gold with (nolock)";
            command.Connection = _connectionProvider.GetConnection();
            command.CommandText = query;
            command.Connection.Open();
            using (var reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    gold = Convert.ToDecimal(reader["gold"]);
                    silver = Convert.ToDecimal(reader["silver"]);
                    plat = Convert.ToDecimal(reader["plat"]);
                    multi = Convert.ToDecimal(reader["markup"]);
                }
            }
        }
        public DataRow CheckStyleonConsignment(string style)
        {
            return _helperCommonService.GetSqlRow(string.Format("select inv_no,style from apm_item with (nolock) where style = '{0}' and pcs > sfm_qty + rtv_qty order by inv_no desc", style));
        }
        public DataTable GetGLAcctsByName(string Accname)
        {
            return _helperCommonService.GetSqlData("select cast(0 as bit) Sel, ACC,Name AccName from gl_accs with (nolock) where name like '%" + Accname.Trim() + "%' order by acc", "@Accname", Accname.Trim());
        }
        public DataTable CheckInvoiceforbill(string invno, bool lOpenItems = false)
        {
            if (lOpenItems)
                return _helperCommonService.GetSqlData(@"select trim(style)style, orignal_style, rsrvd_qty,inv_no from in_items with (nolock) where inv_no=@invno and rsrvd_qty=0", "@invno", invno);
            return _helperCommonService.GetSqlData(@"select trim(style)style, orignal_style, rsrvd_qty,inv_no from in_items with (nolock) where inv_no=@invno", "@invno", invno);
        }
        public bool IsValidVendorpo(string VpoNo)
        {
            return _helperCommonService.DataTableOK(_helperCommonService.GetSqlData(@"SELECT * FROM CAST_ORD WHERE Trimmed_inv_no=@VendorPONNo", "@VendorPONNo", VpoNo.Trim()));
        }
        public bool IsRejectedVendorpo(string VpoNo)
        {
            return _helperCommonService.DataTableOK(_helperCommonService.GetSqlData(@"SELECT * FROM CAST_ORD a with (nolock) WHERE  LTRIM(RTRIM(a.INV_NO))=@VendorPONNo and isnull(a.approved,'')='R'", "@VendorPONNo", VpoNo.Trim()));
        }
        public void ValidateVpoStyleAndOpenQty(string vpono, string style, out bool IsValidVpostyle, out decimal OpenQty)
        {
            DataTable dtvpo = _helperCommonService.GetSqlData("select inv_no,style,(qty-rcvd)OpenQty from cast_ord where Trimmed_inv_no=@invno and style=@style", "@invno", vpono.Trim(), "@style", style);
            if (IsValidVpostyle = _helperCommonService.DataTableOK(dtvpo))
                OpenQty = _helperCommonService.CheckForDBNull(dtvpo.Rows[0]["OpenQty"], typeof(decimal).FullName);
            else
                OpenQty = 0;
        }
        public DataRow GetItemInfoStyleandInvoice(string table, string Colname, string invno, string style)
        {
            return _helperCommonService.GetSqlRow(string.Format("select * from {0} where {1}=@invno and style=@style", table, Colname), "@invno", invno, "@style", style);
        }
        public DataTable PaidCommisionReport_post(string paidref)
        {
            return _helperCommonService.GetStoreProc("PaidCommisionReport_post", "@PAID_REF", paidref);
        }
        public string GetStyleImages_PrintBillCons(string p_style)
        {
            string imagename = string.Empty;

            if (string.IsNullOrWhiteSpace(p_style))
                return string.Empty;

            try
            {
                using (SqlConnection con = _connectionProvider.GetConnection())
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("GetStyleImages_PrintBillCons", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@style", p_style);

                        using (SqlDataReader rdr = cmd.ExecuteReader())
                        {
                            byte[] objContext = null;
                            if (rdr.Read())
                                objContext = rdr[0] as byte[];

                            if (objContext != null)
                            {
                                using (MemoryStream ms = new MemoryStream(objContext))
                                {
                                    using (Image image = Image.Load(ms))
                                    {
                                        imagename = Path.Combine(
                                            Path.GetTempPath(),
                                            $"{_helperCommonService.RemoveSpecialCharacters(p_style)}{DateTime.Now:yyyyMMddHHmmssfff}.jpg"
                                        );

                                        image.Save(imagename, new JpegEncoder());
                                    }
                                }
                            }
                        }
                    }
                }

                return "File:" + imagename;
            }
            catch (Exception ex)
            {
                _helperCommonService.MsgBox($"An error occurred : {ex.Message}");
                return string.Empty;
            }
        }

        public bool SaveBillConsDrafts(string billitems, out string error, BillModel bill, bool isbill = false)
        {
            error = string.Empty;
            try
            {
                using (SqlConnection connection = _connectionProvider.GetConnection())
                using (SqlCommand dbCommand = new SqlCommand("SaveBillConsDrafts", connection))
                {
                    dbCommand.CommandType = CommandType.StoredProcedure;
                    dbCommand.CommandTimeout = 6000;

                    //void AddParameter(string name, object value) => dbCommand.Parameters.AddWithValue(name, value ?? DBNull.Value);
                    Action<string, object> AddParameter = (name, value) => dbCommand.Parameters.AddWithValue(name, value ?? DBNull.Value);

                    AddParameter("@INV_NO", bill.INV_NO ?? "");
                    AddParameter("@ACC", bill.ACC ?? "");
                    AddParameter("@AMOUNT", bill.AMOUNT);
                    AddParameter("@DATE", bill.DATE);
                    AddParameter("@VND_NO", bill.VND_NO ?? "");
                    AddParameter("@TERM", bill.TERM);
                    AddParameter("@DUE_DATE", bill.DUE_DATE);
                    AddParameter("@BALANCE", bill.BALANCE);
                    AddParameter("@ENTER_DATE", bill.ENTER_DATE);
                    AddParameter("@LOGGEDUSER", _loggedUser ?? "");
                    AddParameter("@StoreNo", bill.Store_No ?? "");
                    AddParameter("@IsBill", isbill);
                    AddParameter("@PayTerms", bill.PaymentTerms ?? "");
                    AddParameter("@SHIPPING_CH", bill.SHIPPING_CHARGE);
                    AddParameter("@OTHER_CH", bill.OTHER_CHARGE);
                    AddParameter("@Discount", bill.Discount);
                    AddParameter("@TermNote", bill.TermNote ?? "");
                    AddParameter("@NoOfDay1", bill.NoOfDay1);
                    AddParameter("@NoOfDay2", bill.NoOfDay2);
                    AddParameter("@NoOfDay3", bill.NoOfDay3);
                    AddParameter("@DiscPercent1", bill.DiscPercent1);
                    AddParameter("@DiscPercent2", bill.DiscPercent2);
                    AddParameter("@DiscPercent3", bill.DiscPercent3);
                    AddParameter("@NOOFTERMS", bill.NOOFTERMS);
                    AddParameter("@TERMINTERVAL", bill.TERMINTERVAL);
                    AddParameter("@Note", bill.Note ?? "");
                    AddParameter("@Insurance", bill.Insurance);
                    SqlParameter parameter = new SqlParameter("@BILLITEMS", SqlDbType.Xml)
                    {
                        Value = billitems
                    };
                    dbCommand.Parameters.Add(parameter);
                    connection.Open();
                    int rowsAffected = dbCommand.ExecuteNonQuery();

                    return rowsAffected > 0;
                }

            }
            catch (Exception ex)
            {
                error = "An error occurred: " + ex.Message;
                return false;
            }
        }

        public DataTable GetSpecialOrderItemsProcess_popup(string vendcode)
        {
            return _helperCommonService.GetSqlData(@"SELECT DISTINCT I.INV_NO,C.NAME,II.VENDOR,II.STYLE,I.STORE_NO,II.MANUFACTURER_NO,II.METAL_TYPE,II.FINGER_SIZE,II.[DESC],CONVERT(VARCHAR(10),IIF(ISDATE(II.DUEDATE)=1,II.DUEDATE,'19000101'),101) DUEDATE,II.CTR_STN_DIAMENSION,II.PRICE, I.SALESMAN1,VendOrderDt,ExpDelDt,VendConfNo,
                                II.VENDOR OLD_VENDOR,II.STYLE OLD_STYLE,II.MANUFACTURER_NO OLD_MANUFACTURER_NO,II.METAL_TYPE OLD_METAL_TYPE,II.FINGER_SIZE OLD_FINGER_SIZE,II.[DESC] OLD_DESC,CONVERT(VARCHAR(10),IIF(ISDATE(II.DUEDATE)=1,II.DUEDATE,'19000101'),101) OLD_DUEDATE,II.CTR_STN_DIAMENSION OLD_CTR_STN_DIAMENSION,II.PRICE OLD_PRICE
                                FROM IN_ITEMS II with (nolock)
                                INNER JOIN INVOICE I with (nolock) ON I.INV_NO=II.INV_NO
                                LEFT JOIN CUSTOMER C with (nolock) ON C.ACC=I.ACC 
                                where trim(ii.vendor)=trim(@vendcode) AND IsSpecialItem=1 and ISNULL(I.PICKED,0)=0", "@vendcode", vendcode);
        }
        public DataRow GetBillConsDraftsData(string Invno, bool isbill = false)
        {
            if (isbill)
                return _helperCommonService.GetSqlRow("select top 1 *  From draft_bil_item with (nolock) Where trim(inv_no)=@Invno ", "@Invno", Invno.Trim());
            return _helperCommonService.GetSqlRow("select top 1 *  From DRAFT_APM_ITEM with (nolock) Where trim(inv_no)=@Invno ", "@Invno", Invno.Trim());
        }

        public DataTable GetBillsTemplate(string strTemplateName)
        {
            if (String.IsNullOrWhiteSpace(strTemplateName.Trim()))
                return _helperCommonService.GetSqlData("Select * from BILLS_Template with(nolock) order by Template_Name");
            return _helperCommonService.GetSqlData("Select * from BILLS_Template with(nolock) where Template_Name like '%' + @TemplateName + '%' order by Template_Name", "@TemplateName", strTemplateName.Trim());
        }
        public bool AddBillTemplate(BillModel.BillTemplateModel BillTemplateModel)
        {
            using (SqlCommand dbCommand = new SqlCommand())
            {
                dbCommand.Connection = _connectionProvider.GetConnection();
                dbCommand.CommandType = CommandType.Text;

                if (BillTemplateModel.Issavenewtemplate == "Yes")
                {
                    dbCommand.CommandText = @"INSERT INTO BILLS_TEMPLATE VALUES(@TEMPLATE_NAME,@VSTYLE,@GROUP,@STYLE,@QTY,@UCOST,@PRICE,@BRAND,@SUBBRAND,@GLCASS,@CATEGORY,@SUBCATEGORY,@METAL,@DIAWT,@CENTERSIZE,@CENTERTYPE,@CENTERSHAPE,@WEIGHT,@BYWT,'',@TOTALCOST,@ORDERNO,@DESC,@DQLTY,@COLOR_WT,@CQLTY,
                                             @Attrib1,@Attrib2,@Attrib3,@Attrib4,@Attrib5,@Attrib6,@Attrib7,@Attrib8,@Attrib9,@Attrib10,
                                             @Attrib11,@Attrib12,@Attrib19,@Attrib20,@Attrib21,
                                             @ATTR_CHECK1,@ATTR_CHECK2,@ATTR_CHECK3,@ATTR_CHECK4,@ATTR_CHECK5,@ATTR_CHECK6,@Disclaimer,
                                             @Fieldvalue4,@Fieldvalue5,@Fieldvalue6,@Fieldvalue7,@Fieldvalue8,@ItemType,@CertType,@CertNo,@Shape,@Culet,@Measurements,@CertWeight,@Depth,@RapTable,@Girdle,@Polish,@Symmetry,@CutGrade,@Clarity,@Color,@Finger_Size,@Silver_Wt,@Platinum_Wt,@Minimum_Price,
                                             @TagInfo1,@TagInfo2,@TagInfo3,@TagInfo4,@Pricebywt,@GOLDWT,@Labor,@LongDesc,@FAN_CLR,@FAN_INT,@FAN_OVER)";
                }
                else
                {
                    dbCommand.CommandText = @"UPDATE BILLS_TEMPLATE SET VND_STYLE=@VSTYLE,
                                               [GROUP]=@GROUP,STYLE=@STYLE,PCS=@QTY,COST=@UCOST,
                                               PRICE=@PRICE,BRAND=@BRAND,SUBBRAND=@SUBBRAND,
                                               GLCLASS=@GLCASS,CATEGORY=@CATEGORY,SUBCATEGORY=@SUBCATEGORY,
                                               METAL=@METAL,GOLD_WT= @GOLDWT,STONE_WT= @DIAWT,CENTER_SIZE=@CENTERSIZE,
                                               CENTER_TYPE=@CENTERTYPE,CTR_SHAPE=@CENTERSHAPE,
                                               WEIGHT=@WEIGHT,BY_WT=@BYWT,TOTAL_COST=@TOTALCOST,ORDERNO=@ORDERNO,[DESC] = @DESC,DQLTY=@DQLTY,COLOR_WT=@COLOR_WT,CQLTY=@CQLTY ,
                                               attrib1=@Attrib1,attrib2=@Attrib2,attrib3=@Attrib3,attrib4=@Attrib4,attrib5=@Attrib5,attrib6=@Attrib6,attrib7=@Attrib7,attrib8=@Attrib8,
                                               attrib9=@Attrib9,attrib10=@Attrib10,attrib11=@Attrib11,attrib12=@Attrib12,
                                               attrib19=@Attrib19,attrib20=@Attrib20,attrib21=@Attrib21,
                                               attr_check1= @ATTR_CHECK1,attr_check2= @ATTR_CHECK2,attr_check3= @ATTR_CHECK3,attr_check4= @ATTR_CHECK4,attr_check5= @ATTR_CHECK5,attr_check6= @ATTR_CHECK6,
                                               Disclaimer=@Disclaimer,fieldvalue4=@Fieldvalue4,fieldvalue5=@Fieldvalue5,fieldvalue6=@Fieldvalue6,fieldvalue7=@Fieldvalue7,fieldvalue8=@Fieldvalue8,
                                               ItemType=@ItemType,CertType=@CertType,CertNo=@CertNo,Shape=@Shape,Culet=@Culet,Measurements=@Measurements,CertWeight=@CertWeight,Depth=@Depth,RapTable=@RapTable,Girdle=@Girdle,Polish=@Polish,Symmetry=@Symmetry,CutGrade=@CutGrade,Clarity=@Clarity,Color=@Color,
                                               Finger_Size=@Finger_Size,Silver_Wt=@Silver_Wt,Platinum_Wt=@Platinum_Wt,Minimum_Price=@Minimum_Price,
                                               TagInfo1=@TagInfo1,TagInfo2=@TagInfo2,TagInfo3=@TagInfo3,TagInfo4=@TagInfo4,PriceBywt=@Pricebywt,Labor=@Labor,LongDesc=@LongDesc,
                                               FAN_CLR= @FAN_CLR,FAN_INT=@FAN_INT,FAN_OVER=@FAN_OVER
                                               WHERE TEMPLATE_NAME=@TEMPLATE_NAME";
                }

                dbCommand.Parameters.AddWithValue("@ADDEDIT", BillTemplateModel.Issavenewtemplate);
                dbCommand.Parameters.AddWithValue("@TEMPLATE_NAME", BillTemplateModel.SaveImpTemplateName ?? "");
                dbCommand.Parameters.AddWithValue("@OldTemplate_Name", BillTemplateModel.ddlImpTemplate ?? "");
                dbCommand.Parameters.AddWithValue("@VSTYLE", BillTemplateModel.VendorStyleNo ?? "");
                dbCommand.Parameters.AddWithValue("@GROUP", BillTemplateModel.Group ?? "");
                dbCommand.Parameters.AddWithValue("@STYLE", BillTemplateModel.OurStyleNo ?? "");
                dbCommand.Parameters.AddWithValue("@PRICE", BillTemplateModel.Price ?? "");
                dbCommand.Parameters.AddWithValue("@QTY", BillTemplateModel.Quantity ?? "");
                dbCommand.Parameters.AddWithValue("@UCOST", BillTemplateModel.UnitCost ?? "");
                dbCommand.Parameters.AddWithValue("@BRAND", BillTemplateModel.Brand ?? "");
                dbCommand.Parameters.AddWithValue("@SUBBRAND", BillTemplateModel.SubBrand ?? "");
                dbCommand.Parameters.AddWithValue("@GLCASS", BillTemplateModel.GLClass ?? "");
                dbCommand.Parameters.AddWithValue("@CATEGORY", BillTemplateModel.Category ?? "");
                dbCommand.Parameters.AddWithValue("@SUBCATEGORY", BillTemplateModel.SubCat ?? "");
                dbCommand.Parameters.AddWithValue("@METAL", BillTemplateModel.Metal ?? "");
                dbCommand.Parameters.AddWithValue("@GOLDWT", BillTemplateModel.GoldWt ?? "");
                dbCommand.Parameters.AddWithValue("@DIAWT", BillTemplateModel.DiaWt ?? "");
                dbCommand.Parameters.AddWithValue("@CENTERSIZE", BillTemplateModel.CntrSize ?? "");
                dbCommand.Parameters.AddWithValue("@CENTERTYPE", BillTemplateModel.CntrType ?? "");
                dbCommand.Parameters.AddWithValue("@CENTERSHAPE", BillTemplateModel.CentShape ?? "");
                dbCommand.Parameters.AddWithValue("@WEIGHT", BillTemplateModel.Weight ?? "");
                dbCommand.Parameters.AddWithValue("@BYWT", BillTemplateModel.ByWt ?? "");
                dbCommand.Parameters.AddWithValue("@IMAGE", BillTemplateModel.IMAGE ?? "");
                dbCommand.Parameters.AddWithValue("@TOTALCOST", BillTemplateModel.TOTALCOST ?? "");
                dbCommand.Parameters.AddWithValue("@ORDERNO", BillTemplateModel.Orderno ?? "");
                dbCommand.Parameters.AddWithValue("@DESC", BillTemplateModel.Description ?? "");
                dbCommand.Parameters.AddWithValue("@DQLTY", BillTemplateModel.DiamQuality ?? "");
                dbCommand.Parameters.AddWithValue("@COLOR_WT", BillTemplateModel.ColorWt ?? "");
                dbCommand.Parameters.AddWithValue("@CQLTY", BillTemplateModel.ColorQuality ?? "");

                dbCommand.Parameters.AddWithValue("@Attrib1", BillTemplateModel.Attrib1 ?? "");
                dbCommand.Parameters.AddWithValue("@Attrib2", BillTemplateModel.Attrib2 ?? "");
                dbCommand.Parameters.AddWithValue("@Attrib3", BillTemplateModel.Attrib3 ?? "");
                dbCommand.Parameters.AddWithValue("@Attrib4", BillTemplateModel.Attrib4 ?? "");
                dbCommand.Parameters.AddWithValue("@Attrib5", BillTemplateModel.Attrib5 ?? "");
                dbCommand.Parameters.AddWithValue("@Attrib6", BillTemplateModel.Attrib6 ?? "");
                dbCommand.Parameters.AddWithValue("@Attrib7", BillTemplateModel.Attrib7 ?? "");
                dbCommand.Parameters.AddWithValue("@Attrib8", BillTemplateModel.Attrib8 ?? "");
                dbCommand.Parameters.AddWithValue("@Attrib9", BillTemplateModel.Attrib9 ?? "");
                dbCommand.Parameters.AddWithValue("@Attrib10", BillTemplateModel.Attrib10 ?? "");
                dbCommand.Parameters.AddWithValue("@Attrib11", BillTemplateModel.Attrib11 ?? "");
                dbCommand.Parameters.AddWithValue("@Attrib12", BillTemplateModel.Attrib12 ?? "");

                dbCommand.Parameters.AddWithValue("@Attrib19", BillTemplateModel.Attrib19 ?? "");
                dbCommand.Parameters.AddWithValue("@Attrib20", BillTemplateModel.Attrib20 ?? "");
                dbCommand.Parameters.AddWithValue("@Attrib21", BillTemplateModel.Attrib21 ?? "");

                dbCommand.Parameters.AddWithValue("@ATTR_CHECK1", BillTemplateModel.StyleCheck1 ?? "");
                dbCommand.Parameters.AddWithValue("@ATTR_CHECK2", BillTemplateModel.StyleCheck2 ?? "");
                dbCommand.Parameters.AddWithValue("@ATTR_CHECK3", BillTemplateModel.StyleCheck3 ?? "");
                dbCommand.Parameters.AddWithValue("@ATTR_CHECK4", BillTemplateModel.StyleCheck4 ?? "");
                dbCommand.Parameters.AddWithValue("@ATTR_CHECK5", BillTemplateModel.StyleCheck5 ?? "");
                dbCommand.Parameters.AddWithValue("@ATTR_CHECK6", BillTemplateModel.StyleCheck6 ?? "");
                dbCommand.Parameters.AddWithValue("@Disclaimer", BillTemplateModel.Disclaimer ?? "");

                dbCommand.Parameters.AddWithValue("@Fieldvalue4", BillTemplateModel.StyleField1 ?? "");
                dbCommand.Parameters.AddWithValue("@Fieldvalue5", BillTemplateModel.StyleField2 ?? "");
                dbCommand.Parameters.AddWithValue("@Fieldvalue6", BillTemplateModel.StyleField3 ?? "");
                dbCommand.Parameters.AddWithValue("@Fieldvalue7", BillTemplateModel.StyleField4 ?? "");
                dbCommand.Parameters.AddWithValue("@Fieldvalue8", BillTemplateModel.StyleField5 ?? "");

                dbCommand.Parameters.AddWithValue("@ItemType", BillTemplateModel.ItemType ?? "");
                dbCommand.Parameters.AddWithValue("@CertType", BillTemplateModel.CertType ?? "");
                dbCommand.Parameters.AddWithValue("@CertNo", BillTemplateModel.CertNo ?? "");
                dbCommand.Parameters.AddWithValue("@Shape", BillTemplateModel.Shape ?? "");
                dbCommand.Parameters.AddWithValue("@Culet", BillTemplateModel.Culet ?? "");
                dbCommand.Parameters.AddWithValue("@Measurements", BillTemplateModel.Measurements ?? "");
                dbCommand.Parameters.AddWithValue("@CertWeight", BillTemplateModel.CertWeight ?? "");
                dbCommand.Parameters.AddWithValue("@Depth", BillTemplateModel.Depth ?? "");
                dbCommand.Parameters.AddWithValue("@RapTable", BillTemplateModel.Table ?? "");
                dbCommand.Parameters.AddWithValue("@Girdle", BillTemplateModel.Girdle ?? "");
                dbCommand.Parameters.AddWithValue("@Polish", BillTemplateModel.Polish ?? "");
                dbCommand.Parameters.AddWithValue("@Symmetry", BillTemplateModel.Symmetry ?? "");
                dbCommand.Parameters.AddWithValue("@CutGrade", BillTemplateModel.CutGrade ?? "");
                dbCommand.Parameters.AddWithValue("@Clarity", BillTemplateModel.Clarity ?? "");
                dbCommand.Parameters.AddWithValue("@Color", BillTemplateModel.Color ?? "");

                dbCommand.Parameters.AddWithValue("@Finger_Size", BillTemplateModel.FingerSize ?? "");
                dbCommand.Parameters.AddWithValue("@Silver_Wt", BillTemplateModel.SilverWt ?? "");
                dbCommand.Parameters.AddWithValue("@Platinum_Wt", BillTemplateModel.PlatinumWt ?? "");
                dbCommand.Parameters.AddWithValue("@Minimum_Price", BillTemplateModel.MinPrice ?? "");

                dbCommand.Parameters.AddWithValue("@TagInfo1", BillTemplateModel.TagInfo1 ?? "");
                dbCommand.Parameters.AddWithValue("@TagInfo2", BillTemplateModel.TagInfo2 ?? "");
                dbCommand.Parameters.AddWithValue("@TagInfo3", BillTemplateModel.TagInfo3 ?? "");
                dbCommand.Parameters.AddWithValue("@TagInfo4", BillTemplateModel.TagInfo4 ?? "");
                dbCommand.Parameters.AddWithValue("@Pricebywt", BillTemplateModel.PriceByWt ?? "");
                dbCommand.Parameters.AddWithValue("@Labor", BillTemplateModel.Labor ?? "");
                dbCommand.Parameters.AddWithValue("@LongDesc", BillTemplateModel.LongDesc ?? "");
                dbCommand.Parameters.AddWithValue("@FAN_CLR", BillTemplateModel.FancyColor ?? "");
                dbCommand.Parameters.AddWithValue("@FAN_INT", BillTemplateModel.FancyIntensity ?? "");
                dbCommand.Parameters.AddWithValue("@FAN_OVER", BillTemplateModel.FancyOvertone ?? "");
                // Open the connection, execute the query and close the connection
                dbCommand.Connection.Open();
                var rowsAffected = dbCommand.ExecuteNonQuery();
                dbCommand.Connection.Close();

                return rowsAffected > 0;
            }
        }
        public DataTable GetDistinctStyles()
        {
            return _helperCommonService.GetSqlData("SELECT DISTINCT STYLE  from STYLES WITH (NOLOCK)");
        }
        public bool IsStylebarcode(string style)
        {
            return _helperCommonService.GetSqlData("select * from styles where TRIM(BARCODE)=TRIM(@style)", "@style", style).Rows.Count > 0;
        }
        public DataTable ImportDataOnGrid(string billOrCons, string dtXml, string invNo, string vendacc)
        {
            return _helperCommonService.GetStoreProc("GetLoadedData", "@Bill_Cons", billOrCons, "@INVNO", invNo, "@vendacc", vendacc, "@IMPDATA", dtXml);
        }
        public DataTable GetBillItemsByInvNo_GL(string inv_no)
        {
            return _helperCommonService.GetSqlData("SELECT ISNULL(GL_LOG,'') GL_LOG,DATE BILL_DT,ISNULL(AMOUNT,0) BILL_AMT FROM BILLS with (nolock) WHERE INV_NO = @inv_no", "@inv_no", inv_no);
        }
        public string Asciiinvisiblecharcheck(string styleno)
        {
            if (string.IsNullOrEmpty(styleno))
                return "";

            StringBuilder result = new StringBuilder(styleno.Length);

            foreach (char ch in styleno)
            {
                // Append only printable ASCII characters
                if (ch >= 32)
                    result.Append(ch);
            }
            return result.ToString();
        }
        public DataTable CheckVendorStyle(string vndstyle, string group, bool isstylenobycat = false)
        {
            if (isstylenobycat)
                return _helperCommonService.GetSqlData("SELECT * FROM styles with (nolock) WHERE TRIM(vnd_style)=TRIM(@vndstyle) and TRIM(category)=TRIM(@group)", "@vndstyle", vndstyle, "@group", group);
            return _helperCommonService.GetSqlData("SELECT * FROM styles with (nolock) WHERE TRIM(vnd_style)=TRIM(@vndstyle) and TRIM([group])=TRIM(@group)", "@vndstyle", vndstyle, "@group", group);
        }

        public bool iSNumeric(string input)
        {
            BigInteger result;
            return BigInteger.TryParse(input, out result);
        }
        public bool IsGroupDashActive(string Group)
        {
            DataRow dataRow = _helperCommonService.GetSqlRow("Select use_dash from groups with (nolock) where trim([group])=@Group", "@Group", Group.Trim());
            if (_helperCommonService.DataTableOK(dataRow))
                return dataRow["use_dash"] == null || dataRow["use_dash"] == DBNull.Value || dataRow["use_dash"].ToString() == string.Empty ? false : Convert.ToBoolean(dataRow["use_dash"]);
            return false;
        }

    }
}
