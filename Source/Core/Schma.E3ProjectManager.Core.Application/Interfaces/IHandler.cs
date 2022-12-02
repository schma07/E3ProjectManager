﻿using System.Threading.Tasks;

namespace Schma.E3ProjectManager.Core.Application
{
    /// <summary>
    /// Abstraction over a handler of messages coming from a message broker.
    /// Should implement any interfaces to comply with any given message brokers handler object
    /// </summary>
    /// <typeparam name="TRequest"></typeparam>
    /// <typeparam name="TResponse"></typeparam>
    public interface IHandler<TRequest, TResponse>
        where TRequest : class, IRequest<TResponse>
        where TResponse : class
    {
        Task<TResponse> HandleAsync(TRequest request);
    }
}