using System;
using System.Collections.Generic;
using System.Text;
using Tactical.DDD;

namespace Core.Person
{
    public class PersonId : EntityId
    {
        private Guid _guid;

        public PersonId()
        {
            _guid = Guid.NewGuid();
        }

        public PersonId(string id)
        {
            _guid = Guid.Parse(id);
        }

        public override string ToString() => _guid.ToString();
    }
}
