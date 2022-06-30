using DataAccessLayer.IntegrationTests.Common;
using Microsoft.Data.SqlClient;

namespace DataAccessLayer.IntegrationTests.Helpers
{
    internal class ProductTypeDataHelper
    {
        internal static void FillTable()
        {
            using var connection = new SqlConnection(ConnectionService.GetConnectionString());
            connection.Open();

            var tableName = "ProductType";

            var sqlExpression =
                $"INSERT INTO {tableName} (Id, Name) VALUES (1, @Name)"; ;

            var command = new SqlCommand(sqlExpression, connection);
            command.Parameters.AddWithValue("@Id", 1);
            command.Parameters.AddWithValue("@Name", "Food");
            command.ExecuteNonQuery();

            connection.Close();
        }
    }
}