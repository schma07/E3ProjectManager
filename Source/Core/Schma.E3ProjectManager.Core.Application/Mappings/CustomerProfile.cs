using AutoMapper;
using Schma.E3ProjectManager.Core.Application.ReadModels.Customers;
using Schma.E3ProjectManager.Core.Domain.Entities.CustomerAggregate;

namespace Schma.E3ProjectManager.Core.Application.Mappings
{
    internal class CustomerProfile : Profile
    {
        public CustomerProfile()
        {
            CreateMap<Customer, CustomerReadModel>();                      
        }
    }
}
