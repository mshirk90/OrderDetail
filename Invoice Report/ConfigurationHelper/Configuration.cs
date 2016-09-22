using System.Configuration;

namespace ConfigurationHelper
{
    public class Configuration
    {
        public static string GetConnectionString(string name)
        {
            string result = string.Empty;
            result = ConfigurationManager.ConnectionStrings[name].ConnectionString;

            return result;
        }
    }
}