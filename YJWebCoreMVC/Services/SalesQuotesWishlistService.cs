/*
 *  Created By Phanindra
 *  Phanindra 10/23/2024 Added CheckValidQuote, DeleteQuote methods
 *  Phanindra 11/12/2024 Fixed issues in Add Quote and Edit Quote pages
 *  Phanindra 10/03/2025 added bankcode parameters
 *  Chakri    12/01/2025 added ListOfScrapGold mehtod .
 *  Chakri    12/09/2025 Added ListOfScrapGold method.
 *  Chakri    12/12/2025 added UpdateProcesstradeins and GetProcessTradein methods.
 */

using Microsoft.Data.SqlClient;
using System.Data;
using YJWebCoreMVC.Models;

namespace YJWebCoreMVC.Services
{
    public class SalesQuotesWishlistService
    {
        private readonly ConnectionProvider _connectionProvider;
        private readonly HelperCommonService _helperCommonService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SalesQuotesWishlistService(ConnectionProvider connectionProvider, IHttpContextAccessor httpContextAccessor, HelperCommonService helperCommonService)
        {
            _connectionProvider = connectionProvider;
            _helperCommonService = helperCommonService;
            _httpContextAccessor = httpContextAccessor;
        }

        public string AddUpdateQuoteDetails(QuoteModel objQuotes, string XML, string STYLEXML, string storeno, bool lostopprt = false, string invno = "", bool ispotentialcust = false, string qtStatus = "", DateTime? callDate = null, DateTime? callTime = null)
        {
            using (SqlDataAdapter dataAdapter = new SqlDataAdapter())
            {
                var rowsAffected = 0;
                try
                {
                    // Create the command and set its properties
                    dataAdapter.SelectCommand = new SqlCommand();
                    dataAdapter.SelectCommand.Connection = _connectionProvider.GetConnection();

                    dataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                    dataAdapter.SelectCommand.CommandText = "AddUpdateQuote";
                    //dbCommand.CommandText = "[AddUpdateQuoteTEST]";
                    // Add the input parameter to the parameter collection
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@QN", objQuotes.QN);
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@ACC", objQuotes.ACC);
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@ENTER_DATE", objQuotes.ENTER_DATE);
                    //dataAdapter.SelectCommand.Parameters.AddWithValue("@STYLE", objQuotes.STYLE);
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@STYLE", "");
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@DESC", objQuotes.DESC);
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@PRICE", objQuotes.PRICE);
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@OPERATOR", objQuotes.OPERATOR);
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@invno", invno);
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@lostopprt", lostopprt);
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@storeno", storeno);
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@ispotentialcust", ispotentialcust);
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@QuoteStatus", qtStatus);
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@CALLDATE", callDate);
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@CALLTIME", callTime);
                    SqlParameter parameter = new SqlParameter();
                    parameter.ParameterName = "@QNDATA";
                    parameter.SqlDbType = System.Data.SqlDbType.Xml;
                    parameter.Value = XML;
                    dataAdapter.SelectCommand.Parameters.Add(parameter);

                    SqlParameter parameter1 = new SqlParameter();
                    parameter1.ParameterName = "@QNSTYLEDATA";
                    parameter1.SqlDbType = System.Data.SqlDbType.Xml;
                    parameter1.Value = STYLEXML;
                    dataAdapter.SelectCommand.Parameters.Add(parameter1);

                    // Open the connection, execute the query and close the connection
                    dataAdapter.SelectCommand.Connection.Open();
                    rowsAffected = dataAdapter.SelectCommand.ExecuteNonQuery();
                    dataAdapter.SelectCommand.Connection.Close();

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                if (rowsAffected > 0)
                {
                    _httpContextAccessor.HttpContext?.Session.SetString("QTNO", "");
                    return "Success";
                }
                else
                {
                    return "Fail";
                }

            }
        }

        public DataTable GetQuotesByCustomerCode(string acc, bool openonly = false)
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
                    dataAdapter.SelectCommand.CommandText =
                        @"SELECT qt.qn, qt.acc, C.NAME, Qt.date, Qi.STYLE,Qi.NOTE AS description, Qi.PRICE, Qt.operator, Qt.INV_NO, Qt.LOSTOPPORTUNITY, 
                        ISNULL(Qt.QStatus,'') AS STATUS, IIF(ISNULL(QStatus,'') LIKE '%WILL CALL%', CallToCust_Date,null) AS CALLTOCUSTDATE 
                        FROM QUOTES Qt JOIN QT_ITEMS Qi ON RIGHT('000000' + CONVERT(VARCHAR, LTRIM(RTRIM(QT.QN))), 6) = RIGHT(QI.QN, 6) JOIN CUSTOMER C ON Qt.acc = C.ACC
                        WHERE " + (openonly ? " Qt.lostopportunity=0 AND ISNULL(Qt.inv_no,'')='' AND " : "") + "  (@acc='' or ltrim(rtrim(Qt.[acc]))=@acc) ORDER BY date DESC";
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@acc", acc.Trim());
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@openonly", openonly);
                    // Open the connection, execute the query and close the connection
                    dataAdapter.Fill(dataTable);

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                return dataTable;

            }
        }
        public DataTable GetJobagdataforquote(string quote)
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
                    dataAdapter.SelectCommand.CommandText = "SELECT TOP 1 * FROM OR_ITEMS WHERE PON=@PON";
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@PON", "QT" + quote.Trim().PadLeft(6, '0'));

                    dataAdapter.Fill(dataTable);

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                return dataTable;

            }

        }
        public string AddPotentialCustomerDetails(PotentialCustomerModel potentialmodel)
        {
            using (SqlDataAdapter dataAdapter = new SqlDataAdapter())
            {
                var rowsAffected = 0;
                try
                {
                    // Create the command and set its properties
                    dataAdapter.SelectCommand = new SqlCommand();
                    dataAdapter.SelectCommand.Connection = _connectionProvider.GetConnection();
                    Object estdate;
                    if (potentialmodel.EST_DATE == null)
                        estdate = DBNull.Value;
                    else
                        estdate = potentialmodel.EST_DATE;

                    dataAdapter.SelectCommand.CommandText = @"Insert Into MAILING(ACC,NAME,ADDR1,ADDR12,CITY1,STATE1,ZIP1,
                    TEL,EST_DATE,FAX,DNB,CHANGED,STORES,NOTE1,SALESMAN,COUNTRY,EMAIL)
                    Values(@ACC,@NAME,@ADDR1,@ADDR12,@CITY1,@STATE1,@ZIP1,@TEL,@EST_DATE,@FAX,@DNB,@CHANGED,@STORE,@NOTE1,@SALESMAN,@COUNTRY,@EMAIL)"
                    ;

                    dataAdapter.SelectCommand.Parameters.AddWithValue("@ACC", string.IsNullOrEmpty(potentialmodel.ACC) ? "" : potentialmodel.ACC);
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@NAME", string.IsNullOrEmpty(potentialmodel.NAME) ? "" : potentialmodel.NAME);
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@ADDR1", string.IsNullOrEmpty(potentialmodel.ADDR1) ? "" : potentialmodel.ADDR1);
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@ADDR12", string.IsNullOrEmpty(potentialmodel.ADDR12) ? "" : potentialmodel.ADDR12);
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@CITY1", string.IsNullOrEmpty(potentialmodel.CITY1) ? "" : potentialmodel.CITY1);
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@STATE1", string.IsNullOrEmpty(potentialmodel.STATE1) ? "" : potentialmodel.STATE1);
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@ZIP1", string.IsNullOrEmpty(potentialmodel.ZIP1) ? "" : potentialmodel.ZIP1);
                    //dataAdapter.SelectCommand.Parameters.AddWithValue("@BUYER", string.IsNullOrEmpty(potentialmodel.BUYER) ? "" : potentialmodel.BUYER);
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@TEL", potentialmodel.TEL);
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@EST_DATE", estdate);
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@FAX", potentialmodel.FAX);
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@DNB", "");
                    //dataAdapter.SelectCommand.Parameters.AddWithValue("@JBT", string.IsNullOrEmpty(potentialmodel.JBT) ? "" : potentialmodel.JBT);
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@CHANGED", 0);
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@STORE", 1);
                    //dataAdapter.SelectCommand.Parameters.AddWithValue("@SOURCE", string.IsNullOrEmpty(potentialmodel.SOURCE) ? "" : potentialmodel.SOURCE);
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@NOTE1", string.IsNullOrEmpty(potentialmodel.NOTE1) ? "" : potentialmodel.NOTE1);
                    //dataAdapter.SelectCommand.Parameters.AddWithValue("@NOTE2", string.IsNullOrEmpty(potentialmodel.NOTE2) ? "" : potentialmodel.NOTE2);
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@SALESMAN", string.IsNullOrEmpty(potentialmodel.SALESMAN) ? "" : potentialmodel.SALESMAN);
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@COUNTRY", string.IsNullOrEmpty(potentialmodel.COUNTRY) ? "" : potentialmodel.COUNTRY);
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@EMAIL", string.IsNullOrEmpty(potentialmodel.EMAIL) ? "" : potentialmodel.EMAIL);
                    //dataAdapter.SelectCommand.Parameters.AddWithValue("@WWW", string.IsNullOrEmpty(potentialmodel.WWW) ? "" : potentialmodel.WWW);

                    // Open the connection, execute the query and close the connection
                    dataAdapter.SelectCommand.Connection.Open();
                    rowsAffected = dataAdapter.SelectCommand.ExecuteNonQuery();
                    dataAdapter.SelectCommand.Connection.Close();

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return ex.Message;
                }
                if (rowsAffected > 0)
                {
                    return "Potential Customer added Successfully";
                }
                else
                {
                    return "Error while saving Potential Customer";
                }

            }
        }

        public DataTable CheckValidQuote(string qn)
        {
            DataTable dataTable = new DataTable();
            dataTable = _helperCommonService.GetSqlData("SELECT * FROM QUOTES WHERE LTRIM(RTRIM(qn)) = LTRIM(RTRIM(@qn))", "@qn", qn);
            return dataTable.Rows.Count > 0 ? dataTable : null;
        }

        public bool DeleteQuote(string quote)
        {
            DataTable dataTable = new DataTable();
            var rowsAffected = 0;
            using (SqlDataAdapter dataAdapter = new SqlDataAdapter())
            {
                try
                {
                    // Create the command and set its properties
                    dataAdapter.SelectCommand = new SqlCommand();
                    dataAdapter.SelectCommand.Connection = _connectionProvider.GetConnection();

                    dataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                    dataAdapter.SelectCommand.CommandText = "DeleteQuote";

                    dataAdapter.SelectCommand.Parameters.AddWithValue("@QN", quote);
                    dataAdapter.SelectCommand.Connection.Open();
                    rowsAffected = dataAdapter.SelectCommand.ExecuteNonQuery();
                    dataAdapter.SelectCommand.Connection.Close();


                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                return rowsAffected > 0;

            }

        }
        public DataTable GetQuoteListData(string ACC, DateTime? FromDt, DateTime? ToDt, bool isOpened, string scode, string qStatus = "ALL", string source = "", string summaryBy = "")
        {
            DataTable dataTable = new DataTable();
            using (SqlDataAdapter dataAdapter = new SqlDataAdapter())
            {
                try
                {
                    // Create the command and set its properties
                    dataAdapter.SelectCommand = new SqlCommand();
                    dataAdapter.SelectCommand.Connection = _connectionProvider.GetConnection();

                    dataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                    dataAdapter.SelectCommand.CommandText = @"GetQuoteList";

                    // Add the parameter to the parameter collection
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@ACC", string.IsNullOrWhiteSpace(ACC.Trim()) ? null : ACC.Trim());
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@FromDt", FromDt);
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@ToDt", ToDt);
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@isOpened", isOpened == true ? 0 : 1);
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@scode", scode);
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@Source", source);
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@qtStatus", qStatus);
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@SummaryBy", summaryBy);
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
        public DataTable ListOfScrapGold(string fromAccCode, string toAccCode, string fromDate, string toDate, string storeNoCondition,
           bool isCashOut = false)
        {
            using (var connection = _connectionProvider.GetConnection())
            using (var command = new SqlCommand("LISSCRAPGOLD", connection))
            using (var adapter = new SqlDataAdapter(command))
            {
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@FROMACCCODE", fromAccCode.Trim());
                command.Parameters.AddWithValue("@TOACCCODE", toAccCode.Trim());
                command.Parameters.AddWithValue("@FROMDATE", fromDate.Trim());
                command.Parameters.AddWithValue("@TODATE", toDate.Trim());
                command.Parameters.AddWithValue("@STORE_NO_CONDITION", storeNoCondition.Trim());
                command.Parameters.AddWithValue("@iscashout", isCashOut);

                var dataTable = new DataTable();
                adapter.Fill(dataTable);
                return dataTable;
            }
        }

        public bool UpdateProcesstradeins(string XML, int IsStyleorScrap = 0, string Style = "", bool IsnewStyle = false)
        {
            using (var connection = _connectionProvider.GetConnection())
            using (var dbCommand = new SqlCommand("UpdateProcesstradeinitems", connection) { CommandType = CommandType.StoredProcedure })
            {
                dbCommand.Parameters.AddRange(new[]
                {
                new SqlParameter("@TRADEINITEMS", SqlDbType.Xml) { Value = XML },
                new SqlParameter("@IsStyleorScrap", SqlDbType.Int) { Value = IsStyleorScrap },
                new SqlParameter("@Style", SqlDbType.Text) { Value = Style },
                new SqlParameter("@Storecode", SqlDbType.Text) { Value = _helperCommonService.StoreCode },
                new SqlParameter("@LOGGEDUSER", SqlDbType.Text) { Value = _helperCommonService.LoggedUser },
                new SqlParameter("@IsnewStyle", SqlDbType.Bit) { Value = IsnewStyle }
                });

                connection.Open();
                return dbCommand.ExecuteNonQuery() > 0;
            }
        }

        public DataTable GetProcessTradein(string fromdate, string todate, bool isprcall, string Stores, int received, bool isNotprocessedStockorStock = false)
        {
            return _helperCommonService.GetStoreProc("GetProcessTradeinitems", "@fromdate", fromdate.ToString(), "@todate", todate.ToString(), "@isprcall", isprcall.ToString(), "@Stores", Stores.Trim(), "@received", received.ToString(), "@isNotprocessedStockorStock", isNotprocessedStockorStock.ToString());
        }
    }
}
