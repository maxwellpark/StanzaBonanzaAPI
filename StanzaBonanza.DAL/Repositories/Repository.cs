using Microsoft.EntityFrameworkCore;
using StanzaBonanza.DataAccess.DbContexts;
using StanzaBonanza.DataAccess.Repositories.Interfaces;

namespace StanzaBonanza.DataAccess.Repositories
{
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly ApplicationDbContext Context;
        protected readonly DbSet<TEntity> DbSet;

        protected Repository(ApplicationDbContext context)
        {
            Context = context;
            DbSet = context.Set<TEntity>();
        }

        public abstract Task<IEnumerable<TEntity>> GetAllAsync();
        public abstract Task<TEntity> GetByIdAsync(int id);
    }
}
