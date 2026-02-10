// Neetha    02/06/2025 Created new file. 

using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using System.Data;
using YJWebCoreMVC.Models;


namespace YJWebCoreMVC.Services
{
    public class AdminMiscService
    {
        private readonly ConnectionProvider _connectionProvider;

        public AdminMiscService(ConnectionProvider connectionProvider)
        {
            _connectionProvider = connectionProvider;
        }
        public async Task<List<AdminMiscModel>> GetDiscountType()
        {
            List<AdminMiscModel> discountList = new();
            using (var connection = _connectionProvider.GetConnection())
            using (SqlCommand dbCommand = new SqlCommand("SELECT ISNULL(Discount,'') AS Discount, ISNULL(Percentage,0) AS Percentage FROM Discounts ORDER BY Discount", connection))
            {
                dbCommand.CommandType = CommandType.Text;
                await connection.OpenAsync();

                using (SqlDataReader reader = await dbCommand.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        discountList.Add(new AdminMiscModel
                        {
                            Discount = reader["Discount"].ToString().Trim(),
                            percentage = Convert.ToDecimal(reader["Percentage"])
                        });
                    }
                }
            }
            return discountList;
        }
        public async Task<bool> SaveDiscountsToDatabase(string xmlData)
        {
            try
            {
                using var connection = _connectionProvider.GetConnection();
                using var cmd = new SqlCommand("DiscountTypes", connection);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@DiscountType", SqlDbType.Xml).Value = xmlData;

                await connection.OpenAsync();
                await cmd.ExecuteNonQueryAsync();

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
        public async Task<bool> DeleteDiscountFromDatabase(string discountType)
        {
            try
            {
                using var connection = _connectionProvider.GetConnection();
                using var cmd = new SqlCommand(
                    @"DELETE FROM DISCOUNTS WHERE Discount = @Dname",
                    connection);

                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@Dname", discountType.Trim().ToLower());

                await connection.OpenAsync();
                var rowsAffected = await cmd.ExecuteNonQueryAsync();

                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
        public async Task<DataTable> ReferrlaUpdate(string checkValid = "",string advtText = "",decimal refByPoint = 0,decimal refToPoint = 0,decimal refPer = 0)
        {
            using var connection = _connectionProvider.GetConnection();
            using var cmd = new SqlCommand();
            cmd.Connection = connection;

            if (checkValid == "GetData")
            {
                cmd.CommandText = @"SELECT referred_to_points,referred_by_points,referred_per,advt_text, * FROM UPS_INS1";
                cmd.CommandType = CommandType.Text;
            }
            else
            {
                cmd.CommandText = @"UPDATE UPS_INS1 SET referred_to_points = @RefToPoint,referred_by_points = @RefByPoint,referred_per = @RefPer,advt_text = @AdvtText";
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add("@RefToPoint", SqlDbType.Decimal).Value = refToPoint;
                cmd.Parameters.Add("@RefByPoint", SqlDbType.Decimal).Value = refByPoint;
                cmd.Parameters.Add("@RefPer", SqlDbType.Decimal).Value = refPer;
                cmd.Parameters.Add("@AdvtText", SqlDbType.NVarChar).Value = advtText ?? "";
            }
            await connection.OpenAsync();
            var dt = new DataTable();
            if (checkValid == "GetData")
            {
                using var reader = await cmd.ExecuteReaderAsync();
                dt.Load(reader);
            }
            else
            {
                await cmd.ExecuteNonQueryAsync();
            }

            return dt;
        }
        public async Task<DataTable> SalesNoTax(string xmlData = "")
        {
            using var connection = _connectionProvider.GetConnection();
            using var cmd = new SqlCommand();
            cmd.Connection = connection;
            if (!string.IsNullOrEmpty(xmlData)) { cmd.CommandText = "SalesNoTaxDetails"; cmd.CommandType = CommandType.StoredProcedure; cmd.Parameters.Add("@Salesnotax", SqlDbType.Xml).Value = xmlData; }
            else { cmd.CommandText = "SELECT Reason FROM notax_reasons"; cmd.CommandType = CommandType.Text; }
            await connection.OpenAsync();
            var dt = new DataTable();
            using var reader = await cmd.ExecuteReaderAsync();
            dt.Load(reader);
            return dt.Rows.Count > 0 ? dt : null;
        }


        public async Task<bool> DeleteSalesTaxExemption(string reason)
        {
            using var connection = _connectionProvider.GetConnection();
            using var cmd = new SqlCommand("DELETE FROM notax_reasons WHERE Reason=@Reason", connection);
            cmd.Parameters.Add("@Reason", SqlDbType.NVarChar).Value = reason.Trim();
            await connection.OpenAsync();
            return await cmd.ExecuteNonQueryAsync() > 0;
        }

        public async Task<DataTable> GetCharitiesList()
        {
            using var con = _connectionProvider.GetConnection();
            using var cmd = new SqlCommand("SELECT * FROM charities ORDER BY charity", con);

            await con.OpenAsync();
            var dt = new DataTable();
            using var r = await cmd.ExecuteReaderAsync();
            dt.Load(r);
            return dt;
        }

        public async Task<bool> CheckCharityInfo(string charity)
        {
            using var con = _connectionProvider.GetConnection();
            using var cmd = new SqlCommand("SELECT COUNT(1) FROM charities WHERE Charity=@Charity", con);
            cmd.Parameters.Add("@Charity", SqlDbType.NVarChar).Value = charity;

            await con.OpenAsync();
            return (int)await cmd.ExecuteScalarAsync() > 0;
        }

        public async Task<bool> AddCharityInfo(string cCharity, CharityModel c)
        {
            using var con = _connectionProvider.GetConnection();
            using var cmd = new SqlCommand(@"INSERT INTO charities (Charity,Address1,Address2,Address3,Contact,Telephone,Email)
                                     VALUES (@C,@A1,@A2,@A3,@CT,@T,@E)", con);

            cmd.Parameters.Add("@C", SqlDbType.NVarChar).Value = cCharity ?? "";
            cmd.Parameters.Add("@A1", SqlDbType.NVarChar).Value = c.Address1 ?? "";
            cmd.Parameters.Add("@A2", SqlDbType.NVarChar).Value = c.Address2 ?? "";
            cmd.Parameters.Add("@A3", SqlDbType.NVarChar).Value = c.Address3 ?? "";
            cmd.Parameters.Add("@CT", SqlDbType.NVarChar).Value = c.Contact ?? "";
            cmd.Parameters.Add("@T", SqlDbType.NVarChar).Value = c.Telephone ?? "";
            cmd.Parameters.Add("@E", SqlDbType.NVarChar).Value = c.Email ?? "";

            await con.OpenAsync();
            return await cmd.ExecuteNonQueryAsync() > 0;
        }

        public async Task<bool> EditCharityInfo(string cCharity, CharityModel c)
        {
            using var con = _connectionProvider.GetConnection();
            using var cmd = new SqlCommand(@"UPDATE charities SET Address1=@A1,Address2=@A2,Address3=@A3,
                                     Contact=@CT,Telephone=@T,Email=@E WHERE Charity=@C", con);

            cmd.Parameters.Add("@C", SqlDbType.NVarChar).Value = cCharity ?? "";
            cmd.Parameters.Add("@A1", SqlDbType.NVarChar).Value = c.Address1 ?? "";
            cmd.Parameters.Add("@A2", SqlDbType.NVarChar).Value = c.Address2 ?? "";
            cmd.Parameters.Add("@A3", SqlDbType.NVarChar).Value = c.Address3 ?? "";
            cmd.Parameters.Add("@CT", SqlDbType.NVarChar).Value = c.Contact ?? "";
            cmd.Parameters.Add("@T", SqlDbType.NVarChar).Value = c.Telephone ?? "";
            cmd.Parameters.Add("@E", SqlDbType.NVarChar).Value = c.Email ?? "";

            await con.OpenAsync();
            return await cmd.ExecuteNonQueryAsync() > 0;
        }

        public async Task<bool> UpdateCharities(string xml)
        {
            using var con = _connectionProvider.GetConnection();
            using var cmd = new SqlCommand("updateCharities", con);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 5000;
            cmd.Parameters.Add("@tblcharities", SqlDbType.Xml).Value = xml;

            await con.OpenAsync();
            return await cmd.ExecuteNonQueryAsync() > 0;
        }


    }
}
