namespace Schma.E3ProjectManager.Infrastructure.Services
{
    public interface IDbInitializerService
    {
        void Migrate();
        void Seed();
    }
}
