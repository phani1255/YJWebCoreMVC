/*Neetha   01/03/2025 Created new model.*/
/*Neetha    01 / 03 /2025 Renamed customermodel to CustomerModelNew.*/
/*
 * Phanindra 03/26/2025 Added 2 CheckValidCustomerCode with different parameters
 * Dharani   09/15/2025 Added GetCustomerMultiAttr, UpdateCustomerAttr, AddNewAttribute, UpdateAttribute  Methods.
 * Dharani   09/16/2025 Added CustomerAttributeViewModel
 * Dharani   09/25/2025 Added ShowCustomerNotes method.
 * Dharani   09/26/2025 Added AddNewCustNote method.
 * Dharani   09/30/2025 Added CheckValidBillingAcct method.
 * Dharani   10/15/2025 Added GetCustomerImportTemplate, CheckCustomerImportTemplate, AddCustomerImportTemplate methods and CustomerImportTemplateModel
 * Dharani   10/17/2025 Added CheckTemplateExistOrNot, ImportCustomerData and CheckAccExistsOrNot_Import
 * Dharani   10/23/2025 Added ImportCustomerForEvent method.
 * Dharani   10/27/2025 Added ImportOccasions
 * Dharani   12/16/2025 Added GetCurrencyConvDetails and UpdateCurrencyConvDetails methods.
 * venkat    12/25/2025 Added _Noreffal,_Action,_LoyaltyProgram
 */

using Microsoft.Data.SqlClient;
using System.Data;
using System.Text;

namespace YJWebCoreMVC.Services
{
    public class CustomerNewService
    {
        private readonly ConnectionProvider _connectionProvider;
        private readonly HelperCommonService _helperCommonService;

        public CustomerNewService(ConnectionProvider connectionProvider, HelperCommonService helperCommonService, IWebHostEnvironment env)
        {
            _connectionProvider = connectionProvider;
            _helperCommonService = helperCommonService;
        }


        public DataRow CheckValidCustomerCode(string acc, bool is_glenn, bool iSWrist = false)
        {
            if (iSWrist)
            {
                DataRow rw = _helperCommonService.GetSqlRow("select [NAME2] NAME From Customer Where trim(acc)=trim(@acc) or old_customer=trim(@acc)", "@acc", acc);
                if (rw == null || String.IsNullOrWhiteSpace(Convert.ToString(rw["NAME"])))
                    return _helperCommonService.GetSqlRow("select *  From Customer Where trim(acc)=trim(@acc) or old_customer=trim(@acc)", "@acc", acc);
                return _helperCommonService.GetSqlRow("select [NAME2] NAME, [ADDR2] ADDR1,[ADDR22] ADDR12,[CITY2] CITY1,[STATE2] STATE1,[ZIP2] ZIP1,ADDR13,[COUNTRY2] COUNTRY,[TEL2] TEL,*  From Customer Where trim(acc)=trim(@acc) or old_customer=trim(@acc)", "@acc", acc);
            }
            return _helperCommonService.GetSqlRow("select *  From Customer Where trim(acc)=trim(@acc) or old_customer=trim(@acc)", "@acc", acc);
        }

        public DataRow CheckValidCustomerCode(string acc)
        {
            return _helperCommonService.GetSqlRow("SELECT TOP 1 * FROM Customer WHERE LTRIM(RTRIM(acc))=LTRIM(RTRIM(@acc))", "@acc", acc);
        }
        public SqlDataReader GetCustomerMultiAttr(string[] attrNumbers)
        {
            // Build the query using a StringBuilder for better performance and clarity
            var queryBuilder = new StringBuilder();
            foreach (var attrNum in attrNumbers)
            {
                int parsed;
                if (!int.TryParse(attrNum, out parsed))
                    throw new ArgumentException("All attribute numbers must be valid integers.", nameof(attrNumbers));

                queryBuilder.AppendLine($"SELECT DISTINCT ISNULL(ATTR_VAL, '') AS AttrVal, ATTR_NUM FROM CUST_ATT WHERE ATTR_NUM = {attrNum} UNION");
            }

            // Remove the trailing "UNION" and append the order clause
            string query = queryBuilder.ToString();
            query = query.Substring(0, query.LastIndexOf("UNION")).Trim() + " ORDER BY ATTR_NUM";

            var connection = _connectionProvider.GetConnection();
            var command = new SqlCommand(query, connection);

            connection.Open();
            return command.ExecuteReader(CommandBehavior.CloseConnection);
        }
        public class CustomerModel
        {
            public string CUSTATTRLABELS { get; set; }
        }
        public bool UpdateCustomerAttr(CustomerModel customer)
        {
            using (var connection = _connectionProvider.GetConnection())
            using (var command = new SqlCommand("AddupdateCustAttr", connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add(new SqlParameter("@CUSTATTRLABELS", SqlDbType.Xml)
                {
                    Value = customer.CUSTATTRLABELS
                });

                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();

                return rowsAffected > 0;
            }
        }
        public bool AddNewAttribute(string attr_val, string attr_num)
        {
            using (SqlConnection dbConnection = _connectionProvider.GetConnection())
            using (SqlCommand dbCommand = new SqlCommand(@"INSERT INTO cust_att (attr_num, attr_val) VALUES (@attr_num, @attr_val)", dbConnection))
            {
                // Set up the parameters explicitly
                dbCommand.Parameters.Add("@attr_num", SqlDbType.VarChar).Value = attr_num;
                dbCommand.Parameters.Add("@attr_val", SqlDbType.VarChar).Value = attr_val;
                dbConnection.Open();
                var rowsAffected = dbCommand.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }
        public bool UpdateAttribute(string attr_oldval, string attr_val, string attr_num)
        {
            using (SqlConnection dbConnection = _connectionProvider.GetConnection())
            using (SqlCommand dbCommand = new SqlCommand(@"UPDATE cust_att SET attr_val = @attr_val WHERE attr_val = @attr_oldval AND attr_num = @attr_num", dbConnection))
            {
                // Explicitly set the parameters
                dbCommand.Parameters.Add("@attr_val", SqlDbType.VarChar).Value = attr_val;
                dbCommand.Parameters.Add("@attr_oldval", SqlDbType.VarChar).Value = attr_oldval;
                dbCommand.Parameters.Add("@attr_num", SqlDbType.VarChar).Value = attr_num;
                dbConnection.Open();
                var rowsAffected = dbCommand.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }
        public class CustomerAttributeViewModel
        {
            public string AttrVal { get; set; }
        }
        public DataTable ShowCustomerNotes(string acc)
        {
            return _helperCommonService.GetSqlData(@"select WHO as [User] ,DTIME as Date,[TYPE] as Type ,NOTE as Note ,followup as FollowUp, completed as Completed, time as FollowUp_Time,reminder as Reminder from CUSTNOTE where acc= trim(@acc) order by DTIME", "@acc", acc);
        }
        public DataTable AddNewCustNote(string acc, string user, string leveldet) //OK
        {
            return _helperCommonService.GetStoreProc("CUSTNOTES", "@ACC", acc, "@LOGGEDUSER", user, "@TBLCUSTNOTES", leveldet);
        }
        public DataRow CheckValidBillingAcct(string billacc)
        {
            return _helperCommonService.GetSqlRow("select *  From Customer with (nolock) Where trim(bill_acc)= trim(@bill_acc) and bill_acc = acc", "@bill_acc", billacc);
        }
        public DataTable GetCustomerImportTemplate(string strTemplateName)
        {
            if (string.IsNullOrWhiteSpace(strTemplateName.Trim()))
                return _helperCommonService.GetSqlData("Select * from CUSTOMER_Template with(nolock) order by Template_Name");
            return _helperCommonService.GetSqlData("Select * from CUSTOMER_Template with(nolock) where Template_Name like '%" + strTemplateName + "%' order by Template_Name");
        }
        public DataTable CheckCustomerImportTemplate(string strTemplateName)
        {
            return _helperCommonService.GetSqlData(@"Select * from CUSTOMER_Template with(nolock) where Template_Name = @TemplateName", "@TemplateName", strTemplateName);
        }
        public bool AddCustomerImportTemplate(CustomerImportTemplateModel customerImportTemplateModel)
        {
            using (SqlCommand dbCommand = new SqlCommand())
            {
                // Set the command object properties
                dbCommand.Connection = _connectionProvider.GetConnection();
                dbCommand.CommandType = CommandType.StoredProcedure;
                dbCommand.CommandText = "AddEditCustomerImportTemplateData";
                dbCommand.Parameters.AddWithValue("@ADDEDIT", string.IsNullOrEmpty(customerImportTemplateModel.AddEdit) ? "" : customerImportTemplateModel.AddEdit);
                dbCommand.Parameters.AddWithValue("@TEMPLATE_NAME", string.IsNullOrEmpty(customerImportTemplateModel.Template_Name) ? "" : customerImportTemplateModel.Template_Name);
                dbCommand.Parameters.AddWithValue("@ACC", string.IsNullOrEmpty(customerImportTemplateModel.ACC) ? "" : customerImportTemplateModel.ACC);
                dbCommand.Parameters.AddWithValue("@NAME", string.IsNullOrEmpty(customerImportTemplateModel.NAME) ? "" : customerImportTemplateModel.NAME);
                dbCommand.Parameters.AddWithValue("@ADDR1", string.IsNullOrEmpty(customerImportTemplateModel.ADDR1) ? "" : customerImportTemplateModel.ADDR1);
                dbCommand.Parameters.AddWithValue("@ADDR12", string.IsNullOrEmpty(customerImportTemplateModel.ADDR12) ? "" : customerImportTemplateModel.ADDR12);
                dbCommand.Parameters.AddWithValue("@ADDR13", string.IsNullOrEmpty(customerImportTemplateModel.ADDR13) ? "" : customerImportTemplateModel.ADDR13);
                dbCommand.Parameters.AddWithValue("@STATE1", string.IsNullOrEmpty(customerImportTemplateModel.STATE1) ? "" : customerImportTemplateModel.STATE1);
                dbCommand.Parameters.AddWithValue("@CITY1", string.IsNullOrEmpty(customerImportTemplateModel.CITY1) ? "" : customerImportTemplateModel.CITY1);
                dbCommand.Parameters.AddWithValue("@ZIP1", string.IsNullOrEmpty(customerImportTemplateModel.ZIP1) ? "" : customerImportTemplateModel.ZIP1);
                dbCommand.Parameters.AddWithValue("@TEL", string.IsNullOrEmpty(customerImportTemplateModel.TEL) ? "" : customerImportTemplateModel.TEL);
                dbCommand.Parameters.AddWithValue("@TEL2", string.IsNullOrEmpty(customerImportTemplateModel.TEL2) ? "" : customerImportTemplateModel.TEL2);
                dbCommand.Parameters.AddWithValue("@COUNTRY", string.IsNullOrEmpty(customerImportTemplateModel.COUNTRY) ? "" : customerImportTemplateModel.COUNTRY);
                dbCommand.Parameters.AddWithValue("@WWW", string.IsNullOrEmpty(customerImportTemplateModel.WWW) ? "" : customerImportTemplateModel.WWW);
                dbCommand.Parameters.AddWithValue("@EMAIL", string.IsNullOrEmpty(customerImportTemplateModel.EMAIL) ? "" : customerImportTemplateModel.EMAIL);
                dbCommand.Parameters.AddWithValue("@TAX_ID", string.IsNullOrEmpty(customerImportTemplateModel.TAX_ID) ? "" : customerImportTemplateModel.TAX_ID);
                dbCommand.Parameters.AddWithValue("@EST_DATE", string.IsNullOrEmpty(customerImportTemplateModel.EST_DATE) ? "" : customerImportTemplateModel.EST_DATE);
                dbCommand.Parameters.AddWithValue("@PRICE_FILE", string.IsNullOrEmpty(customerImportTemplateModel.PRICE_FILE) ? "" : customerImportTemplateModel.PRICE_FILE);
                dbCommand.Parameters.AddWithValue("@JBT", string.IsNullOrEmpty(customerImportTemplateModel.JBT) ? "" : customerImportTemplateModel.JBT);
                dbCommand.Parameters.AddWithValue("@NAME2", string.IsNullOrEmpty(customerImportTemplateModel.NAME2) ? "" : customerImportTemplateModel.NAME2);
                dbCommand.Parameters.AddWithValue("@BILL_ACC", string.IsNullOrEmpty(customerImportTemplateModel.BILL_ACC) ? "" : customerImportTemplateModel.BILL_ACC);
                dbCommand.Parameters.AddWithValue("@ADDR2", string.IsNullOrEmpty(customerImportTemplateModel.ADDR2) ? "" : customerImportTemplateModel.ADDR2);
                dbCommand.Parameters.AddWithValue("@ADDR22", string.IsNullOrEmpty(customerImportTemplateModel.ADDR22) ? "" : customerImportTemplateModel.ADDR22);
                dbCommand.Parameters.AddWithValue("@ADDR23", string.IsNullOrEmpty(customerImportTemplateModel.ADDR23) ? "" : customerImportTemplateModel.ADDR23);
                dbCommand.Parameters.AddWithValue("@CITY2", string.IsNullOrEmpty(customerImportTemplateModel.CITY2) ? "" : customerImportTemplateModel.CITY2);
                dbCommand.Parameters.AddWithValue("@STATE2", string.IsNullOrEmpty(customerImportTemplateModel.STATE2) ? "" : customerImportTemplateModel.STATE2);
                dbCommand.Parameters.AddWithValue("@ZIP2", string.IsNullOrEmpty(customerImportTemplateModel.ZIP2) ? "" : customerImportTemplateModel.ZIP2);
                dbCommand.Parameters.AddWithValue("@FAX", string.IsNullOrEmpty(customerImportTemplateModel.FAX) ? "" : customerImportTemplateModel.FAX);
                dbCommand.Parameters.AddWithValue("@COUNTRY2", string.IsNullOrEmpty(customerImportTemplateModel.COUNTRY2) ? "" : customerImportTemplateModel.COUNTRY2);
                dbCommand.Parameters.AddWithValue("@BUYER", string.IsNullOrEmpty(customerImportTemplateModel.BUYER) ? "" : customerImportTemplateModel.BUYER);
                dbCommand.Parameters.AddWithValue("@SHIP_VIA", string.IsNullOrEmpty(customerImportTemplateModel.SHIP_VIA) ? "" : customerImportTemplateModel.SHIP_VIA);
                dbCommand.Parameters.AddWithValue("@IS_COD", string.IsNullOrEmpty(customerImportTemplateModel.IS_COD) ? "" : customerImportTemplateModel.IS_COD);
                dbCommand.Parameters.AddWithValue("@COD_TYPE", string.IsNullOrEmpty(customerImportTemplateModel.COD_TYPE) ? "" : customerImportTemplateModel.COD_TYPE);
                dbCommand.Parameters.AddWithValue("@ON_MAIL", string.IsNullOrEmpty(customerImportTemplateModel.ON_MAIL) ? "" : customerImportTemplateModel.ON_MAIL);
                dbCommand.Parameters.AddWithValue("@RESIDENT", string.IsNullOrEmpty(customerImportTemplateModel.RESIDENT) ? "" : customerImportTemplateModel.RESIDENT);
                dbCommand.Parameters.AddWithValue("@NOTE", string.IsNullOrEmpty(customerImportTemplateModel.NOTE) ? "" : customerImportTemplateModel.NOTE);
                dbCommand.Parameters.AddWithValue("@INTEREST", string.IsNullOrEmpty(customerImportTemplateModel.INTEREST) ? "" : customerImportTemplateModel.INTEREST);
                dbCommand.Parameters.AddWithValue("@LAST_INT", string.IsNullOrEmpty(customerImportTemplateModel.LAST_INT) ? "" : customerImportTemplateModel.LAST_INT);
                dbCommand.Parameters.AddWithValue("@GRACE", string.IsNullOrEmpty(customerImportTemplateModel.GRACE) ? "" : customerImportTemplateModel.GRACE);
                dbCommand.Parameters.AddWithValue("@SALESMAN1", string.IsNullOrEmpty(customerImportTemplateModel.SALESMAN1) ? "" : customerImportTemplateModel.SALESMAN1);
                dbCommand.Parameters.AddWithValue("@SALESMAN2", string.IsNullOrEmpty(customerImportTemplateModel.SALESMAN2) ? "" : customerImportTemplateModel.SALESMAN2);
                dbCommand.Parameters.AddWithValue("@SALESMAN3", string.IsNullOrEmpty(customerImportTemplateModel.SALESMAN3) ? "" : customerImportTemplateModel.SALESMAN3);
                dbCommand.Parameters.AddWithValue("@SALESMAN4", string.IsNullOrEmpty(customerImportTemplateModel.SALESMAN4) ? "" : customerImportTemplateModel.SALESMAN4);
                dbCommand.Parameters.AddWithValue("@PERCENT1", string.IsNullOrEmpty(customerImportTemplateModel.PERCENT1) ? "" : customerImportTemplateModel.PERCENT1);
                dbCommand.Parameters.AddWithValue("@PERCENT2", string.IsNullOrEmpty(customerImportTemplateModel.PERCENT2) ? "" : customerImportTemplateModel.PERCENT2);
                dbCommand.Parameters.AddWithValue("@PERCENT3", string.IsNullOrEmpty(customerImportTemplateModel.PERCENT3) ? "" : customerImportTemplateModel.PERCENT3);
                dbCommand.Parameters.AddWithValue("@PERCENT4", string.IsNullOrEmpty(customerImportTemplateModel.PERCENT4) ? "" : customerImportTemplateModel.PERCENT4);
                dbCommand.Parameters.AddWithValue("@TERM1", string.IsNullOrEmpty(customerImportTemplateModel.TERM1) ? "" : customerImportTemplateModel.TERM1);
                dbCommand.Parameters.AddWithValue("@TERM2", string.IsNullOrEmpty(customerImportTemplateModel.TERM2) ? "" : customerImportTemplateModel.TERM2);
                dbCommand.Parameters.AddWithValue("@TERM3", string.IsNullOrEmpty(customerImportTemplateModel.TERM3) ? "" : customerImportTemplateModel.TERM3);
                dbCommand.Parameters.AddWithValue("@TERM4", string.IsNullOrEmpty(customerImportTemplateModel.TERM4) ? "" : customerImportTemplateModel.TERM4);
                dbCommand.Parameters.AddWithValue("@TERM5", string.IsNullOrEmpty(customerImportTemplateModel.TERM5) ? "" : customerImportTemplateModel.TERM5);
                dbCommand.Parameters.AddWithValue("@TERM6", string.IsNullOrEmpty(customerImportTemplateModel.TERM6) ? "" : customerImportTemplateModel.TERM6);
                dbCommand.Parameters.AddWithValue("@TERM7", string.IsNullOrEmpty(customerImportTemplateModel.TERM7) ? "" : customerImportTemplateModel.TERM7);
                dbCommand.Parameters.AddWithValue("@TERM8", string.IsNullOrEmpty(customerImportTemplateModel.TERM8) ? "" : customerImportTemplateModel.TERM8);
                dbCommand.Parameters.AddWithValue("@TERM_PCT1", string.IsNullOrEmpty(customerImportTemplateModel.TERM_PCT1) ? "" : customerImportTemplateModel.TERM_PCT1);
                dbCommand.Parameters.AddWithValue("@TERM_PCT2", string.IsNullOrEmpty(customerImportTemplateModel.TERM_PCT2) ? "" : customerImportTemplateModel.TERM_PCT2);
                dbCommand.Parameters.AddWithValue("@TERM_PCT3", string.IsNullOrEmpty(customerImportTemplateModel.TERM_PCT3) ? "" : customerImportTemplateModel.TERM_PCT3);
                dbCommand.Parameters.AddWithValue("@TERM_PCT4", string.IsNullOrEmpty(customerImportTemplateModel.TERM_PCT4) ? "" : customerImportTemplateModel.TERM_PCT4);
                dbCommand.Parameters.AddWithValue("@TERM_PCT5", string.IsNullOrEmpty(customerImportTemplateModel.TERM_PCT5) ? "" : customerImportTemplateModel.TERM_PCT5);
                dbCommand.Parameters.AddWithValue("@TERM_PCT6", string.IsNullOrEmpty(customerImportTemplateModel.TERM_PCT6) ? "" : customerImportTemplateModel.TERM_PCT6);
                dbCommand.Parameters.AddWithValue("@TERM_PCT7", string.IsNullOrEmpty(customerImportTemplateModel.TERM_PCT7) ? "" : customerImportTemplateModel.TERM_PCT7);
                dbCommand.Parameters.AddWithValue("@TERM_PCT8", string.IsNullOrEmpty(customerImportTemplateModel.TERM_PCT8) ? "" : customerImportTemplateModel.TERM_PCT8);
                dbCommand.Parameters.AddWithValue("@CREDIT", string.IsNullOrEmpty(customerImportTemplateModel.CREDIT) ? "" : customerImportTemplateModel.CREDIT);
                dbCommand.Parameters.AddWithValue("@PERCENT", string.IsNullOrEmpty(customerImportTemplateModel.PERCENT) ? "" : customerImportTemplateModel.PERCENT);
                dbCommand.Parameters.AddWithValue("@ON_HOLD", string.IsNullOrEmpty(customerImportTemplateModel.ON_HOLD) ? "" : customerImportTemplateModel.ON_HOLD);
                dbCommand.Parameters.AddWithValue("@INACTIVE", string.IsNullOrEmpty(customerImportTemplateModel.INACTIVE) ? "" : customerImportTemplateModel.INACTIVE);
                dbCommand.Parameters.AddWithValue("@REASON", string.IsNullOrEmpty(customerImportTemplateModel.REASON) ? "" : customerImportTemplateModel.REASON);
                dbCommand.Parameters.AddWithValue("@COLCSENT", string.IsNullOrEmpty(customerImportTemplateModel.COLCSENT) ? "" : customerImportTemplateModel.COLCSENT);
                dbCommand.Parameters.AddWithValue("@COLCDATE", string.IsNullOrEmpty(customerImportTemplateModel.COLCDATE) ? "" : customerImportTemplateModel.COLCDATE);
                dbCommand.Parameters.AddWithValue("@COLCRSN", string.IsNullOrEmpty(customerImportTemplateModel.COLCRSN) ? "" : customerImportTemplateModel.COLCRSN);
                dbCommand.Parameters.AddWithValue("@ATTR1VAL", string.IsNullOrEmpty(customerImportTemplateModel.ATTR1VAL) ? "" : customerImportTemplateModel.ATTR1VAL);
                dbCommand.Parameters.AddWithValue("@ATTR2VAL", string.IsNullOrEmpty(customerImportTemplateModel.ATTR2VAL) ? "" : customerImportTemplateModel.ATTR2VAL);
                dbCommand.Parameters.AddWithValue("@ATTR3VAL", string.IsNullOrEmpty(customerImportTemplateModel.ATTR3VAL) ? "" : customerImportTemplateModel.ATTR3VAL);
                dbCommand.Parameters.AddWithValue("@ATTR4VAL", string.IsNullOrEmpty(customerImportTemplateModel.ATTR4VAL) ? "" : customerImportTemplateModel.ATTR4VAL);
                dbCommand.Parameters.AddWithValue("@ATTR5VAL", string.IsNullOrEmpty(customerImportTemplateModel.ATTR5VAL) ? "" : customerImportTemplateModel.ATTR5VAL);
                dbCommand.Parameters.AddWithValue("@ATTR6VAL", string.IsNullOrEmpty(customerImportTemplateModel.ATTR6VAL) ? "" : customerImportTemplateModel.ATTR6VAL);
                dbCommand.Parameters.AddWithValue("@ATTR7VAL", string.IsNullOrEmpty(customerImportTemplateModel.ATTR7VAL) ? "" : customerImportTemplateModel.ATTR7VAL);
                dbCommand.Parameters.AddWithValue("@ATTR8VAL", string.IsNullOrEmpty(customerImportTemplateModel.ATTR8VAL) ? "" : customerImportTemplateModel.ATTR8VAL);
                dbCommand.Parameters.AddWithValue("@MULTIATTR1VAL", string.IsNullOrEmpty(customerImportTemplateModel.MULTIATTR1VAL) ? "" : customerImportTemplateModel.MULTIATTR1VAL);
                dbCommand.Parameters.AddWithValue("@MULTIATTR2VAL", string.IsNullOrEmpty(customerImportTemplateModel.MULTIATTR2VAL) ? "" : customerImportTemplateModel.MULTIATTR2VAL);
                dbCommand.Parameters.AddWithValue("@MULTIATTR3VAL", string.IsNullOrEmpty(customerImportTemplateModel.MULTIATTR3VAL) ? "" : customerImportTemplateModel.MULTIATTR3VAL);
                dbCommand.Parameters.AddWithValue("@CUSTCHECKVAL1", string.IsNullOrEmpty(customerImportTemplateModel.CUSTCHECKVAL1) ? "" : customerImportTemplateModel.CUSTCHECKVAL1);
                dbCommand.Parameters.AddWithValue("@CUSTCHECKVAL2", string.IsNullOrEmpty(customerImportTemplateModel.CUSTCHECKVAL2) ? "" : customerImportTemplateModel.CUSTCHECKVAL2);
                dbCommand.Parameters.AddWithValue("@CUSTCHECKVAL3", string.IsNullOrEmpty(customerImportTemplateModel.CUSTCHECKVAL3) ? "" : customerImportTemplateModel.CUSTCHECKVAL3);
                dbCommand.Parameters.AddWithValue("@CUSTCHECKVAL4", string.IsNullOrEmpty(customerImportTemplateModel.CUSTCHECKVAL4) ? "" : customerImportTemplateModel.CUSTCHECKVAL4);
                dbCommand.Parameters.AddWithValue("@CUSTCHECKVAL5", string.IsNullOrEmpty(customerImportTemplateModel.CUSTCHECKVAL5) ? "" : customerImportTemplateModel.CUSTCHECKVAL5);
                dbCommand.Parameters.AddWithValue("@CUSTCHECKVAL6", string.IsNullOrEmpty(customerImportTemplateModel.CUSTCHECKVAL6) ? "" : customerImportTemplateModel.CUSTCHECKVAL6);
                dbCommand.Parameters.AddWithValue("@CUSTCHECKVAL7", string.IsNullOrEmpty(customerImportTemplateModel.CUSTCHECKVAL7) ? "" : customerImportTemplateModel.CUSTCHECKVAL7);
                dbCommand.Parameters.AddWithValue("@CUSTCHECKVAL8", string.IsNullOrEmpty(customerImportTemplateModel.CUSTCHECKVAL8) ? "" : customerImportTemplateModel.CUSTCHECKVAL8);
                dbCommand.Parameters.AddWithValue("@DIVISION", string.IsNullOrEmpty(customerImportTemplateModel.DIVISION) ? "" : customerImportTemplateModel.DIVISION);
                dbCommand.Parameters.AddWithValue("@OSTEL", string.IsNullOrEmpty(customerImportTemplateModel.OSTEL) ? "" : customerImportTemplateModel.OSTEL);
                dbCommand.Parameters.AddWithValue("@POPUPNOTE", string.IsNullOrEmpty(customerImportTemplateModel.POPUPNOTE) ? "" : customerImportTemplateModel.POPUPNOTE);
                dbCommand.Parameters.AddWithValue("@WEB_USER", string.IsNullOrEmpty(customerImportTemplateModel.WEB_USER) ? "" : customerImportTemplateModel.WEB_USER);
                dbCommand.Parameters.AddWithValue("@WEB_PASSWORD", string.IsNullOrEmpty(customerImportTemplateModel.WEB_PASSWORD) ? "" : customerImportTemplateModel.WEB_PASSWORD);
                dbCommand.Parameters.AddWithValue("@SHIP_TYPE", string.IsNullOrEmpty(customerImportTemplateModel.SHIP_TYPE) ? "" : customerImportTemplateModel.SHIP_TYPE);
                dbCommand.Parameters.AddWithValue("@ON_ACCOUNT", string.IsNullOrEmpty(customerImportTemplateModel.ON_ACCOUNT) ? "" : customerImportTemplateModel.ON_ACCOUNT);

                dbCommand.Parameters.AddWithValue("@OLD_CUSTOMER", string.IsNullOrEmpty(customerImportTemplateModel.OLD_CUSTOMER) ? "" : customerImportTemplateModel.OLD_CUSTOMER);
                dbCommand.Parameters.AddWithValue("@DIFFERENT_SHIP", string.IsNullOrEmpty(customerImportTemplateModel.DIFFERENT_SHIP) ? "" : customerImportTemplateModel.DIFFERENT_SHIP);
                dbCommand.Parameters.AddWithValue("@CONTACTMODE", string.IsNullOrEmpty(customerImportTemplateModel.CONTACTMODE) ? "" : customerImportTemplateModel.CONTACTMODE);
                dbCommand.Parameters.AddWithValue("@ISPRIVATECUSTOMER", string.IsNullOrEmpty(customerImportTemplateModel.ISPRIVATECUSTOMER) ? "" : customerImportTemplateModel.ISPRIVATECUSTOMER);
                dbCommand.Parameters.AddWithValue("@Cellno", string.IsNullOrEmpty(customerImportTemplateModel.Cellno) ? "" : customerImportTemplateModel.Cellno);
                dbCommand.Parameters.AddWithValue("@Ok_ToText", string.IsNullOrEmpty(customerImportTemplateModel.Ok_totext) ? "" : customerImportTemplateModel.Ok_totext);
                dbCommand.Parameters.AddWithValue("@Ok_ToCell", string.IsNullOrEmpty(customerImportTemplateModel.Ok_tocall) ? "" : customerImportTemplateModel.Ok_tocall);
                dbCommand.Parameters.AddWithValue("@Ok_ToMail", string.IsNullOrEmpty(customerImportTemplateModel.Ok_tomail) ? "" : customerImportTemplateModel.Ok_tomail);
                dbCommand.Parameters.AddWithValue("@Ok_ToEmail", string.IsNullOrEmpty(customerImportTemplateModel.Ok_toemail) ? "" : customerImportTemplateModel.Ok_toemail);
                dbCommand.Parameters.AddWithValue("@DriverLicenseState", string.IsNullOrEmpty(customerImportTemplateModel.DriverLicenseState) ? "" : customerImportTemplateModel.DriverLicenseState);
                dbCommand.Parameters.AddWithValue("@DriverLicenseNumber", string.IsNullOrEmpty(customerImportTemplateModel.DriverLicenseNumber) ? "" : customerImportTemplateModel.DriverLicenseNumber);
                dbCommand.Parameters.AddWithValue("@NON_TAXABLE", string.IsNullOrEmpty(customerImportTemplateModel.Non_taxbale) ? "" : customerImportTemplateModel.Non_taxbale);
                dbCommand.Parameters.AddWithValue("@notax_reasson", string.IsNullOrEmpty(customerImportTemplateModel.Non_taxbaleReason) ? "" : customerImportTemplateModel.Non_taxbaleReason);
                dbCommand.Parameters.AddWithValue("@Source", string.IsNullOrEmpty(customerImportTemplateModel.Source) ? "" : customerImportTemplateModel.Source);
                dbCommand.Parameters.AddWithValue("@Clientringsize", string.IsNullOrEmpty(customerImportTemplateModel.Clientringsize) ? "" : customerImportTemplateModel.Clientringsize);
                dbCommand.Parameters.AddWithValue("@SpouseRingSizem", string.IsNullOrEmpty(customerImportTemplateModel.SpouseRingSize) ? "" : customerImportTemplateModel.SpouseRingSize);
                dbCommand.Parameters.AddWithValue("@Idtype", string.IsNullOrEmpty(customerImportTemplateModel.Idtype) ? "" : customerImportTemplateModel.Idtype);
                dbCommand.Parameters.AddWithValue("@Idnum", string.IsNullOrEmpty(customerImportTemplateModel.IdNum) ? "" : customerImportTemplateModel.IdNum);
                dbCommand.Parameters.AddWithValue("@Nation", string.IsNullOrEmpty(customerImportTemplateModel._Nation) ? "" : customerImportTemplateModel._Nation);
                dbCommand.Parameters.AddWithValue("@Height", string.IsNullOrEmpty(customerImportTemplateModel._Height) ? "" : customerImportTemplateModel._Height);
                dbCommand.Parameters.AddWithValue("@Weight", string.IsNullOrEmpty(customerImportTemplateModel._Weight) ? "" : customerImportTemplateModel._Weight);
                dbCommand.Parameters.AddWithValue("@HairColor", string.IsNullOrEmpty(customerImportTemplateModel._HairColor) ? "" : customerImportTemplateModel._HairColor);
                dbCommand.Parameters.AddWithValue("@EyeColor", string.IsNullOrEmpty(customerImportTemplateModel._EyeColor) ? "" : customerImportTemplateModel._EyeColor);
                dbCommand.Parameters.AddWithValue("@Dob", string.IsNullOrEmpty(customerImportTemplateModel._Dob) ? "" : customerImportTemplateModel._Dob);
                dbCommand.Parameters.AddWithValue("@Referred_by", string.IsNullOrEmpty(customerImportTemplateModel._Referred_by) ? "" : customerImportTemplateModel._Referred_by);
                dbCommand.Parameters.AddWithValue("@Storecde", string.IsNullOrEmpty(customerImportTemplateModel._Store) ? "" : customerImportTemplateModel._Store);
                dbCommand.Connection.Open();
                var rowsAffected = dbCommand.ExecuteNonQuery();
                dbCommand.Connection.Close();

                return rowsAffected > 0;
            }
        }
        public class CustomerImportTemplateModel
        {
            public string AddEdit { get; set; }
            public string Template_Name { get; set; }
            public string ACC { get; set; }
            public string NAME { get; set; }
            public string ADDR1 { get; set; }
            public string ADDR12 { get; set; }
            public string ADDR13 { get; set; }
            public string STATE1 { get; set; }
            public string CITY1 { get; set; }
            public string ZIP1 { get; set; }
            public string TEL { get; set; }
            public string TEL2 { get; set; }
            public string COUNTRY { get; set; }
            public string WWW { get; set; }
            public string EMAIL { get; set; }
            public string TAX_ID { get; set; }
            public string EST_DATE { get; set; }
            public string PRICE_FILE { get; set; }
            public string JBT { get; set; }
            public string NAME2 { get; set; }
            public string BILL_ACC { get; set; }
            public string ADDR2 { get; set; }
            public string ADDR22 { get; set; }
            public string ADDR23 { get; set; }
            public string CITY2 { get; set; }
            public string STATE2 { get; set; }
            public string ZIP2 { get; set; }
            public string FAX { get; set; }
            public string COUNTRY2 { get; set; }
            public string BUYER { get; set; }
            public string SHIP_VIA { get; set; }
            public string IS_COD { get; set; }
            public string COD_TYPE { get; set; }
            public string ON_MAIL { get; set; }
            public string RESIDENT { get; set; }
            public string NOTE { get; set; }
            public string INTEREST { get; set; }
            public string LAST_INT { get; set; }
            public string GRACE { get; set; }
            public string SALESMAN1 { get; set; }
            public string SALESMAN2 { get; set; }
            public string SALESMAN3 { get; set; }
            public string SALESMAN4 { get; set; }
            public string PERCENT1 { get; set; }
            public string PERCENT2 { get; set; }
            public string PERCENT3 { get; set; }
            public string PERCENT4 { get; set; }
            public string TERM1 { get; set; }
            public string TERM2 { get; set; }
            public string TERM3 { get; set; }
            public string TERM4 { get; set; }
            public string TERM5 { get; set; }
            public string TERM6 { get; set; }
            public string TERM7 { get; set; }
            public string TERM8 { get; set; }
            public string TERM_PCT1 { get; set; }
            public string TERM_PCT2 { get; set; }
            public string TERM_PCT3 { get; set; }
            public string TERM_PCT4 { get; set; }
            public string TERM_PCT5 { get; set; }
            public string TERM_PCT6 { get; set; }
            public string TERM_PCT7 { get; set; }
            public string TERM_PCT8 { get; set; }
            public string CREDIT { get; set; }
            public string PERCENT { get; set; }
            public string ON_HOLD { get; set; }
            public string INACTIVE { get; set; }
            public string REASON { get; set; }
            public string COLCSENT { get; set; }
            public string COLCDATE { get; set; }
            public string COLCRSN { get; set; }
            public string ATTR1VAL { get; set; }
            public string ATTR2VAL { get; set; }
            public string ATTR3VAL { get; set; }
            public string ATTR4VAL { get; set; }
            public string ATTR5VAL { get; set; }
            public string ATTR6VAL { get; set; }
            public string ATTR7VAL { get; set; }
            public string ATTR8VAL { get; set; }
            public string MULTIATTR1VAL { get; set; }
            public string MULTIATTR2VAL { get; set; }
            public string MULTIATTR3VAL { get; set; }
            public string CUSTCHECKVAL1 { get; set; }
            public string CUSTCHECKVAL2 { get; set; }
            public string CUSTCHECKVAL3 { get; set; }
            public string CUSTCHECKVAL4 { get; set; }
            public string CUSTCHECKVAL5 { get; set; }
            public string CUSTCHECKVAL6 { get; set; }
            public string CUSTCHECKVAL7 { get; set; }
            public string CUSTCHECKVAL8 { get; set; }
            public string DIVISION { get; set; }
            public string OSTEL { get; set; }
            public string POPUPNOTE { get; set; }
            public string WEB_USER { get; set; }
            public string WEB_PASSWORD { get; set; }
            public string SHIP_TYPE { get; set; }
            public string ON_ACCOUNT { get; set; }
            public string OLD_CUSTOMER { get; set; }
            public string DIFFERENT_SHIP { get; set; }
            public string CONTACTMODE { get; set; }
            public string ISPRIVATECUSTOMER { get; set; }
            public string Cellno { get; set; }
            public string Ok_totext { get; set; }
            public string Ok_tocall { get; set; }
            public string Ok_tomail { get; set; }
            public string Ok_toemail { get; set; }
            public string DriverLicenseState { get; set; }
            public string DriverLicenseNumber { get; set; }
            public string Non_taxbale { get; set; }
            public string Non_taxbaleReason { get; set; }
            public string Source { get; set; }
            public string Clientringsize { get; set; }
            public string SpouseRingSize { get; set; }
            public string Idtype { get; set; }
            public string IdNum { get; set; }
            public string _Nation { get; set; }
            public string _Height { get; set; }
            public string _Weight { get; set; }
            public string _HairColor { get; set; }
            public string _EyeColor { get; set; }
            public string _Dob { get; set; }
            public string _Referred_by { get; set; }
            public string _Store { get; set; }
        }

        public DataTable CheckTemplateExistOrNot(string strTemplateName)
        {
            return _helperCommonService.GetSqlData(@"Select * from CUSTOMER_Template with(nolock) where Template_Name= @TemplateName", "@TemplateName", strTemplateName);
        }
        public bool ImportCustomerData(string dtMainDataxml, string strmainevent, string strsubevent)
        {
            using (SqlConnection dbConnection = _connectionProvider.GetConnection())
            using (SqlCommand dbCommand = new SqlCommand("CUSTOMERIMPORT", dbConnection))
            {
                dbCommand.CommandType = CommandType.StoredProcedure;
                dbCommand.CommandTimeout = 3000;
                dbCommand.Parameters.Add("@DataTable", SqlDbType.Xml).Value = dtMainDataxml;
                dbCommand.Parameters.Add("@MEVENT", SqlDbType.VarChar).Value = strmainevent;
                dbCommand.Parameters.Add("@SEVENT", SqlDbType.VarChar).Value = strsubevent;
                dbConnection.Open();
                var rowsAffected = dbCommand.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }
        public DataTable CheckAccExistsOrNot_Import(string strXML)
        {
            DataTable dataTable = new DataTable();
            using (SqlConnection dbConnection = _connectionProvider.GetConnection())
            using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter())
            {
                sqlDataAdapter.SelectCommand = new SqlCommand("CheckAccExistsOrNot_Import", dbConnection)
                {
                    CommandType = CommandType.StoredProcedure,
                    CommandTimeout = 3000
                };
                sqlDataAdapter.SelectCommand.Parameters.Add("@XML", SqlDbType.Xml).Value = strXML;
                dbConnection.Open();
                sqlDataAdapter.Fill(dataTable);
            }
            return dataTable;
        }
        public bool ImportCustomerForEvent(DataTable dtMainData, string strMainEvent, string strSubEvent)
        {
            using (SqlCommand dbCommand = new SqlCommand())
            {
                dbCommand.Connection = _connectionProvider.GetConnection();
                dbCommand.CommandType = CommandType.StoredProcedure;
                dbCommand.CommandText = "IMPORTCUSTOMER_FOREVENT";
                dbCommand.Parameters.AddWithValue("@DataTable", dtMainData);
                dbCommand.Parameters.AddWithValue("@MEVENT", strMainEvent);
                dbCommand.Parameters.AddWithValue("@SEVENT", strSubEvent);

                dbCommand.Connection.Open();
                var rowsAffected = dbCommand.ExecuteNonQuery();
                dbCommand.Connection.Close();
                return rowsAffected > 0;
            }
        }
        public bool ImportOccasions(string xmlDtOccasions)
        {
            using (var connection = _connectionProvider.GetConnection())
            using (var command = new SqlCommand("IMPORTOCCASIONS", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@DTOCCASIONS", SqlDbType.Xml) { Value = xmlDtOccasions });
                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }
        public DataTable GetCurrencyConvDetails(string unit)
        {
            return _helperCommonService.GetStoreProc("GetCurrencyDetails", "@Unit", unit);
        }
        public int UpdateCurrencyConvDetails(string unit, string currencyXml)
        {
            using (var connection = _connectionProvider.GetConnection())
            using (var command = new SqlCommand("UpdateCurrencyDetails", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@Unit", SqlDbType.NVarChar, 50) { Value = unit });
                command.Parameters.Add(new SqlParameter("@CurrencyXML", SqlDbType.Xml) { Value = currencyXml });
                connection.Open();
                return command.ExecuteNonQuery();
            }
        }

    }
}
