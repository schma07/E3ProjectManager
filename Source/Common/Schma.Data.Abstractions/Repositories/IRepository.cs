namespace Schma.Data.Abstractions
{
    public interface IRepository
    {
        IUnitOfWork UnitOfWork { get; }
    }
}