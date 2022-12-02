using System;
using System.Threading.Tasks;
using AutoMapper;
using Schma.E3ProjectManager.Core.Application;
using Schma.E3ProjectManager.Core.Domain.Entities.OrderAggregate;
using Schma.E3ProjectManager.Infrastructure.Models;

namespace Schma.E3ProjectManager.Infrastructure
{
    /// <summary>
    /// Implementation of <see cref="IOrderRepository"/> which allows persistence on both EventStore and relational store.
    /// </summary>
    internal class OrderRepository : EFRepository<Order, OrderEntity, Guid>, IOrderRepository
    {
        private readonly IESRepository<Order, Guid> _eventStoreRepository;

        public OrderRepository(IMapper mapper, IEntityRepository<OrderEntity, Guid> persistenceRepo, IESRepository<Order, Guid> esRepository)
            : base(mapper, persistenceRepo)
        {

            _eventStoreRepository = esRepository;
        }

        public async Task SaveToEventStoreAsync(Order order)
        {
            await _eventStoreRepository.SaveAsync(order);
        }
    }
}
