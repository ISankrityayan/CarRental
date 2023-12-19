using System;
using Microsoft.Extensions.Configuration;

namespace CarRentalAssignment.Utilities
{
	public class ConnectionStringUtility
	{
        public static string GetConnectionString(string name)
        {
            var basePath = AppContext.BaseDirectory;
            var appSettingsPath = Path.Combine(basePath, "appsettings.json");
            //Console.WriteLine($"Base Path: {basePath}");
            //Console.WriteLine($"Expected appsettings.json Path: {appSettingsPath}");

            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            return configuration.GetConnectionString(name);
        }
    }
}

