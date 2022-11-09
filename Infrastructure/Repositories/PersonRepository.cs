using Core.Person;
using Core.Person.Repositories;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class PersonRepository : IPersonRepository
    {
        private readonly IEventStore _eventStore;
        public PersonRepository(IEventStore eventStore)
        {
            _eventStore = eventStore;
        }

        public async Task<Person> GetPerson(string id)
        {
            var personId = new PersonId(id);
            var personEvents = await _eventStore.LoadAsync(personId);

            return personEvents.Count>0 ? new Person(personEvents) : null;
        }

        public async Task<PersonId> SavePersonAsync(Person person)
        {
            await _eventStore.SaveAsync(person.Id, person.Version, person.DomainEvents);
            return person.Id;
        }
    }
}
