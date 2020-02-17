using Core.Person;
using Core.Person.Repositories;
using System;
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

        public async Task SavePersonAsync(Person person) => await _eventStore.SaveAsync(person.Id, person.Version, person.DomainEvents);
    }
}
