using Tacta.EventStore.Domain;

namespace Core.Person.DomainEvents
{
    public class AddressChanged : DomainEvent
    {
        public string City { get;  }
        public string Country { get;  }
        public string ZipCode { get;  }
        public string Street { get;  }

        public AddressChanged(string city,
            string country,
            string zipcode,
            string street,
            string aggregateId) : base(aggregateId)
        {
            City = city;
            Country = country;
            ZipCode = zipcode;
            Street = street;
        }
    }
}

