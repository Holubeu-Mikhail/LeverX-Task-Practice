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
                $"INSERT INTO {tableName} (Id, Name, Description, TownId) VALUES (@Id, @Name, @Description, @TownId)"; ;

            var command = new SqlCommand(sqlExpression, connection);
            command.Parameters.AddWithValue("@Id", 1);
            command.Parameters.AddWithValue("@Name", "Apple");
            command.Parameters.AddWithValue("@Description", "American company");
            command.Parameters.AddWithValue("@TownId", 1);
            command.ExecuteNonQuery();

            connection.Close();
        }
    }
}
