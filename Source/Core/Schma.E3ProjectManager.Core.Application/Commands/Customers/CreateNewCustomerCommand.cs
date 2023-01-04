using System.Collections.Generic;
using System.Threading.Tasks;
using Schma.E3ProjectManager.Common;
using Schma.E3ProjectManager.Core.Domain.Entities.CustomerAggregate;
using Schma.Messaging.Abstractions;

namespace Schma.E3ProjectManager.Core.Application.Commands
{
    public record CreateNewCustomerCommand(string Name) : IRequest<Result>
    {
    }

    /// <summary>
    /// Create Customer Command Handler
    /// </summary>
    public class CreateNewCustomerCommandHandler : BaseMessageHandler<CreateNewCustomerCommand, Result>
    
    {
        private readonly ICustomerRepository _customerRepository;

        public CreateNewCustomerCommandHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public override async Task<Result> HandleAsync(CreateNewCustomerCommand command)
        {
            var customer = new Customer(command.Name);            
            await _customerRepository.AddAsync(customer);
            await _customerRepository.UnitOfWork.SaveChangesAsync();
            await _customerRepository.SaveToEventStoreAsync(customer); //saving also to event store
            return new Result().Successful();
        }
    }
}