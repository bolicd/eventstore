using System.Threading.Tasks;

namespace Core.Person.Repositories
{
    public interface IPersonRepository
    {
        Task<PersonId> SavePersonAsync(Person person);
        Task<Person> GetPerson(string id);
    }
}
