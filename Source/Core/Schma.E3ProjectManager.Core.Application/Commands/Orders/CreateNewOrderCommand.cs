﻿using System.Threading.Tasks;
using Schma.E3ProjectManager.Common;
using Schma.E3ProjectManager.Core.Domain.Entities.OrderAggregate;

namespace Schma.E3ProjectManager.Core.Application.Commands
{
    public record CreateNewOrderCommand(string TrackingNumber) : IRequest<Result>
    {
    }

    /// <summary>
    /// Create New Order Command Handler
    /// </summary>
    public class CreateNewOrderCommandHandler : BaseMessageHandler<CreateNewOrderCommand, Result>
    {
        private readonly IOrderRepository _orderRepository;

        public CreateNewOrderCommandHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public override async Task<Result> HandleAsync(CreateNewOrderCommand command)
        {
            var order = new Order(command.TrackingNumber);
            order.AddOrderItem("Some product name", 10, 1);
            await _orderRepository.AddAsync(order);
            await _orderRepository.UnitOfWork.SaveChangesAsync();
            await _orderRepository.SaveToEventStoreAsync(order); //saving also to event store
            return new Result().Successful();
        }
    }
}
