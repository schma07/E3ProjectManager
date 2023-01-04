using System;
using System.Threading.Tasks;
using AutoMapper;
using Schma.Data.Abstractions;
using Schma.E3ProjectManager.Core.Application;
using Schma.E3ProjectManager.Core.Domain.Entities.CustomerAggregate;
using Schma.E3ProjectManager.Infrastructure.Models;

namespace Schma.E3ProjectManager.Infrastructure
{
    /// <summary>
    /// Implementation of <see cref="ICustomerRepository"/> which allows persistence on both EventStore and relational store.
    /// </summary>
    internal class CustomerRepository : EFRepository<Customer, CustomerEntity, Guid>, ICustomerRepository
    {
        private readonly IESRepository<Customer, Guid> _eventStoreRepository;

        public CustomerRepository(IMapper mapper, IEntityRepository<CustomerEntity, Guid> persistenceRepo, IESRepository<Customer, Guid> esRepository)
            : base(mapper, persistenceRepo)
        {

            _eventStoreRepository = esRepository;
        }

        public async Task SaveToEventStoreAsync(Customer customer)
        {
            await _eventStoreRepository.SaveAsync(customer);
        }
    }
}
