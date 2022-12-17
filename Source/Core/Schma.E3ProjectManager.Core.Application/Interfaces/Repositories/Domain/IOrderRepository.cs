using System;
using System.Threading.Tasks;
using Schma.Data.Abstractions;
using Schma.E3ProjectManager.Core.Domain.Entities.OrderAggregate;

namespace Schma.E3ProjectManager.Core.Application
{
    public interface IOrderRepository : IAggregateRepository<Order, Guid>
    {
        Task SaveToEventStoreAsync(Order order);
    }
}