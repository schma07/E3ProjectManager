using System;
using System.Threading.Tasks;
using Schma.Data.Abstractions;
using Schma.E3ProjectManager.Core.Domain.Entities.CustomerAggregate;

namespace Schma.E3ProjectManager.Core.Application
{
    public interface ICustomerRepository : IAggregateRepository<Customer, Guid>
    {
        Task SaveToEventStoreAsync(Customer customer);
    }
}