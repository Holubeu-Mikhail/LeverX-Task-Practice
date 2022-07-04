using Microsoft.Extensions.Configuration;

namespace DataAccessLayer.IntegrationTests.Common
{
    internal static class ConnectionService
    {
        public static string GetConnectionString()
        {
            //var result = ConfigurationManager.ConnectionStrings["databasePath"].ConnectionString;
            var result = "Data Source=LXIBY788;Initial Catalog=ProductsDb;Integrated Security=True;";
            return result;
        }
    }
}