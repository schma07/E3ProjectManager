using System.Linq;
using AutoMapper;

using Schma.E3ProjectManager.Core.Application.Commands;
using Schma.E3ProjectManager.Core.Application.DTOs;
using Schma.E3ProjectManager.Core.Application.Mappings;
using Schma.E3ProjectManager.Core.Application.ReadModels;
using Schma.E3ProjectManager.Core.Domain;
using Schma.E3ProjectManager.Core.Domain.Entities.UserAggregate;
using Schma.E3ProjectManager.Presentation.Web.ViewModels;

namespace Schma.E3ProjectManager.Presentation.Web.Mappings
{
    public class AppProfile : Profile
    {
        public AppProfile()
        {
            #region Commands

            CreateMap<CreateUserViewModel, CreateUserCommand>()
                .ForMember(target => target.Roles, opt => opt.MapFrom(m => m.Roles.Select(r => r.ToString())));
            CreateMap<EditUserViewModel, UpdateUserDetailsCommand>();
            CreateMap<EditUserViewModel, UpdateUserRolesCommand>()
                 .ForMember(target => target.Roles, opt => opt.MapFrom(m => m.Roles.Select(r => r.ToString())));

            #endregion

            #region User

            CreateMap<LoginViewModel, LoginRequestDTO>();
            CreateMap<UserReadModel, EditUserViewModel>();
            CreateMap<UserReadModel, UserDetailsModel>();
            CreateMap<User, UserReadModel>()
               .ForMember(target => target.LocalizedRoles, source => source.MapFrom<LocalizedRolesResolver>());

            #endregion

            CreateMap<string, PhoneNumber>().ConvertUsing<StringToPhoneNumberConverter>();
            CreateMap<PhoneNumber, string>().ConvertUsing<PhoneNumberToStringConverter>();
        }
    }
}
