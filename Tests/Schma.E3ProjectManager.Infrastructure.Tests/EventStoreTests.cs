using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Schma.E3ProjectManager.Core.Application;
using Schma.E3ProjectManager.Core.Domain;
using Schma.E3ProjectManager.Infrastructure.Models;
using Schma.E3ProjectManager.Tests;
using Schma.EventStore.EntityFramework.Entities;
using Schma.EventStore.Abstractions;
using Xunit;

namespace Schma.E3ProjectManager.Infrastructure.Tests
{
    public class EventStoreTests : TestBase
    {
        private readonly IEventStore _eventStore;

        public EventStoreTests()
        {
            _eventStore = ServiceProvider.GetService<IEventStore>();
        }

        [Fact]
        public async Task EventStore_SaveAsync_SavesEventToStore()
        {
            string aggregateName = "some aggregate name";
            int aggregateVersion = 1;
            await _eventStore.SaveAsync(aggregateName, aggregateVersion, new OrderItemAddedEvent("some product name", 100, 1));

            var eventsFromStore = EventStoreContext.Events.ToList();

            Assert.NotEmpty(eventsFromStore);
        }

        [Fact]
        public async Task EventStore_LoadAsync_AggregateNotFound_ReturnsEmptyDomainEventList()
        {
            var events = await _eventStore.LoadAsync(Guid.NewGuid(), "Order", 0, 1);

            Assert.Empty(events);
        }

        [Fact]
        public async Task EventStore_LoadAsync_FromVersion_Negative_ThrowsArgumentException()
        {
            await Assert.ThrowsAsync<ArgumentException>(async () => await _eventStore.LoadAsync(Guid.NewGuid(), "", -1, 0)); ;
        }

        [Fact]
        public async Task EventStore_LoadAsync_ToVersion_Negative_ThrowsArgumentException()
        {
            await Assert.ThrowsAsync<ArgumentException>(async () => await _eventStore.LoadAsync(Guid.NewGuid(), "", 0, -1));
        }

        [Fact]
        public async Task EventStore_LoadAsync_ToVersionSmallerThanFromVersion_ThrowsArgumentException()
        {
            await Assert.ThrowsAsync<ArgumentException>(async () => await _eventStore.LoadAsync(Guid.NewGuid(), "", 6, 5));
        }

        [Fact]
        public async Task EventStore_LoadAsync_AggregateVersionNotFound_ReturnsEmptyDomainEventList()
        {
            Guid aggregateId = Guid.NewGuid();
            AddDummyEvent(typeof(ProjectCreatedEvent), aggregateId.ToString(), 0);
            var events = await _eventStore.LoadAsync(aggregateId, "Order", 10, 11);

            Assert.Empty(events);
        }

        [Fact]
        public async Task EventStore_LoadAsync_ReturnsDomainEventsForAggregate_NotEmpty()
        {
            Guid aggregateId = Guid.NewGuid();
            int aggergateVersion = 0;
            AddDummyEvent(typeof(ProjectCreatedEvent), aggregateId.ToString(), aggergateVersion);
            var events = await _eventStore.LoadAsync(aggregateId, "Order", aggergateVersion, 1);

            Assert.NotEmpty(events);
        }

        [Fact]
        public async Task EventStore_LoadAsync_ReturnsDomainEventsForAggregate_Events_ContainEventType()
        {
            Guid aggregateId = Guid.NewGuid();
            Type eventType = typeof(ProjectCreatedEvent);
            AddDummyEvent(eventType, aggregateId.ToString(), 0);
            var events = await _eventStore.LoadAsync(aggregateId, "Order", 0, 1);

            Assert.IsType(eventType, events.Single());
        }

        private void AddDummyEvent(Type type, string aggregateId, int version)
        {
            Guid eventId = Guid.NewGuid();
            EventStoreContext.Events.Add(
            new EventEntity
            {
                Id = eventId,
                AggregateName = "Order",
                AggregateId = aggregateId,
                CreatedAt = DateTime.UtcNow,
                Name = type.Name,
                AssemblyTypeName = type.AssemblyQualifiedName,
                Data = "{\"TrackingNumber\":\"TRACKING NUMBER HERE\",\"EventId\":\"" + eventId + "\",\"AggregateId\":\"" + aggregateId + "\",\"AggregateVersion\":" + version + "}",
                Version = version
            });
            EventStoreContext.SaveChanges();
        }
    }
}
