﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Schma.Domain.Abstractions;
using Schma.EventStore.Abstractions;
using Schma.EventStore.EntityFramework.Entities;

namespace Schma.EventStore.EntityFramework
{
    /// <inheritdoc cref="IRetroactiveEventsService"/>
    /// <summary>
    /// Service to handle applying retroactive events to the event stream
    /// </summary>
    internal class EFRetroactiveEventsService : IRetroactiveEventsService
    {
        private readonly ILogger<EFRetroactiveEventsService> _logger;
        private readonly DbSet<BranchPointEntity> _branchPoints;
        private const string STREAM_NAME = "Main";

        public EFRetroactiveEventsService(ILogger<EFRetroactiveEventsService> logger, EventStoreDbContext eventStoreDbContext)
        {
            _logger = logger;
            _branchPoints = eventStoreDbContext.Set<BranchPointEntity>();
        }

        public IReadOnlyCollection<IDomainEvent<TAggregateId>> ApplyRetroactiveEventsToStream<T, TAggregateId>(IReadOnlyCollection<IDomainEvent<TAggregateId>> eventStream) where T : class, IAggregateRoot<TAggregateId>
        {
            var allBranchPointsForEvents = _branchPoints.Include(bp => bp.RetroactiveEvents).Where(bp => eventStream.Select(es => es.EventId).Contains(bp.EventId) && bp.Name == STREAM_NAME).ToList();
            if (!allBranchPointsForEvents.Any())
                return eventStream;

            var newEventStream = new List<IDomainEvent<TAggregateId>>();

            foreach (var @event in eventStream.OrderBy(e => e.AggregateVersion))
            {
                var branchPointForEvent = allBranchPointsForEvents.SingleOrDefault(bp => bp.EventId == @event.EventId);
                if (branchPointForEvent != null)
                {
                    //insert events for branch point
                    var currentBranchPointEvents = branchPointForEvent.RetroactiveEvents.OrderBy(re => re.Sequence);
                    newEventStream.AddRange(GetBranchPointEvents(@event, currentBranchPointEvents, branchPointForEvent.Type));
                }
                else
                    newEventStream.Add(@event);
            }
            return newEventStream;
        }

        private List<IDomainEvent<TAggregateId>> GetBranchPointEvents<TAggregateId>(IDomainEvent<TAggregateId> currentEvent, IOrderedEnumerable<RetroactiveEventEntity> retroactiveEvents, BranchPointTypeEnum branchPointType)
        {
            var events = new List<IDomainEvent<TAggregateId>>();
            switch (branchPointType)
            {
                case BranchPointTypeEnum.OutOfOrder:
                    {
                        events.Add(currentEvent);
                        events.AddRange(ConstructDomainEvents<TAggregateId>(retroactiveEvents));
                    }
                    break;
                case BranchPointTypeEnum.Incorrect: events.AddRange(ConstructDomainEvents<TAggregateId>(retroactiveEvents)); break;
                case BranchPointTypeEnum.Rejected:
                default: break;
            }
            return events;
        }

        private IEnumerable<IDomainEvent<TAggregateId>> ConstructDomainEvents<TAggregateId>(IOrderedEnumerable<RetroactiveEventEntity> retroactiveEvents)
        {
            return retroactiveEvents.Select(re => DomainEventHelper.ConstructDomainEvent<TAggregateId>(re.Data, re.AssemblyTypeName));
        }
    }
}