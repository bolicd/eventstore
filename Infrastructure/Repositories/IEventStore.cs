using System.Collections.Generic;
using System.Threading.Tasks;
using Tactical.DDD;

namespace Infrastructure.Repositories
{
    public interface IEventStore
    {
        Task SaveAsync(IEntityId aggregateId, 
            int originatingVersion, 
            IReadOnlyCollection<IDomainEvent> events,
            string aggregateName = "Aggregate Name");

        Task<IReadOnlyCollection<IDomainEvent>> LoadAsync(IEntityId aggregateRootId);
    }
}
