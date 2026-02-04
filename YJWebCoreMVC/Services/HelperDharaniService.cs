using Microsoft.Data.SqlClient;
using System.Data;
using System.Xml;

namespace YJWebCoreMVC.Services
{
    public class HelperDharaniService
    {

        private readonly ConnectionProvider _connectionProvider;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly HelperCommonService _helperCommonService;
        private readonly IWebHostEnvironment _env;
        private readonly string _companyName;
        private readonly string _storeCodeInUse;
        private readonly string _loggedUser;

        public HelperDharaniService(ConnectionProvider connectionProvider, IHttpContextAccessor httpContextAccessor, HelperCommonService helperCommonService, IWebHostEnvironment env)
        {
            _connectionProvider = connectionProvider;
            _httpContextAccessor = httpContextAccessor;
            _helperCommonService = helperCommonService;
            _companyName = _httpContextAccessor.HttpContext?.Session.GetString("COMPANYNAME");
            _storeCodeInUse = _httpContextAccessor.HttpContext?.Session.GetString("STORE_CODE");
            _loggedUser = _httpContextAccessor.HttpContext?.Session.GetString("UserId");
            _env = env;
        }

        public bool adjustStockNoAffectGL = false, is_Catherine, frmCustomerPOReport, iS_Crisson;
        public string po_prefix = "PON", Po_Note = string.Empty;

        public string GetModifiedRdlcXml(string rdlcFileName, List<float> widths, string tablixName = "Tablix1")
        {
            string reportPath = Path.Combine(_env.ContentRootPath, "reports", rdlcFileName);

            if (!File.Exists(reportPath))
                throw new FileNotFoundException($"RDLC file not found: {reportPath}");

            XmlDocument doc = new XmlDocument();
            doc.Load(reportPath);

            XmlNamespaceManager nsmgr = new XmlNamespaceManager(doc.NameTable);
            nsmgr.AddNamespace("rd", "http://schemas.microsoft.com/SQLServer/reporting/reportdesigner");
            nsmgr.AddNamespace("def", "http://schemas.microsoft.com/sqlserver/reporting/2016/01/reportdefinition");

            string xpath = $"//def:Tablix[@Name='{tablixName}']/def:TablixBody/def:TablixColumns";
            XmlNode tablixColumns = doc.SelectSingleNode(xpath, nsmgr);

            if (tablixColumns == null)
                throw new Exception($"Tablix with name '{tablixName}' not found or has no TablixColumns.");

            XmlNodeList columnNodes = tablixColumns.SelectNodes("def:TablixColumn", nsmgr);

            if (columnNodes == null || columnNodes.Count == 0)
                throw new Exception("No TablixColumn elements found.");

            if (columnNodes.Count != widths.Count)
                throw new InvalidOperationException(
                    $"Column count mismatch: RDLC has {columnNodes.Count} columns, but {widths.Count} widths were provided.");

            for (int i = 0; i < columnNodes.Count; i++)
            {
                XmlNode widthNode = columnNodes[i].SelectSingleNode("def:Width", nsmgr);
                if (widthNode == null)
                {
                    widthNode = doc.CreateElement("Width", "http://schemas.microsoft.com/sqlserver/reporting/2016/01/reportdefinition");
                    columnNodes[i].AppendChild(widthNode);
                }
                widthNode.InnerText = $"{widths[i]}in";
            }

            using (StringWriter sw = new StringWriter())
            using (XmlTextWriter xw = new XmlTextWriter(sw) { Formatting = Formatting.Indented })
            {
                doc.WriteTo(xw);
                return sw.ToString();
            }
        }
        private class Utf8StringWriter : StringWriter
        {
            public override System.Text.Encoding Encoding => System.Text.Encoding.UTF8;
        }
        public DataTable GetOptionDataBYBRAND(string fldname, Boolean bullion = false)
        {
            return _helperCommonService.GetSqlData("SELECT SUBBRAND FROM SUBBRANDS with (nolock) WHERE brand =  '" + fldname.Replace("'", "''") + "' and bullion = '" + bullion + "' order by subbrand ");
        }
        public DataTable GetOptionDataBYCAT(string fldname, Boolean bullion = false)
        {
            return _helperCommonService.GetSqlData("SELECT  SUBCATEGORY,prefix  FROM  SUBCATS WHERE CATEGORY = @fldname and bullion = '" + bullion + "' order by SUBCATEGORY ", "@fldname", fldname);
        }
        public DataTable AddNewSubcatOrBrandOption(string colname, string user, string leveldet, string tabletype, bool bullion)
        {
            return _helperCommonService.GetStoreProc("AddOrEditOptionSUBCATORBRAND1Values", "@CATNAME", colname, "@LOGGEDUSER", user, "@OPTIONTYPE", tabletype, "@BULLION", bullion.ToString(), "@TBLOPTIONSUBCATFILE", leveldet);
        }
        public DataTable DeleteSubcatOptionVal(string catname, string subcat)
        {
            return _helperCommonService.GetSqlData(@"delete from subcats where subcategory = RTRIM('" + subcat.Replace("'", "''") + "') and category = TRIM('" + catname.Replace("'", "''") + "') ");
        }
        public DataTable DeleteSubBrandOptionVal(string catname, string subcat)
        {
            return _helperCommonService.GetSqlData(@"delete from subbrands with (nolock) where subbrand = TRIM('" + subcat.Replace("'", "''") + "') and brand = tRIM('" + catname.Replace("'", "''") + "') ");
        }
        public DataTable GetCastingMetal()
        {
            return _helperCommonService.GetSqlData("select * from castingmetal");
        }
        public DataTable getwarrantiedataInOrder()
        {
            return _helperCommonService.GetSqlData(@"select CODE,DESCRIPTION,IS_DEFAULT,price,COST,by_percent,min_price,max_price,no_warranty from Warranties with (nolock)");
        }
        public DataTable AddWarranties(DataTable leveldet, int DefaultWarrantyPeriod)
        {
            return _helperCommonService.GetStoreProc("WARRANTIE", "@WARRANTIES", _helperCommonService.GetDataTableXML("Warranty", leveldet), "@DefaultWarrantyPeriod", DefaultWarrantyPeriod.ToString());
        }
        public DataTable DeleteWarrantie(string code)
        {
            return _helperCommonService.GetSqlData(@"delete from WARRANTIES where code = TRIM('" + code.Replace("'", "''") + "') ");
        }
        public DataTable GetInfoToExport(string invno)
        {
            return _helperCommonService.GetStoreProc("GETINFOTOEXCEL", "@MEMO", invno);
        }
        public DataTable GetInvoiceDetailWithPickupDate(string inv = "", string pdate = "", string Checkvalid = "")
        {
            if (Checkvalid == "Validation")
                return _helperCommonService.GetSqlData($"Select Inv_no,Acc,Date,Pickupdate from invoice Where Trimmed_inv_no =trim('{inv}')");
            if (pdate != "")
                return _helperCommonService.GetSqlData($"Update invoice set  Pickupdate= '{pdate}' ,NotPickedUp= 0,PICKED=1 where Trimmed_inv_no=trim('{inv}')");
            return _helperCommonService.GetSqlData($"Select Inv_no,Acc,Date,Pickupdate from invoice Where Trimmed_inv_no =trim('{inv}')");
        }
        public DataTable GetPartners()
        {
            return _helperCommonService.GetSqlData("select *,'1' as STATUS from partners with(nolock) order by name asc");
        }
        public bool UpdatePartnerName(string names)
        {
            using (var connection = _connectionProvider.GetConnection())
            using (var command = new SqlCommand("UpdatePartnerName", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@PartName", SqlDbType.Xml) { Value = names });
                connection.Open();
                return command.ExecuteNonQuery() > 0;
            }
        }
        public bool AddDeductReferralLoyaltyPoints(ReferralLoyalty refloy)
        {
            using (var connection = _connectionProvider.GetConnection())
            using (var dbCommand = new SqlCommand("INSERT INTO [dbo].[referal_point]([acc],[date],[AMOUNT],[what],[inv_no],[IsLoyalty],[who]) " +
                                                    "VALUES(@ACC,@DATE, IIF(ISNULL(@OPT, '') = '1', @AMOUNT, ISNULL(@AMOUNT, 0) * -1), @WHAT, @INVNO, @ISLOYALTY,@Loggeduser)", connection))
            {
                dbCommand.CommandType = CommandType.Text;

                dbCommand.Parameters.AddWithValue("@ACC", refloy.Acc);
                dbCommand.Parameters.AddWithValue("@DATE", refloy.Date);
                dbCommand.Parameters.AddWithValue("@AMOUNT", refloy.Points);
                dbCommand.Parameters.AddWithValue("@WHAT", refloy.Notes);
                dbCommand.Parameters.AddWithValue("@ISLOYALTY", refloy.PointMethod);
                dbCommand.Parameters.AddWithValue("@INVNO", string.Empty);
                dbCommand.Parameters.AddWithValue("@OPT", refloy.Option);
                dbCommand.Parameters.AddWithValue("@Loggeduser", refloy.Loggeduser);

                connection.Open();
                var rowsAffected = dbCommand.ExecuteNonQuery();

                return rowsAffected > 0;
            }
        }
        public DataTable GetRtvDetailsBasedOnNumber(string rtvno, string custrtvno)
        {
            return _helperCommonService.GetSqlData("Select * from rtv_head with (nolock) where inv_no ='" + rtvno + "' or rtv_no = '" + custrtvno + "'");
        }
        public DataTable getmemosfmandothercharges(string rtvnumber, string billingacctnumber)
        {
            return _helperCommonService.GetSqlData(@"SELECT RI.MEMO_NO,RI.STYLE AS STYLE,RI.SIZE AS SIZE,S.[DESC]+' '+ IIF(RI.MEMO_NO != '' OR RI.INVOIC_NO != '',IIF(RI.MEMO_NO != '','M #'+TRIM(RI.MEMO_NO),'I #'+TRIM(RI.INVOIC_NO)),'') AS DESCRIPT,
            cast(RI.QTY as decimal(8,1)) AS QTY,RI.[WEIGHT] AS [WEIGHT],RI.PRICE AS PRICE, (case when RI.BY_WT = 0 then convert(decimal(10, 2), (RI.PRICE * RI.QTY)) when RI.BY_WT = 1 then convert(decimal(10, 2), (RI.PRICE * RI.[WEIGHT]))end) AS AMOUNT,
            RI.[WEIGHT],RI.BY_WT,RI.SFMED,RI.BILLED 
            FROM RTV_ITEM RI with (nolock)
            LEFT JOIN STYLES S with (nolock) ON S.STYLE = Dbo.Invstyle(RI.STYLE) WHERE RI.INV_NO = RIGHT('      ' + TRIM(@rtvnumber), 6)",
            "@rtvnumber", rtvnumber, "@billingacctnumber", billingacctnumber);
        }
        public DataTable Getsnhandotherchargesdetailsbasedonmemo(string invno)
        {
            return _helperCommonService.GetSqlData("SELECT snh,add_cost FROM memo with (nolock) WHERE MEMO_NO IN(" + invno + ")");
        }
        public DataTable GetInventoryofwip()
        {
            return _helperCommonService.GetStoreProc("GetInventoryofwip");
        }
        public DataTable ShowDetailedPartsHist(string code)
        {
            return _helperCommonService.GetSqlData("SELECT CODE, DESCRIPTION, [USER], JOB_BAG AS [JOB BAG], DATE, VENDOR, IIF(by_wt = 1, change_wt, change) * cost AS Cost, change AS PCS, change_wt AS Weight FROM PARTS_HIST WITH (NOLOCK) WHERE CODE = @CODE AND LEFT(code, 4) <> 'code' AND code <> 'code' AND job_bag NOT IN (SELECT inv_no FROM mfg WITH (NOLOCK) WHERE closed = 1 OR rcvd > 0) ORDER BY DATE DESC", "@CODE", code);
        }
        public DataTable GetRepairnotedata(String sku, bool ShowAll = false)
        {
            string qry = ShowAll ?
                @"SELECT DISTINCT TRIM(SKU) SKU,NOTE,PRICE,RUSH_PRICE,COST,MATERIAL,OVERHEAD,CONVERT(INT,IS_FIXED)IS_FIXED,isnuLL(NOTAX,0) as NOTAX,isnull(REQUIRE_SIZE,0) as REQUIRE_SIZE, ESTIMATE FROM Repair_notes Where IS_FIXED=0 order by sku" :
                @"SELECT DISTINCT TRIM(SKU) SKU,NOTE,PRICE,RUSH_PRICE,COST,MATERIAL,OVERHEAD,CONVERT(INT,IS_FIXED)IS_FIXED,isnuLL(NOTAX,0) as NOTAX,isnull(REQUIRE_SIZE,0) as REQUIRE_SIZE, ESTIMATE FROM Repair_notes WHERE TRIM(SKU)='" + sku.Trim().Replace("'", "''") + "'";
            return _helperCommonService.GetSqlData(qry);
        }
        public bool Updaterepairnotecode(string originalcode, string renamecode)
        {
            using (var connection = _connectionProvider.GetConnection())
            using (var dbCommand = new SqlCommand())
            {
                dbCommand.Connection = connection;
                dbCommand.CommandType = CommandType.Text;

                if (renamecode == "Delete")
                    dbCommand.CommandText = "DELETE FROM Repair_notes WHERE TRIM(sku) = TRIM(@originalCode)";
                else
                    dbCommand.CommandText = "UPDATE Repair_notes SET SKU = @renameCode WHERE TRIM(sku) = TRIM(@originalCode)";
                dbCommand.Parameters.AddWithValue("@originalCode", originalcode);
                dbCommand.Parameters.AddWithValue("@renameCode", renamecode);

                connection.Open();
                var rowsAffected = dbCommand.ExecuteNonQuery();

                return rowsAffected > 0;
            }
        }
        public int UpdateCustRepNotes(string data)
        {
            const string procedureName = "AddCustomRepairtNotes";

            using (var connection = _connectionProvider.GetConnection())
            using (var command = new SqlCommand(procedureName, connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandTimeout = 6000;
                command.Parameters.Add(new SqlParameter("@TBLCUSTREPITEMS", SqlDbType.Xml) { Value = data });

                var returnValue = new SqlParameter("@RETVAL", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Output
                };
                command.Parameters.Add(returnValue);

                connection.Open();
                using (var adapter = new SqlDataAdapter(command))
                {
                    adapter.Fill(new DataTable());
                }

                return Convert.ToInt32(returnValue.Value);
            }
        }
        public DataTable GetCustRepairCode()
        {
            return _helperCommonService.GetSqlData($"SELECT SKU, NOTE, COST, PRICE, RUSH_PRICE FROM REPAIR_NOTES WHERE IS_FIXED=0");
        }
        public string Getimagepaths(string styleName)
        {
            if (string.IsNullOrWhiteSpace(styleName))
                return string.Empty;

            const string procedureName = "Getpaths";
            string imagePath = string.Empty;

            using (var connection = _connectionProvider.GetConnection())
            using (var command = new SqlCommand(procedureName, connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@stylename", styleName);

                connection.Open();

                using (var reader = command.ExecuteReader())
                {
                    byte[] imageBytes = null;

                    if (reader.Read())
                        imageBytes = reader[0] as byte[];

                    if (imageBytes != null)
                    {
                        string fileName = $"{_helperCommonService.RemoveSpecialCharacters(styleName)}_{DateTime.Now:yyyyMMddHHmmssfff}.jpg";
                        imagePath = Path.Combine(Path.GetTempPath(), fileName);

                        // Commented at yjcore migration
                        //using (var memoryStream = new MemoryStream(imageBytes))
                        //using (var image = System.Drawing.Image.FromStream(memoryStream))
                        //{
                        //    image.Save(imagePath);
                        //}
                    }
                }
            }

            return string.IsNullOrEmpty(imagePath) ? string.Empty : $"File:{imagePath}";
        }
        public string GetJobsByPon(string pon)
        {
            return _helperCommonService.GetValue(_helperCommonService.GetSqlData(@"SELECT  top 1 BARCODE FROM OR_ITEMS WHERE LTRIM(RTRIM(PON))=LTRIM(RTRIM(@PON))", "@pon", pon), "BARCODE");
        }
        public DataTable IsSplitJobbagOrNot(string jobbagno)
        {
            return _helperCommonService.GetSqlData("select * from lbl_bar with (nolock) where barcode = @jobbagno", "@jobbagno", jobbagno);
        }
        public bool iSFromCustomerRepair(String inv_no)
        {
            DataTable dtCanceled = _helperCommonService.GetSqlData($@"select 1 from invoice WITH (NOLOCK) where inv_no=@inv_no and v_ctl_no='repair'", "@inv_no", inv_no);
            return _helperCommonService.DataTableOK(dtCanceled);
        }
        public string GetImageforStyle(string p_style, bool iSFromReprintInvoice = false)
        {
            string imagename = string.Empty;
            if (string.IsNullOrWhiteSpace(p_style))
                return string.Empty;
            p_style = GetStyle(p_style).Replace("/", "");
            using (SqlConnection con = _connectionProvider.GetConnection())
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("GetStyleImages", con))
                {
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@style", p_style);
                    if (iSFromReprintInvoice)
                    {
                        cmd.Parameters.AddWithValue("@norows", 1);
                        cmd.Parameters.AddWithValue("@iSFromAccordion", true);
                    }
                    SqlDataReader rdr = cmd.ExecuteReader();
                    byte[] objContext = null;
                    while (rdr.Read())
                    {
                        string filePath = rdr[0].ToString();
                        objContext = (byte[])rdr[1];
                    }
                    if (objContext != null)
                    {
                        using (MemoryStream ms = new MemoryStream(objContext))
                        {
                            // Commented at yjcore migration
                            //System.Drawing.Image i = System.Drawing.Image.FromStream(ms);
                            //imagename = Path.Combine(new string[] { System.IO.Path.GetTempPath(), string.Format("{0}{1}{2}", RemoveSpecialCharacters(p_style), DateTime.Now.ToString("yyyyMMddHHmmssfff"), ".jpg") });
                            //i.Save(imagename);
                            //i.Dispose();
                        }
                    }
                    rdr.Close();
                }
                con.Close();
            }
            return "File:" + imagename;
        }
        public string GetStyle(string style)
        {
            if (_helperCommonService.CheckModuleEnabled(HelperCommonService.Modules.SN_Images) || _helperCommonService.CheckModuleEnabled(HelperCommonService.Modules.ModelImages))
            {
                style = _helperCommonService.GetBeforeUnderScore(style);
                string beforeHyphen = _helperCommonService.GetBeforeHyphen(style);
                string baseStyle = beforeHyphen.Length >= 2 ? beforeHyphen : style;

                if (_helperCommonService.CheckModuleEnabled(HelperCommonService.Modules.SN_Images))
                {
                    string suffix = style.Replace($"{baseStyle}-", "");
                    int temp;
                    if (int.TryParse(suffix, out temp))
                    {
                        return _helperCommonService.InvStyle(baseStyle);
                    }
                    return _helperCommonService.InvStyle(style);
                }
                return baseStyle;
            }
            return style;
        }
        public string GetAnnivNote(String cust)
        {
            return _helperCommonService.GetValue0(_helperCommonService.GetSqlData(@"Select IIF(ISNULL(note,'') = '',(RIGHT(CONCAT('00', [month]), 2) + '/' + RIGHT(CONCAT('00', dat), 2)), note) as ANNIV_NOTE from occassions where UPPER(type) LIKE 'AN%' AND LTRIM(RTRIM(ACC))=LTRIM(RTRIM(@cust))", "@cust", cust));
        }
        public DataTable GetOrderbyCustPOBasedOnShowname(string showname)
        {
            return _helperCommonService.GetSqlData("select * from orders with (nolock) where cust_ref = @showname", "@showname", showname);
        }
        public DataTable GetPONList(string fdate, string tdate, string acc)
        {
            string[] fromdate = fdate.Split(' ');
            string dtf = fromdate[0];
            string[] todate = tdate.Split(' ');
            string dtt = todate[0];
            return _helperCommonService.GetSqlData("SELECT o.PON, o.Order_Date, o.Acc, SUM(oi.qty) Total_Pcs, SUM( oi.qty*oi.price) Total_Amount, Cast(iif(o.UnApproved=1,0,1) as bit) Approve FROM ORDERs o with (nolock) LEFT JOIN OR_ITEMS oi with (nolock) ON o.PON = oi.PON WHERE (@ACC='' OR ACC=@ACC) AND ORDER_DATE BETWEEN @date1 AND @date2  AND UnApproved=1 GROUP BY O.PON, o.order_Date, o.Acc, o.UnApproved ",
                "@date1", dtf, "@date2", dtt, "@acc", acc);
        }
        public DataTable UpdatePOApproveStatus(string POData, string LoggedUser)
        {
            return _helperCommonService.GetStoreProc("UpdatePOApproveStatus", "@PO_DATA", POData, "@LOG_USER", LoggedUser);
        }
        public bool ImportVendorDataExcel(string resultpoData)
        {
            try
            {
                _helperCommonService.GetStoreProc("VENDORIMPORTEXCEL", "@VendorImport", resultpoData);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Database Error: " + ex.Message);
            }
        }
    }

    public class ReferralLoyalty
    {
        public string Acc { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        public bool PointMethod { get; set; } = false;
        public decimal Points { get; set; } = 0;
        public string Notes { get; set; } = string.Empty;
        public string Option { get; set; } = string.Empty;
        public string Loggeduser { get; set; } = string.Empty;
    }

}
