using DbUp;
using Microsoft.Extensions.Configuration;
using System;
using System.Reflection;

namespace DbMigration
{
    class Program
    {
        static void Main(string[] args)
        {
            IConfiguration config = new ConfigurationBuilder()
           .AddJsonFile("appsettings.json",
                        true,
                        true).Build();

            var connectionString = config.GetConnectionString("EventStoreDatabase");
            Console.WriteLine("Migration starting. ");

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
    }
}
