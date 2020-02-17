using Core.Person;
using Core.Person.Repositories;
using System.Threading.Tasks;

namespace RestAPI.Services
{
    public class PersonService
    {
        private readonly IPersonRepository _personRepository;
        public PersonService(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        public async Task<Person> CreatePerson(string firstName, string lastName)
        {
            var person = Person.CreateNewPerson(firstName, lastName);
            await _personRepository.SavePersonAsync(person);
            return person;
        }
    }
}
