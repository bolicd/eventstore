using Core.Person;
using Microsoft.AspNetCore.Mvc;
using RestAPI.Services;
using Swashbuckle.Swagger.Annotations;
using System.Threading.Tasks;

namespace RestAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PersonController : ControllerBase
    {

        private readonly PersonService _personService;


        public PersonController(PersonService personService)
        {
            _personService = personService;
        }
 

        /// <summary>
        /// Creates new person Aggregate using first and last name as parameters
        /// Person will be saved as stream of events in event store
        /// </summary>
        /// <param name="person"></param>
        /// <returns>Newly created person object</returns>
        [HttpPost]
        [SwaggerResponse(System.Net.HttpStatusCode.OK, Type = typeof(Person))]
        public async Task GeneratePerson([FromBody]GeneratePersonDTO person)
        {
            Ok(await _personService.CreatePerson(person.FirstName, person.LastName));
        }

    }
}
