﻿using System;
using Schma.Domain.Abstractions;

namespace Schma.E3ProjectManager.Core.Domain.Entities.OrderAggregate
{
    public class OrderItem : IEntity<Guid>
    {
        public Guid Id { get; private set; }
        public string ProductName { get; private set; }
        public decimal ProductPrice { get; private set; }
        public int Quantity { get; set; }

        public OrderItem(string productName, decimal productPrice, int quantity)
        {
            ProductName = productName;
            ProductPrice = productPrice;
            Quantity = quantity;
        }


        internal void UpdateQuantity(int quantity)
        {
            Quantity = quantity;
        }
    }
}
