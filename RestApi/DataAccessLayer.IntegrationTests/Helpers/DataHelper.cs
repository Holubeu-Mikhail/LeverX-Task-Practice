using DataAccessLayer.IntegrationTests.Common;
using Microsoft.Data.SqlClient;

namespace DataAccessLayer.IntegrationTests.Helpers
{
    internal static class DataHelper
    {
        public static Guid CityId
        {
            get
            {
                return new Guid("e4c08e22-e8fb-473b-8f9a-b30983923cab");
            }
        }
        public static Guid BrandId
        {
            get
            {
                return new Guid("9f8ce36d-09e1-42e5-9c69-085bf284dd09");
            }
        }

        public static Guid ProductId
        {
            get
            {
                return new Guid("f2c56589-5a46-47f4-8108-b42c09d977dc");
            }
        }

        public static Guid ProductTypeId
        {
            get
            {
                return new Guid("4bacf157-2ee3-4efa-9466-a4edfe5ed529");
            }
        }

        public static void DeleteAllFromDatabase()
        {
            using var connection = new SqlConnection(ConnectionService.GetConnectionString());
            connection.Open();

            var tableName = "Products";

            var sqlExpression = $"DELETE FROM {tableName}";

            var productCommand = new SqlCommand(sqlExpression, connection);
            productCommand.ExecuteNonQuery();

            tableName = "ProductTypes";

            sqlExpression = $"DELETE FROM {tableName}";

            var productTypeCommand = new SqlCommand(sqlExpression, connection);
            productTypeCommand.ExecuteNonQuery();

            tableName = "Brands";

            sqlExpression = $"DELETE FROM {tableName}";

            var brandCommand = new SqlCommand(sqlExpression, connection);
            brandCommand.ExecuteNonQuery();

            tableName = "Cities";

            sqlExpression = $"DELETE FROM {tableName}";

            var cityCommand = new SqlCommand(sqlExpression, connection);
            cityCommand.ExecuteNonQuery();

            connection.Close();
        }
    }
}
