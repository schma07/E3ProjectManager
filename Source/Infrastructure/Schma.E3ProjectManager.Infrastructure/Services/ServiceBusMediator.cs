﻿using System.Threading;
using System.Threading.Tasks;
using MassTransit.Mediator;
using Schma.E3ProjectManager.Core.Application;
using Schma.Messaging.Abstractions;

namespace Schma.E3ProjectManager.Infrastructure.Services
{
    /// <summary>
    /// Abstraction over the implementation specifics of a message broker transmission
    /// Concrete implementation of <see cref="IServiceBus"/> which uses MassTransit's <see cref="IMediator"/>
    /// </summary>
    public class ServiceBusMediator : IServiceBus
    {
        private readonly IMediator _mediator;

        public ServiceBusMediator(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default) where TResponse : class
        {
            var client = _mediator.CreateRequestClient<IRequest<TResponse>>();
            cancellationToken.ThrowIfCancellationRequested();
            var response = await client.GetResponse<TResponse>(request, cancellationToken);
            return response.Message;
        }
    }
}