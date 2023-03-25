using StanzaBonanza.DataAccess.DbContexts;
using StanzaBonanza.DataAccess.Repositories;
using StanzaBonanza.DataAccess.Repositories.Interfaces;
using StanzaBonanza.Models.Models;

namespace StanzaBonanza.DataAccess.Factories
{
    public static class RepositoryFactory
    {
        public static IRepository<TEntity> Create<TEntity>(ApplicationDbContext context) where TEntity : class
        {
            if (typeof(TEntity) == typeof(Author))
            {
                return new AuthorRepository(context) as IRepository<TEntity>;
            }
            else if (typeof(TEntity) == typeof(Poem))
            {
                return new PoemRepository(context) as IRepository<TEntity>;
            }
            else if (typeof(TEntity) == typeof(Poem_Author))
            {
                return new Poem_AuthorRepository(context) as IRepository<TEntity>;
            }
            return default;
        }
    }
}
