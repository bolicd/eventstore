using Tacta.EventStore.Domain;

namespace Core.Person.DomainEvents
{
    public class PersonCreated : DomainEvent
    {
        public string PersonId { get; }
        public string FirstName { get; }
        public string LastName { get; }

        public PersonCreated(
            string personId, 
            string firstName, 
            string lastName) : base(personId) 
        {
            PersonId = personId;
            FirstName = firstName;
            LastName = lastName;
        }
    }
}
