using Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using RestAPI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
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
 
        [HttpGet]
        public string GetTest()
        {
            return "test";
        }

        [HttpPost]
        public async Task GeneratePerson([FromBody]GeneratePersonDTO person)
        {
            await _personService.CreatePerson(person.FirstName, person.LastName);
        }

    }
}
