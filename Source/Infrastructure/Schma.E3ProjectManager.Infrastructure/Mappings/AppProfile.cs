using AutoMapper;
using Schma.E3ProjectManager.Core.Application.Mappings;
using Schma.E3ProjectManager.Core.Domain;
using Schma.E3ProjectManager.Core.Domain.Entities.OrderAggregate;
using Schma.E3ProjectManager.Core.Domain.Entities.ProjectAggregate;
using Schma.E3ProjectManager.Infrastructure.Models;

namespace Schma.E3ProjectManager.Infrastructure.Mappings
{
    internal class AppProfile : Profile
    {
        public AppProfile()
        {
            CreateMap<string, PhoneNumber>().ConvertUsing<StringToPhoneNumberConverter>();
            CreateMap<PhoneNumber, string>().ConvertUsing<PhoneNumberToStringConverter>();

            CreateMap<OrderItem, OrderItemEntity>().ReverseMap();
            CreateMap<Order, OrderEntity>()
                .ForMember(target => target.AddressCity, opt => opt.MapFrom(source => source.Address.City))
                .ForMember(target => target.AddressCountry, opt => opt.MapFrom(source => source.Address.Country))
                .ForMember(target => target.AddressLine1, opt => opt.MapFrom(source => source.Address.Line1))
                .ForMember(target => target.AddressLine2, opt => opt.MapFrom(source => source.Address.Line2))
                .ForMember(target => target.AddressPostcode, opt => opt.MapFrom(source => source.Address.Postcode));

            CreateMap<OrderEntity, Order>()
                .ForMember(target => target.Address, opt => opt.MapFrom(source => new Address() { City = source.AddressCity, Country = source.AddressCountry, Line1 = source.AddressLine1, Line2 = source.AddressLine2, Postcode = source.AddressPostcode }));

            CreateMap<ProjectEntity, Project>();
            CreateMap<ProjectDevice, ProjectDeviceEntity>().ReverseMap();
        }
    }
}
