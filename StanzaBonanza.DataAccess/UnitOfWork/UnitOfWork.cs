using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using StanzaBonanza.DataAccess.DbContexts;
using StanzaBonanza.DataAccess.Factories;
using StanzaBonanza.DataAccess.Repositories.Interfaces;
using StanzaBonanza.Models.Models;
using System.Transactions;

namespace StanzaBonanza.DataAccess.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<UnitOfWork> _logger;
        private readonly Dictionary<Type, object> _repositories = new();

        public IRepository<Poem> PoemRepository => GetRepository<Poem>();
        public IRepository<Author> AuthorRepository => GetRepository<Author>();
        public IRepository<Poem_Author> Poem_AuthorRepository => GetRepository<Poem_Author>();

        public UnitOfWork(ApplicationDbContext context, ILogger<UnitOfWork> logger)
        {
            _context = context;
            _logger = logger;
        }

        public IRepository<TEntity> GetRepository<TEntity>() where TEntity : class
        {
            var entityType = typeof(TEntity);

            // Create repository if not exists 
            if (!_repositories.ContainsKey(entityType))
            {
                _logger.LogInformation($"Creating new repository with generic type '{entityType}' as it doesn't exist.");
                _repositories[entityType] = RepositoryFactory.Create<TEntity>(_context);
            }

            return (IRepository<TEntity>)_repositories[entityType];
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public async Task<Poem> AddPoemAsync(Poem poem)
        {
            using var scope = new TransactionScope(
                TransactionScopeOption.Required, new TransactionOptions
                {
                    IsolationLevel = IsolationLevel.ReadCommitted,
                }, TransactionScopeAsyncFlowOption.Enabled);

            try
            {
                // Make sure all repositories use the same db connection 
                using var sqlConnection = (SqlConnection)_context.Database.GetDbConnection();
                await sqlConnection.OpenAsync();

                // Add poem 
                await PoemRepository.AddAsync(poem);
                await SaveChangesAsync();

                // Get the poem's author 
                var author = await AuthorRepository.GetByIdAsync(poem.AuthorCreatorId);

                // Add junction table entity between the two 
                var poemAuthor = new Poem_Author(poem.PoemId, author.AuthorId);
                await Poem_AuthorRepository.AddAsync(poemAuthor);
                await SaveChangesAsync();

                // Commit the transaction
                scope.Complete();
                return poem;
            }
            catch (Exception ex)
            {
                // Handle any exceptions that occur during the transaction
                _logger.LogError(ex, "Error occurred during transaction scope when adding poem and author.");
                return null;

                // The transaction will be automatically rolled back if Complete is not called
            }
        }
    }
}
