using Microsoft.Extensions.Logging;
using StanzaBonanza.DataAccess.DbContexts;
using StanzaBonanza.DataAccess.Factories;
using StanzaBonanza.DataAccess.Repositories.Interfaces;

namespace StanzaBonanza.DataAccess.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<UnitOfWork> _logger;
        private Dictionary<Type, object> _repositories;

        public UnitOfWork(ApplicationDbContext context, ILogger<UnitOfWork> logger)
        {
            _context = context;
            _logger = logger;
        }

        public IRepository<TEntity> GetRepository<TEntity>() where TEntity : class
        {
            _repositories ??= new Dictionary<Type, object>();

            var entityType = typeof(TEntity);

            // Create repository if not exists 
            if (!_repositories.ContainsKey(entityType))
            {
                _logger.LogInformation($"Creating new repository with generic type '{entityType}' as it doesn't exist.");
                _repositories[entityType] = RepositoryFactory.Create<TEntity>(_context);
            }

            return (IRepository<TEntity>)_repositories[entityType];
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
