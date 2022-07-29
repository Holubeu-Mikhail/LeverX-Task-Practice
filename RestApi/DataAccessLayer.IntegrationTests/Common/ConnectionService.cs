using System.Configuration;

namespace DataAccessLayer.IntegrationTests.Common
{
    internal static class ConnectionService
    {
        public static string GetConnectionString()
        {
            var result = ConfigurationManager.ConnectionStrings["databasePath"].ConnectionString;
            return result;
        }
    }
}