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

        Task UpdatePersonAddress(PersonId personId, string city, string country, string street, string zipcode);
    }
}
