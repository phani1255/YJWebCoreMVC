using Microsoft.Data.SqlClient;
using System.Data;

namespace YJWebCoreMVC.Services
{
    public class CashRegisterService
    {
        private readonly ConnectionProvider _connectionProvider;
        private readonly HelperCommonService _helperCommonService;

        public CashRegisterService(ConnectionProvider connectionProvider, HelperCommonService helperCommonService, IWebHostEnvironment env)
        {
            _connectionProvider = connectionProvider;
            _helperCommonService = helperCommonService;
        }


        public bool ManagerToDepositToABank(string InvNo, DateTime? dDate, string Bank, decimal Amt, string StoreCode, bool IsManagerGetCashFromABank = false, decimal depno = 0, string currency = "")
        {
            using (SqlCommand dbcommand = new SqlCommand())
            {
                dbcommand.Connection = _connectionProvider.GetConnection();
                dbcommand.CommandType = CommandType.StoredProcedure;

                // Assign the SQL to the command object
                dbcommand.CommandText = "ManagerToDepositToABank";
                dbcommand.Parameters.AddWithValue("@InvNo", string.IsNullOrEmpty(InvNo) ? "" : InvNo);
                dbcommand.Parameters.AddWithValue("@Bank", string.IsNullOrEmpty(Bank.Trim()) ? "" : Bank.Trim());
                dbcommand.Parameters.AddWithValue("@Amt", Amt != 0 ? Amt : (object)0);
                dbcommand.Parameters.AddWithValue("@DATE", dDate != DateTime.MinValue ? dDate : (object)DBNull.Value);
                dbcommand.Parameters.AddWithValue("@StoreCode", StoreCode.Trim());
                dbcommand.Parameters.AddWithValue("@IsManagerGetCashFromABank", IsManagerGetCashFromABank);
                dbcommand.Parameters.AddWithValue("@depno", depno);
                dbcommand.Parameters.AddWithValue("@CURRENY", currency);
                dbcommand.Connection.Open();
                var rowaffected = dbcommand.ExecuteNonQuery();
                dbcommand.Connection.Close();
                // Fill the table from adapter
                //  SqlDataAdapter.Fill(dataTable);
                return rowaffected > 0;
            }
        }

        public DataTable GetRegisterACC(string Store_No)
        {
            return _helperCommonService.GetSqlData(@"  select CODE  from Registers where trim(store_no)=trim(@Store_No) and code not like '%MANAGER%' 
                                                            UNION
                                                            SELECT CODE FROM Register_Xaction where trim(store_no)=trim(@Store_No) and code not like '%MANAGER%' AND LEN(TRIM(CODE))>0
                                                            order by code",
                "@Store_No", Store_No.Trim());
        }
        public bool GiveAmountToManager(string StoreCode, string Register, decimal Amt, DateTime? dDate, bool AddCashToARegisterFromManager, string currency = "")
        {
            try
            {
                using (SqlCommand dbcommand = new SqlCommand())
                {
                    //SqlDataAdapter.SelectCommand = new SqlCommand();
                    dbcommand.Connection = _connectionProvider.GetConnection();
                    dbcommand.CommandType = CommandType.StoredProcedure;

                    // Assign the SQL to the command object
                    dbcommand.CommandText = "GiveAmountToManager";
                    dbcommand.Parameters.AddWithValue("@StoreCode", StoreCode.Trim());
                    dbcommand.Parameters.AddWithValue("@Register", Register.Trim());
                    dbcommand.Parameters.AddWithValue("@Amt", Amt);
                    dbcommand.Parameters.AddWithValue("@DATE", dDate);
                    dbcommand.Parameters.AddWithValue("@ADDCASHTOAREGISTERFROMMANAGER", AddCashToARegisterFromManager);
                    dbcommand.Parameters.AddWithValue("@CURRENY", currency);
                    dbcommand.Connection.Open();
                    var rowaffected = dbcommand.ExecuteNonQuery();
                    dbcommand.Connection.Close();
                    // Fill the table from adapter
                    //  SqlDataAdapter.Fill(dataTable);
                    return rowaffected > 0;
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }
        public DataTable GetRegister(string Store_No)
        {
            return _helperCommonService.GetSqlData("select *,cast(1 as bit) IsOld  from Registers where trim(store_no)=@Store_No and code not like '%MANAGER%'order by code",
                "@Store_No", Store_No.Trim());
        }
        public DataRow CheckTransactionsInRegister(string Store, string RegisterCode)
        {
            DataTable dataTable = _helperCommonService.GetSqlData("SELECT * FROM register_xaction WHERE TRIM(code)=TRIM(@RegisterCode) AND RTRIM(store_no)=TRIM(@Store)",
                "@Store", Store.Trim(), "@RegisterCode", RegisterCode.Trim());

            return dataTable.Rows.Count > 0 ? dataTable.Rows[0] : null;
        }
        public bool AddEditDeleteRegister(string dtDynamicData, string Store, string Code)
        {
            using (SqlCommand dbcommand = new SqlCommand())
            {
                dbcommand.Connection = _connectionProvider.GetConnection();
                dbcommand.CommandType = CommandType.StoredProcedure;
                dbcommand.CommandText = "AddEditDeleteRegister";
                dbcommand.Parameters.AddWithValue("@DynamicData", dtDynamicData);
                dbcommand.Parameters.AddWithValue("@Store", Store.Trim());
                dbcommand.Parameters.AddWithValue("@Code", Code.Trim());
                dbcommand.Connection.Open();
                var rowaffected = dbcommand.ExecuteNonQuery();
                dbcommand.Connection.Close();
                return rowaffected > 0;
            }
        }
        public DataTable GetDataByFilter(DateTime? FromDate, DateTime? ToDate, string Store, string Register, out decimal Balance, bool IsManager = false, string Currency = "", bool IsbankCash = false)
        {
            DataTable dataTable = new DataTable();
            using (SqlDataAdapter SqlDataAdapter = new SqlDataAdapter())
            {
                SqlDataAdapter.SelectCommand = new SqlCommand();
                SqlDataAdapter.SelectCommand.Connection = _connectionProvider.GetConnection();
                SqlDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter.SelectCommand.CommandText = @"GetCash_Xaction";
                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@FromDate", FromDate);
                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@ToDate", ToDate);
                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@Store", Store);
                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@Register", Register);
                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@IsManager", IsManager);
                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@CURRENY", Currency);
                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@IsbankCash", IsbankCash);


                SqlParameter pBal = new SqlParameter("@Balance", SqlDbType.Decimal) { Precision = 18, Scale = 2 };
                pBal.Direction = ParameterDirection.Output;
                SqlDataAdapter.SelectCommand.Parameters.Add(pBal);

                SqlDataAdapter.Fill(dataTable);

                Balance = pBal.Value != DBNull.Value ? (decimal)pBal.Value : 0;
            }
            return dataTable;
        }
        public DataTable GetCurrentBalanceOfAllRegister(string Store, out decimal Balance, string Currency = "", bool IsbankCash = false)
        {
            Balance = 0;
            DataTable dataTable = new DataTable();
            using (SqlDataAdapter SqlDataAdapter = new SqlDataAdapter())
            {
                SqlDataAdapter.SelectCommand = new SqlCommand();
                SqlDataAdapter.SelectCommand.Connection = _connectionProvider.GetConnection();
                SqlDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter.SelectCommand.CommandText = @"GetCurrentBalanceOfAllRegister";
                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@Store", Store);
                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@CURRENY", Currency);
                SqlDataAdapter.SelectCommand.Parameters.AddWithValue("@IsbankCash", IsbankCash);
                SqlDataAdapter.Fill(dataTable);
                if (dataTable.Rows.Count > 0)
                    Balance = dataTable.Compute("SUM(BALANCE)", "1=1") == DBNull.Value ? 0 : (decimal)dataTable.Compute("SUM(BALANCE)", "1=1");
            }
            return dataTable;
        }

    }
}
