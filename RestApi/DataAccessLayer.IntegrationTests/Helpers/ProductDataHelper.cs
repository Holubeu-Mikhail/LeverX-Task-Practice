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

            var tableName = "Products";

            var sqlExpression =
                $"INSERT INTO {tableName} (Id, Name, Quantity, TypeId, BrandId) VALUES (@Id, @Name, @Quantity, @TypeId, @BrandId)";

            var command = new SqlCommand(sqlExpression, connection);
            command.Parameters.AddWithValue("@Id", DataHelper.ProductId);
            command.Parameters.AddWithValue("@Name", "Fish");
            command.Parameters.AddWithValue("@Quantity", 1);
            command.Parameters.AddWithValue("@TypeId", DataHelper.ProductTypeId);
            command.Parameters.AddWithValue("@BrandId", DataHelper.BrandId);
            command.ExecuteNonQuery();

            connection.Close();
        }
    }
}
