using Core.Person.DomainEvents;
using System.Collections.Generic;
using Tactical.DDD;

namespace Core.Person
{
    public class Person: Tactical.DDD.EventSourcing.AggregateRoot<PersonId>
    {
        public override PersonId Id { get; protected set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Address PersonAddress { get; set; }

        public Person(IEnumerable<IDomainEvent> events) : base(events)
        {
        }

        private Person()
        {

        }

        public static Person CreateNewPerson(
            string firstName,
            string lastName)
        {

            var person = new Person();
            person.Apply(new PersonCreated(new PersonId(),
                firstName, lastName));

            return person;
        }

        public void On(PersonCreated @event)
        {
            Id = @event.PersonId;
            FirstName = @event.FirstName;
            LastName = @event.LastName;
        }
    }
}
