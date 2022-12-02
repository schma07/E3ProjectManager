using AutoMapper;
using Schma.E3ProjectManager.Core.Domain;
using Schma.E3ProjectManager.Core.Domain.Entities.OrderAggregate;
using Schma.E3ProjectManager.Infrastructure.Models;

namespace Schma.E3ProjectManager.Infrastructure.Mappings
{
    internal class OrderAddressResolver : IValueResolver<OrderEntity, Order, Address>
    {
        public Address Resolve(OrderEntity source, Order destination, Address destMember, ResolutionContext context)
        {
            return new Address()
            {
                City = source?.AddressCity,
                Country = source?.AddressCountry,
                Postcode = source?.AddressPostcode,
                Line1 = source?.AddressLine1,
                Line2 = source?.AddressLine2
            };
        }
    }
}