using Core.Person.DomainEvents;
using System.Collections.Generic;
using Tacta.EventStore.Domain;

namespace Core.Person
{
    public class Person: AggregateRoot<PersonId>
    {
        public override PersonId Id { get; protected set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public Address PersonAddress { get; private set; }

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
            person.Apply(new PersonCreated(new PersonId().ToString(),
                firstName, lastName));

            return person;
        }

        public void ChangePersonAddress(string street,string country, string zipCode, string city)
        {
            Apply(new AddressChanged(city, country, zipCode, street, Id.ToString()));
        }

        public void On(PersonCreated @event)
        {
            Id = new PersonId(@event.PersonId);
            FirstName = @event.FirstName;
            LastName = @event.LastName;
        }

        public void On(AddressChanged @event)
        {
            PersonAddress = new Address()
            {
                City = @event.City,
                Country = @event.Country,
                Street = @event.Street,
                ZipCode = @event.ZipCode
            };
        }
    }
}
