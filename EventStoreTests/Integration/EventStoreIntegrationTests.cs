using Core.Person;
using EventStoreTests.Infrastructure;
using Infrastructure.Factories;
using Infrastructure.Repositories;
using NUnit.Framework;
using System.Threading.Tasks;

namespace EventStoreTests.Integration
{
    [TestFixture]
    public class EventStoreIntegrationTests : IntegrationTestBase
    {

        private IEventStore _eventStore;

        [SetUp]
        public void SetUp()
        {
            _eventStore = new EventStoreRepository(new SqlConnectionFactory(ConnectionString));
        }

        [Test]
        public async Task Given_PersonAggregateCreated_When_SavedToEventStore_ThenShouldBeTheSameWhenFetched()
        {
            var personId = new PersonId();

            var personAggregate = Person.CreateNewPerson("Chuck", "Norris");

            await _eventStore.SaveAsync(personId,personAggregate.Version, personAggregate.DomainEvents, "PersonAggregate");
            
            var results = await _eventStore.LoadAsync(personId);

            // TODO:  check why reconstructed aggregate has 0 domain events in the list?
            var fetchedPerson = new Person(results);
            Assert.IsNotNull(results);
            Assert.AreEqual(personAggregate, fetchedPerson);
        }
    }
}
