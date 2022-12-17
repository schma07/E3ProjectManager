using System;
using System.Threading.Tasks;
using AutoMapper;
using Schma.E3ProjectManager.Common;
using Schma.E3ProjectManager.Core.Application.ReadModels.Orders;
using Schma.Messaging.Abstractions;

namespace Schma.E3ProjectManager.Core.Application.Queries.Orders
{
    public class GetOrderByIdQuery : IRequest<Result<OrderReadModel>>
    {
        public Guid OrderId { get; private set; }

        public GetOrderByIdQuery(Guid orderId)
        {
            OrderId = orderId;
        }
    }

    public class GetOrderByIdQueryHandler : BaseMessageHandler<GetOrderByIdQuery, Result<OrderReadModel>>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        public GetOrderByIdQueryHandler(IMapper mapper, IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        public override async Task<Result<OrderReadModel>> HandleAsync(GetOrderByIdQuery query)
        {
            var result = new Result<OrderReadModel>();
            var order = await _orderRepository.FindAsync(query.OrderId);

            result.Data = _mapper.Map<OrderReadModel>(order);

            return result;
        }
    }
}
