using Core.Person;
using Core.Person.Repositories;
using System.Threading.Tasks;

namespace RestAPI.Services
{
    public class PersonService : IPersonService
    {
        private readonly IPersonRepository _personRepository;
        public PersonService(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        public async Task<PersonId> CreatePerson(string firstName, string lastName)
        {
            var person = Person.CreateNewPerson(firstName, lastName);
            return await _personRepository.SavePersonAsync(person);
        }

        public Task<Person> GetPerson(string personId)
        {
            throw new System.NotImplementedException();
        }
    }
}
