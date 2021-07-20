using Core.Person;
using Core.Person.Repositories;
using EventStoreTests.Infrastructure;
using FluentAssertions;
using Infrastructure.Factories;
using Infrastructure.Repositories;
using NUnit.Framework;
using System.Linq;
using System.Threading.Tasks;

namespace EventStoreTests.Integration
{
    [TestFixture]
    public class EventStoreIntegrationTests : IntegrationTestBase
    {

        private IEventStore _eventStore;
        private IPersonRepository _personRepository; 

        [SetUp]
        public void SetUp()
        {
            _eventStore = new EventStoreRepository(new SqlConnectionFactory(ConnectionString));
            _personRepository = new PersonRepository(_eventStore);
        }

        [Test]
        public async Task Given_PersonAggregateCreated_When_SavedToEventStore_Then_ShouldBeTheSameWhenFetched()
        {
            var personId = new PersonId();

            var personAggregate = Person.CreateNewPerson("Chuck", "Norris");

            await _eventStore.SaveAsync(personId,personAggregate.Version, personAggregate.DomainEvents, "PersonAggregate");
            
            var results = await _eventStore.LoadAsync(personId);

            var fetchedPerson = new Person(results);
            Assert.IsNotNull(results);
            Assert.AreEqual(personAggregate, fetchedPerson);
        }

        [Test]
        public async Task Given_PersonAggregate_When_AddressChanged_Then_ShouldBeTheSameWhenFetched()
        {
            var personId = new PersonId();

            var personAggregate = Person.CreateNewPerson("Chuck", "Norris");
            personAggregate.ChangePersonAddress("street1", "country1", "111222", "city");

            await _eventStore.SaveAsync(personId, personAggregate.Version, personAggregate.DomainEvents, "PersonAggregate");

            var results = await _eventStore.LoadAsync(personId);

            var fetchedPerson = new Person(results);
            Assert.IsNotNull(results);
            Assert.AreEqual(fetchedPerson.PersonAddress.City, "city");
            Assert.AreEqual(fetchedPerson.PersonAddress.Country, "country1");
            Assert.AreEqual(fetchedPerson.PersonAddress.ZipCode, "111222");
            Assert.AreEqual(fetchedPerson.PersonAddress.Street, "street1");
        }

        [Test]
        public async Task Given_PersonAggregate_When_AddressChanged_Then_FetchWithPersonRepository()
        {
            var personId = new PersonId();

            var personAggregate = Person.CreateNewPerson("Chuck", "Norris");
            personAggregate.ChangePersonAddress("street1", "country1", "111222", "city");

            await _eventStore.SaveAsync(personId, personAggregate.Version, personAggregate.DomainEvents, "PersonAggregate");

            var results = await _personRepository.GetPerson(personId.ToString());
            results.LastName.Should().Be("Norris");
            results.FirstName.Should().Be("Chuck");
        }

        [Test]
        public async Task Fetch_NonExistantpersonFromRepo_Should_Return_NUll()
        {
            var personId = new PersonId();

            var results = await _personRepository.GetPerson(personId.ToString());

            results.Should().BeNull();

        }
    }
}
