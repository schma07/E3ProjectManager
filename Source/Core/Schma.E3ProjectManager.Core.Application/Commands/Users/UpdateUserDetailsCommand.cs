﻿using System.Threading.Tasks;
using AutoMapper;
using Schma.E3ProjectManager.Common;
using Schma.E3ProjectManager.Core.Application.DTOs;
using Schma.Messaging.Abstractions;

namespace Schma.E3ProjectManager.Core.Application.Commands
{
    public class UpdateUserDetailsCommand : IRequest<Result>
    {
        public UpdateUserDetailsCommand()
        {

        }

        public string Id { get; set; }
        public string Username { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PrimaryPhone { get; set; }
        public string SecondaryPhone { get; set; }
    }

    /// <summary>
    /// Update User Details Command Handler
    /// </summary>
    public class UpdateUserDetailsCommandHandler : BaseMessageHandler<UpdateUserDetailsCommand, Result>
    {
        private readonly IApplicationUserService _applicationUserService;
        private readonly IMapper _mapper;

        public UpdateUserDetailsCommandHandler()
        {

        }


        public UpdateUserDetailsCommandHandler(IApplicationUserService applicationUserService, IMapper mapper)
        {
            _applicationUserService = applicationUserService;
            _mapper = mapper;
        }

        public override async Task<Result> HandleAsync(UpdateUserDetailsCommand command)
        {
            return await _applicationUserService.UpdateUserDetails(_mapper.Map<UpdateUserDetailsDTO>(command));
        }
    }
}