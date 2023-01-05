using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Schma.E3ProjectManager.Common;
using Schma.E3ProjectManager.Core.Application.ReadModels.Customers;
using Schma.Messaging.Abstractions;

namespace Schma.E3ProjectManager.Core.Application.Queries.Customers
{
    public class GetAllCustomersQuery : IRequest<Result<List<CustomerReadModel>>>
    {
        public QueryOptions Options { get; set; }
    }

    public class GetAllCustomersQueryHandler : BaseMessageHandler<GetAllCustomersQuery, Result<List<CustomerReadModel>>>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;

        public GetAllCustomersQueryHandler(IMapper mapper, ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
        }

        public override async Task<Result<List<CustomerReadModel>>> HandleAsync(GetAllCustomersQuery query)
        {
            var result = new Result<List<CustomerReadModel>>();
            var customers = _customerRepository.GetAll(query.Options);

            result.Data = _mapper.Map<List<CustomerReadModel>>(customers);

            return result;
        }
    }
}