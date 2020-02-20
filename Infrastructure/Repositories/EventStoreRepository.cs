using Dapper;
using Infrastructure.Factories;
using Infrastructure.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tactical.DDD;

namespace Infrastructure.Repositories
{
    public class EventStoreRepository : IEventStore
    {
        private string EventStoreTableName = "EventStore";

        private static string EventStoreListOfColumnsInsert = "[Id], [CreatedAt], [Version], [Name], [AggregateId], [Data], [Aggregate]";

        private static readonly string EventStoreListOfColumnsSelect = $"{EventStoreListOfColumnsInsert},[Sequence]";

        private readonly ISqlConnectionFactory _connectionFactory;

        private readonly JsonSerializerSettings _jsonSerializerSettings = new JsonSerializerSettings()
        {
            TypeNameHandling = TypeNameHandling.All,
            NullValueHandling = NullValueHandling.Ignore
        };

        public EventStoreRepository(ISqlConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }


        public async Task<IReadOnlyCollection<IDomainEvent>> LoadAsync(IEntityId aggregateRootId)
        {
            try
            {
                var query = new StringBuilder($@"SELECT {EventStoreListOfColumnsSelect} FROM {EventStoreTableName}");
                if (aggregateRootId != null) query.Append(" WHERE [AggregateId] = @AggregateId ");
                query.Append(" ORDER BY [Version] ASC;");

                using (var connection = _connectionFactory.SqlConnection())
                {
                    var events = (await connection.QueryAsync<EventStoreDao>(query.ToString(), aggregateRootId != null ? new { AggregateId = aggregateRootId.ToString() } : null)).ToList();
                    var domainEvents = events.Select(TransformEvent).Where(x => x != null).ToList().AsReadOnly();

                    return domainEvents;
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                throw;
            }

        }

        private IDomainEvent TransformEvent(EventStoreDao eventSelected)
        {
            var o = JsonConvert.DeserializeObject(eventSelected.Data, _jsonSerializerSettings);
            var evt = o as IDomainEvent;

            // TODO: 
            // Check if this is needed
            //if (evt != null)
            //{
            //    evt.Sequence = eventSelected.Sequence;
            //    evt.Version = eventSelected.Version;
            //}

            return evt;
        }


        public async Task SaveAsync(IEntityId aggregateId, int originatingVersion, IReadOnlyCollection<IDomainEvent> events, string aggregateName = "Aggregate Name")
        {
            if (events.Count == 0) return;

            var query =
                $@"INSERT INTO {EventStoreTableName} ({EventStoreListOfColumnsInsert})
                    VALUES (@Id,@CreatedAt,@Version,@Name,@AggregateId,@Data,@Aggregate);";

            var listOfEvents = events.Select(ev => new
            {
                Aggregate = aggregateName,
                ev.CreatedAt,
                Data = JsonConvert.SerializeObject(ev, Formatting.Indented, _jsonSerializerSettings),
                Id = Guid.NewGuid(),
                ev.GetType().Name,
                AggregateId = aggregateId.ToString(),
                Version = ++originatingVersion
            });

            using (var connection = _connectionFactory.SqlConnection())
            {
                await connection.ExecuteAsync(query, listOfEvents);
            }

        }
    }
}
