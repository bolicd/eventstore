using System;
using System.Collections.Generic;
using System.Text;
using Tactical.DDD;

namespace Core.Person.DomainEvents
{
    public class PersonCreated : DomainEvent
    {
        public PersonId PersonId { get; }
        public string FirstName { get; }
        public string LastName { get; }

        public PersonCreated(
            PersonId personId, 
            string firstName, 
            string lastName)
        {
            PersonId = personId;
            FirstName = firstName;
            LastName = lastName;
        }
    }
}
