﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Schma.E3ProjectManager.Common;
using Schma.E3ProjectManager.Core.Application.ReadModels;
using Schma.Messaging.Abstractions;

namespace Schma.E3ProjectManager.Core.Application.Queries
{
    public class GetAllUsersQuery : IRequest<Result<IEnumerable<UserReadModel>>>
    {
        public QueryOptions Options { get; set; }
        public bool ApplyRoleFilter { get; set; } = true;
    }

    public class GetAllUsersQueryHandler : BaseMessageHandler<GetAllUsersQuery, Result<IEnumerable<UserReadModel>>>
    {
        private readonly IApplicationUserService _applicationUserService;
        private readonly IAuthenticatedUserService _authenticatedUserService;
        private readonly IMapper _mapper;

        public GetAllUsersQueryHandler(IApplicationUserService applicationUserService, IMapper mapper, IAuthenticatedUserService authenticatedUserService)
        {
            _applicationUserService = applicationUserService;
            _mapper = mapper;
            _authenticatedUserService = authenticatedUserService;
        }

        public override async Task<Result<IEnumerable<UserReadModel>>> HandleAsync(GetAllUsersQuery query)
        {
            var result = new Result<IEnumerable<UserReadModel>>();

            var getUsersResult = await _applicationUserService.GetAllUsers(query.Options);

            if (getUsersResult.Failed)
                result.Failed();
            else
            {
                var data = _mapper.Map<IEnumerable<UserReadModel>>(getUsersResult.Data);

                if (query.ApplyRoleFilter)
                {
                    var currentUserHighestRole = _authenticatedUserService.Roles.Max();
                    data = data.Where(u => u.Roles.Max() <= currentUserHighestRole);
                }

                result.Successful().WithData(data);
                result.AddMetadata("RecordCount", data.Count());
            }

            return result;
        }
    }
}