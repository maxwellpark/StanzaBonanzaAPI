using StanzaBonanza.DataAccess.Repositories.Interfaces;

namespace StanzaBonanza.DataAccess.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<TEntity> GetRepository<TEntity>() where TEntity : class;
        void SaveChanges();
    }
}
