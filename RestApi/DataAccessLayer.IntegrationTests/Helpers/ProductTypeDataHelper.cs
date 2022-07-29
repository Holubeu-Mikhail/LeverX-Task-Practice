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

            var tableName = "ProductTypes";

            var sqlExpression =
                $"INSERT INTO {tableName} (Id, Name) VALUES (@Id, @Name)"; ;

            var command = new SqlCommand(sqlExpression, connection);
            command.Parameters.AddWithValue("@Id", DataHelper.ProductTypeId);
            command.Parameters.AddWithValue("@Name", "Food");
            command.ExecuteNonQuery();

            connection.Close();
        }
    }
}