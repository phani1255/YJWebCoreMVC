using Microsoft.Data.SqlClient;
using System.Data;
using YJWebCoreMVC.Models;

namespace YJWebCoreMVC.Services
{
    public class CustomersPotentialsService
    {
        private readonly ConnectionProvider _connectionProvider;
        private readonly HelperCommonService _helperCommonService;

        public CustomersPotentialsService(ConnectionProvider connectionProvider, HelperCommonService helperCommonService, IWebHostEnvironment env)
        {
            _connectionProvider = connectionProvider;
            _helperCommonService = helperCommonService;
        }


        public DataRow GetPotentialCustomerById(string custACC)
        {
            DataTable dataTable = _helperCommonService.GetSqlData(@"SELECT ACC,NAME,TEL,EMAIL,ADDR12,ADDR1,STATE1,ZIP1,CITY1,COUNTRY,JBT,STORES,SOURCE,SALESMAN,BUYER,WWW,NOTE1,NOTE2,TEL,FAX,EST_DATE FROM MAILING Where ACC = @ACC",
                "@ACC", custACC);
            return _helperCommonService.GetRowOne(dataTable);
        }

        public void CheckValidCustomerCode(string acc, out string matchedAcc, string optionname = "")
        {
            if (_helperCommonService.DataTableOK(_helperCommonService.GetSqlRow($"select *  From Customer with (nolock) Where trim(acc)=trim('{acc}') or old_customer=trim('{acc}')")))
                matchedAcc = optionname == "Exists" ? "Customer Code Already Exists." : "There is a Customer With this Code : " + acc;
            else if (_helperCommonService.DataTableOK(_helperCommonService.GetSqlRow("select *  From MAILING Where trim(acc)=trim(@acc)", "@acc", acc)) && optionname != "CustCOnvert")
                matchedAcc = "There is a Potential Customer With this Code : " + acc;
            else if (_helperCommonService.DataTableOK(_helperCommonService.GetSqlRow($"select *  From Customer with (nolock) Where trim(acc)=trim('{acc}') ")) && optionname == "CustCOnvert")
                matchedAcc = optionname == "Exists" ? "Customer Code Already Exists." : "There is a Customer With this Code : " + acc;
            else
                matchedAcc = "";
        }

        public DataRow CheckValidCustomerCode(string custACC)
        {
            DataTable dataTable = _helperCommonService.GetSqlData(@"SELECT TOP 1 * FROM MAILING with (nolock) WHERE acc=TRIM(@acc)", "@ACC", custACC);
            return _helperCommonService.GetRowOne(dataTable);
        }
        public DataRow getHighestCustAccValue(int minVal, int maxVal, bool isPotentialCust = false)
        {
            using (var connection = _connectionProvider.GetConnection())
            using (var command = new SqlCommand("GetNextCustomer", connection))
            using (var adapter = new SqlDataAdapter(command))
            {
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add("@minValue", SqlDbType.Int).Value = minVal;
                command.Parameters.Add("@maxValue", SqlDbType.Int).Value = maxVal;
                command.Parameters.Add("@IsPotentialCust", SqlDbType.Bit).Value = isPotentialCust;

                var dataTable = new DataTable();
                connection.Open();
                adapter.Fill(dataTable);

                return dataTable.Rows.Count > 0 ? dataTable.Rows[0] : null;
            }
        }
        public bool AddPotentialCustomer(CustomersPotentialsModel potentialmodel)
        {
            using (SqlConnection connection = _connectionProvider.GetConnection())
            {
                connection.Open();

                using (SqlCommand dbCommand = new SqlCommand(@"
            INSERT INTO MAILING 
            (ACC, NAME, ADDR1, ADDR12, CITY1, STATE1, ZIP1, TEL, EST_DATE, FAX, DNB, CHANGED, STORES, NOTE1, SALESMAN, COUNTRY, EMAIL)
            VALUES 
            (@ACC, @NAME, @ADDR1, @ADDR12, @CITY1, @STATE1, @ZIP1, @TEL, @EST_DATE, @FAX, @DNB, @CHANGED, @STORES, @NOTE1, @SALESMAN, @COUNTRY, @EMAIL)", connection))
                {
                    dbCommand.CommandType = CommandType.Text;

                    dbCommand.Parameters.AddWithValue("@ACC", potentialmodel.ACC ?? "");
                    dbCommand.Parameters.AddWithValue("@NAME", potentialmodel.NAME ?? "");
                    dbCommand.Parameters.AddWithValue("@ADDR1", potentialmodel.ADDR1 ?? "");
                    dbCommand.Parameters.AddWithValue("@ADDR12", potentialmodel.ADDR12 ?? "");
                    dbCommand.Parameters.AddWithValue("@CITY1", potentialmodel.CITY1 ?? "");
                    dbCommand.Parameters.AddWithValue("@STATE1", potentialmodel.STATE1 ?? "");
                    dbCommand.Parameters.AddWithValue("@ZIP1", potentialmodel.ZIP1 ?? "");
                    dbCommand.Parameters.AddWithValue("@TEL", _helperCommonService.RemoveSpecialCharacters((potentialmodel.TEL)));
                    dbCommand.Parameters.AddWithValue("@EST_DATE", (object)potentialmodel.EST_DATE ?? DBNull.Value);
                    dbCommand.Parameters.AddWithValue("@FAX", _helperCommonService.RemoveSpecialCharacters((potentialmodel.FAX)));
                    dbCommand.Parameters.AddWithValue("@DNB", "");
                    dbCommand.Parameters.AddWithValue("@CHANGED", 0);
                    dbCommand.Parameters.AddWithValue("@STORES", 1);
                    dbCommand.Parameters.AddWithValue("@NOTE1", potentialmodel.NOTE1 ?? "");
                    dbCommand.Parameters.AddWithValue("@SALESMAN", potentialmodel.SALESMAN ?? "");
                    dbCommand.Parameters.AddWithValue("@COUNTRY", potentialmodel.COUNTRY ?? "");
                    dbCommand.Parameters.AddWithValue("@EMAIL", potentialmodel.EMAIL ?? "");

                    int rowsAffected = dbCommand.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }

        public DataTable GetCustomerEvents(string acc)
        {
            return _helperCommonService.GetSqlData(@"SELECT MAINEVENT,SUBEVENT FROM CUSTOMEREVENTS with (nolock) WHERE TRIM(acc)= TRIM(@acc)", "@acc", acc);
        }

        public DataTable getsubevents(string mainevent)
        {
            return _helperCommonService.GetSqlData("SELECT '' AS SUBEVENT UNION SELECT DISTINCT TRIM(SUBEVENT) FROM EVENTS  with (nolock) where MAINEVENT = '" + mainevent + "' order by SUBEVENT");
        }

        public DataTable AddnewCustEvent(string acc, string leveldet)
        {
            return _helperCommonService.GetStoreProc("CUSTOMEREVENTSINFO", "@ACC", acc, "@TBLCUSTOMEREVENTS", leveldet);
        }
        public DataTable DelCustomerEvent(string accname, string mainevent, string subevent)
        {
            return _helperCommonService.GetSqlData($"delete from [dbo].[CUSTOMEREVENTS] where acc= @accname and MAINEVENT= @mainevent and SUBEVENT = @subevent ", "@accname", accname, "@mainevent", mainevent, "@subevent", subevent);
        }
        public DataTable AddNewPotentialCustNote(string acc, string user, string leveldet)
        {
            return _helperCommonService.GetStoreProc("ADDPOTENTIALCUSTNOTES", "@ACC", acc, "@LOGGEDUSER", user,
                "@TBLPOTENTIALCUSTNOTES", leveldet);
        }
        public DataTable AddNewCustNote(string acc, string user, string leveldet)
        {
            return _helperCommonService.GetStoreProc("CUSTNOTES", "@ACC", acc, "@LOGGEDUSER", user, "@TBLCUSTNOTES", leveldet);
        }
        public DataTable ShowPotentialCustomerNotes(string acc)
        {
            return _helperCommonService.GetSqlData(@"select WHO as [User] ,DTIME as Date,[TYPE] as Type ,NOTE as Note ,try_cast(followup as date) as FollowUp,completed as Completed from POTENTIALCUSTNOTE where acc= trim(@acc) order by DTIME", "@acc", acc);
        }
        public DataTable ShowCustomerNotes(string acc)
        {
            return _helperCommonService.GetSqlData(@"select WHO as [User] ,DTIME as Date,[TYPE] as Type ,NOTE as Note ,followup as FollowUp, completed as Completed, time as FollowUp_Time,reminder as Reminder from CUSTNOTE where acc= trim(@acc) order by DTIME", "@acc", acc);
        }
        public List<string> GetNoteTypes()
        {
            var dt = _helperCommonService.GetSqlData("SELECT UPPER(ISNULL(fType,'')) [Type] FROM followup_type");

            return dt.AsEnumerable()
                     .Select(r => r.Field<string>("Type"))
                     .ToList();
        }

        public DataTable GetAllCustomers()
        {
            return _helperCommonService.GetSqlData(@"SELECT ACC,NAME,TEL,EMAIL,ADDR1,STATE1,ZIP1,CITY1,COUNTRY,JBT,STORES,SOURCE,SALESMAN,BUYER,WWW,NOTE1,NOTE2,TEL,FAX,EST_DATE FROM MAILING");
        }

        public DataTable DelPotentialCustNote(string acc)
        {
            return _helperCommonService.GetSqlData("delete from [dbo].[POTENTIALCUSTNOTE] where acc = @acc", "@acc", acc);
        }
        public bool AddCustomer(CustomerModel customer)
        {
            using (SqlCommand dbCommand = new SqlCommand())
            {
                // Set the command object properties
                dbCommand.Connection = _connectionProvider.GetConnection();
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
                dbCommand.Parameters.AddWithValue("@INTEREST", customer.INTEREST ?? 0);
                dbCommand.Parameters.AddWithValue("@LAST_INT", lastint);
                dbCommand.Parameters.AddWithValue("@GRACE", customer.GRACE ?? 0);
                dbCommand.Parameters.AddWithValue("@SALESMAN1", customer.SALESMAN1);
                dbCommand.Parameters.AddWithValue("@SALESMAN2", customer.SALESMAN2);
                dbCommand.Parameters.AddWithValue("@SALESMAN3", customer.SALESMAN3);
                dbCommand.Parameters.AddWithValue("@SALESMAN4", customer.SALESMAN4);
                dbCommand.Parameters.AddWithValue("@PERCENT1", customer.PERCENT1 ?? 0);
                dbCommand.Parameters.AddWithValue("@PERCENT2", customer.PERCENT2 ?? 0);
                dbCommand.Parameters.AddWithValue("@PERCENT3", customer.PERCENT3 ?? 0);
                dbCommand.Parameters.AddWithValue("@PERCENT4", customer.PERCENT4 ?? 0);
                dbCommand.Parameters.AddWithValue("@TERM1", customer.TERM1 ?? 0);
                dbCommand.Parameters.AddWithValue("@TERM2", customer.TERM2 ?? 0);
                dbCommand.Parameters.AddWithValue("@TERM3", customer.TERM3 ?? 0);
                dbCommand.Parameters.AddWithValue("@TERM4", customer.TERM4 ?? 0);
                dbCommand.Parameters.AddWithValue("@TERM5", customer.TERM5 ?? 0);
                dbCommand.Parameters.AddWithValue("@TERM6", customer.TERM6 ?? 0);
                dbCommand.Parameters.AddWithValue("@TERM7", customer.TERM7 ?? 0);
                dbCommand.Parameters.AddWithValue("@TERM8", customer.TERM8 ?? 0);
                dbCommand.Parameters.AddWithValue("@TERM_PCT1", customer.TERM_PCT1 ?? 0);
                dbCommand.Parameters.AddWithValue("@TERM_PCT2", customer.TERM_PCT2 ?? 0);
                dbCommand.Parameters.AddWithValue("@TERM_PCT3", customer.TERM_PCT3 ?? 0);
                dbCommand.Parameters.AddWithValue("@TERM_PCT4", customer.TERM_PCT4 ?? 0);
                dbCommand.Parameters.AddWithValue("@TERM_PCT5", customer.TERM_PCT5 ?? 0);
                dbCommand.Parameters.AddWithValue("@TERM_PCT6", customer.TERM_PCT6 ?? 0);
                dbCommand.Parameters.AddWithValue("@TERM_PCT7", customer.TERM_PCT7 ?? 0);
                dbCommand.Parameters.AddWithValue("@TERM_PCT8", customer.TERM_PCT8 ?? 0);
                dbCommand.Parameters.AddWithValue("@CREDIT", customer.CREDIT ?? 0);
                dbCommand.Parameters.AddWithValue("@PERCENT", customer.PERCENT ?? 0);
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
                dbCommand.Parameters.AddWithValue("@TEL2", customer.TEL2 ?? 0);
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

        public bool GetPotentialCustomerByAcc(string acc, string loggedUser = "")
        {
            using (var connection = _connectionProvider.GetConnection())
            {
                const string query = @"
            DELETE FROM MAILING WHERE ACC = @ACC;
            UPDATE QUOTES 
            SET ISPOTENTIALCUSTOMER = 0 
            WHERE ACC = @ACC;
            INSERT INTO KEEP_REC ([DATE], [TIME], WHO, WHAT)  
            VALUES (SYSDATETIME(), CONVERT(time, SYSDATETIME()), @LoggedUser, @LogMessage);
        ";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add("@ACC", SqlDbType.VarChar).Value = acc;
                    command.Parameters.Add("@LoggedUser", SqlDbType.VarChar).Value = loggedUser;
                    command.Parameters.Add("@LogMessage", SqlDbType.VarChar).Value = $"Customer {acc} was deleted";

                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }

        public bool UpdatePotentialCustomer(PotentialcustomerModel potentialmodel)
        {
            using (SqlCommand dbCommand = new SqlCommand())
            {
                dbCommand.Connection = _connectionProvider.GetConnection();
                dbCommand.CommandType = CommandType.Text;
                Object estdate;
                if (potentialmodel.EST_DATE == null)
                    estdate = DBNull.Value;
                else
                    estdate = potentialmodel.EST_DATE;

                dbCommand.CommandText = @"Update MAILING set NAME=@NAME,TEL=@TEL,EMAIL=@EMAIL,ADDR1=@ADDR1,ADDR12=@ADDR12,STATE1=@STATE1,ZIP1=@ZIP1,CITY1=@CITY1,COUNTRY=@COUNTRY,STORES=@STORES,SALESMAN=@SALESMAN,NOTE1=@NOTE1,FAX=@FAX,CHANGED=@CHANGED,EST_DATE=@EST_DATE Where([ACC] = @ACC)";

                dbCommand.Parameters.AddWithValue("@ACC", potentialmodel.ACC);
                dbCommand.Parameters.AddWithValue("@NAME", potentialmodel.NAME ?? "");
                dbCommand.Parameters.AddWithValue("@ADDR1", potentialmodel.ADDR1 ?? "");
                dbCommand.Parameters.AddWithValue("@ADDR12", potentialmodel.ADDR12 ?? "");
                dbCommand.Parameters.AddWithValue("@CITY1", potentialmodel.CITY1 ?? "");
                dbCommand.Parameters.AddWithValue("@STATE1", potentialmodel.STATE1 ?? "");
                dbCommand.Parameters.AddWithValue("@ZIP1", potentialmodel.ZIP1 ?? "");
                //dbCommand.Parameters.AddWithValue("@BUYER", potentialmodel.BUYER);
                dbCommand.Parameters.AddWithValue("@TEL", potentialmodel.TEL);
                dbCommand.Parameters.AddWithValue("@EST_DATE", estdate);
                dbCommand.Parameters.AddWithValue("@FAX", potentialmodel.FAX);
                dbCommand.Parameters.AddWithValue("@DNB", "");
                //dbCommand.Parameters.AddWithValue("@JBT", potentialmodel.JBT);
                dbCommand.Parameters.AddWithValue("@CHANGED", 1);
                dbCommand.Parameters.AddWithValue("@STORES", 1);
                //dbCommand.Parameters.AddWithValue("@SOURCE", potentialmodel.SOURCE);
                dbCommand.Parameters.AddWithValue("@NOTE1", potentialmodel.NOTE1 ?? "");
                //dbCommand.Parameters.AddWithValue("@NOTE2", potentialmodel.NOTE2);
                dbCommand.Parameters.AddWithValue("@SALESMAN", potentialmodel.SALESMAN ?? "");
                dbCommand.Parameters.AddWithValue("@COUNTRY", potentialmodel.COUNTRY ?? "");
                dbCommand.Parameters.AddWithValue("@EMAIL", potentialmodel.EMAIL ?? "");
                //dbCommand.Parameters.AddWithValue("@WWW", potentialmodel.WWW);
                dbCommand.Connection.Open();
                var rowsAffected = dbCommand.ExecuteNonQuery();
                dbCommand.Connection.Close();
                return rowsAffected > 0;
            }
        }

        public DataTable ShowPotentialCustomerFollowup(string acc, DateTime fromdate, DateTime todate, int complete)
        {
            DataTable dataTable = new DataTable();
            using (SqlDataAdapter SqlDataAdapter = new SqlDataAdapter())
            {
                // Create the command and set its properties
                SqlDataAdapter.SelectCommand = new SqlCommand();
                SqlDataAdapter.SelectCommand.Connection = _connectionProvider.GetConnection();
                SqlDataAdapter.SelectCommand.CommandType = CommandType.Text;

                if (acc == "ALL")
                {
                    // Assign the SQL to the command object
                    if (complete == 1)
                        SqlDataAdapter.SelectCommand.CommandText = "select ACC as Customercode ,WHO as [User] ,DTIME as Date,[TYPE] as Type ,NOTE as Note ,followup as FollowUp,completed as Completed from POTENTIALCUSTNOTE where  followup <> '' and followup is not null and convert(varchar(8),followup,112) >= @fromdate  and convert(varchar(8),followup,112) <= @todate  order by DTIME";
                    else
                        SqlDataAdapter.SelectCommand.CommandText = "select ACC as Customercode ,WHO as [User] ,DTIME as Date,[TYPE] as Type ,NOTE as Note ,followup as FollowUp,completed as Completed from POTENTIALCUSTNOTE where completed = @complete and followup <> '' and followup is not null and convert(varchar(8),followup,112) >= @fromdate and convert(varchar(8),followup,112) <= @todate  order by DTIME";
                }
                else if (complete == 1)
                    SqlDataAdapter.SelectCommand.CommandText = "select ACC as Customercode ,WHO as [User] ,DTIME as Date,[TYPE] as Type ,NOTE as Note ,followup as FollowUp,completed as Completed from POTENTIALCUSTNOTE where who= @acc and followup <> '' and followup is not null and convert(varchar(8),followup,112) >= @fromdate and convert(varchar(8),followup,112) <= @todate  order by DTIME";
                else
                    SqlDataAdapter.SelectCommand.CommandText = "select ACC as Customercode ,WHO as [User] ,DTIME as Date,[TYPE] as Type ,NOTE as Note ,followup as FollowUp,completed as Completed from POTENTIALCUSTNOTE where completed = @complete  and who= @acc and followup <> '' and followup is not null and convert(varchar(8),followup,112) >= @fromdate and convert(varchar(8),followup,112) <= @todate  order by DTIME";
                // Fill the table from adapter

                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@acc", acc.Trim());
                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@fromdate", fromdate.ToString("yyyyMMdd"));
                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@todate", todate.ToString("yyyyMMdd"));
                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@complete", complete);
                SqlDataAdapter.Fill(dataTable);
            }
            return dataTable;
        }

        public DataTable GetPotentialCustomerImportTemplate(string strTemplateName = "")
        {
            if (String.IsNullOrWhiteSpace(strTemplateName.Trim()))
                return _helperCommonService.GetSqlData("Select * from POTENTIAL_CUSTOMER_Template with(nolock) order by Template_Name", "@TemplateName", strTemplateName);
            return _helperCommonService.GetSqlData("Select * from POTENTIAL_CUSTOMER_Template with(nolock) where Template_Name like '%'+@TemplateName+'%' order by Template_Name", "@TemplateName", strTemplateName);
        }

        public DataTable CheckTemplateExistOrNot(string strTemplateName)
        {
            return _helperCommonService.GetSqlData(@"Select * from POTENTIAL_CUSTOMER_Template with(nolock) where Template_Name= @TemplateName", "@TemplateName", strTemplateName);
        }
        public DataTable CheckPotentialCustomerImportTemplate(string strTemplateName)
        {
            return _helperCommonService.GetSqlData(@"Select * from POTENTIAL_CUSTOMER_Template with(nolock) where Template_Name = @TemplateName", "@TemplateName", strTemplateName);
        }

        public bool AddPotentialCustomerImportTemplate(PotentialcustomerImportModel potentialcustomerImportModel)
        {
            using (SqlCommand dbCommand = new SqlCommand())
            {
                // Set the command object properties
                dbCommand.Connection = _connectionProvider.GetConnection();
                dbCommand.CommandType = CommandType.StoredProcedure;
                dbCommand.CommandText = "AddEditPotentialCustomerImportTemplateData";

                dbCommand.Parameters.AddWithValue("@ADDEDIT", potentialcustomerImportModel.AddEdit);
                dbCommand.Parameters.AddWithValue("@TEMPLATE_NAME", potentialcustomerImportModel.Template_Name);
                dbCommand.Parameters.AddWithValue("@ACC", potentialcustomerImportModel.ACC);
                dbCommand.Parameters.AddWithValue("@NAME", potentialcustomerImportModel.NAME ?? "");
                dbCommand.Parameters.AddWithValue("@ADDR1", potentialcustomerImportModel.ADDR1 ?? "");
                dbCommand.Parameters.AddWithValue("@ADDR12", potentialcustomerImportModel.ADDR12 ?? "");
                dbCommand.Parameters.AddWithValue("@CITY1", potentialcustomerImportModel.CITY1 ?? "");
                dbCommand.Parameters.AddWithValue("@STATE1", potentialcustomerImportModel.STATE1 ?? "");
                dbCommand.Parameters.AddWithValue("@ZIP1", potentialcustomerImportModel.ZIP1 ?? "");
                dbCommand.Parameters.AddWithValue("@BUYER", potentialcustomerImportModel.BUYER ?? "");
                dbCommand.Parameters.AddWithValue("@TEL", potentialcustomerImportModel.TEL ?? "");
                dbCommand.Parameters.AddWithValue("@EST_DATE", potentialcustomerImportModel.EST_DATE ?? "");
                dbCommand.Parameters.AddWithValue("@FAX", potentialcustomerImportModel.FAX ?? "");
                dbCommand.Parameters.AddWithValue("@DNB", potentialcustomerImportModel.DNB ?? "");
                dbCommand.Parameters.AddWithValue("@JBT", potentialcustomerImportModel.JBT ?? "");
                dbCommand.Parameters.AddWithValue("@CHANGED", potentialcustomerImportModel.CHANGED ?? "");
                dbCommand.Parameters.AddWithValue("@STORES", potentialcustomerImportModel.STORES ?? "");
                dbCommand.Parameters.AddWithValue("@SOURCE", potentialcustomerImportModel.SOURCE ?? "");
                dbCommand.Parameters.AddWithValue("@NOTE1", potentialcustomerImportModel.NOTE1 ?? "");
                dbCommand.Parameters.AddWithValue("@NOTE2", potentialcustomerImportModel.NOTE2 ?? "");
                dbCommand.Parameters.AddWithValue("@SALESMAN", potentialcustomerImportModel.SALESMAN ?? "");
                dbCommand.Parameters.AddWithValue("@COUNTRY", potentialcustomerImportModel.COUNTRY ?? "");
                dbCommand.Parameters.AddWithValue("@EMAIL", potentialcustomerImportModel.EMAIL ?? "");
                dbCommand.Parameters.AddWithValue("@WWW", potentialcustomerImportModel.WWW ?? "");

                // Open the connection, execute the query and close the connection
                dbCommand.Connection.Open();
                var rowsAffected = dbCommand.ExecuteNonQuery();
                dbCommand.Connection.Close();

                return rowsAffected > 0;
            }
        }
        public bool AddNewPotentialCustNote(string strXML)
        {
            using (SqlCommand dbCommand = new SqlCommand())
            {
                // Set the command object properties

                dbCommand.Connection = _connectionProvider.GetConnection();
                dbCommand.CommandType = CommandType.StoredProcedure;
                dbCommand.CommandText = "ADDPOTENTIALCUSTNOTE";
                dbCommand.Parameters.AddWithValue("@XML", strXML);
                // Open the connection, execute the query and close the connection
                dbCommand.Connection.Open();
                var rowsAffected = dbCommand.ExecuteNonQuery();
                dbCommand.Connection.Close();
                return rowsAffected > 0;
            }
        }
    }
}
