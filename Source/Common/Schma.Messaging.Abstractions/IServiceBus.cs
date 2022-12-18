using System.Threading;
using System.Threading.Tasks;


namespace Schma.Messaging.Abstractions
{
    public interface IServiceBus
    {
        Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default) where TResponse : class;
    }
}