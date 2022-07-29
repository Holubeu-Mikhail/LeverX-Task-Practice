using DataAccessLayer.IntegrationTests.Common;
using Microsoft.Data.SqlClient;

namespace DataAccessLayer.IntegrationTests.Helpers
{
    internal class CityDataHelper
    {
        internal static void FillTable()
        {
            using var connection = new SqlConnection(ConnectionService.GetConnectionString());
            connection.Open();

            var tableName = "Cities";

            var sqlExpression =
                $"INSERT INTO {tableName} (Id, Name, Code) VALUES (@Id, @Name, @Code)"; ;

            var command = new SqlCommand(sqlExpression, connection);
            command.Parameters.AddWithValue("@Id", DataHelper.CityId);
            command.Parameters.AddWithValue("@Name", "Zhlobin");
            command.Parameters.AddWithValue("@Code", 201);
            command.ExecuteNonQuery();

            connection.Close();
        }
    }
}
