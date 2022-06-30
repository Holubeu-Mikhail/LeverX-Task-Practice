using DataAccessLayer.IntegrationTests.Common;
using Microsoft.Data.SqlClient;

namespace DataAccessLayer.IntegrationTests.Helpers
{
    internal class ProductDataHelper
    {
        internal static void FillTable()
        {
            using var connection = new SqlConnection(ConnectionService.GetConnectionString());
            connection.Open();

            var tableName = "Product";

            var sqlExpression =
                $"INSERT INTO {tableName} (Id, Name, Quantity, TypeId) VALUES (1, @Name, @Quantity, @TypeId)"; ;

            var command = new SqlCommand(sqlExpression, connection);
            command.Parameters.AddWithValue("@Id", 1);
            command.Parameters.AddWithValue("@Name", "Fish");
            command.Parameters.AddWithValue("@Quantity", 1);
            command.Parameters.AddWithValue("@TypeId", 1);
            command.ExecuteNonQuery();

            connection.Close();
        }
    }
}
