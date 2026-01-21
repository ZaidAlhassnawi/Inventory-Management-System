using Microsoft.Extensions.Configuration;

namespace InventoryManagementSystem_DataAccessLayer
{
    public class DataAccessSettings
    {
        public string ConnectionString { get; }

        public DataAccessSettings()
        {
            IConfiguration config = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json", false, true)
                .Build();

            ConnectionString = config.GetConnectionString("DBConnectionString");
        }
    }

}
