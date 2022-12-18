using Newtonsoft.Json;
using Schma.Domain.Abstractions;

namespace Schma.EventStore.Abstractions
{
    public static class DomainEventHelper
    {
        public static readonly JsonSerializerSettings _jsonSerializerSettings = new JsonSerializerSettings { ContractResolver = new PrivateSetterContractResolver() };

        public static IDomainEvent<TAggregateId> ConstructDomainEvent<TAggregateId>(string data, string assemblyTypeName)
        {
            Type type = Type.GetType(assemblyTypeName);
            var domainEvent = (IDomainEvent<TAggregateId>)JsonConvert.DeserializeObject(data, type, _jsonSerializerSettings);
            return domainEvent;
        }
    }
}