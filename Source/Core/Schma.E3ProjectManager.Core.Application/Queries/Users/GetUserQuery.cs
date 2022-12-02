﻿using System.Threading.Tasks;
using AutoMapper;
using Schma.E3ProjectManager.Common;
using Schma.E3ProjectManager.Core.Application.ReadModels;

namespace Schma.E3ProjectManager.Core.Application.Queries
{
    public class GetUserQuery : IRequest<Result<UserReadModel>>
    {
        public string Id { get; set; }
    }

    public class GetUserQueryHandler : BaseMessageHandler<GetUserQuery, Result<UserReadModel>>
    {
        private readonly IApplicationUserService _applicationUserService;
        private readonly IMapper _mapper;

        public GetUserQueryHandler(IApplicationUserService applicationUserService, IMapper mapper)
        {
            _applicationUserService = applicationUserService;
            _mapper = mapper;
        }

        public override async Task<Result<UserReadModel>> HandleAsync(GetUserQuery query)
        {
            var result = new Result<UserReadModel>();
            var userResult = await _applicationUserService.GetUser(query.Id);

            if (userResult.Failed)
                result.Failed();
            else
                result.Successful().WithData(_mapper.Map<UserReadModel>(userResult.Data));

            return result;
        }
    }
}