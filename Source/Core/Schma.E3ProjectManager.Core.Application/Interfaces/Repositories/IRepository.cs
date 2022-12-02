namespace Schma.E3ProjectManager.Core.Application
{
    public interface IRepository
    {
        IUnitOfWork UnitOfWork { get; }
    }
}