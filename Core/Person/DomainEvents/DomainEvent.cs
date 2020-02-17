using System;
using Tactical.DDD;

namespace Core.Person.DomainEvents
{
    public class DomainEvent : IDomainEvent
    {

        public DomainEvent()
        {
            CreatedAt = DateTime.UtcNow;
        }

        public DateTime CreatedAt { get; set; }
    }
}
