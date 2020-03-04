using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DbMigration;

namespace EventStoreTests.Infrastructure
{
    public abstract class IntegrationTestBase
    {
        // start in memory database
        // run migrations project
        public string ConnectionString { get; set; }

        public void StartDatabase()
        {
            ConnectionString = $@"Server=(localdb)\mssqllocaldb;Database={Guid.NewGuid().ToString()};Trusted_Connection=True;";
            // this should generate new temporary database
            Program.Main(new string[] { ConnectionString });
        }
    }
}
