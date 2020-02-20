using Core.Person;
using Microsoft.AspNetCore.Mvc;
using RestAPI.Model;
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
        /// <returns>Newly created person object aggregateId</returns>
        [HttpPost]
        [SwaggerResponse(System.Net.HttpStatusCode.OK, Type = typeof(object))]
        public async Task<object> GeneratePerson([FromBody]GeneratePersonDTO person)
        {
            var insertedPersonId = await _personService.CreatePerson(person.FirstName, person.LastName);
            return new { PersonId = insertedPersonId.ToString() };
        }

        /// <summary>
        /// Fetch aggregate from event store using aggregateId(personId)
        /// This will fetch all the events for given aggregate and mutate
        /// aggregate using each event in sequence, therefore reconstructing
        /// latest aggregate state
        /// </summary>
        /// <param name="personId"></param>
        /// <returns></returns>
        [HttpGet]
        [SwaggerResponse(System.Net.HttpStatusCode.OK, Type = typeof(PersonDto))]
        public async Task<PersonDto> GetPerson([FromQuery]string personId)
        {
            return await _personService.GetPerson(personId);
        }

    }
}
