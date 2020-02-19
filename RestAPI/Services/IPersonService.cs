using Core.Person;
using RestAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestAPI.Services
{
    public interface IPersonService
    {
        Task<PersonId> CreatePerson(string firstName, string lastName);
        Task<PersonDto> GetPerson(string personId);
    }
}
