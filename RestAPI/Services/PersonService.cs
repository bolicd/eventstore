using Core.Person;
using Core.Person.Repositories;
using RestAPI.Model;
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
            var pid=  await _personRepository.SavePersonAsync(person);
            return pid;
        }

        public async Task<PersonDto> GetPerson(string personId)
        {
            var person = await _personRepository.GetPerson(personId);
            //return person != null ? new PersonDto() { FirstName = person.FirstName, LastName = person.LastName,
            //    PersonId = person.Id.ToString() } : null;
            return new PersonDto()
            {
                FirstName = person.FirstName,
                LastName = person.LastName,
                PersonId = person.Id.ToString()

            };
        }
          
    }
}
