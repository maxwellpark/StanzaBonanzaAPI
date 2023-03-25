using StanzaBonanza.DataAccess.Repositories.Interfaces;
using StanzaBonanza.Models.Models;

namespace StanzaBonanza.DataAccess.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<TEntity> GetRepository<TEntity>() where TEntity : class;
        Task SaveChangesAsync();
        Task<Poem> AddPoemAsync(Poem poem);
    }
}
