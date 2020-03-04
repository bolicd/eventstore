using System;
using System.Data.SqlClient;
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
        private string _databaseName { get; set; }

        [OneTimeSetUp]
        public void StartDatabase()
        {
            _databaseName = Guid.NewGuid().ToString();
            ConnectionString = $@"Server=(localdb)\mssqllocaldb;Database={_databaseName};Trusted_Connection=True;";
            Program.Main(new string[] { ConnectionString });
        }

        [OneTimeTearDown]
        public void Teardown()
        {
            //TODO : resolve this, need to move to master database and
//            USE master --be sure that you're not on MYDB
//ALTER DATABASE MYDB SET SINGLE_USER WITH ROLLBACK IMMEDIATE
//DROP DATABASE MYDB;
            // drop database here!
            string commandQuery = $"DROP DATABASE \"{_databaseName}\"";

            using SqlConnection conn = new SqlConnection(ConnectionString);
            using SqlCommand cmd = new SqlCommand(commandQuery, conn);
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }
    }
}
