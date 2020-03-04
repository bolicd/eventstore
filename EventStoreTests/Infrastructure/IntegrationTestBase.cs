using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DbMigration;
using NUnit.Framework;

namespace EventStoreTests.Infrastructure
{
    [SetUpFixture]
    public abstract class IntegrationTestBase
    {
        // start in memory database
        // run migrations project
        public string ConnectionString { get; set; }

        [OneTimeSetUp]
        public void StartDatabase()
        {
            ConnectionString = $@"Server=(localdb)\mssqllocaldb;Database={Guid.NewGuid().ToString()};Trusted_Connection=True;";
            Program.Main(new string[] { ConnectionString });
        }

        [OneTimeTearDown]
        public void Teardown()
        {
            // drop database here!
        }
    }
}
