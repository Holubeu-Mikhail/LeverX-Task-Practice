using Microsoft.Data.SqlClient;

namespace DataAccessLayer.IntegrationTests.Common
{
    internal static class BackupService
    {
        private static readonly string ConnectionString = "Data Source=LXIBY788;Initial Catalog=ProductsDb;Integrated Security=True";
        private const string DatabaseName = "ProductsDb";
        private const string BackupPath = "D:\\backup.bak";

        public static void CreateDatabaseBackup()
        {
            var sqlExpression = $"USE master BACKUP DATABASE {DatabaseName} TO DISK=@backupFolderFullPathAndFileName";
            using var connection = new SqlConnection(ConnectionString);
            connection.Open();
            var command = new SqlCommand(sqlExpression, connection);
            command.Parameters.AddWithValue("@backupFolderFullPathAndFileName", BackupPath);
            command.ExecuteNonQuery();
        }

        public static void RestoreDatabaseBackup()
        {
            var stringQuerySingleUser = $"use master; " +
                                           $"ALTER DATABASE {DatabaseName} SET SINGLE_USER WITH ROLLBACK IMMEDIATE;";
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                var command = new SqlCommand(stringQuerySingleUser, connection);
                command.ExecuteNonQuery();
            }

            var sqlExpression = $"use master; " +
                                   $"RESTORE DATABASE {DatabaseName} FROM DISK=@backupFolderFullPathAndFileName WITH REPLACE;";
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                var command = new SqlCommand(sqlExpression, connection);
                command.Parameters.AddWithValue("@backupFolderFullPathAndFileName", BackupPath);
                command.ExecuteNonQuery();
            }

            var stringQueryMultiUser = $"use master; " +
                                          $"ALTER DATABASE {DatabaseName} SET MULTI_USER;";
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                var command = new SqlCommand(stringQueryMultiUser, connection);
                command.ExecuteNonQuery();
            }
        }
    }
}
