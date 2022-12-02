using AutoMapper;
using Schma.E3ProjectManager.Core.Application.Commands;
using Schma.E3ProjectManager.Core.Application.DTOs;

namespace Schma.E3ProjectManager.Core.Application.Mappings
{
    internal class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UpdateUserDetailsCommand, UpdateUserDetailsDTO>();
        }
    }
}