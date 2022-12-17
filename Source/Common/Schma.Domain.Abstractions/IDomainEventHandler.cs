namespace Schma.Domain.Abstractions
{
    public interface IDomainEventHandler<T> where T : IDomainEvent
    {
        public void Apply(T @event);
    }
}
