using System;
using Schma.Domain.Abstractions;

namespace Schma.E3ProjectManager.Core.Domain.Entities.CustomerAggregate
{
    public class CustomerProject : IEntity<Guid>
    {
        public Guid Id { get; private set; }
        public string ProductName { get; private set; }
        public decimal ProductPrice { get; private set; }
        public int Quantity { get; set; }

        public CustomerProject(string productName, decimal productPrice, int quantity)
        {
            ProductName = productName;
            ProductPrice = productPrice;
            Quantity = quantity;
        }
              
    }
}
