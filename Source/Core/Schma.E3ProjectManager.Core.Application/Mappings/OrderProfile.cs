using AutoMapper;
using Schma.E3ProjectManager.Core.Application.ReadModels;
using Schma.E3ProjectManager.Core.Application.ReadModels.Orders;
using Schma.E3ProjectManager.Core.Domain;
using Schma.E3ProjectManager.Core.Domain.Entities.OrderAggregate;

namespace Schma.E3ProjectManager.Core.Application.Mappings
{
    internal class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<Order, OrderReadModel>();
            CreateMap<OrderItem, OrderItemReadModel>();
            CreateMap<Address, AddressReadModel>();
        }
    }
}
