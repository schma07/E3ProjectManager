using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Schma.Domain.Abstractions;
using Schma.EventStore.Abstractions;
using Schma.EventStore.EntityFramework.Entities;

namespace Schma.EventStore.EntityFramework
{
    /// <summary>
    /// Implementation of <see cref="IEventStoreSnapshotProvider"/> using Entity Framework
    /// </summary>
    internal class EFEventStoreSnapshotProvider : IEventStoreSnapshotProvider
    {
        private readonly EventStoreDbContext _context;
        private readonly DbSet<AggregateSnapshotEntity> _snapshots;
        private readonly JsonSerializerSettings _jsonSerializerSettings = new JsonSerializerSettings { ContractResolver = new PrivateSetterContractResolver() };

        public EFEventStoreSnapshotProvider(EventStoreDbContext context)
        {
            _context = context;
            _snapshots = context.Set<AggregateSnapshotEntity>();
        }

        public async Task<T> GetAggregateFromSnapshotAsync<T, TAggregateId>(TAggregateId aggregateId, string aggregateName) where T : class, IAggregateRoot<TAggregateId>
        {
            var entity = await GetLatestSnapshotAsync(aggregateId, aggregateName);
            if (entity == null)
                return default;
            var aggregate = JsonConvert.DeserializeObject<T>(entity.Data, _jsonSerializerSettings);
            aggregate.ClearUncommittedEvents(); //to remove constructor creation event
            return aggregate;
        }

        public async Task SaveSnapshotAsync<T, TId>(T aggregate, Guid lastEventId) where T : class, IAggregateRoot<TId>
        {
            var newSnapshot = new AggregateSnapshotEntity()
            {
                Data = JsonConvert.SerializeObject(aggregate),
                AggregateId = aggregate.Id.ToString(),
                LastAggregateVersion = aggregate.Version,
                AggregateName = typeof(T).Name,
                LastEventId = lastEventId
            };
            _snapshots.Add(newSnapshot);
            await _context.SaveChangesAsync();
        }

        private Task<AggregateSnapshotEntity> GetLatestSnapshotAsync<TAggregateId>(TAggregateId aggregateId, string aggregateName)
        {
            return _snapshots.Where(snap => snap.AggregateId == aggregateId.ToString() && snap.AggregateName == aggregateName).OrderByDescending(a => a.LastAggregateVersion).FirstOrDefaultAsync();
        }
    }
}