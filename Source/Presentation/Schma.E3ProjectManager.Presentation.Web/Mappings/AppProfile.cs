using System.Linq;
using AutoMapper;

using Schma.E3ProjectManager.Core.Application.Commands;
using Schma.E3ProjectManager.Core.Application.DTOs;
using Schma.E3ProjectManager.Core.Application.Mappings;
using Schma.E3ProjectManager.Core.Application.ReadModels;
using Schma.E3ProjectManager.Core.Application.ReadModels.Customers;
using Schma.E3ProjectManager.Core.Application.ReadModels.Projects;
using Schma.E3ProjectManager.Core.Domain;
using Schma.E3ProjectManager.Core.Domain.Entities.CustomerAggregate;
using Schma.E3ProjectManager.Core.Domain.Entities.ProjectAggregate;
using Schma.E3ProjectManager.Core.Domain.Entities.UserAggregate;
using Schma.E3ProjectManager.Presentation.Web.ViewModels;
using Schma.E3ProjectManager.Presentation.Web.ViewModels.Customers;
using Schma.E3ProjectManager.Presentation.Web.ViewModels.Projects;

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
            CreateMap<CreateUserViewModel, CreateUserCommand>();



            #endregion

            #region User

            CreateMap<LoginViewModel, LoginRequestDTO>();
            CreateMap<UserReadModel, EditUserViewModel>();
            CreateMap<UserReadModel, UserDetailsModel>();
            CreateMap<User, UserReadModel>()
               .ForMember(target => target.LocalizedRoles, source => source.MapFrom<LocalizedRolesResolver>());

            #endregion

            #region Customer

            CreateMap<Customer, CustomerReadModel>();
            CreateMap<CreateCustomerViewModel, CreateNewCustomerCommand>();
            
            #endregion

            #region Project

            CreateMap<Project, ProjectReadModel>();
            CreateMap<CreateProjectViewModel, CreateNewProjectCommand>();
            #endregion

            CreateMap<string, PhoneNumber>().ConvertUsing<StringToPhoneNumberConverter>();
            CreateMap<PhoneNumber, string>().ConvertUsing<PhoneNumberToStringConverter>();
        }
    }
}
