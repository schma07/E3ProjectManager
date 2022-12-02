using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Schma.E3ProjectManager.Common;
using Schma.E3ProjectManager.Core.Application.ReadModels;
using Schma.E3ProjectManager.Core.Domain;
using Schma.E3ProjectManager.Core.Domain.Entities.UserAggregate;
using Schma.E3ProjectManager.Infrastructure.Models;

namespace Schma.E3ProjectManager.Infrastructure.Mappings
{
    internal class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<ApplicationUserRole, RoleEnum>()
                .ConvertUsing(r => r.Role.Name.ToEnum<RoleEnum>());

            CreateMap<User, ApplicationUser>()
                .ForMember(target => target.Roles, opt => opt.Ignore())
                .ForPath(target => target.PhoneNumber, source => source.MapFrom(m => m.PrimaryPhoneNumber));

            CreateMap<ApplicationUser, User>()
                .ForMember(target => target.PrimaryPhoneNumber, source => source.MapFrom(m => m.PhoneNumber));

            CreateMap<ApplicationUser, UserReadModel>();

            CreateMap<IdentityError, ResultError>()
                .ForMember(target => target.Error, opt => opt.MapFrom(source => source.Description));
        }
    }
}
