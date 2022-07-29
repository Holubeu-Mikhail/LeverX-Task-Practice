using DataAccessLayer.IntegrationTests.Common;
using Microsoft.Data.SqlClient;

namespace DataAccessLayer.IntegrationTests.Helpers
{
    internal class BrandDataHelper
    {
        internal static void FillTable()
        {
            using var connection = new SqlConnection(ConnectionService.GetConnectionString());
            connection.Open();

            var tableName = "Brands";

            var sqlExpression =
                $"INSERT INTO {tableName} (Id, Name, Description, CityId) VALUES (@Id, @Name, @Description, @CityId)"; ;

            var command = new SqlCommand(sqlExpression, connection);
            command.Parameters.AddWithValue("@Id", DataHelper.BrandId);
            command.Parameters.AddWithValue("@Name", "Apple");
            command.Parameters.AddWithValue("@Description", "American company");
            command.Parameters.AddWithValue("@CityId", DataHelper.CityId);
            command.ExecuteNonQuery();

            connection.Close();
        }
    }
}
