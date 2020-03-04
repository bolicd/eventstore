using DbUp;
using Microsoft.Extensions.Configuration;
using System;
using System.Reflection;

namespace DbMigration
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Migration starting. ");
            var connectionString = GetConnectionString(args.Length > 0 ? args[0] : null);

            EnsureDatabase.For.SqlDatabase(connectionString); //Creates database if not exist

            var upgradeEngineBuilder = DeployChanges.To
                            .SqlDatabase(connectionString, null)
                            .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly())
                            .WithTransaction()
                            .LogToConsole();

            var upgrader = upgradeEngineBuilder.Build();
            if (upgrader.IsUpgradeRequired())
            {
                var results = upgrader.PerformUpgrade();
                if (results.Successful)
                {
                    Console.WriteLine("Database upgrade success");
                }
            } else
            {
                Console.WriteLine("No upgrade is required");
            }
        }

        private static string GetConnectionString(string connString)
        {

            if (!string.IsNullOrWhiteSpace(connString)) return connString;

            IConfiguration config = new ConfigurationBuilder()
           .AddJsonFile("appsettings.json",
                        true,
                        true).Build();

            return config.GetConnectionString("EventStoreDatabase");
        }
    }
}
