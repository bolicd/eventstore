using EventStoreTests.Infrastructure;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventStoreTests.Integration
{
    public class EventStoreIntegrationTests : IntegrationTestBase
    {
        [SetUp]
        public void SetUp()
        {
            StartDatabase();
        }

        [Test]
        public void SomeTest()
        {
            Assert.AreEqual(1, 1);
        }
    }
}
