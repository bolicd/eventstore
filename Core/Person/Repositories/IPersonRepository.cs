using System.Threading.Tasks;

namespace Core.Person.Repositories
{
    public interface IPersonRepository
    {
        Task SavePersonAsync(Person person);
    }
}
