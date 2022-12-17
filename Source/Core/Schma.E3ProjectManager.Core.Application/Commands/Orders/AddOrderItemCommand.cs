using System;
using System.Threading.Tasks;
using Schma.E3ProjectManager.Common;
using Schma.Messaging.Abstractions;

namespace Schma.E3ProjectManager.Core.Application.Commands
{
    public record AddOrderItemCommand(Guid OrderId, string ProductName, decimal ProductPrice, int Quantity) : IRequest<Result>
    {
    }

    /// <summary>
    /// Add Order Item Command Handler
    /// </summary>
    public class AddOrderItemCommandHandler : BaseMessageHandler<AddOrderItemCommand, Result>
    {
        private readonly IOrderRepository _orderRepository;

        public AddOrderItemCommandHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public override async Task<Result> HandleAsync(AddOrderItemCommand command)
        {
            var order = await _orderRepository.FindAsync(command.OrderId);
            order.AddOrderItem(command.ProductName, command.ProductPrice, command.Quantity);
            _orderRepository.Update(order);
            await _orderRepository.UnitOfWork.SaveChangesAsync();
            return new Result().Successful();
        }
    }
}
