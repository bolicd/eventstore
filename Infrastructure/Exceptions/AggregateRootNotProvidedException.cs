using System;

namespace Infrastructure.Exceptions
{
    public class AggregateRootNotProvidedException : Exception
    {
        public AggregateRootNotProvidedException(string message) : base(message)
        {

        }
        
    }
}
