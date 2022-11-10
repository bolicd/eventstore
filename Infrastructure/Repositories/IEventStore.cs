using System.Collections.Generic;
using System.Threading.Tasks;
using Tacta.EventStore.Domain;

namespace Infrastructure.Repositories
{
    public interface IEventStore
    {
        Task SaveAsync(EntityId aggregateId, 
            int originatingVersion, 
            IReadOnlyCollection<IDomainEvent> events,
            string aggregateName = "Aggregate Name");

        Task<IReadOnlyCollection<IDomainEvent>> LoadAsync(IEntityId aggregateRootId);
    }
}
