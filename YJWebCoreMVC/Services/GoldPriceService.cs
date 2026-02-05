/*
 *  Created By Dharani
 *  Dharani 11/28/2025 Added GetGoldPriceValues, UpdateMarkupPrice
 */
using Microsoft.Data.SqlClient;
using System.Data;

namespace YJWebCoreMVC.Services
{
    public class GoldPriceService
    {
        private readonly ConnectionProvider _connectionProvider;
        private readonly HelperCommonService _helperCommonService;

        public GoldPriceService(ConnectionProvider connectionProvider, HelperCommonService helperCommonService)
        {
            _connectionProvider = connectionProvider;
            _helperCommonService = helperCommonService;
        }

        public DataTable GetGoldPriceValues()
        {
            return _helperCommonService.GetSqlData(@"SELECT GOLD,SILVER,PLAT,MARKUP FROM GOLD");
        }

        public void UpdateMarkupPrice(decimal markup, decimal lnmMultiVal, decimal goldSurchargeVal, string loggedUser,
            decimal settingMulti = 0, decimal findingMulti = 0, decimal stoneMulti = 0, decimal laborMulti = 0,
            string castingMulti = "")
        {
            using (var connection = _connectionProvider.GetConnection())
            using (var command = new SqlCommand("UpdateMarkupPrice", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@markup", SqlDbType.Decimal) { Value = markup });
                command.Parameters.Add(new SqlParameter("@lnm_multival", SqlDbType.Decimal) { Value = lnmMultiVal });
                command.Parameters.Add(new SqlParameter("@gold_surchrgval", SqlDbType.Decimal) { Value = goldSurchargeVal });
                command.Parameters.Add(new SqlParameter("@loggedUser", SqlDbType.NVarChar, 50) { Value = loggedUser ?? string.Empty });
                command.Parameters.Add(new SqlParameter("@settingmulti", SqlDbType.Decimal) { Value = settingMulti });
                command.Parameters.Add(new SqlParameter("@findingmulti", SqlDbType.Decimal) { Value = findingMulti });
                command.Parameters.Add(new SqlParameter("@stonemulti", SqlDbType.Decimal) { Value = stoneMulti });
                command.Parameters.Add(new SqlParameter("@labormulti", SqlDbType.Decimal) { Value = laborMulti });
                command.Parameters.Add(new SqlParameter("@castingmulti", SqlDbType.NVarChar, -1) { Value = castingMulti ?? string.Empty });
                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }
}
