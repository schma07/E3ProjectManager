namespace Schma.E3ProjectManager.Core.Domain
{
    internal interface IDomainEventHandler<T> where T : IDomainEvent
    {
        internal void Apply(T @event);
    }
}
